using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Tool.Help;
using Tool.Helper;
using Tool.Model;
using WebAPI.Tool;

namespace WebAPI.filters
{
    public class AuthLdFilter : AuthorizationFilterAttribute
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

                string sign = ToolFunction.JsonValue(p, "sign")?.ToString();
                string timestamp = ToolFunction.JsonValue(p, "timestamp")?.ToString();
                string clientId = ToolFunction.JsonValue(p, "clientId")?.ToString();
                if (null == sign)
                {
                    Code.Result(ref msg, 编码.参数错误, "参数中缺少sign，或sign值无效");
                    goto 退出;
                }
                if (null == timestamp)
                {
                    Code.Result(ref msg, 编码.参数错误, "参数中缺少timestamp，或timestamp值无效");
                    goto 退出;
                }
                if (null == clientId)
                {
                    Code.Result(ref msg, 编码.参数错误, "参数中缺少clientId，或clientId值无效");
                    goto 退出;
                }
                if (clientId != Config.ClientId)
                {
                    Code.Result(ref msg, 编码.参数错误, "无效的clientId");
                    goto 退出;
                }

                DateTime dt_请求时间 = DateTime.Now.AddDays(-1);
                try
                {
                    dt_请求时间 = DateTime.ParseExact(timestamp, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                }
                catch (Exception)
                {
                    Code.Result(ref msg, 编码.参数错误, "timestamp格式错误");
                    goto 退出;
                }

                if ((DateTime.Now - dt_请求时间).TotalMinutes > 2)
                {
                    Code.Result(ref msg, 编码.参数错误, "请求消息已超时");
                    goto 退出;
                }

                string sign1 = EnHelper.EncryptForMD5(clientId + Config.Secret + timestamp);//32位小写
                if (sign != sign1)
                {
                    Code.Result(ref msg, 编码.参数错误, "签名错误");
                    goto 退出;
                }

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
                JObject outObject = new JObject();
                outObject.Add("success", false);
                outObject.Add("msg", msg.msgtext);
                outObject.Add("data", new JArray());
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, outObject);

                RequestModel req = new RequestModel();
                req.param = JsonConvert.SerializeObject(p);
                Log.Error(actionName, JsonConvert.SerializeObject(req) + ",msg=" + msg.msgtext);
            }
        }
    }
}