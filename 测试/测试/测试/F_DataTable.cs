using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 测试
{
    public partial class F_DataTable : Form
    {
        DataTable dt;
        public F_DataTable()
        {
            InitializeComponent();
        }

        private void F_DataTable_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Columns.Add("id",typeof(string));
            dt.Columns.Add("name", typeof(string));
            DataRow row = dt.NewRow();
            row["id"] = "1";
            row["name"] = "张三";
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["id"] = "2";
            row["name"] = "李四";
            dt.Rows.Add(row);
            dt.AcceptChanges();
            gridControl1.DataSource = dt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRow row = dt.NewRow();
            row["id"] = new Random().Next(1, 100);
            row["name"] = "张三";
            dt.Rows.Add(row);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           DataTable ddt= dt.GetChanges(DataRowState.Deleted);
           string id= ddt.Rows[0]["id", DataRowVersion.Original].ToString();
            MessageBox.Show("" ,"提示");
        }
        
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DataRow[] row = dt.Select("id='1'");
            row[0]["name"] = "张三11";
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
             DataRow dr_系统字典 = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            dr_系统字典.Delete();
            // dt.Rows.Remove(dr_系统字典);
        }
    }
}
