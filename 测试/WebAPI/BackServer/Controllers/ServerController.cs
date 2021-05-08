using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Web.Http;
using Tool.Help;
using Tool.Helper;
using Tool.Model;

namespace BackServer.Controllers
{
    public class ServerController : ApiController
    {
        [HttpPost]
        public IHttpActionResult proc(dynamic p)
        {
            //HttpPost("http://localhost:61639/api/v1/main/process", p);
            return HttpPost("http://192.168.88.88:8010/api/v1/main/process", p);
        }



        public string HttpPost(string url, dynamic body)
        {
            HttpResponseMessage result;
            string json;
            using (var client = new HttpClient())
            {
                MessageModel msg = new MessageModel();
                client.DefaultRequestHeaders.Add("msgid", Guid.NewGuid().ToString("N"));
                client.DefaultRequestHeaders.Add("customid", "1");
                client.DefaultRequestHeaders.Add("clienttype", "web");
                client.DefaultRequestHeaders.Add("token", "1");
                client.DefaultRequestHeaders.Add("reqtime", DateTime.Now.ToString("yyyyMMddHHmmss"));
                client.DefaultRequestHeaders.Add("code", "1001");
                client.DefaultRequestHeaders.Add("sign", EnHelper.GetRequsetSign(msg, body, Tool.Helper.Config.AppSecret));
                result = client.PostAsJsonAsync<JObject>(url, (JObject)body).Result;

                json= result.Content.ReadAsStringAsync().Result;
            }



            if (!RedisHelper.GetDatabase().KeyExists("aaa"))
            {
                RedisHelper.GetDatabase().StringSet("aaa", json);
            }
            string aa = RedisHelper.GetDatabase().StringGet("aaa");
            return json;

            
        }
    }
}
