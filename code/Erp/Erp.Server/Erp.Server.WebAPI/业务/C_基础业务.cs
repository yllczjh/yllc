using Erp.Pro.Utils;
using Erp.Server.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace Erp.Server.WebAPI.业务
{
    public class C_基础业务
    {
        ServerHelper.Params outParam = new ServerHelper.Params();
        JObject inObject = new JObject();
        JObject outObject = new JObject();
        public ServerHelper.Params M_用户信息_初始化(ServerHelper.Params param)
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

        public ServerHelper.Params M_菜单信息_初始化(ServerHelper.Params param)
        {
            outParam.Clear();
            try
            {
                inObject = new JObject();
                inObject.Add("sql", $"select * from xt_mk where 系统ID='{C_实体信息.C_共享变量.系统ID}'");
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
