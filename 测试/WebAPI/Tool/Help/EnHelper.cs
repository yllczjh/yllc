using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Tool.Model;

namespace Tool.Help
{
    public class EnHelper
    {
        #region 签名验证
        /// <summary>
        /// 得到签名
        /// </summary>
        /// <param name="MsgID"></param>
        /// <param name="UserID"></param>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="parame"></param>
        /// <returns></returns>
        public static string GetRequsetSign(MessageModel msg, dynamic param, string secret)
        {
            string str_json = string.Empty;
            if (null != param)
            {
                str_json = JsonConvert.SerializeObject(param);
            }
            SortedDictionary<string, object> dic = new SortedDictionary<string, object>();
            dic.Add("msgid", msg.msgid);
            dic.Add("customid", msg.customid);
            dic.Add("token", msg.token);
            dic.Add("code", msg.code);
            dic.Add("clienttype", msg.clienttype);
            dic.Add("reqtime", msg.reqtime);
            dic.Add("param", str_json);
            var dicstr = dic.Select(kv => kv.Key + "=" + kv.Value);
            string p = string.Join("&", dicstr) + secret;
            return EncryptForMD5(p);
        }

        #endregion

        #region 加密相关
        static RSACryptoServiceProvider oRSA = new RSACryptoServiceProvider();

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Encrypt(string publickey, string content)
        {
            //得到公钥
            oRSA.FromXmlString(publickey);
            //把你要加密的内容转换成byte[]
            byte[] PlainTextBArray = Encoding.UTF8.GetBytes(content);
            //使用.NET中的Encrypt方法加密
            byte[] CypherTextBArray = oRSA.Encrypt(PlainTextBArray, false);
            //最后把加密后的byte[]转换成Base64String，这里就是加密后的内容了
            string EncryptedContent = Convert.ToBase64String(CypherTextBArray);
            return EncryptedContent;
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Decrypt(string privatekey, string content)
        {
            //得到私钥
            oRSA.FromXmlString(privatekey);
            //把原来加密后的String转换成byte[]
            byte[] PlainTextBArray = Convert.FromBase64String(content);
            //使用.NET中的Decrypt方法解密
            byte[] DypherTextBArray = oRSA.Decrypt(PlainTextBArray, false);
            //转换解密后的byte[]，这就得到了我们原来的加密前的内容了
            string EncryptedContent = Encoding.UTF8.GetString(DypherTextBArray);
            return EncryptedContent;
        }

        /// <summary>
        /// MD5 加签
        /// </summary>
        /// <param name="encryptString">待加签的字符串</param>
        /// <returns></returns>
        public static string EncryptForMD5(string encryptString)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(encryptString);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        #endregion
    }
}
