using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
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
        //[HttpGet]
        //public IHttpActionResult wxwebgrant()
        //{
        //    MessageModel msg = new MessageModel();
        //    string code = HttpContext.Current.Request.QueryString["code"];
        //    if (code != null)
        //    {
        //        HttpContext.Current.Session["code"] = code;
        //    }
        //    else
        //    {
        //        Code.Result(ref msg, 编码.其他错误, "获取code失败");
        //        return GetResponseString(msg);
        //    }
        //    UserModel webgrant = new UserModel();
        //    try
        //    {
        //        webgrant.getWxWebGrantByCode(code, ref msg);

        //    }
        //    catch (Exception ex)
        //    {
        //        Code.Result(ref msg, 编码.程序错误, ex.Message);
        //    }
        //    if (msg.success == 1)
        //    {
        //        TokenModel token = new TokenModel();
        //        webgrant.token = token;
        //        HttpContext.Current.Session["UserModel"] = webgrant;
        //        msg.dateset = token;
        //    }
        //    return GetResponseString(msg);
        //}

        //[HttpPost]
        //public IHttpActionResult login()
        //{
        //    MessageModel msg = this.Request.Properties["msg"] as MessageModel;
        //    try
        //    {
        //        if (msg.clienttype == "wx")
        //        {
        //            return RedirectWX();
        //        }
        //        else if (msg.clienttype == "web")
        //        {
        //            UserModel webModel = new UserModel();
        //            webModel.onlyid = GetRequestString("customid");
        //            TokenModel token = new TokenModel();
        //            webModel.token = token;
        //            HttpContext.Current.Session["UserModel"] = webModel;
        //            msg.dateset = token;
        //            return GetResponseString(msg);
        //        }
        //        else
        //        {
        //            Code.Result(ref msg, 编码.消息头错误, "无效的clienttype");
        //            return GetResponseString(msg);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Code.Result(ref msg, 编码.程序错误, e.Message);
        //        return GetResponseString(msg);
        //    }
        //}


        private IHttpActionResult process(dynamic p)
        {
            MessageModel msg = this.Request.Properties["msg"] as MessageModel;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                if (msg.clienttype == "wx" || msg.clienttype == "web")
                {
                    UserModel userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                    if ((null == userModel || string.IsNullOrEmpty(userModel.onlyid)) && Config.CeShi == "0")
                    {
                        if (msg.clienttype == "wx")
                        {
                            return RedirectWX();
                        }
                        else
                        {
                            Code.Result(ref msg, 编码.用户身份错误, "用户未登录");
                        }
                    }
                }

                if (msg.state == 0)
                {
                    dic = DataHelper.M_业务数据处理(ref msg, p);
                }
            }
            catch (Exception ex)
            {
                Code.Result(ref msg, 编码.程序错误, ex.Message);
            }
            return GetResponseString(msg, dic);
        }


        [HttpPost]
        public IHttpActionResult webapi(dynamic p)
        {
            MessageModel msg = this.Request.Properties["msg"] as MessageModel;
            TokenModel token;
            switch (msg.code)
            {
                case "login":
                    if (msg.clienttype == "wx") return RedirectWX();
                    if (msg.clienttype == "web")
                    {
                        Dictionary<string, object> param = Helper.DynamicToDictionary(p);
                        if (!DataHelper.M_验证用户信息(param, ref msg))
                        {
                            return GetResponseString(msg);
                        }

                        Dictionary<string, object> resultNew;
                        if (!param.ContainsKey("query"))
                        {
                            ArrayList list = new ArrayList() { new Dictionary<string, object>() { { "dataname", "userinfo" } }, new Dictionary<string, object>() { { "dataname", "menu" } } };
                            param.Add("query", list);
                            JObject ob = Helper.DictionaryToJObject(param);
                            resultNew = ((JsonResult<Dictionary<string, object>>)process(ob)).Content;
                        }
                        else
                        {
                            resultNew = ((JsonResult<Dictionary<string, object>>)process(p)).Content;
                        }

                        UserModel userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                        if (null == userModel)
                        {
                            userModel = new UserModel();
                        }
                        token = new TokenModel();
                        userModel.token = token;
                        userModel.userinfo = resultNew["userinfo"];
                        HttpContext.Current.Session["UserModel"] = userModel;
                        resultNew.Add("token", token);

                        return Json(resultNew);
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
                        UserModel userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                        if (null == userModel)
                        {
                            userModel = new UserModel();
                        }
                        userModel.token = token;
                        msg.dateset = token;
                        HttpContext.Current.Session["UserModel"] = userModel;
                    }

                    return GetResponseString(msg);
                default:
                    return process(p);
            }
            return null;
        }
    }
}
