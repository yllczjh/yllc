using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Tools.DB
{
    public class C_业务分支
    {
        public static  C_基础业务 基础业务 { get; set; }

    }


    public class C_基础业务
    {
        public E_按钮名称 用户信息 { get; set; }
    }

    public class E_按钮名称
    {
        public string 新增 { get; set; }
        public string 修改 { get; set; }
        public string 删除 { get; set; }
    }
}
