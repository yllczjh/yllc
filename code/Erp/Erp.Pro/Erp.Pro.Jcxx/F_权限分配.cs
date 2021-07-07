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
        public F_权限分配(string str_系统id,E_权限类型 e, string id)
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
            }
        }


        private DataTable M_加载权限列表()
        {
            inParam.P_模块名 = E_模块名称.基础业务;
            inParam.P_功能名 = P_权限类型.ToString();
            inParam.P1 = P_主键;
            inParam.P2 = P_系统id;
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

        private void btn_取消_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
