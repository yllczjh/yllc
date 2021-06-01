using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Erp.Pro.Jcxx;
using Erp.Server.Init;
using Erp.Server.WebAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static Erp.Server.Init.C_系统参数;

namespace Erp.Main
{
    public partial class F_主界面 : XtraForm
    {
        ServerParams inParam = new ServerParams();
        ServerParams outParam = new ServerParams();

        
        public F_主界面()
        {
            InitializeComponent();
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            if (xttc_主界面.SelectedTabPage.Name != "首页")
            {
                int index = xttc_主界面.SelectedTabPageIndex;
                xttc_主界面.TabPages.RemoveAt(index);
                xttc_主界面.SelectedTabPageIndex = index - 1;
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

        public void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (XtraTabPage p in xttc_主界面.TabPages)
            {
                if (p.Name == "Erp.Pro.Jcxx.F_用户信息")
                {
                    xttc_主界面.SelectedTabPage = p;
                    return;
                }
            }


            Form form = GetForm("Erp.Pro.Jcxx.dll", "Erp.Pro.Jcxx", "F_用户信息");
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            XtraTabPage page = new XtraTabPage();
            page.Name = "Erp.Pro.Jcxx.F_用户信息";
            page.Text = "用户信息";
            page.Controls.Add(form);
            form.Show();
            form.Dock = DockStyle.Fill;
            xttc_主界面.TabPages.Add(page);
            xttc_主界面.SelectedTabPage = page;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void F_主界面_Load(object sender, EventArgs e)
        {
            JObject json = HttpHelper.HTTP.HttpPost(new Newtonsoft.Json.Linq.JObject(), "login");


            inParam.p0 = E_模块名称.基础业务;
            inParam.p1 = "菜单信息_初始化";
            outParam = C_Server.Call(inParam);
            if (outParam.p0.ToString() == "1")
            {
                DataTable dt_菜单 = outParam.p2 as DataTable;
                C_菜单加载.Init(xttc_主界面, ribbon_菜单, dt_菜单);
            }
            else
            {
                MessageBox.Show(outParam.p1.ToString(), "提示");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            //HttpHelper.HTTP.HttpPost("http://test7.ql-soft.com/api/v1/main/webapi", "");
        }
    }
}
