﻿using DevExpress.XtraEditors;
using System.Data;
using System.Text;
using static Erp.Pro.Utils.C_实体信息;
using System.Windows.Forms;
using System;
using Erp.Server.Helper;
using static Erp.Server.Helper.ServerHelper;
using Erp.Server.Init;
using static Erp.Pro.Utils.C_实体信息.C_共享数据集;
using System.Security.Cryptography;

namespace Erp.Pro.Utils
{
    public class C_通用方法
    {
        static ServerHelper.Params inParam = new ServerHelper.Params();
        static ServerHelper.Params outParam = new ServerHelper.Params();

        #region 控件绑定
        public static void M_绑定控件(LookUpEdit lue_绑定控件, C_数据源 c_数据源)
        {
            DataTable dt_新数据字典 = c_数据源.DataSource.DefaultView.ToTable(true, new string[] { c_数据源.ValueMember, c_数据源.DisplayMember });

            lue_绑定控件.Properties.DataSource = dt_新数据字典;
            lue_绑定控件.Properties.NullText = "";

            lue_绑定控件.Properties.DisplayMember = c_数据源.DisplayMember;
            lue_绑定控件.Properties.ValueMember = c_数据源.ValueMember;
        }

        public static void M_绑定控件(ComboBoxEdit com_绑定控件, C_数据源 c_数据源)
        {
            com_绑定控件.Properties.Items.Clear();
            if (null != c_数据源?.list)
            {
                foreach (string str in c_数据源.list)
                {
                    com_绑定控件.Properties.Items.Add(str);
                }
            }
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
        #endregion

        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name=\"guid\"></param>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
        public static string MD5_16D(string ConvertString) //16位大写
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        public static string MD5_16X(string ConvertString) //16位小写
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }
        public string MD5_32D(string ConvertString) //32位大写
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2;
        }
        public string MD5_32X(string ConvertString) //32位小写
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2.ToLower();
        }

        public static DataTable M_加载共享数据集(string str_表名)
        {
            inParam.P_模块名 = E_模块名称.通用业务;
            inParam.P_功能名 = "M_加载共享数据集";
            inParam.P1 = str_表名;
            inParam.P2 = string.IsNullOrEmpty(C_实体信息.C_共享变量.当前系统ID) ? C_实体信息.C_共享变量.系统ID : C_实体信息.C_共享变量.当前系统ID;
            outParam = C_Server.Call(inParam);
            if (outParam.P_结果 == 1)
            {
                return outParam.P_数据集;
            }
            else
            {
                return null;
            }
        }
    }
}
