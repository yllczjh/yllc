using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Tool.Helper;
using Tool.Model;

namespace WebAPI.Controllers.v1
{
    public class BaseController : ApiController
    {
        public string GetRequestString(string name)
        {
            return GetRequestString(name, this.Request);
        }
        public string GetRequestString(string name, HttpRequestMessage request)
        {
            if (request == null)
                request = this.Request;
            return request.Headers.GetValues(name).First();
        }
        /// <summary>
        /// 获取返回json对象
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public JsonResult<Dictionary<string, object>> GetResponseString(MessageModel msg, Dictionary<string, object> dic_返回数据)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("msgid", msg.msgid);
            dic.Add("remsgid", msg.remsgid);
            dic.Add("errcode", msg.errcode);
            dic.Add("msgtext", "(" + msg.errcode + ")" + msg.msgtext);
            if (null != dic_返回数据 && dic_返回数据.Count > 0)
            {
                foreach (KeyValuePair<string, object> pair in dic_返回数据)
                {
                    if (!string.IsNullOrEmpty(pair.Key))
                    {
                        dic.Add(pair.Key, pair.Value);
                    }
                }
            }
            else
            {
                dic.Add("dataset", msg.dataset);
            }
            Encoding utf8 = Encoding.GetEncoding(65001);
            byte[] temp = utf8.GetBytes("yyyy-MM-dd hh:mm:ss");
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
                new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                {
                    DateTimeFormat = utf8.GetString(temp)
                }
            );

            return Json(dic, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
        }
        public static JToken JsonValue(JObject p, string name)
        {
            if (null == p)
            {
                return null;
            }
            return p.GetValue(name, StringComparison.InvariantCultureIgnoreCase);
        }
        public IHttpActionResult RedirectWX()
        {
            string appid = GetRequestString("appid");
            string base_url = Config.BaseURL;
            string url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" +
                $@"{appid}&redirect_uri={base_url}WebAPI/wxwebgrant&response_type=code&scope=snsapi_userinfo&state=123#wechat_redirect";
            //snsapi_userinfo       snsapi_base
            return Redirect(url);
        }
    }
}
