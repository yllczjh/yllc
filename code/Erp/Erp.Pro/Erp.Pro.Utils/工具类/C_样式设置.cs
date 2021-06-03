using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Erp.Server.Init;
using System.Data;
using System.Windows.Forms;
using static Erp.Server.Init.C_系统参数;

namespace Erp.Pro.Utils.工具类
{
    public class C_样式设置
    {
        static ServerParams inParam = new ServerParams();
        static ServerParams outParam = new ServerParams();

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
                foreach (DataRow row in dt_样式列表.Rows)
                {
                    string str_字段名 = row["字段名"].ToString();
                    gridView.Columns[str_字段名].Caption = row["显示名称"].ToString();
                    gridView.Columns[str_字段名].Width = int.Parse(row["宽度"].ToString());
                    gridView.Columns[str_字段名].VisibleIndex = int.Parse(row["排序"].ToString());
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
            inParam.p5 = GetDataTable(gridView, str_页面名称);
            outParam = C_Server.Call(inParam);
            if (outParam.p0.ToString() == "1")
            {
                C_实体信息.C_共享数据集.P_样式列表 = outParam.p2 as DataTable;
                return true;
            }
            else
            {
                MessageBox.Show(outParam.p1.ToString(), "提示");
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
                MessageBox.Show(outParam.p1.ToString(), "提示");
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
                dt_样式列表.Columns.Add("宽度", typeof(int));
                dt_样式列表.Columns.Add("排序", typeof(int));
            }
            else
            {
                C_实体信息.C_共享数据集.P_样式列表.DefaultView.RowFilter = "用户ID='" + C_实体信息.C_共享变量.用户ID + "' and 样式ID='" + str_页面名称 + "'";
                dt_样式列表 = C_实体信息.C_共享数据集.P_样式列表.DefaultView.ToTable();
            }

            if (null == dt_样式列表 || dt_样式列表.Rows.Count <= 0)
            {
                foreach (GridColumn column in gridView.Columns)
                {
                    dt_样式列表.Rows.Add(new object[] { null, "系统ID", C_实体信息.C_共享变量.用户ID, str_页面名称, column.FieldName, string.IsNullOrEmpty(column.Caption) ? column.FieldName : column.Caption, column.Width, column.VisibleIndex });
                }
            }
            else
            {
                foreach (DataRow dr in dt_样式列表.Rows)
                {
                    dr["显示名称"] = gridView.Columns[dr["字段名"].ToString()].Caption;
                    dr["宽度"] = gridView.Columns[dr["字段名"].ToString()].Width;
                    dr["排序"] = gridView.Columns[dr["字段名"].ToString()].VisibleIndex;
                }
            }

            return dt_样式列表;
        }
    }
}
