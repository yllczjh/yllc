using Erp.Pro.Utils.工具类;
using Erp.Pro.Utils;
using Erp.Server.WebAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Text;
using static Erp.Server.Init.C_系统参数;

namespace Erp.Server.WebAPI.业务
{
    public class C_通用业务
    {
        ServerParams outParam = new ServerParams();

        //inParam.p0 = E_模块名称.通用列表编辑;
        //inParam.p1 = f_父窗体.P_页面名称;
        //inParam.p2 = "修改";
        //inParam.p3 = f_父窗体.GridControl.DataSource;
        //inParam.p4 = i_数据源行号;
        public ServerParams M_通用列表编辑(ServerParams inParam)
        {
            outParam.Clear();
            try
            {
                JObject inObject = new JObject();
                switch (inParam.p2.ToString())
                {
                    case "新增":
                        M_新增(inParam, ref inObject);
                        break;

                    case "修改":
                        M_修改(inParam, ref inObject);
                        break;

                    case "删除":
                        M_删除(inParam, ref inObject);
                        break;

                    case "保存":
                        M_保存(inParam, ref inObject);
                        break;

                    case "初始化":
                        M_初始化(inParam, ref inObject);
                        break;
                }

                JObject outObject = HttpHelper.HTTP.HttpPost(inObject, "sys.execsql");
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
            catch (MyException ex)
            {
                outParam.p0 = "0";
                outParam.p1 = ex.Message;
                outParam.p2 = null;
            }
            catch (Exception e)
            {
                outParam.p0 = "0";
                outParam.p1 = e.Message;
                outParam.p2 = null;
            }

            return outParam;
        }

        private void M_新增(ServerParams inParam, ref JObject inObject)
        {
            DataRow row = inParam.p3 as DataRow;
            switch (inParam.p1.ToString())
            {
                case "用户信息":
                    if (row["用户ID"].ToString() == "1001")
                    {
                        throw new MyException("用户ID重复");
                    }
                    inObject.Add("sql", $"insert into xt_yh(用户ID,密码,性别,出生日期,手机号码,现住址,头像地址,禁用) values ('{row["用户ID"]}','{row["密码"]}','{row["性别"]}','{row["出生日期"]}','{row["手机号码"]}','{row["现住址"]}','{row["头像地址"]}','{row["禁用"]}')");
                    break;
            }
        }
        private void M_修改(ServerParams inParam, ref JObject inObject)
        {
            DataRow row = inParam.p3 as DataRow;
            switch (inParam.p1.ToString())
            {
                case "用户信息":
                    if (row["用户ID"].ToString() == "1001")
                    {
                        throw new MyException("用户ID重复");
                    }
                    inObject.Add("sql", $"update xt_yh set 密码='{row["密码"]}',性别='{row["性别"]}',出生日期='{row["出生日期"]}',手机号码='{row["手机号码"]}',现住址='{row["现住址"]}',头像地址='{row["头像地址"]}',禁用='{row["禁用"]}' where rowid='{row["rowid"]}'");
                    break;
            }
        }
        private void M_删除(ServerParams inParam, ref JObject inObject)
        {
            DataTable dt = inParam.p3 as DataTable;
            switch (inParam.p1.ToString())
            {
                case "用户信息":
                    inObject.Add("sql", $"delete from xt_yh where rowid in({C_通用方法.M_获取主键IN(dt, "rowid")})");
                    break;
            }
        }
        private void M_保存(ServerParams inParam, ref JObject inObject)
        {
            switch (inParam.p1.ToString())
            {
                case "样式列表":
                    DataRow row = inParam.p5 as DataRow;
                    if (string.IsNullOrEmpty(row["rowid"]?.ToString()))
                    {
                        inObject.Add("sql", $"insert into xt_yslb(系统ID,用户ID,样式ID,字段名,显示名称,宽度,排序) values ('{row["系统ID"]}','{row["用户ID"]}','{row["样式ID"]}','{row["字段名"]}','{row["显示名称"]}','{row["宽度"]}','{row["排序"]}')");
                    }else
                    {
                        inObject.Add("sql", $"update xt_yslb set 显示名称='{row["显示名称"]}',宽度='{row["宽度"]}',排序='{row["排序"]}'");
                    }
                    break;
            }
        }
        private void M_初始化(ServerParams inParam, ref JObject inObject)
        {
            switch (inParam.p1.ToString())
            {
                case "用户信息":
                    inObject.Add("sql", $"select * from xt_yh");
                    break;
                case "样式列表":
                    inObject.Add("sql", $"select * from xt_yslb");
                    break;
            }
        }
    }
}
