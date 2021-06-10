using Erp.Pro.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;

namespace Erp.Server.Helper
{
    public class HttpHelper
    {
        private readonly static object lockObj = new object();
        private static HttpHelper instance = null;
        private HttpClient client = new HttpClient();
        private string accessToken = string.Empty;
        private HttpHelper() { }

        public static HttpHelper HTTP
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new HttpHelper();

                        }
                    }
                }
                return instance;
            }
        }


        public JObject HttpPost(JObject body, string method)
        {
            StringContent httpContent = new StringContent(body?.ToString(), Encoding.UTF8, "application/json");

            if (client.DefaultRequestHeaders.Contains("msgid"))
            {
                client.DefaultRequestHeaders.Remove("msgid");
            }
            if (client.DefaultRequestHeaders.Contains("appid"))
            {
                client.DefaultRequestHeaders.Remove("appid");
            }
            if (client.DefaultRequestHeaders.Contains("token"))
            {
                client.DefaultRequestHeaders.Remove("token");
            }
            if (client.DefaultRequestHeaders.Contains("reqtime"))
            {
                client.DefaultRequestHeaders.Remove("reqtime");
            }
            if (client.DefaultRequestHeaders.Contains("method"))
            {
                client.DefaultRequestHeaders.Remove("method");
            }
            if (client.DefaultRequestHeaders.Contains("sign"))
            {
                client.DefaultRequestHeaders.Remove("sign");
            }
            client.DefaultRequestHeaders.Add("msgid", Guid.NewGuid().ToString("N"));
            client.DefaultRequestHeaders.Add("appid", "test");
            client.DefaultRequestHeaders.Add("token", accessToken);
            client.DefaultRequestHeaders.Add("reqtime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            client.DefaultRequestHeaders.Add("method", method);
            client.DefaultRequestHeaders.Add("sign", "12");

            HttpResponseMessage response = client.PostAsync(C_实体信息.C_共享变量.ServicesAddress, httpContent).Result;

            string statusCode = response.StatusCode.ToString();
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                JObject ob = (JObject)JsonConvert.DeserializeObject(result);
                if (null != ob.GetValue("token"))
                {
                    JObject t = (JObject)ob.GetValue("token");
                    accessToken = t.GetValue("accessToken").ToString();
                }
                return ob;
            }
            return null;
        }
    }
}
