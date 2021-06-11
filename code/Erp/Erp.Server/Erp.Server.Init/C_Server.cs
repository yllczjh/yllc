using Erp.Server.Helper;
using Erp.Server.WebAPI.业务;
using static Erp.Server.Helper.ServerHelper;

namespace Erp.Server.Init
{
    public class C_Server
    {
        public static ServerHelper.Params Call(ServerHelper.Params param)
        {
            ServerHelper.Params outParam = new ServerHelper.Params();
            E_模块名称 e_模块名称 = (E_模块名称)param.P_模块名;
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


        private static ServerHelper.Params Call_基础业务(ServerHelper.Params param)
        {
            C_基础业务 c_基础业务 = new C_基础业务();
            ServerHelper.Params outParam = new ServerHelper.Params();
            string str_功能 = param.P_功能名;
            if (string.IsNullOrEmpty(str_功能))
            {
                outParam.P_结果 = 0;
                outParam.P_结果描述 = "找不到方法!";
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
                    outParam.P_结果 = 0;
                    outParam.P_结果描述 = "找不到方法!";
                    break;
            }
            return outParam;
        }

        private static ServerHelper.Params Call_通用列表编辑(ServerHelper.Params param)
        {
            C_通用业务 c_通用业务 = new C_通用业务();
            ServerHelper.Params outParam = new ServerHelper.Params();
            //string str_功能 = param.p1?.ToString();
            //if (string.IsNullOrEmpty(str_功能))
            //{
            //    outParam.P_返回结果 = 0;
            //    outParam.P_返回描述 = "找不到方法!";
            //    return outParam;
            //}
            outParam = c_通用业务.M_通用列表编辑(param);
            return outParam;
        }
    }
}
