using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Erp.Pro.Utils;
using Erp.Server.Helper;
using Erp.Server.Init;
using System.Data;
using System.Windows.Forms;
using static DevExpress.XtraEditors.BaseCheckedListBoxControl;
using static Erp.Server.Helper.ServerHelper;

namespace Erp.Pro.Jcxx
{
    public partial class F_权限分配 : XtraForm
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();
        DataTable dt_数据源;
        E_权限类型 e_权限类型;
        CheckedListBoxControl c;
        string P_主键;
        public F_权限分配(E_权限类型 e, string id)
        {
            InitializeComponent();
            e_权限类型 = e;
            P_主键 = id;
        }

        private void F_权限分配_Load(object sender, System.EventArgs e)
        {
            switch (e_权限类型)
            {
                case E_权限类型.菜单分配人员:
                    c = new CheckedListBoxControl();
                    dt_数据源 = M_加载权限列表();
                    panel_主.Controls.Add(c);
                    c.Dock = DockStyle.Fill;
                    c.Show();
                    c.CheckOnClick = true;

                    CheckedListBoxItem[] items = new CheckedListBoxItem[dt_数据源.Rows.Count];
                    for (int i = 0; i < dt_数据源.Rows.Count; i++)
                    {
                        items[i] = new CheckedListBoxItem(dt_数据源.Rows[i]["用户id"].ToString(), dt_数据源.Rows[i]["用户名"] + "(" + dt_数据源.Rows[i]["用户id"].ToString() + ")", !string.IsNullOrEmpty(dt_数据源.Rows[i]["模块id"].ToString()) ? CheckState.Checked : CheckState.Unchecked, true);
                    }
                    c.Items.AddRange(items);
                    break;
            }
        }


        private DataTable M_加载权限列表()
        {
            inParam.P_模块名 = E_模块名称.基础业务;
            inParam.P_功能名 = e_权限类型.ToString();
            inParam.P1 = P_主键;
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
            switch (e_权限类型)
            {
                case E_权限类型.菜单分配人员:
                    DataTable dt = new DataTable();
                    dt_数据源.Rows.Clear();
                    foreach (object oo in c.CheckedItems)
                    {
                        DataRow row = dt_数据源.NewRow();
                        row["用户id"] = ((CheckedListBoxItem)oo).Value;
                        row["用户名"] = "";
                        row["模块id"] = P_主键;
                        dt_数据源.Rows.Add(row);
                    }

                    inParam.P_模块名 = E_模块名称.基础业务;
                    inParam.P_功能名 = e_权限类型.ToString() + "_保存";
                    inParam.P_数据集 = dt_数据源;
                    inParam.P1 = P_主键;
                    outParam = C_Server.Call(inParam);
                    if (outParam.P_结果 == 1)
                    {
                        XtraMessageBox.Show("保存成功", "提示");
                    }
                    else
                    {
                        XtraMessageBox.Show(outParam.P_结果描述, "提示");
                    }
                    break;
            }
        }

        private void btn_取消_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
