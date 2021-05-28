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
            TokenModel token;
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
                                token = new TokenModel(msg);
                                userModel.token = token;

                                result.Add("token", token);

                                HttpContext.Current.Session["UserModel"] = userModel;
                                HttpContext.Current.Session.Timeout = Config.AccessTokenTime;
                            }

                            return GetResponseString(msg, result);
                        }
                        if (msg.clienttype == "third")
                        {
                            userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                            if (null == userModel)
                            {
                                userModel = new UserModel();
                            }
                            userModel.onlyid = msg.appid;
                            userModel.userinfo = msg.appid;
                            token = new TokenModel(msg);
                            userModel.token = token;

                            HttpContext.Current.Session["UserModel"] = userModel;
                            HttpContext.Current.Session.Timeout = Config.AccessTokenTime;

                            result = new Dictionary<string, object>();
                            result.Add("dataset", new ArrayList());
                            result.Add("token", token);
                            return GetResponseString(msg, result);
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
                        userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                        if (null == userModel)
                        {
                            if (Config.YanZheng == "1")
                            {
                                Code.Result(ref msg, 编码.用户身份错误, "未登录");
                                return GetResponseString(msg, null);
                            }
                            else
                            {
                                userModel = new UserModel();
                            }
                        }

                        token = userModel.token;
                        if (null == token)
                        {
                            if (Config.YanZheng == "1")
                            {
                                Code.Result(ref msg, 编码.用户身份错误, "未登录");
                                return GetResponseString(msg, null);
                            }
                            else
                            {
                                token = new TokenModel(msg);
                            }
                        }
                        else
                        {
                            token.RefreshAccessTokenTime();
                        }

                        userModel.token = token;
                        HttpContext.Current.Session["UserModel"] = userModel;

                        result = DataHelper.Process(p, ref msg);
                        if(null== result)
                        {
                            result = new Dictionary<string, object>();
                        }
                        result.Add("token", token);
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
