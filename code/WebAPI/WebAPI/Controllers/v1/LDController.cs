using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using Tool.DB;
using Tool.Help;
using WebAPI.filters;
using WebAPI.Tool;

namespace WebAPI.Controllers.v1
{
    //[AuthLdFilter]
    //提供润美康对接⽂档  拉单系统
    public class LDController : ApiController
    {
        [HttpPost]
        public IHttpActionResult getddxx(JObject p)
        {
            JObject outObject = new JObject();
            try
            {
                string str_数据库连接串 = ConfigurationManager.ConnectionStrings["connectString_ld"].ConnectionString;
                string str_数据库类型 = ConfigurationManager.ConnectionStrings["connectString_ld"].ProviderName;

                string branchId = ToolFunction.JsonValue(p, "branchId")?.ToString();//站点id
                if (null == branchId)
                {
                    outObject.Add("success", false);
                    outObject.Add("msg", "参数中缺少branchId节点");
                    outObject.Add("data", new JArray());
                    return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
                }
                string danwBh = ToolFunction.JsonValue(p, "danwBh")?.ToString();//客户编码
                JArray prodNoList = (JArray)ToolFunction.JsonValue(p, "prodNoList");
                if (null == prodNoList)
                {
                    outObject.Add("success", false);
                    outObject.Add("msg", "参数中缺少prodNoList节点");
                    outObject.Add("data", new JArray());
                    return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
                }

                DataTable dt_结果 = null;
                foreach (JObject jo in prodNoList)
                {
                    string prodNo = ToolFunction.JsonValue(jo, "prodNo").ToString();//商品编码
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("user", "");
                    parameters[1] = new SqlParameter("zdid", branchId);
                    if (null == danwBh)
                    {
                        parameters[2] = new SqlParameter("khbm", DBNull.Value);
                    }
                    else
                    {
                        parameters[2] = new SqlParameter("khbm", danwBh);
                    }

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
            catch (System.Exception e)
            {
                outObject.Add("success", false);
                outObject.Add("msg", e.Message);
                outObject.Add("data", new JArray());
                return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
            }
        }
        [HttpPost]
        public IHttpActionResult setddxx(JObject p)
        {
            JObject outObject = new JObject();
            try
            {
                string str_数据库连接串 = ConfigurationManager.ConnectionStrings["connectString_ld"].ConnectionString;
                string str_数据库类型 = ConfigurationManager.ConnectionStrings["connectString_ld"].ProviderName;

                DataTable dt = DbHelper.Db(str_数据库类型, str_数据库连接串).ExecuteBatch_订单下发(p);

                var query = from g in dt.AsEnumerable()
                            group g by new { t1 = g.Field<string>("orderCode") } into companys
                            select new { orderCode = companys.Key.t1, StallInfo = companys };
                JArray j = new JArray();
                foreach (var userInfo in query)
                {
                    JObject o = new JObject();
                    System.Collections.Generic.List<DataRow> dataRows = userInfo.StallInfo.ToList();

                    JArray jo = new JArray();
                    foreach (System.Data.DataRow dr in dataRows)
                    {
                        JObject oo = new JObject();
                        oo.Add("prodNo", dr["prodNo"].ToString());
                        jo.Add(oo);
                    }
                    o.Add("detailList", jo);
                    o.Add("orderCode", dataRows[0]["orderCode"].ToString());
                    o.Add("isOnlinePay", dataRows[0]["isOnlinePay"].ToString());
                    j.Add(o);
                }
                outObject.Add("success", true);
                outObject.Add("msg", "调用成功");
                outObject.Add("data", j);
                return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
            }
            catch (System.Exception e)
            {
                outObject.Add("success", false);
                outObject.Add("msg", e.Message);
                outObject.Add("data", new JArray());
                return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
            }
        }

        [HttpPost]
        public IHttpActionResult getzffs(JObject p)
        {
            JObject outObject = new JObject();
            try
            {
                string str_数据库连接串 = ConfigurationManager.ConnectionStrings["connectString_ld"].ConnectionString;
                string str_数据库类型 = ConfigurationManager.ConnectionStrings["connectString_ld"].ProviderName;

                string branchId = ToolFunction.JsonValue(p, "branchId")?.ToString();//站点id
                if (null == branchId)
                {
                    outObject.Add("success", false);
                    outObject.Add("msg", "参数中缺少branchId节点");
                    outObject.Add("data", new JArray());
                    return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
                }
                string danwBh = ToolFunction.JsonValue(p, "danwBh")?.ToString();//客户编码
                if (null == danwBh)
                {
                    outObject.Add("success", false);
                    outObject.Add("msg", "参数中缺少danwBh节点");
                    outObject.Add("data", new JArray());
                    return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
                }

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("user", "");
                parameters[1] = new SqlParameter("zdid", branchId);
                parameters[2] = new SqlParameter("khbm", danwBh);

                DataTable dt = DbHelper.Db(str_数据库类型, str_数据库连接串).GetDataTable("exec  jk_rmkzffs @user,@zdid,@khbm", parameters);

                outObject.Add("success", true);
                outObject.Add("msg", "调用成功");
                JObject oo = new JObject();
                oo.Add("isOnlinePay",dt.Rows[0]["isOnlinePay"].ToString());
                oo.Add("isOfflinePay", dt.Rows[0]["isOfflinePay"].ToString());
                
                outObject.Add("data", oo);
                return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
            }
            catch (System.Exception e)
            {
                outObject.Add("success", false);
                outObject.Add("msg", e.Message);
                outObject.Add("data", new JArray());
                return Json(outObject, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
            }
        }
    }
}
