using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HisCommon.DataEntity;

namespace HisDBLayer
{   //医生相关业务
    public class DoctorManager
    {
        /// <summary>
        /// 查询医生常用项目
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ITEM_OFFEN_USE> QueryItemOffenUses(string doctor, string dept)
        {
            string sql = @"SELECT * FROM ITEM_OFFENUSE t
where (t.doctor='{0}' and t.type=3)
or (t.dept='{1}' and t.type=2)
or t.type=1";
            sql = string.Format(sql, doctor, dept);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<ITEM_OFFEN_USE>(ds).ToList();
        }
        /// <summary>
        /// 新插入医生常用项目
        /// </summary>
        /// <returns></returns>
        public int InsertItemOffenUse(ITEM_OFFEN_USE o)
        {
            string sql = @"insert into ITEM_OFFENUSE
  (item_code,
   item_class,
   drug_spec,
   firm_id,
   price,
   package_spec,
   doctor,
   dept,
   createdate,
   item_name,
   type)
values
  ('{0}',
   '{1}',
   '{2}',
   '{3}',
   {4},
   '{5}',
   '{6}',
   '{7}',
   to_date('{8}', 'yyyy-MM-dd hh24:mi:ss'),
   '{9}',
    {10})
";
            object[] param = new object[] {o.ITEM_CODE, 
                                          o.ITEM_CLASS,
                                          string.IsNullOrEmpty(o.DRUG_SPEC)?"空":o.DRUG_SPEC,
                                          string.IsNullOrEmpty(o.FIRM_ID)?"空":o.FIRM_ID, 
                                          string.IsNullOrEmpty(o.PRICE)?"null":o.PRICE.ToString(), 
                                          string.IsNullOrEmpty(o.PACKAGE_SPEC)?"空":o.PACKAGE_SPEC, 
                                          o.DOCTOR,
                                          o.DEPT,
                                          o.CREATEDATE,
                                          o.ITEM_NAME,
                                          o.TYPE};
            sql = string.Format(sql, param);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除医生常用项目
        /// </summary>
        /// <returns></returns>
        public int DeleteItemOffenUse(ITEM_OFFEN_USE o)
        {
            string sql = @"delete ITEM_OFFENUSE t
where t.item_code='{0}'
and t.item_class='{1}'
and t.drug_spec='{2}'
and t.firm_id='{3}'
and t.package_spec='{4}'
and t.doctor='{5}'
and t.type='{6}'
";
            object[] param = new object[] {o.ITEM_CODE, 
                                          o.ITEM_CLASS,
                                          o.DRUG_SPEC,
                                          o.FIRM_ID, 
                                          o.PACKAGE_SPEC, 
                                          o.DOCTOR,
                                          o.TYPE
                                          };
            sql = string.Format(sql, param);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 获得组套选择数据源
        /// </summary>
        /// <param name="currentDrugDeptA">西药取药科室</param>
        /// <param name="currentDrugDeptB">中药取药科室</param>
        /// <returns></returns>
        public DataTable QuerySelectZT(List<string> depts)
        {
            //2013-10-24 BY LI 组套选取的数据源与医生单独开医嘱数据源不一致，修改取数据源视图
            string sql = @"select *
  from OUTP_SELECT_ZT_NEW t
 where  t.storage in ({0}) or (t.class!='A' and t.class!='B') ";
            var str = "";
            foreach (var d in depts)
            { 
                var temp= "'" + d + "'";
                str = str + temp + ",";
            }
            str = str.TrimEnd(',');
            sql = string.Format(sql, str);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 根据指定id删除组套
        /// </summary>
        /// <param name="ID">组套id</param>
        /// <returns></returns>
        public int DelZTbyId(string ID,BaseEntityer db)
        {
            string sql = @"delete from outp_zt t where t.id='{0}'";
            sql = string.Format(sql, ID);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 插入一条新的组套
        /// </summary>
        /// <param name="o"></param>
        public int InsertZT(HisCommon.DataEntity.OUTP_ZT o, BaseEntityer db)
        {
            //2013-12-5 by li 组套增加执行科室
            string sql = @"insert into OUTP_ZT
                              (id,
                               item_id,
                               type,
                               doctor,
                               dept,
                               inputcode,
                               code,
                               class,
                               exam_sub_class,
                               exam_class,
                               name,
                               firm_id,
                               drug_spec,
                               package_SPEC,
                               zt_name,
                               amount,
                               ADMINISTRATION,
                            FREQUENCY,
                            inp_tag,
                            inp_order_type,
                            PERFORMED_BY,
                            ORDER_NO)
                            values
                              ('{0}',
                               {1},
                               {2},
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
                               {15},
                            '{16}',
                            '{17}',
                            {18},
                            {19},
                            '{20}',
                            '{21}')";
            object[] param = new object[] {o.ID, 
                                          o.ITEM_ID,
                                          o.TYPE,
                                          o.DOCTOR, 
                                          o.DEPT, 
                                          o.INPUTCODE, 
                                          o.CODE,
                                          o.CLASS,
                                          o.EXAM_SUB_CLASS,
                                          o.EXAM_CLASS,
                                          o.NAME,
                                          o.FIRM_ID,
                                          o.DRUG_SPEC,
                                          o.PACKAGE_SPEC,
                                          o.ZT_NAME,
                                          o.AMOUNT,
                                          o.ADMINISTRATION,
                                          o.FREQUENCY,
                                          o.INP_TAG,
                                          o.INP_ORDER_TYPE,
                                          o.PERFORMED_BY,
                                          o.ORDER_NO};
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 查询医生可用的组套项目
        /// </summary>
        /// <param name="doctor">医生编码</param>
        /// <param name="dept">科室编码</param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OUTP_ZT> QueryZtList(string doctor, string dept)
        {
            string sql = @"SELECT * FROM OUTP_ZT t
where (t.doctor='{0}' and t.type=3)
or (t.dept='{1}' and t.type=2)
or t.type=1 order by id,item_id";
            sql = string.Format(sql, doctor, dept);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_ZT>(ds).ToList();
        }
        /// <summary>
        /// 查询门诊科室费用
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOutpFeeByDept(string visitNo, string name, string dateBegin, string dateEnd, string deptCode, string execDeptCode)
        {
            //2013-12-6 by li 数据库查询语句时间条件错误
            //2013-12-11 by li 增加执行科室显示
            string sql = @"SELECT b.VISIT_DATE,
       b.VISIT_NO ,
       b.RCPT_NO  ,
       b.ITEM_NO,
       b.ITEM_CLASS,
       b.CLASS_ON_RCPT,
       b.ITEM_CODE,
       b.ITEM_NAME,
       b.ITEM_SPEC,
       b.AMOUNT,
       b.UNITS,
       b.PERFORMED_BY,
       d.dept_name,
       b.COSTS,
       b.CHARGES,
       b.SERIAL_NO,
       b.BATCHNO,
       b.D_ITEM_NO,
       b.APPOINT_NO,
       r.NAME,
       r.CHARGE_INDICATOR
  FROM OUTP_BILL_ITEMS b, OUTP_RCPT_MASTER r,dept_dict d
 WHERE b.RCPT_NO = r.RCPT_NO and b.performed_by=d.dept_code
  and (b.PERFORMED_BY = '{5}' or '{5}' is null)  
   and ((b.order_dept='{4}' and b.performed_by!='{4}') or '{4}' is null)
  AND b.VISIT_DATE >= to_date('{2}', 'yyyy-MM-dd hh24:mi:ss') 
  AND b.VISIT_DATE <= to_date('{3}', 'yyyy-MM-dd hh24:mi:ss')
  and ('{0}' is null or b.visit_no='{0}')
  and ('{1}' is null or r.name='{1}')
  order by b.visit_no,b.visit_date";
            sql = string.Format(sql, visitNo, name,dateBegin,dateEnd,deptCode,execDeptCode);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 查询患者历史医嘱信息
        /// </summary>
        /// <param name="visitNo"></param>
        /// <param name="visitDate"></param>
        /// <returns></returns>
        public IList<HisCommon.DataEntity.OUTP_SELECT_ORDER> QueryPatientHisOrders(string visitNo, string visitDate)
        {
            string sql = @"select * from outp_patient_orders t
where t.visit_no='{0}' 
and t.visit_date=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
and t.charge_indicator='1'";
            sql = string.Format(sql, visitNo, visitDate);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<OUTP_SELECT_ORDER>(ds);
            return list;
        }

        #region 日志记录
        /// <summary>
        /// 根据制定ID查询组套信息
        /// </summary>
        /// <param name="ID">组套id</param>
        /// <returns></returns>
        public List<OUTP_ZT> QueryZTbyId(string ID, BaseEntityer db)
        {
            string sql = @"select  * from outp_zt t where t.id='{0}'";
            sql = string.Format(sql, ID);
            return DataSetToEntity.DataSetToT<OUTP_ZT>(db.GetDataSet(sql)).ToList();
        } 
        #endregion
    }
}
