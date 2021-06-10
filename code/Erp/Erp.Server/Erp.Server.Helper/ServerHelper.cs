using System.Data;

namespace Erp.Server.Helper
{
    public class ServerHelper
    {
        public enum E_模块名称
        {
            基础业务,
            通用业务
        }

        public class Params
        {
            public E_模块名称 P_模块名 { get; set; }
            public string P_页面名 { get; set; }
            public string P_方法名 { get; set; }
            public string P_功能名 { get; set; }
            public DataTable P_数据集 { get; set; }
            public DataRow P_数据行 { get; set; }

            public int P_结果 { get; set; }
            public string P_结果描述 { get; set; }

            public void Clear()
            {
                P_页面名 = string.Empty;
                P_方法名 = string.Empty;
                P_功能名 = string.Empty;
                P_数据集 = null;
                P_数据行 = null;

                P_结果 = 0;
                P_结果描述 = string.Empty;
            }
        }
    }
}
