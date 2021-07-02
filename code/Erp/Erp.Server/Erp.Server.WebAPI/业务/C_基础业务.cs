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


        public ServerHelper.Params Process(ServerHelper.Params inParam)
        {
            int t = 0;
            outParam.Clear();
            try
            {
                JObject inObject = new JObject();
                if (!string.IsNullOrEmpty(inParam.P_功能名))
                {
                    switch (inParam.P_功能名)
                    {
                        case "用户信息_初始化":
                            inObject.Add("sql", $"select y.* from xt_xt_yh x,xt_yh y where x.用户id=y.用户id and x.系统id='{C_实体信息.C_共享变量.系统ID}'");
                            break;
                        case "菜单信息_初始化":
                            inObject.Add("sql", $"select * from xt_mk where 系统ID='{C_实体信息.C_共享变量.系统ID}'");
                            break;
                        case "菜单分配人员":
                            inObject.Add("sql", $"select y.用户id,y.用户名,m.模块id from xt_xt_yh x inner join xt_yh y on x.用户id = y.用户id and x.系统id = 'test' left join xt_yh_mk m on y.用户id = m.用户id and m.模块id = '{inParam.P1}'");
                            break;
                        case "菜单分配人员_保存":
                            t = 1;
                            inObject.Add("sql", $"delete from xt_yh_mk where 模块id='{inParam.P1}'; insert into xt_yh_mk(用户id,模块id) select ?用户id,?模块id");
                            inObject.Add("dataset", TypeConvert.DataTableToJArray(inParam.P_数据集));
                            break;
                    }
                }


                JObject outObject = HttpHelper.HTTP.HttpPost(inObject, t == 0 ? "sys.execsql" : "sys.execsql.update");
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
            catch (MyException ex)
            {
                outParam.P_结果 = 0;
                outParam.P_结果描述 = ex.Message;
                outParam.P_数据集 = null;
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
