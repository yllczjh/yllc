using System;
using Tool.Helper;

namespace WebAPI.Models
{
    public class TokenModel
    {
        /// <summary>
        /// 请求token
        /// </summary>
        public string accessToken { get; set; }
        /// <summary>
        /// 请求token过期时间
        /// </summary>
        public long accessPastTime { get; set; }
       

        public TokenModel()
        {
            accessToken = Guid.NewGuid().ToString("N");
            //accessPastTime = DateTime.Now.AddMinutes(Config.AccessTokenTime).;
            accessPastTime=DateTimeOffset.Now.AddMinutes(Config.AccessTokenTime).ToUnixTimeMilliseconds();
        }
    }
}