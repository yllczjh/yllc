using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Erp.Tools.DB;
using Erp.Tools.Tygn;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Erp.Main
{
    public partial class F_主界面 : XtraForm
    {
        public F_主界面()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Name != "首页")
            {
                int index = xtraTabControl1.SelectedTabPageIndex;
                xtraTabControl1.TabPages.RemoveAt(index);
                xtraTabControl1.SelectedTabPageIndex = index - 1;
            }
        }

        public Form GetForm(string LibName, string NameSpace, string formName)
        {
            try
            {
                string allName;
                string LibFilePath = Application.StartupPath + "\\" + LibName;
                allName = NameSpace + '.' + formName;
                Assembly FrmAss = Assembly.LoadFrom(LibFilePath);
                Form form = FrmAss.CreateInstance(allName) as Form;
                return form;
            }
            catch
            {
                return null;
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form form = GetForm("Erp.Pro.Test.dll", "Erp.Pro.Test", "F_Test");
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            XtraTabPage page = new XtraTabPage();
            page.Text = "124";
            page.Controls.Add(form);
            form.Show();
            form.Dock = DockStyle.Fill;
            xtraTabControl1.TabPages.Add(page);
            xtraTabControl1.SelectedTabPage = page;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void F_主界面_Load(object sender, EventArgs e)
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
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            HttpHelper.HTTP.HttpPost("http://test7.ql-soft.com/api/v1/main/webapi", "");
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            C_控件参数[] 控件参数 = new C_控件参数[3];
            控件参数[0] = new C_控件参数("a", "aaaaa", E_控件类型.Dev_Text, true, true);
            控件参数[1] = new C_控件参数("b", "bbbbb", E_控件类型.Dev_Text, true, true);

            控件参数[2] = new C_控件参数("c", "ccccc", E_控件类型.Dev_LookUpEdit, true, true,new C_数据源(dt1,"cc","bb"));

            F_通用编辑页面 f_编辑 = new F_通用编辑页面(dt, 控件参数, 1, 3);
            f_编辑.ShowDialog();
        }
    }
}
