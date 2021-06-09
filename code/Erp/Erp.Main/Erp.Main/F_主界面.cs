using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Erp.Pro.Jcxx;
using Erp.Server.Init;
using System;
using System.Data;
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


        private void F_主界面_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea; //设置最大化的大小为工作区域
            this.WindowState = FormWindowState.Maximized;

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
                XtraMessageBox.Show(outParam.p1.ToString(), "提示");
            }
        }


        private void sib_最小化_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void sib_退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
