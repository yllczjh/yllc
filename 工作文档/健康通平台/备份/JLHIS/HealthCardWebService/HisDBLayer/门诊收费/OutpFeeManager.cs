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
    /// <summary>
    /// 门诊挂号收费数据访问类
    /// </summary>
    public class OutpFeeManager
    {
        /// <summary>
        /// 查询当前可用的打折信息，
        /// 满足条件1、打折已经启用
        ///         2、当前数据库时间在打折时间段内
        /// </summary>
        /// <returns></returns>
        public List<CHARGE_TEMPLET> GetChargeTempletList()
        {
            //2013-9-13 by li 门诊打折功能判断具体打折时间，不仅仅只是判断日期
            string sql = @"select * from CHARGE_TEMPLET t where t.state='1' 
                        and t.start_date<=sysdate
                        and t.end_date>=sysdate";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CHARGE_TEMPLET>(ds).ToList();
        }
        /// <summary>
        /// 根据templetID查询打折明细
        /// </summary>
        /// <returns></returns>
        public List<CHARGE_TEMPLET_DETAIL> GetChargeTempletDetailList(string templetID)
        {
            string sql = @"select * from charge_templet_detail t where t.templet_id='{0}'";
            sql = sql.SqlFormate(templetID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CHARGE_TEMPLET_DETAIL>(ds).ToList();
        }

        /// <summary>
        /// 门诊号表查询
        /// </summary>
        /// <returns></returns>
        public DataTable QueryClinicSchduling(string CLINIC_LABEL, string CLINIC_TYPE, string TIME_DESC, string dateBegin, string dateEnd)
        {
            string sql = @"SELECT CLINIC_FOR_REGIST.CLINIC_DATE,
                                       CLINIC_FOR_REGIST.CLINIC_LABEL,
                                       CLINIC_FOR_REGIST.TIME_DESC,
                                       CLINIC_INDEX.CLINIC_DEPT,
                                       --CLINIC_INDEX.DOCTOR,
                                       --CLINIC_INDEX.DOCTOR_TITLE,
                                       CLINIC_INDEX.CLINIC_TYPE,
                                       CLINIC_FOR_REGIST.REGISTRATION_LIMITS,
                                       CLINIC_FOR_REGIST.CURRENT_NO,
                                       CLINIC_FOR_REGIST.APPOINTMENT_LIMITS,
                                       CLINIC_FOR_REGIST.REGIST_PRICE,
                                       (case (select c.is_valid
                                            from HEALTHCARD_STOPREG_INFO c
                                           where c.clinic_date = CLINIC_FOR_REGIST.Clinic_Date
                                             and c.clinic_label = CLINIC_FOR_REGIST.Clinic_Label
                                             and c.time_desc = CLINIC_FOR_REGIST.Time_Desc)
                                         when '0' then
                                          '是'
                                         else
                                          '否'
                                       end) as is_valid
                                  FROM CLINIC_FOR_REGIST, CLINIC_INDEX
                                 WHERE (CLINIC_FOR_REGIST.CLINIC_LABEL = CLINIC_INDEX.CLINIC_LABEL)
                                   and (CLINIC_FOR_REGIST.CLINIC_LABEL = '{0}' or '{0}' is null)
                                   and (CLINIC_INDEX.CLINIC_TYPE = '{1}' or '{1}' is null)
                                   and (CLINIC_FOR_REGIST.TIME_DESC = '{2}' or '{2}' is null)
                                   and CLINIC_FOR_REGIST.CLINIC_DATE >=
                                       TO_DATE('{3}', 'yyyy-MM-dd hh24:mi:ss')
                                   and CLINIC_FOR_REGIST.CLINIC_DATE <=
                                       TO_DATE('{4}', 'yyyy-MM-dd hh24:mi:ss')
                    ";
            sql = sql.SqlFormate(CLINIC_LABEL, CLINIC_TYPE, TIME_DESC, dateBegin, dateEnd);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 获取门诊收据上显示的费别字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OUTP_RCPT_FEE_DICT> GetOutpRcptFeeType()
        {
            string sql = @"select rownum as id, t.*
             from outp_rcpt_fee_dict t
             order by t.fee_class_code";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_RCPT_FEE_DICT>(ds).ToList();
        }
        /// <summary>
        /// 根据就诊序号，和日期限制查询病人要交款的医嘱明细
        /// </summary>
        /// <param name="visitNO"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OUTP_ORDERS_DETAIL> QueryPatientOrderDetail(string visitNO, string date)
        {
            string sql = @"select * from outp_orders_detail t
where t.visit_no={0}
and t.visit_date =to_date('{1}','yyyy-MM-dd hh24:mi:ss')
and t.charge_indicator=0  and t.oper_date +(select param_value from sys_param where param_name='CHARGE_CHECK_DAYS')>= sysdate";
            sql = string.Format(sql, visitNO, date);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_ORDERS_DETAIL>(ds).ToList();
        }
        public List<HisCommon.DataEntity.OUTP_ORDERS_DETAIL> QueryPatientOrderDetailInp(string visitNO, string patientId)
        {
            string sql = @"select * from outp_orders_detail t
where t.visit_no={0}
and t.charge_indicator=0  and t.remark='{1}'";
            sql = string.Format(sql, visitNO, patientId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_ORDERS_DETAIL>(ds).ToList();
        }

        public List<HisCommon.DataEntity.OUTP_ORDERS_DETAIL> QueryPatientOrderDetailByCurrentDay(string visitNO,string visitDate)
        {
            string sql = @"select * from outp_orders_detail t
where t.visit_no={0}
and t.charge_indicator=0  and t.visit_date>= to_date('{1}', 'yyyy-mm-dd')";
            sql = string.Format(sql, visitNO, visitDate);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_ORDERS_DETAIL>(ds).ToList();
        }
        /// <summary>
        /// 发票重打医嘱明细
        /// </summary>
        /// <param name="SerialNo"></param>
        /// <param name="ItemNo"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OUTP_ORDERS_DETAIL> QueryRePrintOrderDetail(string SerialNo, string ItemNo)
        {
            string sql = @"select * from outp_orders_detail t
                        where t.serial_no='{0}'
                        and t.item_no = '{1}' ";
            sql = string.Format(sql, SerialNo, ItemNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_ORDERS_DETAIL>(ds).ToList();
        }
        /// <summary>
        /// 根据收款员ID得到该收款员的最新收据号 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetNewRcptNoByUserId(string userId)
        {
            string sql = @"SELECT Max ( RCPT_NO )+1 From OUTP_RCPT_MASTER WHERE OPERATOR_NO ='{0}'";
            sql = sql.SqlFormate(new object[] { userId });
            return BaseEntityer.Db.ExecuteScalar<string>(sql);
        }
        /// <summary>
        /// 根据收款员ID得到该收款员的最新结算单号
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetNewAcctNoByUserId(string userId)
        {
            string sql = @"SELECT Max ( acct_no )+1 from outp_acct_master where operator_no ='{0}'";
            sql = sql.SqlFormate(new object[] { userId });
            return BaseEntityer.Db.ExecuteScalar<string>(sql);
        }
        /// <summary>
        /// 收款日报最大号
        /// </summary>
        /// <returns></returns>
        public string GetAllAcctNo()
        {
            string sql = @"SELECT Max ( acct_no )+1 from ALL_ACCT_MASTER";
            //sql = sql.SqlFormate(new object[] { userId });
            return BaseEntityer.Db.ExecuteScalar<string>(sql);
        }
        /// <summary>
        /// 根据退费票据号查询
        /// </summary>
        /// <returns></returns>
        public OUTP_RCPT_MASTER GetRcptByReturnNo(string returnNO)
        {
            string sql = @"select * from outp_rcpt_master t where t.refunded_rcpt_no='{0}'";
            sql = sql.SqlFormate(returnNO);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<OUTP_RCPT_MASTER>(ds).ToList();
            if (list.Count > 0)
                return list[0];
            else return null;
        }
        #region 收据及明细查询
        /// <summary>
        /// 根据收据号查询发票主表OUTP_RCPT_MASTER
        /// </summary>
        /// <param name="rcptNo">收据号</param>
        /// <returns>返回OUTP_RCPT_MASTER实体</returns>
        public List<OUTP_RCPT_MASTER> QueryRcptByNo(string rcptNo)
        {
            string sql = @"select * from outp_rcpt_master t
where t.rcpt_no='{0}'";
            sql = sql.SqlFormate(new object[] { rcptNo });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_RCPT_MASTER>(ds).ToList();
        }

        public List<OUTP_RCPT_MASTER> QueryRcptByInvoice(string INVOICE_NEW)
        {
            string sql = @"select * from outp_rcpt_master t
where t.INVOICE_NEW='{0}'";
            sql = sql.SqlFormate(new object[] { INVOICE_NEW });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_RCPT_MASTER>(ds).ToList();
        }

        /// <summary>
        /// 通过发票表查询Rcpt_no
        /// </summary>
        /// <param name="INVOICE_NEW"></param>
        /// <returns></returns>
        public List<fin_invoiceinfo_record> QueryReCordByInvoice(string INVOICE_NEW)
        {
            string sql = @"select *
  from fin_invoiceinfo_record r
 where r.invoice_state in ('0', '2')
   and r.invoice_kind = '01'
   and r.invoice_no = '{0}'";
            sql = sql.SqlFormate(new object[] { INVOICE_NEW });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<fin_invoiceinfo_record>(ds).ToList();
        }

        /// <summary>
        /// 根据收据号查询开单表OUTP_ORDER_DESC
        /// </summary>
        /// <param name="rcptNo">收据号</param>
        /// <returns>返回OUTP_ORDER_DESC实体</returns>
        public List<OUTP_ORDER_DESC> QueryOrderDescByNo(string rcptNo)
        {
            string sql = @"select * from outp_order_desc t
where t.rcpt_no='{0}'";
            sql = sql.SqlFormate(new object[] { rcptNo });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_ORDER_DESC>(ds).ToList();
        }
        /// <summary>
        /// 根据收据号查询发票明细OUTP_BILL_ITEMS
        /// </summary>
        /// <param name="rcptNo">收据号</param>
        /// <returns>返回OUTP_BILL_ITEMS实体</returns>
        public List<OUTP_BILL_ITEMS> QueryBillItemsByNo(string rcptNo)
        {
            string sql = @"select * from outp_bill_items t
where t.rcpt_no='{0}'";
            sql = sql.SqlFormate(new object[] { rcptNo });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_BILL_ITEMS>(ds).ToList();
        }
        /// <summary>
        /// 根据收据号查询费用分类OUTP_PAYMENTS_MONEY
        /// </summary>
        /// <param name="rcptNo">收据号</param>
        /// <returns>返回OUTP_PAYMENTS_MONEY实体</returns>
        public List<OUTP_PAYMENTS_MONEY> QueryPaymentsMoneyByNo(string rcptNo)
        {
            string sql = @"select * from outp_payments_money t
where t.rcpt_no='{0}'";
            sql = sql.SqlFormate(new object[] { rcptNo });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_PAYMENTS_MONEY>(ds).ToList();
        }
        /// <summary>
        /// 根据收据号查询挂号患者信息
        /// </summary>
        /// <param name="rcptNo">收据号</param>
        /// <returns>返回CLINIC_MASTER实体</returns>
        public List<CLINIC_MASTER> QueryPatientByRcptNo(string rcptNo)
        {
            string sql = @"select * from clinic_master t
inner join outp_rcpt_master o 
on t.visit_date=o.reg_date
and t.visit_no=o.reg_no
and o.rcpt_no='{0}'";
            sql = sql.SqlFormate(new object[] { rcptNo });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(ds).ToList();
        }

        public static List<CLINIC_MASTER> QueryPatientByClinicNo(string clinicNo)
        {
            string sql = @"select patient_id,
  visit_no,
  name,
  sex,
  age, 
  (select d.dept_name from dept_dict d where d.dept_code = m.visit_dept and rownum=1) visit_dept,
  m.visit_date
  from clinic_master m where patient_id = '{0}'";
            sql = string.Format(sql, clinicNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(ds).ToList();
        }

        #endregion
        #region 门诊收款结算
        /// <summary>
        /// 通过发票号查询医保信息
        /// </summary>
        /// <returns></returns>
        public List<SIInfo> QuerySiInfoByRcptNo(string RcptNO)
        {
            string sql = @"select * from siinfo s where s.invoice_no ='{0}' and type_code='1'";
            sql = sql.SqlFormate(RcptNO);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<SIInfo>(ds).ToList();

        }
        /// <summary>
        /// 通过发票号查询医保信息
        /// </summary>
        /// <returns></returns>
        public List<SI_SYMZFEEDETAILS> QuerySiInfo_SymzfeeDetailsByRcptNo(string RcptNO)
        {
            string sql = @"select * from SI_SYMZFEEDETAILS s where s.invoice ='{0}'";
            sql = sql.SqlFormate(RcptNO);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<SI_SYMZFEEDETAILS>(ds).ToList();

        }
        /// <summary>
        /// 根据收款员ID和截止时间查询未结算的收据信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<OUTP_RCPT_MASTER> QueryOutpRcptNotAccount(string userId, string date)
        {
            string sql = @"SELECT *
    FROM OUTP_RCPT_MASTER t
   WHERE t.OPERATOR_NO = '{0}'
 AND t.visit_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
 AND t.acct_no is null
 order by t.rcpt_no";
            sql = sql.SqlFormate(userId, date);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_RCPT_MASTER>(ds).ToList();
        }
        /// <summary>
        /// 发票管理的发票日结查询
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<fin_invoiceinfo_record> QueryInvoiceInfoNotAccount(string userId, string date)
        {
            string sql = @"SELECT *
            FROM fin_invoiceinfo_record t
            WHERE t.FEE_OPER_CODE = '{0}'
            AND t.FEE_OPER_DATE<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
            AND t.DAYBALANCED_FLAG='0' and (t.INVOICE_KIND='00' or t.INVOICE_KIND='01')
            order by t.INVOICE_NO";
            sql = sql.SqlFormate(userId, date);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<fin_invoiceinfo_record>(ds).ToList();
        }
        /// <summary>
        /// 一卡通预存款
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<ONECARD_PAYMENT> QueryOneCardPaymentNotAccount(string userId, string date)
        {
            //充值类别（1.门诊 2.住院）RECHARGE_CLASS
            string sql = @"SELECT * FROM onecard_payment t
            WHERE t.oper_id = '{0}'and t.RECHARGE_CLASS='1'
            AND t.RECHARGE_DATE<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
            AND (t.ACC_BALANCE='0' or t.ACC_BALANCE is null )";
            sql = sql.SqlFormate(userId, date);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<ONECARD_PAYMENT>(ds).ToList();
        }

        public List<ONECARD_PAYMENT> QueryOneCardPaymentAllAccount(string userId, string stardate, string enddate)
        {
            //充值类别（1.门诊 2.住院）RECHARGE_CLASS
            string sql = @"SELECT * FROM onecard_payment t
            WHERE t.oper_id = '{0}'and t.RECHARGE_CLASS='1'
            and t.RECHARGE_DATE>to_date('{1}','yyyy-MM-dd hh24:mi:ss')
            AND t.RECHARGE_DATE<=to_date('{2}','yyyy-MM-dd hh24:mi:ss')";
            sql = sql.SqlFormate(userId, stardate, enddate);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<ONECARD_PAYMENT>(ds).ToList();
        }
        /// <summary>
        /// OUTP_ACCT_MASTER 表插入
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int InsertOutpAcctMaster(OUTP_ACCT_MASTER o, BaseEntityer db)
        {
            string sql = @"insert into OUTP_ACCT_MASTER
  (ACCT_NO,
   OPERATOR_NO,
   ACCT_DATE,
   MIN_RCPT_NO,
   MAX_RCPT_NO,
   RCPTS_NUM,
   FREE_OF_CHARGE_NUM,
   REFUND_NUM,
   REFUND_AMOUNT,
   TOTAL_COSTS,
   TOTAL_INCOMES,
   TALLY_DATE,
   RECHARGE_CARD_COST,
   RECHARGE_MONEY_COST,
   REFUND_RECHARGE_CARD_COST,
   REFUND_RECHARGE_MONEY_COST
)
values
  ('{0}',
   '{1}',
   to_date('{2}', 'yyyy-MM-dd hh24:mi:ss'),
   '{3}',
   '{4}',
   {5},
   {6},
   {7},
   {8},
   {9},
   {10},
    to_date('{15}', 'yyyy-MM-dd hh24:mi:ss') ,
   {11},
   {12},
   {13},
   {14})";
            object[] os = new object[]{
            o.ACCT_NO,
            o.OPERATOR_NO,
            o.ACCT_DATE,
            o.MIN_RCPT_NO,
            o.MAX_RCPT_NO,
            o.RCPTS_NUM,
            o.FREE_OF_CHARGE_NUM,
            o.REFUND_NUM,
            o.REFUND_AMOUNT,
            o.TOTAL_COSTS,
            o.TOTAL_INCOMES,
            o.RECHARGE_CARD_COST,
            o.RECHARGE_MONEY_COST,
            o.REFUND_RECHARGE_CARD_COST,
            o.REFUND_RECHARGE_MONEY_COST,
            o.TALLY_DATE
        };
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// OUTP_ACCT_MONEY 表插入
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int InsertOutpAcctMoney(OUTP_ACCT_MONEY o, BaseEntityer db)
        {
            string sql = @"insert into OUTP_ACCT_MONEY(ACCT_NO,MONEY_TYPE,INCOME_AMOUNT,REFUNDED_AMOUNT,PAYMENT_NO)
                        values('{0}','{1}',{2},{3},{4})";

            object[] os = new object[]{
            o.ACCT_NO,
            o.MONEY_TYPE,
            o.INCOME_AMOUNT,
            o.REFUNDED_AMOUNT,
            o.PAYMENT_NO};
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// OUTP_ACCT_DETAIL 表插入
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int InsertOutpAcctDetail(OUTP_ACCT_DETAIL o, BaseEntityer db)
        {
            string sql = @"insert into OUTP_ACCT_DETAIL(ACCT_NO,TALLY_FEE_CLASS,COSTS,INCOME)
                        values('{0}','{1}',{2},{3})";

            object[] os = new object[]{
            o.ACCT_NO,
            o.TALLY_FEE_CLASS,
            o.COSTS,
            o.INCOME};
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// OUTP_ACCT_DETAIL 表插入
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int UpdateOutpAcctRcpt(string acctNo, string rcptNo, string userId, BaseEntityer db)
        {
            string sql = @"update OUTP_RCPT_MASTER SET ACCT_NO ='{0}' WHERE RCPT_NO ='{1}' and OPERATOR_NO = '{2}' ";

            object[] os = new object[]{
            acctNo,
           rcptNo,
          userId};
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 根据结算序号查询发票
        /// </summary>
        /// <param name="acctNo"></param>
        /// <returns></returns>
        public List<OUTP_RCPT_MASTER> QueryOutpRcptByAcctNo(string acctNo)
        {
            string sql = @"select * from outp_rcpt_master t
where t.acct_no='{0}'";
            sql = sql.SqlFormate(new object[] { acctNo });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_RCPT_MASTER>(ds).ToList();
        }
        #endregion
        #region 门诊挂号

        /// <summary>
        /// 获取固定日期的门诊号
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataTable GetClinicForRegistByDate(string date)
        {
            string sql = @"  SELECT CLINIC_FOR_REGIST.CLINIC_LABEL,   
                             CLINIC_FOR_REGIST.TIME_DESC,   
                             CLINIC_FOR_REGIST.REGISTRATION_LIMITS,   
                             CLINIC_FOR_REGIST.APPOINTMENT_LIMITS,   
                             CLINIC_FOR_REGIST.REGIST_PRICE,
                             a.CLINIC_DEPT,   
                             a.DOCTOR,   
                             a.DOCTOR_TITLE,   
                             a.CLINIC_TYPE,   
                             CLINIC_FOR_REGIST.CURRENT_NO,
                             a.input_code,
                             a.regprice,
                             a.diagprice,
                             a.clinc_type
                        FROM CLINIC_FOR_REGIST inner join 
                        (select CLINIC_INDEX.CLINIC_LABEL,
                                CLINIC_INDEX.CLINIC_DEPT,
                                CLINIC_INDEX.Doctor,
                                CLINIC_INDEX.Doctor_Title,
                                CLINIC_INDEX.CLINIC_TYPE,
                                CLINIC_INDEX.Input_Code,
                                CLINIC_TYPE_DICT.SERIAL_NO,
                                CLINIC_TYPE_DICT.Regprice,
                                CLINIC_TYPE_DICT.DIAGPRICE,
                                CLINIC_INDEX.CLINC_TYPE  
                        from CLINIC_INDEX left join CLINIC_TYPE_DICT on 
                        CLINIC_INDEX.Clinic_Type=CLINIC_TYPE_DICT.CLINIC_TYPE) a 
                        on CLINIC_FOR_REGIST.CLINIC_LABEL = a.CLINIC_LABEL 
                       WHERE (CLINIC_FOR_REGIST.REGISTRATION_LIMITS>0 or CLINIC_FOR_REGIST.REGISTRATION_LIMITS is null) and 
                             ( CLINIC_FOR_REGIST.CLINIC_DATE = to_date('{0}','yyyy-mm-dd'))";
            sql = string.Format(sql, date);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 根据病人ID获取病人主索引信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public PAT_MASTER_INDEX GetPatientInforByID(string patientID)
        {
            string sql = @"select PAT_MASTER_INDEX.PATIENT_ID,
                           PAT_MASTER_INDEX.INP_NO,
                           PAT_MASTER_INDEX.NAME,
                           PAT_MASTER_INDEX.NAME_PHONETIC,
                           PAT_MASTER_INDEX.SEX,--性别字典
                           PAT_MASTER_INDEX.DATE_OF_BIRTH,
                           PAT_MASTER_INDEX.BIRTH_PLACE,--行政区字典
                           PAT_MASTER_INDEX.CITIZENSHIP,--关联国家地区字典
                           PAT_MASTER_INDEX.NATION,--民族字典
                           PAT_MASTER_INDEX.ID_NO,
                           PAT_MASTER_INDEX.IDENTITY,
                           PAT_MASTER_INDEX.CHARGE_TYPE,--费别定义字典
                           PAT_MASTER_INDEX.UNIT_IN_CONTRACT,
                           PAT_MASTER_INDEX.MAILING_ADDRESS,
                           PAT_MASTER_INDEX.ZIP_CODE,
                           PAT_MASTER_INDEX.PHONE_NUMBER_HOME,
                           PAT_MASTER_INDEX.PHONE_NUMBER_BUSINESS,
                           PAT_MASTER_INDEX.NEXT_OF_KIN,--联系人
                           PAT_MASTER_INDEX.RELATIONSHIP,--联系人关系字典
                           PAT_MASTER_INDEX.NEXT_OF_KIN_ADDR,
                           PAT_MASTER_INDEX.NEXT_OF_KIN_ZIP_CODE,
                           PAT_MASTER_INDEX.NEXT_OF_KIN_PHONE,
                           PAT_MASTER_INDEX.LAST_VISIT_DATE,
                           PAT_MASTER_INDEX.VIP_INDICATOR,
                           PAT_MASTER_INDEX.CREATE_DATE,
                           PAT_MASTER_INDEX.OPERATOR,
                           PAT_MASTER_INDEX.PAT_BED_BMP,
                           PAT_MASTER_INDEX.BALANCE,
                           PAT_MASTER_INDEX.WEIGHT 
                           from PAT_MASTER_INDEX where PAT_MASTER_INDEX.PATIENT_ID = '{0}'";
            sql = string.Format(sql, patientID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_MASTER_INDEX>(ds).First();
            else
                return null;
        }

        /// <summary>
        /// 根据病人ID获取水库移民主索引列表信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public List<PAT_MASTER_INDEX> GetReservoirList(string patientID, string chargeType)
        {
            string sql = @"select PAT_MASTER_INDEX.PATIENT_ID,
                           PAT_MASTER_INDEX.INP_NO,
                           PAT_MASTER_INDEX.NAME,
                           PAT_MASTER_INDEX.NAME_PHONETIC,
                           PAT_MASTER_INDEX.SEX,--性别字典
                           PAT_MASTER_INDEX.DATE_OF_BIRTH,
                           PAT_MASTER_INDEX.BIRTH_PLACE,--行政区字典
                           PAT_MASTER_INDEX.CITIZENSHIP,--关联国家地区字典
                           PAT_MASTER_INDEX.NATION,--民族字典
                           PAT_MASTER_INDEX.ID_NO,
                           PAT_MASTER_INDEX.IDENTITY,
                           PAT_MASTER_INDEX.CHARGE_TYPE,--费别定义字典
                           PAT_MASTER_INDEX.UNIT_IN_CONTRACT,
                           PAT_MASTER_INDEX.MAILING_ADDRESS,
                           PAT_MASTER_INDEX.ZIP_CODE,
                           PAT_MASTER_INDEX.PHONE_NUMBER_HOME,
                           PAT_MASTER_INDEX.PHONE_NUMBER_BUSINESS,
                           PAT_MASTER_INDEX.NEXT_OF_KIN,--联系人
                           PAT_MASTER_INDEX.RELATIONSHIP,--联系人关系字典
                           PAT_MASTER_INDEX.NEXT_OF_KIN_ADDR,
                           PAT_MASTER_INDEX.NEXT_OF_KIN_ZIP_CODE,
                           PAT_MASTER_INDEX.NEXT_OF_KIN_PHONE,
                           PAT_MASTER_INDEX.LAST_VISIT_DATE,
                           PAT_MASTER_INDEX.VIP_INDICATOR,
                           PAT_MASTER_INDEX.CREATE_DATE,
                           PAT_MASTER_INDEX.OPERATOR,
                           PAT_MASTER_INDEX.PAT_BED_BMP,
                           PAT_MASTER_INDEX.BALANCE 
                           from PAT_MASTER_INDEX where (PAT_MASTER_INDEX.CHARGE_TYPE = '{0}' or '{0}' is null)";
            if (patientID != string.Empty)
            {
                sql += @" AND PAT_MASTER_INDEX.PATIENT_ID = '{1}'";
                sql = string.Format(sql, chargeType, patientID);
            }
            else
                sql = string.Format(sql, chargeType);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_MASTER_INDEX>(ds).ToList();
            else
                return null;
        }

        /// <summary>
        /// 医保患者根据身份证号查询是否存在主索引信息
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        public string GetPatientIDByIdNo(string idNo)
        {
            string sql = @"select PAT_MASTER_INDEX.PATIENT_ID 
                           from PAT_MASTER_INDEX where PAT_MASTER_INDEX.ID_NO = '{0}'";
            sql = sql.SqlFormate(new object[] { idNo });
            return BaseEntityer.Db.ExecuteScalar<string>(sql);
        }

        /// <summary>
        /// 根据住院号获取病人主索引信息
        /// </summary>
        /// <param name="inpno"></param>
        /// <returns></returns>
        public PAT_MASTER_INDEX GetPatientInforByINPNO(string inpno)
        {
            string sql = @"select PAT_MASTER_INDEX.PATIENT_ID,
                           PAT_MASTER_INDEX.INP_NO,
                           PAT_MASTER_INDEX.NAME,
                           PAT_MASTER_INDEX.NAME_PHONETIC,
                           PAT_MASTER_INDEX.SEX,--性别字典
                           PAT_MASTER_INDEX.DATE_OF_BIRTH,
                           PAT_MASTER_INDEX.BIRTH_PLACE,--行政区字典
                           PAT_MASTER_INDEX.CITIZENSHIP,--关联国家地区字典
                           PAT_MASTER_INDEX.NATION,--民族字典
                           PAT_MASTER_INDEX.ID_NO,
                           PAT_MASTER_INDEX.IDENTITY,
                           PAT_MASTER_INDEX.CHARGE_TYPE,--费别定义字典
                           PAT_MASTER_INDEX.UNIT_IN_CONTRACT,
                           PAT_MASTER_INDEX.MAILING_ADDRESS,
                           PAT_MASTER_INDEX.ZIP_CODE,
                           PAT_MASTER_INDEX.PHONE_NUMBER_HOME,
                           PAT_MASTER_INDEX.PHONE_NUMBER_BUSINESS,
                           PAT_MASTER_INDEX.NEXT_OF_KIN,--联系人
                           PAT_MASTER_INDEX.RELATIONSHIP,--联系人关系字典
                           PAT_MASTER_INDEX.NEXT_OF_KIN_ADDR,
                           PAT_MASTER_INDEX.NEXT_OF_KIN_ZIP_CODE,
                           PAT_MASTER_INDEX.NEXT_OF_KIN_PHONE,
                           PAT_MASTER_INDEX.LAST_VISIT_DATE,
                           PAT_MASTER_INDEX.VIP_INDICATOR,
                           PAT_MASTER_INDEX.CREATE_DATE,
                           PAT_MASTER_INDEX.OPERATOR,
                           PAT_MASTER_INDEX.PAT_BED_BMP
                            from PAT_MASTER_INDEX where PAT_MASTER_INDEX.INP_NO = '{0}'";
            sql = string.Format(sql, inpno);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_MASTER_INDEX>(ds).First();
            else
                return null;
        }

        /// <summary>
        /// 获取性别字典表信息
        /// </summary>
        /// <returns></returns>
        public List<SEX_DICT> GetSEX_DICTInfor()
        {
            string sql = @"SELECT SEX_DICT.SERIAL_NO,
                           SEX_DICT.SEX_CODE,
                           SEX_DICT.SEX_NAME,
                           SEX_DICT.INPUT_CODE FROM SEX_DICT ORDER BY SEX_DICT.SERIAL_NO";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<SEX_DICT>(ds).ToList();
        }

        /// <summary>
        /// 获取身份字典表信息
        /// </summary>
        /// <returns></returns>
        public List<IDENTITY_DICT> GetIDENTITY_DICTInfor()
        {
            string sql = @"SELECT IDENTITY_DICT.SERIAL_NO,
                           IDENTITY_DICT.IDENTITY_CODE,
                           IDENTITY_DICT.IDENTITY_NAME,
                           IDENTITY_DICT.INPUT_CODE,
                           IDENTITY_DICT.PRIORITY_INDICATOR FROM IDENTITY_DICT ORDER BY IDENTITY_DICT.SERIAL_NO";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<IDENTITY_DICT>(ds).ToList();
        }

        /// <summary>
        /// 获取费别字典表信息
        /// </summary>
        /// <returns></returns>
        public List<CHARGE_TYPE_DICT> GetCHARGE_TYPE_DICTInfor()
        {
            //2013-9-17 by li 门诊费别是否显示--0为不显示，1为显示
            //2013-10-30 by li 表增加字段，读取时sql语句必须全部读取
            string sql = @"SELECT CHARGE_TYPE_DICT.SERIAL_NO,
                           CHARGE_TYPE_DICT.CHARGE_TYPE_CODE,
                           CHARGE_TYPE_DICT.CHARGE_TYPE_NAME,
                           CHARGE_TYPE_DICT.CHARGE_PRICE_INDICATOR,
                           CHARGE_TYPE_DICT.CHARGE_PRICE,
                           CHARGE_TYPE_DICT.CHARGE_LOW,
                           CHARGE_TYPE_DICT.PRINT_MODEL,
                           CHARGE_TYPE_DICT.OUTP_DISPLAY,
                           CHARGE_TYPE_DICT.IS_UPLOAD,
                           CHARGE_TYPE_DICT.SPELL_CODE,
                           CHARGE_TYPE_DICT.WB_CODE 
                          FROM CHARGE_TYPE_DICT ORDER BY CHARGE_TYPE_DICT.SERIAL_NO";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CHARGE_TYPE_DICT>(ds).ToList();
        }

        /// <summary>
        /// 获取关系字典表信息
        /// </summary>
        /// <returns></returns>
        public List<RELATIONSHIP_DICT> GetRELATIONSHIP_DICTInfor()
        {
            string sql = @"SELECT RELATIONSHIP_DICT.SERIAL_NO,
                           RELATIONSHIP_DICT.RELATIONSHIP_CODE,
                           RELATIONSHIP_DICT.RELATIONSHIP_NAME,
                           RELATIONSHIP_DICT.INPUT_CODE FROM RELATIONSHIP_DICT ORDER BY RELATIONSHIP_DICT.SERIAL_NO";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<RELATIONSHIP_DICT>(ds).ToList();
        }

        /// <summary>
        /// 保存病人主索引信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pat_master_index"></param>
        /// <returns></returns>
        public int SavePatientInfor(BaseEntityer db, PAT_MASTER_INDEX pat_master_index)
        {
            string sqlsearch = @"SELECT PAT_MASTER_INDEX.PATIENT_ID
                            FROM  PAT_MASTER_INDEX
                            WHERE PAT_MASTER_INDEX.PATIENT_ID = '{0}'";
            sqlsearch = string.Format(sqlsearch, pat_master_index.PATIENT_ID);

            string sql = string.Empty;
            DbDataReader dr = db.ExecuteReader(sqlsearch);
            if (dr.Read())
            {
                //update
                sql = @"update PAT_MASTER_INDEX 
                       set PAT_MASTER_INDEX.INP_NO = '{1}', 
                       PAT_MASTER_INDEX.NAME = '{2}', 
                       PAT_MASTER_INDEX.NAME_PHONETIC = '{3}', 
                       PAT_MASTER_INDEX.SEX = '{4}', 
                       PAT_MASTER_INDEX.DATE_OF_BIRTH = to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'), 
                       PAT_MASTER_INDEX.BIRTH_PLACE = '{6}', 
                       PAT_MASTER_INDEX.CITIZENSHIP = '{7}', 
                       PAT_MASTER_INDEX.NATION = '{8}', 
                       PAT_MASTER_INDEX.ID_NO = '{9}', 
                       PAT_MASTER_INDEX.IDENTITY = '{10}', 
                       PAT_MASTER_INDEX.CHARGE_TYPE = '{11}', 
                       PAT_MASTER_INDEX.UNIT_IN_CONTRACT = '{12}', 
                       PAT_MASTER_INDEX.MAILING_ADDRESS = '{13}', 
                       PAT_MASTER_INDEX.ZIP_CODE = '{14}', 
                       PAT_MASTER_INDEX.PHONE_NUMBER_HOME = '{15}', 
                       PAT_MASTER_INDEX.PHONE_NUMBER_BUSINESS = '{16}', 
                       PAT_MASTER_INDEX.NEXT_OF_KIN = '{17}', 
                       PAT_MASTER_INDEX.RELATIONSHIP = '{18}', 
                       PAT_MASTER_INDEX.NEXT_OF_KIN_ADDR = '{19}', 
                       PAT_MASTER_INDEX.NEXT_OF_KIN_ZIP_CODE = '{20}', 
                       PAT_MASTER_INDEX.NEXT_OF_KIN_PHONE = '{21}', 
                       PAT_MASTER_INDEX.LAST_VISIT_DATE = to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'), 
                       PAT_MASTER_INDEX.VIP_INDICATOR = '{23}', 
                       PAT_MASTER_INDEX.CREATE_DATE = to_date('{24}', 'yyyy-mm-dd hh24:mi:ss'), 
                       PAT_MASTER_INDEX.OPERATOR = '{25}', 
                       PAT_MASTER_INDEX.PAT_BED_BMP = '{26}' 
                     where PAT_MASTER_INDEX.PATIENT_ID = '{0}'";
            }
            else
            {
                //insert
                sql = @"insert into PAT_MASTER_INDEX
                      (PAT_MASTER_INDEX.PATIENT_ID,
                       PAT_MASTER_INDEX.INP_NO,
                       PAT_MASTER_INDEX.NAME,
                       PAT_MASTER_INDEX.NAME_PHONETIC,
                       PAT_MASTER_INDEX.SEX,
                       PAT_MASTER_INDEX.DATE_OF_BIRTH,
                       PAT_MASTER_INDEX.BIRTH_PLACE,
                       PAT_MASTER_INDEX.CITIZENSHIP,
                       PAT_MASTER_INDEX.NATION,
                       PAT_MASTER_INDEX.ID_NO,
                       PAT_MASTER_INDEX.IDENTITY,
                       PAT_MASTER_INDEX.CHARGE_TYPE,
                       PAT_MASTER_INDEX.UNIT_IN_CONTRACT,
                       PAT_MASTER_INDEX.MAILING_ADDRESS,
                       PAT_MASTER_INDEX.ZIP_CODE,
                       PAT_MASTER_INDEX.PHONE_NUMBER_HOME,
                       PAT_MASTER_INDEX.PHONE_NUMBER_BUSINESS,
                       PAT_MASTER_INDEX.NEXT_OF_KIN,
                       PAT_MASTER_INDEX.RELATIONSHIP,
                       PAT_MASTER_INDEX.NEXT_OF_KIN_ADDR,
                       PAT_MASTER_INDEX.NEXT_OF_KIN_ZIP_CODE,
                       PAT_MASTER_INDEX.NEXT_OF_KIN_PHONE,
                       PAT_MASTER_INDEX.LAST_VISIT_DATE,
                       PAT_MASTER_INDEX.VIP_INDICATOR,
                       PAT_MASTER_INDEX.CREATE_DATE,
                       PAT_MASTER_INDEX.OPERATOR,
                       PAT_MASTER_INDEX.PAT_BED_BMP)
                    values
                      ('{0}', '{1}', '{2}', '{3}', '{4}', to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'), 
                       '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}',
                       '{17}', '{18}', '{19}', '{20}', '{21}', to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'), 
                       '{23}', to_date('{24}', 'yyyy-mm-dd hh24:mi:ss'), '{25}', '{26}')";
            }
            object[] param = new object[] { pat_master_index.PATIENT_ID, pat_master_index.INP_NO, 
                pat_master_index.NAME, pat_master_index.NAME_PHONETIC, pat_master_index.SEX, 
                pat_master_index.DATE_OF_BIRTH, pat_master_index.BIRTH_PLACE, pat_master_index.CITIZENSHIP, 
                pat_master_index.NATION, pat_master_index.ID_NO, pat_master_index.IDENTITY, 
                pat_master_index.CHARGE_TYPE, pat_master_index.UNIT_IN_CONTRACT, pat_master_index.MAILING_ADDRESS, 
                pat_master_index.ZIP_CODE, pat_master_index.PHONE_NUMBER_HOME, pat_master_index.PHONE_NUMBER_BUSINESS, 
                pat_master_index.NEXT_OF_KIN, pat_master_index.RELATIONSHIP, pat_master_index.NEXT_OF_KIN_ADDR, 
                pat_master_index.NEXT_OF_KIN_ZIP_CODE, pat_master_index.NEXT_OF_KIN_PHONE, 
                pat_master_index.LAST_VISIT_DATE, pat_master_index.VIP_INDICATOR, pat_master_index.CREATE_DATE, 
                pat_master_index.OPERATOR, pat_master_index.PAT_BED_BMP };
            sql = Utility.SqlFormate(sql, param);

            if (!dr.IsClosed)
                dr.Close();
            return db.ExecuteNonQuery(sql);
        }

        public int SaveOutPatientInfo(BaseEntityer db, PAT_MASTER_INDEX pat_master_index)
        {
            //insert
            string sql = @"insert into PAT_MASTER_INDEX
                      (PAT_MASTER_INDEX.PATIENT_ID,
                       PAT_MASTER_INDEX.INP_NO,
                       PAT_MASTER_INDEX.NAME,
                       PAT_MASTER_INDEX.NAME_PHONETIC,
                       PAT_MASTER_INDEX.SEX,
                       PAT_MASTER_INDEX.DATE_OF_BIRTH,
                       PAT_MASTER_INDEX.BIRTH_PLACE,
                       PAT_MASTER_INDEX.CITIZENSHIP,
                       PAT_MASTER_INDEX.NATION,
                       PAT_MASTER_INDEX.ID_NO,
                       PAT_MASTER_INDEX.IDENTITY,
                       PAT_MASTER_INDEX.CHARGE_TYPE,
                       PAT_MASTER_INDEX.UNIT_IN_CONTRACT,
                       PAT_MASTER_INDEX.MAILING_ADDRESS,
                       PAT_MASTER_INDEX.ZIP_CODE,
                       PAT_MASTER_INDEX.PHONE_NUMBER_HOME,
                       PAT_MASTER_INDEX.PHONE_NUMBER_BUSINESS,
                       PAT_MASTER_INDEX.NEXT_OF_KIN,
                       PAT_MASTER_INDEX.RELATIONSHIP,
                       PAT_MASTER_INDEX.NEXT_OF_KIN_ADDR,
                       PAT_MASTER_INDEX.NEXT_OF_KIN_ZIP_CODE,
                       PAT_MASTER_INDEX.NEXT_OF_KIN_PHONE,
                       PAT_MASTER_INDEX.LAST_VISIT_DATE,
                       PAT_MASTER_INDEX.VIP_INDICATOR,
                       PAT_MASTER_INDEX.CREATE_DATE,
                       PAT_MASTER_INDEX.OPERATOR,
                       PAT_MASTER_INDEX.PAT_BED_BMP)
                    values
                      ('{0}', '{1}', '{2}', '{3}', '{4}', to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'), 
                       '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}',
                       '{17}', '{18}', '{19}', '{20}', '{21}', to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'), 
                       '{23}', to_date('{24}', 'yyyy-mm-dd hh24:mi:ss'), '{25}', '{26}')";
            object[] param = new object[] { pat_master_index.PATIENT_ID, pat_master_index.INP_NO,
                pat_master_index.NAME, pat_master_index.NAME_PHONETIC, pat_master_index.SEX,
                pat_master_index.DATE_OF_BIRTH, pat_master_index.BIRTH_PLACE, pat_master_index.CITIZENSHIP,
                pat_master_index.NATION, pat_master_index.ID_NO, pat_master_index.IDENTITY,
                pat_master_index.CHARGE_TYPE, pat_master_index.UNIT_IN_CONTRACT, pat_master_index.MAILING_ADDRESS,
                pat_master_index.ZIP_CODE, pat_master_index.PHONE_NUMBER_HOME, pat_master_index.PHONE_NUMBER_BUSINESS,
                pat_master_index.NEXT_OF_KIN, pat_master_index.RELATIONSHIP, pat_master_index.NEXT_OF_KIN_ADDR,
                pat_master_index.NEXT_OF_KIN_ZIP_CODE, pat_master_index.NEXT_OF_KIN_PHONE,
                pat_master_index.LAST_VISIT_DATE, pat_master_index.VIP_INDICATOR, pat_master_index.CREATE_DATE,
                pat_master_index.OPERATOR, pat_master_index.PAT_BED_BMP };
            sql = Utility.SqlFormate(sql, param);

            return db.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 根据挂号操作员查询返回其开具的最大发票号
        /// </summary>
        /// <param name="operatorNo"></param>
        /// <returns></returns>
        public DataTable GetBillNo(string operatorNo)
        {
            string sql = @"SELECT nvl(Max(billno),'') as billno
                            INTO :rcpt_no  
                            FROM clinic_master  
                           WHERE OPERATOR = '{0}'";
            sql = string.Format(sql, operatorNo);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 从数据库中取当前号的已挂号人数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="cliniclabel"></param>
        /// <param name="timedesc"></param>
        /// <returns></returns>
        public DataTable GetRegistrationNum(BaseEntityer db, string date, string cliniclabel, string timedesc)
        {
            string sql = @"select  registration_num,current_no,registration_limits
                          into :reg_sum,:current_no1
                          from clinic_for_regist
                          where clinic_date=to_date('{0}','yyyy-mm-dd') and clinic_label='{1}' and time_desc='{2}'";
            sql = string.Format(sql, date, cliniclabel, timedesc);
            return db.GetDataTable(sql);
        }

        /// <summary>
        /// 挂号数据插入
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_master"></param>
        /// <returns></returns>
        public int InsertClinicMaster(BaseEntityer db, CLINIC_MASTER clinic_master)
        {
            string sql = @"insert into CLINIC_MASTER
                      (CLINIC_MASTER.VISIT_DATE,
                        CLINIC_MASTER.VISIT_NO,
                        CLINIC_MASTER.CLINIC_LABEL,
                        CLINIC_MASTER.VISIT_TIME_DESC,
                        CLINIC_MASTER.SERIAL_NO,
                        CLINIC_MASTER.PATIENT_ID,
                        CLINIC_MASTER.NAME,
                        CLINIC_MASTER.NAME_PHONETIC,
                        CLINIC_MASTER.SEX,
                        CLINIC_MASTER.AGE,
                        CLINIC_MASTER.IDENTITY,
                        CLINIC_MASTER.CHARGE_TYPE,
                        CLINIC_MASTER.INSURANCE_TYPE,
                        CLINIC_MASTER.INSURANCE_NO,
                        CLINIC_MASTER.UNIT_IN_CONTRACT,
                        CLINIC_MASTER.CLINIC_TYPE,
                        CLINIC_MASTER.FIRST_VISIT_INDICATOR,
                        CLINIC_MASTER.VISIT_DEPT,
                        CLINIC_MASTER.VISIT_SPECIAL_CLINIC,
                        CLINIC_MASTER.DOCTOR,
                        CLINIC_MASTER.MR_PROVIDE_INDICATOR,
                        CLINIC_MASTER.REGISTRATION_STATUS,
                        CLINIC_MASTER.REGISTERING_DATE,
                        CLINIC_MASTER.SYMPTOM,
                        CLINIC_MASTER.REGIST_FEE,
                        CLINIC_MASTER.CLINIC_FEE,
                        CLINIC_MASTER.OTHER_FEE,
                        CLINIC_MASTER.CLINIC_CHARGE,
                        CLINIC_MASTER.OPERATOR,
                        CLINIC_MASTER.BILLNO,
                        CLINIC_MASTER.CHARGE_TYPE_CODE,
                        CLINIC_MASTER.INVOICE_NEW,CLINIC_MASTER.CARD_FEE,CLINIC_MASTER.CLINC_TYPE,
CLINIC_MASTER.PAY_COST,CLINIC_MASTER.PUB_COST,CLINIC_MASTER.serialnumber,
RETURNED_DATE,RETURNED_OPERATOR
)
                    values
                      (to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'), {1}, '{2}', '{3}', {4}, '{5}', 
                       '{6}', '{7}', '{8}', {9}, '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', {16},
                       '{17}', '{18}', '{19}', {20}, {21}, to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'), 
                       '{23}', {24}, {25}, {26}, {27}, '{28}', '{29}', '{30}', '{31}','{32}','{33}','{34}','{35}','{36}',
 to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'),'{28}'
)";
          
            object[] param = new object[] { clinic_master.VISIT_DATE.Date, clinic_master.VISIT_NO, clinic_master.CLINIC_LABEL, clinic_master.VISIT_TIME_DESC, clinic_master.SERIAL_NO, clinic_master.PATIENT_ID, clinic_master.NAME, clinic_master.NAME_PHONETIC, clinic_master.SEX, clinic_master.AGE, clinic_master.IDENTITY, clinic_master.CHARGE_TYPE, clinic_master.INSURANCE_TYPE, clinic_master.INSURANCE_NO, clinic_master.UNIT_IN_CONTRACT, clinic_master.CLINIC_TYPE, clinic_master.FIRST_VISIT_INDICATOR, clinic_master.VISIT_DEPT, clinic_master.VISIT_SPECIAL_CLINIC, clinic_master.DOCTOR, clinic_master.MR_PROVIDE_INDICATOR, clinic_master.REGISTRATION_STATUS, clinic_master.REGISTERING_DATE, clinic_master.SYMPTOM, clinic_master.REGIST_FEE, clinic_master.CLINIC_FEE, clinic_master.OTHER_FEE, clinic_master.CLINIC_CHARGE, clinic_master.OPERATOR, clinic_master.BILLNO, clinic_master.CHARGE_TYPE_CODE, clinic_master.INVOICE_NEW, clinic_master.CARD_FEE, clinic_master.CLINC_TYPE, clinic_master.PAY_COST, clinic_master.PUB_COST,clinic_master.SERIALNUMBER};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新门诊号表中该门诊号的当前号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="date"></param>
        /// <param name="cliniclabel"></param>
        /// <param name="timedesc"></param>
        /// <param name="currentno"></param>
        /// <param name="registrationnum"></param>
        /// <returns></returns>
        public int UpdateClinicForRegist(BaseEntityer db, string date, string cliniclabel, string timedesc, int currentno, int registrationnum)
        {
            string sql = @"Update clinic_for_regist set current_no ={3},registration_num={4}
		where clinic_date=to_date('{0}','yyyy-mm-dd') and clinic_label='{1}' and time_desc='{2}'";
            sql = string.Format(sql, date, cliniclabel, timedesc, currentno, registrationnum);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询病人挂号信息
        /// </summary>
        /// <param name="date">挂号日期</param>
        /// <param name="cliniclabel">门诊号别</param>
        /// <param name="visitno">就诊序号</param>
        /// <param name="serialno">序号//2013-3-18客户要求根据PATIENT_ID查询</param>
        /// <returns></returns>
        public CLINIC_MASTER GetClinicMaster(string date, string cliniclabel, string visitno, string serialno)
        {
            string sql = @"select CLINIC_MASTER.VISIT_DATE,
                       CLINIC_MASTER.VISIT_NO,
                       CLINIC_MASTER.CLINIC_LABEL,
                       CLINIC_MASTER.VISIT_TIME_DESC,
                       CLINIC_MASTER.SERIAL_NO,
                       CLINIC_MASTER.PATIENT_ID,
                       CLINIC_MASTER.NAME,
                       CLINIC_MASTER.NAME_PHONETIC,
                       CLINIC_MASTER.SEX,
                       CLINIC_MASTER.AGE,
                       CLINIC_MASTER.IDENTITY,
                       CLINIC_MASTER.CHARGE_TYPE,
                       CLINIC_MASTER.INSURANCE_TYPE,
                       CLINIC_MASTER.INSURANCE_NO,
                       CLINIC_MASTER.UNIT_IN_CONTRACT,
                       CLINIC_MASTER.CLINIC_TYPE,
                       CLINIC_MASTER.FIRST_VISIT_INDICATOR,
                       CLINIC_MASTER.VISIT_DEPT,
                       CLINIC_MASTER.VISIT_SPECIAL_CLINIC,
                       CLINIC_MASTER.DOCTOR,
                       CLINIC_MASTER.MR_PROVIDE_INDICATOR,
                       CLINIC_MASTER.REGISTRATION_STATUS,
                       CLINIC_MASTER.REGISTERING_DATE,
                       CLINIC_MASTER.SYMPTOM,
                       CLINIC_MASTER.REGIST_FEE,
                       CLINIC_MASTER.CLINIC_FEE,
                       CLINIC_MASTER.OTHER_FEE,
                       CLINIC_MASTER.CLINIC_CHARGE,
                       CLINIC_MASTER.OPERATOR,
                       CLINIC_MASTER.RETURNED_DATE,
                       CLINIC_MASTER.RETURNED_OPERATOR,
                       CLINIC_MASTER.ADMIS,
                       CLINIC_MASTER.BILLNO,
                       CLINIC_MASTER.ACCT_NO,
                       CLINIC_MASTER.Charge_Type_Code,
                       CLINIC_MASTER.INVOICE_NEW,
                       CLINIC_MASTER.CARD_FEE,
                       CLINIC_MASTER.Pay_Cost,
                       CLINIC_MASTER.Pub_Cost,
(select pm.phone_number_home from pat_master_index pm where pm.patient_id = CLINIC_MASTER.patient_id)
                       from CLINIC_MASTER
                       where CLINIC_MASTER.VISIT_DATE = to_date('{0}','yyyy-mm-dd') ";
            if (cliniclabel != string.Empty)
            {
                sql += " and CLINIC_MASTER.CLINIC_LABEL = '{1}'";
            }
            if (serialno != string.Empty)
            {
                sql += " and CLINIC_MASTER.PATIENT_ID = {2}";
            }
            if (visitno != string.Empty)
            {
                sql += " and CLINIC_MASTER.Visit_No = {3}";
            }
            sql = string.Format(sql, date, cliniclabel, serialno, visitno);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<CLINIC_MASTER>(ds).First();
            else
                return null;
        }

        /// <summary>
        /// 门诊退号时更新门诊号表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="date"></param>
        /// <param name="cliniclabel"></param>
        /// <param name="timedesc"></param>
        /// <param name="registrationnum"></param>
        /// <returns></returns>
        public int UpdateClinicForRegist(BaseEntityer db, string date, string cliniclabel, string timedesc)
        {
            string sql = @"Update clinic_for_regist set registration_num = registration_num - 1 
		        where clinic_date =to_date('{0}','yyyy-mm-dd') and clinic_label ='{1}' and time_desc ='{2}'";
            sql = string.Format(sql, date, cliniclabel, timedesc);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 门诊退号更新退号员
        /// </summary>
        /// <param name="db"></param>
        /// <param name="returneddate"></param>
        /// <param name="returnedoperator"></param>
        /// <param name="visitdate"></param>
        /// <param name="visitno"></param>
        /// <returns></returns>
        public int UpdateClinicMaster(BaseEntityer db, string returneddate, string returnedoperator, string visitdate, string visitno)
        {
            string sql = @"Update CLINIC_MASTER set RETURNED_DATE = 
                    to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'), RETURNED_OPERATOR = '{1}' 
		        where VISIT_DATE = to_date('{2}','yyyy-mm-dd') and VISIT_NO = {3}";
            sql = string.Format(sql, returneddate, returnedoperator, visitdate, visitno);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取今日挂号病人列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetClinicMasterToday(string date)
        {
            string sql = @"select CLINIC_MASTER.VISIT_DATE,
                       CLINIC_MASTER.VISIT_NO,
                       CLINIC_MASTER.CLINIC_LABEL,
                       CLINIC_MASTER.NAME,
                       F_TRANS_PINYIN_CAPITAL(CLINIC_MASTER.NAME) spell_name
                        from CLINIC_MASTER
                       where CLINIC_MASTER.VISIT_DATE = to_date('{0}','yyyy-mm-dd')";
            sql = string.Format(sql, date);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取未退号患者列表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataTable GetOutpatientNORefund(string date)
        {
            string sql = @"select CLINIC_MASTER.VISIT_DATE,
                           CLINIC_MASTER.VISIT_NO,
                           CLINIC_MASTER.CLINIC_LABEL,
                           CLINIC_MASTER.NAME,
                           F_TRANS_PINYIN_CAPITAL(CLINIC_MASTER.NAME) spell_name
                            from CLINIC_MASTER
                           where CLINIC_MASTER.VISIT_DATE = to_date('{0}','yyyy-mm-dd')
                            and CLINIC_MASTER.Returned_Date is null ";
            sql = string.Format(sql, date);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 挂号累计查询
        /// </summary>
        /// <param name="date">截止日期</param>
        /// <param name="operatorid">收款员号</param>
        /// <returns></returns>
        public DataTable GetRegistrationAccounts(string date, string operatorid)
        {
            string sql = @"select a.typeaccounts,a.CLINIC_TYPE,a.regist_fee,a.clinic_fee,a.other_fee,
                        (a.regist_fee+a.clinic_fee+a.other_fee) as sum_fee,a.CARD_FEE,a.PAY_COST,a.PUB_COST,a.visit_no,a.charge_type_code from 
                        (
                           select * from 
                           (
                            select '挂号' as typeaccounts,aa.regist_fee,aa.clinic_fee,aa.other_fee,aa.CARD_FEE,aa.PAY_COST,aa.PUB_COST,aa.visit_no,aa.charge_type_code,
                            CLINIC_TYPE_DICT.CLINIC_TYPE from 
                            (
                                select nvl(sum(regist_fee),0.00) as regist_fee, nvl(sum(clinic_fee),0.00) as clinic_fee, 
                                nvl(sum(other_fee),0.00) as other_fee,nvl(sum(CARD_FEE),0.00) as CARD_FEE,nvl(sum(PAY_COST),0.00) as PAY_COST,nvl(sum(PUB_COST),0.00) as PUB_COST, nvl(count(visit_no),0) as visit_no, 
                                clinic_master.clinic_type,clinic_master.charge_type_code from clinic_master 
                                where registering_date <= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                                and clinic_master.operator = '{1}'
                                and acct_no = '------' and serialnumber is null
                                group by clinic_master.clinic_type,clinic_master.charge_type_code
                            ) 
                            aa right join  CLINIC_TYPE_DICT on aa.clinic_type = CLINIC_TYPE_DICT.CLINIC_TYPE
                            order by CLINIC_TYPE_DICT.CLINIC_TYPE desc
                            )
                          union all
                          select * from 
                           (
                            select '退号' as typeaccounts,aa.regist_fee,aa.clinic_fee,aa.other_fee,aa.CARD_FEE,aa.PAY_COST,aa.PUB_COST,aa.visit_no,aa.charge_type_code,
                            CLINIC_TYPE_DICT.CLINIC_TYPE from 
                            (
                                select nvl(sum(regist_fee),0.00) as regist_fee, nvl(sum(clinic_fee),0.00) as clinic_fee, 
                                nvl(sum(other_fee),0.00) as other_fee,nvl(sum(CARD_FEE),0.00) as CARD_FEE,nvl(sum(PAY_COST),0.00) as PAY_COST,nvl(sum(PUB_COST),0.00) as PUB_COST, nvl(count(visit_no),0) as visit_no, 
                                clinic_master.clinic_type,clinic_master.charge_type_code from clinic_master 
                                where RETURNED_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                                and clinic_master.returned_operator = '{1}'
                                and RETURNED_ACCT_NO = '------'
                                group by clinic_master.clinic_type,clinic_master.charge_type_code
                            ) 
                            aa right join  CLINIC_TYPE_DICT on aa.clinic_type = CLINIC_TYPE_DICT.CLINIC_TYPE
                            order by CLINIC_TYPE_DICT.CLINIC_TYPE desc
                            )
                        ) a";
            sql = string.Format(sql, date, operatorid);
            return BaseEntityer.Db.GetDataTable(sql);
        }


        /// <summary>
        /// 挂号累计查询
        /// </summary>
        /// <param name="date">结算单号</param>
        /// <returns></returns>
        public DataTable GetRegistrationAccountsNO(string ACCT_NO)
        {
            string sql = @"select a.typeaccounts,a.CLINIC_TYPE,a.regist_fee,a.clinic_fee,a.other_fee,
                        (a.regist_fee+a.clinic_fee+a.other_fee) as sum_fee,a.CARD_FEE,a.PAY_COST,a.PUB_COST,a.visit_no,a.charge_type_code from 
                        (
                           select * from 
                           (
                            select '挂号' as typeaccounts,aa.regist_fee,aa.clinic_fee,aa.other_fee,aa.CARD_FEE,aa.PAY_COST,aa.PUB_COST,aa.visit_no,aa.charge_type_code,
                            CLINIC_TYPE_DICT.CLINIC_TYPE from 
                            (
                                select nvl(sum(regist_fee),0.00) as regist_fee, nvl(sum(clinic_fee),0.00) as clinic_fee, 
                                nvl(sum(other_fee),0.00) as other_fee,nvl(sum(CARD_FEE),0.00) as CARD_FEE,nvl(sum(PAY_COST),0.00) as PAY_COST,nvl(sum(PUB_COST),0.00) as PUB_COST, nvl(count(visit_no),0) as visit_no, 
                                clinic_master.clinic_type,clinic_master.charge_type_code from clinic_master 
                                where  acct_no ='{0}'
                                group by clinic_master.clinic_type,clinic_master.charge_type_code
                            ) 
                            aa right join  CLINIC_TYPE_DICT on aa.clinic_type = CLINIC_TYPE_DICT.CLINIC_TYPE
                            order by CLINIC_TYPE_DICT.CLINIC_TYPE desc
                            )
                          union all
                          select * from 
                           (
                            select '退号' as typeaccounts,aa.regist_fee,aa.clinic_fee,aa.other_fee,aa.CARD_FEE,aa.PAY_COST,aa.PUB_COST,aa.visit_no,aa.charge_type_code,
                            CLINIC_TYPE_DICT.CLINIC_TYPE from 
                            (
                                select nvl(sum(regist_fee),0.00) as regist_fee, nvl(sum(clinic_fee),0.00) as clinic_fee, 
                                nvl(sum(other_fee),0.00) as other_fee,nvl(sum(CARD_FEE),0.00) as CARD_FEE,nvl(sum(PAY_COST),0.00) as PAY_COST,nvl(sum(PUB_COST),0.00) as PUB_COST, nvl(count(visit_no),0) as visit_no, 
                                clinic_master.clinic_type,clinic_master.charge_type_code from clinic_master 
                                where  RETURNED_ACCT_NO = '{0}'
                                group by clinic_master.clinic_type,clinic_master.charge_type_code
                            ) 
                            aa right join  CLINIC_TYPE_DICT on aa.clinic_type = CLINIC_TYPE_DICT.CLINIC_TYPE
                            order by CLINIC_TYPE_DICT.CLINIC_TYPE desc
                            )
                        ) a";
            sql = string.Format(sql, ACCT_NO);
            return BaseEntityer.Db.GetDataTable(sql);
        }


        /// <summary>
        /// 挂号累计查询
        /// </summary>
        /// <param name="date">结算单号</param>
        /// <returns></returns>
        public DataTable GetINRegistrationAccountsNO(string ACCT_NO)
        {
            string sql = @"select a.typeaccounts,a.CLINIC_TYPE,a.regist_fee,a.clinic_fee,a.other_fee,
                        (a.regist_fee+a.clinic_fee+a.other_fee) as sum_fee,a.CARD_FEE,a.PAY_COST,a.PUB_COST,a.visit_no,a.charge_type_code from 
                        (
                           select * from 
                           (
                            select '挂号' as typeaccounts,aa.regist_fee,aa.clinic_fee,aa.other_fee,aa.CARD_FEE,aa.PAY_COST,aa.PUB_COST,aa.visit_no,aa.charge_type_code,
                            CLINIC_TYPE_DICT.CLINIC_TYPE from 
                            (
                                select nvl(sum(regist_fee),0.00) as regist_fee, nvl(sum(clinic_fee),0.00) as clinic_fee, 
                                nvl(sum(other_fee),0.00) as other_fee,nvl(sum(CARD_FEE),0.00) as CARD_FEE,nvl(sum(PAY_COST),0.00) as PAY_COST,nvl(sum(PUB_COST),0.00) as PUB_COST, nvl(count(visit_no),0) as visit_no, 
                                clinic_master.clinic_type,clinic_master.charge_type_code from clinic_master 
                                where  acct_no in({0})
                                group by clinic_master.clinic_type,clinic_master.charge_type_code
                            ) 
                            aa right join  CLINIC_TYPE_DICT on aa.clinic_type = CLINIC_TYPE_DICT.CLINIC_TYPE
                            order by CLINIC_TYPE_DICT.CLINIC_TYPE desc
                            )
                          union all
                          select * from 
                           (
                            select '退号' as typeaccounts,aa.regist_fee,aa.clinic_fee,aa.other_fee,aa.CARD_FEE,aa.PAY_COST,aa.PUB_COST,aa.visit_no,aa.charge_type_code,
                            CLINIC_TYPE_DICT.CLINIC_TYPE from 
                            (
                                select nvl(sum(regist_fee),0.00) as regist_fee, nvl(sum(clinic_fee),0.00) as clinic_fee, 
                                nvl(sum(other_fee),0.00) as other_fee,nvl(sum(CARD_FEE),0.00) as CARD_FEE,nvl(sum(PAY_COST),0.00) as PAY_COST,nvl(sum(PUB_COST),0.00) as PUB_COST, nvl(count(visit_no),0) as visit_no, 
                                clinic_master.clinic_type,clinic_master.charge_type_code from clinic_master 
                                where  RETURNED_ACCT_NO in({0})
                                group by clinic_master.clinic_type,clinic_master.charge_type_code
                            ) 
                            aa right join  CLINIC_TYPE_DICT on aa.clinic_type = CLINIC_TYPE_DICT.CLINIC_TYPE
                            order by CLINIC_TYPE_DICT.CLINIC_TYPE desc
                            )
                        ) a";
            sql = string.Format(sql, ACCT_NO);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 挂号累计查询（统一日结）
        /// </summary>
        /// <param name="date">截止日期</param>
        /// <param name="operatorid">收款员号</param>
        /// <returns></returns>
        public DataTable GetUnifyRegistrationAccounts(string date, string operatorid)
        {

            string sql = @"Update CLINIC_MASTER set ACCT_FLAG ='1' where 
                 registering_date <= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                                and clinic_master.operator = '{1}'
                                and acct_no = '------'";
            sql = string.Format(sql, date, operatorid);
            BaseEntityer.Db.ExecuteNonQuery(sql);


            sql = @"Update CLINIC_MASTER set RETURNED_ACCT_FLAG ='1' where 
                RETURNED_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                                and clinic_master.returned_operator = '{1}'
                                and RETURNED_ACCT_NO = '------'";
            sql = string.Format(sql, date, operatorid);
            BaseEntityer.Db.ExecuteNonQuery(sql);


            sql = @"select a.typeaccounts,a.CLINIC_TYPE,a.regist_fee,a.clinic_fee,a.other_fee,
                        (a.regist_fee+a.clinic_fee+a.other_fee) as sum_fee,a.CARD_FEE,a.visit_no from 
                        (
                           select * from 
                           (
                            select '挂号' as typeaccounts,aa.regist_fee,aa.clinic_fee,aa.other_fee,aa.CARD_FEE,aa.visit_no,
                            CLINIC_TYPE_DICT.CLINIC_TYPE from 
                            (
                                select nvl(sum(regist_fee),0.00) as regist_fee, nvl(sum(clinic_fee),0.00) as clinic_fee, 
                                nvl(sum(other_fee),0.00) as other_fee,nvl(sum(CARD_FEE),0.00) as CARD_FEE, nvl(count(visit_no),0) as visit_no, 
                                clinic_master.clinic_type from clinic_master 
                                where registering_date <= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                                and clinic_master.operator = '{1}'
                                and acct_no = '------' and ACCT_FLAG ='1'
                                group by clinic_master.clinic_type
                            ) 
                            aa right join  CLINIC_TYPE_DICT on aa.clinic_type = CLINIC_TYPE_DICT.CLINIC_TYPE
                            order by CLINIC_TYPE_DICT.CLINIC_TYPE desc
                            )
                          union all
                          select * from 
                           (
                            select '退号' as typeaccounts,aa.regist_fee,aa.clinic_fee,aa.other_fee,aa.CARD_FEE,aa.visit_no,
                            CLINIC_TYPE_DICT.CLINIC_TYPE from 
                            (
                                select nvl(sum(regist_fee),0.00) as regist_fee, nvl(sum(clinic_fee),0.00) as clinic_fee, 
                                nvl(sum(other_fee),0.00) as other_fee,nvl(sum(CARD_FEE),0.00) as CARD_FEE, nvl(count(visit_no),0) as visit_no, 
                                clinic_master.clinic_type from clinic_master 
                                where RETURNED_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                                and clinic_master.returned_operator = '{1}'
                                and RETURNED_ACCT_NO = '------' and RETURNED_ACCT_FLAG ='1'
                                group by clinic_master.clinic_type
                            ) 
                            aa right join  CLINIC_TYPE_DICT on aa.clinic_type = CLINIC_TYPE_DICT.CLINIC_TYPE
                            order by CLINIC_TYPE_DICT.CLINIC_TYPE desc
                            )
                        ) a";
            sql = string.Format(sql, date, operatorid);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 挂号结账确认
        /// </summary>
        /// <param name="db"></param>
        /// <param name="regist_acct"></param>
        /// <returns></returns>
        public int InsertRegistAcct(BaseEntityer db, REGIST_ACCT regist_acct)
        {
            string sql = @"INSERT INTO regist_acct
                           (regist_acct.acct_no, 
                           regist_acct.operator_no, 
                           regist_acct.acct_date, 
                           regist_acct.regist_num, 
                           regist_acct.refund_num, 
                           regist_acct.refund_amount, 
                           regist_acct.total_amount, 
                           regist_acct.tally_date,
                           regist_acct.OUTP_ACCT_NO,
                           REFUND_CARD_AMOUNT,
                           TOTAL_CARD_AMOUNT,
                           REFUND_PAY_COST,
                           TOTAL_PAY_COST
                           )
                           VALUES
                           ('{0}','{1}',to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'),
                           {3},{4},{5},{6},
                           to_date('{7}', 'yyyy-mm-dd hh24:mi:ss'),{8},{9},{10},{11},{12})";
            object[] param = new object[] { regist_acct.ACCT_NO, regist_acct.OPERATOR_NO, regist_acct.ACCT_DATE, 
                regist_acct.REGIST_NUM, regist_acct.REFUND_NUM, regist_acct.REFUND_AMOUNT, 
                regist_acct.TOTAL_AMOUNT, regist_acct.TALLY_DATE, regist_acct.Outp_acct_no,
                regist_acct.REFUND_CARD_AMOUNT,regist_acct.TOTAL_CARD_AMOUNT,regist_acct.REFUND_PAY_COST,regist_acct.TOTAL_PAY_COST};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 门诊挂号结算时更新所有此次结算挂号记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="registeringdate"></param>
        /// <param name="operatorid"></param>
        /// <param name="acct_no"></param>
        /// <returns></returns>
        public int UpdateClinicMaster(BaseEntityer db, string registeringdate, string operatorid, string acct_no)
        {
            string sql = @"update clinic_master 
                            set acct_no = '{2}' 
                            where registering_date < to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')  
                            and operator= '{1}' and acct_no = '------' and serialnumber is null";
            sql = string.Format(sql, registeringdate, operatorid, acct_no);
            return db.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 门诊挂号结算时更新所有此次结算挂号记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="registeringdate"></param>
        /// <param name="operatorid"></param>
        /// <param name="acct_no"></param>
        /// <returns></returns>
        public int UpdateUnifyClinicMaster(BaseEntityer db, string registeringdate, string operatorid, string acct_no)
        {
            string sql = @"update clinic_master 
                            set acct_no = '{2}' 
                            where registering_date <= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')  
                            and operator= '{1}' and acct_no = '------' and ACCT_FLAG ='1'";
            sql = string.Format(sql, registeringdate, operatorid, acct_no);
            return db.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 2013-3-27 by li 挂号结算此前一个结算标志字段出现bug
        /// 需要增加退号结算标志字段，挂号员结算更新挂号字段，
        /// 退号员结算更新退号字段，保证统计数据正确性
        /// 门诊挂号结算时更新所有此次结算退号记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="registeringdate"></param>
        /// <param name="operatorid"></param>
        /// <param name="acct_no"></param>
        /// <returns></returns>
        public int UpdateClinicMasterRetreat(BaseEntityer db, string registeringdate, string operatorid, string acct_no)
        {
            string sql = @"update clinic_master 
                            set RETURNED_ACCT_NO = '{2}' 
                            where RETURNED_DATE < to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')  
                            and RETURNED_OPERATOR= '{1}' and RETURNED_ACCT_NO = '------' ";
            sql = string.Format(sql, registeringdate, operatorid, acct_no);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2013-3-27 by li 挂号结算此前一个结算标志字段出现bug
        /// 需要增加退号结算标志字段，挂号员结算更新挂号字段，
        /// 退号员结算更新退号字段，保证统计数据正确性
        /// 门诊挂号结算时更新所有此次结算退号记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="registeringdate"></param>
        /// <param name="operatorid"></param>
        /// <param name="acct_no"></param>
        /// <returns></returns>
        public int UpdateUnifyClinicMasterRetreat(BaseEntityer db, string registeringdate, string operatorid, string acct_no)
        {
            string sql = @"update clinic_master 
                            set RETURNED_ACCT_NO = '{2}' 
                            where RETURNED_DATE <= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')  
                            and RETURNED_OPERATOR= '{1}' and RETURNED_ACCT_NO = '------' and RETURNED_ACCT_FLAG ='1'";
            sql = string.Format(sql, registeringdate, operatorid, acct_no);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 根据挂号操作员查询返回其结算的最大号
        /// </summary>
        /// <param name="operatorNo"></param>
        /// <returns></returns>
        public DataTable GetAcctNo(string operatorNo)
        {
            string sql = @"SELECT Max(acct_no) as acct_no 
                            from regist_acct 
                            where operator_no = '{0}'";
            sql = string.Format(sql, operatorNo);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 根据病患ID获取消费记录
        /// </summary>
        /// <param name="_patientID"></param>
        /// <param name="_dateFrom"></param>
        /// <param name="_dateTo"></param>
        /// <returns></returns>
        public DataTable GetPatientFeeView(string _patientID, string _dateFrom, string _dateTo)
        {
            string sql = @"select * from v_fee_statistic_new t 
                        where t.patient_id = '{0}' and 
                        t.operdate >= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and 
                        t.operdate <= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')";
            sql = string.Format(sql, _patientID, _dateFrom, _dateTo);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 根据病患ID获取充值记录
        /// </summary>
        /// <param name="_patientID"></param>
        /// <param name="_dateFrom"></param>
        /// <param name="_dateTo"></param>
        /// <returns></returns>
        public DataTable GetPatientRechargeView(string _patientID, string _dateFrom, string _dateTo)
        {
            string sql = @"select * from transaction_record t 
                        where t.patient_id = '{0}' and 
                        t.rcpt_date >= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and 
                        t.rcpt_date <= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') and
                        length(t.rcpt_no) < 12 order by t.rcpt_date";
            sql = string.Format(sql, _patientID, _dateFrom, _dateTo);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        #region 门诊维护

        /// <summary>
        /// 获取门诊号别列表
        /// </summary>
        /// <returns></returns>
        public List<CLINIC_INDEX> GetClinicIndex()
        {
            string sql = @"select CLINIC_INDEX.Clinic_Label,
                       CLINIC_INDEX.CLINIC_DEPT,
                       DEPT_DICT.DEPT_NAME,
                       CLINIC_INDEX.Doctor,
                       CLINIC_INDEX.DOCTOR_TITLE,
                       DOCTOR_TITLE_DICT.TITLE_NAME,
                       CLINIC_INDEX.Clinic_Type,
                       CLINIC_INDEX.Input_Code,
                       CLINIC_INDEX.Clinc_Type    
                       from CLINIC_INDEX
                       left join DEPT_DICT on CLINIC_INDEX.Clinic_Dept = DEPT_DICT.DEPT_CODE
                       left join DOCTOR_TITLE_DICT on CLINIC_INDEX.Doctor_Title = DOCTOR_TITLE_DICT.TITLE_CODE
                       order by CLINIC_INDEX.CLINIC_LABEL,CLINIC_INDEX.CLINIC_TYPE desc";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_INDEX>(ds).ToList();
        }

        /// <summary>
        /// 新增门诊号别
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_index"></param>
        /// <returns></returns>
        public int InsertClinicIndex(BaseEntityer db, CLINIC_INDEX clinic_index)
        {
            string sql = @"INSERT INTO CLINIC_INDEX
                        (
                               CLINIC_INDEX.Clinic_Label,
                               CLINIC_INDEX.CLINIC_DEPT,
                               CLINIC_INDEX.Doctor,
                               CLINIC_INDEX.DOCTOR_TITLE,
                               CLINIC_INDEX.Clinic_Type,
                               CLINIC_INDEX.Input_Code,
                               CLINIC_INDEX.Clinc_Type
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}','{3}','{4}','{5}','{6}'
                        )";
            object[] param = new object[] { clinic_index.CLINIC_LABEL, clinic_index.CLINIC_DEPT, 
                clinic_index.DOCTOR, clinic_index.DOCTOR_TITLE, clinic_index.CLINIC_TYPE, clinic_index.INPUT_CODE, clinic_index.CLINC_TYPE};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新门诊号别记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_index"></param>
        /// <param name="old_clinic_label"></param>
        /// <returns></returns>
        public int UpdateClinicIndex(BaseEntityer db, CLINIC_INDEX clinic_index, string old_clinic_label)
        {
            string sql = @"UPDATE CLINIC_INDEX 
                        SET 
                               CLINIC_INDEX.Clinic_Label = '{0}',
                               CLINIC_INDEX.CLINIC_DEPT = '{1}',
                               CLINIC_INDEX.Doctor = '{2}',
                               CLINIC_INDEX.DOCTOR_TITLE = '{3}',
                               CLINIC_INDEX.Clinic_Type = '{4}',
                               CLINIC_INDEX.Input_Code = '{5}',
                               CLINIC_INDEX.Clinc_Type = '{7}'
                        WHERE CLINIC_INDEX.Clinic_Label = '{6}'";
            object[] param = new object[] { clinic_index.CLINIC_LABEL, clinic_index.CLINIC_DEPT, 
                clinic_index.DOCTOR, clinic_index.DOCTOR_TITLE, clinic_index.CLINIC_TYPE, 
                clinic_index.INPUT_CODE, old_clinic_label ,clinic_index.CLINC_TYPE};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定号类数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_Label"></param>
        /// <returns></returns>
        public int DeleteClinicIndex(BaseEntityer db, string clinic_Label)
        {
            string sql = @"delete from CLINIC_INDEX where CLINIC_INDEX.Clinic_Label = '{0}'";
            sql = string.Format(sql, clinic_Label);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取号类列表
        /// </summary>
        /// <returns></returns>
        public List<CLINIC_TYPE_DICT> GetClinicTypeDict()
        {
            string sql = @"select CLINIC_TYPE_DICT.SERIAL_NO,
                           CLINIC_TYPE_DICT.CLINIC_TYPE,
                           CLINIC_TYPE_DICT.REGPRICE,
                           CLINIC_TYPE_DICT.DIAGPRICE
                           from CLINIC_TYPE_DICT";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_TYPE_DICT>(ds).ToList();
        }

        /// <summary>
        /// 获取号类列表最大SerialNo
        /// </summary>
        /// <returns></returns>
        public DbDataReader GetMaxClinicTypeDictSerialNo(BaseEntityer db)
        {
            string sql = @"SELECT MAX(CLINIC_TYPE_DICT.SERIAL_NO) AS SERIAL_NO
                            FROM CLINIC_TYPE_DICT";
            return db.ExecuteReader(sql);
        }

        /// <summary>
        /// 新增号类
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_type_dict"></param>
        /// <returns></returns>
        public int InsertClinicTypeDict(BaseEntityer db, CLINIC_TYPE_DICT clinic_type_dict)
        {
            string sql = @"INSERT INTO CLINIC_TYPE_DICT
                        (
                               CLINIC_TYPE_DICT.SERIAL_NO,
                               CLINIC_TYPE_DICT.CLINIC_TYPE,
                               CLINIC_TYPE_DICT.REGPRICE,
                               CLINIC_TYPE_DICT.DIAGPRICE
                        )
                        VALUES
                        (
                               {0},'{1}',{2},{3}
                        )";
            object[] param = new object[] { clinic_type_dict.SERIAL_NO, clinic_type_dict.CLINIC_TYPE, 
                clinic_type_dict.REGPRICE, clinic_type_dict.DIAGPRICE };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新号类记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_type_dict"></param>
        /// <param name="old_clinic_type"></param>
        /// <returns></returns>
        public int UpdateClinicTypeDict(BaseEntityer db, CLINIC_TYPE_DICT clinic_type_dict, string old_clinic_type)
        {
            string sql = @"UPDATE CLINIC_TYPE_DICT 
                        SET 
                               CLINIC_TYPE_DICT.SERIAL_NO = {0},
                               CLINIC_TYPE_DICT.CLINIC_TYPE = '{1}',
                               CLINIC_TYPE_DICT.REGPRICE = {2},
                               CLINIC_TYPE_DICT.DIAGPRICE = {3} 
                        WHERE CLINIC_TYPE_DICT.CLINIC_TYPE = '{4}'";
            object[] param = new object[] { clinic_type_dict.SERIAL_NO, clinic_type_dict.CLINIC_TYPE, 
                clinic_type_dict.REGPRICE, clinic_type_dict.DIAGPRICE, old_clinic_type };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定号类数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_type"></param>
        /// <returns></returns>
        public int DeleteClinicTypeDict(BaseEntityer db, string clinic_type)
        {
            string sql = @"delete from CLINIC_TYPE_DICT where CLINIC_TYPE_DICT.CLINIC_TYPE = '{0}'";
            sql = string.Format(sql, clinic_type);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取诊疗费收费标准列表
        /// </summary>
        /// <returns></returns>
        public List<CLINIC_PRICE> GetClinicPrice()
        {
            string sql = @"SELECT CLINIC_PRICE.CHARGE_TYPE,   
                           CLINIC_PRICE.PRICE  
                           FROM CLINIC_PRICE";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_PRICE>(ds).ToList();
        }

        /// <summary>
        /// 新增诊疗费收费标准
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_price"></param>
        /// <returns></returns>
        public int InsertClinicPrice(BaseEntityer db, CLINIC_PRICE clinic_price)
        {
            string sql = @"INSERT INTO CLINIC_PRICE
                        (
                               CLINIC_PRICE.CHARGE_TYPE,   
                               CLINIC_PRICE.PRICE
                        )
                        VALUES
                        (
                               '{0}',{1}
                        )";
            object[] param = new object[] { clinic_price.CHARGE_TYPE, clinic_price.PRICE };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新诊疗费收费标准记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_price"></param>
        /// <param name="old_charge_type"></param>
        /// <returns></returns>
        public int UpdateClinicPrice(BaseEntityer db, CLINIC_PRICE clinic_price, string old_charge_type)
        {
            string sql = @"UPDATE CLINIC_PRICE 
                        SET 
                               CLINIC_PRICE.CHARGE_TYPE = '{0}',   
                               CLINIC_PRICE.PRICE = {1} 
                        WHERE CLINIC_PRICE.CHARGE_TYPE = '{2}'";
            object[] param = new object[] { clinic_price.CHARGE_TYPE, clinic_price.PRICE, old_charge_type };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定诊疗费收费标准数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="charge_type"></param>
        /// <returns></returns>
        public int DeleteClinicPrice(BaseEntityer db, string charge_type)
        {
            string sql = @"delete from CLINIC_PRICE where CLINIC_PRICE.CHARGE_TYPE = '{0}'";
            sql = string.Format(sql, charge_type);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取特殊挂号费列表
        /// </summary>
        /// <returns></returns>
        public List<SPECIAL_REGIST_PRICE> GetSpecialRegistPrice()
        {
            string sql = @"SELECT SPECIAL_REGIST_PRICE.CLINIC_TYPE,   
                           SPECIAL_REGIST_PRICE.CHARGE_TYPE,   
                           SPECIAL_REGIST_PRICE.PRICE  
                           FROM SPECIAL_REGIST_PRICE";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<SPECIAL_REGIST_PRICE>(ds).ToList();
        }

        /// <summary>
        /// 新增特殊挂号费
        /// </summary>
        /// <param name="db"></param>
        /// <param name="special_regist_price"></param>
        /// <returns></returns>
        public int InsertSpecialRegistPrice(BaseEntityer db, SPECIAL_REGIST_PRICE special_regist_price)
        {
            string sql = @"INSERT INTO SPECIAL_REGIST_PRICE
                        (
                               SPECIAL_REGIST_PRICE.CLINIC_TYPE,   
                               SPECIAL_REGIST_PRICE.CHARGE_TYPE,   
                               SPECIAL_REGIST_PRICE.PRICE
                        )
                        VALUES
                        (
                               '{0}','{1}',{2}
                        )";
            object[] param = new object[] { special_regist_price.CLINIC_TYPE, 
                special_regist_price.CHARGE_TYPE, special_regist_price.PRICE };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新特殊挂号费记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="special_regist_price"></param>
        /// <param name="old_clinic_type"></param>
        /// <param name="old_charge_type"></param>
        /// <returns></returns>
        public int UpdateSpecialRegistPrice(BaseEntityer db, SPECIAL_REGIST_PRICE special_regist_price, string old_clinic_type, string old_charge_type)
        {
            string sql = @"UPDATE SPECIAL_REGIST_PRICE 
                        SET 
                               SPECIAL_REGIST_PRICE.CLINIC_TYPE = '{0}',   
                               SPECIAL_REGIST_PRICE.CHARGE_TYPE = '{1}',   
                               SPECIAL_REGIST_PRICE.PRICE = {2} 
                        WHERE SPECIAL_REGIST_PRICE.CLINIC_TYPE = '{3}' AND SPECIAL_REGIST_PRICE.CHARGE_TYPE = '{4}'";
            object[] param = new object[] { special_regist_price.CLINIC_TYPE, 
                special_regist_price.CHARGE_TYPE, special_regist_price.PRICE, old_clinic_type, old_charge_type };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定特殊挂号费数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_type"></param>
        /// <param name="charge_type"></param>
        /// <returns></returns>
        public int DeleteSpecialRegistPrice(BaseEntityer db, string clinic_type, string charge_type)
        {
            string sql = @"delete from SPECIAL_REGIST_PRICE where SPECIAL_REGIST_PRICE.CLINIC_TYPE = '{0}' 
                            AND SPECIAL_REGIST_PRICE.CHARGE_TYPE = '{1}'";
            sql = string.Format(sql, clinic_type, charge_type);
            return db.ExecuteNonQuery(sql);
        }

        #endregion

        #region 门诊号表维护

        /// <summary>
        /// 获取星期号表模版
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public List<CLINIC_FOR_REGIST_TEMPLATES> GetTemplatesByWeek(string week)
        {
            string sql = @"SELECT CLINIC_FOR_REGIST_TEMPLATES.CLINIC_WEEK, 
                           CLINIC_FOR_REGIST_TEMPLATES.CLINIC_LABEL, 
                           CLINIC_FOR_REGIST_TEMPLATES.TIME_DESC, 
                           CLINIC_FOR_REGIST_TEMPLATES.REGISTRATION_LIMITS, 
                           CLINIC_FOR_REGIST_TEMPLATES.APPOINTMENT_LIMITS, 
                           CLINIC_FOR_REGIST_TEMPLATES.CURRENT_NO, 
                           CLINIC_FOR_REGIST_TEMPLATES.REGIST_PRICE 
                           FROM CLINIC_FOR_REGIST_TEMPLATES 
                           WHERE CLINIC_FOR_REGIST_TEMPLATES.CLINIC_WEEK = '{0}' 
                           ORDER BY CLINIC_FOR_REGIST_TEMPLATES.CLINIC_LABEL";
            sql = string.Format(sql, week);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_FOR_REGIST_TEMPLATES>(ds).ToList();
        }

        /// <summary>
        /// 获取时间段定义字典表信息
        /// </summary>
        /// <returns></returns>
        public List<TIME_INTERVAL_DICT> GetTIME_INTERVAL_DICTInfor()
        {
            string sql = @"select TIME_INTERVAL_DICT.Serial_No,
                           TIME_INTERVAL_DICT.TIME_INTERVAL_CODE,
                           TIME_INTERVAL_DICT.TIME_INTERVAL_NAME,
                           TIME_INTERVAL_DICT.Input_Code
                           from TIME_INTERVAL_DICT ORDER BY TIME_INTERVAL_DICT.Serial_No";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<TIME_INTERVAL_DICT>(ds).ToList();
        }

        /// <summary>
        /// 删除所有号表模版数据
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteTemplates(BaseEntityer db)
        {
            string sql = @"delete from CLINIC_FOR_REGIST_TEMPLATES";
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 新增号表模版数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_for_regist_template"></param>
        /// <returns></returns>
        public int InsertClinicForRegistTemplate(BaseEntityer db, CLINIC_FOR_REGIST_TEMPLATES clinic_for_regist_template)
        {
            string sql = @"INSERT INTO CLINIC_FOR_REGIST_TEMPLATES
                        (
                               CLINIC_FOR_REGIST_TEMPLATES.CLINIC_WEEK, 
                               CLINIC_FOR_REGIST_TEMPLATES.CLINIC_LABEL, 
                               CLINIC_FOR_REGIST_TEMPLATES.TIME_DESC, 
                               CLINIC_FOR_REGIST_TEMPLATES.REGISTRATION_LIMITS, 
                               CLINIC_FOR_REGIST_TEMPLATES.APPOINTMENT_LIMITS, 
                               CLINIC_FOR_REGIST_TEMPLATES.CURRENT_NO, 
                               CLINIC_FOR_REGIST_TEMPLATES.REGIST_PRICE
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}',{3},{4},{5},{6}
                        )";
            object[] param = new object[] { clinic_for_regist_template.CLINIC_WEEK, 
                clinic_for_regist_template.CLINIC_LABEL, clinic_for_regist_template.TIME_DESC, 
                clinic_for_regist_template.REGISTRATION_LIMITS, clinic_for_regist_template.APPOINTMENT_LIMITS, 
                clinic_for_regist_template.CURRENT_NO, clinic_for_regist_template.REGIST_PRICE };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定日期后号表数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_Label"></param>
        /// <returns></returns>
        public int DeleteClinicForRegistByClinicDate(BaseEntityer db, string clinic_date)
        {
            string sql = @"delete from CLINIC_FOR_REGIST 
                        where CLINIC_FOR_REGIST.CLINIC_DATE >= to_date('{0}','yyyy-mm-dd')";
            sql = string.Format(sql, clinic_date);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取门诊号表排班的最大日期
        /// </summary>
        /// <returns></returns>
        public DbDataReader GetMaxDateInClinicForRegist()
        {
            string sql = @"select max(CLINIC_FOR_REGIST.CLINIC_DATE) as CLINIC_DATE from CLINIC_FOR_REGIST";
            return BaseEntityer.Db.ExecuteReader(sql);
        }

        /// <summary>
        /// 插入号表记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_for_regist"></param>
        /// <returns></returns>
        public int InsertClinicForRegist(BaseEntityer db, CLINIC_FOR_REGIST clinic_for_regist)
        {
            string sql = @"INSERT INTO CLINIC_FOR_REGIST
                        (
                               CLINIC_FOR_REGIST.Clinic_Date,
                               CLINIC_FOR_REGIST.Clinic_Label,
                               CLINIC_FOR_REGIST.Time_Desc,
                               CLINIC_FOR_REGIST.Registration_Limits,
                               CLINIC_FOR_REGIST.Appointment_Limits,
                               CLINIC_FOR_REGIST.Current_No,
                               CLINIC_FOR_REGIST.Registration_Num,
                               CLINIC_FOR_REGIST.Appointment_Num,
                               CLINIC_FOR_REGIST.Regist_Price
                        )
                        VALUES
                        (
                               to_date('{0}','yyyy-mm-dd'),'{1}','{2}',{3},{4},{5},{6},{7},{8}
                        )";
            object[] param = new object[] { clinic_for_regist.CLINIC_DATE.ToShortDateString(), 
                clinic_for_regist.CLINIC_LABEL, clinic_for_regist.TIME_DESC, 
                clinic_for_regist.REGISTRATION_LIMITS, clinic_for_regist.APPOINTMENT_LIMITS, 
                clinic_for_regist.CURRENT_NO, clinic_for_regist.REGISTRATION_NUM, 
                clinic_for_regist.APPOINTMENT_NUM, clinic_for_regist.REGIST_PRICE };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        #endregion

        #endregion

        public static List<HisCommon.DataEntity.outp_industrial_injury> GetIndustrial(string dateStart, string dateEnd)
        {
            string sql = @"select  a.SEQ_NO ,a.STATE,a.CLINIC_NO,a.NAME,a.SEX,a.CARD_NO,
                                          a.BIRTHDAY,a.WORKERS,a.COMPCODE,a.COMPNAME,a.OCCURRDATE,
                                          a.IDENTDATE,a.APPRDATE,a.DISABLEVEL,a.APPRAISAL,a.DEPT,
                                          a.MANUAL,a.GRANTDATE,a.NURSELEVEL,a.IDENTITYCATEGORY,a.visit_date,a.visit_no
                                        from outp_industrial_injury a where 
                                        occurrdate  >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                                        and occurrdate  < to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.outp_industrial_injury>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public List<HisCommon.DataEntity.fin_invoice_use> GetInvoiceUseInfo(string dateStart, string dateEnd, string personId, List<string> invoiceType)
        {
            string sqlWhere = string.Empty;
            if (invoiceType != null && invoiceType.Count > 0)
            {
                for (int i = 0; i < invoiceType.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlWhere += " invoice_kind like '%" + invoiceType[i] + "%' ";
                    }
                    else
                    {
                        sqlWhere += " or invoice_kind like '%" + invoiceType[i] + "%' ";
                    }
                }
                sqlWhere = " and (" + sqlWhere + ")";
            }
            string sql = @"select 
 invoice_name,
 start_no,
 end_no,
 used_no,
 decode(used_state,'1','使用','0','未用','-1','已用') used_state,
 get_dtime,
 get_person_code,
 (select user_name  from users_staff_dict d where d.user_id = f.get_person_code and rownum = 1) get_person_name,
 invoice_kind   
 from fin_invoice_use f where 
 f.get_dtime  >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
 and f.get_dtime <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 and (f.get_person_code = '{2}' or 'ALL'= '{2}') {3}";
            sql = string.Format(sql, dateStart, dateEnd, personId, sqlWhere);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.fin_invoice_use>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public int GetIndustrialCount(string clinicNo, string visitNo, string visitDate)
        {
            string sql = @"select count(*) from outp_industrial_injury 
where clinic_no = '{0}' and visit_no = '{1}' and visit_date = to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')";
            sql = string.Format(sql, clinicNo, visitNo, visitDate);
            return int.Parse(BaseEntityer.Db.ExecuteScalar(sql).ToString());
        }

        /// <summary>
        /// 通过收款结算号读取挂号日结信息
        /// </summary>
        /// <param name="OutpAcctNo"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.REGIST_ACCT> GetRegistAcctByOutpAcctNo(string OutpAcctNo)
        {
            string sql = @"select * from regist_acct where OUTP_ACCT_NO='{0}'";
            sql = string.Format(sql, OutpAcctNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<REGIST_ACCT>(ds).ToList();
        }
        /// <summary>
        /// 查询结算的发票明细
        /// </summary>
        /// <param name="OutpAcctNo"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.fin_invoiceinfo_record> GetFinInvoiceinfoRecord(string OutpAcctNo)
        {
            string sql = @"select * from fin_invoiceinfo_record where DAYBALANCED_NO='{0}' and (INVOICE_KIND='00' or INVOICE_KIND='01')";
            sql = string.Format(sql, OutpAcctNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<fin_invoiceinfo_record>(ds).ToList();
        }

        /// <summary>
        /// 查询结算的发票明细
        /// </summary>
        /// <param name="OutpAcctNo"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.fin_invoiceinfo_record> QueryFinInvoiceinfoRecord(string invoicNew, string rcpt_no)
        {
            // 只查询正常和重打的记录
            string sql = @"SELECT *
  FROM fin_invoiceinfo_record
 WHERE (invoice_no = '{0}' OR rcpt_no = '{1}')
   AND invoice_state IN ('0', '2')
";
            sql = string.Format(sql, invoicNew, rcpt_no);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<fin_invoiceinfo_record>(ds).ToList();
        }

        public int UpdateIndustrial(BaseEntityer db, ref string err, HisCommon.DataEntity.outp_industrial_injury industrialObj)
        {
            int exec = 0;
            try
            {
                string sql = @"update outp_industrial_injury set 
STATE =  '{1}',
CLINIC_NO =  '{2}',
NAME =  '{3}',
SEX =  '{4}',
CARD_NO =  '{5}',
BIRTHDAY =  to_date('{6}', 'yyyy-mm-dd hh24:mi:ss'),
WORKERS =  '{7}',
COMPCODE =  '{8}',
COMPNAME =  '{9}',
OCCURRDATE =  to_date('{10}', 'yyyy-mm-dd hh24:mi:ss'),
IDENTDATE =  '{11}',
APPRDATE =  '{12}',
DISABLEVEL =  '{13}',
APPRAISAL =  '{14}',
DEPT =  '{15}',
MANUAL =  '{16}',
GRANTDATE =  to_date('{17}', 'yyyy-mm-dd hh24:mi:ss'),
NURSELEVEL =  '{18}',
IDENTITYCATEGORY =  '{19}',
VISIT_DATE =  to_date('{20}', 'yyyy-mm-dd hh24:mi:ss'),
VISIT_NO = '{21}'
where SEQ_NO = '{0}'";
                sql = string.Format(sql, GetParam(industrialObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                exec = -1;
            }
            return exec;
        }

        public int InsertIndustrial(BaseEntityer db, ref string err, HisCommon.DataEntity.outp_industrial_injury industrialObj)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into outp_industrial_injury
 (
SEQ_NO,
STATE,
CLINIC_NO,
NAME,
SEX,
CARD_NO,
BIRTHDAY,
WORKERS,
COMPCODE,
COMPNAME,
OCCURRDATE,
IDENTDATE,
APPRDATE,
DISABLEVEL,
APPRAISAL,
DEPT,
MANUAL,
GRANTDATE,
NURSELEVEL,
IDENTITYCATEGORY,
VISIT_DATE,
VISIT_NO
 )
 values
 (
 '{0}',
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 '{5}',
 to_date('{6}', 'yyyy-mm-dd hh24:mi:ss'),
 '{7}',
 '{8}',
 '{9}',
 to_date('{10}', 'yyyy-mm-dd hh24:mi:ss'),
 '{11}',
 '{12}',
 '{13}',
 '{14}',
 '{15}',
 '{16}',
 to_date('{17}', 'yyyy-mm-dd hh24:mi:ss'),
 '{18}',
 '{19}',
 to_date('{20}', 'yyyy-mm-dd hh24:mi:ss'),
 '{21}'
 )";
                sql = string.Format(sql, GetParam(industrialObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                exec = -1;
            }
            return exec;
        }

        private string[] GetParam(HisCommon.DataEntity.outp_industrial_injury industrialObj)
        {
            string[] para = new string[]{
                                        industrialObj.SEQ_NO,
                                        industrialObj.STATE,
                                        industrialObj.CLINIC_NO,
                                        industrialObj.NAME,
                                        industrialObj.SEX,
                                        industrialObj.CARD_NO,
                                        industrialObj.BIRTHDAY.ToString(),
                                        industrialObj.WORKERS,
                                        industrialObj.COMPCODE,
                                        industrialObj.COMPNAME,
                                        industrialObj.OCCURRDATE.ToString(),
                                        industrialObj.IDENTDATE,
                                        industrialObj.APPRDATE,
                                        industrialObj.DISABLEVEL,
                                        industrialObj.APPRAISAL,
                                        industrialObj.DEPT,
                                        industrialObj.MANUAL,
                                        industrialObj.GRANTDATE.ToString(),
                                        industrialObj.NURSELEVEL,
                                        industrialObj.IDENTITYCATEGORY,
                                        industrialObj.VISIT_DATE.ToString(),
                                        industrialObj.VISIT_NO.ToString(),
            };
            return para;
        }

        public int InsertInvoiceUse(BaseEntityer db, ref string err, HisCommon.DataEntity.fin_invoice_use invoiceObj)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into fin_invoice_use
 (
get_dtime, 
get_person_code, 
invoice_kind, 
invoice_name, 
start_no, 
end_no, 
used_no, 
used_state, 
is_pub, 
oper_code, 
oper_date, 
back_start_no, 
back_end_no
 )
 values
 (
 to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'),
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 '{5}',
 '{6}',
 '{7}',
 '{8}',
 '{9}',
 to_date('{10}', 'yyyy-mm-dd hh24:mi:ss'),
 '{11}',
 '{12}'
 )";
                sql = string.Format(sql, GetInvoiceUseParam(invoiceObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                exec = -1;
            }
            return exec;
        }

        private string[] GetInvoiceUseParam(HisCommon.DataEntity.fin_invoice_use invoiceObj)
        {
            string[] para = new string[]{
                                        invoiceObj.GET_DTIME.ToString(),
                                        invoiceObj.GET_PERSON_CODE,
                                        invoiceObj.INVOICE_KIND,
                                        invoiceObj.INVOICE_NAME,
                                        invoiceObj.START_NO,
                                        invoiceObj.END_NO,
                                        invoiceObj.USED_NO,
                                        invoiceObj.USED_STATE =="未用"?"0":(invoiceObj.USED_STATE =="使用"?"1":"-1"),
                                        invoiceObj.IS_PUB,
                                        invoiceObj.OPER_CODE,
                                        invoiceObj.OPER_DATE.ToString(),
                                        invoiceObj.BACK_START_NO,
                                        invoiceObj.BACK_END_NO
            };
            return para;
        }

        public int InsertInvoiceUseRecovery(BaseEntityer db, HisCommon.DataEntity.fin_invoice_use invoiceObj)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into INVOICE_USE_Recovery
 (
get_dtime, 
get_person_code, 
invoice_kind, 
invoice_name, 
start_no, 
end_no, 
is_pub, 
oper_code, 
oper_date
 )
 values
 (
 to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'),
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 '{5}',
 '{6}',
 '{7}',
 to_date('{8}', 'yyyy-mm-dd hh24:mi:ss')
 )";
                sql = string.Format(sql, GetInvoiceUseRecoveryParam(invoiceObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch
            {
                exec = -1;
            }
            return exec;
        }

        private string[] GetInvoiceUseRecoveryParam(HisCommon.DataEntity.fin_invoice_use invoiceObj)
        {
            string[] para = new string[]{
                                        invoiceObj.GET_DTIME.ToString(),
                                        invoiceObj.GET_PERSON_CODE,
                                        invoiceObj.INVOICE_KIND,
                                        invoiceObj.INVOICE_NAME,
                                        invoiceObj.START_NO,
                                        invoiceObj.END_NO,
                                        invoiceObj.IS_PUB,
                                        invoiceObj.OPER_CODE,
                                        invoiceObj.OPER_DATE.ToString()
            };
            return para;
        }

        public List<HisCommon.DataEntity.fin_invoice_use> GetInvoiceUseRecoveryInfo(string dateStart, string dateEnd)
        {
            string sql = @"select 
 invoice_name,
 start_no,
 end_no,
 get_person_code,
 (select user_name  from users_staff_dict d where d.user_id = f.get_person_code and rownum = 1) get_person_name,
 oper_date, 
 (select user_name  from users_staff_dict d where d.user_id = f.oper_code and rownum = 1) oper_code, 
 invoice_kind   
 from INVOICE_USE_Recovery f where 
 f.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
 and f.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 order by invoice_kind,to_number(start_no)";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.fin_invoice_use>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 删除工伤信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        public int DeleteIndustrial(BaseEntityer db, string seqNo)
        {
            string sql = @"delete from outp_industrial_injury oii where oii.seq_no = '{0}'";
            sql = string.Format(sql, seqNo);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.BringObject> getEmpInfo()
        {
            string sql = @"select user_id Id,user_name Name from users_staff_dict where is_cashier != '0' order by user_name";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.BringObject>(ds).ToList();
        }

        /// <summary>
        /// 得到门诊收款员
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.USERS_STAFF_DICT> getSFInfo()
        {
            string sql = @"select * from users_staff_dict where is_cashier != '0'";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.USERS_STAFF_DICT>(ds).ToList();
        }
        public string GetMaxInvoiceNo(List<string> invoiceType)
        {
            string sqlWhere = string.Empty;
            if (invoiceType != null && invoiceType.Count > 0)
            {
                for (int i = 0; i < invoiceType.Count; i++)
                {
                    if (i == 0)
                    {
                        sqlWhere += " invoice_kind like '%" + invoiceType[i] + "%' ";
                    }
                    else
                    {
                        sqlWhere += " or invoice_kind like '%" + invoiceType[i] + "%' ";
                    }
                }
                sqlWhere = " and (" + sqlWhere + ")";
            }
            string sql = @"select 
nvl(lpad(to_char(max(to_number(end_no))+1),12,'0'),lpad('1',12,'0')) end_no 
from fin_invoice_use 
where 'ALL' = 'ALL' {0}";
            sql = string.Format(sql, sqlWhere);
            object obj = BaseEntityer.Db.ExecuteScalar(sql);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public DataTable GetInvDictionary(string type)
        {
            string sql = @"select lpad(code,2,'0') cCode,name cName,mark cGroup from com_dictionary
                                        where type = '{0}' order by sort_id";
            sql = string.Format(sql, type);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        private string GetSqlWhere(string invoiceType, string dictType, string columName)
        {
            string sqlWhere = string.Empty;
            DataTable _dt = GetInvDictionary(dictType);
            DataRow[] _dr1 = _dt.Select(" cCode = '" + invoiceType.Trim() + "'");
            if (_dr1 != null && _dr1.Length > 0)
            {
                DataRow[] _dr = _dt.Select(" cGroup = '" + _dr1[0]["cGroup"].ToString() + "'");
                if (_dr != null && _dr.Length > 0)
                {
                    for (int i = 0; i < _dr.Length; i++)
                    {
                        if (i == 0)
                        {
                            sqlWhere += columName + " like '%C" + _dr[i]["cCode"].ToString() + "|%' ";
                        }
                        else
                        {
                            sqlWhere += " or " + columName + " like '%C" + _dr[i]["cCode"].ToString() + "|%' ";
                        }
                    }
                    sqlWhere = " and (" + sqlWhere + ")";
                }
            }
            return sqlWhere;
        }

        public string GetInvoiceAboutNo(string operId, HisCommon.Enum.InvoiceKind invoiceType)
        {
            string sqlWhere = GetSqlWhere(((int)invoiceType).ToString().PadLeft(2, '0'), "InvoiceType", "invoice_kind");
            if (string.IsNullOrEmpty(sqlWhere))
            {
                return sqlWhere;
            }
            string sql = @"select start_no||'|'||end_no||'|'||nvl(used_no,'start_no')||'|'||used_state invoiceNo 
  from fin_invoice_use where get_person_code = '{0}' {1} and used_state != '-1' 
  and lpad(start_no,12,'0') = (select lpad(to_char(min(to_number(start_no))),12,'0') from fin_invoice_use where 
  get_person_code = '{0}' {1} and used_state != '-1' )";
            sql = string.Format(sql, operId, sqlWhere);
            object obj = BaseEntityer.Db.ExecuteScalar(sql);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetInvoiceNo(string operId, HisCommon.Enum.InvoiceKind invoiceType)
        {
            string sqlWhere = "C" + ((int)invoiceType).ToString().PadLeft(2, '0') + "|";
            string sql = @"select 
  case when (to_number(used_no)+1)>to_number(end_no) then 
  'beyond' when used_no is null then lpad(start_no,12,'0')
  else lpad(to_char(to_number(used_no)+1),12,'0') end invoiceNo 
  from fin_invoice_use where get_person_code = '{0}' and invoice_kind like '%{1}%' and used_state = '1' 
  and lpad(start_no,12,'0') = (select lpad(to_char(min(to_number(start_no))),12,'0') from fin_invoice_use where 
  get_person_code = '{0}' and invoice_kind like '%{1}%' and used_state = '1' )";
            sql = string.Format(sql, operId, sqlWhere);
            object obj = BaseEntityer.Db.ExecuteScalar(sql);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                sql = @"select 
  case when (to_number(used_no)+1)>to_number(end_no) then 
  'beyond' when used_no is null then lpad(start_no,12,'0')
  else lpad(to_char(to_number(used_no)+1),12,'0') end invoiceNo 
  from fin_invoice_use where get_person_code = '{0}' and invoice_kind like '%{1}%' and used_state = '0' 
  and lpad(start_no,12,'0') = (select lpad(to_char(min(to_number(start_no))),12,'0') from fin_invoice_use where 
  get_person_code = '{0}' and invoice_kind like '%{1}%' and used_state = '0' )";
                sql = string.Format(sql, operId, sqlWhere);
                object newObj = BaseEntityer.Db.ExecuteScalar(sql);
                if (newObj != null)
                {
                    return newObj.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public int UpdateInvoiceUse(BaseEntityer db, string InvoiceNo, string operId, HisCommon.Enum.InvoiceKind invoiceType, string mode, ref string mess)
        {
            int exec = 0;
            try
            {
                string sqlWhere = "C" + ((int)invoiceType).ToString().PadLeft(2, '0') + "|";
                string sqlExec = @" select 
 start_no,
 end_no,
 nvl(used_no,start_no) used_no,
 used_state  
 from fin_invoice_use f 
 where f.get_person_code = '{0}' 
 and f.invoice_kind like '%{1}%'
 and f.used_state = '{2}'";
                string sqlUsed = string.Format(sqlExec, operId, sqlWhere, "1");
                List<HisCommon.DataEntity.fin_invoice_use> invoiceList =
                DataSetToEntity.DataSetToT<HisCommon.DataEntity.fin_invoice_use>(BaseEntityer.Db.GetDataSet(sqlUsed)).ToList();
                if (invoiceList == null || invoiceList.Count <= 0)
                {
                    sqlUsed = string.Format(sqlExec, operId, sqlWhere, "0");
                    invoiceList =
                        DataSetToEntity.DataSetToT<HisCommon.DataEntity.fin_invoice_use>(BaseEntityer.Db.GetDataSet(sqlUsed)).ToList();
                    if (invoiceList == null || invoiceList.Count <= 0)
                    {
                        mess = "更新领用发票信息时出现异常!";
                        return -1;
                    }
                }
                decimal startNum = 0;
                decimal endNum = 0;
                decimal usedNum = 0;
                decimal InvoiceValue = decimal.Parse(InvoiceNo);
                bool isThere = false;
                List<string> skipList = new List<string>();
                foreach (HisCommon.DataEntity.fin_invoice_use obj in invoiceList)
                {
                    startNum = decimal.Parse(obj.START_NO);
                    endNum = decimal.Parse(obj.END_NO);
                    usedNum = decimal.Parse(obj.USED_NO);
                    if (InvoiceValue >= startNum && InvoiceValue <= endNum)
                    {
                        if (obj.USED_STATE.Equals("1") && (InvoiceValue >= startNum && InvoiceValue <= usedNum))
                        {
                            mess = "该发票号已被使用，请重新调整!";
                            return -1;
                        }
                        if (mode == "noFee")
                        {
                            int firstNum = int.Parse(usedNum.ToString());
                            decimal usedNumCopy = (usedNum - 1);
                            if (obj.USED_STATE.Equals("1"))
                            {
                                firstNum = int.Parse((usedNum + 1).ToString());
                                usedNumCopy = usedNum;
                            }
                            int skipCount = int.Parse((InvoiceValue - usedNumCopy).ToString());
                            if (skipCount >= 1 && skipCount <= 5)
                            {
                                for (int i = firstNum; i <= int.Parse(InvoiceValue.ToString()); i++)
                                {
                                    skipList.Add(i.ToString().PadLeft(12, '0'));
                                }
                            }
                            else
                            {
                                mess = "跳号数不能超出5张";
                                return -1;
                            }
                        }
                        isThere = true;
                        break;
                    }
                    else
                    {
                        if (obj.USED_STATE.Equals("1"))
                        {
                            mess = "该发票号不在当前使用票段范围之内，请重新调整!";
                            return -1;
                        }
                    }
                }

                if (!isThere)
                {
                    mess = "该发票号不在所领取的票段范围之内，请重新调整!";
                    return -1;
                }

                string sql = @"update fin_invoice_use 
set used_no = lpad('{0}',12,'0'),used_state = case when lpad(start_no,12,'0')=lpad('{0}',12,'0') then '1' when lpad(end_no,12,'0')=lpad('{0}',12,'0') then '-1' else '1' end
where get_person_code = '{1}' and invoice_kind like '%{2}%' and lpad(start_no,12,'0') = lpad('{3}',12,'0') and lpad(end_no,12,'0')=lpad('{4}',12,'0')";
                sql = string.Format(sql, InvoiceNo, operId, sqlWhere, startNum.ToString().PadLeft(12, '0'), endNum.ToString().PadLeft(12, '0'));
                exec = db.ExecuteNonQuery(sql);
                if (exec < 0)
                {
                    mess = "更新领用发票信息时出现异常!";
                    return exec;
                }
                if (skipList != null && skipList.Count > 0)
                {
                    foreach (string skipId in skipList)
                    {
                        HisCommon.DataEntity.fin_invoiceinfo_record record = SetInvoiceinfoRecord(skipId.PadLeft(12, '0'), operId, ((int)invoiceType).ToString().PadLeft(2, '0'));
                        exec = InsertFinInvoiceinfoRecord(record, db);
                        if (exec < 0)
                        {
                            mess = "更新领用发票信息时出现异常!";
                            return exec;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                mess = e.Message;
                exec = -1;
            }
            return exec;
        }

        private HisCommon.DataEntity.fin_invoiceinfo_record SetInvoiceinfoRecord(string InvoiceNo, string operId, string invoiceType)
        {
            HisCommon.DataEntity.fin_invoiceinfo_record record = new HisCommon.DataEntity.fin_invoiceinfo_record();
            record.INVOICE_NO = InvoiceNo.PadLeft(12, '0');
            record.TRANS_TYPE = "1";
            record.TOT_COST = 0;
            record.PUB_COST = 0;
            record.OWN_COST = 0;
            record.PAY_COST = 0;
            record.INVOICE_SEQ = "1";
            record.REB_COST = 0;
            record.ECO_COST = 0;
            record.FEE_OPER_CODE = operId;
            record.FEE_OPER_DATE = HisDBLayer.Common.getServerTime();
            record.INVOICE_STATE = "3";
            record.BACKFEE_OPER_CODE = string.Empty;
            //record.	BACKFEE_OPER_DATE=
            record.BACKFEE_INVOICE_NO = string.Empty;
            record.BACKFEE_INVOICE_SEQ = string.Empty;
            record.REPRINT_OPER_CODE = string.Empty;
            //record.	REPRINT_OPER_DATE=
            record.INVOICE_KIND = invoiceType;
            record.DAYBALANCED_FLAG = "0";
            record.DAYBALANCED_OPER_CODE = string.Empty;
            record.DAYBALANCED_NO = string.Empty;
            record.VALID_FLAG = "1";
            record.PACT_CODE = "1";
            record.PACT_NAME = "普通";
            record.REPRINT_INVOICE_NO = string.Empty;
            record.REPRINT_INVOICE_SEQ = string.Empty;
            return record;
        }

        public int DeleteInvoiceUse(BaseEntityer db, string getTime, string getCode)
        {
            string sql = @"delete from fin_invoice_use fiu where get_dtime = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
  and get_person_code = '{1}' and used_state = '0'";
            sql = string.Format(sql, getTime, getCode);
            return db.ExecuteNonQuery(sql);
        }

        public int UpdateInvoiceUseByCode(BaseEntityer db, string getTime, string getCode)
        {
            string sql = @"update fin_invoice_use set used_state = '-1',back_start_no=lpad(to_char(to_number(used_no)+1),12,'0'),
  back_end_no = end_no,end_no=used_no where get_dtime = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
  and get_person_code = '{1}' and used_state = '1'";
            sql = string.Format(sql, getTime, getCode);
            return db.ExecuteNonQuery(sql);
        }

        public int InsertFinInvoiceinfoRecord(HisCommon.DataEntity.fin_invoiceinfo_record record, BaseEntityer db)
        {
            string sql = @"insert into fin_invoiceinfo_record
                 (
                    INVOICE_NO	,
                    TRANS_TYPE	,
                    TOT_COST	,
                    PUB_COST	,
                    OWN_COST	,
                    PAY_COST	,
                    INVOICE_SEQ	,
                    REB_COST	,
                    ECO_COST	,
                    FEE_OPER_CODE	,
                    FEE_OPER_DATE	,
                    INVOICE_STATE	,
                    BACKFEE_OPER_CODE	,
                    BACKFEE_OPER_DATE	,
                    BACKFEE_INVOICE_NO	,
                    BACKFEE_INVOICE_SEQ	,
                    REPRINT_OPER_CODE	,
                    REPRINT_OPER_DATE	,
                    INVOICE_KIND	,
                    DAYBALANCED_FLAG	,
                    DAYBALANCED_OPER_CODE	,
                    DAYBALANCED_NO	,
                    VALID_FLAG	,
                    PACT_CODE	,
                    PACT_NAME	,
                    REPRINT_INVOICE_NO	,
                    REPRINT_INVOICE_SEQ	,
                    RCPT_NO
                 )
                 values
                 (
                 '{0}',
                 '{1}',
                 '{2}',
                 '{3}',
                 '{4}',
                 '{5}',
                 '{6}',
                 '{7}',
                 '{8}',
                 '{9}',
                 to_date('{10}', 'yyyy-mm-dd hh24:mi:ss'),
                 '{11}',
                 '{12}',
                to_date('{13}', 'yyyy-mm-dd hh24:mi:ss'),
                 '{14}',
                 '{15}',
                 '{16}',
                 to_date('{17}', 'yyyy-mm-dd hh24:mi:ss'),
                 '{18}',
                 '{19}',
                  '{20}',
                 '{21}','{22}','{23}','{24}','{25}','{26}','{27}'
                 )";
            object[] param = new object[] { 
                record. INVOICE_NO	,
                record.	TRANS_TYPE	,
                record.	TOT_COST	,
                record.	PUB_COST	,
                record.	OWN_COST	,
                record.	PAY_COST	,
                record.	INVOICE_SEQ	,
                record.	REB_COST	,
                record.	ECO_COST	,
                record.	FEE_OPER_CODE	,
                record.	FEE_OPER_DATE	,
                record.	INVOICE_STATE	,
                record.	BACKFEE_OPER_CODE	,
                record.	BACKFEE_OPER_DATE	,
                record.	BACKFEE_INVOICE_NO	,
                record.	BACKFEE_INVOICE_SEQ	,
                record.	REPRINT_OPER_CODE	,
                record.	REPRINT_OPER_DATE	,
                record.	INVOICE_KIND	,
                record.	DAYBALANCED_FLAG	,
                record.	DAYBALANCED_OPER_CODE	,
                record.	DAYBALANCED_NO	,
                record.	VALID_FLAG	,
                record.	PACT_CODE	,
                record.	PACT_NAME	,
                record.	REPRINT_INVOICE_NO	,
                record.	REPRINT_INVOICE_SEQ	,
                record. RCPT_NO
                };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新发票总金额信息，发票打印，更新第一次插入的金额
        /// 原因，第一次插入的是总金额，
        /// </summary>
        /// <param name="totalCost"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateFinInvoiceTotalInfo(decimal totalCost, string invoiceNO, string rcptNO, string operCode, DateTime operDate, BaseEntityer db, ref string errMsg)
        {
            try
            {
                string sql = @"UPDATE fin_invoiceinfo_record t --发票结算记录表
   SET t.fee_oper_date = to_date('{3}', 'YYYY-MM-DD HH24:MI:SS'), --结算时间
       t.fee_oper_code = '{2}', --结算员
       t.tot_cost      = '{1}', --总金额
       t.rcpt_no='{4}'-- 收费单据号
 WHERE t.trans_type = '1'  and  --交易类别 收费（1）退费（2）正交易1 负交易2
 t.invoice_no = '{0}' --发票号
 ";
                sql = string.Format(sql, invoiceNO, totalCost.ToString(), operCode, operDate.ToString(), rcptNO);
                return db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return -1;
            }
        }


        /// <summary>
        /// 获得发票信息
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="tranType"></param>
        /// <param name="invoiceKind"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<fin_invoiceinfo_record> QueryFinInvoiceInfoByNO(string invoiceNO, string tranType, string invoiceKind, ref string errMsg)
        {
            try
            {
                string sql = @"SELECT *
  FROM fin_invoiceinfo_record --发票结算记录表
 WHERE invoice_kind = '{0}'
   AND trans_type = '{1}'
   AND invoice_no = '{2}'
 ";
                sql = string.Format(sql, invoiceKind.PadLeft(2, '0'), tranType, invoiceNO);
                return DataSetToEntity.DataSetToT<HisCommon.DataEntity.fin_invoiceinfo_record>(BaseEntityer.Db.GetDataSet(sql)).ToList();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        public int UpdateFinInvoiceinfoRecordByRPrint(HisCommon.DataEntity.fin_invoiceinfo_record record, BaseEntityer db)
        {
            string sql = @"update  fin_invoiceinfo_record  set INVOICE_STATE='4' where (INVOICE_NO='{0}' or REPRINT_INVOICE_NO='{0}' or RCPT_NO='{1}') and  INVOICE_STATE<>'4'";
            object[] param = new object[] {record.REPRINT_INVOICE_NO,record.RCPT_NO
                 };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        ///  更新发票记录表
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="rcptNO"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public  int UpdateInpSettleMasterInvoiceNOByRcptNo(string invoiceNO,string rcptNO, BaseEntityer db)
        {
            string sql = @"UPDATE inp_settle_master t SET t.invoice = '{0}' WHERE t.rcpt_no = '{1}'";
            object[] param = new object[] {invoiceNO,rcptNO
                 };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 插入跳号的记录表
        /// </summary>
        /// <param name="record"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertINVOICE_CHANGE_INFO(HisCommon.DataEntity.INVOICE_CHANGE_INFO record, BaseEntityer db)
        {
            string sql = @"insert into INVOICE_CHANGE_INFO
                 (
                    ID	,
                    OUPER_DATE	,
                    START_NO	,
                    END_NO	,
                    FEE_PERSON	,
                    OUPER_NO	,
                    REMARK,
                    TYPE
                 )
                 values
                 (
                 '{0}',
                  to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'),
                 '{2}',
                 '{3}',
                 '{4}',
                 '{5}',
                 '{6}',
                 '{7}'
                 )";
            object[] param = new object[] { 
                
                record.	ID	,
                record.	OUPER_DATE	,
                record.	START_NO	,
                record.	END_NO	,
                record.	FEE_PERSON	,
                record.	OUPER_NO	,
                record.	REMARK,
                record.TYPE
                };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 得到发票跳号的历史记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetInvoiceNext()
        {
            string sql;
            sql = "select * from INVOICE_CHANGE_INFO order by OUPER_DATE desc";
            sql = string.Format(sql);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public int UpdateFinInvoiceinfoRecord(HisCommon.DataEntity.fin_invoiceinfo_record record, BaseEntityer db)
        {
            string sql = @" update fin_invoiceinfo_record set DAYBALANCED_FLAG='1' ,DAYBALANCED_NO='{0}',DAYBALANCED_OPER_CODE='{1}'
                           where INVOICE_NO='{2}' and TRANS_TYPE='{3}' and INVOICE_SEQ='{4}' and INVOICE_KIND='{5}'";
            object[] param = new object[] { 
                record.DAYBALANCED_NO	,
                record.	DAYBALANCED_OPER_CODE	,
                record.	INVOICE_NO	,
                record.	TRANS_TYPE	,
                record.	INVOICE_SEQ,
                record.INVOICE_KIND
                };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

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
        /// <summary>
        /// 更新收据主表的发票数量
        /// </summary>
        /// <param name="rcpt_no"></param>
        /// <param name="pageCount"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateOutpRcptMasterPageCount(string rcpt_no, string pageCount, BaseEntityer db)
        {
            string sql = @"update  OUTP_RCPT_MASTER  set PAGECOUNT='{0}' where rcpt_no='{1}' ";
            object[] param = new object[] { pageCount, rcpt_no };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);

        }

    
        public List<HisCommon.ClinicFeeDayReport> GetShipingClinicFeeDayReport(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @" select operator_no OperCode,
                           (select u.user_name
                              from users_staff_dict u
                             where u.user_id = m.operator_no) OperName,
                           (select nvl(u.is_nh, 0)
                              from users_staff_dict u
                             where u.user_id = m.operator_no) IsNo,
                           sum(skxj) skxj,
                           sum(tkxj) tkxj,
                           sum(yljz) yljz,
                           sum(ynjz) ynjz,
                           sum(yltk) yltk,
                           sum(yntk) yntk,
                           sum(jzje) jzje,
                           sum(tjje) tjje,
                           sum(ghje) ghje,
                           sum(thje) thje,
                           sum(ghjz) ghjz,
                           sum(gtjz) gtjz,
                           sum(ylgjz) ylgjz,
                           sum(yngjz) yngjz,
                           sum(ylgtz) ylgtz,
                           sum(yngtz) yngtz,
                           sum(ycxj) ycxj,
                           sum(ycyh) ycyh,
                           sum(grzh) grzh,
                           sum(tczf) tczf,
                           sum(czje) czje
                      from (select am.operator_no,
                                   sum(case
                                         when payment_no = '7' and payment_amount >= 0 then
                                          payment_amount
                                         else
                                          0
                                       end) skxj,
                                   sum(case
                                         when payment_no = '7' and payment_amount < 0 then
                                          payment_amount
                                         else
                                          0
                                       end) tkxj,
                                   sum(case
                                         when payment_no != '7' and payment_amount >= 0 and
                                              (select card_type
                                                 from onecard_patientinfo s
                                                where s.out_patientid = rm.patient_id
                                                  and rownum = 1) = '3' then
                                          payment_amount
                                         else
                                          0
                                       end) yljz,
                                   sum(case
                                         when payment_no != '7' and payment_amount >= 0 and
                                              (select card_type
                                                 from onecard_patientinfo s
                                                where s.out_patientid = rm.patient_id
                                                  and rownum = 1) = '1' then
                                          payment_amount
                                         else
                                          0
                                       end) ynjz,
                                   sum(case
                                         when payment_no != '7' and payment_amount < 0 and
                                              (select card_type
                                                 from onecard_patientinfo s
                                                where s.out_patientid = rm.patient_id
                                                  and rownum = 1) = '3' then
                                          payment_amount
                                         else
                                          0
                                       end) yltk,
                                   sum(case
                                         when payment_no != '7' and payment_amount < 0 and
                                              (select card_type
                                                 from onecard_patientinfo s
                                                where s.out_patientid = rm.patient_id
                                                  and rownum = 1) = '1' then
                                          payment_amount
                                         else
                                          0
                                       end) yntk,
                                   sum(case
                                         when payment_no != '7' and payment_amount >= 0 then
                                          payment_amount
                                         else
                                          0
                                       end) jzje,
                                   sum(case
                                         when payment_no != '7' and payment_amount < 0 then
                                          payment_amount
                                         else
                                          0
                                       end) tjje,
                                   0 ghje,
                                   0 thje,
                                   0 ghjz,
                                   0 gtjz,
                                   0 yngjz,
                                   0 ylgjz,
                                   0 yngtz,
                                   0 ylgtz,
                                   0 ycxj,
                                   0 ycyh,
                                   sum(case
                                         when payment_no = '2' then
                                          payment_amount
                                         else
                                          0
                                       end) grzh,
                                   sum(case
                                         when payment_no = '4' then
                                          payment_amount
                                         else
                                          0
                                       end) tczf,
 ( SELECT nvl(sum(t.recharge_cost),0) FROM onecard_payment t
            WHERE t.oper_id = am.operator_no and t.RECHARGE_CLASS ='3'
            AND t.RECHARGE_DATE>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
            AND  t.RECHARGE_DATE <=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
           ) czje
                              from OUTP_ACCT_MASTER    am,
                                   OUTP_RCPT_MASTER    rm,
                                   OUTP_PAYMENTS_MONEY pm
                             where am.acct_no = rm.acct_no
                               and rm.rcpt_no = pm.rcpt_no
                               and (am.acct_date > to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') and
                                   am.acct_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss'))
                             group by am.operator_no
                            union all
                            select am.operator_no,
                                   0 skxj,
                                   0 tkxj,
                                   0 yljz,
                                   0 ynjz,
                                   0 yltk,
                                   0 yntk,
                                   0 jzje,
                                   0 tjje,
                                   sum(ra.refund_amount + ra.total_amount - ra.total_card_amount -
                                       ra.refund_card_amount) ghje,
                                   sum(ra.refund_amount - ra.refund_card_amount) thje,
                                   sum(ra.total_card_amount + ra.refund_card_amount) ghjz,
                                   sum(ra.refund_card_amount) gtjz,
                                   (select nvl(sum(t.clinic_charge), 0)
                                      from clinic_master t, onecard_patientinfo m
                                     where t.patient_id = m.out_patientid
                                       and t.registering_date between
                                           to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') and
                                           to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                                       and m.card_type = '1'
                                       and t.operator = am.operator_no) yngjz,
                                   (select nvl(sum(t.clinic_charge), 0)
                                      from clinic_master t, onecard_patientinfo m
                                     where t.patient_id = m.out_patientid
                                       and t.registering_date between
                                           to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') and
                                           to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                                       and m.card_type = '3'
                                       and t.operator = am.operator_no) ylgjz,
                                   (select nvl(sum(-t.clinic_charge), 0)
                                      from clinic_master t, onecard_patientinfo m
                                     where t.patient_id = m.out_patientid
                                       and t.registering_date between
                                           to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') and
                                           to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                                       and m.card_type = '1'
                                       and t.returned_acct_flag = '1'
                                       and t.returned_operator = am.operator_no) yngtz,
                                   (select nvl(sum(-t.clinic_charge), 0)
                                      from clinic_master t, onecard_patientinfo m
                                     where t.patient_id = m.out_patientid
                                       and t.registering_date between
                                           to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') and
                                           to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                                       and m.card_type = '3'
                                       and t.returned_acct_flag = '1'
                                       and t.returned_operator = am.operator_no) ylgtz,
                                   0 ycxj,
                                   0 ycyh,
                                   0 grzh,
                                   0 tczf,
                                   0 czje
                              from OUTP_ACCT_MASTER am, regist_acct ra
                             where am.acct_no = ra.outp_acct_no
                               and (am.acct_date > to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') and
                                   am.acct_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss'))
                             group by am.operator_no
                            union all
                            select am.operator_no,
                                   0 skxj,
                                   0 tkxj,
                                   0 yljz,
                                   0 ynjz,
                                   0 yltk,
                                   0 yntk,
                                   0 jzje,
                                   0 tjje,
                                   0 ghje,
                                   0 thje,
                                   0 ghjz,
                                   0 gtjz,
                                   0 yngjz,
                                   0 ylgjz,
                                   0 yngtz,
                                   0 ylgtz,
                                   sum(am.recharge_money_cost) ycxj,
                                   sum(am.recharge_card_cost) ycyh,
                                   0 grzh,
                                   0 tczf,
                                   0 czje
                              from OUTP_ACCT_MASTER am
                             where (am.acct_date > to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') and
                                   am.acct_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss'))
                             group by am.operator_no) m
                     group by operator_no ";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.ClinicFeeDayReport>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 维康医院日结汇总
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public WKAllOutpDayBlance GetWKClinicFeeDayReport(string dateStart, string dateEnd)
        {
            WKAllOutpDayBlance wkAllOutpDayBlance = new WKAllOutpDayBlance();

            string sql = string.Empty;
            //--门诊日结表 1
            sql = @"select　*　 from  OUTP_ACCT_MASTER am
                where  (am.acct_date >to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                and am.acct_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss'))";
            sql = string.Format(sql, dateStart, dateEnd);
            wkAllOutpDayBlance.AcctInfoList = DataSetToEntity.DataSetToT<OUTP_ACCT_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
            //--日结的费用分类明细2
            sql = @"select d.*,s.subj_name from outp_acct_detail d  left join TALLY_SUBJECT_DICT s 
                                 on d.tally_fee_class=s.subj_code where d.acct_no in(select am.acct_no  from  OUTP_ACCT_MASTER am
                   where  (am.acct_date >to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                   and am.acct_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss')))";
            sql = string.Format(sql, dateStart, dateEnd);
            wkAllOutpDayBlance.ListOutp_acct_detail = DataSetToEntity.DataSetToT<OUTP_ACCT_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
            //---结账支付明细3
            sql = @"select * from outp_acct_money where acct_no in(select am.acct_no  from  OUTP_ACCT_MASTER am
            where  (am.acct_date >to_date('{0}','yyyy-MM-dd hh24:mi:ss')
            and am.acct_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss')))";
            sql = string.Format(sql, dateStart, dateEnd);
            wkAllOutpDayBlance.ListOut_acct_moneys = DataSetToEntity.DataSetToT<OUTP_ACCT_MONEY>(BaseEntityer.Db.GetDataSet(sql)).ToList();

            //---收费结算明细表 4
            //            sql = @"select  *  from  OUTP_RCPT_MASTER rm where rm.acct_no in
            //                (select am.acct_no  from  OUTP_ACCT_MASTER am
            //                where  (am.acct_date >to_date('{0}','yyyy-MM-dd hh24:mi:ss')
            //                and am.acct_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss')))";
            //            sql = string.Format(sql, dateStart, dateEnd);
            //            wkAllOutpDayBlance. = DataSetToEntity.DataSetToT<OUTP_ACCT_MONEY>(BaseEntityer.Db.GetDataSet(sql)).ToList();
            //--医保明细5
            sql = @"select * from siinfo s where s.invoice_no in(select  rm.rcpt_no  from  OUTP_RCPT_MASTER rm where rm.acct_no in
                (select am.acct_no  from  OUTP_ACCT_MASTER am
                where  (am.acct_date >to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                and am.acct_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss'))))
                and s.type_code='1'";
            sql = string.Format(sql, dateStart, dateEnd);
            wkAllOutpDayBlance.ListSiinfo = DataSetToEntity.DataSetToT<SIInfo>(BaseEntityer.Db.GetDataSet(sql)).ToList();

            //收费明细6
            sql = @"select * from OUTP_BILL_ITEMS i where i.rcpt_no in (select  rm.rcpt_no  from  OUTP_RCPT_MASTER rm where rm.acct_no in
                (select am.acct_no  from  OUTP_ACCT_MASTER am
                where  (am.acct_date >to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                and am.acct_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss'))))";
            sql = string.Format(sql, dateStart, dateEnd);
            wkAllOutpDayBlance.ListOutpBillItems = DataSetToEntity.DataSetToT<OUTP_BILL_ITEMS>(BaseEntityer.Db.GetDataSet(sql)).ToList();

            //发票数据7
            sql = @"select * from fin_invoiceinfo_record f where f.DAYBALANCED_NO in(select am.acct_no  from  OUTP_ACCT_MASTER am
                    where  (am.acct_date >to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    and am.acct_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss'))) and  (f.INVOICE_KIND='00' or f.INVOICE_KIND='01')";
            sql = string.Format(sql, dateStart, dateEnd);
            wkAllOutpDayBlance.ListInvoice = DataSetToEntity.DataSetToT<fin_invoiceinfo_record>(BaseEntityer.Db.GetDataSet(sql)).ToList();

            //挂号日结
            sql = @"select * from REGIST_ACCT r where r.outp_acct_no in(select am.acct_no  from  OUTP_ACCT_MASTER am
                    where  (am.acct_date >to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    and am.acct_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss')))";
            sql = string.Format(sql, dateStart, dateEnd);
            wkAllOutpDayBlance.ListRegist_acct = DataSetToEntity.DataSetToT<REGIST_ACCT>(BaseEntityer.Db.GetDataSet(sql)).ToList();

            return wkAllOutpDayBlance;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ALL_ACCT_MASTER> GetMaxALLAcctMaster()
        {
            string sql = string.Empty;
            sql = @" SELECT * from ALL_ACCT_MASTER where acct_no=( SELECT Max (acct_no)from ALL_ACCT_MASTER) ";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ALL_ACCT_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public List<HisCommon.DataEntity.ALL_ACCT_MASTER> GetALLAcctMaster()
        {
            string sql = string.Empty;
            sql = @" SELECT * from ALL_ACCT_MASTER order by acct_no desc";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ALL_ACCT_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 通过时间查询历史日结汇总信息
        /// </summary>
        /// <param name="beginTime">日结开始时间</param>
        /// <param name="endTime">日结结束时间</param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ALL_ACCT_MASTER> GetALLRecordAcctMaster(string beginTime, string endTime, string UserID)
        {
            string sql = string.Empty;
            sql = @" SELECT *
                    from ALL_ACCT_MASTER t
                    where t.acct_date > to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                    and t.acct_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                    and ( t.operator_no='{2}' or 'ALL'='{2}')                
                    order by acct_no desc";
            sql = string.Format(sql, beginTime, endTime, UserID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ALL_ACCT_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }


        public int InsertALLAcctMaster(HisCommon.DataEntity.ALL_ACCT_MASTER record, BaseEntityer db)
        {
            string sql = @"insert into ALL_ACCT_MASTER
                 (
                    ACCT_NO,
                    OPERATOR_NO,
                    ACCT_DATE,
                    ACCT_START_DATE,
                    ACCT_END_DATE)
                 values
                 (
                 '{0}',
                 '{1}',
                   to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'),
                   to_date('{3}', 'yyyy-mm-dd hh24:mi:ss'),
                  to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')
                 )";
            object[] param = new object[] { 
               record.ACCT_NO,
               record.OPERATOR_NO,
               record.ACCT_DATE,
               record.ACCT_START_DATE,
               record.ACCT_END_DATE
            };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }




        /// <summary>
        /// 2013-9-26 by li 获取科室门诊收入统计表 数据视图
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.VIEW_V_MZ> GetViewVMz(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"SELECT *
                      from v_mz
                     where acct_date > to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                       and acct_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.VIEW_V_MZ>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 2013-12-23 by li 获取科室门诊收入统计表 数据视图，按收费时间统计
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.VIEW_V_MZ> GetViewVMzCanChange(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"SELECT *
                      from v_mz
                     where visit_date > to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                       and visit_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.VIEW_V_MZ>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 2014-2-20 by li 获取门诊收费时时费用明细视图数据，按收费时间统计
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.VIEW_V_MZ> GetViewVMzConstantly(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"SELECT *
                      from v_mz_constantly
                     where visit_date > to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                       and visit_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.VIEW_V_MZ>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 2013-9-28 by li 获取科室门诊挂号数据
        /// </summary>
        /// <returns></returns>
        public List<CLINIC_MASTER> GetClinicRegistData(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select *
                      from clinic_master c
                     where c.acct_no in
                           (select r.acct_no
                              from REGIST_ACCT r
                             where r.outp_acct_no in
                                   (select o.acct_no
                                      from outp_acct_master o
                                     where o.acct_date >
                                           to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                       AND o.acct_date <=
                                           to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')))";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 2013-9-28 by li 获取科室门诊挂号退号数据
        /// </summary>
        /// <returns></returns>
        public List<CLINIC_MASTER> GetClinicRegistReturnData(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select *
                      from clinic_master c
                     where c.returned_acct_no in
                           (select r.acct_no
                              from REGIST_ACCT r
                             where r.outp_acct_no in
                                   (select o.acct_no
                                      from outp_acct_master o
                                     where o.acct_date >
                                           to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                       AND o.acct_date <=
                                           to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')))";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 获取门诊医嘱数据
        /// </summary>
        /// <param name="visitNo"></param>
        /// <param name="visitDate"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OUTP_PATIENT_ORDERS> GetClinicOutpPatientOrders(string visitNo, string visitDate, HisCommon.Enum.ChargeState chargeState)
        {
            string state = "0";
            if (chargeState == HisCommon.Enum.ChargeState.无效)
            {
                state = "-1";
            }
            string sql = @"select 
visit_date, 
visit_no, 
serial_no, 
item_no, 
class item_class,
decode(class,'A','药品','B','药品','非药品') item_class_name, 
code item_code, 
name item_name, 
drug_spec, 
firm_id, 
packge_units, 
amount, 
dosage, 
dosage_units, 
administration, 
frequency, 
provided_indicator, 
costs, 
charge_indicator, 
deptcode dept_code, 
dept_name, 
price, 
zb, 
fs, 
batchno, 
min_units, 
oper_date, 
ts, 
dcsl, 
order_doctor, 
(select s.user_name from users_staff_dict s where s.user_id = pOrders.order_doctor and rownum = 1) order_doctor_name,
ordered_by, 
(select d.dept_name from dept_dict d where d.dept_code = pOrders.ordered_by and rownum = 1) ordered_by_name,
appoint_no, 
common_flag, 
special_flag
from outp_patient_orders pOrders
where pOrders.visit_no={0}
and pOrders.visit_date=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
and pOrders.charge_indicator={2}";
            sql = string.Format(sql, visitNo, visitDate, state);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OUTP_PATIENT_ORDERS>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public int UpdateOutpPatientOrdersDrug(BaseEntityer db, string visitNo, string visitDate, string serialNo, string itemNo, HisCommon.Enum.ChargeState chargeState)
        {
            int exec = 0;
            try
            {
                string state = "0";
                if (chargeState == HisCommon.Enum.ChargeState.无效)
                {
                    state = "-1";
                }
                string sql = @"update OUTP_PRESC set charge_indicator = {4}
where visit_no={0}
and visit_date=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
and serial_no = '{2}'
and item_no = '{3}'";
                sql = string.Format(sql, visitNo, visitDate, serialNo, itemNo, state);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        public int UpdateOutpPatientOrdersUnDrug(BaseEntityer db, string visitNo, string visitDate, string serialNo, string itemNo, HisCommon.Enum.ChargeState chargeState)
        {
            int exec = 0;
            try
            {
                string state = "0";
                if (chargeState == HisCommon.Enum.ChargeState.无效)
                {
                    state = "-1";
                }
                string sql = @"update OUTP_TREAT_REC  set charge_indicator = {4}
where visit_no = {0}
and visit_date = to_date('{1}','yyyy-MM-dd hh24:mi:ss')
and serial_no = '{2}'
and item_no = '{3}'";
                sql = string.Format(sql, visitNo, visitDate, serialNo, itemNo, state);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }
        /// <summary>
        /// 门诊收费信息查询全部
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="StartDate"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<OUTP_RCPT_MASTER> QueryOutpRcptAllAccount(string userId, string StartDate, string date)
        {
            string sql = @"SELECT *
            FROM OUTP_RCPT_MASTER t
            WHERE t.OPERATOR_NO = '{0}'
            AND  t.visit_date>to_date('{1}','yyyy-MM-dd hh24:mi:ss')
            AND  t.visit_date<=to_date('{2}','yyyy-MM-dd hh24:mi:ss') 
            order by t.rcpt_no";
            sql = sql.SqlFormate(userId, StartDate, date);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_RCPT_MASTER>(ds).ToList();
        }

        public List<fin_invoiceinfo_record> QueryInvoiceInfoAllAccount(string userId, string Startdate, string date)
        {
            string sql = @"SELECT *
            FROM fin_invoiceinfo_record t
            WHERE t.FEE_OPER_CODE = '{0}'
            AND t.FEE_OPER_DATE>to_date('{1}','yyyy-MM-dd hh24:mi:ss')
            AND t.FEE_OPER_DATE<=to_date('{2}','yyyy-MM-dd hh24:mi:ss')
            and (t.INVOICE_KIND='00' or t.INVOICE_KIND='01')
            order by t.INVOICE_NO";
            sql = sql.SqlFormate(userId, Startdate, date);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<fin_invoiceinfo_record>(ds).ToList();
        }

        /// <summary>
        /// 挂号累计查询全部
        /// </summary>
        /// <param name="date">截止日期</param>
        /// <param name="operatorid">收款员号</param>
        /// <returns></returns>
        public DataTable GetUnifyRegistrationAllAccounts(string Startdate, string date, string operatorid)
        {
            string sql;
            sql = @"  select a.typeaccounts,a.CLINIC_TYPE,a.regist_fee,a.clinic_fee,a.other_fee,
    (a.regist_fee+a.clinic_fee+a.other_fee) as sum_fee,a.CARD_FEE,a.visit_no from 
    (
       select * from 
       (
        select '挂号' as typeaccounts,aa.regist_fee,aa.clinic_fee,aa.other_fee,aa.CARD_FEE,aa.visit_no,
        CLINIC_TYPE_DICT.CLINIC_TYPE from 
        (
            select nvl(sum(regist_fee),0.00) as regist_fee, nvl(sum(clinic_fee),0.00) as clinic_fee, 
            nvl(sum(other_fee),0.00) as other_fee, nvl(sum(CARD_FEE),0.00) as CARD_FEE,nvl(count(visit_no),0) as visit_no, 
            clinic_master.clinic_type from clinic_master 
            where registering_date >to_date('{0}','yyyy-mm-dd hh24:mi:ss') and 
            registering_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
            and clinic_master.operator = '{2}'
            group by clinic_master.clinic_type
        ) 
        aa right join  CLINIC_TYPE_DICT on aa.clinic_type = CLINIC_TYPE_DICT.CLINIC_TYPE
        order by CLINIC_TYPE_DICT.CLINIC_TYPE desc
        )
      union all
      select * from 
       (
        select '退号' as typeaccounts,aa.regist_fee,aa.clinic_fee,aa.other_fee,aa.CARD_FEE,aa.visit_no,
        CLINIC_TYPE_DICT.CLINIC_TYPE from 
        (
            select nvl(sum(regist_fee),0.00) as regist_fee, nvl(sum(clinic_fee),0.00) as clinic_fee, 
            nvl(sum(other_fee),0.00) as other_fee,nvl(sum(CARD_FEE),0.00) as CARD_FEE, nvl(count(visit_no),0) as visit_no, 
            clinic_master.clinic_type from clinic_master 
            where 
            RETURNED_DATE > to_date('{0}','yyyy-mm-dd hh24:mi:ss') and
            RETURNED_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
            and clinic_master.returned_operator = '{2}'
            group by clinic_master.clinic_type
        ) 
        aa right join  CLINIC_TYPE_DICT on aa.clinic_type = CLINIC_TYPE_DICT.CLINIC_TYPE
        order by CLINIC_TYPE_DICT.CLINIC_TYPE desc
        )
    ) a";
            sql = string.Format(sql, Startdate, date, operatorid);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 根据发票号查询发票打印次数
        /// </summary>
        /// <param name="rcptNo">发票号</param>
        /// <returns></returns>
        public int GetPrintFlagByRcptNO(string rcptNo)
        {
            string strSQL = string.Empty;
            strSQL = @" select nvl(t.print_flag,0) print_flag from outp_rcpt_master t where t.rcpt_no = '{0}' ";
            strSQL = string.Format(strSQL, rcptNo);

            int printCount = 0;
            DbDataReader dr = BaseEntityer.Db.ZDExecReader(strSQL);
            while (dr.Read())
            {
                printCount = Convert.ToInt32(dr[0].ToString());
            }
            if (!dr.IsClosed)
                dr.Close();
            return printCount;
        }

        /// <summary>
        /// 更新发票打印状态
        /// </summary>
        /// <param name="rcptNo">发票号</param>
        /// <returns></returns>
        public int UpdateInvoicePrintCount(string rcptNo)
        {
            string strSQL = string.Empty;
            strSQL = @" update outp_rcpt_master t
                           set t.print_flag = nvl(t.print_flag, 0) + 1
                         where t.rcpt_no = '{0}' ";
            strSQL = string.Format(strSQL, rcptNo);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 根据结算流水号获取门诊结算发票
        /// </summary>
        /// <param name="rctpNO"></param>
        /// <returns></returns>
        public List<fin_invoiceinfo_record> GetOutpatientBalanceInvoicesByRcptNO(string rcptNO)
        {
            string strSQL = @" select t.*
                              from fin_invoiceinfo_record t
                             where t.rcpt_no = (select m.rcpt_no
                                                  from fin_invoiceinfo_record m
                                                 where m.invoice_no = '{0}' AND m.invoice_kind = '01')
                               and t.trans_type = '1'
                               and t.invoice_state IN( '0','2')
                               and t.invoice_kind = '01' ";
            strSQL = strSQL.SqlFormate(rcptNO);

            DataSet ds = BaseEntityer.Db.GetDataSet(strSQL);
            return DataSetToEntity.DataSetToT<fin_invoiceinfo_record>(ds).ToList();
        }


        /// <summary>
        /// 获取操作员最后一次日结汇总时间
        /// </summary>
        /// <param name="db"></param>
        /// <param name="balanceID"></param>
        /// <param name="dpc"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetDayReportMaxDate(BaseEntityer db, string opercode, ref DateTime maxDate, ref string errMsg)
        {
            string sql = @"SELECT NVL(TO_CHAR(MAX(t.acct_date + 1 / 86400), 'yyyy-mm-dd hh24:mi:ss'), '2013-01-01 00:00:00')
                      FROM outp_acct_master t
                     WHERE t.operator_no = '{0}'";

            try
            {
                sql = string.Format(sql, opercode);
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "没有查询到上次日结日期。";
                    return -1;
                }
                maxDate = DateTime.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }


        public DataTable GetDrugPriceList(string drug_code)
        {
            string sql = @"select t.drug_spec from drug_price_list t where t.drug_code='{0}'";
            sql = string.Format(sql, drug_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        #region 门诊药品申请业务方法


        #region 药品申请

        public int InsertDrugPrescMasterTemp(HisCommon.DataEntity.DRUG_PRESC_MASTER_TEMP record, BaseEntityer db)
        {
            string sql = @"INSERT INTO DRUG_PRESC_MASTER_TEMP  t   --
                            (
                            t.PRESC_DATE,   --处方日期
                            t.PRESC_NO,   --处方号
                            t.DISPENSARY,   --发药药局
                            t.QUEUE_ID,   --发药队列号
                            t.STATUS,   --处理状态
                            t.PATIENT_ID,   --病人标识号
                            t.NAME,   --姓名
                            t.NAME_PHONETIC,   --姓名拼音
                            t.IDENTITY,   --身份
                            t.CHARGE_TYPE,   --费别
                            t.UNIT_IN_CONTRACT,   --病人合同单位
                            t.PRESC_TYPE,   --处方类别
                            t.PRESC_ATTR,   --处方属性
                            t.PRESC_SOURCE,   --处方来源
                            t.REPETITION,   --剂数
                            t.COSTS,   --费用
                            t.PAYMENTS,   --实付费用
                            t.ORDERED_BY,   --开单科室
                            t.PRESCRIBED_BY,   --开方医生
                            t.ENTERED_BY,   --录方人
                            t.RCPT_NO,   --
                            t.FLAG_CLASS   --药品类别
                            ) 
                            VALUES
                            (
                            TO_DATE('{0}','YYYY-MM-DD HH24:MI:SS'),   --处方日期
                            '{1}',   --处方号
                            '{2}',   --发药药局
                            '{3}',   --发药队列号
                            '{4}',   --处理状态
                            '{5}',   --病人标识号
                            '{6}',   --姓名
                            '{7}',   --姓名拼音
                            '{8}',   --身份
                            '{9}',   --费别
                            '{10}',   --病人合同单位
                            '{11}',   --处方类别
                            '{12}',   --处方属性
                            '{13}',   --处方来源
                            '{14}',   --剂数
                            '{15}',   --费用
                            '{16}',   --实付费用
                            '{17}',   --开单科室
                            '{18}',   --开方医生
                            '{19}',   --录方人
                            '{20}',   --
                            '{21}'   --药品类别
                            ) ";
            object[] param = new object[] { 
                record.Presc_date	,
                record.Presc_no	,
                record.	Dispensary	,
                record.	Queue_id	,
                record.	Status	,
                record.	Patient_id	,
                record.	Name	,
                record.	Name_phonetic	,
                record.	Identity	,
                record.	Charge_type	,
                record.	Unit_in_contract	,
                record.	Presc_type	,
                record.	Presc_attr	,
                record.	Presc_source	,
                record.	Repetition	,
                record.	Costs	,
                record.	Payments	,
                record.	Ordered_by	,
                record.	Prescribed_by	,
                record.	Entered_by	,
                record.	Rcpt_no	,
                record.	Flag_class	
               
                };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除表中数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dispensary">开放科室</param>
        /// <param name="RcptNo">唯一标示</param>
        /// <returns></returns>
        public int DeleteDrugPrescMasterTemp(BaseEntityer db, string dispensary, string RcptNo)
        {
            string sql = @"DELETE DRUG_PRESC_MASTER_TEMP  t WHERE t.dispensary='{0}' and t.rcpt_no='{1}'";
            sql = string.Format(sql, dispensary, RcptNo);
            return db.ExecuteNonQuery(sql);
        }

        #endregion

        #region 药品申请明细


        public int InsertDrugPrescDetailTemp(HisCommon.DataEntity.DRUG_PRESC_DETAIL_TEMP record, BaseEntityer db)
        {
            string sql = @"INSERT INTO DRUG_PRESC_DETAIL_TEMP  
                        (
                        PRESC_DATE,
                        PRESC_NO,
                        ITEM_NO,
                        DRUG_CODE,
                        DRUG_SPEC,
                        DRUG_NAME,
                        FIRM_ID,
                        PACKAGE_SPEC,
                        PACKAGE_UNITS,
                        QUANTITY,
                        COSTS,
                        PAYMENTS,
                        SERIAL_NO,
                        BATCHNO,
                        D_ITEM_NO,
                        DISPENSARY,
                        RCPT_NO
                        ) 
                        VALUES
                        (
                        TO_DATE('{0}','YYYY-MM-DD HH24:MI:SS'),
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
                        '{16}'
                        ) ";
            object[] param = new object[] { 
                record.Presc_date	,
                record.Presc_no	,
                record.	Item_no	,
                record.	Drug_code	,
                record.	Drug_spec	,
                record.	Drug_name	,
                record.	Firm_id	,
                record.	Package_spec	,
                record.	Package_units	,
                record.	Quantity	,
                record.	Costs	,
                record.	Payments	,
                record.	Serial_no	,
                record.	Batchno	,
                record.	D_item_no	,
                record.	Dispensary	,
                record.	Rcpt_no	
                };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除表中数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dispensary">开放科室</param>
        /// <param name="RcptNo">唯一标示</param>
        /// <returns></returns>
        public int DeleteDrugPrescDetailTemp(BaseEntityer db, string dispensary, string RcptNo)
        {
            string sql = @"DELETE DRUG_PRESC_DETAIL_TEMP  t WHERE t.Dispensary='{0}' and t.Rcpt_No='{1}'";
            sql = string.Format(sql, dispensary, RcptNo);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据医嘱号和序号删除发药申请明细数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="serial_no"></param>
        /// <param name="item_no"></param>
        /// <returns></returns>
        public int DeleteDrugPrescDetailTempForSerialNo(BaseEntityer db, string serial_no, string item_no)
        {
            string sql = @"DELETE DRUG_PRESC_DETAIL_TEMP  t WHERE t.serial_no='{0}' and t.item_no='{1}'";
            sql = string.Format(sql, serial_no, item_no);
            return db.ExecuteNonQuery(sql);
        }

        #endregion

        #endregion
    }
}
