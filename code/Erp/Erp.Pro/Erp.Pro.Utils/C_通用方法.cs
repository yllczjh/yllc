using DevExpress.XtraEditors;
using System.Data;
namespace Erp.Pro.Utils
{
    class C_通用方法
    {
        public static void M_绑定控件(LookUpEdit lue_绑定控件, string str_过滤条件, string str_表名)
        {
            switch (str_表名)
            {
                #region 基础项目_字典明细
                //case "基础项目_字典明细":

                //    C_全局数据集_共享.P_基础项目_字典明细.DefaultView.RowFilter = str_过滤条件;
                //    DataTable dt_数据字典 = C_全局数据集_共享.P_基础项目_字典明细.DefaultView.ToTable();

                //    var 数据字典 = from c in dt_数据字典.AsEnumerable()
                //               select new
                //               {
                //                   编码 = c.Field<string>("编码"),
                //                   名称 = c.Field<string>("名称")
                //               };
                //    DataTable dt_新数据字典 = 数据字典.MyCopyToDataTable();

                //    if (dt_数据字典.Rows.Count > 0)
                //    {

                //        lue_绑定控件.Properties.DataSource = dt_新数据字典;
                //        lue_绑定控件.Properties.NullText = "";

                //        lue_绑定控件.Properties.DisplayMember = "名称";
                //        lue_绑定控件.Properties.ValueMember = "编码";

                //    }

                //    break;
                #endregion
            }
        }

        public static void M_绑定控件(LookUpEdit lue_绑定控件, C_数据源 c_数据源)
        {
            DataTable dt_新数据字典 = c_数据源.DataSource.DefaultView.ToTable(true, new string[] { c_数据源.ValueMember, c_数据源.DisplayMember });

            lue_绑定控件.Properties.DataSource = dt_新数据字典;
            lue_绑定控件.Properties.NullText = "";

            lue_绑定控件.Properties.DisplayMember = c_数据源.DisplayMember;
            lue_绑定控件.Properties.ValueMember = c_数据源.ValueMember;
            //lue_绑定控件.ItemIndex = 0;
        }
    }
}
