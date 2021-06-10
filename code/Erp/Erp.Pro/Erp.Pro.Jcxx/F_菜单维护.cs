using DevExpress.XtraEditors;
using Erp.Pro.Utils;
using Erp.Server.Helper;
using System;
using System.Data;
using System.Linq;
using static Erp.Pro.Utils.C_实体信息;

namespace Erp.Pro.Jcxx
{
    public partial class F_菜单维护 : XtraForm
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();
        private string P_选择节点;

        public F_菜单维护()
        {
            InitializeComponent();
        }

        private void F_菜单编辑_Load(object sender, System.EventArgs e)
        {
            u_菜单信息维护.P_页面名称 = "菜单维护";

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

        private void tree_菜单_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            u_菜单信息维护.GridControl.DataSource = null;
            if (tree_菜单.SelectedNode == null)
                return;

            P_选择节点 = tree_菜单.SelectedNode.Name;
            var var_字典分类 = from en_字典分类 in C_实体信息.C_共享数据集.P_菜单信息.AsEnumerable()
                           where en_字典分类.Field<string>("上级id").ToString() == P_选择节点
                           select en_字典分类;

            if (var_字典分类.ToList().Count > 0)
            {
                u_菜单信息维护.M_加载列表数据(var_字典分类.CopyToDataTable());
            }
        }

        private void u_菜单信息维护_新增处理(object sender, EventArgs e)
        {
            C_控件参数[] P_控件参数 = M_设置控件参数();
            u_菜单信息维护.P_每行显示列数 = 3;
            u_菜单信息维护._P_过滤条件 = "上级id ='" + tree_菜单.SelectedNode.Name + "'";
            u_菜单信息维护.P_控件参数 = P_控件参数;
        }

        private void u_菜单信息维护_修改处理(object sender, EventArgs e)
        {
            C_控件参数[] P_控件参数 = M_设置控件参数();
            u_菜单信息维护.P_每行显示列数 = 3;
            u_菜单信息维护._P_过滤条件 = "上级id ='" + tree_菜单.SelectedNode.Name + "'";
            u_菜单信息维护.P_控件参数 = P_控件参数;
        }
        private C_控件参数[] M_设置控件参数()
        {
            C_控件参数[] P_控件参数 = new C_控件参数[11];

            P_控件参数[0] = new C_控件参数() { 数据名称 = "模块id", 控件类型 = E_控件类型.Dev_Text, 只读 = true, 默认值 = C_通用方法.GuidTo16String() };
            P_控件参数[1] = new C_控件参数() { 数据名称 = "上级id", 控件类型 = E_控件类型.Dev_Text, 默认值 = tree_菜单.SelectedNode.Name, 是否显示 = false };
            P_控件参数[2] = new C_控件参数() { 数据名称 = "模块名", 控件类型 = E_控件类型.Dev_Text, 是否必填 = true };
            P_控件参数[3] = new C_控件参数() { 数据名称 = "命令", 控件类型 = E_控件类型.Dev_Text };
            P_控件参数[4] = new C_控件参数() { 数据名称 = "参数", 控件类型 = E_控件类型.Dev_Text };
            P_控件参数[5] = new C_控件参数() { 数据名称 = "程序集名", 控件类型 = E_控件类型.Dev_Text };
            P_控件参数[6] = new C_控件参数() { 数据名称 = "窗口名", 控件类型 = E_控件类型.Dev_Text };
            P_控件参数[7] = new C_控件参数() { 数据名称 = "图标", 控件类型 = E_控件类型.Dev_Text };
            P_控件参数[8] = new C_控件参数() { 数据名称 = "禁用", 控件类型 = E_控件类型.Dev_CheckEdit };
            P_控件参数[9] = new C_控件参数() { 数据名称 = "系统id", 控件类型 = E_控件类型.Dev_Text, 是否显示 = false, 默认值 = C_实体信息.C_共享变量.系统ID };
            P_控件参数[10] = new C_控件参数() { 数据名称 = "排序", 控件类型 = E_控件类型.Dev_Text, 是否显示 = false, 默认值 = int.Parse((u_菜单信息维护.GridControl.DataSource as DataTable).Compute("max(排序)", "").ToString()) + 1, 自增 = true};
            return P_控件参数;
        }

        private void u_菜单信息维护_刷新处理(object sender, EventArgs e)
        {
            C_实体信息.C_共享数据集.P_菜单信息 = ((MyEventArgs)e).DT;
            string P_选择节点1 = P_选择节点;
            M_初始化绑定一级树();

            for (int i = 0; i < tree_菜单.Nodes[0].Nodes.Count; i++)
            {
                for (int j = 0; j < tree_菜单.Nodes[0].Nodes[i].Nodes.Count; j++)
                {
                    if (tree_菜单.Nodes[0].Nodes[i].Nodes[j].Name == P_选择节点1)
                    {
                        tree_菜单.SelectedNode = tree_菜单.Nodes[0].Nodes[i].Nodes[j];//选中
                        tree_菜单.Nodes[0].Expand();//展开父级
                        return;
                    }
                }
                if (tree_菜单.Nodes[0].Nodes[i].Name == P_选择节点1)
                {
                    tree_菜单.SelectedNode = tree_菜单.Nodes[0].Nodes[i];//选中
                    tree_菜单.Nodes[0].Expand();//展开父级
                    return;
                }
            }
            P_选择节点 = P_选择节点1;
        }
    }
}
