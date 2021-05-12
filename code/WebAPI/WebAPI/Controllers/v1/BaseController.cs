using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public JsonResult<ResponseModel> GetResponseString(MessageModel msg)
        {
            ResponseModel res = new ResponseModel(msg);
            return Json(res);
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
            dic.Add("state", msg.state);
            dic.Add("msgtext", "("+ msg.state + ")"+msg.msgtext);
            //dic.Add("result", dic_返回数据);
            if (msg.state == 0 && null != dic_返回数据)
            {
                foreach (KeyValuePair<string, object> pair in dic_返回数据)
                {
                    if (!string.IsNullOrEmpty(pair.Key))
                    {
                        dic.Add(pair.Key, pair.Value);
                    }
                }
            }
            return Json(dic);
        }
        public IHttpActionResult RedirectWX()
        {
            string appid = GetRequestString("customid");
            string base_url = Config.BaseURL;
            string url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" +
                $@"{appid}&redirect_uri={base_url}WebAPI/wxwebgrant&response_type=code&scope=snsapi_userinfo&state=123#wechat_redirect";
            //snsapi_userinfo       snsapi_base
            return Redirect(url);
        }
    }
}
