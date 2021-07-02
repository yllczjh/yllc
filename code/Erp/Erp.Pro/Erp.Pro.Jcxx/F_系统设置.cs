using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Erp.Pro.Utils;
using Erp.Server.Helper;
using Erp.Server.Init;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static Erp.Server.Helper.ServerHelper;

namespace Erp.Pro.Jcxx
{
    public partial class F_系统设置 : XtraForm
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();

        private string P_选择节点;

        public F_系统设置()
        {
            InitializeComponent();
        }
        private void F_系统设置_Load(object sender, EventArgs e)
        {
            xtra_系统设置_SelectedPageChanged(null, null);
        }
        private void xtra_系统设置_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            string page = xtra_系统设置.SelectedTabPage.Name;
            switch (page)
            {
                case "page_菜单":
                    M_初始化绑定一级树();
                    break;
                case "page_用户":
                    grc_用户维护.DataSource = C_实体信息.C_共享数据集.P_用户信息;
                    break;
            }
        }

        #region 菜单维护
        private void M_初始化绑定一级树()
        {
            try
            {
                tree_菜单.Nodes.Clear();
                C_通用方法.M_树形助手(C_实体信息.C_共享数据集.P_菜单信息, tree_菜单, "菜单", "0", "模块名", "模块id", "上级id");
                tree_菜单.ExpandAll();
                tree_菜单.SelectedNode = tree_菜单.Nodes[0].Nodes[0];
            }
            catch
            {

            }
        }

        private void tree_菜单_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tree_菜单.SelectedNode == null)
                return;

            P_选择节点 = tree_菜单.SelectedNode.Name;
            var var_字典分类 = from en_字典分类 in C_实体信息.C_共享数据集.P_菜单信息.AsEnumerable()
                           where en_字典分类.Field<string>("上级id").ToString() == P_选择节点
                           select en_字典分类;

            if (var_字典分类.ToList().Count > 0)
            {
                grc_菜单维护.DataSource = var_字典分类.CopyToDataTable();
                grc_菜单维护.RefreshDataSource();
            }
        }
        private void 菜单权限操作_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

            DataRow dr_菜单维护 = grv_菜单维护.GetDataRow(grv_菜单维护.FocusedRowHandle);
            if (null == dr_菜单维护) return;

            if (e.Button.Caption == "人员分配")
            {
                F_权限分配 f = new F_权限分配(E_权限类型.菜单分配人员, dr_菜单维护["模块id"].ToString());
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog();
                f.Dispose();
            }
        }

        #endregion

        #region 用户信息

        #endregion

    }
}
