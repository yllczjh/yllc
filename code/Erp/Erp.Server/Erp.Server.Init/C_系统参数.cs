using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Server.Init
{
    public class C_系统参数
    {
        public enum E_模块名称
        {
            基础业务
        }

        public class ServerParams
        {
            public object p0 { get; set; }
            public object p1 { get; set; }
            public object p2 { get; set; }
            public object p3 { get; set; }
            public object p4 { get; set; }
            public object p5 { get; set; }
            public object p6 { get; set; }
            public object p7 { get; set; }
            public object p8 { get; set; }
            public object p9 { get; set; }
            public object p10 { get; set; }
            public void Clear()
            {
                p0 = null;
                p1 = null;
                p2 = null;
                p3 = null;
                p4 = null;
                p5 = null;
                p6 = null;
                p7 = null;
                p8 = null;
                p9 = null;
                p10 = null;
            }

        }
    }
}
