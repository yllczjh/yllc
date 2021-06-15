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
        public IHttpActionResult webapi(JObject p)
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
                if (msg.method.Contains("login"))
                {
                    if (msg.clienttype == "wx") return RedirectWX();
                    if (msg.clienttype == "web")
                    {
                        if (null != p["login"])
                        {
                            p.Remove("login");
                        }
                        if (null != p["appid"])
                        {
                            p.Remove("appid");
                        }
                        p.Add("login", "1");
                        p.Add("appid", msg.appid);
                        result = DataHelper.Process(p, ref msg);
                        if (msg.errcode == 0)
                        {
                            userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                            if (null == userModel)
                            {
                                userModel = new UserModel();
                            }
                            userModel.onlyid = p["username"].ToString();
                            userModel.loginmethod = msg.method;
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
                        userModel.onlyid = p["username"]?.ToString();
                        userModel.loginmethod = msg.method;
                        token = new TokenModel(msg);
                        userModel.token = token;

                        HttpContext.Current.Session["UserModel"] = userModel;
                        HttpContext.Current.Session.Timeout = Config.AccessTokenTime;

                        result = new Dictionary<string, object>();
                        result.Add("dataset", new ArrayList());
                        result.Add("token", token);
                        return GetResponseString(msg, result);
                    }
                }
                else if (msg.method == "logout")
                {
                    userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                    if (null != userModel)
                    {
                        HttpContext.Current.Session["UserModel"] = null;
                    }
                    return GetResponseString(msg, null);
                }
                else if (msg.method == "init")
                {
                    userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                    token = userModel.token;//获取session-token
                    token.RefreshAccessTokenTime();//刷新token过期时间
                    userModel.token = token;//写回session
                    HttpContext.Current.Session["UserModel"] = userModel;

                    JObject jo = new JObject();
                    jo.Add("username", userModel.onlyid);
                    jo.Add("password", "1");//无意义 占位用
                    jo.Add("login", "0");//标识初始化信息，不验证用户名密码
                    jo.Add("appid", msg.appid);
                    msg.method = userModel.loginmethod;//初始化 调用登录方法
                    result = DataHelper.Process(jo, ref msg);
                    if (null == result)
                    {
                        result = new Dictionary<string, object>();
                    }
                    result.Add("token", token);
                    return GetResponseString(msg, result);
                }
                else
                {
                    if (null != p["appid"])
                    {
                        p.Remove("appid");
                    }
                    p.Add("appid", msg.appid);
                    result = DataHelper.Process(p, ref msg);
                    if (ToolFunction.VerifyLogin(msg.method))
                    {
                        userModel = (UserModel)HttpContext.Current.Session["UserModel"];//获取session
                        token = userModel.token;//获取session-token
                        token.RefreshAccessTokenTime();//刷新token过期时间
                        userModel.token = token;//写回session
                        HttpContext.Current.Session["UserModel"] = userModel;
                        if (null == result)
                        {
                            result = new Dictionary<string, object>();
                        }
                        result.Add("token", token);
                    }

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
