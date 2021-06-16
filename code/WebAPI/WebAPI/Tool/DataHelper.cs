using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Tool.DB;
using Tool.Help;
using Tool.Model;
using WebAPI.Models;

namespace WebAPI.Tool
{
    public class DataHelper
    {
        /// <summary>
        /// 数据库操作,执行表中配置的查询、插入、更新的语句
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="Code">业务编码</param>
        /// <param name="p">参数</param>
        public static Dictionary<string, object> Process(JObject param, ref MessageModel msg)
        {
            try
            {
                Dictionary<string, object> dic_返回 = new Dictionary<string, object>();

                string sql = $@"SELECT t2.序号, t2.业务编号,t2.主查询语言,t2.明细查询语言,t2.主插入语言,t2.主更新语言,t2.明细插入语言,t2.明细更新语言,t2.完成语言,t3.数据库连接串,t3.数据库类型 from webapi_xttolist t1 left join webapi_list t2 on t1.业务编号=t2.业务编号 left join webapi_link t3 on t2.连接标识=t3.连接标识 where t1.系统ID='{msg.appid}' and t1.业务编号='{msg.method}' and t1.有效状态='True' and t2.有效状态='True'";
                DataTable dt = DbHelper.Db().GetDataTable(sql);
                if (null != dt && dt.Rows.Count == 1)
                {
                    string str_数据库连接串 = dt.Rows[0]["数据库连接串"].ToString();
                    string str_数据库类型 = dt.Rows[0]["数据库类型"].ToString();
                    if (null != JsonValue(param, "dbcon"))
                    {
                        sql = $@"SELECT t.数据库连接串, t.数据库类型 from webapi_link t where t.连接标识='{JsonValue(param, "dbcon")}'";
                        DataTable dt_连接 = DbHelper.Db().GetDataTable(sql);
                        if (null == dt_连接 || dt_连接.Rows.Count <= 0)
                        {
                            Code.Result(ref msg, 编码.参数错误, "[dbcon]未查到有效的数据信息");
                            return null;
                        }

                        str_数据库连接串 = dt_连接.Rows[0]["数据库连接串"].ToString();
                        str_数据库类型 = dt_连接.Rows[0]["数据库类型"].ToString();
                    }

                    #region 插入更新
                    if (!string.IsNullOrEmpty(dt.Rows[0]["主插入语言"].ToString()))
                    {
                        Dictionary<string, object> dic_插入信息 = ToolFunction.getInsert(param, dt.Rows[0], ref msg);
                        if (msg.errcode != 0)
                        {
                            return null;
                        }

                        if (dic_插入信息.Count > 0)
                        {
                            DbHelper.Db(str_数据库类型, str_数据库连接串).ExecuteBatch(dic_插入信息);
                        }
                    }
                    #endregion

                    #region 主查询语言
                    string str_sql = dt.Rows[0]["主查询语言"].ToString();
                    DataTable dt_主记录 = new DataTable();
                    if (!string.IsNullOrEmpty(str_sql))
                    {
                        JArray arr_query = new JArray();
                        int count = 1;
                        if (null != JsonValue(param, "query"))
                        {
                            arr_query = JsonValue(param, "query") as JArray;
                            count = arr_query.Count;
                        }
                        for (int i = 0; i < count; i++)
                        {
                            SqlParameter[] parameters_主 = null;
                            JObject dic_query = null;
                            string str_数据集名称 = string.Empty;
                            if (null != JsonValue(param, "query"))
                            {
                                //包含query节点则有点读取query节点下的参数
                                dic_query = arr_query[i] as JObject;
                                parameters_主 = ToolFunction.GetParameter(ref str_sql, dic_query, param, ref msg);
                                if (msg.errcode != 0)
                                {
                                    return null;
                                }
                                if (null != JsonValue(dic_query, "dataname"))
                                {
                                    str_数据集名称 = JsonValue(dic_query, "dataname").ToString();
                                }
                                else
                                {
                                    Code.Result(ref msg, 编码.参数错误, "query节点下缺少dataname节点");
                                    return null;
                                }
                            }
                            else
                            {
                                //不包含query节点则读取主节点的参数
                                parameters_主 = ToolFunction.GetParameter(ref str_sql, param, null, ref msg);
                                if (msg.errcode != 0)
                                {
                                    return null;
                                }
                            }


                            dt_主记录 = DbHelper.Db(str_数据库类型, str_数据库连接串).GetDataTable(str_sql.Replace("?", "@"), parameters_主);

                            #region 明细查询语言
                            string str_sql1 = dt.Rows[0]["明细查询语言"].ToString();
                            DataTable dt_明细记录 = null;
                            ArrayList list_返回 = new ArrayList();
                            Dictionary<string, object> dic_明细返回 = new Dictionary<string, object>();
                            if (null != dt_主记录 && dt_主记录.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(str_sql1))
                                {
                                    for (int j = 0; j < dt_主记录.Rows.Count; j++)
                                    {
                                        DataRow row = dt_主记录.Rows[j];
                                        SqlParameter[] parameters_明细 = ToolFunction.GetParameter(str_sql1, param, row, str_数据集名称, ref msg);
                                        if (msg.errcode != 0)
                                        {
                                            return null;
                                        }
                                        dt_明细记录 = DbHelper.Db(str_数据库类型, str_数据库连接串).GetDataTable(str_sql1.Replace("?", "@"), parameters_明细);
                                        row = ToolFunction.M_替换表头及顺序(row.Table, dt.Rows[0]["序号"].ToString(), 0, str_数据集名称).Rows[j];
                                        dt_明细记录 = ToolFunction.M_替换表头及顺序(dt_明细记录, dt.Rows[0]["序号"].ToString(), 1, str_数据集名称);
                                        dic_明细返回 = Helper.ToJson(row, dt_明细记录);
                                        list_返回.Add(dic_明细返回);
                                    }
                                }
                                else
                                {
                                    dt_主记录 = ToolFunction.M_替换表头及顺序(dt_主记录, dt.Rows[0]["序号"].ToString(), 0, str_数据集名称);
                                }
                            }
                            #endregion
                            if (null != JsonValue(param, "query"))
                            {
                                if (null == JsonValue(dic_query, "dataname"))
                                {
                                    Code.Result(ref msg, 编码.参数错误, "query节点下缺少dataname节点");
                                    return null;
                                }

                                if (list_返回.Count == 0)
                                {
                                    dic_返回.Add(JsonValue(dic_query, "dataname").ToString(), dt_主记录);
                                }
                                else
                                {
                                    dic_返回.Add(JsonValue(dic_query, "dataname").ToString(), list_返回);
                                }
                            }
                            else
                            {
                                if (list_返回.Count == 0)
                                {
                                    dic_返回.Add("dataset", dt_主记录);
                                }
                                else
                                {
                                    dic_返回.Add("dataset", list_返回);
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    Code.Result(ref msg, 编码.消息头错误, "业务编号[" + msg.method + "]未配置数据库信息");
                    return null;
                }
                return dic_返回;
            }
            catch (SqlException e)
            {
                try
                {
                    if (((SqlException)e).State == 255)
                    {
                        int errcode = int.Parse(e.Message.Split(')')[0].Split('(')[1]);
                        string errmsg = e.Message.Split(')')[1];
                        Code.Result(ref msg, errcode, errmsg);
                        return null;
                    }
                    else
                    {
                        Code.Result(ref msg, 编码.数据库错误, e.Message);
                        Log.Error("M_业务数据处理", e.Message);
                        return null;
                    }
                }
                catch (Exception e1)
                {
                    Code.Result(ref msg, 编码.程序错误, e1.Message);
                    Log.Error("M_业务数据处理", e.Message);
                    return null;
                }

            }
            catch (Exception e)
            {
                Code.Result(ref msg, 编码.程序错误, e.Message);
                Log.Error("M_业务数据处理", e.Message);
                return null;
            }
        }
        public static bool M_验证Code(string method, ref MessageModel msg, out int i_基础业务)
        {
            try
            {
                string sql = $@"SELECT * from webapi_list where 业务编号='{method}' and 有效状态='True'";
                DataTable dt = DbHelper.Db().GetDataTable(sql);
                if (null == dt || dt.Rows.Count <= 0)
                {
                    //此处不验证数据库中是否存在method
                    i_基础业务 = 1;
                    return true;
                }
                if (null != dt && dt.Rows.Count > 1)
                {
                    Code.Result(ref msg, 编码.消息头错误, "匹配到多个method");
                    i_基础业务 = 0;
                    return false;
                }
                i_基础业务 = (int)dt.Rows[0]["基础业务"];
            }
            catch (Exception)
            {
                i_基础业务 = 0;
                return false;
            }

            return true;
        }
        public static bool M_验证客户ID(string appid, ref MessageModel msg, out string clienttype, out string secret)
        {
            try
            {
                string sql = $@"select * from xt_xt where 系统ID='{appid}'";
                DataTable dt = DbHelper.Db().GetDataTable(sql);
                if (null == dt || dt.Rows.Count <= 0)
                {
                    Code.Result(ref msg, 编码.消息头错误, "无效的appid");
                    clienttype = string.Empty;
                    secret = string.Empty;
                    return false;
                }
                if (null != dt && dt.Rows.Count > 1)
                {
                    Code.Result(ref msg, 编码.消息头错误, "appid匹配到多个用户");
                    clienttype = string.Empty;
                    secret = string.Empty;
                    return false;
                }
                clienttype = dt.Rows[0]["客户端类别"].ToString();
                secret = dt.Rows[0]["密钥"].ToString();
            }
            catch (Exception e)
            {
                Code.Result(ref msg, 编码.程序错误, e.Message);
                Log.Error("M_验证客户ID", e.Message);
                clienttype = string.Empty;
                secret = string.Empty;
                return false;
            }
            return true;
        }
        public static int M_更新Token(TokenModel token, string appid)
        {
            string sql = $"update webapi_customer set accessToken='{token.accessToken}',accessPastTime=convert(datetime,'{token.accessPastTime}') where appid='{appid}'";
            return DbHelper.Db().ExecuteSql(sql);
        }

        public static JToken JsonValue(JObject p, string name)
        {
            if (null == p)
            {
                return null;
            }
            return p.GetValue(name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}