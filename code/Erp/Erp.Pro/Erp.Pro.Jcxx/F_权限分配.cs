using DevExpress.XtraEditors;
using Erp.Pro.Utils;
using Erp.Server.Helper;
using Erp.Server.Init;
using System.Data;
using System.Windows.Forms;
using static Erp.Server.Helper.ServerHelper;

namespace Erp.Pro.Jcxx
{
    public partial class F_权限分配 : XtraForm
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();
        DataTable dt_数据源;
        string P_系统id;
        E_权限类型 P_权限类型;
        string P_主键;
        public F_权限分配(string str_系统id, E_权限类型 e, string id)
        {
            InitializeComponent();
            P_系统id = str_系统id;
            P_权限类型 = e;
            P_主键 = id;
        }
        private void F_权限分配_Load(object sender, System.EventArgs e)
        {
            dt_数据源 = M_加载权限列表();
            switch (P_权限类型)
            {
                case E_权限类型.菜单分配人员:
                    lv_权限分配.Visible = true;
                    tv_权限分配.Visible = false;
                    lv_权限分配.Items.Clear();
                    foreach (DataRow dr_操作权限 in dt_数据源.Rows)
                    {
                        string str_操作编码 = dr_操作权限["用户id"].ToString();
                        ListViewItem item = new ListViewItem();
                        item.Name = str_操作编码;
                        item.Text = dr_操作权限["用户名"].ToString() + "(" + str_操作编码 + ")";
                        if (!string.IsNullOrEmpty(dr_操作权限["模块id"]?.ToString()))
                        {
                            item.Checked = true;
                        }

                        lv_权限分配.Items.Add(item);
                    }
                    break;
                case E_权限类型.菜单分配角色:
                    lv_权限分配.Visible = true;
                    tv_权限分配.Visible = false;
                    lv_权限分配.Items.Clear();
                    foreach (DataRow dr_操作权限 in dt_数据源.Rows)
                    {
                        string str_操作编码 = dr_操作权限["角色id"].ToString();
                        ListViewItem item = new ListViewItem();
                        item.Name = str_操作编码;
                        item.Text = dr_操作权限["角色名"].ToString() + "(" + str_操作编码 + ")";
                        if (!string.IsNullOrEmpty(dr_操作权限["模块id"]?.ToString()))
                        {
                            item.Checked = true;
                        }

                        lv_权限分配.Items.Add(item);
                    }
                    break;
                case E_权限类型.角色分配人员:
                    lv_权限分配.Visible = true;
                    tv_权限分配.Visible = false;
                    lv_权限分配.Items.Clear();
                    foreach (DataRow dr_操作权限 in dt_数据源.Rows)
                    {
                        string str_操作编码 = dr_操作权限["用户id"].ToString();
                        ListViewItem item = new ListViewItem();
                        item.Name = str_操作编码;
                        item.Text = dr_操作权限["用户名"].ToString() + "(" + str_操作编码 + ")";
                        if (!string.IsNullOrEmpty(dr_操作权限["角色id"]?.ToString()))
                        {
                            item.Checked = true;
                        }

                        lv_权限分配.Items.Add(item);
                    }
                    break;
                case E_权限类型.人员分配菜单:
                    lv_权限分配.Visible = false;
                    tv_权限分配.Visible = true;
                    DataRow[] drs_一级功能 = dt_数据源.Select(string.Format("上级id='0'"));
                    if (drs_一级功能.Length > 0)
                    {
                        TreeNode tln_根节点 = tv_权限分配.Nodes.Add("-1", "菜单");
                        foreach (DataRow row in drs_一级功能)
                        {
                            string str_模块id = row["模块id"].ToString();
                            TreeNode tln_一级节点 = tln_根节点.Nodes.Add(row["模块名"].ToString() + "," + str_模块id, row["模块名"].ToString());
                            if (!string.IsNullOrEmpty(row["用户id"]?.ToString()))
                            {
                                tln_一级节点.Checked = true;
                                Set_上级节点选中状态(tln_一级节点);
                            }
                            M_构造_子节点(tln_一级节点, true, "用户id");
                        }
                    }
                    tv_权限分配.Nodes[0].Expand();
                    break;
                case E_权限类型.角色分配菜单:
                    lv_权限分配.Visible = false;
                    tv_权限分配.Visible = true;
                    DataRow[] drs_一级功能1 = dt_数据源.Select(string.Format("上级id='0'"));
                    if (drs_一级功能1.Length > 0)
                    {
                        TreeNode tln_根节点 = tv_权限分配.Nodes.Add("-1", "菜单");
                        foreach (DataRow row in drs_一级功能1)
                        {
                            string str_模块id = row["模块id"].ToString();
                            TreeNode tln_一级节点 = tln_根节点.Nodes.Add(row["模块名"].ToString() + "," + str_模块id, row["模块名"].ToString());
                            if (!string.IsNullOrEmpty(row["角色id"]?.ToString()))
                            {
                                tln_一级节点.Checked = true;
                                Set_上级节点选中状态(tln_一级节点);
                            }
                            M_构造_子节点(tln_一级节点, true, "角色id");
                        }
                    }
                    tv_权限分配.Nodes[0].Expand();
                    break;
            }
        }
        private void M_构造_子节点(TreeNode tln_父节点, bool b_是否多层, string str_主键名)
        {
            dt_数据源.DefaultView.RowFilter = "上级id = '" + tln_父节点.Name.Split(',')[1] + "'";

            foreach (DataRow row in dt_数据源.DefaultView.ToTable().Rows)
            {
                string str_模块id = row["模块id"].ToString();
                TreeNode tln_功能 = tln_父节点.Nodes.Add(row["模块名"] + "," + str_模块id, row["模块名"].ToString());
                if (!string.IsNullOrEmpty(row[str_主键名]?.ToString()))
                {
                    tln_功能.Checked = true;
                }
                if (b_是否多层)
                {
                    M_构造_子节点(tln_功能, true, str_主键名);
                }
            }
        }
        private void Set_上级节点选中状态(TreeNode tln_子节点)
        {
            TreeNode tln_父节点 = tln_子节点.Parent;
            if (tln_父节点 != null)
            {
                tln_父节点.Checked = true;
                Set_上级节点选中状态(tln_父节点);
            }
        }

        private DataTable M_加载权限列表()
        {
            inParam.P_模块名 = E_模块名称.基础业务;
            inParam.P_功能名 = P_权限类型.ToString();
            inParam.P1 = P_主键;
            inParam.P_系统ID = P_系统id;
            outParam = C_Server.Call(inParam);
            if (outParam.P_结果 == 1)
            {
                return outParam.P_数据集;
            }
            else
            {
                XtraMessageBox.Show(outParam.P_结果描述, "提示");
                return null;
            }
        }

        private void btn_确定_Click(object sender, System.EventArgs e)
        {
            dt_数据源.Rows.Clear();
            switch (P_权限类型)
            {
                case E_权限类型.菜单分配人员:
                    foreach (ListViewItem item in lv_权限分配.Items)
                    {
                        if (item.Checked)
                        {
                            DataRow dr_操作 = dt_数据源.NewRow();
                            dr_操作["用户id"] = item.Name;
                            dr_操作["用户名"] = "";
                            dr_操作["模块id"] = P_主键;
                            dt_数据源.Rows.Add(dr_操作);
                        }
                    }
                    break;
                case E_权限类型.菜单分配角色:
                    foreach (ListViewItem item in lv_权限分配.Items)
                    {
                        if (item.Checked)
                        {
                            DataRow dr_操作 = dt_数据源.NewRow();
                            dr_操作["角色id"] = item.Name;
                            dr_操作["角色名"] = "";
                            dr_操作["模块id"] = P_主键;
                            dt_数据源.Rows.Add(dr_操作);
                        }
                    }
                    break;
                case E_权限类型.角色分配人员:
                    foreach (ListViewItem item in lv_权限分配.Items)
                    {
                        if (item.Checked)
                        {
                            DataRow dr_操作 = dt_数据源.NewRow();
                            dr_操作["用户id"] = item.Name;
                            dr_操作["用户名"] = "";
                            dr_操作["角色id"] = P_主键;
                            dt_数据源.Rows.Add(dr_操作);
                        }
                    }
                    break;
                case E_权限类型.人员分配菜单:
                    foreach (TreeNode tln_根节点 in tv_权限分配.Nodes)
                    {
                        Get_下级节点选中值(tln_根节点, "用户id");
                    }
                    break;
                case E_权限类型.角色分配菜单:
                    foreach (TreeNode tln_根节点 in tv_权限分配.Nodes)
                    {
                        Get_下级节点选中值(tln_根节点, "角色id");
                    }
                    break;
            }
            inParam.P_模块名 = E_模块名称.基础业务;
            inParam.P_功能名 = P_权限类型.ToString() + (dt_数据源.Rows.Count == 0 ? "_删除" : "_修改");
            inParam.P_数据集 = dt_数据源;
            inParam.P1 = P_主键;
            outParam = C_Server.Call(inParam);
            if (outParam.P_结果 == 1)
            {
                XtraMessageBox.Show("保存成功", "提示");
                this.Close();
            }
            else
            {
                XtraMessageBox.Show(outParam.P_结果描述, "提示");
            }
        }
        private void Get_下级节点选中值(TreeNode tln_父节点, string str_主键名)
        {
            foreach (TreeNode tln_子节点 in tln_父节点.Nodes)
            {
                if (tln_子节点.Checked)
                {
                    Set_DataTableValue(tln_子节点, str_主键名);
                }
                Get_下级节点选中值(tln_子节点, str_主键名);
            }
        }
        private void Set_DataTableValue(TreeNode tln_节点, string str_主键名)
        {
            DataRow[] drs = dt_数据源.Select(string.Format(str_主键名 + "='{0}' and 模块id='{1}'", P_主键, tln_节点.Name.Split(',')[1]));
            if (drs.Length == 0)
            {
                DataRow dr_功能 = dt_数据源.NewRow();
                dr_功能["模块id"] = tln_节点.Name.Split(',')[1];
                dr_功能["模块名"] = tln_节点.Name.Split(',')[0];
                dr_功能[str_主键名] = P_主键;
                dt_数据源.Rows.Add(dr_功能);
            }
        }
        private void btn_取消_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
