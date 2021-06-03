using DevExpress.XtraEditors;
using System.Data;
using static Erp.Pro.Utils.C_实体信息;

namespace Erp.Pro.Utils
{
    class C_通用方法
    {
        public static void M_绑定控件(LookUpEdit lue_绑定控件, C_数据源 c_数据源)
        {
            DataTable dt_新数据字典 = c_数据源.DataSource.DefaultView.ToTable(true, new string[] { c_数据源.ValueMember, c_数据源.DisplayMember });

            lue_绑定控件.Properties.DataSource = dt_新数据字典;
            lue_绑定控件.Properties.NullText = "";

            lue_绑定控件.Properties.DisplayMember = c_数据源.DisplayMember;
            lue_绑定控件.Properties.ValueMember = c_数据源.ValueMember;
            //lue_绑定控件.ItemIndex = 0;
        }

        public static void M_绑定控件(ComboBoxEdit com_绑定控件, C_数据源 c_数据源)
        {
            com_绑定控件.Properties.Items.Clear();

            foreach (string str in c_数据源.list)
            {
                com_绑定控件.Properties.Items.Add(str);
            }
        }
    }
}
