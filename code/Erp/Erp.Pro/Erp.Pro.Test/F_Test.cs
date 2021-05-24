using DevExpress.XtraEditors;
using Erp.Tools.Tygn;
using System.Data;

namespace Erp.Pro.Test
{
    public partial class F_Test : XtraForm
    {
        public F_Test()
        {
            InitializeComponent();
        }

        private void F_Test_Load(object sender, System.EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("a", typeof(string));
            dt.Columns.Add("b", typeof(string));
            dt.Columns.Add("c", typeof(string));
            for(int i = 0; i < 5; i++)
            {
                DataRow row = dt.NewRow();
                row["a"] = "a" + i;
                row["b"] = "b" + i;
                row["c"] = "c" + i;
                dt.Rows.Add(row);
            }
            //u_列表控件1.GridControl.DataSource = dt;
           // u_列表控件1.GridControl.DataSource(dt);

        }
    }
}
