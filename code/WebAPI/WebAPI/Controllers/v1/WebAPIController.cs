using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Tool.Help;
using Tool.Helper;
using Tool.Model;
using WebAPI.Models;

namespace WebAPI.Controllers.v1
{
    public class WebAPIController : BaseController
    {
        [HttpGet]
        public IHttpActionResult wxwebgrant()
        {
            MessageModel msg = new MessageModel();
            string code = HttpContext.Current.Request.QueryString["code"];
            if (code != null)
            {
                HttpContext.Current.Session["code"] = code;
            }
            else
            {
                Code.Result(ref msg, 编码.其他错误, "获取code失败");
                return GetResponseString(msg);
            }
            UserModel webgrant = new UserModel();
            try
            {
                webgrant.getWxWebGrantByCode(code, ref msg);

            }
            catch (Exception ex)
            {
                Code.Result(ref msg, 编码.程序错误, ex.Message);
            }
            if (msg.state == 0)
            {
                TokenModel token = new TokenModel();
                webgrant.token = token;
                HttpContext.Current.Session["UserModel"] = webgrant;
                msg.dateset = token;
            }
            return GetResponseString(msg);
        }
    }
}
