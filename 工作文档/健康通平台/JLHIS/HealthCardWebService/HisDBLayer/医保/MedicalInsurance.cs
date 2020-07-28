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
    public class MedicalInsurance
    {
        #region 手术
        /// <summary>
        /// 删除住院患者手术
        /// </summary>
        /// <returns></returns>
        public int DeleteInPatientOperation(string patientId, int visitId, BaseEntityer db)
        {
            string sql = @"delete from SI_SYUPLOADOPERATION t where t.patientid='{0}' and t.visitid={1}";
            sql = sql.SqlFormate(patientId, visitId);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除门诊患者手术
        /// </summary>
        /// <returns></returns>
        public int DeleteOutPatientOperation(int visitNo, string visitDate, BaseEntityer db)
        {
            string sql = @"delete from SI_SYUPLOADOPERATION t where t.visit_no='{0}' and t.visit_date=to_date('{1}','yyyy-MM-dd hh24:mi:ss')";
            sql = sql.SqlFormate(visitNo, visitDate);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 增加一条患者手术
        /// </summary>
        /// <returns></returns>
        public int InsertPatientOperation(SI_SYUPLOADOPERATION o, BaseEntityer db)
        {
            string sql = @" insert into SI_SYUPLOADOPERATION(PATIENTID,
                            VISITID,
                            SERIAL,
                            OPER_DATE,
                            OPERATION_CODE,
                            OPERATION_NAME,
                            VAILD_FLAG,
                            VISIT_NO,
                            VISIT_DATE,
                            OPER_CODE,
                            OPER_DEPT,
                            OPERATION_BODY)
 values('{0}',
              {1},
              '{2}',
              to_date('{3}', 'yyyy-MM-dd hh24:mi:ss'),
              '{4}',
              '{5}',
              '{6}',
              {7},
            to_date('{8}', 'yyyy-MM-dd hh24:mi:ss'),
              '{9}',
              '{10}',
              '{11}')";
            object[] os = new object[] { o.PATIENTID,o.VISITID,o.SERIAL,
                                       o.OPER_DATE,o.OPERATION_CODE,o.OPERATION_NAME,
                                       o.VAILD_FLAG,o.VISIT_NO,o.VISIT_DATE,
                                       o.OPER_CODE,o.OPER_DEPT,o.OPERATION_BODY};

            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 查询患者诊断信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public List<SI_SYUPLOADOPERATION> QueryOutPatientOperation(int visitNo, string visitDate)
        {
            string sql = @"select *
  from SI_SYUPLOADOPERATION t
 where t.visit_no ={0}
   and t.visit_date=to_date('{1}','yyyy-MM-dd hh24:mi:ss')";
            sql = sql.SqlFormate(visitNo, visitDate);
            var ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<SI_SYUPLOADOPERATION>(ds).ToList();
        }
        /// <summary>
        /// 查询患者诊断信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public List<SI_SYUPLOADOPERATION> QueryInPatientOperation(string patientId, int visitId)
        {
            string sql = @"select *
  from SI_SYUPLOADOPERATION t
 where t.PATIENTID ='{0}'
   and t.VISITID={1}";
            sql = sql.SqlFormate(patientId, visitId);
            var ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<SI_SYUPLOADOPERATION>(ds).ToList();
        }
        /// <summary>
        /// 获得医保手术字典信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetSIOperationDict()
        {
            string sql = @"select * from SI_SYOPERATION t";
            return BaseEntityer.Db.GetDataTable(sql);
        }
        #endregion

        #region 门诊收费及退费
        /// <summary>
        /// OUTP_RCPT_MASTER 表插入
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int InsertOutpRcptMaster(OUTP_RCPT_MASTER o, BaseEntityer db)
        {
            string sql = @"insert into OUTP_RCPT_MASTER
  (RCPT_NO,
   PATIENT_ID,
   NAME,
   NAME_PHONETIC,
   IDENTITY,
   CHARGE_TYPE,
   UNIT_IN_CONTRACT,
   VISIT_DATE,
   TOTAL_COSTS,
   TOTAL_CHARGES,
   OPERATOR_NO,
   CHARGE_INDICATOR,
   REFUNDED_RCPT_NO,
   ACCT_NO,
   INSURANCE_TYPE,
   TB_FLAG,
   REG_DATE,
   REG_NO,
   INVOICE_NEW,purcharge,precharge)
values
  ('{0}',
   '{1}',
   '{2}',
   '{3}',
   '{4}',
   '{5}',
   '{6}',
   to_date('{7}', 'yyyy-MM-dd hh24:mi:ss'),
   {8},
   {9},
   '{10}',
   {11},
   '{12}',
   '{13}',
   '{14}',
   '{15}',
  to_date('{16}', 'yyyy-MM-dd hh24:mi:ss'),
   {17},'{18}', {19},'{20}')
";
            object[] os = new object[]{
            o.RCPT_NO,
            o.PATIENT_ID,
            o.NAME,
            o.NAME_PHONETIC,
            o.IDENTITY,
            o.CHARGE_TYPE,
            o.UNIT_IN_CONTRACT,
            o.VISIT_DATE,
            o.TOTAL_COSTS,
            o.TOTAL_CHARGES,
            o.OPERATOR_NO,
            o.CHARGE_INDICATOR,
            o.REFUNDED_RCPT_NO,
            o.ACCT_NO,
            o.INSURANCE_TYPE,
            o.TB_FLAG,
            o.REG_DATE,
            o.REG_NO,
            o.INVOICE_NEW,
            o.PurCharge,
            o.PreCharge
        };
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// OUTP_PAYMENTS_MONEY 表插入
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int InsertOutpPaymentsMoney(OUTP_PAYMENTS_MONEY o, BaseEntityer db)
        {
            string sql = @"insert into OUTP_PAYMENTS_MONEY
(RCPT_NO, PAYMENT_NO, MONEY_TYPE, PAYMENT_AMOUNT, REFUNDED_AMOUNT,INVOICE_NEW)
values ('{0}', {1}, '{2}', {3}, {4},'{5}')
";
            object[] os = new object[]{
            o.RCPT_NO,
            o.PAYMENT_NO,
            o.MONEY_TYPE,
            o.PAYMENT_AMOUNT,
            o.REFUNDED_AMOUNT,
            o.INVOICE_NEW
        };
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// OUTP_ORDER_DESC 表插入
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int InsertOutpOrderDesc(OUTP_ORDER_DESC o, BaseEntityer db)
        {
            string sql = @"insert into OUTP_ORDER_DESC 
(VISIT_DATE, VISIT_NO, PATIENT_ID, PRESC_INDICATOR, ORDERED_BY_DEPT, ORDERED_BY_DOCTOR, RCPT_NO)
values (to_date('{0}', 'yyyy-MM-dd hh24:mi:ss'), {1}, '{2}', {3}, '{4}', '{5}', '{6}')
";
            object[] os = new object[]{
            o.VISIT_DATE,
            o.VISIT_NO,
            o.PATIENT_ID,
            o.PRESC_INDICATOR,
            o.ORDERED_BY_DEPT,
            o.ORDERED_BY_DOCTOR,
            o.RCPT_NO
        };
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// OUTP_BILL_ITEMS 表插入
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int InsertOutpBillItems(OUTP_BILL_ITEMS o, BaseEntityer db)
        {
            #region SQL

            string sql = @" insert into OUTP_BILL_ITEMS
                              (VISIT_DATE,
                               VISIT_NO,
                               RCPT_NO,
                               ITEM_NO,
                               ITEM_CLASS,
                               CLASS_ON_RCPT,
                               ITEM_CODE,
                               ITEM_NAME,
                               ITEM_SPEC,
                               AMOUNT,
                               UNITS,
                               PERFORMED_BY,
                               COSTS,
                               CHARGES,
                               SERIAL_NO,
                               BATCHNO,
                               D_ITEM_NO,
                               APPOINT_NO,
                               ORDER_DEPT,
                               ORDER_DOCTOR,
                               CHECKFLAG,
                               SYSBJ,
                               QR_TIME,
                               QR_OPPER,
                               SUBJ_CODE,
                               CLASS_ON_RECKONING,
                               COMMON_FLAG,
                               SPECIAL_FLAG,
                               PRICE,
                               INVOICE_NEW,
                               CLINIC_ITEM_CLASS,
                               CLINIC_ITEM_CODE,
                               memo,
                               CLINIC_ITEM_NAME,
                               CLINIC_ITEM_AMOUNT,
                               CLINIC_ITEM_PRICE)
                            values
                              (to_date('{0}', 'yyyy-MM-dd hh24:mi:ss'),
                               {1},
                               '{2}',
                               {3},
                               '{4}',
                               '{5}',
                               '{6}',
                               '{7}',
                               '{8}',
                               {9},
                               '{10}',
                               '{11}',
                               {12},
                               {13},
                               '{14}',
                               '{15}',
                               {16},
                               '{17}',
                               '{18}',
                               '{19}',
                               '{20}',
                               '{21}',
                               to_date('{22}', 'yyyy-MM-dd hh24:mi:ss'),
                               '{23}',
                               '{24}',
                               '{25}',
                               '{26}',
                               '{27}',
                               {28},
                               '{29}',
                               '{30}',
                               '{31}',
                               '{32}',
                               '{33}',
                               '{34}',
                               '{35}') ";

            #endregion

            object[] os = new object[]{
            o.VISIT_DATE,
            o.VISIT_NO,
            o.RCPT_NO,
            o.ITEM_NO,
            o.ITEM_CLASS,
            o.CLASS_ON_RCPT,
            o.ITEM_CODE,
            o.ITEM_NAME,
            o.ITEM_SPEC,
            o.AMOUNT,
            o.UNITS,
            o.PERFORMED_BY,
            o.COSTS,
            o.CHARGES,
            o.SERIAL_NO,
            o.BATCHNO,
            o.D_ITEM_NO,
            o.APPOINT_NO,
            o.ORDER_DEPT,
            o.ORDER_DOCTOR,
            o.CHECKFLAG,
            o.SYSBJ,
            o.QR_TIME,
            o.QR_OPPER,
            o.SUBJ_CODE,
            o.CLASS_ON_RECKONING,
            o.COMMON_FLAG,
            o.SPECIAL_FLAG,
            o.PRICE,
            o.INVOICE_NEW,
            o.CLINIC_ITEM_CLASS,
            o.CLINIC_ITEM_CODE,
            o.Memo,
            o.CLINIC_ITEM_NAME,
            o.CLINIC_ITEM_AMOUNT,
            o.CLINIC_ITEM_PRICE
        };
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// OUTP_RCPT_MASTER 表更新收费状态
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int UpdateOutpRcptMaster(string rcptNo, string RetunRcptNp, int state, BaseEntityer db)
        {
            string sql = @" update outp_rcpt_master t
                               set t.charge_indicator = '{1}',
                                   t.refunded_rcpt_no = '{2}',
                                   t.print_flag       = nvl(t.print_flag, 0) + 1
                             where t.rcpt_no = '{0}' ";
            //暂时这么处理，默认收费后更新打印状态
            //Modify By ZhanGD 2014-09-09
            object[] os = new object[] { rcptNo, state, RetunRcptNp };
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// outp_presc 更新收费状态
        /// </summary>
        /// <returns></returns>
        public int UpdateOutpPresc(string visitDate, string visitNo, string serailNo, string itemNo, int state, BaseEntityer db)
        {
            string sql = @" update outp_presc set
                                 charge_indicator = {4}
                                 where visit_date = to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                   and Visit_no = {1}
                                   and serial_no ='{2}'
                                   and item_no = {3}";
            sql = sql.SqlFormate(new object[] { visitDate, visitNo, serailNo, itemNo, state });
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// outp_treat_rec 更新收费状态
        /// </summary>
        /// <returns></returns>
        public int UpdateOutpTreatPresc(string visitDate, string visitNo, string serailNo, string itemNo, int state, BaseEntityer db)
        {
            string sql = @" update outp_treat_rec set
                                 charge_indicator ={4}
                                 where visit_date = to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                   and Visit_no = {1}
                                   and serial_no ='{2}'
                                   and item_no = {3}";
            sql = sql.SqlFormate(new object[] { visitDate, visitNo, serailNo, itemNo, state });
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// EXAM_appoints 更新收费状态
        /// </summary>
        /// <returns></returns>
        public int UpdateExamAppoints(string examNo, int state, BaseEntityer db)
        {
            string sql = @"update EXAM_appoints set BILLING_INDICATOR ={1} where exam_no ={0}";
            sql = sql.SqlFormate(new object[] { examNo, state });
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// lab_test_master 更新收费状态
        /// </summary>
        /// <returns></returns>
        public int UpdateLabTestMaster(string testNo, int state, BaseEntityer db)
        {
            string sql = @"update lab_test_master set BILLING_INDICATOR ={1} where test_no ={0}";
            sql = sql.SqlFormate(new object[] { testNo, state });
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2014-4-22 by li 一次收费明细项目存储
        /// </summary>
        /// <param name="db"></param>
        /// <param name="item">一次收费明细项目</param>
        /// <returns></returns>
        public int InsertOutpOrdersDetail(BaseEntityer db, OUTP_ORDERS_DETAIL item)
        {
            string sql = @"insert into ONECARD_OUTPORDERSDETAIL
                              (PRINTSERIAL_NO,
                               PRINTGROUP,
                               COUNTNUM,
                               PRINTNUM,
                               ITEM_NAME,
                               NOWCOST,
                               NOWRATE,
                               NOWPRICE,
                               NOWAMOUNT,
                               NOWCODE,
                               NOWCLASS,
                               NOWCLASS_NAME,
                               ITEM_SPEC,
                               UNITS,
                               VISIT_NO,
                               VISIT_DATE,
                               SERIAL_NO,
                               ITEM_NO,
                               CLASS,
                               CODE,
                               NAME,
                               PACKGE_UNITS,
                               AMOUNT,
                               DOSAGE_UNITS,
                               PROVIDED_INDICATOR,
                               CHARGE_INDICATOR,
                               DEPTCODE,
                               DEPT_NAME,
                               PRICE,
                               FS,
                               BATCHNO,
                               ORDER_DOCTOR,
                               ORDERED_BY,
                               APPOINT_NO,
                               CLASS_ON_OUTP_RCPT,
                               SUBJ_CODE,
                               CLASS_ON_RECKONING,
                               COMMON_FLAG,
                               SPECIAL_FLAG)
                            values
                              ({0},
                               {1},
                               {2},
                               {3},
                               '{4}',
                               {5},
                               {6},
                               {7},
                               {8},
                               '{9}',
                               '{10}',
                               '{11}',
                               '{12}',
                               '{13}',
                               {14},
                               to_date('{15}', 'yyyy-MM-dd hh24:mi:ss'),
                               '{16}',
                               {17},
                               '{18}',
                               '{19}',
                               '{20}',
                               '{21}',
                               {22},
                               '{23}',
                               {24},
                               {25},
                               '{26}',
                               '{27}',
                               {28},
                               {29},
                               '{30}',
                               '{31}',
                               '{32}',
                               '{33}',
                               '{34}',
                               '{35}',
                               '{36}',
                               '{37}',
                               '{38}')";
            object[] os = new object[] { item.PrintSerial_No, item.PrintGroup, item.CountNum, 
                item.PrintNum, item.ITEM_NAME, item.NOWCOST, item.NOWRATE, item.NOWPRICE, item.NOWAMOUNT, 
                item.NOWCODE, item.NOWCLASS, item.NOWCLASS_NAME, item.ITEM_SPEC, item.UNITS, item.VISIT_NO, 
                item.VISIT_DATE.ToString(), item.SERIAL_NO, item.ITEM_NO, item.CLASS, item.CODE, 
                item.NAME, item.PACKGE_UNITS, item.AMOUNT, item.DOSAGE_UNITS, item.PROVIDED_INDICATOR, 
                item.CHARGE_INDICATOR, item.DEPTCODE, item.DEPT_NAME, item.PRICE, item.FS, item.BATCHNO, 
                item.ORDER_DOCTOR, item.ORDERED_BY, item.APPOINT_NO, item.CLASS_ON_OUTP_RCPT, 
                item.SUBJ_CODE, item.CLASS_ON_RECKONING, item.COMMON_FLAG, item.SPECIAL_FLAG };
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2014-4-22 by li 获取一次收费明细项目存储
        /// </summary>
        /// <param name="PrintSerial_No"></param>
        /// <returns></returns>
        public IList<OUTP_ORDERS_DETAIL> GetOutpOrdersDetails(string PrintSerial_No)
        {
            string sql = @"SELECT * from ONECARD_OUTPORDERSDETAIL where PRINTSERIAL_NO = {0}";
            sql = string.Format(sql, PrintSerial_No);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<OUTP_ORDERS_DETAIL>(ds);
            return list;
        }

        #endregion

        #region 沈阳医保接口

        #region 获取HIS交易流水号的序号
        /// <summary>
        /// 获取HIS交易流水号的序号
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetHisSequence(ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select SIHISSEQUENCE.NEXTVAL from dual";
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                revString = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return revString;
        }
        #endregion

        #region 获取中心药品最后修改日期
        /// <summary>
        /// 获取中心药品最后修改日期
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GeDrugUpdateDate()
        {
            DataTable dt = new DataTable();
            string sql = @"select nvl(max(to_number(t.update_date)),20100101000000) from SI_sydrug t";
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                revString = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return revString;
        }
        #endregion

        #region 获取就诊流水号序列
        /// <summary>
        /// 获取门诊就诊流水号序列
        /// </summary>
        /// <returns></returns>
        public string GetOutpatientRun()
        {
            DataTable dt = new DataTable();
            string sql = @"select si_outpatientrun.nextval from dual";
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                revString = dt.Rows[0][0].ToString();
            }
            catch
            {
                return null;
            }
            return revString;
        }

        /// <summary>
        /// 获取住院就诊流水号序列
        /// </summary>
        /// <returns></returns>
        public string GetInpatientRun()
        {
            DataTable dt = new DataTable();
            string sql = @"select si_inpatientrun.nextval from dual";
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                revString = dt.Rows[0][0].ToString();
            }
            catch
            {
                return null;
            }
            return revString;
        }
        #endregion

        #region 获取医保最大结算序号
        /// <summary> 
        /// 获取医保最大结算序号
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <returns></returns>
        public int GetMaxBalanceNO(BaseEntityer db, string patientid, string visitid)
        {
            DataTable dt = new DataTable();
            string sql = @"select nvl(max(to_number(t.balance_no)),0) from SIINFO t where t.inpatient_id = '{0}' and t.visit_id = '{1}'";
            sql = string.Format(sql, patientid, visitid);
            int revString = 0;
            try
            {
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                revString = int.Parse(dt.Rows[0][0].ToString());
            }
            catch
            {
                return -1;
            }
            return revString;
        }
        /// <summary>
        /// 读取农合编号
        /// </summary>
        /// <param name="dept_code"></param>
        /// <returns></returns>
        public string GetNHDeptCode(string dept_code)
        {
            DataTable dt = new DataTable();
            string sql = @"select nh_dept_code from  dept_dict where dept_code='{0}'";
            sql = string.Format(sql, dept_code);
            string revString;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return "";
                revString = dt.Rows[0][0].ToString();
            }
            catch
            {
                return "";
            }
            return revString;
        }

        /// <summary>
        /// 读取农合编号和名称
        /// </summary>
        /// <param name="dept_code"></param>
        /// <returns></returns>
        public BringSpringObject GetNHDeptCodeByBringSpring(string dept_code)
        {
            DataTable dt = new DataTable();
            BringSpringObject obj = new BringSpringObject();
            string sql = @"select nh_dept_code.code,nh_dept_code.name,nh_dept_code.pycode from  dept_dict,nh_dept_code where dept_code='{0}' and dept_dict.nh_dept_code = nh_dept_code.code";

            try
            {
                sql = string.Format(sql, dept_code);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                obj.ID = dt.Rows[0][0].ToString();
                obj.Name = dt.Rows[0][1].ToString();
                obj.Memo = dt.Rows[0][2].ToString();
            }
            catch
            {
                return null;
            }
            return obj;
        }

        /// <summary>
        /// 获取门诊的的最大序号
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <returns></returns>
        public int GetMaxBalanceNOByVisitDate(BaseEntityer db, string patientid, string visitid, string SHIFTDATE)
        {
            DataTable dt = new DataTable();
            string sql = @"select nvl(max(to_number(t.balance_no)),0) from SIINFO t where t.inpatient_id = '{0}' and t.visit_id = '{1}' and t.SHIFTDATE=to_date('{2}','yyyy-MM-dd hh24:mi:ss')";
            sql = string.Format(sql, patientid, visitid, SHIFTDATE);
            int revString = 0;
            try
            {
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                revString = int.Parse(dt.Rows[0][0].ToString());
            }
            catch
            {
                return -1;
            }
            return revString;
        }

        /// <summary>
        /// 获取门诊的的最大序号
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <returns></returns>
        public int GetMinBalanceNOByVisitDate(BaseEntityer db, string patientid, string visitid, string SHIFTDATE)
        {
            DataTable dt = new DataTable();
            string sql = @"select nvl(min(to_number(t.balance_no)),0) from SIINFO t where t.inpatient_id = '{0}' and t.visit_id = '{1}' and t.SHIFTDATE=to_date('{2}','yyyy-MM-dd hh24:mi:ss') and t.isvalid = 1";
            sql = string.Format(sql, patientid, visitid, SHIFTDATE);
            int revString = 0;
            try
            {
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                revString = int.Parse(dt.Rows[0][0].ToString());
            }
            catch
            {
                return -1;
            }
            return revString;
        }

        #endregion

        #region 签到签退
        /// <summary>
        /// 插入签到表
        /// </summary>
        /// <param name="inSign"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertinSignfo(BaseEntityer db, HisCommon.DataEntity.Sign inSign, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql = @"INSERT INTO SIOPERSIGN
                              (OPER_CODE, --操作员编码 VARCHAR2 #0
                               SIGNIN_CENTER_BIZSEQNO, --中心业务交易流水号 VARCHAR2 #1
                               SIGNIN_CENTER_BIZCYCLENO, --中心业务周期号 VARCHAR2 #2
                               ONLINE_FLAG, --联机模式状态标志（1：联机，0：脱机） VARCHAR2 #3
                               SIGNIN_FLAG, --签到标志（1：签到，0：签退） VARCHAR2 #4
                               SIGNIN_DATE, --签到时间 DATE #5
                               SIGNOUT_DATE, --签退时间 DATE #6
                               SIGNOUT_CENTER_BIZSEQNO, --签退中心业务交易流水号 VARCHAR2 #7
                               SIGNOUT_CENTER_BIZCYCLENO, --签退中心业务周期号 VARCHAR2 #8
                               SIGNIN_HIS_BIZSEQNO, --签到HIS交易流水号 VARCHAR2 #9
                               SIGNOUT_HIS_BIZSEQNO --签退HIS交易流水号 VARCHAR2 #10
                               )
                            VALUES
                              ('{0}', --操作员编码 OPER_CODE #0
                               '{1}', --中心业务交易流水号 SIGNIN_CENTER_BIZSEQNO #1
                               '{2}', --中心业务周期号 SIGNIN_CENTER_BIZCYCLENO #2
                               '{3}', --联机模式状态标志（1：联机，0：脱机） ONLINE_FLAG #3
                               '{4}', --签到标志（1：签到，0：签退） SIGNIN_FLAG #4
                               to_date('{5}','yyyy-MM-dd hh24:mi:ss'), --签到时间 SIGNIN_DATE #5
                               to_date('{6}','yyyy-MM-dd hh24:mi:ss'), --签退时间 SIGNOUT_DATE #6
                               '{7}', --签退中心业务交易流水号 SIGNOUT_CENTER_BIZSEQNO #7
                               '{8}', --签退中心业务周期号 SIGNOUT_CENTER_BIZCYCLENO #8
                               '{9}', --签到HIS交易流水号 SIGNIN_HIS_BIZSEQNO #9
                               '{10}' --签退HIS交易流水号 SIGNOUT_HIS_BIZSEQNO #10
                               )";
            try
            {
                if (string.IsNullOrEmpty(inSign.Oper.ID) == true)
                {
                    err = inSign.Oper.ID;
                    return -1;
                }
                sql = string.Format(sql,
                    //OPER_CODE操作员编码 VARCHAR2  #0
                inSign.Oper.ID
                    //inSign_CENTER_BIZSEQNO中心业务交易流水号 VARCHAR2  #1
                , inSign.SigninBizSeqNO
                    //inSign_CENTER_BIZCYCLENO中心业务周期号 VARCHAR2  #2
                , inSign.SigninBizCycleNO
                    //ONLINE_FLAG联机模式状态标志（1：联机，0：脱机） VARCHAR2  #3
                    , inSign.IsOnline ? "1" : "0"
                    //inSign_FLAG签到标志（1：签到，0：签退） VARCHAR2  #4
                , inSign.IsSignIn ? "1" : "0"
                    //inSign_DATE签到时间 DATE  #5
                , inSign.SignInOper.ToString("yyyy-MM-dd HH:mm:ss")
                    //SIGNOUT_DATE签退时间 DATE  #6
                , inSign.SignOutOper.ToString("yyyy-MM-dd HH:mm:ss")
                    //SIGNOUT_CENTER_BIZSEQNO签退中心业务交易流水号 VARCHAR2  #7
                , inSign.SignoutBizSeqNO
                    //SIGNOUT_CENTER_BIZCYCLENO签退中心业务周期号 VARCHAR2  #8
                , inSign.SignoutBizCycleNO
                    //inSign_HIS_BIZSEQNO签到HIS交易流水号 VARCHAR2  #9
                , inSign.SigninHISBizSeqNO
                    //SIGNOUT_HIS_BIZSEQNO签退HIS交易流水号 VARCHAR2  #10
                , inSign.SignoutHISBizSeqNO);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message + inSign.Oper.ID;
                errSql = sql;
            }
            return exec;
        }

        /// <summary>
        /// 修改签到表
        /// </summary>
        /// <param name="inSign"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateSignfo(BaseEntityer db, HisCommon.DataEntity.Sign inSign, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql = @"UPDATE SIOPERSIGN T
                               SET T.SIGNIN_FLAG               = '{2}', --签到标志（1：签到，0：签退） SIGNIN_FLAG #2
                                   T.SIGNOUT_DATE              = TO_DATE('{3}', 'YYYY-MM-DD HH24:MI:SS'), --签退时间 SIGNOUT_DATE #3
                                   T.SIGNOUT_CENTER_BIZSEQNO   = '{4}', --签退中心业务交易流水号 SIGNOUT_CENTER_BIZSEQNO #4
                                   T.SIGNOUT_CENTER_BIZCYCLENO = '{5}', --签退中心业务周期号 SIGNOUT_CENTER_BIZCYCLENO #5
                                   T.SIGNOUT_HIS_BIZSEQNO      = '{6}' --签退HIS交易流水号 SIGNOUT_HIS_BIZSEQNO #6
                             WHERE T.OPER_CODE = '{0}' --操作员编码 OPER_CODE #0
                               AND T.SIGNIN_CENTER_BIZCYCLENO = '{1}' --中心业务周期号 SIGNIN_CENTER_BIZCYCLENO #1
                               ";
            try
            {
                sql = string.Format(sql,
                    inSign.Oper.ID,
                inSign.SigninBizCycleNO,
                inSign.IsSignIn ? "1" : "0",
                inSign.SignOutOper.ToString("yyyy-MM-dd HH:mm:ss"),
                inSign.SignoutBizSeqNO,
                inSign.SignoutBizCycleNO,
                inSign.SignoutHISBizSeqNO);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                errSql = sql;
            }
            return exec;
        }

        /// <summary>
        /// 获取签到未签退信息DataGridView
        /// </summary>
        /// <param name="opercode"></param>
        /// <param name="isSignIn"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInSignInfoDataGridView(string opercode, bool isSignIn, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT 
                             t.OPER_CODE  AS 操作员编码    --操作员编码 VARCHAR2  #0
                            ,t.SIGNIN_CENTER_BIZSEQNO  AS 中心业务交易流水号    --中心业务交易流水号 VARCHAR2  #1
                            ,t.SIGNIN_CENTER_BIZCYCLENO  AS 中心业务周期号    --中心业务周期号 VARCHAR2  #2
                            ,t.ONLINE_FLAG  AS 联机模式    --联机模式状态标志（1：联机，0：脱机） VARCHAR2  #3
                            ,t.SIGNIN_FLAG  AS 签到标志    --签到标志（1：签到，0：签退） VARCHAR2  #4
                            ,t.SIGNIN_DATE  AS 签到时间    --签到时间 DATE  #5
                            ,t.SIGNOUT_DATE  AS 签退时间    --签退时间 DATE  #6
                            ,t.SIGNOUT_CENTER_BIZSEQNO  AS 签退中心业务交易流水号    --签退中心业务交易流水号 VARCHAR2  #7
                            ,t.SIGNOUT_CENTER_BIZCYCLENO  AS 签退中心业务周期号    --签退中心业务周期号 VARCHAR2  #8
                            ,t.SIGNIN_HIS_BIZSEQNO  AS 签到HIS交易流水号    --签到HIS交易流水号 VARCHAR2  #9
                            ,t.SIGNOUT_HIS_BIZSEQNO  AS 签退HIS交易流水号    --签退HIS交易流水号 VARCHAR2  #10
                        From SIOPERSIGN t 
                        WHERE t.OPER_CODE = '{0}' 
                        AND t.SIGNIN_FLAG = '{1}'";
            Sign inSign = new Sign();
            try
            {
                sql = string.Format(sql, opercode, isSignIn ? "1" : "0");
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                #region  构建实体作废
                ////OPER_CODE 操作员编码 VARCHAR2 #0
                //inSign.Oper.ID = dt.Rows[0][0].ToString();
                ////SIGNIN_CENTER_BIZSEQNO 中心业务交易流水号 VARCHAR2 #1
                //inSign.SigninBizSeqNO = dt.Rows[0][1].ToString();
                ////SIGNIN_CENTER_BIZCYCLENO 中心业务周期号 VARCHAR2 #2
                //inSign.SigninBizCycleNO = dt.Rows[0][2].ToString();
                ////ONLINE_FLAG 联机模式状态标志（1：联机，0：脱机） VARCHAR2 #3
                //inSign.IsOnline = dt.Rows[0][3].ToString() == "1" ? true : false;
                ////SIGNIN_FLAG 签到标志（1：签到，0：签退） VARCHAR2 #4
                //inSign.IsSignIn = dt.Rows[0][4].ToString() == "1" ? true : false;
                ////SIGNIN_DATE 签到时间 DATE #5
                //inSign.SignInOper = DateTime.Parse(dt.Rows[0][5].ToString());
                ////SIGNOUT_DATE 签退时间 DATE #6
                //inSign.SignOutOper = DateTime.Parse(dt.Rows[0][6].ToString());
                ////SIGNOUT_CENTER_BIZSEQNO 签退中心业务交易流水号 VARCHAR2 #7
                //inSign.SignoutBizSeqNO = dt.Rows[0][7].ToString();
                ////SIGNOUT_CENTER_BIZCYCLENO 签退中心业务周期号 VARCHAR2 #8
                //inSign.SignoutBizCycleNO = dt.Rows[0][8].ToString();
                ////SIGNIN_HIS_BIZSEQNO 签到HIS交易流水号 VARCHAR2 #9
                //inSign.SigninHISBizSeqNO = dt.Rows[0][9].ToString();
                ////SIGNOUT_HIS_BIZSEQNO 签退HIS交易流水号 VARCHAR2 #10
                //inSign.SignoutHISBizSeqNO = dt.Rows[0][10].ToString();
                #endregion
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 获取签到未签退信息
        /// </summary>
        /// <param name="opercode"></param>
        /// <param name="isSignIn"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public Sign GetInSignInfo(BaseEntityer db, string opercode, bool isSignIn, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT 
                             t.OPER_CODE  AS 操作员编码    --操作员编码 VARCHAR2  #0
                            ,t.SIGNIN_CENTER_BIZSEQNO  AS 中心业务交易流水号    --中心业务交易流水号 VARCHAR2  #1
                            ,t.SIGNIN_CENTER_BIZCYCLENO  AS 中心业务周期号    --中心业务周期号 VARCHAR2  #2
                            ,t.ONLINE_FLAG  AS 联机模式    --联机模式状态标志（1：联机，0：脱机） VARCHAR2  #3
                            ,t.SIGNIN_FLAG  AS 签到标志    --签到标志（1：签到，0：签退） VARCHAR2  #4
                            ,t.SIGNIN_DATE  AS 签到时间    --签到时间 DATE  #5
                            ,t.SIGNOUT_DATE  AS 签退时间    --签退时间 DATE  #6
                            ,t.SIGNOUT_CENTER_BIZSEQNO  AS 签退中心业务交易流水号    --签退中心业务交易流水号 VARCHAR2  #7
                            ,t.SIGNOUT_CENTER_BIZCYCLENO  AS 签退中心业务周期号    --签退中心业务周期号 VARCHAR2  #8
                            ,t.SIGNIN_HIS_BIZSEQNO  AS 签到HIS交易流水号    --签到HIS交易流水号 VARCHAR2  #9
                            ,t.SIGNOUT_HIS_BIZSEQNO  AS 签退HIS交易流水号    --签退HIS交易流水号 VARCHAR2  #10
                        From SIOPERSIGN t 
                        WHERE t.OPER_CODE = '{0}' 
                        AND t.SIGNIN_FLAG = '{1}'";
            Sign inSign = new Sign();
            try
            {
                sql = string.Format(sql, opercode, isSignIn ? "1" : "0");
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                #region  构建实体
                //OPER_CODE 操作员编码 VARCHAR2 #0
                inSign.Oper.ID = dt.Rows[0][0].ToString();
                //SIGNIN_CENTER_BIZSEQNO 中心业务交易流水号 VARCHAR2 #1
                inSign.SigninBizSeqNO = dt.Rows[0][1].ToString();
                //SIGNIN_CENTER_BIZCYCLENO 中心业务周期号 VARCHAR2 #2
                inSign.SigninBizCycleNO = dt.Rows[0][2].ToString();
                //ONLINE_FLAG 联机模式状态标志（1：联机，0：脱机） VARCHAR2 #3
                inSign.IsOnline = dt.Rows[0][3].ToString() == "1" ? true : false;
                //SIGNIN_FLAG 签到标志（1：签到，0：签退） VARCHAR2 #4
                inSign.IsSignIn = dt.Rows[0][4].ToString() == "1" ? true : false;
                //SIGNIN_DATE 签到时间 DATE #5
                inSign.SignInOper = DateTime.Parse(dt.Rows[0][5].ToString());
                //SIGNOUT_DATE 签退时间 DATE #6
                inSign.SignOutOper = DateTime.Parse(dt.Rows[0][6].ToString());
                //SIGNOUT_CENTER_BIZSEQNO 签退中心业务交易流水号 VARCHAR2 #7
                inSign.SignoutBizSeqNO = dt.Rows[0][7].ToString();
                //SIGNOUT_CENTER_BIZCYCLENO 签退中心业务周期号 VARCHAR2 #8
                inSign.SignoutBizCycleNO = dt.Rows[0][8].ToString();
                //SIGNIN_HIS_BIZSEQNO 签到HIS交易流水号 VARCHAR2 #9
                inSign.SigninHISBizSeqNO = dt.Rows[0][9].ToString();
                //SIGNOUT_HIS_BIZSEQNO 签退HIS交易流水号 VARCHAR2 #10
                inSign.SignoutHISBizSeqNO = dt.Rows[0][10].ToString();
                #endregion
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return inSign;
        }

        /// <summary>
        /// 获得医保签到的费用信息
        /// </summary>
        /// <param name="opercode"></param>
        /// <param name="centerBizCycleNO"></param>
        /// <param name="typeCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSignAccountFeeInfoForShenYang(string opercode, string centerBizCycleNO, string typeCode, ref string errMsg)
        {
            DataTable dt = new DataTable();
            // 增加了大病基金支付 by dong_w 2014年10月16日
            string sql = @"SELECT T.OPER_NO AS 操作员,
                               SUM(T.TOT_COST) AS 医疗费总额,
                               SUM(T.OWN_COST) AS 现金支付合计,
                               SUM(T.PAY_COST) AS 账户支付合计,
                               SUM(T.PUB_COST) AS 统筹基金支付合计,
                               SUM(T.HELP_OWN_COST) AS 救助基金支付合计,
                               SUM(T.OWN_SUPPLE_COST) AS 个人补充基金支付合计,
                               SUM(T.HELP_ALLOWANCES_COST) AS 低保救助基金支付合计,
                               SUM(T.ENTERPRISE_SUPPLE_COST) AS　企业补充基金支付合计,
                               SUM(t.over_cost) as 大病基金支付合计
                          FROM SIINFO T
                         WHERE T.OPER_NO = '{0}' --操作员
                           AND T.center_bussinessseqno = '{1}' --业务周期号
                           AND (T.TYPE_CODE = '{2}' OR 'ALL' = '{2}') --业务类别
                           AND T.BALANCE_STATE = '1'
                         GROUP BY T.OPER_NO";
            sql = string.Format(sql, opercode, centerBizCycleNO, typeCode);
            dt = BaseEntityer.Db.GetDataTable(sql);
            return dt;
        }

        /// <summary>
        /// 获取签退对账总额
        /// </summary>
        /// <param name="opercode"></param>
        /// <param name="centerBizCycleNO"></param>
        /// <param name="typeCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSignAccountFeeInfo(string opercode, string centerBizCycleNO, string typeCode, ref string errMsg)
        {
            DataTable dt = new DataTable();
            // 增加了大病基金支付 by dong_w 2014年10月16日
            string sql = @"SELECT T.OPER_NO AS 操作员,
                               SUM(T.TOT_COST) AS 医疗费总额,
                               SUM(T.OWN_COST) AS 现金支付合计,
                               SUM(T.PAY_COST) AS 账户支付合计,
                               SUM(T.PUB_COST) AS 统筹基金支付合计,
                               SUM(T.HELP_OWN_COST) AS 救助基金支付合计,
                               SUM(T.OWN_SUPPLE_COST) AS 个人补充基金支付合计,
                               SUM(T.HELP_ALLOWANCES_COST) AS 低保救助基金支付合计,
                               SUM(T.ENTERPRISE_SUPPLE_COST) AS　企业补充基金支付合计,
                               SUM(t.over_cost) as 大病基金支付合计
                          FROM SIINFO T
                         WHERE T.OPER_NO = '{0}' --操作员
                           AND T.center_bussinessseqno = '{1}' --业务周期号
                           AND (T.TYPE_CODE = '{2}' OR 'ALL' = '{2}') --业务类别
                           AND T.BALANCE_STATE = '1'
                         GROUP BY T.OPER_NO";
            SIInfo accountSign = new SIInfo();
            try
            {
                sql = string.Format(sql, opercode, centerBizCycleNO, typeCode);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                #region  构建实体
                //OPER_CODE 操作员编码 VARCHAR2 #0
                accountSign.OPER_NO = dt.Rows[0][0].ToString();
                //TotCost 总额 NUMBER #1
                accountSign.TOT_COST = decimal.Parse(dt.Rows[0][1].ToString());
                //PAY_COST 账户支付总额 NUMBER #2
                accountSign.PAY_COST = decimal.Parse(dt.Rows[0][2].ToString());
                //OWN_COST 自费总额 NUMBER #3
                accountSign.OWN_COST = decimal.Parse(dt.Rows[0][3].ToString());
                //PUB_COST 统筹总额 NUMBER #4
                accountSign.PUB_COST = decimal.Parse(dt.Rows[0][4].ToString());
                //HELP_OWN_COST 救助金支付 NUMBER #5
                accountSign.HELP_OWN_COST = decimal.Parse(dt.Rows[0][5].ToString());
                //OWN_SUPPLE_COST 个人补充支付 NUMBER #6
                accountSign.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][6].ToString());
                //OfficalCost 低保救助支付 NUMBER #7
                accountSign.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][7].ToString());
                //OfficalCost 企业补充支付 NUMBER #8
                accountSign.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][8].ToString());
                dt.Clear();
                DataColumn dc1 = dt.Columns.Add();
                dc1.ColumnName = "HIS本地名称";
                dc1.MaxLength = 60;
                DataColumn dc2 = dt.Columns.Add();
                dc2.ColumnName = "HIS本地金额";
                dc2.MaxLength = 80;
                DataRow dr = dt.NewRow();
                dr[0] = "总费用";
                dr[1] = accountSign.TOT_COST;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "自费总额";
                dr[1] = accountSign.OWN_COST;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "账户支付总额";
                dr[1] = accountSign.PAY_COST;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "统筹总额";
                dr[1] = accountSign.PUB_COST;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "救助金支付";
                dr[1] = accountSign.HELP_OWN_COST;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "个人补充支付";
                dr[1] = accountSign.OWN_SUPPLE_COST;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "低保救助支付";
                dr[1] = accountSign.HELP_ALLOWANCES_COST;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "企业补充支付";
                dr[1] = accountSign.HELP_OWN_COST;
                dt.Rows.Add(dr);
                #endregion
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return dt;
        }
        #endregion

        #region 目录下载
        /// <summary>
        /// 插入药品目录表
        /// </summary>
        /// <param name="drug"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSIDRUG(HisCommon.DataEntity.SI_SYDRUG drug, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"insert into SI_SYDRUG
                                  (DRUG_NO,
                                   COMMON_NAME,
                                   ENGLISH_NAME,
                                   TRADE_NAME,
                                   TRADE_INPUT,
                                   COMMON_INPUT,
                                   SIDRUG_NO,
                                   FDA_NO,
                                   FDA_STANDARDCODE,
                                   FEE_TYPE,
                                   FEE_ITEMGRADE,
                                   PRESCRIPTION_FLAG,
                                   DOSAGE,
                                   DOSAGE_UNIT,
                                   ONCE_DOSAGE,
                                   FREQUENCY,
                                   UNIT,
                                   SPECIFICATION,
                                   HOSP_PREPARATION,
                                   HOSPITAL_ID,
                                   SPECIALDRUG_FLAG,
                                   USAGE,
                                   LIMIT_DAYS,
                                   PRODUCE_UNIT,
                                   COUNTRY_MED_ACCUATE,
                                   PRODUCE_AREA,
                                   VALUATION_UNIT,
                                   DRUG_SMALL_CLASS,
                                   DRUG_BIG_CLASS,
                                   INJURE_FLAG,
                                   BIRTH_FLAG,
                                   BASEMEDICAL_FLAG,
                                   ACCOUNT_USE_FLAG,
                                   DRUGSTORE_USE_FLAG,
                                   OUT_PUB_USE_FLAG,
                                   UNIVERSALCODE_FLAG,
                                   DRUG_TYPE,
                                   DOMESTIC_OR_IMPORT,
                                   DRUG_SPECIAL_LIMIT_FLAG,
                                   DRUG_SPECIAL_LIMIT_RANGE,
                                   DRUG_ONETYPE,
                                   DRUG_TWOTYPE,
                                   DRUG_THIRDTYPE,
                                   DRUG_FOURTHTYPE,
                                   BEGIN_DATE,
                                   END_DATE,
                                   VAILD_FALG,
                                   REMARK,
                                   OPER_COER,
                                   OPER_DATE,
                                   MAX_LIMIT_PRICE,
                                   OVER_OWN_RATIO,
                                   PUB_OWN_RATIO,
                                   BIRTH_OWN_RATIO,
                                   OVERTAKE_OVER_RATIO,
                                   TRANSFER_FLAG,
                                   DRUG_COMMON_LIMIT_RANGE,
                                   DRUG_COMMON_LIMIT_FLAG,
                                   APPROVE_NUMBER,
                                   UPDATE_DATE,
                                   CHARGE_CODE)
                                values
                                  ('{0}',
                                   '{1}',
                                   '{2}',
                                   '{3}',
                                   '{4}',
                                   '{5}',
                                   '{6}',
                                   '{7}',
                                   '{8}',
                                   '{9}',
                                   '{10}',
                                   '{11}',
                                   '{12}',
                                   '{13}',
                                   '{14}',
                                   '{15}',
                                   '{16}',
                                   '{17}',
                                   '{18}',
                                   '{19}',
                                   '{20}',
                                   '{21}',
                                   '{22}',
                                   '{23}',
                                   '{24}',
                                   '{25}',
                                   '{26}',
                                   '{27}',
                                   '{28}',
                                   '{29}',
                                   '{30}',
                                   '{31}',
                                   '{32}',
                                   '{33}',
                                   '{34}',
                                   '{35}',
                                   '{36}',
                                   '{37}',
                                   '{38}',
                                   '{39}',
                                   '{40}',
                                   '{41}',
                                   '{42}',
                                   '{43}',
                                   '{44}',
                                   '{45}',
                                   '{46}',
                                   '{47}',
                                   '{48}',
                                    to_Date('{49}','yyyy-mm-dd hh24:mi:ss'),
                                   '{50}',
                                   '{51}',
                                   '{52}',
                                   '{53}',
                                   '{54}',
                                   '{55}',
                                   '{56}',
                                   '{57}',
                                   '{58}',
                                   '{59}',
                                   '{60}')";
                #endregion

                sql = string.Format(sql,
                #region
 drug.DRUG_NO,
                    drug.COMMON_NAME,
                    drug.ENGLISH_NAME,
                    drug.TRADE_NAME,
                    drug.TRADE_INPUT,
                    drug.COMMON_INPUT,
                    drug.SIDRUG_NO,
                    drug.FDA_NO,
                    drug.FDA_STANDARDCODE,
                    drug.FEE_TYPE,
                    drug.FEE_ITEMGRADE,
                    drug.PRESCRIPTION_FLAG,
                    drug.DOSAGE,
                    drug.DOSAGE_UNIT,
                    drug.ONCE_DOSAGE,
                    drug.FREQUENCY,
                    drug.UNIT,
                    drug.SPECIFICATION,
                    drug.HOSP_PREPARATION,
                    drug.HOSPITAL_ID,
                    drug.SPECIALDRUG_FLAG,
                    drug.USAGE,
                    drug.LIMIT_DAYS,
                    drug.PRODUCE_UNIT,
                    drug.COUNTRY_MED_ACCUATE,
                    drug.PRODUCE_AREA,
                    drug.VALUATION_UNIT,
                    drug.DRUG_SMALL_CLASS,
                    drug.DRUG_BIG_CLASS,
                    drug.INJURE_FLAG,
                    drug.BIRTH_FLAG,
                    drug.BASEMEDICAL_FLAG,
                    drug.ACCOUNT_USE_FLAG,
                    drug.DRUGSTORE_USE_FLAG,
                    drug.OUT_PUB_USE_FLAG,
                    drug.UNIVERSALCODE_FLAG,
                    drug.DRUG_TYPE,
                    drug.DOMESTIC_OR_IMPORT,
                    drug.DRUG_SPECIAL_LIMIT_FLAG,
                    drug.DRUG_SPECIAL_LIMIT_RANGE,
                    drug.DRUG_ONETYPE,
                    drug.DRUG_TWOTYPE,
                    drug.DRUG_THIRDTYPE,
                    drug.DRUG_FOURTHTYPE,
                    drug.BEGIN_DATE,
                    drug.END_DATE,
                    drug.VAILD_FALG,
                    drug.REMARK,
                    drug.OPER_COER,
                    drug.OPER_DATE,
                    drug.MAX_LIMIT_PRICE,
                    drug.OVER_OWN_RATIO,
                    drug.PUB_OWN_RATIO,
                    drug.BIRTH_OWN_RATIO,
                    drug.OVERTAKE_OVER_RATIO,
                    drug.TRANSFER_FLAG,
                    drug.DRUG_COMMON_LIMIT_RANGE,
                    drug.DRUG_COMMON_LIMIT_FLAG,
                    drug.APPROVE_NUMBER,
                    drug.UPDATE_DATE,
                    drug.CHARGE_CODE
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 删除药品目录表
        /// </summary>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int DeleteSIDRUG(BaseEntityer db, string pactcode, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"delete from SI_SYDRUG where charge_code = '{0}'";
                #endregion
                sql = string.Format(sql, pactcode);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 插入诊疗目录表
        /// </summary>
        /// <param name="undrug"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSIUNDRUG(BaseEntityer db, HisCommon.DataEntity.SI_SYUNDRUG undrug, ref string err, ref 

string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"insert into SI_SYUNDRUG
                      (ITEM_NO,
                       ITEM_NAME,
                       ITEM_SPELL,
                       ITEM_WB,
                       PRODEUCE_AREA,
                       MATERIAL_CATALOG,
                       MATERIAL_MED_NAME,
                       INJURE_FLAG,
                       BIRTH_FLAG,
                       BASEMEDICAL_FLAG,
                       ACCOUNT_USE_FLAG,
                       DRUGSTORE_USE_FLAG,
                       ALONE_ITEM_TYPE,
                       FEE_TYPE,
                       FEE_ITEMGRADE,
                       UNIT,
                       PRODUCE_FACTORY,
                       OVERTAKE_LIMITPRICE_METHOD,
                       SPECIFICATION,
                       PRICE,
                       USE_UNIT,
                       COUNTRY_CATALOG_CODE,
                       REMARK,
                       BEGIN_DATE,
                       END_DATE,
                       VAILD_FLAG,
                       OPER_CODE,
                       OPER_DATE,
                       SPECIALITEM_PERSON,
                       MAX_LIMIT_PRICE,
                       OVER_OWN_RATIO,
                       PUB_OWN_RATIO,
                       BIRTH_OWN_RATIO,
                       OVERTAKE_OVER_RATIO,
                       LIMIT_USE_RANGE,
                       MATERIAL_LIMITUSE_FLAG,
                       HOSP_BIND_FLAG,
                       BODY_PACKAGE_DISCOUNT,
                       PRODUCE_REG_NO,
                       REGNO_VALID_DATE,
                       BODY_PACKAGE_HOSPITALID,
                       BODY_PACKAGE_HOSPITALNAME,
                       PRODUCE_REG_NAME,
                       BRAND,
                       AGENCY,
                       PRICE_CONTENT,
                       PRICE_EXCEPT_CONTENT,
                       ISNEED_SITECODE,
                       UPDATE_DATE,
                       CHARGE_CODE,
                       MEDICAL_MATERIAL)
                    values
                      ('{0}',
                       '{1}',
                       '{2}',
                       '{3}',
                       '{4}',
                       '{5}',
                       '{6}',
                       '{7}',
                       '{8}',
                       '{9}',
                       '{10}',
                       '{11}',
                       '{12}',
                       '{13}',
                       '{14}',
                       '{15}',
                       '{16}',
                       '{17}',
                       '{18}',
                       '{19}',
                       '{20}',
                       '{21}',
                       '{22}',
                       '{23}',
                       '{24}',
                       '{25}',
                       '{26}',
                      to_Date('{27}','yyyy-mm-dd hh24:mi:ss'),
                       '{28}',
                       '{29}',
                       '{30}',
                       '{31}',
                       '{32}',
                       '{33}',
                       '{34}',
                       '{35}',
                       '{36}',
                       '{37}',
                       '{38}',
                       '{39}',
                       '{40}',
                       '{41}',
                       '{42}',
                       '{43}',
                       '{44}',
                       '{45}',
                       '{46}',
                       '{47}',
                       '{48}',
                       '{49}',
                       '{50}')";
                #endregion

                sql = string.Format(sql,
                #region
 undrug.ITEM_NO,
                   undrug.ITEM_NAME,
                   undrug.ITEM_SPELL,
                   undrug.ITEM_WB,
                   undrug.PRODEUCE_AREA,
                   undrug.MATERIAL_CATALOG,
                   undrug.MATERIAL_MED_NAME,
                   undrug.INJURE_FLAG,
                   undrug.BIRTH_FLAG,
                   undrug.BASEMEDICAL_FLAG,
                   undrug.ACCOUNT_USE_FLAG,
                   undrug.DRUGSTORE_USE_FLAG,
                   undrug.ALONE_ITEM_TYPE,
                   undrug.FEE_TYPE,
                   undrug.FEE_ITEMGRADE,
                   undrug.UNIT,
                   undrug.PRODUCE_FACTORY,
                   undrug.OVERTAKE_LIMITPRICE_METHOD,
                   undrug.SPECIFICATION,
                   undrug.PRICE,
                   undrug.USE_UNIT,
                   undrug.COUNTRY_CATALOG_CODE,
                   undrug.REMARK,
                   undrug.BEGIN_DATE,
                   undrug.END_DATE,
                   undrug.VAILD_FLAG,
                   undrug.OPER_CODE,
                   undrug.OPER_DATE,
                   undrug.SPECIALITEM_PERSON,
                   undrug.MAX_LIMIT_PRICE,
                   undrug.OVER_OWN_RATIO,
                   undrug.PUB_OWN_RATIO,
                   undrug.BIRTH_OWN_RATIO,
                   undrug.OVERTAKE_OVER_RATIO,
                   undrug.LIMIT_USE_RANGE,
                   undrug.MATERIAL_LIMITUSE_FLAG,
                   undrug.HOSP_BIND_FLAG,
                   undrug.BODY_PACKAGE_DISCOUNT,
                   undrug.PRODUCE_REG_NO,
                   undrug.REGNO_VALID_DATE,
                   undrug.BODY_PACKAGE_HOSPITALID,
                   undrug.BODY_PACKAGE_HOSPITALNAME,
                   undrug.PRODUCE_REG_NAME,
                   undrug.BRAND,
                   undrug.AGENCY,
                   undrug.PRICE_CONTENT,
                   undrug.PRICE_EXCEPT_CONTENT,
                   undrug.ISNEED_SITECODE,
                   undrug.UPDATE_DATE,
                   undrug.CHARGE_CODE,
                   undrug.MEDICAL_MATERIAL
                #endregion
);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 删除药品目录表
        /// </summary>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int DeleteSIUNDRUG(BaseEntityer db, string pactcode, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"delete from SI_SYUNDRUG where charge_code = '{0}'";
                #endregion
                sql = string.Format(sql, pactcode);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 插入服务设施目录表
        /// </summary>
        /// <param name="server"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSIServer(HisCommon.DataEntity.SI_SYSERVER server, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"insert into SI_SYSERVER
                    (server_code,
server_name, 
server_spell, 
server_wb, 
injuer_flag, 
birth_flag,
basemedical_flag,
out_pub_use_flag,
account_use_flag, 
fee_type, 
fee_itemgrade, 
bed_grade, 
pay_standard,
valid_flag, 
begin_date,
end_date, 
oper_code, 
oper_date, 
max_limit_price, 
pub_own_ratio, 
over_own_ratio, 
birth_own_ratio, 
overtake_over_ratio,
limit_use_range, 
update_date,
charge_code)
 
                    values
                      ('{0}',
                       '{1}',
                       '{2}',
                       '{3}',
                       '{4}',
                       '{5}',
                       '{6}',
                       '{7}',
                       '{8}',
                       '{9}',
                       '{10}',
                       '{11}',
                       '{12}',
                       '{13}',
                       '{14}',
                       '{15}',
                       '{16}',
                       '{17}',
                       '{18}',
                       '{19}',
                       '{20}',
                       '{21}',
                       '{22}',
                       '{23}',
                       '{24}',
                       '{25}')";
                #endregion

                sql = string.Format(sql,
                #region
 server.SERVER_CODE,
                   server.SERVER_NAME,
                   server.SERVER_SPELL,
                   server.SERVER_WB,
                   server.INJUER_FLAG,
                   server.BIRTH_FLAG,
                   server.BASEMEDICAL_FLAG,
                   server.OUT_PUB_USE_FLAG,
                   server.ACCOUNT_USE_FLAG,
                   server.FEE_TYPE,
                   server.FEE_ITEMGRADE,
                   server.BED_GRADE,
                   server.PAY_STANDARD,
                   server.VALID_FLAG,
                   server.BEGIN_DATE,
                   server.END_DATE,
                   server.OPER_CODE,
                   server.OPER_DATE,
                   server.MAX_LIMIT_PRICE,
                   server.PUB_OWN_RATIO,
                   server.OVER_OWN_RATIO,
                   server.BIRTH_OWN_RATIO,
                   server.OVERTAKE_OVER_RATIO,
                   server.LIMIT_USE_RANGE,
                   server.UPDATE_DATE,
                   server.CHARGE_CODE,
                   server.REMARK,
                   server.PRICE_CONTENT,
                   server.PRICE_EXCEPT_CONTENT,
                   server.DESCRIBE,
                   server.UNIT,
                   server.PRICE_CATEGORIES
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 插入病种目录表
        /// </summary>
        /// <param name="diag"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSIDiagnose(HisCommon.DataEntity.SI_SYDIAGNOSE diag, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"insert into SI_SYDIAGNOSE
                      (DIAGNOSE_CODE,
                       DIAGNOSE_NAME,
                       DIAGNOSE_IDENTIFICATION,
                       BEGIN_DATE,
                       END_DATE,
                       DIAGNOSE_SPELL,
                       DIAGNOSE_WB,
                       VALID_FLAG,
                       INJURE_FLAG,
                       BIRTH_FLAG,
                       DIAGNOSE_KIND,
                       DIAGNOSE_TYPE,
                       SPECIAL_TYPE,
                       ISUSE_TREATMENT_PLAN,
                       RESIDENT_SPECIAL_RANGE,
                       RANGE,
                       DEPART_TYPE,
                       SPOUSE_QUOTA,
                       OPER_CODE,
                       OPER_DATE,
                       REMARK,
                       CHARGE_TYPE_CODE)
                    values
                      ('{0}',
                       '{1}',
                       '{2}',
                       '{3}',
                       '{4}',
                       '{5}',
                       '{6}',
                       '{7}',
                       '{8}',
                       '{9}',
                       '{10}',
                       '{11}',
                       '{12}',
                       '{13}',
                       '{14}',
                       '{15}',
                       '{16}',
                       '{17}',
                       '{18}',
                       '{19}',
                       '{20}','{21}')";
                #endregion

                sql = string.Format(sql,
                #region
 diag.DIAGNOSE_CODE,
                   diag.DIAGNOSE_NAME,
                   diag.DIAGNOSE_IDENTIFICATION,
                   diag.BEGIN_DATE,
                   diag.END_DATE,
                   diag.DIAGNOSE_SPELL,
                   diag.DIAGNOSE_WB,
                   diag.VALID_FLAG,
                   diag.INJURE_FLAG,
                   diag.BIRTH_FLAG,
                   diag.DIAGNOSE_KIND,
                   diag.DIAGNOSE_TYPE,
                   diag.SPECIAL_TYPE,
                   diag.ISUSE_TREATMENT_PLAN,
                   diag.RESIDENT_SPECIAL_RANGE,
                   diag.RANGE,
                   diag.DEPART_TYPE,
                   diag.SPOUSE_QUOTA,
                   diag.OPER_CODE,
                   diag.OPER_DATE,
                   diag.REMARK,
                   diag.CHARGE_TYPE_CODE
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 删除病种目录表
        /// </summary>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int DeleteSIDiagnose(BaseEntityer db, string pactcode, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"delete from Si_Sydiagnose where charge_type_code = '{0}'";
                #endregion
                sql = string.Format(sql, pactcode);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 删除病种目录表
        /// </summary>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int DeleteSIDiagnoseByFlag(BaseEntityer db, string pactcode, string flag, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql = @" delete from Si_Sydiagnose t
                         where t.charge_type_code = '{0}'
                           and t.special_type = '{1}' ";

                sql = string.Format(sql, pactcode, flag);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 插入手术目录表
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSIOperation(HisCommon.DataEntity.SI_SYOPERATION operation, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"insert into SI_SYOPERATION
                  (OPERATION_CODE,
                   OPERATION_NAME,
                   OPERATION_SPELL,
                   OPERATION_WB,
                   OPERATION_BODY,
                   OPERATION_LIMITPRICE,
                   BEGIN_DATE,
                   END_DATE,
                   OPER_CODE,
                   OPER_DATE)
                values
                  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')";
                #endregion

                sql = string.Format(sql,
                #region
 operation.OPERATION_CODE,
                   operation.OPERATION_NAME,
                   operation.OPERATION_SPELL,
                   operation.OPERATION_WB,
                   operation.OPERATION_BODY,
                   operation.OPERATION_LIMITPRICE,
                   operation.BEGIN_DATE,
                   operation.END_DATE,
                   operation.OPER_CODE,
                   operation.OPER_DATE
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 删除手术目录表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int DeleteSIOperation(BaseEntityer db, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"delete from Si_Syoperation";
                #endregion
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 插入病种识别表
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSIIdentify(HisCommon.DataEntity.SI_SYDIAGNOSEIDENTIFICATION identify, ref string err, ref 

string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"insert into SI_SYDIAGNOSEIDENTIFICATION
                  (IDENTIFICATION_CODE,
                   IDENTIFICATION_NAME,
                   IDENTIFICATION_SPELL,
                   IDENTIFICATION_WB,
                   DIAGNOSE_KIND,
                   DIAGNOSE_TYPE,
                   SPECIAL_TYPE,
                   RANGE,
                   DEPART_TYPE,
                   RESIDENT_SPECIAL_RANGE,
                   BEGIN_DATE,
                   END_DATE,
                   OPER_CODE,
                   OPER_DATE,
                   DIAGNOSE_KINDS)
                values
                  ('{0}',
                   '{1}',
                   '{2}',
                   '{3}',
                   '{4}',
                   '{5}',
                   '{6}',
                   '{7}',
                   '{8}',
                   '{9}',
                   '{10}',
                   '{11}',
                   '{12}',
                   '{13}',
                   '{14}')";
                #endregion

                sql = string.Format(sql,
                #region
 identify.IDENTIFICATION_CODE,
                   identify.IDENTIFICATION_NAME,
                   identify.IDENTIFICATION_SPELL,
                   identify.IDENTIFICATION_WB,
                   identify.DIAGNOSE_KIND,
                   identify.DIAGNOSE_TYPE,
                   identify.SPECIAL_TYPE,
                   identify.RANGE,
                   identify.DEPART_TYPE,
                   identify.RESIDENT_SPECIAL_RANGE,
                   identify.BEGIN_DATE,
                   identify.END_DATE,
                   identify.OPER_CODE,
                   identify.OPER_DATE,
                   identify.DIAGNOSE_KINDS
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 删除病种识别表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int DeleteSIIdentify(BaseEntityer db, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"delete from Si_Sydiagnoseidentification";
                #endregion
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 插入体检目录表
        /// </summary>
        /// <param name="health"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSIHealth(HisCommon.DataEntity.SI_SYHEALTHEXAMINATION health, ref string err, ref string

sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"insert into SI_SYHEALTHEXAMINATION
                  (HEALTHEXAMINATION_CODE,
                   HEALTHEXAMINATION_NAME,
                   HEALTHEXAMINATION_TYPE,
                   HEALTHEXAMINATION_ITEMNO,
                   HEALTHEXAMINATION_ITEMNAME,
                   OPER_CODE,
                   OPER_DATE,
                   HOSPITAL_ID)
                values
                  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')";
                #endregion

                sql = string.Format(sql,
                #region
 health.HEALTHEXAMINATION_CODE,
                   health.HEALTHEXAMINATION_NAME,
                   health.HEALTHEXAMINATION_TYPE,
                   health.HEALTHEXAMINATION_ITEMNO,
                   health.HEALTHEXAMINATION_ITEMNAME,
                   health.OPER_CODE,
                   health.OPER_DATE,
                   health.HOSPITAL_ID
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 插入部位码目录表
        /// </summary>
        /// <param name="body"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSIBody(HisCommon.DataEntity.SI_SYBODY body, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"insert into SI_SYBODY
                  (BODY_CODE,
                   BODY_NAME,
                   ITEM_CODE,
                   ITEM_NAME,
                   BEGIN_DATE,
                   END_DATE,
                   PUB_OWN_RATIO,
                   OVER_OWN_RATIO,
                   PAY_STANDARD,
                   OPER_CODE,
                   OPER_DATE)
                values
                  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')";
                #endregion

                sql = string.Format(sql,
                #region
 body.BODY_CODE,
                   body.BODY_NAME,
                   body.ITEM_CODE,
                   body.ITEM_NAME,
                   body.BEGIN_DATE,
                   body.END_DATE,
                   body.PUB_OWN_RATIO,
                   body.OVER_OWN_RATIO,
                   body.PAY_STANDARD,
                   body.OPER_CODE,
                   body.OPER_DATE
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 删除病种识别表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int DeleteSIBody(BaseEntityer db, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"delete from Si_Sybody";
                #endregion
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }
        #endregion

        #region 获取所有病种明细
        /// <summary>
        /// 获取所有病种明细
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<BringSpringObject> GetDiagnoseInfo1(ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            string sql = @"select DIAGNOSE_CODE as 病种编码,
                           DIAGNOSE_NAME as 病种名称
                      from SI_SYDIAGNOSE a";
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = dt.Rows[i][0].ToString();
                    obj.Name = dt.Rows[i][1].ToString();
                    objlist.Add(obj);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return objlist;
        }

        /// <summary>
        /// 获取所有病种明细
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<BringSpringObject> GetDiagnoseInfo2(string range, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            string sql = @"select a.DIAGNOSE_CODE as 病种编码,
                           a.DIAGNOSE_NAME as 病种名称
                      from SI_SYDIAGNOSE a
                        where a.RANGE = '{0}'";
            sql = string.Format(sql, range);
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = dt.Rows[i][0].ToString();
                    obj.Name = dt.Rows[i][1].ToString();
                    objlist.Add(obj);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return objlist;
        }

        /// <summary>
        /// 获取所有病种明细
        /// </summary>
        /// <param name="range"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<BringSpringObject> GetDiagnoseInfo3(string range, string type)
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            string sql = @"select DIAGNOSE_CODE as 病种编码,
                           DIAGNOSE_NAME as 病种名称
                      from SI_SYDIAGNOSE a
                    where a.RANGE = '{0}' and a.diagnose_type in ({1})";
            sql = string.Format(sql, range, type);
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = dt.Rows[i][0].ToString();
                    obj.Name = dt.Rows[i][1].ToString();
                    objlist.Add(obj);
                }
            }
            catch
            {
                //errMsg = e.Message;
                return null;
            }
            return objlist;
        }

        /// <summary>
        /// 获取所有病种明细
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetDiagnoseInfo1byDataTable(ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            string sql = @"select DIAGNOSE_CODE      as 病种编码,
                               DIAGNOSE_NAME      as 病种名称,
                               a.charge_type_code
                          from SI_SYDIAGNOSE a";
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
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

        /// <summary>
        /// 获取所有病种明细
        /// </summary>
        /// <param name="range"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetDiagnoseInfo3byDataTable(string range, string type, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select DIAGNOSE_CODE as 病种编码,
                           DIAGNOSE_NAME as 病种名称
                      from SI_SYDIAGNOSE a
                    where a.RANGE = '{0}' and a.diagnose_type in ({1})";
            sql = string.Format(sql, range, type);
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
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

        /// <summary>
        /// 获取所有病种明细
        /// </summary>
        /// <param name="range"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetDiagnoseInfo2byDataTable(string range, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            string sql = @"select a.DIAGNOSE_CODE as 病种编码,
                           a.DIAGNOSE_NAME as 病种名称
                      from SI_SYDIAGNOSE a
                        where a.RANGE in ({0})";
            sql = string.Format(sql, range);
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    errMsg = "没有查询到疾病信息";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 获取所有病种明细
        /// </summary>
        /// <param name="charge_type_code"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetDiagnosebyDataTable(string charge_type_code, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            string sql = @"select v.DIAGNOSE_CODE as 病种编码, v.DIAGNOSE_NAME as 病种名称
                              from si_sydiagnose v
                             where v.charge_type_code = '{0}'";
            sql = string.Format(sql, charge_type_code);
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    errMsg = "没有查询到疾病信息";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 获取所有病种明细
        /// </summary>
        /// <param name="charge_type_code"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetDiagnosebyDataTable(string charge_type_code, string isSpecial, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            string sql = string.Empty;

            sql = @" select v.DIAGNOSE_CODE as 病种编码, v.DIAGNOSE_NAME as 病种名称
                              from si_sydiagnose v
                             where v.charge_type_code = '{0}' and v.special_type = '{1}' ";

            sql = string.Format(sql, charge_type_code, isSpecial);
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    errMsg = "没有查询到疾病信息";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 根据就诊序号贺就诊日期查询患者病历
        /// </summary>
        /// <param name="visitNO">就诊序号</param>
        /// <param name="visitDate">就诊日期</param>
        /// <returns></returns>
        public BringSpringObject GetDiagnoseInfo4(string visitNO, DateTime visitDate, ref string errMsg)
        {
            BringSpringObject obj = new BringSpringObject();
            string sql = @"SELECT  OUTP_MR.PATIENT_ID ,
           OUTP_MR.ILLNESS_DESC ,
           OUTP_MR.ANAMNESIS ,
           OUTP_MR.FAMILY_ILL ,
           OUTP_MR.MARRITAL ,
           OUTP_MR.INDIVIDUDL ,
           OUTP_MR.MENSES ,
           OUTP_MR.MED_HISTORY ,
           OUTP_MR.BODY_EXAM ,
           OUTP_MR.DIAG_DESC ,
            OUTP_MR.DIAG_CODE ,
           OUTP_MR.ADVICE ,
           OUTP_MR.VISIT_DATE ,
           OUTP_MR.VISIT_NO ,
           OUTP_MR.DOCTOR,
            OUTP_MR.diagnose_identification
        FROM OUTP_MR    
        WHERE       ( ( OUTP_MR.VISIT_NO = {0} ) )  and ( OUTP_MR.VISIT_DATE = to_date('{1}','yyyy-MM-dd hh24:mi:ss') ) ";
            try
            {
                sql = string.Format(sql, new object[] { visitNO, visitDate });
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                    return null;
                var mr = DataSetToEntity.DataSetToT<OUTP_MR>(ds).First();
                obj.ID = mr.DIAG_CODE;
                obj.Name = mr.DIAG_DESC;
                obj.Memo = mr.DIAGNOSE_IDENTIFICATION;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return obj;
        }

        /// <summary>
        /// 根据医保类型
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<BringSpringObject> GetDiagnoseInfoByPactCode(string code, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            string sql = @"select DIAGNOSE_CODE as 病种编码,
                           DIAGNOSE_NAME as 病种名称
                      from SI_SYDIAGNOSE a where CHARGE_TYPE_CODE='" + code + "'";
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = dt.Rows[i][0].ToString();
                    obj.Name = dt.Rows[i][1].ToString();
                    objlist.Add(obj);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return objlist;
        }
        #endregion

        public int UpdateOrderWhenPudBeyong(BaseEntityer db, string patientId, string visitNo, string stopDate)
        {
            int revInt = 0;
            try
            {
                string sql = @"update orders set stop_date_time = to_date('{2}','yyyy-MM-dd hh24:mi:ss'),order_status = '4' where patient_id = '{0}' and  visit_id = '{1}'";
                sql = Utility.SqlFormate(sql, patientId, visitNo, stopDate);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                revInt = -1;
            }
            return revInt;
        }

        public CHARGE_TYPE_DICT GetChargeTypeDictCost(string chargeType)
        {
            DataTable dt = new DataTable();
            CHARGE_TYPE_DICT obj = new CHARGE_TYPE_DICT();
            string sql = @"select pubup,pubup from charge_type_dict where charge_type_code = '{0}'";
            sql = string.Format(sql, chargeType);
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                obj.PUB_UP = Int32.Parse(dt.Rows[0][0].ToString());
                obj.PUB_DOWN = Int32.Parse(dt.Rows[0][1].ToString());
            }
            catch
            {
                return obj;
            }
            return obj;
        }

        /// <summary>
        /// 读取科室医生
        /// </summary>
        /// <param name="deptNo"></param>
        /// <returns></returns>
        public List<BringSpringObject> GetDoctorInfoByDeptNo(string deptNo)
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            //string sql = @"select user_id,user_name from users_staff_dict where user_dept='{0}'";

            string sql = @"select user_id,user_name from users_staff_dict 
             where user_id in (select USERID from users_group_dict where group_dept='{0}')";

            sql = string.Format(sql, deptNo);
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = dt.Rows[i][0].ToString();
                    obj.Name = dt.Rows[i][1].ToString();
                    objlist.Add(obj);
                }
            }
            catch
            {
                return null;
            }
            return objlist;
        }

        #region 插入医保主表
        /// <summary>
        /// 插入医保主表
        /// </summary>
        /// <param name="drug"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSI_INFO(BaseEntityer db, HisCommon.DataEntity.SIInfo info, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql =
            #region
 @"insert into SI_INFO
                  (INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO)
                values
                  ('{0}',
                   '{1}',
                   '{2}',
                   '{3}',
                   '{4}',
                   to_date('{5}','yyyy-MM-dd hh24:mi:ss'),
                   '{6}',
                   '{7}',
                   '{8}',
                   '{9}',
                   '{10}',
                   '{11}',
                   '{12}',
                   '{13}',
                   '{14}',
                   '{15}',
                   '{16}',
                   '{17}',
                   '{18}',
                   '{19}',
                   to_date('{20}','yyyy-MM-dd hh24:mi:ss'),
                   '{21}',
                   '{22}',
                   '{23}',
                   '{24}',
                   '{25}',
                   '{26}',
                   '{27}',
                   '{28}',
                   '{29}',
                   '{30}',
                   '{31}',
                   '{32}',
                   '{33}',
                   '{34}',
                   '{35}',
                   '{36}',
                   '{37}',
                   '{38}',
                   '{39}',
                   '{40}',
                   '{41}',
                   '{42}',
                   '{43}',
                   '{44}',
                   to_date('{45}','yyyy-MM-dd hh24:mi:ss'),
                   '{46}',
                   '{47}',
                   to_date('{48}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{49}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{50}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{51}','yyyy-MM-dd hh24:mi:ss'),
                   '{52}',
                   '{53}',
                   '{54}',
                   to_date('{55}','yyyy-MM-dd hh24:mi:ss'),
                   '{56}',
                   '{57}',
                   '{58}',
                   '{59}',
                   '{60}',
                   '{61}',
                   '{62}',
                   '{63}',
                   '{64}',
                   '{65}',
                   '{66}',
                   '{67}',
                   '{68}',
                   '{69}',
                   '{70}',
                   '{71}',
                   '{72}',
                   '{73}',
                   '{74}',
                   '{75}',
                   '{76}',
                   '{77}',
                   '{78}',
                   '{79}',
                   '{80}',
                   '{81}',
                   '{82}',
                   '{83}',
                   '{84}',
                   '{85}',
                   '{86}',
                   '{87}',
                   '{88}',
                   '{89}',
                   '{90}',
                   '{91}',
                   '{92}',
                   '{93}',
                   '{94}',
                   '{95}',
                   '{96}',
                   '{97}',
                   '{98}',
                   '{99}',
                   '{100}',
                   to_date('{101}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{102}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{103}','yyyy-MM-dd'),
                   '{104}',
                   '{105}',
                   '{106}',
                   '{107}',
                   '{108}',
                   '{109}',
                   '{110}',
                   '{111}',
                   '{112}',
                   '{113}',
                   '{114}',
                   '{115}',
                   '{116}')
                ";
            #endregion
            try
            {
                sql = string.Format(sql,
                #region
 info.INPATIENT_ID,
                info.VISIT_ID,
                info.BALANCE_NO,
                info.NAME,
                info.SEX,
                info.BIRTHDAY,
                info.IDENNO,
                info.PACT_CODE,
                info.PACT_NAME,
                info.REGISTER_NO,
                info.HOSPITAL_NO,
                info.HOSPITAL_NAME,
                info.HOSPITAL_GRADE,
                info.IMP_NO,
                info.RUNNING_NO,
                info.INVOICE_NO,
                info.MEDICAL_TYPE,
                info.CARD_NO,
                info.MCARD_NO,
                info.PERSON_NO,
                info.SI_BEGINDATE,
                info.SI_STATE,
                info.ICCARD_NO,
                info.OVERAL_NO,
                info.FUND_NO,
                info.FUND_NAME,
                info.BUSINESSSEQUENCE,
                info.APPLYSEQUENCE,
                info.ANOTHERCITY_NO,
                info.ANOTHERCITY_NAME,
                info.CORPORATION_NO,
                info.CORPORATION_NAME,
                info.INSURANCETYPE,
                info.EMPL_TYPE,
                info.BED_NO,
                info.ISBALANCED,
                info.INDOCTOR_NO,
                info.INDOCTOR_NAME,
                info.OUTDOCTOR_NO,
                info.OUTDOCTOR_NAME,
                info.OUTREASON,
                info.PAYUSERFLAG,
                info.CLINICDIAGNOSE,
                info.INDIAGNOSE_NO,
                info.INDIAGNOSE_NAME,
                info.INDIAGNOSE_DATE,
                info.OUTDIAGNOSE_NO,
                info.OUTDIAGNOSE_NAME,
                info.OUTDIAGNOSE_DATE,
                info.INHOSPITAL_DATE,
                info.OUTHOSPITAL_DATE,
                info.BALANCE_DATE,
                info.BALANCE_STATE,
                info.OPER_NO,
                info.OPER_NAME,
                info.OPER_DATE,
                info.CLINICDEPT_NO,
                info.CLINICDEPT_NAME,
                info.CLINICDOCTOR_NO,
                info.CLINICDOCTOR_NAME,
                info.INDEPT_NO,
                info.INDEPT_NAME,
                info.OUTDEPT_NO,
                info.OUTDEPT_NAME,
                info.TOT_COST,
                info.PAY_COST,
                info.PUB_COST,
                info.OWN_COST,
                info.OFFICIAL_COST,
                info.OVER_COST,
                info.BASE_COST,
                info.OWN_SUPPLE_COST,
                info.HELP_ALLOWANCES_COST,
                info.ENTERPRISE_SUPPLE_COST,
                info.HELP_OWN_COST,
                info.YEAR_TOT_COST,
                info.YEAR_PUB_COST,
                info.YEAR_PAY_COST,
                info.YEAR_OWN_COST,
                info.YEAR_OFFICIAL_COST,
                info.YEAR_BASE_COST,
                info.YEAR_HELP_COST,
                info.YEAR_SPECIALMED_COST,
                info.INDIVIDUAL_COST,
                info.SPECIALMED_TOTCOST,
                info.SPECIALMED_PUBCOST,
                info.SPECIALMED_BASECOST,
                info.LXPUB_COST,
                info.YLOWN_COST,
                info.TCOWN_COST,
                info.DEOWN_COST,
                info.EXCEEDLIMIT_OWNCOST,
                info.SEALTOPLINE_OWNCOST,
                info.ENTERPRISEADD_COST,
                info.APPINFO_NO,
                info.APPINFO_NAME,
                info.APPINFO_MEMO,
                info.APPTYPE_NO,
                info.APPTYPE_NAME,
                info.APPTYPE_MEMO,
                info.APP_FLAG,
                info.APP_DATE,
                info.CARDVALIDDATE,
                info.SHIFTDATE.ToString("yyyy-MM-dd"),
                info.INHOSTIMES,
                info.ISVALID,
                info.PC_NO,
                info.REGINFORETURN,
                info.READCARDRETURN,
                info.BANINFORETURN,
                info.REMARK,
                info.TYPE_CODE,
                info.TRANS_TYPE,
                info.CENTER_BIZCYCLENO,
                info.HIS_BIZCYCLENO,
                info.CENTER_BUSSINESSSEQNO,
                info.HIS_BUSSINESSSEQNO
                #endregion
);
                exec = db.ExecuteNonQuery(sql);
                //errSql = sql;
            }
            catch (Exception e)
            {
                err = e.Message;
                errSql = sql;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 插入医保主表
        /// <summary>
        /// 插入医保主表
        /// </summary>
        /// <param name="drug"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSIINFO(BaseEntityer db, HisCommon.DataEntity.SIInfo info, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql =
            #region
 @"insert into SIINFO
                  (INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO,
                   INVOICE_NEW
                   
                 )
                values
                  ('{0}',
                   '{1}',
                   '{2}',
                   '{3}',
                   '{4}',
                   to_date('{5}','yyyy-MM-dd hh24:mi:ss'),
                   '{6}',
                   '{7}',
                   '{8}',
                   '{9}',
                   '{10}',
                   '{11}',
                   '{12}',
                   '{13}',
                   '{14}',
                   '{15}',
                   '{16}',
                   '{17}',
                   '{18}',
                   '{19}',
                   to_date('{20}','yyyy-MM-dd hh24:mi:ss'),
                   '{21}',
                   '{22}',
                   '{23}',
                   '{24}',
                   '{25}',
                   '{26}',
                   '{27}',
                   '{28}',
                   '{29}',
                   '{30}',
                   '{31}',
                   '{32}',
                   '{33}',
                   '{34}',
                   '{35}',
                   '{36}',
                   '{37}',
                   '{38}',
                   '{39}',
                   '{40}',
                   '{41}',
                   '{42}',
                   '{43}',
                   '{44}',
                   to_date('{45}','yyyy-MM-dd hh24:mi:ss'),
                   '{46}',
                   '{47}',
                   to_date('{48}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{49}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{50}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{51}','yyyy-MM-dd hh24:mi:ss'),
                   '{52}',
                   '{53}',
                   '{54}',
                   to_date('{55}','yyyy-MM-dd hh24:mi:ss'),
                   '{56}',
                   '{57}',
                   '{58}',
                   '{59}',
                   '{60}',
                   '{61}',
                   '{62}',
                   '{63}',
                   '{64}',
                   '{65}',
                   '{66}',
                   '{67}',
                   '{68}',
                   '{69}',
                   '{70}',
                   '{71}',
                   '{72}',
                   '{73}',
                   '{74}',
                   '{75}',
                   '{76}',
                   '{77}',
                   '{78}',
                   '{79}',
                   '{80}',
                   '{81}',
                   '{82}',
                   '{83}',
                   '{84}',
                   '{85}',
                   '{86}',
                   '{87}',
                   '{88}',
                   '{89}',
                   '{90}',
                   '{91}',
                   '{92}',
                   '{93}',
                   '{94}',
                   '{95}',
                   '{96}',
                   '{97}',
                   '{98}',
                   '{99}',
                   '{100}',
                   to_date('{101}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{102}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{103}','yyyy-MM-dd'),
                   '{104}',
                   '{105}',
                   '{106}',
                   '{107}',
                   '{108}',
                   '{109}',
                   '{110}',
                   '{111}',
                   '{112}',
                   '{113}',
                   '{114}',
                   '{115}',
                   '{116}',
                   '{117}'
                   )
                ";
            #endregion
            try
            {
                

                sql = string.Format(sql,
                #region
 info.INPATIENT_ID,
                info.VISIT_ID,
                info.BALANCE_NO,
                info.NAME,
                info.SEX,
                info.BIRTHDAY,
                info.IDENNO,
                info.PACT_CODE,
                info.PACT_NAME,
                info.REGISTER_NO,
                info.HOSPITAL_NO,
                info.HOSPITAL_NAME,
                info.HOSPITAL_GRADE,
                info.IMP_NO,
                info.RUNNING_NO,
                info.INVOICE_NO,
                info.MEDICAL_TYPE,
                info.CARD_NO,
                info.MCARD_NO,
                info.PERSON_NO,
                info.SI_BEGINDATE,
                info.SI_STATE,
                info.ICCARD_NO,
                info.OVERAL_NO,
                info.FUND_NO,
                info.FUND_NAME,
                info.BUSINESSSEQUENCE,
                info.APPLYSEQUENCE,
                info.ANOTHERCITY_NO,
                info.ANOTHERCITY_NAME,
                info.CORPORATION_NO,
                info.CORPORATION_NAME,
                info.INSURANCETYPE,
                info.EMPL_TYPE,
                info.BED_NO,
                info.ISBALANCED,
                info.INDOCTOR_NO,
                info.INDOCTOR_NAME,
                info.OUTDOCTOR_NO,
                info.OUTDOCTOR_NAME,
                info.OUTREASON,
                info.PAYUSERFLAG,
                info.CLINICDIAGNOSE,
                info.INDIAGNOSE_NO,
                info.INDIAGNOSE_NAME,
                info.INDIAGNOSE_DATE,
                info.OUTDIAGNOSE_NO,
                info.OUTDIAGNOSE_NAME,
                info.OUTDIAGNOSE_DATE,
                info.INHOSPITAL_DATE,
                info.OUTHOSPITAL_DATE,
                info.BALANCE_DATE,
                info.BALANCE_STATE,
                info.OPER_NO,
                info.OPER_NAME,
                info.OPER_DATE,
                info.CLINICDEPT_NO,
                info.CLINICDEPT_NAME,
                info.CLINICDOCTOR_NO,
                info.CLINICDOCTOR_NAME,
                info.INDEPT_NO,
                info.INDEPT_NAME,
                info.OUTDEPT_NO,
                info.OUTDEPT_NAME,
                info.TOT_COST,
                info.PAY_COST,
                info.PUB_COST,
                info.OWN_COST,
                info.OFFICIAL_COST,
                info.OVER_COST,
                info.BASE_COST,
                info.OWN_SUPPLE_COST,
                info.HELP_ALLOWANCES_COST,
                info.ENTERPRISE_SUPPLE_COST,
                info.HELP_OWN_COST,
                info.YEAR_TOT_COST,
                info.YEAR_PUB_COST,
                info.YEAR_PAY_COST,
                info.YEAR_OWN_COST,
                info.YEAR_OFFICIAL_COST,
                info.YEAR_BASE_COST,
                info.YEAR_HELP_COST,
                info.YEAR_SPECIALMED_COST,
                info.INDIVIDUAL_COST,
                info.SPECIALMED_TOTCOST,
                info.SPECIALMED_PUBCOST,
                info.SPECIALMED_BASECOST,
                info.LXPUB_COST,
                info.YLOWN_COST,
                info.TCOWN_COST,
                info.DEOWN_COST,
                info.EXCEEDLIMIT_OWNCOST,
                info.SEALTOPLINE_OWNCOST,
                info.ENTERPRISEADD_COST,
                info.APPINFO_NO,
                info.APPINFO_NAME,
                info.APPINFO_MEMO,
                info.APPTYPE_NO,
                info.APPTYPE_NAME,
                info.APPTYPE_MEMO,
                info.APP_FLAG,
                info.APP_DATE,
                info.CARDVALIDDATE,
                info.SHIFTDATE.ToString("yyyy-MM-dd"),
                info.INHOSTIMES,
                info.ISVALID,
                info.PC_NO,
                info.REGINFORETURN,
                info.READCARDRETURN,
                info.BANINFORETURN,
                info.REMARK,
                info.TYPE_CODE,
                info.TRANS_TYPE,
                info.CENTER_BIZCYCLENO,
                info.HIS_BIZCYCLENO,
                info.CENTER_BUSSINESSSEQNO,
                info.HIS_BUSSINESSSEQNO,
                info.INVOICE_NEW
                
                #endregion
);
                exec = db.ExecuteNonQuery(sql);
                //errSql = sql;
            }
            catch (Exception e)
            {
                err = e.Message;
                errSql = sql;
                return -1;
            }
            return exec;
        }

        /// <summary>
        /// 获取医保主表登记信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <returns></returns>
        public SIInfo GetSIINFOSenReg(string patientid, string visitid)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();
            string sql =
            #region
 @"select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO
              from SIINFO_SEN
             where INPATIENT_ID = '{0}'
               and VISIT_ID = '{1}'
               and BALANCE_NO = (select max(to_number(t.BALANCE_NO))
                                   from SIINFO_SEN t
                                  where t.INPATIENT_ID = '{0}'
                                    and t.VISIT_ID = '{1}'
                                    and t.ISVALID = '1')
               and BALANCE_STATE = '0'
               and TRANS_TYPE = '1'
               and ISVALID = 1";
            #endregion
            sql = string.Format(sql, patientid, visitid);
            try
            {
                #region
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                #endregion
            }
            catch
            {
                return null;
            }
            return info;
        }

        /// <summary>
        /// 插入医保主表
        /// </summary>
        /// <param name="drug"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertSIINFOSen(BaseEntityer db, HisCommon.DataEntity.SIInfo info, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql =
            #region
 @"insert into SIINFO_SEN
                  (INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO)
                values
                  ('{0}',
                   '{1}',
                   '{2}',
                   '{3}',
                   '{4}',
                   to_date('{5}','yyyy-MM-dd hh24:mi:ss'),
                   '{6}',
                   '{7}',
                   '{8}',
                   '{9}',
                   '{10}',
                   '{11}',
                   '{12}',
                   '{13}',
                   '{14}',
                   '{15}',
                   '{16}',
                   '{17}',
                   '{18}',
                   '{19}',
                   to_date('{20}','yyyy-MM-dd hh24:mi:ss'),
                   '{21}',
                   '{22}',
                   '{23}',
                   '{24}',
                   '{25}',
                   '{26}',
                   '{27}',
                   '{28}',
                   '{29}',
                   '{30}',
                   '{31}',
                   '{32}',
                   '{33}',
                   '{34}',
                   '{35}',
                   '{36}',
                   '{37}',
                   '{38}',
                   '{39}',
                   '{40}',
                   '{41}',
                   '{42}',
                   '{43}',
                   '{44}',
                   to_date('{45}','yyyy-MM-dd hh24:mi:ss'),
                   '{46}',
                   '{47}',
                   to_date('{48}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{49}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{50}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{51}','yyyy-MM-dd hh24:mi:ss'),
                   '{52}',
                   '{53}',
                   '{54}',
                   to_date('{55}','yyyy-MM-dd hh24:mi:ss'),
                   '{56}',
                   '{57}',
                   '{58}',
                   '{59}',
                   '{60}',
                   '{61}',
                   '{62}',
                   '{63}',
                   '{64}',
                   '{65}',
                   '{66}',
                   '{67}',
                   '{68}',
                   '{69}',
                   '{70}',
                   '{71}',
                   '{72}',
                   '{73}',
                   '{74}',
                   '{75}',
                   '{76}',
                   '{77}',
                   '{78}',
                   '{79}',
                   '{80}',
                   '{81}',
                   '{82}',
                   '{83}',
                   '{84}',
                   '{85}',
                   '{86}',
                   '{87}',
                   '{88}',
                   '{89}',
                   '{90}',
                   '{91}',
                   '{92}',
                   '{93}',
                   '{94}',
                   '{95}',
                   '{96}',
                   '{97}',
                   '{98}',
                   '{99}',
                   '{100}',
                   to_date('{101}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{102}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{103}','yyyy-MM-dd'),
                   '{104}',
                   '{105}',
                   '{106}',
                   '{107}',
                   '{108}',
                   '{109}',
                   '{110}',
                   '{111}',
                   '{112}',
                   '{113}',
                   '{114}',
                   '{115}',
                   '{116}')
                ";
            #endregion
            try
            {
                sql = string.Format(sql,
                #region
 info.INPATIENT_ID,
                info.VISIT_ID,
                info.BALANCE_NO,
                info.NAME,
                info.SEX,
                info.BIRTHDAY,
                info.IDENNO,
                info.PACT_CODE,
                info.PACT_NAME,
                info.REGISTER_NO,
                info.HOSPITAL_NO,
                info.HOSPITAL_NAME,
                info.HOSPITAL_GRADE,
                info.IMP_NO,
                info.RUNNING_NO,
                info.INVOICE_NO,
                info.MEDICAL_TYPE,
                info.CARD_NO,
                info.MCARD_NO,
                info.PERSON_NO,
                info.SI_BEGINDATE,
                info.SI_STATE,
                info.ICCARD_NO,
                info.OVERAL_NO,
                info.FUND_NO,
                info.FUND_NAME,
                info.BUSINESSSEQUENCE,
                info.APPLYSEQUENCE,
                info.ANOTHERCITY_NO,
                info.ANOTHERCITY_NAME,
                info.CORPORATION_NO,
                info.CORPORATION_NAME,
                info.INSURANCETYPE,
                info.EMPL_TYPE,
                info.BED_NO,
                info.ISBALANCED,
                info.INDOCTOR_NO,
                info.INDOCTOR_NAME,
                info.OUTDOCTOR_NO,
                info.OUTDOCTOR_NAME,
                info.OUTREASON,
                info.PAYUSERFLAG,
                info.CLINICDIAGNOSE,
                info.INDIAGNOSE_NO,
                info.INDIAGNOSE_NAME,
                info.INDIAGNOSE_DATE,
                info.OUTDIAGNOSE_NO,
                info.OUTDIAGNOSE_NAME,
                info.OUTDIAGNOSE_DATE,
                info.INHOSPITAL_DATE,
                info.OUTHOSPITAL_DATE,
                info.BALANCE_DATE,
                info.BALANCE_STATE,
                info.OPER_NO,
                info.OPER_NAME,
                info.OPER_DATE,
                info.CLINICDEPT_NO,
                info.CLINICDEPT_NAME,
                info.CLINICDOCTOR_NO,
                info.CLINICDOCTOR_NAME,
                info.INDEPT_NO,
                info.INDEPT_NAME,
                info.OUTDEPT_NO,
                info.OUTDEPT_NAME,
                info.TOT_COST,
                info.PAY_COST,
                info.PUB_COST,
                info.OWN_COST,
                info.OFFICIAL_COST,
                info.OVER_COST,
                info.BASE_COST,
                info.OWN_SUPPLE_COST,
                info.HELP_ALLOWANCES_COST,
                info.ENTERPRISE_SUPPLE_COST,
                info.HELP_OWN_COST,
                info.YEAR_TOT_COST,
                info.YEAR_PUB_COST,
                info.YEAR_PAY_COST,
                info.YEAR_OWN_COST,
                info.YEAR_OFFICIAL_COST,
                info.YEAR_BASE_COST,
                info.YEAR_HELP_COST,
                info.YEAR_SPECIALMED_COST,
                info.INDIVIDUAL_COST,
                info.SPECIALMED_TOTCOST,
                info.SPECIALMED_PUBCOST,
                info.SPECIALMED_BASECOST,
                info.LXPUB_COST,
                info.YLOWN_COST,
                info.TCOWN_COST,
                info.DEOWN_COST,
                info.EXCEEDLIMIT_OWNCOST,
                info.SEALTOPLINE_OWNCOST,
                info.ENTERPRISEADD_COST,
                info.APPINFO_NO,
                info.APPINFO_NAME,
                info.APPINFO_MEMO,
                info.APPTYPE_NO,
                info.APPTYPE_NAME,
                info.APPTYPE_MEMO,
                info.APP_FLAG,
                info.APP_DATE,
                info.CARDVALIDDATE,
                info.SHIFTDATE.ToString("yyyy-MM-dd"),
                info.INHOSTIMES,
                info.ISVALID,
                info.PC_NO,
                info.REGINFORETURN,
                info.READCARDRETURN,
                info.BANINFORETURN,
                info.REMARK,
                info.TYPE_CODE,
                info.TRANS_TYPE,
                info.CENTER_BIZCYCLENO,
                info.HIS_BIZCYCLENO,
                info.CENTER_BUSSINESSSEQNO,
                info.HIS_BUSSINESSSEQNO
                #endregion
);
                exec = db.ExecuteNonQuery(sql);
                //errSql = sql;
            }
            catch (Exception e)
            {
                err = e.Message;
                errSql = sql;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 修改医保主表
        /// <summary>
        /// 修改医保主表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="BANINFORETURN"></param>
        /// <param name="err"></param>
        /// <param name="errSql"></param>
        /// <returns></returns>
        public int UpdateSIINFOBalInfo(BaseEntityer db, string invoice_no, string BANINFORETURN, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql =
            #region
 @"update siinfo t
   set t.baninforeturn = '{1}'
 where t.invoice_no = '{0}'
and t.trans_type = 1";
            #endregion
            try
            {
                sql = string.Format(sql,
                invoice_no,
                BANINFORETURN
                );
                exec = db.ExecuteNonQuery(sql);
                //errSql = sql;
            }
            catch (Exception e)
            {
                err = e.Message;
                errSql = sql;
                return -1;
            }
            return exec;
        }
        /// <summary>
        /// 住院更新
        /// </summary>
        /// <param name="db"></param>
        /// <param name="invoice_no"></param>
        /// <param name="BANINFORETURN"></param>
        /// <param name="err"></param>
        /// <param name="errSql"></param>
        /// <returns></returns>
        public int UpdateSIINFOBalInfoByInp(BaseEntityer db, string invoice_no, string BANINFORETURN, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql =
            #region
 @"update siinfo t
   set t.baninforeturn = '{1}'
 where t.invoice_no = '{0}'
and t.trans_type = 1 and t.TYPE_CODE='2'";
            #endregion
            try
            {
                sql = string.Format(sql,
                invoice_no,
                BANINFORETURN
                );
                exec = db.ExecuteNonQuery(sql);
                //errSql = sql;
            }
            catch (Exception e)
            {
                err = e.Message;
                errSql = sql;
                return -1;
            }
            return exec;
        }
        /// <summary>
        /// 门诊更新
        /// </summary>
        /// <param name="db"></param>
        /// <param name="invoice_no"></param>
        /// <param name="BANINFORETURN"></param>
        /// <param name="err"></param>
        /// <param name="errSql"></param>
        /// <returns></returns>
        public int UpdateSIINFOBalInfoByOutp(BaseEntityer db, string invoice_no, string BANINFORETURN, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql =
            #region
 @"update siinfo t
   set t.baninforeturn = '{1}'
 where t.invoice_no = '{0}'
and t.trans_type = 1 and t.TYPE_CODE='1'";
            #endregion
            try
            {
                sql = string.Format(sql,
                invoice_no,
                BANINFORETURN
                );
                exec = db.ExecuteNonQuery(sql);
                //errSql = sql;
            }
            catch (Exception e)
            {
                err = e.Message;
                errSql = sql;
                return -1;
            }
            return exec;
        }
        public int UpdateSIINFORemarkInfo(BaseEntityer db, string invoice_no, string Remark, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql =
            #region
 @"update siinfo t
               set t.REMARK = '{1}'
             where t.invoice_no = '{0}'
            and t.trans_type = 1";
            #endregion
            try
            {
                sql = string.Format(sql,
                invoice_no,
                Remark
                );
                exec = db.ExecuteNonQuery(sql);
                //errSql = sql;
            }
            catch (Exception e)
            {
                err = e.Message;
                errSql = sql;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 获取医保主表登记信息
        /// <summary>
        /// 获取医保主表登记信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <returns></returns>
        public SIInfo GetSIINFOReg(string patientid, string visitid, string typecode)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();
            string sql =
            #region
 @"select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO
              from SIINFO
             where INPATIENT_ID = '{0}'
               and VISIT_ID = '{1}'
               and BALANCE_NO = (select min(to_number(t.BALANCE_NO))
                                   from SIINFO t
                                  where t.INPATIENT_ID = '{0}'
                                    and t.VISIT_ID = '{1}'
                                    and t.TYPE_CODE = '{2}'
                                    and t.ISVALID = '1')
               and BALANCE_STATE = '0'
               and TRANS_TYPE = '1'
               and ISVALID = 1
               and TYPE_CODE = '{2}'";
            #endregion
            sql = string.Format(sql, patientid, visitid, typecode);
            try
            {
                #region
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                #endregion
            }
            catch
            {
                return null;
            }
            return info;
        }

        /// <summary>
        /// 获取医保主表登记信息（测试）
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <returns></returns>
        public SIInfo GetSIINFORegByTest(BaseEntityer db, string RUNNING_NO)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();
            string sql =
            #region
 @"select REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN
              from siinfo
             where RUNNING_NO = '{0}'";
            #endregion
            sql = string.Format(sql, RUNNING_NO);
            try
            {
                #region
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                info.REGINFORETURN = dt.Rows[0][0].ToString();
                info.READCARDRETURN = dt.Rows[0][1].ToString();
                info.BANINFORETURN = dt.Rows[0][2].ToString();
                #endregion
            }
            catch
            {
                return null;
            }
            return info;
        }

        public SIInfo GetSIINFORegByMaxBalance(string patientid, string visitid, string typecode)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();
            string sql =
            #region
 @"select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   (select max( IMP_NO ) from siinfo si where si.inpatient_id=s.inpatient_id and s.visit_id=si.visit_id   and s.type_code=si.type_code)   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO
              from SIINFO s
             where INPATIENT_ID = '{0}'
               and VISIT_ID = '{1}'
 --               and BALANCE_NO = (select max(to_number(t.BALANCE_NO))
--                                  from SIINFO t
--                                  where t.INPATIENT_ID = '{0}'
--                                   and t.VISIT_ID = '{1}'
--                                    and t.TYPE_CODE = '{2}'
--                                    and t.BALANCE_STATE = '0')
               and BALANCE_STATE = '0'
               and TRANS_TYPE = '1'
               and ISVALID = 1
               and TYPE_CODE = '{2}'";
            
            #endregion
            sql = string.Format(sql, patientid, visitid, typecode);
            try
            {
                #region
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                #endregion
            }
            catch
            {
                return null;
            }
            return info;
        }
        /// <summary>
        /// 获取医保主表登记信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="typecode"></param>
        /// <param name="VisitDate"></param>
        /// <returns></returns>
        public SIInfo GetSIINFORegByVisitDate(BaseEntityer db, string patientid, string visitid, string typecode, string VisitDate, ref string errMsg, ref string errSql)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();
            string sql =
            #region
 @"select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO
              from SIINFO
             where INPATIENT_ID = '{0}'
               and VISIT_ID = '{1}'
               and BALANCE_NO = (select min(to_number(t.BALANCE_NO))
                                   from SIINFO t
                                  where t.INPATIENT_ID = '{0}'
                                    and t.VISIT_ID = '{1}'
                                    and t.TYPE_CODE = '{2}' 
                                    and t.SHIFTDATE=to_date('{3}','yyyy-MM-dd hh24:mi:ss')
                                    )
               and BALANCE_STATE = '0'
               and TRANS_TYPE = '1'
               and ISVALID = 1
               and TYPE_CODE = '{2}' 
               and SHIFTDATE=to_date('{3}','yyyy-MM-dd hh24:mi:ss')";
            #endregion
            sql = string.Format(sql, patientid, visitid, typecode, VisitDate);
            try
            {
                #region
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                #endregion
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                errSql = sql;
                return null;
            }
            return info;
        }

        public SIInfo GetSIINFORegByVisitDateByMax(BaseEntityer db, string patientid, string visitid, string typecode, string VisitDate, ref string errMsg, ref string errSql)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();
            string sql =
            #region
 @"select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO
              from SIINFO
             where INPATIENT_ID = '{0}'
               and VISIT_ID = '{1}'
               and BALANCE_NO = (select max(to_number(t.BALANCE_NO))
                                   from SIINFO t
                                  where t.INPATIENT_ID = '{0}'
                                    and t.VISIT_ID = '{1}'
                                    and t.TYPE_CODE = '{2}' and t.SHIFTDATE=to_date('{3}','yyyy-MM-dd hh24:mi:ss')
                                    and BALANCE_STATE = '0' and TRANS_TYPE = '1' and ISVALID = 1 )
               and BALANCE_STATE = '0'
               and TRANS_TYPE = '1'
               and ISVALID = 1
               and TYPE_CODE = '{2}' and SHIFTDATE=to_date('{3}','yyyy-MM-dd hh24:mi:ss')";
            #endregion
            sql = string.Format(sql, patientid, visitid, typecode, VisitDate);
            try
            {
                #region
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                #endregion
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                errSql = sql;
                return null;
            }
            return info;
        }

        #endregion

        #region 获取医保主表结算信息
        /// <summary>
        /// 获取医保主表结算信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <returns></returns>
        public SIInfo GetSIINFOBal(string patientid, string visitid, string typecode)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();
            string sql =
            #region
 @"select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO,INVOICE_NEW
              from SIINFO
             where INPATIENT_ID = '{0}'
               and VISIT_ID = '{1}'
               and BALANCE_NO = (select max(to_number(t.BALANCE_NO))
                                   from SIINFO t
                                  where t.INPATIENT_ID = '{0}'
                                    and t.VISIT_ID = '{1}'
                                    and t.BALANCE_STATE = '1'
                                    and t.TRANS_TYPE = '1'
                                    and t.ISVALID = 1
                                    and t.TYPE_CODE = '{2}')
               and BALANCE_STATE = '1'
               and TRANS_TYPE = '1'
               and ISVALID = 1
               and TYPE_CODE = '{2}'";
            #endregion
            sql = string.Format(sql, patientid, visitid, typecode);
            try
            {
                #region
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                info.INVOICE_NEW = dt.Rows[0][117].ToString();
                #endregion
            }
            catch
            {
                return null;
            }
            return info;
        }
        #endregion

        #region 获取医保主表返回结算信息
        /// <summary>
        /// 获取医保主表返回结算信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public string GetSIINFOReturnBal(string patientid, string visitid, string invoice)
        {
            DataTable dt = new DataTable();
            string ret = string.Empty;
            string sql =
            #region
 @"select f.baninforeturn from SIINFO f
                    where f.INPATIENT_ID = '{0}'
                    and f.VISIT_ID = '{1}'
                    and f.invoice_no = '{2}'";
            #endregion
            sql = string.Format(sql, patientid, visitid, invoice);
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                ret = dt.Rows[0][0].ToString();
            }
            catch
            {
                return null;
            }
            return ret;
        }
        #endregion

        #region 获取医保主表结算信息
        /// <summary>
        /// 获取医保主表结算信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <returns></returns>
        public SIInfo GetSIINFOBalByVisitDate(string patientid, string visitid, string typecode, string VisitDate)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();
            string sql =
            #region
 @"select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,                  
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO,INVOICE_NEW
              from SIINFO
             where INPATIENT_ID = '{0}'
               and VISIT_ID = '{1}'
               and BALANCE_STATE = '1'
               and TRANS_TYPE = '1'
               and ISVALID = 1
               and TYPE_CODE = '{2}'
               and INVOICE_NO = '{3}'";
            #endregion
            sql = string.Format(sql, patientid, visitid, typecode, VisitDate);
            try
            {
                #region
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                info.INVOICE_NEW = dt.Rows[0][117].ToString();
                #endregion
            }
            catch
            {
                return null;
            }
            return info;
        }
        #endregion

        #region 修改医保主表信息(门诊)
        /// <summary>
        /// 修改医保主表信息(门诊)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateSIInfoReg(BaseEntityer db, SIInfo siinfo, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql = string.Empty;
            try
            {
                sql =
                #region
 @"update siinfo set
                            NAME = '{4}',
                            SEX = '{5}',
                            BIRTHDAY = to_date('{6}','yyyy-mm-dd hh24:mi:ss'),
                            IDENNO = '{7}',
                            PACT_CODE = '{8}',
                            PACT_NAME = '{9}',
                            REGISTER_NO = '{10}',
                            HOSPITAL_NO = '{11}',
                            HOSPITAL_NAME = '{12}',
                            HOSPITAL_GRADE = '{13}',
                            IMP_NO = '{14}',
                            RUNNING_NO = '{15}',
                            INVOICE_NO = '{16}',
                            MEDICAL_TYPE = '{17}',
                            CARD_NO = '{18}',
                            MCARD_NO = '{19}',
                            PERSON_NO = '{20}',
                            SI_BEGINDATE = to_date('{21}','yyyy-mm-dd hh24:mi:ss'),
                            SI_STATE = '{22}',
                            ICCARD_NO = '{23}',
                            OVERAL_NO = '{24}',
                            FUND_NO = '{25}',
                            FUND_NAME = '{26}',
                            BUSINESSSEQUENCE = '{27}',
                            APPLYSEQUENCE = '{28}',
                            ANOTHERCITY_NO = '{29}',
                            ANOTHERCITY_NAME = '{30}',
                            CORPORATION_NO = '{31}',
                            CORPORATION_NAME = '{32}',
                            INSURANCETYPE = '{33}',
                            EMPL_TYPE = '{34}',
                            BED_NO = '{35}',
                            ISBALANCED = '{36}',
                            INDOCTOR_NO = '{37}',
                            INDOCTOR_NAME = '{38}',
                            OUTDOCTOR_NO = '{39}',
                            OUTDOCTOR_NAME = '{40}',
                            OUTREASON = '{41}',
                            PAYUSERFLAG = '{42}',
                            CLINICDIAGNOSE = '{43}',
                            INDIAGNOSE_NO = '{44}',
                            INDIAGNOSE_NAME = '{45}',
                            INDIAGNOSE_DATE = to_date('{46}','yyyy-mm-dd hh24:mi:ss'),
                            OUTDIAGNOSE_NO = '{47}',
                            OUTDIAGNOSE_NAME = '{48}',
                            OUTDIAGNOSE_DATE = to_date('{49}','yyyy-mm-dd hh24:mi:ss'),
                            INHOSPITAL_DATE = to_date('{50}','yyyy-mm-dd hh24:mi:ss'),
                            OUTHOSPITAL_DATE = to_date('{51}','yyyy-mm-dd hh24:mi:ss'),
                            BALANCE_DATE = to_date('{52}','yyyy-mm-dd hh24:mi:ss'),
                            BALANCE_STATE = '{53}',
                            OPER_NO = '{54}',
                            OPER_NAME = '{55}',
                            OPER_DATE = to_date('{56}','yyyy-mm-dd hh24:mi:ss'),
                            CLINICDEPT_NO = '{57}',
                            CLINICDEPT_NAME = '{58}',
                            CLINICDOCTOR_NO = '{59}',
                            CLINICDOCTOR_NAME = '{60}',
                            INDEPT_NO = '{61}',
                            INDEPT_NAME = '{62}',
                            OUTDEPT_NO = '{63}',
                            OUTDEPT_NAME = '{64}',
                            TOT_COST = '{65}',
                            PAY_COST = '{66}',
                            PUB_COST = '{67}',
                            OWN_COST = '{68}',
                            OFFICIAL_COST = '{69}',
                            OVER_COST = '{70}',
                            BASE_COST = '{71}',
                            OWN_SUPPLE_COST = '{72}',
                            HELP_ALLOWANCES_COST = '{73}',
                            ENTERPRISE_SUPPLE_COST = '{74}',
                            HELP_OWN_COST = '{75}',
                            YEAR_TOT_COST = '{76}',
                            YEAR_PUB_COST = '{77}',
                            YEAR_PAY_COST = '{78}',
                            YEAR_OWN_COST = '{79}',
                            YEAR_OFFICIAL_COST = '{80}',
                            YEAR_BASE_COST = '{81}',
                            YEAR_HELP_COST = '{82}',
                            YEAR_SPECIALMED_COST = '{83}',
                            INDIVIDUAL_COST = '{84}',
                            SPECIALMED_TOTCOST = '{85}',
                            SPECIALMED_PUBCOST = '{86}',
                            SPECIALMED_BASECOST = '{87}',
                            LXPUB_COST = '{88}',
                            YLOWN_COST = '{89}',
                            TCOWN_COST = '{90}',
                            DEOWN_COST = '{91}',
                            EXCEEDLIMIT_OWNCOST = '{92}',
                            SEALTOPLINE_OWNCOST = '{93}',
                            ENTERPRISEADD_COST = '{94}',
                            APPINFO_NO = '{95}',
                            APPINFO_NAME = '{96}',
                            APPINFO_MEMO = '{97}',
                            APPTYPE_NO = '{98}',
                            APPTYPE_NAME = '{99}',
                            APPTYPE_MEMO = '{100}',
                            APP_FLAG = '{101}',
                            APP_DATE = to_date('{102}','yyyy-mm-dd hh24:mi:ss'),
                            CARDVALIDDATE = to_date('{103}','yyyy-mm-dd hh24:mi:ss'),
                            INHOSTIMES = '{104}',
                            ISVALID = '{105}',
                            PC_NO = '{106}',
                            REGINFORETURN = '{107}',
                            READCARDRETURN = '{108}',
                            BANINFORETURN = '{109}',
                            REMARK = '{110}',
                            TYPE_CODE = '{111}',
                            TRANS_TYPE = '{112}',
                            CENTER_BIZCYCLENO = '{113}',
                            HIS_BIZCYCLENO = '{114}',
                            CENTER_BUSSINESSSEQNO = '{115}',
                            HIS_BUSSINESSSEQNO = '{116}'
                            where INPATIENT_ID = '{0}' 
                            and VISIT_ID = '{1}'
                            and SHIFTDATE = to_date('{2}','yyyy-MM-dd')
                            and BALANCE_NO = '{3}'";
                #endregion
                sql = string.Format(sql,
                #region
 siinfo.INPATIENT_ID,
                    siinfo.VISIT_ID,
                    siinfo.SHIFTDATE.ToString("yyyy-MM-dd"),
                    siinfo.BALANCE_NO,
                    siinfo.NAME,
                    siinfo.SEX,
                    siinfo.BIRTHDAY,
                    siinfo.IDENNO,
                    siinfo.PACT_CODE,
                    siinfo.PACT_NAME,
                    siinfo.REGISTER_NO,
                    siinfo.HOSPITAL_NO,
                    siinfo.HOSPITAL_NAME,
                    siinfo.HOSPITAL_GRADE,
                    siinfo.IMP_NO,
                    siinfo.RUNNING_NO,
                    siinfo.INVOICE_NO,
                    siinfo.MEDICAL_TYPE,
                    siinfo.CARD_NO,
                    siinfo.MCARD_NO,
                    siinfo.PERSON_NO,
                    siinfo.SI_BEGINDATE,
                    siinfo.SI_STATE,
                    siinfo.ICCARD_NO,
                    siinfo.OVERAL_NO,
                    siinfo.FUND_NO,
                    siinfo.FUND_NAME,
                    siinfo.BUSINESSSEQUENCE,
                    siinfo.APPLYSEQUENCE,
                    siinfo.ANOTHERCITY_NO,
                    siinfo.ANOTHERCITY_NAME,
                    siinfo.CORPORATION_NO,
                    siinfo.CORPORATION_NAME,
                    siinfo.INSURANCETYPE,
                    siinfo.EMPL_TYPE,
                    siinfo.BED_NO,
                    siinfo.ISBALANCED,
                    siinfo.INDOCTOR_NO,
                    siinfo.INDOCTOR_NAME,
                    siinfo.OUTDOCTOR_NO,
                    siinfo.OUTDOCTOR_NAME,
                    siinfo.OUTREASON,
                    siinfo.PAYUSERFLAG,
                    siinfo.CLINICDIAGNOSE,
                    siinfo.INDIAGNOSE_NO,
                    siinfo.INDIAGNOSE_NAME,
                    siinfo.INDIAGNOSE_DATE,
                    siinfo.OUTDIAGNOSE_NO,
                    siinfo.OUTDIAGNOSE_NAME,
                    siinfo.OUTDIAGNOSE_DATE,
                    siinfo.INHOSPITAL_DATE,
                    siinfo.OUTHOSPITAL_DATE,
                    siinfo.BALANCE_DATE,
                    siinfo.BALANCE_STATE,
                    siinfo.OPER_NO,
                    siinfo.OPER_NAME,
                    siinfo.OPER_DATE,
                    siinfo.CLINICDEPT_NO,
                    siinfo.CLINICDEPT_NAME,
                    siinfo.CLINICDOCTOR_NO,
                    siinfo.CLINICDOCTOR_NAME,
                    siinfo.INDEPT_NO,
                    siinfo.INDEPT_NAME,
                    siinfo.OUTDEPT_NO,
                    siinfo.OUTDEPT_NAME,
                    siinfo.TOT_COST,
                    siinfo.PAY_COST,
                    siinfo.PUB_COST,
                    siinfo.OWN_COST,
                    siinfo.OFFICIAL_COST,
                    siinfo.OVER_COST,
                    siinfo.BASE_COST,
                    siinfo.OWN_SUPPLE_COST,
                    siinfo.HELP_ALLOWANCES_COST,
                    siinfo.ENTERPRISE_SUPPLE_COST,
                    siinfo.HELP_OWN_COST,
                    siinfo.YEAR_TOT_COST,
                    siinfo.YEAR_PUB_COST,
                    siinfo.YEAR_PAY_COST,
                    siinfo.YEAR_OWN_COST,
                    siinfo.YEAR_OFFICIAL_COST,
                    siinfo.YEAR_BASE_COST,
                    siinfo.YEAR_HELP_COST,
                    siinfo.YEAR_SPECIALMED_COST,
                    siinfo.INDIVIDUAL_COST,
                    siinfo.SPECIALMED_TOTCOST,
                    siinfo.SPECIALMED_PUBCOST,
                    siinfo.SPECIALMED_BASECOST,
                    siinfo.LXPUB_COST,
                    siinfo.YLOWN_COST,
                    siinfo.TCOWN_COST,
                    siinfo.DEOWN_COST,
                    siinfo.EXCEEDLIMIT_OWNCOST,
                    siinfo.SEALTOPLINE_OWNCOST,
                    siinfo.ENTERPRISEADD_COST,
                    siinfo.APPINFO_NO,
                    siinfo.APPINFO_NAME,
                    siinfo.APPINFO_MEMO,
                    siinfo.APPTYPE_NO,
                    siinfo.APPTYPE_NAME,
                    siinfo.APPTYPE_MEMO,
                    siinfo.APP_FLAG,
                    siinfo.APP_DATE,
                    siinfo.CARDVALIDDATE,
                    siinfo.INHOSTIMES,
                    siinfo.ISVALID,
                    siinfo.PC_NO,
                    siinfo.REGINFORETURN,
                    siinfo.READCARDRETURN,
                    siinfo.BANINFORETURN,
                    siinfo.REMARK,
                    siinfo.TYPE_CODE,
                    siinfo.TRANS_TYPE,
                    siinfo.CENTER_BIZCYCLENO,
                    siinfo.HIS_BIZCYCLENO,
                    siinfo.CENTER_BUSSINESSSEQNO,
                    siinfo.HIS_BUSSINESSSEQNO
                #endregion
);
                exec = db.ExecuteNonQuery(sql);

                //2014-12-30 by xuzhongda 提交事务经常不生效，在此加上事务提交
                db.CommitTransaction();

                errSql = sql;
            }
            catch (Exception e)
            {
                err = e.Message;
                errSql = sql;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 修改医保主表信息(门诊测试)
        /// <summary>
        /// 修改医保主表信息(门诊)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateSIInfoRegBytest(BaseEntityer db, SIInfo info, ref string err, ref string errSql)
        {
            int exec = 0;
            string sql =
            #region
 @"insert into SIINFO20120401
                  (INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO)
                values
                  ('{0}',
                   '{1}',
                   '{2}',
                   '{3}',
                   '{4}',
                   to_date('{5}','yyyy-MM-dd hh24:mi:ss'),
                   '{6}',
                   '{7}',
                   '{8}',
                   '{9}',
                   '{10}',
                   '{11}',
                   '{12}',
                   '{13}',
                   '{14}',
                   '{15}',
                   '{16}',
                   '{17}',
                   '{18}',
                   '{19}',
                   to_date('{20}','yyyy-MM-dd hh24:mi:ss'),
                   '{21}',
                   '{22}',
                   '{23}',
                   '{24}',
                   '{25}',
                   '{26}',
                   '{27}',
                   '{28}',
                   '{29}',
                   '{30}',
                   '{31}',
                   '{32}',
                   '{33}',
                   '{34}',
                   '{35}',
                   '{36}',
                   '{37}',
                   '{38}',
                   '{39}',
                   '{40}',
                   '{41}',
                   '{42}',
                   '{43}',
                   '{44}',
                   to_date('{45}','yyyy-MM-dd hh24:mi:ss'),
                   '{46}',
                   '{47}',
                   to_date('{48}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{49}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{50}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{51}','yyyy-MM-dd hh24:mi:ss'),
                   '{52}',
                   '{53}',
                   '{54}',
                   to_date('{55}','yyyy-MM-dd hh24:mi:ss'),
                   '{56}',
                   '{57}',
                   '{58}',
                   '{59}',
                   '{60}',
                   '{61}',
                   '{62}',
                   '{63}',
                   '{64}',
                   '{65}',
                   '{66}',
                   '{67}',
                   '{68}',
                   '{69}',
                   '{70}',
                   '{71}',
                   '{72}',
                   '{73}',
                   '{74}',
                   '{75}',
                   '{76}',
                   '{77}',
                   '{78}',
                   '{79}',
                   '{80}',
                   '{81}',
                   '{82}',
                   '{83}',
                   '{84}',
                   '{85}',
                   '{86}',
                   '{87}',
                   '{88}',
                   '{89}',
                   '{90}',
                   '{91}',
                   '{92}',
                   '{93}',
                   '{94}',
                   '{95}',
                   '{96}',
                   '{97}',
                   '{98}',
                   '{99}',
                   '{100}',
                   to_date('{101}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{102}','yyyy-MM-dd hh24:mi:ss'),
                   to_date('{103}','yyyy-MM-dd'),
                   '{104}',
                   '{105}',
                   '{106}',
                   '{107}',
                   '{108}',
                   '{109}',
                   '{110}',
                   '{111}',
                   '{112}',
                   '{113}',
                   '{114}',
                   '{115}',
                   '{116}')
                ";
            #endregion
            try
            {
                sql = string.Format(sql,
                #region
 info.INPATIENT_ID,
                info.VISIT_ID,
                info.BALANCE_NO,
                info.NAME,
                info.SEX,
                info.BIRTHDAY,
                info.IDENNO,
                info.PACT_CODE,
                info.PACT_NAME,
                info.REGISTER_NO,
                info.HOSPITAL_NO,
                info.HOSPITAL_NAME,
                info.HOSPITAL_GRADE,
                info.IMP_NO,
                info.RUNNING_NO,
                info.INVOICE_NO,
                info.MEDICAL_TYPE,
                info.CARD_NO,
                info.MCARD_NO,
                info.PERSON_NO,
                info.SI_BEGINDATE,
                info.SI_STATE,
                info.ICCARD_NO,
                info.OVERAL_NO,
                info.FUND_NO,
                info.FUND_NAME,
                info.BUSINESSSEQUENCE,
                info.APPLYSEQUENCE,
                info.ANOTHERCITY_NO,
                info.ANOTHERCITY_NAME,
                info.CORPORATION_NO,
                info.CORPORATION_NAME,
                info.INSURANCETYPE,
                info.EMPL_TYPE,
                info.BED_NO,
                info.ISBALANCED,
                info.INDOCTOR_NO,
                info.INDOCTOR_NAME,
                info.OUTDOCTOR_NO,
                info.OUTDOCTOR_NAME,
                info.OUTREASON,
                info.PAYUSERFLAG,
                info.CLINICDIAGNOSE,
                info.INDIAGNOSE_NO,
                info.INDIAGNOSE_NAME,
                info.INDIAGNOSE_DATE,
                info.OUTDIAGNOSE_NO,
                info.OUTDIAGNOSE_NAME,
                info.OUTDIAGNOSE_DATE,
                info.INHOSPITAL_DATE,
                info.OUTHOSPITAL_DATE,
                info.BALANCE_DATE,
                info.BALANCE_STATE,
                info.OPER_NO,
                info.OPER_NAME,
                info.OPER_DATE,
                info.CLINICDEPT_NO,
                info.CLINICDEPT_NAME,
                info.CLINICDOCTOR_NO,
                info.CLINICDOCTOR_NAME,
                info.INDEPT_NO,
                info.INDEPT_NAME,
                info.OUTDEPT_NO,
                info.OUTDEPT_NAME,
                info.TOT_COST,
                info.PAY_COST,
                info.PUB_COST,
                info.OWN_COST,
                info.OFFICIAL_COST,
                info.OVER_COST,
                info.BASE_COST,
                info.OWN_SUPPLE_COST,
                info.HELP_ALLOWANCES_COST,
                info.ENTERPRISE_SUPPLE_COST,
                info.HELP_OWN_COST,
                info.YEAR_TOT_COST,
                info.YEAR_PUB_COST,
                info.YEAR_PAY_COST,
                info.YEAR_OWN_COST,
                info.YEAR_OFFICIAL_COST,
                info.YEAR_BASE_COST,
                info.YEAR_HELP_COST,
                info.YEAR_SPECIALMED_COST,
                info.INDIVIDUAL_COST,
                info.SPECIALMED_TOTCOST,
                info.SPECIALMED_PUBCOST,
                info.SPECIALMED_BASECOST,
                info.LXPUB_COST,
                info.YLOWN_COST,
                info.TCOWN_COST,
                info.DEOWN_COST,
                info.EXCEEDLIMIT_OWNCOST,
                info.SEALTOPLINE_OWNCOST,
                info.ENTERPRISEADD_COST,
                info.APPINFO_NO,
                info.APPINFO_NAME,
                info.APPINFO_MEMO,
                info.APPTYPE_NO,
                info.APPTYPE_NAME,
                info.APPTYPE_MEMO,
                info.APP_FLAG,
                info.APP_DATE,
                info.CARDVALIDDATE,
                info.SHIFTDATE.ToString("yyyy-MM-dd"),
                info.INHOSTIMES,
                info.ISVALID,
                info.PC_NO,
                info.REGINFORETURN,
                info.READCARDRETURN,
                info.BANINFORETURN,
                info.REMARK,
                info.TYPE_CODE,
                info.TRANS_TYPE,
                info.CENTER_BIZCYCLENO,
                info.HIS_BIZCYCLENO,
                info.CENTER_BUSSINESSSEQNO,
                info.HIS_BUSSINESSSEQNO
                #endregion
);
                exec = db.ExecuteNonQuery(sql);
                //errSql = sql;
            }
            catch (Exception e)
            {
                err = e.Message;
                errSql = sql;
                return -1;
            }
            //try
            //{
            //    sql =@"insert info siinfo20120401 set TOT_COST = TOT_COST + '{1}', PAY_COST =  PAY_COST + '{2}', PUB_COST =  PUB_COST + '{3}', OWN_COST =  OWN_COST + '{4}', HELP_ALLOWANCES_COST = HELP_ALLOWANCES_COST + '{5}', OWN_SUPPLE_COST = OWN_SUPPLE_COST + '{6}', HELP_OWN_COST = HELP_OWN_COST+ '{7}', REMARK = '{8]' where running_no='{0}'";
            //    sql = string.Format(sql,
            //        siinfo.RUNNING_NO,
            //        siinfo.TOT_COST,
            //        siinfo.PAY_COST,
            //        siinfo.PUB_COST,
            //        siinfo.OWN_COST,
            //        siinfo.HELP_ALLOWANCES_COST,
            //        siinfo.OWN_SUPPLE_COST,
            //        siinfo.HELP_OWN_COST,
            //        siinfo.REMARK);
            //    exec = db.ExecuteNonQuery(sql);
            //    errSql = sql;
            //}
            //catch (Exception e)
            //{
            //    err = e.Message;
            //    errSql = sql;
            //    return -1;
            //}
            return exec;
        }
        #endregion

        #region 修改医保主表有效标志(门诊)
        /// <summary>
        /// 修改医保主表有效标志(门诊)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateSIInfoValidFlagByOutpatient(BaseEntityer db, string patientid, string visitid, string visitDate, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"update SIINFO
                           set ISVALID = 0,
                               Individual_Cost=Individual_Cost+Pay_Cost
                         where INPATIENT_ID = '{0}'
                           and VISIT_ID = '{1}'
                           and SHIFTDATE = to_date('{2}','yyyy-MM-dd')
                           and ISVALID = 1";
                sql = string.Format(sql, patientid, visitid, visitDate);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 修改医保主表有效标志(住院)
        /// <summary>
        /// 修改医保主表有效标志(住院)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateSIInfoValidFlagByInpatient(BaseEntityer db, string patientid, string visitid, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"update SIINFO
                           set ISVALID = 0
                         where INPATIENT_ID = '{0}'
                           and VISIT_ID = '{1}'
                           and ISVALID = 1  and type_code='2'";
                sql = string.Format(sql, patientid, visitid);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        /// <summary>
        /// 更新新补充标准
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateSIInfoStandInfoByInpatient(BaseEntityer db, SIInfo SIInpatient, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"update SIINFO
                           set APPINFO_MEMO = '{0}',
                               APPINFO_NAME='{1}',
                               APPTYPE_NO='{2}',
                               APPTYPE_NAME='{3}'
                         where INPATIENT_ID = '{4}'
                           and VISIT_ID = '{5}'
                           and ISVALID = 1  and type_code='2'";
                sql = string.Format(sql, SIInpatient.APPINFO_MEMO, SIInpatient.APPINFO_NAME,
                    SIInpatient.APPTYPE_NO, SIInpatient.APPTYPE_NAME,
                    SIInpatient.INPATIENT_ID, SIInpatient.VISIT_ID);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        /// <summary>
        /// 葫芦岛出院信息更新
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SIInpatient"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateSIInfoHLDOutInfoByInpatient(BaseEntityer db, SIInfo SIInpatient, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"update SIINFO
                           set  APPTYPE_NO='{0}',
                                APPTYPE_NAME='{1}',
                                APPTYPE_MEMO='{2}'
                         where INPATIENT_ID = '{3}'
                           and VISIT_ID = '{4}'
                           and ISVALID = 1  and type_code='2'";
                sql = string.Format(sql,
                    SIInpatient.APPTYPE_NO, SIInpatient.APPTYPE_NAME, SIInpatient.APPTYPE_MEMO,
                    SIInpatient.INPATIENT_ID, SIInpatient.VISIT_ID);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        /// <summary>
        /// 黑龙江出院结算信息更新
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SIInpatient"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateSIInfoHLJOutInfoByInpatient(BaseEntityer db, SIInfo SIInpatient, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"update SIINFO
                           set  PAYUSERFLAG='{0}' 
                         where INPATIENT_ID = '{1}' 
                           and VISIT_ID = '{2}' 
                           and ISVALID = 1  and type_code='2'";
                sql = string.Format(sql,
                    SIInpatient.PAYUSERFLAG, SIInpatient.INPATIENT_ID, SIInpatient.VISIT_ID);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 获取医保对照信息
        /// <summary>
        /// 获取医保对照信息
        /// </summary>
        /// <param name="hisCode"></param>
        /// <param name="typeCode"></param>
        /// <param name="hisClass"></param>
        /// <param name="revString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetHisCompare(string hisCode, string typeCode, string hisClass, ref HIS_COMPARE revString, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql =
            #region
 @"select HIS_CODE,
                               CENTER_CODE,
                               FEE_TYPE,
                               CENTER_NAME,
                               CENTER_SPECS,
                               CENTER_UNIT,
                               CENTER_PRICE,
                               CENTER_TYPE,
                               CENTER_RATE,
                               CENTER_PACK,
                               CENTER_PLACE,
                               HIS_NAME,
                               HIS_SPELL,
                               HIS_SPECS,
                               HIS_UNIT,
                               HIS_PRICE,
                               HIS_TYPE,
                               HIS_PACK,
                               HIS_PLACE,
                               OPER_CODE,
                               OPER_DATE,
                               APPLYFLAG,
                               PERSONRATE,
                               HIS_WB_CODE,
                               HIS_USER_CODE,
                               TRANS,
                               HIS_CLASS,
                               CENTER_CLASS,
                               CHARGE_TYPE_CODE,
                               DRUG_TABOO,
                               UNTOWARD_REACTION,
                               PRECAUTIONS,
                               FEE_ITEMGRADE,
                               DOSAGE,
                               USAGE,
                               DOSAGE_UNIT,
                               ONCE_DOSAGE,
                               FREQUENCY,
                               DRUG_COMMON_LIMIT_FLAG,
                               DRUG_SPECIAL_LIMIT_FLAG,
                               MATERIAL_LIMITUSE_FLAG,
                               ISNEED_SITECODE
                          from HIS_COMPARE
                         where HIS_CODE = '{0}'
                           and CHARGE_TYPE_CODE = '{1}'
                           and HIS_CLASS = '{2}'";
            #endregion
            sql = string.Format(sql, hisCode, typeCode, hisClass);
            revString = new HIS_COMPARE();
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                #region 赋值
                revString.HIS_CODE = dt.Rows[0][0].ToString();
                revString.CENTER_CODE = dt.Rows[0][1].ToString();
                revString.FEE_TYPE = dt.Rows[0][2].ToString();
                revString.CENTER_NAME = dt.Rows[0][3].ToString();
                revString.CENTER_SPECS = dt.Rows[0][4].ToString();
                revString.CENTER_UNIT = dt.Rows[0][5].ToString();
                revString.CENTER_PRICE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][6].ToString()) == true ? "0" : dt.Rows[0][6].ToString());
                revString.CENTER_TYPE = dt.Rows[0][7].ToString();
                revString.CENTER_RATE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][8].ToString()) == true ? "0" : dt.Rows[0][8].ToString());
                revString.CENTER_PACK = int.Parse(string.IsNullOrEmpty(dt.Rows[0][9].ToString()) == true ? "0" : dt.Rows[0][9].ToString());
                revString.CENTER_PLACE = dt.Rows[0][10].ToString();
                revString.HIS_NAME = dt.Rows[0][11].ToString();
                revString.HIS_SPELL = dt.Rows[0][12].ToString();
                revString.HIS_SPECS = dt.Rows[0][13].ToString();
                revString.HIS_UNIT = dt.Rows[0][14].ToString();
                revString.HIS_PRICE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][15].ToString()) == true ? "0" : dt.Rows[0][15].ToString());
                revString.HIS_TYPE = dt.Rows[0][16].ToString();
                revString.HIS_PACK = int.Parse(string.IsNullOrEmpty(dt.Rows[0][17].ToString()) == true ? "0" : dt.Rows[0][17].ToString());
                revString.HIS_PLACE = dt.Rows[0][18].ToString();
                revString.OPER_CODE = dt.Rows[0][19].ToString();
                revString.OPER_DATE = DateTime.Parse(string.IsNullOrEmpty(dt.Rows[0][20].ToString()) == true ? DateTime.MinValue.ToString() : dt.Rows[0][20].ToString());
                revString.APPLYFLAG = dt.Rows[0][21].ToString();
                revString.PERSONRATE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][22].ToString()) == true ? "0" : dt.Rows[0][22].ToString());
                revString.HIS_WB_CODE = dt.Rows[0][23].ToString();
                revString.HIS_USER_CODE = dt.Rows[0][24].ToString();
                revString.TRANS = dt.Rows[0][25].ToString();
                revString.HIS_CLASS = dt.Rows[0][26].ToString();
                revString.CENTER_CLASS = dt.Rows[0][27].ToString();
                revString.CHARGE_TYPE_CODE = dt.Rows[0][28].ToString();
                revString.DRUG_TABOO = dt.Rows[0][29].ToString();
                revString.UNTOWARD_REACTION = dt.Rows[0][30].ToString();
                revString.PRECAUTIONS = dt.Rows[0][31].ToString();
                revString.FEE_ITEMGRADE = dt.Rows[0][32].ToString();
                revString.DOSAGE = dt.Rows[0][33].ToString();
                revString.USAGE = dt.Rows[0][34].ToString();
                revString.DOSAGE_UNIT = dt.Rows[0][35].ToString();
                revString.ONCE_DOSAGE = dt.Rows[0][36].ToString();
                revString.FREQUENCY = dt.Rows[0][37].ToString();
                revString.DRUG_COMMON_LIMIT_FLAG = dt.Rows[0][38].ToString();
                revString.DRUG_SPECIAL_LIMIT_FLAG = dt.Rows[0][39].ToString();
                revString.MATERIAL_LIMITUSE_FLAG = dt.Rows[0][40].ToString();
                revString.ISNEED_SITECODE = dt.Rows[0][41].ToString();
                #endregion
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 获取医保对照信息
        /// </summary>
        /// <param name="hisCode"></param>
        /// <param name="typeCode"></param>
        /// <param name="revString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetHisCompareNoClass(string hisCode, string typeCode, ref HIS_COMPARE revString, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql =
            #region
 @"select HIS_CODE,
                               CENTER_CODE,
                               FEE_TYPE,
                               CENTER_NAME,
                               CENTER_SPECS,
                               CENTER_UNIT,
                               CENTER_PRICE,
                               CENTER_TYPE,
                               CENTER_RATE,
                               CENTER_PACK,
                               CENTER_PLACE,
                               HIS_NAME,
                               HIS_SPELL,
                               HIS_SPECS,
                               HIS_UNIT,
                               HIS_PRICE,
                               HIS_TYPE,
                               HIS_PACK,
                               HIS_PLACE,
                               OPER_CODE,
                               OPER_DATE,
                               APPLYFLAG,
                               PERSONRATE,
                               HIS_WB_CODE,
                               HIS_USER_CODE,
                               TRANS,
                               HIS_CLASS,
                               CENTER_CLASS,
                               CHARGE_TYPE_CODE,
                               DRUG_TABOO,
                               UNTOWARD_REACTION,
                               PRECAUTIONS,
                               FEE_ITEMGRADE,
                               DOSAGE,
                               USAGE,
                               DOSAGE_UNIT,
                               ONCE_DOSAGE,
                               FREQUENCY,
                               DRUG_COMMON_LIMIT_FLAG,
                               DRUG_SPECIAL_LIMIT_FLAG,
                               MATERIAL_LIMITUSE_FLAG,
                               ISNEED_SITECODE,
                               NO_COMPENSATE
                          from HIS_COMPARE
                         where HIS_CODE = '{0}'
                           and CHARGE_TYPE_CODE = '{1}' "; 
            #endregion
            sql = string.Format(sql, hisCode, typeCode);
            revString = new HIS_COMPARE();
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                #region 赋值
                revString.HIS_CODE = dt.Rows[0][0].ToString();
                revString.CENTER_CODE = dt.Rows[0][1].ToString();
                revString.FEE_TYPE = dt.Rows[0][2].ToString();
                revString.CENTER_NAME = dt.Rows[0][3].ToString();
                revString.CENTER_SPECS = dt.Rows[0][4].ToString();
                revString.CENTER_UNIT = dt.Rows[0][5].ToString();
                revString.CENTER_PRICE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][6].ToString()) == true ? "0" : dt.Rows[0][6].ToString());
                revString.CENTER_TYPE = dt.Rows[0][7].ToString();
                revString.CENTER_RATE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][8].ToString()) == true ? "0" : dt.Rows[0][8].ToString());
                revString.CENTER_PACK = int.Parse(string.IsNullOrEmpty(dt.Rows[0][9].ToString()) == true ? "0" : dt.Rows[0][9].ToString());
                revString.CENTER_PLACE = dt.Rows[0][10].ToString();
                revString.HIS_NAME = dt.Rows[0][11].ToString();
                revString.HIS_SPELL = dt.Rows[0][12].ToString();
                revString.HIS_SPECS = dt.Rows[0][13].ToString();
                revString.HIS_UNIT = dt.Rows[0][14].ToString();
                revString.HIS_PRICE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][15].ToString()) == true ? "0" : dt.Rows[0][15].ToString());
                revString.HIS_TYPE = dt.Rows[0][16].ToString();
                revString.HIS_PACK = int.Parse(string.IsNullOrEmpty(dt.Rows[0][17].ToString()) == true ? "0" : dt.Rows[0][17].ToString());
                revString.HIS_PLACE = dt.Rows[0][18].ToString();
                revString.OPER_CODE = dt.Rows[0][19].ToString();
                revString.OPER_DATE = DateTime.Parse(string.IsNullOrEmpty(dt.Rows[0][20].ToString()) == true ? DateTime.MinValue.ToString() : dt.Rows[0][20].ToString());
                revString.APPLYFLAG = dt.Rows[0][21].ToString();
                revString.PERSONRATE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][22].ToString()) == true ? "0" : dt.Rows[0][22].ToString());
                revString.HIS_WB_CODE = dt.Rows[0][23].ToString();
                revString.HIS_USER_CODE = dt.Rows[0][24].ToString();
                revString.TRANS = dt.Rows[0][25].ToString();
                revString.HIS_CLASS = dt.Rows[0][26].ToString();
                revString.CENTER_CLASS = dt.Rows[0][27].ToString();
                revString.CHARGE_TYPE_CODE = dt.Rows[0][28].ToString();
                revString.DRUG_TABOO = dt.Rows[0][29].ToString();
                revString.UNTOWARD_REACTION = dt.Rows[0][30].ToString();
                revString.PRECAUTIONS = dt.Rows[0][31].ToString();
                revString.FEE_ITEMGRADE = dt.Rows[0][32].ToString();
                revString.DOSAGE = dt.Rows[0][33].ToString();
                revString.USAGE = dt.Rows[0][34].ToString();
                revString.DOSAGE_UNIT = dt.Rows[0][35].ToString();
                revString.ONCE_DOSAGE = dt.Rows[0][36].ToString();
                revString.FREQUENCY = dt.Rows[0][37].ToString();
                revString.DRUG_COMMON_LIMIT_FLAG = dt.Rows[0][38].ToString();
                revString.DRUG_SPECIAL_LIMIT_FLAG = dt.Rows[0][39].ToString();
                revString.MATERIAL_LIMITUSE_FLAG = dt.Rows[0][40].ToString();
                revString.ISNEED_SITECODE = dt.Rows[0][41].ToString();
                revString.NO_COMPENSATE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0]["NO_COMPENSATE"].ToString()) == true ? "0" : dt.Rows[0]["NO_COMPENSATE"].ToString());
                  
                #endregion
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取空医保对照信息
        /// </summary>
        /// <param name="hisCode"></param>
        /// <param name="revString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetNULLHisCompare(string hisCode, ref HIS_COMPARE revString, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql =
            #region
 @"select t.item_no,t.item_name,t.center_class from si_synullhiscompare t where t.item_no = '{0}' and t.isvaild=1";
            #endregion
            sql = string.Format(sql, hisCode);
            revString = new HIS_COMPARE();
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                    return 0;
                if (dt.Rows.Count >= 2)
                {
                    errMsg = "项目编号大于2条，请联系信息科进行修改。";
                    return -1;
                }
                #region 赋值
                revString.HIS_CODE = dt.Rows[0][0].ToString();
                revString.HIS_NAME = dt.Rows[0][1].ToString();
                revString.CENTER_CLASS = dt.Rows[0][2].ToString();
                revString.CENTER_CODE = "";
                revString.CENTER_NAME = "";
                revString.CENTER_SPECS = "";
                revString.CENTER_UNIT = "";
                revString.CENTER_PRICE = 0;
                revString.CENTER_TYPE = "";
                revString.CENTER_RATE = 0;
                revString.CENTER_PACK = 0;
                revString.CENTER_PLACE = "";
                revString.HIS_SPECS = "";
                revString.HIS_UNIT = "";
                revString.HIS_PRICE = 0;
                revString.HIS_TYPE = "";
                revString.HIS_PACK = 0;
                revString.HIS_PLACE = "";
                revString.HIS_CLASS = "";
                revString.DOSAGE = "";
                revString.USAGE = "";
                revString.DOSAGE_UNIT = "";
                revString.ONCE_DOSAGE = "";
                revString.FREQUENCY = "";

                #endregion
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取医师职业编号
        /// <summary>
        /// 获取医师职业编号
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetProfessDoctorNo(string userid, ref USERS_STAFF_DICT staff)
        {
            DataTable dt = new DataTable();
            string sql = @"select USER_ID,USER_NAME,USER_DEPT,CREATE_DATE,JOB,TITLE,MENU_GROUP,USER_PASS,DIS,TOXI,certificate_code,id_card from USERS_STAFF_DICT  where user_id = '{0}'";
            sql = string.Format(sql, userid);
            staff = new USERS_STAFF_DICT();
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                staff.USER_ID = dt.Rows[0][0].ToString();
                staff.USER_NAME = dt.Rows[0][1].ToString();
                staff.USER_DEPT = dt.Rows[0][2].ToString();
                staff.CREATE_DATE = DateTime.Parse(dt.Rows[0][3].ToString());
                staff.JOB = dt.Rows[0][4].ToString();
                staff.TITLE = dt.Rows[0][5].ToString();
                staff.MENU_GROUP = dt.Rows[0][6].ToString();
                staff.USER_PASS = dt.Rows[0][7].ToString();
                staff.DIS = dt.Rows[0][8].ToString();
                staff.TOXI = dt.Rows[0][9].ToString();
                staff.CERTIFICATE_CODE = dt.Rows[0][10].ToString();
                staff.ID_CARD = dt.Rows[0][11].ToString();
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        #endregion

        #region 门诊根据处方号查询处方日期
        /// <summary>
        /// 门诊根据处方号查询处方日期
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DateTime GetMZRcptDate(string rcptNO, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select order_date from outp_orders where serial_no = '{0}'";
            sql = string.Format(sql, rcptNO);
            DateTime revString = DateTime.MinValue;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return DateTime.MinValue;
                revString = DateTime.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return DateTime.MinValue;
            }
            return revString;
        }
        #endregion

        #region 获取部位码
        /// <summary>
        /// 获取部位码
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetBodyNO(string itemNO, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select body_code from si_sybody where item_code = '{0}'";
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                revString = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return revString;
        }
        #endregion

        #region 获取住院医保诊断信息
        /// <summary>
        /// 获取住院医保诊断信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<DIAGNOSIS> GetInDiagnose(string patientid, string visitid, string type, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<DIAGNOSIS> diagnoselist = new List<DIAGNOSIS>();
            string sql = @"select s.PATIENT_ID,
                               s.VISIT_ID,
                               s.DIAGNOSIS_TYPE,
                               s.DIAGNOSIS_NO,
                               s.DIAGNOSIS_DESC,
                               s.DIAGNOSIS_DATE,
                               s.TREAT_DAYS,
                               s.TREAT_RESULT,
                               s.OPER_TREAT_INDICATOR,
                               s.DIAG_CODE,
                               s.diagnose_identification,
                               s.OPER_CODE
                          from diagnosis s
                         where (s.diagnosis_type = '{2}' or 'ALL' = '{2}')
                           and s.patient_id = '{0}'
                           and s.visit_id = '{1}'
                         order by s.DIAGNOSIS_NO desc";
            sql = string.Format(sql, patientid, visitid, type);
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DIAGNOSIS diag = new DIAGNOSIS();
                    diag.PATIENT_ID = dt.Rows[i][0].ToString();
                    diag.VISIT_ID = int.Parse(dt.Rows[i][1].ToString());
                    diag.DIAGNOSIS_TYPE = dt.Rows[i][2].ToString();
                    diag.DIAGNOSIS_NO = int.Parse(dt.Rows[i][3].ToString());
                    diag.DIAGNOSIS_DESC = dt.Rows[i][4].ToString();
                    diag.DIAGNOSIS_DATE = DateTime.Parse(dt.Rows[i][5].ToString());
                    diag.TREAT_DAYS = int.Parse(dt.Rows[i][6].ToString());
                    diag.TREAT_RESULT = dt.Rows[i][7].ToString();
                    diag.OPER_TREAT_INDICATOR = int.Parse(dt.Rows[i][8].ToString());
                    diag.DIAG_CODE = dt.Rows[i][9].ToString();
                    diag.DIAGNOSE_IDENTIFICATION = dt.Rows[i][10].ToString();
                    diag.OPER_CODE = dt.Rows[i][11].ToString();
                    diagnoselist.Add(diag);
                }

            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return diagnoselist;
        }
        #endregion

        #region 获取医保手术信息
        /// <summary>
        /// 获取医保手术信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<SI_SYUPLOADOPERATION> GetInOperation(string patientid, string visitid, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<SI_SYUPLOADOPERATION> operationlist = new List<SI_SYUPLOADOPERATION>();
            string sql = @"select PATIENTID,
                                   VISITID,
                                   SERIAL,
                                   OPER_DATE,
                                   OPERATION_CODE,
                                   OPERATION_NAME,
                                   VAILD_FLAG,
                                   VISIT_NO,
                                   VISIT_DATE,
                                   OPER_CODE,
                                   OPER_DEPT,
                                   OPERATION_BODY
                              from SI_SYUPLOADOPERATION
                             where patientid = '{0}'
                               and visitid = '{1}'
                               and vaild_flag = '1'";
            sql = string.Format(sql, patientid, visitid);
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SI_SYUPLOADOPERATION oper = new SI_SYUPLOADOPERATION();
                    oper.PATIENTID = dt.Rows[i][0].ToString();
                    oper.VISITID = int.Parse(dt.Rows[i][1].ToString());
                    oper.SERIAL = dt.Rows[i][2].ToString();
                    oper.OPER_DATE = DateTime.Parse(dt.Rows[i][3].ToString());
                    oper.OPERATION_CODE = dt.Rows[i][4].ToString();
                    oper.OPERATION_NAME = dt.Rows[i][5].ToString();
                    oper.VAILD_FLAG = dt.Rows[i][6].ToString();
                    oper.VISIT_NO = int.Parse(dt.Rows[i][7].ToString());
                    oper.VISIT_DATE = DateTime.Parse(dt.Rows[i][8].ToString());
                    oper.OPER_CODE = dt.Rows[i][9].ToString();
                    oper.OPER_DEPT = dt.Rows[i][10].ToString();
                    oper.OPERATION_BODY = dt.Rows[i][11].ToString();
                    operationlist.Add(oper);
                }

            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return operationlist;
        }
        #endregion

        #region 获取医保对照信息（上传用）
        /// <summary>
        /// 获取医保对照信息（上传用）
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetHisCompareUpLoad(string typeCode, ref string errMsg)
        {
            DataTable compareList = new DataTable();
            string sql =
            #region
 @"select HIS_CODE as 医院项目编码,--0
                               CENTER_CODE as 医保项目编码,--1
                               FEE_TYPE as 项目类型,--2
                               CENTER_NAME as 医保项目名称,--3
                               CENTER_SPECS as 医保规格,--4
                               CENTER_UNIT as 医保项目单位,--5
                               CENTER_PRICE as 医保价格,--6
                               CENTER_TYPE as 医保剂型,--7
                               CENTER_RATE as 自付比例,--8
                               CENTER_PACK as 医保包装数量,--9
                               CENTER_PLACE as 医保产地,--10
                               HIS_NAME as 医院项目名称,--11
                               HIS_SPELL as 医院拼音,--12
                              ( select DRUG_SPEC from
                               drug_price_list where   drug_code = his_code and stop_date is   null)  as 医院规格,--13
                               ( select FIRM_ID from
                               drug_price_list where   drug_code = his_code and stop_date is   null)  as 医院项目单位,--14
                               HIS_PRICE as 医院价格,--15
                               HIS_TYPE as 医院剂型,--16                               
                              ( select amount_per_package from
                               drug_price_list where   drug_code = his_code and stop_date is   null)        as 医院包装数量,--17
                               HIS_PLACE as 医院产地,--18
                               OPER_CODE as 操作员,--19
                               OPER_DATE as 操作日期,--20
                               APPLYFLAG as 申请标志,--21
                               PERSONRATE as 个人比例,--22
                               HIS_WB_CODE as 五笔码,--23
                               HIS_USER_CODE as 自定义码,--24
                               TRANS as 交易类型,--25
                               HIS_CLASS as 医院价表类别,--26
                               CENTER_CLASS as 中心项目类别,--27
                               CHARGE_TYPE_CODE as 费别编码,--28
                               DRUG_TABOO as 药品禁忌,--29
                               UNTOWARD_REACTION as 不良反应,--30
                               PRECAUTIONS as 注意事项,--31
                               FEE_ITEMGRADE as 收费项目种类,--32
                               DOSAGE as 药品注册剂型,--33
                               USAGE as 用法,--34
                               DOSAGE_UNIT as 药品剂量单位,--35
                               ONCE_DOSAGE as 每次用量,--36
                               FREQUENCY as 使用频次,--37
                               DRUG_COMMON_LIMIT_FLAG as 药品普通限制标识,--38
                               DRUG_SPECIAL_LIMIT_FLAG as 药品特殊限制标志,--39
                               MATERIAL_LIMITUSE_FLAG as 材料限制使用标识,--40
                               ISNEED_SITECODE as 是否需要传入部位码,--41
                           nvl( (select RATIFY_NO from drug_price_list  where drug_code = his_code and stop_date is   null  ),'')  as  批准文号 

                          from HIS_COMPARE
                         where CHARGE_TYPE_CODE = '{0}'";
            #endregion
            sql = string.Format(sql, typeCode);
            try
            {
                compareList = BaseEntityer.Db.GetDataTable(sql);
                if (compareList.Rows.Count <= 0)
                {
                    errMsg = "没有查询到对照数据";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return compareList;
        }

        #endregion

        #region 获取中心材料信息（上传用）
        /// <summary>
        /// 获取中心材料信息（上传用）
        /// </summary>
        /// <param name="itemNo"></param>
        /// <param name="chargeCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetCenterUndrugInfo(string itemNo, string chargeCode, ref string errMsg)
        {
            DataTable compareList = new DataTable();
            string sql =
            #region
 @"select MATERIAL_CATALOG,
                   PRODUCE_REG_NAME,
                   MATERIAL_MED_NAME,
                   BRAND,
                   PRODUCE_REG_NO,
                   SPECIFICATION,
                   PRODEUCE_AREA,
                   PRODUCE_FACTORY,
                   AGENCY,
                   MEDICAL_MATERIAL
              from SI_SYUNDRUG where ITEM_NO='{0}' and CHARGE_CODE='{1}'";
            #endregion
            sql = string.Format(sql, itemNo, chargeCode);
            try
            {
                compareList = BaseEntityer.Db.GetDataTable(sql);
                if (compareList.Rows.Count <= 0)
                {
                    errMsg = "没有查询到对照数据";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return compareList;
        }
        #endregion

        #region 获取上传医生信息（上传用）

        /// <summary>
        /// 更新医生上传标记
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateDoctorSiUpdateFlag(string userID, string operCode, DateTime operDate, ref  string errMsg)
        {
            int exec = 0;
            try
            {
                string sql = @"UPDATE users_staff_dict t
                       SET t.si_update_date = to_date('{0}', 'YYYY-MM-DD HH24:MI:SS'), --医保上传时间
                           t.si_update_oper = '{1}', --医保上传人
                           t.si_update_flag = '1' --医保上传标记（1,上传 0 未上传）
                     WHERE t.user_id = '{2}'";
                sql = string.Format(sql, operDate.ToString(), operCode, userID);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return exec;
        }

        /// <summary>
        /// 获取上传医生信息（上传用）
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetDoctorUpLoad(ref string errMsg)
        {
            DataTable doctorList = new DataTable();
            string sql =
            #region
 @"select USER_ID as 医生编号,
       USER_NAME as 医生姓名,
       SEX as 性别,
       DOCTOR_TYPE as 医师类别,
       USER_DEPT as 科室编码,
       (select t.dept_name from dept_dict t where t.dept_code=USER_DEPT) as 科室名称,
       JOB_TITLE as 医保职称,
       PROFESSIONAL_TITILE as 技术职称,
       SCHOOL as 毕业院校,
       ID_CARD as 身份证号,
       '' as 联系方式,
       PROFESS_TYPE as 职业类别,
       CERTIFICATE_CODE as 职业证书编号,
       PROFESS_RANGE as 职业范围,
       CERTIFICATE_DATE as 职业证书注册日期,
       PRIMARY_ILLNESS as 主治疾病内容,
       HOS_STAFF_TYPE as 医院人员类别,
       STAR_LEVEL as 星级评定,
       CERTIFICATE_PLACE as 注册地点,
       OPER_CODE as 经办人,
       '' as 备注,
       si_update_flag as 上传标记,
     si_update_oper as 上传人,
     si_update_date as 上传时间
  from USERS_STAFF_DICT
  where JOB = 'A1'";
            #endregion
            try
            {
                doctorList = BaseEntityer.Db.GetDataTable(sql);
                if (doctorList.Rows.Count <= 0)
                {
                    errMsg = "没有查询到医生信息数据";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return doctorList;
        }
        #endregion

        #region 未上传费用患者列表
        /// <summary>
        /// 未上传费用患者列表
        /// </summary>
        /// <param name="charge_code"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<BringSpringObject> GetUnuploadPatientList(string charge_code, ref string errMsg)
        {
            DataTable ReUpLoadList = new DataTable();
            List<BringSpringObject> list = new List<BringSpringObject>();
            string sql =
            #region
 @"select distinct t.inpatient_id, t.visit_id, t.name, s.dept_code, s.dept_name
  from pat_visit f,siinfo t
  left join dept_dict s
    on t.INDEPT_NO = s.dept_code
 where (f.patient_id || f.visit_id)  = (t.inpatient_id || t.visit_id)
   and f.state not in ('O', 'N', 'P')
   and (t.inpatient_id || t.visit_id) in
       (select distinct (a.patient_id || a.visit_id)
          from inp_bill_detail a
         where a.up_flag = '0')
   and t.pact_code in ('{0}')
   and t.isvalid = '1'
   and t.type_code = '2'
   and t.ISBALANCED = '0'";
            // 1
            // @"select t.inpatient_id, t.visit_id, t.name, s.dept_code, s.dept_name
            //  from siinfo t, dept_dict s,pat_visit f
            // where t.INDEPT_NO = s.dept_code
            // and f.patient_id = t.inpatient_id
            // and f.visit_id = t.visit_id
            //   and (t.inpatient_id || t.visit_id) in (select distinct (a.patient_id || a.visit_id)
            //                          from inp_bill_detail a
            //                         where a.up_flag = '0')
            //   and t.pact_code in ('{0}') and t.isvalid='1' and t.type_code='2' and t.ISBALANCED='0'
            //   and f.state not in ('O','N','P')";
            //2
            // @"select t.inpatient_id, t.visit_id, t.name, s.dept_code, s.dept_name
            //  from siinfo t, dept_dict s
            // where t.INDEPT_NO = s.dept_code
            //   and (t.inpatient_id || t.visit_id) in (select distinct (a.patient_id || a.visit_id)
            //                          from inp_bill_detail a
            //                         where a.up_flag = '0')
            //   and t.pact_code in ('{0}') and t.isvalid='1' and t.type_code='2' and t.ISBALANCED='0'";
            // @"select t.patient_id, t.visit_id, c.name, t.dept_code, s.dept_name
            //  from pats_in_hospital t, pat_master_index c, dept_dict s
            // where t.dept_code = s.dept_code
            //   and t.patient_id = c.patient_id
            //   and t.patient_id in (select distinct a.patient_id
            //                          from si_syfeedetails a
            //                         where a.upload_flag = '0')
            //   and c.pact_code in ('{0}')";
            sql = string.Format(sql, charge_code);
            #endregion
            try
            {
                ReUpLoadList = BaseEntityer.Db.GetDataTable(sql);
                if (ReUpLoadList.Rows.Count <= 0)
                {
                    //errMsg = "没有查询到医生信息数据";
                    //return null;
                }
                for (int i = 0; i < ReUpLoadList.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = ReUpLoadList.Rows[i][0].ToString();
                    obj.Name = ReUpLoadList.Rows[i][2].ToString();
                    obj.Memo = ReUpLoadList.Rows[i][1].ToString();
                    obj.User01 = ReUpLoadList.Rows[i][3].ToString();
                    obj.User02 = ReUpLoadList.Rows[i][4].ToString();
                    list.Add(obj);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return list;
        }

        /// <summary>
        /// 未上传费用患者列表
        /// </summary>
        /// <param name="charge_code"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<BringSpringObject> GetUnuploadPatientLists(string charge_code, ref string errMsg)
        {
            DataTable ReUpLoadList = new DataTable();
            List<BringSpringObject> list = new List<BringSpringObject>();
            #region
            string sql = @" select t.inpatient_id, t.visit_id, t.name, s.dept_code, s.dept_name
                              from siinfo t, dept_dict s, pat_visit f
                             where f.patient_id = t.inpatient_id
                               and t.INDEPT_NO = s.dept_code
                               and f.visit_id = t.visit_id
                               and (t.inpatient_id || t.visit_id) in
                                   (select distinct (a.patient_id || a.visit_id)
                                      from inp_bill_detail a
                                     where a.patient_id = t.inpatient_id
                                       and a.visit_id = t.visit_id
                                       and a.up_flag = '0')
                               and t.pact_code in ({0})
                               and t.isvalid = '1'
                               and t.type_code = '2'
                               and t.ISBALANCED = '0'
                               and f.state not in ('O', 'N', 'P') ";
            sql = string.Format(sql, charge_code);
            #endregion
            try
            {
                ReUpLoadList = BaseEntityer.Db.GetDataTable(sql);
                if (ReUpLoadList.Rows.Count <= 0)
                {
                    //errMsg = "没有查询到医生信息数据";
                    //return null;
                }
                for (int i = 0; i < ReUpLoadList.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = ReUpLoadList.Rows[i][0].ToString();
                    obj.Name = ReUpLoadList.Rows[i][2].ToString();
                    obj.Memo = ReUpLoadList.Rows[i][1].ToString();
                    obj.User01 = ReUpLoadList.Rows[i][3].ToString();
                    obj.User02 = ReUpLoadList.Rows[i][4].ToString();
                    list.Add(obj);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return list;
        }

        public List<BringSpringObject> GetUnuploadNursePatientList(string charge_code, string ward_code, ref string errMsg)
        {
            DataTable ReUpLoadList = new DataTable();
            List<BringSpringObject> list = new List<BringSpringObject>();
            string sql =
            #region
 @"select t.inpatient_id, t.visit_id, t.name, s.dept_code, s.dept_name
              from siinfo t, dept_dict s,pat_visit f,PATS_IN_HOSPITAL p 
             where t.INDEPT_NO = s.dept_code
             and f.patient_id = t.inpatient_id
             and f.visit_id = t.visit_id
             and p.patient_id=f.patient_id
             and p.visit_id=f.visit_id
            /*   and (t.inpatient_id || t.visit_id) in (select distinct (a.patient_id || a.visit_id) from inp_bill_detail a
             where a.up_flag = '0')*/
               and t.pact_code in ('{0}') and t.isvalid='1' and t.type_code='2' and t.ISBALANCED='0'
               and f.state not in ('O','N','P') and p.ward_code='{1}'
               
              and   exists(select    a.patient_id from inp_bill_detail a  WHERE  a.patient_id=t.inpatient_id and  a.visit_id=t.visit_id and  a.up_flag='0' and   rownum=1)
";
            sql = string.Format(sql, charge_code, ward_code);
            #endregion
            try
            {
                ReUpLoadList = BaseEntityer.Db.GetDataTable(sql);
                if (ReUpLoadList.Rows.Count <= 0)
                {
                    //errMsg = "没有查询到医生信息数据";
                    //return null;
                }
                for (int i = 0; i < ReUpLoadList.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = ReUpLoadList.Rows[i][0].ToString();
                    obj.Name = ReUpLoadList.Rows[i][2].ToString();
                    obj.Memo = ReUpLoadList.Rows[i][1].ToString();
                    obj.User01 = ReUpLoadList.Rows[i][3].ToString();
                    obj.User02 = ReUpLoadList.Rows[i][4].ToString();
                    list.Add(obj);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return list;
        }

        #endregion

        #region 获取未上传费用明细列表
        /// <summary>
        /// 获取未上传费用明细列表
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<SI_SYFEEDETAILS> GetFeeDetail(string patientid, string visitid, ref string errMsg)
        {
            DataTable FeeDetailList = new DataTable();
            List<SI_SYFEEDETAILS> list = new List<SI_SYFEEDETAILS>();
            string sql =
            #region
 @"select PATIENT_ID,
       VISIT_ID,
       NAME,
       PACT_CODE,
       PACT_NAME,
       INHOS_DEPT_CODE,
       INHOS_DEPT_NAME,
       RUNNING_NO,
       ITEM_TYPE,
       CENTER_CLASS,
       RECEIPT_NO,
       RECEIPT_DATE,
       ITEM_CODE,
       CENTER_CODE,
       ITEM_NAME,
       PRICE,
       QTY,
       DOSE_FORM_CODE,
       DOSE_FORM_NAME,
       SPECS,
       ONCE_DOSAGE,
       FREQUENCY,
       RECEIPT_DOCTOR_CODE,
       RECEIPT_DOCTOR_NAME,
       DOCTOR_CARDNO,
       USAGE,
       UNIT,
       DEPT_CODE,
       DEPT_NAME,
       DAYS,
       SEQUENCE_NO,
       HERB_COMPOUND_FLAG,
       DOSAGE_UNIT,
       PACK_QTY,
       PACK_UNIT,
       MIN_UNIT,
       DRUG_TYPE,
       DRUG_QUALITY,
       FEE_CODE,
       FEE_NAME,
       TOT_COST,
       OWN_COST,
       PAY_COST,
       PUB_COST,
       REB_COST,
       DET_COST,
       CENTER_TOT_COST,
       CANCEL_RECEIPT_NO,
       CANCEL_SEQUENCE_NO,
       OPER_CODE,
       OPER_DATE,
       UPLOAD_FLAG,
       UPLOAD_DATE,
       TRANS_TYPE,
       CENTER_OWN_COST,
       CENTER_OWNFEE_COST,
       EXCEEDLIMIT_OWNFEECOST,
       CENTER_PUB_COST,
       CENTER_REPUB_COST,
       CENTER_ITEM_GRADE,
       CENTER_ALLOWN_FALG,
       CENTER_DOCTOR_ABNORMAL_FALG,
       DRUG_COMMON_LIMIT_FLAG,
       DRUG_SPECIAL_LIMIT_FLAG,
       MATERIAL_LIMITUSE_FLAG,
       MATERIAL_SITECODE,
       RATE,
       BALANCE_FLAG 
  from SI_SYFEEDETAILS where PATIENT_ID='{0}' and VISIT_ID = '{1}'";
            sql = string.Format(sql, patientid, visitid);
            #endregion
            try
            {
                FeeDetailList = BaseEntityer.Db.GetDataTable(sql);
                if (FeeDetailList.Rows.Count <= 0)
                {
                    //errMsg = "没有查询到医生信息数据";
                    //return null;
                }
                for (int i = 0; i < FeeDetailList.Rows.Count; i++)
                {
                    SI_SYFEEDETAILS obj = new SI_SYFEEDETAILS();
                    obj.PATIENT_ID = FeeDetailList.Rows[i][0].ToString();
                    obj.VISIT_ID = int.Parse(FeeDetailList.Rows[i][1].ToString());
                    obj.NAME = FeeDetailList.Rows[i][2].ToString();
                    obj.PACT_CODE = FeeDetailList.Rows[i][3].ToString();
                    obj.PACT_NAME = FeeDetailList.Rows[i][4].ToString();
                    obj.INHOS_DEPT_CODE = FeeDetailList.Rows[i][5].ToString();
                    obj.INHOS_DEPT_NAME = FeeDetailList.Rows[i][6].ToString();
                    obj.RUNNING_NO = FeeDetailList.Rows[i][7].ToString();
                    obj.ITEM_TYPE = FeeDetailList.Rows[i][8].ToString();
                    obj.CENTER_CLASS = FeeDetailList.Rows[i][9].ToString();
                    obj.RECEIPT_NO = FeeDetailList.Rows[i][10].ToString();
                    obj.RECEIPT_DATE = DateTime.Parse(FeeDetailList.Rows[i][11].ToString());
                    obj.ITEM_CODE = FeeDetailList.Rows[i][12].ToString();
                    obj.CENTER_CODE = FeeDetailList.Rows[i][13].ToString();
                    obj.ITEM_NAME = FeeDetailList.Rows[i][14].ToString();
                    obj.PRICE = decimal.Parse(FeeDetailList.Rows[i][15].ToString());
                    obj.QTY = decimal.Parse(FeeDetailList.Rows[i][16].ToString());
                    obj.DOSE_FORM_CODE = FeeDetailList.Rows[i][17].ToString();
                    obj.DOSE_FORM_NAME = FeeDetailList.Rows[i][18].ToString();
                    obj.SPECS = FeeDetailList.Rows[i][19].ToString();
                    obj.ONCE_DOSAGE = FeeDetailList.Rows[i][20].ToString();
                    obj.FREQUENCY = FeeDetailList.Rows[i][21].ToString();
                    obj.RECEIPT_DOCTOR_CODE = FeeDetailList.Rows[i][22].ToString();
                    obj.RECEIPT_DOCTOR_NAME = FeeDetailList.Rows[i][23].ToString();
                    obj.DOCTOR_CARDNO = FeeDetailList.Rows[i][24].ToString();
                    obj.USAGE = FeeDetailList.Rows[i][25].ToString();
                    obj.UNIT = FeeDetailList.Rows[i][26].ToString();
                    obj.DEPT_CODE = FeeDetailList.Rows[i][27].ToString();
                    obj.DEPT_NAME = FeeDetailList.Rows[i][28].ToString();
                    obj.DAYS = FeeDetailList.Rows[i][29].ToString();
                    obj.SEQUENCE_NO = int.Parse(FeeDetailList.Rows[i][30].ToString());
                    obj.HERB_COMPOUND_FLAG = FeeDetailList.Rows[i][31].ToString();
                    obj.DOSAGE_UNIT = FeeDetailList.Rows[i][32].ToString();
                    obj.PACK_QTY = decimal.Parse(FeeDetailList.Rows[i][33].ToString());
                    obj.PACK_UNIT = FeeDetailList.Rows[i][34].ToString();
                    obj.MIN_UNIT = FeeDetailList.Rows[i][35].ToString();
                    obj.DRUG_TYPE = FeeDetailList.Rows[i][36].ToString();
                    obj.DRUG_QUALITY = FeeDetailList.Rows[i][37].ToString();
                    obj.FEE_CODE = int.Parse(FeeDetailList.Rows[i][38].ToString());
                    obj.FEE_NAME = FeeDetailList.Rows[i][39].ToString();
                    obj.TOT_COST = decimal.Parse(FeeDetailList.Rows[i][40].ToString());
                    obj.OWN_COST = decimal.Parse(FeeDetailList.Rows[i][41].ToString());
                    obj.PAY_COST = decimal.Parse(FeeDetailList.Rows[i][42].ToString());
                    obj.PUB_COST = decimal.Parse(FeeDetailList.Rows[i][43].ToString());
                    obj.REB_COST = decimal.Parse(FeeDetailList.Rows[i][44].ToString());
                    obj.DET_COST = decimal.Parse(FeeDetailList.Rows[i][45].ToString());
                    obj.CENTER_TOT_COST = decimal.Parse(FeeDetailList.Rows[i][46].ToString());
                    obj.CANCEL_RECEIPT_NO = FeeDetailList.Rows[i][47].ToString();
                    obj.CANCEL_SEQUENCE_NO = int.Parse(FeeDetailList.Rows[i][48].ToString());
                    obj.OPER_CODE = FeeDetailList.Rows[i][49].ToString();
                    obj.OPER_DATE = DateTime.Parse(FeeDetailList.Rows[i][50].ToString());
                    obj.UPLOAD_FLAG = FeeDetailList.Rows[i][51].ToString();
                    obj.UPLOAD_DATE = DateTime.Parse(FeeDetailList.Rows[i][52].ToString());
                    obj.TRANS_TYPE = FeeDetailList.Rows[i][53].ToString();
                    obj.CENTER_OWN_COST = decimal.Parse(FeeDetailList.Rows[i][54].ToString());
                    obj.CENTER_OWNFEE_COST = decimal.Parse(FeeDetailList.Rows[i][55].ToString());
                    obj.EXCEEDLIMIT_OWNFEECOST = decimal.Parse(FeeDetailList.Rows[i][56].ToString());
                    obj.CENTER_PUB_COST = decimal.Parse(FeeDetailList.Rows[i][57].ToString());
                    obj.CENTER_REPUB_COST = decimal.Parse(FeeDetailList.Rows[i][58].ToString());
                    obj.CENTER_ITEM_GRADE = FeeDetailList.Rows[i][59].ToString();
                    obj.CENTER_ALLOWN_FALG = FeeDetailList.Rows[i][60].ToString();
                    obj.CENTER_DOCTOR_ABNORMAL_FALG = FeeDetailList.Rows[i][61].ToString();
                    obj.DRUG_COMMON_LIMIT_FLAG = FeeDetailList.Rows[i][62].ToString();
                    obj.DRUG_SPECIAL_LIMIT_FLAG = FeeDetailList.Rows[i][63].ToString();
                    obj.MATERIAL_LIMITUSE_FLAG = FeeDetailList.Rows[i][64].ToString();
                    obj.MATERIAL_SITECODE = FeeDetailList.Rows[i][65].ToString();
                    obj.RATE = decimal.Parse(FeeDetailList.Rows[i][66].ToString());
                    obj.BALANCE_FLAG = FeeDetailList.Rows[i][67].ToString();
                    list.Add(obj);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return list;
        }
        #endregion

        #region 获取所有费用明细
        /// <summary>
        /// 获取所有费用明细
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<INP_BILL_DETAIL> GetBalanceDetails(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<INP_BILL_DETAIL> list = new List<INP_BILL_DETAIL>();
            try
            {
                #region
                string sql = @"select t.patient_id,
                                       t.visit_id,
                                       t.item_no,
                                       t.item_class,
                                       t.item_name,
                                       t.item_code,
                                       t.item_spec,
                                       t.amount,
                                       t.units,
                                       t.ordered_by,
                                       t.performed_by,
                                       t.costs,
                                       t.charges,
                                       t.billing_date_time,
                                       t.operator_no,
                                       t.rcpt_no,
                                       t.up_flag,
                                       t.up_time_date,
                                       t.up_operator_no,
                                       t.formularyno,
                                       t.doctor,
                                       t.CLASS_ON_INP_RCPT,
                                       t.common_flag,
                                       t.special_flag,
                                       t.return_flag,
                                       t.center_code
                                  from INP_BILL_DETAIL t
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'
                                   --and t.rcpt_no is null
                                    order by t.costs desc";
                sql = string.Format(sql, patientID, visitID);
                #endregion

                dt = BaseEntityer.Db.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    #region 赋值
                    INP_BILL_DETAIL bill = new INP_BILL_DETAIL();
                    bill.PATIENT_ID = dr.ItemArray[0].ToString();
                    bill.VISIT_ID = int.Parse(dr.ItemArray[1].ToString());
                    bill.ITEM_NO = int.Parse(dr.ItemArray[2].ToString());
                    bill.ITEM_CLASS = dr.ItemArray[3].ToString();
                    bill.ITEM_NAME = dr.ItemArray[4].ToString();
                    bill.ITEM_CODE = dr.ItemArray[5].ToString();
                    bill.ITEM_SPEC = dr.ItemArray[6].ToString();
                    bill.AMOUNT = decimal.Parse(dr.ItemArray[7].ToString());
                    bill.UNITS = dr.ItemArray[8].ToString();
                    bill.ORDERED_BY = dr.ItemArray[9].ToString();
                    bill.PERFORMED_BY = dr.ItemArray[10].ToString();
                    bill.COSTS = decimal.Parse(dr.ItemArray[11].ToString());
                    bill.CHARGES = decimal.Parse(dr.ItemArray[12].ToString());
                    bill.BILLING_DATE_TIME = DateTime.Parse(dr.ItemArray[13].ToString());
                    bill.OPERATOR_NO = dr.ItemArray[14].ToString();
                    bill.RCPT_NO = dr.ItemArray[15].ToString();
                    bill.UP_FLAG = dr.ItemArray[16].ToString();
                    bill.UP_TIME_DATE = string.IsNullOrEmpty(dr.ItemArray[17].ToString()) == true ? DateTime.MinValue.Date : DateTime.Parse(dr.ItemArray[17].ToString()).Date;
                    bill.UP_OPERATOR_NO = dr.ItemArray[18].ToString();
                    bill.FORMULARYNO = dr.ItemArray[19].ToString();
                    bill.DOCTOR = dr.ItemArray[20].ToString();
                    bill.CLASS_ON_INP_RCPT = dr.ItemArray[21].ToString();
                    bill.Common_flag = dr.ItemArray[22].ToString();
                    bill.Special_flag = dr.ItemArray[23].ToString();
                    bill.RETURN_FLAG = dr.ItemArray[24].ToString();
                    bill.Center_code =  dr.ItemArray[25].ToString() ;
                    list.Add(bill);
                    #endregion
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return null;
            }
            return list;
        }
        #endregion

        #region 获取费用明细最大处方号
        /// <summary>
        /// 获取最大处方号
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public string GetMaxReceiptDetails(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            string maxReceipt = string.Empty;
            try
            {
                #region
                string sql = @"select nvl(max(to_number(RECEIPT_NO)),0)
                                  from SI_SYFEEDETAILS t
                                 where PATIENT_ID = '{0}'
                                   and VISIT_ID = '{1}'";
                sql = string.Format(sql, patientID, visitID);
                #endregion

                dt = BaseEntityer.Db.GetDataTable(sql);

                maxReceipt = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return null;
            }
            return maxReceipt;
        }
        #endregion

        #region 删除医保明细表
        /// <summary>
        /// 删除医保明细表
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DeleteFeeDetailsALL(string patientid, string visitid, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"delete from SI_SYFEEDETAILS
                           where PATIENT_ID = '{0}' 
                           and VISIT_ID = '{1}' 
                           and (BALANCE_FLAG != '1' or BALANCE_FLAG is null)";
                sql = string.Format(sql, patientid, visitid);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 修改医保明细表是否结算标志
        /// <summary>
        /// 修改医保明细表是否结算标志
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int ModifyFeeDetailsBALANCE_FLAG(string patientid, string visitid, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"update SI_SYFEEDETAILS set BALANCE_FLAG='1' 
                           where PATIENT_ID = '{0}' 
                           and VISIT_ID = '{1}' 
                           and BALANCE_FLAG != '1'";
                sql = string.Format(sql, patientid, visitid);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 插入明细表单条
        /// <summary>
        /// 插入明细表单条
        /// </summary>
        /// <param name="feeDetail"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertFeeDetail(BaseEntityer db, SI_SYFEEDETAILS feeDetail, ref string err)
        {
            int exec = 0;
            try
            {
                string sql =
                #region
 @"insert into SI_SYFEEDETAILS
  (PATIENT_ID,
   VISIT_ID,
   NAME,
   PACT_CODE,
   PACT_NAME,
   INHOS_DEPT_CODE,
   INHOS_DEPT_NAME,
   RUNNING_NO,
   ITEM_TYPE,
   CENTER_CLASS,
   RECEIPT_NO,
   RECEIPT_DATE,
   ITEM_CODE,
   CENTER_CODE,
   ITEM_NAME,
   PRICE,
   QTY,
   DOSE_FORM_CODE,
   DOSE_FORM_NAME,
   SPECS,
   ONCE_DOSAGE,
   FREQUENCY,
   RECEIPT_DOCTOR_CODE,
   RECEIPT_DOCTOR_NAME,
   DOCTOR_CARDNO,
   USAGE,
   UNIT,
   DEPT_CODE,
   DEPT_NAME,
   DAYS,
   SEQUENCE_NO,
   HERB_COMPOUND_FLAG,
   DOSAGE_UNIT,
   PACK_QTY,
   PACK_UNIT,
   MIN_UNIT,
   DRUG_TYPE,
   DRUG_QUALITY,
   FEE_CODE,
   FEE_NAME,
   TOT_COST,
   OWN_COST,
   PAY_COST,
   PUB_COST,
   REB_COST,
   DET_COST,
   CENTER_TOT_COST,
   CANCEL_RECEIPT_NO,
   CANCEL_SEQUENCE_NO,
   OPER_CODE,
   OPER_DATE,
   UPLOAD_FLAG,
   UPLOAD_DATE,
   TRANS_TYPE,
   CENTER_OWN_COST,
   CENTER_OWNFEE_COST,
   EXCEEDLIMIT_OWNFEECOST,
   CENTER_PUB_COST,
   CENTER_REPUB_COST,
   CENTER_ITEM_GRADE,
   CENTER_ALLOWN_FALG,
   CENTER_DOCTOR_ABNORMAL_FALG,
   DRUG_COMMON_LIMIT_FLAG,
   DRUG_SPECIAL_LIMIT_FLAG,
   MATERIAL_LIMITUSE_FLAG,
   MATERIAL_SITECODE,
   RATE,
   BALANCE_FLAG)
values
  ('{0}',
   '{1}',
   '{2}',
   '{3}',
   '{4}',
   '{5}',
   '{6}',
   '{7}',
   '{8}',
   '{9}',
   '{10}',
   to_date('{11}','yyyy-mm-dd hh24:mi:ss'),
   '{12}',
   '{13}',
   '{14}',
   '{15}',
   '{16}',
   '{17}',
   '{18}',
   '{19}',
   '{20}',
   '{21}',
   '{22}',
   '{23}',
   '{24}',
   '{25}',
   '{26}',
   '{27}',
   '{28}',
   '{29}',
   '{30}',
   '{31}',
   '{32}',
   '{33}',
   '{34}',
   '{35}',
   '{36}',
   '{37}',
   '{38}',
   '{39}',
   '{40}',
   '{41}',
   '{42}',
   '{43}',
   '{44}',
   '{45}',
   '{46}',
   '{47}',
   '{48}',
   '{49}',
   to_date('{50}','yyyy-MM-dd hh24:mi:ss'),
   '{51}',
   to_date('{52}','yyyy-MM-dd hh24:mi:ss'),
   '{53}',
   '{54}',
   '{55}',
   '{56}',
   '{57}',
   '{58}',
   '{59}',
   '{60}',
   '{61}',
   '{62}',
   '{63}',
   '{64}',
   '{65}',
   '{66}',
   '0')
";
                #endregion
                sql = string.Format(sql,
                #region
 feeDetail.PATIENT_ID
                    , feeDetail.VISIT_ID
                    , feeDetail.NAME
                    , feeDetail.PACT_CODE
                    , feeDetail.PACT_NAME
                    , feeDetail.INHOS_DEPT_CODE
                    , feeDetail.INHOS_DEPT_NAME
                    , feeDetail.RUNNING_NO
                    , feeDetail.ITEM_TYPE
                    , feeDetail.CENTER_CLASS
                    , feeDetail.RECEIPT_NO
                    , feeDetail.RECEIPT_DATE
                    , feeDetail.ITEM_CODE
                    , feeDetail.CENTER_CODE
                    , feeDetail.ITEM_NAME
                    , feeDetail.PRICE
                    , feeDetail.QTY
                    , feeDetail.DOSE_FORM_CODE
                    , feeDetail.DOSE_FORM_NAME
                    , feeDetail.SPECS
                    , feeDetail.ONCE_DOSAGE
                    , feeDetail.FREQUENCY
                    , feeDetail.RECEIPT_DOCTOR_CODE
                    , feeDetail.RECEIPT_DOCTOR_NAME
                    , feeDetail.DOCTOR_CARDNO
                    , feeDetail.USAGE
                    , feeDetail.UNIT
                    , feeDetail.DEPT_CODE
                    , feeDetail.DEPT_NAME
                    , feeDetail.DAYS
                    , feeDetail.SEQUENCE_NO
                    , feeDetail.HERB_COMPOUND_FLAG
                    , feeDetail.DOSAGE_UNIT
                    , feeDetail.PACK_QTY
                    , feeDetail.PACK_UNIT
                    , feeDetail.MIN_UNIT
                    , feeDetail.DRUG_TYPE
                    , feeDetail.DRUG_QUALITY
                    , feeDetail.FEE_CODE
                    , feeDetail.FEE_NAME
                    , feeDetail.TOT_COST
                    , feeDetail.OWN_COST
                    , feeDetail.PAY_COST
                    , feeDetail.PUB_COST
                    , feeDetail.REB_COST
                    , feeDetail.DET_COST
                    , feeDetail.CENTER_TOT_COST
                    , feeDetail.CANCEL_RECEIPT_NO
                    , feeDetail.CANCEL_SEQUENCE_NO
                    , feeDetail.OPER_CODE
                    , feeDetail.OPER_DATE
                    , feeDetail.UPLOAD_FLAG
                    , feeDetail.UPLOAD_DATE
                    , feeDetail.TRANS_TYPE
                    , feeDetail.CENTER_OWN_COST
                    , feeDetail.CENTER_OWNFEE_COST
                    , feeDetail.EXCEEDLIMIT_OWNFEECOST
                    , feeDetail.CENTER_PUB_COST
                    , feeDetail.CENTER_REPUB_COST
                    , feeDetail.CENTER_ITEM_GRADE
                    , feeDetail.CENTER_ALLOWN_FALG
                    , feeDetail.CENTER_DOCTOR_ABNORMAL_FALG
                    , feeDetail.DRUG_COMMON_LIMIT_FLAG
                    , feeDetail.DRUG_SPECIAL_LIMIT_FLAG
                    , feeDetail.MATERIAL_LIMITUSE_FLAG
                    , feeDetail.MATERIAL_SITECODE
                    , feeDetail.RATE
                #endregion
);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 修改明细表上传标志
        /// <summary>
        /// 修改明细表上传标志
        /// </summary>
        /// <param name="feeDetail"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateBillUpLoadFlag(BaseEntityer db, INP_BILL_DETAIL feeDetail, ref string err)
        {
            int exec = 0;
            try
            {
                string sql =
                #region
 @"update INP_BILL_DETAIL
  set up_flag = '{3}',up_time_date = to_date('{4}','yyyy-MM-dd hh24:mi:ss'),up_operator_no = '{5}' 
where PATIENT_ID = '{0}' and VISIT_ID = '{1}' and ITEM_NO = '{2}'
";
                #endregion
                sql = string.Format(sql,
                #region
 feeDetail.PATIENT_ID
 , feeDetail.VISIT_ID.ToString()
 , feeDetail.ITEM_NO.ToString()//FORMULARYNO.ToString()
 , feeDetail.UP_FLAG
 , feeDetail.UP_TIME_DATE
 , feeDetail.UP_OPERATOR_NO 
                #endregion
);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }

        public int UpdateBillUpLoadFlagNH(BaseEntityer db, INP_BILL_DETAIL feeDetail, ref string err)
        {
            int exec = 0;
            try
            {
                string sql =
                #region
                     @" update INP_BILL_DETAIL t
                       set up_flag        = '{3}',
                           up_time_date   = to_date('{4}', 'yyyy-MM-dd hh24:mi:ss'),
                           up_operator_no = '{5}',
                           center_code = '{6}'
                     where PATIENT_ID = '{0}'
                       and VISIT_ID = '{1}'
                       and ITEM_NO = '{2}' ";
                #endregion
                sql = string.Format(sql,
                #region
 feeDetail.PATIENT_ID
 , feeDetail.VISIT_ID.ToString()
 , feeDetail.ITEM_NO.ToString()
 , feeDetail.UP_FLAG
 , feeDetail.UP_TIME_DATE
 , feeDetail.UP_OPERATOR_NO
 , feeDetail.Center_code
                 #endregion
);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 修改明细表上传标志
        /// <summary>
        /// 修改明细表上传标志
        /// </summary>
        /// <param name="feeDetail"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateBillUpLoadFlagALL(BaseEntityer db, string patientID, string visitID, ref string err)
        {
            int exec = 0;
            try
            {
                string sql =
                #region
 @"update INP_BILL_DETAIL
  set up_flag = '0',up_time_date = '',up_operator_no = ''
where PATIENT_ID = '{0}' and VISIT_ID = '{1}'
";
                #endregion
                sql = string.Format(sql,
                             patientID
                             , visitID
                            );
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 修改在院患者统筹
        /// <summary>
        /// 修改在院患者统筹
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="pubCost"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateInpatientPubCost(BaseEntityer db, string patientID, string visitID, string charge, ref string err)
        {
            int exec = 0;
            try
            {
                string sql =
                #region
 @"update pats_in_hospital t
   set t.total_charges = '{2}'
 where t.patient_id = '{0}'
   and t.visit_id = '{1}'
";
                #endregion
                sql = string.Format(sql,
                             patientID
                             , visitID
                             , charge
                            );
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }

        /// <summary>
        /// 修改患者的在院统筹信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="pubCost"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateInpatientVisitPubCost(BaseEntityer db, string patientID, string visitID, string pubCost, ref string err)
        {
            int exec = 0;
            try
            {
                string sql =
                #region
 @"update PAT_VISIT t
   set t.PUB_COST = '{2}'
 where t.patient_id = '{0}'
   and t.visit_id = '{1}'
";
                #endregion
                sql = string.Format(sql,
                             patientID
                             , visitID
                             , pubCost
                            );
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 插入门诊明细表单条
        /// <summary>
        /// 插入门诊明细表单条
        /// </summary>
        /// <param name="feeDetail"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertMZFeeDetail(BaseEntityer db, SI_SYMZFEEDETAILS feeDetail, ref string err)
        {
            int exec = 0;
            try
            {
                string sql =
                #region
 @"insert into SI_SYMZFEEDETAILS
  (ITEM_SERIAL,
   ITEM_CODE,
   ITEM_NAME,
   RUNNING_NO,
   BALANCE_DATE,
   INVOICE,
   PRICE,
   QTY,
   ITEM_GRADE,
   RATE,
   ISMZTC_TYPE)
values
  ('{0}',
   '{1}',
   '{2}',
   '{3}',
   to_date('{4}','yyyy-mm-dd hh24:mi:ss'),
   '{5}',
   '{6}',
   '{7}',
   '{8}',
   '{9}',
   '{10}')
";
                #endregion
                sql = string.Format(sql,
                #region
 feeDetail.ITEM_SERIAL,
                    feeDetail.ITEM_CODE,
                    feeDetail.ITEM_NAME,
                    feeDetail.RUNNING_NO,
                    feeDetail.BALANCE_DATE,
                    feeDetail.INVOICE,
                    feeDetail.PRICE,
                    feeDetail.QTY,
                    feeDetail.ITEM_GRADE,
                    feeDetail.RATE,
                    feeDetail.ISMZTC_TYPE
                #endregion
);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 获取门诊病历上传信息
        /// <summary>
        /// 获取门诊病历上传信息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public DataTable GetMZMRUpload(string startDate, string endDate, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                #region
                string sql = @"select s.running_no,
                                   s.visit_id,
                                   s.medical_type,
                                   s.remark,
                                   t.doctor,
                                   c.visit_dept,
                                   t.diag_code,
                                   s.person_no,
                                   s.name,
                                   s.birthday,
                                   s.sex,
                                   s.inhospital_date,
                                   t.illness_desc,
                                   t.anamnesis,
                                   t.med_history,
                                   t.individudl,
                                   t.family_ill,
                                   t.check_body,
                                   t.printing,
                                   t.handle,
                                   t.treat,
                                   t.informed_consentform,
                                   t.recordpage,
                                   c.visit_dept,
                                   t.operate_date,
                                   t.operate_startdate,
                                   t.operate_enddate,
                                   t.preoperative_diag,
                                   t.intraoperative_diag,
                                   t.operate_name,
                                   t.operate_oper,
                                   t.helper,
                                   t.nurse,
                                   t.analgesist,
                                   t.analgesia_method,
                                   t.operate,
                                   s.shiftdate,
                                   t.isupload
                              from clinic_master c, outp_mr t,siinfo s
                             where t.visit_date = c.visit_date
                               and t.visit_no = c.visit_no
                               and s.visit_id = t.visit_no
                               and t.visit_date = s.shiftdate
                               and s.balance_date >= to_date('{0}', 'yyyy-mm-dd')
                               and s.balance_date <= to_date('{1}', 'yyyy-mm-dd')
                               and s.medical_type in ('16', '18')
                               and s.pact_code = '2'
                               and s.isbalanced = '1'
                               and t.isupload = '0'";
                sql = string.Format(sql, startDate, endDate);
                #endregion

                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errmsg = "没有查询到门诊统筹病历信息";
                    return null;
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return null;
            }
            return dt;
        }


        #endregion

        #region 获取病历上传信息 2014年10月24日

        /// <summary>
        /// 获得电子病历人员基本信息
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetMedicalRecordPatientInfoUpdate(ref string errMsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select * from MEDICALRECORDPATIENTINFO t";

                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 获得电子病历疾病诊断上传信息
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetMedicalRecordDiagnosisInfoUpdate(ref string errMsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT * FROM V_MEDICALRECORDDIAGNOSE t";

                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return dt;
        }
        #endregion

        #region 修改门诊统筹病历上传状态
        /// <summary>
        /// 修改门诊统筹病历上传状态
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="charge"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateMZMRUpload(BaseEntityer db, string visitDate, string visitNo, string isUpload, ref string err)
        {
            int exec = 0;
            try
            {
                string sql =
                #region
 @"update outp_mr t set t.isupload='{2}' where t.visit_date=to_date('{0}','yyyy-mm-dd') and t.visit_no='{1}'";
                #endregion
                sql = string.Format(sql
                             , visitDate
                             , visitNo
                             , isUpload
                            );
                exec = db.ExecuteNonQuery(sql);
                if (exec < 0)
                {
                    err = "修改上传标志信息错误，" + sql;
                    return -1;
                }
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion
        #endregion

        #region 沈阳农合接口

        /// <summary>
        /// 获取中心病种项目数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetCenterDiagnosis(string charge_type_code)
        {
            string sql = @"select DIAGNOSE_CODE AS 诊断编码,
                           DIAGNOSE_NAME AS 诊断名称,
                           DIAGNOSE_SPELL AS 拼音助记码,
                           DIAGNOSE_TYPE AS 诊断类别
                           from SI_SYDIAGNOSE 
                           where CHARGE_TYPE_CODE = '{0}'";
            sql = string.Format(sql, charge_type_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 工伤审批条件
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visit_no"></param>
        /// <param name="visit_date"></param>
        /// <returns></returns>
        public int GetGongSangShenPi(string patientid, string visit_no, string visit_date)
        {
            DataTable dt = new DataTable();
            //string sql = @"select * from outp_industrial_injury i where i.clinic_no='{0}' and i.visit_no='{1}' and i.visit_date=to_date('{2}','yyyy-MM-dd hh24:mi:ss')";
            string sql = @"select * from outp_industrial_injury i where i.clinic_no='{0}'";
            sql = string.Format(sql, patientid);
            int revString = 0;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                revString = dt.Rows.Count;
            }
            catch
            {
                return -1;
            }
            return revString;
        }
        #endregion

        #region 沈阳水库移民接口

        /// <summary>
        /// 获取水库移民充值交易发票流水号
        /// </summary>
        /// <returns></returns>
        public string GetRechargeRcptNo()
        {
            DataTable dt = new DataTable();
            string sql = @"select RECHARGE_SEQ.nextval from dual";
            string revString = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                revString = dt.Rows[0][0].ToString();
            }
            catch
            {
                return null;
            }
            return revString;
        }

        /// <summary>
        /// 水库移民交易记录数据插入
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction_record"></param>
        /// <returns></returns>
        public int InsertTransaction_Record(BaseEntityer db, TRANSACTION_RECORD transaction_record)
        {
            string sql = @"INSERT INTO TRANSACTION_RECORD
                            (
                                  TRANSACTION_RECORD.RCPT_NO,
                                  TRANSACTION_RECORD.PATIENT_ID,
                                  TRANSACTION_RECORD.RCPT_DATE,
                                  TRANSACTION_RECORD.TOTAL_CHARGES,
                                  TRANSACTION_RECORD.OPERATOR,
                                  TRANSACTION_RECORD.REMARK 
                            )
                            VALUES
                            (     '{0}','{1}',to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'),{3},'{4}','{5}' )";
            object[] param = new object[] { transaction_record.RCPT_NO, transaction_record.PATIENT_ID, 
                transaction_record.RCPT_DATE, transaction_record.TOTAL_CHARGES, 
                transaction_record.OPERATOR, transaction_record.REMARK};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 水库移民储值卡扣取交易金额
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patient_id"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        public int UpdatePatient_Balance(BaseEntityer db, string patient_id, decimal balance)
        {
            string sql = @"UPDATE PAT_MASTER_INDEX
                            SET PAT_MASTER_INDEX.BALANCE = {1} 
                            WHERE PAT_MASTER_INDEX.PATIENT_ID = '{0}'";
            sql = string.Format(sql, patient_id, balance);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取水库移民病患储值卡余额
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patient_id"></param>
        /// <returns></returns>
        public DbDataReader GetPatientBalance(BaseEntityer db, string patient_id)
        {
            string sql = @"SELECT PAT_MASTER_INDEX.BALANCE FROM PAT_MASTER_INDEX WHERE 
                            PAT_MASTER_INDEX.PATIENT_ID = '{0}'";
            sql = string.Format(sql, patient_id);
            return db.ExecuteReader(sql);
        }

        #endregion

        #region 四平市农合接口
        /// <summary>
        /// 获取出院诊断
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public BringSpringObject GetOutBalanceDiagnose(string patient, string visit, ref string errMsg)
        {
            DataTable dt = new DataTable();
            BringSpringObject obj = new BringSpringObject();
            string sql = @"select t.diag_code, t.diagnosis_desc
                          from diagnosis t
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}'
                           and t.diagnosis_type = '3'";
            string revString = string.Empty;
            try
            {
                sql = string.Format(sql, patient, visit);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;

                obj.ID = dt.Rows[0][0].ToString();
                obj.Name = dt.Rows[0][1].ToString();

            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return obj;
        }

        /// <summary>
        /// 修改出院诊断
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="visit"></param>
        /// <param name="obj"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateOutBalanceDiagnose(BaseEntityer Db, string patient, string visit, BringSpringObject obj, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"update diagnosis t set t.diag_code='{2}', t.diagnosis_desc='{3}'
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}'
                           and t.diagnosis_type = '3'
                           and t.diagnosis_no = (select min(s.diagnosis_no)
                                                  from diagnosis s
                                                 where s.patient_id = '{0}'
                                                   and s.visit_id = '{1}'
                                                   and s.diagnosis_type = '3')";
            string revString = string.Empty;
            try
            {
                sql = string.Format(sql, patient, visit, obj.ID, obj.Name);
                int revInt = Db.ExecuteNonQuery(sql);
                if (revInt <= 0)
                {
                    errMsg = "修改失败，没有查询到患者出院诊断信息";
                    return -1;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 修改出入院日期
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="visit"></param>
        /// <param name="obj"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateInOutDateInfo(BaseEntityer Db, string patient, string visit, BringSpringObject obj, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"update pat_visit t
                           set t.admission_date_time = to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'),
                               t.discharge_date_time = to_date('{3}', 'yyyy-mm-dd hh24:mi:ss')
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}'";
            string revString = string.Empty;
            try
            {
                sql = string.Format(sql, patient, visit, obj.ID, obj.Name);
                int revInt = Db.ExecuteNonQuery(sql);
                if (revInt <= 0)
                {
                    errMsg = "修改失败，没有查询到患者信息";
                    return -1;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return -1;
        }

        /// <summary>
        /// 获取特殊病种明细
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<BringSpringObject> GetSpecialDiagnose(string Type, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            string sql = @"select c.diagnose_code 病种编码,c.diagnose_name 病种名称,c.spell 拼音码 from si_spspecialdiagnose c 
                            where (c.type='{0}' or 'ALL' = '{0}') and c.is_vaild='1'";
            string revString = string.Empty;
            try
            {
                sql = string.Format(sql, Type);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = dt.Rows[i][0].ToString();
                    obj.Name = dt.Rows[i][1].ToString();
                    obj.Memo = dt.Rows[i][2].ToString();
                    objlist.Add(obj);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return objlist;
        }

        #region 获取医保对照信息（上传用）
        /// <summary>
        /// 获取医保对照信息（上传用）
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSPHisCompareUpLoad(string typeCode, ref string errMsg)
        {
            DataTable compareList = new DataTable();
            string sql =
            #region
 @"select t.his_code 项目编码,t.his_name 项目名称,'' 产地,'' 批准文号,t.HIS_TYPE HIS剂型,
t.HIS_SPECS 规格,t.HIS_UNIT 单位,'' 包装,'' 含量,t.center_code 中心编码,t.center_name 中心名称,t.center_type 中心剂型,t.fee_type 类型,ISNEED_SITECODE 上报标志
from his_compare t 
where t.charge_type_code='{0}'";
            #endregion
            sql = string.Format(sql, typeCode);
            try
            {
                compareList = BaseEntityer.Db.GetDataTable(sql);
                if (compareList.Rows.Count <= 0)
                {
                    errMsg = "没有查询到对照数据";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return compareList;
        }
        #endregion

        #region 农合门诊

        #region 按照发票号获取人员信息
        /// <summary>
        /// 按照发票号获取人员信息
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<OUTP_RCPT_MASTER> GetOutMasterBySPNH(string invoice, ref string errMsg)
        {
            DataSet ds = new DataSet();
            List<OUTP_RCPT_MASTER> objmaster = new List<OUTP_RCPT_MASTER>();
            string sql = @"select * from outp_rcpt_master t where t.invoice_new='{0}'";
            string revString = string.Empty;
            try
            {
                sql = string.Format(sql, invoice);
                ds = BaseEntityer.Db.GetDataSet(sql);
                if (ds.Tables.Count <= 0)
                {
                    errMsg = "按照发票号没有找到患者信息。";
                    return null;
                }
                objmaster = DataSetToEntity.DataSetToT<OUTP_RCPT_MASTER>(ds).ToList();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return objmaster;
        }
        #endregion

        #region 按照病人ID与姓名获取人员信息
        /// <summary>
        /// 按照病人ID与姓名获取人员信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="name"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<PAT_MASTER_INDEX> GetOutIndexBySPNH(string patientid, string name, ref string errMsg)
        {
            DataSet ds = new DataSet();
            List<PAT_MASTER_INDEX> objindex = new List<PAT_MASTER_INDEX>();
            string sql = @"select * from pat_master_index f where f.patient_id='{0}' and f.name='{1}'";
            string revString = string.Empty;
            try
            {
                sql = string.Format(sql, patientid, name);
                ds = BaseEntityer.Db.GetDataSet(sql);
                if (ds.Tables.Count <= 0)
                {
                    return null;
                }
                objindex = DataSetToEntity.DataSetToT<PAT_MASTER_INDEX>(ds).ToList();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return objindex;
        }
        #endregion

        #region 按照发票号获取患者明细
        /// <summary>
        /// 按照发票号获取患者明细
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<OUTP_BILL_ITEMS> GetOutItemsBySPNH(string invoice, ref string errMsg)
        {
            DataSet ds = new DataSet();
            List<OUTP_BILL_ITEMS> objitems = new List<OUTP_BILL_ITEMS>();
            string sql = @"select * from outp_bill_items s where s.invoice_new = '{0}'  and s.price<>0";
            string revString = string.Empty;
            try
            {
                sql = string.Format(sql, invoice);
                ds = BaseEntityer.Db.GetDataSet(sql);
                if (ds.Tables.Count <= 0)
                {
                    errMsg = "该发票没有查到费用明细。";
                    return null;
                }
                objitems = DataSetToEntity.DataSetToT<OUTP_BILL_ITEMS>(ds).ToList();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return objitems;
        }
        #endregion

        #region 获取门诊病历信息
        /// <summary>
        /// 获取门诊病历信息
        /// </summary>
        /// <param name="visitDate"></param>
        /// <param name="visitNO"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<OUTP_MR> GetOutMrBySPNH(string visitDate, string visitNO, ref string errMsg)
        {
            DataSet ds = new DataSet();
            List<OUTP_MR> objmr = new List<OUTP_MR>();
            string sql = @"select * from outp_mr r where r.visit_no = '{1}' and r.visit_date = to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
            string revString = string.Empty;
            try
            {
                sql = string.Format(sql, visitDate, visitNO);
                ds = BaseEntityer.Db.GetDataSet(sql);
                if (ds.Tables.Count <= 0)
                {
                    errMsg = "没有找到患者病历信息。";
                    return null;
                }
                objmr = DataSetToEntity.DataSetToT<OUTP_MR>(ds).ToList();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return objmr;
        }
        #endregion


        #endregion//农合门诊
        #endregion

        #region 四平预交金发票
        /// <summary>
        /// 四平预交金发票序号
        /// </summary>
        /// <param name="SEQ_NO">日志对象</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns>1、成功 0、异常 -1、失败</returns>
        public int GetPrepayInvoice(BaseEntityer db, ref PREPAYINVOICE prepay, ref string pErrMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select nvl(max(f.seq_no),0) from PREPAYINVOICE f";
            string sql2 = @"insert into PREPAYINVOICE
                              (SEQ_NO, PATIENT_ID, VISIT_ID, INVOICE_NO, OPER_CODE, OPER_DATE)
                            values
                              ('{0}',
                               '{1}',
                               '{2}',
                               '{3}',
                               '{4}',
                               to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'))";

            try
            {
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                prepay.SEQ_NO = decimal.Parse(dt.Rows[0][0].ToString());
                prepay.SEQ_NO++;
                sql2 = string.Format(sql2, prepay.SEQ_NO, prepay.PATIENT_ID, prepay.VISIT_ID, prepay.INVOICE_NO, prepay.OPER_CODE, prepay.OPER_DATE);
                int ext = db.ExecuteNonQuery(sql2);
                if (ext <= 0)
                {
                    pErrMsg = "插入PREPAYINVOICE表失败。" + sql2;
                    return -1;
                }
            }
            catch (Exception e)
            {
                pErrMsg = "插入PREPAYINVOICE表失败，失败原因：" + e.Message + "|" + e.StackTrace;
                return -1;
            }
            return 1;
        }
        #endregion

        #region "葫芦岛"
        /// <summary>
        /// 得到科室名称
        /// </summary>
        /// <param name="dept_code"></param>
        /// <returns></returns>
        public string GetDeptNameByCode(string dept_code)
        {
            DataTable dt = new DataTable();
            string sql = @"select DEPT_NAME from  dept_dict where dept_code='{0}'";
            sql = string.Format(sql, dept_code);
            string revString;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return "";
                revString = dt.Rows[0][0].ToString();
            }
            catch
            {
                return "";
            }
            return revString;
        }
        /// <summary>
        /// 读取用户name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserNameByID(string id)
        {
            DataTable dt = new DataTable();
            string sql = @"select USER_NAME from users_staff_dict  where USER_ID='{0}'";
            sql = string.Format(sql, id);
            string revString;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return "";
                revString = dt.Rows[0][0].ToString();
            }
            catch
            {
                return "";
            }
            return revString;
        }
        /// <summary>
        /// 得到住院号
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <returns></returns>
        public string GetInpNOeByPATIENTID(string PATIENT_ID)
        {
            DataTable dt = new DataTable();
            string sql = @"select INP_NO from  pat_master_index  where PATIENT_ID='{0}'";
            sql = string.Format(sql, PATIENT_ID);
            string revString;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return "";
                revString = dt.Rows[0][0].ToString();
            }
            catch
            {
                return "";
            }
            return revString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<INP_BILL_DETAIL> GetBalanceDetailsByHLD(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<INP_BILL_DETAIL> list = new List<INP_BILL_DETAIL>();
            try
            {
                #region
                string sql = @"select t.patient_id,
                                       t.visit_id,
                                       t.item_no,
                                       t.item_class,
                                       t.item_name,
                                       t.item_code,
                                       t.item_spec,
                                       t.amount,
                                       t.units,
                                       t.ordered_by,
                                       t.performed_by,
                                       t.costs,
                                       t.charges,
                                       t.billing_date_time,
                                       t.operator_no,
                                       t.rcpt_no,
                                       t.up_flag,
                                       t.up_time_date,
                                       t.up_operator_no,
                                       t.formularyno,
                                       t.doctor,
                                       t.CLASS_ON_INP_RCPT,
                                       t.common_flag,
                                       t.special_flag,
                                       t.return_flag,
                                       (select case
                                                 when t.costs < 0 then
                                                  m.center_code
                                                 else
                                                  t.center_code
                                               end
                                          from INP_BILL_DETAIL m
                                     where m.item_code = t.item_code
                                       and m.formularyno = t.formularyno
                                       and m.costs > 0) center_code,
                                   t.orders_no
                                  from INP_BILL_DETAIL t
                                 where t.patient_id = '{0}' 
                                   and t.visit_id = '{1}' 
                                   and t.rcpt_no is null 
                                    AND t.UP_FLAG='0'
                                    order by t.costs desc";
                sql = string.Format(sql, patientID, visitID);
                #endregion

                dt = BaseEntityer.Db.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    #region 赋值
                    INP_BILL_DETAIL bill = new INP_BILL_DETAIL();
                    bill.PATIENT_ID = dr.ItemArray[0].ToString();
                    bill.VISIT_ID = int.Parse(dr.ItemArray[1].ToString());
                    bill.ITEM_NO = int.Parse(dr.ItemArray[2].ToString());
                    bill.ITEM_CLASS = dr.ItemArray[3].ToString();
                    bill.ITEM_NAME = dr.ItemArray[4].ToString();
                    bill.ITEM_CODE = dr.ItemArray[5].ToString();
                    bill.ITEM_SPEC = dr.ItemArray[6].ToString();
                    bill.AMOUNT = decimal.Parse(dr.ItemArray[7].ToString());
                    bill.UNITS = dr.ItemArray[8].ToString();
                    bill.ORDERED_BY = dr.ItemArray[9].ToString();
                    bill.PERFORMED_BY = dr.ItemArray[10].ToString();
                    bill.COSTS = decimal.Parse(dr.ItemArray[11].ToString());
                    bill.CHARGES = decimal.Parse(dr.ItemArray[12].ToString());
                    bill.BILLING_DATE_TIME = DateTime.Parse(dr.ItemArray[13].ToString());
                    bill.OPERATOR_NO = dr.ItemArray[14].ToString();
                    bill.RCPT_NO = dr.ItemArray[15].ToString();
                    bill.UP_FLAG = dr.ItemArray[16].ToString();
                    bill.UP_TIME_DATE = string.IsNullOrEmpty(dr.ItemArray[17].ToString()) == true ? DateTime.MinValue.Date : DateTime.Parse(dr.ItemArray[17].ToString()).Date;
                    bill.UP_OPERATOR_NO = dr.ItemArray[18].ToString();
                    bill.FORMULARYNO = dr.ItemArray[19].ToString();
                    bill.DOCTOR = dr.ItemArray[20].ToString();
                    bill.CLASS_ON_INP_RCPT = dr.ItemArray[21].ToString();
                    bill.Common_flag = dr.ItemArray[22].ToString();
                    bill.Special_flag = dr.ItemArray[23].ToString();
                    bill.RETURN_FLAG = dr.ItemArray[24].ToString();
                    bill.Center_code = dr.ItemArray[25].ToString();
                    bill.ORDERS_NO = dr.ItemArray[26].ToString();
                    list.Add(bill);
                    #endregion
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return null;
            }
            return list;
        }
        #endregion

        #region 一卡通接口

        /// <summary>
        /// 2013-11-21 by li 一卡通接口日志插入
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="log">日志对象</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns>1、成功 -1、异常</returns>
        public int InsertMedicalOneCard_Log(BaseEntityer db, MEDICALONECARD_LOG log, ref string pErrMsg)
        {
            int revInt = 0;
            string sql = @"INSERT INTO MEDICALONECARD_LOG
                              (MEDICALONECARD_LOG.SERIAL_NO,
                               MEDICALONECARD_LOG.OPERATOR_NO,
                               MEDICALONECARD_LOG.OPER_DATE,
                               MEDICALONECARD_LOG.FUNCTION_NAME,
                               MEDICALONECARD_LOG.FUNCTION_IN_PARAM,
                               MEDICALONECARD_LOG.FUNCTION_OUT_PARAM,
                               MEDICALONECARD_LOG.FUNCTION_RESULT,
                               MEDICALONECARD_LOG.FUNCTION_ERRMSG,
                               MEDICALONECARD_LOG.LOG_TYPE,
                               MEDICALONECARD_LOG.INTERFACE_NAME)
                            VALUES
                              ((select nvl(max(SERIAL_NO), 1000000000) + 1 from MEDICALONECARD_LOG),
                               '{0}',
                               to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'),
                               '{2}',
                               '{3}',
                               '{4}',
                               '{5}',
                               '{6}',
                               '{7}',
                               '{8}')";
            try
            {
                object[] param = new object[] { log.OPERATOR_NO, log.OPER_DATE, log.FUNCTION_NAME, 
                log.FUNCTION_IN_PARAM, log.FUNCTION_OUT_PARAM, log.FUNCTION_RESULT, 
                log.FUNCTION_ERRMSG, log.LOG_TYPE, log.INTERFACE_NAME };
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }

        /// <summary>
        /// 2013-11-22 by li 就诊卡号获取一卡通患者信息
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cardID">就诊卡号</param>
        /// <param name="card_Info">一卡通患者信息</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns>1、成功 -1、异常</returns>
        public int GetOneCardPatientInfoByCardId(BaseEntityer db, string cardID, ref OneCard_PatientInfo card_Info, ref string pErrMsg)
        {
            int revInt = 1;
            string sql = @"SELECT CARD_ID,
                                   CARD_SERIAL,
                                   SERIAL_NO,
                                   NAME,
                                   PASSWORD,
                                   BIRTHDAY,
                                   AGE,
                                   SEX,
                                   ID_CARD,
                                   HOME_ADD,
                                   LINK_PERSON,
                                   PHONE,
                                   OUT_PATIENTID,
                                   IN_PATIENTID,
                                   ACC_OUTBALANCE,
                                   ACC_INBALANCE,
                                   CARD_TYPE,
                                   CARD_STATE,
                                   YH_CARDNO,
                                   HIS_BUSSINESSNO,
                                   BANK_BUSSINESSNO,
                                   OPER_ID,
                                   OPER_NAME,
                                   OPER_DATE,
                                   CASH_PLEDGE
                              FROM ONECARD_PATIENTINFO
                             WHERE CARD_ID = {0} and CARD_STATE !='4'";
            try
            {
                sql = string.Format(sql, cardID);
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                var list = DataSetToEntity.DataSetToT<OneCard_PatientInfo>(ds);
                if (list.Count > 0)
                    card_Info = list[0];
                else
                    card_Info = null;
            }
            catch (Exception e)
            {
                card_Info = null;
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }

        /// <summary>
        /// 2013-11-25 by li 消费交易查询
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="rcptNo">消费交易发票号</param>
        /// <param name="visitType">就诊类别，1、门诊 0、住院</param>
        /// <param name="medicalOneCard_BillDetail">院内一卡通消费明细记录</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns>1、成功 -1、异常</returns>
        public int GetOneCardBillDetailByRcptNo(BaseEntityer db, string rcptNo, string visitType, ref MEDICALONECARD_BILLDETAIL medicalOneCard_BillDetail, ref string pErrMsg)
        {
            int revInt = 1;
            string sql = @"SELECT RCPT_NO,
                                   VISIT_TYPE,
                                   INVOICE_NEW,
                                   OPERATOR_NO,
                                   OPER_DATE,
                                   CARD_COSTS,
                                   CASH,
                                   PATIENT_ID,
                                   VISIT_ID
                              FROM MEDICALONECARD_BILLDETAIL
                             WHERE RCPT_NO = '{0}'
                               AND VISIT_TYPE = '{1}'";
            try
            {
                sql = string.Format(sql, rcptNo, visitType);
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                var list = DataSetToEntity.DataSetToT<MEDICALONECARD_BILLDETAIL>(ds);
                if (list.Count > 0)
                    medicalOneCard_BillDetail = list[0];
                else
                    medicalOneCard_BillDetail = null;
            }
            catch (Exception e)
            {
                medicalOneCard_BillDetail = null;
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }

        /// <summary>
        /// 2013-11-23 by li 患者ID获取一卡通患者信息
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="patientID">患者ID</param>
        /// <param name="visitType">就诊类别，1、门诊 0、住院</param>
        /// <param name="card_Info">一卡通患者信息</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns>1、成功 -1、异常</returns>
        public int GetOneCardPatientInfoByPatientId(BaseEntityer db, string patientID, string visitType, ref OneCard_PatientInfo card_Info, ref string pErrMsg)
        {
            //2014-4-11 by li 获取一卡通信息增加【开卡序号】
            int revInt = 1;
            string sql = string.Empty;
            //门诊
            if (visitType.Equals("1"))
            {
                sql = @"SELECT CARD_ID,
                            CARD_SERIAL,
                            SERIAL_NO,
                            NAME,
                            PASSWORD,
                            BIRTHDAY,
                            AGE,
                            SEX,
                            ID_CARD,
                            HOME_ADD,
                            LINK_PERSON,
                            PHONE,
                            OUT_PATIENTID,
                            IN_PATIENTID,
                            ACC_OUTBALANCE,
                            ACC_INBALANCE,
                            CARD_TYPE,
                            CARD_STATE,
                            YH_CARDNO,
                            HIS_BUSSINESSNO,
                            BANK_BUSSINESSNO,
                            OPER_ID,
                            OPER_NAME,
                            OPER_DATE,
                            CASH_PLEDGE
                        FROM ONECARD_PATIENTINFO
                             WHERE OUT_PATIENTID = {0} and card_state != '4'";
            }
            //住院
            else if (visitType.Equals("0"))
            {
                sql = @"SELECT CARD_ID,
                            CARD_SERIAL,
                            SERIAL_NO,
                            NAME,
                            PASSWORD,
                            BIRTHDAY,
                            AGE,
                            SEX,
                            ID_CARD,
                            HOME_ADD,
                            LINK_PERSON,
                            PHONE,
                            OUT_PATIENTID,
                            IN_PATIENTID,
                            ACC_OUTBALANCE,
                            ACC_INBALANCE,
                            CARD_TYPE,
                            CARD_STATE,
                            YH_CARDNO,
                            HIS_BUSSINESSNO,
                            BANK_BUSSINESSNO,
                            OPER_ID,
                            OPER_NAME,
                            OPER_DATE,
                            CASH_PLEDGE
                        FROM ONECARD_PATIENTINFO
                             WHERE IN_PATIENTID = {0}  and card_state != '4'";
            }
            else
            {
                card_Info = null;
                pErrMsg = "输入的就诊类别错误，不能查询到对应的患者信息！";
                revInt = 0;
                return revInt;
            }
            try
            {
                sql = string.Format(sql, patientID);
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                var list = DataSetToEntity.DataSetToT<OneCard_PatientInfo>(ds);
                if (list.Count > 0)
                {
                    card_Info = list[0];
                }
                else
                {
                    card_Info = null;
                }
            }
            catch (Exception e)
            {
                card_Info = null;
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }

        /// <summary>
        /// 2013-11-22 by li 更新一卡通患者信息
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="card_Info">一卡通患者信息</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns>1、成功 -1、异常</returns>
        public int UpdateOneCardPatientInfo(BaseEntityer db, OneCard_PatientInfo card_Info)
        {
            int revInt = 0;
            try
            {
                string sql = @"UPDATE ONECARD_PATIENTINFO
                                   SET SERIAL_NO        = '{2}',
                                       NAME             = '{3}',
                                       PASSWORD         = '{4}',
                                       BIRTHDAY         = to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'),
                                       AGE              = '{6}',
                                       SEX              = '{7}',
                                       ID_CARD          = '{8}',
                                       HOME_ADD         = '{9}',
                                       LINK_PERSON      = '{10}',
                                       PHONE            = '{11}',
                                       OUT_PATIENTID    = '{12}',
                                       IN_PATIENTID     = '{13}',
                                       ACC_OUTBALANCE   = '{14}',
                                       ACC_INBALANCE    = '{15}',
                                       CARD_TYPE        = '{16}',
                                       CARD_STATE       = '{17}',
                                       YH_CARDNO        = '{18}',
                                       HIS_BUSSINESSNO  = '{19}',
                                       BANK_BUSSINESSNO = '{20}',
                                       OPER_ID          = '{21}',
                                       OPER_NAME        = '{22}',
                                       OPER_DATE        = to_date('{23}', 'yyyy-mm-dd hh24:mi:ss'),
                                       CASH_PLEDGE      = '{24}'
                                 WHERE CARD_ID = '{0}' and CARD_SERIAL = '{1}'";
                object[] param = new object[] { card_Info.Card_id, card_Info.Card_Serial,card_Info.Serial, card_Info.Name, 
                    card_Info.Password, card_Info.Birthday, card_Info.Age, card_Info.Sex, card_Info.Id_card, 
                    card_Info.Home_add, card_Info.Link_person, card_Info.Phone, card_Info.Out_patientid, 
                    card_Info.In_patientid, card_Info.Acc_outbalance, card_Info.Acc_inbalance, 
                    card_Info.Card_type, card_Info.Card_state, card_Info.Yh_CardNo, card_Info.His_bussinessno, 
                    card_Info.Bank_bussinessno, card_Info.Oper_id, card_Info.Oper_name, card_Info.Oper_date ,card_Info.Cash_pledge};
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                db.Err = e.Message;
                revInt = -1;
            }
            return revInt;
        }

        public int GetNewCardNo(BaseEntityer db, string cardId)
        {
            string sql = @"select nvl(max(card_serial),10000) + 1 from ONECARD_PATIENTINFO where CARD_ID = '{0}'";
            sql = Utility.SqlFormate(sql, cardId);
            object obj = db.ExecuteScalar(sql);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            else
            {
                return -1;
            }
        }

        public int InsertOneCardPatientInfo(BaseEntityer db, OneCard_PatientInfo card_Info)
        {
            int revInt = 0;
            try
            {
                card_Info.Card_Serial = GetNewCardNo(db, card_Info.Card_id.ToString());
                if (card_Info.Card_Serial == -1)
                {
                    db.Err = "生成卡序号失败";
                    revInt = -1;
                    return revInt;
                }
                string sql = @"insert into ONECARD_PATIENTINFO
(
CARD_ID,
CARD_SERIAL,
SERIAL_NO, 
NAME,
PASSWORD,
BIRTHDAY,
AGE,
SEX,
ID_CARD,
HOME_ADD,
LINK_PERSON,
PHONE,
OUT_PATIENTID,
IN_PATIENTID,
ACC_OUTBALANCE,
ACC_INBALANCE,
CARD_TYPE,
CARD_STATE,
YH_CARDNO,
HIS_BUSSINESSNO,
BANK_BUSSINESSNO,
OPER_ID,
OPER_NAME,
OPER_DATE,
CASH_PLEDGE
)
values
(
 '{0}',
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'),
 '{6}',
 '{7}',
 '{8}',
 '{9}',
 '{10}',
 '{11}',
 '{12}',
 '{13}',
 '{14}',
 '{15}',
 '{16}',
 '{17}',
 '{18}',
 '{19}',
 '{20}',
 '{21}',
 '{22}',
 to_date('{23}', 'yyyy-mm-dd hh24:mi:ss'),
 '{24}'
)";
                object[] param = new object[] { card_Info.Card_id,card_Info.Card_Serial,card_Info.Serial, card_Info.Name, 
                    card_Info.Password, card_Info.Birthday, card_Info.Age, card_Info.Sex, card_Info.Id_card, 
                    card_Info.Home_add, card_Info.Link_person, card_Info.Phone, card_Info.Out_patientid, 
                    card_Info.In_patientid, card_Info.Acc_outbalance, card_Info.Acc_inbalance, 
                    card_Info.Card_type, card_Info.Card_state, card_Info.Yh_CardNo, card_Info.His_bussinessno, 
                    card_Info.Bank_bussinessno, card_Info.Oper_id, card_Info.Oper_name, card_Info.Oper_date,card_Info.Cash_pledge};
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                revInt = -1;
            }
            return revInt;
        }

        public int InsertOneStateLogInfo(BaseEntityer db, ONECARD_STATELOG card_state_log)
        {
            int revInt = 0;
            try
            {
                string sql = @"insert into onecard_statelog
(
card_id, 
serial, 
card_state, 
oper_id, 
oper_name, 
oper_date, 
his_bussinessno, 
bank_bussinessno,
card_serial
)
values
(
 '{0}',
 (select nvl(max(serial),10000) + 1 from onecard_statelog where card_id = '{0}'),
 '{1}',
 '{2}',
 '{3}',
 to_date('{4}', 'yyyy-mm-dd hh24:mi:ss'),
 '{5}',
 '{6}',
 '{7}'
)";
                object[] param = new object[] { card_state_log.CARD_ID,card_state_log.CARD_STATE,
                card_state_log.OPER_ID,card_state_log.OPER_NAME,card_state_log.OPER_DATE,card_state_log.HIS_BUSSINESSNO,
                card_state_log.BANK_BUSSINESSNO,card_state_log.CARD_SERIAL
                };
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                revInt = -1;
            }
            return revInt;
        }

        public int InsertOneCardPaymentInfo(BaseEntityer db, ONECARD_PAYMENT card_payment)
        {
            int revInt = 0;
            try
            {
                string sql = @"insert into onecard_payment
(
card_id, 
serial, 
card_type, 
bank_cardno, 
recharge_type, 
recharge_cost, 
recharge_class, 
recharge_mode, 
recharge_date,
remark, 
trans_type, 
acc_balance, 
acc_date,
invoice_no, 
his_bussinessno, 
bank_bussinessno, 
oper_id, 
oper_name, 
oper_date,
bank_no, 
bank_date,
bank_accno, 
bank_accdate,
bank_accflag,
card_serial
)
values
(
 '{0}',
 (select nvl(max(serial),10000) + 1 from onecard_payment where card_id = '{0}'),
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 '{5}',
 '{6}',
 to_date('{7}', 'yyyy-mm-dd hh24:mi:ss'),
 '{8}',
 '{9}',
 '{10}',
 to_date('{11}', 'yyyy-mm-dd hh24:mi:ss'),
 '{12}',
 '{13}',
 '{14}',
 '{15}',
 '{16}',
 to_date('{17}', 'yyyy-mm-dd hh24:mi:ss'),
 '{18}',
 to_date('{19}', 'yyyy-mm-dd hh24:mi:ss'),
 '{20}',
 to_date('{21}', 'yyyy-mm-dd hh24:mi:ss'),
 '{22}',
 '{23}'
)";
                object[] param = new object[] { card_payment.CARD_ID,card_payment.CARD_TYPE, 
                    card_payment.BANK_CARDNO, card_payment.RECHARGE_TYPE, card_payment.RECHARGE_COST, card_payment.RECHARGE_CLASS, card_payment.RECHARGE_MODE, 
                    card_payment.RECHARGE_DATE, card_payment.REMARK, card_payment.TRANS_TYPE, card_payment.ACC_BALANCE, 
                    card_payment.ACC_DATE, card_payment.INVOICE_NO, card_payment.HIS_BUSSINESSNO, 
                    card_payment.BANK_BUSSINESSNO, card_payment.OPER_ID, card_payment.OPER_NAME, card_payment.OPER_DATE,
                    card_payment.BANK_NO, card_payment.BANK_DATE, card_payment.BANK_ACCNO, card_payment.BANK_ACCDATE,card_payment.BANK_ACCFLAG,card_payment.CARD_SERIAL };
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                revInt = -1;
            }
            return revInt;
        }

        public int InsertOneCardSpendingInfo(BaseEntityer db, ONECARD_SPENDING oneCardSpending)
        {
            //2014-4-11 by li 消费记录插入消费卡序号
            int revInt = 0;
            try
            {
                string sql = @"insert into onecard_spending
(
card_id, 
serial, 
card_type, 
bank_cardno, 
spend_type, 
spend_class, 
spend_model, 
spend_amount, 
spend_date,
oper_id, 
oper_name, 
oper_date,
spend_remark, 
trans_type, 
acc_balance, 
acc_date,
invoice_no, 
refund_invoiceno, 
his_bussinessno, 
bank_bussinessno, 
bank_no, 
bank_date,
bank_acc_balance, 
bank_acc_date,
bank_acc_flag,
CARD_SERIAL
)
values
(
 '{0}',
 (select nvl(max(serial),10000) + 1 from onecard_spending where card_id = '{0}'),
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 '{5}',
 '{6}',
 to_date('{7}', 'yyyy-mm-dd hh24:mi:ss'),
 '{8}',
 '{9}',
 to_date('{10}', 'yyyy-mm-dd hh24:mi:ss'),
 '{11}',
 '{12}',
 '{13}',
 to_date('{14}', 'yyyy-mm-dd hh24:mi:ss'),
 '{15}',
 '{16}',
 '{17}',
 '{18}',
 '{19}',
 to_date('{20}', 'yyyy-mm-dd hh24:mi:ss'),
 '{21}',
 to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'),
 '{23}',{24}
)";
                object[] param = new object[] { oneCardSpending.CARD_ID,oneCardSpending.CARD_TYPE, 
                    oneCardSpending.BANK_CARDNO, oneCardSpending.SPEND_TYPE, oneCardSpending.SPEND_CLASS, 
                    oneCardSpending.SPEND_MODEL, oneCardSpending.SPEND_AMOUNT, 
                    oneCardSpending.SPEND_DATE, oneCardSpending.OPER_ID, oneCardSpending.OPER_NAME, oneCardSpending.OPER_DATE, 
                    oneCardSpending.SPEND_REMARK, oneCardSpending.TRANS_TYPE,oneCardSpending.ACC_BALANCE, 
                    oneCardSpending.ACC_DATE, oneCardSpending.INVOICE_NO, oneCardSpending.REFUND_INVOICENO, 
                    oneCardSpending.HIS_BUSSINESSNO, oneCardSpending.BANK_BUSSINESSNO, oneCardSpending.BANK_NO, 
                    oneCardSpending.BANK_DATE, oneCardSpending.BANK_ACC_BALANCE, oneCardSpending.BANK_ACC_DATE, 
                    oneCardSpending.BANK_ACC_FLAG, oneCardSpending.CARD_SERIAL };
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                revInt = -1;
            }
            return revInt;
        }

        public int GetOneCardSpendingByInvoiceNo(BaseEntityer db, string rcptNo, string spendMode, ref ONECARD_SPENDING oneCardSpending, ref string pErrMsg)
        {
            int revInt = 1;
            string sql = @" SELECT card_id,
                                   serial,
                                   card_type,
                                   bank_cardno,
                                   spend_type,
                                   spend_class,
                                   spend_model,
                                   spend_amount,
                                   spend_date,
                                   oper_id,
                                   oper_name,
                                   oper_date,
                                   spend_remark,
                                   trans_type,
                                   acc_balance,
                                   acc_date,
                                   invoice_no,
                                   refund_invoiceno,
                                   his_bussinessno,
                                   bank_bussinessno,
                                   bank_no,
                                   bank_date,
                                   bank_acc_balance,
                                   bank_acc_date,
                                   bank_acc_flag,
                                   bank_bussinessno_real
                              FROM onecard_spending
                             WHERE invoice_no = '{0}'
                               and spend_model = '{1}'
                               and refund_invoiceno is null ";
            try
            {
                sql = string.Format(sql, rcptNo, spendMode);
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                var list = DataSetToEntity.DataSetToT<ONECARD_SPENDING>(ds);
                if (list.Count > 0)
                    oneCardSpending = list[0];
                else
                    oneCardSpending = null;
            }
            catch (Exception e)
            {
                oneCardSpending = null;
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }

        public int UpdateOneCardSpendingReturnNo(BaseEntityer db, string newRcptNo, string oldRcptNo, string spendMode)
        {
            int revInt = 0;
            try
            {
                string sql = @"update onecard_spending set refund_invoiceno = '{0}' where invoice_no = '{1}' and spend_model = '{2}' and refund_invoiceno is null and trans_type = '1'";
                sql = Utility.SqlFormate(sql, newRcptNo, oldRcptNo, spendMode);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                revInt = -1;
            }
            return revInt;
        }

        public List<HisCommon.DataEntity.OneCard_PatientInfo> GetOneCardPatientInfoByIdCardandType(string IdCard, string Type)
        {
            string whereSql = string.Empty;
            if (Type.Equals("G"))
            {
                whereSql = " and card_state in ('1','2') ";
            }
            else
            {
                whereSql = " and card_state = '3' ";
            }
            string sql = @"select 
card_id, 
card_serial, 
serial_no, 
name, 
password, 
birthday, 
age, 
sex, 
id_card, 
home_add, 
link_person, 
phone, 
out_patientid, 
in_patientid, 
acc_outbalance, 
acc_inbalance, 
card_type, 
card_state, 
yh_cardno, 
his_bussinessno, 
bank_bussinessno, 
oper_id, 
oper_name, 
oper_date
from onecard_patientinfo where id_card = '{0}' {1}";
            sql = string.Format(sql, IdCard, whereSql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OneCard_PatientInfo>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        #region 2014-4-14 by li 一卡通押金日结

        /// <summary>
        /// 获取一卡通押金日结信息列表
        /// </summary>
        /// <returns></returns>
        public List<ONE_CARD_PLEDGE_ACCT_MASTER> GetOneCardPledgeInfo()
        {
            string sql = @"select o.acct_no,
                               o.operator_no,
                               o.acct_date,
                               o.acct_date_from,
                               o.acct_date_to,
                               o.recharge_card_cost,
                               o.recharge_money_cost,
                               o.refund_recharge_card_cost,
                               o.refund_recharge_money_cost
                          from one_card_pledge_acct_master o";
            return DataSetToEntity.DataSetToT<ONE_CARD_PLEDGE_ACCT_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 一卡通预存款未日结押金列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<ONECARD_PAYMENT> QueryOneCardPledgeNotAccount(string userId, string date)
        {
            //充值类别（1.门诊 2.住院 3.卡押金）RECHARGE_CLASS
            string sql = @"SELECT * FROM onecard_payment t
            WHERE t.oper_id = '{0}'and t.RECHARGE_CLASS = '3'
            AND t.RECHARGE_DATE<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
            AND (t.ACC_BALANCE = '0' or t.ACC_BALANCE is null )";
            sql = sql.SqlFormate(userId, date);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<ONECARD_PAYMENT>(ds).ToList();
        }

        /// <summary>
        /// 插入卡押金日结信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="acct_Master"></param>
        /// <returns></returns>
        public int InsertPledgeAcctMaster(BaseEntityer db, ONE_CARD_PLEDGE_ACCT_MASTER acct_Master)
        {
            int revInt = 0;
            try
            {
                string sql = @"INSERT INTO ONE_CARD_PLEDGE_ACCT_MASTER
                              (ONE_CARD_PLEDGE_ACCT_MASTER.ACCT_NO,
                               ONE_CARD_PLEDGE_ACCT_MASTER.OPERATOR_NO,
                               ONE_CARD_PLEDGE_ACCT_MASTER.ACCT_DATE,
                               ONE_CARD_PLEDGE_ACCT_MASTER.ACCT_DATE_FROM,
                               ONE_CARD_PLEDGE_ACCT_MASTER.ACCT_DATE_TO,
                               ONE_CARD_PLEDGE_ACCT_MASTER.RECHARGE_CARD_COST,
                               ONE_CARD_PLEDGE_ACCT_MASTER.RECHARGE_MONEY_COST,
                               ONE_CARD_PLEDGE_ACCT_MASTER.REFUND_RECHARGE_CARD_COST,
                               ONE_CARD_PLEDGE_ACCT_MASTER.REFUND_RECHARGE_MONEY_COST)
                            VALUES
                              ((select nvl(max(ACCT_NO), 1000000000) + 1 from ONE_CARD_PLEDGE_ACCT_MASTER),
                               '{0}',
                               to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'),
                               to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'),
                               to_date('{3}', 'yyyy-mm-dd hh24:mi:ss'),
                               {4},
                               {5},
                               {6},
                               {7})";
                object[] param = new object[] { acct_Master.OPERATOR_NO, acct_Master.ACCT_DATE, 
                    acct_Master.ACCT_DATE_FROM, acct_Master.ACCT_DATE_TO, acct_Master.RECHARGE_CARD_COST, 
                    acct_Master.RECHARGE_MONEY_COST, acct_Master.REFUND_RECHARGE_CARD_COST, 
                    acct_Master.REFUND_RECHARGE_MONEY_COST };
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                revInt = -1;
            }
            return revInt;
        }

        /// <summary>
        /// 更新充值记录表押金日结数据
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateOneCardPaymentAcct(HisCommon.DataEntity.ONECARD_PAYMENT payment, BaseEntityer db)
        {
            //ONECARD_PAYMENT
            string sql = @"  update ONECARD_PAYMENT set ACC_BALANCE='{0}' ,ACC_DATE=to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                          where CARD_ID='{2}' and SERIAL='{3}'";
            object[] param = new object[] { 
                payment.ACC_BALANCE	,
                payment.	ACC_DATE,
                payment.	CARD_ID	,
                payment.	SERIAL
                };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        #endregion

        /// <summary>
        /// 2014-5-5 by li 根据发票号获取消费交易记录
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="invoiceNo">消费交易发票号</param>
        /// <param name="spending">一卡通消费交易明细记录</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns>1、成功 -1、异常</returns>
        public int GetOneCardSpendingByInvoiceNo(BaseEntityer db, string invoiceNo, ref ONECARD_SPENDING spending, ref string pErrMsg)
        {
            int revInt = 1;
            string sql = string.Empty;

            if (string.IsNullOrEmpty(spending.SPEND_MODEL))
            {
                sql = @"SELECT *
                              FROM onecard_spending
                             WHERE INVOICE_NO = '{0}'";
                sql = string.Format(sql, invoiceNo);
            }
            else
            {
                sql = @"SELECT *
                              FROM onecard_spending
                             WHERE INVOICE_NO = '{0}' and SPEND_MODEL = {1}";
                sql = string.Format(sql, invoiceNo, spending.SPEND_MODEL);
            }

            try
            {
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                var list = DataSetToEntity.DataSetToT<ONECARD_SPENDING>(ds);
                if (list.Count > 0)
                    spending = list[0];
                else
                    spending = null;
            }
            catch (Exception e)
            {
                spending = null;
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }

        /// <summary>
        /// 2014-5-26 by li 发票退费查询是否为一卡通退费
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="rcptNo">消费交易发票号</param>
        /// <param name="visitType">就诊类别，1、门诊 0、住院</param>
        /// <param name="spending">银医卡消费明细记录</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns>1、成功 -1、异常</returns>
        public int IsMedicalCardBusiness(BaseEntityer db, string rcptNo, string visitType, ref ONECARD_SPENDING spending, ref string pErrMsg)
        {
            int revInt = 1;
            string sql = @"SELECT *
                              FROM ONECARD_SPENDING
                             WHERE invoice_no = '{0}'
                               AND spend_type = '{1}'";
            try
            {
                sql = string.Format(sql, rcptNo, visitType);
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                var list = DataSetToEntity.DataSetToT<ONECARD_SPENDING>(ds);
                if (list.Count > 0)
                    spending = list[0];
                else
                    spending = null;
            }
            catch (Exception e)
            {
                spending = null;
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }

        /// <summary>
        /// 根据卡号查询所有收费
        /// Add By ZhanGD 2014-07-03
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public List<HisCommon.BringObject> QueryFeeByCardID(string cardID, string beginDate, string endDate)
        {
            string strSQL = @" select m.rcpt_no, sum(m.total_costs) costs, m.visit_date
                              From onecard_patientinfo t, outp_rcpt_master m
                             where m.patient_id = t.out_patientid
                               and m.refunded_rcpt_no is null
                               and m.visit_date between
                                   to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
                               and t.card_id = '{0}'
                             group by m.rcpt_no, m.visit_date
                            having sum(m.total_costs) > 0 ";
            strSQL = string.Format(strSQL, cardID, beginDate, endDate);

            List<HisCommon.BringObject> lstFeeDetail = new List<HisCommon.BringObject>();

            DbDataReader dr = BaseEntityer.Db.ExecuteReader(strSQL, 0);
            while (dr.Read())
            {
                HisCommon.BringObject bo = new BringObject();
                bo.Id = dr[0].ToString();
                bo.Exp05 = Convert.ToDecimal(dr[1].ToString());
                bo.Exp06 = Convert.ToDateTime(dr[2].ToString());
                lstFeeDetail.Add(bo);
            }
            if (!dr.IsClosed)
                dr.Close();
            return lstFeeDetail;
        }

        #endregion

        #region 黑龙江医保接口
        /// <summary>
        /// 在做出院登记时插入一条记录保存出院时的床位号（此床位号在医保结算时使用）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="outhospital"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int InsertSI_OUTHOSPITAL(BaseEntityer db, SI_OUTHOSPITAL outhospital, ref string pErrMsg)
        {
            int revInt = 0;
            string sql = @"INSERT INTO SI_OUTHOSPITAL 
                              (SI_OUTHOSPITAL.INPATIENT_ID,
                               SI_OUTHOSPITAL.VISIT_ID,
                               SI_OUTHOSPITAL.BED_NO) 
                            VALUES 
                              ('{0}',{1},'{2}')";
            try
            {
                object[] param = new object[] { outhospital.INPATIENT_ID, outhospital.VISIT_ID, outhospital.BED_NO };
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }
        /// <summary>
        /// 在做出院登记时修改一条已经存在的保存出院时的床位号的记录（此床位号在医保结算时使用）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="outhospital"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int UpdateSI_OUTHOSPITAL(BaseEntityer db, SI_OUTHOSPITAL outhospital, ref string pErrMsg)
        {
            int revInt = 0;
            try
            {
                string sql = @"UPDATE SI_OUTHOSPITAL 
                                   SET BED_NO = '{0}'
                                 WHERE INPATIENT_ID = '{1}' and VISIT_ID = {2}";
                object[] param = new object[] { outhospital.BED_NO, outhospital.INPATIENT_ID, outhospital.VISIT_ID };
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }
        /// <summary>
        /// 根据患者ID和住院次数查询是否已经有一条该患者本次的出院时床位号记录（此床位号在医保结算时使用）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="INPATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int GetSI_OUTHOSPITAL(BaseEntityer db, string INPATIENT_ID, int VISIT_ID, ref string pErrMsg)
        {
            int revInt = 0;
            string sql = @"SELECT INPATIENT_ID,
                                   VISIT_ID,
                                   BED_NO 
                              FROM SI_OUTHOSPITAL 
                             WHERE INPATIENT_ID = '{0}' 
                               AND VISIT_ID = '{1}'";
            try
            {
                sql = string.Format(sql, INPATIENT_ID, VISIT_ID);
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                var list = DataSetToEntity.DataSetToT<SI_OUTHOSPITAL>(ds);
                if (list.Count > 0)
                    revInt = 1;
                else
                    revInt = 0;
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }
        /// <summary>
        /// 根据患者ID和住院次数查询该患者本次的出院时床位号记录（此床位号在医保结算时使用）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="INPATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public string GetSI_OUTHOSPITAL_BED_NO(BaseEntityer db, string INPATIENT_ID, int VISIT_ID, ref string pErrMsg)
        {
            string sql = @"SELECT BED_NO 
                              FROM SI_OUTHOSPITAL 
                             WHERE INPATIENT_ID = '{0}' 
                               AND VISIT_ID = '{1}'";
            try
            {
                sql = string.Format(sql, INPATIENT_ID, VISIT_ID);
                var BED_NO = BaseEntityer.Db.ExecuteScalar(sql);
                if (BED_NO != null)
                    return BED_NO.ToString();
                else
                    return null;
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 获取医保对照信息_药品目录类（上传用）
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetHisCompareUpLoad_Drug(string typeCode, ref string errMsg)
        {
            DataTable compareList = new DataTable();
            string sql =
            #region
 @"select HIS_CODE as 医院项目编码,--0
                               CENTER_CODE as 医保项目编码,--1
                               FEE_TYPE as 项目类型,--2
                               CENTER_NAME as 医保项目名称,--3
                               CENTER_SPECS as 医保规格,--4
                               CENTER_UNIT as 医保项目单位,--5
                               CENTER_PRICE as 医保价格,--6
                               CENTER_TYPE as 医保剂型,--7
                               CENTER_RATE as 自付比例,--8
                               CENTER_PACK as 医保包装数量,--9
                               CENTER_PLACE as 医保产地,--10
                               HIS_NAME as 医院项目名称,--11
                               HIS_SPELL as 医院拼音,--12
                               HIS_SPECS as 医院规格,--13
                               HIS_UNIT as 医院项目单位,--14
                               HIS_PRICE as 医院价格,--15
                               HIS_TYPE as 医院剂型,--16
                               HIS_PACK as 医院包装数量,--17
                               HIS_PLACE as 医院产地,--18
                               OPER_CODE as 操作员,--19
                               OPER_DATE as 操作日期,--20
                               APPLYFLAG as 申请标志,--21
                               PERSONRATE as 个人比例,--22
                               HIS_WB_CODE as 五笔码,--23
                               HIS_USER_CODE as 自定义码,--24
                               TRANS as 交易类型,--25
                               HIS_CLASS as 医院价表类别,--26
                               CENTER_CLASS as 中心项目类别,--27
                               CHARGE_TYPE_CODE as 费别编码,--28
                               DRUG_TABOO as 药品禁忌,--29
                               UNTOWARD_REACTION as 不良反应,--30
                               PRECAUTIONS as 注意事项,--31
                               FEE_ITEMGRADE as 收费项目种类,--32
                               DOSAGE as 药品注册剂型,--33
                               USAGE as 用法,--34
                               DOSAGE_UNIT as 药品剂量单位,--35
                               ONCE_DOSAGE as 每次用量,--36
                               FREQUENCY as 使用频次,--37
                               DRUG_COMMON_LIMIT_FLAG as 药品普通限制标识,--38
                               DRUG_SPECIAL_LIMIT_FLAG as 药品特殊限制标志,--39
                               MATERIAL_LIMITUSE_FLAG as 材料限制使用标识,--40
                               ISNEED_SITECODE as 是否需要传入部位码--41
                          from HIS_COMPARE
                         where FEE_TYPE in ('{0}')";
            #endregion
            sql = string.Format(sql, typeCode);
            try
            {
                compareList = BaseEntityer.Db.GetDataTable(sql);
                if (compareList.Rows.Count <= 0)
                {
                    errMsg = "没有查询到对照数据";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return compareList;
        }
        /// <summary>
        /// 获取医保对照信息_诊疗项目类（上传用）
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetHisCompareUpLoad_UnDrug(string typeCode, ref string errMsg)
        {
            DataTable compareList = new DataTable();
            string sql =
            #region
 @"select HIS_CODE as 医院项目编码,--0
                               CENTER_CODE as 医保项目编码,--1
                               FEE_TYPE as 项目类型,--2
                               CENTER_NAME as 医保项目名称,--3
                               CENTER_SPECS as 医保规格,--4
                               CENTER_UNIT as 医保项目单位,--5
                               CENTER_PRICE as 医保价格,--6
                               CENTER_TYPE as 医保剂型,--7
                               CENTER_RATE as 自付比例,--8
                               CENTER_PACK as 医保包装数量,--9
                               CENTER_PLACE as 医保产地,--10
                               HIS_NAME as 医院项目名称,--11
                               HIS_SPELL as 医院拼音,--12
                               HIS_SPECS as 医院规格,--13
                               HIS_UNIT as 医院项目单位,--14
                               HIS_PRICE as 医院价格,--15
                               HIS_TYPE as 医院剂型,--16
                               HIS_PACK as 医院包装数量,--17
                               HIS_PLACE as 医院产地,--18
                               OPER_CODE as 操作员,--19
                               OPER_DATE as 操作日期,--20
                               APPLYFLAG as 申请标志,--21
                               PERSONRATE as 个人比例,--22
                               HIS_WB_CODE as 五笔码,--23
                               HIS_USER_CODE as 自定义码,--24
                               TRANS as 交易类型,--25
                               HIS_CLASS as 医院价表类别,--26
                               CENTER_CLASS as 中心项目类别,--27
                               CHARGE_TYPE_CODE as 费别编码,--28
                               DRUG_TABOO as 药品禁忌,--29
                               UNTOWARD_REACTION as 不良反应,--30
                               PRECAUTIONS as 注意事项,--31
                               FEE_ITEMGRADE as 收费项目种类,--32
                               DOSAGE as 药品注册剂型,--33
                               USAGE as 用法,--34
                               DOSAGE_UNIT as 药品剂量单位,--35
                               ONCE_DOSAGE as 每次用量,--36
                               FREQUENCY as 使用频次,--37
                               DRUG_COMMON_LIMIT_FLAG as 药品普通限制标识,--38
                               DRUG_SPECIAL_LIMIT_FLAG as 药品特殊限制标志,--39
                               MATERIAL_LIMITUSE_FLAG as 材料限制使用标识,--40
                               ISNEED_SITECODE as 是否需要传入部位码--41
                          from HIS_COMPARE
                         where FEE_TYPE='4'";
            #endregion
            sql = string.Format(sql, typeCode);
            try
            {
                compareList = BaseEntityer.Db.GetDataTable(sql);
                if (compareList.Rows.Count <= 0)
                {
                    errMsg = "没有查询到对照数据";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return compareList;
        }
        /// <summary>
        /// 获取医保对照信息_服务设施类（上传用）
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetHisCompareUpLoad_Server(string typeCode, ref string errMsg)
        {
            DataTable compareList = new DataTable();
            string sql =
            #region
 @"select HIS_CODE as 医院项目编码,--0
                               CENTER_CODE as 医保项目编码,--1
                               FEE_TYPE as 项目类型,--2
                               CENTER_NAME as 医保项目名称,--3
                               CENTER_SPECS as 医保规格,--4
                               CENTER_UNIT as 医保项目单位,--5
                               CENTER_PRICE as 医保价格,--6
                               CENTER_TYPE as 医保剂型,--7
                               CENTER_RATE as 自付比例,--8
                               CENTER_PACK as 医保包装数量,--9
                               CENTER_PLACE as 医保产地,--10
                               HIS_NAME as 医院项目名称,--11
                               HIS_SPELL as 医院拼音,--12
                               HIS_SPECS as 医院规格,--13
                               HIS_UNIT as 医院项目单位,--14
                               HIS_PRICE as 医院价格,--15
                               HIS_TYPE as 医院剂型,--16
                               HIS_PACK as 医院包装数量,--17
                               HIS_PLACE as 医院产地,--18
                               OPER_CODE as 操作员,--19
                               OPER_DATE as 操作日期,--20
                               APPLYFLAG as 申请标志,--21
                               PERSONRATE as 个人比例,--22
                               HIS_WB_CODE as 五笔码,--23
                               HIS_USER_CODE as 自定义码,--24
                               TRANS as 交易类型,--25
                               HIS_CLASS as 医院价表类别,--26
                               CENTER_CLASS as 中心项目类别,--27
                               CHARGE_TYPE_CODE as 费别编码,--28
                               DRUG_TABOO as 药品禁忌,--29
                               UNTOWARD_REACTION as 不良反应,--30
                               PRECAUTIONS as 注意事项,--31
                               FEE_ITEMGRADE as 收费项目种类,--32
                               DOSAGE as 药品注册剂型,--33
                               USAGE as 用法,--34
                               DOSAGE_UNIT as 药品剂量单位,--35
                               ONCE_DOSAGE as 每次用量,--36
                               FREQUENCY as 使用频次,--37
                               DRUG_COMMON_LIMIT_FLAG as 药品普通限制标识,--38
                               DRUG_SPECIAL_LIMIT_FLAG as 药品特殊限制标志,--39
                               MATERIAL_LIMITUSE_FLAG as 材料限制使用标识,--40
                               ISNEED_SITECODE as 是否需要传入部位码--41
                          from HIS_COMPARE
                         where FEE_TYPE='5'";
            #endregion
            sql = string.Format(sql, typeCode);
            try
            {
                compareList = BaseEntityer.Db.GetDataTable(sql);
                if (compareList.Rows.Count <= 0)
                {
                    errMsg = "没有查询到对照数据";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return compareList;
        }
        /// <summary>
        /// 获取所有医保中心药品表信息集合
        /// </summary>
        /// <returns></returns>
        public List<SI_SYDRUG> QueryAllSI_SYDRUG()
        {
            string sql = @"select * from SI_SYDRUG t";
            var ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<SI_SYDRUG>(ds).ToList();
        }

        /// <summary>
        /// 获取院内目录
        /// </summary>
        /// <param name="typeCode">类别</param>
        /// <returns></returns>
        public List<HIS_COMPARE> GetHospitalCatalog(string typeCode)
        {
            switch (typeCode)
            {
                case "1":
                    return this.GetDrugCatalog();
                case "2":
                    return this.GetUnDrugCatalog();
                case "3":
                    return this.GetServerCatalog();
                case "4":
                    return this.GetDiagnoseCatalog();
                default:
                    return new List<HIS_COMPARE>();
            }
        }

        /// <summary>
        /// 获取院内药品目录
        /// </summary>
        /// <returns></returns>
        private List<HIS_COMPARE> GetDrugCatalog()
        {
            List<HIS_COMPARE> compare = new List<HIS_COMPARE>();

            string strSQL = string.Empty;
            strSQL = @" select t.drug_code, --编码
                           t.drug_name, --名称
                           t.input_code, --拼音码
                           m.drug_spec, --规格
                           (select a.serial_no
                              from drug_form_dict a
                             where a.form_name = m.drug_form) dose, --剂型
                           n.retail_price, --价格
                           n.units, --单位
                           '' 产地, --产地
                           '' 收费类别 --收费类别
                      from drug_name_dict t, drug_dict m, drug_price_list n
                     where t.drug_code = m.drug_code
                       and t.drug_code = n.drug_code ";
            DbDataReader dr = BaseEntityer.Db.ExecuteReader(strSQL, 0);
            while (dr.Read())
            {
                HIS_COMPARE hCompare = new HIS_COMPARE();
                hCompare.HIS_CODE = dr[0].ToString();
                hCompare.HIS_NAME = dr[1].ToString();
                hCompare.HIS_SPELL = dr[2].ToString();
                hCompare.HIS_SPECS = dr[3].ToString();
                hCompare.HIS_TYPE = dr[4].ToString();
                hCompare.HIS_PRICE = System.Convert.ToDecimal(dr[5].ToString());
                hCompare.HIS_UNIT = dr[6].ToString();
                hCompare.HIS_PLACE = dr[7].ToString();
                hCompare.FEE_ITEMGRADE = dr[8].ToString();
                compare.Add(hCompare);
            }
            if (!dr.IsClosed)
                dr.Close();
            return compare;
        }

        /// <summary>
        /// 获取院内非药品目录
        /// </summary>
        /// <returns></returns>
        private List<HIS_COMPARE> GetUnDrugCatalog()
        {
            List<HIS_COMPARE> compare = new List<HIS_COMPARE>();

            string strSQL = string.Empty;
            strSQL = @" select t.item_code, --编码
                           t.item_name, --名称
                           '' spell, --拼音码
                           t.price, --价格
                           t.units, --单位
                           m.serial_no
                      From price_list t, bill_item_class_dict m
                     where t.item_class = m.class_code
                       and t.item_class not in ('A', 'B', 'J') ";
            DbDataReader dr = BaseEntityer.Db.ExecuteReader(strSQL, 0);
            while (dr.Read())
            {
                HIS_COMPARE hCompare = new HIS_COMPARE();
                hCompare.HIS_CODE = dr[0].ToString();
                hCompare.HIS_NAME = dr[1].ToString();
                hCompare.HIS_SPELL = dr[2].ToString();
                hCompare.HIS_PRICE = System.Convert.ToDecimal(dr[3].ToString());
                hCompare.HIS_UNIT = dr[4].ToString();
                hCompare.FEE_ITEMGRADE = dr[5].ToString();
                compare.Add(hCompare);
            }
            if (!dr.IsClosed)
                dr.Close();
            return compare;
        }

        /// <summary>
        /// 获取院内服务设施目录
        /// </summary>
        /// <returns></returns>
        private List<HIS_COMPARE> GetServerCatalog()
        {
            List<HIS_COMPARE> compare = new List<HIS_COMPARE>();

            string strSQL = string.Empty;
            strSQL = @" select t.item_code, --编码
                           t.item_name, --名称
                           '' spell, --拼音码
                           t.price, --价格
                           t.units, --单位
                           m.serial_no
                      From price_list t, bill_item_class_dict m
                     where t.item_class = m.class_code
                       and t.item_class = 'J' ";
            DbDataReader dr = BaseEntityer.Db.ExecuteReader(strSQL, 0);
            while (dr.Read())
            {
                HIS_COMPARE hCompare = new HIS_COMPARE();
                hCompare.HIS_CODE = dr[0].ToString();
                hCompare.HIS_NAME = dr[1].ToString();
                hCompare.HIS_SPELL = dr[2].ToString();
                hCompare.HIS_PRICE = System.Convert.ToDecimal(dr[3].ToString());
                hCompare.HIS_UNIT = dr[4].ToString();
                hCompare.FEE_ITEMGRADE = dr[5].ToString();
                compare.Add(hCompare);
            }

            if (!dr.IsClosed)
                dr.Close();
            return compare;
        }

        /// <summary>
        /// 获取院内病种目录
        /// </summary>
        /// <returns></returns>
        private List<HIS_COMPARE> GetDiagnoseCatalog()
        {
            List<HIS_COMPARE> compare = new List<HIS_COMPARE>();
            return compare;
        }

        /// <summary>
        /// 价表项目住院收据类别项目集合
        /// </summary>
        /// <returns></returns>
        public IList<INP_RCPT_FEE_DICT> GetInpRectFeeDict(bool isShowCompare)
        {
            string strSQL = string.Empty;
            if (isShowCompare)
            {
                strSQL = @" SELECT INP_RCPT_FEE_DICT.SERIAL_NO,
                               INP_RCPT_FEE_DICT.FEE_CLASS_CODE,
                               INP_RCPT_FEE_DICT.FEE_CLASS_NAME,
                               INP_RCPT_FEE_DICT.INPUT_CODE
                          FROM INP_RCPT_FEE_DICT
                         ORDER BY INP_RCPT_FEE_DICT.FEE_CLASS_CODE ASC ";
            }
            else
            {
                strSQL = @" SELECT INP_RCPT_FEE_DICT.SERIAL_NO,
                               INP_RCPT_FEE_DICT.FEE_CLASS_CODE,
                               INP_RCPT_FEE_DICT.FEE_CLASS_NAME,
                               INP_RCPT_FEE_DICT.INPUT_CODE
                          FROM INP_RCPT_FEE_DICT
                         where INP_RCPT_FEE_DICT.FEE_CLASS_CODE not in
                               (select m.his_code from si_compare m where m.type = '1')
                         ORDER BY INP_RCPT_FEE_DICT.FEE_CLASS_CODE ASC ";
            }
            DataSet ds = BaseEntityer.Db.GetDataSet(strSQL);
            var list = DataSetToEntity.DataSetToT<INP_RCPT_FEE_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 价表项目门诊收据类别项目集合
        /// </summary>
        /// <returns></returns>
        public IList<OUTP_RCPT_FEE_DICT> GetOutpRectFeeDict(bool isShowCompare)
        {
            string strSQL = string.Empty;
            if (isShowCompare)
            {
                strSQL = @" SELECT OUTP_RCPT_FEE_DICT.SERIAL_NO,
                               OUTP_RCPT_FEE_DICT.FEE_CLASS_CODE,
                               OUTP_RCPT_FEE_DICT.FEE_CLASS_NAME,
                               OUTP_RCPT_FEE_DICT.INPUT_CODE
                          FROM OUTP_RCPT_FEE_DICT
                         ORDER BY OUTP_RCPT_FEE_DICT.FEE_CLASS_CODE ASC ";
            }
            else
            {
                strSQL = @" SELECT OUTP_RCPT_FEE_DICT.SERIAL_NO,
                               OUTP_RCPT_FEE_DICT.FEE_CLASS_CODE,
                               OUTP_RCPT_FEE_DICT.FEE_CLASS_NAME,
                               OUTP_RCPT_FEE_DICT.INPUT_CODE
                          FROM OUTP_RCPT_FEE_DICT
                         where OUTP_RCPT_FEE_DICT.FEE_CLASS_CODE not in
                               (select m.his_code from si_compare m where m.type = '0')
                         ORDER BY OUTP_RCPT_FEE_DICT.FEE_CLASS_CODE ASC ";
            }
            DataSet ds = BaseEntityer.Db.GetDataSet(strSQL);
            var list = DataSetToEntity.DataSetToT<OUTP_RCPT_FEE_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 保存医保对照信息（剂型，费用类别）
        /// </summary>
        /// <param name="type">类型（1、费用，2、剂型）</param>
        /// <param name="SICode">医保编码</param>
        /// <param name="HosCode">院内编码</param>
        /// <returns></returns>
        public int SaveSICompare(string type, string SICode, string HosCode, string HISName, string SIName, string operCode)
        {
            string strSQL = string.Empty;
            strSQL = @" insert into si_compare
                          (type, si_code, his_code, his_name, si_name, oper_code, oper_date)
                        values
                          ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', sysdate) ";
            strSQL = string.Format(strSQL, type, SICode, HosCode, HISName, SIName, operCode);
            return BaseEntityer.Db.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 取消医保对照（剂型，费用类别）
        /// </summary>
        /// <param name="type">类型（1、费用，2、剂型）</param>
        /// <param name="SICode">医保编码</param>
        /// <param name="HosCode">院内编码</param>
        /// <returns></returns>
        public int CancelSICompare(string type, string SICode, string HosCode)
        {
            string strSQL = string.Empty;
            strSQL = @" delete from si_compare where type = '{0}' and si_code = '{1}' and his_code = '{2}' ";
            strSQL = string.Format(strSQL, type, SICode, HosCode);
            return BaseEntityer.Db.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 根据院内编码获取对应医保编码
        /// </summary>
        /// <param name="HosCode"></param>
        /// <returns></returns>
        public string GetSICode(string HosCode, string type)
        {
            string strSQL = string.Empty;
            strSQL = @" select t.si_code
                      From si_compare t
                     where t.type = '{0}'
                       and t.his_code = '{1}' ";
            strSQL = string.Format(strSQL, type, HosCode);
            var temp = BaseEntityer.Db.ExecuteScalar(strSQL);
            return temp == null ? "" : temp.ToString();
        }

        /// <summary>
        /// 根据类别获取所有对照信息
        /// </summary>
        /// <param name="typeCode">类别编码</param>
        /// <returns></returns>
        public List<HisCommon.BringObject> GetSICompare(string typeCode)
        {
            List<HisCommon.BringObject> com = new List<BringObject>();

            string strSQL = @" SELECT t.TYPE, --类别
                                       t.HIS_CODE, --HIS发票科目编码
                                       t.HIS_NAME, --HIS发票科目名称
                                       t.SI_CODE, --SI发票科目编码
                                       t.SI_NAME, --SI发票科目名称
                                       t.OPER_CODE, --操作员
                                       t.STATE, --
                                       t.OPER_DATE --操作日期
                                  FROM SI_COMPARE t --发票科目对照
                                 WHERE t.type = '{0}' ";
            strSQL = string.Format(strSQL, typeCode);

            DbDataReader dbr = BaseEntityer.Db.ZDExecReader(strSQL);
            while (dbr.Read())
            {
                HisCommon.BringObject obj = new BringObject();
                obj.Id = dbr["TYPE"].ToString();
                obj.Name = dbr["HIS_CODE"].ToString();
                obj.Memo = dbr["HIS_NAME"].ToString();
                obj.Exp01 = dbr["SI_CODE"].ToString();
                obj.Exp02 = dbr["SI_NAME"].ToString();
                obj.Exp03 = dbr["OPER_CODE"].ToString();
                obj.Exp06 = Convert.ToDateTime(dbr["OPER_DATE"].ToString());
                com.Add(obj);
            }
            if (!dbr.IsClosed)
                dbr.Close();
            return com;
        }

        #endregion

   

        #region 四平医保
        public int InsertApprove(BaseEntityer db, SI_APPROVEINFO approveinfo, ref string pErrMsg)
        {
            int revInt = 0;
            string sql = @"INSERT INTO APPROVEINFO 
                              (personinfo    ,
                           approvetype   ,
                           startdate    ,
                           enddate      ,
                           outhospital  ,
                           diagnosecode ,
                           inhospital   ,
                           source       ,
                           opername     ,
                           hospitalinfo ,
                           updatetype   ,
                           citytype     ,
                           telephone    ,
                           address      ,
                           flag         ) 
                            VALUES 
                              ('{0}','{1}','{2}',
'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')";
            try
            {
                object[] param = new object[] { 
                approveinfo.PERSONINFO,
                approveinfo.APPROVETYPE,
                approveinfo.STARTDATE,
                approveinfo.ENDDATE,
                approveinfo.OUTHOSPITAL,
                approveinfo.DIAGNOSECODE,
                approveinfo.INHOSPITAL,
                approveinfo.SOURCE,
                approveinfo.OPERNAME,
                approveinfo.HOSPITALINFO,
                approveinfo.UPDATETYPE,
                approveinfo.CITYTYPE,
                approveinfo.TELEPHONE,
                approveinfo.ADDRESS,
                approveinfo.FLAG               
                };
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }
        public DataTable QueryApprove(string personfinfo, ref string pErrMsg)
        {
            DataTable APpList = new DataTable();
            string sql =
            #region
 @"select  *
                          from APProveinfo
                         where personinfo ='{0}'";
            #endregion
            sql = string.Format(sql, personfinfo);
            try
            {
                APpList = BaseEntityer.Db.GetDataTable(sql);
                if (APpList.Rows.Count <= 0)
                {
                    pErrMsg = "没有查询到审批信息";
                    return null;
                }
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                return null;
            }
            return APpList;
        }
        #endregion

        #region 贵阳医保

        /// <summary>
        /// 保存入参
        /// </summary>
        /// <param name="inParam"></param>
        /// <returns></returns>
        public int SaveInParam(HisCommon.DataEntity.SI_InParam inParam, string patientID, string visitID)
        {
            string strSQL = string.Empty;

            strSQL = @" 
SELECT COUNT(*)
  FROM guiyang_si_inparam t
 WHERE t.inpatient_id = '{0}'
   AND t.visit_id = '{1}'
 ";
            strSQL = string.Format(strSQL, patientID,visitID);

            var count = BaseEntityer.Db.ExecuteScalar(strSQL);
            if (count.ToString() == "0")
            {
                #region  SQL 
                strSQL = @" INSERT INTO GUIYANG_SI_INPARAM t --贵阳市医保入参
                              (t.INPATIENT_ID, --患者ID
                               t.VISIT_ID, --住院次数
                               t.CARDTYPE, --卡类别
                               t.MAGNETICSTRIPEDATA, --磁条数据
                               t.CARDNO, --社会保障号
                               t.IPADDRESS, --终端机IP地址
                               t.PASMNO, --PASM卡号
                               t.PASSWORD, --密码
                               t.PAYTYPE, --支付类别
                               t.INSURANCETYPE, --保险类别
                               t.GSNO, --工伤认定编码
                               t.balancetype, --结算方式
                               t.DiagnoseID, --单病种
                               t.RECIPENO, --处方本编号
                               t.SpecillNessCode --特种病
                               )
                            VALUES
                              ('{0}', --患者ID
                               '{1}', --住院次数
                               '{2}', --卡类别
                               '{3}', --磁条数据
                               '{4}', --社会保障号
                               '{5}', --终端机IP地址
                               '{6}', --PASM卡号
                               '{7}', --密码
                               '{8}', --支付类别
                               '{9}', --保险类别
                               '{10}', --工伤认定编码
                               '{11}', --结算方式
                               '{12}', --单病种
                               '{13}', --处方本编号
                               '{14}') ";

                #endregion
                strSQL = string.Format(strSQL, patientID, visitID, inParam.CardType, inParam.MagneticStripeData, inParam.CardNO, inParam.IpAddress, inParam.PASMNO, inParam.PassWord, inParam.PayType, inParam.InsuranceType, inParam.GSNO, inParam.BalanceType, inParam.DiagnoseID, inParam.RecipeNO, inParam.SpecillNessCode);
            }
            else
            {
                strSQL = @" UPDATE GUIYANG_SI_INPARAM t --贵阳医保入参
                               SET t.CARDTYPE           = '{0}', --卡类别
                                   t.MAGNETICSTRIPEDATA = '{1}', --磁条数据
                                   t.IPADDRESS          = '{3}', --终端机IP地址
                                   t.PASMNO             = '{4}', --PASM卡号
                                   t.PASSWORD           = '{5}', --密码
                                   t.PAYTYPE            = '{6}', --支付类别
                                   t.INSURANCETYPE      = '{7}', --保险类别
                                   t.GSNO               = '{8}', --工伤认定编码
                                   t.INPATIENT_ID       = '{9}', --患者ID
                                   t.VISIT_ID           = '{10}', --住院次数
                                   t.balancetype        = '{11}', --结算方式
                                   t.DiagnoseID         = '{12}', --单病种
                                   t.recipeno           = '{13}' --处方本编号
                             WHERE t.CARDNO = '{2}'  ";
                strSQL = string.Format(strSQL, inParam.CardType, inParam.MagneticStripeData, inParam.CardNO, inParam.IpAddress, inParam.PASMNO, inParam.PassWord, inParam.PayType, inParam.InsuranceType, inParam.GSNO, patientID, visitID, inParam.BalanceType, inParam.DiagnoseID, inParam.RecipeNO, inParam.SpecillNessCode);
            }

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 查询入参
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.SI_InParam QueryInParam(string patientID, string visitID)
        {
            SI_InParam inParam = new SI_InParam();

            string strSQL = string.Empty;
            strSQL = @" SELECT t.CARDTYPE, --卡类别
                               t.MAGNETICSTRIPEDATA, --磁条数据
                               t.CARDNO, --社会保障号
                               t.IPADDRESS, --终端机IP地址
                               t.PASMNO, --PASM卡号
                               t.PASSWORD, --密码
                               t.PAYTYPE, --支付类别
                               t.INSURANCETYPE, --保险类别
                               t.GSNO, --工伤认定编码
                               t.balancetype, --结算方式
                               t.DiagnoseID, --单病种
                               t.RECIPENO, --处方号
                               t.SpecillNessCode --特种病
                          FROM GUIYANG_SI_INPARAM t --贵阳市医保入参
                         WHERE t.inpatient_id = '{0}'
                           and t.visit_id = '{1}' ";

            strSQL = string.Format(strSQL, patientID, visitID);

            DbDataReader dr = BaseEntityer.Db.ZDExecReader(strSQL);

            while (dr.Read())
            {
                inParam.CardType = dr["CARDTYPE"].ToString();
                inParam.MagneticStripeData = dr["MAGNETICSTRIPEDATA"].ToString();
                inParam.CardNO = dr["CARDNO"].ToString();
                inParam.IpAddress = dr["IPADDRESS"].ToString();
                inParam.PASMNO = dr["PASMNO"].ToString();
                inParam.PassWord = dr["PASSWORD"].ToString();
                inParam.PayType = dr["PAYTYPE"].ToString();
                inParam.InsuranceType = dr["INSURANCETYPE"].ToString();
                inParam.GSNO = dr["GSNO"].ToString();
                inParam.BalanceType = dr["BALANCETYPE"].ToString();
                inParam.DiagnoseID = dr["DIAGNOSEID"].ToString();
                inParam.RecipeNO = dr["RECIPENO"].ToString();
                inParam.SpecillNessCode = dr["SpecillNessCode"].ToString();
            }
            if (!dr.IsClosed)
                dr.Close();
            return inParam;
        }

        /// <summary>
        /// 保存中心消息
        /// </summary>
        /// <param name="lstMsg"></param>
        /// <returns></returns>
        public int SaveCenterMessage(List<HisCommon.DataEntity.BringSpringObject> lstMsg)
        {
            string strSQL = @" INSERT SI_MESSAGE t --贵阳医保消息通知
                              (t.ID, --编号
                               t.TITLE, --标题
                               t.CONTENT, --内容
                               t.OPERCODE, --操作员
                               t.OPERDATE --操作日期
                               )
                            VALUES
                              ('{0}', --编号
                               '{1}', --标题
                               '{2}', --内容
                               '{3}', --操作员
                               TO_DATE('{4}', 'YYYY-MM-DD HH24:MI:SS') --操作日期
                               ) ";

            for (int i = 0; i < lstMsg.Count; i++)
            {
                BringSpringObject msg = lstMsg[i];
                strSQL = string.Format(strSQL, msg.ID, msg.Name, msg.Memo, msg.User01, msg.User02);
                BaseEntityer.Db.ExecuteNonQuery(strSQL);
            }

            return 1;
        }

        /// <summary>
        /// 加载中心消息
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BringSpringObject> LoadCenterMessage()
        {
            List<HisCommon.DataEntity.BringSpringObject> lstMsg = new List<BringSpringObject>();

            string strSQL = @" SELECT t.ID, --编号
                                       t.TITLE, --标题
                                       t.CONTENT, --内容
                                       t.OPERCODE, --操作员
                                       t.OPERDATE --操作日期
                                  FROM SI_MESSAGE t --贵阳医保消息通知
                                 order by t.operdate desc ";

            DbDataReader dbr = BaseEntityer.Db.ExecuteReader(strSQL);
            while (dbr.Read())
            {
                BringSpringObject msg = new BringSpringObject();
                msg.ID = dbr["ID"].ToString();
                msg.Name = dbr["TITLE"].ToString();
                msg.Memo = dbr["CONTENT"].ToString();
                msg.User01 = dbr["OPERCODE"].ToString();
                msg.User02 = dbr["OPERDATE"].ToString();
                lstMsg.Add(msg);
            }
            if (!dbr.IsClosed)
                dbr.Close();
            return lstMsg;
        }

        /// <summary>
        ///  更新登记市医保的结算方式
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="balanceNo"></param>
        /// <param name="serialNO"></param>
        /// <returns></returns>
        public int UpdateSIInfoCalculateTypeInfoByIndex(BaseEntityer db, string patientID, string visitID, string balanceNo, string typeCode, string calculateType, string diagnoseID, string birthFlag, ref  string errSql)
        {
            try
            {
                string sql = @"UPDATE siinfo t
   SET t.clinicdoctor_no    = '{4}',
       t.Readcardreturn     = '{5}',
       t.birth_balance_flag = '{6}'
 WHERE t.inpatient_id = '{0}'
   AND t.visit_id = '{1}'
   AND t.balance_no = '{2}'
   AND t.type_code = '{3}'
   AND t.pact_code = '2'
";

                sql = string.Format(sql, patientID, visitID, balanceNo, typeCode, calculateType, diagnoseID, birthFlag);
                return db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                errSql = ex.Message;
            }
            return -1;
        }

        /// <summary>
        ///  更新登记市医保的结算方式
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="balanceNo"></param>
        /// <param name="serialNO"></param>
        /// <returns></returns>
        public int UpdateSIInfoBalanceTypeInfoByIndex(BaseEntityer db, string patientID, string visitID, string balanceNo, string typeCode, string balanceType, string diagnoseID, ref  string errSql)
        {
            try
            {
                string sql = @"UPDATE siinfo t
   SET t.clinicdoctor_name = '{4}',
       t.Clinicdiagnose='{5}'
 WHERE t.inpatient_id = '{0}'
   AND t.visit_id = '{1}'
   AND t.balance_no = '{2}'
   AND t.type_code = '{3}'
   AND t.pact_code = '2'";

                sql = string.Format(sql, patientID, visitID, balanceNo, typeCode, balanceType, diagnoseID);
                return db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                errSql = ex.Message;
            }
            return -1;
        }

         /// <summary>
        /// 获取医保对照信息
        /// </summary>
        /// <param name="hisCode"></param>
        /// <param name="typeCode"></param>
        /// <param name="revString"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetHisCompareNoFee(ref HIS_COMPARE revString, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql =
            #region
                     @"SELECT his_code,
                           center_code,
                           fee_type,
                           center_name,
                           center_specs,
                           center_unit,
                           center_price,
                           center_type,
                           center_rate,
                           center_pack,
                           center_place,
                           his_name,
                           his_spell,
                           his_specs,
                           his_unit,
                           his_price,
                           his_type,
                           his_pack,
                           his_place,
                           oper_code,
                           oper_date,
                           applyflag,
                           personrate,
                           his_wb_code,
                           his_user_code,
                           trans,
                           his_class,
                           center_class,
                           charge_type_code,
                           drug_taboo,
                           untoward_reaction,
                           precautions,
                           fee_itemgrade,
                           dosage,
                           usage,
                           dosage_unit,
                           once_dosage,
                           frequency,
                           drug_common_limit_flag,
                           drug_special_limit_flag,
                           material_limituse_flag,
                           isneed_sitecode
                      FROM his_compare t
                     WHERE t.applyflag = '1'
                       AND t.charge_type_code = '2'
                       AND rownum = 1
                    ";
            #endregion

            revString = new HIS_COMPARE();
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                #region 赋值
                revString.HIS_CODE = dt.Rows[0][0].ToString();
                revString.CENTER_CODE = dt.Rows[0][1].ToString();
                revString.FEE_TYPE = dt.Rows[0][2].ToString();
                revString.CENTER_NAME = dt.Rows[0][3].ToString();
                revString.CENTER_SPECS = dt.Rows[0][4].ToString();
                revString.CENTER_UNIT = dt.Rows[0][5].ToString();
                revString.CENTER_PRICE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][6].ToString()) == true ? "0" : dt.Rows[0][6].ToString());
                revString.CENTER_TYPE = dt.Rows[0][7].ToString();
                revString.CENTER_RATE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][8].ToString()) == true ? "0" : dt.Rows[0][8].ToString());
                revString.CENTER_PACK = int.Parse(string.IsNullOrEmpty(dt.Rows[0][9].ToString()) == true ? "0" : dt.Rows[0][9].ToString());
                revString.CENTER_PLACE = dt.Rows[0][10].ToString();
                revString.HIS_NAME = dt.Rows[0][11].ToString();
                revString.HIS_SPELL = dt.Rows[0][12].ToString();
                revString.HIS_SPECS = dt.Rows[0][13].ToString();
                revString.HIS_UNIT = dt.Rows[0][14].ToString();
                revString.HIS_PRICE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][15].ToString()) == true ? "0" : dt.Rows[0][15].ToString());
                revString.HIS_TYPE = dt.Rows[0][16].ToString();
                revString.HIS_PACK = int.Parse(string.IsNullOrEmpty(dt.Rows[0][17].ToString()) == true ? "0" : dt.Rows[0][17].ToString());
                revString.HIS_PLACE = dt.Rows[0][18].ToString();
                revString.OPER_CODE = dt.Rows[0][19].ToString();
                revString.OPER_DATE = DateTime.Parse(string.IsNullOrEmpty(dt.Rows[0][20].ToString()) == true ? DateTime.MinValue.ToString() : dt.Rows[0][20].ToString());
                revString.APPLYFLAG = dt.Rows[0][21].ToString();
                revString.PERSONRATE = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][22].ToString()) == true ? "0" : dt.Rows[0][22].ToString());
                revString.HIS_WB_CODE = dt.Rows[0][23].ToString();
                revString.HIS_USER_CODE = dt.Rows[0][24].ToString();
                revString.TRANS = dt.Rows[0][25].ToString();
                revString.HIS_CLASS = dt.Rows[0][26].ToString();
                revString.CENTER_CLASS = dt.Rows[0][27].ToString();
                revString.CHARGE_TYPE_CODE = dt.Rows[0][28].ToString();
                revString.DRUG_TABOO = dt.Rows[0][29].ToString();
                revString.UNTOWARD_REACTION = dt.Rows[0][30].ToString();
                revString.PRECAUTIONS = dt.Rows[0][31].ToString();
                revString.FEE_ITEMGRADE = dt.Rows[0][32].ToString();
                revString.DOSAGE = dt.Rows[0][33].ToString();
                revString.USAGE = dt.Rows[0][34].ToString();
                revString.DOSAGE_UNIT = dt.Rows[0][35].ToString();
                revString.ONCE_DOSAGE = dt.Rows[0][36].ToString();
                revString.FREQUENCY = dt.Rows[0][37].ToString();
                revString.DRUG_COMMON_LIMIT_FLAG = dt.Rows[0][38].ToString();
                revString.DRUG_SPECIAL_LIMIT_FLAG = dt.Rows[0][39].ToString();
                revString.MATERIAL_LIMITUSE_FLAG = dt.Rows[0][40].ToString();
                revString.ISNEED_SITECODE = dt.Rows[0][41].ToString();
                #endregion
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        ///  查询诊断编码通过诊断名称
        /// </summary>
        /// <param name="db"></param>
        /// <param name="diagName"></param>
        /// <returns></returns>
        public string QueryDiagnoseCodeByName(BaseEntityer db, string diagName, ref  string errSql)
        {
            try
            {
                string sql = @"SELECT t.diagnose_code
  FROM si_sydiagnose t
 WHERE t.charge_type_code = '1'
   AND t.diagnose_name = '{0}'
";
                sql = string.Format(sql, diagName);
                return db.ExecuteScalar<string>(sql);
            }
            catch (Exception ex)
            {
                errSql = ex.Message;
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据就诊顺序号查询医保信息
        /// </summary>
        /// <param name="registerNO"></param>
        /// <returns></returns>
        public SIInfo QuerySIInfoByRegisterNO(string registerNO)
        {
            SIInfo siInfo = new SIInfo();
            string strSQL = @" SELECT t.INPATIENT_ID, --患者ID
                                   t.VISIT_ID, --住院次数
                                   t.BALANCE_NO, --结算序号
                                   t.NAME, --姓名
                                   t.SEX, --性别
                                   t.BIRTHDAY, --出生年月
                                   t.IDENNO, --身份证号
                                   t.PACT_CODE, --费别编码
                                   t.PACT_NAME, --费别名称
                                   t.REGISTER_NO, --就诊登记号
                                   t.HOSPITAL_NO, --医院编号
                                   t.HOSPITAL_NAME, --医院名称
                                   t.HOSPITAL_GRADE, --医院级别
                                   t.IMP_NO, --住院号
                                   t.RUNNING_NO, --就诊流水号
                                   t.INVOICE_NO, --发票号
                                   t.MEDICAL_TYPE, --医疗类别
                                   t.CARD_NO, --就诊卡号
                                   t.MCARD_NO, --医疗证号
                                   t.PERSON_NO, --个人编号
                                   t.SI_BEGINDATE, --参保日期
                                   t.SI_STATE, --参保状态
                                   t.ICCARD_NO, --IC卡卡号
                                   t.OVERAL_NO, --统筹区号
                                   t.FUND_NO, --基金编码
                                   t.FUND_NAME, --基金名称
                                   t.BUSINESSSEQUENCE, --业务序号
                                   t.APPLYSEQUENCE, --申请序号
                                   t.ANOTHERCITY_NO, --异地安置编码
                                   t.ANOTHERCITY_NAME, --异地安置名称
                                   t.CORPORATION_NO, --单位编码
                                   t.CORPORATION_NAME, --单位名称
                                   t.INSURANCETYPE, --险种类别
                                   t.EMPL_TYPE, --人员类别（农合慢病患者标志）
                                   t.BED_NO, --床号
                                   t.ISBALANCED, --是否已经结算(未结:0,已结:1)未结代表是挂号信息（0）
                                   t.INDOCTOR_NO, --入院医师编码
                                   t.INDOCTOR_NAME, --入院医师名称
                                   t.OUTDOCTOR_NO, --出院医师编码
                                   t.OUTDOCTOR_NAME, --出院医师名称
                                   t.OUTREASON, --出院原因(农合入院状态)
                                   t.PAYUSERFLAG, --账户使用标志
                                   t.CLINICDIAGNOSE, --门诊诊断
                                   t.INDIAGNOSE_NO, --入院诊断编码
                                   t.INDIAGNOSE_NAME, --入院诊断名称
                                   t.INDIAGNOSE_DATE, --入院诊断日期
                                   t.OUTDIAGNOSE_NO, --出院诊断编码
                                   t.OUTDIAGNOSE_NAME, --出院诊断名称
                                   t.OUTDIAGNOSE_DATE, --出院诊断日期
                                   t.INHOSPITAL_DATE, --入院日期
                                   t.OUTHOSPITAL_DATE, --出院日期
                                   t.BALANCE_DATE, --结算日期
                                   t.BALANCE_STATE, --1 结算 0 未结算
                                   t.OPER_NO, --操作员编码
                                   t.OPER_NAME, --操作员名称
                                   t.OPER_DATE, --操作日期
                                   t.CLINICDEPT_NO, --挂号科室编码
                                   t.CLINICDEPT_NAME, --挂号科室名称
                                   t.CLINICDOCTOR_NO, --门诊医师编码
                                   t.CLINICDOCTOR_NAME, --门诊医师名称
                                   t.INDEPT_NO, --入院科室编码
                                   t.INDEPT_NAME, --入院科室名称
                                   t.OUTDEPT_NO, --出院科室编码
                                   t.OUTDEPT_NAME, --出院科室名称
                                   t.TOT_COST, --医疗费总额
                                   t.PAY_COST, --账户支付金额
                                   t.PUB_COST, --统筹支付金额
                                   t.OWN_COST, --现金支付金额
                                   t.OFFICIAL_COST, --公务员账户支付金额
                                   t.OVER_COST, --大额补助支付金额
                                   t.BASE_COST, --起伏线金额
                                   t.OWN_SUPPLE_COST, --个人补充支付
                                   t.HELP_ALLOWANCES_COST, --低保救助支付
                                   t.ENTERPRISE_SUPPLE_COST, --企业补充支付
                                   t.HELP_OWN_COST, --救助金支付金额
                                   t.YEAR_TOT_COST, --年度医疗费总额
                                   t.YEAR_PUB_COST, --年度医疗费统筹支付总额
                                   t.YEAR_PAY_COST, --年度医疗费账户支付总额
                                   t.YEAR_OWN_COST, --年度个人补充支付总额
                                   t.YEAR_OFFICIAL_COST, --年度公务员支付总额
                                   t.YEAR_BASE_COST, --年度起付钱支付总额
                                   t.YEAR_HELP_COST, --年度救助金支出累计
                                   t.YEAR_SPECIALMED_COST, --年度特殊病支出累计
                                   t.INDIVIDUAL_COST, --账户余额
                                   t.SPECIALMED_TOTCOST, --特殊疾病总额
                                   t.SPECIALMED_PUBCOST, --特殊疾病统筹总额
                                   t.SPECIALMED_BASECOST, --特殊疾病起伏线金额
                                   t.LXPUB_COST, --离休统筹支出
                                   t.YLOWN_COST, --乙类自理金额
                                   t.TCOWN_COST, --统筹自理金额
                                   t.DEOWN_COST, --大额自理金额
                                   t.EXCEEDLIMIT_OWNCOST, --超限价自理金额
                                   t.SEALTOPLINE_OWNCOST, --超封顶线自理金额
                                   t.ENTERPRISEADD_COST, --企业补充金额
                                   t.APPINFO_NO, --申请信息编码
                                   t.APPINFO_NAME, --申请信息名称(补偿名称)
                                   t.APPINFO_MEMO, --申请信息备注(农合补偿代码)
                                   t.APPTYPE_NO, --申请类别编码(单病种)
                                   t.APPTYPE_NAME, --申请类别名称(单病种名称)
                                   t.APPTYPE_MEMO, --申请类别备注
                                   t.APP_FLAG, --申请标识
                                   t.APP_DATE, --申请日期
                                   t.CARDVALIDDATE, --卡有效期
                                   t.SHIFTDATE, --变更日期(visit_date门诊用的主键VISIT_DATE, VISIT_NO）
                                   t.INHOSTIMES, --医疗住院次数
                                   t.ISVALID, --有效标志,1:有效 2:作废
                                   t.PC_NO, --电脑号
                                   t.REGINFORETURN, --接口登记返回信息
                                   t.READCARDRETURN, --接口读卡返回信息
                                   t.BANINFORETURN, --接口结算返回信息
                                   t.REMARK, --备注（个人属性）
                                   t.TYPE_CODE, --结算类别,1.门诊2.住院
                                   t.TRANS_TYPE, --交易类型,1.正交易2.负交易
                                   t.CENTER_BIZCYCLENO, --Center业务周期号
                                   t.HIS_BIZCYCLENO, --HIS业务周期号
                                   t.CENTER_BUSSINESSSEQNO, --Center交易流水号
                                   t.HIS_BUSSINESSSEQNO, --HIS交易流水号
                                   t.BIRTH_BALANCE_FLAG, --生育结算标志
                                   t.INVOICE_NEW, --新发票号
                                   t.ISSTOPMEDICAL --停保标识
                              FROM SIINFO t --医保信息表
                             WHERE t.register_no = '{0}' ";

            strSQL = string.Format(strSQL, registerNO);

            DataSet dsSIInfo = BaseEntityer.Db.GetDataSet(strSQL);
            IList<SIInfo> siLst = DataSetToEntity.DataSetToT<SIInfo>(dsSIInfo);
            if (siLst.Count > 0)
            {
                siInfo = siLst[0];
            }

            return siInfo;
        }

        /// <summary>
        /// 贵阳市医保工伤保险申报
        /// </summary>
        /// <param name="gs"></param>
        /// <returns></returns>
        public int InsertGYSIGS(GUIYANG_REPORT_GS gs)
        {
            string strSQL = @" INSERT INTO GUIYANG_REPORT_GS t --贵阳市医保工伤保险申报（按月度）
                                  (t.PERIOD, --期号
                                   t.OPERATOR, --操作员
                                   t.DODATE, --办理时间
                                   t.MZPSNS, --门诊人次
                                   t.MZFUND, --门诊统筹支付
                                   t.ZYPSNS, --住院结算人次
                                   t.ZYFUND, --住院统筹支付
                                   t.APPNO --清算申请流水号
                                   )
                                VALUES
                                  ('{0}', --期号
                                   '{1}', --操作员
                                   TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --办理时间
                                   '{3}', --门诊人次
                                   '{4}', --门诊统筹支付
                                   '{5}', --住院结算人次
                                   '{6}', --住院统筹支付
                                   '{7}' --清算申请流水号
                                   ) ";
            strSQL = string.Format(strSQL, gs.Period, gs.Operator, gs.Dodate.ToString(), gs.Mzpsns, gs.Mzfund, gs.Zypsns, gs.Zyfund, gs.Appno);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 贵阳市医保居民保险申报
        /// </summary>
        /// <param name="jm"></param>
        /// <returns></returns>
        public int InsertGYSIJM(GUIYANG_REPORT_JM jm)
        {
            string strSQL = @" INSERT INTO GUIYANG_REPORT_JM t --贵阳市医保居民保险申报（按年度）
                                  (t.PERIOD, --期号
                                   t.OPERATOR, --操作员
                                   t.DODATE, --办理时间
                                   t.MZPSNS, --门诊人次数
                                   t.MZFUND1, --门诊统筹支付
                                   t.APPNO --清算申请流水号
                                   )
                                VALUES
                                  ('{0}', --期号
                                   '{1}', --操作员
                                   TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --办理时间
                                   '{3}', --门诊人次数
                                   '{4}', --门诊统筹支付
                                   '{5}' --清算申请流水号
                                   )  ";
            strSQL = string.Format(strSQL, jm.Period, jm.Operator, jm.Dodate, jm.Mzpsns, jm.Mzfund1, jm.Appno);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 贵阳市医保生育保险申报
        /// </summary>
        /// <param name="sy"></param>
        /// <returns></returns>
        public int InsertGYSISY(GUIYANG_REPORT_SY sy)
        {
            string strSQL = @" INSERT INTO GUIYANG_REPORT_SY t --贵阳市医保生育保险申报（按月度）
                                  (t.PERIOD, --期号
                                   t.OPERATOR, --操作员
                                   t.DODATE, --办理时间
                                   t.INSURETYPE, --保险类别
                                   t.FMBGPSNS, --分娩住院包干人次
                                   t.FMBGFEEALL, --分娩住院包干费用总额
                                   t.FMBGFUND, --分娩住院包干统筹支付
                                   t.FMPSNS, --分娩住院非包干人次
                                   t.FMFEEALL, --分娩住院非包干费用总额
                                   t.FMFUND, --分娩住院非包干统筹支付
                                   t.JSPSNS, --计生住院人次
                                   t.JSFEEALL, --计生住院费用总额
                                   t.JSFUND, --计生住院统筹支付
                                   t.JCF, --产前检查费用总额
                                   t.APPNO --清算申请流水号
                                   )
                                VALUES
                                  ('{0}', --期号
                                   '{1}', --操作员
                                   TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --办理时间
                                   '{3}', --保险类别
                                   '{4}', --分娩住院包干人次
                                   '{5}', --分娩住院包干费用总额
                                   '{6}', --分娩住院包干统筹支付
                                   '{7}', --分娩住院非包干人次
                                   '{8}', --分娩住院非包干费用总额
                                   '{9}', --分娩住院非包干统筹支付
                                   '{10}', --计生住院人次
                                   '{11}', --计生住院费用总额
                                   '{12}', --计生住院统筹支付
                                   '{13}', --产前检查费用总额
                                   '{14}' --清算申请流水号
                                   ) ";
            strSQL = string.Format(strSQL, sy.Period, sy.Operator, sy.Dodate, sy.Insuretype, sy.Fmbgpsns, sy.Fmbgfeeall, sy.Fmbgfund, sy.Fmpsns, sy.Fmfeeall, sy.Fmfund, sy.Jspsns, sy.Jsfeeall, sy.Jsfund, sy.Jcf, sy.Appno);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 贵阳市医保医疗保险申报
        /// </summary>
        /// <param name="yl"></param>
        /// <returns></returns>
        public int InsertGYSIYL(GUIYANG_REPORT_YL yl)
        {
            string strSQL = @" INSERT INTO GUIYANG_REPORT_YL t --贵阳市医保医疗保险申报（按月度）
                                  (t.PERIOD, --期号
                                   t.OPERATOR, --操作员
                                   t.DODATE, --办理时间
                                   t.INSURETYPE, --保险类别
                                   t.MZPSNS, --门诊就诊人次
                                   t.MZACCT, --门诊个人账户支付金额
                                   t.MZFUND3, --门诊医疗补助支付金额
                                   t.TMPSNS, --特殊疾病门诊人数
                                   t.TMACCT, --特殊疾病门诊个人账户支付金额
                                   t.TMFUND1, --特殊疾病门诊基本医疗统筹支付金额
                                   t.TMFUND2, --特殊疾病门诊大额医疗支付金额
                                   t.TMFUND3, --特殊疾病门诊医疗补助支付金额
                                   t.ZY1PSNS, --住院就诊人次
                                   t.ZY1ACCT, --控制线住院个人账户支付金额
                                   t.ZY1FUND1, --控制线住院基本医疗统筹支付金额
                                   t.ZY1FUND2, --控制线住院大额医疗支付金额
                                   t.ZY1FUND3, --控制线住院医疗补助支付金额
                                   t.ZY2PSNS, --重症住院就诊人次
                                   t.ZY2ACCT, --重症住院个人账户支付金额
                                   t.ZY2FUND1, --重症住院基本医疗统筹支付金额
                                   t.ZY2FUND2, --重症住院大额医疗支付金额
                                   t.ZY2FUND3, --重症住院医疗补助支付金额
                                   t.ZY3PSNS, --按日包干住院就诊人次
                                   t.ZY3DAYS, --按日包干住院天数
                                   t.ZY3ACCT, --按日包干住院个人账户支付金额
                                   t.ZY3FUND3, --按日包干住院医疗补助支付金额
                                   t.ZY4PSNS, --包干结算就诊人次
                                   t.ZY4ACCT, --包干结算个人账户支付金额
                                   t.ZY4FUND1, --包干结算基本医疗统筹支付金额
                                   t.ZY4FUND2, --包干结算大额医疗支付金额
                                   t.ZY4FUND3, --包干结算医疗补助支付金额
                                   t.APPNO --清算申请流水号
                                   )
                                VALUES
                                  ('{0}', --期号
                                   '{1}', --操作员
                                   TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --办理时间
                                   '{3}', --保险类别
                                   '{4}', --门诊就诊人次
                                   '{5}', --门诊个人账户支付金额
                                   '{6}', --门诊医疗补助支付金额
                                   '{7}', --特殊疾病门诊人数
                                   '{8}', --特殊疾病门诊个人账户支付金额
                                   '{9}', --特殊疾病门诊基本医疗统筹支付金额
                                   '{10}', --特殊疾病门诊大额医疗支付金额
                                   '{11}', --特殊疾病门诊医疗补助支付金额
                                   '{12}', --住院就诊人次
                                   '{13}', --控制线住院个人账户支付金额
                                   '{14}', --控制线住院基本医疗统筹支付金额
                                   '{15}', --控制线住院大额医疗支付金额
                                   '{16}', --控制线住院医疗补助支付金额
                                   '{17}', --重症住院就诊人次
                                   '{18}', --重症住院个人账户支付金额
                                   '{19}', --重症住院基本医疗统筹支付金额
                                   '{20}', --重症住院大额医疗支付金额
                                   '{21}', --重症住院医疗补助支付金额
                                   '{22}', --按日包干住院就诊人次
                                   '{23}', --按日包干住院天数
                                   '{24}', --按日包干住院个人账户支付金额
                                   '{25}', --按日包干住院医疗补助支付金额
                                   '{26}', --包干结算就诊人次
                                   '{27}', --包干结算个人账户支付金额
                                   '{28}', --包干结算基本医疗统筹支付金额
                                   '{29}', --包干结算大额医疗支付金额
                                   '{30}', --包干结算医疗补助支付金额
                                   '{31}' --清算申请流水号
                                   ) ";
            strSQL = string.Format(strSQL, yl.Period, yl.Operator, yl.Dodate.ToString(), yl.Insuretype, yl.Mzpsns, yl.Mzacct, yl.Mzfund3,
                yl.Tmpsns, yl.Tmacct, yl.Tmfund1, yl.Tmfund2, yl.Tmfund3, yl.Zy1psns, yl.Zy1acct, yl.Zy1fund1, yl.Zy1fund2, yl.Zy1fund3,
                yl.Zy2psns, yl.Zy2acct, yl.Zy2fund1, yl.Zy2fund2, yl.Zy2fund3, yl.Zy3psns, yl.Zy3days, yl.Zy3acct, yl.Zy3fund3, yl.Zy4psns,
                yl.Zy4acct, yl.Zy4fund1, yl.Zy4fund2, yl.Zy4fund3, yl.Appno);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 贵阳市医保工伤保险申报
        /// </summary>
        /// <param name="gs"></param>
        /// <returns></returns>
        public int UpdateGYSIGS(GUIYANG_REPORT_GS gs)
        {
            string strSQL = @" UPDATE GUIYANG_REPORT_GS t --贵阳市医保工伤保险申报（按月度）
                               SET t.OPERATOR = '{1}', --操作员
                                   t.DODATE   = TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --办理时间
                                   t.MZPSNS   = '{3}', --门诊人次
                                   t.MZFUND   = '{4}', --门诊统筹支付
                                   t.ZYPSNS   = '{5}', --住院结算人次
                                   t.ZYFUND   = '{6}', --住院统筹支付
                                   t.APPNO    = '{7}' --清算申请流水号
                             WHERE t.PERIOD = '{0}' ";
            strSQL = string.Format(strSQL, gs.Period, gs.Operator, gs.Dodate.ToString(), gs.Mzpsns, gs.Mzfund, gs.Zypsns, gs.Zyfund, gs.Appno);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 贵阳市医保居民保险申报
        /// </summary>
        /// <param name="jm"></param>
        /// <returns></returns>
        public int UpdateGYSIJM(GUIYANG_REPORT_JM jm)
        {
            string strSQL = @" UPDATE GUIYANG_REPORT_JM t --贵阳市医保居民保险申报（按年度）
                                   SET t.OPERATOR = '{1}', --操作员
                                       t.DODATE   = TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --办理时间
                                       t.MZPSNS   = '{3}', --门诊人次数
                                       t.MZFUND1  = '{4}', --门诊统筹支付
                                       t.APPNO    = '{5}' --清算申请流水号
                                 WHERE t.PERIOD = '{0}' ";
            strSQL = string.Format(strSQL, jm.Period, jm.Operator, jm.Dodate, jm.Mzpsns, jm.Mzfund1, jm.Appno);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 贵阳市医保生育保险申报
        /// </summary>
        /// <param name="sy"></param>
        /// <returns></returns>
        public int UpdateGYSISY(GUIYANG_REPORT_SY sy)
        {
            string strSQL = @" UPDATE GUIYANG_REPORT_SY t --贵阳市医保生育保险申报（按月度）
                                   SET t.OPERATOR   = '{1}', --操作员
                                       t.DODATE     = TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --办理时间
                                       t.INSURETYPE = '{3}', --保险类别
                                       t.FMBGPSNS   = '{4}', --分娩住院包干人次
                                       t.FMBGFEEALL = '{5}', --分娩住院包干费用总额
                                       t.FMBGFUND   = '{6}', --分娩住院包干统筹支付
                                       t.FMPSNS     = '{7}', --分娩住院非包干人次
                                       t.FMFEEALL   = '{8}', --分娩住院非包干费用总额
                                       t.FMFUND     = '{9}', --分娩住院非包干统筹支付
                                       t.JSPSNS     = '{10}', --计生住院人次
                                       t.JSFEEALL   = '{11}', --计生住院费用总额
                                       t.JSFUND     = '{12}', --计生住院统筹支付
                                       t.JCF        = '{13}', --产前检查费用总额
                                       t.APPNO      = '{14}' --清算申请流水号
                                 WHERE t.PERIOD = '{0}' ";

            strSQL = string.Format(strSQL, sy.Period, sy.Operator, sy.Dodate, sy.Insuretype, sy.Fmbgpsns, sy.Fmbgfeeall, sy.Fmbgfund, sy.Fmpsns, sy.Fmfeeall, sy.Fmfund, sy.Jspsns, sy.Jsfeeall, sy.Jsfund, sy.Jcf, sy.Appno);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 贵阳市医保医疗保险申报
        /// </summary>
        /// <param name="yl"></param>
        /// <returns></returns>
        public int UpdateGYSIYL(GUIYANG_REPORT_YL yl)
        {
            string strSQL = @" UPDATE GUIYANG_REPORT_YL t --贵阳市医保医疗保险申报（按月度）
                                   SET t.OPERATOR   = '{1}', --操作员
                                       t.DODATE     = TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --办理时间
                                       t.INSURETYPE = '{3}', --保险类别
                                       t.MZPSNS     = '{4}', --门诊就诊人次
                                       t.MZACCT     = '{5}', --门诊个人账户支付金额
                                       t.MZFUND3    = '{6}', --门诊医疗补助支付金额
                                       t.TMPSNS     = '{7}', --特殊疾病门诊人数
                                       t.TMACCT     = '{8}', --特殊疾病门诊个人账户支付金额
                                       t.TMFUND1    = '{9}', --特殊疾病门诊基本医疗统筹支付金额
                                       t.TMFUND2    = '{10}', --特殊疾病门诊大额医疗支付金额
                                       t.TMFUND3    = '{11}', --特殊疾病门诊医疗补助支付金额
                                       t.ZY1PSNS    = '{12}', --住院就诊人次
                                       t.ZY1ACCT    = '{13}', --控制线住院个人账户支付金额
                                       t.ZY1FUND1   = '{14}', --控制线住院基本医疗统筹支付金额
                                       t.ZY1FUND2   = '{15}', --控制线住院大额医疗支付金额
                                       t.ZY1FUND3   = '{16}', --控制线住院医疗补助支付金额
                                       t.ZY2PSNS    = '{17}', --重症住院就诊人次
                                       t.ZY2ACCT    = '{18}', --重症住院个人账户支付金额
                                       t.ZY2FUND1   = '{19}', --重症住院基本医疗统筹支付金额
                                       t.ZY2FUND2   = '{20}', --重症住院大额医疗支付金额
                                       t.ZY2FUND3   = '{21}', --重症住院医疗补助支付金额
                                       t.ZY3PSNS    = '{22}', --按日包干住院就诊人次
                                       t.ZY3DAYS    = '{23}', --按日包干住院天数
                                       t.ZY3ACCT    = '{24}', --按日包干住院个人账户支付金额
                                       t.ZY3FUND3   = '{25}', --按日包干住院医疗补助支付金额
                                       t.ZY4PSNS    = '{26}', --包干结算就诊人次
                                       t.ZY4ACCT    = '{27}', --包干结算个人账户支付金额
                                       t.ZY4FUND1   = '{28}', --包干结算基本医疗统筹支付金额
                                       t.ZY4FUND2   = '{29}', --包干结算大额医疗支付金额
                                       t.ZY4FUND3   = '{30}', --包干结算医疗补助支付金额
                                       t.APPNO      = '{31}' --清算申请流水号
                                 WHERE t.PERIOD = '{0}' ";
            strSQL = string.Format(strSQL, yl.Period, yl.Operator, yl.Dodate.ToString(), yl.Insuretype, yl.Mzpsns, yl.Mzacct, yl.Mzfund3, 
                yl.Tmpsns, yl.Tmacct, yl.Tmfund1, yl.Tmfund2, yl.Tmfund3, yl.Zy1psns, yl.Zy1acct, yl.Zy1fund1, yl.Zy1fund2, yl.Zy1fund3, 
                yl.Zy2psns, yl.Zy2acct, yl.Zy2fund1, yl.Zy2fund2, yl.Zy2fund3, yl.Zy3psns, yl.Zy3days, yl.Zy3acct, yl.Zy3fund3, yl.Zy4psns, 
                yl.Zy4acct, yl.Zy4fund1, yl.Zy4fund2, yl.Zy4fund3, yl.Appno);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        public DataTable GetPeriod(string typeID)
        {
            string strSQL = string.Empty;
            switch (typeID)
            {
                case "SY"://生育保险
                    strSQL = @" select t.period, t.appno
                                  from guiyang_report_sy t
                                union
                                select 'New' period, '' appno from dual ";
                    break;
                case "JM"://居民保险
                    strSQL = @" select t.period, t.appno
                                  from guiyang_report_jm t
                                union
                                select 'New' period, '' appno from dual ";
                    break;
                case "GS"://工伤保险
                    strSQL = @" select t.period, t.appno
                                  from guiyang_report_gs t
                                union
                                select 'New' period, '' appno from dual ";
                    break;
                case "YL"://医疗保险
                    strSQL = @" select t.period, t.appno
                                  from guiyang_report_yl t
                                union
                                select 'New' period, '' appno from dual ";
                    break;
                default:
                    strSQL = @" select 'New' period, '' appno from dual ";
                    break;
            }

            return HisDBLayer.Common.Query(strSQL);
        }

        /// <summary>
        /// 查询市医保清算数据
        /// </summary>
        /// <param name="type">保险类别</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>DataTable</returns>
        public DataTable QuerySquareData(string type, string beginDate, string endDate)
        {
            string strSQL = string.Empty;
            strSQL = " select * from CITY_CALC t where t.FEECLASS='{0}' and t.QUERYDATE between to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') order by paytype asc";
            strSQL = string.Format(strSQL, type, beginDate, endDate);
            return BaseEntityer.Db.GetDataTable(strSQL);
        }

        /// <summary>
        /// 增加市医保清算数据
        /// </summary>
        /// <returns></returns>
        public int InsertSquareData(BaseEntityer db, GUIYANG_CITY_CALC o, ref string err)
        {
            #region
            try
            {
                string sql = @" insert into CITY_CALC(
                            TJ_YF,
                            PAYTYPE,
                            FEECLASS,
                            times,
                            settletimes,
                            totalhospital,
                            totalinsure,
                            totalown,
                            special,
                            accpay,
                            allowpay,
                            paybase,
                            paybig,
                            ownbase,
                            ownbig,
                            payover,
                            buspay,
                            busover,
                            buscare,
                            appno,
                            operdate,
                            operatorno,
                            querydate,
                            begindate,
                            enddate,
                            applyflag)
                         values('{0}',
                                      '{1}',
                                      '{2}',
                                      {3},
                                      {4},
                                      {5},
                                      {6},
                                      {7},
                                      {8},
                                      {9},
                                      {10},
                                      {11},
                                      {12},
                                      {13},
                                      {14},
                                      {15},
                                      {16},
                                      {17},
                                      {18},
                                      '{19}',
                                      to_date('{20}', 'yyyy-mm-dd hh24:mi:ss'),
                                      '{21}',
                                      to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'),
                                      to_date('{23}', 'yyyy-mm-dd hh24:mi:ss'),
                                      to_date('{24}', 'yyyy-mm-dd hh24:mi:ss'),
                                      {25}
                                      )";
            #endregion

                #region
                object[] os = new object[] { 
                o.TJ_YF,
                o.PAYTYPE,
                o.FEECLASS,o.TIMES,
                o.SETTLETIMES,
                o.TOTALHOSPITAL,
                o.TOTALINSURE,
                o.TOTALOWN,
                o.SPECIAL,
                o.ACCPAY,
                o.ALLOWPAY,
                o.PAYBASE,
                o.PAYBIG,
                o.OWNBASE,
                o.OWNBIG,
                o.PAYOVER,
                o.BUSPAY,
                o.BUSOVER,
                o.BUSCARE,
                o.APPNO,
                o.OPERDATE,
                o.OPERATORNO,
                o.QUERYDATE,
                o.BEGINDATE,
                o.ENDDATE,
                o.APPLYFLAG};
                #endregion

                sql = sql.SqlFormate(os);
                return db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// 修改市医保清算数据
        /// </summary>
        /// <param name="type">保险类别</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>int</returns>
        public int UpdateSquareData(BaseEntityer db, GUIYANG_CITY_CALC obj, string type, string beginDate, string endDate, string paytype, ref string err)
        {
            string strSQL = string.Empty;

            try
            {
                #region
                strSQL = @" update  CITY_CALC t
                                set t.TJ_YF='{0}',
                                  t.PAYTYPE='{22}',
                                  t.FEECLASS='{1}',
                                  t.times={2},
                                  t.settletimes={3},
                                  t.totalhospital={4},
                                  t.totalinsure={5},
                                  t.totalown={6},
                                  t.special={7},
                                  t.accpay={8},
                                  t.allowpay={9},
                                  t.paybase={10},
                                  t.paybig={11},
                                  t.ownbase={12},
                                  t.ownbig={13},
                                  t.payover={14},
                                  t.buspay={15},
                                  t.busover={16},
                                  t.buscare={17},
                                  t.appno='{18}',
                                  t.operdate=to_date('{19}', 'yyyy-mm-dd hh24:mi:ss'),
                                  t.operatorno='{20}',
                                  t.querydate=to_date('{21}', 'yyyy-mm-dd hh24:mi:ss'),
                                  t.begindate=to_date('{23}', 'yyyy-mm-dd hh24:mi:ss'),
                                  t.enddate=to_date('{24}', 'yyyy-mm-dd hh24:mi:ss'),
                                  t.applyflag={25}";
                #endregion

                #region
                strSQL = string.Format(strSQL,
                        obj.TJ_YF,
                        obj.FEECLASS,
                        obj.TIMES,
                        obj.SETTLETIMES,
                        obj.TOTALHOSPITAL,
                        obj.TOTALINSURE,
                        obj.TOTALOWN,
                        obj.SPECIAL,
                        obj.ACCPAY,
                        obj.ALLOWPAY,
                        obj.PAYBASE,
                        obj.PAYBIG,
                        obj.OWNBASE,
                        obj.OWNBIG,
                        obj.PAYOVER,
                        obj.BUSPAY,
                        obj.BUSOVER,
                        obj.BUSCARE,
                        obj.APPNO,
                        obj.OPERDATE,
                        obj.OPERATORNO,
                        obj.QUERYDATE,
                        obj.PAYTYPE,
                        obj.BEGINDATE,
                        obj.ENDDATE,
                        obj.APPLYFLAG
                        );
                #endregion

                strSQL += " where t.FEECLASS='{0}' and t.QUERYDATE between to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') and t.PAYTYPE='{3}'";
                strSQL = string.Format(strSQL, type, beginDate, endDate, paytype);
                return db.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// 修改市医保清算数据
        /// </summary>
        /// <param name="type">保险类别</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>int</returns>
        public int UpdateSquareDataApplyFlag(BaseEntityer db, string applyflag, string appno, string type, string beginDate, string endDate, ref string err)
        {
            string strSQL = string.Empty;

            try
            {
                #region
                strSQL = @" update  CITY_CALC t
                                set t.applyflag={0},
                                     t.appno='{1}'";
                #endregion

                strSQL = string.Format(strSQL, applyflag, appno);

                strSQL += " where t.FEECLASS='{0}' and t.QUERYDATE between to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') ";
                strSQL = string.Format(strSQL, type, beginDate, endDate);
                return db.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// 删除市医保清算数据
        /// </summary>
        /// <param name="type">保险类别</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>int</returns>
        public int DeleteSquareData(string type, string beginDate, string endDate, ref string err)
        {
            string strSQL = string.Empty;
            try
            {
                strSQL = " delete from CITY_CALC t where t.FEECLASS='{0}' and t.QUERYDATE between to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') and t.ApplyFlag = '0' ";
                strSQL = string.Format(strSQL, type, beginDate, endDate);

                int rev = BaseEntityer.Db.ExecuteNonQuery(strSQL);

                if (rev > 0)
                {
                    return rev;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return 0;
            }
        }

        #endregion

        #region 贵阳省正医保

        #region 目录下载

        #region 服务项目大类

        /// <summary>
        /// 插入服务项目大类
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int InsertGYSERVICEITEMS(HisCommon.DataEntity.SI_GYSERVICEITEMS obj, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"INSERT  INTO SI_GYSERVICEITEMS  t   --贵阳医保服务项目下载用表
(
t.CHARGE_CODE,   --费别编码
t.NAME,   --名称
t.CODE   --编码
) 
VALUES
(
'{0}',   --费别编码
'{1}',   --名称
'{2}'   --编码
) ";
                #endregion

                sql = string.Format(sql,
                #region
 obj.Charge_code,
                    obj.Name,
                    obj.Code
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 删除服务项目大类
        /// </summary>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int DeleteGYSERVICEITEMS(BaseEntityer db, string pactcode, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"delete from SI_GYSERVICEITEMS where charge_code = '{0}'";
                #endregion
                sql = string.Format(sql, pactcode);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }


        /// <summary>
        /// 获取服务项目大类
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<SI_GYSERVICEITEMS> GetGYSERVICEITEMS(string charge_code, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<SI_GYSERVICEITEMS> obj = new List<SI_GYSERVICEITEMS>();
            string sql = @"SELECT
t.CHARGE_CODE,   --费别编码
t.NAME,   --名称
t.CODE   --编码
FROM
SI_GYSERVICEITEMS  t   --贵阳医保服务项目下载用表
WHERE t.CHARGE_CODE='{0}'";
            sql = string.Format(sql, charge_code);
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SI_GYSERVICEITEMS item = new SI_GYSERVICEITEMS();
                    item.Code = dt.Rows[i][2].ToString();
                    item.Name = dt.Rows[i][1].ToString();
                    item.Charge_code = dt.Rows[i][0].ToString();
                    obj.Add(item);
                }

            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return obj;
        }

        #endregion

        #region ICD-10目录

        /// <summary>
        /// 插入服务项目大类
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int InsertGYICD(HisCommon.DataEntity.SI_GYICD obj, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"INSERT INTO SI_GYICD  t   --贵阳省正医保ICD-10码下载
(
t.ICDCODE,   --ICD编码
t.ICDNAME,   --ICD名称
t.ICDSPELL,   --ICD拼音码
t.REMARK,   --备注
t.CHARGE_CODE   --费用类型
) 
VALUES
(
'{0}',   --ICD编码
'{1}',   --ICD名称
'{2}',   --ICD拼音码
'{3}',   --备注
'{4}'   --费用类型
) ";
                #endregion

                sql = string.Format(sql,
                #region
 obj.Icdcode,
                    obj.Icdname,
                    obj.Icdspell,
                    obj.Remark,
                    obj.Charge_code
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 删除服务项目大类
        /// </summary>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int DeleteGYICD(BaseEntityer db, string pactcode, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"delete from SI_GYICD where charge_code = '{0}'";
                #endregion
                sql = string.Format(sql, pactcode);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }


        /// <summary>
        /// 获取服务项目大类
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<SI_GYICD> GetGYICD(string charge_code, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<SI_GYICD> obj = new List<SI_GYICD>();
            string sql = @"SELECT
t.ICDCODE,   --ICD编码
t.ICDNAME,   --ICD名称
t.ICDSPELL,   --ICD拼音码
t.REMARK,   --备注
t.CHARGE_CODE   --费用类型
FROM
SI_GYICD  t   --贵阳省正医保ICD-10码下载
WHERE t.CHARGE_CODE='{0}'";
            sql = string.Format(sql, charge_code);
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SI_GYICD item = new SI_GYICD();
                    item.Icdcode = dt.Rows[i][0].ToString();
                    item.Icdname = dt.Rows[i][1].ToString();
                    item.Icdspell = dt.Rows[i][2].ToString();
                    item.Remark = dt.Rows[i][3].ToString();
                    item.Charge_code = dt.Rows[i][4].ToString();
                    obj.Add(item);
                }

            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return obj;
        }


        #endregion

        #region 病种就诊结算信息

        /// <summary>
        /// 插入服务项目大类
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int InsertGYBALANCE(HisCommon.DataEntity.SI_GYBALANCE obj, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"INSERT INTO SI_GYBALANCE  t   --病种就诊结算信息
(
t.BALANCE_NO,   --编码
t.NAME,   --名称
t.BALANCETYPE,   --就诊结算方式
t.SPELLCODE,   --拼音码
t.CHARGE_CODE   --费别分类
) 
VALUES
(
'{0}',   --编码
'{1}',   --名称
'{2}',   --就诊结算方式
'{3}',   --拼音码
'{4}'   --费别分类
) ";
                #endregion

                sql = string.Format(sql,
                #region
 obj.Balance_no,
                    obj.Name,
                    obj.Balancetype,
                    obj.Spellcode,
                    obj.Charge_code
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 删除服务项目大类
        /// </summary>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int DeleteGYBALANCE(BaseEntityer db, string pactcode, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"delete from SI_GYBALANCE where charge_code = '{0}'";
                #endregion
                sql = string.Format(sql, pactcode);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }


        /// <summary>
        /// 获取服务项目大类
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<SI_GYBALANCE> GetGYBALANCE(string charge_code, ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<SI_GYBALANCE> obj = new List<SI_GYBALANCE>();
            string sql = @"SELECT
t.BALANCE_NO,   --编码
t.NAME,   --名称
t.BALANCETYPE,   --就诊结算方式
t.SPELLCODE,   --拼音码
t.CHARGE_CODE   --费别分类
FROM
SI_GYBALANCE  t   --病种就诊结算信息
WHERE t.CHARGE_CODE='{0}'";
            sql = string.Format(sql, charge_code);
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SI_GYBALANCE item = new SI_GYBALANCE();
                    item.Balance_no = dt.Rows[i][0].ToString();
                    item.Name = dt.Rows[i][1].ToString();
                    item.Balancetype = dt.Rows[i][2].ToString();
                    item.Spellcode = dt.Rows[i][3].ToString();
                    item.Charge_code = dt.Rows[i][4].ToString();
                    obj.Add(item);
                }

            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return obj;
        }


        #endregion

        #endregion

        #region 获取医保对照信息（上传用）
        /// <summary>
        /// 获取医保对照信息（上传用）
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetGYHisCompareUpLoad(string typeCode, ref string errMsg)
        {
            DataTable compareList = new DataTable();
            string sql =
            #region
 @"select HIS_CODE as 医院项目编码,--0
                               CENTER_CODE as 医保项目编码,--1
                               FEE_TYPE as 项目类型,--2
                               CENTER_NAME as 医保项目名称,--3
                               CENTER_SPECS as 医保规格,--4
                               CENTER_UNIT as 医保项目单位,--5
                               CENTER_PRICE as 医保价格,--6
                               CENTER_TYPE as 医保剂型,--7
                               CENTER_RATE as 自付比例,--8
                               CENTER_PACK as 医保包装数量,--9
                               CENTER_PLACE as 医保产地,--10
                               HIS_NAME as 医院项目名称,--11
                               HIS_SPELL as 医院拼音,--12
                              ( select DRUG_SPEC from
                               drug_price_list where   drug_code = his_code and stop_date is   null)  as 医院规格,--13
                               ( select FIRM_ID from
                               drug_price_list where   drug_code = his_code and stop_date is   null)  as 医院项目单位,--14
                               HIS_PRICE as 医院价格,--15
                               HIS_TYPE as 医院剂型,--16                               
                              ( select amount_per_package from
                               drug_price_list where   drug_code = his_code and stop_date is   null)        as 医院包装数量,--17
                               HIS_PLACE as 医院产地,--18
                               OPER_CODE as 操作员,--19
                               OPER_DATE as 操作日期,--20
                               APPLYFLAG as 申请标志,--21
                               PERSONRATE as 个人比例,--22
                               HIS_WB_CODE as 五笔码,--23
                               HIS_USER_CODE as 自定义码,--24
                               TRANS as 交易类型,--25
                               HIS_CLASS as 医院价表类别,--26
                               CENTER_CLASS as 中心项目类别,--27
                               CHARGE_TYPE_CODE as 费别编码,--28
                               DRUG_TABOO as 药品禁忌,--29
                               UNTOWARD_REACTION as 不良反应,--30
                               PRECAUTIONS as 注意事项,--31
                               FEE_ITEMGRADE as 收费项目种类,--32
                               DOSAGE as 药品注册剂型,--33
                               USAGE as 用法,--34
                               DOSAGE_UNIT as 药品剂量单位,--35
                               ONCE_DOSAGE as 每次用量,--36
                               FREQUENCY as 使用频次,--37
                               DRUG_COMMON_LIMIT_FLAG as 药品普通限制标识,--38
                               DRUG_SPECIAL_LIMIT_FLAG as 药品特殊限制标志,--39
                               MATERIAL_LIMITUSE_FLAG as 材料限制使用标识,--40
                               ISNEED_SITECODE as 是否需要传入部位码,--41
                           nvl( (select RATIFY_NO from drug_price_list  where drug_code = his_code and stop_date is   null  ),'')  as  批准文号 

                          from HIS_COMPARE
                         where CHARGE_TYPE_CODE = '{0}'
                            and ISNEED_SITECODE is null";
            #endregion
            sql = string.Format(sql, typeCode);
            try
            {
                compareList = BaseEntityer.Db.GetDataTable(sql);
                if (compareList.Rows.Count <= 0)
                {
                    errMsg = "没有查询到对照数据";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return compareList;
        }


        /// <summary>
        /// 获取医保对照信息（上传用）
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetGYQueryHisCompareUpLoad(string typeCode,string Code, ref string errMsg)
        {
            DataTable compareList = new DataTable();
            string sql =
            #region
 @"select HIS_CODE as 医院项目编码,--0
                               CENTER_CODE as 医保项目编码,--1
                               FEE_TYPE as 项目类型,--2
                               CENTER_NAME as 医保项目名称,--3
                               CENTER_SPECS as 医保规格,--4
                               CENTER_UNIT as 医保项目单位,--5
                               CENTER_PRICE as 医保价格,--6
                               CENTER_TYPE as 医保剂型,--7
                               CENTER_RATE as 自付比例,--8
                               CENTER_PACK as 医保包装数量,--9
                               CENTER_PLACE as 医保产地,--10
                               HIS_NAME as 医院项目名称,--11
                               HIS_SPELL as 医院拼音,--12
                              ( select DRUG_SPEC from
                               drug_price_list where   drug_code = his_code and stop_date is   null)  as 医院规格,--13
                               ( select FIRM_ID from
                               drug_price_list where   drug_code = his_code and stop_date is   null)  as 医院项目单位,--14
                               HIS_PRICE as 医院价格,--15
                               HIS_TYPE as 医院剂型,--16                               
                              ( select amount_per_package from
                               drug_price_list where   drug_code = his_code and stop_date is   null)        as 医院包装数量,--17
                               HIS_PLACE as 医院产地,--18
                               OPER_CODE as 操作员,--19
                               OPER_DATE as 操作日期,--20
                               APPLYFLAG as 申请标志,--21
                               PERSONRATE as 个人比例,--22
                               HIS_WB_CODE as 五笔码,--23
                               HIS_USER_CODE as 自定义码,--24
                               TRANS as 交易类型,--25
                               HIS_CLASS as 医院价表类别,--26
                               CENTER_CLASS as 中心项目类别,--27
                               CHARGE_TYPE_CODE as 费别编码,--28
                               DRUG_TABOO as 药品禁忌,--29
                               UNTOWARD_REACTION as 不良反应,--30
                               PRECAUTIONS as 注意事项,--31
                               FEE_ITEMGRADE as 收费项目种类,--32
                               DOSAGE as 药品注册剂型,--33
                               USAGE as 用法,--34
                               DOSAGE_UNIT as 药品剂量单位,--35
                               ONCE_DOSAGE as 每次用量,--36
                               FREQUENCY as 使用频次,--37
                               DRUG_COMMON_LIMIT_FLAG as 药品普通限制标识,--38
                               DRUG_SPECIAL_LIMIT_FLAG as 药品特殊限制标志,--39
                               MATERIAL_LIMITUSE_FLAG as 材料限制使用标识,--40
                               ISNEED_SITECODE as 是否需要传入部位码,--41
                           nvl( (select RATIFY_NO from drug_price_list  where drug_code = his_code and stop_date is   null  ),'')  as  批准文号 

                          from HIS_COMPARE
                         where CHARGE_TYPE_CODE = '{0}'
                            and CENTER_CODE='{1}'
                            and ISNEED_SITECODE='1'";
            #endregion
            sql = string.Format(sql, typeCode, Code);
            try
            {
                compareList = BaseEntityer.Db.GetDataTable(sql);
                if (compareList.Rows.Count <= 0)
                {
                    errMsg = "没有查询到对照数据";
                    return null;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return compareList;
        }

        #endregion

        #region 修改对照信息
       /// <summary>
        /// 修改对照信息上传标示
       /// </summary>
       /// <param name="db"></param>
       /// <param name="hisCode"></param>
       /// <param name="typeCode"></param>
       /// <param name="err"></param>
       /// <returns></returns>
        public int UpdateHisCompareFlag(BaseEntityer db, string hisCode, string typeCode, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"update his_compare
                           set ISNEED_SITECODE = 1
                          where HIS_CODE = '{0}'
                           and CHARGE_TYPE_CODE = '{1}'";
                sql = string.Format(sql, hisCode, typeCode);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 交互信息

        /// <summary>
        /// 插入交互信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="err"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int InsertGYINFORMATIONTABLE(HisCommon.DataEntity.SI_GYINFORMATIONTABLE obj, ref string err, ref string sql)
        {
            int exec = 0;
            try
            {
                sql =
                #region
 @"INSERT INTO SI_GYINFORMATIONTABLE  t   --贵样省正医保交互信息表
(
t.SERIAL_NO,   --信息流水号
t.TREATMENT_NO,   --就诊编号
t.EXAMINEFLAG,   --审核标志
t.TYPE,   --信息类别
t.INTERACTIVESTATE,   --信息状态
t.INFORMATION,   --交互信息
t.SUBMITOPER,   --提交人
t.SUBMITDATE,   --提交时间
t.MECHANISM,   --社保经办机构
t.SOURCE,   --信息来源
t.HOSPITALCODE   --医院编码
) 
VALUES
(
'{0}',   --信息流水号
'{1}',   --就诊编号
'{2}',   --审核标志
'{3}',   --信息类别
'{4}',   --信息状态
'{5}',   --交互信息
'{6}',   --提交人
TO_DATE('{7}','YYYY-MM-DD HH24:MI:SS'),   --提交时间
'{8}',   --社保经办机构
'{9}',   --信息来源
'{10}'   --医院编码
) ";
                #endregion

                sql = string.Format(sql,
                #region
                    obj.Serial_no,
                    obj.Treatment_no,
                    obj.Examineflag,
                    obj.Type,
                    obj.Interactivestate,
                    obj.Information,
                    obj.Submitoper,
                    obj.Submitdate,
                    obj.Mechanism,
                    obj.Source,
                    obj.Hospitalcode
                #endregion
);
                exec = BaseEntityer.Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
        }

        /// <summary>
        /// 获取服务信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<SI_GYINFORMATIONTABLE> GetGYINFORMATIONTABLE(ref string errMsg)
        {
            DataTable dt = new DataTable();
            List<SI_GYINFORMATIONTABLE> obj = new List<SI_GYINFORMATIONTABLE>();
            string sql = @"SELECT
t.SERIAL_NO ,   --信息流水号
t.TREATMENT_NO ,   --就诊编号
t.EXAMINEFLAG,   --审核标志
t.TYPE ,   --信息类别
t.INTERACTIVESTATE,   --信息状态
t.INFORMATION,   --交互信息 
t.SUBMITOPER ,   --提交人
t.SUBMITDATE ,   --提交时间
t.MECHANISM ,   --社保经办机构
t.SOURCE ,   --信息来源
t.HOSPITALCODE    --医院编码
FROM
SI_GYINFORMATIONTABLE  t   --贵样省正医保交互信息表
where t.CONFIRMFLAG='0'";
            //sql = string.Format(sql, charge_code);
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SI_GYINFORMATIONTABLE item = new SI_GYINFORMATIONTABLE();
                    item.Serial_no=dt.Rows[i][0].ToString();
                    item.Treatment_no=dt.Rows[i][1].ToString();
                    item.Examineflag=dt.Rows[i][2].ToString();
                    item.Type = dt.Rows[i][3].ToString();
                    item.Interactivestate=dt.Rows[i][4].ToString();
                    item.Information=dt.Rows[i][5].ToString();
                    item.Submitoper=dt.Rows[i][6].ToString();
                    item.Submitdate=Convert.ToDateTime(dt.Rows[i][7].ToString());
                    item.Mechanism=dt.Rows[i][8].ToString();
                    item.Source=dt.Rows[i][9].ToString();
                    item.Hospitalcode = dt.Rows[i][10].ToString();
                    obj.Add(item);
                }

            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return obj;
        }

        /// <summary>
        /// 修改确认标示
        /// </summary>
        /// <param name="db"></param>
        /// <param name="hisCode"></param>
        /// <param name="typeCode"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateGetGYINFORMATIONTABLEFlag(BaseEntityer db, string hisCode, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"update si_gyinformationtable
                           set CONFIRMFLAG = 1
                          where SERIAL_NO = '{0}'";
                sql = string.Format(sql, hisCode);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }

        #endregion

        #endregion

        #region 贵阳铁路医保

        /// <summary>
        /// 获取挂号登记信息根据医保卡号
        /// </summary>
        /// <param name="personNO"></param>
        /// <param name="typecode"></param>
        /// <param name="chargeCode"></param>
        /// <returns></returns>
        public SIInfo GetSIINFORegByPersonNO(string personNO, string typecode, string chargeCode)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();
            string sql =
            #region
 @"select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO
              from SIINFO
             where Person_No='{0}'
               and BALANCE_NO = (select min(to_number(t.BALANCE_NO))
                                   from SIINFO t
                                  where t.Person_No='{0}'
                                    and t.TYPE_CODE = '{1}'
                                    and t.ISVALID = '1'
                                    and t.pact_code='{2}')
               and BALANCE_STATE = '0'
               and TRANS_TYPE = '1'
               and ISVALID = 1
               and pact_code='{2}'
               and TYPE_CODE = '{1}'
               order by   Oper_Date desc ";
            #endregion
            sql = string.Format(sql, personNO, typecode, chargeCode);
            try
            {
                #region
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                #endregion
            }
            catch
            {
                return null;
            }
            return info;
        }

        /// <summary>
        ///  获得医保出院登记信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="chargeCode"></param>
        /// <param name="errSql"></param>
        /// <returns></returns>
        public int GetSiinfoLoginInfo(string patientID, string visitID, string chargeCode, ref  string errSql)
        {
            string strSQL = @"
                                    SELECT COUNT(1)
                                      FROM siinfo t
                                     WHERE t.inpatient_id = '{0}'
                                       AND t.visit_id =  '{1}'
                                       AND t.balance_state = '0'
                                       AND t.app_flag = '1'
                                       and t.type_code='2'
                                       and t.trans_type='1'
                                       and t.pact_code='{2}'";
            try
            {
                strSQL = string.Format(strSQL, patientID, visitID, chargeCode);
                var BED_NO = BaseEntityer.Db.ExecuteScalar(strSQL);
                if (BED_NO != null)
                    return int.Parse(BED_NO.ToString());
                else
                    return -1;
            }
            catch (Exception e)
            {
                errSql = e.Message;
                return -1;
            }
        }

        /// <summary>
        ///  根据时间段来查询医保结算信息
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="chargeCode"></param>
        /// <param name="errSql"></param>
        /// <returns></returns>
        public DataTable QuerySiInfoByDateRange(string beginDate, string endDate, string chargeCode, ref  string errSql)
        {
            string strSQL = @" select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,                  
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO,INVOICE_NEW
  FROM siinfo t
 WHERE t.oper_date >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
   AND t.oper_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
   AND t.balance_state = '1'
   AND t.type_code = '2'
   AND t.trans_type = '1'
   AND t.pact_code = '{2}'
   AND t.insurancetype = '1'
 ";
            strSQL = string.Format(strSQL, beginDate.ToString(), endDate.ToString(), chargeCode);

            try
            {
                return BaseEntityer.Db.GetDataTable(strSQL);
            }
            catch (Exception e)
            {
                errSql += e.Message;
                return null;
            }
        }

        /// <summary>
        /// 获得医保中心的结算的单据号
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="chargeCode"></param>
        /// <returns></returns>
        public SIInfo GetSiInfoByInvoiceNO(string invoiceNO, string patientID, string visitID, string chargeCode)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();

            string strSQL = string.Empty;
            // 结算标志，有效状态，正交易
            strSQL = @" select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,                  
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO,INVOICE_NEW
  FROM siinfo t
 WHERE t.inpatient_id = '{0}'
   AND t.visit_id = '{1}'
   AND t.invoice_no = '{2}'
   AND t.pact_code = '{3}'
   AND t.Balance_State = '1'
   AND t.isvalid = '1'
   AND t.trans_type = '1'

 ";
            strSQL = string.Format(strSQL, patientID, visitID, invoiceNO, chargeCode);

            try
            {
                #region
                dt = BaseEntityer.Db.GetDataTable(strSQL);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                info.INVOICE_NEW = dt.Rows[0][117].ToString();
                #endregion
            }
            catch
            {
                return null;
            }
            return info;
        }

        /// <summary>
        /// 获取医保中间表数据
        /// </summary>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="patientID">患者编号</param>
        /// <param name="visitID">次数</param>
        /// <param name="chargeCode">合同单位</param>
        /// <param name="inState">true门诊挂号false门诊收费</param>
        /// <returns></returns>
        public SIInfo GetSIInfo(string invoiceNO, string patientID, string visitID, string chargeCode, bool inState)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();

            #region strSQL

            string strSQL = string.Empty;
            // 结算标志，有效状态，正交易
            strSQL = @" select INPATIENT_ID,
                               VISIT_ID,
                               BALANCE_NO,
                               NAME,
                               SEX,
                               BIRTHDAY,
                               IDENNO,
                               PACT_CODE,
                               PACT_NAME,
                               REGISTER_NO,
                               HOSPITAL_NO,
                               HOSPITAL_NAME,
                               HOSPITAL_GRADE,
                               IMP_NO,
                               RUNNING_NO,
                               INVOICE_NO,                  
                               MEDICAL_TYPE,
                               CARD_NO,
                               MCARD_NO,
                               PERSON_NO,
                               SI_BEGINDATE,
                               SI_STATE,
                               ICCARD_NO,
                               OVERAL_NO,
                               FUND_NO,
                               FUND_NAME,
                               BUSINESSSEQUENCE,
                               APPLYSEQUENCE,
                               ANOTHERCITY_NO,
                               ANOTHERCITY_NAME,
                               CORPORATION_NO,
                               CORPORATION_NAME,
                               INSURANCETYPE,
                               EMPL_TYPE,
                               BED_NO,
                               ISBALANCED,
                               INDOCTOR_NO,
                               INDOCTOR_NAME,
                               OUTDOCTOR_NO,
                               OUTDOCTOR_NAME,
                               OUTREASON,
                               PAYUSERFLAG,
                               CLINICDIAGNOSE,
                               INDIAGNOSE_NO,
                               INDIAGNOSE_NAME,
                               INDIAGNOSE_DATE,
                               OUTDIAGNOSE_NO,
                               OUTDIAGNOSE_NAME,
                               OUTDIAGNOSE_DATE,
                               INHOSPITAL_DATE,
                               OUTHOSPITAL_DATE,
                               BALANCE_DATE,
                               BALANCE_STATE,
                               OPER_NO,
                               OPER_NAME,
                               OPER_DATE,
                               CLINICDEPT_NO,
                               CLINICDEPT_NAME,
                               CLINICDOCTOR_NO,
                               CLINICDOCTOR_NAME,
                               INDEPT_NO,
                               INDEPT_NAME,
                               OUTDEPT_NO,
                               OUTDEPT_NAME,
                               TOT_COST,
                               PAY_COST,
                               PUB_COST,
                               OWN_COST,
                               OFFICIAL_COST,
                               OVER_COST,
                               BASE_COST,
                               OWN_SUPPLE_COST,
                               HELP_ALLOWANCES_COST,
                               ENTERPRISE_SUPPLE_COST,
                               HELP_OWN_COST,
                               YEAR_TOT_COST,
                               YEAR_PUB_COST,
                               YEAR_PAY_COST,
                               YEAR_OWN_COST,
                               YEAR_OFFICIAL_COST,
                               YEAR_BASE_COST,
                               YEAR_HELP_COST,
                               YEAR_SPECIALMED_COST,
                               INDIVIDUAL_COST,
                               SPECIALMED_TOTCOST,
                               SPECIALMED_PUBCOST,
                               SPECIALMED_BASECOST,
                               LXPUB_COST,
                               YLOWN_COST,
                               TCOWN_COST,
                               DEOWN_COST,
                               EXCEEDLIMIT_OWNCOST,
                               SEALTOPLINE_OWNCOST,
                               ENTERPRISEADD_COST,
                               APPINFO_NO,
                               APPINFO_NAME,
                               APPINFO_MEMO,
                               APPTYPE_NO,
                               APPTYPE_NAME,
                               APPTYPE_MEMO,
                               APP_FLAG,
                               APP_DATE,
                               CARDVALIDDATE,
                               SHIFTDATE,
                               INHOSTIMES,
                               ISVALID,
                               PC_NO,
                               REGINFORETURN,
                               READCARDRETURN,
                               BANINFORETURN,
                               REMARK,
                               TYPE_CODE,
                               TRANS_TYPE,
                               CENTER_BIZCYCLENO,
                               HIS_BIZCYCLENO,
                               CENTER_BUSSINESSSEQNO,
                               HIS_BUSSINESSSEQNO,INVOICE_NEW
              FROM siinfo t
             WHERE t.inpatient_id = '{0}'
               AND t.visit_id = '{1}'
               AND t.invoice_no = '{2}'
               AND t.pact_code = '{3}'
               AND t.Balance_State = '{4}'
               AND t.isvalid = '1'
               AND t.trans_type = '1' ";

            #endregion

            string inFlag = inState ? "0" : "1";

            strSQL = string.Format(strSQL, patientID, visitID, invoiceNO, chargeCode, inFlag);

            try
            {
                #region
                dt = BaseEntityer.Db.GetDataTable(strSQL);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                info.INVOICE_NEW = dt.Rows[0][117].ToString();
                #endregion
            }
            catch
            {
                return null;
            }
            return info;
        }

        /// <summary>
        ///  获得最后一条清算单记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetLastLiquidationBill(ref string  errSql)
        {
            try
            {
                string sql = @"
                                SELECT r.*
                                  FROM tlsiliquidrecord r
                                 WHERE r.enddate = (SELECT MAX(t.enddate) FROM tlsiliquidrecord t)";

                return BaseEntityer.Db.GetDataTable(sql);

            }
            catch (Exception e)
            {
                errSql = e.Message;
            }
            return null;
        }

        /// <summary>
        ///  门诊没有清算的记录信息
        /// </summary>
        /// <param name="begDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errSql"></param>
        /// <returns></returns>
        public DataTable QueryClinicLiquidationByDate(string begDate,string endDate,ref string errSql)
        {
            try
            {
                string sql = @"
                               
                            SELECT t.inpatient_id,
                                   t.visit_id,
                                   t.balance_no,
                                   t.person_no,
                                   t.name,
                                   t.empl_type,
  t.Balance_State,
                                   t.center_bussinessseqno,
                                   t.oper_date,
                                   t.tot_cost,
                                   t.pay_cost,
                                   t.fund_no,
                                   t.own_cost,
                                   t.pub_cost
                              FROM siinfo t
                             WHERE t.pact_code = '30'
                               AND t.appinfo_memo is null
                              AND t.oper_date > to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                               AND t.oper_date <= to_date( '{1}','yyyy-MM-dd hh24:mi:ss')
                               AND t.type_code = '1'
                               AND t.isvalid = '1'
                               AND (t.balance_state = '1' OR
                                   (t.balance_state = '0' AND t.balance_no = '1'))";

                sql= string.Format(sql, begDate, endDate);
                return BaseEntityer.Db.GetDataTable(sql);

            }
            catch (Exception e)
            {
                errSql = e.Message;
            }
            return null;
        }

        /// <summary>
        ///  住院没有清算的记录信息
        /// </summary>
        /// <param name="begDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errSql"></param>
        /// <returns></returns>
        public DataTable QueryInpatLiquidationByDate(string begDate, string endDate, ref string errSql)
        {
            try
            {
                string sql = @"
                               
                            SELECT t.inpatient_id,
                                   t.visit_id,
                                   t.balance_no,
                                   t.person_no,
                                   t.name,
                                   t.empl_type,
                                   t.center_bussinessseqno,
                                   t.oper_date,
                                   t.tot_cost,
                                   t.pay_cost,
                                   t.fund_no,
                                   t.own_cost,
                                   t.pub_cost
                              FROM siinfo t
                             WHERE t.pact_code = '30'
                                   AND t.appinfo_memo  is null
                                   AND t.oper_date > to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                   AND t.oper_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                   AND t.type_code = '2'
                                   AND t.isvalid = '1'
                                   AND t.balance_state = '1'
                                   AND (t.insurancetype = '1' OR t.pc_no = 1)
                                ";

                sql = string.Format(sql, begDate, endDate);
                return BaseEntityer.Db.GetDataTable(sql);

            }
            catch (Exception e)
            {
                errSql = e.Message;
            }
            return null;
        }

        /// <summary>
        ///  更新状态信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="balanceNo"></param>
        /// <param name="serialNO"></param>
        /// <returns></returns>
        public int UpdateSIInfoApplyMemoInfoByIndex(BaseEntityer db, string patientID, string visitID, string balanceNo, string serialNO,ref  string errSql)
        {
            try
            {
                string sql = @"UPDATE siinfo t
   SET t.appinfo_memo = '1'
 WHERE t.inpatient_id = '{0}'
   AND t.visit_id = '{1}'
   AND t.balance_no = '{2}'
   AND t.center_bussinessseqno = '{3}'";

                sql = string.Format(sql, patientID, visitID, balanceNo, serialNO);
                return db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                errSql = ex.Message;
            }
            return -1;
        }

        /// <summary>
        ///  插入中心记录表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="serialNO"></param>
        /// <param name="typeCode"></param>
        /// <param name="begDate"></param>
        /// <param name="endDate"></param>
        /// <param name="balanceNO"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <param name="errSql"></param>
        /// <returns></returns>
        public int InsertLiqdationRecord(BaseEntityer db,string serialNO, string typeCode, string begDate, string endDate, string liqData, string operCode, string operDate,ref  string errSql)
        {
            try
            {
                string sql = @"INSERT INTO TLSILIQUIDRECORD  
                                            (
                                            OPERDATE,
                                            OPERCODE,
                                            TYPECODE,
                                            LIQUDATE,
                                            ENDDATE,
                                            BEGDATE,
                                            SERIALNUM
                                            ) 
                                            VALUES
                                            (
                                            TO_DATE('{0}','YYYY-MM-DD HH24:MI:SS'),
                                            '{1}',
                                            '{2}',
                                            '{3}',
                                            TO_DATE('{4}','YYYY-MM-DD HH24:MI:SS'),
                                            TO_DATE('{5}','YYYY-MM-DD HH24:MI:SS'),
                                            '{6}'
                                            ) ";

                sql = string.Format(sql, operDate, operCode, typeCode, liqData, endDate,begDate,serialNO);
                return db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                errSql = ex.Message;
            }
            return -1;
        }

        /// <summary>
        ///  获取清算流水号
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetLiqudationSerial(ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select TLSILIQUIDRECORD_SEQ.nextval from dual";
            try
            {
                return BaseEntityer.Db.ExecuteScalar<string>(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;

            }
            return string.Empty;
        }

        /// <summary>
        ///  插入清算记录明细
        /// </summary>
        /// <param name="db"></param>
        /// <param name="serialNO"></param>
        /// <param name="typeCode"></param>
        /// <param name="temp"></param>
        /// <param name="errSql"></param>
        /// <returns></returns>
        public int InsertLiqudationDetail(BaseEntityer db, string serialNO, string typeCode, SI_GYLiquidatDetail temp, ref string errSql)
        {
            try
            {
                string sql = @"INSERT INTO TLSILIQUIDTOTCOST  
                                        (
                                        ACCOUNTCOST,
                                        FUNDCOST,
                                        TOTCOST,
                                        OWNCOST,
                                        OTHERCOST,
                                        PUBCOST,
                                        PATIENTTYPE,
                                        SERIALNUM,
                                        SEQID
                                        ) 
                                        VALUES
                                        (
                                        '{0}',
                                        '{1}',
                                        '{2}',
                                        '{3}',
                                        '{4}',
                                        '{5}',
                                        '{6}',
                                        '{7}',
                                        TLSILIQUIDTOTCOST_seq.nextval
                                        ) ";

                sql = string.Format(sql, temp.PayCost, temp.FundCost, temp.TotCost, temp.OwnCost, temp.PubCost - temp.FundCost, temp.PubCost, typeCode, serialNO);
                return db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                errSql = ex.Message;
            }
            return -1;
        }

        /// <summary>
        /// 修改医保主表有效标志(门诊)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateSIInfoValidFlagByTLOutpatient(BaseEntityer db, string patientid, string visitid, string visitDate, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"update SIINFO
                           set ISVALID = 0,
                               Individual_Cost=Individual_Cost+Pay_Cost
                         where INPATIENT_ID = '{0}'
                           and VISIT_ID = '{1}'
                           and SHIFTDATE = to_date('{2}','yyyy-MM-dd')
                           and ISVALID = 1
                         and  Balance_No='1' ";
                sql = string.Format(sql, patientid, visitid, visitDate);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 贵阳省异地医保
        /// <summary>
        /// 修改医保主表出院登记有效标志(住院)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateLogoutSIInfoValidFlagByInpatient(BaseEntityer db, string patientid, string visitid, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"UPDATE siinfo
   SET isvalid = 0
 WHERE inpatient_id = '{0}'
   AND visit_id = '{1}'
   AND isvalid = 1
   AND type_code = '2'
   AND app_flag = '1'
";
                sql = string.Format(sql, patientid, visitid);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }


        /// <summary>
        ///  获得医保中心挂号费明细
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="chargeCode"></param>
        /// <returns></returns>
        public SIInfo GetRegFeeSiInfoByInvoiceNO(string invoiceNO, string patientID, string visitID, string chargeCode)
        {
            DataTable dt = new DataTable();
            SIInfo info = new SIInfo();

            string strSQL = string.Empty;
            // 结算标志，有效状态，正交易
            strSQL = @" select INPATIENT_ID,
                   VISIT_ID,
                   BALANCE_NO,
                   NAME,
                   SEX,
                   BIRTHDAY,
                   IDENNO,
                   PACT_CODE,
                   PACT_NAME,
                   REGISTER_NO,
                   HOSPITAL_NO,
                   HOSPITAL_NAME,
                   HOSPITAL_GRADE,
                   IMP_NO,
                   RUNNING_NO,
                   INVOICE_NO,                  
                   MEDICAL_TYPE,
                   CARD_NO,
                   MCARD_NO,
                   PERSON_NO,
                   SI_BEGINDATE,
                   SI_STATE,
                   ICCARD_NO,
                   OVERAL_NO,
                   FUND_NO,
                   FUND_NAME,
                   BUSINESSSEQUENCE,
                   APPLYSEQUENCE,
                   ANOTHERCITY_NO,
                   ANOTHERCITY_NAME,
                   CORPORATION_NO,
                   CORPORATION_NAME,
                   INSURANCETYPE,
                   EMPL_TYPE,
                   BED_NO,
                   ISBALANCED,
                   INDOCTOR_NO,
                   INDOCTOR_NAME,
                   OUTDOCTOR_NO,
                   OUTDOCTOR_NAME,
                   OUTREASON,
                   PAYUSERFLAG,
                   CLINICDIAGNOSE,
                   INDIAGNOSE_NO,
                   INDIAGNOSE_NAME,
                   INDIAGNOSE_DATE,
                   OUTDIAGNOSE_NO,
                   OUTDIAGNOSE_NAME,
                   OUTDIAGNOSE_DATE,
                   INHOSPITAL_DATE,
                   OUTHOSPITAL_DATE,
                   BALANCE_DATE,
                   BALANCE_STATE,
                   OPER_NO,
                   OPER_NAME,
                   OPER_DATE,
                   CLINICDEPT_NO,
                   CLINICDEPT_NAME,
                   CLINICDOCTOR_NO,
                   CLINICDOCTOR_NAME,
                   INDEPT_NO,
                   INDEPT_NAME,
                   OUTDEPT_NO,
                   OUTDEPT_NAME,
                   TOT_COST,
                   PAY_COST,
                   PUB_COST,
                   OWN_COST,
                   OFFICIAL_COST,
                   OVER_COST,
                   BASE_COST,
                   OWN_SUPPLE_COST,
                   HELP_ALLOWANCES_COST,
                   ENTERPRISE_SUPPLE_COST,
                   HELP_OWN_COST,
                   YEAR_TOT_COST,
                   YEAR_PUB_COST,
                   YEAR_PAY_COST,
                   YEAR_OWN_COST,
                   YEAR_OFFICIAL_COST,
                   YEAR_BASE_COST,
                   YEAR_HELP_COST,
                   YEAR_SPECIALMED_COST,
                   INDIVIDUAL_COST,
                   SPECIALMED_TOTCOST,
                   SPECIALMED_PUBCOST,
                   SPECIALMED_BASECOST,
                   LXPUB_COST,
                   YLOWN_COST,
                   TCOWN_COST,
                   DEOWN_COST,
                   EXCEEDLIMIT_OWNCOST,
                   SEALTOPLINE_OWNCOST,
                   ENTERPRISEADD_COST,
                   APPINFO_NO,
                   APPINFO_NAME,
                   APPINFO_MEMO,
                   APPTYPE_NO,
                   APPTYPE_NAME,
                   APPTYPE_MEMO,
                   APP_FLAG,
                   APP_DATE,
                   CARDVALIDDATE,
                   SHIFTDATE,
                   INHOSTIMES,
                   ISVALID,
                   PC_NO,
                   REGINFORETURN,
                   READCARDRETURN,
                   BANINFORETURN,
                   REMARK,
                   TYPE_CODE,
                   TRANS_TYPE,
                   CENTER_BIZCYCLENO,
                   HIS_BIZCYCLENO,
                   CENTER_BUSSINESSSEQNO,
                   HIS_BUSSINESSSEQNO,INVOICE_NEW
  FROM siinfo t
 WHERE t.inpatient_id = '{0}'
   AND t.visit_id = '{1}'
   AND t.invoice_no = '{2}'
   AND t.pact_code = '{3}'
   AND t.Balance_State = '1'
   AND t.isvalid = '1'
   AND t.trans_type = '1'
  AND t.REGISTER_NO = '1'
 ";
            strSQL = string.Format(strSQL, patientID, visitID, invoiceNO, chargeCode);

            try
            {
                #region
                dt = BaseEntityer.Db.GetDataTable(strSQL);
                if (dt.Rows.Count <= 0)
                    return null;
                info.INPATIENT_ID = dt.Rows[0][0].ToString();
                info.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                info.BALANCE_NO = dt.Rows[0][2].ToString();
                info.NAME = dt.Rows[0][3].ToString();
                info.SEX = dt.Rows[0][4].ToString();
                info.BIRTHDAY = DateTime.Parse(dt.Rows[0][5].ToString());
                info.IDENNO = dt.Rows[0][6].ToString();
                info.PACT_CODE = dt.Rows[0][7].ToString();
                info.PACT_NAME = dt.Rows[0][8].ToString();
                info.REGISTER_NO = dt.Rows[0][9].ToString();
                info.HOSPITAL_NO = dt.Rows[0][10].ToString();
                info.HOSPITAL_NAME = dt.Rows[0][11].ToString();
                info.HOSPITAL_GRADE = dt.Rows[0][12].ToString();
                info.IMP_NO = dt.Rows[0][13].ToString();
                info.RUNNING_NO = dt.Rows[0][14].ToString();
                info.INVOICE_NO = dt.Rows[0][15].ToString();
                info.MEDICAL_TYPE = dt.Rows[0][16].ToString();
                info.CARD_NO = dt.Rows[0][17].ToString();
                info.MCARD_NO = dt.Rows[0][18].ToString();
                info.PERSON_NO = dt.Rows[0][19].ToString();
                info.SI_BEGINDATE = DateTime.Parse(dt.Rows[0][20].ToString());
                info.SI_STATE = dt.Rows[0][21].ToString();
                info.ICCARD_NO = dt.Rows[0][22].ToString();
                info.OVERAL_NO = dt.Rows[0][23].ToString();
                info.FUND_NO = dt.Rows[0][24].ToString();
                info.FUND_NAME = dt.Rows[0][25].ToString();
                info.BUSINESSSEQUENCE = dt.Rows[0][26].ToString();
                info.APPLYSEQUENCE = dt.Rows[0][27].ToString();
                info.ANOTHERCITY_NO = dt.Rows[0][28].ToString();
                info.ANOTHERCITY_NAME = dt.Rows[0][29].ToString();
                info.CORPORATION_NO = dt.Rows[0][30].ToString();
                info.CORPORATION_NAME = dt.Rows[0][31].ToString();
                info.INSURANCETYPE = dt.Rows[0][32].ToString();
                info.EMPL_TYPE = dt.Rows[0][33].ToString();
                info.BED_NO = dt.Rows[0][34].ToString();
                info.ISBALANCED = int.Parse(dt.Rows[0][35].ToString());
                info.INDOCTOR_NO = dt.Rows[0][36].ToString();
                info.INDOCTOR_NAME = dt.Rows[0][37].ToString();
                info.OUTDOCTOR_NO = dt.Rows[0][38].ToString();
                info.OUTDOCTOR_NAME = dt.Rows[0][39].ToString();
                info.OUTREASON = dt.Rows[0][40].ToString();
                info.PAYUSERFLAG = dt.Rows[0][41].ToString();
                info.CLINICDIAGNOSE = dt.Rows[0][42].ToString();
                info.INDIAGNOSE_NO = dt.Rows[0][43].ToString();
                info.INDIAGNOSE_NAME = dt.Rows[0][44].ToString();
                info.INDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][45].ToString());
                info.OUTDIAGNOSE_NO = dt.Rows[0][46].ToString();
                info.OUTDIAGNOSE_NAME = dt.Rows[0][47].ToString();
                info.OUTDIAGNOSE_DATE = DateTime.Parse(dt.Rows[0][48].ToString());
                info.INHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][49].ToString());
                info.OUTHOSPITAL_DATE = DateTime.Parse(dt.Rows[0][50].ToString());
                info.BALANCE_DATE = DateTime.Parse(dt.Rows[0][51].ToString());
                info.BALANCE_STATE = dt.Rows[0][52].ToString();
                info.OPER_NO = dt.Rows[0][53].ToString();
                info.OPER_NAME = dt.Rows[0][54].ToString();
                info.OPER_DATE = DateTime.Parse(dt.Rows[0][55].ToString());
                info.CLINICDEPT_NO = dt.Rows[0][56].ToString();
                info.CLINICDEPT_NAME = dt.Rows[0][57].ToString();
                info.CLINICDOCTOR_NO = dt.Rows[0][58].ToString();
                info.CLINICDOCTOR_NAME = dt.Rows[0][59].ToString();
                info.INDEPT_NO = dt.Rows[0][60].ToString();
                info.INDEPT_NAME = dt.Rows[0][61].ToString();
                info.OUTDEPT_NO = dt.Rows[0][62].ToString();
                info.OUTDEPT_NAME = dt.Rows[0][63].ToString();
                info.TOT_COST = decimal.Parse(dt.Rows[0][64].ToString());
                info.PAY_COST = decimal.Parse(dt.Rows[0][65].ToString());
                info.PUB_COST = decimal.Parse(dt.Rows[0][66].ToString());
                info.OWN_COST = decimal.Parse(dt.Rows[0][67].ToString());
                info.OFFICIAL_COST = decimal.Parse(dt.Rows[0][68].ToString());
                info.OVER_COST = decimal.Parse(dt.Rows[0][69].ToString());
                info.BASE_COST = decimal.Parse(dt.Rows[0][70].ToString());
                info.OWN_SUPPLE_COST = decimal.Parse(dt.Rows[0][71].ToString());
                info.HELP_ALLOWANCES_COST = decimal.Parse(dt.Rows[0][72].ToString());
                info.ENTERPRISE_SUPPLE_COST = decimal.Parse(dt.Rows[0][73].ToString());
                info.HELP_OWN_COST = decimal.Parse(dt.Rows[0][74].ToString());
                info.YEAR_TOT_COST = decimal.Parse(dt.Rows[0][75].ToString());
                info.YEAR_PUB_COST = decimal.Parse(dt.Rows[0][76].ToString());
                info.YEAR_PAY_COST = decimal.Parse(dt.Rows[0][77].ToString());
                info.YEAR_OWN_COST = decimal.Parse(dt.Rows[0][78].ToString());
                info.YEAR_OFFICIAL_COST = decimal.Parse(dt.Rows[0][79].ToString());
                info.YEAR_BASE_COST = decimal.Parse(dt.Rows[0][80].ToString());
                info.YEAR_HELP_COST = decimal.Parse(dt.Rows[0][81].ToString());
                info.YEAR_SPECIALMED_COST = decimal.Parse(dt.Rows[0][82].ToString());
                info.INDIVIDUAL_COST = decimal.Parse(dt.Rows[0][83].ToString());
                info.SPECIALMED_TOTCOST = decimal.Parse(dt.Rows[0][84].ToString());
                info.SPECIALMED_PUBCOST = decimal.Parse(dt.Rows[0][85].ToString());
                info.SPECIALMED_BASECOST = decimal.Parse(dt.Rows[0][86].ToString());
                info.LXPUB_COST = decimal.Parse(dt.Rows[0][87].ToString());
                info.YLOWN_COST = decimal.Parse(dt.Rows[0][88].ToString());
                info.TCOWN_COST = decimal.Parse(dt.Rows[0][89].ToString());
                info.DEOWN_COST = decimal.Parse(dt.Rows[0][90].ToString());
                info.EXCEEDLIMIT_OWNCOST = decimal.Parse(dt.Rows[0][91].ToString());
                info.SEALTOPLINE_OWNCOST = decimal.Parse(dt.Rows[0][92].ToString());
                info.ENTERPRISEADD_COST = decimal.Parse(dt.Rows[0][93].ToString());
                info.APPINFO_NO = dt.Rows[0][94].ToString();
                info.APPINFO_NAME = dt.Rows[0][95].ToString();
                info.APPINFO_MEMO = dt.Rows[0][96].ToString();
                info.APPTYPE_NO = dt.Rows[0][97].ToString();
                info.APPTYPE_NAME = dt.Rows[0][98].ToString();
                info.APPTYPE_MEMO = dt.Rows[0][99].ToString();
                info.APP_FLAG = dt.Rows[0][100].ToString();
                info.APP_DATE = DateTime.Parse(dt.Rows[0][101].ToString());
                info.CARDVALIDDATE = DateTime.Parse(dt.Rows[0][102].ToString());
                info.SHIFTDATE = DateTime.Parse(dt.Rows[0][103].ToString());
                info.INHOSTIMES = int.Parse(dt.Rows[0][104].ToString());
                info.ISVALID = int.Parse(dt.Rows[0][105].ToString());
                info.PC_NO = int.Parse(dt.Rows[0][106].ToString());
                info.REGINFORETURN = dt.Rows[0][107].ToString();
                info.READCARDRETURN = dt.Rows[0][108].ToString();
                info.BANINFORETURN = dt.Rows[0][109].ToString();
                info.REMARK = dt.Rows[0][110].ToString();
                info.TYPE_CODE = dt.Rows[0][111].ToString();
                info.TRANS_TYPE = dt.Rows[0][112].ToString();
                info.CENTER_BIZCYCLENO = dt.Rows[0][113].ToString();
                info.HIS_BIZCYCLENO = dt.Rows[0][114].ToString();
                info.CENTER_BUSSINESSSEQNO = dt.Rows[0][115].ToString();
                info.HIS_BUSSINESSSEQNO = dt.Rows[0][116].ToString();
                info.INVOICE_NEW = dt.Rows[0][117].ToString();
                #endregion
            }
            catch
            {
                return null;
            }
            return info;
        }
 
        /// <summary>
        /// 修改医保主表有效标志and医保返回标记(门诊)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="visitDate"></param>
        /// <param name="appinfoNO">结算编号</param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateSIInfoValidFlagAndAppinfoNOByOutpatient(BaseEntityer db, string patientid, string visitid, string visitDate, string appinfoNO, ref string err)
        {
            int exec = 0;
            try
            {
                string sql = @"UPDATE siinfo
                                               SET isvalid    = 0,
                                                   appinfo_no = '{3}'
                                             WHERE inpatient_id = '{0}'
                                               AND visit_id = '{1}'
                                               AND shiftdate = to_date('{2}', 'yyyy-MM-dd')
                                               AND isvalid = 1";
                sql = string.Format(sql, patientid, visitid, visitDate, appinfoNO);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return exec;
        }
        #endregion

        #region 贵阳电网医保
        public int UpdatePRESCRIPT_FLAG(BaseEntityer db, OUTP_BILL_ITEMS item, ref string err, ref string errSql)
        {
            int revInt = 0;
            try
            {
                string sql = @"update OUTP_BILL_ITEMS set prescript_flag = '1' where VISIT_DATE=to_date('{0}','yyyy-MM-dd hh24:mi:ss'), VISIT_NO={1}, ITEM_NO={2}, RCPT_NO='{3}'";
                sql = Utility.SqlFormate(sql, item.VISIT_DATE, item.VISIT_NO, item.ITEM_NO, item.RCPT_NO);
                revInt = db.ExecuteNonQuery(sql);
                db.CommitTransaction();
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                revInt = -1;
            }
            return revInt;
        }
        #endregion 
    }
}