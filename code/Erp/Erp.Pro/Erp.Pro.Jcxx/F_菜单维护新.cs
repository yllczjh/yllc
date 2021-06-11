using Erp.Pro.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp.Pro.Jcxx
{
    public partial class F_菜单维护新 : Form
    {
        private string P_选择节点;
        public F_菜单维护新()
        {
            InitializeComponent();
        }

        private void tree_菜单_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //u_菜单信息维护.GridControl.DataSource = null;
            if (tree_菜单.SelectedNode == null)
                return;

            P_选择节点 = tree_菜单.SelectedNode.Name;
            var var_字典分类 = from en_字典分类 in C_实体信息.C_共享数据集.P_菜单信息.AsEnumerable()
                           where en_字典分类.Field<string>("上级id").ToString() == P_选择节点
                           select en_字典分类;

            if (var_字典分类.ToList().Count > 0)
            {
                //u_菜单信息维护.M_加载列表数据(var_字典分类.CopyToDataTable());
            }
        }

        private void F_菜单维护新_Load(object sender, EventArgs e)
        {
            //u_菜单信息维护.P_页面名称 = "菜单维护";

            M_初始化绑定一级树();
        }
        private void M_初始化绑定一级树()
        {
            try
            {
                tree_菜单.Nodes.Clear();
                C_通用方法.M_树形助手(C_实体信息.C_共享数据集.P_菜单信息, tree_菜单, "菜单", "0", "模块名", "模块id", "上级id");
                tree_菜单.ExpandAll();
                tree_菜单.SelectedNode = tree_菜单.Nodes[0].Nodes[0];
            }
            catch (Exception)
            {

            }
        }
    }
}
