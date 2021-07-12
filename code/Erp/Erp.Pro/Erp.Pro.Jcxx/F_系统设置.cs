using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
        #region 变量
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();
        string str_系统id;
        DataTable dt_菜单信息;
        DataTable dt_用户信息;
        DataTable dt_角色信息;
        DataTable dt_业务信息;
        #region 
        enum E_系统信息
        {
            菜单信息,
            用户信息,
            角色信息,
            业务信息
        }
        #endregion

        private string P_选择节点;

        public F_系统设置()
        {
            InitializeComponent();
        }
        #endregion

        private void F_系统设置_Load(object sender, EventArgs e)
        {
            com_系统id.ComboBox.DataSource = C_实体信息.C_共享数据集.P_系统信息;
            com_系统id.ComboBox.DisplayMember = "系统名称";
            com_系统id.ComboBox.ValueMember = "系统id";

            xtra_系统设置_SelectedPageChanged(null, null);
        }
        private void xtra_系统设置_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            string page = xtra_系统设置.SelectedTabPage.Name;
            switch (page)
            {
                case "page_菜单":
                    M_加载系统信息(E_系统信息.菜单信息);
                    M_初始化绑定一级树();
                    break;
                case "page_用户":
                    M_加载系统信息(E_系统信息.用户信息);
                    grc_用户维护.DataSource = dt_用户信息;
                    break;
                case "page_角色":
                    M_加载系统信息(E_系统信息.角色信息);
                    grc_角色维护.DataSource = dt_角色信息;
                    break;
                case "page_业务":
                    M_加载系统信息(E_系统信息.业务信息);
                    grc_业务维护.DataSource = dt_业务信息;
                    break;
            }
        }
        private void M_加载系统信息(E_系统信息 e_系统信息)
        {
            inParam.P_模块名 = E_模块名称.基础业务;
            inParam.P_功能名 = "加载系统信息_" + e_系统信息.ToString();
            inParam.P_系统ID = str_系统id;
            outParam = C_Server.Call(inParam);
            if (outParam.P_结果 == 1)
            {
                switch (e_系统信息)
                {
                    case E_系统信息.菜单信息:
                        if (null == outParam.P_数据集 || outParam.P_数据集.Rows.Count <= 0)
                        {
                            dt_菜单信息 = C_实体信息.C_共享数据集.P_菜单信息.Clone();
                        }
                        else
                        {
                            dt_菜单信息 = outParam.P_数据集;
                        }
                        if (!dt_菜单信息.Columns.Contains("编辑状态"))
                        {
                            dt_菜单信息.Columns.Add("编辑状态", typeof(string));
                        }
                        foreach (DataRow row in dt_菜单信息.Rows)
                        {
                            row["编辑状态"] = "0";
                        }
                        break;
                    case E_系统信息.用户信息:
                        if (null == outParam.P_数据集 || outParam.P_数据集.Rows.Count <= 0)
                        {
                            dt_用户信息 = C_实体信息.C_共享数据集.P_用户信息.Clone();
                        }
                        else
                        {
                            dt_用户信息 = outParam.P_数据集;
                        }
                        if (!dt_用户信息.Columns.Contains("编辑状态"))
                        {
                            dt_用户信息.Columns.Add("编辑状态", typeof(string));
                        }
                        foreach (DataRow row in dt_用户信息.Rows)
                        {
                            row["编辑状态"] = "0";
                        }
                        break;
                    case E_系统信息.角色信息:
                        if (null == outParam.P_数据集 || outParam.P_数据集.Rows.Count <= 0)
                        {
                            dt_角色信息 = C_实体信息.C_共享数据集.P_角色信息.Clone();
                        }
                        else
                        {
                            dt_角色信息 = outParam.P_数据集;
                        }
                        if (!dt_角色信息.Columns.Contains("编辑状态"))
                        {
                            dt_角色信息.Columns.Add("编辑状态", typeof(string));
                        }
                        foreach (DataRow row in dt_角色信息.Rows)
                        {
                            row["编辑状态"] = "0";
                        }
                        break;
                    case E_系统信息.业务信息:
                        if (null == outParam.P_数据集 || outParam.P_数据集.Rows.Count <= 0)
                        {
                            dt_业务信息 = C_实体信息.C_共享数据集.P_角色信息.Clone();
                        }
                        else
                        {
                            dt_业务信息 = outParam.P_数据集;
                        }
                        if (!dt_业务信息.Columns.Contains("编辑状态"))
                        {
                            dt_业务信息.Columns.Add("编辑状态", typeof(string));
                        }
                        foreach (DataRow row in dt_业务信息.Rows)
                        {
                            row["编辑状态"] = "0";
                        }
                        break;
                }
            }
            else
            {
                XtraMessageBox.Show(outParam.P_结果描述, "提示");
            }
        }
        private void com_系统id_SelectedIndexChanged(object sender, EventArgs e)
        {
            str_系统id = (com_系统id.SelectedItem as DataRowView).Row["系统id"].ToString();
            xtra_系统设置_SelectedPageChanged(null, null);
        }
        private void btn_刷新_Click(object sender, EventArgs e)
        {
            xtra_系统设置_SelectedPageChanged(null, null);
        }
        private void btn_新增_Click(object sender, EventArgs e)
        {
            string page = xtra_系统设置.SelectedTabPage.Name;
            switch (page)
            {
                case "page_菜单":
                    if (tree_菜单.SelectedNode == null)
                        return;

                    grv_菜单维护.AddNewRow();
                    this.grv_菜单维护.SetFocusedRowCellValue("rowid", "0");
                    this.grv_菜单维护.SetFocusedRowCellValue("编辑状态", "1");
                    this.grv_菜单维护.SetFocusedRowCellValue("系统id", str_系统id);
                    this.grv_菜单维护.SetFocusedRowCellValue("模块id", C_通用方法.GuidTo16String());
                    this.grv_菜单维护.SetFocusedRowCellValue("上级id", tree_菜单.SelectedNode.Name);
                    this.grv_菜单维护.SetFocusedRowCellValue("禁用_菜单", Boolean.Parse("false"));
                    break;
                case "page_用户":
                    grv_用户维护.AddNewRow();
                    this.grv_用户维护.SetFocusedRowCellValue("rowid", "0");
                    this.grv_用户维护.SetFocusedRowCellValue("编辑状态", "1");
                    this.grv_用户维护.SetFocusedRowCellValue("密码", C_通用方法.MD5_16D("<<123456>>"));
                    this.grv_用户维护.SetFocusedRowCellValue("禁用_用户", Boolean.Parse("false"));
                    break;
                case "page_角色":
                    grv_角色维护.AddNewRow();
                    this.grv_角色维护.SetFocusedRowCellValue("rowid", "0");
                    this.grv_角色维护.SetFocusedRowCellValue("编辑状态", "1");
                    this.grv_角色维护.SetFocusedRowCellValue("系统id", str_系统id);
                    this.grv_角色维护.SetFocusedRowCellValue("角色id", C_通用方法.GuidTo16String());
                    break;
                case "page_业务":
                    grv_业务维护.AddNewRow();
                    this.grv_业务维护.SetFocusedRowCellValue("序号", "0");
                    this.grv_业务维护.SetFocusedRowCellValue("编辑状态", "1");
                    this.grv_业务维护.SetFocusedRowCellValue("系统ID", str_系统id);
                    break;
            }
        }
        private void btn_删除_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == XtraMessageBox.Show("是否确定删除数据?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            string str_功能名 = string.Empty;
            string str_主键id = string.Empty;
            string id = string.Empty;
            DataRow dr_焦点行 = null;
            string page = xtra_系统设置.SelectedTabPage.Name;
            switch (page)
            {
                case "page_菜单":
                    dr_焦点行 = grv_菜单维护.GetDataRow(grv_菜单维护.FocusedRowHandle);
                    str_主键id = dr_焦点行["rowid"].ToString();
                    id = dr_焦点行["模块id"].ToString();
                    str_功能名 = "菜单信息_删除";
                    break;
                case "page_用户":
                    dr_焦点行 = grv_用户维护.GetDataRow(grv_用户维护.FocusedRowHandle);
                    str_主键id = dr_焦点行["rowid"].ToString();
                    id = dr_焦点行["用户id"].ToString();
                    str_功能名 = "用户信息_删除";
                    break;
                case "page_角色":
                    dr_焦点行 = grv_角色维护.GetDataRow(grv_角色维护.FocusedRowHandle);
                    str_主键id = dr_焦点行["rowid"].ToString();
                    id = dr_焦点行["角色id"].ToString();
                    str_功能名 = "角色信息_删除";
                    break;
                case "page_业务":
                    dr_焦点行 = grv_业务维护.GetDataRow(grv_业务维护.FocusedRowHandle);
                    str_主键id = dr_焦点行["序号"].ToString();
                    id = null;
                    str_功能名 = "业务信息_删除";
                    break;
            }
            inParam.P_模块名 = E_模块名称.基础业务;
            inParam.P_功能名 = str_功能名;
            inParam.P1 = str_主键id;
            inParam.P2 = id;
            outParam = C_Server.Call(inParam);
            if (outParam.P_结果 == 1)
            {
                XtraMessageBox.Show("删除成功", "提示");
                xtra_系统设置_SelectedPageChanged(null, null);
            }
            else
            {
                XtraMessageBox.Show(outParam.P_结果描述, "提示");
            }
        }
        private void btn_保存_Click(object sender, EventArgs e)
        {
            DataTable dt_数据源 = new DataTable();
            string str_功能名 = string.Empty;
            string page = xtra_系统设置.SelectedTabPage.Name;
            switch (page)
            {
                case "page_菜单":
                    grv_菜单维护.CloseEditor();
                    str_功能名 = "菜单信息_保存";
                    dt_数据源 = (grv_菜单维护.DataSource as DataView).ToTable().Clone();
                    foreach (DataRow row in (grv_菜单维护.DataSource as DataView).ToTable().Rows)
                    {
                        if (row["编辑状态"].ToString() == "1")
                        {
                            DataRow dr = dt_数据源.NewRow();
                            dr.ItemArray = row.ItemArray;
                            dt_数据源.Rows.Add(dr);
                        }
                        if (string.IsNullOrEmpty(row["模块名"].ToString()))
                        {
                            MessageBox.Show("请补全[模块名]字段信息！");
                            return;
                        }
                    }
                    break;
                case "page_用户":
                    grv_用户维护.CloseEditor();
                    str_功能名 = "用户信息_保存";
                    dt_数据源 = (grv_用户维护.DataSource as DataView).ToTable().Clone();
                    foreach (DataRow row in (grv_用户维护.DataSource as DataView).ToTable().Rows)
                    {
                        if (row["编辑状态"].ToString() == "1")
                        {
                            DataRow dr = dt_数据源.NewRow();
                            dr.ItemArray = row.ItemArray;
                            dt_数据源.Rows.Add(dr);
                        }
                        if (string.IsNullOrEmpty(row["用户名"].ToString()))
                        {
                            MessageBox.Show("请补全[用户名]字段信息！");
                            return;
                        }
                    }
                    break;
                case "page_角色":
                    grv_角色维护.CloseEditor();
                    str_功能名 = "角色信息_保存";
                    dt_数据源 = (grv_角色维护.DataSource as DataView).ToTable().Clone();
                    foreach (DataRow row in (grv_角色维护.DataSource as DataView).ToTable().Rows)
                    {
                        if (row["编辑状态"].ToString() == "1")
                        {
                            DataRow dr = dt_数据源.NewRow();
                            dr.ItemArray = row.ItemArray;
                            dt_数据源.Rows.Add(dr);
                        }
                        if (string.IsNullOrEmpty(row["角色名"].ToString()))
                        {
                            MessageBox.Show("请补全[角色名]字段信息！");
                            return;
                        }
                    }
                    break;
                case "page_业务":
                    grv_业务维护.CloseEditor();
                    str_功能名 = "业务信息_保存";
                    dt_数据源 = (grv_业务维护.DataSource as DataView).ToTable().Clone();
                    foreach (DataRow row in (grv_业务维护.DataSource as DataView).ToTable().Rows)
                    {
                        if (row["编辑状态"].ToString() == "1")
                        {
                            DataRow dr = dt_数据源.NewRow();
                            dr.ItemArray = row.ItemArray;
                            dt_数据源.Rows.Add(dr);
                        }
                        if (string.IsNullOrEmpty(row["业务编号"].ToString()))
                        {
                            MessageBox.Show("请补全[业务编号]字段信息！");
                            return;
                        }
                    }
                    break;
            }
            if (string.IsNullOrEmpty(str_功能名)) return;
            inParam.P_模块名 = E_模块名称.基础业务;
            inParam.P_功能名 = str_功能名;
            inParam.P_数据集 = dt_数据源;
            inParam.P_系统ID = str_系统id;
            outParam = C_Server.Call(inParam);
            if (outParam.P_结果 == 1)
            {
                if (page == "page_用户")
                {
                    XtraMessageBox.Show("保存成功,新增用户密码为[123456]", "提示");
                }
                else
                {
                    XtraMessageBox.Show("保存成功", "提示");
                }
                xtra_系统设置_SelectedPageChanged(null, null);
            }
            else
            {
                XtraMessageBox.Show(outParam.P_结果描述, "提示");
            }
        }

        #region 菜单维护
        private void M_初始化绑定一级树()
        {
            try
            {
                tree_菜单.Nodes.Clear();
                C_通用方法.M_树形助手(dt_菜单信息, tree_菜单, "菜单", "0", "模块名", "模块id", "上级id");
                tree_菜单.ExpandAll();
                tree_菜单.SelectedNode = tree_菜单.Nodes[0];
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
            var var_字典分类 = from en_字典分类 in dt_菜单信息.AsEnumerable()
                           where en_字典分类.Field<string>("上级id").ToString() == P_选择节点
                           select en_字典分类;

            DataTable dt_菜单;
            if (var_字典分类.ToList().Count > 0)
            {
                dt_菜单 = var_字典分类.CopyToDataTable();
            }
            else
            {
                dt_菜单 = dt_菜单信息.Clone();
            }

            grc_菜单维护.DataSource = dt_菜单;
            grc_菜单维护.RefreshDataSource();
        }
        private void 菜单权限操作_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

            DataRow dr_菜单维护 = grv_菜单维护.GetDataRow(grv_菜单维护.FocusedRowHandle);
            if (null == dr_菜单维护) return;
            if (dr_菜单维护["rowid"].ToString() == "0")
            {
                MessageBox.Show("请先保存");
                return;
            }

            if (e.Button.Caption == "人员分配")
            {
                F_权限分配 f = new F_权限分配(str_系统id, E_权限类型.菜单分配人员, dr_菜单维护["模块id"].ToString());
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog();
                f.Dispose();
            }
            else if (e.Button.Caption == "角色分配")
            {
                F_权限分配 f = new F_权限分配(str_系统id, E_权限类型.菜单分配角色, dr_菜单维护["模块id"].ToString());
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog();
                f.Dispose();
            }
        }
        private void grv_菜单维护_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow dr_菜单维护 = grv_菜单维护.GetDataRow(grv_菜单维护.FocusedRowHandle);
            dr_菜单维护["编辑状态"] = "1";
        }

        #endregion

        #region 用户信息
        private void grv_用户维护_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow dr_用户维护 = grv_用户维护.GetDataRow(grv_用户维护.FocusedRowHandle);
            dr_用户维护["编辑状态"] = "1";
        }

        private void 用户权限操作_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DataRow dr_用户维护 = grv_用户维护.GetDataRow(grv_用户维护.FocusedRowHandle);
            if (null == dr_用户维护) return;
            if (dr_用户维护["rowid"].ToString() == "0")
            {
                MessageBox.Show("请先保存");
                return;
            }

            if (e.Button.Caption == "菜单分配")
            {
                F_权限分配 f = new F_权限分配(str_系统id, E_权限类型.人员分配菜单, dr_用户维护["用户id"].ToString());
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog();
                f.Dispose();
            }
        }
        #endregion

        #region 角色信息
        private void grv_角色维护_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow dr_角色维护 = grv_角色维护.GetDataRow(grv_角色维护.FocusedRowHandle);
            dr_角色维护["编辑状态"] = "1";
        }
        private void 角色权限操作_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DataRow dr_角色维护 = grv_角色维护.GetDataRow(grv_角色维护.FocusedRowHandle);
            if (null == dr_角色维护) return;
            if (dr_角色维护["rowid"].ToString() == "0")
            {
                MessageBox.Show("请先保存");
                return;
            }

            if (e.Button.Caption == "人员分配")
            {
                F_权限分配 f = new F_权限分配(str_系统id, E_权限类型.角色分配人员, dr_角色维护["角色id"].ToString());
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog();
                f.Dispose();
            }
            else if (e.Button.Caption == "菜单分配")
            {
                F_权限分配 f = new F_权限分配(str_系统id, E_权限类型.角色分配菜单, dr_角色维护["角色id"].ToString());
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog();
                f.Dispose();
            }
        }
        #endregion
    }
}
