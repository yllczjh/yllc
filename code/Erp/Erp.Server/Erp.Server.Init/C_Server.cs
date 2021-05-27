using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Erp.Server.Init.C_系统参数;

namespace Erp.Server.Init
{
    public class C_Server
    {
        public static ServerParams Call(ServerParams param)
        {
            ServerParams outParam = new ServerParams();
            E_模块名称 e_模块名称 = (E_模块名称)param.p0;
            switch (e_模块名称)
            {
                case E_模块名称.基础业务:
                    outParam = Call_基础业务(param);
                    break;
                default:
                    break;
            }
            return outParam;
        }


        public static ServerParams Call_基础业务(ServerParams param)
        {
            string str_功能 = param.p1?.ToString();
            if (string.IsNullOrEmpty(str_功能))
            {

            }
            switch (str_功能)
            {
                case "用户信息编辑_新增":
                    break;
            }
            return null;
        }
    }

}
