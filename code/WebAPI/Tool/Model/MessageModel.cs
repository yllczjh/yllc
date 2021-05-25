using System;
using System.Collections;

namespace Tool.Model
{
    /// <summary>
    /// 消息参数类
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// 请求消息唯一ID
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 响应消息唯一ID
        /// </summary>
        public string remsgid { get; set; }
        /// <summary>
        /// 处理结果。0成功
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 处理消息
        /// </summary>
        public string msgtext { get; set; }
        /// <summary>
        /// 返回结果数据
        /// </summary>
        public object dataset { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 业务编码
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 客户端类别
        /// </summary>
        public string clienttype { get; set; }
        /// <summary>
        /// 请求时间戳
        /// </summary>
        public string reqtime { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        public MessageModel()
        {
            msgid = string.Empty;
            remsgid = Guid.NewGuid().ToString("N");
            errcode = 0;
            msgtext = "成功!";
            dataset = new ArrayList();
            appid = string.Empty;
            token = string.Empty;
            method = string.Empty;
            sign = string.Empty;
        }
    }

    public class ResponseModel
    {
        /// <summary>
        /// 请求消息唯一ID
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 响应消息唯一ID
        /// </summary>
        public string remsgid { get; set; }
        /// <summary>
        /// 处理结果。0成功
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 处理消息
        /// </summary>
        public string msgtext { get; set; }
        /// <summary>
        /// 返回结果数据
        /// </summary>
        public object dataset { get; set; }

        public ResponseModel(MessageModel msg)
        {
            this.msgid = msg.msgid;
            this.remsgid = msg.remsgid;
            this.errcode = msg.errcode;
            this.msgtext = "(" + msg.errcode + ")" + msg.msgtext;
            this.dataset = msg.dataset;
        }
    }
    public class RequestModel
    {
        /// <summary>
        /// 请求消息唯一ID
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 业务编码
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 客户端类别
        /// </summary>
        public string clienttype { get; set; }
        /// <summary>
        /// 请求时间戳
        /// </summary>
        public string reqtime { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public string param { get; set; }

        public RequestModel()
        {
            msgid = Guid.NewGuid().ToString("N");
            reqtime = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}