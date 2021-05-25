using Erp.Tools.DB;
using Erp.Tools.Tygn;
using System;
using System.Data;

namespace Erp.Pro.Jcxx
{
    public partial class F_用户信息 : F_通用列表界面
    {
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

            base.u_列表控件.GridControl.DataSource = dt;
        }

        public override void M_新增()
        {
            P_控件参数 = new C_控件参数[3];
            P_控件参数[0] = new C_控件参数("a", "aaaaa", E_控件类型.Dev_Text, true, true);
            P_控件参数[1] = new C_控件参数("b", "bbbbb", E_控件类型.Dev_Text, true, true);
            P_控件参数[2] = new C_控件参数("c", "ccccc", E_控件类型.Dev_LookUpEdit, true, true, new C_数据源(dt1, "cc", "bb"));
            base.M_新增();
        }

        public override void M_修改()
        {
            P_控件参数 = new C_控件参数[3];
            P_控件参数[0] = new C_控件参数("a", "aaaaa", E_控件类型.Dev_Text, true, true);
            P_控件参数[1] = new C_控件参数("b", "bbbbb", E_控件类型.Dev_Text, true, true);
            P_控件参数[2] = new C_控件参数("c", "ccccc", E_控件类型.Dev_LookUpEdit, true, true, new C_数据源(dt1, "cc", "bb"));

            base.M_修改();
        }
    }
}
