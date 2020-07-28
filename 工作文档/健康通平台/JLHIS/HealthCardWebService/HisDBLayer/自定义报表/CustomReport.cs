using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HisCommon.DataEntity;
using HisCommon;
using System.Data.Common;

namespace HisDBLayer
{
    public class CustomReport
    {
        #region 插入条件约束
        /// <summary>
        /// 插入条件约束
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int InsertSqlWhere(BaseEntityer db, COM_SQLWHERE sqlWhere, ref string errMsg)
        {
            string sql = @"insert into COM_SQLWHERE(WHERE_ID,WHERE_NAME,WHERE_PARAM,WHERE_REMARK) values ('{0}','{1}','{2}','{3}')";
            sql = string.Format(sql, sqlWhere.WHERE_ID, sqlWhere.WHERE_NAME, sqlWhere.WHERE_PARAM, sqlWhere.WHERE_REMARK);
            int revInt = 0;
            try
            {
                revInt = db.ExecuteNonQuery(sql);
                if (revInt <= 0)
                    return -1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return revInt;
        }
        #endregion

        #region 删除条件约束
        /// <summary>
        /// 删除条件约束
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int DeleteSqlWhere(BaseEntityer db, string WhereID, ref string errMsg)
        {
            string sql = @"delete from COM_SQLWHERE where WHERE_ID = '{0}'";
            sql = string.Format(sql, WhereID);
            int revInt = 0;
            try
            {
                revInt = db.ExecuteNonQuery(sql);
                if (revInt <= 0)
                    return -1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return revInt;
        }
        #endregion

        #region 查询条件约束
        /// <summary>
        /// 查询条件约束
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static COM_SQLWHERE GetSqlWhere(BaseEntityer db, string WhereID, ref string errMsg)
        {
            string sql = @"select WHERE_ID,WHERE_NAME,WHERE_REMARK,WHERE_PARAM from COM_SQLWHERE where WHERE_ID = '{0}'";
            sql = string.Format(sql, WhereID);
            DataTable dt = new DataTable();
            COM_SQLWHERE sqlwhere = new COM_SQLWHERE();
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                sqlwhere.WHERE_ID = dt.Rows[0][0].ToString();
                sqlwhere.WHERE_NAME = dt.Rows[0][1].ToString();
                sqlwhere.WHERE_PARAM = dt.Rows[0][3].ToString();
                sqlwhere.WHERE_REMARK = dt.Rows[0][2].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return sqlwhere;
        }
        #endregion

        #region 插入SQL约束
        /// <summary>
        /// 插入SQL约束
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int InsertSql(BaseEntityer db, COM_SQL sqlWhere, ref string errMsg)
        {
            //{266CE171-D67A-4C01-AFD6-2368F1623546}
            string sql = @"insert into COM_SQL(SQL_ID,SQL_NAME,SQL_TYPE,SQL_PARAM,SQL_REMARK,SQL_PBLNAME,SQL_REPORTNAME) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
            sql = string.Format(sql, sqlWhere.Sql_ID, sqlWhere.Sql_NAME, sqlWhere.Sql_TYPE, sqlWhere.Sql_PARAM, sqlWhere.Sql_REMARK, sqlWhere.Sql_PBLName, sqlWhere.Sql_ReportName);
            int revInt = 0;
            try
            {
                revInt = db.ExecuteNonQuery(sql);
                if (revInt <= 0)
                    return -1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return revInt;
        }
        #endregion

        #region 删除SQL约束
        /// <summary>
        /// 删除SQL约束
        /// </summary>
        /// <param name="SqlID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int DeleteSql(BaseEntityer db, string SqlID, ref string errMsg)
        {
            string sql = @"delete from COM_SQL where SQL_ID = '{0}'";
            sql = string.Format(sql, SqlID);
            int revInt = 0;
            try
            {
                revInt = db.ExecuteNonQuery(sql);
                if (revInt <= 0)
                    return -1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return revInt;
        }
        #endregion

        #region 查询SQL约束
        /// <summary>
        /// 查询SQL约束
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static COM_SQL GetSql(BaseEntityer db, string SqlID, ref string errMsg)
        {
            string sql = @"select SQL_ID,SQL_NAME,SQL_TYPE,SQL_PARAM,SQL_REMARK,SQL_PBLNAME,SQL_REPORTNAME from COM_SQL where SQL_ID='{0}'";
            sql = string.Format(sql, SqlID);
            DataTable dt = new DataTable();
            COM_SQL sqlwhere = new COM_SQL();
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                sqlwhere.Sql_ID = dt.Rows[0][0].ToString();
                sqlwhere.Sql_NAME = dt.Rows[0][1].ToString();
                sqlwhere.Sql_TYPE = dt.Rows[0][2].ToString();
                sqlwhere.Sql_PARAM = dt.Rows[0][3].ToString();
                sqlwhere.Sql_REMARK = dt.Rows[0][4].ToString();
                sqlwhere.Sql_PBLName = dt.Rows[0][5].ToString();
                sqlwhere.Sql_ReportName = dt.Rows[0][6].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return sqlwhere;
        }
        #endregion

        #region 执行ComboboxSQL值
        /// <summary>
        /// 执行ComboboxSQL值
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static List<BringSpringObject> GetSqlValue(BaseEntityer db, string sql, ref string errMsg)
        {
            System.Data.DataSet ds = new DataSet();
            List<BringSpringObject> dl = new List<BringSpringObject>();
            try
            {
                ds = BaseEntityer.Db.GetDataSet(sql);
                if (ds.Tables.Count <= 0)
                    return null;
                dl = DataSetToEntity.DataSetToT<BringSpringObject>(ds).ToList();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return dl;
        }
        #endregion

        #region 执行自定义报表SQL语句
        /// <summary>
        /// 执行自定义报表SQL语句
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataTable ExecuteSql(BaseEntityer db, string sql, ref string errMsg, params object[] args)
        {
            System.Data.DataTable dt = new DataTable();

            try
            {
                if (args.Length > 0)
                    sql = string.Format(sql, args);

                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return dt;
        }
        #endregion
    }
}
