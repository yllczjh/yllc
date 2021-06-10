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
                    outParam.P_结果 = 1;
                    outParam.P_结果描述 = "成功!";
                    outParam.P_数据集 = dt;
                }
                else
                {
                    outParam.P_结果 = int.Parse(outObject.GetValue("errcode").ToString());
                    outParam.P_结果描述 = outObject.GetValue("msgtext").ToString();
                    outParam.P_数据集 = null;
                }
            }
            catch (Exception e)
            {
                outParam.P_结果 = 0;
                outParam.P_结果描述 = e.Message;
                outParam.P_数据集 = null;
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
                    outParam.P_结果 = 1;
                    outParam.P_结果描述 = "成功!";
                    outParam.P_数据集 = dt;
                }
                else
                {
                    outParam.P_结果 = int.Parse(outObject.GetValue("errcode").ToString());
                    outParam.P_结果描述 = outObject.GetValue("msgtext").ToString();
                    outParam.P_数据集 = null;
                }
            }
            catch (Exception e)
            {
                outParam.P_结果 = 0;
                outParam.P_结果描述 = e.Message;
                outParam.P_数据集 = null;
            }

            return outParam;
        }
    }
}
