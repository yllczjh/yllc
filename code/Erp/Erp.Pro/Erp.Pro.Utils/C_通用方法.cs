using DevExpress.XtraEditors;
using System.Data;
using System.Text;
using static Erp.Pro.Utils.C_实体信息;

namespace Erp.Pro.Utils
{
    public class C_通用方法
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
        /// <summary>
        /// 根据主键拼接sql in字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="str_主键"></param>
        /// <returns></returns>
        public static string M_获取主键IN(DataTable dt, string str_主键)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("'").Append(row[str_主键]).Append("',");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString(); 
        }
    }
}
