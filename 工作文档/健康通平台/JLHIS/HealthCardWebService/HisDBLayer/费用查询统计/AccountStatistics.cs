using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HisCommon.DataEntity.Report;
using HisCommon;
using HisCommon.DataEntity;

namespace HisDBLayer
{
    public class AccountStatistics
    {
        #region Old 2013-4-2封版，以前开发的语句上这里找,二院的查询在这里，请三思修改

        public List<HisCommon.DataEntity.Report.LowCostInputMonthClass> LowCostInputMonth(string dateStart, string dateEnd, string Stock, string supply_code, string third_code)
        {
            string sql = @"
SELECT exp_input_account.supply,   
             substr( exp_in_book.exp_code, 1, 6 ) as class_third,   
             exp_class_third.third_name,   
             exp_in_book.item_sum AS item_sum  
        FROM exp_in_book,   
             exp_input_account,   
             exp_class_third  
       WHERE ( exp_in_book.store_id = exp_input_account.store_id ) and  
             ( exp_in_book.bill_id = exp_input_account.bill_id ) and  
             ( ( exp_input_account.in_class not in ('06','07') ) AND
             ( exp_input_account.store_id = {2} ) AND  
             ( exp_input_account.in_date >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') ) AND  
             ( exp_input_account.in_date <=to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') ) AND  
             ( exp_input_account.indicator = '0' ) AND 
             ( substr( exp_in_book.exp_code, 1, 6 )  = exp_class_third.class_code ) )  
             and ( exp_input_account.supply='{3}' or '{3}' is null) and (exp_class_third.class_code='{4}'or '{4}' is null)
    UNION ALL
      SELECT exp_output_account.receive,   
             substr( exp_out_book.exp_code, 1, 6 ) as class_third,   
             exp_class_third.third_name,   
             0 - exp_out_book.retail_pric * exp_out_book.book_quan as item_sum  
        FROM exp_out_book,   
             exp_output_account,   
             exp_class_third  
       WHERE ( exp_out_book.store_id = exp_output_account.store_id ) and  
             ( exp_out_book.bill_id = exp_output_account.bill_id ) and  
             ( ( exp_output_account.out_class in ('08','09') ) and
          ( exp_out_book.store_id = {2} ) AND  
             ( exp_output_account.out_date >=  to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') ) AND  
             ( exp_output_account.out_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') ) AND  
          ( exp_output_account.indicator = '0' ) AND 
             ( substr( exp_out_book.exp_code, 1, 6 ) = exp_class_third.class_code ) )
               and (  exp_output_account.receive='{3}' or '{3}' is null) and (exp_class_third.class_code='{4}'or '{4}' is null) ";
            sql = string.Format(sql, dateStart, dateEnd, Stock, supply_code, third_code);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.LowCostInputMonthClass>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="Stock"></param>
        /// <param name="supply_code"></param>
        /// <param name="third_code"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.LowCostOutputMonthClass> LowCostOutputMonth(string dateStart, string dateEnd, string Stock, string supply_code, string third_code)
        {
            string sql = @"SELECT exp_output_account.receive,   
         substr( exp_out_book.exp_code, 1, 6 ) as class_third,   
         exp_class_third.third_name,   
         exp_out_book.retail_pric * exp_out_book.book_quan as item_sum  
    FROM exp_out_book,   
         exp_output_account,   
         exp_class_third  
   WHERE ( exp_out_book.store_id = exp_output_account.store_id ) and  
         ( exp_out_book.bill_id = exp_output_account.bill_id ) and  
         ( ( exp_output_account.out_class not in ('08','09') ) and
      ( exp_out_book.store_id = {2} ) AND  
         ( exp_output_account.out_date >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') ) AND  
         ( exp_output_account.out_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') ) AND  
      ( exp_output_account.indicator = '0' ) AND 
         ( substr( exp_out_book.exp_code, 1, 6 ) = exp_class_third.class_code ) ) 
       and ( exp_output_account.receive='{3}' or '{3}' is null) and (exp_class_third.class_code='{4}'or '{4}' is null)   
UNION ALL
  SELECT exp_input_account.supply,   
         substr( exp_in_book.exp_code, 1, 6 ) as class_third,   
         exp_class_third.third_name,   
         0 - exp_in_book.item_sum AS item_sum  
    FROM exp_in_book,   
         exp_input_account,   
         exp_class_third  
   WHERE ( exp_in_book.store_id = exp_input_account.store_id ) and  
         ( exp_in_book.bill_id = exp_input_account.bill_id ) and  
         ( ( exp_input_account.in_class in ('06','07') ) AND
      ( exp_input_account.store_id = {2} ) AND  
         ( exp_input_account.in_date >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') ) AND  
         ( exp_input_account.in_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') ) AND  
      ( exp_input_account.indicator = '0' ) AND 
         ( substr( exp_in_book.exp_code, 1, 6 )  = exp_class_third.class_code ) ) 
       and ( exp_input_account.supply='{3}' or '{3}' is null) and (exp_class_third.class_code='{4}'or '{4}' is null)";
            sql = string.Format(sql, dateStart, dateEnd, Stock, supply_code, third_code);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.LowCostOutputMonthClass>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 低值易耗品厂商
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.EXP_SELL> GetExpSellInfo()
        {
            string sql = "select * from EXP_SELL";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXP_SELL>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 低值易耗品三级分类
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.EXP_CLASS_THIRD> GetExpClassThirdInfo()
        {
            string sql = "select * from  EXP_CLASS_THIRD";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXP_CLASS_THIRD>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 库房
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.EXP_STOCK_NAME> GetExpStockNameInfo()
        {
            string sql = "select * from  EXP_STOCK_NAME";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXP_STOCK_NAME>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }

        #region 2013-1-29 以后添加,查询统计用
        /// <summary>
        /// 门诊科室工作量统计
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable OutpDeptWorkAccount(string dateStart, string dateEnd)
        {
            string sql = @"
select t.visit_date as 就诊日期,
       t.visit_no as 就诊序号,
       t.clinic_label as 号别,
       t.patient_id as 病人标识号,
       t.name as 姓名,
       t.sex as 性别,
       t.age as 年龄,
       t.identity as 身份,
       t.charge_type as 费别,
       t.insurance_no as  医疗保险号,
       t.clinic_type as 号类,
       t.first_visit_indicator as 初诊标志,
       t.visit_dept as 就诊科室,
       t.regist_fee as 挂号费,
       t.clinic_fee as 诊疗费,
       t.other_fee as 其他费,
       t.clinic_charge as 实收费用,
       t.registering_date as 挂号日期,
       t.operator as 挂号员
  from clinic_master t
 where t.visit_date >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
   and t.visit_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
   and t.returned_operator is null
 order by t.visit_date
";
            sql = string.Format(sql, dateStart, dateEnd);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 退号统计
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.UnRegistQuery> QueryUnRegist(string dateStart, string dateEnd)
        {
            string sql = @"SELECT t.RETURNED_OPERATOR as un_user_id,
       t.PATIENT_ID,
       t.NAME,
       t.CHARGE_TYPE,
       t.IDENTITY,
       t.OPERATOR as user_id,
       t.VISIT_NO,
       t.REGIST_FEE,
       t.CLINIC_FEE,
       t.OTHER_FEE,
       t.REGIST_FEE + t.CLINIC_FEE + t.OTHER_FEE as cost,
       t.RETURNED_DATE
  FROM CLINIC_MASTER t
 WHERE (t.RETURNED_DATE is not null or t.RETURNED_OPERATOR is not null)
   and (t.RETURNED_DATE >=to_date('{0}','yyyy-MM-dd hh24:mi:ss'))
   AND (t.RETURNED_DATE <=to_date('{1}','yyyy-MM-dd hh24:mi:ss'))
   order by t.RETURNED_DATE desc";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.UnRegistQuery>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        #endregion
        #region 项目统计 lql
        /// <summary>
        /// 统计项目对照
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.JxProjectCompare> GetJxProjectCompare(string id)
        {
            string sql = @"select * from TJ_PROJECT_COMPARE WHERE ID='{0}'";
            sql = string.Format(sql, id);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.JxProjectCompare>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 统计项目查询
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.JxProjectItem> GetJxProjectItem()
        {
            string sql = @"select * from TJ_PROJECT_ITEM";

            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.JxProjectItem>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 删除统计项目
        /// </summary>
        /// <returns></returns>
        public int DelJxProjectItem(string id, BaseEntityer db)
        {
            string sql = @"delete from TJ_PROJECT_ITEM where id='{0}'";
            sql = string.Format(sql, id);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        ///  保存统计项目
        /// </summary>
        /// <returns></returns>
        public int SaveJxProjectItem(HisCommon.DataEntity.Report.JxProjectItem item, BaseEntityer db)
        {
            string sql = @"insert into TJ_PROJECT_ITEM(ID,PROJECT_NAME,PROJECT_MEMO) values ('{0}','{1}','{2}')";
            sql = string.Format(sql, item.ID, item.PROJECT_NAME, item.PROJECT_MEMO);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 插入jx_doctor_dict表数据
        /// </summary>
        /// <param name="o">jx_doctor_dict数据</param>
        /// <returns></returns>
        public int InsertProjectCompareItem(HisCommon.DataEntity.Report.JxProjectCompare o, BaseEntityer db)
        {
            string sql = @"insert into TJ_PROJECT_COMPARE (ID,ITEM_CODE,ITEM_NAME,ITEM_CLASS,ITEM_COMPARE_NAME)
                      values('{0}','{1}','{2}','{3}','{4}')";
            sql = string.Format(sql, o.ID, o.ITEM_CODE, o.ITEM_NAME, o.ITEM_CLASS, o.ITEM_COMPARE_NAME);
            return db.ExecuteNonQuery(sql);

        }
        /// <summary>
        /// 删除一条jx_doctor_dict表数据
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db">jx_doctor_dict表数据</param>
        /// <returns></returns>
        public int DeleteProjectCompareItem(HisCommon.DataEntity.Report.JxProjectCompare o, BaseEntityer db)
        {
            string sql = @"delete from TJ_PROJECT_COMPARE t
                   where t.ID='{0}'
                    and t.ITEM_CODE='{1}'
                    and t.ITEM_NAME='{2}'
                    and t.ITEM_CLASS='{3}'";
            sql = string.Format(sql, o.ID, o.ITEM_CODE, o.ITEM_NAME, o.ITEM_CLASS);
            return db.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 统计项目对照
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet QueryJxItem(string id, string flag, string starDate, string endDate)
        {
            string sql = "";
            string temsql = "select * from tj_project_item where id='" + id + "'";
            string temDep = "";
            DataSet dt = BaseEntityer.Db.GetDataSet(temsql);
            if (dt.Tables[0].Rows.Count > 0)
            {
                temDep = dt.Tables[0].Rows[0]["DEPTCODE"].ToString().Trim();
            }

            if (flag == "0")
            {
                if (temDep != "")
                {
                    sql = @"select m.item_code as 项目代码,m.item_name as 项目名称,(select d.dept_name  from dept_dict d where d.dept_code=m.order_dept) as 开单科室 ,
              m.cn as 数量, to_char(m.cos,'999999999999.99') as 金额 ,to_char((CASE m.cn  WHEN 0 then 0  else  m.cos/m.cn end),'999999999999.99') as 单价
              from ( select v.item_code,v.item_name,v.order_dept,sum(v.amount) as cn,sum(v.costs)  as cos  
              from v_fee_statistic  v where v.item_code in( select item_code from TJ_PROJECT_COMPARE WHERE ID='{0}')
              and  v.operdate>=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
              and v.operdate<=to_date('{2}','yyyy-MM-dd hh24:mi:ss') and v.order_dept in(" + temDep + ") group by  v.item_code,v.item_name,v.order_dept) m order by 开单科室 ";
                }
                else
                {
                    sql = @"select m.item_code as 项目代码,m.item_name as 项目名称,(select d.dept_name  from dept_dict d where d.dept_code=m.order_dept) as 开单科室 ,
              m.cn as 数量, to_char(m.cos,'999999999999.99') as 金额  ,to_char((CASE m.cn  WHEN 0 then 0  else  m.cos/m.cn end),'999999999999.99') as 单价
              from ( select v.item_code,v.item_name,v.order_dept,sum(v.amount) as cn,sum(v.costs)  as cos  
              from v_fee_statistic  v where v.item_code in( select item_code from TJ_PROJECT_COMPARE WHERE ID='{0}')
              and  v.operdate>=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
              and v.operdate<=to_date('{2}','yyyy-MM-dd hh24:mi:ss') group by  v.item_code,v.item_name,v.order_dept) m order by 开单科室 ";
                }
            }
            else
            {

                if (temDep != "")
                {
                    sql = @"select m.item_code as 项目代码,m.item_name as 项目名称,(select d.dept_name  from dept_dict d where d.dept_code=m.performed_by) as 执行科室 ,
                m.cn as 数量, to_char(m.cos,'999999999999.99') as 金额 ,to_char((CASE m.cn  WHEN 0 then 0  else  m.cos/m.cn end),'999999999999.99') as 单价
                from ( select v.item_code,v.item_name,v.performed_by,sum(v.amount) as cn,sum(v.costs)  as cos  
                from v_fee_statistic  v where v.item_code in( select item_code from TJ_PROJECT_COMPARE WHERE ID='{0}')
                and  v.operdate>=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                and v.operdate<=to_date('{2}','yyyy-MM-dd hh24:mi:ss') and v.performed_by in(" + temDep + ") group by  v.item_code,v.item_name,v.performed_by) m order by 执行科室 ";
                }
                else
                {
                    sql = @"select m.item_code as 项目代码,m.item_name as 项目名称,(select d.dept_name  from dept_dict d where d.dept_code=m.performed_by) as 执行科室 ,
                m.cn as 数量, to_char(m.cos,'999999999999.99') as 金额 ,to_char((CASE m.cn  WHEN 0 then 0  else  m.cos/m.cn end),'999999999999.99') as 单价
                from ( select v.item_code,v.item_name,v.performed_by,sum(v.amount) as cn,sum(v.costs)  as cos  
                from v_fee_statistic  v where v.item_code in( select item_code from TJ_PROJECT_COMPARE WHERE ID='{0}')
                and  v.operdate>=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                and v.operdate<=to_date('{2}','yyyy-MM-dd hh24:mi:ss') group by  v.item_code,v.item_name,v.performed_by) m order by 执行科室 ";
                }


            }
            sql = string.Format(sql, id, starDate, endDate);
            return BaseEntityer.Db.GetDataSet(sql);
        }
        public DataSet QueryChildItem(string DeptId, string starDate, string endDate)
        {
            string sql = @" select m.patient_id as 病人标识,
       m.inp_no as 住院号,
       m.name as 姓名,
       m.sex as 性别,
       m.date_of_birth as 生日,
       (select dept_name
          from dept_dict d
         where d.dept_code = v.dept_admission_to) as 入院科室,
       v.admission_date_time as 入院时间,
       (select s.user_name
          from users_staff_dict s
         where s.user_id = v.doctor_in_charge) as 医生
  from PAT_VISIT v
  left join pat_master_index m on v.patient_id = m.patient_id
 where v.dept_admission_to = '010701'
   and v.admission_date_time >=
       to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
   and v.admission_date_time <=
       to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')";
            sql = string.Format(sql, DeptId, starDate, endDate);
            return BaseEntityer.Db.GetDataSet(sql);
        }
        /// <summary>
        /// 查询未交款人员
        /// </summary>
        /// <returns></returns>
        public DataSet QueryNoSendMoney(string flag)
        {
            string sql = "";
            if (flag == "O")
            {
                sql = @"select m.operator_no as 编号,
       (select u.user_name
          from users_staff_dict u
         where u.user_id = m.operator_no) as 姓名,
       to_char(m.合计,'999999999.99') as 合计,
       to_char(nvl(m.帐户支付,0) ,'999999999.99') as 帐户支付 ,
       to_char(nvl(m.大额支付,0) ,'999999999.99') as 大额支付 ,
       to_char(nvl(m.统筹支付,0),'999999999.99')  as 统筹支付 ,
       to_char(nvl(m.公务员,0) ,'999999999.99') as 公务员 ,
       to_char(nvl(m.现金,0) ,'999999999.99')  as 现金 
  from (select a.operator_no,
               sum(a.total_costs) as 合计,
               sum((select nvl(b.payment_amount, 0)
                     from outp_payments_money b
                    where b.rcpt_no = a.rcpt_no
                      and b.money_type = '帐户支付')) as 帐户支付,
               sum((select nvl(b.payment_amount, 0)
                     from outp_payments_money b
                    where b.rcpt_no = a.rcpt_no
                      and b.money_type = '大额支付')) as 大额支付,
               sum((select nvl(b.payment_amount, 0)
                     from outp_payments_money b
                    where b.rcpt_no = a.rcpt_no
                      and b.money_type = '统筹支付')) as 统筹支付,
               sum((select nvl(b.payment_amount, 0)
                     from outp_payments_money b
                    where b.rcpt_no = a.rcpt_no
                      and b.money_type = '公务员')) as 公务员,
               sum((select nvl(b.payment_amount, 0)
                     from outp_payments_money b
                    where b.rcpt_no = a.rcpt_no
                      and (b.money_type = '现金' or b.money_type = '现金支付'))) as 现金
          from outp_rcpt_master a
         where a.acct_no is null or a.acct_no='**'
         group by a.operator_no) m
";
            }
            else
            {
                sql = @"select 收款员ID,(select s.user_name
          from users_staff_dict s
         where s.user_id = 收款员ID ) as 姓名,to_char(预交金现金,'999999999999.99') as 预交金现金 ,to_char(预交金支票,'999999999999.99') as 预交金支票,to_char(结算支票,'999999999999.99') as 结算支票 ,to_char(结算现金,'999999999999.99') as 结算现金,to_char(余额,'999999999999.99') as 余额
          from (
select   case  when yj.收款员 is null then js.收款员 else  yj.收款员 end as 收款员ID,yj.预交金现金,yj.预交金支票,js.结算支票,js.结算现金,(nvl(yj.预交金现金,0)+nvl(yj.预交金支票,0)-nvl(js.结算支票,0)-nvl(js.结算现金,0)) as 余额 from( 
(select case  when z.operator_no is null then x.operator_no else  z.operator_no end as 收款员  ,x.现金 as 预交金现金 ,z.支票 as 预交金支票 from (select r.operator_no,r.pay_way,sum(r.amount) as 现金  from PREPAYMENT_RCPT r where  transact_type<>'结算' and acct_no is null and pay_way='现金'
 group by r.operator_no,r.pay_way ) x left join  (select r.operator_no,r.pay_way,sum(r.amount) as 支票  from PREPAYMENT_RCPT r where  transact_type<>'结算' and acct_no is null and pay_way='支票'
 group by r.operator_no,r.pay_way ) z  on x.operator_no=z.operator_no) yj left join 
 (select s.operator_no as 收款员,
          sum ((select nvl(b.refunded_amount, 0)
                 from INP_PAYMENTS_MONEY b
                where b.rcpt_no = s.rcpt_no
                  and b.money_type = '支票')) as 结算支票,
          sum ((select nvl(b.refunded_amount, 0)
                 from INP_PAYMENTS_MONEY b
                where b.rcpt_no = s.rcpt_no
                  and b.money_type = '现金')) as 结算现金
     from INP_SETTLE_MASTER s
    where s.transact_type <> '作废' and (ACCT_NO is null or ACCT_NO='**')  group by s.operator_no) js on yj.收款员=js.收款员) )";

            }
            return BaseEntityer.Db.GetDataSet(sql);
        }
        /// <summary>
        /// 查询未交款人员
        /// </summary>
        /// <returns></returns>
        public DataSet QueryNoSendMoneyDetail(string flag, string userId)
        {
            string sql = "";
            if (flag == "O")
            {
                sql = @"select a.operator_no as 操作员ID ,a.patient_id 病人id,a.NAME 姓名,a.VISIT_DATE as 就诊日期,
              a.total_costs  as 合计,
               to_char((select nvl(b.payment_amount, 0)
                     from outp_payments_money b
                    where b.rcpt_no = a.rcpt_no
                      and b.money_type = '帐户支付') ,'999999999.99') as 帐户支付,
               to_char((select nvl(b.payment_amount, 0)
                     from outp_payments_money b
                    where b.rcpt_no = a.rcpt_no
                      and b.money_type = '大额支付') ,'999999999.99')   as 大额支付,
                to_char((select nvl(b.payment_amount, 0)
                     from outp_payments_money b
                    where b.rcpt_no = a.rcpt_no
                      and b.money_type = '统筹支付') ,'999999999.99')   as 统筹支付,
                to_char((select nvl(b.payment_amount, 0)
                     from outp_payments_money b
                    where b.rcpt_no = a.rcpt_no
                      and b.money_type = '公务员') ,'999999999.99')   as 公务员,
                to_char((select nvl(b.payment_amount, 0)
                     from outp_payments_money b
                    where b.rcpt_no = a.rcpt_no
                      and (b.money_type = '现金' or b.money_type = '现金支付') ) ,'999999999.99')  as 现金
          from outp_rcpt_master a
         where a.acct_no is null and  a.operator_no='{0}'";
                sql = string.Format(sql, userId);
            }
            else
            {
                sql = @"select s.operator_no as 收款员,s.patient_id 病人ID,(select p.NAME from pat_master_index p where p.PATIENT_ID=s.patient_id) as 姓名,s.settling_date as 结算日期,
                         (select nvl(b.payment_amount, 0)
                              from INP_PAYMENTS_MONEY b
                             where b.rcpt_no = s.rcpt_no
                               and b.money_type = '支票') as 结算支票,
                         (select nvl(b.refunded_amount, 0)
                              from INP_PAYMENTS_MONEY b
                             where b.rcpt_no = s.rcpt_no
                               and b.money_type = '现金')as 结算现金
                   from INP_SETTLE_MASTER s
                  where s.transact_type <> '作废'
                    and ACCT_NO is null and s.operator_no='{0}'";
                sql = string.Format(sql, userId);
            }
            return BaseEntityer.Db.GetDataSet(sql);
        }

        /// <summary>
        /// 查询未交款人员
        /// </summary>
        /// <returns></returns>
        public DataSet QueryNoSendMoneyDetailYJJ(string flag, string userId)
        {
            string sql = "";
            if (flag == "I")
            {
                sql = @"select r.operator_no as 收款员id,r.patient_id as  病人id,(
                                 select p.NAME from pat_master_index p where p.PATIENT_ID=r.patient_id) as 患者姓名,r.transact_date as 预交金日期,
                                r.pay_way as 支付方式,
                                r.amount as 预交金金额
                           from PREPAYMENT_RCPT r
                          where transact_type <> '结算'
                            and acct_no is null and operator_no='{0}'";
                sql = string.Format(sql, userId);
            }
            return BaseEntityer.Db.GetDataSet(sql);
        }

        /// <summary>
        /// 查询查询工资科室对照表
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.SLARY_DEPT_COMPARE> GetSlaryDeptCompares()
        {
            string sql = @"select * from SLARY_DEPT_COMPARE t";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.SLARY_DEPT_COMPARE>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 插入QUERY_DEPT_COMPARE表数据
        /// </summary>
        /// <param name="o">QUERY_DEPT_COMPARE数据</param>
        /// <returns></returns>
        public int InsertSlaryDeptCompare(HisCommon.DataEntity.Report.SLARY_DEPT_COMPARE o, BaseEntityer db)
        {
            string sql = @"insert into SLARY_DEPT_COMPARE (COMPARE_NAME,DEPT_NAME,DEPT_CODE,TAG,NAME,COMPARE_ID)
values('{0}','{1}','{2}','{3}','{4}','{5}')";
            sql = string.Format(sql, o.COMPARE_NAME, o.DEPT_NAME, o.DEPT_CODE, o.TAG, o.NAME, o.COMPARE_ID);
            return db.ExecuteNonQuery(sql);

        }
        /// <summary>
        /// 删除一条QUERY_DEPT_COMPARE表数据
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db">QUERY_DEPT_COMPARE表数据</param>
        /// <returns></returns>
        public int DeleteSlaryDeptCompare(HisCommon.DataEntity.Report.SLARY_DEPT_COMPARE o, BaseEntityer db)
        {
            string sql = @"delete from SLARY_DEPT_COMPARE t
where t.dept_code='{0}'
and t.dept_name='{1}'";
            sql = string.Format(sql, o.DEPT_CODE, o.DEPT_NAME);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        ///  保存工资科室项目
        /// </summary>
        /// <returns></returns>
        public int SaveJxSlaryDept(HisCommon.DataEntity.Report.TJ_SLARY_DEPT item, BaseEntityer db)
        {
            string sql = @"insert into tj_slary_dept(ID,DEPT_NAME,DEPT_MEMO) values ('{0}','{1}','{2}')";
            sql = string.Format(sql, item.ID, item.DEPT_NAME, item.DEPT_MEMO);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 科室工资项目查询
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.TJ_SLARY_DEPT> GetJxSlaryDeptItem()
        {
            string sql = @"select * from TJ_SLARY_DEPT";

            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.TJ_SLARY_DEPT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 删除工资科室项目
        /// </summary>
        /// <returns></returns>
        public int DelJxSlaryDept(string id, BaseEntityer db)
        {
            string sql = @"delete from tj_slary_dept where id='{0}'";
            sql = string.Format(sql, id);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询比例科室项目
        /// </summary>
        /// <returns></returns>
        public DataSet QueryJxSlaryDeptBl(string id)
        {
            string sql = @"select * from  jx_dept_item_bl where DEPT_CODE='{0}'";
            sql = string.Format(sql, id);
            return BaseEntityer.Db.GetDataSet(sql);
        }

        /// <summary>
        /// 查询对照科室比例的信息
        /// </summary>
        /// <returns></returns>
        public DataSet QueryJxCompareDeptBl()
        {
            string sql = @"select s.dept_code, t.dept_name, t.jx_id, t.jx_name, t.bl
  from jx_dept_item_bl t
  left join slary_dept_compare s on t.dept_code = s.compare_id";
            //sql = string.Format(sql, id);
            return BaseEntityer.Db.GetDataSet(sql);
        }
        /// <summary>
        /// 查询对核磁CT院外数量
        /// </summary>
        /// <returns></returns>
        public DataSet QueryJxCompareOutItem(string starDate, string endDate, string CompareID, string DoctorID)
        {
            string sql = @" select m.item_code as 项目代码,m.item_name as 项目名称,(select d.dept_name  from dept_dict d where d.dept_code=m.order_dept) as 开单科室 ,
                        m.cn as 数量, to_char(m.cos,'999999999999.99') as 金额 ,to_char((CASE m.cn  WHEN 0 then 0  else  m.cos/m.cn end),'999999999999.99') as 单价
                        from ( select v.item_code,v.item_name,v.order_dept,sum(v.amount) as cn,sum(v.costs)  as cos  
                        from v_fee_statistic  v where v.item_code in( select item_code from TJ_PROJECT_COMPARE WHERE ID='{2}')
                        and  v.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                        and v.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') and v.order_doctor='{3}' group by  v.item_code,v.item_name,v.order_dept) m order by 开单科室";
            sql = string.Format(sql, starDate, endDate, CompareID, DoctorID);
            return BaseEntityer.Db.GetDataSet(sql);
        }

        /// <summary>
        ///查询科室医生收费项目数量
        /// </summary>
        /// <returns></returns>
        public DataSet QueryDeptDoctorItem(string starDate, string endDate, string deptID, string DoctorID)
        {
            //2013-5-9 by li 视图修改 v_fee_statistic --> v_fee_statistic_new
            string sql = @"select m.item_code as 项目代码,
       m.item_name as 项目名称,
       (select d.dept_name from dept_dict d where d.dept_code = m.order_dept) as 开单科室,
       (select u.user_name
          from users_staff_dict u
         where u.user_id = m.order_doctor) as 开单医生,
       m.cn as 数量,
       to_char(m.cos, '999999999999.99') as 金额
  from (select v.item_code,
               v.item_name,
               v.order_dept,
               v.order_doctor,
               sum(v.amount) as cn,
               sum(v.costs) as cos
          from v_fee_statistic_new v
         where v.operdate >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
           and v.operdate <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') ";
            if (DoctorID.Trim() != "")
            {
                sql = sql + " and v.order_doctor='" + DoctorID + "'";
            }
            if (deptID.Trim() != "")
            {
                sql = sql + " and v.order_dept='" + deptID + "'";
            }
            sql = sql + " group by v.item_code, v.item_name, v.order_dept, v.order_doctor) m order by 开单科室,开单医生";
            sql = string.Format(sql, starDate, endDate);
            return BaseEntityer.Db.GetDataSet(sql);
        }

        /// <summary>
        ///查询门诊科室收费项目数量
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOutDeptItem(string starDate, string endDate, string deptID, string DoctorID, string ChargeId, string PactType, string ItemCode, string RcptNo, string Name, string Type, string Invoice)
        {
            //2013-5-9 by li 医保类别项目置空
            //2014-4-25 by li 增加显示核算科目
            string sql = @"select m.patient_id as 患者ID,
       m.name as 患者姓名,
       m.charge_type 收费类别,
        --case when m.charge_type='医疗保险' and m.insurance_type='12' then '职工特殊门诊'
        --     when m.charge_type='医疗保险' and m.insurance_type='11'    then '职工普通门诊'
        --     when m.charge_type='医疗保险' and m.insurance_type='7N'   then '居民低保门诊'
        --     when m.charge_type='医疗保险' and m.insurance_type='79'   then '居民特殊门诊'
        --     when m.charge_type='医疗保险' and m.insurance_type ='70'   then '居民门诊慢性病'
        -- else  m.insurance_type end as  医保类别,
        '' as  医保类别,
       v.item_code as 项目编码,
       v.item_name as 项目名称,
       to_char(v.charges, '999999999999.99') 金额,
       v.amount  数量,
       (select u.user_name
          from users_staff_dict u
         where u.user_id =  v.order_doctor) as 开单医生,
       (select d.dept_name from dept_dict d where d.dept_code =  v.order_dept) as 开单科室,
(select u.user_name
          from users_staff_dict u
         where u.user_id = m.operator_no) as 收款员,
        v.rcpt_no as 收据号,
       v.invoice_new as 票据号,
       m.VISIT_DATE as 日期,
       r.class_name as 核算科目 
  from outp_bill_items v 
    left join reck_item_class_dict r
    on v.class_on_reckoning = r.class_code 
  right  join (select * from outp_rcpt_master where  ('{0}' is null or charge_type = '{0}') 
    and  ('{1}' is null or rcpt_no = '{1}')  and  ('{2}' is null or name like '%{2}%') 
    and  ('{3}' is null or operator_no = '{3}') and  ('{4}' is null or invoice_new = '{4}')) m on v.rcpt_no = m.rcpt_no
 where 1 = 1";
            //开始日期
            if (starDate.Trim() != "")
            {
                sql = sql + "  and m.VISIT_DATE >= to_date('" + starDate + "', 'yyyy-MM-dd hh24:mi:ss')";
            }
            if (endDate.Trim() != "")
            {
                sql = sql + "   and m.VISIT_DATE <= to_date('" + endDate + "', 'yyyy-MM-dd hh24:mi:ss')";
            }
            if (deptID.Trim() != "")
            {
                sql = sql + "    and v.order_dept = '" + deptID + "'";
            }
            if (DoctorID.Trim() != "")
            {
                sql = sql + "  and  v.order_doctor = '" + DoctorID + "'";
            }
            //if (ChargeId.Trim() != "")
            //{
            //    sql = sql + "  and m.operator_no = '" + ChargeId + "'";
            //}
            //if (PactType.Trim() != "")
            //{
            //    sql = sql + "  and m.charge_type = '" + PactType + "'";
            //}

            if (ItemCode.Trim() != "")
            {
                sql = sql + "  and v.item_code = '" + ItemCode + "'";
            }
            //if (RcptNo.Trim() != "")
            //{
            //    sql = sql + "  and m.rcpt_no = '" + RcptNo + "'";
            //}

            //if (Name.Trim() != "")
            //{
            //    sql = sql + "  and m.name like  '%" + Name + "%'";
            //}


            if (Type.Trim().Equals("收款"))
            {
                sql = sql + " and v.charges >= 0";
            }
            else if (Type.Trim().Equals("退款"))
            {
                sql = sql + " and v.charges < 0";
            }
            sql = sql + " order by m.VISIT_DATE DESC";
            sql = string.Format(sql, PactType, RcptNo, Name, ChargeId,Invoice);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        ///查询门诊科室收费项目数量
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOutDeptItem_SI(string starDate, string endDate, string deptID, string DoctorID, string ChargeId, string PactType, string ItemCode, string RcptNo, string Name, string Type)
        {
            //2013-5-9 by li 医保类别项目置空
            string sql = @"select m.patient_id as 患者ID,
       m.name as 患者姓名,
       m.charge_type 收费类别,
        --case when m.charge_type='医疗保险' and m.insurance_type='12' then '职工特殊门诊'
        --     when m.charge_type='医疗保险' and m.insurance_type='11'    then '职工普通门诊'
        --     when m.charge_type='医疗保险' and m.insurance_type='7N'   then '居民低保门诊'
        --     when m.charge_type='医疗保险' and m.insurance_type='79'   then '居民特殊门诊'
        --     when m.charge_type='医疗保险' and m.insurance_type ='70'   then '居民门诊慢性病'
        -- else  m.insurance_type end as  医保类别,
        '' as  医保类别,
       v.item_code as 项目编码,
       v.item_name as 项目名称,
       to_char(v.charges, '999999999999.99') 金额,
       v.amount  数量,
       (select u.user_name
          from users_staff_dict u
         where u.user_id =  v.order_doctor) as 开单医生,
       (select d.dept_name from dept_dict d where d.dept_code =  v.order_dept) as 开单科室,
(select u.user_name
          from users_staff_dict u
         where u.user_id = m.operator_no) as 收款员,
       v.rcpt_no as 发票号,
       m.VISIT_DATE as 日期,
       s.PAY_COST as 账户支付金额,
       s.PUB_COST as 统筹支付金额,
       s.OWN_COST as 现金支付金额,
       s.OFFICIAL_COST as 公务员账户支付金额,
       s.OVER_COST as 大额补助支付金额,
       s.OWN_SUPPLE_COST as 个人补充支付,
       s.HELP_ALLOWANCES_COST as 低保救助支付,
       s.ENTERPRISE_SUPPLE_COST as 企业补充支付,
       s.HELP_OWN_COST as 救助金支付金额 
  from outp_bill_items v
  right  join (select * from outp_rcpt_master where  ('{0}' is null or charge_type = '{0}') 
    and  ('{1}' is null or rcpt_no = '{1}')  and  ('{2}' is null or name like '%{2}%') 
    and  ('{3}' is null or operator_no = '{3}')  ) m on v.rcpt_no = m.rcpt_no 
  left join si_info s on s.invoice_no = v.rcpt_no and s.type_code = '1' 
 where 1 = 1";
            //开始日期
            if (starDate.Trim() != "")
            {
                sql = sql + "  and m.VISIT_DATE >= to_date('" + starDate + "', 'yyyy-MM-dd hh24:mi:ss')";
            }
            if (endDate.Trim() != "")
            {
                sql = sql + "   and m.VISIT_DATE <= to_date('" + endDate + "', 'yyyy-MM-dd hh24:mi:ss')";
            }
            if (deptID.Trim() != "")
            {
                sql = sql + "    and v.order_dept = '" + deptID + "'";
            }
            if (DoctorID.Trim() != "")
            {
                sql = sql + "  and  v.order_doctor = '" + DoctorID + "'";
            }
            //if (ChargeId.Trim() != "")
            //{
            //    sql = sql + "  and m.operator_no = '" + ChargeId + "'";
            //}
            //if (PactType.Trim() != "")
            //{
            //    sql = sql + "  and m.charge_type = '" + PactType + "'";
            //}

            if (ItemCode.Trim() != "")
            {
                sql = sql + "  and v.item_code = '" + ItemCode + "'";
            }
            //if (RcptNo.Trim() != "")
            //{
            //    sql = sql + "  and m.rcpt_no = '" + RcptNo + "'";
            //}

            //if (Name.Trim() != "")
            //{
            //    sql = sql + "  and m.name like  '%" + Name + "%'";
            //}
            if (Type.Trim().Equals("收款"))
            {
                sql = sql + " and v.charges >= 0";
            }
            else if (Type.Trim().Equals("退款"))
            {
                sql = sql + " and v.charges < 0";
            }
            sql = sql + " order by m.VISIT_DATE DESC";
            sql = string.Format(sql, PactType, RcptNo, Name, ChargeId);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 门诊退费发票查询
        /// </summary>
        /// <param name="RcptNo"></param>
        /// <returns></returns>
        public DataTable QueryOutDeptReturnItem(string RcptNo)
        {
            string sql = @"select m.patient_id as 患者ID,
       m.name as 患者姓名,
       m.charge_type 收费类别,
        case when m.charge_type='医疗保险' and m.insurance_type='12' then '职工特殊门诊'
             when m.charge_type='医疗保险' and m.insurance_type='11'    then '职工普通门诊'
             when m.charge_type='医疗保险' and m.insurance_type='7N'   then '居民低保门诊'
             when m.charge_type='医疗保险' and m.insurance_type='79'   then '居民特殊门诊'
             when m.charge_type='医疗保险' and m.insurance_type ='70'   then '居民门诊慢性病'
         else  m.insurance_type end as  医保类别,
       v.item_code as 项目编码,
       v.item_name as 项目名称,
       to_char(v.costs, '999999999999.99')  金额,
       v.amount  数量,
       (select u.user_name
          from users_staff_dict u
         where u.user_id =  v.order_doctor) as 开单医生,
       (select d.dept_name from dept_dict d where d.dept_code =  v.order_dept) as 开单科室,
(select u.user_name
          from users_staff_dict u
         where u.user_id = m.operator_no) as 收款员,
       v.rcpt_no as 发票号,
       m.VISIT_DATE as 日期
  from outp_bill_items v
  right  join (select * from outp_rcpt_master where  rcpt_no in (select REFUNDED_RCPT_NO from  outp_rcpt_master where rcpt_no='{0}')   ) m on v.rcpt_no = m.rcpt_no ";
            sql = string.Format(sql, RcptNo);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 住院明细查询
        /// </summary>
        /// <param name="starDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptID"></param>
        /// <param name="DoctorID"></param>
        /// <param name="PactType"></param>
        /// <param name="ItemCode"></param>
        /// <param name="InpnoNo"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public DataTable QueryInPatientDeptItem(string starDate, string endDate, string deptID, string DoctorID, string PactType, string ItemCode, string InpnoNo, string Name, string RCPT_NO)
        {
            //2013-5-9 by li 医保类别项目置空
            string sql = @"select 
       i.patient_id 病人编号,
       i.visit_id as 序号,
       m.name as 病人姓名,
       m.inp_no as 病人住院号,
       p.charge_type as 收费类别,
        -- case when p.charge_type='医疗保险' and p.insurance_type ='71' then '居民普通住院'
            -- when p.charge_type='医疗保险' and p.insurance_type='21'    then '职工普通住院'
            -- when p.charge_type='医疗保险' and p.insurance_type='38'   then '职工门诊转住院'
            -- when p.charge_type='医疗保险' and p.insurance_type='7E'   then '居民转住院'
        -- else  p.insurance_type end as   医保类别,
        '' as   医保类别,
        (select u.user_name
          from users_staff_dict u
         where u.user_id =  i.doctor )    as 开单医生,
        (select d.dept_name from dept_dict d where d.dept_code = i.ordered_by)  as 开单科室,       
       i.item_code 项目代码,
       i.item_name 项目名称, 
       i.item_spec as 规格,
       i.amount  as 数量,
       to_char(i.costs, '999999999999.99') as 金额,
       (select d.dept_name from dept_dict d where d.dept_code =  i.performed_by)  执行科室,
       i.billing_date_time as 执行时间,--执行时间
       i.RCPT_NO as 发票号
  from inp_bill_detail i --住院收费明细;
  left join pat_visit p on p.patient_id=i.patient_id and p.visit_id=i.visit_id
 left join pat_master_index m on m.patient_id=p.patient_id
  where 1=1 and ('{0}' is null or m.name like  '%{0}%')
  and  ('{1}' is null or m.inp_no =  '{1}')
  and ('{2}' is null or p.charge_type =  '{2}')
  and ('{3}' is null or i.item_code =  '{3}')
  and ('{4}' is null or i.doctor =  '{4}')
  and ('{5}' is null or i.ordered_by =  '{5}')
  and (i.billing_date_time>=to_date('{6}','yyyy-MM-dd hh24:mi:ss') or '{6}' is null  or  substr('{6}',1,4)='0001')
and (i.billing_date_time<=to_date('{7}','yyyy-MM-dd hh24:mi:ss') or '{7}' is null  or  substr('{7}',1,4)='0001') 
  and ('{8}' is null or i.RCPT_NO =  '{8}')";
            sql = string.Format(sql, Name, InpnoNo, PactType, ItemCode, DoctorID, deptID, starDate, endDate, RCPT_NO);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        ///查询科室医生药品项目数量
        /// </summary>
        /// <returns></returns>
        public DataSet QueryDeptDoctorDrugItem(string starDate, string endDate, string deptID, string DoctorID)
        {
            //2013-5-9 by li 视图修改 v_fee_statistic --> v_fee_statistic_new
            string sql = @"select m.item_code as 项目代码,
       m.item_name as 项目名称,
        m.cn as 数量,
       to_char(m.cos, '999999999999.99') as 金额,
       (select d.dept_name from dept_dict d where d.dept_code = m.order_dept) as 开单科室,
       (select u.user_name
          from users_staff_dict u
         where u.user_id = m.order_doctor) as 开单医生
       
  from (select v.item_code,
               v.item_name,
               v.order_dept,
               v.order_doctor,
               sum(v.amount) as cn,
               sum(v.costs) as cos
          from v_fee_statistic_new v
         where v.operdate >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
           and v.operdate <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') and v.item_class in('A','B')";
            if (DoctorID.Trim() != "")
            {
                sql = sql + " and v.order_doctor='" + DoctorID + "'";
            }
            if (deptID.Trim() != "")
            {
                sql = sql + " and v.order_dept='" + deptID + "'";
            }
            sql = sql + " group by v.item_code, v.item_name, v.order_dept, v.order_doctor) m order by 开单科室,开单医生,数量";
            sql = string.Format(sql, starDate, endDate);
            return BaseEntityer.Db.GetDataSet(sql);
        }

        /// <summary>
        /// 获得药品、检查的价表项目
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetAllPriceListItem()
        {
            string sql = @"SELECT t.*,
                       item_class as class_type
  from current_price_list t";
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 收费类别字典
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetChargeTypeListItem()
        {
            string sql = @"select * from charge_type_dict";
            return BaseEntityer.Db.GetDataTable(sql);

        }


        #endregion  lql
        #region 工资核算
        /// <summary>
        /// 统计医生工资
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.JxDoctorSalary> AccountDoctorSalary(string dateStart, string dateEnd)
        {
            string sql = @"select t.user_id,
       t.user_name,
       t.dept_code,
       t.dept_name,
       to_char(t.salary,999999999.99) as salary,
       t.type,
       t.bl,
       to_char( decode(t.type,'医疗收入',round(sum(cost),2)-t.payment, round(sum(cost),2) ),'999999999.99') as cost,
       to_char( decode(t.type,'医疗收入',round(((sum(cost)-t.payment )* bl) / 100,2),round(sum((cost * bl) / 100),2) ) ,'999999999.99')as tc
  from v_jx_salay t
 where t.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
 and t.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') and t.type<>'药品收入'
 group by t.user_id,
          t.user_name,
          t.dept_code,
          t.dept_name,
          t.salary,
          t.type,
          t.bl,
          t.payment
          order by dept_code";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.JxDoctorSalary>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 获取已经录入基本工资的医生的信息。
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.JxDoctorDict> GetJXDoctorDic()
        {
            string sql = @"select * from jx_doctor_dict";

            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.JxDoctorDict>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }

        /// <summary>
        /// 插入jx_doctor_dict表数据
        /// </summary>
        /// <param name="o">jx_doctor_dict数据</param>
        /// <returns></returns>
        public int InsertDoctorDict(HisCommon.DataEntity.Report.JxDoctorDict o, BaseEntityer db)
        {
            string sql = @"insert into jx_doctor_dict (USER_ID,USER_NAME,DEPT_CODE,DEPT_NAME,SALARY,PAYMENT)
                      values('{0}','{1}','{2}','{3}','{4}','{5}')";
            sql = string.Format(sql, o.USER_ID, o.USER_NAME, o.DEPT_CODE, o.DEPT_NAME, o.SALARY, o.PAYMENT);
            return db.ExecuteNonQuery(sql);

        }
        /// <summary>
        /// 删除一条jx_doctor_dict表数据
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db">jx_doctor_dict表数据</param>
        /// <returns></returns>
        public int DeleteDoctorDict(HisCommon.DataEntity.Report.JxDoctorDict o, BaseEntityer db)
        {
            string sql = @"delete from jx_doctor_dict t
                   where t.USER_ID='{0}'";
            sql = string.Format(sql, o.USER_ID);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 获得所有检查项目
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.JxExamItem> GetExamList()
        {
            string sql = @" SELECT t.DESCRIPTION_CODE,
s.item_code,
s.item_class,
       t.DESCRIPTION,
       t.EXAM_SUB_CLASS,
       t.EXAM_CLASS,
       sum(s.price*c.amount) as price,
       t.DESC_ITEM,
       t.INPUT_CODE
  FROM EXAM_CLASS_DICT a
      left join EXAM_SUBCLASS_DICT b on a.exam_class_name=b.exam_class_name
      left join EXAM_RPT_PATTERN  t on t.exam_class=b.exam_class_name and t.exam_sub_class=b.exam_subclass_name
      left join CLINIC_VS_CHARGE c on c.clinic_item_code=t.description_code and c.clinic_item_class='D' --and c.charge_item_no=1
      left join current_price_list s on s.item_code=c.charge_item_code and s.item_class=c.charge_item_class
      --left join perform_dept p on p.item_code=c.charge_item_code and p.item_class=c.charge_item_class
      --left join dept_dict d on d.dept_code=p.performed_by
 where t.DESC_ITEM like '%检查项目'
 group by
       t.DESCRIPTION_CODE,
       t.DESCRIPTION,
       t.EXAM_SUB_CLASS,
       t.EXAM_CLASS,
       t.DESC_ITEM,
       t.INPUT_CODE,
       s.item_code,
       s.item_class";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.JxExamItem>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 获得除药品、检查外的价表项目
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetUndrugItem()
        {
            string sql = @"SELECT t.*,
                       item_class as class_type
  from current_price_list t 
where t.item_class not in ('A','B')";
            return BaseEntityer.Db.GetDataTable(sql);

        }

        /// <summary>
        /// 查询所有已对照的项目
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.JxItemDict> GetJxItemDict()
        {
            string sql = @"select * from jx_item_dict t";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.JxItemDict>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 插入QUERY_DEPT_COMPARE表数据
        /// </summary>
        /// <param name="o">QUERY_DEPT_COMPARE数据</param>
        /// <returns></returns>
        public int InsertJxItemDict(HisCommon.DataEntity.Report.JxItemDict o, BaseEntityer db)
        {
            string sql = @"insert into JX_ITEM_DICT (JX_ID,JX_NAME,ITEM_CODE,ITEM_NAME,ITEM_CLASS,OPER)
values('{0}','{1}','{2}','{3}','{4}','{5}')";
            sql = string.Format(sql, o.JX_ID, o.JX_NAME, o.ITEM_CODE, o.ITEM_NAME, o.ITEM_CLASS, o.OPER);
            return db.ExecuteNonQuery(sql);

        }
        /// <summary>
        /// 删除一条QUERY_DEPT_COMPARE表数据
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db">QUERY_DEPT_COMPARE表数据</param>
        /// <returns></returns>
        public int DeleteJxItemDict(HisCommon.DataEntity.Report.JxItemDict o, BaseEntityer db)
        {
            string sql = @"delete from JX_ITEM_DICT t
where t.JX_NAME='{0}'
and t.ITEM_CODE='{1}'
and t.ITEM_NAME='{2}'
and t.ITEM_CLASS='{3}'";
            sql = string.Format(sql, o.JX_NAME, o.ITEM_CODE, o.ITEM_NAME, o.ITEM_CLASS);
            return db.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 查询所有已维护的项目比例
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.JxDeptItemBl> GetJxDeptItemBl()
        {
            string sql = @"select * from jx_dept_item_bl t";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.JxDeptItemBl>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 插入jx_dept_item_bl表数据
        /// </summary>
        /// <param name="o">jx_dept_item_bl数据</param>
        /// <returns></returns>
        public int InsertJxDeptItemBl(HisCommon.DataEntity.Report.JxDeptItemBl o, BaseEntityer db)
        {
            string sql = @"insert into jx_dept_item_bl (DEPT_CODE,DEPT_NAME,JX_ID,JX_NAME,BL)
values('{0}','{1}','{2}','{3}','{4}')";
            sql = string.Format(sql, o.DEPT_CODE, o.DEPT_NAME, o.JX_ID, o.JX_NAME, o.BL);
            return db.ExecuteNonQuery(sql);

        }
        /// <summary>
        /// 删除一条jx_dept_item_bl表数据
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db">jx_dept_item_bl表数据</param>
        /// <returns></returns>
        public int DeleteJxDeptItemBl(HisCommon.DataEntity.Report.JxDeptItemBl o, BaseEntityer db)
        {
            string sql = @"delete from jx_dept_item_bl t
where t.DEPT_CODE='{0}'
and t.JX_ID='{1}'";
            sql = string.Format(sql, o.DEPT_CODE, o.JX_ID);
            return db.ExecuteNonQuery(sql);
        }
        #endregion
        #region 门诊费用查询
        /// <summary>
        /// 门诊费用查询
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable OutpFeeQuery(string dateFeeStart, string dateFeeEnd, string dateActoStart, string dateActoEnd)
        {
            //2013-5-9 by li 医保类别项目置空
            string sql = @"select t.rcpt_no as 收据号,
       t.invoice_new as 发票号,
       t.patient_id as 病人ID,
       t.name as 姓名,
        --case when t.charge_type='医疗保险' and t.insurance_type='12' then '职工特殊门诊'
        --     when t.charge_type='医疗保险' and t.insurance_type='11'    then '职工普通门诊'
        --     when t.charge_type='医疗保险' and t.insurance_type='7N'   then '居民低保门诊'
        --     when t.charge_type='医疗保险' and t.insurance_type='79'   then '居民特殊门诊'
        --     when t.charge_type='医疗保险' and t.insurance_type ='70'   then '居民门诊慢性病'
        -- else t.charge_type end as 类别,
        t.charge_type as 类别,
       t.visit_date as 收费时间,
       to_char(t.total_costs,'999999999.99') as 总费用,
       to_char(t.total_charges,'999999999.99') as 应收费用,
       u.user_name as 收款员,
       decode(t.charge_indicator,0,'已收费',2,'退费',1,'欠费') as 收费标志,
              t.acct_no as 结算单号,
              m.acct_date as 结算时间
       from OUTP_RCPT_MASTER t
left join users_staff_dict u on t.operator_no=u.user_id
left join outp_acct_master m on t.acct_no=m.acct_no
where --(t.charge_indicator != 2 or t.total_costs<0) and
 t.visit_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
and t.visit_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
and (m.acct_date>=to_date('{2}','yyyy-MM-dd hh24:mi:ss') or '{2}' is null  or  substr('{2}',1,4)='0001')
and (m.acct_date<=to_date('{3}','yyyy-MM-dd hh24:mi:ss') or '{3}' is null  or  substr('{3}',1,4)='0001')
";
            sql = string.Format(sql, dateFeeStart, dateFeeEnd, dateActoStart, dateActoEnd);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 门诊费用项目明细查询
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable OutpFeeDetailQuery(string rcptNo)
        {
            string sql = @" select O.*,r.fee_class_name,D.DEPT_NAME from outp_bill_items O
 LEFT JOIN DEPT_DICT D ON O.PERFORMED_BY=D.DEPT_CODE
 left join OUTP_RCPT_FEE_DICT r on o.class_on_rcpt=r.fee_class_code
 where O.rcpt_no='{0}'
";
            sql = string.Format(sql, rcptNo);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 门诊费用项目类别查询
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable OutpFeeTypeQuery(string rcptNo)
        {
            string sql = @"SELECT t.MONEY_TYPE,
       t.PAYMENT_AMOUNT - t.REFUNDED_AMOUNT as cost,
       t.RCPT_NO,
       t.PAYMENT_AMOUNT,
       t.REFUNDED_AMOUNT,
       t.PAYMENT_NO
  FROM OUTP_PAYMENTS_MONEY t
 WHERE t.RCPT_NO = {0}
 ORDER BY t.PAYMENT_NO ASC
";
            sql = string.Format(sql, rcptNo);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        #endregion
        /// <summary>
        /// 门诊医生挂号量统计
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable DoctorAdmisAccount(string dateStart, string dateEnd)
        {
            string sql = @"select u.user_id,
       u.user_name,
       sum(t.clinic_charge) as cost,
       count(*) as num
  from clinic_master t
  left join clinic_admis a
    on t.visit_date = a.visit_date
   and t.visit_no = a.visit_no
  left join users_staff_dict u
    on a.doctor = u.user_id
 where t.admis = 1
 and a.admis_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
 and a.admis_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
 group by u.user_id, u.user_name
";
            sql = string.Format(sql, dateStart, dateEnd);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 医生收费统计
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.DoctorStatisticClass> DoctorAccountStatistic(string dateStart, string dateEnd, string type)
        {
            //2013-5-9 by li 视图修改 v_fee_statistic --> v_fee_statistic_new
            //2013-12-7 by li 医生工作量统计会计科目改成核算科目
            //2014-2-20 by li 修正医生工作量相同医生相同核算科目下数据多条问题，数据合并成一条
            string sql = @"select aaa.ID,
                               aaa.姓名,
                               aaa.科室,
                               sum(aaa.收入总额) as 收入总额,
                               sum(aaa.医疗收入) as 医疗收入,
                               sum(aaa.药品收入) as 药品收入,
                               aaa.类型,
                               aaa.是否药品,
                               aaa.类型代码
                          from (select o.order_doctor as ID,
                                       nvl(u.user_name, o.order_doctor) as 姓名,
                                       d.dept_name as 科室,
                                       to_char(sum(o.costs), '999999999.99') as 收入总额,
                                       to_char(sum(decode(o.item_class, 'A', 0, 'B', 0, o.costs)),
                                               '999999999.99') as 医疗收入,
                                       to_char(sum(decode(o.item_class,
                                                          'A',
                                                          o.costs,
                                                          'B',
                                                          o.costs,
                                                          0)),
                                               '999999999.99') as 药品收入,
                                       o.recktype as 类型,
                                       decode(o.item_class, 'A', 'Y', 'B', 'Y', 'N') as 是否药品,
                                       o.class_on_reckoning as 类型代码
                                  from v_fee_statistic_new o
                                  left join users_staff_dict u
                                    on u.user_id = o.order_doctor
                                  left join dept_dict d
                                    on u.user_dept = d.dept_code
                                 where o.operdate >=
                                       to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                   and o.operdate <=
                                       to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                                   and (o.type = '{2}' or '{2}' is null)
                                 group by o.order_doctor,
                                          u.user_name,
                                          d.dept_name,
                                          o.recktype,
                                          o.item_class,
                                          o.class_on_reckoning
                                 order by 科室) aaa
                         group by aaa.ID, aaa.姓名, aaa.科室, aaa.类型, aaa.是否药品, aaa.类型代码";
            sql = string.Format(sql, dateStart, dateEnd, type);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.DoctorStatisticClass>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 2013-12-7 by li 医生工作量提成统计
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="type">门诊住院类别</param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.DoctorStatisticClass> DoctorAccountScaleStatistic(string dateStart, string dateEnd, string type)
        {
            //2014-4-17 by li 统计数据分组错误导致数据统计不唯一
            string sql = @"select o.order_doctor as ID,
                               nvl(u.user_name, o.order_doctor) as 姓名,
                               d.dept_name as 科室,
                               to_char(sum(o.costs) * o.scale, '999999999.99') as 收入总额,
                               o.recktype as 类型,
                               decode(o.item_class, 'A', 'Y', 'B', 'Y', 'N') as 是否药品,o.class_on_reckoning as 类型代码
                          from v_fee_statistic_new o
                          left join users_staff_dict u
                            on u.user_id = o.order_doctor
                          left join dept_dict d
                            on u.user_dept = d.dept_code
                         where o.operdate >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                           and o.operdate <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                           and (o.type = '{2}' or '{2}' is null)
                         group by o.order_doctor,
                                  u.user_name,
                                  d.dept_name,
                                  o.recktype,
                                  decode(o.item_class, 'A', 'Y', 'B', 'Y', 'N'),
                                  o.scale,class_on_reckoning
                         order by 科室";
            sql = string.Format(sql, dateStart, dateEnd, type);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.DoctorStatisticClass>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 科室收费统计
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.DeptStatisticClass> OrderDeptAccountStatistic(string dateStart, string dateEnd, string type, string accountType, string performby)
        {
            //2013-5-10 by li 视图修改 v_fee_statistic --> v_fee_statistic_new
            //2013-6-19 by li 增加科目类别统计参数accountType，分为会计科目-参数为（SUBJ），核算科目-参数为（RECK）
            //2013-12-13 by li 增加核算科目代码（排序用）
            //2013-12-27 by li 挂号费统计查询视图中不能使用就诊日期
            //(退费就诊日期会产生相同日期的负数据)，使用挂号日期进行统计
            string sql = string.Empty;
            string str = @"(
                        select 
                        order_dept,
                        costs,
                        item_class,
                        feetype,
                        recktype,class_on_reckoning

                        from 
                        v_fee_statistic_new vf 
                        where vf.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                        and  vf.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                        and (vf.type='{2}' or '{2}' is null) and (vf.performed_by ='{3}' or '{3}' is null)
                        union all
                        select 
                        m.就诊科室代码  order_dept,
                        m.挂号费  costs,
                        'unDrug' item_class,
                        '挂号费' feetype,
                        '挂号费' recktype,'H' class_on_reckoning
                        from v_clinic_master m
                        where m.挂号日期  >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                        and  m.挂号日期  <=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                        union all
                        select 
                        m.就诊科室代码  order_dept,
                        m.诊疗费   costs,
                        'unDrug' item_class,
                        '挂号诊查费' feetype,
                        '挂号诊查费' recktype,'H' class_on_reckoning
                        from v_clinic_master m
                        where m.挂号日期  >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                        and  m.挂号日期  <=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                        )";
            if (type == "I")
            {
                str = @"(
                        select 
                        order_dept,
                        costs,
                        item_class,
                        feetype,
                        recktype,class_on_reckoning
                        from 
                        v_fee_statistic_new vf
                        where vf.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                        and  vf.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                        and (vf.type='{2}' or '{2}' is null)  and (vf.performed_by ='{3}' or '{3}' is null)
                        )";
            }
            str = string.Format(str, dateStart, dateEnd, type, performby);
            if (accountType == "SUBJ")
                sql = @"select 代码,科室,to_char( sum(收入总额),'999999999.99') as 收入总额,
                                to_char( sum(医疗收入),'999999999.99') as 医疗收入,
                                to_char( sum(药品收入),'999999999.99') as 药品收入,
                                类型,
                                是否药品,类型代码
                        from(
                        select o.order_dept as 代码,
                               d.dept_name as 科室,
                               sum(o.costs) as 收入总额,
                               sum( decode(o.item_class,'A',0,'B',0,o.costs)) as 医疗收入,
                               sum(decode(o.item_class,'A',o.costs,'B',o.costs,0)) as 药品收入,
                               o.feetype as 类型,class_on_reckoning as 类型代码,
                               decode(o.item_class,'A','Y','B','Y','N') as 是否药品
                        from {0} o
                        left join dept_dict d on o.order_dept=d.dept_code
                        group by  o.order_dept, d.dept_name,o.feetype,o.item_class,class_on_reckoning
                        order by 科室
                        ) r
                        group by 代码,科室,类型,是否药品,类型代码";
            else
                sql = @"select 代码,科室,to_char( sum(收入总额),'999999999.99') as 收入总额,
                                to_char( sum(医疗收入),'999999999.99') as 医疗收入,
                                to_char( sum(药品收入),'999999999.99') as 药品收入,
                                类型,
                                是否药品,类型代码
                        from(
                        select o.order_dept as 代码,
                               d.dept_name as 科室,
                               sum(o.costs) as 收入总额,
                               sum( decode(o.item_class,'A',0,'B',0,o.costs)) as 医疗收入,
                               sum(decode(o.item_class,'A',o.costs,'B',o.costs,0)) as 药品收入,
                               o.recktype as 类型,class_on_reckoning as 类型代码,
                               decode(o.item_class,'A','Y','B','Y','N') as 是否药品
                        from {0} o
                        left join dept_dict d on o.order_dept=d.dept_code
                        group by  o.order_dept, d.dept_name,o.recktype,o.item_class ,class_on_reckoning
                        order by 科室
                        ) r
                        group by 代码,科室,类型,是否药品,类型代码";
            sql = string.Format(sql, str);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.DeptStatisticClass>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 执行科室收费统计
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.PerformedByStatisticClass> PerformedByAccountStatistic(string dateStart, string dateEnd, int deptAtr, string accountType,string deptcode,string type)
        {
            //2013-5-9 by li 视图修改 v_fee_statistic --> v_fee_statistic_new
            //2013-6-17 by li 去除query_dept_compare连表，原有连表为二院专用
            //2013-6-19 by li 增加科目类别统计参数accountType，分为会计科目-参数为（SUBJ），核算科目-参数为（RECK）
            string sql = string.Empty;
            if (accountType == "SUBJ")
                sql = @"select 代码,科室,to_char(sum(收入总额),'999999999.99') as 收入总额,类型,类型代码
                        from(
                        select o.performed_by as 代码,
                               d.dept_name as 科室,
                               sum(o.costs) as 收入总额,
                               o.feetype as 类型,
                               o.subj_code as 类型代码
                        from v_fee_statistic_new o
                        left join dept_dict d on o.performed_by=d.dept_code
                        where (d.clinic_attr={2} or {2}=9999)
                        and o.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                        and  o.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                        and (o.performed_by ='{3}' or '{3}' is null )
                        and (o.type= '{4}' or '{4}' is null)
                        group by  o.performed_by, d.dept_name, o.feetype,o.subj_code
                        order by 科室
                        ) r
                        group by 代码,科室,类型,类型代码";
            else
                sql = @"select 代码,科室,to_char(sum(收入总额),'999999999.99') as 收入总额,类型,类型代码
                        from(
                        select o.performed_by as 代码,
                               d.dept_name as 科室,
                               sum(o.costs) as 收入总额,
                               o.recktype as 类型,
                               o.class_on_reckoning as 类型代码
                        from v_fee_statistic_new o
                        left join dept_dict d on o.performed_by=d.dept_code
                        where (d.clinic_attr={2} or {2}=9999)
                        and o.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                        and  o.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                        and (o.performed_by ='{3}' or '{3}' is null )
                        and (o.type= '{4}' or '{4}' is null)
                        group by  o.performed_by, d.dept_name, o.recktype,o.class_on_reckoning
                        order by 科室
                        ) r
                        group by 代码,科室,类型,类型代码";
            sql = string.Format(sql, dateStart, dateEnd, deptAtr,deptcode,type);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.PerformedByStatisticClass>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 药局付药查询
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable DrugSaleStatistic(string dateStart, string dateEnd)
        {
            string sql = @"select r.日期,r.代码,r.名称,r.规格,r.单位,
sum(r.数量) as 数量,
r.药局代码,
sum(r.零售) as 零售,
sum(r.批发) as 批发,
r.药局 from
(
select   d.dispensing_date_time as 日期,
         d.drug_code as 代码,
         d.drug_name as 名称,
         d.drug_spec as 规格,
         d.package_units as 单位,
         d.dispense_amount as 数量,
         d.dispensary as 药局代码,
 sum(d.costs) as 零售,
         round(sum(d.trade_price * d.dispense_amount), 2) as 批发,
               decode(d.drug_indicator,
                      2,
                      c.dept_name || '/草药' ,
                      3,
                      c.dept_name || '/成药',
                      c.dept_name) as 药局
          from (select d.*, n.drug_indicator,n.drug_name
                  from drug_dispense_rec d
                  left join drug_dict n
                    on d.drug_code = n.drug_code
                   and d.drug_spec = n.drug_spec) d
          left join dept_dict c
            on d.dispensary = c.dept_code
         where d.dispensing_date_time >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
           and d.dispensing_date_time <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
         group by d.dispensary, c.dept_name, d.drug_indicator, d.dispensing_date_time,
          d.drug_code,
         d.drug_name,
         d.drug_spec,
         d.package_units ,
         d.dispense_amount --住院
        union all
        --门诊
        select d.dispensing_date as 日期,
         d.drug_code as 代码,
         d.drug_name as 名称,
         d.drug_spec as 规格,
         d.package_units as 单位,
         d.quantity as 数量,
         m.dispensary as 药局代码,
        sum(d.costs) as 零售,
               round(sum(d.trade_price * d.quantity), 2) as 批发,
               decode(d.drug_indicator,
                      2,
                      c.dept_name || '/草药' ,
                      3,
                      c.dept_name || '/成药',
                      c.dept_name ) as 药局
          from (select t.*, n.drug_indicator
                  from drug_presc_detail  t
                  left join drug_dict n
                    on t.drug_code = n.drug_code
                   and t.drug_spec = n.drug_spec) d
          left join drug_presc_master m
            on m.presc_no = d.presc_no 
            and m.presc_date =d.presc_date 
            and m.dispensing_date=d.dispensing_date
          left join dept_dict c
            on m.dispensary = c.dept_code
         where d.dispensing_date >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
           and d.dispensing_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
         group by m.dispensary, c.dept_name, d.drug_indicator,d.dispensing_date,
          d.drug_code,
         d.drug_name,
         d.drug_spec,
         d.package_units,
         d.quantity,
         m.dispensary
         ) r
         group by  r.日期,r.代码,r.名称,r.规格,r.单位,r.药局代码,r.药局 
         order by r.药局,r.日期
";
            sql = string.Format(sql, dateStart, dateEnd);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        #region 化验室统计
        /// <summary>
        /// 化验室门诊统计
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.LabStatisticClass> LabOutpStatistic(string dateStart, string dateEnd)
        {
            string sql = @"
 select    to_char(time,'yyyy-MM-dd') as 日期,
          to_char(sum(cost),'999999999999.99') as 金额,
          feetype 类别 from
 (
 select sum(o.costs) as cost, 
       s.class_name as feetype,
     trunc( t.visit_date) as time
  from outp_bill_items o
  left join  Outp_Rcpt_Master t
  on o.rcpt_no=t.rcpt_no--门诊收费明细
  left join  (select distinct subj_code,item_name,item_class,class_on_reckoning  from  current_price_list) c
  on o.item_name=c.item_name
  left join RECK_ITEM_CLASS_DICT s on s.class_code=c.class_on_reckoning
  where c.item_class='C' 
  and t.visit_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
  and t.visit_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
  group by s.class_name,o.item_class,t.visit_date
  ) r group by r.time,feetype
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.LabStatisticClass>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 化验室住院统计
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.LabStatisticClass> LabInpStatistic(string dateStart, string dateEnd)
        {
            string sql = @"
 select    to_char(time,'yyyy-MM-dd') as 日期,
          to_char(sum(cost),'999999999999.99') as 金额,
          feetype 类别 from
 (
 select sum(o.costs) as cost, 
       s.class_name as feetype,
     trunc( o.billing_date_time) as time
  from inp_bill_detail o
  left join  (select distinct subj_code,item_name,item_class,class_on_reckoning  from  current_price_list) c
  on o.item_name=c.item_name
  left join RECK_ITEM_CLASS_DICT s on s.class_code=c.class_on_reckoning
  where c.item_class='C' 
  and o.billing_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
  and o.billing_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
  group by s.class_name,o.item_class,o.billing_date_time
  ) r group by r.time,feetype
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.LabStatisticClass>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        #endregion
        #region 门诊结算对账
        /// <summary>
        ///收款员结算查询outp_acct_master
        /// </summary>
        /// <returns></returns>
        public DataTable QuerySFAccount(string dateStart, string dateEnd, string userID)
        {
            string sql = @"select o.acct_no as 结账序号,
       o.acct_date as 结账日期,
       o.rcpts_num as 收据张数,
       o.refund_num as 退费张数,
       to_char(o.refund_amount,'999999999.99') as 退费金额,
       to_char(o.total_costs,'999999999.99') as 计价总额,
       to_char(o.total_incomes,'999999999.99') as 应收总额,
       u.user_name as 收款员,
       o.operator_no as 账号
 from outp_acct_master o 
 left join users_staff_dict u
 on o.operator_no=u.user_id
 where o.acct_date>=to_date('{0}','yyyy-MM-dd')
 and o.acct_date<=to_date('{1}','yyyy-MM-dd')
 and (o.operator_no='{2}' or '{2}' is null)
 order by 结账日期
";
            sql = string.Format(sql, dateStart, dateEnd, userID);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 在outp_acct_master中，根据结账单号查询指定单据信息
        /// </summary>
        /// <returns></returns>
        public HisCommon.DataEntity.OUTP_ACCT_MASTER QueryOutpAcctMasterByNo(string acct_no)
        {
            string sql = @"select * from outp_acct_master t
                           where t.acct_no='{0}'";
            sql = string.Format(sql, acct_no);
            var list = DataSetToEntity.DataSetToT<HisCommon.DataEntity.OUTP_ACCT_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
            if (list.Count == 1)
            {
                return list[0];
            }
            return null;
        }
        /// <summary>
        /// 在OUTP_ACCT_MONEY中，根据结账单号查询指定单据支付信息
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OUTP_ACCT_MONEY> QueryOutpAcctMoneysByNo(string acct_no)
        {
            string sql = @"select * from outp_acct_money t
                           where t.acct_no='{0}'";
            sql = string.Format(sql, acct_no);
            var list = DataSetToEntity.DataSetToT<HisCommon.DataEntity.OUTP_ACCT_MONEY>(BaseEntityer.Db.GetDataSet(sql)).ToList();
            return list;
        }
        /// <summary>
        /// 在OUTP_ACCT_DETAIL中，根据结账单号查询指定单据明细信息
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OUTP_ACCT_DETAIL> QueryOutpAcctDetailsByNo(string acct_no)
        {
            string sql = @"select t.*,d.subj_name from outp_acct_detail t
                          left join TALLY_SUBJECT_DICT d 
                                 on t.tally_fee_class=d.subj_code
                           where t.acct_no='{0}'";
            sql = string.Format(sql, acct_no);
            var list = DataSetToEntity.DataSetToT<HisCommon.DataEntity.OUTP_ACCT_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
            return list;
        }
        #endregion
        #region 住院结算对账
        /// <summary>
        ///住院结算查询inp_acct_master
        /// </summary>
        /// <returns></returns>
        public DataTable QueryInpSFAccount(string dateStart, string dateEnd)
        {
            string sql = @"select o.acct_no as 结账序号,
       o.acct_date as 结账日期,
       o.rcpts_num as 收据张数,
       o.refunded_num as 退费张数,
       '' as 退费金额,
       to_char(o.total_costs,'999999999.99') as 计价总额,
       to_char(o.total_incomes,'999999999.99') as 应收总额,
       u.user_name as 收款员
 from inp_acct_master o 
 left join users_staff_dict u
 on o.operator_no=u.user_id
 where o.acct_date>=to_date('{0}','yyyy-MM-dd')
 and o.acct_date<=to_date('{1}','yyyy-MM-dd')
 order by 结账日期
";
            sql = string.Format(sql, dateStart, dateEnd);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 在inp_acct_master中，根据结账单号查询指定单据信息
        /// </summary>
        /// <returns></returns>
        public HisCommon.DataEntity.INP_ACCT_MASTER QueryInpAcctMasterByNo(string acct_no)
        {
            string sql = @"select * from INP_ACCT_MASTER t
                           where t.acct_no='{0}'";
            sql = string.Format(sql, acct_no);
            var list = DataSetToEntity.DataSetToT<HisCommon.DataEntity.INP_ACCT_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
            if (list.Count == 1)
            {
                return list[0];
            }
            return null;
        }
        /// <summary>
        /// 在inp_ACCT_MONEY中，根据结账单号查询指定单据支付信息
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.INP_ACCT_MONEY> QueryInpAcctMoneysByNo(string acct_no)
        {
            string sql = @"select * from INP_ACCT_MONEY t
                           where t.acct_no='{0}'";
            sql = string.Format(sql, acct_no);
            var list = DataSetToEntity.DataSetToT<HisCommon.DataEntity.INP_ACCT_MONEY>(BaseEntityer.Db.GetDataSet(sql)).ToList();
            return list;
        }
        /// <summary>
        /// 在inp_ACCT_DETAIL中，根据结账单号查询指定单据明细信息
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.INP_ACCT_DETAIL> QueryInpAcctDetailsByNo(string acct_no)
        {
            string sql = @"
select t.*,d.subj_name from Inp_acct_detail t
                          left join TALLY_SUBJECT_DICT d 
                                 on t.subj_code=d.subj_code
                           where t.acct_no='{0}'";
            sql = string.Format(sql, acct_no);
            var list = DataSetToEntity.DataSetToT<HisCommon.DataEntity.INP_ACCT_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
            return list;
        }
        #endregion
        #region 财务汇总统计
        /// <summary>
        /// 门诊汇总明细查询
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.FeeDetailAccount> FeeOutpDetailAccountStatistic(string dateStart, string dateEnd)
        {
            string sql = @"select charge_type as 类别,
        to_char( sum(a.total_costs),'999999999.99') as 总金额,
       to_char( sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '现金支付')),'999999999.99') as 现金支付,
       to_char(sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '帐户支付')),'999999999.99') as 帐户支付,
       to_char(sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '大额支付')),'999999999.99') as 大额支付,
       to_char(sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '统筹支付')),'999999999.99') as 统筹支付,
       to_char(sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '公务员')),'999999999.99') as 公务员,
       to_char(sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '现金')),'999999999.99') as 现金
  from outp_rcpt_master a
  left join  outp_acct_master o on a.acct_no=o.acct_no
  where o.acct_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
and o.acct_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
and a.acct_no is not null
 and length(a.acct_no)>2 
 group by charge_type
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.FeeDetailAccount>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 住院汇总明细查询
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.FeeDetailAccount> FeeIntpDetailAccountStatistic(string dateStart, string dateEnd)
        {
            string sql = @"select p.charge_type as 类别,
       to_char( sum(a.costs),'999999999.99') as 总金额,
         to_char(  sum((select nvl( b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '现金支付')),'999999999.99')  as 现金支付,
        to_char(  sum((select nvl(b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '帐户支付')),'999999999.99')  as 帐户支付,
        to_char(  sum((select nvl(b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '大额支付')),'999999999.99')  as 大额支付,
        to_char(  sum((select nvl(b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '统筹支付')),'999999999.99')  as 统筹支付,
        to_char(  sum((select nvl(b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '公务员')),'999999999.99')  as 公务员,
        to_char(  sum((select nvl(b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '现金')),'999999999.99')  as 现金
from INP_SETTLE_MASTER a
left join pat_visit p on p.patient_id=a.patient_id and p.visit_id=a.visit_id
where a.transact_type!='作废'
and a.settling_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
and a.settling_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
group by  p.charge_type
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.FeeDetailAccount>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 门诊财务汇总分类查询
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.FeeTypeAccount> FeeOutpTypeAccountStatistic(string dateStart, string dateEnd)
        {
            string sql = @"select r.类别,r.费用分类,Sum(r.总金额) as 总金额 
from 
(select m.charge_type as 类别,
       t.feetype as 费用分类,
       sum(t.costs) as 总金额
       from v_fee_statistic t
left join outp_rcpt_master m on t.rcpt_no=m.rcpt_no
left join  outp_acct_master o on t.isjs=o.acct_no
where 
    t.isjs is not null
   and length(t.isjs)>2 
   and t.type='O'
  and  o.acct_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
 and o.acct_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
group by m.charge_type,t.feetype) r
group by r.类别,r.费用分类
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.FeeTypeAccount>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 住院财务汇总分类查询
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.FeeTypeAccount> FeeInpTypeAccountStatistic(string dateStart, string dateEnd)
        {
            string sql = @"  select r.类别,r.费用分类,Sum(r.总金额) as 总金额 
from 
(select p.charge_type as 类别,
        d.fee_class_name as 费用分类,
        sum(d.costs) as 总金额
  from INP_SETTLE_MASTER a
 left join inp_settle_detail d on a.rcpt_no=d.rcpt_no
 left join pat_visit p on p.patient_id=a.patient_id and p.visit_id=a.visit_id
where a.transact_type!='作废'
and a.costs!=0
and a.settling_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
and a.settling_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
group by p.charge_type,d.fee_class_name) r
group by r.类别,r.费用分类
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.FeeTypeAccount>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        #endregion
        #region 辽阳二院专用 财务汇总查询
        /// <summary>
        /// 门诊汇总明细查询
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.FeeDetailAccountLYEY> FeeOutpDetailAccountStatisticLYEY(string dateStart, string dateEnd)
        {
            string sql = @"select --charge_type as 类别,
                case when charge_type='医疗保险' and insurance_type='12' then '职工特殊门诊'
             when charge_type='医疗保险' and insurance_type='11'    then '职工普通门诊'
             when charge_type='医疗保险' and insurance_type='7N'   then '居民低保门诊'
             when charge_type='医疗保险' and insurance_type='79'   then '居民特殊门诊'
             when charge_type='医疗保险' and insurance_type ='70'   then '居民门诊慢性病'
         else charge_type end as 类别,
       sum(a.total_costs) as 总金额,
       sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '帐户支付')) as 帐户支付,
       sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '大额支付')) as 大额支付,
       sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '统筹支付')) as 统筹支付,
       sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '公务员')) as 公务员,
       sum((select nvl(b.payment_amount, 0)
             from outp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and (b.money_type = '现金' or b.money_type = '现金支付'))) as 现金
  from outp_rcpt_master a
  left join outp_acct_master m on a.acct_no=m.acct_no
  where m.acct_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
and m.acct_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
 and (a.acct_no is not null and length(a.acct_no)>2)
 group by charge_type,insurance_type
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.FeeDetailAccountLYEY>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 住院汇总明细查询
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.FeeDetailAccountLYEY> FeeIntpDetailAccountStatisticLYEY(string dateStart, string dateEnd)
        {
            string sql = @"select p.charge_type as 类别,
       sum(a.costs) as 总金额,
       sum((select nvl(b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '帐户支付')) as 帐户支付,
       sum((select nvl(b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '大额支付')) as 大额支付,
       sum((select nvl(b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '统筹支付')) as 统筹支付,
       sum((select nvl(b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and b.money_type = '公务员')) as 公务员,
       sum((select nvl(b.payment_amount-b.refunded_amount, 0)
             from inp_payments_money b
            where b.rcpt_no = a.rcpt_no
              and (b.money_type = '现金' or b.money_type = '现金支付') )) as 现金
from INP_SETTLE_MASTER a
left join pat_visit p on p.patient_id=a.patient_id and p.visit_id=a.visit_id
where a.transact_type!='作废'
and a.settling_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
and a.settling_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
group by  p.charge_type
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.FeeDetailAccountLYEY>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 门诊财务汇总分类查询
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.FeeTypeAccountLYEY> FeeOutpTypeAccountStatisticLYEY(string dateStart, string dateEnd)
        {
            string sql = @"select r.类别,r.费用分类,Sum(r.总金额) as 总金额 
from 
(
select   case when charge_type='医疗保险' and insurance_type='12' then '职工特殊门诊'
             when charge_type='医疗保险' and insurance_type='11'    then '职工普通门诊'
             when charge_type='医疗保险' and insurance_type='7N'   then '居民低保门诊'
             when charge_type='医疗保险' and insurance_type='79'   then '居民特殊门诊'
             when charge_type='医疗保险' and insurance_type ='70'   then '居民门诊慢性病'
         else charge_type end as 类别,
       m.feetype as 费用分类,
       sum(m.costs) as 总金额
       from v_fee_statistic m 
       left join outp_acct_master o on m.isjs=o.acct_no
where 
  (m.isjs is not null and length(m.isjs)>2)
   and m.type='O'
   and  o.acct_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
   and o.acct_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
group by m.charge_type,m.feetype,m.insurance_type, tb_flag) r
group by r.类别,r.费用分类
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.FeeTypeAccountLYEY>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 住院财务汇总分类查询
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.FeeTypeAccountLYEY> FeeInpTypeAccountStatisticLYEY(string dateStart, string dateEnd)
        {
            string sql = @" select r.类别,r.费用分类,Sum(r.总金额) as 总金额 
from 
(
 select case 
             when charge_type='医疗保险' and insurance_type='71' then '居民普通住院'
             when charge_type='医疗保险' and insurance_type='21' then '职工普通住院'
             when charge_type='医疗保险' and insurance_type='38' then '职工门诊转住院'
             when charge_type='医疗保险' and insurance_type ='7E' then '居民转住院'
         else charge_type end as 类别,
       t.feetype as 费用分类,
       sum(t.costs) as 总金额
       from v_fee_statistic t
where  t.type='I'
 and  t.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
  and t.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
group by charge_type,t.feetype,insurance_type) r
group by r.类别,r.费用分类
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.FeeTypeAccountLYEY>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        #endregion
        #region 查询科室对照
        /// <summary>
        /// 查询查询科室对照表
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.Report.QUERY_DEPT_COMPARE> GetDeptCompares()
        {
            string sql = @"select * from QUERY_DEPT_COMPARE t";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.QUERY_DEPT_COMPARE>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 插入QUERY_DEPT_COMPARE表数据
        /// </summary>
        /// <param name="o">QUERY_DEPT_COMPARE数据</param>
        /// <returns></returns>
        public int InsertQueryDeptCompare(HisCommon.DataEntity.Report.QUERY_DEPT_COMPARE o, BaseEntityer db)
        {
            string sql = @"insert into QUERY_DEPT_COMPARE (COMPARE_NAME,DEPT_NAME,DEPT_CODE,TAG,NAME,COMPARE_ID)
values('{0}','{1}','{2}','{3}','{4}','{5}')";
            sql = string.Format(sql, o.COMPARE_NAME, o.DEPT_NAME, o.DEPT_CODE, o.TAG, o.NAME, o.COMPARE_ID);
            return db.ExecuteNonQuery(sql);

        }
        /// <summary>
        /// 删除一条QUERY_DEPT_COMPARE表数据
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db">QUERY_DEPT_COMPARE表数据</param>
        /// <returns></returns>
        public int DeleteQueryDeptCompare(HisCommon.DataEntity.Report.QUERY_DEPT_COMPARE o, BaseEntityer db)
        {
            string sql = @"delete from QUERY_DEPT_COMPARE t
where t.dept_code='{0}'
and t.dept_name='{1}'";
            sql = string.Format(sql, o.DEPT_CODE, o.DEPT_NAME);
            return db.ExecuteNonQuery(sql);
        }
        #endregion
        public DataTable GetClinicInfoByOper(string dateStart, string dateEnd, string Oper, string ClinicType, string patientName,string Invoice)
        {
            string sql = @"select 票据号,就诊日期  ,  就诊序号 , 号别  , 病人标识号  ,  姓名  , 性别  ,  年龄  ,   身份  ,  费别  ,
           医疗保险号, 号类, 初诊标志,就诊科室,挂号费,诊疗费, 其他费 , 实收费用,挂号日期,类型, (select u.user_name
          from users_staff_dict u
          where u.user_id =  挂号员) 挂号员  ,是否接诊 
          from  v_clinic_master   where  挂号日期 >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
          and 挂号日期 <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') and (挂号员='{2}' or '{2}' is null) 
          and (类型='{3}' or '{3}'= '全部') and (姓名 like '%{4}%' or '{4}'= 'ALL') and (票据号 = '{5}' or '{5}'= 'ALL')
          order by 挂号日期";
            sql = string.Format(sql, dateStart, dateEnd, Oper, ClinicType, patientName, string.IsNullOrEmpty(Invoice) ? "ALL" : Invoice);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 查询门诊未发药记录
        /// </summary>
        /// <param name="Deptno"></param>
        /// <returns></returns>
        public DataTable GetNoSendDrugByDept(string DeptNo)
        {
            string sql = @"select PRESC_DATE  处方日期,
            PRESC_NO  处方号,
            ITEM_NO  项目序号,
            DRUG_CODE  药品代码,
            DRUG_SPEC  药品规格,
            DRUG_NAME  药品名称,
            FIRM_ID  厂商标识,
            PACKAGE_SPEC  包装规格,
            PACKAGE_UNITS  单位,
            QUANTITY  数量,
            COSTS  费用,
            PAYMENTS	实付费用,
             (select dept_name
                      from dept_dict d
                     where d.dept_code = DISPENSARY)  药局 
                     from drug_presc_detail_temp
            where (DISPENSARY='{0}' or '{0}' is null) ";
            sql = string.Format(sql, DeptNo);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        public DataTable GetClinicItem()
        {
            string sql = @"select 
                d.item_code as 项目编码,
                d.item_class as 类别,
                d.item_name as 项目名称,
                c.CHARGE_ITEM_CODE as 价表项目编码,
               p.item_name as 价表项目名称,
              p.price*c.amount as 项目价格,
              d.input_code as 输入码
                from clinic_item_name_dict d
                left join CLINIC_VS_CHARGE c
                on c.clinic_item_code = d.item_code
                and c.clinic_item_class = d.item_class
                left join current_price_list p
                on p.item_code = c.charge_item_code
                and p.item_class = c.charge_item_class
                where d.item_class not in ('A', 'B') ";
            return BaseEntityer.Db.GetDataTable(sql);
        }
        #endregion

        #region 浑南新区医院报表服务

        /// <summary>
        /// 医生收费统计
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="type">检索类型-全部、门诊、住院</param>
        /// <param name="deptCode">科室代码</param>
        /// <returns></returns>
        public List<DoctorStatisticClass> DoctorStatistic(string dateStart, string dateEnd, string type, string deptCode)
        {
            //2013-7-5 by li 科室连表原关系为用户所在科室，修改为收费项目开单科室连表
            string sql = string.Empty;
            if (type != null)
            {
                sql = @" select o.order_doctor as ID,
                           nvl(u.user_name,o.order_doctor) as 姓名,
                           d.dept_name as 科室,
                          to_char(sum(o.costs),'999999999.99') as 收入总额,
                           to_char(sum( decode(o.item_class,'A',0,'B',0,o.costs)),'999999999.99') as 医疗收入,
                           to_char( sum(decode(o.item_class,'A',o.costs,'B',o.costs,0)),'999999999.99') as 药品收入,
                            o.feetype as 类型,
                           decode(o.item_class,'A','Y','B','Y','N') as 是否药品
                    from v_fee_statistic_new o
                    left join users_staff_dict u on u.user_id=o.order_doctor 
                    left join dept_dict d on o.order_dept=d.dept_code
                    where o.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    and  o.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                    and (o.type='{2}' or '{2}' is null)
                    group by  o.order_doctor,u.user_name, d.dept_name,o.feetype,o.item_class
                    order by 科室";
                sql = string.Format(sql, dateStart, dateEnd, type);
            }
            else
            {
                sql = @" select o.order_doctor as ID,
                           nvl(u.user_name,o.order_doctor) as 姓名,
                           d.dept_name as 科室,
                          to_char(sum(o.costs),'999999999.99') as 收入总额,
                           to_char(sum( decode(o.item_class,'A',0,'B',0,o.costs)),'999999999.99') as 医疗收入,
                           to_char( sum(decode(o.item_class,'A',o.costs,'B',o.costs,0)),'999999999.99') as 药品收入,
                            o.feetype as 类型,
                           decode(o.item_class,'A','Y','B','Y','N') as 是否药品
                    from v_fee_statistic_new o
                    left join users_staff_dict u on u.user_id=o.order_doctor 
                    left join dept_dict d on o.order_dept=d.dept_code
                    where o.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    and  o.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                    and (o.order_dept='{2}')
                    group by  o.order_doctor,u.user_name, d.dept_name,o.feetype,o.item_class
                    order by 科室";
                sql = string.Format(sql, dateStart, dateEnd, deptCode);
            }
            return DataSetToEntity.DataSetToT<DoctorStatisticClass>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 视图统计v_fee_statistic_new
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="type">检索类型-全部、门诊、住院</param>
        /// <param name="order_dept">开单科室代码</param>
        /// <param name="performed_by">执行科室代码</param>
        /// <param name="order_doctor">开单医生</param>
        /// <param name="sum_flag">汇总标志</param>
        /// <returns></returns>
        public FEE_DETAIL[] VFeeStatistic(string dateStart,
                                                string dateEnd,
                                                string type,
                                                string order_doctor,
                                                string order_dept,
                                                string performed_by,
                                                string sum_flag)
        {
            string sql = string.Empty;
            if (sum_flag==string.Empty)//明细
            {
            sql = @"select * from v_fee_statistic_new v  
where (v.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss') or '{0}' is null)
and (v.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') or '{1}' is null)
and (v.type='{2}' or '{2}' is null)
and (v.order_doctor='{3}' or '{3}' is null)
and (v.order_dept='{4}' or '{4}' is null)
and (v.performed_by='{5}' or '{5}' is null)";
            }
            else if (sum_flag=="PERFORM") //执行科室
            {

                sql = @"select v.performed_by,v.subj_code,sum(costs) costs from v_fee_statistic_new v  ";
sql =sql +@"where (v.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss') or '{0}' is null)
and (v.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') or '{1}' is null)
and (v.type='{2}' or '{2}' is null)
and (v.order_doctor='{3}' or '{3}' is null)
and (v.order_dept='{4}' or '{4}' is null)
and (v.performed_by='{5}' or '{5}' is null) group by v.performed_by,subj_code";

            }
            sql = string.Format(sql, dateStart, dateEnd, type, order_doctor, order_dept, performed_by);
            return DataSetToEntity.DataSetToT<FEE_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 视图统计v_drug_fee_statistic_new
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="type">检索类型-全部、门诊、住院</param>
        /// <param name="order_dept">开单科室代码</param>
        /// <param name="performed_by">执行科室代码</param>
        /// <param name="order_doctor">开单医生</param>
        /// <returns></returns>
        public DRUG_FEE_DETAIL[] VDrugFeeStatistic(string dateStart,
                                                string dateEnd,
                                                string type,
                                                string order_doctor,
                                                string order_dept,
                                                string performed_by)
        {
            string sql = string.Empty;
            sql = @"select * from v_drug_fee_statistic_new v
where (v.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss') or '{0}' is null)
and (v.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') or '{1}' is null)
and (v.type='{2}' or '{2}' is null)
and (v.order_doctor='{3}' or '{3}' is null)
and (v.order_dept='{4}' or '{4}' is null)
and (v.performed_by='{5}' or '{5}' is null)";
            sql = string.Format(sql, dateStart, dateEnd, type, order_doctor, order_dept, performed_by);
            return DataSetToEntity.DataSetToT<DRUG_FEE_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 住院结算患者费用清单
        /// </summary>
        /// <param name="rcpt"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public OutBalanceFeeDetailsTotal[] OutBalanceFeeDetailsTotal(string rcpt, string errMsg)
        {
            List<OutBalanceFeeDetailsTotal> feeDetailsTotal = new List<HisCommon.DataEntity.Report.OutBalanceFeeDetailsTotal>();
            DataTable feeDetailsTotalSet = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = @"select t.fee_class_name  项目类别,
       t.ITEM_NAME 项目名称,
       t.ITEM_CODE 项目编码,
       t.ITEM_SPEC 规格,
       t.UNITS 单位,
       sum(t.AMOUNT) 数量,
       sum(t.COSTS) 计价金额,
       sum(t.CHARGES) 应收金额,
       t.item_grade 医保类别
  from OUTBALANCEFEEDETAILSTOTAL t
 where rcpt_no = '{0}'
 GROUP BY t.fee_class_name,t.ITEM_NAME ,t.ITEM_CODE ,t.ITEM_SPEC ,t.UNITS,t.item_grade having sum(t.COSTS)>0
 order by t.item_grade,t.fee_class_name,t.ITEM_NAME";
                //                sql = @"SELECT t2.fee_class_name 项目类别,
                //                               t.ITEM_NAME 项目名称,
                //                               t.ITEM_CODE 项目编码,
                //                               t.ITEM_SPEC 规格,
                //                               t.UNITS 单位,
                //                               SUM(t.AMOUNT) 数量,
                //                               SUM(t.COSTS) 计价金额,
                //                               SUM(t.CHARGES) 应收金额,
                //                               decode(t1.fee_itemgrade,
                //                                      '1',
                //                                      '甲类',
                //                                      '2',
                //                                      '乙类',
                //                                      '3',
                //                                      '丙类',
                //                                      '其他') 医保类别
                //                          FROM pat_visit, INP_BILL_DETAIL t
                //                          left join his_compare t1
                //                            on t1.his_code = t.ITEM_CODE 
                //                          left join inp_rcpt_fee_dict t2
                //                            on t2.fee_class_code = t.CLASS_ON_INP_RCPT
                //                         WHERE t.patient_id = '{0}'
                //                           AND t.VISIT_ID = '{1}'
                //                           AND t1.charge_type_code = pat_visit.charge_type_code
                //                           and pat_visit.patient_id = t.PATIENT_ID
                //                           AND pat_visit.visit_id = t.VISIT_ID
                //                         GROUP BY t2.fee_class_name,
                //                                  t.ITEM_NAME,
                //                                  t.ITEM_CODE,
                //                                  t.ITEM_SPEC,
                //                                  t.UNITS,
                //                                  t1.fee_itemgrade
                //                         order by t2.fee_class_name";
                sql = string.Format(sql, rcpt);
                feeDetailsTotalSet = BaseEntityer.Db.GetDataTable(sql);
                if (feeDetailsTotalSet.Rows.Count <= 0)
                {
                    sql = @"SELECT inp_rcpt_fee_dict.fee_class_code 项目类别,
                                   INP_BILL_DETAIL.ITEM_NAME 项目名称,
                                   INP_BILL_DETAIL.ITEM_CODE 项目编码,
                                   INP_BILL_DETAIL.ITEM_SPEC 规格,
                                   INP_BILL_DETAIL.UNITS 单位,
                                   SUM(INP_BILL_DETAIL.AMOUNT) 数量,
                                   SUM(INP_BILL_DETAIL.COSTS) 计价金额,
                                   SUM(INP_BILL_DETAIL.CHARGES) 应收金额,
                                   '' 医保类别
                              FROM INP_BILL_DETAIL
                              LEFT JOIN inp_rcpt_fee_dict on inp_rcpt_fee_dict.fee_class_code = INP_BILL_DETAIL.CLASS_ON_INP_RCPT
                             WHERE INP_BILL_DETAIL.Rcpt_No = '{0}'
                             GROUP BY inp_rcpt_fee_dict.fee_class_code,
                                      INP_BILL_DETAIL.ITEM_NAME,
                                      INP_BILL_DETAIL.ITEM_CODE,
                                      INP_BILL_DETAIL.ITEM_SPEC,
                                      INP_BILL_DETAIL.UNITS
                             ORDER BY  inp_rcpt_fee_dict.fee_class_code";
                    sql = string.Format(sql, rcpt);
                    feeDetailsTotalSet = BaseEntityer.Db.GetDataTable(sql);
                    if (feeDetailsTotalSet.Rows.Count <= 0)
                    {
                        return null;
                    }
                    else
                    {
                        for (int i = 0; i < feeDetailsTotalSet.Rows.Count; i++)
                        {
                            OutBalanceFeeDetailsTotal feeDetail = new OutBalanceFeeDetailsTotal();
                            feeDetail.项目类别 = feeDetailsTotalSet.Rows[i][0].ToString();
                            feeDetail.项目名称 = feeDetailsTotalSet.Rows[i][1].ToString();
                            feeDetail.项目编号 = feeDetailsTotalSet.Rows[i][2].ToString();
                            feeDetail.规格 = feeDetailsTotalSet.Rows[i][3].ToString();
                            feeDetail.单位 = feeDetailsTotalSet.Rows[i][4].ToString();
                            feeDetail.数量 = feeDetailsTotalSet.Rows[i][5].ToString();
                            feeDetail.计价金额 = feeDetailsTotalSet.Rows[i][6].ToString();
                            feeDetail.应收金额 = feeDetailsTotalSet.Rows[i][7].ToString();
                            feeDetail.医保类别 = feeDetailsTotalSet.Rows[i][8].ToString();
                            feeDetailsTotal.Add(feeDetail);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < feeDetailsTotalSet.Rows.Count; i++)
                    {
                        OutBalanceFeeDetailsTotal feeDetail = new OutBalanceFeeDetailsTotal();
                        feeDetail.项目类别 = feeDetailsTotalSet.Rows[i][0].ToString();
                        feeDetail.项目名称 = feeDetailsTotalSet.Rows[i][1].ToString();
                        feeDetail.项目编号 = feeDetailsTotalSet.Rows[i][2].ToString();
                        feeDetail.规格 = feeDetailsTotalSet.Rows[i][3].ToString();
                        feeDetail.单位 = feeDetailsTotalSet.Rows[i][4].ToString();
                        feeDetail.数量 = feeDetailsTotalSet.Rows[i][5].ToString();
                        feeDetail.计价金额 = feeDetailsTotalSet.Rows[i][6].ToString();
                        feeDetail.应收金额 = feeDetailsTotalSet.Rows[i][7].ToString();
                        feeDetail.医保类别 = feeDetailsTotalSet.Rows[i][8].ToString();
                        feeDetailsTotal.Add(feeDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
            return feeDetailsTotal.ToArray();
        }

        /// <summary>
        /// 获取结算住院发票号
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetMasterRcpt(string patientID, string visitid, ref string invoice, ref string errMsg)
        {
            string sql = @"select t.rcpt_no from inp_settle_master t where t.patient_id='{0}' and t.visit_id='{1}' and t.refunded_rcpt_no is null and t.transact_type='3'";
            try
            {
                sql = string.Format(sql, patientID, visitid);
                DataTable dt = BaseEntityer.Db.GetDataTable(sql);
                invoice = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return 1;
        }

        /// <summary>
        /// 获取结算住院发票号
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetInpatientInvoiceNO(string patientID, string visitid, ref string invoice, ref string errMsg)
        {
            string sql = @" select t.invoice
                              from inp_settle_master t
                             where t.patient_id = '{0}'
                               and t.visit_id = '{1}'
                               and t.refunded_rcpt_no is null
                               and t.transact_type = '3' ";
            try
            {
                sql = string.Format(sql, patientID, visitid);
                DataTable dt = BaseEntityer.Db.GetDataTable(sql);
                invoice = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return 1;
        }


        /// <summary>
        /// 获取结算住院病人ID与次数
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitid"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetPatientIDByRcpt(string invoice, ref string patientID, ref string visitid, ref string errMsg)
        {
            string sql = @"select t.patient_id, t.visit_id from inp_settle_master t where t.rcpt_no='{0}'";
            try
            {
                sql = string.Format(sql, invoice);
                DataTable dt = BaseEntityer.Db.GetDataTable(sql);
                patientID = dt.Rows[0][0].ToString();
                visitid = dt.Rows[0][1].ToString();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return 1;
        }

        /// <summary>
        /// 统计时间段内挂号人数
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <returns></returns>
        public DataTable GetDeptVisitCount(string dateStart, string dateEnd)
        {
            string sql = string.Empty;

            sql = @"select v.就诊科室,nvl(count(*),0) as 挂号人数 from v_clinic_master v
 where v.就诊日期>=to_date('{0}','yyyy-MM-dd')
 and v.就诊日期<=to_date('{1}','yyyy-MM-dd') and v.类型 != '退号' 
 group by v.就诊科室";
            sql = string.Format(sql, dateStart, dateEnd);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 统计时间段内挂号费用合计
        /// </summary>
        /// <param name="dateStart">起始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="visitDept">挂号科室</param>
        /// <returns></returns>
        public DataTable GetClinicRegistStatistic(string dateStart, string dateEnd, string visitDept)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(visitDept))
            {
                sql += @"select nvl(sum(v.挂号费),0) as regist_fee,nvl(sum(v.诊疗费),0) as clinic_fee from v_clinic_master v
                        where v.挂号日期>=to_date('{0}','yyyy-MM-dd hh24:mi:ss') 
                        and v.挂号日期<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')";
                sql = string.Format(sql, dateStart, dateEnd);
            }
            else
            {
                sql += @"select nvl(sum(v.挂号费),0) as regist_fee,nvl(sum(v.诊疗费),0) as clinic_fee from v_clinic_master v
                        where v.挂号日期>=to_date('{0}','yyyy-MM-dd hh24:mi:ss') 
                        and v.挂号日期<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                        and v.就诊科室代码 = '{2}'";
                sql = string.Format(sql, dateStart, dateEnd, visitDept);
            }
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 科室住院人数统计及科室住院天数统计
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetDeptInpatientStatistic(string dateStart, string dateEnd)
        {
            string sql1 = string.Empty;
            sql1 += @"select d.dept_name as 科室, nvl(count(*),0) as 住院人数 from pat_visit p 
                    left join dept_dict d on p.dept_admission_to=d.dept_code
                    where p.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                    group by d.dept_name";
            string sql2 = string.Empty;
            sql2 += @"select dept_name as 科室, nvl(sum(days),0) as 住院天数 from (
                    select q.patient_id,t.dept_name,
                    (trunc(q.discharge_date_time)-trunc(q.admission_date_time)) as days 
                    from pat_visit q left join dept_dict t on q.dept_admission_to=t.dept_code
                    where q.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND q.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                    and q.state='O') group by dept_name";
            string sql3 = string.Empty;
            sql3 += @"select d.dept_name as 科室, nvl(count(*),0) as 无费退院人数 from pat_visit p 
                    left join dept_dict d on p.dept_admission_to=d.dept_code
                    where p.DISCHARGE_DATE_TIME>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.DISCHARGE_DATE_TIME<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                    and p.state='N' 
                    group by d.dept_name";
            sql1 = string.Format(sql1, dateStart, dateEnd);
            sql2 = string.Format(sql2, dateStart, dateEnd);
            sql3 = string.Format(sql3, dateStart, dateEnd);
            DataTable dt1 = BaseEntityer.Db.GetDataTable(sql1);
            DataTable dt2 = BaseEntityer.Db.GetDataTable(sql2);
            DataTable dt3 = BaseEntityer.Db.GetDataTable(sql3);
            dt2.TableName = "table2";
            dt3.TableName = "table3";
            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);
            return ds;
        }

        /// <summary>
        /// 挂号视图统计v_clinic_master
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns></returns>
        public CLINIC_MASTER_DETAIL[] VClinicMasterStatistic(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select * from v_clinic_master v
                    where (v.就诊日期>=to_date('{0}','yyyy-MM-dd hh24:mi:ss') or '{0}' is null)
                    and (v.就诊日期<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') or '{1}' is null)";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 院长日报统计
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetInpatientStatistic(string dateStart, string dateEnd)
        {
            //入院人数
            string sql1 = string.Empty;
            sql1 += @"select nvl(count(*),0) as 入院人数 from pat_visit p 
                    where p.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')";
            //预出院人数
            string sql2 = string.Empty;
            sql2 += @"select nvl(count(*),0) as 预出院人数 from pat_visit p 
                    where p.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                    AND p.state='P'";
            //预出院金额
            string sql3 = string.Empty;
            sql3 += @"select nvl(sum(v.costs),0) as 预出院金额 from pat_visit p left join v_fee_statistic_new v 
                    on p.patient_id=v.patient_id and p.visit_id=v.visit_id 
                    where p.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                    AND p.state='P' and v.type='I'";
            //结算人数
            string sql4 = string.Empty;
            sql4 += @"select nvl(count(*),0) as 结算人数 from pat_visit p 
                    where p.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                    AND p.state='O'";
            //结算金额
            string sql5 = string.Empty;
            sql5 += @"select nvl(sum(v.costs),0) as 结算金额 from pat_visit p left join v_fee_statistic_new v 
                    on p.patient_id=v.patient_id and p.visit_id=v.visit_id 
                    where p.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                    AND p.state='O' and v.type='I'";
            //结算药品金额
            string sql6 = string.Empty;
            sql6 += @"select nvl(sum(v.costs),0) as 结算药品金额 from pat_visit p left join v_fee_statistic_new v 
                    on p.patient_id=v.patient_id and p.visit_id=v.visit_id 
                    where p.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                    AND p.state='O' and v.type='I' and (v.item_class='A' or v.item_class='B')";
            //在院人数
            string sql7 = string.Empty;
            sql7 += @"select nvl(count(*),0) as 在院人数 from pat_visit p 
                    where p.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                    AND p.state='I'";
            //记帐金额
            string sql8 = string.Empty;
            sql8 += @"select nvl(sum(v.costs),0) as 记帐金额 from pat_visit p left join v_fee_statistic_new v 
                    on p.patient_id=v.patient_id and p.visit_id=v.visit_id 
                    where p.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                    AND p.state='I' and v.type='I'";
            //预收款
            string sql9 = string.Empty;
            sql9 += @"select nvl(sum(p.payments),0) as 预收款 from inp_settle_master p 
                    where p.settling_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.settling_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')";
            //药库增加
            string sql10 = string.Empty;
            sql10 += @"select nvl(sum(d.account_receivable),0) as 药库增加 from drug_import_master d 
                    where d.import_date>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                    and d.import_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                    and d.sub_storage like '%药库'";
            //药房增加
            string sql11 = string.Empty;
            sql11 += @"select nvl(sum(d.account_receivable),0) as 药房增加 from drug_import_master d 
                    where d.import_date>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                    and d.import_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                    and d.sub_storage like '%药局'";
            //药库减少
            string sql12 = string.Empty;
            sql12 += @"select nvl(sum(d.account_receivable),0) as 药库减少 from drug_export_master d 
                    where d.export_date>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                    and d.export_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                    and d.sub_storage like '%药库'";
            //药房减少
            string sql13 = string.Empty;
            sql13 += @"select nvl(sum(d.account_receivable),0) as 药房减少 from drug_export_master d 
                    where d.export_date>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                    and d.export_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                    and d.sub_storage like '%药局'";
            sql1 = string.Format(sql1, dateStart, dateEnd);
            sql2 = string.Format(sql2, dateStart, dateEnd);
            sql3 = string.Format(sql3, dateStart, dateEnd);
            sql4 = string.Format(sql4, dateStart, dateEnd);
            sql5 = string.Format(sql5, dateStart, dateEnd);
            sql6 = string.Format(sql6, dateStart, dateEnd);
            sql7 = string.Format(sql7, dateStart, dateEnd);
            sql8 = string.Format(sql8, dateStart, dateEnd);
            sql9 = string.Format(sql9, dateStart, dateEnd);
            sql10 = string.Format(sql10, dateStart, dateEnd);
            sql11 = string.Format(sql11, dateStart, dateEnd);
            sql12 = string.Format(sql12, dateStart, dateEnd);
            sql13 = string.Format(sql13, dateStart, dateEnd);
            DataTable dt1 = BaseEntityer.Db.GetDataTable(sql1);
            DataTable dt2 = BaseEntityer.Db.GetDataTable(sql2);
            DataTable dt3 = BaseEntityer.Db.GetDataTable(sql3);
            DataTable dt4 = BaseEntityer.Db.GetDataTable(sql4);
            DataTable dt5 = BaseEntityer.Db.GetDataTable(sql5);
            DataTable dt6 = BaseEntityer.Db.GetDataTable(sql6);
            DataTable dt7 = BaseEntityer.Db.GetDataTable(sql7);
            DataTable dt8 = BaseEntityer.Db.GetDataTable(sql8);
            DataTable dt9 = BaseEntityer.Db.GetDataTable(sql9);
            DataTable dt10 = BaseEntityer.Db.GetDataTable(sql10);
            DataTable dt11 = BaseEntityer.Db.GetDataTable(sql11);
            DataTable dt12 = BaseEntityer.Db.GetDataTable(sql12);
            DataTable dt13 = BaseEntityer.Db.GetDataTable(sql13);
            dt2.TableName = "table2";
            dt3.TableName = "table3";
            dt4.TableName = "table4";
            dt5.TableName = "table5";
            dt6.TableName = "table6";
            dt7.TableName = "table7";
            dt8.TableName = "table8";
            dt9.TableName = "table9";
            dt10.TableName = "table10";
            dt11.TableName = "table11";
            dt12.TableName = "table12";
            dt13.TableName = "table13";
            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);
            ds.Tables.Add(dt4);
            ds.Tables.Add(dt5);
            ds.Tables.Add(dt6);
            ds.Tables.Add(dt7);
            ds.Tables.Add(dt8);
            ds.Tables.Add(dt9);
            ds.Tables.Add(dt10);
            ds.Tables.Add(dt11);
            ds.Tables.Add(dt12);
            ds.Tables.Add(dt13);
            return ds;
        }

        /// <summary>
        /// 住院人次统计及记账金额统计
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataSet GetInpatientStatistics(string dateStart, string dateEnd, string chargeType)
        {
            //医保住院人次
            string sql1 = string.Empty;
            sql1 += @"select nvl(count(*),0) as 医保住院人次 from pat_visit p 
                    where p.admission_date_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    AND p.admission_date_time<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                    AND p.charge_type='{2}'";
            //保险记帐金额
            string sql2 = string.Empty;
            sql2 += @"select nvl(sum(o.payment_amount),0) as 保险记帐金额 from (
                    select distinct v.rcpt_no as rcpt_no from v_fee_statistic_new v 
                    where v.operdate >=to_date('{0}','yyyy-MM-dd hh24:mi:ss') 
                    AND v.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                    AND v.type='O' and v.charge_type='{2}') f left join outp_payments_money o 
                    on o.rcpt_no=f.rcpt_no
                    where o.money_type!='现金'";
            //医保住院记帐金额
            string sql3 = string.Empty;
            sql3 += @"select nvl((sum(i.costs)-sum(i.charges)),0) as 保险记帐金额 from (
                    select distinct v.rcpt_no as rcpt_no from v_fee_statistic_new v 
                    where v.operdate >=to_date('{0}','yyyy-MM-dd hh24:mi:ss') 
                    AND v.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                    AND v.type='I' and v.charge_type='{2}') f left join inp_settle_master i 
                    on i.rcpt_no=f.rcpt_no";
            sql1 = string.Format(sql1, dateStart, dateEnd, chargeType);
            sql2 = string.Format(sql2, dateStart, dateEnd, chargeType);
            sql3 = string.Format(sql3, dateStart, dateEnd, chargeType);
            DataTable dt1 = BaseEntityer.Db.GetDataTable(sql1);
            DataTable dt2 = BaseEntityer.Db.GetDataTable(sql2);
            DataTable dt3 = BaseEntityer.Db.GetDataTable(sql3);
            dt2.TableName = "table2";
            dt3.TableName = "table3";
            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);
            return ds;
        }

        /// <summary>
        /// 2013-7-9 by li 获取工伤审批信息
        /// </summary>
        /// <returns></returns>
        public outp_industrial_injury[] GetIndustrialInjury()
        {
            string sql = string.Empty;
            sql = @"select * from outp_industrial_injury ";
            return DataSetToEntity.DataSetToT<outp_industrial_injury>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 2013-7-9 by li 获取时间段内门诊诊断
        /// </summary>
        /// <returns></returns>
        public OUTP_MR[] GetOutpMr(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select * from OUTP_MR v 
                    where (v.visit_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss') or '{0}' is null)
                    and (v.visit_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') or '{1}' is null)";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<OUTP_MR>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 视图统计v_fee_statistic_new
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="type">检索类型-全部、门诊、住院</param>
        /// <returns></returns>
        public FEE_DETAIL[] GETVFeeStatistic(string dateStart, string dateEnd, string chargeType)
        {
            string sql = string.Empty;
            sql = @"select * from v_fee_statistic_new v
where (v.operdate>=to_date('{0}','yyyy-MM-dd hh24:mi:ss') or '{0}' is null)
and (v.operdate<=to_date('{1}','yyyy-MM-dd hh24:mi:ss') or '{1}' is null)
and (v.charge_type='{2}')";
            sql = string.Format(sql, dateStart, dateEnd, chargeType);
            return DataSetToEntity.DataSetToT<FEE_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        #endregion

        #region 浑南新区医院医保报表
        public static List<ShenYangSIReportClass> ShenYangSIReportClass(string Month, ref string errMsg)
        {
            List<ShenYangSIReportClass> report = new List<ShenYangSIReportClass>();

            string sql =
            #region
 @"select decode(t.type_code, '1', '门诊', '2', '住院') Type_Code,
       decode(t.medical_type,
              '11',
              '门诊',
              '12',
              '特病门诊',
              '15',
              '门诊',
              '16',
              '门诊',
              '18',
              '门诊',
              '19',
              '慢病门诊',
              '20',
              '家庭病床',
              '21',
              '住院',
              '22',
              '住院',
              '24',
              '住院',
              '25',
              '家庭病床',
              '27',
              '急诊',
              '29',
              '急诊',
              '32',
              '住院',
              '41',
              '生育门诊',
              '42',
              '生育住院',
              '43',
              '节育门诊',
              '44',
              '节育住院',
              '45',
              '生育转院',
              'X') Medical_Type,
       decode(t.remark,
              '11',
              '职工',
              '13',
              '职工',
              '21',
              '职工',
              '91',
              '其他',
              '99',
              '其他',
              '',
              '空',
              '居民') Medical_DYType,
       decode(t.HELP_ALLOWANCES_COST, 0, '非特困', '', '空', '特困') Allowance_Help,
       count(*) Person_Count,
       nvl(sum(t.tot_cost), 0) Tot_Cost,
       nvl(sum(t.pay_cost), 0) Pay_Cost,
       nvl(sum(t.pub_cost)  + sum(t.official_cost), 0) Pub_Cost,
       nvl(sum(t.own_cost), 0) Own_Cost,
       nvl(sum(t.help_allowances_cost), 0) AllowanceHelp_Cost,
       nvl(sum(t.specialmed_pubcost), 0) BirthCheck_Cost,
       nvl(sum(t.over_cost), 0) Over_cost,
       t.trans_type
  from siinfo t
 where t.isbalanced = 1
   and t.balance_state = 1
   and t.pact_code = 2 
   and to_char(t.balance_date, 'yyyy-mm') = '{0}'
   --and t.balance_date >= to_date('2013-04-01 00:00:00','yyyy-mm-dd hh24:mi:ss')
--and t.balance_date <= to_date('2013-04-30 23:59:59','yyyy-mm-dd hh24:mi:ss')
 group by decode(t.type_code, '1', '门诊', '2', '住院'),
          decode(t.medical_type,
                 '11',
                 '门诊',
                 '12',
                 '特病门诊',
                 '15',
                 '门诊',
                 '16',
                 '门诊',
                 '18',
                 '门诊',
                 '19',
                 '慢病门诊',
                 '20',
                 '家庭病床',
                 '21',
                 '住院',
                 '22',
                 '住院',
                 '24',
                 '住院',
                 '25',
                 '家庭病床',
                 '27',
                 '急诊',
                 '29',
                 '急诊',
                 '32',
                 '住院',
                 '41',
                 '生育门诊',
                 '42',
                 '生育住院',
                 '43',
                 '节育门诊',
                 '44',
                 '节育住院',
                 '45',
                 '生育转院',
                 'X'),
          decode(t.remark,
                 '11',
                 '职工',
                 '13',
                 '职工',
                 '21',
                 '职工',
                 '91',
                 '其他',
                 '99',
                 '其他',
                 '',
                 '空',
                 '居民'),
          decode(t.HELP_ALLOWANCES_COST, 0, '非特困', '', '空', '特困'),
          t.trans_type
        order by t.trans_type
            ";
            #endregion

            try
            {
                sql = string.Format(sql, Month);
                //DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                report = DataSetToEntity.DataSetToT<ShenYangSIReportClass>(BaseEntityer.Db.GetDataSet(sql)).ToList();
                if (report.Count <= 0)
                    return null;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }

            return report;
        }

         /// <summary>
        /// 获取门诊统筹的现金金额
        /// </summary>
        /// <param name="medcialType"></param>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static decimal GetPubOwnCostByMedicalType(string medcialType, string dateString)
        {
            string sql = @"SELECT nvl(SUM(own_cost), '0')
  FROM siinfo
 WHERE to_char(balance_date, 'YYYY-MM') = '{1}'
   AND medical_type = '18'
   AND insurancetype = '{0}'
   ";
            try
            {
                sql = string.Format(sql, medcialType,dateString);
                //DataSet ds = BaseEntityer.Db.GetDataSet(sql);
               return BaseEntityer.Db.ExecuteScalar<decimal>(sql);
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public static List<ShenYangSIBirthBalDiagnose> ShenYangSIBirthBalDiagnoseClass(string Month, ref string errMsg)
        {
            List<ShenYangSIBirthBalDiagnose> report = new List<ShenYangSIBirthBalDiagnose>();
            List<ShenYangSIBirthBalDiagnose> balNegative = new List<ShenYangSIBirthBalDiagnose>();
            List<ShenYangSIBirthBalDiagnose> balPositive = new List<ShenYangSIBirthBalDiagnose>();
            string sql =
            #region
 @"select t.name Name,
       t.person_no Person_NO,
       decode(t.medical_type,'42','生育住院','43','节育门诊','44','节育住院') Medical_Type,
       t.register_no Item_NO,
       decode(t.outdiagnose_name, '', t.indiagnose_name, t.outdiagnose_name) Item_Name,
       nvl(sum(t.pub_cost), 0) Pub_Cost,
       to_char(t.balance_date,'yyyy-mm-dd') Balance_Date,
       t.invoice_no Invoice,
       decode(t.remark,'11','职工','13','职工','21','职工','居民') MedicalRemark
  from siinfo t
 where t.isbalanced = 1
   and t.balance_state = 1
   and t.pact_code = 2
   and t.medical_type in ('42', '43', '44')
   and to_char(t.balance_date, 'yyyy-mm') = '{0}'
   and t.trans_type = 1
   --and t.pub_cost <> 0
 group by t.register_no,
          decode(t.outdiagnose_name,
                 '',
                 t.indiagnose_name,
                 t.outdiagnose_name),
                 t.medical_type,
          t.name,
          t.person_no,
          t.balance_date,
          t.invoice_no,
          decode(t.remark,'11','职工','13','职工','21','职工','居民')
 order by decode(t.remark,'11','职工','13','职工','21','职工','居民') desc,
          t.medical_type,
          t.register_no
            ";
            #endregion

            string sql2 =
            #region
 @"select t.name Name,
       t.person_no Person_NO,
       decode(t.medical_type,'42','生育住院','43','节育门诊','44','节育住院') Medical_Type,
       t.register_no Item_NO,
       decode(t.outdiagnose_name, '', t.indiagnose_name, t.outdiagnose_name) Item_Name,
       nvl(sum(t.pub_cost), 0) Pub_Cost,
       to_char(t.balance_date,'yyyy-mm-dd') Balance_Date,
       t.invoice_no Invoice,
       decode(t.remark,'11','职工','13','职工','21','职工','居民') MedicalRemark
  from siinfo t
 where t.isbalanced = 1
   and t.balance_state = 1
   and t.pact_code = 2
   and t.medical_type in ('42', '43', '44')
   and to_char(t.balance_date, 'yyyy-mm') = '{0}'
   and t.trans_type = 2
   --and t.pub_cost <> 0
 group by t.register_no,
          decode(t.outdiagnose_name,
                 '',
                 t.indiagnose_name,
                 t.outdiagnose_name),
                 t.medical_type,
          t.name,
          t.person_no,
          t.balance_date,
          t.invoice_no,
          decode(t.remark,'11','职工','13','职工','21','职工','居民')
 order by decode(t.remark,'11','职工','13','职工','21','职工','居民') desc,
          t.medical_type,
          t.register_no
            ";
            #endregion

            try
            {
                sql = string.Format(sql, Month);
                sql2 = string.Format(sql2, Month);
                //DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                balPositive = DataSetToEntity.DataSetToT<ShenYangSIBirthBalDiagnose>(BaseEntityer.Db.GetDataSet(sql)).ToList();
                if (balPositive.Count <= 0)
                    return null;
                balNegative = DataSetToEntity.DataSetToT<ShenYangSIBirthBalDiagnose>(BaseEntityer.Db.GetDataSet(sql2)).ToList();
                bool isHave = false;
                for (int i = 0; i < balPositive.Count; i++)
                {
                    for (int j = 0; j < balNegative.Count; j++)
                    {
                        if (balPositive[i].Invoice.Equals(balNegative[j].Invoice) == true)
                        {
                            isHave = true;
                            break;
                        }
                    }
                    if (isHave == true)
                    {
                        isHave = false;
                    }
                    else
                    {
                        report.Add(balPositive[i]);
                    }
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }

            return report;
        }

        public static List<ShenYangSISingleDiagnose> ShenYangSISingleDiagnoseClass(string Month, ref string errMsg)
        {
            List<ShenYangSISingleDiagnose> report = new List<ShenYangSISingleDiagnose>();
            List<ShenYangSISingleDiagnose> balNegative = new List<ShenYangSISingleDiagnose>();
            List<ShenYangSISingleDiagnose> balPositive = new List<ShenYangSISingleDiagnose>();
            string sql =
            #region
 @"select t.person_no Person_No,
       t.name Name,
       c.single_code Single_code,
       c.single_name Single_name,
       to_char(t.balance_date,'yyyy-mm-dd') Balance_Date,
       nvl(t.tot_cost,0) Tot_cost,
       nvl(t.pub_cost,0) Pub_cost,
       (case when t.remark in ('41','42','43','44','45') then nvl(c.jm_pubcost,0)
         when t.remark in ('11','13','21') then nvl(c.zg_pubcost,0) else 0 end) Cost,
          t.invoice_no Invoice,
       decode(t.remark,'11','职工','13','职工','21','职工','居民') Type
  from siinfo t
  , diagnosis s 
  , SI_SYSINGLEDIAGNOSE c 
 where t.isbalanced = 1
   and t.balance_state = 1
   and t.inpatient_id = s.patient_id
   and t.visit_id = s.visit_id
   and s.diagnose_identification = c.single_code
   and to_char(t.balance_date, 'yyyy-mm') = '{0}'
   and t.trans_type = 1
   and t.pact_code=2
            ";
            #endregion

            string sql2 =
            #region
 @"select t.person_no Person_No,
       t.name Name,
       c.single_code Single_code,
       c.single_name Single_name,
       to_char(t.balance_date,'yyyy-mm-dd') Balance_Date,
       nvl(t.tot_cost,0) Tot_cost,
       nvl(t.pub_cost,0) Pub_cost,
       (case when t.remark in ('41','42','43','44','45') then nvl(c.jm_pubcost,0)
         when t.remark in ('11','13','21') then nvl(c.zg_pubcost,0) else 0 end) Cost,
          t.invoice_no Invoice,
       decode(t.remark,'11','职工','13','职工','21','职工','居民') Type
  from siinfo t
  , diagnosis s 
  , SI_SYSINGLEDIAGNOSE c 
 where t.isbalanced = 1
   and t.balance_state = 1
   and t.inpatient_id = s.patient_id
   and t.visit_id = s.visit_id
   and s.diagnose_identification = c.single_code
   and to_char(t.balance_date, 'yyyy-mm') = '{0}'
   and t.trans_type = 2
   and t.pact_code=2
            ";
            #endregion

            try
            {
                sql = string.Format(sql, Month);
                sql2 = string.Format(sql2, Month);
                //DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                balPositive = DataSetToEntity.DataSetToT<ShenYangSISingleDiagnose>(BaseEntityer.Db.GetDataSet(sql)).ToList();
                balNegative = DataSetToEntity.DataSetToT<ShenYangSISingleDiagnose>(BaseEntityer.Db.GetDataSet(sql2)).ToList();
                bool isHave = false;
                if (balNegative.Count > 0)
                {
                    for (int i = 0; i < balPositive.Count; i++)
                    {
                        for (int j = 0; j < balNegative.Count; j++)
                        {
                            if (balPositive[i].Invoice.Equals(balNegative[j].Invoice) == true)
                            {
                                isHave = true;
                                break;
                            }
                        }
                        if (isHave == true)
                        {
                            isHave = false;
                        }
                        else
                        {
                            report.Add(balPositive[i]);
                        }
                    }
                }
                else if (balPositive.Count > 0)
                {
                    for (int i = 0; i < balPositive.Count; i++)
                    {
                        report.Add(balPositive[i]);
                    }
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }

            return report;
        }

        public static List<HisCommon.DataEntity.DrugCategory> GetDrugCategory(string dateStart, string dateEnd)
        {
            string sql = @"select 
DrugType,
sum(Amount) Amount,
0 Rate
from 
(select 
(select nvl(t.toxi_property,'其他药品') from drug_dict t where t.drug_code = d.drug_code and rownum = 1) DrugType,
d.retail_price*d.quantity Amount
from drug_export_detail d where document_no in 
(select document_no  from drug_export_master m where 
m.export_date  >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
and m.export_date <=  to_date('{1}','yyyy-mm-dd hh24:mi:ss')
)) group by DrugType order by  DrugType";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DrugCategory>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.DataEntity.DrugOutbound> GetDrugOutbound(string dateStart, string dateEnd)
        {
            string sql = @"select 
m.receiver DeptName, 
sum(m.costs) Amount
from drug_export_master m
where m.export_date  >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
and m.export_date <=  to_date('{1}','yyyy-mm-dd hh24:mi:ss')
group by m.receiver order by m.receiver";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DrugOutbound>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.DataEntity.DrugStock> GetDrugStock(string drugType, string deptCode)
        {
            string sql = @"select 
DeptName,
DrugCategory,
sum(WholesaleCost) WholesaleCost,
0 RetailCost,
0 Difference
from 
( 
select 
sub_storage DeptName,  
(select nvl(t.drug_form,'其他') from drug_dict t where t.drug_code = s.drug_code and rownum = 1) DrugCategory,
s.purchase_price*s.quantity WholesaleCost
from drug_stock s
) ss where (DrugCategory = '{0}' or '{0}'='ALL') and (DeptName = '{1}' or '{1}'='ALL') group by DeptName, DrugCategory
order by DeptName";
            sql = string.Format(sql, drugType, deptCode);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DrugStock>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.DataEntity.DrugDetail> GetDrugDetail(string drugType, string deptCode)
        {
            string sql = @"select 
DrugName,
Specif,
package_units Unit,
purchase_price WholesalePrice,
0 Price,
sum(quantity) Qty,
sum(purchase_price*quantity) Amount
from 
(select (select t.drug_name from drug_dict t where t.drug_code = ds.drug_code and rownum = 1) DrugName,
(select t.drug_spec from drug_dict t where t.drug_code = ds.drug_code and rownum = 1) Specif,
package_units,purchase_price,quantity,
 (select nvl(t.drug_form,'其他') from drug_dict t where t.drug_code = ds.drug_code and rownum = 1) DrugCategory,
 sub_storage DeptName
from drug_stock ds) 
s where (DrugCategory = '{0}' or '{0}'='ALL') and (DeptName = '{1}' or '{1}'='ALL')
group by s.DrugName,Specif,purchase_price,package_units order by s.DrugName,Specif,purchase_price,package_units";
            sql = string.Format(sql, drugType, deptCode);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DrugDetail>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.DataEntity.DrugInOut> GetDrugInOut(string dateStart, string dateEnd)
        {
            string sql = @"select 
DeptName,
DrugCategory,
sum(InWholesaleCost) InWholesaleCost,
sum(InRetailCost) InRetailCost,
sum(OutWholesaleCost) OutWholesaleCost,
sum(OutRetailCost) OutRetailCost from
(
select 
m.sub_storage DeptName,
(select nvl(t.drug_form,'其他') from drug_dict t where t.drug_code = d.drug_code and rownum = 1) DrugCategory,
0 InWholesaleCost,
0 InRetailCost,
d.purchase_price*d.quantity OutWholesaleCost,
d.retail_price*d.quantity OutRetailCost   
from drug_export_master m, drug_export_detail d  
where m.document_no = d.document_no and m.export_date  >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
and m.export_date <=  to_date('{1}','yyyy-mm-dd hh24:mi:ss')
union all
select 
sub_storage DeptName,
(select nvl(t.drug_form,'其他') from drug_dict t where t.drug_code = di.drug_code and rownum = 1) DrugCategory,
di.purchase_price*di.quantity InWholesaleCost,
di.retail_price*di.quantity InRetailCost,
0 OutWholesaleCost,
0 OutRetailCost 
from drug_import_master mi,drug_import_detail di
where mi.document_no = di.document_no and mi.import_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
and mi.import_date <=  to_date('{1}','yyyy-mm-dd hh24:mi:ss')
) group by DeptName,DrugCategory order by DeptName,DrugCategory";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DrugInOut>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.DataEntity.DrugStorage> GetDrugStorage(string dateStart, string dateEnd)
        {
            string sql = @"select 
mi.supplier Factory,
sum(account_payed) InWholesaleCost,
sum(costs) InRetailCost
from drug_import_master mi
where mi.import_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
and mi.import_date <=  to_date('{1}','yyyy-mm-dd hh24:mi:ss')
group by mi.supplier order by mi.supplier";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DrugStorage>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.ClinicCheckSource> GetClinicCheckSource(string dateStart, string dateEnd)
        {
            string sql = @"select 
  case when nvl(user_name,'nobody')!='nobody' then '医生开立'
   else '其他' end SourceType,
  count(distinct rcpt_no) RcptQty
  from  
  v_fee_statistic_new vfee where type = 'O'
  and vfee.operdate  >=
  to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
  and vfee.operdate  <=
  to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
  group by 
  grouping sets
  (case when nvl(user_name,'nobody')!='nobody' then '医生开立'
   else '其他' end 
  ) order by SourceType desc";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.ClinicCheckSource>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.ClinicCheckType> GetClinicCheckType(string dateStart, string dateEnd)
        {
            string sql = @"select 
  '挂号' AS CheckType,
  sum(clinic_charge) as CheckAmount
  from clinic_master mr where 
  mr.registering_date >=
  to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
  and mr.registering_date <=
  to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
  union all
  select 
  case when class_on_reckoning = 'A01' or 
  class_on_reckoning = 'A02' or
  class_on_reckoning = 'A03' then '药品'
  else '医疗' end CheckType,
  sum(costs) as CheckAmount
  from  
  v_fee_statistic_new vfee where type = 'O'
  and vfee.operdate  >=
  to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
  and vfee.operdate  <=
  to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
  group by 
  grouping sets
  (case when class_on_reckoning = 'A01' or 
   class_on_reckoning = 'A02' or
   class_on_reckoning = 'A03' then '药品'
   else '医疗' end 
  )";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.ClinicCheckType>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.ClinicCheckSituation> GetClinicCheckSituation(string dateStart, string dateEnd)
        {
            string sql = @"SELECT SUM(initialdiagnosis) initialdiagnosis,
       SUM(repeatdiagnosis) repeatdiagnosis,
       SUM(diagnosisqty) diagnosisqty,
       SUM(diagnosischarges) diagnosischarges
  FROM (SELECT SUM(decode(first_visit_indicator, '1', 1, 0)) initialdiagnosis,
               SUM(decode(first_visit_indicator, '1', 0, 1)) repeatdiagnosis,
               SUM(decode(admis, '1', 1, 0)) diagnosisqty,
               0 diagnosischarges
          FROM clinic_master mr
         WHERE mr.registering_date >=
               to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
           AND mr.registering_date <=
               to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
           AND mr.returned_operator IS NULL
        UNION ALL
        SELECT 0 initialdiagnosis,
               0 repeatdiagnosis,
               0 diagnosisqty,
               COUNT(DISTINCT patient_id) diagnosischarges
          FROM v_fee_statistic_new vfee
         WHERE TYPE = 'O'
           AND vfee.operdate >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
           AND vfee.operdate <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))
";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.ClinicCheckSituation>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.ClinicDiagnoseStat> GetClinicDiagnoseStat(string dateStart, string dateEnd)
        {
            string sql = @"select 
diag_desc DiagName,
count(*) DiagQty,
0 
from OUTP_MR mr where diag_desc is not null
and mr.visit_date  >=
to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and mr.visit_date  <=
to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
group by diag_desc 
order by count(*) desc";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.ClinicDiagnoseStat>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.ClinicPrescriptionDetail> GetClinicPrescriptionDetail(string dateStart, string dateEnd, string deptCode)
        {
            string sql = @"select 
nvl(order_dept_name,'其他科室')  DeptName,
user_name DocName,
rcpt_no RcptNo,
0 Qty,
0 OrderQty,
0 OrderUnDrugQty,
0 OrderDrugQty,
sum(costs) OrderFee,
0 OrderUnDrugFee,
sum(decode(class_on_reckoning,'A01',costs,'A02',costs,'A03',costs,0)) OrderDrugFee,
0 OrderAvgFee,
0 OrderAvgUnDrugFee,
0 OrderAvgDrugFee
from  
v_fee_statistic_new vfee where type = 'O'
and vfee.operdate  >=
to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and vfee.operdate  <=
to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
and (order_dept_name = '{2}' or 'ALL' = '{2}')
group by order_dept_name,user_name,rcpt_no 
order by order_dept_name,user_name,rcpt_no";
            sql = string.Format(sql, dateStart, dateEnd, deptCode);
            return DataSetToEntity.DataSetToT<HisCommon.ClinicPrescriptionDetail>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.DocWorkloadStat> GetClinicDocWorkloadStat(string dateStart, string dateEnd, string deptCode)
        {
            string sql = @"select 
nvl(order_dept_name,'其他科室')  DeptName,
user_name DocName,
count(distinct patient_id) Qty,
count(distinct rcpt_no) OrderQty,
--count(distinct(case when nvl(class_on_reckoning,'nobody') != 'A01' and nvl(class_on_reckoning,'nobody') != 'A02' and nvl(class_on_reckoning,'nobody') != 'A03' then rcpt_no end)) OrderUnDrugQty,
--count(distinct(case when nvl(class_on_reckoning,'nobody') = 'A01' or nvl(class_on_reckoning,'nobody') = 'A02' or nvl(class_on_reckoning,'nobody') = 'A03' then rcpt_no end)) OrderDrugQty,
0 OrderUnDrugQty,
0 OrderDrugQty,
sum(costs) OrderFee,
sum(decode(class_on_reckoning,'A01',0,'A02',0,'A03',0,costs)) OrderUnDrugFee,
sum(decode(class_on_reckoning,'A01',costs,'A02',costs,'A03',costs,0)) OrderDrugFee,
0 OrderAvgFee,
0 OrderAvgUnDrugFee,
0 OrderAvgDrugFee
from  
v_fee_statistic_new vfee where type = 'O'
and vfee.operdate  >=
to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and vfee.operdate  <=
to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
and (order_dept_name = '{2}' or 'ALL' = '{2}')
group by order_dept_name,user_name 
order by order_dept_name,user_name";
            sql = string.Format(sql, dateStart, dateEnd, deptCode);
            return DataSetToEntity.DataSetToT<HisCommon.DocWorkloadStat>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.ClinicDrugRatio> GetClinicDrugRatio(string dateStart, string dateEnd, string deptCode)
        {
            string sql = @"select 
nvl(order_dept_name,'其他科室')  DeptName,
sum(decode(class_on_reckoning,'A01',costs,'A02',costs,'A03',costs,0)) DrugFee,
sum(decode(class_on_reckoning,'A01',0,'A02',0,'A03',0,costs))NoDrugFee,
sum(costs)AmountTotal,
0 Ratio
from  
v_fee_statistic_new vfee where type = 'O'
and vfee.operdate  >=
to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and vfee.operdate  <=
to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
and (order_dept_name = '{2}' or 'ALL' = '{2}')
group by order_dept_name ";
            sql = string.Format(sql, dateStart, dateEnd, deptCode);
            return DataSetToEntity.DataSetToT<HisCommon.ClinicDrugRatio>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.ClinicCheckSummary> GetClinicCheckSummary(string dateStart, string dateEnd)
        {
            string sql = @"select 
 nvl(recktype,'其他') ItemName, 
 count(*) ClinicCheckSum,
 sum(costs) ClinicCheckAmount,
 '' Exp
from  v_fee_statistic_new vfee where type = 'O' and recktype in
('多普勒彩色超声','胃肠镜','DR费','心电图费','B超费','CT费','化验检查费','透视费','门诊诊查费','检查费','病理检查费')
and vfee.operdate  >=
to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and vfee.operdate  <=
to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
group by recktype order by ClinicCheckAmount desc";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.ClinicCheckSummary>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.SummaryClinic> GetSummaryClinic(string dateStart, string dateEnd)
        {
            string sql = @"select 
to_number(TO_CHAR(registering_date,'HH24')) as ClinicTime,
count(*) ClinicSum,
'' Exp
from clinic_master where
registering_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and registering_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
group by TO_CHAR(registering_date,'HH24')
order by to_number(TO_CHAR(registering_date,'HH24'))";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.SummaryClinic>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.SummaryClinic> GetDeptSummaryClinic(string dateStart, string dateEnd)
        {
            string sql = @"select 0 ClinicTime ,nvl(ClinicSum,0) ClinicSum,
Exp
   from 
  (
   SELECT 
   (select dept_name from dept_dict d where d.dept_code=visit_dept) as Exp,
          count(*) as ClinicSum
  FROM clinic_master where
registering_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and registering_date < to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
  group by visit_dept
  order by  NVL(count(*),0) desc 
   )where rownum <=5";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.SummaryClinic>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.SummaryClinic> GetDeptSummaryClinic2(string dateStart, string dateEnd)
        {
            string sql = @"select 0 ClinicTime ,nvl(ClinicSum,0) ClinicSum,
Exp
   from 
  (
   SELECT 
   (select dept_name from dept_dict d where d.dept_code=visit_dept) as Exp,
          count(*) as ClinicSum
  FROM clinic_master where
registering_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and registering_date < to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
  group by visit_dept
  order by  NVL(count(*),0)
   )where rownum <=5";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.SummaryClinic>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static DataTable GetDictionary(string type)
        {
            string sql = @"select * from (select '' code,'' name,0 sort_id from dual
                                        union all
                                        select code,name,sort_id from com_dictionary
                                        where type = '{0}') order by sort_id";
            sql = string.Format(sql, type);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public static DataTable GetInvDictionary(string type)
        {
            string sql = @"select lpad(code,2,'0') cCode,name cName,mark cGroup from com_dictionary
                                        where type = '{0}' order by sort_id";
            sql = string.Format(sql, type);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public static DataTable GetStorage()
        {
            string sql = @"
select '' code,'' name from dual
union all
select  
storage_code code,
sub_storage  name
from drug_sub_storage_dict";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public static DataTable GetDrugClass()
        {
            string sql = @"
select '' code,'' name from dual
union all
select  
class_code code,
class_name name
from drug_class_dict";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public static string GetindustrialSeq()
        {
            string sql = @"select INDUSTRIALSEQ.NEXTVAL from dual";
            return BaseEntityer.Db.ExecuteScalar(sql).ToString();
        }

        public static DataTable GetOutInpatientInfo(string ward_code, string dateStart, string dateEnd)
        {
            string sql = @" select v.patient_id,v.visit_id,m.name,m.sex,m.DATE_OF_BIRTH,v.discharge_date_time from pat_visit v  
            left join pat_master_index m on v.patient_id=m.patient_id 
            where v.state in('B','O','P') 
            and v.DEPT_DISCHARGE_FROM in(select w.dept_code from dept_vs_ward w where w.ward_code='{0}')
            and   ( v.DISCHARGE_DATE_TIME >=  to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') ) AND  
             (v.DISCHARGE_DATE_TIME <= to_date('{2}', 'yyyy-MM-dd hh24:mi:ss') ) ";
            sql = string.Format(sql, ward_code, dateStart, dateEnd);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public static List<HisCommon.DataEntity.INP_BILL_DETAIL> GetOutPersonBill(string patient_id, string visit_id)
        {
            string sql = @"select *  from inp_bill_detail 
      where patient_id='{0}' and visit_id='{1}'";
            sql = string.Format(sql, patient_id, visit_id);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.INP_BILL_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public static List<HisCommon.DataEntity.DRUG_DISPENSE_REGRET_REQ> GetOutRegretReq(string ward_code)
        {
            string sql = @"SELECT  WARD,
              DISPENSARY,
              POST_DATE_TIME,
              PATIENT_ID,
              VISIT_ID,
              (select p.name from pat_master_index  p where p.patient_id=DRUG_DISPENSE_REGRET_REQ.PATIENT_ID) as name,
              DRUG_CODE,
              DRUG_SPEC,
              DRUG_UNITS,
              FIRM_ID,
              DISPENSE_AMOUNT,
              APPLICANT,
              RETAIL_PRICE,
              nvl(DRUG_DISPENSE_REGRET_REQ.REGRET, 0) as regret,
              COSTS,
              CHARGES,
              TOTAL_AMOUNT,
              ITEM_NO,
              DISPENSING_DATE_TIME,
              BATCH_NO,
              ORDER_NO,
              ORDER_SUB_NO,
              DEPT_CODE,
                round((costs/TOTAL_AMOUNT),2) as price,
                (select d.dept_name from dept_dict d where d.dept_code=DISPENSARY) as PERFORMED_BY_NAME,
           (SELECT  ITEM_NAME FROM PRICE_ITEM_NAME_DICT  WHERE  ITEM_CLASS in ('A', 'B') and std_indicator = 1 and PRICE_ITEM_NAME_DICT.ITEM_CODE= DRUG_CODE and rownum=1) as ITEM_NAME
        FROM DRUG_DISPENSE_REGRET_REQ 
       WHERE  REGRET='0'  and WARD='{0}'";
            sql = string.Format(sql, ward_code);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DRUG_DISPENSE_REGRET_REQ>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        #endregion

        #region 四平人民医院医院报表服务

        /// <summary>
        /// 门诊收款汇总表
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns></returns>
        public DataTable OutpPaymentSummary(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            //2013-7-31 by li 收款员属性修改，增加门诊/住院收款员
            sql = @"select t.subj_code,
                           t.subj_name,
                           t.user_id,
                           t.user_name,
                           sum(nvl(d.income, 0)) as income
                      from (select s.*, u.*
                              from tally_subject_dict s, users_staff_dict u
                             where u.user_id in (select distinct o.user_id
                                                   from USERS_STAFF_DICT o
                                                  where o.is_cashier = '1'
                                                        or o.is_cashier = '3')
                             order by u.user_name) t
                      left join (select c.*, m.operator_no
                                   from outp_acct_detail c
                                   left join outp_acct_master m
                                     on c.acct_no = m.acct_no
                                  where m.acct_date >=
                                        to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                                    and m.acct_date <=
                                        to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')) d
                        on (t.subj_code = d.tally_fee_class
                       and t.user_id = d.operator_no)
                     group by t.subj_code, t.subj_name, t.user_id, t.user_name
                     order by t.user_id, t.subj_code";
            sql = string.Format(sql, dateStart, dateEnd);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 门诊收款支付方式汇总表
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns></returns>
        public REPORT_PAY_WAY_SUMMARY[] OutpPayWaySummary(string dateStart, string dateEnd)
        {
            string sql = string.Empty;

            sql = @"select p.pay_way_code,
                           p.pay_way_name,
                           m.operator_no,
                           sum(nvl(o.income_amount, 0)) as income
                      from pay_way_dict p
                      left join outp_acct_money o
                        on p.pay_way_name = o.money_type
                      left join outp_acct_master m
                        on m.acct_no = o.acct_no
                     where m.acct_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                       and m.acct_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                     group by m.operator_no, p.pay_way_code, p.pay_way_name
                     order by m.operator_no, p.pay_way_code";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<REPORT_PAY_WAY_SUMMARY>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 2013-6-17 by li 支付方式字典
        /// </summary>
        /// <returns></returns>
        public PAY_WAY_DICT[] GetPayWayDict()
        {
            string sql = string.Empty;
            sql = @"select * from pay_way_dict p order by p.pay_way_code";
            return DataSetToEntity.DataSetToT<PAY_WAY_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 2013-6-20 by li 患者在院信息查询
        /// </summary>
        /// <returns></returns>
        public PATS_IN_HOSPITAL[] GetPatInHospital()
        {
            string sql = string.Empty;
            sql = @"select p.*,
                       v.state as statecode,
                       (CASE v.state
                         WHEN 'R' then
                          '住院登记'
                         WHEN 'I' then
                          '病房接诊'
                         WHEN 'B' then
                          '出院登记'
                         WHEN 'O' then
                          '出院结算'
                         WHEN 'P' then
                          '预约出院'
                         else
                          '无费退院'
                       end) as state
                  from pats_in_hospital p
                  left join pat_visit v
                    on p.patient_id = v.patient_id";
            return DataSetToEntity.DataSetToT<PATS_IN_HOSPITAL>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 住院人员
        /// </summary>
        /// <returns></returns>
        public PAT_VISIT[] GetPatVisitInfo()
        {
            string sql = string.Empty;
            //2013-6-25 by li 增加护理单元科室代码字段
            sql = @"select p.*, nvl(h.ward_code, d.ward_code) as ward_code
                  from pat_visit p
                  left join dept_vs_ward d
                    on p.dept_discharge_from = d.dept_code
                  left join pats_in_hospital h
                    on (p.patient_id = h.patient_id and p.visit_id = h.visit_id)";
            return DataSetToEntity.DataSetToT<PAT_VISIT>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 床位信息
        /// </summary>
        /// <returns></returns>
        public BED_REC[] GetBedRecInfo()
        {
            string sql = string.Empty;
            sql = @"select * from bed_rec b";
            return DataSetToEntity.DataSetToT<BED_REC>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 2013-6-25 by li 出院方式字典表
        /// </summary>
        /// <returns></returns>
        public DISCHARGE_DISPOSITION_DICT[] GetDischargeDispositionDict()
        {
            string sql = string.Empty;
            sql = @"select * from DISCHARGE_DISPOSITION_DICT p order by p.discharge_disposition_code";
            return DataSetToEntity.DataSetToT<DISCHARGE_DISPOSITION_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 2013-6-27 by li 诊断类型字典表
        /// </summary>
        /// <returns></returns>
        public DIAGNOSIS_TYPE_DICT[] GetDiagnosisTypeDict()
        {
            string sql = string.Empty;
            sql = @"select * from diagnosis_type_dict d order by d.diagnosis_type_code";
            return DataSetToEntity.DataSetToT<DIAGNOSIS_TYPE_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 2013-6-27 by li 住院诊断列表
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns></returns>
        public DIAGNOSIS[] GetDiagnosisList(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select d.*
                      from diagnosis d
                      left join pat_visit p
                        on (d.patient_id = p.patient_id and d.visit_id = p.visit_id)
                     where p.discharge_date_time >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                       and p.discharge_date_time <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<DIAGNOSIS>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 科室与病区对照关系
        /// </summary>
        /// <returns></returns>
        public DEPT_VS_WARD[] GetDeptVsWard()
        {
            string sql = string.Empty;
            sql = @"select * from DEPT_VS_WARD";
            return DataSetToEntity.DataSetToT<DEPT_VS_WARD>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 住院收入统计表（按人）
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public List<InHospitalIncome> GetInHospitalIncome(string strWhere)
        {
            //2013-11-7 by li 收费总额charges--》costs，costs为打折后金额
            string sql = @"select order_doctor,t.user_name,recktype, sum(t.costs) allcose 
                           from v_fee_statistic_new t 
                           where 1=1  " + strWhere + @"
                           GROUP BY t.order_doctor,user_name,t.recktype ";

            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.InHospitalIncome>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 按时间、执行科室查询住院收入
        /// </summary>
        /// <param name="dateStart">查询开始时间</param>
        /// <param name="dateEnd">查询结束时间</param>
        /// <param name="PerformedDeptCODE">执行科室代码</param>
        /// <returns></returns>
        public List<OperatingRoomIncome> GetOperatingRoomIncome(string dateStart, string dateEnd, string PerformedDeptCODE, string charge_type)
        {
            string sql = string.Empty;
            sql = @"select v.order_dept,v.order_dept_name,v.recktype,sum(v.costs) allcost 
                    from v_fee_statistic_new v 
                    where type = 'I' 
                    and v.operdate >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                    and v.operdate <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";
            if (PerformedDeptCODE != string.Empty)
            {
                sql += " and v.performed_by = '{2}'";
            }
            if (charge_type != string.Empty)
            {
                if (charge_type.Trim().Equals("自费"))
                {
                    sql += @" and (v.charge_type_code != '2' and v.charge_type_code != '13' and v.charge_type_code != '14' and 
                                   v.charge_type_code != '16' and v.charge_type_code != '15' and v.charge_type_code != '17' and 
                                   v.charge_type_code != '18' and v.charge_type_code != '22')";
                }
                else if (charge_type.Trim().Equals("公费"))
                {
                    sql += @" and (v.charge_type_code = '2' or v.charge_type_code = '13' or v.charge_type_code = '14' or 
                                   v.charge_type_code = '16' or v.charge_type_code = '15' or v.charge_type_code = '17' or 
                                   v.charge_type_code = '18' or v.charge_type_code = '22')";
                }
            }
            sql += " group by v.order_dept,v.order_dept_name,v.recktype";
            sql = string.Format(sql, dateStart, dateEnd, PerformedDeptCODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OperatingRoomIncome>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 按时间、开单科室查询住院收入
        /// </summary>
        /// <param name="dateStart">查询开始时间</param>
        /// <param name="dateEnd">查询结束时间</param>
        /// <param name="OrderDeptCODE">执行科室代码</param>
        /// <returns></returns>
        public List<RoomIncomeByOrderDept> GetInHospitalIncomeByOrderDept(string dateStart, string dateEnd, string OrderDeptCODE)
        {
            string sql = string.Empty;
            sql = @"select v.dept_admission_to,d.dept_name as dept_admission_name,v.recktype,sum(v.costs) allcost 
                    from v_fee_statistic_new v 
                    left join dept_dict d on v.dept_admission_to=d.dept_code 
                    where type = 'I' 
                    and v.operdate >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                    and v.operdate <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";
            if (OrderDeptCODE != string.Empty)
            {
                sql += " and v.order_dept = '{2}'";
            }
            sql += " group by v.dept_admission_to,d.dept_name,v.recktype";
            sql = string.Format(sql, dateStart, dateEnd, OrderDeptCODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.RoomIncomeByOrderDept>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 按类别获取出院结算统计
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="charge_type">统计类别</param>
        /// <returns></returns>
        public List<OutHospitalSettlementStatistical> GetOutHospitalSettlementStatistical(string dateStart, string dateEnd, string charge_type)
        {
            string sql = string.Empty;
            sql = @"select  
                    PAT.PATIENT_ID,
                    (select name from pat_master_index where patient_id = pat.patient_id) pat_name,
                    charge_type,
                    charge_type rylb,
                    (select dept_name from dept_dict where dept_code = pat.dept_discharge_from) dept_discharge_from,
                    to_char(inp.settling_date,'yyyy-mm-dd hh24:mi:ss') as settling_date,
                    to_char(pat.admission_date_time,'yyyy-mm-dd hh24:mi:ss') as admission_date_time,
                    to_char(pat.discharge_date_time,'yyyy-mm-dd hh24:mi:ss') as discharge_date_time,
                    nvl(-(select sum(amount) from prepayment_rcpt where balance_invoice=inp.rcpt_no and pay_way in (1,3) and TRANSACT_TYPE >'2' ),0) prepay_cash,
                    nvl(-(select sum(amount) from prepayment_rcpt where balance_invoice=inp.rcpt_no and pay_way=2 and TRANSACT_TYPE >'2' ),0) prepay_posh,
                    -(select sum(amount) from prepayment_rcpt where (pay_way=1 or pay_way=2 or pay_way=3) and TRANSACT_TYPE >'2'  and balance_invoice=inp.rcpt_no) prepay_allamount,
                    nvl((select sum(return_amount) from pay_way where rcpt_no = inp.rcpt_no and money_type_id='1'),0) tf_cash,
                    nvl((select sum(return_amount) from pay_way where rcpt_no = inp.rcpt_no and money_type_id='2'),0) tf_posh,
                    (select sum(return_amount) from pay_way where rcpt_no = inp.rcpt_no and (money_type_id='1' or money_type_id='2')) tf_allamount,
                    inp.operator_no,
                    inp.costs ,
                    sett.fee_class_name ,
                    sett.costs sub_costs 
                    from inp_settle_master inp,pat_visit pat ,inp_settle_detail sett 
                    where inp.patient_id =pat.patient_id and inp.visit_id = pat.visit_id 
                    and inp.rcpt_no = sett.rcpt_no 
                    and inp.settling_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                    and inp.settling_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";
            if (charge_type != string.Empty)
            {
                if (charge_type.Trim().Equals("自费"))
                {
                    sql += " and (charge_type_code != '2' and charge_type_code != '13' and charge_type_code != '14' and charge_type_code != '16' and charge_type_code != '15' and charge_type_code != '17' and charge_type_code != '18' and charge_type_code != '22')";
                }
                else if (charge_type.Trim().Equals("公费"))
                {
                    sql += " and (charge_type_code = '2' or charge_type_code = '13' or charge_type_code = '14' or charge_type_code = '16' or charge_type_code = '15' or charge_type_code = '17' or charge_type_code = '18' or charge_type_code = '22')";
                }
            }
            sql += " group by inp.rcpt_no,pat.PATIENT_ID,pat.admission_date_time,pat.discharge_date_time,charge_type,charge_type_code,dept_discharge_from,inp.operator_no,inp.costs ,sett.fee_class_name,sett.costs,inp.patient_id,inp.visit_id,inp.rcpt_no,inp.settling_date";
            sql += " order by PAT.PATIENT_ID";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OutHospitalSettlementStatistical>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 按类别获取出院结算统计---计算患者人数
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="charge_type">统计类别</param>
        /// <returns></returns>
        public List<OutHospitalSettlementStatistical> GetOutHospitalSettlementStatistical_PatientCount(string dateStart, string dateEnd, string charge_type, string oper_id)
        {
            string sql = string.Empty;
            sql = @"select 
                    PAT.PATIENT_ID 
                    from inp_settle_master inp,pat_visit pat ,inp_settle_detail sett 
                    where inp.patient_id =pat.patient_id and inp.visit_id = pat.visit_id 
                    and inp.rcpt_no = sett.rcpt_no 
                    and inp.settling_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                    and inp.settling_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";
            if (oper_id != string.Empty)
            {
                sql += " and inp.operator_no = '{2}'";
            }
            if (charge_type != string.Empty)
            {
                if (charge_type.Trim().Equals("自费"))
                {
                    sql += " and (charge_type_code != '2' and charge_type_code != '13' and charge_type_code != '14' and charge_type_code != '16' and charge_type_code != '15' and charge_type_code != '17' and charge_type_code != '18' and charge_type_code != '22')";
                }
                else if (charge_type.Trim().Equals("公费"))
                {
                    sql += " and (charge_type_code = '2' or charge_type_code = '13' or charge_type_code = '14' or charge_type_code = '16' or charge_type_code = '15' or charge_type_code = '17' or charge_type_code = '18' or charge_type_code = '22')";
                }
            }
            sql += " group by pat.PATIENT_ID";
            sql = string.Format(sql, dateStart, dateEnd, oper_id);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OutHospitalSettlementStatistical>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 按类别获取出院结算统计---计算预交金总额
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="charge_type">统计类别</param>
        /// <returns></returns>
        public string GetOutHospitalSettlementStatistical_prepay_allamount(string dateStart, string dateEnd, string charge_type, string oper_id)
        {
            string sql = string.Empty;
            sql = @"select sum(prepay_allamount) from
                    (select  
                    PAT.PATIENT_ID,
                    (select name from pat_master_index where patient_id =pat.patient_id) pat_name,
                    charge_type,
                    charge_type_code,
                    charge_type rylb,
                    (select dept_name from dept_dict where dept_code =pat.dept_discharge_from) dept_discharge_from,
                    pat.admission_date_time,
                    pat.discharge_date_time,
                    -(select sum(amount) from prepayment_rcpt where (pay_way=1 or pay_way=2 or pay_way=3) and TRANSACT_TYPE>'2'  and balance_invoice=inp.rcpt_no) prepay_allamount 
                    from inp_settle_master inp,pat_visit pat ,inp_settle_detail sett 
                    where inp.patient_id =pat.patient_id and inp.visit_id = pat.visit_id 
                    and inp.rcpt_no = sett.rcpt_no 
                    and inp.settling_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                    and inp.settling_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";
            if (oper_id != string.Empty)
            {
                sql += " and inp.operator_no = '{2}'";
            }
            if (charge_type != string.Empty)
            {
                if (charge_type.Trim().Equals("自费"))
                {
                    sql += " and (charge_type_code != '2' and charge_type_code != '13' and charge_type_code != '14' and charge_type_code != '16' and charge_type_code != '15' and charge_type_code != '17' and charge_type_code != '18' and charge_type_code != '22')";
                }
                else if (charge_type.Trim().Equals("公费"))
                {
                    sql += " and (charge_type_code = '2' or charge_type_code = '13' or charge_type_code = '14' or charge_type_code = '16' or charge_type_code = '15' or charge_type_code = '17' or charge_type_code = '18' or charge_type_code = '22')";
                }
            }
            sql += " group by inp.rcpt_no,pat.PATIENT_ID,pat.admission_date_time,pat.discharge_date_time,charge_type,charge_type_code,dept_discharge_from,inp.patient_id,inp.visit_id)";
            sql = string.Format(sql, dateStart, dateEnd, oper_id);
            return BaseEntityer.Db.ExecuteScalar(sql).ToString();
        }
        /// <summary>
        /// 按类别获取出院结算统计---计算退款总额
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="charge_type">统计类别</param>
        /// <returns></returns>
        public string GetOutHospitalSettlementStatistical_tf_allamount(string dateStart, string dateEnd, string charge_type, string oper_id)
        {
            string sql = string.Empty;
            sql = @"select sum(TF_ALLAMOUNT) from
                    (select  
                    PAT.PATIENT_ID,
                    (select name from pat_master_index where patient_id =pat.patient_id) pat_name,
                    charge_type,
                    charge_type_code,
                    charge_type rylb,
                    (select dept_name from dept_dict where dept_code =pat.dept_discharge_from) dept_discharge_from,
                    inp.settling_date,
                    pat.admission_date_time,
                    pat.discharge_date_time,
                    (select sum(amount) from prepayment_rcpt where patient_id = inp.patient_id and visit_id = inp.visit_id and (transact_type = '1' or transact_type = '2') and pay_way=1) prepay_cash,
                    (select sum(amount) from prepayment_rcpt where patient_id = inp.patient_id and visit_id = inp.visit_id and (transact_type = '1' or transact_type = '2') and pay_way=2) prepay_posh,
                    (select sum(amount) from prepayment_rcpt where patient_id = inp.patient_id and visit_id = inp.visit_id and (transact_type = '1' or transact_type = '2') and (pay_way=1 or pay_way=2)) prepay_allamount,
                    (select sum(return_amount) from pay_way where rcpt_no = inp.rcpt_no and money_type_id='1') tf_cash,
                    (select sum(return_amount) from pay_way where rcpt_no = inp.rcpt_no and money_type_id='2') tf_posh,
                    (select sum(return_amount) from pay_way where rcpt_no = inp.rcpt_no and (money_type_id='1' or money_type_id='2')) tf_allamount,
                    inp.operator_no,
                    inp.costs 
                    from inp_settle_master inp,pat_visit pat ,inp_settle_detail sett 
                    where inp.patient_id =pat.patient_id and inp.visit_id = pat.visit_id 
                    and inp.rcpt_no = sett.rcpt_no 
                    and inp.settling_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                    and inp.settling_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";
            if (oper_id != string.Empty)
            {
                sql += " and inp.operator_no = '{2}'";
            }
            if (charge_type != string.Empty)
            {
                if (charge_type.Trim().Equals("自费"))
                {
                    sql += " and (charge_type_code != '2' and charge_type_code != '13' and charge_type_code != '14' and charge_type_code != '16' and charge_type_code != '15' and charge_type_code != '17' and charge_type_code != '18' and charge_type_code != '22')";
                }
                else if (charge_type.Trim().Equals("公费"))
                {
                    sql += " and (charge_type_code = '2' or charge_type_code = '13' or charge_type_code = '14' or charge_type_code = '16' or charge_type_code = '15' or charge_type_code = '17' or charge_type_code = '18' or charge_type_code = '22')";
                }
            }
            sql += " group by pat.PATIENT_ID,pat.admission_date_time,pat.discharge_date_time,charge_type,charge_type_code,dept_discharge_from,inp.operator_no,inp.costs,inp.patient_id,inp.visit_id,inp.rcpt_no,inp.settling_date)";
            sql = string.Format(sql, dateStart, dateEnd, oper_id);
            return BaseEntityer.Db.ExecuteScalar(sql).ToString();
        }


        /// <summary>
        /// 按类别获取出院结算统计
        /// </summary>
        /// <param name="dateStart">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="charge_type">统计类别</param>
        /// <returns></returns>
        public HisCommon.BringObject GetOutHospitalSettlementStatistical(string dateStart, string dateEnd, string charge_type, string oper_id)
        {
            string sql = string.Empty;
            sql = @"select nvl(sum(prepay_cash),0) prepay_cash,
                           nvl(sum(prepay_posh),0) prepay_posh,
                           nvl(sum(prepay_allamount),0) prepay_allamount,
                           nvl(sum(tf_cash),0) tf_cash,
                           nvl(sum(tf_posh),0) tf_posh,
                           nvl(sum(tf_allamount),0) tf_allamount from
                    (select PAT.PATIENT_ID,
                           (select name
                              from pat_master_index
                             where patient_id = pat.patient_id) pat_name,
                           charge_type,
                           charge_type_code,
                           charge_type rylb,
                           (select dept_name
                              from dept_dict
                             where dept_code = pat.dept_discharge_from) dept_discharge_from,
                           inp.settling_date,
                           pat.admission_date_time,
                           pat.discharge_date_time,
                           (select sum(-amount)
                              from prepayment_rcpt
                             where patient_id = inp.patient_id
                               and visit_id = inp.visit_id
                               and transact_type = inp.transact_type
                               and inp.rcpt_no = balance_invoice
                               and pay_way != 2) prepay_cash,
                           (select sum(-amount)
                              from prepayment_rcpt
                             where patient_id = inp.patient_id
                               and visit_id = inp.visit_id
                               and transact_type = inp.transact_type
                               and inp.rcpt_no = balance_invoice
                               and pay_way = 2) prepay_posh,
                           (select sum(-amount)
                              from prepayment_rcpt
                             where patient_id = inp.patient_id
                               and visit_id = inp.visit_id
                               and transact_type = inp.transact_type
                               and inp.rcpt_no = balance_invoice) prepay_allamount,
                           (select sum(return_amount)
                              from pay_way
                             where rcpt_no = inp.rcpt_no
                               and money_type_id != '2') tf_cash,
                           (select sum(return_amount)
                              from pay_way
                             where rcpt_no = inp.rcpt_no
                               and money_type_id = '2') tf_posh,
                           (select sum(return_amount)
                              from pay_way
                             where rcpt_no = inp.rcpt_no) tf_allamount,
                           inp.operator_no,
                           inp.costs
                    from inp_settle_master inp,pat_visit pat ,inp_settle_detail sett 
                    where inp.patient_id =pat.patient_id and inp.visit_id = pat.visit_id 
                    and inp.rcpt_no = sett.rcpt_no 
                    and inp.settling_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                    and inp.settling_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";
            if (oper_id != string.Empty)
            {
                sql += " and inp.operator_no = '{2}'";
            }
            if (charge_type != string.Empty)
            {
                if (charge_type.Trim().Equals("自费"))
                {
                    sql += " and (charge_type_code != '2' and charge_type_code != '13' and charge_type_code != '14' and charge_type_code != '16' and charge_type_code != '15' and charge_type_code != '17' and charge_type_code != '18' and charge_type_code != '22')";
                }
                else if (charge_type.Trim().Equals("公费"))
                {
                    sql += " and (charge_type_code = '2' or charge_type_code = '13' or charge_type_code = '14' or charge_type_code = '16' or charge_type_code = '15' or charge_type_code = '17' or charge_type_code = '18' or charge_type_code = '22')";
                }
            }
            sql += " group by pat.PATIENT_ID,pat.admission_date_time,pat.discharge_date_time,charge_type,charge_type_code,dept_discharge_from,inp.operator_no,inp.costs,inp.patient_id,inp.visit_id,inp.rcpt_no,inp.settling_date,inp.transact_type)";
            sql = string.Format(sql, dateStart, dateEnd, oper_id);

            HisCommon.BringObject obj = new BringObject();
            System.Data.Common.DbDataReader dr = BaseEntityer.Db.ZDExecReader(sql);
            while (dr.Read())
            {
                obj.Exp01 = dr[0].ToString();
                obj.Exp02 = dr[1].ToString();
                obj.Exp03 = dr[3].ToString();
                obj.Exp05 = Convert.ToDecimal(dr[4].ToString());
            }
            if (!dr.IsClosed)
                dr.Close();
            return obj;
        }

        #endregion

        public static DataTable GetInvoiceUseState(string dateStart, string dateEnd, string type, string state, string no)
        {
            string sql = @"select
INVOICE_NO,
decode(INVOICE_STATE,'0','正常交费','1','退费','2','重打发票','3','跳号发票','4','重打作废') INVOICE_STATE,
TOT_COST,
FEE_OPER_CODE,
FEE_OPER_DATE,
decode(INVOICE_KIND,'00','门诊挂号','01','门诊收费','02','预交金','03','住院发票','04','账户发票') INVOICE_KIND,
decode(DAYBALANCED_FLAG,'0','未日结','1','已日结') DAYBALANCED_FLAG,
(select emp.user_name from users_staff_dict emp where emp.user_id = fr.DAYBALANCED_OPER_CODE and rownum = 1) DAYBALANCED_OPER_CODE,
DAYBALANCED_NO
PACT_NAME
from fin_invoiceinfo_record fr where fr.fee_oper_date >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
and fr.fee_oper_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss') and (invoice_kind = '{2}' or '{2}' = 'ALL')
and (invoice_state = '{3}' or '{3}' = 'ALL') and (INVOICE_NO = '{4}' or '{4}' = 'ALL')";
            sql = string.Format(sql, dateStart, dateEnd, type, state, no);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 四平第一人民医院门诊科室工作量报表
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<ShiPingRiBaoMZ> GetShiPingRiBaoMZByDate(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select  c.visit_dept,(select d.dept_name from dept_dict d where d.dept_code=c.visit_dept)  as dept_name ,count(*) as cn from clinic_master c  where  c.registering_date >= TO_DATE('{0}', 'yyyy-MM-dd hh24:mi:ss')
                  and c.registering_date <= TO_DATE('{1}', 'yyyy-MM-dd hh24:mi:ss')  
                  and c.RETURNED_OPERATOR is null
                  group by c.visit_dept order by c.visit_dept";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<ShiPingRiBaoMZ>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public List<ShiPingRiBaoCW> GetShiPingRiBaoCWByDate(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select
                     dept_code,--科室编码
                     dept_name,--科室名称
                     bed_standard,--实有床位
                     in_normal,--入院人数
                     out_normal,--出院人数
                     end_patient_num,--现有人数
                     (end_patient_num - bed_standard) as addjian,--加减床
                     round (  (sum_own_bed  / sum_bed_standard)*100 ,2 ) as per, --使用率
                     sum_bed_standard,
                     sum_own_bed
                     from 
                     (
                     select 
                     dept_code,
                     (select d.dept_name from dept_dict d where d.dept_code = rk.dept_code and rownum = 1) dept_name,
                     sum(decode(stat_date,trunc(to_date('{1}','yyyy-MM-dd hh24:mi:ss')),bed_standard ,0)) bed_standard,
                     sum(in_normal) in_normal,
                     sum(out_normal) out_normal,
                     sum(decode(stat_date,trunc(to_date('{1}','yyyy-MM-dd hh24:mi:ss')),end_patient_num,0)) end_patient_num,
                     sum(bed_standard) sum_bed_standard,
                     sum(end_patient_num) sum_own_bed
                     from rk_mrs_inhosdayreport rk
                     where stat_date >= to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                     and stat_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                     group by rk.dept_code order by rk.dept_code
                     )";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<ShiPingRiBaoCW>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public List<ShiPingRiBaoYJ> GetShiPingRiBaoYJByDate(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select e.performed_by,(select d.dept_name from dept_dict d where d.dept_code=e.performed_by)  as dept_name,e.exam_class,count(*) as cn 
from exam_master e where e.result_status='2' and e.EXAM_DATE_TIME >= to_date('{0}','yyyy-MM-dd hh24:mi:ss')
 and e.EXAM_DATE_TIME <= to_date('{1}','yyyy-MM-dd hh24:mi:ss') group by e.performed_by,e.exam_class";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<ShiPingRiBaoYJ>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 四平第一人民医院门诊挂号工作量
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<HisCommon.ClinicWorkLoad> GetShiPingClinicWorkLoadByDate(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select 
 (select d.dept_name  from dept_dict d where d.dept_code = m.visit_dept and rownum = 1) deptname,
 sum(case to_char(m.registering_date,'mm') when '01' then 1 else 0 end) January,
 sum(case to_char(m.registering_date,'mm') when '02' then 1 else 0 end) Februar,
 sum(case to_char(m.registering_date,'mm') when '03' then 1 else 0 end) March,
 sum(case to_char(m.registering_date,'mm') when '04' then 1 else 0 end) April,
 sum(case to_char(m.registering_date,'mm') when '05' then 1 else 0 end) May,
 sum(case to_char(m.registering_date,'mm') when '06' then 1 else 0 end) June,
 sum(case to_char(m.registering_date,'mm') when '07' then 1 else 0 end) July,
 sum(case to_char(m.registering_date,'mm') when '08' then 1 else 0 end) August,
 sum(case to_char(m.registering_date,'mm') when '09' then 1 else 0 end) September,
 sum(case to_char(m.registering_date,'mm') when '10' then 1 else 0 end) October,
 sum(case to_char(m.registering_date,'mm') when '11' then 1 else 0 end) November,
 sum(case to_char(m.registering_date,'mm') when '12' then 1 else 0 end) December,
 count(*) total
 from clinic_master m
 where m.registering_date >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
 and m.registering_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss')
 and m.RETURNED_OPERATOR is null
 group by visit_dept order by visit_dept";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.ClinicWorkLoad>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 四平第一人民医院病房主要指标
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<HisCommon.rk_mrs_inhosdayreport> GetShiPingDeptWorkLoadByDate(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select 
 (select d.dept_name  from dept_dict d where d.dept_code = rk.dept_code and rownum = 1) dept_code,
 sum(decode(trunc(to_date('{1}','yyyy-MM-dd hh24:mi:ss')),stat_date ,bed_standard ,0)) bed_standard,
 sum(in_normal) in_normal,
 sum(out_normal) out_normal,
 sum(bed_standard) sum_bed_standard,
 sum(end_patient_num) sum_own_standard,
 0 rate
 from rk_mrs_inhosdayreport rk
 where stat_date >= to_date('{0}','yyyy-MM-dd hh24:mi:ss')
 and stat_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss')
 group by rk.dept_code order by rk.dept_code";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.rk_mrs_inhosdayreport>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 四平第一人民医院病房工作报表
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<HisCommon.rk_mrs_inhosdayreport> GetShiPingDeptWorkRptByDate(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @"select 
 dept_code,
 (select d.dept_name from dept_dict d where d.dept_code = rk.dept_code and rownum = 1) dept_name,
 sum(decode(stat_date,trunc(to_date('{1}','yyyy-MM-dd hh24:mi:ss')),bed_add ,0)) bed_add,
 sum(decode(stat_date,trunc(to_date('{1}','yyyy-MM-dd hh24:mi:ss')),bed_standard ,0)) bed_standard,
 sum(decode(stat_date,trunc(to_date('{0}','yyyy-MM-dd hh24:mi:ss')),begin_patient_num,0)) begin_patient_num,
 sum(in_normal) in_normal,
 sum(out_normal) out_normal,
 sum(decode(stat_date,trunc(to_date('{1}','yyyy-MM-dd hh24:mi:ss')),end_patient_num,0)) end_patient_num,
 sum(in_transfer) in_transfer,
 sum(out_transfer) out_transfer,
 0 appointment,
 0 rate,
 sum(bed_standard) sum_bed_standard,
 sum(end_patient_num) sum_own_standard
 from rk_mrs_inhosdayreport rk
 where stat_date >= to_date('{0}','yyyy-MM-dd hh24:mi:ss')
 and stat_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss')
 group by rk.dept_code order by rk.dept_code";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.rk_mrs_inhosdayreport>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 四平第一人民医院病房工作报表
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<HisCommon.rk_mrs_inhosdayreport> GetShipingWorkLoadSByDate(string dateStart, string dateEnd)
        {
            string sql = string.Empty;
            sql = @" select *
                      from V_SPWorkReport t
                     where p_view_param.set_begin('{0}') =
                           '{0}'
                       and p_view_param.set_end('{1}') = '{1}' ";
            sql = string.Format(sql, dateStart, dateEnd);
            return DataSetToEntity.DataSetToT<HisCommon.rk_mrs_inhosdayreport>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 医技科室收入统计表
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BringSpringObject> GetMedicalDeptSR(string startDate, string endDate, string dept, string type, ref string errMsg)
        {
            string sql = string.Empty;
            /*sql = @"select clinic_item_code,item_name,price,qty,charge
  from (select dept,
               clinic_item_code,
               item_name,
               round (sum(cost1) / sum(qty),2) price,
               sum(qty) qty,
               sum(cost1) charge,
               '1' type
          from (select a.dept_name dept,
                       t.clinic_item_code clinic_item_code,
                       s.item_name item_name,
                       t.serial_no serial_no,
                       round (sum(t.charges / decode(t.amount,0,1,t.amount)),2) price,                   
             round (count(t.amount) / decode(count(t.serial_no),0,1,count(t.SERIAL_NO)),2) qty,
                       sum(t.charges) cost1 
                  from v_outp_bill_items t, clinic_item_dict s, dept_dict a 
                 where t.clinic_item_code = s.item_code 
                   and a.dept_code = t.performed_by_other
                   and t.visit_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                   and t.visit_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
				   and (t.performed_by_other in ({2})) 
                 group by a.dept_name,
                          t.clinic_item_code,
                          s.item_name,
                          t.serial_no) q
         group by dept, clinic_item_code, item_name
        union all
        select dept,
               clinic_item_code,
               item_name,
               round (sum(cost1) / sum(qty),2) price,
               sum(qty) qty,
               sum(cost1) charge,
               '2' type
          from (select a.dept_name dept,
                       t.clinic_item_code clinic_item_code,
                       s.item_name item_name,
                       t.orders_no orders_no,
                       sum( round (t.charges / decode(t.amount,0,1,t.amount),2)) price,
                       round (count(t.amount) / decode(count(t.orders_no),0,1,count(t.orders_no)),2) qty,
                       sum(t.charges) cost1
                  from v_inp_bill_detail t, clinic_item_dict s, dept_dict a
                 where t.clinic_item_code = s.item_code
                   and t.performed_by_other = a.dept_code
                   and t.ORDERS_NO is not null
                   and (t.performed_by_other in ({2})) 
                   and t.billing_date_time >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                   and t.billing_date_time <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                 group by a.dept_name,
                          t.clinic_item_code,
                          s.item_name,
                          t.orders_no) q
         group by dept, clinic_item_code, item_name) j
 where j.type = '{3}'
    or 'ALL' = '{3}'
";*/
            sql = @"select   clinic_item_code,itemname, ROUND(sum(costs)/sum(vamount),2) price,sum(vamount) qty ,sum(costs) charge from 
(select 
distinct 
clinic_item_class,
clinic_item_code,
 v.serial_no,
 (select item_name from  clinic_item_dict  where item_code=v.clinic_item_code and item_class= v.clinic_item_class) itemname,
  ( max(ROUND(amount / 
(nvl((
  select sum(amount) from clinic_vs_charge vs where vs.clinic_item_class= v.clinic_item_class and
  vs.clinic_item_code=v.clinic_item_code AND ROWNUM=1   and charge_item_code= v.item_code),1) 
 ),1))  ) vamount,
 sum(v.charges)  charges 
,sum(v.costs) costs,performed_by
 from v_fee_statistic_new v  
  where  
      v.operdate >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
           and v.operdate <= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
  and (v.performed_by  in ({2}))  and (v.type = '{3}' or '{3}'='null' )  
 group by  v.performed_by, clinic_item_class,clinic_item_code,v.serial_no having sum(v.charges)>0   )
 WHERE ITEMNAME IS NOT NULL
 group by  performed_by,clinic_item_class,clinic_item_code,itemname ";
            List<BringSpringObject> listBringSpring = new List<BringSpringObject>();
            try
            {
                if (type == "0")
                    sql = string.Format(sql, startDate, endDate, dept, "null");
                else if (type == "1")
                    sql = string.Format(sql, startDate, endDate, dept, "O");
                else

                    sql = string.Format(sql, startDate, endDate, dept, "I");
                DataTable dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return listBringSpring;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = dt.Rows[i][0].ToString();
                    obj.Name = dt.Rows[i][1].ToString();
                    obj.Memo = dt.Rows[i][2].ToString();
                    obj.User01 = dt.Rows[i][3].ToString();
                    obj.User02 = dt.Rows[i][4].ToString();
                    listBringSpring.Add(obj);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
            return listBringSpring;
        }

        /// <summary>
        /// 医技科室工作量报表(按科室)
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        /// 2013-12-7 by li 修改为时间段查询
        public List<HisCommon.DataEntity.BringSpringObject> GetMedicalDeptWork(string startDate, string dateEnd, string type, ref string errMsg)
        {
            string sql = string.Empty;
            sql = @"select dept, sum(qty)
                      from (select s.dept_name dept, count(t.amount) qty, '1' type
                              from OUTP_BILL_ITEMS t, dept_dict s
                             where t.performed_by = s.dept_code
                               and s.clinic_attr = '7'
                               and (t.visit_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and
                                   t.visit_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))
                             group by s.dept_name
                            union all
                            select s.dept_name dept, count(t.amount) qty, '2' type
                              from inp_bill_detail t, dept_dict s
                             where t.performed_by = s.dept_code
                               and s.clinic_attr = '7'
                               and (t.billing_date_time >=
                                   to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and
                                   t.billing_date_time <=
                                   to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))
                             group by s.dept_name)
                     where type = '{2}'
                        or 'ALL' = '{2}'
                     group by dept";
            List<BringSpringObject> listBringSpring = new List<BringSpringObject>();
            try
            {
                if (type == "0")
                    sql = string.Format(sql, startDate, dateEnd, "ALL");
                else
                    sql = string.Format(sql, startDate, dateEnd, type);
                DataTable dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return listBringSpring;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BringSpringObject obj = new BringSpringObject();
                    obj.ID = dt.Rows[i][0].ToString();
                    obj.Name = dt.Rows[i][1].ToString();
                    listBringSpring.Add(obj);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return listBringSpring;
            }
            return listBringSpring;
        }
        /// <summary>
        /// 四平第一人民医院患者费用明细查询
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OUTP_BILL_ITEMS> GetShipingFeeDetailByDate(string dateStart, string dateEnd, string deptCode, string doctor, string patientId)
        {
            string sql = string.Empty;
            sql = @" select 
 					patient_name,
				 	vf.item_code,
 					vf.item_name,
 					vf.item_spec,
 					vf.amount,
 					vf.costs,
 					vf.operdate as visit_date,
 					(select type_name  from item_type t where t.type_code = vf.item_class) item_class,
 					vf.user_name order_doctor ,
 					performed_dept_name performed_by
 					from v_fee_statistic_new vf 
 					where vf.operdate >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
 					and vf.operdate <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') 
 					and (vf.performed_dept_name = '{2}' or 'ALL' = '{2}')
 					and vf.user_name like '%{3}%'
 					and vf.patient_name like '%{4}%'
 					order by performed_by,order_doctor,operdate";
            sql = string.Format(sql, dateStart, dateEnd, deptCode, doctor, patientId);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OUTP_BILL_ITEMS>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 门诊收款结算单报表(四平)
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable QuerySFAccountSP(string dateStart, string dateEnd, string userID)
        {
            string sql = @"select o.acct_no as 结账序号,
                u.user_name as 收款员,
                o.operator_no as 账号,
                o.acct_date as 结账日期,
                o.rcpts_num as 收据张数,
                o.refund_num as 退费张数,
                to_char(o.refund_amount,'999999999.99') as 退费金额,
                to_char(o.total_costs,'999999999.99') as 计价总额,
                to_char(o.total_incomes,'999999999.99') as 应收总额
                from outp_acct_master o 
                left join users_staff_dict u
                on o.operator_no=u.user_id
                where o.acct_date>to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                and o.acct_date<=to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                and (o.operator_no='{2}' or '{2}' is null)
                order by 结账日期 ";
            sql = string.Format(sql, dateStart, dateEnd, userID);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 2013-12-10 by li 查询用根据条件查询挂号患者
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="dept">挂号部门</param>
        /// <param name="isJz">是否接诊</param>
        /// <returns></returns>
        public IList<CLINIC_MASTER> GetPatients(string beginDate, string endDate, string dept, int isJz)
        {
            string sql =
          @" SELECT CLINIC_MASTER.VISIT_DATE,   
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
         CLINIC_MASTER.CHARGE_TYPE_CODE,  
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
         0 as charge_indicator
    FROM CLINIC_MASTER  
   WHERE CLINIC_MASTER.registering_date >= to_date('{0}','yyyy-MM-dd hh24:mi:ss') AND  
         CLINIC_MASTER.registering_date <= to_date('{1}','yyyy-MM-dd hh24:mi:ss') AND  
         ( CLINIC_MASTER.VISIT_DEPT = '{2}'  or
         '{2}' is null) AND  
         nvl(CLINIC_MASTER.ADMIS,'0') = {3} and clinic_master.returned_operator is null";
            sql = string.Format(sql, new object[] { beginDate, endDate, dept, isJz });
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(patients);
        }

        /// <summary>
        ///查询出院结算统计---主要为显示医保信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOutHospital_SI(string starDate, string endDate, string PactType, string RcptNo, string Name, string Operator_no, string type)
        {
            //2013-12-18 by yuxi 医保类别项目置空
            string sql = @"select m.patient_id as 患者ID,
       p.name as 患者姓名,
       a.charge_type 收费类别,
        '' as  医保类别,
       --v.item_code as 项目编码,
       --v.item_name as 项目名称,
       --sum(to_char(v.charges, '999999999999.99')) 金额,
       to_char(m.costs, '999999999999.99') 金额,
       --v.amount  数量,
       (select u.user_name
          from users_staff_dict u
         where u.user_id = m.operator_no) as 结算员,
       m.rcpt_no as 发票号,
       m.settling_date as 结算日期,
       s.PAY_COST as 账户支付金额,
       s.PUB_COST as 统筹支付金额,
       s.OWN_COST as 现金支付金额,
       s.OFFICIAL_COST as 公务员账户支付金额,
       s.OVER_COST as 大额补助支付金额,
       s.OWN_SUPPLE_COST as 个人补充支付,
       s.HELP_ALLOWANCES_COST as 低保救助支付,
       s.ENTERPRISE_SUPPLE_COST as 企业补充支付,
       s.HELP_OWN_COST as 救助金支付金额 
  from inp_bill_detail v 
  right join (select * from inp_settle_master where ('{0}' is null or rcpt_no = '{0}') and ('{1}' is null or operator_no = '{1}')) m on v.rcpt_no = m.rcpt_no 
  left join pat_master_index p on p.patient_id = m.patient_id 
  left join pat_visit a on a.patient_id = m.patient_id and a.visit_id = m.visit_id 
  left join si_info s on s.invoice_no = v.rcpt_no 
  where 1 = 1 and  ('{2}' is null or p.name like '%{2}%') and ('{3}' is null or a.charge_type = '{3}') ";
            //开始日期
            if (starDate.Trim() != "")
            {
                sql = sql + " and m.settling_date >= to_date('" + starDate + "', 'yyyy-MM-dd hh24:mi:ss')";
            }
            if (endDate.Trim() != "")
            {
                sql = sql + " and m.settling_date <= to_date('" + endDate + "', 'yyyy-MM-dd hh24:mi:ss')";
            }

            if (type.Trim().Equals("收款"))
            {
                sql = sql + " and m.transact_type = 3";
            }
            else if (type.Trim().Equals("退款"))
            {
                sql = sql + " and m.transact_type = 4";
            }
            sql = sql + @"group by m.patient_id,p.name,a.charge_type,m.operator_no,m.rcpt_no,m.settling_date,s.PAY_COST,s.PUB_COST,s.OWN_COST,
                          s.OFFICIAL_COST,s.OVER_COST,s.OWN_SUPPLE_COST,s.HELP_ALLOWANCES_COST,s.ENTERPRISE_SUPPLE_COST,s.HELP_OWN_COST,m.costs  
                          order by m.patient_id,m.settling_date";
            sql = string.Format(sql, RcptNo, Operator_no, Name, PactType);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 2013-12-31 by yuxi 患者在院信息查询---为了查看患者是否接诊手术
        /// </summary>
        /// <returns></returns>
        public PATS_IN_HOSPITAL_ACCEPT_FLAG[] GetPatInHospital_AcceptsFlag(string patientName, string deptcode, string deginDate, string endDate, string AcceptsFlag)
        {
            string sql = string.Empty;
            sql = @"select m.name as 患者姓名,
                    p.visit_id 住院标识,
                    p.patient_id as 患者ID,
                    (select dept_dict.dept_name from dept_dict where dept_dict.dept_code = p.dept_code) 所在科室,
                    (select users_staff_dict.user_name from users_staff_dict where users_staff_dict.user_id = p.doctor_in_charge) 医生,
                    (p.prepayments-p.total_charges) 预交金余额,
                    p.admission_date_time 入院时间,
                    (CASE p.accepts_flag 
                    WHEN '0' then '未接诊' 
                    WHEN '1' then '已接诊' 
                    else '未接诊' end )是否接诊手术,
                    (CASE v.state 
                         WHEN 'R' then '住院登记' 
                         WHEN 'I' then '病房接诊' 
                         WHEN 'B' then '出院登记' 
                         WHEN 'O' then '出院结算' 
                         WHEN 'P' then '预约出院' 
                         else '无费退院' end) as 状态 
                  from pats_in_hospital p 
                  left join pat_visit v on p.patient_id = v.patient_id 
                  left join pat_master_index m on m.patient_id = p.patient_id
                  left join OPERATION_SCHEDULE d on p.patient_id=d.patient_id
                  where 1=1
                  and d.ack_indicator='0'";
            //患者姓名
            if (patientName.Trim() != "")
            {
                sql = sql + " and m.name like '%" + patientName + "%'";
            }
            //科室ID
            if (deptcode.Trim() != "")
            {
                sql = sql + " and p.dept_code = '" + deptcode + "'";
            }
            //入院日期
            if (deginDate.Trim() != "")
            {
                sql = sql + " and p.admission_date_time >= to_date('" + deginDate + "', 'yyyy-MM-dd hh24:mi:ss')";
            }
            if (endDate.Trim() != "")
            {
                sql = sql + " and p.admission_date_time <= to_date('" + endDate + "', 'yyyy-MM-dd hh24:mi:ss')";
            }
            //是否接诊手术标识
            if (AcceptsFlag.Trim() == "未接诊")
            {
                sql = sql + " and p.accepts_flag != '1'";
            }
            else if (AcceptsFlag.Trim() == "已接诊")
            {
                sql = sql + " and p.accepts_flag = '1'";
            }
            sql = sql + " order by p.admission_date_time DESC";
            return DataSetToEntity.DataSetToT<PATS_IN_HOSPITAL_ACCEPT_FLAG>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
        }

        /// <summary>
        /// 2013-12-31 by yuxi 患者在院信息修改---修改是否接诊手术的标识
        /// </summary>
        /// <returns></returns>
        public int ModifyPATS_IN_HOSPITAL_ACCEPT_FLAG(BaseEntityer db, PATS_IN_HOSPITAL_ACCEPT_FLAG p)
        {
            string sql = @"UPDATE PATS_IN_HOSPITAL 
                        SET PATS_IN_HOSPITAL.ACCEPTS_FLAG = '{2}' 
                        WHERE PATS_IN_HOSPITAL.PATIENT_ID = '{0}' and PATS_IN_HOSPITAL.VISIT_ID = '{1}'";
            object[] param = new object[] { p.患者ID, p.住院标识, p.是否接诊手术 };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取费别费用统筹部分统计
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetFeeChargeTypeCosts(string dateStart, string dateEnd, string type, ref string errMsg)
        {
            string sql = @"select a.charge_type_code, --费别编码
                               a.charge_type_name, --费别名称
                               sum(a.tot_cost) as tot_cost, --医疗费总额
                               sum(a.pay_cost) as pay_cost, --账户支付金额
                               sum(a.pub_cost) as pub_cost, --统筹支付金额
                               sum(a.own_cost) as own_cost, --现金支付金额
                               sum(a.official_cost) as official_cost, --公务员账户支付金额
                               sum(a.over_cost) as over_cost, --大额补助支付金额
                               sum(a.own_supple_cost) as own_supple_cost, --个人补充支付
                               sum(a.help_allowances_cost) as help_allowances_cost, --低保救助支付
                               sum(a.enterprise_supple_cost) as enterprise_supple_cost, --企业补充支付
                               sum(a.help_own_cost) as help_own_cost --救助金支付金额
                          from (select c.charge_type_code,
                                       c.charge_type_name,
                                       s.tot_cost, --医疗费总额
                                       s.pay_cost, --账户支付金额
                                       s.pub_cost, --统筹支付金额
                                       s.own_cost, --现金支付金额
                                       s.official_cost, --公务员账户支付金额
                                       s.over_cost, --大额补助支付金额
                                       s.own_supple_cost, --个人补充支付
                                       s.help_allowances_cost, --低保救助支付
                                       s.enterprise_supple_cost, --企业补充支付
                                       s.help_own_cost --救助金支付金额
                                  from siinfo s
                                  left join charge_type_dict c
                                    on s.pact_code = c.charge_type_code
                                 where {2}
                                   and s.oper_date >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                   and s.oper_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')) a
                         group by a.charge_type_code, a.charge_type_name";
            try
            {
                if (type == "ALL")
                {
                    string sqla = @" (s.type_code = '1' or s.type_code = '2') ";
                    sql = string.Format(sql, dateStart, dateEnd, sqla);
                }
                else if (type == "I")
                {
                    string sqla = @" s.type_code = '2' ";
                    sql = string.Format(sql, dateStart, dateEnd, sqla);
                }
                else
                {
                    string sqla = @" s.type_code = '1' ";
                    sql = string.Format(sql, dateStart, dateEnd, sqla);
                }
                return BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message + ex.StackTrace;
                return null;
            }
        }

        /// <summary>
        /// 获取费别费用各核算科目明细
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public IList<REPORT_PAY_WAY_SUMMARY> GetFeeChargeTypeDetail(string dateStart, string dateEnd, string type, ref string errMsg)
        {
            string sql = string.Empty;
            try
            {
                if (type == "ALL")
                {
                    sql = @"select *
                          from (select sum(a.charges) as INCOME,
                                       a.class_on_reckoning as PAY_WAY_CODE,
                                       a.charge_type as OPERATOR_NO
                                  from (select o.charges, o.class_on_reckoning, s.charge_type
                                          from outp_bill_items o
                                          left join outp_rcpt_master s
                                            on o.rcpt_no = s.rcpt_no
                                         where s.visit_date >=
                                               to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                           and s.visit_date <=
                                               to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')) a
                                 group by a.charge_type, a.class_on_reckoning
                                union all
                                select sum(b.charges) as INCOME,
                                       b.class_on_reckoning as PAY_WAY_CODE,
                                       b.charge_type as OPERATOR_NO
                                  from (select i.charges, i.class_on_reckoning, p.charge_type
                                          from inp_bill_detail i
                                          left join pat_visit p
                                            on i.patient_id = p.patient_id
                                           and i.visit_id = p.visit_id
                                         where i.billing_date_time >=
                                               to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                           and i.billing_date_time <=
                                               to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')) b
                                 group by b.charge_type, b.class_on_reckoning) c
                         order by c.OPERATOR_NO, c.PAY_WAY_CODE";
                    sql = string.Format(sql, dateStart, dateEnd);
                }
                else if (type == "I")
                {
                    sql = @"select sum(b.charges) as INCOME,
                               b.class_on_reckoning as PAY_WAY_CODE,
                               b.charge_type as OPERATOR_NO
                          from (select i.charges, i.class_on_reckoning, p.charge_type
                                  from inp_bill_detail i
                                  left join pat_visit p
                                    on i.patient_id = p.patient_id
                                   and i.visit_id = p.visit_id
                                 where i.billing_date_time >=
                                       to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                   and i.billing_date_time <=
                                       to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')) b
                         group by b.charge_type, b.class_on_reckoning) c
                         order by b.charge_type, b.class_on_reckoning";
                    sql = string.Format(sql, dateStart, dateEnd);
                }
                else
                {
                    sql = @"select sum(a.charges) as INCOME,
                               a.class_on_reckoning as PAY_WAY_CODE,
                               a.charge_type as OPERATOR_NO
                          from (select o.charges, o.class_on_reckoning, s.charge_type
                                  from outp_bill_items o
                                  left join outp_rcpt_master s
                                    on o.rcpt_no = s.rcpt_no
                                 where s.visit_date >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                   and s.visit_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')) a
                         group by a.charge_type, a.class_on_reckoning
                         order by a.charge_type, a.class_on_reckoning";
                    sql = string.Format(sql, dateStart, dateEnd);
                }
                return DataSetToEntity.DataSetToT<REPORT_PAY_WAY_SUMMARY>(BaseEntityer.Db.GetDataSet(sql)).ToArray();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message + ex.StackTrace;
                return null;
            }
        }
    }
}