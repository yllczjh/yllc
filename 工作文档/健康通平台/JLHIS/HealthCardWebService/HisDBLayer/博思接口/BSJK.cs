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
    public class BSJK
    {
        /// <summary>
        /// 插入一条收据号对照信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="bs_rcpt"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int InsertBS_RCPT(BS_RCPT bs_rcpt, BaseEntityer db)
        {
            if (bs_rcpt.BS_DATA == null)
            {
                bs_rcpt.BS_DATA = "";
            }

            string sql = @"INSERT INTO BS_RCPT  
                              (BS_RCPT.HIS_RCPT_NO,
                               BS_RCPT.BS_RCPT_NO,
                               BS_RCPT.BS_RCPT_TYPE,
                               BS_RCPT.BS_RCPT_INDEXNO,
                               BS_RCPT.BS_DATA,
                               BS_RCPT.BS_DATETIME,
                               BS_RCPT.BS_VALID,
                               BS_RCPT.BS_REV_DATA,
                               BS_RCPT.OPER_CODE,
                               BS_RCPT.OPER_DATE) 
                            VALUES 
                              ('{0}','{1}','{2}','{3}','{4}',to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'),'{6}','{7}','{8}',to_date('{9}', 'yyyy-mm-dd hh24:mi:ss'))";
            object[] param = new object[] { bs_rcpt.HIS_RCPT_NO, bs_rcpt.BS_RCPT_NO, bs_rcpt.BS_RCPT_TYPE, bs_rcpt.BS_RCPT_INDEXNO, bs_rcpt.BS_DATA, bs_rcpt.BS_DATETIME.ToString(), bs_rcpt.BS_VALID, bs_rcpt.BS_REV_DATA, bs_rcpt.OPER_CODE, bs_rcpt.OPER_DATE.ToString() };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新收据状态为作废状态
        /// </summary>
        /// <param name="HIS_RCPT_NO"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateBS_RCPTbyKey(string hisInvoiceNO, string bsInvoiceNO, string invoiceType, string oper_Code, string oper_Date, BaseEntityer db)
        {
            string sql = @"UPDATE bs_rcpt t
                           SET t.bs_valid = '0',
                          t.OPER_CODE='{3}',
                          t.OPER_DATE=to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')
                         WHERE t.his_rcpt_no = '{0}'
                           AND t.bs_rcpt_no = '{1}'
                           AND t.bs_rcpt_type = '{2}'
                           and t.bs_valid='1'
                        ";
            sql = sql.SqlFormate(hisInvoiceNO, bsInvoiceNO, invoiceType, oper_Code, oper_Date);
            return db.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 查询收据号对照信息
        /// </summary>
        /// <param name="HIS_RCPT_NO"></param>
        /// <returns></returns>
        public List<BS_RCPT> QueryBS_RCPT(string HIS_RCPT_NO)
        {
            string sql = @"SELECT *
                              FROM bs_rcpt t
                             WHERE t.bs_rcpt_indexno = '{0}'
                               AND t.bs_valid = '1'
                            ";
            sql = sql.SqlFormate(HIS_RCPT_NO);
            var ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<BS_RCPT>(ds).ToList();
        }
    }
}
