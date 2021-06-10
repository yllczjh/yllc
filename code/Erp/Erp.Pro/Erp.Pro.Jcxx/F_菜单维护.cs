using DevExpress.XtraEditors;
using Erp.Server.Helper;
using System;

namespace Erp.Pro.Jcxx
{
    public partial class F_菜单维护 : XtraForm
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();

        public F_菜单维护()
        {
            InitializeComponent();
        }

        private void F_菜单编辑_Load(object sender, System.EventArgs e)
        {

            M_初始化绑定一级树();
        }
        private void M_初始化绑定一级树()
        {
            try
            {
                tree_菜单.Nodes.Clear();
                //C_通用方法.M_树形助手(公共类.dt_病案管理_项目字典分类, tree_菜单, "字典分类", "GB", "字典分类名称", "字典分类编码", "项目编码");
                tree_菜单.ExpandAll();
                tree_菜单.SelectedNode = tree_菜单.Nodes[0].Nodes[0];
            }
            catch (Exception)
            {

            }
        }
    }
}
