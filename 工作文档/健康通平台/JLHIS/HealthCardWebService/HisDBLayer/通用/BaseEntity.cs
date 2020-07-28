/**********************************************************
* 模块名称：实体常用简单数据操作辅助扩展基类
* 当前版本：1.0
* 开发人员：楚涛
* 开发时间：2012/7/18
* 版本历史：此代码由 VB/C#.Net实体代码生成工具(EntitysCodeGenerate 4.4) 自动生成。
*
***********************************************************/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.Common;
using System.Configuration;
using HisCommon.DataEntity;
using System.Data.OracleClient;

namespace HisDBLayer
{
    /// <summary>
    /// 数据库名称枚举
    /// </summary>
    public enum DbProviderName
    {
        /// <summary>
        /// sqlserver(System.Data.SqlClient)
        /// </summary>
        SqlClient,

        /// <summary>
        /// access(System.Data.OleDb)
        /// </summary>
        OleDb,

        /// <summary>
        /// mysql(MySql.Data.MySqlClient)
        /// </summary>
        MySqlClient,

        /// <summary>
        /// Oracle(System.Data.OracleClient)
        /// </summary>
        OracleClient,

        /// <summary>
        /// Oracle(Oracle.DataAccess.Client)
        /// </summary>
        Oracle
    }

    /// <summary>
    /// 数据库连接配置信息
    /// </summary>
    public static class DbConfig
    {
        //这里先写死
        //private readonly static string _ConnectionString = ConfigurationManager.ConnectionStrings["OraConnString2"].ConnectionString;
        //private readonly static string _ProviderName = ConfigurationManager.ConnectionStrings["OraConnString2"].ProviderName;
        private readonly static string _ConnectionString = " Data Source=192.168.0.9/JHB;User ID=fshis;Password=fshis;";
        private readonly static string _ProviderName = "System.Data.OracleClient";
        

        // <add name="OraConnString2" connectionString="Data Source=hisch ;Persist Security Info=True;User ID=bpms;Password=rkbpms;Unicode=True; " providerName= />
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get { return _ConnectionString; }
        }

        /// <summary>
        /// 数据提供程序的名称
        /// </summary>
        public static string ProviderName
        {
            get { return _ProviderName; }
        }
    }

    /// <summary>
    /// 数据库操作助手
    /// </summary>
    public sealed class BaseEntityer
    {   //日志
        log4net.ILog log = log4net.LogManager.GetLogger("log4net");
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据提供程序的名称
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 数据提供程序的对象
        /// </summary>
        public static DbProviderFactory Factory = DbProviderFactories.GetFactory(DbConfig.ProviderName);

        /// <summary>
        /// 是否开启事务
        /// </summary>
        public bool IsBeginTransaction { get; set; }

        /// <summary>
        /// 事务对象
        /// </summary>
        public DbTransaction Transaction { get; set; }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public DbConnection Con { get; set; }

        /// <summary>
        /// 命令对象
        /// </summary>
        public DbCommand Cmd { get; set; }

        /// <summary>
        /// 数据源读取
        /// </summary>
        public DbDataReader Reader { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Err { get; set; }

        /// <summary>
        /// 调用服务的客户端信息
        /// </summary>
        ClientInforMation clientInfo;

        /// <summary>
        /// 默认构造
        /// </summary>
        private BaseEntityer()
        {
            ConnectionString = DbConfig.ConnectionString;
            ProviderName = DbConfig.ProviderName;
            CreateFactory();
        }

        /// <summary>
        /// 需要记录客户端信息
        /// </summary>
        /// <param name="info"></param>
        public BaseEntityer(ClientInforMation info): this()
        {
            this.clientInfo = info;
        }

        private static BaseEntityer _db = new BaseEntityer();

        /// <summary>
        /// 默认构造创建BaseEntityer实例
        /// </summary>
        public static BaseEntityer Db
        {
            get
            {
                return new BaseEntityer();
            }
        }

        // 只适合单线程调用
        public static BaseEntityer DbBase
        {
            get
            {
                return _db;
            }
        }
        /// <summary>
        /// 创建数据提供程序的实例
        /// </summary>
        private void CreateFactory()
        {
            //if(Factory==null)Factory = DbProviderFactories.GetFactory(ProviderName);
        }

        /// <summary>
        /// 开始事务,配合CommitTransaction方法使用
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                Con = Factory.CreateConnection();
                Con.ConnectionString = ConnectionString;
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                Transaction = Con.BeginTransaction();
                Cmd = Con.CreateCommand();
                Cmd.Transaction = Transaction;
                IsBeginTransaction = true;
            }
            catch
            {
                IsBeginTransaction = false;
                if (Con.State == ConnectionState.Open)
                {
                    Cmd.Dispose();
                    Transaction.Dispose();
                    Con.Close();
                }
                throw;
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (IsBeginTransaction && Transaction != null)
                {
                    Transaction.Commit();
                }
            }
            catch
            {
                try
                {
                    Transaction.Rollback();
                    throw;
                }
                catch
                {
                    throw;
                }
            }
            finally
            {
                IsBeginTransaction = false;
                if (Con.State == ConnectionState.Open)
                {
                    Cmd.Dispose();
                    Transaction.Dispose();
                    Con.Close();
                }
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            if (IsBeginTransaction)
            {
                Transaction.Rollback();
            }


            IsBeginTransaction = false;
            if (Con.State == ConnectionState.Open)
            {
                Cmd.Dispose();
                Transaction.Dispose();
                Con.Close();
            }
        }

        /// <summary>
        /// 清除参数
        /// </summary>
        public void ClearParameters()
        {
            Cmd.Parameters.Clear();
        }

        /// <summary>
        /// 记录日志到数据库中
        /// </summary>
        public static void WriteLogToDb(HisCommon.DataEntity.ClientInforMation info, string sqltext, string errortext)
        {
            try
            {
                if (info == null)
                    return;
                string sql = @"   insert into sys_log t 
   values('{0}','{1}','{2}',to_date('{3}','yyyy-MM-dd hh24:mi:ss'))";
                sqltext = sqltext.Replace("'","''");
                sql = string.Format(sql,info.IP, sqltext, errortext, DateTime.Now.ToString());
                BaseEntityer db = new BaseEntityer();
                db.ExecuteNonQuery(sql);
            }
            catch
            { }
        }

        /// <summary>
        /// 获得数据提供程序的名称
        /// </summary>
        /// <param name="provider">数据提名称枚举</param>
        /// <returns></returns>
        private string GetProviderName(DbProviderName provider)
        {
            switch (provider)
            {
                case DbProviderName.SqlClient:
                    return "System.Data.SqlClient";
                case DbProviderName.OleDb:
                    return "System.Data.OleDb";
                case DbProviderName.MySqlClient:
                    return "MySql.Data.MySqlClient";
                case DbProviderName.OracleClient:
                    return "System.Data.OracleClient";
                case DbProviderName.Oracle:
                    return "Oracle.DataAccess.Client";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 执行非查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        public int ExecuteNonQuery(string sql)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType.Text;
                    Cmd.CommandText = sql;
                    log.Info(Cmd.CommandText);
                    return Cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    try
                    {
                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, sql, ex.Message);
                        RollbackTransaction();
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType.Text;
                            Cmd.CommandText = sql;

                            log.Info(Cmd.CommandText);
                            Con.Open();
                            return Cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            Err = ex.Message;
                            WriteLogToDb(this.clientInfo, sql, ex.Message);
                            //return -1;
                            //Edit By ZhanGD 2014-05-23 改为 return -1 不抛出异常 返回-1 方便处理
                            throw;
                        }
                    }
                }
            }
        }
       
        /// <summary>
        /// 执行非查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="CommandType">执行类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, CommandType CommandType, params DbParameter[] param)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType;
                    Cmd.CommandText = sql;
                    Cmd.Parameters.AddRange(param);

                    log.Info(Cmd.CommandText);
                    return Cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    try
                    {
                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, sql, ex.Message);
                        RollbackTransaction();
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType;
                            Cmd.CommandText = sql;
                            Cmd.Parameters.AddRange(param);

                            log.Info(Cmd.CommandText);
                            Con.Open();
                            return Cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            WriteLogToDb(this.clientInfo, sql, ex.Message);
                            throw ex;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行非查询(SGL)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="CommandType">执行类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, CommandType CommandType, Dictionary<string, object> param)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType;
                    Cmd.CommandText = sql;
                    CreateAndSetCommandParameters(param);

                    log.Info(Cmd.CommandText);
                    // Cmd.Parameters.AddRange(param);
                    return Cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();

                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, sql, ex.Message);
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }

            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType;
                            Cmd.CommandText = sql;
                            CreateAndSetCommandParameters(param);

                            log.Info(Cmd.CommandText);
                            Con.Open();
                            return Cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            WriteLogToDb(this.clientInfo, sql, ex.Message);
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 组合参数(SGL)
        /// </summary>
        /// <param name="paraList"></param>
        private void CreateAndSetCommandParameters(IDictionary<string, object> paraList)
        {
            Cmd.Parameters.Clear();
            if (paraList == null) return;
            foreach (string key in paraList.Keys)
            {
                object value = paraList[key];
                if (value is string && string.IsNullOrEmpty(value as string))
                {
                    value = DBNull.Value;
                }
                if (value == null)
                {
                    value = DBNull.Value;
                }
                DbParameter p = Cmd.CreateParameter();
                p.ParameterName = key;
                p.Value = value;
                Cmd.Parameters.Add(p);
            }
        }

        /// <summary>
        /// 执行非查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, params DbParameter[] param)
        {
            return ExecuteNonQuery(sql, CommandType.Text, param);
        }

        /// <summary>
        /// 获得DataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="CommandType">执行类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string sql, CommandType CommandType, params DbParameter[] param)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType;
                    Cmd.CommandText = sql;
                    Cmd.Parameters.AddRange(param);

                    log.Info(Cmd.CommandText);
                    return Cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();
                        Err = ex.Message;
                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, sql, ex.Message);
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }

            }
            else
            {
                Con = Factory.CreateConnection();
                using (Cmd = Con.CreateCommand())
                {
                    try
                    {
                        Con.ConnectionString = ConnectionString;
                        Cmd.CommandType = CommandType;
                        Cmd.CommandText = sql;
                        Cmd.Parameters.AddRange(param);

                        log.Info(Cmd.CommandText);
                        Con.Open();
                        return Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (Exception ex)
                    {
                        Con.Close();
                        Err = ex.Message;
                        log.Error(ex.Message);
                        //return null;
                        //Edit By ZhanGD 2014-05-23 改为 return null 不抛出异常 返回-1 方便处理
                        throw;
                    }
                }

            }
        }

        /// <summary>
        /// 获得DataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string sql, params DbParameter[] param)
        {
            return ExecuteReader(sql, CommandType.Text, param);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExecQuery(string sql, params DbParameter[] param)
        {
            Reader = ExecuteReader(sql, param);
            if (Reader == null)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获得第一行第一列结果
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="CommandType">执行类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, CommandType CommandType, params DbParameter[] param)
        {
            if (IsBeginTransaction)
            {
                try
                {

                    Cmd.CommandType = CommandType;
                    Cmd.CommandText = sql;
                    Cmd.Parameters.AddRange(param);

                    log.Info(Cmd.CommandText);
                    return Cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();

                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, sql, ex.Message);
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }

            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType;
                            Cmd.CommandText = sql;
                            Cmd.Parameters.AddRange(param);

                            log.Info(Cmd.CommandText);
                            Con.Open();
                            return Cmd.ExecuteScalar();
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            WriteLogToDb(this.clientInfo, sql, ex.Message);
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获得第一行第一列结果
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, params DbParameter[] param)
        {
            return ExecuteScalar(sql, CommandType.Text, param);
        }

        /// <summary>
        /// 获得第一行第一列结果，采用泛型作为返回值类型
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="CommandType">执行类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, CommandType CommandType, params DbParameter[] param)
        {
            return (T)Convert.ChangeType(ExecuteScalar(sql, CommandType, param), typeof(T));
        }

        /// <summary>
        /// 获得第一行第一列结果，采用泛型作为返回值类型
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, params DbParameter[] param)
        {
            return ExecuteScalar<T>(sql, CommandType.Text, param);
        }

        /// <summary>
        /// 获得DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="CommandType">执行类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, CommandType CommandType, params DbParameter[] param)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType;
                    Cmd.CommandText = sql;
                    Cmd.Parameters.AddRange(param);

                    log.Info(Cmd.CommandText);
                    using (DbDataAdapter da = Factory.CreateDataAdapter())
                    {
                        da.SelectCommand = Cmd;
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();

                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, sql, ex.Message);
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }

            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType;
                            Cmd.CommandText = sql;
                            Cmd.Parameters.AddRange(param);

                            log.Info(Cmd.CommandText);
                            Con.Open();
                            using (DbDataAdapter da = Factory.CreateDataAdapter())
                            {
                                da.SelectCommand = Cmd;
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                return ds;
                            }
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            WriteLogToDb(this.clientInfo, sql, ex.Message);
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获得DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, params DbParameter[] param)
        {
            return GetDataSet(sql, CommandType.Text, param);
        }

        /// <summary>
        /// 获得DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="CommandType">执行类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, CommandType CommandType, params DbParameter[] param)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType;
                    Cmd.CommandText = sql;
                    Cmd.Parameters.AddRange(param);

                    log.Info(Cmd.CommandText);
                    using (DbDataAdapter da = Factory.CreateDataAdapter())
                    {
                        da.SelectCommand = Cmd;
                        DataTable dt = new DataTable();
                        dt.TableName = "table";
                        da.Fill(dt);
                        return dt;
                    }
                    //read方式
                    //using (DbDataReader dr = Cmd.ExecuteReader())
                    //{
                    //    DataTable dt = new DataTable();
                    //    dt.Load(dr);
                    //    return dt;
                    //}

                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();

                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, sql, ex.Message);
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }

            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType;
                            Cmd.CommandText = sql;
                            Cmd.Parameters.AddRange(param);

                            log.Info(Cmd.CommandText);
                            Con.Open();
                            using (DbDataAdapter da = Factory.CreateDataAdapter())
                            {
                                da.SelectCommand = Cmd;
                                DataTable dt = new DataTable();
                                dt.TableName = "table";
                                da.Fill(dt);
                                return dt;
                            }
                            //read方式
                            //using (DbDataReader dr = Cmd.ExecuteReader())
                            //{
                            //    DataTable dt = new DataTable();
                            //    dt.Load(dr);
                            //    return dt;
                            //}
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            WriteLogToDb(this.clientInfo, sql, ex.Message);
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获得DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, params DbParameter[] param)
        {
            return GetDataTable(sql, CommandType.Text, param);
        }

        /// <summary>
        /// 获得DataRow
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="index">行号从0开始</param>
        /// <param name="CommandType">执行类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DataRow GetDataRow(string sql, int index, CommandType CommandType, params DbParameter[] param)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType;
                    Cmd.CommandText = sql;
                    Cmd.Parameters.AddRange(param);

                    log.Info(Cmd.CommandText);
                    using (DbDataAdapter da = Factory.CreateDataAdapter())
                    {
                        da.SelectCommand = Cmd;
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return dt.Rows[index];
                        }
                        return null;
                    }
                    //read方式
                    //using (DbDataReader dr = Cmd.ExecuteReader())
                    //{
                    //    DataTable dt = new DataTable();
                    //    dt.Load(dr);
                    //    return dt;
                    //}
                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();

                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, sql, ex.Message);
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }

            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType;
                            Cmd.CommandText = sql;
                            Cmd.Parameters.AddRange(param);

                            log.Info(Cmd.CommandText);
                            Con.Open();
                            using (DbDataAdapter da = Factory.CreateDataAdapter())
                            {
                                da.SelectCommand = Cmd;
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    return dt.Rows[index];
                                }
                                return null;
                            }
                            //read方式
                            //using (DbDataReader dr = Cmd.ExecuteReader())
                            //{
                            //    DataTable dt = new DataTable();
                            //    dt.Load(dr);
                            //    return dt;
                            //}
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            WriteLogToDb(this.clientInfo, sql, ex.Message);
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获得第一行DataRow
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DataRow GetDataRow(string sql, params DbParameter[] param)
        {
            return GetDataRow(sql, 0, CommandType.Text, param);
        }

        /// <summary>
        /// 获得DataRow
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DataRow GetDataRow(string sql, int index, params DbParameter[] param)
        {
            return GetDataRow(sql, index, CommandType.Text, param);
        }

        /// <summary>
        /// 执行储存过程,并返回参数的集合
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DbParameterCollection ExecuteProc(string procName, params DbParameter[] param)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.CommandText = procName;
                    Cmd.Parameters.AddRange(param);

                    log.Info(Cmd.CommandText);
                    Cmd.ExecuteNonQuery();
                    return Cmd.Parameters;
                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();

                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, Cmd.CommandText, ex.Message);
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }

            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType.StoredProcedure;
                            Cmd.CommandText = procName;
                            Cmd.Parameters.AddRange(param);

                            log.Info(Cmd.CommandText);
                            Con.Open();
                            Cmd.ExecuteNonQuery();
                            return Cmd.Parameters;
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            WriteLogToDb(this.clientInfo, Cmd.CommandText, ex.Message);
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行储存过程,并返回表集合
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DataSet ExecuteProcDataSet(string procName, params DbParameter[] param)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.CommandText = procName;
                    Cmd.Parameters.AddRange(param);

                    log.Info(Cmd.CommandText);
                    //Cmd.ExecuteNonQuery();
                    //return Cmd.Parameters;
                    using (DbDataAdapter da = Factory.CreateDataAdapter())
                    {
                        da.SelectCommand = Cmd;
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();

                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, Cmd.CommandText, ex.Message);
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }

            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType.StoredProcedure;
                            Cmd.CommandText = procName;
                            Cmd.Parameters.AddRange(param);

                            log.Info(Cmd.CommandText);
                            //Con.Open();
                            //Cmd.ExecuteNonQuery();
                            //return Cmd.Parameters;
                            using (DbDataAdapter da = Factory.CreateDataAdapter())
                            {
                                da.SelectCommand = Cmd;
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                return ds;
                            }
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            WriteLogToDb(this.clientInfo, Cmd.CommandText, ex.Message);
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行储存过程,并返回表集合和返回参数集合
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DataSet ExecuteProcDataSet(string procName, out DbParameterCollection dbpc, params DbParameter[] param)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.CommandText = procName;
                    Cmd.Parameters.AddRange(param);

                    log.Info(Cmd.CommandText);
                    //Cmd.ExecuteNonQuery();
                    //return Cmd.Parameters;
                    using (DbDataAdapter da = Factory.CreateDataAdapter())
                    {
                        da.SelectCommand = Cmd;
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dbpc = da.SelectCommand.Parameters;
                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();

                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, Cmd.CommandText, ex.Message);
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }

            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType.StoredProcedure;
                            Cmd.CommandText = procName;
                            Cmd.Parameters.AddRange(param);

                            log.Info(Cmd.CommandText);
                            //Con.Open();
                            //Cmd.ExecuteNonQuery();
                            //return Cmd.Parameters;
                            using (DbDataAdapter da = Factory.CreateDataAdapter())
                            {
                                da.SelectCommand = Cmd;
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                dbpc = da.SelectCommand.Parameters;
                                return ds;
                            }
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            WriteLogToDb(this.clientInfo, Cmd.CommandText, ex.Message);
                            throw;
                        }
                    }
                }
            }
        }

        #region 处理过数据库操作异常的函数 
        //Add By ZhanGD 2014-05-27

        /// <summary>
        /// 执行查询
        /// 捕捉到异常，返回null
        /// Add By ZhanGD 2014-05-27
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public DbDataReader ZDExecReader(string strSQL)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType.Text;
                    Cmd.CommandText = strSQL;

                    log.Info(Cmd.CommandText);
                    return Cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    try
                    {
                        RollbackTransaction();
                        Err = ex.Message;
                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, strSQL, ex.Message);
                        return null;
                    }
                    catch
                    {
                        return null;
                    }
                }

            }
            else
            {
                Con = Factory.CreateConnection();
                using (Cmd = Con.CreateCommand())
                {
                    try
                    {
                        Con.ConnectionString = ConnectionString;
                        Cmd.CommandType = CommandType.Text;
                        Cmd.CommandText = strSQL;

                        log.Info(Cmd.CommandText);
                        Con.Open();
                        return Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (Exception ex)
                    {
                        Con.Close();
                        Err = ex.Message;
                        log.Error(ex.Message);
                        return null;
                    }
                }

            }
        }

        /// <summary>
        /// 执行非查询
        /// 捕捉到异常，返回-1
        /// Add By ZhanGD 2014-05-27
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public int ZDExecNonQuery(string strSQL)
        {
            if (IsBeginTransaction)
            {
                try
                {
                    Cmd.CommandType = CommandType.Text;
                    Cmd.CommandText = strSQL;
                    log.Info(Cmd.CommandText);
                    return Cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    try
                    {
                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, strSQL, ex.Message);
                        RollbackTransaction();
                        return -1;
                    }
                    catch
                    {
                        return -1;
                    }
                }

            }
            else
            {
                using (Con = Factory.CreateConnection())
                {
                    using (Cmd = Con.CreateCommand())
                    {
                        try
                        {
                            Con.ConnectionString = ConnectionString;
                            Cmd.CommandType = CommandType.Text;
                            Cmd.CommandText = strSQL;

                            log.Info(Cmd.CommandText);
                            Con.Open();
                            return Cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Con.Close();

                            log.Error(ex.Message);
                            Err = ex.Message;
                            WriteLogToDb(this.clientInfo, strSQL, ex.Message);
                            return -1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询
        /// 捕捉到异常，返回null
        /// Add By ZhanGD 2014-05-27
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public DbDataReader ZDExecReader(string conString, string strSQL)
        {
            string proName = ConfigurationManager.ConnectionStrings[conString].ProviderName;

            //创建一个数据库对应的实例，使用该实例就可以创建对应的connection,command 和adapater等等对象
            DbProviderFactory provider = DbProviderFactories.GetFactory(proName);

            //创建具体的数据库连接类型和命令执行类型
            DbConnection oraCon = provider.CreateConnection();
            oraCon.ConnectionString = ConfigurationManager.ConnectionStrings[conString].ConnectionString;
            DbCommand oraCom = provider.CreateCommand();
            oraCom.Connection = oraCon;

            WriteLog log = new WriteLog();

            try
            {
                oraCom.CommandType = CommandType.Text;
                oraCom.CommandText = strSQL;
                log.WriteLogs(oraCom.CommandText);

                oraCon.Open();
                DbDataReader reader = oraCom.ExecuteReader(CommandBehavior.CloseConnection);
                oraCon.Close();
                return reader;
            }
            catch (Exception ex)
            {
                oraCon.Close();
                Err = ex.Message;
                log.WriteLogs(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.Source);
                return null;
            }
        }

        /// <summary>
        /// 执行非查询
        /// 捕捉到异常，返回-1
        /// Add By ZhanGD 2014-05-27
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public int ZDExecNonQuery(string conString, string strSQL)
        {
            string proName = ConfigurationManager.ConnectionStrings[conString].ProviderName;

            //创建一个数据库对应的实例，使用该实例就可以创建对应的connection,command 和adapater等等对象
            DbProviderFactory provider = DbProviderFactories.GetFactory(proName);

            //创建具体的数据库连接类型和命令执行类型
            DbConnection oraCon = provider.CreateConnection();
            oraCon.ConnectionString = ConfigurationManager.ConnectionStrings[conString].ConnectionString;
            DbCommand oraCom = provider.CreateCommand();
            oraCom.Connection = oraCon;

            WriteLog log = new WriteLog();

            try
            {
                oraCom.CommandType = CommandType.Text;
                oraCom.CommandText = strSQL;
                log.WriteLogs(oraCom.CommandText);

                oraCon.Open();
                int revInt = oraCom.ExecuteNonQuery();
                oraCon.Close();
                return revInt;
            }
            catch (Exception ex)
            {
                oraCon.Close();
                Err = ex.Message;
                log.WriteLogs(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.Source);
                return -1;
            }
        }

        /// <summary>
        /// 执行查询sql 语句
        /// 捕捉到异常，返回空的dataTable表
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public DataTable ZDGetDataTable(string conString, string strSQL)
        {
            string proName = ConfigurationManager.ConnectionStrings[conString].ProviderName;

            //创建一个数据库对应的实例，使用该实例就可以创建对应的connection,command 和adapater等等对象
            DbProviderFactory provider = DbProviderFactories.GetFactory(proName);

            //创建具体的数据库连接类型和命令执行类型
            DbConnection oraCon = provider.CreateConnection();
            oraCon.ConnectionString = ConfigurationManager.ConnectionStrings[conString].ConnectionString;
            DbCommand oraCom = provider.CreateCommand();
            oraCom.Connection = oraCon;

            WriteLog log = new WriteLog();

            try
            {
                oraCom.CommandType = CommandType.Text;
                oraCom.CommandText = strSQL;

                log.WriteLogs(oraCom.CommandText);
                using (DbDataAdapter da = provider.CreateDataAdapter())
                {
                    da.SelectCommand = oraCom;
                    DataTable dt = new DataTable();
                    dt.TableName = "table";
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    log.WriteLogs(ex.Message);
                    WriteLogToDb(this.clientInfo, strSQL, ex.Message);
                    throw;
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion

        #region 执行带参数的SQL语句  dlq 2013-12-11

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSql(string SQLString, params OracleParameter[] cmdParms)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.OracleClient.OracleException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的OracleParameter[]）</param>
        public  bool ExecuteSqlTran(Hashtable SQLStringList)
        {
            bool b = false;
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    OracleCommand cmd = new OracleCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            OracleParameter[] cmdParms = (OracleParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                           
                        }
                        trans.Commit();
                        b = true;
                    }
                    catch
                    {
                        trans.Rollback();                        
                    }                    
                }
            }
            return b;
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public  bool ExecuteSqlTran(ArrayList SQLStringList)
        {
            bool b = false;
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                OracleTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    b = true;
                }
                catch
                {
                    tx.Rollback();                
                }
            }
            return b;
        }

        private  void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion
    }
}
