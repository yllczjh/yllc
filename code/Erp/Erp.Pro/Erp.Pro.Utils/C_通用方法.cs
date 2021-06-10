﻿using DevExpress.XtraEditors;
using System.Data;
using System.Text;
using System.Configuration;
using static Erp.Pro.Utils.C_实体信息;
using System.Windows.Forms;

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

        /// <summary>
        /// 字典分类树形绑定助手 
        /// </summary>
        /// <param name="dt_数据集">传入的数据集</param>
        /// <param name="list_树">树形控件</param>
        /// <param name="str_根节点编码">根节点编码</param>
        /// <param name="str_根节点名称">根节点名称</param>
        /// <param name="str_显示名称">字段名称</param>
        /// <param name="str_显示编码">字段编码</param>
        /// <param name="str_上级编码">字段上级编码</param>
        /// <param name="img">节点图片</param>
        /// <param name="i_显示图片">节点图片下标</param>
        public static void M_树形助手(DataTable dt_数据集, TreeView list_树, string str_根节点名称, string str_根节点编码, string str_字段名称, string str_字段编码, string str_字段上级编码)
        {
            TreeNode tln_节点 = list_树.Nodes.Add(str_根节点编码, str_根节点名称);

            foreach (DataRow row_行集 in dt_数据集.Rows)
            {
                if (row_行集[str_字段上级编码].ToString() == str_根节点编码)
                {
                    C_通用方法.M_创建子节点(list_树, tln_节点, dt_数据集, row_行集[str_字段名称].ToString(), row_行集[str_字段编码].ToString(), row_行集[str_字段上级编码].ToString(), str_字段名称, str_字段编码, str_字段上级编码);
                }
            }
        }

        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="list_树">传入的树形控件</param>
        /// <param name="tln_父节点">父节点</param>
        /// <param name="dt_数据集">传入的数据结果集</param>
        /// <param name="str_显示名称">子节点名称</param>
        /// <param name="str_显示编码">子节点编码</param>
        /// <param name="str_上级编码">上级编码</param>
        public static void M_创建子节点(TreeView list_树, TreeNode tln_父节点, DataTable dt_数据集, string str_名称, string str_编码, string str_上级编码, string str_字段名称, string str_字段编码, string str_字段上级编码)
        {
            TreeNode tln_子节点 = tln_父节点.Nodes.Add(str_编码, str_名称);
            if (str_编码 != str_上级编码)
            {
                foreach (DataRow row_子集 in dt_数据集.Rows)
                {
                    if (row_子集[str_字段上级编码].ToString() == str_编码)
                    {
                        C_通用方法.M_创建子节点(list_树, tln_子节点, dt_数据集, row_子集[str_字段名称].ToString(), row_子集[str_字段编码].ToString(), str_编码, str_字段名称, str_字段编码, str_字段上级编码);
                    }
                }
            }
        }
    }
}
