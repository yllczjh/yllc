using Erp.Server.Init.业务;
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
                case E_模块名称.通用业务:
                    outParam = Call_通用列表编辑(param);
                    break;
                default:
                    break;
            }
            return outParam;
        }


        private static ServerParams Call_基础业务(ServerParams param)
        {
            C_基础业务 c_基础业务 = new C_基础业务();
            ServerParams outParam = new ServerParams();
            string str_功能 = param.p1?.ToString();
            if (string.IsNullOrEmpty(str_功能))
            {
                outParam.p0 = "0";
                outParam.p1 = "找不到方法!";
                return outParam;
            }
            switch (str_功能)
            {
                case "用户信息_初始化":
                    outParam = c_基础业务.M_用户信息_初始化(param);
                    break;
                case "菜单信息_初始化":
                    outParam = c_基础业务.M_菜单信息_初始化(param);
                    break;
                default:
                    outParam.p0 = "0";
                    outParam.p1 = "找不到方法!";
                    break;
            }
            return outParam;
        }

        private static ServerParams Call_通用列表编辑(ServerParams param)
        {
            C_通用业务 c_通用列表编辑 = new C_通用业务();
            ServerParams outParam = new ServerParams();
            string str_功能 = param.p1?.ToString();
            if (string.IsNullOrEmpty(str_功能))
            {
                outParam.p0 = "0";
                outParam.p1 = "找不到方法!";
                return outParam;
            }
            outParam = c_通用列表编辑.M_通用列表编辑(param);
            return outParam;
        }
    }
}
