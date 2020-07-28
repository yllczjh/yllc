/**********************************************************
* 模块名称：检验接口访问数据库
* 当前版本：1.0
* 开发人员：李其亮
* 开发时间：2013/7/1 雨
* 版本历史：无
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

namespace HisDBLayer
{
   
    /// <summary>
    /// sql数据库连接配置信息
    /// </summary>
    public static class DbConfigToSql
    {
        //这里先写死
        private readonly static string _ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
        private readonly static string _ProviderName = ConfigurationManager.ConnectionStrings["SQLConnString"].ProviderName;
       /// <summary>
        /// sql连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get { return _ConnectionString; }
        }

        /// <summary>
        /// sql数据提供程序的名称
        /// </summary>
        public static string ProviderName
        {
            get { return _ProviderName; }
        }
    }

    /// <summary>
    /// Sql Server 第三方检验数据库操作助手
    /// </summary>
    public sealed class BaseEntityToSQL
    {   //日志
        log4net.ILog log = log4net.LogManager.GetLogger("log4net");
        /// <summary>
        /// sql连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// sql数据提供程序的名称
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// sql数据提供程序的对象
        /// </summary>
        public static DbProviderFactory Factory = DbProviderFactories.GetFactory(DbConfigToSql.ProviderName);

        /// <summary>
        /// sql是否开启事务
        /// </summary>
        public bool IsBeginTransaction { get; set; }

        /// <summary>
        /// sql事务对象
        /// </summary>
        public DbTransaction Transaction { get; set; }

        /// <summary>
        /// sql数据库连接对象
        /// </summary>
        public DbConnection Con { get; set; }

        /// <summary>
        /// sql命令对象
        /// </summary>
        public DbCommand Cmd { get; set; }

        /// <summary>
        /// sql调用服务的客户端信息
        /// </summary>
        ClientInforMation clientInfo;
        /// <summary>
        /// sql默认构造
        /// </summary>
        private BaseEntityToSQL()
        {
            ConnectionString = DbConfigToSql.ConnectionString;
            ProviderName = DbConfigToSql.ProviderName;
            CreateFactory();
        }
        /// <summary>
        /// 需要记录客户端信息
        /// </summary>
        /// <param name="info"></param>
        public BaseEntityToSQL(ClientInforMation info)
            : this()
        {
            this.clientInfo = info;
        }

        private static BaseEntityToSQL _db = new BaseEntityToSQL();
        /// <summary>
        /// 默认构造创建BaseEntityer实例
        /// </summary>
        public static BaseEntityToSQL Db
        {
            get
            {
                return new BaseEntityToSQL();
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
                BaseEntityToSQL db = new BaseEntityToSQL();
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
                            WriteLogToDb(this.clientInfo, sql, ex.Message);
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

                        log.Error(ex.Message);
                        WriteLogToDb(this.clientInfo, sql, ex.Message);
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
    }
}
