using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Erp.Pro.Utils.工具类;
using Erp.Pro.Utils.公共窗体;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using static Erp.Pro.Utils.C_实体信息;
using DevExpress.XtraEditors;
using System.Drawing;
using static Erp.Server.Helper.ServerHelper;
using Erp.Server.Helper;
using Erp.Server.Init;

namespace Erp.Pro.Utils.自定义控件
{
    public partial class U_通用列表编辑 : UserControl
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();

        public string P_操作类型;
        public int P_焦点行 = 0;
        public C_控件参数[] _P_控件参数;

        private DataTable dt_数据源;
        [Description("设置编辑界面控件相关信息,在代码中赋值"), Category("#通用列表自定义属性")]
        public C_控件参数[] P_控件参数
        {
            get { return _P_控件参数; }
            set { _P_控件参数 = value; }
        }

        public int _P_每行显示列数 = 2;
        [Browsable(true)]
        [Description("设置编辑界面上每行显示的控件列数"), Category("#通用列表自定义属性")]
        public int P_每行显示列数
        {
            get { return _P_每行显示列数; }
            set { _P_每行显示列数 = value; }
        }

        public string _P_页面名称;
        [Browsable(false)]
        [Description("页面名称(通过[页面名称_按钮显示名]来定义服务名)"), Category("#通用列表自定义属性")]
        public string P_页面名称
        {
            get { return _P_页面名称; }
            set { _P_页面名称 = value; }
        }

        public U_通用列表编辑()
        {
            InitializeComponent();
        }
        private void btn_刷新_Click(object sender, EventArgs e)
        {
            inParam.Clear();
            inParam.p0 = E_模块名称.通用业务;
            inParam.p1 = P_页面名称;
            inParam.p2 = "初始化";
            outParam = C_Server.Call(inParam);
            if (outParam.p0.ToString() == "1")
            {
                dt_数据源 = outParam.p2 as DataTable;
                dt_数据源.Columns.Add("选择", typeof(bool)).SetOrdinal(0);
                foreach (DataRow row in dt_数据源.Rows)
                {
                    row["选择"] = false;
                }
                GridControl.DataSource = dt_数据源;
                C_样式设置.Init(GridView);
            }
            else
            {
                XtraMessageBox.Show(outParam.p1.ToString(), "提示");
            }
        }

        private void btn_新增_Click(object sender, EventArgs e)
        {
            OnAction_新增(null, null);

            P_操作类型 = "新增";
            P_焦点行 = 0;
            F_通用编辑页面 f_编辑 = new F_通用编辑页面(this);
            f_编辑.StartPosition = FormStartPosition.CenterParent;
            f_编辑.ShowDialog();
            f_编辑.Dispose();
            M_加载列表数据();
            GridView.FocusedRowHandle = P_焦点行;
        }

        private void btn_修改_Click(object sender, EventArgs e)
        {
            OnAction_修改(null, null);

            P_操作类型 = "修改";
            P_焦点行 = GridView.GetFocusedDataSourceRowIndex();
            F_通用编辑页面 f_编辑 = new F_通用编辑页面(this);
            f_编辑.StartPosition = FormStartPosition.CenterParent;
            f_编辑.ShowDialog();
            f_编辑.Dispose();
            M_加载列表数据();
            GridView.FocusedRowHandle = P_焦点行;
        }

        private void btn_删除_Click(object sender, EventArgs e)
        {
            GridView.CloseEditor();
            dt_数据源.AcceptChanges();
            DataTable dt_删除行 = dt_数据源.Copy();
            dt_删除行.DefaultView.RowFilter = "选择='True'";
            dt_删除行 = dt_删除行.DefaultView.ToTable();
            //有复选框选择的则删除选中的，没有则删除焦点行的
            if (dt_删除行.Rows.Count <= 0)
            {
                P_焦点行 = GridView.FocusedRowHandle;
                DataRow dr_删除行 = GridView.GetDataRow(P_焦点行);
                if (null == dr_删除行)
                {
                    XtraMessageBox.Show("请选择要删除的数据!", "提示");
                    return;
                }
                dt_删除行.Rows.Add(dr_删除行.ItemArray);
            }

            if (DialogResult.Yes == XtraMessageBox.Show("是否要删除?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                inParam.p0 = E_模块名称.通用业务;
                inParam.p1 = P_页面名称;
                inParam.p2 = "删除";
                inParam.p3 = dt_删除行;
                outParam = C_Server.Call(inParam);

                if (outParam.p0.ToString() == "1")
                {
                    XtraMessageBox.Show("删除成功!", "提示");
                    if (dt_删除行.Rows.Count <= 0)
                    {
                        GridView.DeleteSelectedRows();
                    }
                    else
                    {
                        for (int i = GridView.RowCount - 1; i > 0; i--)
                        {
                            DataRow dr = GridView.GetDataRow(i);
                            if (dr["选择"].ToString() == "True")
                            {
                                GridView.DeleteRow(i);
                            }
                        }
                    }

                    //M_加载列表数据();
                    GridView.FocusedRowHandle = P_焦点行;
                }
                else
                {
                    XtraMessageBox.Show(outParam.p1.ToString(), "提示");
                }
            }
        }

        public event EventHandler 新增处理;
        protected void OnAction_新增(object sender, EventArgs e)
        {
            if (新增处理 != null)
            {
                新增处理(sender, e);
            }
        }

        public event EventHandler 修改处理;
        protected void OnAction_修改(object sender, EventArgs e)
        {
            if (修改处理 != null)
            {
                修改处理(sender, e);
            }
        }

        private void btn_导出_Click(object sender, EventArgs e)
        {
            C_文件导出 exportUtil = new C_文件导出();
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.RestoreDirectory = true;
            saveDialog.FilterIndex = 1;

            saveDialog.FileName = P_页面名称 + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

            saveDialog.Title = "保存文件";
            saveDialog.Filter = "Excel files(*.xls)|*.xls|Excel files(*.xlsx)|*.xlsx|All files|*.*";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string str_文件类型 = Path.GetExtension(saveDialog.FileName).Split('.')[1].ToLower();
                exportUtil.ExportGridDataEx(GridControl, str_文件类型, saveDialog.FileName);
            }
        }
        public void M_加载列表数据()
        {
            btn_刷新_Click(null, null);
        }

        #region 右键菜单
        GridHitInfo info;
        private void GridControl_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                info = GridView.CalcHitInfo(e.Location);
                if (e.Button == MouseButtons.Right && !info.InRow && info.Column.FieldName != "选择")
                {
                    popupMenu1.ShowPopup(GridControl.PointToScreen(e.Location));
                    //加载隐藏列菜单
                    for (int i = 0; i < popupMenu1.ItemLinks.Count; i++)
                    {
                        if (popupMenu1.ItemLinks[i].GetType() == typeof(BarSubItemLink))
                        {
                            BarSubItemLink barSubItem = (BarSubItemLink)popupMenu1.ItemLinks[i];
                            barSubItem.Item.ItemLinks.Clear();
                            foreach (GridColumn column in GridView.Columns)
                            {
                                if (column.VisibleIndex == -1)
                                {
                                    BarButtonItem b = new BarButtonItem();
                                    b.Tag = column.FieldName;
                                    b.ItemClick += new ItemClickEventHandler(menu_显示隐藏列_ItemClick);
                                    b.Caption = string.IsNullOrEmpty(column.Caption) ? column.FieldName : column.Caption;
                                    barSubItem.Item.ItemLinks.Add(b);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void menu_右键_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Name == "menu_修改列别名")
            {
                F_修改列别名 f_别名 = new F_修改列别名(string.IsNullOrEmpty(info.Column.Caption) ? info.Column.FieldName : info.Column.Caption);
                f_别名.StartPosition = FormStartPosition.CenterParent;
                f_别名.ShowDialog();
                f_别名.Dispose();
                if (f_别名.B_结果)
                {
                    GridView.Columns[info.Column.FieldName].Caption = f_别名.P_别名;
                    if (!C_样式设置.Save(GridView, P_页面名称))
                    {
                        GridView.Columns[info.Column.FieldName].Caption = info.Column.Caption;
                    }
                }
            }
            else if (e.Item.Name == "menu_隐藏列")
            {
                int index = GridView.Columns[info.Column.FieldName].VisibleIndex;
                GridView.Columns[info.Column.FieldName].VisibleIndex = -1;
                if (!C_样式设置.Save(GridView, P_页面名称))
                {
                    GridView.Columns[info.Column.FieldName].VisibleIndex = index;
                }
            }
            else if (e.Item.Name == "menu_显示隐藏列")
            {

            }
        }
        private void menu_显示隐藏列_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView.Columns[e.Item.Tag.ToString()].VisibleIndex = info.Column.VisibleIndex;
        }

        #endregion

        private void btn_保存样式_Click(object sender, EventArgs e)
        {
            if (C_样式设置.Save(GridView, P_页面名称))
            {
                XtraMessageBox.Show("保存成功", "提示");
            }
        }

        private void GridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = GridView.CalcHitInfo(new Point(e.X, e.Y));
                if (e.Button == MouseButtons.Left && e.Clicks == 2)//判断是否左键双击
                {
                    //判断光标是否在行范围内
                    if (hInfo.InRow)
                    {
                        btn_修改_Click(null, null);
                    }
                }
            }
            catch (Exception)
            {
                //IM_IOT_ST.Tools.ExceptionHelper.ShowException(ex);
            }
        }
    }
}
