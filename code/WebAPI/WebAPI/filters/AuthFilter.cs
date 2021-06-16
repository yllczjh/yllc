using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Tool.Help;
using Tool.Helper;
using Tool.Model;
using WebAPI.Models;
using WebAPI.Tool;

namespace WebAPI.filters
{
    public class AuthFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //如果用户方位的Action带有AllowAnonymousAttribute，则不进行授权验证   [AllowAnonymous]
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            //检查当前请求的 Controller 是否有[AllowAnonymous],有的话则直接返回,不再进行下面的验证
            if (actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            JObject p = null;
            MessageModel msg = new MessageModel();
            string controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string actionName = actionContext.ActionDescriptor.ActionName.ToLower();//process、webapi
            try
            {
                #region 验证

                try
                {
                    StreamReader reader = new StreamReader(HttpContext.Current.Request.GetBufferedInputStream());
                    p = (JObject)JsonConvert.DeserializeObject(reader.ReadToEnd());
                }
                catch (Exception)
                {
                    Code.Result(ref msg, 编码.参数错误, "解析消息内容失败,请检查是否为正确的Json格式");
                    Log.Error("DataValidation", "解析消息内容失败,请检查是否为正确的Json格式");
                    goto 退出;
                }

                HttpRequestHeaders headers = actionContext.Request.Headers;

                #region msgid 消息ID
                IEnumerable<string> msgid;
                if (!headers.TryGetValues("msgid", out msgid))
                {
                    Code.Result(ref msg, 编码.消息头错误, "缺少msgid");
                    goto 退出;
                }
                msg.msgid = msgid.First();
                if (string.IsNullOrEmpty(msg.msgid))
                {
                    Code.Result(ref msg, 编码.消息头错误, "msgid值无效");
                    goto 退出;
                }
                if (Config.dic.ContainsKey(msg.msgid))
                {
                    Code.Result(ref msg, 编码.其他错误, "重复请求");
                    goto 退出;
                }
                #endregion

                #region method 业务编码
                IEnumerable<string> method;
                if (!headers.TryGetValues("method", out method))
                {
                    Code.Result(ref msg, 编码.消息头错误, "缺少method");
                    goto 退出;
                }
                msg.method = method.First();
                if (string.IsNullOrEmpty(msg.method))
                {
                    Code.Result(ref msg, 编码.消息头错误, "method值无效");
                    goto 退出;
                }

                #endregion

                #region appid 
                IEnumerable<string> appid;
                if (!headers.TryGetValues("appid", out appid))
                {
                    Code.Result(ref msg, 编码.消息头错误, "缺少appid");
                    goto 退出;
                }
                msg.appid = appid.First();
                if (string.IsNullOrEmpty(msg.appid))
                {
                    Code.Result(ref msg, 编码.消息头错误, "appid值无效");
                    goto 退出;
                }

                string clienttype = string.Empty;
                string secret = string.Empty;
                //验证数据库中appid
                DataHelper.M_验证客户ID(msg.appid, ref msg, out clienttype, out secret);
                if (msg.errcode != 0) goto 退出;
                msg.clienttype = clienttype;
                #endregion

                #region token、appid
                IEnumerable<string> token;
                if (!headers.TryGetValues("token", out token))
                {
                    Code.Result(ref msg, 编码.消息头错误, "缺少token");
                    goto 退出;
                }

                msg.token = token.First();

                if (ToolFunction.VerifyLogin(msg.method))
                {
                    UserModel userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                    if (null != userModel)
                    {
                        if ((string.IsNullOrEmpty(userModel.onlyid)))
                        {
                            Code.Result(ref msg, 编码.用户身份错误, "用户未登录");
                            goto 退出;
                        }
                        if (null != userModel.token)
                        {
                            if (userModel.token.accessToken != msg.token)
                            {
                                Code.Result(ref msg, 编码.用户身份错误, "无效的token");
                                goto 退出;
                            }
                            else
                            {
                                if (DateTimeOffset.Now.ToUnixTimeMilliseconds() > userModel.token.accessPastTime)
                                {
                                    Code.Result(ref msg, 编码.用户身份错误, "Token已过期");
                                    goto 退出;
                                }
                            }
                        }
                        else
                        {
                            Code.Result(ref msg, 编码.用户身份错误, "无效的Token");
                            goto 退出;
                        }
                    }
                    else
                    {
                        Code.Result(ref msg, 编码.用户身份错误, "未登录");
                        goto 退出;
                    }
                }

                #endregion

                #region reqtime 请求时间
                IEnumerable<string> reqtime;
                if (!headers.TryGetValues("reqtime", out reqtime))
                {
                    Code.Result(ref msg, 编码.消息头错误, "缺少reqtime");
                    goto 退出;
                }
                msg.reqtime = reqtime.First();

                DateTime dt_请求时间 = DateTime.Now.AddDays(-1);
                try
                {
                    dt_请求时间 = DateTime.ParseExact(msg.reqtime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                }
                catch (Exception)
                {
                    Code.Result(ref msg, 编码.消息头错误, "reqtime格式错误");
                    goto 退出;
                }

                if ((DateTime.Now - dt_请求时间).TotalMinutes > 5 && Config.YanZheng == "1")
                {
                    Code.Result(ref msg, 编码.消息头错误, "请求消息已超时");
                    goto 退出;
                }


                #endregion

                #region sign 签名
                IEnumerable<string> sign;
                if (!headers.TryGetValues("sign", out sign))
                {
                    Code.Result(ref msg, 编码.消息头错误, "缺少sign");
                    goto 退出;
                }
                msg.sign = sign.First();
                if (string.IsNullOrEmpty(msg.sign))
                {
                    Code.Result(ref msg, 编码.消息头错误, "sign值无效");
                    goto 退出;
                }
                if (Config.YanZheng == "1")
                {
                    string Sign = EnHelper.GetRequsetSign(msg, p, secret);
                    if (msg.sign != Sign)
                    {
                        Code.Result(ref msg, 编码.消息头错误, "签名错误");
                        goto 退出;
                    }
                }

                #endregion

                #endregion
            }
            catch (Exception e)
            {
                Code.Result(ref msg, 编码.程序错误, e.Message);
                Log.Error("DataValidation", e.Message);
                goto 退出;
            }

            退出: if (msg.errcode != 0)
            {
                ResponseModel res = new ResponseModel(msg);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, res);
                HttpRequestHeaders headers = actionContext.Request.Headers;
                RequestModel req = new RequestModel();
                req.clienttype = GetValue("clienttype", headers);
                req.method = GetValue("method", headers);
                req.appid = GetValue("appid", headers);
                req.msgid = GetValue("msgid", headers);
                req.reqtime = GetValue("reqtime", headers);
                req.sign = GetValue("sign", headers);
                req.token = GetValue("token", headers);
                req.param = JsonConvert.SerializeObject(p);

                Log.Error(actionName, JsonConvert.SerializeObject(req));
            }
            else
            {
                actionContext.Request.Properties["msg"] = msg;
                Config.dic.Add(msg.msgid, DateTime.Now);
                foreach (var kv in Config.dic.ToList())
                {
                    if ((DateTime.Now - kv.Value).TotalMinutes > 10)
                    {
                        Config.dic.Remove(kv.Key);
                    }
                }
            }
        }
        private string GetValue(string name, HttpRequestHeaders headers)
        {
            if (headers.Contains(name))
            {
                return headers.GetValues(name).First();
            }
            else
            {
                return "NOT_FOUND";
            }
        }
    }
}