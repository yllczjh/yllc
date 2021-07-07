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
                        case "加载系统信息_菜单信息":
                            inObject.Add("sql", $"select * from xt_mk where 系统ID='{inParam.P1}'");
                            break;
                        case "加载系统信息_用户信息":
                            inObject.Add("sql", $"select y.* from xt_xt_yh x, xt_yh y where x.用户id=y.用户id and x.系统id='{inParam.P1}'");
                            break;
                        case "加载系统信息_角色信息":
                            inObject.Add("sql", $"select j.* from xt_js j where j.系统id='{inParam.P1}'");
                            break;

                        case "菜单信息_保存":
                            t = 1;
                            inObject.Add("sql", $@"if ?rowid=0
                                                        insert into xt_mk(系统id, 模块id, 上级id, 模块名, 命令, 参数, 图标, 排序, 禁用, 所有人可用, 程序集名, 窗口名, 添加时间)
                                                        select ?系统id,?模块id,?上级id,?模块名,?命令,?参数,?图标,?排序,?禁用,?所有人可用,?程序集名,?窗口名, getdate()
                                                    else
                                                        update xt_mk set 模块名 =?模块名, 命令 =?命令, 参数 =?参数, 图标 =?图标, 排序 =?排序, 禁用 =?禁用, 所有人可用 =?所有人可用, 程序集名 =?程序集名, 窗口名 =?窗口名
                                                        where rowid =?rowid ");
                            inObject.Add("dataset", TypeConvert.DataTableToJArray(inParam.P_数据集));
                            break;
                        case "菜单信息_删除":
                            t = 1;
                            inObject.Add("sql", $@"delete xt_mk where rowid='{inParam.P1}';delete xt_yh_mk where 模块id='{inParam.P2}';delete xt_js_mk where 模块id='{inParam.P2}';");
                            break;

                        case "用户信息_保存":
                            t = 1;
                            inObject.Add("sql", $@"if ?rowid=0
                                                        begin
                                                        insert into xt_yh(用户id, 密码, 性别, 出生日期, 手机号码, 现住址, 头像地址, 禁用, 添加时间,用户名)
                                                        select ?用户id, ?密码, ?性别, ?出生日期, ?手机号码, ?现住址, ?头像地址, ?禁用, getdate(),?用户名;
                                                        insert into xt_xt_yh(系统id,用户id) select '{inParam.P1}',?用户id
                                                        end
                                                    else
                                                        update xt_yh set 性别 =?性别, 出生日期 =?出生日期, 手机号码 =?手机号码, 现住址 =?现住址, 头像地址 =?头像地址, 禁用 =?禁用, 用户名 =?用户名
                                                        where rowid =?rowid ");
                            inObject.Add("dataset", TypeConvert.DataTableToJArray(inParam.P_数据集));
                            break;
                        case "用户信息_删除":
                            t = 1;
                            inObject.Add("sql", $@"delete xt_yh where rowid='{inParam.P1}';delete xt_yh_js where 用户id='{inParam.P2}';delete xt_yh_mk where 用户id='{inParam.P2}';");
                            break;

                        case "角色信息_保存":
                            t = 1;
                            inObject.Add("sql", $@"if ?rowid=0
                                                        insert into xt_js(系统id,角色id, 角色名, 添加人, 添加时间)
                                                        select ?系统id,?角色id, ?角色名, ?添加人, getdate()
                                                    else
                                                        update xt_js set 角色名 =?角色名 where rowid =?rowid ");
                            inObject.Add("dataset", TypeConvert.DataTableToJArray(inParam.P_数据集));
                            break;
                        case "角色信息_删除":
                            t = 1;
                            inObject.Add("sql", $@"delete xt_js where rowid='{inParam.P1}';delete xt_yh_js where 角色id='{inParam.P2}';delete xt_js_mk where 角色id='{inParam.P2}';");
                            break;

                        case "菜单分配人员":
                            inObject.Add("sql", $"select y.用户id,y.用户名,m.模块id from xt_xt_yh x inner join xt_yh y on x.用户id = y.用户id and x.系统id = '{inParam.P2}' left join xt_yh_mk m on y.用户id = m.用户id and m.模块id = '{inParam.P1}'");
                            break;
                        case "菜单分配人员_修改":
                            t = 1;
                            inObject.Add("sql", $"delete from xt_yh_mk where 模块id='{inParam.P1}' and 用户id=?用户id; insert into xt_yh_mk(用户id,模块id) select ?用户id,?模块id");
                            inObject.Add("dataset", TypeConvert.DataTableToJArray(inParam.P_数据集));
                            break;
                        case "菜单分配人员_删除":
                            t = 1;
                            inObject.Add("sql", $"delete from xt_yh_mk where 模块id='{inParam.P1}'");
                            break;

                        case "菜单分配角色":
                            inObject.Add("sql", $"select j.角色id,j.角色名,a.模块id from xt_js j left join (select jm.* from xt_js_mk jm inner join xt_mk m on jm.模块id=m.模块id where m.系统id='{inParam.P2}' and m.模块id='{inParam.P1}') a on j.角色id=a.角色id");
                            break;
                        case "菜单分配角色_修改":
                            t = 1;
                            inObject.Add("sql", $"delete from xt_js_mk where 模块id='{inParam.P1}' and 角色id=?角色id; insert into xt_js_mk(角色id,模块id) select ?角色id,?模块id");
                            inObject.Add("dataset", TypeConvert.DataTableToJArray(inParam.P_数据集));
                            break;
                        case "菜单分配角色_删除":
                            t = 1;
                            inObject.Add("sql", $"delete from xt_js_mk where 模块id='{inParam.P1}'");
                            break;
                       
                        case "角色分配人员":
                            inObject.Add("sql", $"select y.用户id,y.用户名,j.角色id from xt_xt_yh x inner join xt_yh y on x.用户id = y.用户id and x.系统id = '{inParam.P2}' left join xt_yh_js j on y.用户id=j.用户id and j.角色id='{inParam.P1}'");
                            break;
                        case "角色分配人员_修改":
                            t = 1;
                            inObject.Add("sql", $"delete from xt_yh_js where 角色id='{inParam.P1}' and 用户id=?用户id; insert into xt_yh_js(用户id,角色id) select ?用户id,?角色id");
                            inObject.Add("dataset", TypeConvert.DataTableToJArray(inParam.P_数据集));
                            break;
                        case "角色分配人员_删除":
                            t = 1;
                            inObject.Add("sql", $"delete from xt_yh_js where 角色id='{inParam.P1}'");
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
