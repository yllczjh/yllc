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

                StreamReader reader = new StreamReader(HttpContext.Current.Request.GetBufferedInputStream());
                p = (JObject)JsonConvert.DeserializeObject(reader.ReadToEnd());
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

                #region clienttype 客户端标识
                IEnumerable<string> clienttype;
                if (!headers.TryGetValues("clienttype", out clienttype))
                {
                    Code.Result(ref msg, 编码.消息头错误, "缺少clienttype");
                    goto 退出;
                }
                msg.clienttype = clienttype.First();
                if (msg.clienttype != "wx" && msg.clienttype != "web" && msg.clienttype != "third")
                {
                    Code.Result(ref msg, 编码.消息头错误, "clienttype值无效:" + msg.clienttype);
                    goto 退出;
                }
                #endregion

                #region code 业务编码
                IEnumerable<string> code;
                if (!headers.TryGetValues("code", out code))
                {
                    Code.Result(ref msg, 编码.消息头错误, "缺少code");
                    goto 退出;
                }
                msg.code = code.First();
                if (string.IsNullOrEmpty(msg.code))
                {
                    Code.Result(ref msg, 编码.消息头错误, "code值无效");
                    goto 退出;
                }
                int i_基础业务 = 0;
                DataHelper.M_验证Code(msg.code, ref msg, out i_基础业务);
                if (msg.state != 0) goto 退出;
                #endregion

                #region customid 
                IEnumerable<string> customid;
                if (!headers.TryGetValues("customid", out customid))
                {
                    Code.Result(ref msg, 编码.消息头错误, "缺少customid");
                    goto 退出;
                }
                msg.customid = customid.First();
                if (string.IsNullOrEmpty(msg.customid))
                {
                    Code.Result(ref msg, 编码.消息头错误, "customid值无效");
                    goto 退出;
                }

                string accessToken = string.Empty;
                long accessPastTime = long.MinValue;
                string secret = string.Empty;
                //验证数据库中customid
                DataHelper.M_验证客户ID(msg.customid, ref msg, out accessToken, out accessPastTime, out secret);
                if (msg.state != 0) goto 退出;
                #endregion

                #region token、customid
                IEnumerable<string> token;
                if (!headers.TryGetValues("token", out token))
                {
                    Code.Result(ref msg, 编码.消息头错误, "缺少token");
                    goto 退出;
                }
                msg.token = token.First();
                if (!(msg.code == "login" || msg.code == "token"))
                {
                    UserModel userModel = (UserModel)HttpContext.Current.Session["UserModel"];
                    if (null != userModel)
                    {
                        if (null == userModel.userinfo && i_基础业务 == 0)
                        {
                            Code.Result(ref msg, 编码.用户身份错误, "用户未登录");
                            goto 退出;
                        }
                        if (null != userModel.token)
                        {
                            if (msg.clienttype != "third")
                            {
                                accessToken = userModel.token.accessToken;
                            }
                            if (accessToken != msg.token)
                            {
                                Code.Result(ref msg, 编码.Token错误, "请重新获取");
                                goto 退出;
                            }
                            else
                            {
                                if (msg.clienttype != "third")
                                {
                                    accessPastTime = userModel.token.accessPastTime;
                                }
                                if (DateTimeOffset.Now.ToUnixTimeMilliseconds() > accessPastTime)
                                {
                                    Code.Result(ref msg, 编码.Token错误, "Token已过期，请重新获取");
                                    goto 退出;
                                }
                            }
                        }
                        else
                        {
                            Code.Result(ref msg, 编码.Token错误, "请重新获取");
                            goto 退出;
                        }
                    }else
                    {
                        Code.Result(ref msg, 编码.Token错误, "请重新获取");
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
                if (Config.CeShi == "0")
                {
                    if ((DateTime.Now - dt_请求时间).TotalMinutes > 5)
                    {
                        Code.Result(ref msg, 编码.消息头错误, "请求消息已超时");
                        goto 退出;
                    }
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
                if (Config.CeShi == "0")
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

            退出: if (msg.state != 0)
            {
                ResponseModel res = new ResponseModel(msg);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, res);
                HttpRequestHeaders headers = actionContext.Request.Headers;
                RequestModel req = new RequestModel();
                req.clienttype = GetValue("clienttype", headers);
                req.code = GetValue("code", headers);
                req.customid = GetValue("customid", headers);
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
                foreach (KeyValuePair<string, DateTime> kv in Config.dic)
                {
                    if ((DateTime.Now - kv.Value).TotalHours > 1)
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