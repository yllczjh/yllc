using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Server.WebAPI
{
   public class HttpHelper
    {
        private readonly static object lockObj = new object();
        private static HttpHelper instance = null;
        private HttpClient client = new HttpClient();
        private string webURL = "http://test7.ql-soft.com/api/v1/main/webapi";
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

        
        public string HttpPost(JObject body,string method)
        {
            HttpContent httpContent = new StringContent(body.ToString());

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
            client.DefaultRequestHeaders.Add("appid", "mainapp");
            client.DefaultRequestHeaders.Add("token", "1");
            client.DefaultRequestHeaders.Add("reqtime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            client.DefaultRequestHeaders.Add("method", method);
            client.DefaultRequestHeaders.Add("sign", "12");

            HttpResponseMessage response = client.PostAsync(webURL, httpContent).Result;

            string statusCode = response.StatusCode.ToString();
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }
    }
}
