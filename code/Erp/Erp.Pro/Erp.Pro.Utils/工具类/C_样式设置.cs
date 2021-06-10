using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting.Native;
using Erp.Server.Helper;
using Erp.Server.Init;
using System.Data;
using static Erp.Server.Helper.ServerHelper;

namespace Erp.Pro.Utils.工具类
{
    public class C_样式设置
    {
        static ServerHelper.Params inParam = new ServerHelper.Params();
        static ServerHelper.Params outParam = new ServerHelper.Params();

        /// <summary>
        /// 初始化样式
        /// </summary>
        /// <param name="gridView"></param>
        public static void Init(GridView gridView)
        {
            if (null == C_实体信息.C_共享数据集.P_样式列表 || C_实体信息.C_共享数据集.P_样式列表.Rows.Count <= 0)
            {
                Load();
            }

            if (null != C_实体信息.C_共享数据集.P_样式列表 && C_实体信息.C_共享数据集.P_样式列表.Rows.Count > 0)
            {
                C_实体信息.C_共享数据集.P_样式列表.DefaultView.RowFilter = "样式ID='" + "用户信息" + "'";
                DataTable dt_样式列表 = C_实体信息.C_共享数据集.P_样式列表.DefaultView.ToTable();
                if (null != dt_样式列表 && dt_样式列表.Rows.Count > 0)
                {
                    string[] arr_字段名 = dt_样式列表.Rows[0]["字段名"].ToString().Split('^');
                    for (int i = 0; i < arr_字段名.Length; i++)
                    {
                        string str_字段名 = arr_字段名[i];
                        try
                        {
                            gridView.Columns[str_字段名].Caption = dt_样式列表.Rows[0]["显示名称"].ToString().Split('^')[i];
                        }
                        catch (System.Exception)
                        {
                            gridView.Columns[str_字段名].Caption = str_字段名;
                        }
                        try
                        {
                            gridView.Columns[str_字段名].Width = int.Parse(dt_样式列表.Rows[0]["宽度"].ToString().Split('^')[i]);
                        }
                        catch (System.Exception)
                        {
                            gridView.Columns[str_字段名].Width = 100;
                        }
                        try
                        {
                            gridView.Columns[str_字段名].VisibleIndex = int.Parse(dt_样式列表.Rows[0]["排序"].ToString().Split('^')[i]);
                        }
                        catch (System.Exception)
                        {
                            gridView.Columns[str_字段名].VisibleIndex = i;
                        }
                    }
                }

                if (null != gridView.Columns["选择"])
                {
                    gridView.Columns["选择"].VisibleIndex = 0;
                    gridView.Columns["选择"].Width = 50;
                }
            }
        }
        /// <summary>
        /// 保存样式
        /// </summary>
        /// <param name="gridView"></param>
        public static bool Save(GridView gridView, string str_页面名称)
        {
            inParam.Clear();
            inParam.p0 = E_模块名称.通用业务;
            inParam.p1 = "样式列表";
            inParam.p2 = "保存";
            inParam.p3 = C_实体信息.C_共享变量.用户ID;
            inParam.p4 = str_页面名称;
            inParam.p5 = GetDataTable(gridView, str_页面名称).Rows[0];
            outParam = C_Server.Call(inParam);
            if (outParam.p0.ToString() == "1")
            {
                C_实体信息.C_共享数据集.P_样式列表 = outParam.p2 as DataTable;
                return true;
            }
            else
            {
                XtraMessageBox.Show(outParam.p1.ToString(), "提示");
                return false;
            }
        }
        /// <summary>
        /// 获取样式表
        /// </summary>
        private static void Load()
        {
            inParam.Clear();
            inParam.p0 = E_模块名称.通用业务;
            inParam.p1 = "样式列表";
            inParam.p2 = "初始化";
            inParam.p3 = C_实体信息.C_共享变量.用户ID;
            outParam = C_Server.Call(inParam);
            if (outParam.p0.ToString() == "1")
            {
                C_实体信息.C_共享数据集.P_样式列表 = outParam.p2 as DataTable;
            }
            else
            {
                XtraMessageBox.Show(outParam.p1.ToString(), "提示");
            }
        }

        private static DataTable GetDataTable(GridView gridView, string str_页面名称)
        {
            DataTable dt_样式列表 = new DataTable();
            if (null == C_实体信息.C_共享数据集.P_样式列表 || C_实体信息.C_共享数据集.P_样式列表.Rows.Count <= 0)
            {
                dt_样式列表.Columns.Add("rowid", typeof(int));
                dt_样式列表.Columns.Add("系统ID", typeof(string));
                dt_样式列表.Columns.Add("用户ID", typeof(string));
                dt_样式列表.Columns.Add("样式ID", typeof(string));
                dt_样式列表.Columns.Add("字段名", typeof(string));
                dt_样式列表.Columns.Add("显示名称", typeof(string));
                dt_样式列表.Columns.Add("宽度", typeof(string));
                dt_样式列表.Columns.Add("排序", typeof(string));
            }
            else
            {
                C_实体信息.C_共享数据集.P_样式列表.DefaultView.RowFilter = "用户ID='" + C_实体信息.C_共享变量.用户ID + "' and 样式ID='" + str_页面名称 + "'";
                dt_样式列表 = C_实体信息.C_共享数据集.P_样式列表.DefaultView.ToTable();
            }
            string str_字段名 = "";
            gridView.Columns.ForEach(column => str_字段名 += column.FieldName + "^");
            str_字段名 = str_字段名.TrimEnd('^');
            string str_显示名称 = "";
            gridView.Columns.ForEach(column => str_显示名称 += (string.IsNullOrEmpty(column.Caption) ? column.FieldName : column.Caption) + "^");
            str_显示名称 = str_显示名称.TrimEnd('^');
            string str_宽度 = "";
            gridView.Columns.ForEach(column => str_宽度 += column.Width + "^");
            str_宽度 = str_宽度.TrimEnd('^');
            string str_排序 = "";
            gridView.Columns.ForEach(column => str_排序 += column.VisibleIndex + "^");
            str_排序 = str_排序.TrimEnd('^');
            if (null == dt_样式列表 || dt_样式列表.Rows.Count <= 0)
            {
                dt_样式列表.Rows.Add(new object[] { null, C_实体信息.C_共享变量.系统ID, C_实体信息.C_共享变量.用户ID, str_页面名称, str_字段名, str_显示名称, str_宽度, str_排序 });
            }
            else
            {
                dt_样式列表.Rows[0]["字段名"] = str_字段名;
                dt_样式列表.Rows[0]["显示名称"] = str_显示名称;
                dt_样式列表.Rows[0]["宽度"] = str_宽度;
                dt_样式列表.Rows[0]["排序"] = str_排序;
            }

            return dt_样式列表;
        }
    }
}
