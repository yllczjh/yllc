using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

using System.Data.OracleClient;
//using System.Windows.Forms;

namespace 业务管理.数据库
{

    public class MsSqlHelper : IDbHelper
    {
        private string _ConnString;
        public string ConnString
        {
            get { return _ConnString; }
            set { _ConnString = value; }
        }

        public SqlDatabase _db = null;
        public SqlConnection _dbconn = null;
        public SqlTransaction _dbtran = null;

        public MsSqlHelper() { }

        public MsSqlHelper(string constring)
        {
            ConnString = constring;
        }

        /// <summary>
        /// 返回一个数据库实例
        /// </summary>
        /// <returns></returns>
        public SqlDatabase GetSqlDatabase()
        {
            // ConnString = Db_Common.GetConnStr("DB_SQL_ConStr");
            SqlDatabase db = new SqlDatabase(ConnString);
            return db;
        }
        /// <summary>
        /// 返回一个数据库实例
        /// </summary>
        /// <returns></returns>
        public SqlDatabase GetSqlDatabase(String Key)
        {
            ConnString = Db_Common.GetConnStr(Key);
            SqlDatabase db = new SqlDatabase(ConnString);

            return db;
        }
        /// <summary>
        /// 根据Sql语句或存储过程向数据集添加DataTable
        /// </summary>
        /// <param name="CType">类型</param>
        /// <param name="ds">数据集</param>
        /// <param name="command">命令字符串</param>
        /// <param name="TableName">表名</param>
        public void Load(CommandType CType, DataSet ds, string command, string TableName)
        {
            Int16 i = 0;

            SqlDatabase db = GetSqlDatabase();

            try
            {
                for (i = 0; i < ds.Tables.Count; i++)
                {
                    //如果DataTable已经存在,则清除TableName中的数据
                    if (ds.Tables[i].TableName.ToLower() == TableName.ToLower())
                    {
                        ds.Tables[i].Clear();
                    }
                }
                //执行Sql语句
                if (CType == CommandType.Text)
                {
                    SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(command);

                    db.LoadDataSet(cmd, ds, TableName);

                }
                //执行不带参数的存储过程
                if (CType == CommandType.StoredProcedure)
                {
                    SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(command);
                    db.LoadDataSet(cmd, ds, TableName);
                }

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据Sql语句或存储过程向数据集添加DataTable
        /// </summary>
        /// <param name="CType">类型</param>
        /// <param name="ds">数据集</param>
        /// <param name="command">命令字符串</param>
        /// <param name="TableName">表名</param>
        public void Load(CommandType CType, Db_Common.InParameter[] InparameterValues, DataSet ds, string command, string TableName)
        {
            Int16 i = 0;

            SqlDatabase db = GetSqlDatabase();

            try
            {
                for (i = 0; i < ds.Tables.Count; i++)
                {
                    //如果DataTable已经存在,则清除TableName中的数据
                    if (ds.Tables[i].TableName.ToLower() == TableName.ToLower())
                    {
                        ds.Tables[i].Clear();
                    }
                }

                //执行Sql语句
                if (CType == CommandType.Text)
                {
                    SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(get_Replace_SQL(command));

                    for (int u = 0; u < InparameterValues.Length; u++)
                    {
                        db.AddInParameter(cmd, get_Replace_Param(InparameterValues[u].name), InparameterValues[u].dbType, InparameterValues[u].value);
                    }


                    db.LoadDataSet(cmd, ds, TableName);

                }
                //执行不带参数的存储过程
                if (CType == CommandType.StoredProcedure)
                {
                    SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(command);


                    for (int r = 0; r < InparameterValues.Length; r++)
                    {
                        db.AddInParameter(cmd, get_Replace_Param(InparameterValues[r].name), InparameterValues[r].dbType, InparameterValues[r].value);
                    }
                    db.LoadDataSet(cmd, ds, TableName);
                }

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据Sql语句或存储过程向数据集添加DataTable
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="CType">类型</param>
        /// <param name="ds">数据集</param>
        /// <param name="command">命令字符串</param>
        /// <param name="TableName">表名</param>
        public void Load(String Key, CommandType CType, DataSet ds, string command, string TableName)
        {
            Int16 i = 0;

            SqlDatabase db = GetSqlDatabase(Key);

            try
            {
                for (i = 0; i < ds.Tables.Count; i++)
                {
                    //如果DataTable已经存在,则清除TableName中的数据
                    if (ds.Tables[i].TableName.ToLower() == TableName.ToLower())
                    {
                        ds.Tables[i].Clear();
                    }
                }
                //执行Sql语句
                if (CType == CommandType.Text)
                {
                    SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(command);

                    db.LoadDataSet(cmd, ds, TableName);

                }
                //执行不带参数的存储过程
                if (CType == CommandType.StoredProcedure)
                {
                    SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(command);
                    db.LoadDataSet(cmd, ds, TableName);
                }

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 执行Sql语句或执行不带参数的存储过程返回数据集
        /// </summary>
        /// <param name="CType">执行类型</param>
        /// <param name="command">执行语句</param>
        /// <returns></returns>
        public DataSet Retrieve(CommandType CType, string command, string TableName)
        {
            DataSet ds = new DataSet();

            SqlDatabase db = GetSqlDatabase();
            try
            {
                //执行Sql语句
                if (CType == CommandType.Text)
                {
                    SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(command);
                    ds = db.ExecuteDataSet(cmd);

                }
                //执行不带参数的存储过程
                if (CType == CommandType.StoredProcedure)
                {
                    SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(command);
                    ds = db.ExecuteDataSet(cmd);
                }

                ds.Tables[0].TableName = TableName;

            }
            catch
            {
                throw;
            }
            return ds;
        }
        /// <summary>
        /// 执行Sql语句返回数据集
        /// </summary>
        /// <param name="command"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataSet Retrieve(CommandType CType, string command, Db_Common.InParameter[] InparameterValues, string TableName)
        {
            DataSet ds = new DataSet();

            SqlDatabase db = GetSqlDatabase();
            try
            {

                //执行Sql语句
                if (CType == CommandType.Text)
                {
                    SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(get_Replace_SQL(command));

                    for (int i = 0; i < InparameterValues.Length; i++)
                    {
                        db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
                    }

                    ds = db.ExecuteDataSet(cmd);

                }

                //执行不带参数的存储过程
                if (CType == CommandType.StoredProcedure)
                {
                    SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(command);

                    for (int i = 0; i < InparameterValues.Length; i++)
                    {
                        db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
                    }

                    ds = db.ExecuteDataSet(cmd);
                }


                ds.Tables[0].TableName = TableName;

            }
            catch
            {
                throw;
            }
            return ds;
        }
        /// <summary>
        /// 根据存储过程返回数据集-执行带参数的存储过程
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="InparameterValues">存储过程入口参数</param>
        /// <returns></returns>
        public DataSet Retrieve(string StoredProcedureName, Db_Common.InParameter[] InparameterValues, string TableName)
        {
            DataSet ds = new DataSet();

            SqlDatabase db = GetSqlDatabase();

            SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(StoredProcedureName);

            if (InparameterValues.Length < 0)
            {
                return ds;
            }

            //添加存储过程入口参数
            for (int i = 0; i < InparameterValues.Length; i++)
            {
                db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
            }

            try
            {
                ds = db.ExecuteDataSet(cmd);
                ds.Tables[0].TableName = TableName;
            }
            catch
            {
                throw;
            }
            return ds;
        }

        /// <summary>
        /// 执行Sql语句或执行不带参数的存储过程返回数据集
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="CType">执行类型</param>
        /// <param name="command">执行语句</param>
        /// <returns></returns>
        public DataSet Retrieve(String Key, CommandType CType, string command, string TableName)
        {
            DataSet ds = new DataSet();

            SqlDatabase db = GetSqlDatabase(Key);
            try
            {
                //执行Sql语句
                if (CType == CommandType.Text)
                {
                    SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(command);
                    ds = db.ExecuteDataSet(cmd);

                }
                //执行不带参数的存储过程
                if (CType == CommandType.StoredProcedure)
                {
                    SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(command);
                    ds = db.ExecuteDataSet(cmd);
                }

                ds.Tables[0].TableName = TableName;

            }
            catch
            {
                throw;
            }
            return ds;
        }

        /// <summary>
        /// 根据存储过程返回数据集-执行带参数的存储过程
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="InparameterValues">存储过程入口参数</param>
        /// <returns></returns>
        public DataSet Retrieve(String Key, string StoredProcedureName, Db_Common.InParameter[] InparameterValues, string TableName)
        {
            DataSet ds = new DataSet();

            SqlDatabase db = GetSqlDatabase(Key);
            SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(StoredProcedureName);

            if (InparameterValues.Length < 0)
            {
                return ds;
            }

            //添加存储过程入口参数
            for (int i = 0; i < InparameterValues.Length; i++)
            {
                db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
            }

            try
            {
                ds = db.ExecuteDataSet(cmd);
                ds.Tables[0].TableName = TableName;
            }
            catch
            {
                throw;
            }
            return ds;
        }

        public DataSet Retrieve(CommandType CType, string command, string TableName, string Organization)
        {
            return null;
        }

        public DataSet Retrieve(string key, CommandType CType, string command, string TableName, string Organization)
        {
            return null;
        }

        public DataSet Retrieve(CommandType CType, string command, Db_Common.InParameter[] InparameterValues, string TableName, string Organization)
        {
            return null;
        }

        /// <summary>
        /// 执行带参数的存储过程
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名</param>
        /// <param name="parameters">参数</param>
        public void RunProcedure(string StoredProcedureName, Db_Common.InParameter[] InparameterValues)
        {
            SqlDatabase db = GetSqlDatabase();

            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();
                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();
                SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(StoredProcedureName);
                cmd.CommandText = StoredProcedureName;//声明存储过程名
                cmd.Connection = dbconn;
                cmd.Transaction = dbtran;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    //foreach (OracleParameter parameter in parameters)
                    //{
                    //    cmd.Parameters.Add(parameter);
                    //}
                    for (int i = 0; i < InparameterValues.Length; i++)
                    {
                        //db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
                        SqlParameter parameter = new SqlParameter(get_Replace_Param(InparameterValues[i].name), InparameterValues[i].value);
                        parameter.DbType = InparameterValues[i].dbType;
                        parameter.Direction = InparameterValues[i].direction;
                        cmd.Parameters.Add(parameter);
                    }
                    cmd.ExecuteNonQuery();//执行存储过程

                    for (int i = 0; i < InparameterValues.Length; i++)
                    {
                        if (InparameterValues[i].direction == ParameterDirection.Output || InparameterValues[i].direction == ParameterDirection.InputOutput)
                        {
                            InparameterValues[i].value = cmd.Parameters[i].Value;
                        }
                    }

                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch (Exception)
                {

                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
        }

        /// <summary>
        /// 存储过程返回结果集
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="tableName">表名</param>
        /// <returns>结果集</returns>
        public DataSet RunProcedure(string StoredProcedureName, Db_Common.InParameter[] InparameterValues, string tableName)
        {
            SqlDatabase db = GetSqlDatabase();

            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                DataSet dataSet = new DataSet();
                //打开连接
                dbconn.Open();
                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();

                SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(StoredProcedureName);
                cmd.Connection = dbconn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = dbtran;

                //foreach (OracleParameter parameter in parameters)
                //{
                //    cmd.Parameters.Add(parameter);
                //}
                for (int i = 0; i < InparameterValues.Length; i++)
                {
                    //db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
                    SqlParameter parameter = new SqlParameter(get_Replace_Param(InparameterValues[i].name), InparameterValues[i].value);
                    parameter.DbType = InparameterValues[i].dbType;
                    cmd.Parameters.Add(parameter);
                }

                SqlDataAdapter adapt = new SqlDataAdapter(cmd);

                adapt.Fill(dataSet, tableName);
                dbtran.Commit();
                dbconn.Close();

                return dataSet;
            }
        }

        /// <summary>
        /// 执行带参数的存储过程
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名</param>
        /// <param name="parameters">参数</param>
        public void RunProcedure(string StoredProcedureName, System.Data.OracleClient.OracleParameter[] parameters)
        {
        }

        /// <summary>
        /// 存储过程返回结果集
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="tableName">表名</param>
        /// <returns>结果集</returns>
        public DataSet RunProcedure(string StoredProcedureName, System.Data.OracleClient.OracleParameter[] parameters, string tableName)
        {
            return null;
        }

        /// <summary>
        /// 执行删除语句,并返回影响的行数
        /// </summary>
        /// <param name="command">Sql语句</param>
        /// <returns></returns>
        public int Delete(string command)
        {
            int RowCount = 0;

            SqlDatabase db = GetSqlDatabase();
            try
            {
                RowCount = db.ExecuteNonQuery(CommandType.Text, command);
            }
            catch
            {
                throw;
            }
            return RowCount;
        }
        /// <summary>
        /// 带参数SQL 执行删除语句,并返回影响的行数
        /// </summary>
        /// <param name="command">Sql语句</param>
        /// <returns></returns>
        public int Delete(string command, Db_Common.InParameter[] InparameterValues)
        {
            int RowCount = 0;

            SqlDatabase db = GetSqlDatabase();

            SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(get_Replace_SQL(command));

            for (int i = 0; i < InparameterValues.Length; i++)
            {
                db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
            }

            try
            {
                RowCount = db.ExecuteNonQuery(cmd);
                //RowCount = db.ExecuteNonQuery(CommandType.Text, command);
            }
            catch
            {
                throw;
            }
            return RowCount;
        }
        /// <summary>
        /// 执行删除语句,并返回影响的行数
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="command">Sql语句</param>
        /// <returns></returns>
        public int Delete(String Key, string command)
        {
            int RowCount = 0;

            SqlDatabase db = GetSqlDatabase(Key);
            try
            {
                RowCount = db.ExecuteNonQuery(CommandType.Text, command);
            }
            catch
            {
                throw;
            }
            return RowCount;
        }
        /// <summary>
        /// 执行插入语句,并返回影响的行数
        /// </summary>
        /// <param name="command">Sql语句</param>
        /// <returns></returns>
        public int Insert(string command)
        {
            int RowCount = 0;

            SqlDatabase db = GetSqlDatabase();
            try
            {
                RowCount = db.ExecuteNonQuery(CommandType.Text, command);
            }
            catch
            {
                throw;
            }
            return RowCount;
        }
        /// <summary>
        /// 带参数SQL 执行插入语句,并返回影响的行数
        /// </summary>
        /// <param name="command">Sql语句</param>
        /// <returns></returns>
        public int Insert(string command, Db_Common.InParameter[] InparameterValues)
        {

            SqlDatabase db = GetSqlDatabase();


            int RowCount = 0;

            ReplaceColName(ref command, ref InparameterValues);


            SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(get_Replace_SQL(command));

            cmd.Connection = _dbconn;

            cmd.Transaction = _dbtran;


            for (int i = 0; i < InparameterValues.Length; i++)
            {
                db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
            }

            try
            {
                RowCount = db.ExecuteNonQuery(cmd);

            }
            catch
            {
                throw;
            }
            return RowCount;
        }

        /// <summary>
        /// 带参数SQL 执行插入语句,并返回影响的行数
        /// </summary>
        /// <param name="command">Sql语句</param>
        /// <returns></returns>
        public int Insert(string command, OracleParameter[] InparameterValues)
        {
            int RowCount = 0;
            return RowCount;
        }

        /// <summary>
        /// 执行插入语句,并返回影响的行数
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="command">Sql语句</param>
        /// <returns></returns>
        public int Insert(String Key, string command)
        {
            int RowCount = 0;

            SqlDatabase db = GetSqlDatabase(Key);

            try
            {
                RowCount = db.ExecuteNonQuery(CommandType.Text, command);
            }
            catch
            {
                throw;
            }
            return RowCount;
        }

        /// <summary>
        /// 多行插入
        /// </summary>
        /// <param name="InsertSql">插入SQL</param>
        /// <param name="StrVal">值字符串(格式:列名1,列名2##列名1值~列名1值~|) DataTableToStr或ListToStr可以生成</param>
        /// <returns></returns>
        public int InsertBatch(string InsertSql, string StrVal)
        {
            int ColCount = 0;
            int InsertRow = 0;
            SqlDatabase db = GetSqlDatabase();
            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();

                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbconn;

                cmd.CommandType = CommandType.Text;
                try
                {   //执行两个保存数据集的操作

                    ReplaceColName(ref InsertSql, ref StrVal);

                    cmd.CommandText = get_Replace_SQL(InsertSql);

                    Db_Common.InParameter[] InParam = BatchInParameter(InsertSql, StrVal, ref  ColCount);

                    if (InParam == null)
                    {
                        InsertRow = db.ExecuteNonQuery(cmd, dbtran);
                    }
                    else
                    {
                        int RowCount = InParam.Length / ColCount;
                        int Int_for_s = 0;
                        int Int_for_e = 0;

                        for (int r = 1; r <= RowCount; r++)
                        {
                            Int_for_s = (r * ColCount) - ColCount;
                            Int_for_e = (r * ColCount);

                            cmd.Parameters.Clear();

                            for (int u = Int_for_s; u < Int_for_e; u++)
                            {
                                cmd.Parameters.AddWithValue(get_Replace_Param(InParam[u].name), InParam[u].value);
                            }
                            InsertRow = db.ExecuteNonQuery(cmd, dbtran);
                        }
                    }
                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch
                {
                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
            return InsertRow;
        }
        /// <summary>
        /// 批量执行Sql语句
        /// </summary>
        /// <param name="BatchSql">Sql语句</param>
        /// <param name="Dic_InParam">参数泛型Dictionary Key格式：sql对应序号-行号</param>
        public void InsertBatch(string[] BatchSql, Dictionary<string, Db_Common.InParameter[]> Dic_InParam)
        {
            SqlDatabase db = GetSqlDatabase();
            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();

                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbconn;

                cmd.CommandType = CommandType.Text;
                try
                {
                    for (int i = 0; i < BatchSql.Length; i++)
                    {
                        cmd.CommandText = BatchSql[i];

                        var Dic_itme = from p in Dic_InParam
                                       where p.Key.ToString().Contains(i.ToString() + "-")
                                       select new { value = p.Value };

                        int Arr_Count = Dic_itme.Count();

                        if (Arr_Count <= 0)
                        {
                            db.ExecuteNonQuery(cmd, dbtran);
                        }
                        else
                        {
                            foreach (var item in Dic_itme)
                            {
                                Db_Common.InParameter[] Inparam = item.value;
                                cmd.Parameters.Clear();

                                ReplaceColName(ref BatchSql[i], ref Inparam);

                                cmd.CommandText = get_Replace_SQL(BatchSql[i]);

                                for (int x = 0; x < Inparam.Length; x++)
                                {
                                    cmd.Parameters.AddWithValue(get_Replace_Param(Inparam[x].name), Inparam[x].value);
                                }
                                db.ExecuteNonQuery(cmd, dbtran);
                            }
                        }
                    }

                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch
                {
                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
        }
        /// <summary>
        /// <summary>
        /// SQL语句查询指定字段值,返回到查询结果值通用结构体中
        /// </summary>
        /// <param name="selectcommand">查询Sql语句</param>
        /// <returns></returns>
        public Db_Common.rtnvalues QueryValues(string selectcommand)
        {
            DataSet ds;

            Db_Common.rtnvalues rtnv = new Db_Common.rtnvalues();

            SqlDatabase db = GetSqlDatabase();
            try
            {
                ds = db.ExecuteDataSet(CommandType.Text, selectcommand);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return rtnv;
                }
                else
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                rtnv.arg0 = ds.Tables[0].Rows[0][0].ToString();
                                break;
                            case 1:
                                rtnv.arg1 = ds.Tables[0].Rows[0][1].ToString();
                                break;
                            case 2:
                                rtnv.arg2 = ds.Tables[0].Rows[0][2].ToString();
                                break;
                            case 3:
                                rtnv.arg3 = ds.Tables[0].Rows[0][3].ToString();
                                break;
                            case 4:
                                rtnv.arg4 = ds.Tables[0].Rows[0][4].ToString();
                                break;
                            case 5:
                                rtnv.arg5 = ds.Tables[0].Rows[0][5].ToString();
                                break;
                            case 6:
                                rtnv.arg6 = ds.Tables[0].Rows[0][6].ToString();
                                break;
                            case 7:
                                rtnv.arg7 = ds.Tables[0].Rows[0][7].ToString();
                                break;
                            case 8:
                                rtnv.arg8 = ds.Tables[0].Rows[0][8].ToString();
                                break;
                            case 9:
                                rtnv.arg9 = ds.Tables[0].Rows[0][9].ToString();
                                break;
                            case 10:
                                rtnv.arg10 = ds.Tables[0].Rows[0][10].ToString();
                                break;
                            case 11:
                                rtnv.arg11 = ds.Tables[0].Rows[0][11].ToString();
                                break;
                            case 12:
                                rtnv.arg12 = ds.Tables[0].Rows[0][12].ToString();
                                break;
                            case 13:
                                rtnv.arg13 = ds.Tables[0].Rows[0][13].ToString();
                                break;
                            case 14:
                                rtnv.arg14 = ds.Tables[0].Rows[0][14].ToString();
                                break;
                            case 15:
                                rtnv.arg15 = ds.Tables[0].Rows[0][15].ToString();
                                break;
                            case 16:
                                rtnv.arg16 = ds.Tables[0].Rows[0][16].ToString();
                                break;
                            case 17:
                                rtnv.arg17 = ds.Tables[0].Rows[0][17].ToString();
                                break;
                            case 18:
                                rtnv.arg18 = ds.Tables[0].Rows[0][18].ToString();
                                break;
                            case 19:
                                rtnv.arg19 = ds.Tables[0].Rows[0][19].ToString();
                                break;
                            case 20:
                                rtnv.arg20 = ds.Tables[0].Rows[0][20].ToString();
                                break;
                            case 21:
                                rtnv.arg21 = ds.Tables[0].Rows[0][21].ToString();
                                break;
                            case 22:
                                rtnv.arg22 = ds.Tables[0].Rows[0][22].ToString();
                                break;
                            case 23:
                                rtnv.arg23 = ds.Tables[0].Rows[0][23].ToString();
                                break;
                            case 24:
                                rtnv.arg24 = ds.Tables[0].Rows[0][24].ToString();
                                break;
                            case 25:
                                rtnv.arg25 = ds.Tables[0].Rows[0][25].ToString();
                                break;
                            case 26:
                                rtnv.arg26 = ds.Tables[0].Rows[0][26].ToString();
                                break;
                            case 27:
                                rtnv.arg27 = ds.Tables[0].Rows[0][27].ToString();
                                break;
                            case 28:
                                rtnv.arg28 = ds.Tables[0].Rows[0][28].ToString();
                                break;
                            case 29:
                                rtnv.arg29 = ds.Tables[0].Rows[0][29].ToString();
                                break;
                            case 30:
                                rtnv.arg30 = ds.Tables[0].Rows[0][30].ToString();
                                break;
                        }
                    }
                }
                return rtnv;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// SQL语句查询指定字段值,返回到查询结果值通用结构体中
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="selectcommand">查询Sql语句</param>
        /// <returns></returns>
        public Db_Common.rtnvalues QueryValues(String Key, string selectcommand)
        {
            DataSet ds;

            Db_Common.rtnvalues rtnv = new Db_Common.rtnvalues();

            SqlDatabase db = GetSqlDatabase(Key);
            try
            {
                ds = db.ExecuteDataSet(CommandType.Text, selectcommand);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return rtnv;
                }
                else
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                rtnv.arg0 = ds.Tables[0].Rows[0][0].ToString();
                                break;
                            case 1:
                                rtnv.arg1 = ds.Tables[0].Rows[0][1].ToString();
                                break;
                            case 2:
                                rtnv.arg2 = ds.Tables[0].Rows[0][2].ToString();
                                break;
                            case 3:
                                rtnv.arg3 = ds.Tables[0].Rows[0][3].ToString();
                                break;
                            case 4:
                                rtnv.arg4 = ds.Tables[0].Rows[0][4].ToString();
                                break;
                            case 5:
                                rtnv.arg5 = ds.Tables[0].Rows[0][5].ToString();
                                break;
                            case 6:
                                rtnv.arg6 = ds.Tables[0].Rows[0][6].ToString();
                                break;
                            case 7:
                                rtnv.arg7 = ds.Tables[0].Rows[0][7].ToString();
                                break;
                            case 8:
                                rtnv.arg8 = ds.Tables[0].Rows[0][8].ToString();
                                break;
                            case 9:
                                rtnv.arg9 = ds.Tables[0].Rows[0][9].ToString();
                                break;
                            case 10:
                                rtnv.arg10 = ds.Tables[0].Rows[0][10].ToString();
                                break;
                            case 11:
                                rtnv.arg11 = ds.Tables[0].Rows[0][11].ToString();
                                break;
                            case 12:
                                rtnv.arg12 = ds.Tables[0].Rows[0][12].ToString();
                                break;
                            case 13:
                                rtnv.arg13 = ds.Tables[0].Rows[0][13].ToString();
                                break;
                            case 14:
                                rtnv.arg14 = ds.Tables[0].Rows[0][14].ToString();
                                break;
                            case 15:
                                rtnv.arg15 = ds.Tables[0].Rows[0][15].ToString();
                                break;
                            case 16:
                                rtnv.arg16 = ds.Tables[0].Rows[0][16].ToString();
                                break;
                            case 17:
                                rtnv.arg17 = ds.Tables[0].Rows[0][17].ToString();
                                break;
                            case 18:
                                rtnv.arg18 = ds.Tables[0].Rows[0][18].ToString();
                                break;
                            case 19:
                                rtnv.arg19 = ds.Tables[0].Rows[0][19].ToString();
                                break;
                            case 20:
                                rtnv.arg20 = ds.Tables[0].Rows[0][20].ToString();
                                break;
                            case 21:
                                rtnv.arg21 = ds.Tables[0].Rows[0][21].ToString();
                                break;
                            case 22:
                                rtnv.arg22 = ds.Tables[0].Rows[0][22].ToString();
                                break;
                            case 23:
                                rtnv.arg23 = ds.Tables[0].Rows[0][23].ToString();
                                break;
                            case 24:
                                rtnv.arg24 = ds.Tables[0].Rows[0][24].ToString();
                                break;
                            case 25:
                                rtnv.arg25 = ds.Tables[0].Rows[0][25].ToString();
                                break;
                            case 26:
                                rtnv.arg26 = ds.Tables[0].Rows[0][26].ToString();
                                break;
                            case 27:
                                rtnv.arg27 = ds.Tables[0].Rows[0][27].ToString();
                                break;
                            case 28:
                                rtnv.arg28 = ds.Tables[0].Rows[0][28].ToString();
                                break;
                            case 29:
                                rtnv.arg29 = ds.Tables[0].Rows[0][29].ToString();
                                break;
                            case 30:
                                rtnv.arg30 = ds.Tables[0].Rows[0][30].ToString();
                                break;
                        }
                    }
                }
                return rtnv;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 带参数SQL  SQL语句查询指定字段值,返回到查询结果值通用结构体中
        /// </summary>
        /// <param name="selectcommand">查询Sql语句</param>
        /// <returns></returns>
        public Db_Common.rtnvalues QueryValues(string selectcommand, Db_Common.InParameter[] InparameterValues)
        {
            DataSet ds;

            Db_Common.rtnvalues rtnv = new Db_Common.rtnvalues();

            SqlDatabase db = GetSqlDatabase();

            SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(get_Replace_SQL(selectcommand));
            for (int i = 0; i < InparameterValues.Length; i++)
            {
                db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
            }

            try
            {
                ds = db.ExecuteDataSet(cmd);
                //ds = db.ExecuteDataSet(CommandType.Text, selectcommand);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return rtnv;
                }
                else
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        switch (i)
                        {
                            #region
                            case 0:
                                rtnv.arg0 = ds.Tables[0].Rows[0][0].ToString();
                                break;
                            case 1:
                                rtnv.arg1 = ds.Tables[0].Rows[0][1].ToString();
                                break;
                            case 2:
                                rtnv.arg2 = ds.Tables[0].Rows[0][2].ToString();
                                break;
                            case 3:
                                rtnv.arg3 = ds.Tables[0].Rows[0][3].ToString();
                                break;
                            case 4:
                                rtnv.arg4 = ds.Tables[0].Rows[0][4].ToString();
                                break;
                            case 5:
                                rtnv.arg5 = ds.Tables[0].Rows[0][5].ToString();
                                break;
                            case 6:
                                rtnv.arg6 = ds.Tables[0].Rows[0][6].ToString();
                                break;
                            case 7:
                                rtnv.arg7 = ds.Tables[0].Rows[0][7].ToString();
                                break;
                            case 8:
                                rtnv.arg8 = ds.Tables[0].Rows[0][8].ToString();
                                break;
                            case 9:
                                rtnv.arg9 = ds.Tables[0].Rows[0][9].ToString();
                                break;
                            case 10:
                                rtnv.arg10 = ds.Tables[0].Rows[0][10].ToString();
                                break;
                            case 11:
                                rtnv.arg11 = ds.Tables[0].Rows[0][11].ToString();
                                break;
                            case 12:
                                rtnv.arg12 = ds.Tables[0].Rows[0][12].ToString();
                                break;
                            case 13:
                                rtnv.arg13 = ds.Tables[0].Rows[0][13].ToString();
                                break;
                            case 14:
                                rtnv.arg14 = ds.Tables[0].Rows[0][14].ToString();
                                break;
                            case 15:
                                rtnv.arg15 = ds.Tables[0].Rows[0][15].ToString();
                                break;
                            case 16:
                                rtnv.arg16 = ds.Tables[0].Rows[0][16].ToString();
                                break;
                            case 17:
                                rtnv.arg17 = ds.Tables[0].Rows[0][17].ToString();
                                break;
                            case 18:
                                rtnv.arg18 = ds.Tables[0].Rows[0][18].ToString();
                                break;
                            case 19:
                                rtnv.arg19 = ds.Tables[0].Rows[0][19].ToString();
                                break;
                            case 20:
                                rtnv.arg20 = ds.Tables[0].Rows[0][20].ToString();
                                break;
                            case 21:
                                rtnv.arg21 = ds.Tables[0].Rows[0][21].ToString();
                                break;
                            case 22:
                                rtnv.arg22 = ds.Tables[0].Rows[0][22].ToString();
                                break;
                            case 23:
                                rtnv.arg23 = ds.Tables[0].Rows[0][23].ToString();
                                break;
                            case 24:
                                rtnv.arg24 = ds.Tables[0].Rows[0][24].ToString();
                                break;
                            case 25:
                                rtnv.arg25 = ds.Tables[0].Rows[0][25].ToString();
                                break;
                            case 26:
                                rtnv.arg26 = ds.Tables[0].Rows[0][26].ToString();
                                break;
                            case 27:
                                rtnv.arg27 = ds.Tables[0].Rows[0][27].ToString();
                                break;
                            case 28:
                                rtnv.arg28 = ds.Tables[0].Rows[0][28].ToString();
                                break;
                            case 29:
                                rtnv.arg29 = ds.Tables[0].Rows[0][29].ToString();
                                break;
                            case 30:
                                rtnv.arg30 = ds.Tables[0].Rows[0][30].ToString();
                                break;
                            #endregion
                        }
                    }
                }
                return rtnv;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 存储过程查询值,返回到查询结果值通用结构体中
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="InparameterValues">存储过程入口参数</param>
        /// <param name="OutparameterValues">存储过程出口参数</param>
        /// <returns></returns>
        public Db_Common.rtnvalues QueryValues(string StoredProcedureName, Db_Common.InParameter[] InparameterValues, Db_Common.OutParameter[] OutparameterValues)
        {
            Db_Common.rtnvalues rtnv = new Db_Common.rtnvalues();

            SqlDatabase db = GetSqlDatabase();

            SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(StoredProcedureName);

            if (InparameterValues.Length < 0 || OutparameterValues.Length < 0)
            {
                return rtnv;
            }

            //添加存储过程入口参数
            for (int i = 0; i < InparameterValues.Length; i++)
            {
                db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
            }
            //添加存储过程出口参数
            for (int o = 0; o < OutparameterValues.Length; o++)
            {
                db.AddOutParameter(cmd, get_Replace_Param(OutparameterValues[o].name), OutparameterValues[o].dbType, OutparameterValues[o].size);
            }

            try
            {
                db.ExecuteNonQuery(cmd);
                for (int u = 0; u < OutparameterValues.Length; u++)
                {
                    switch (u)
                    {
                        #region
                        case 0:
                            rtnv.arg0 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 1:
                            rtnv.arg1 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 2:
                            rtnv.arg2 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 3:
                            rtnv.arg3 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 4:
                            rtnv.arg4 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 5:
                            rtnv.arg5 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 6:
                            rtnv.arg6 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 7:
                            rtnv.arg7 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 8:
                            rtnv.arg8 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 9:
                            rtnv.arg9 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 10:
                            rtnv.arg10 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 11:
                            rtnv.arg11 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 12:
                            rtnv.arg12 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 13:
                            rtnv.arg13 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 14:
                            rtnv.arg14 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 15:
                            rtnv.arg15 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 16:
                            rtnv.arg16 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 17:
                            rtnv.arg17 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 18:
                            rtnv.arg18 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 19:
                            rtnv.arg19 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 20:
                            rtnv.arg20 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 21:
                            rtnv.arg21 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 22:
                            rtnv.arg22 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 23:
                            rtnv.arg23 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 24:
                            rtnv.arg24 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 25:
                            rtnv.arg25 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 26:
                            rtnv.arg26 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 27:
                            rtnv.arg27 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 28:
                            rtnv.arg28 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 29:
                            rtnv.arg29 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 30:
                            rtnv.arg30 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        #endregion
                    }
                }
                return rtnv;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 存储过程查询值,返回到查询结果值通用结构体中
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="StoredProcedureName">存储过程名称</param>
        /// <param name="InparameterValues">存储过程入口参数</param>
        /// <param name="OutparameterValues">存储过程出口参数</param>
        /// <returns></returns>
        public Db_Common.rtnvalues QueryValues(String Key, string StoredProcedureName, Db_Common.InParameter[] InparameterValues, Db_Common.OutParameter[] OutparameterValues)
        {
            Db_Common.rtnvalues rtnv = new Db_Common.rtnvalues();

            SqlDatabase db = GetSqlDatabase(Key);

            SqlCommand cmd = (SqlCommand)db.GetStoredProcCommand(StoredProcedureName);

            if (InparameterValues.Length < 0 || OutparameterValues.Length < 0)
            {
                return rtnv;
            }

            //添加存储过程入口参数
            for (int i = 0; i < InparameterValues.Length; i++)
            {
                db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
            }
            //添加存储过程出口参数
            for (int o = 0; o < OutparameterValues.Length; o++)
            {
                db.AddOutParameter(cmd, get_Replace_Param(OutparameterValues[o].name), OutparameterValues[o].dbType, OutparameterValues[o].size);
            }

            try
            {
                db.ExecuteNonQuery(cmd);
                for (int u = 0; u < OutparameterValues.Length; u++)
                {
                    switch (u)
                    {
                        #region
                        case 0:
                            rtnv.arg0 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 1:
                            rtnv.arg1 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 2:
                            rtnv.arg2 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 3:
                            rtnv.arg3 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 4:
                            rtnv.arg4 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 5:
                            rtnv.arg5 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 6:
                            rtnv.arg6 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 7:
                            rtnv.arg7 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 8:
                            rtnv.arg8 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 9:
                            rtnv.arg9 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 10:
                            rtnv.arg10 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 11:
                            rtnv.arg11 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 12:
                            rtnv.arg12 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 13:
                            rtnv.arg13 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 14:
                            rtnv.arg14 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 15:
                            rtnv.arg15 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 16:
                            rtnv.arg16 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 17:
                            rtnv.arg17 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 18:
                            rtnv.arg18 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 19:
                            rtnv.arg19 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 20:
                            rtnv.arg20 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 21:
                            rtnv.arg21 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 22:
                            rtnv.arg22 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 23:
                            rtnv.arg23 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 24:
                            rtnv.arg24 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 25:
                            rtnv.arg25 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 26:
                            rtnv.arg26 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 27:
                            rtnv.arg27 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 28:
                            rtnv.arg28 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 29:
                            rtnv.arg29 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        case 30:
                            rtnv.arg30 = db.GetParameterValue(cmd, OutparameterValues[u].name).ToString();
                            break;
                        #endregion
                    }
                }
                return rtnv;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 保存数据(注:一次只保存一张表)
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="tablename">更新的表名</param>
        /// <param name="selectcommand">用于生成insert,update,delete的select语句</param>
        public void Update(DataSet ds, string tablename, string selectcommand)
        {
            SqlDatabase db = GetSqlDatabase();
            db.UpdateDataSet(ConnString, ds, tablename, selectcommand);
        }
        /// <summary>
        /// 保存数据(注:一次只保存一张表)
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="ds">数据集</param>
        /// <param name="tablename">更新的表名</param>
        /// <param name="selectcommand">用于生成insert,update,delete的select语句</param>
        public void Update(string Key, DataSet ds, string tablename, string selectcommand)
        {
            SqlDatabase db = GetSqlDatabase(Key);
            db.UpdateDataSet(ConnString, ds, tablename, selectcommand);
        }
        /// <summary>
        /// 保存数据(注:一次保存多张表)
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="UpdateItems">更新项的集合</param>
        public void Update(DataSet ds, params Db_Common.UpdateItem[] UpdateItems)
        {
            SqlDatabase db = GetSqlDatabase();
            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();

                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();

                try
                {   //执行两个保存数据集的操作
                    for (int i = 0; i < UpdateItems.Length; i++)
                    {
                        db.UpdateDataSet(ds, UpdateItems[i].TableName, UpdateItems[i].SelectSql, dbtran);
                    }

                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch
                {
                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
        }
        /// <summary>
        /// 保存数据(注:一次保存多张表)
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="ds">数据集</param>
        /// <param name="UpdateItems">更新项的集合</param>
        public void Update(String Key, DataSet ds, params Db_Common.UpdateItem[] UpdateItems)
        {
            SqlDatabase db = GetSqlDatabase(Key);
            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();

                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();

                try
                {   //执行两个保存数据集的操作
                    for (int i = 0; i < UpdateItems.Length; i++)
                    {
                        db.UpdateDataSet(ds, UpdateItems[i].TableName, UpdateItems[i].SelectSql, dbtran);
                    }

                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch
                {
                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
        }
        /// <summary>
        /// 执行修改语句,并返回影响的行数
        /// </summary>
        /// <param name="command">Sql语句</param>
        /// <returns></returns>
        public int Update(string command)
        {
            int RowCount = 0;

            SqlDatabase db = GetSqlDatabase();
            try
            {
                RowCount = db.ExecuteNonQuery(CommandType.Text, command);
            }
            catch
            {
                throw;
            }
            return RowCount;
        }
        /// <summary>
        /// 带参数SQL 执行修改语句,并返回影响的行数
        /// </summary>
        /// <param name="command">Sql语句</param>
        /// <returns></returns>
        public int Update(string command, Db_Common.InParameter[] InparameterValues)
        {
            int RowCount = 0;

            SqlDatabase db = GetSqlDatabase();


            SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(get_Replace_SQL(command));

            for (int i = 0; i < InparameterValues.Length; i++)
            {
                db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
            }


            try
            {
                RowCount = db.ExecuteNonQuery(cmd);
                //RowCount = db.ExecuteNonQuery(CommandType.Text, command);
            }
            catch
            {
                throw;
            }
            return RowCount;
        }
        /// <summary>
        /// 执行修改语句,并返回影响的行数
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="command">Sql语句</param>
        /// <returns></returns>
        public int Update(String Key, string command)
        {
            int RowCount = 0;

            SqlDatabase db = GetSqlDatabase(Key);

            try
            {
                RowCount = db.ExecuteNonQuery(CommandType.Text, command);
            }
            catch
            {
                throw;
            }
            return RowCount;
        }
        /// <summary>
        /// 多行更新
        /// </summary>
        /// <param name="UpdateSql">更新SQL</param>
        /// <param name="StrVal">值字符串(格式:列名1,列名2##列名1值~列名1值~|) DataTableToStr或ListToStr可以生成</param>
        /// <returns></returns>
        public int UpdateBatch(string UpdateSql, string StrVal)
        {
            int ColCount = 0;
            int InsertRow = 0;
            SqlDatabase db = GetSqlDatabase();
            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();

                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                try
                {   //执行两个保存数据集的操作

                    cmd.CommandText = UpdateSql;

                    Db_Common.InParameter[] InParam = BatchInParameter(UpdateSql, StrVal, ref  ColCount);

                    if (InParam == null)
                    {
                        InsertRow = db.ExecuteNonQuery(cmd, dbtran);
                    }
                    else
                    {
                        int RowCount = InParam.Length / ColCount;
                        int Int_for_s = 0;
                        int Int_for_e = 0;

                        for (int r = 1; r <= RowCount; r++)
                        {
                            Int_for_s = (r * ColCount) - ColCount;
                            Int_for_e = (r * ColCount);

                            cmd.Parameters.Clear();

                            for (int u = Int_for_s; u < Int_for_e; u++)
                            {
                                cmd.Parameters.AddWithValue(InParam[u].name, InParam[u].value);
                            }
                            InsertRow = db.ExecuteNonQuery(cmd, dbtran);
                        }
                    }
                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch
                {
                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
            return InsertRow;
        }
        /// <summary>
        /// 批量执行Sql语句
        /// </summary>
        /// <param name="BatchSql">Sql语句</param>
        /// <param name="Dic_InParam">参数泛型Dictionary Key格式：sql对应序号-行号</param>
        public void UpdateBatch(string[] BatchSql, Dictionary<string, Db_Common.InParameter[]> Dic_InParam)
        {
            SqlDatabase db = GetSqlDatabase();
            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();

                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbconn;

                cmd.CommandType = CommandType.Text;
                try
                {
                    for (int i = 0; i < BatchSql.Length; i++)
                    {
                        cmd.CommandText = BatchSql[i];

                        var Dic_itme = from p in Dic_InParam
                                       where p.Key.ToString().Contains(i.ToString() + "-")
                                       select new { value = p.Value };

                        int Arr_Count = Dic_itme.Count();

                        if (Arr_Count <= 0)
                        {
                            db.ExecuteNonQuery(cmd, dbtran);
                        }
                        else
                        {
                            foreach (var item in Dic_itme)
                            {
                                Db_Common.InParameter[] Inparam = item.value;
                                cmd.Parameters.Clear();

                                ReplaceColName(ref BatchSql[i], ref Inparam);

                                cmd.CommandText = get_Replace_SQL(BatchSql[i]);

                                for (int x = 0; x < Inparam.Length; x++)
                                {
                                    cmd.Parameters.AddWithValue(get_Replace_Param(Inparam[x].name), Inparam[x].value);
                                }
                                db.ExecuteNonQuery(cmd, dbtran);
                            }
                        }
                    }

                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch
                {
                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
        }

        /// <summary>
        /// 批量执行Sql语句
        /// </summary>
        /// <param name="BatchSql">Sql语句数组</param>
        public void ExecuteBatch(string[] BatchSql)
        {
            SqlDatabase db = GetSqlDatabase();
            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();

                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                try
                {   //执行两个保存数据集的操作
                    for (int i = 0; i < BatchSql.Length; i++)
                    {
                        cmd.CommandText = BatchSql[i];
                        db.ExecuteNonQuery(cmd, dbtran);
                    }

                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch
                {
                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
        }
        public void ExecuteBatch(string[] BatchSql, string[] BatchVal)
        {
            int ColCount = 0;
            SqlDatabase db = GetSqlDatabase();
            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();

                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbconn;
                cmd.CommandType = CommandType.Text;
                try
                {   //执行两个保存数据集的操作
                    for (int i = 0; i < BatchSql.Length; i++)
                    {
                        cmd.CommandText = get_Replace_SQL(BatchSql[i]);
                        if (cmd.CommandText != "")
                        {

                            Db_Common.InParameter[] InParam = BatchInParameter(BatchSql[i], BatchVal[i], ref  ColCount);

                            if (InParam == null)
                            {
                                db.ExecuteNonQuery(cmd, dbtran);
                            }
                            else
                            {
                                int RowCount = InParam.Length / ColCount;
                                int Int_for_s = 0;
                                int Int_for_e = 0;

                                for (int r = 1; r <= RowCount; r++)
                                {
                                    Int_for_s = (r * ColCount) - ColCount;
                                    Int_for_e = (r * ColCount);

                                    cmd.Parameters.Clear();

                                    for (int u = Int_for_s; u < Int_for_e; u++)
                                    {
                                        cmd.Parameters.AddWithValue(get_Replace_Param(InParam[u].name), InParam[u].value);
                                    }
                                    db.ExecuteNonQuery(cmd, dbtran);
                                }
                            }
                        }

                    }

                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch
                {
                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
        }
        /// <summary>
        /// 批量执行Sql语句
        /// </summary>
        /// <param name="BatchSql">Sql语句</param>
        /// <param name="Dic_InParam">参数泛型Dictionary Key格式：sql对应序号-行号</param>
        public void ExecuteBatch(string[] BatchSql, Dictionary<string, Db_Common.InParameter[]> Dic_InParam)
        {
            SqlDatabase db = GetSqlDatabase();
            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();



                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbconn;

                cmd.CommandType = CommandType.Text;
                try
                {
                    for (int i = 0; i < BatchSql.Length; i++)
                    {
                        cmd.CommandText = get_Replace_SQL(BatchSql[i]);

                        var Dic_itme = from p in Dic_InParam
                                       where p.Key.ToString().Contains(i.ToString() + "-")
                                       select new { value = p.Value };

                        int Arr_Count = Dic_itme.Count();

                        if (Arr_Count <= 0)
                        {
                            db.ExecuteNonQuery(cmd, dbtran);
                        }
                        else
                        {
                            foreach (var item in Dic_itme)
                            {
                                Db_Common.InParameter[] Inparam = item.value;
                                cmd.Parameters.Clear();

                                ReplaceColName(ref BatchSql[i], ref Inparam);

                                cmd.CommandText = get_Replace_SQL(BatchSql[i]);

                                for (int x = 0; x < Inparam.Length; x++)
                                {
                                    cmd.Parameters.AddWithValue(get_Replace_Param(Inparam[x].name), Inparam[x].value);
                                }
                                db.ExecuteNonQuery(cmd, dbtran);
                            }
                        }
                    }

                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch
                {
                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
        }
        /// <summary>
        /// 批量执行Sql语句
        /// </summary>
        /// <param name="Key">机构对应的Key</param>
        /// <param name="BatchSql">Sql语句数组</param>
        public void ExecuteBatch(String Key, string[] BatchSql)
        {
            SqlDatabase db = GetSqlDatabase(Key);
            using (SqlConnection dbconn = (SqlConnection)db.CreateConnection())
            {
                //打开连接
                dbconn.Open();

                //创建事务
                SqlTransaction dbtran = dbconn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                try
                {   //执行两个保存数据集的操作
                    for (int i = 0; i < BatchSql.Length; i++)
                    {
                        cmd.CommandText = BatchSql[i];
                        db.ExecuteNonQuery(cmd, dbtran);
                    }

                    //执行完成后提交事务
                    dbtran.Commit();
                }
                catch
                {
                    //回滚事务
                    dbtran.Rollback();
                    throw;
                }
                finally
                {
                    //关闭连接
                    dbconn.Close();
                }
            }
        }
        private void ReplaceColName(ref string OldSql, ref string OldVal)
        {
            return;
            string StrCol = OldVal.Split(new string[] { "##" }, StringSplitOptions.None)[0];
            string[] Arr_Col = StrCol.Split(new string[] { "," }, StringSplitOptions.None);

            for (int y = 0; y < Arr_Col.Length; y++)
            {
                if (Arr_Col[y].Length > 10)
                {
                    string Str_OldColName = Arr_Col[y];
                    String Str_NewColName = "COL" + (y + 10).ToString();

                    if (y == 0)
                    {
                        OldVal = OldVal.Replace(Str_OldColName + ",", Str_NewColName + ",");
                    }
                    else
                    {
                        OldVal = OldVal.Replace("," + Str_OldColName, "," + Str_NewColName);
                    }

                    if (OldSql.Contains(":" + Str_OldColName + ")"))
                    {
                        OldSql = OldSql.Replace(":" + Str_OldColName + ")", ":" + Str_NewColName + ")");
                    }
                    else
                    {
                        OldSql = OldSql.Replace(":" + Str_OldColName + ",", ":" + Str_NewColName + ",");
                    }

                }
            }
        }
        private void ReplaceColName(ref string OldSql, ref Db_Common.InParameter[] InparameterValues)
        {
            return;
            for (int y = 0; y < InparameterValues.Length; y++)
            {
                if (InparameterValues[y].name.Length > 10)
                {
                    string Str_OldColName = InparameterValues[y].name;
                    String Str_NewColName = "COL" + (y + 10).ToString();

                    if (OldSql.Contains(Str_OldColName + ")"))
                    {
                        OldSql = OldSql.Replace(Str_OldColName + ")", ":" + Str_NewColName + ")");
                    }
                    else
                    {
                        OldSql = OldSql.Replace(Str_OldColName + ",", ":" + Str_NewColName + ",");
                    }

                    InparameterValues[y].name = ":" + Str_NewColName;

                }
            }
        }
        private Db_Common.InParameter[] BatchInParameter(string InSql, string InVal, ref int Colcount)
        {
            int InParam_index = 0;

            Dictionary<string, string> Dic_SqlArg = new Dictionary<string, string>();

            string StrVal = InVal.Split(new string[] { "##" }, StringSplitOptions.None)[1];
            string StrCol = InVal.Split(new string[] { "##" }, StringSplitOptions.None)[0];

            string[] Arr_Col = StrCol.Split(new string[] { "," }, StringSplitOptions.None);


            for (int i = 0; i < Arr_Col.Length; i++)
            {
                Dic_SqlArg.Add(i.ToString(), Arr_Col[i]);
            }

            if (Dic_SqlArg.Count <= 0)
            {
                Colcount = 0;
                return null;
            }

            string[][] Arr_Val = StrToArray(StrVal);

            int InParam_Length = Dic_SqlArg.Count * Arr_Val.Length;

            Db_Common.InParameter[] InParam = new Db_Common.InParameter[InParam_Length];

            for (int i = 0; i < Arr_Val.Length; i++)
            {
                for (int r = 0; r < Arr_Val[i].Length; r++)
                {
                    if (Dic_SqlArg.ContainsKey(r.ToString()))
                    {

                        InParam[InParam_index].name = get_Replace_Param(Dic_SqlArg[r.ToString()]);
                        InParam[InParam_index].value = Arr_Val[i][r];
                        InParam_index = InParam_index + 1;
                    }
                }
            }
            Colcount = Dic_SqlArg.Count;
            return InParam;
        }
        private string[][] StrToArray(String Str)
        {


            try
            {
                if (!string.IsNullOrEmpty(Str))
                {

                    string[] RowsObj = Str.Split(new string[] { "~|" }, StringSplitOptions.None);

                    string[][] RtnArray = new string[RowsObj.Length - 1][];


                    for (int l = 0; l < RowsObj.Length - 1; l++)
                    {
                        string[] TmpArr = RowsObj[l].Split(new string[] { "~" }, StringSplitOptions.None);

                        string[] RowArr = new string[TmpArr.Length];

                        for (int i = 0; i < RowArr.Length; i++)
                        {
                            RowArr[i] = TmpArr[i];
                        }

                        RtnArray[l] = RowArr;
                    }
                    return RtnArray;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 带参数SQL  SQL语句查询指定字段值,返回到object中
        /// </summary>
        /// <param name="selectcommand">查询Sql语句</param>
        /// <returns></returns>
        public object QueryObject(string selectcommand, Db_Common.InParameter[] InparameterValues)
        {
            SqlDatabase db = GetSqlDatabase();

            ReplaceColName(ref selectcommand, ref InparameterValues);

            SqlCommand cmd = (SqlCommand)db.GetSqlStringCommand(get_Replace_SQL(selectcommand));

            for (int i = 0; i < InparameterValues.Length; i++)
            {
                db.AddInParameter(cmd, get_Replace_Param(InparameterValues[i].name), InparameterValues[i].dbType, InparameterValues[i].value);
            }

            try
            {
                object obj = db.ExecuteScalar(cmd);

                return obj;
            }
            catch
            {
                throw;
            }

        }

        int IDbHelper.GetScalar(string safeSql)
        {
            throw new NotImplementedException();
        }

        #region 更改序号，用于排序

        /// <summary>
        /// 更改序号,上移,序号由大变小,也即跟比它大一点的进行交换
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="ID"></param>
        /// <param name="sWhere"></param>
        public Db_Common.rtnvalues ChangeSortUp(string sTableName, string s_主键字段, string s_主键字段值, string s_排序字段, string sWhere)
        {
            Db_Common.rtnvalues rtnv = new Db_Common.rtnvalues();
            List<string> lstSQL = new List<string>();
            string sql = string.Format(@"select t1.{3},t2.{1} as {1}2,t2.{3} as {3}2 from {0} t1,
(select top 1 {1},{3} from {0} where {3}<(select {3} from {0} where {1}='{2}') {4} order by {3} Desc)t2
where t1.{1}='{2}'", sTableName,
                   s_主键字段,
                   s_主键字段值,
                   s_排序字段,
                string.IsNullOrEmpty(sWhere) ? "" : " and " + sWhere);
            DataTable dtSort = Retrieve(CommandType.Text, sql, sTableName).Tables[0];
            if (dtSort.Rows.Count > 0)
            {
                lstSQL.Add(string.Format(@"update {0} set {3}={1} where {4}='{2}'", sTableName, dtSort.Rows[0][s_排序字段 + "2"], s_主键字段值, s_排序字段, s_主键字段));
                lstSQL.Add(string.Format(@"update {0} set {3}={1} where {4}='{2}'", sTableName, dtSort.Rows[0][s_排序字段], dtSort.Rows[0][s_主键字段 + "2"], s_排序字段, s_主键字段));
                try
                {
                    ExecuteBatch(lstSQL.ToArray());
                    rtnv.arg0 = "1";
                    rtnv.arg1 = dtSort.Rows[0][s_排序字段].ToString();//传入的主键值对应的序号
                    rtnv.arg2 = dtSort.Rows[0][s_主键字段 + "2"].ToString();//需交换（上移）的主键值
                    rtnv.arg3 = dtSort.Rows[0][s_排序字段 + "2"].ToString();//需交换（上移）的主键值对应的序号
                }
                catch
                {
                    rtnv.arg0 = "0";
                }
            }
            return rtnv;
        }

        /// <summary>
        /// 更改序号,下移,序号由小变大,也即跟比它小一点的进行交换
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="ID"></param>
        /// <param name="sWhere"></param>
        public Db_Common.rtnvalues ChangeSortDown(string sTableName, string s_主键字段, string s_主键字段值, string s_排序字段, string sWhere)
        {
            Db_Common.rtnvalues rtnv = new Db_Common.rtnvalues();
            List<string> lstSQL = new List<string>();
            //排序上移
            string sql = string.Format(@"select t1.{3},t2.{1} as {1}2,t2.{3} as {3}2 from {0} t1,
(select top 1 {1},{3} from {0} where {3}>(select {3} from {0} where {1}='{2}') {4} order by {3} asc)t2
where t1.{1}='{2}'", sTableName,
                   s_主键字段,
                   s_主键字段值,
                   s_排序字段,
                string.IsNullOrEmpty(sWhere) ? "" : " and " + sWhere);
            DataTable dtSort = Retrieve(CommandType.Text, sql, sTableName).Tables[0];
            if (dtSort.Rows.Count > 0)
            {
                lstSQL.Add(string.Format(@"update {0} set {3}={1} where {4}='{2}'", sTableName, dtSort.Rows[0][s_排序字段 + "2"], s_主键字段值, s_排序字段, s_主键字段));
                lstSQL.Add(string.Format(@"update {0} set {3}={1} where {4}='{2}'", sTableName, dtSort.Rows[0][s_排序字段], dtSort.Rows[0][s_主键字段 + "2"], s_排序字段, s_主键字段));
                try
                {
                    ExecuteBatch(lstSQL.ToArray());
                    rtnv.arg0 = "1";
                    rtnv.arg1 = dtSort.Rows[0][s_排序字段].ToString();//传入的主键值对应的序号
                    rtnv.arg2 = dtSort.Rows[0][s_主键字段 + "2"].ToString();//需交换（上移）的主键值
                    rtnv.arg3 = dtSort.Rows[0][s_排序字段 + "2"].ToString();//需交换（上移）的主键值对应的序号
                }
                catch
                {
                    rtnv.arg0 = "0";
                }
            }
            return rtnv;
        }
        #endregion


        #region 取得唯一号

        public Db_Common.rtnvalues M_取得_唯一号(string str_唯一号编码, string str_机构编码, string str_事务类型)
        {
            Db_Common.rtnvalues rtnv = new Db_Common.rtnvalues();
            return rtnv;
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 字符串替换函数
        /// </summary>
        /// <param name="sql_parm">SQL语句</param>
        /// <returns></returns>
        public string get_Replace_SQL(string str_sql)
        {
            return Db_Common.get_Replace_SQL(str_sql, ":", "@");
        }

        /// <summary>
        /// 字符串替换函数
        /// </summary>
        /// <param name="sql_parm">参数名称</param>
        /// <returns></returns>
        public string get_Replace_Param(string str_param)
        {
            return Db_Common.get_Replace_Param(str_param, ":", "@");
        }
        #endregion


        #region 多个过程共用同一事务

        public void OpenDataBase()
        {
            if (_dbconn == null || _db == null)
            {
                _db = GetSqlDatabase();
                _dbconn = (SqlConnection)_db.CreateConnection();
            }
            if (_dbconn.State == ConnectionState.Closed)
                _dbconn.Open();
        }
        public void CloseDataBase()
        {
            if (_dbconn != null)
            {
                _dbconn.Close();
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTrans()
        {
            OpenDataBase();
            _dbtran = _dbconn.BeginTransaction();
        }
        /// <summary>
        /// 执行事务
        /// </summary>
        public void CommintTrans()
        {
            if (_dbtran != null)
                _dbtran.Commit();
            //关闭数据库连接
            CloseDataBase();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTrans()
        {
            if (_dbtran != null)
                _dbtran.Rollback();
            //关闭数据库连接
            CloseDataBase();
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="command"></param>
        /// <param name="InparameterValues"></param>
        /// <returns></returns>
        public int Insert_Tran(string command, Db_Common.InParameter[] InparameterValues)
        {
            int RowCount = 0;
            //打开连接
            OpenDataBase();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = _dbconn;
            cmd.Transaction = _dbtran;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = get_Replace_SQL(command);

            for (int i = 0; i < InparameterValues.Length; i++)
            {
                cmd.Parameters.AddWithValue(get_Replace_Param(InparameterValues[i].name), InparameterValues[i].value);
                cmd.Parameters[i].DbType = InparameterValues[i].dbType;
            }

            try
            {
               RowCount = cmd.ExecuteNonQuery();
                //RowCount = db.ExecuteNonQuery(CommandType.Text, command);
            }
            catch
            {
                throw;
            }
            return  RowCount;
        }
        /// <summary>
        /// 批量执行Sql语句
        /// </summary>
        /// <param name="BatchSql">Sql语句数组</param>
        public void ExecuteBatch_Tran(string[] BatchSql)
        {
            //打开连接
            OpenDataBase();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _dbconn;
            cmd.Transaction = _dbtran;

            cmd.CommandType = CommandType.Text;
            try
            {   //执行两个保存数据集的操作
                for (int i = 0; i < BatchSql.Length; i++)
                {
                    cmd.CommandText = BatchSql[i];
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 批量执行Sql语句
        /// </summary>
        /// <param name="BatchSql">批量SQL</param>
        /// <param name="BatchVal">批量值字符串(格式:列名1,列名2##列名1值~列名1值~|) DataTableToStr或ListToStr可以生成</param>
        /// <returns></returns>
        public void ExecuteBatch_Tran(string[] BatchSql, string[] BatchVal)
        {
            //打开连接
            OpenDataBase();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _dbconn;
            cmd.Transaction = _dbtran;
            cmd.CommandType = CommandType.Text;
            try
            {   //执行两个保存数据集的操作
                for (int i = 0; i < BatchSql.Length; i++)
                {
                    ReplaceColName(ref BatchSql[i], ref BatchVal[i]);

                    cmd.CommandText = BatchSql[i];

                    if (BatchVal[i].Split(new string[] { "##" }, StringSplitOptions.None)[1] == "")
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(BatchSql[i]))
                    {
                        cmd.ExecuteNonQuery();
                        continue;
                    }

                    int ColCount = 0;
                    Db_Common.InParameter[] InParam = BatchInParameter(BatchSql[i], BatchVal[i], ref  ColCount);

                    if (InParam == null)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        int RowCount = InParam.Length / ColCount;
                        int Int_for_s = 0;
                        int Int_for_e = 0;

                        for (int r = 1; r <= RowCount; r++)
                        {
                            Int_for_s = (r * ColCount) - ColCount;
                            Int_for_e = (r * ColCount);

                            cmd.Parameters.Clear();

                            for (int u = Int_for_s; u < Int_for_e; u++)
                            {
                                cmd.Parameters.AddWithValue(get_Replace_Param(InParam[u].name), InParam[u].value);
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 批量执行Sql语句
        /// </summary>
        /// <param name="BatchSql">Sql语句</param>
        /// <param name="Dic_InParam">参数泛型Dictionary Key格式：sql对应序号-行号</param>
        public void ExecuteBatch_Tran(string[] BatchSql, Dictionary<string, Db_Common.InParameter[]> Dic_InParam)
        {
            //打开连接
            OpenDataBase();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _dbconn;
            cmd.Transaction = _dbtran;

            cmd.CommandType = CommandType.Text;
            try
            {
                for (int i = 0; i < BatchSql.Length; i++)
                {
                    cmd.CommandText = get_Replace_SQL(BatchSql[i]);

                    var Dic_itme = from p in Dic_InParam
                                   where p.Key.ToString().Contains(i.ToString() + "-")
                                   select new { value = p.Value };

                    int Arr_Count = Dic_itme.Count();

                    if (Arr_Count <= 0)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        foreach (var item in Dic_itme)
                        {
                            Db_Common.InParameter[] Inparam = item.value;
                            cmd.Parameters.Clear();

                            ReplaceColName(ref BatchSql[i], ref Inparam);

                            cmd.CommandText = get_Replace_SQL(BatchSql[i]);

                            for (int x = 0; x < Inparam.Length; x++)
                            {
                                cmd.Parameters.AddWithValue(get_Replace_Param(Inparam[x].name), Inparam[x].value);
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }


        public void ExecuteBatch_Tran(string[] BatchSql, Dictionary<string, Db_Common.InParameter[]> Dic_InParam,bool B_true)
        {
            //打开连接
            OpenDataBase();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _dbconn;
            cmd.Transaction = _dbtran;

            cmd.CommandType = CommandType.Text;
            try
            {
                for (int i = 0; i < BatchSql.Length; i++)
                {
                    cmd.CommandText = get_Replace_SQL(BatchSql[i]);

                    var Dic_itme = from p in Dic_InParam
                                   where p.Key.ToString().Contains(i.ToString() + "-")
                                   select new { value = p.Value };

                    int Arr_Count = Dic_itme.Count();

                    if (Arr_Count <= 0)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        foreach (var item in Dic_itme)
                        {
                            Db_Common.InParameter[] Inparam = item.value;
                            cmd.Parameters.Clear();

                            ReplaceColName(ref BatchSql[i], ref Inparam);

                            cmd.CommandText = get_Replace_SQL(BatchSql[i]);

                            for (int x = 0; x < Inparam.Length; x++)
                            {
                                cmd.Parameters.AddWithValue(get_Replace_Param(Inparam[x].name), Inparam[x].value);
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 执行带参数的存储过程
        /// </summary>
        /// <param name="StoredProcedureName">存储过程名</param>
        /// <param name="parameters">参数</param>
        public void RunProcedure_Tran(string StoredProcedureName, System.Data.OracleClient.OracleParameter[] parameters)
        {
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="str_Sql">SQL语句</param>
        /// <param name="str_PhotoCoumn">图片字段</param>
        /// <param name="imgByte"></param>
        public void SavePhoto_Tran(string str_Sql, string str_PhotoCoumn, Byte[] imgByte)
        {
            //打开连接
            OpenDataBase();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _dbconn;
            cmd.Transaction = _dbtran;

            cmd.CommandType = CommandType.Text;
            try
            {   //执行两个保存数据集的操作
                cmd.CommandText = str_Sql;
                cmd.Parameters.Add(str_PhotoCoumn, SqlDbType.Binary, imgByte.Length);
                cmd.Parameters[0].Value = imgByte;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        public bool Connect_test(Db_Common.Db_类型 dbtype, string str_con)
        {
            try
            {
                OpenDataBase();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool getConn(string conn)
        {
            ConnString = conn;
            return true;
        }
    }
}
