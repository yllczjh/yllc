using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using Tool.DB;
using Tool.Help;
using Tool.Model;
using WebAPI.Tool;

namespace WebAPI.Controllers.v1
{
    //提供润美康对接⽂档  拉单系统
    public class LDController : ApiController
    {
        [HttpPost]
        public IHttpActionResult getddxx(JObject p)
        {
            JObject outObject = new JObject();
            MessageModel msg = this.Request.Properties["msg"] as MessageModel;
            if (msg.errcode != 0)
            {
                outObject.Add("success", false);
                outObject.Add("msg", msg.msgtext);
                return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
            }

            
            string str_数据库连接串 = ConfigurationManager.ConnectionStrings["connectString_ld"].ConnectionString;
            string str_数据库类型 = ConfigurationManager.ConnectionStrings["connectString_ld"].ProviderName;

            string branchId = ToolFunction.JsonValue(p, "branchId").ToString();//站点id
            string danwBh = ToolFunction.JsonValue(p, "danwBh").ToString();//客户编码
            JArray prodNoList = (JArray)ToolFunction.JsonValue(p, "prodNoList");

            DataTable dt_结果 = null;
            foreach (JObject jo in prodNoList)
            {
                string prodNo = ToolFunction.JsonValue(jo, "prodNo").ToString();//商品编码
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("user", "");
                parameters[1] = new SqlParameter("zdid", branchId);
                parameters[2] = new SqlParameter("khbmn", danwBh);
                parameters[3] = new SqlParameter("spbm", prodNo);
                DataTable dt = DbHelper.Db(str_数据库类型, str_数据库连接串).GetDataTable("exec jk_rmkcpxx @user,@zdid,@khbm,@spbm", parameters);
                if (null == dt_结果)
                {
                    dt_结果 = dt;
                }
                else
                {
                    dt_结果.Merge(dt);
                }
            }
            outObject.Add("success", true);
            outObject.Add("msg", "调用成功");
            outObject.Add("data", Helper.DataTableToJArray(dt_结果));
            return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
        }

        public IHttpActionResult setddxx(JObject p)
        {
            JObject outObject = new JObject();
            MessageModel msg = this.Request.Properties["msg"] as MessageModel;
            if (msg.errcode != 0)
            {
                outObject.Add("success", false);
                outObject.Add("msg", msg.msgtext);
                return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
            }

            string str_数据库连接串 = ConfigurationManager.ConnectionStrings["connectString_ld"].ConnectionString;
            string str_数据库类型 = ConfigurationManager.ConnectionStrings["connectString_ld"].ProviderName;

            DbHelper.Db(str_数据库类型, str_数据库连接串).ExecuteBatch_订单下发(p);

            
            return null;
        }
    }
}
