using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System;
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


        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Name != "首页")
            {
                int index = xtraTabControl1.SelectedTabPageIndex;
                xtraTabControl1.TabPages.RemoveAt(index);
                xtraTabControl1.SelectedTabPageIndex = index-1;
            }
        }

        public  Form GetForm(string LibName, string NameSpace, string formName)
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
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
