using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Tool.Help;
using Tool.Helper;
using Tool.Model;
using WebAPI.filters;
using WebAPI.Models;
using WebAPI.Tool;

namespace WebAPI.Controllers.v1
{
    [AuthFilter]
    [ActionFilter]
    public class MainController : BaseController
    {
        //[AllowAnonymous]//屏蔽AuthFilter过滤器
        [HttpPost]
        public IHttpActionResult webapi(dynamic p)
        {
            MessageModel msg = this.Request.Properties["msg"] as MessageModel;
            if (msg.errcode != 0)
            {
                return GetResponseString(msg, null);
            }

            Dictionary<string, object> result;
            UserModel userModel;
            try
            {
                switch (msg.method)
                {
                    case "login":
                        if (msg.clienttype == "wx") return RedirectWX();
                        if (msg.clienttype == "web")
                        {
                            result = DataHelper.Process(p, ref msg);
                            if (msg.errcode == 0)
                            {
                                userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                                if (null == userModel)
                                {
                                    userModel = new UserModel();
                                }
                                userModel.onlyid = p["username"];
                                userModel.userinfo = result;
                                TokenModel token = new TokenModel(msg);
                                userModel.token = token;
                                try
                                {
                                    ((result["dataset"] as ArrayList)[0] as Dictionary<string, object>).Add("accessToken", token.accessToken);
                                    ((result["dataset"] as ArrayList)[0] as Dictionary<string, object>).Add("accessPastTime", token.accessPastTime);
                                }
                                catch (System.Exception)
                                {
                                    result.Add("token", token);
                                }
                                HttpContext.Current.Session["UserModel"] = userModel;
                                HttpContext.Current.Session.Timeout = Config.AccessTokenTime;
                            }

                            return GetResponseString(msg, result);
                        }
                        if(msg.clienttype == "third")
                        {
                            userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                            if (null == userModel)
                            {
                                userModel = new UserModel();
                            }
                            userModel.onlyid = msg.appid;
                            userModel.userinfo = msg.appid;
                            TokenModel token = new TokenModel(msg);
                            userModel.token = token;
                            msg.dataset = new ArrayList() { token};

                            HttpContext.Current.Session["UserModel"] = userModel;
                            HttpContext.Current.Session.Timeout = Config.AccessTokenTime;
                            return GetResponseString(msg, null);
                        }
                        break;
                    case "logout":
                        userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                        if (null != userModel)
                        {
                            HttpContext.Current.Session["UserModel"] = null;
                        }
                        return GetResponseString(msg, null);

                    case "init":
                        userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                        if (null == userModel)
                        {
                            Code.Result(ref msg, 编码.用户身份错误, "未登录");
                            return GetResponseString(msg, null);
                        }
                        string json = "{\"username\": \"" + userModel.onlyid + "\"}";
                        JObject jo = (JObject)JsonConvert.DeserializeObject(json.ToString());
                        result = DataHelper.Process(jo, ref msg);
                        return GetResponseString(msg, result);
                    default:
                        result = DataHelper.Process(p, ref msg);
                        return GetResponseString(msg, result);
                }
            }
            catch (System.Exception e)
            {
                Code.Result(ref msg, 编码.程序错误, e.Message);
                return GetResponseString(msg, null);
            }
            return GetResponseString(msg, null);
        }
    }
}
