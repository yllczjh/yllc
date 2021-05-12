using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Tool.Help;
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
            if (msg.state != 0)
            {
                GetResponseString(msg);
            }
            TokenModel token;
            Dictionary<string, object> result;
            UserModel userModel;
            switch (msg.code)
            {
                case "login":
                    if (msg.clienttype == "wx") return RedirectWX();
                    if (msg.clienttype == "web")
                    {
                        result = DataHelper.Process(p, ref msg);
                        if (msg.state == 0)
                        {
                            userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                            if (null == userModel)
                            {
                                userModel = new UserModel();
                            }
                            userModel.onlyid = p["username"];
                            userModel.userinfo = result;
                            HttpContext.Current.Session["UserModel"] = userModel;
                        }

                        return GetResponseString(msg, result);
                    }
                    break;

                case "token":
                    token = new TokenModel();
                    if (msg.clienttype == "third")
                    {
                        if (DataHelper.M_更新Token(token, GetRequestString("customid")) == 1)
                        {
                            msg.dateset = token;
                        }
                        else
                        {
                            Code.Result(ref msg, 编码.程序错误, "获取token失败");
                        }
                    }
                    else
                    {
                        userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                        if (null == userModel)
                        {
                            userModel = new UserModel();
                        }
                        userModel.token = token;
                        msg.dateset = token;
                        HttpContext.Current.Session["UserModel"] = userModel;
                    }

                    return GetResponseString(msg);
                case "init":
                    userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                    string json = "{\"username\": \"" + userModel.onlyid + "\"}";
                    JObject jo = (JObject)JsonConvert.DeserializeObject(json.ToString());
                    result = DataHelper.Process(jo, ref msg);
                    return GetResponseString(msg, result);
                default:
                    result = DataHelper.Process(p, ref msg);
                    return GetResponseString(msg, result);
            }
            return null;
        }
    }
}
