using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 业务管理.数据库;

namespace 业务整理.数据库
{
    class 数据库操作
    {
       static  IDbHelper dbhelper = Db_Common.Get_DbHelper();
       static  WcfHelper.ParmObj OutObj = new WcfHelper.ParmObj();
        public static WcfHelper.ParmObj M_获取_校对医嘱记录(WcfHelper.ParmObj InObj)
        {


            WcfHelper.ParmObj OutObj = new WcfHelper.ParmObj();

            string sql = @"select * from 基础项目_人员资料";


            try
            {
                DataTable dt_已校对病人医嘱 = dbhelper.Retrieve(CommandType.Text, sql, "已校对病人医嘱").Tables[0];
                OutObj.p1 = "1";
                OutObj.p2 = "ok";
                OutObj.p3 = WcfHelper.DataTableToStr(dt_已校对病人医嘱, "", true);
            }
            catch (Exception ex)
            {

                OutObj.p1 = "";
                OutObj.p2 = ex.Message;
                OutObj.p3 = "";
            }
            return OutObj;
        }
    }
}
