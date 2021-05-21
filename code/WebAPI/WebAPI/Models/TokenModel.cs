using System;
using Tool.Help;
using Tool.Helper;
using Tool.Model;

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
       

        public TokenModel(MessageModel msg)
        {
            accessToken = EnHelper.EncryptForMD5(msg.msgid+ msg.customid+ msg.token+ msg.reqtime);
            accessPastTime =DateTimeOffset.Now.AddMinutes(Config.AccessTokenTime).ToUnixTimeMilliseconds();
        }
    }
}