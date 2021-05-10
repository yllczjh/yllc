using StackExchange.Redis;
using System;
using System.Configuration;

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
    }
}