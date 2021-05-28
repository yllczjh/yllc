using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Tool.Model;
using Tool.Help;
using Tool.DB;

namespace WebAPI.Tool
{
    public class ToolFunction
    {
        #region 数据处理相关
        public static SqlParameter[] GetParameter(string str_sql, Dictionary<string, object> p,Dictionary<string, object> p1,ref MessageModel msg)
        {
            try
            {
                p = new Dictionary<string, object>(p, StringComparer.CurrentCultureIgnoreCase);//参数不区分大小写

                MatchCollection mats = Regex.Matches(str_sql, @"(?<p>\?\w+)");
                List<string> list_SQL参数 = new List<string>();
                foreach (Match mat in mats)
                {
                    if (!list_SQL参数.Contains(mat.Value))
                    {
                        list_SQL参数.Add(mat.Value);
                    }
                }
                if (list_SQL参数.Count > 0)
                {
                    SqlParameter[] parameters = new SqlParameter[list_SQL参数.Count];
                    for (int i = 0; i < list_SQL参数.Count; i++)
                    {
                        string str_参数名 = list_SQL参数[i].ToString();
                        str_参数名 = str_参数名.Replace("?", "");
                        object str_参数值 = DBNull.Value;
                        if (null == p || !p.ContainsKey(str_参数名))
                        {
                            if (null != p1)
                            {
                                p1 = new Dictionary<string, object>(p1, StringComparer.CurrentCultureIgnoreCase);
                                if (!p1.ContainsKey(str_参数名))
                                {
                                    Code.Result(ref msg, 编码.参数错误, "SQL中参数[" + str_参数名 + "]不存在!");
                                    return null;
                                }
                                else
                                {
                                    str_参数值 = p1[str_参数名];
                                }
                            }
                            else
                            {
                                Code.Result(ref msg, 编码.参数错误, "SQL中参数[" + str_参数名 + "]不存在!");
                                return null;
                            }
                        }
                        else
                        {
                            str_参数值 = p[str_参数名];
                        }
                        parameters[i] = new SqlParameter(str_参数名.Replace("?", "@"), (null == str_参数值) ? DBNull.Value : str_参数值);
                    }
                    return parameters;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Code.Result(ref msg, 编码.程序错误, e.Message);
                return null;
            }
        }

        public static SqlParameter[] GetParameter(string str_sql, Dictionary<string, object> p, DataRow row, string str_数据集名称, ref MessageModel msg)
        {
            try
            {
                p = new Dictionary<string, object>(p, StringComparer.CurrentCultureIgnoreCase);//参数不区分大小写

                MatchCollection mats = Regex.Matches(str_sql, @"(?<p>\?\w+)");
                List<string> list_SQL参数 = new List<string>();
                foreach (Match mat in mats)
                {
                    list_SQL参数.Add(mat.Value);
                }
                if (list_SQL参数.Count > 0)
                {
                    SqlParameter[] parameters = new SqlParameter[list_SQL参数.Count];
                    for (int i = 0; i < list_SQL参数.Count; i++)
                    {
                        string str_参数名 = list_SQL参数[i].ToString();
                        str_参数名 = str_参数名.Replace("?", "");
                        object str_参数值 = DBNull.Value;
                        if (str_参数名 == "dataname" && !string.IsNullOrEmpty(str_数据集名称))
                        {
                            str_参数值 = str_数据集名称;
                        }
                        else
                        {
                            if (row.Table.Columns.Contains(str_参数名))
                            {
                                str_参数值 = row[str_参数名];
                            }
                            else
                            {
                                if (p.ContainsKey(str_参数名))
                                {
                                    str_参数值 = p[str_参数名];
                                }
                                else
                                {
                                    Code.Result(ref msg, 编码.参数错误, "SQL中参数[" + str_参数名 + "]不存在!");
                                    return null;
                                }
                            }
                        }
                        parameters[i] = new SqlParameter(str_参数名.Replace("?", "@"), (null == str_参数值) ? DBNull.Value : str_参数值);
                    }
                    return parameters;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Code.Result(ref msg, 编码.程序错误, e.Message);
                return null;
            }
        }

        /// <summary>
        /// 生成插入信息字典
        /// </summary>
        /// <param name="param">入参字典</param>
        /// <param name="row">接口列表信息</param>
        /// <param name="str_error">错误信息</param>
        /// <returns></returns>
        public static Dictionary<string, object> getInsert(Dictionary<string, object> param, DataRow row, ref MessageModel msg)
        {
            Dictionary<string, object> out_dic = new Dictionary<string, object>();//返回主Dictionary
            try
            {
                string str_完成语言 = row["完成语言"].ToString();
                string str_主插入语言 = row["主插入语言"].ToString();
                string str_明细插入语言 = row["明细插入语言"].ToString();
                string str_主更新语言 = row["主更新语言"].ToString();
                if (param.ContainsKey("dataset"))
                {
                    out_dic.Add("finishsql", str_完成语言.Replace("?", "@"));
                    out_dic.Add("updatesql", str_主更新语言.Replace("?", "@"));
                    out_dic.Add("datasql", str_主插入语言.Replace("?", "@"));
                    out_dic.Add("rowsql", str_明细插入语言.Replace("?", "@"));
                    if (param.ContainsKey("datacount"))
                    {
                        out_dic.Add("datacount", param["datacount"]);
                    }
                    else
                    {
                        Code.Result(ref msg, 编码.参数错误, "入参缺少datacount节点");
                        return null;
                    }
                    ArrayList in_arr_dataset = param["dataset"] as ArrayList;
                    ArrayList out_listset = new ArrayList();
                    for (int i = 0; i < in_arr_dataset.Count; i++)
                    {
                        Dictionary<string, object> out_dic_set = new Dictionary<string, object>();
                        Dictionary<string, object> in_dic_dataset = in_arr_dataset[i] as Dictionary<string, object>;
                        out_dic_set.Add("dataparam", GetParameter(str_主插入语言, in_dic_dataset, null, ref msg));
                        if (msg.errcode != 0)
                        {
                            return null;
                        }
                        out_dic_set.Add("updateparam", GetParameter(str_主更新语言, in_dic_dataset, null, ref msg));
                        if (msg.errcode != 0)
                        {
                            return null;
                        }

                        if (in_dic_dataset.ContainsKey("datadetail"))
                        {
                            if (in_dic_dataset.ContainsKey("rowcount"))
                            {
                                out_dic_set.Add("rowcount", in_dic_dataset["rowcount"]);
                            }
                            else
                            {
                                Code.Result(ref msg, 编码.参数错误, "入参缺少rowcount节点");
                                return null;
                            }
                            if (!string.IsNullOrEmpty(str_明细插入语言))
                            {
                                ArrayList in_arr_datadetail = in_dic_dataset["datadetail"] as ArrayList;
                                ArrayList out_listdetail = new ArrayList();
                                for (int j = 0; j < in_arr_datadetail.Count; j++)
                                {
                                    Dictionary<string, object> in_dic_datadetail = in_arr_datadetail[j] as Dictionary<string, object>;
                                    out_listdetail.Add(GetParameter(str_明细插入语言, in_dic_datadetail, in_dic_dataset, ref msg));
                                    if (msg.errcode != 0)
                                    {
                                        return null;
                                    }
                                }
                                out_dic_set.Add("rowparam", out_listdetail);
                            }
                            else
                            {
                                Code.Result(ref msg, 编码.参数错误, "未设置明细插入语言");
                                return null;
                            }
                        }
                        out_listset.Add(out_dic_set);
                    }
                    out_dic.Add("dataparam", out_listset);

                    SqlParameter[] parameters_主 = ToolFunction.GetParameter(str_完成语言, param, null, ref msg);
                    if (msg.errcode != 0)
                    {
                        return null;
                    }
                    out_dic.Add("finishparam", parameters_主);
                }
                else
                {
                    Code.Result(ref msg, 编码.参数错误, "插入语言需在参数中配置[dataset]结点信息");
                    return null;
                }
            }
            catch (Exception e)
            {
                Code.Result(ref msg, 编码.程序错误, e.Message);
                return null;
            }

            return out_dic;
            //finishsql:"完成语言"
            //datasql:"主插入语言"
            //rowsql:"明细插入语言"
            //datacount:"主记录条数"
            //dataparam:list
            //    dataparam:"主记录参数"
            //    rowcount:"明细记录条数"
            //    rowparam:list
        }

        #endregion

        #region 数据转换工具


        /// <summary>
        /// 替换表头及顺序
        /// </summary>
        /// <param name="dt">要处理的原数据</param>
        /// <param name="str_序号">接口列表序号</param>
        /// <param name="type">0主记录  1明细记录</param>
        /// <param name="str_数据集名称">当返回多个数据集时，query节点下的name节点</param>
        public static DataTable M_替换表头及顺序(DataTable dt_原数据集, string str_序号, int type, string str_数据集名称)
        {
            string sql = $@"SELECT * from webapi_contrast where 业务序号='{str_序号}' and 有效状态='True'";
            if (!string.IsNullOrEmpty(str_数据集名称))
            {
                sql += $@" and 数据集名称='{str_数据集名称}'";
            }
            DataTable dt_对照表 = DbHelper.Db().GetDataTable(sql);
            if (null != dt_对照表 && dt_对照表.Rows.Count > 0)
            {
                if (type == 0)
                {
                    dt_对照表.DefaultView.RowFilter = "上级序号='0' and 分级节点='False'";
                    dt_对照表.DefaultView.Sort = "排序";
                    dt_对照表 = dt_对照表.DefaultView.ToTable();
                }
                else
                {
                    string str_上级节点 = dt_对照表.Compute("max(序号)", "分级节点='True'").ToString();
                    if (!string.IsNullOrEmpty(str_上级节点))
                    {
                        dt_对照表.DefaultView.RowFilter = "上级序号='" + str_上级节点 + "'";
                        dt_对照表.DefaultView.Sort = "排序";
                        dt_对照表 = dt_对照表.DefaultView.ToTable();
                    }
                    else
                    {
                        dt_对照表 = null;
                    }
                }
                if (null != dt_对照表 && dt_对照表.Rows.Count > 0)
                {
                    #region 修改顺序
                    List<string> l_新表头顺序 = new List<string>();
                    foreach (DataRow dr in dt_对照表.Rows)
                    {
                        if (dt_原数据集.Columns.Contains(dr["本地列名"].ToString()))
                        {
                            l_新表头顺序.Add(dr["本地列名"].ToString());
                        }
                    }
                    DataTable dt_新数据集 = dt_原数据集;
                    if (l_新表头顺序.Count > 0)
                    {
                        dt_新数据集 = dt_原数据集.DefaultView.ToTable(false, l_新表头顺序.ToArray());
                    }
                    #endregion

                    #region 修改列名
                    foreach (DataColumn col in dt_新数据集.Columns)
                    {
                        var 新表头 = (from a in dt_对照表.AsEnumerable()
                                   where a.Field<string>("本地列名") == col.ColumnName
                                   select a.Field<string>("返回列名")).FirstOrDefault() ?? "";
                        if (!string.IsNullOrEmpty(新表头.ToString()))
                        {
                            col.ColumnName = 新表头.ToString();
                        }
                    }
                    #endregion
                    return dt_新数据集;
                }
            }
            return dt_原数据集;
        }

        #endregion
    }
}