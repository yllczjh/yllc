using Erp.Server.WebAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using static Erp.Server.Init.C_系统参数;

namespace Erp.Server.Init.业务
{
    public class C_基础业务
    {
        ServerParams outParam = new ServerParams();
        JObject inObject = new JObject();
        JObject outObject = new JObject();
        public ServerParams M_用户信息_初始化(ServerParams param)
        {
            outParam.Clear();
            try
            {
                inObject = new JObject();
                inObject.Add("sql", "select * from xt_yh");
                outObject = HttpHelper.HTTP.HttpPost(inObject, "sys.execsql");
                if (outObject.GetValue("errcode").ToString() == "0")
                {
                    DataTable dt = TypeConvert.JObjectToDataTable(outObject);
                    outParam.p0 = "1";
                    outParam.p1 = "成功!";
                    outParam.p2 = dt;
                }
                else
                {
                    outParam.p0 = outObject.GetValue("errcode").ToString();
                    outParam.p1 = outObject.GetValue("msgtext").ToString();
                    outParam.p2 = null;
                }
            }
            catch (Exception e)
            {
                outParam.p0 = "0";
                outParam.p1 = e.Message;
                outParam.p2 = null;
            }

            return outParam;
        }

        public ServerParams M_菜单信息_初始化(ServerParams param)
        {
            outParam.Clear();
            try
            {
                inObject = new JObject();
                inObject.Add("sql", "select * from xt_mk where 系统ID='9999'");
                outObject = HttpHelper.HTTP.HttpPost(inObject, "sys.execsql");
                if (outObject.GetValue("errcode").ToString() == "0")
                {
                    DataTable dt = TypeConvert.JObjectToDataTable(outObject);
                    outParam.p0 = "1";
                    outParam.p1 = "成功!";
                    outParam.p2 = dt;
                }
                else
                {
                    outParam.p0 = outObject.GetValue("errcode").ToString();
                    outParam.p1 = outObject.GetValue("msgtext").ToString();
                    outParam.p2 = null;
                }
            }
            catch (Exception e)
            {
                outParam.p0 = "0";
                outParam.p1 = e.Message;
                outParam.p2 = null;
            }

            return outParam;
        }
    }
}
