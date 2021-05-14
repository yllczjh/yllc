using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Configuration;
using System.Net.Http;
using Tool.Help;
using Tool.Model;

namespace Tool.Helper
{
    //同步Nuget工具安装StackExchange.Redis.StrongName类库
    public static class Redis
    {
        // 127.0.0.1:6379
        private static readonly string conn = ConfigurationManager.ConnectionStrings["RedisExchangeHosts"].ConnectionString;
        private static object _locker = new Object();
        private static ConnectionMultiplexer _instance = null;

        /// <summary>
        /// 使用一个静态属性来返回已连接的实例，如下列中所示。这样，一旦 
        //ConnectionMultiplexer 断开连接，便可以初始化新的连接实例。
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = ConnectionMultiplexer.Connect(conn);
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 获取Redis数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDatabase GetDatabase()
        {
            return Instance.GetDatabase();
        }

        #region 注释
        //public static string HttpPost(string url, dynamic body)
        //{
        //    HttpResponseMessage result;
        //    string json;
        //    using (var client = new HttpClient())
        //    {
        //        MessageModel msg = new MessageModel();
        //        client.DefaultRequestHeaders.Add("msgid", Guid.NewGuid().ToString("N"));
        //        client.DefaultRequestHeaders.Add("customid", "1");
        //        client.DefaultRequestHeaders.Add("clienttype", "web");
        //        client.DefaultRequestHeaders.Add("token", "1");
        //        client.DefaultRequestHeaders.Add("reqtime", DateTime.Now.ToString("yyyyMMddHHmmss"));
        //        client.DefaultRequestHeaders.Add("code", "1001");
        //        client.DefaultRequestHeaders.Add("sign", EnHelper.GetRequsetSign(msg, body, Tool.Helper.Config.AppSecret));
        //        result = client.PostAsJsonAsync<JObject>(url, (JObject)body).Result;

        //        json = result.Content.ReadAsStringAsync().Result;
        //    }

        //    if (!Redis.GetDatabase().KeyExists("aaa"))
        //    {
        //        Redis.GetDatabase().StringSet("aaa", json);
        //    }
        //    string aa = Redis.GetDatabase().StringGet("aaa");
        //    return json;
        //}
        #endregion
    }
}