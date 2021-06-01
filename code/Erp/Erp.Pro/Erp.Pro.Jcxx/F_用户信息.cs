
using Erp.Pro.Utils;
using Erp.Server.Init;
using System;
using System.Data;
using System.Windows.Forms;
using static Erp.Server.Init.C_系统参数;

namespace Erp.Pro.Jcxx
{
    public partial class F_用户信息 : Form
    {
        ServerParams inParam = new ServerParams();
        ServerParams outParam = new ServerParams();

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        public F_用户信息()
        {
            InitializeComponent();
        }

        private void F_用户信息_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("a", typeof(string));
            dt.Columns.Add("b", typeof(string));
            dt.Columns.Add("c", typeof(string));
            for (int i = 0; i < 5; i++)
            {
                DataRow row = dt.NewRow();
                row["a"] = "a" + i;
                row["b"] = "b" + i;
                row["c"] = "c" + i;
                dt.Rows.Add(row);
            }

            dt1.Columns.Add("aa", typeof(string));
            dt1.Columns.Add("bb", typeof(string));
            dt1.Columns.Add("cc", typeof(string));
            for (int i = 0; i < 5; i++)
            {
                DataRow row = dt1.NewRow();
                row["aa"] = "a" + i;
                row["bb"] = "b" + i;
                row["cc"] = "c" + i;
                dt1.Rows.Add(row);
            }


            //u_通用列表编辑2.GridControl.DataSource = dt;
            //u_通用列表编辑2.P_页面名称 = "用户信息";

            inParam.p0 = E_模块名称.基础业务;
            inParam.p1 = "用户信息_初始化";
            outParam = C_Server.Call(inParam);
            if (outParam.p0.ToString() == "1")
            {
                u_通用列表编辑2.GridControl.DataSource = outParam.p2;
                u_通用列表编辑2.P_页面名称 = "用户信息";
            }
            else
            {
                MessageBox.Show(outParam.p1.ToString(), "提示");
            }
        }


        private void u_通用列表编辑2_新增处理(object sender, EventArgs e)
        {
            C_控件参数[] P_控件参数 = new C_控件参数[3];
            P_控件参数[0] = new C_控件参数("a", "aaaaa", E_控件类型.Dev_Text);
            P_控件参数[1] = new C_控件参数("b", "bbbbb", E_控件类型.Dev_Text, true, true);
            P_控件参数[2] = new C_控件参数("c", "ccccc", E_控件类型.Dev_LookUpEdit, new C_数据源(dt1, "cc", "bb"), true, true);
            u_通用列表编辑2.P_控件参数 = P_控件参数;
        }

        private void u_通用列表编辑2_修改处理(object sender, EventArgs e)
        {
            C_控件参数[] P_控件参数 = new C_控件参数[3];
            P_控件参数[0] = new C_控件参数("a", "aaaaa", E_控件类型.Dev_Text, true, true);
            P_控件参数[1] = new C_控件参数("b", "bbbbb", E_控件类型.Dev_Text, true, true);
            P_控件参数[2] = new C_控件参数("c", "ccccc", E_控件类型.Dev_LookUpEdit, new C_数据源(dt1, "cc", "bb"), true, true);
            u_通用列表编辑2.P_控件参数 = P_控件参数;
        }
    }
}
