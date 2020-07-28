using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Collections;
using HisCommon.DataEntity;
using HisCommon;
using System.Data.Common;
using System.IO;

namespace HisDBLayer
{
    /// <summary>
    /// 住院医生站
    /// </summary>
    public class InPatientWorkStation
    {
        /// <summary>
        /// 得到住院诊断类型字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetDiagnosisTypeDict()
        {
            string sql = @"select d.diagnosis_type_code,d.diagnosis_type_name from diagnosis_type_dict d";
            var dict = BaseEntityer.Db.GetDataTable(sql).AsEnumerable().ToDictionary(x => x["diagnosis_type_code"].ToString(), y => y["diagnosis_type_name"].ToString());
            return dict;
        }
        /// <summary>
        /// 得到患者住院状态字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetInpStateDict()
        {
            string sql = @"select d.state_id,d.state_name from instate_dict d";
            var dict = BaseEntityer.Db.GetDataTable(sql).AsEnumerable().ToDictionary(x => x["state_id"].ToString(), y => y["state_name"].ToString());
            return dict;
        }
        /// <summary>
        /// 删除患者诊断
        /// </summary>
        /// <returns></returns>
        public int DeleteInPatientDiagnose(string patientId, int visitId, BaseEntityer db)
        {
            string sql = @"delete from diagnosis t where t.patient_id='{0}' and t.visit_id={1}";
            sql = sql.SqlFormate(patientId, visitId);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 增加一条患者诊断
        /// </summary>
        /// <returns></returns>
        public int InsertInPatientDiagnose(DIAGNOSIS o, BaseEntityer db)
        {
            string sql = @"insert into diagnosis
  (PATIENT_ID,
   VISIT_ID,
   DIAGNOSIS_TYPE,
   DIAGNOSIS_NO,
   DIAGNOSIS_DESC,
   DIAGNOSIS_DATE,
   TREAT_DAYS,
   TREAT_RESULT,
   OPER_TREAT_INDICATOR,
   DIAG_CODE,
   DIAGNOSE_IDENTIFICATION,
   OPER_CODE)
values
  ('{0}',
   {1},
   '{2}',
   {3},
   '{4}',
   to_date('{5}', 'yyyy-MM-dd hh24:mi:ss'),
   {6},
   '{7}',
   {8},
   '{9}',
   '{10}','{11}')";
            object[] os = new object[] { o.PATIENT_ID,o.VISIT_ID,o.DIAGNOSIS_TYPE,
                                       o.DIAGNOSIS_NO,o.DIAGNOSIS_DESC,o.DIAGNOSIS_DATE,
                                       o.TREAT_DAYS,o.TREAT_RESULT,o.OPER_TREAT_INDICATOR,
                                       o.DIAG_CODE,o.DIAGNOSE_IDENTIFICATION,o.OPER_CODE};

            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        ///  更新住院住诊断
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="diagnoseName"></param>
        /// <returns></returns>
        public int UpdatePatInHosptialDiagnose(BaseEntityer db, string patientID, string visitID, string diagnoseName)
        {
            string sql = @"
                            UPDATE pats_in_hospital t
                            SET t.diagnosis = '{2}'
                            WHERE t.patient_id = '{0}'
                            AND t.visit_id = '{1}'";

            sql = string.Format(sql, patientID, visitID, diagnoseName);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询患者诊断信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public List<DIAGNOSIS> QueryInpatientDiagnose(string patientId, int visitId)
        {
            string sql = @"select * from diagnosis t
where t.patient_id='{0}' and t.visit_id={1}";
            sql = sql.SqlFormate(patientId, visitId);
            var ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<DIAGNOSIS>(ds).ToList();
        }
        /// <summary>
        /// 查询住院患者最大医嘱号
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public int GetMaxOrderNoPatient(string patientId, int visitId)
        {
            string sql = @"select max(t.order_no) from doctor_orders t where t.patient_id='{0}' and t.visit_id={1}";
            sql = sql.SqlFormate(patientId, visitId);
            var orderNo = BaseEntityer.Db.ExecuteScalar(sql);
            if (orderNo != DBNull.Value)
                return int.Parse(orderNo.ToString());
            else return 0;
        }
        /// <summary>
        /// 查询住院患者最大医嘱号
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public int GetMaxOrderNo(BaseEntityer db, string patientId, int visitId)
        {
            string sql = @"select max(t.order_no) from doctor_orders t where t.patient_id='{0}' and t.visit_id={1}";
            sql = sql.SqlFormate(patientId, visitId);
            var orderNo = db.ExecuteScalar(sql);
            if (orderNo != DBNull.Value)
                return int.Parse(orderNo.ToString());
            else return 0;
        }

        /// <summary>
        /// 根据科室编号查询在院患者信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public List<PATS_IN_HOSPITAL> GetListPatInHostpitalByCode(string dept_code)
        {
            string sql = @"select * from PATS_IN_HOSPITAL t where t.dept_code='{0}' order by  t.Bed_No asc";
            sql = sql.SqlFormate(dept_code);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PATS_IN_HOSPITAL>(ds).ToList();
            else
                return null;
        }
        /// <summary>
        /// 根据科室编号查询在院患者信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public List<PAT_VISIT> GetListPatVisitByCode(string dept_code)
        {
            string sql = @"SELECT p.pub_alarm, --最低警戒线
       p.pub_cost, --报销金额
       p.si_update_date, --上传时间
       p.si_update_oper, --上传人
       p.si_update_flag, --0,未上传，1上传
       p.from_dept, --
       p.icu_dept_code, --患者转移到ICU疗区对应的科室代码
       p.balance_date, --结算日期
       p.charge_type_code, --费别编号
       p.outdiagnosis, --门诊诊断
       p.state, --在院状态（R-住院登记  I-病房接诊 B-出院登记 O-出院结算 P-预约出院,N-无费退院）
       p.charge, --
       p.personinfo, --
       p.cost_alarm, --
       p.cataloger, --编目人
       p.catalog_date, --编目日期
       p.total_payments, --实付费用
       
       p.total_costs, --总费用
       
       p.discharge_disposition, --出院方式
       
       p.doctor_in_charge, --经治医师
       
       p.attending_doctor, --主治医师
       
       p.director, --科主任
       
       p.follow_interval_units, --随诊期限单位
       
       p.follow_interval, --随诊期限
       
       p.follow_indicator, --随诊标志
       
       p.mr_quality, --病案质量
       
       p.mr_value, --病案价值
       
       p.adverse_reaction_drugs, --不良反应药物
       
       p.alergy_drugs, --过敏药物
       
       p.decubital_ulcer_times, --发生褥疮次数
       
       p.blood_tran_react_times, --输血反应次数
       
       p.blood_tran_vol, --输血总量
       
       p.blood_tran_times, --输血次数
       
       p.infusion_react_times, --输液反应次数
       
       p.blood_type_rh, --Rh血型
       
       p.blood_type, --血型
       
       p.autopsy_indicator, --尸检标识
       
       p.second_level_nurs_days, --二级护理天数
       
       p.first_level_nurs_days, --一级护理天数
       
       p.spec_level_nurs_days, --特别护理天数
       
       p.ccu_days, --CCU天数
       
       p.icu_days, --ICU天数
       
       p.critical_cond_days, --病危天数
       
       p.serious_cond_days, --病重天数
       
       p.esc_emer_times, --抢救成功次数
       
       p.emer_treat_times, --抢救次数
       
       p.admitted_by, --办理住院者
       
       p.consulting_doctor, --门诊医师
       
       p.pat_adm_condition, --入院病情
       
       p.consulting_date, --接诊日期
       
       p.admission_cause, --住院目的
       
       p.patient_class, --入院方式
       
       p.next_of_kin_phone, --联系人电话
       
       p.next_of_kin_zipcode, --联系人邮政编码
       
       p.next_of_kin_addr, --联系人地址
       
       p.relationship, --与联系人关系
       
       p.next_of_kin, --联系人姓名
       
       p.zip_code, --邮政编码
       
       p.mailing_address, --通信地址
       
       p.service_agency, --医疗体系病人标志
       
       p.insurance_no, --隶属大单位
       
       p.insurance_type, --工作单位
       
       p.working_status, --医疗保险号
       
       (p.charge_type || (SELECT r.clinicdept_name
                            FROM siinfo r
                           WHERE r.inpatient_id = p.patient_id
                             AND r.visit_id = p.visit_id
                             AND rownum = 1)) AS charge_type, --医保类别
       
       p.unit_in_contract, --在职标志
       
       p.service_system_indicator, --费别
       
       p.top_unit, --合同单位
       
       p.duty, --勤务
       
       p.armed_services, --军种
       
       p.identity, --身份
       
       p.marital_status, --婚姻状况
       
       p.occupation, --职业
       
       p.discharge_date_time, --出院日期及时间
       
       p.dept_discharge_from, --出院科室
       
       p.admission_date_time, --入院日期及时间
       
       p.dept_admission_to, --入院科室
       
       p.visit_id, --病人本次住院标识
       
       p.patient_id --病人标识
  FROM pats_in_hospital t
  LEFT JOIN pat_visit p
    ON t.patient_id = p.patient_id
   AND t.visit_id = p.visit_id
 WHERE t.dept_code = '{0}'
";
            sql = sql.SqlFormate(dept_code);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_VISIT>(ds).ToList();
            else
                return null;
        }

        /// <summary>
        /// 根据科室编号查询在院&出院患者信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public List<PAT_VISIT> GetListPatVisitByDept(string dept_code)
        {
            string sql = @"SELECT p.pub_alarm, --最低警戒线
       p.pub_cost, --报销金额
       p.si_update_date, --上传时间
       p.si_update_oper, --上传人
       p.si_update_flag, --0,未上传，1上传
       p.from_dept, --
       p.icu_dept_code, --患者转移到ICU疗区对应的科室代码
       p.balance_date, --结算日期
       p.charge_type_code, --费别编号
       p.outdiagnosis, --门诊诊断
       p.state, --在院状态（R-住院登记  I-病房接诊 B-出院登记 O-出院结算 P-预约出院,N-无费退院）
       p.charge, --
       p.personinfo, --
       p.cost_alarm, --
       p.cataloger, --编目人
       p.catalog_date, --编目日期
       p.total_payments, --实付费用
       
       p.total_costs, --总费用
       
       p.discharge_disposition, --出院方式
       
       p.doctor_in_charge, --经治医师
       
       p.attending_doctor, --主治医师
       
       p.director, --科主任
       
       p.follow_interval_units, --随诊期限单位
       
       p.follow_interval, --随诊期限
       
       p.follow_indicator, --随诊标志
       
       p.mr_quality, --病案质量
       
       p.mr_value, --病案价值
       
       p.adverse_reaction_drugs, --不良反应药物
       
       p.alergy_drugs, --过敏药物
       
       p.decubital_ulcer_times, --发生褥疮次数
       
       p.blood_tran_react_times, --输血反应次数
       
       p.blood_tran_vol, --输血总量
       
       p.blood_tran_times, --输血次数
       
       p.infusion_react_times, --输液反应次数
       
       p.blood_type_rh, --Rh血型
       
       p.blood_type, --血型
       
       p.autopsy_indicator, --尸检标识
       
       p.second_level_nurs_days, --二级护理天数
       
       p.first_level_nurs_days, --一级护理天数
       
       p.spec_level_nurs_days, --特别护理天数
       
       p.ccu_days, --CCU天数
       
       p.icu_days, --ICU天数
       
       p.critical_cond_days, --病危天数
       
       p.serious_cond_days, --病重天数
       
       p.esc_emer_times, --抢救成功次数
       
       p.emer_treat_times, --抢救次数
       
       p.admitted_by, --办理住院者
       
       p.consulting_doctor, --门诊医师
       
       p.pat_adm_condition, --入院病情
       
       p.consulting_date, --接诊日期
       
       p.admission_cause, --住院目的
       
       p.patient_class, --入院方式
       
       p.next_of_kin_phone, --联系人电话
       
       p.next_of_kin_zipcode, --联系人邮政编码
       
       p.next_of_kin_addr, --联系人地址
       
       p.relationship, --与联系人关系
       
       p.next_of_kin, --联系人姓名
       
       p.zip_code, --邮政编码
       
       p.mailing_address, --通信地址
       
       p.service_agency, --医疗体系病人标志
       
       p.insurance_no, --隶属大单位
       
       p.insurance_type, --工作单位
       
       p.working_status, --医疗保险号
       
       (p.charge_type || (SELECT r.clinicdept_name
                            FROM siinfo r
                           WHERE r.inpatient_id = p.patient_id
                             AND r.visit_id = p.visit_id
                             AND rownum = 1)) AS charge_type, --医保类别
       
       p.unit_in_contract, --在职标志
       
       p.service_system_indicator, --费别
       
       p.top_unit, --合同单位
       
       p.duty, --勤务
       
       p.armed_services, --军种
       
       p.identity, --身份
       
       p.marital_status, --婚姻状况
       
       p.occupation, --职业
       
       p.discharge_date_time, --出院日期及时间
       
       p.dept_discharge_from, --出院科室
       
       p.admission_date_time, --入院日期及时间
       
       p.dept_admission_to, --入院科室
       
       p.visit_id, --病人本次住院标识
       
       p.patient_id --病人标识
  from pat_visit p
 WHERE p.dept_admission_to = '{0}'
";
            sql = sql.SqlFormate(dept_code);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_VISIT>(ds).ToList();
            else
                return null;
        }


        /// <summary>
        /// 根据科室编号查询在院患者信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public List<PAT_MASTER_INDEX> GetListPatMasterIndexByCode(string dept_code)
        {
            string sql = @"select p.* from PATS_IN_HOSPITAL t 
left join pat_master_index p on t.patient_id=p.patient_id 
where t.dept_code='{0}'";
            sql = sql.SqlFormate(dept_code);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_MASTER_INDEX>(ds).ToList();
            else
                return null;
        }
        /// <summary>
        /// 根据患者住院visit_id和patient_id查询PAT_VISIT信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public PAT_VISIT GetPatVisitByCode(string patientId, int visitId)
        {
            string sql = @"select * from PAT_VISIT t where t.patient_id='{0}' and t.visit_id={1}";
            sql = sql.SqlFormate(patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_VISIT>(ds).First();
            else
                return null;
        }
        /// <summary>
        /// 更新PATS_IN_HOSPITAL表 admis，doctor_in_charge
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public int UpDatePatInHospital(int admis, string doctor, string patientId, int visitId, BaseEntityer db)
        {
            string sql = @"update PATS_IN_HOSPITAL p set p.admis={0},p.doctor_in_charge='{1}'
where p.patient_id='{2}' and p.visit_id={3}";
            sql = sql.SqlFormate(admis, doctor, patientId, visitId);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        ///  更新PATS_VISIT表 doctor_in_charge
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public int UpDatePatVisit(string doctor, string patientId, int visitId, BaseEntityer db)
        {
            string sql = @"update PAT_VISIT p set p.doctor_in_charge='{0}'
where p.patient_id='{1}' and p.visit_id={2}";
            sql = sql.SqlFormate(doctor, patientId, visitId);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        ///  更新doctor_orders表 order_status
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public int UpdateDoctorOrderSatus(string PATIENT_ID, int VISIT_ID, int ORDER_NO, int ORDER_SUB_NO, string ORDER_STATUS, BaseEntityer db)
        {
            string sql = @"update doctor_orders t 
set t.order_status='{4}'
where t.patient_id='{0}'
and t.visit_id={1}
and t.order_no={2}
and t.order_sub_no={3}";
            object[] obs = new object[]
          {
              PATIENT_ID,
              VISIT_ID,
              ORDER_NO,
              ORDER_SUB_NO,
              ORDER_STATUS
          };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        ///  提交doctor_orders
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public int TJDoctorOrder(string PATIENT_ID, int VISIT_ID, int ORDER_NO, int ORDER_SUB_NO, string enter_date_time, BaseEntityer db)
        {
            string sql = @"update doctor_orders t 
set t.order_status='1', t.enter_date_time=to_date('{4}','yyyy-MM-dd hh24:mi:ss')
where t.patient_id='{0}'
and t.visit_id={1}
and t.order_no={2}
and t.order_sub_no={3}";
            object[] obs = new object[]
          {
              PATIENT_ID,
              VISIT_ID,
              ORDER_NO,
              ORDER_SUB_NO,
              enter_date_time
          };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        ///  更新orders表 order_status
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public int UpdateOrdersSatus(string PATIENT_ID, int VISIT_ID, int? related_order_no, int? related_order_sub_no, string ORDER_STATUS, BaseEntityer db)
        {
            string sql = @"update orders t 
set t.order_status='{4}'
where t.patient_id='{0}'
and t.visit_id={1}
and t.order_no ={2}
and t.order_sub_no={3}";
            object[] obs = new object[]
          {
              PATIENT_ID,
              VISIT_ID,
              related_order_no,
              related_order_sub_no,
              ORDER_STATUS
          };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        ///  更新PATS_VISIT表 doctor_in_charge
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public int InsertDoctorOrders(HisCommon.DataEntity.DOCTOR_ORDERS o, BaseEntityer db)
        {
            //2013-6-27 by li 增加药品限制字段
            //2013-10-1 by li 增加执行科室
            //2013-10-22 by li 住院医生站增加新开医嘱--单医嘱项目金额
            //2013-11-29 by li 实际ICU开单科室记录
            // 2014-1-9 by li 住院医生站草药付数存储
            string sql = @"insert into DOCTOR_ORDERS
                          (PATIENT_ID,
                           VISIT_ID,
                           ORDER_NO,
                           ORDER_SUB_NO,
                           START_DATE_TIME,
                           START_STOP_INDICATOR,
                           REPEAT_INDICATOR,
                           ORDER_CLASS,
                           ORDER_TEXT,
                           ORDER_CODE,
                           DOSAGE,
                           DOSAGE_UNITS,
                           ADMINISTRATION,
                           DURATION,
                           DURATION_UNITS,
                           FREQUENCY,
                           FREQ_COUNTER,
                           FREQ_INTERVAL,
                           FREQ_INTERVAL_UNIT,
                           FREQ_DETAIL,
                           ORDERING_DEPT,
                           DOCTOR,
                           NURSE,
                           ORDER_STATUS,
                           ENTER_DATE_TIME,
                           PROCESSING_DATE_TIME,
                           DRUG_BILLING_ATTR,
                           BILLING_ATTR,
                           ORDER_PRINT_INDICATOR,
                           RELATED_ORDER_NO,
                           RELATED_ORDER_SUB_NO,
                           TEST_NO,
                           DRUG_SPEC,
                           TEST_ITEM_NO,
                           COMMON_FLAG,
                           SPECIAL_FLAG,
                           PERFORMED_BY,
                           ORDER_COSTS,
                           ICU_DEPT_CODE,
                           FS,
                           NWARN,
                           MEMO,
                          OPERATION_ORDER)
                        values
                          ('{0}',
                           {1},
                           {2},
                           {3},
                           to_date('{4}', 'yyyy-MM-dd hh24:mi:ss'),
                           {5},
                           {6},
                           '{7}',
                           '{8}',
                           '{9}',
                           {10},
                           '{11}',
                           '{12}',
                           {13},
                           '{14}',
                           '{15}',
                           {16},
                           {17},
                           '{18}',
                           '{19}',
                           '{20}',
                           '{21}',
                           '{22}',
                           '{23}',
                           to_date('{24}', 'yyyy-MM-dd  hh24:mi:ss'),
                           to_date('{25}', 'yyyy-MM-dd  hh24:mi:ss'),
                           {26},
                           {27},
                           {28},
                           {29},
                           {30},
                           '{31}',
                           '{32}',
                           {33},
                           '{34}',
                           {35},
                           '{36}',
                           {37},
                           '{38}',
                           {39},
                           {40},
                           '{41}',
                           '{42}')";
            object[] obs = new object[]
          {
                o.PATIENT_ID,
                o.VISIT_ID,
                o.ORDER_NO,
                o.ORDER_SUB_NO,
                o.START_DATE_TIME,
                o.START_STOP_INDICATOR,
                o.REPEAT_INDICATOR,
                o.ORDER_CLASS,
                o.ORDER_TEXT,
                o.ORDER_CODE,
                o.DOSAGE,
                o.DOSAGE_UNITS,
                o.ADMINISTRATION,
                o.DURATION,
                o.DURATION_UNITS,
                o.FREQUENCY,
                o.FREQ_COUNTER,
                o.FREQ_INTERVAL,
                o.FREQ_INTERVAL_UNIT,
                o.FREQ_DETAIL,
                o.ORDERING_DEPT,
                o.DOCTOR,
                o.NURSE,
                o.ORDER_STATUS,
                o.ENTER_DATE_TIME,
                o.PROCESSING_DATE_TIME,
                o.DRUG_BILLING_ATTR,
                o.BILLING_ATTR,
                o.ORDER_PRINT_INDICATOR,
                o.RELATED_ORDER_NO,
                o.RELATED_ORDER_SUB_NO,
                o.TEST_NO,
                o.DRUG_SPEC,
                o.TEST_ITEM_NO,
                o.COMMON_FLAG,
                o.SPECIAL_FLAG,
                o.PERFORMED_BY,
                o.ORDER_COSTS,
                o.ICU_DEPT_CODE,
                o.FS,
                o.NWARN,
                o.MEMO,
                o.OPERATION_ORDER
          };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        ///  删除DOCTOR_ORDERS表中一条医嘱
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public int DeleteDoctorOrders(string PATIENT_ID, int VISIT_ID, int ORDER_NO, int ORDER_SUB_NO, BaseEntityer db)
        {
            string sql = @"delete from doctor_orders t
 where t.patient_id = '{0}' 
 and t.visit_id = {1} 
 and t.order_no = {2}
 and t.order_sub_no = {3}";
            object[] obs = new object[]
          {
               PATIENT_ID,
               VISIT_ID,
               ORDER_NO,
               ORDER_SUB_NO
          };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 通过检查单号和检查序号，查询doctorOrder实体
        /// </summary>
        /// <returns></returns>
        public HisCommon.DataEntity.DOCTOR_ORDERS GetDoctorOrderByExamNO(string examNo, int itemNO)
        {
            string sql = @"select * from doctor_orders t 
where t.test_no='{0}'
and t.test_item_no={1}";
            sql = sql.SqlFormate(examNo, itemNO);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<DOCTOR_ORDERS>(ds).First();
            else
                return null;
        }

        /// <summary>
        /// 查询患者检查项目
        /// </summary>
        /// <returns></returns>
        public List<OUTP_SELECT_EXAM> QuereyInPatientExam(string patientId, int visitId)
        {
            //此功能不修改//2013-10-1 by li 检查申请数据护士站作废数据在医生站显示，可以删除
            //2013-12-3 by li 门诊医生站检查医嘱开单医生增加
            //2013-12-4 by li 门诊医生站检查医嘱开单科室增加
            string sql = @"select t.order_code as DESCRIPTION_CODE,
        t.order_text as DESCRIPTION,
        o.EXAM_CLASS,
        o.EXAM_SUB_CLASS,
        o.DESC_ITEM,
        o.INPUT_CODE,
        o.price,
        nvl(e.performed_by,m.performed_by) as performed_by,
       -- d.dept_name,
        t.test_no as EXAM_NO,
        t.test_item_no as EXAM_ITEM_NO,
        1 as amount,
         o.price as costs,
        null as visit_date,
        null as  visit_no,
        t.patient_id,
        t.visit_id,
           decode(t.order_status,'4','NONE','M')  as oper,
        --检查确认表中查不到，则为未收费，否则为已收费
        decode(m.exam_no,null,0,1) charge_indicator,
        t.order_no,
        t.order_sub_no,
        t.start_date_time as REQ_DATE_TIME,
        t.doctor,
        t.ORDERING_DEPT as ORDERED_BY 
  from doctor_orders t
  left join exam_appoints e on e.exam_no=t.test_no
  left join exam_master m on m.exam_no=t.test_no
  left join outp_exam_list o on o.DESCRIPTION_CODE=t.order_code 
        and o.DESCRIPTION=t.order_text 
        and o.EXAM_SUB_CLASS=e.exam_sub_class 
        and o.EXAM_CLASS=e.exam_class
  where  
       t.order_class='D'
      and t.test_no is not null
      and t.order_status!='4'
      and t.patient_id='{0}'
      and t.visit_id={1}";
            sql = sql.SqlFormate(patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<OUTP_SELECT_EXAM>(ds).ToList();
            else
                return null;
        }
        /// <summary>
        /// 查询检查项目是否已经做确认了
        /// </summary>
        /// <returns></returns>
        public bool QueryExamIsDone(string examNo)
        {
            string sql = @"select * from exam_master t where t.exam_no='{0}'";
            sql = sql.SqlFormate(examNo);
            if (BaseEntityer.Db.ExecuteScalar(sql) == null)
            {
                return false;
            }
            else return true;
        }
        /// <summary>
        /// 查询住院患者检验项目
        /// </summary>
        /// <returns></returns>
        public List<LAB_TEST_MASTER> QuereyInPatientLabs(string patientId, int visitId)
        {
            string sql = @"select distinct(t.test_no),
                           t.priority_indicator,
                           t.patient_id,
                           t.visit_id,
                           t.working_id,
                           t.execute_date,
                           t.name,
                           t.name_phonetic,
                           t.charge_type,
                           t.sex,
                           t.age,
                           t.test_cause,
                           t.relevant_clinic_diag,
                           t.specimen,
                           t.notes_for_spcm,
                           t.spcm_received_date_time,
                           t.spcm_sample_date_time,
                           t.requested_date_time,
                           t.ordering_dept,
                           t.ordering_provider,
                           t.performed_by,
                           t.result_status,
                           t.results_rpt_date_time,
                           t.transcriptionist,
                           t.verified_by,
                           t.costs,
                           t.charges,
                           t.billing_indicator,
                           t.print_indicator
                    from lab_test_master t
                    left join doctor_orders o on o.test_no=t.test_no
                    where o.patient_id='{0}' and o.visit_id={1} 
                    and o.test_no is not null and o.order_class='C'
                    order by t.test_no asc";
            sql = sql.SqlFormate(patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<LAB_TEST_MASTER>(ds).ToList();
            else
                return null;
        }
        /// <summary>
        /// 查询患者在DOCTOR_ORDERS中的检验项目
        /// </summary>
        /// <returns></returns>
        public List<DOCTOR_ORDERS> QueryLabInDoctorOrdersByTestNO(string patientId, int visitId, string testNo)
        {
            string sql = @"select t.*, 
       decode(l.result_status,null,0,1,0,1) as CHARGE_INDICATOR
  from doctor_orders t
  left join lab_test_master l
    on t.test_no = l.test_no
 where t.patient_id = '{0}'
   and t.visit_id = {1}
   and t.test_no = '{2}'";

            sql = string.Format(sql, patientId, visitId, testNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<DOCTOR_ORDERS>(ds).ToList();
        }
        /// <summary>
        /// 查询患者在DOCTOR_ORDERS中的所有项目
        /// </summary>
        /// <returns></returns>
        public List<DOCTOR_ORDERS> QueryPatientDoctorOrders(string patientId, int visitId)
        {
            string sql = @"select * from doctor_orders t where t.patient_id='{0}' and t.visit_id={1}";

            sql = string.Format(sql, patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<DOCTOR_ORDERS>(ds).ToList();
        }
        /// <summary>
        /// 查询患者在ORDERS中的所有项目
        /// </summary>
        /// <returns></returns>
        public List<ORDERS> QueryPatientOrdersByID(string patientId, int visitId)
        {
            string sql = @"select * from orders t where t.patient_id='{0}' and t.visit_id={1}";

            sql = string.Format(sql, patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<ORDERS>(ds).ToList();
        }
        /// <summary>
        /// 根据relatedOrderNo、relatedSubOrderNo查询ORDERS中项目
        /// </summary>
        /// <returns></returns>
        public ORDERS QueryPatientOrders(int OrderNo, int SubOrderNo)
        {
            string sql = @"select * from orders r where r.order_no={0} and r.order_sub_no={1}";

            sql = string.Format(sql, OrderNo, SubOrderNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                    return null;
            }
            return DataSetToEntity.DataSetToT<ORDERS>(ds).First();
        }
        /// <summary>
        /// 根据患者就诊序号和就诊时间查询患者所有药品和非药品医嘱
        /// </summary>
        /// <param name="visitNo"></param>
        /// <param name="visitDate"></param>
        /// <returns></returns>
        public IList<HisCommon.DataEntity.OPERATING_SELECT_ORDER> QueryPatientOrders(string patient_id, string dept_id)
        {
            string sql = @"select * from operating_patient_orders t where t.patient_id='{0}' and t.ORDERED_BY='{1}'";
            sql = string.Format(sql, patient_id, dept_id);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<OPERATING_SELECT_ORDER>(ds);
            return list;
        }

        /// <summary>
        /// 查询当前医嘱是否处于停止状态
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="relationOrderNO"></param>
        /// <param name="relationOrderSubNO"></param>
        /// <returns></returns>
        public bool QueryCurOrdersIsStopState(string patientID, string visitID, string relationOrderNO, string relationOrderSubNO)
        {
            string sql = @"
                            /*
                             * 查询当前医嘱是否处于停止状态
                            */
                            SELECT COUNT(*)
                              FROM doctor_orders r
                             WHERE r.patient_id = '{0}'
                               AND r.visit_id = '{1}'
                               AND r.repeat_indicator = '1'
                               AND r.start_stop_indicator = '1'
                               AND r.related_order_no = '{2}'
                               AND r.related_order_sub_no = '{3}'";
            sql = string.Format(sql, patientID, visitID, relationOrderNO, relationOrderSubNO);
            int rev = BaseEntityer.Db.ExecuteScalar<int>(sql);

            if (rev > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除药品医嘱----ORDERS_DRUG
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int DelDrugOrders(HisCommon.DataEntity.OPERATING_SELECT_ORDER o, BaseEntityer db)
        {
            string sql = @"delete from ORDERS_DRUG t where 
                            t.presc_no='{0}'
                            and t.item_no='{1}'";
            object[] param = new object[]
                           {
                               o.SERIAL_NO,
                               o.ITEM_NO,
                           };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除非药品医嘱----ORDERS_TREAT
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int DelUnDrugOrders(HisCommon.DataEntity.OPERATING_SELECT_ORDER o, BaseEntityer db)
        {
            string sql = @"delete from ORDERS_TREAT t where 
                            t.presc_no='{0}'
                            and t.item_no='{1}'";
            object[] param = new object[]
                           {
                               o.SERIAL_NO,
                               o.ITEM_NO,
                           };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 医嘱字典查询
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOrdersDict(List<string> depts)
        {
            //2013-6-27 by li 医保药品增加限定标志
            //2013-9-29 by li 增加显示药品计量
            //2013-12-31 by li 排除检查化验项目
            string sql = @"select i.class_name as 项目类别,
                               t.name as 项目名称,
                               t.DRUG_SPEC as 规格,
                               t.price as 价格,
                               t.kc as 库存,                               
  t.yb as 省医保,
                               t.city as 市医保,
                               t.Xinnonghe as 新农合 ,
                               t.tielu as 铁路,
                               t.dianli as 电力,
                               t.caizhenglaogan as 财政老干,
                              
                               t.deptname as 执行科室,
                               t.DOSE_PRE_UNIT as 计量,
                               t.MIN_UNITS as 剂量单位,
                               t.FIRM_ID as 厂家,
                               t.AMOUNT_PER_PACKAGE as 包装数量,
                               t.package_spec as 包装规格,
                               t.PACKAGE_UNITS as 包装单位,
                               t.drug_common_limit_flag as 药品属性,
                               t.drug_special_limit_flag as 药品特殊限制标志,
                               t.ismazui as 毒麻,
                               t.EXPIRE_DATE as 有效期, 
                             
                               t.class as 项目类别编码,
                               t.deptcode as 科室编号,
                               t.code as 项目编码,
                               t.inputCode as 输入码  
                         from outp_order_list t
                         left join clinic_item_class_dict i on t.class=i.class_code 
                        where (t.deptcode in ({0})) or class not in ('A', 'B', 'C', 'D')";
            var str = "";
            foreach (var d in depts)
            {
                var temp = "'" + d + "'";
                str = str + temp + ",";
            }
            str = str.TrimEnd(',');
            sql = string.Format(sql, str);
            var dt = BaseEntityer.Db.GetDataTable(sql);
            return dt;

        }
        /// <summary>
        /// 医嘱字典查询
        /// 2013-12-19 by yuxi
        /// </summary>
        /// <param name="drug_code">药品代码</param>
        /// <param name="drug_class">药品类型</param>
        /// <param name="drug_spec">药品规格</param>
        /// <param name="drug_performed_by">药房代码</param>
        /// <returns></returns>
        public DataTable QueryOrdersDict_item(string drug_code, string drug_class, string drug_spec, string drug_performed_by)
        {
            string sql = @"select i.type_name as 项目类别,
       t.name as 项目名称,
       t.price as 价格,
       t.kc as 库存,
       t.deptname as 执行科室,
       t.deptcode as 科室编号,
       decode(t.yb,1,'甲',2,'乙',3,'丙') as 医保等级,
       t.DOSE_PRE_UNIT as 计量,
       t.FIRM_ID as 厂家,
       t.DRUG_SPEC as 规格,
       t.MIN_UNITS as 剂量单位,
       t.AMOUNT_PER_PACKAGE as 包装数量,
       t.drug_common_limit_flag as 药品普通限制标识,
       t.drug_special_limit_flag as 药品特殊限制标志,
       t.package_spec as 包装规格,
       t.PACKAGE_UNITS as 包装单位,
       t.yb as 医保等级编号,
       t.class as 项目类别编码,
       t.code as 项目编码,
       t.ismazui as 毒麻,
       t.EXPIRE_DATE as 有效期, 
       t.inputCode as 输入码 
 from outp_order_list t
 left join item_type i on t.class=i.type_code  
 where t.code = '{0}' --CODE 
                  and t.class = '{1}' --CLASS 
                  and t.DRUG_SPEC = '{2}' --DRUG_SPEC 
                  and t.deptcode = '{3}' --PERFORMED_BY"; ;
            sql = string.Format(sql, drug_code, drug_class, drug_spec, drug_performed_by);
            var dt = BaseEntityer.Db.GetDataTable(sql);
            return dt;

        }
        /// <summary>
        /// 根据药品的代码、类型、规格、药房代码获取药品信息
        /// </summary>
        /// <param name="drug_code">药品代码</param>
        /// <param name="drug_class">药品类型</param>
        /// <param name="drug_spec">药品规格</param>
        /// <param name="drug_performed_by">药房代码</param>
        /// <returns></returns>
        public DataTable QueryDrugDict(string drug_code, string drug_class, string drug_spec, string drug_performed_by)
        {
            //2013-11-5 by li 计量查询连表需要code与spec连接
            //2013-12-27 by li 包装数量连表需要增加厂家连接保证数据唯一性
            string sql = @"select *
  from (select t.input_code     as inputcode,
               t.item_code      as code,
               t.item_class     as class,
               d.drug_indicator,
               t.item_name      as name,
               p.RETAIL_PRICE   as price,
               --修改为不考虑批号了
               sum(s.QUANTITY) as kc,
               h.fee_itemgrade as yb,
               p.FIRM_ID,
               '' as BATCHNO,
               --s.BATCH_NO as BATCHNO,
               decode((select drug_code 
                        from drug_dict m 
                       where m.TOXI_PROPERTY IN ('毒性药品', '麻醉药品') 
                         and m.drug_code = t.item_code),
                      null,
                      '否',
                      '是') as ismazui,
               s.drug_spec,
               s.package_spec,
               s.package_units,
               d.dose_units as MIN_UNITS,
               s.storage as deptcode,
               s.sub_storage as deptname,
               (select aa.dose_per_unit 
                  from drug_dict aa 
                 where aa.drug_code = t.item_code and aa.drug_spec=s.drug_spec) as DOSE_PRE_UNIT, --计量 
               (select pp.AMOUNT_PER_PACKAGE 
                  from outp_order_list pp 
                 where pp.code = t.item_code and pp.drug_spec=s.drug_spec and pp.FIRM_ID=p.firm_id and pp.class=t.item_class and pp.deptcode = '{3}' and rownum=1) as AMOUNT_PER_PACKAGE, --包装数量 
               null as EXPIRE_DATE,
               h.drug_common_limit_flag,
               h.drug_special_limit_flag,
               s.package_spec || p.FIRM_ID as spec 
          from DRUG_STOCK s 
         right join PRICE_ITEM_NAME_DICT t 
            on t.item_code = s.drug_code 
         inner join DRUG_PRICE_LIST p 
            on (s.DRUG_CODE = p.DRUG_CODE and s.FIRM_ID = p.FIRM_ID and 
               s.PACKAGE_SPEC = p.DRUG_SPEC and s.PACKAGE_UNITS = p.UNITS) 
          left join his_compare h 
            on his_code = t.item_code 
              --and his_class = t.item_class 
           and h.charge_type_code = '2' 
          left join DRUG_DICT d 
            on d.drug_code = p.DRUG_CODE 
           and d.drug_spec = s.DRUG_SPEC 
         where t.item_class in ('A', 'B') 
           and s.SUPPLY_INDICATOR = '1' 
           and (p.start_date < = sysdate) 
           AND (sysdate < p.stop_date OR p.stop_date is null) 
         group by t.input_code,
                  t.item_code,
                  t.item_class,
                  d.drug_indicator,
                  t.item_name,
                  p.RETAIL_PRICE,
                  s.STORAGE,
                  h.fee_itemgrade,
                  p.FIRM_ID,
                  s.DRUG_SPEC,
                  s.package_spec,
                  s.PACKAGE_UNITS,
                  s.sub_storage,
                  d.dose_units,
                  h.drug_common_limit_flag,
                  h.drug_special_limit_flag 
         order by class, name, price) bb 
                  where bb.code = '{0}' --ORDER_CODE 
                  and bb.class = '{1}' --ORDER_CLASS 
                  and bb.spec = '{2}' --DRUG_SPEC 
                  and bb.deptcode = '{3}' --PERFORMED_BY";
            sql = string.Format(sql, drug_code, drug_class, drug_spec, drug_performed_by);
            var dt = BaseEntityer.Db.GetDataTable(sql);
            return dt;
        }
        /// <summary>
        /// 根据住院标识号和本次住院标志获取住院患者信息
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public PATS_IN_HOSPITAL GetPatsInHospitalByPatientidAndVisitid(string patient_id, string visit_id)
        {
            string sql = @"select * from PATS_IN_HOSPITAL p where p.patient_id='{0}' and p.visit_id={1}";
            sql = string.Format(sql, patient_id, visit_id);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                    return null;
            }
            return DataSetToEntity.DataSetToT<PATS_IN_HOSPITAL>(ds).First();
        }

        #region 手术室

        /// <summary>
        /// 新增手术室药品医嘱项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orderDrug"></param>
        /// <returns></returns>
        public int InsertOrdersDrug(BaseEntityer db, ORDERS_DRUG orderDrug)
        {
            //2013-11-29 by li 实际ICU开单科室记录
            string sql = @"INSERT INTO ORDERS_DRUG
                        (
                               ORDERS_DRUG.PRESC_NO,
                               ORDERS_DRUG.PATIENT_ID,
                               ORDERS_DRUG.VISIT_ID,
                               ORDERS_DRUG.ORDER_ID,
                               ORDERS_DRUG.SUB_ORDER_ID,
                               ORDERS_DRUG.VISIT_DATE,
                               ORDERS_DRUG.VISIT_NO,
                               ORDERS_DRUG.SERIAL_NO,
                               ORDERS_DRUG.ITEM_NO,
                               ORDERS_DRUG.ITEM_CLASS,
                               ORDERS_DRUG.DRUG_CODE,
                               ORDERS_DRUG.DRUG_NAME,
                               ORDERS_DRUG.DRUG_SPEC,
                               ORDERS_DRUG.FIRM_ID,
                               ORDERS_DRUG.UNITS,
                               ORDERS_DRUG.AMOUNT,
                               ORDERS_DRUG.DOSAGE,
                               ORDERS_DRUG.DOSAGE_UNITS,
                               ORDERS_DRUG.ADMINISTRATION,
                               ORDERS_DRUG.FREQUENCY,
                               ORDERS_DRUG.PROVIDED_INDICATOR,
                               ORDERS_DRUG.COSTS,
                               ORDERS_DRUG.CHARGES,
                               ORDERS_DRUG.CHARGE_INDICATOR,
                               ORDERS_DRUG.DISPENSARY,
                               ORDERS_DRUG.PRICE,
                               ORDERS_DRUG.ZB,
                               ORDERS_DRUG.FS,
                               ORDERS_DRUG.BATCHNO,
                               ORDERS_DRUG.MIN_SPEC,
                               ORDERS_DRUG.OPERATOR_DATE,
                               ORDERS_DRUG.DOCTOR,
                               ORDERS_DRUG.ORDERING_DEPT,
                               ORDERS_DRUG.COMMON_FLAG,
                               ORDERS_DRUG.SPECIAL_FLAG,
                               ORDERS_DRUG.ICU_DEPT_CODE,
                               ORDERS_DRUG.MEMO,
                               ORDERS_DRUG.TS,
                               ORDERS_DRUG.YB
                        )
                        VALUES
                        (
                               {0},'{1}',{2},{3},{4},to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'),
                                {6},'{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}',{15},
                                '{16}','{17}','{18}','{19}',{20},{21},{22},{23},'{24}',{25},
                                {26},{27},'{28}','{29}',to_date('{30}', 'yyyy-mm-dd hh24:mi:ss'),
                                '{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}'
                        )";
            object[] param = new object[] { orderDrug.PRESC_NO, orderDrug.PATIENT_ID, orderDrug.VISIT_ID,
                orderDrug.ORDER_ID, orderDrug.SUB_ORDER_ID, orderDrug.VISIT_DATE, orderDrug.VISIT_NO,
                orderDrug.SERIAL_NO, orderDrug.ITEM_NO, orderDrug.ITEM_CLASS, orderDrug.DRUG_CODE,
                orderDrug.DRUG_NAME, orderDrug.DRUG_SPEC, orderDrug.FIRM_ID, orderDrug.UNITS, orderDrug.AMOUNT,
                orderDrug.DOSAGE, orderDrug.DOSAGE_UNITS, orderDrug.ADMINISTRATION, orderDrug.FREQUENCY,
                orderDrug.PROVIDED_INDICATOR, orderDrug.COSTS, orderDrug.CHARGES, orderDrug.CHARGE_INDICATOR,
                orderDrug.DISPENSARY, orderDrug.PRICE, orderDrug.ZB, orderDrug.FS, orderDrug.BATCHNO,
                orderDrug.MIN_SPEC, orderDrug.OPERATOR_DATE, orderDrug.DOCTOR, orderDrug.ORDERING_DEPT,
                orderDrug.COMMON_FLAG, orderDrug.SPECIAL_FLAG, orderDrug.ICU_DEPT_CODE ,orderDrug.MEMO,
                orderDrug.TS,orderDrug.YB};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 新增手术室非药品医嘱项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orderUnDrug"></param>
        /// <returns></returns>
        public int InsertOrdersTreat(BaseEntityer db, ORDERS_TREAT orderUnDrug)
        {
            //2013-11-29 by li 实际ICU开单科室记录
            string sql = @"INSERT INTO ORDERS_TREAT
                        (
                               ORDERS_TREAT.VISIT_ID,
                               ORDERS_TREAT.ORDER_ID,
                               ORDERS_TREAT.PATIENT_ID,
                               ORDERS_TREAT.VISIT_DATE,
                               ORDERS_TREAT.VISIT_NO,
                               ORDERS_TREAT.ITEM_NO,
                               ORDERS_TREAT.ITEM_CLASS,
                               ORDERS_TREAT.ITEM_CODE,
                               ORDERS_TREAT.ITEM_NAME,
                               ORDERS_TREAT.AMOUNT,
                               ORDERS_TREAT.FREQUENCY,
                               ORDERS_TREAT.PERFORMED_BY,
                               ORDERS_TREAT.COSTS,
                               ORDERS_TREAT.CHARGES,
                               ORDERS_TREAT.CHARGE_INDICATOR,
                               ORDERS_TREAT.DOCTOR,
                               ORDERS_TREAT.SUB_ORDER_ID,
                               ORDERS_TREAT.PRESC_NO,
                               ORDERS_TREAT.MATERIAL_LIMITUSE_FLAG,
                               ORDERS_TREAT.MATERIAL_SITECODE,
                               ORDERS_TREAT.TEST_NO,
                               ORDERS_TREAT.ORDER_STATUS,
                               ORDERS_TREAT.TEST_ITEM_NO,
                               ORDERS_TREAT.ORDERED_BY,
                               ORDERS_TREAT.ICU_DEPT_CODE,
                               ORDERS_TREAT.MEMO,
                               ORDERS_TREAT.TS,
                               ORDERS_TREAT.YB
                        )
                        VALUES
                        (
                               {0},{1},'{2}',to_date('{3}', 'yyyy-mm-dd hh24:mi:ss'),{4},{5},
                                '{6}','{7}','{8}',{9},'{10}','{11}',{12},{13},{14},'{15}',
                                {16},{17},'{18}','{19}','{20}','{21}',{22},'{23}','{24}','{25}','{26}','{27}'
                        )";
            object[] param = new object[] { orderUnDrug.VISIT_ID, orderUnDrug.ORDER_ID, orderUnDrug.PATIENT_ID,
                orderUnDrug.VISIT_DATE, orderUnDrug.VISIT_NO, orderUnDrug.ITEM_NO, orderUnDrug.ITEM_CLASS,
                orderUnDrug.ITEM_CODE, orderUnDrug.ITEM_NAME, orderUnDrug.AMOUNT, orderUnDrug.FREQUENCY,
                orderUnDrug.PERFORMED_BY, orderUnDrug.COSTS, orderUnDrug.CHARGES, orderUnDrug.CHARGE_INDICATOR,
                orderUnDrug.DOCTOR, orderUnDrug.SUB_ORDER_ID, orderUnDrug.PRESC_NO,
                orderUnDrug.MATERIAL_LIMITUSE_FLAG, orderUnDrug.MATERIAL_SITECODE, orderUnDrug.TEST_NO,
                orderUnDrug.ORDER_STATUS, orderUnDrug.TEST_ITEM_NO, orderUnDrug.ORDERED_BY,
                orderUnDrug.ICU_DEPT_CODE,orderUnDrug.MEMO,orderUnDrug.TS,orderUnDrug.YB };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2013-7-16 by li 手术室药品医嘱获取此次病人入院后最大项目号
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public int GetDrugMaxOrderID(string patient_id, string visit_id)
        {
            string sql = @"select max(t.ORDER_ID) from orders_drug t where t.patient_id='{0}' and t.visit_id={1}";
            sql = sql.SqlFormate(patient_id, visit_id);
            var orderNo = BaseEntityer.Db.ExecuteScalar(sql);
            if (orderNo != DBNull.Value)
                return int.Parse(orderNo.ToString());
            else return 0;
        }

        /// <summary>
        /// 2013-7-16 by li 手术室非药品医嘱获取此次病人入院后最大项目号
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public int GetUnDrugMaxOrderID(string patient_id, string visit_id)
        {
            string sql = @"select max(t.ORDER_ID) from orders_treat t where t.patient_id='{0}' and t.visit_id={1}";
            sql = sql.SqlFormate(patient_id, visit_id);
            var orderNo = BaseEntityer.Db.ExecuteScalar(sql);
            if (orderNo != DBNull.Value)
                return int.Parse(orderNo.ToString());
            else return 0;
        }

        /// <summary>
        /// 插入住院病人费用信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="inpBillDetail"></param>
        /// <returns></returns>
        public int InsertInpBillDetail(BaseEntityer db, INP_BILL_DETAIL detail)
        {
            string sql = @" INSERT INTO INP_BILL_DETAIL
                      (PATIENT_ID ,
                        VISIT_ID ,
                        ITEM_NO ,
                        ITEM_CLASS ,
                        ITEM_NAME ,
                        ITEM_CODE ,
                        ITEM_SPEC ,
                        AMOUNT ,
                        UNITS ,
                        ORDERED_BY ,
                        PERFORMED_BY ,
                        COSTS ,
                        CHARGES ,
                        BILLING_DATE_TIME ,
                        OPERATOR_NO ,
                        RCPT_NO ,
                        UP_FLAG ,
                        UP_TIME_DATE ,
                        UP_OPERATOR_NO ,
                        FORMULARYNO ,
                        DOCTOR ,
                        CHECKFLAG,
                       SUBJ_CODE,
                       CLASS_ON_MR,
                       CLASS_ON_RECKONING,
                       CLASS_ON_INP_RCPT,
                       ORDERS_NO,
                      RETURN_NUM,
                      RETURN_FLAG,
                      COMMON_FLAG,
                      SPECIAL_FLAG,
                      PRICE,
                      MATERIAL_LIMITUSE_FLAG,
                      MATERIAL_SITECODE)
                    VALUES
                      ('{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}'
                       ,'{10}','{11}','{12}',to_date('{13}', 'yyyy-MM-dd  hh24:mi:ss'),'{14}',
                      '{15}','{16}',to_date('{17}', 'yyyy-MM-dd  hh24:mi:ss'),'{18}','{19}','{20}','{21}',
                    '{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}')";
            object[] obs = new object[]
               {
                detail.PATIENT_ID ,
                detail.VISIT_ID ,
                detail.ITEM_NO ,
                detail.ITEM_CLASS ,
                detail.ITEM_NAME ,
                detail.ITEM_CODE ,
                detail.ITEM_SPEC ,
                detail.AMOUNT ,
                detail.UNITS ,
                detail.ORDERED_BY ,
                detail.PERFORMED_BY ,
                detail.COSTS ,
                detail.CHARGES ,
                detail.BILLING_DATE_TIME ,
                detail.OPERATOR_NO ,
                detail.RCPT_NO ,
                detail.UP_FLAG ,
                detail.UP_TIME_DATE ,
                detail.UP_OPERATOR_NO ,
                detail.FORMULARYNO ,
                detail.DOCTOR ,
                detail.CHECKFLAG,
                detail.SUBJ_CODE,
                detail.CLASS_ON_MR,
                detail.CLASS_ON_RECKONING,
                detail.CLASS_ON_INP_RCPT,
                detail.ORDERS_NO,
                detail.RETURN_NUM,
                detail.RETURN_FLAG,
                detail.Common_flag,
                detail.Special_flag,
                detail.PRICE,
                detail.MATERIAL_LIMITUSE_FLAG,
                detail.MATERIAL_SITECODE
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据病人ID获取在院病人主索引信息
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
                           PAT_MASTER_INDEX.BALANCE 
                           from PATS_IN_HOSPITAL
                          left join PAT_MASTER_INDEX
                            on PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID
                         where PATS_IN_HOSPITAL.PATIENT_ID = '{0}'";
            sql = string.Format(sql, patientID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_MASTER_INDEX>(ds).First();
            else
                return null;
        }

       

        /// <summary>
        /// 手术室医嘱列表用，检索当前药品和非药品项目，除检查检验外
        /// 新建了视图operating_room_order_list
        /// </summary>
        /// <param name="currentDrugDept">当前默认取药科室代码</param>
        /// <returns></returns>
        public DataTable GetDrugAndUndrugItem(List<string> depts, bool isShowExam, bool isShowLab)
        {
            // string sql = @"select * from operating_room_order_list t  
            //where ((t.deptcode in ({0})) or class not in ('A', 'B')) and class!='D' and class!='C'";
            string sql = string.Empty;
            if (isShowExam && isShowLab)
            {
                sql = @"select * from operating_room_order_list t  
where 1=1  ";
            }
            else if (!isShowExam && isShowLab)
                sql = @"select * from operating_room_order_list t  
where  class!='D' ";
            else if (isShowExam && !isShowLab)
                sql = @"select * from operating_room_order_list t  
where  class!='C' ";
            else if (!isShowExam && !isShowLab)
                sql = @"select * from operating_room_order_list t  
where  class!='D' and class!='C'";
            var str = "";
            foreach (var d in depts)
            {
                var temp = "'" + d + "'";
                str = str + temp + ",";
            }

            str = str.TrimEnd(',');

            if (string.IsNullOrEmpty(str))
            {
                sql += " and  ( class not in ('A', 'B'))";
            }
            else
            {
                sql += " and  ((t.deptcode in ({0})) or class not in ('A', 'B'))";
                sql = string.Format(sql, str);
            }


            sql = string.Format(sql, str);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 2013-8-13 by li 住院患者列表,供手术室查询用
        /// </summary>
        /// <returns></returns>
        public DataTable GetInpList()
        {
            string sql = @"select m.name, m.sex, m.charge_type, m.patient_id, m.name_phonetic
                          from pats_in_hospital p
                          left join pat_master_index m
                            on p.patient_id = m.patient_id";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 2013-8-14 by li 手术室费用明细查询
        /// </summary>
        /// <returns></returns>
        public DataTable GetInpFeeDetail(string patient_id, string visit_id, string ordered_by, string item_class, string begin_time, string end_time)
        {
            string sql = @"SELECT INP_BILL_DETAIL.PATIENT_ID,
                           --INP_BILL_DETAIL.VISIT_ID,
                           --INP_BILL_DETAIL.ITEM_NO,
                           INP_BILL_DETAIL.ITEM_CLASS,
                           INP_BILL_DETAIL.ITEM_NAME,
                           --INP_BILL_DETAIL.ITEM_CODE,
                           INP_BILL_DETAIL.ITEM_SPEC,
                           INP_BILL_DETAIL.AMOUNT,
                           INP_BILL_DETAIL.UNITS,
                           INP_BILL_DETAIL.COSTS,
                           INP_BILL_DETAIL.CHARGES,
                           INP_BILL_DETAIL.PERFORMED_BY,
                           INP_BILL_DETAIL.ORDERED_BY,
                           INP_BILL_DETAIL.BILLING_DATE_TIME,
                           INP_BILL_DETAIL.OPERATOR_NO 
                           --,INP_BILL_DETAIL.RCPT_NO 
                      FROM INP_BILL_DETAIL
                     WHERE (PATIENT_ID = '{0}')
                       AND (VISIT_ID = '{1}')
                       AND (ORDERED_BY = '{2}')
                       AND (ITEM_CLASS = '{3}' OR '{3}' IS NULL)
                       AND (BILLING_DATE_TIME >= to_date('{4}', 'yyyy-MM-dd  hh24:mi:ss') AND
                           BILLING_DATE_TIME <= to_date('{5}', 'yyyy-MM-dd  hh24:mi:ss'))
                     ORDER BY ITEM_NO";
            object[] obs = new object[]
               {
                patient_id,visit_id,ordered_by,item_class,begin_time,end_time
               };
            sql = sql.SqlFormate(obs);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 2013-10-2 by li 查询住院患者手术室开单的检验项目
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="ordering_dept"></param>
        /// <returns></returns>
        public List<LAB_TEST_MASTER> QueryInPOperatingRoomLabs(string patientId, int visitId, string ordering_dept)
        {
            string sql = @"select distinct(t.test_no),
                               t.priority_indicator,
                               t.patient_id,
                               t.visit_id,
                               t.working_id,
                               t.execute_date,
                               t.name,
                               t.name_phonetic,
                               t.charge_type,
                               t.sex,
                               t.age,
                               t.test_cause,
                               t.relevant_clinic_diag,
                               t.specimen,
                               t.notes_for_spcm,
                               t.spcm_received_date_time,
                               t.spcm_sample_date_time,
                               t.requested_date_time,
                               t.ordering_dept,
                               t.ordering_provider,
                               t.performed_by,
                               t.result_status,
                               t.results_rpt_date_time,
                               t.transcriptionist,
                               t.verified_by,
                               t.costs,
                               t.charges,
                               t.billing_indicator,
                               t.print_indicator
                        from lab_test_master t
                        right join ORDERS_TREAT o on o.test_no=t.test_no
                        where o.patient_id = '{0}' and o.visit_id = {1} 
                        and o.test_no is not null and o.item_class='C' 
                        and t.ordering_dept = '{2}' 
                        order by t.test_no asc";
            sql = sql.SqlFormate(patientId, visitId, ordering_dept);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<LAB_TEST_MASTER>(ds).ToList();
            else
                return new List<LAB_TEST_MASTER>();
        }

        /// <summary>
        /// 2013-10-2 by li 查询住院患者手术室开单的检查项目
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="ordering_dept"></param>
        /// <returns></returns>
        public List<OUTP_SELECT_EXAM> QueryInPOperatingRoomExam(string patientId, int visitId, string ordering_dept)
        {
            //2013-12-3 by li 门诊医生站检查医嘱开单医生增加
            //2013-12-4 by li 门诊医生站检查医嘱开单科室增加
            string sql = @"select t.ITEM_CODE as DESCRIPTION_CODE,
                               t.ITEM_NAME as DESCRIPTION,
                               o.EXAM_CLASS,
                               o.EXAM_SUB_CLASS,
                               o.DESC_ITEM,
                               o.INPUT_CODE,
                               o.price,
                               nvl(e.performed_by, m.performed_by) as performed_by,
                               -- d.dept_name,
                               t.test_no as EXAM_NO,
                               t.test_item_no as EXAM_ITEM_NO,
                               1 as amount,
                               o.price as costs,
                               null as visit_date,
                               null as visit_no,
                               t.patient_id,
                               t.visit_id,
                               decode(t.order_status, '4', 'NONE', 'M') as oper,
                               --检查确认表中查不到，则为未收费，否则为已收费
                               decode(m.exam_no, null, 0, 1) charge_indicator,
                               t.ORDER_ID as order_no,
                               t.SUB_ORDER_ID as order_sub_no,
                               t.VISIT_DATE as REQ_DATE_TIME,
                               t.doctor,
                               t.ORDERED_BY
                          from ORDERS_TREAT t
                          left join exam_appoints e
                            on e.exam_no = t.test_no
                          left join exam_master m
                            on m.exam_no = t.test_no
                          left join outp_exam_list o
                            on o.DESCRIPTION_CODE = t.ITEM_CODE
                           and o.DESCRIPTION = t.ITEM_NAME
                              --and o.EXAM_SUB_CLASS=e.exam_sub_class 
                           and o.EXAM_CLASS = e.exam_class
                         where t.ITEM_CLASS = 'D'
                           and t.test_no is not null
                           and t.order_status!='4'
                           and t.patient_id = '{0}'
                           and t.visit_id = {1}
                           AND t.ORDERED_BY = '{2}'";
            sql = sql.SqlFormate(patientId, visitId, ordering_dept);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<OUTP_SELECT_EXAM>(ds).ToList();
            else
                return new List<OUTP_SELECT_EXAM>();
        }

        /// <summary>
        /// 2013-10-2 by li 更新ORDERS_TREAT表 order_status
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <param name="TEST_NO"></param>
        /// <param name="ORDER_STATUS"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateInPOperatingRoomOrderSatus(string PATIENT_ID, int VISIT_ID, string TEST_NO, string ORDER_STATUS, BaseEntityer db)
        {
            string sql = @"update ORDERS_TREAT t 
                            set t.order_status='{3}'
                            where t.patient_id='{0}'
                            and t.visit_id={1}
                            and t.TEST_NO='{2}'";
            object[] obs = new object[]
              {
                  PATIENT_ID,
                  VISIT_ID,
                  TEST_NO,
                  ORDER_STATUS
              };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2013-10-2 by li 查询患者在ORDERS_TREAT中的检验项目
        /// </summary>
        /// <returns></returns>
        public List<ORDERS_TREAT> QueryLabInOperatingRoomByTestNO(string patientId, int visitId, string testNo)
        {
            string sql = @"select t.VISIT_ID,
                               t.ORDER_ID,
                               t.PATIENT_ID,
                               t.VISIT_DATE,
                               t.VISIT_NO,
                               t.SERIAL_NO,
                               t.ITEM_NO,
                               t.ITEM_CLASS,
                               t.ITEM_CODE,
                               t.ITEM_NAME,
                               t.ITEM_SPEC,
                               t.UNITS,
                               t.NURSE,
                               t.PERFORM_TIME,
                               t.AMOUNT,
                               t.FREQUENCY,
                               t.PERFORMED_BY,
                               t.COSTS,
                               t.CHARGES,
                               t.DOCTOR,
                               t.SUB_ORDER_ID,
                               t.PRESC_NO,
                               t.MATERIAL_LIMITUSE_FLAG,
                               t.MATERIAL_SITECODE,
                               t.TEST_NO,
                               t.ORDER_STATUS,
                               t.TEST_ITEM_NO,
                               t.ORDERED_BY,
                           decode(l.result_status,null,0,1,0,1) as CHARGE_INDICATOR
                      from ORDERS_TREAT t
                      left join lab_test_master l
                        on t.test_no = l.test_no
                     where t.patient_id = '{0}'
                       and t.visit_id = {1}
                       and t.test_no = '{2}'";
            sql = string.Format(sql, patientId, visitId, testNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<ORDERS_TREAT>(ds).ToList();
        }

        //-----------ICU手术室增加功能------------

        /// <summary>
        /// 2013-11-27 by li 更新患者ICU科室接入
        /// </summary>
        /// <param name="icuDeptCode">ICU科室代码</param>
        /// <param name="patientId">患者ID</param>
        /// <param name="visitId">病人本次住院标识</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdatePatVisitAdmis(string icuDeptCode, string patientId, int visitId, BaseEntityer db)
        {
            string sql = @"UPDATE PAT_VISIT P
                               SET P.ICU_DEPT_CODE = '{0}'
                             WHERE P.PATIENT_ID = '{1}'
                               AND P.VISIT_ID = '{2}'";
            sql = sql.SqlFormate(icuDeptCode, patientId, visitId);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2013-11-28 by li ICU疗区患者接诊日志写入
        /// </summary>
        /// <param name="icu_admis_log">接诊日志</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertICUAdmisLog(ICU_ADMIS_LOG icu_admis_log, BaseEntityer db)
        {
            string sql = @"insert into ICU_ADMIS_LOG
                              (SERIAL_NO,
                               OPERATOR_NO,
                               OPER_DATE,
                               ORDERED_BY,
                               ADMIS_TYPE,
                               PATIENT_ID,
                               VISIT_ID)
                            values
                              ((select nvl(max(SERIAL_NO), 1000000000) + 1 from ICU_ADMIS_LOG),
                               '{0}',
                               to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'),
                               '{2}',
                               '{3}',
                               '{4}',
                               {5})";
            sql = sql.SqlFormate(icu_admis_log.OPERATOR_NO, icu_admis_log.OPER_DATE,
                icu_admis_log.ORDERED_BY, icu_admis_log.ADMIS_TYPE, icu_admis_log.PATIENT_ID,
                icu_admis_log.VISIT_ID);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2013-12-25 by li 查询住院患者ICU开单的检查项目
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="ordering_dept"></param>
        /// <returns></returns>
        public List<OUTP_SELECT_EXAM> QueryInPICUExam(string patientId, int visitId, string ordering_dept)
        {
            string sql = @"select t.ITEM_CODE as DESCRIPTION_CODE,
                               t.ITEM_NAME as DESCRIPTION,
                               o.EXAM_CLASS,
                               o.EXAM_SUB_CLASS,
                               o.DESC_ITEM,
                               o.INPUT_CODE,
                               o.price,
                               nvl(e.performed_by, m.performed_by) as performed_by,
                               -- d.dept_name,
                               t.test_no as EXAM_NO,
                               t.test_item_no as EXAM_ITEM_NO,
                               1 as amount,
                               o.price as costs,
                               null as visit_date,
                               null as visit_no,
                               t.patient_id,
                               t.visit_id,
                               decode(t.order_status, '4', 'NONE', 'M') as oper,
                               --检查确认表中查不到，则为未收费，否则为已收费
                               decode(m.exam_no, null, 0, 1) charge_indicator,
                               t.ORDER_ID as order_no,
                               t.SUB_ORDER_ID as order_sub_no,
                               t.VISIT_DATE as REQ_DATE_TIME,
                               t.doctor,
                               t.ORDERED_BY
                          from ORDERS_TREAT t
                          left join exam_appoints e
                            on e.exam_no = t.test_no
                          left join exam_master m
                            on m.exam_no = t.test_no
                          left join outp_exam_list o
                            on o.DESCRIPTION_CODE = t.ITEM_CODE
                           and o.DESCRIPTION = t.ITEM_NAME
                              --and o.EXAM_SUB_CLASS=e.exam_sub_class 
                           --and o.EXAM_CLASS = e.exam_class
                         where t.ITEM_CLASS = 'D'
                           and t.test_no is not null
                           and t.order_status!='4'
                           and t.patient_id = '{0}'
                           and t.visit_id = {1}
                           AND t.ICU_DEPT_CODE = '{2}'";
            sql = sql.SqlFormate(patientId, visitId, ordering_dept);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<OUTP_SELECT_EXAM>(ds).ToList();
            else
                return new List<OUTP_SELECT_EXAM>();
        }

        /// <summary>
        /// 2013-12-25 by li 查询住院患者ICU开单的检验项目
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="ordering_dept"></param>
        /// <returns></returns>
        public List<LAB_TEST_MASTER> QueryInPICULabs(string patientId, int visitId, string ordering_dept)
        {
            string sql = @"select distinct(t.test_no),
                               t.priority_indicator,
                               t.patient_id,
                               t.visit_id,
                               t.working_id,
                               t.execute_date,
                               t.name,
                               t.name_phonetic,
                               t.charge_type,
                               t.sex,
                               t.age,
                               t.test_cause,
                               t.relevant_clinic_diag,
                               t.specimen,
                               t.notes_for_spcm,
                               t.spcm_received_date_time,
                               t.spcm_sample_date_time,
                               t.requested_date_time,
                               t.ordering_dept,
                               t.ordering_provider,
                               t.performed_by,
                               t.result_status,
                               t.results_rpt_date_time,
                               t.transcriptionist,
                               t.verified_by,
                               t.costs,
                               t.charges,
                               t.billing_indicator,
                               t.print_indicator
                        from lab_test_master t
                        right join ORDERS_TREAT o on o.test_no=t.test_no
                        where o.patient_id = '{0}' and o.visit_id = {1} 
                        and o.test_no is not null and o.item_class='C' 
                        and t.ICU_DEPT_CODE = '{2}' 
                        order by t.test_no asc";
            sql = sql.SqlFormate(patientId, visitId, ordering_dept);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<LAB_TEST_MASTER>(ds).ToList();
            else
                return new List<LAB_TEST_MASTER>();
        }

        /// <summary>
        /// 2014-1-2 by li ICU手术室费用明细查询（查询住院明细中icu_dept_code）
        /// </summary>
        /// <returns></returns>
        public DataTable GetICUInpFeeDetail(string patient_id, string visit_id, string ordered_by, string item_class, string begin_time, string end_time)
        {
            string sql = @" SELECT INP_BILL_DETAIL.PATIENT_ID,
                           --INP_BILL_DETAIL.VISIT_ID,
                           --INP_BILL_DETAIL.ITEM_NO,
                           INP_BILL_DETAIL.ITEM_CLASS,
                           INP_BILL_DETAIL.ITEM_NAME,
                           --INP_BILL_DETAIL.ITEM_CODE,
                           INP_BILL_DETAIL.ITEM_SPEC,
                           INP_BILL_DETAIL.AMOUNT,
                           INP_BILL_DETAIL.UNITS,
                           INP_BILL_DETAIL.COSTS,
                           INP_BILL_DETAIL.CHARGES,
                           INP_BILL_DETAIL.PERFORMED_BY,
                           INP_BILL_DETAIL.icu_dept_code,
                           INP_BILL_DETAIL.BILLING_DATE_TIME,
                           INP_BILL_DETAIL.OPERATOR_NO
                    --,INP_BILL_DETAIL.RCPT_NO 
                      FROM INP_BILL_DETAIL
                     WHERE (PATIENT_ID = '{0}')
                       AND (VISIT_ID = '{1}')
                       AND (icu_dept_code = '{2}' or
                           icu_dept_code = (select m.name
                                               from com_dictionary m
                                              where m.type = 'RelationShip'
                                                and m.code = '{2}'))
                       AND (ITEM_CLASS = '{3}' OR '{3}' IS NULL)
                       AND (BILLING_DATE_TIME >= to_date('{4}', 'yyyy-MM-dd  hh24:mi:ss') AND
                           BILLING_DATE_TIME <= to_date('{5}', 'yyyy-MM-dd  hh24:mi:ss'))
                     ORDER BY ITEM_NO ";
            object[] obs = new object[]
               {
                patient_id,visit_id,ordered_by,item_class,begin_time,end_time
               };
            sql = sql.SqlFormate(obs);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 2014-1-9 by li ICU读取发药药品
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public List<DRUG_DISPENSE_REC_NAME> GetICUInpDurgList(string patient_id, int visit_id, string DeptCode)
        {
            string sql = @" SELECT PATIENT_ID,
                    VISIT_ID,
                    DRUG_CODE ITEM_CODE,
                        (SELECT   ITEM_NAME 
                        FROM PRICE_ITEM_NAME_DICT  WHERE  ITEM_CLASS in ('A', 'B') and std_indicator = 1 and PRICE_ITEM_NAME_DICT.ITEM_CODE= DRUG_CODE and rownum=1) as ITEM_NAME,
                    package_spec || firm_id ITEM_SPEC,
                    to_char(a.ORDER_NO) || to_char(a.ORDER_SUB_NO) ||
                    to_char(a.dispensing_date_time, 'yyyymmddHH24:mi:ss') ||
                    batch_no item_no,
                    DISPENSE_AMOUNT -
                    (select nvl(sum(dispense_amount), 0)
                        from DRUG_DISPENSE_REGRET_REQ
                        where patient_id = a.Patient_Id
                        and visit_id = a.Visit_Id
                        and item_No =
                            to_char(a.ORDER_NO) || to_char(a.ORDER_SUB_NO) ||
                            to_char(a.dispensing_date_time, 'yyyymmddHH24:mi:ss') ||
                            batch_no
                        and dispensary = a.DISPENSARY) amount,
                    drug_units,
                    PACKAGE_UNITS UNITS,
                    package_spec drug_spec,
                    firm_id,
                    COSTS - (select nvl(sum(costs), 0)
                                from DRUG_DISPENSE_REGRET_REQ
                                where patient_id = a.Patient_Id
                                and visit_id = a.Visit_Id
                                and item_No =
                                    to_char(a.ORDER_NO) || to_char(a.ORDER_SUB_NO) ||
                                    to_char(a.dispensing_date_time,
                                            'yyyymmddHH24:mi:ss') || batch_no
                                and dispensary = a.DISPENSARY) costs,
                    CHARGES - (select nvl(sum(charges), 0)
                                from DRUG_DISPENSE_REGRET_REQ
                                where patient_id = Patient_Id
                                    and visit_id = Visit_Id
                                    and item_No = Item_No
                                    and dispensary = a.DISPENSARY) charges,
                    DISPENSARY PERFORMED_BY,
                    (select d.dept_name from dept_dict d where d.dept_code=DISPENSARY) as PERFORMED_BY_NAME,
                    dispensing_date_time,
                    batch_no,
                    ORDER_NO,
                    ORDER_SUB_NO,
                round((a.costs/a.DISPENSE_AMOUNT),6) as price
                FROM DRUG_DISPENSE_REC a
                where (patient_id = '{0}')
                AND (visit_id = '{1}')
                and (DISPENSE_AMOUNT -
                    (select nvl(sum(dispense_amount), 0)
                        from DRUG_DISPENSE_REGRET_REQ
                        where patient_id = a.Patient_Id
                        and visit_id = a.Visit_Id
                        and item_No =
                            to_char(a.ORDER_NO) || to_char(a.ORDER_SUB_NO) ||
                            to_char(a.dispensing_date_time, 'yyyymmddHH24:mi:ss') ||
                            batch_no
                        and dispensary = a.DISPENSARY)) > 0
                AND ICU_DEPT_CODE = '{2}'";
            object[] obs = new object[]
               {
                patient_id,
                visit_id,
                DeptCode};
            sql = sql.SqlFormate(obs);
            return DataSetToEntity.DataSetToT<DRUG_DISPENSE_REC_NAME>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        #endregion

        #region 住院医生站会诊


        /// <summary>
        /// 2014-1-6 by li 根据会诊医生编号查询在院患者信息
        /// </summary>
        /// <param name="doctor">会诊医生编号</param>
        /// <returns></returns>
        public List<PATS_IN_HOSPITAL> GetListConsultationPatientInfo(string doctor)
        {
            string sql = @"SELECT *
                              FROM pats_in_hospital t
                             WHERE t.consultation_doctor = '{0}'
                                OR instr(t.consultation_doctor || ',', '{0},') > 0
                            ";
            sql = sql.SqlFormate(doctor);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
            {
                var tempLst = DataSetToEntity.DataSetToT<PATS_IN_HOSPITAL>(ds).ToList();

                List<PATS_IN_HOSPITAL> consultationLst = new List<PATS_IN_HOSPITAL>();
                tempLst.ForEach(t =>
                {
                    PATS_IN_HOSPITAL temp = new PATS_IN_HOSPITAL();
                    temp = t;
                    temp.CONSULTATION_DOCTOR = doctor;
                    consultationLst.Add(temp);
                });
                return consultationLst;
            }
            else
                return null;
        }

        /// <summary>
        /// 2014-1-6 by li 根据在院患者ID查询在院患者信息
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public PAT_VISIT GetPatVisitByPatientInfo(string patient_id, int visit_id)
        {
            string sql = @"select p.*
                              from pat_visit p
                             where p.patient_id = '{0}'
                               and p.visit_id = {1}";
            sql = sql.SqlFormate(patient_id, visit_id);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_VISIT>(ds).ToList()[0];
            else
                return null;
        }

        /// <summary>
        /// 2014-1-6 by li 根据在院患者ID查询在院患者信息
        /// </summary>
        /// <param name="patient_id"></param>
        /// <returns></returns>
        public PAT_MASTER_INDEX GetPatMasterIndexByPatientInfo(string patient_id)
        {
            string sql = @"select p.* from pat_master_index p where p.patient_id = '{0}'";
            sql = sql.SqlFormate(patient_id);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_MASTER_INDEX>(ds).ToList()[0];
            else
                return null;
        }

        /// <summary>
        /// 2014-1-3 by li 获取会诊日志最大ID
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public DbDataReader GetMaxLogData(BaseEntityer db)
        {
            string sql = @"select NVL(Max(c.consultation_id),0) as consultation_id 
                            from consultation_log c";
            return db.ExecuteReader(sql);
        }

        /// <summary>
        /// 2014-1-3 by li 保存会诊日志
        /// </summary>
        /// <param name="db"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public int InsertConsultationLog(BaseEntityer db, CONSULTATION_LOG log)
        {
            string sql = @"INSERT INTO CONSULTATION_LOG
                              (CONSULTATION_ID,
                               PATIENT_ID,
                               VISIT_ID,
                               APPLICATION_DATE_TIME,
                               APPLICATION_DOCTOR,
                               APPLICATION_DEPT_CODE,
                               CONSULTATION_DOCTOR,
                               CONSULTATION_DEPT_CODE,
                               CONSULTATION_DATE_TIME,
                               CONSULTATION_END_DATE_TIME)
                            VALUES
                              ({0},
                               '{1}',
                               {2},
                               to_date('{3}', 'yyyy-MM-dd hh24:mi:ss'),
                               '{4}',
                               '{5}',
                               '{6}',
                               '{7}',
                               to_date('{8}', 'yyyy-MM-dd hh24:mi:ss'),
                               to_date('{9}', 'yyyy-MM-dd hh24:mi:ss'))";
            object[] param = new object[] { log.CONSULTATION_ID, log.PATIENT_ID, log.VISIT_ID,
                log.APPLICATION_DATE_TIME, log.APPLICATION_DOCTOR, log.APPLICATION_DEPT_CODE,
                log.CONSULTATION_DOCTOR, log.CONSULTATION_DEPT_CODE, log.CONSULTATION_DATE_TIME,
                log.CONSULTATION_END_DATE_TIME };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2014-1-4 by li 更新在院患者会诊信息
        /// </summary>
        /// <param name="patient_id">会诊病人标识号</param>
        /// <param name="visit_id">会诊病人本次住院标识</param>
        /// <param name="doctor">会诊医生</param>
        /// <param name="consultation_id">会诊日志ID</param>
        /// <param name="consultation_admis">会诊接诊状态</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateConsultationSatus(string patient_id, int visit_id, string doctor, int consultation_id, int consultation_admis, BaseEntityer db)
        {
            string sql = @"UPDATE PATS_IN_HOSPITAL P
                           SET P.CONSULTATION_DOCTOR = '{0}',
                               P.CONSULTATION_ID     = {1},
                               P.CONSULTATION_ADMIS  = {2}
                         WHERE P.PATIENT_ID = '{3}'
                           AND P.VISIT_ID = {4}";
            object[] obs = new object[]
                {
                    doctor,
                    consultation_id,
                    consultation_admis,
                    patient_id,
                    visit_id
                };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2014-1-4 by li 更新患者会诊状态为接诊状态
        /// </summary>
        /// <param name="patient_id">会诊病人标识号</param>
        /// <param name="visit_id">会诊病人本次住院标识</param>
        /// <param name="doctor">会诊医生</param>
        /// <param name="consultation_id">会诊日志ID</param>
        /// <param name="consultation_admis">会诊接诊状态</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateConsultationAdmissionState(string patient_id, int visit_id, BaseEntityer db)
        {
            string sql = @"UPDATE PATS_IN_HOSPITAL P
                           SET  
                               P.CONSULTATION_ADMIS  = 1
                         WHERE P.PATIENT_ID = '{0}'
                           AND P.VISIT_ID = {1}";
            object[] obs = new object[] 
                {
                    patient_id,
                    visit_id
                };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 2014-1-4 by li 更新在院患者会诊信息
        /// </summary>
        /// <param name="patient_id">会诊病人标识号</param>
        /// <param name="visit_id">会诊病人本次住院标识</param>
        /// <param name="doctor">会诊医生</param>
        /// <param name="consultation_id">会诊日志ID</param>
        /// <param name="consultation_admis">会诊接诊状态</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateConsultationInfo(string patient_id, int visit_id, string doctor, int consultation_id, int consultation_admis, BaseEntityer db)
        {
            string sql = @"UPDATE PATS_IN_HOSPITAL P
                           SET P.CONSULTATION_DOCTOR =P.CONSULTATION_DOCTOR || '{0},',
                               P.CONSULTATION_ID     = {1},
                               P.CONSULTATION_ADMIS  = {2}
                         WHERE P.PATIENT_ID = '{3}'
                           AND P.VISIT_ID = {4}";
            object[] obs = new object[] 
                {
                    doctor,
                    consultation_id,
                    consultation_admis,
                    patient_id,
                    visit_id
                };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2014-1-6 by li 会诊接诊和会诊取消接诊时更新日志时间
        /// </summary>
        /// <param name="consultation_id"></param>
        /// <param name="consultation_datetime"></param>
        /// <param name="consultation_admis"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateConsultationLog(int consultation_id, string consulation_doctor, string consulation_dept, string consultation_datetime, int consultation_admis, BaseEntityer db)
        {
            string sql = string.Empty;
            //更新接诊时间
            if (consultation_admis > 0)
                sql = @"UPDATE consultation_log c
   SET c.consultation_date_time = to_date('{1}', 'yyyy-MM-dd hh24:mi:ss'),
       c.consultation_doctor    = '{2}',
       c.consultation_dept_code = '{3}'
 WHERE c.consultation_id = {0}
   AND (c.consultation_doctor = '{2}' OR c.consultation_doctor IS NULL)
   AND (c.consultation_dept_code = '{3}' OR
       c.consultation_dept_code IS NULL)
";
            //更新接诊完成时间
            else
                sql = @"UPDATE consultation_log c
   SET c.consultation_end_date_time = to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
 WHERE c.consultation_id = {0}
   AND c.consultation_doctor = '{2}'
   AND c.consultation_dept_code = '{3}'";
            object[] obs = new object[] 
                {
                    consultation_id,
                    consultation_datetime,
                    consulation_doctor,
                    consulation_dept
                };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2014-1-6 by li 会诊接诊和会诊取消接诊时更新日志时间
        /// </summary>
        /// <param name="consultation_id"></param>
        /// <param name="consultation_datetime"></param>
        /// <param name="consultation_admis"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public string GetConsultationInfo(string patientID, int visitID, BaseEntityer db)
        {
            string sql = @"SELECT t.consultation_doctor
  FROM pats_in_hospital t
 WHERE t.patient_id = '{0}'
   AND t.visit_id = {1}
";

            sql = string.Format(sql, patientID, visitID.ToString());

            return db.ExecuteScalar<string>(sql);
        }

        #endregion

        /// <summary>
        /// 2014-3-5 BY LI 查询手术间最大手术台次
        /// </summary>
        /// <param name="scheduleDateTime"></param>
        /// <param name="roomNo"></param>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public int GetOperationScheduleSequence(string scheduleDateTime, string roomNo, string DeptCode)
        {
            string sql = @"SELECT MAX(O.Sequence)
                              FROM OPERATION_SCHEDULE O
                             WHERE O.Scheduled_Date_Time = to_date('{0}','yyyy-mm-dd')
                               AND O.OPERATING_ROOM_NO = '{1}'
                               AND O.OPERATING_ROOM = '{2}'";
            sql = sql.SqlFormate(scheduleDateTime, roomNo, DeptCode);
            var schedule = BaseEntityer.Db.ExecuteScalar(sql);
            if (schedule != DBNull.Value)
                return int.Parse(schedule.ToString());
            else return 0;
        }

        /// <summary>
        /// 2014-3-3 BY LI 查询住院患者手术申请单项目
        /// </summary>
        /// <param name="patientId">患者标识</param>
        /// <param name="visitId">本次住院标识</param>
        /// <returns></returns>
        public List<OPERATION_SCHEDULE> QueryInPatientOperationSchedules(string patientId, int visitId)
        {
            string sql = @"SELECT *
                          FROM OPERATION_SCHEDULE O
                         WHERE O.PATIENT_ID = '{0}'
                           AND O.VISIT_ID = {1}";
            sql = sql.SqlFormate(patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<OPERATION_SCHEDULE>(ds).ToList();
            else
                return new List<OPERATION_SCHEDULE>();
        }

        /// <summary>
        /// 2014-3-3 BY LI 查询住院患者手术申请单明细项目
        /// </summary>
        /// <param name="patientId">患者标识</param>
        /// <param name="visitId">本次住院标识</param>
        /// <param name="scheduleID">手术安排标识</param>
        /// <returns></returns>
        public List<SCHEDULED_OPERATION_NAME> QueryInPatientOperationItems(string patientId, int visitId, int scheduleID)
        {
            string sql = @"SELECT *
                          FROM SCHEDULED_OPERATION_NAME S
                         WHERE S.PATIENT_ID = '{0}'
                           AND S.VISIT_ID = {1}
                           AND S.SCHEDULE_ID = {2}";
            sql = sql.SqlFormate(patientId, visitId, scheduleID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<SCHEDULED_OPERATION_NAME>(ds).ToList();
            else
                return new List<SCHEDULED_OPERATION_NAME>();
        }

        /// <summary>
        /// 2014-3-3 BY LI 查询最大手术申请号
        /// </summary>
        /// <param name="patientID">患者标识</param>
        /// <param name="visitID">本次住院标识</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int GetOperationMaxScheduleID(string patientID, int visitID, BaseEntityer db)
        {
            string sql = @"SELECT MAX(O.SCHEDULE_ID)
                          FROM OPERATION_SCHEDULE O
                         WHERE O.PATIENT_ID = '{0}'
                           AND O.VISIT_ID = {1}";
            sql = sql.SqlFormate(patientID, visitID);
            var schedule = db.ExecuteScalar(sql);
            if (schedule != DBNull.Value)
                return int.Parse(schedule.ToString());
            else return 0;
        }

        /// <summary>
        /// 2014-3-3 BY LI 删除手术申请单项目
        /// </summary>
        /// <param name="patientID">患者标识</param>
        /// <param name="visitID">本次住院标识</param>
        /// <param name="scheduleID">手术安排标识</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DelOperationSchedule(string patientID, int visitID, int scheduleID, BaseEntityer db)
        {
            string sql = @"DELETE FROM OPERATION_SCHEDULE O
                         WHERE O.PATIENT_ID = '{0}'
                           AND O.VISIT_ID = {1}
                           AND O.SCHEDULE_ID = {2}";
            object[] param = new object[] { patientID, visitID, scheduleID };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2014-3-3 BY LI 删除手术申请单明细项目
        /// </summary>
        /// <param name="patientID">患者标识</param>
        /// <param name="visitID">本次住院标识</param>
        /// <param name="scheduleID">手术安排标识</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DelOperationItems(string patientID, int visitID, int scheduleID, BaseEntityer db)
        {
            string sql = @"DELETE FROM SCHEDULED_OPERATION_NAME S
                         WHERE S.PATIENT_ID = '{0}'
                           AND S.VISIT_ID = {1}
                           AND S.SCHEDULE_ID = {2}";
            object[] param = new object[]
                           {
                               patientID,
                               visitID,
                               scheduleID
                           };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2014-3-3 BY LI 保存手术申请单项目主表
        /// </summary>
        /// <param name="o">手术申请单对象</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int SaveOperationSchedule(OPERATION_SCHEDULE o, BaseEntityer db)
        {
            string sql = @"INSERT INTO OPERATION_SCHEDULE
                              (PATIENT_ID,
                               VISIT_ID,
                               SCHEDULE_ID,
                               DEPT_STAYED,
                               BED_NO,
                               SCHEDULED_DATE_TIME,
                               OPERATING_ROOM,
                               OPERATING_ROOM_NO,
                               SEQUENCE,
                               DIAG_BEFORE_OPERATION,
                               PATIENT_CONDITION,
                               OPERATION_SCALE,
                               ISOLATION_INDICATOR,
                               OPERATING_DEPT,
                               SURGEON,
                               FIRST_ASSISTANT,
                               SECOND_ASSISTANT,
                               THIRD_ASSISTANT,
                               FOURTH_ASSISTANT,
                               ANESTHESIA_METHOD,
                               ANESTHESIA_DOCTOR,
                               ANESTHESIA_ASSISTANT,
                               BLOOD_TRAN_DOCTOR,
                               FIRST_OPERATION_NURSE,
                               SECOND_OPERATION_NURSE,
                               FIRST_SUPPLY_NURSE,
                               SECOND_SUPPLY_NURSE,
                               NOTES_ON_OPERATION,
                               ENTERED_BY,
                               REQ_DATE_TIME,
                               ACK_INDICATOR)
                            VALUES
                              ('{0}',
                               {1},
                               {2},
                               '{3}',
                               {4},
                               to_date('{5}', 'yyyy-MM-dd hh24:mi:ss'),
                               '{6}',
                               '{7}',
                               {8},
                               '{9}',
                               '{10}',
                               '{11}',
                               {12},
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
                               to_date('{29}', 'yyyy-MM-dd hh24:mi:ss'),
                               {30})";
            object[] param = new object[]
                {
                    o.PATIENT_ID,
                    o.VISIT_ID,
                    o.SCHEDULE_ID,
                    o.DEPT_STAYED,
                    o.BED_NO,
                    o.SCHEDULED_DATE_TIME,
                    o.OPERATING_ROOM,
                    o.OPERATING_ROOM_NO,
                    o.SEQUENCE,
                    o.DIAG_BEFORE_OPERATION,
                    o.PATIENT_CONDITION,
                    o.OPERATION_SCALE,
                    o.ISOLATION_INDICATOR,
                    o.OPERATING_DEPT,
                    o.SURGEON,
                    o.FIRST_ASSISTANT,
                    o.SECOND_ASSISTANT,
                    o.THIRD_ASSISTANT,
                    o.FOURTH_ASSISTANT,
                    o.ANESTHESIA_METHOD,
                    o.ANESTHESIA_DOCTOR,
                    o.ANESTHESIA_ASSISTANT,
                    o.BLOOD_TRAN_DOCTOR,
                    o.FIRST_OPERATION_NURSE,
                    o.SECOND_OPERATION_NURSE,
                    o.FIRST_SUPPLY_NURSE,
                    o.SECOND_SUPPLY_NURSE,
                    o.NOTES_ON_OPERATION,
                    o.ENTERED_BY,
                    o.REQ_DATE_TIME,
                    o.ACK_INDICATOR
                };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2014-3-3 BY LI 保存手术申请单明细
        /// </summary>
        /// <param name="o">手术申请单明细对象</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int SaveOperationItems(SCHEDULED_OPERATION_NAME o, BaseEntityer db)
        {
            string sql = @"INSERT INTO SCHEDULED_OPERATION_NAME
                              (PATIENT_ID,
                               VISIT_ID,
                               SCHEDULE_ID,
                               OPERATION_NO,
                               OPERATION,
                               OPERATION_SCALE)
                            VALUES
                              ('{0}', {1}, {2}, {3}, '{4}', '{5}')";
            object[] param = new object[]
                    {
                        o.PATIENT_ID,
                        o.VISIT_ID,
                        o.SCHEDULE_ID,
                        o.OPERATION_NO,
                        o.OPERATION,
                        o.OPERATION_SCALE
                    };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 插入麻醉信息
        /// </summary>
        /// <param name="anaesthesia"></param>
        /// <returns></returns>
        public int InsertAnaesthesiaDict(HisCommon.DataEntity.ANAESTHESIA_DICT anaesthesia, BaseEntityer db)
        {
            string sql = @" INSERT INTO ANAESTHESIA_DICT (SERIAL_NO, ANAESTHESIA_CODE, ANAESTHESIA_NAME, INPUT_CODE)
                    VALUES
                      ('{0}','{1}', '{2}', '{3}')";
            object[] obs = new object[]
               {
                anaesthesia.SERIAL_NO,
                anaesthesia.ANAESTHESIA_CODE,
                anaesthesia.ANAESTHESIA_NAME,
                anaesthesia.INPUT_CODE };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除麻醉信息
        /// </summary>
        /// <param name="anaesthesia"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteAnaesthesiaDict(HisCommon.DataEntity.ANAESTHESIA_DICT anaesthesia, BaseEntityer db)
        {
            string sql = @" delete  from  ANAESTHESIA_DICT where SERIAL_NO={0} ";
            object[] obs = new object[]
               {
                anaesthesia.SERIAL_NO
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 得到麻醉列表
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ANAESTHESIA_DICT> GetAnaesthesiaDictListInfo()
        {
            string sql = @"select * from ANAESTHESIA_DICT order by SERIAL_NO";
            //sql = string.Format(sql, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ANAESTHESIA_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }

        /// <summary>
        /// 插入手术间信息
        /// </summary>
        /// <param name="OperatingRoom"></param>
        /// <returns></returns>
        public int InsertOperatingRoom(HisCommon.DataEntity.OPERATING_ROOM OperatingRoom, BaseEntityer db)
        {
            string sql = @" insert into OPERATING_ROOM (ROOM_NO, DEPT_CODE, LOCATION, STATUS)
                    VALUES
                      ('{0}','{1}', '{2}', '{3}')";
            object[] obs = new object[]
               {
                OperatingRoom.ROOM_NO,
                OperatingRoom.DEPT_CODE,
                OperatingRoom.LOCATION,
                OperatingRoom.STATUS };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除手术间信息
        /// </summary>
        /// <param name="OperatingRoom"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteOperatingRoom(HisCommon.DataEntity.OPERATING_ROOM OperatingRoom, BaseEntityer db)
        {
            string sql = @" delete  from  OPERATING_ROOM where ROOM_NO={0} ";
            object[] obs = new object[]
               {
                OperatingRoom.Old_room_no
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 得到手术间列表
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OPERATING_ROOM> GetOperatingRoomListInfo()
        {
            string sql = @"select O.*, D.DEPT_NAME
                          from OPERATING_ROOM O
                          LEFT JOIN DEPT_DICT D
                            ON O.DEPT_CODE = D.DEPT_CODE
                         order by ROOM_NO";
            //sql = string.Format(sql, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OPERATING_ROOM>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }

        /// <summary>
        /// 插入手术名称信息
        /// </summary>
        /// <param name="OperationDict"></param>
        /// <returns></returns>
        public int InsertOperationDict(HisCommon.DataEntity.OPERATION_DICT OperationDict, BaseEntityer db)
        {
            string sql = @" insert into OPERATION_DICT (OPERATION_CODE, OPERATION_NAME, OPERATION_SCALE, STD_INDICATOR, APPROVED_INDICATOR, INPUT_CODE)
                    VALUES
                      ('{0}','{1}', '{2}', '{3}','{4}','{5}')";
            object[] obs = new object[]
               {
                OperationDict.OPERATION_CODE,
                OperationDict.OPERATION_NAME,
                OperationDict.OPERATION_SCALE,
                OperationDict.STD_INDICATOR,
                OperationDict.APPROVED_INDICATOR,
               OperationDict.INPUT_CODE};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除手术名称信息
        /// </summary>
        /// <param name="OperationDict"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteOperationDict(HisCommon.DataEntity.OPERATION_DICT OperationDict, BaseEntityer db)
        {
            string sql = @" delete  from  OPERATION_DICT where OPERATION_CODE='{0}' ";
            object[] obs = new object[]
               {
                OperationDict.Old_operation_code
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 得到手术名称列表
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OPERATION_DICT> GetOperationDictListInfo()
        {
            string sql = @"select * from OPERATION_DICT";
            //sql = string.Format(sql, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OPERATION_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 读取手术安排申请
        /// </summary>
        /// <param name="Indicator">0，1，2</param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OPERATION_SCHEDULE> GetOperationScheduleListInfo(string Indicator)
        {
            string sql = @"select o.*,p.name,p.inp_no,p.date_of_birth from  operation_schedule o,pat_master_index p where o.patient_id=p.patient_id
                         and o.ACK_INDICATOR='{0}'";
            sql = string.Format(sql, Indicator);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OPERATION_SCHEDULE>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 插入术后安排
        /// </summary>
        /// <param name="OperatingRoom"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertOperatingMaster(HisCommon.DataEntity.OPERATION_MASTER OperatingRoom, BaseEntityer db)
        {
            string sql = @"insert into OPERATION_MASTER (PATIENT_ID, VISIT_ID, OPER_ID, DEPT_STAYED, OPERATING_ROOM, OPERATING_ROOM_NO, DIAG_BEFORE_OPERATION, PATIENT_CONDITION, OPERATION_SCALE, DIAG_AFTER_OPERATION, EMERGENCY_INDICATOR, ISOLATION_INDICATOR, OPERATION_CLASS, OPERATING_DEPT, SURGEON, FIRST_ASSISTANT, SECOND_ASSISTANT, THIRD_ASSISTANT, FOURTH_ASSISTANT, ANESTHESIA_METHOD, ANESTHESIA_DOCTOR, ANESTHESIA_ASSISTANT, BLOOD_TRAN_DOCTOR, FIRST_OPERATION_NURSE, SECOND_OPERATION_NURSE, FIRST_SUPPLY_NURSE, SECOND_SUPPLY_NURSE, NURSE_SHIFT_INDICATOR, START_DATE_TIME, END_DATE_TIME, SATISFACTION_DEGREE, SMOOTH_INDICATOR, IN_FLUIDS_AMOUNT, OUT_FLUIDS_AMOUNT, BLOOD_LOSSED, BLOOD_TRANSFERED, ENTERED_BY)
            values ('{0}', {1}, {2}, '{3}', '{4}', {5}, '{6}', {7}, '{8}', '{9}', {10}, {11}, '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', {27}, to_date('{28}', 'yyyy-MM-dd  hh24:mi:ss'), to_date('{29}', 'yyyy-MM-dd  hh24:mi:ss'), {30}, {31}, {32}, {33}, {34}, {35}, '{36}')";
            object[] obs = new object[]
               {
                OperatingRoom.PATIENT_ID,
                OperatingRoom.VISIT_ID,
                OperatingRoom.OPER_ID,
                OperatingRoom.DEPT_STAYED,
                OperatingRoom.OPERATING_ROOM,
                OperatingRoom.OPERATING_ROOM_NO ,
                OperatingRoom.DIAG_BEFORE_OPERATION,
                OperatingRoom.PATIENT_CONDITION,
                OperatingRoom.OPERATION_SCALE,
                OperatingRoom.DIAG_AFTER_OPERATION,
                OperatingRoom.EMERGENCY_INDICATOR,
                OperatingRoom.ISOLATION_INDICATOR,
                OperatingRoom.OPERATION_CLASS,
                OperatingRoom.OPERATING_DEPT,
                OperatingRoom.SURGEON,
                OperatingRoom.FIRST_ASSISTANT,
                OperatingRoom.SECOND_ASSISTANT,
                OperatingRoom.THIRD_ASSISTANT,
                OperatingRoom.FOURTH_ASSISTANT,
                OperatingRoom.ANESTHESIA_METHOD,
                OperatingRoom.ANESTHESIA_DOCTOR,
                OperatingRoom.ANESTHESIA_ASSISTANT,
                OperatingRoom.BLOOD_TRAN_DOCTOR,
                OperatingRoom.FIRST_OPERATION_NURSE,
                OperatingRoom.SECOND_OPERATION_NURSE,
                OperatingRoom.FIRST_SUPPLY_NURSE,
                OperatingRoom.SECOND_SUPPLY_NURSE,
                OperatingRoom.NURSE_SHIFT_INDICATOR,
                OperatingRoom.START_DATE_TIME,
                OperatingRoom.END_DATE_TIME ,
                OperatingRoom.SATISFACTION_DEGREE,
                OperatingRoom.SMOOTH_INDICATOR,
                OperatingRoom.IN_FLUIDS_AMOUNT,
                OperatingRoom.OUT_FLUIDS_AMOUNT,
                OperatingRoom.BLOOD_LOSSED,
                OperatingRoom.BLOOD_TRANSFERED,
                OperatingRoom.ENTERED_BY };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 插入术后安排手术信息
        /// </summary>
        /// <param name="OperatingRoom"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertOperatingName(HisCommon.DataEntity.OPERATION_NAME OperatingRoom, BaseEntityer db)
        {
            string sql = @"insert into OPERATION_NAME (PATIENT_ID, VISIT_ID, OPER_ID, OPERATION_NO, OPERATION, OPERATION_CODE, OPERATION_SCALE, WOUND_GRADE)
                      values ('{0}', {1}, {2}, {3}, '{4}', {5}, '{6}', '{7}')";
            object[] obs = new object[]
               {
                OperatingRoom.  PATIENT_ID  ,
                OperatingRoom.  VISIT_ID    ,
                OperatingRoom.  OPER_ID ,
                OperatingRoom.  OPERATION_NO    ,
                OperatingRoom.  OPERATION   ,
                OperatingRoom.  OPERATION_CODE  ,
                OperatingRoom.  OPERATION_SCALE ,
                OperatingRoom.  WOUND_GRADE};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新安排的状态
        /// </summary>
        /// <param name="OperatingSchedule"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateOperationScheduleState(HisCommon.DataEntity.OPERATION_SCHEDULE OperatingSchedule, BaseEntityer db)
        {
            string sql = @"update  OPERATION_SCHEDULE  set ACK_INDICATOR='{3}' where PATIENT_ID='{0}' and VISIT_ID={1} and SCHEDULE_ID={2} ";
            object[] obs = new object[]
               {
                OperatingSchedule.PATIENT_ID,
                OperatingSchedule.VISIT_ID,
                OperatingSchedule.SCHEDULE_ID,
               OperatingSchedule.ACK_INDICATOR};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除手术安排信息
        /// </summary>
        /// <param name="OperatingRoom"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteOperatingMaster(HisCommon.DataEntity.OPERATION_MASTER OperatingRoom, BaseEntityer db)
        {
            string sql = @" delete from  OPERATION_MASTER  where PATIENT_ID='{0}' and  VISIT_ID={1} and  OPER_ID='{2}'";
            object[] obs = new object[]
               {
                OperatingRoom.PATIENT_ID,
                OperatingRoom.VISIT_ID,
                OperatingRoom.OPER_ID
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除手术安排信息明细
        /// </summary>
        /// <param name="OperatingRoom"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteOperatingName(HisCommon.DataEntity.OPERATION_NAME OperatingRoom, BaseEntityer db)
        {
            string sql = @" delete from  OPERATION_NAME  where PATIENT_ID='{0}' and  VISIT_ID={1} and  OPER_ID='{2}' and OPERATION_NO='{3}'";
            object[] obs = new object[]
               {
                OperatingRoom.PATIENT_ID,
                OperatingRoom.VISIT_ID,
                OperatingRoom.OPER_ID,
                OperatingRoom.OPERATION_NO
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除该患者本次全部手术明细
        /// </summary>
        /// <param name="OperatingRoom"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteOperatingNameByMaster(HisCommon.DataEntity.OPERATION_MASTER OperatingRoom, BaseEntityer db)
        {
            string sql = @" delete from  OPERATION_NAME  where PATIENT_ID='{0}' and VISIT_ID={1} and  OPER_ID='{2}'";
            object[] obs = new object[]
               {
                OperatingRoom.PATIENT_ID,
                OperatingRoom.VISIT_ID,
                OperatingRoom.OPER_ID
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除该患者全部护士换班信息
        /// </summary>
        /// <param name="OperatingRoom"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteOperationNurseShiftByMaster(HisCommon.DataEntity.OPERATION_MASTER OperatingRoom, BaseEntityer db)
        {
            string sql = @" delete from  OPERATION_NURSE_SHIFT  where PATIENT_ID='{0}' and VISIT_ID={1} and OPER_ID='{2}'";
            object[] obs = new object[]
               {
                OperatingRoom.PATIENT_ID,
                OperatingRoom.VISIT_ID,
                OperatingRoom.OPER_ID
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        public int DeleteOperationNurseShiftInfo(HisCommon.DataEntity.OPERATION_NURSE_SHIFT OperatingRoom, BaseEntityer db)
        {
            string sql = @" delete from  OPERATION_NURSE_SHIFT  where PATIENT_ID='{0}' and  VISIT_ID={1} and  OPER_ID='{2}' and SHIFT_DATE_TIME=to_date('{3}', 'yyyy-MM-dd hh24:mi:ss')";
            object[] obs = new object[]
               {
                OperatingRoom.PATIENT_ID,
                OperatingRoom.VISIT_ID,
                OperatingRoom.OPER_ID,
                OperatingRoom.Old_SHIFT_DATE_TIME
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 插入术后换班护士信息
        /// </summary>
        /// <param name="OperationNurseShift"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertOperationNurseShiftInfo(HisCommon.DataEntity.OPERATION_NURSE_SHIFT OperationNurseShift, BaseEntityer db)
        {
            string sql = @"insert into OPERATION_NURSE_SHIFT (PATIENT_ID, VISIT_ID, OPER_ID, SHIFT_DATE_TIME, FIRST_OPERATION_NURSE, SECOND_OPERATION_NURSE, FIRST_SUPPLY_NURSE, SECOND_SUPPLY_NURSE)
                       values ('{0}', {1}, {2}, to_date('{3}', 'yyyy-MM-dd hh24:mi:ss'), '{4}', '{5}', '{6}', '{7}')";
            object[] obs = new object[]
               {
                OperationNurseShift.    PATIENT_ID  ,
                OperationNurseShift.    VISIT_ID    ,
                OperationNurseShift.    OPER_ID ,
                OperationNurseShift.    SHIFT_DATE_TIME ,
                OperationNurseShift.    FIRST_OPERATION_NURSE   ,
                OperationNurseShift.    SECOND_OPERATION_NURSE  ,
                OperationNurseShift.    FIRST_SUPPLY_NURSE  ,
                OperationNurseShift.    SECOND_SUPPLY_NURSE };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 查询术后换班护士信息
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <param name="OPER_ID"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OPERATION_NURSE_SHIFT> GetOperationNurseShiftListInfo(string PATIENT_ID, int VISIT_ID, int OPER_ID)
        {
            string sql = @"select * from OPERATION_NURSE_SHIFT where PATIENT_ID='{0}' and VISIT_ID={1} and OPER_ID={2}";
            sql = string.Format(sql, PATIENT_ID, VISIT_ID, OPER_ID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OPERATION_NURSE_SHIFT>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 查询术后患者手术的信息
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <param name="OPER_ID"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OPERATION_NAME> GetOperationNameInfoList(string PATIENT_ID, int VISIT_ID, int OPER_ID)
        {
            string sql = @"select * from OPERATION_NAME where PATIENT_ID='{0}' and VISIT_ID={1} and OPER_ID={2}";
            sql = string.Format(sql, PATIENT_ID, VISIT_ID, OPER_ID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OPERATION_NAME>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 查询术后信息
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <param name="OPER_ID"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.OPERATION_MASTER GetOperationMasterInfoList(string PATIENT_ID, int VISIT_ID, int OPER_ID)
        {
            string sql = @"select * from OPERATION_MASTER where PATIENT_ID='{0}' and VISIT_ID={1} and OPER_ID={2}";
            sql = string.Format(sql, PATIENT_ID, VISIT_ID, OPER_ID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OPERATION_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList()[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.OPERATION_SCHEDULE GetOperationScheduleInfo(OPERATION_SCHEDULE operation)
        {
            string sql = @"select o.*,p.name,p.inp_no,p.date_of_birth from  operation_schedule o,pat_master_index p where o.patient_id=p.patient_id
                         and o.PATIENT_ID='{0}' and o.VISIT_ID={1} and o.SCHEDULE_ID={2} ";
            sql = string.Format(sql, operation.PATIENT_ID, operation.VISIT_ID, operation.SCHEDULE_ID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.OPERATION_SCHEDULE>(BaseEntityer.Db.GetDataSet(sql)).ToList()[0];
        }

        #region 医嘱单打印

        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateNurseOrderPrintFlag(HisCommon.DataEntity.InpatientOrder order)
        {
            string strSQL = @" update orders t
                               set t.print_flag = '1'
                             where t.patient_id = '{0}'
                               and t.visit_id = '{1}'
                               and t.order_no = '{2}'
                               and t.order_sub_no = '{3}'  ";
            strSQL = string.Format(strSQL, order.ORDERS.PATIENT_ID, order.ORDERS.VISIT_ID, order.ORDERS.ORDER_NO, order.ORDERS.ORDER_SUB_NO);
            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateDoctorOrderPrintFlag(HisCommon.DataEntity.InpatientOrder order)
        {
            string strSQL = @" update doctor_orders t
                               set t.print_flag = '1'
                             where t.patient_id = '{0}'
                               and t.visit_id = '{1}'
                               and t.order_no = '{2}'
                               and t.order_sub_no = '{3}'  ";
            strSQL = string.Format(strSQL, order.DOCTOR_ORDERS.PATIENT_ID, order.DOCTOR_ORDERS.VISIT_ID, order.DOCTOR_ORDERS.ORDER_NO, order.DOCTOR_ORDERS.ORDER_SUB_NO);
            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        #endregion

        #region 维康医院PACS接口

        /// <summary>
        /// 插入数据到BrokerUp
        /// </summary>
        /// <returns></returns>
        public int PacsInterfaceBroker(EXAM_PAT_MI exam_pat_mi, EXAM_MASTER exam_master)
        {
            string strSQL = @" SELECT exam_items.exam_no,
                                   exam_items.exam_item_no,
                                   exam_items.exam_item,
                                   exam_items.exam_item_code,
                                   exam_items.costs
                              FROM exam_items
                             where exam_no = '{0}'
                             ORDER BY exam_items.exam_item_no ASC ";
            strSQL = string.Format(strSQL, exam_master.EXAM_NO);

            List<EXAM_ITEMS> items = new List<EXAM_ITEMS>();

            DbDataReader reader = BaseEntityer.Db.ZDExecReader(strSQL);
            while (reader.Read())
            {
                EXAM_ITEMS item = new EXAM_ITEMS();
                item.EXAM_ITEM_NO = Convert.ToInt32(reader[1].ToString());
                item.EXAM_ITEM_CODE = reader[3].ToString();
                item.EXAM_ITEM = reader[2].ToString();
                item.COSTS = Convert.ToDecimal(reader[4].ToString());

                if (item.EXAM_ITEM_CODE != "C220800008")
                {
                    items.Add(item);
                }
            }
            if (!reader.IsClosed)
                reader.Close();
            string source = string.Empty;
            string inp_no = "";
            strSQL = " select inp_no,id_no from pat_master_index t where t.patient_id = '{0}' ";
            strSQL = string.Format(strSQL, exam_pat_mi.PATIENT_ID);
            string IDNO="";//身份证号码
            DbDataReader dbreader = BaseEntityer.Db.ZDExecReader(strSQL);
            while (dbreader.Read())
            {
                source = string.IsNullOrEmpty(dbreader[0].ToString()) ? "O" : "I";
                inp_no = dbreader[0].ToString();
                IDNO = dbreader[1].ToString();
            }

            if (!dbreader.IsClosed)
                dbreader.Close();

            if (source =="O")
            {

                strSQL = " select  visit_no from clinic_master t where t.patient_id = '{0}' ";
             strSQL = string.Format(strSQL, exam_pat_mi.PATIENT_ID);
           
               dbreader = BaseEntityer.Db.ZDExecReader(strSQL);
             while (dbreader.Read())
             { 
                // inp_no = dbreader[0].ToString(); 
                 exam_pat_mi.PHONE_NUMBER=dbreader[0].ToString(); 
             }

             if (!dbreader.IsClosed)
                 dbreader.Close();
            }

            System.Data.DataTable dt = BaseEntityer.Db.GetDataTable(@"select sysdate from dual");
            DateTime time = new DateTime();
            if (dt != null)
                time = DateTime.Parse(dt.Rows[0][0].ToString());

            string timeStr = time.Year.ToString() + time.Month.ToString() + time.Day.ToString() + time.Hour.ToString() + time.Minute.ToString() + time.Second.ToString();

            //用于生成流水号
            int noSeq = 1;
            foreach (EXAM_ITEMS item in items)
            {
                string spellCode = string.Empty;

                System.Data.DataTable dt1 = BaseEntityer.Db.GetDataTable(@"SELECT   F_GetPinYinCode('" + item.EXAM_ITEM + "') FROM  dual ");
               
                if (dt1 != null)
                    spellCode = dt1.Rows[0][0].ToString();

                #region SQL
                strSQL = @" INSERT into BROKER 
                              (PATIENT_ID,
                               PATIENT_NAME, 
                               IDNO, 
                               PATIENT_SEX,
                               PATIENT_SOURCE, 
                               OCCUPATION, 
                               PATIENT_BIRTH, 
                               PATIENT_AGE, 
                               PATIENT_TEL, 
                               PATIENT_ADDRESS,
                               DIAGDOCTOR, 
                               DIAGDOCTOR_NO, 
                               DEPT, 
                               DEPT_NAME, 
                               ACCESSION_NUM, 
                               DIAGDOCT, 
                               DIAGDOCT_NO, 
                               ADMISSION_ID, 
                               SCHED_PROC_ID, 
                               SCHED_PROC_DESC, 
                               SCHED_PROC_DESC_EN, 
                               CLINICAL_INFOR, 
                               SICK_NAME, 
                               ASSIGN, 
                               IPD_NO, 
                               PATIENT_BED, 
                               STATUS, 
                               MODALITY, 
                               ICDNINE, 
                               ORDCANCEL, 
                               UPD_DATETIME, 
                               EVENTTIME, 
                               BODYPARTS, 
                               FEE_STATUS, 
                               FEE, 
                               PATIENT_MOB, 
                               PATIENT_FAX  
                               )
                            VALUES
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
                                '{35}', 
                                getdate(), 
                               '{30}', 
                               '{31}', 
                               '{32}', 
                               '{33}', 
                               '{34}'  
                               ) ";
                #endregion

                #region 检查类别对应编码

                string type = string.Empty;

                string sqlTemp = @" select t.name
                                      From com_dictionary t
                                     where t.type = (select m.param_value
                                                       from sys_param m
                                                      where m.param_name = 'DefaultPACSPARAM')
                                       and t.code = '{0}' ";
                sqlTemp = string.Format(sqlTemp, exam_master.EXAM_CLASS);

                var result = BaseEntityer.Db.ExecuteScalar(sqlTemp);
                type = result == null || result.ToString() == "" ? string.Empty : result.ToString();

                #endregion

                string no = DateTime.Now.ToString("yyyyMMdd") + exam_master.EXAM_NO + noSeq.ToString();
                string sex = exam_pat_mi.SEX == "男" ? "1" : "2";

                int age = 20;
                if (exam_pat_mi.DATE_OF_BIRTH != null)
                {
                    if (exam_pat_mi.DATE_OF_BIRTH > Convert.ToDateTime("1800-01-01"))
                    {
                        age = DateTime.Now.Year - exam_pat_mi.DATE_OF_BIRTH.Value.Year;
                    }

                }
               
                string deptName = GetDeptName(exam_master.REQ_DEPT);
                string birthday = exam_pat_mi.DATE_OF_BIRTH.Value.ToString("yyyyMMdd");
                string dname = GetUserName(exam_master.REQ_PHYSICIAN);
                string strSQL1 = "select * from pat_visit where PATIENT_ID='" + exam_master.PATIENT_ID + "' and visit_id='"+exam_master.VISIT_ID+"'";
                string jzys_id = "";
                string jzys_name = "";
                DataTable dtname = BaseEntityer.Db.GetDataTable(strSQL1);
                if (dtname.Rows.Count > 0)
                {
                    jzys_id = dtname.Rows[0]["DOCTOR_IN_CHARGE"].ToString();
                    jzys_name = GetUserName(jzys_id);
                }

                strSQL = string.Format(strSQL, exam_pat_mi.PATIENT_ID, exam_pat_mi.NAME, IDNO, sex, source, "11", birthday,
                    age, exam_pat_mi.PHONE_NUMBER, exam_pat_mi.MAILING_ADDRESS, jzys_name, jzys_id, exam_master.REQ_DEPT, deptName, no,
                    dname, exam_master.REQ_PHYSICIAN, exam_master.EXAM_NO, item.EXAM_ITEM_CODE, item.EXAM_ITEM, spellCode, "", exam_master.CLIN_DIAG, "N",
                    inp_no,  exam_master.NOTICE, "N", type, exam_master.CLIN_DIAG, "N", exam_master.EXAM_SUB_CLASS, "1", item.COSTS,
                    exam_pat_mi.PHONE_NUMBER, exam_pat_mi.PHONE_NUMBER, timeStr );

                //



                int ret = BaseEntityer.Db.ZDExecNonQuery("OraConPacsInterface", strSQL);
                FileStream fs = new FileStream(@"d:\mm.txt", FileMode.Create);
                //实例化一个StreamWriter-->与fs相关联
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(ret.ToString() + "----" + strSQL);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();

                noSeq++;
            }

            return 1;
        }
        private string GetUserName(string user_id)
        {
            string dname = "";
            string strSQL1 = "select * from users_staff_dict where user_id='" + user_id + "'";
         
            DataTable dtname = BaseEntityer.Db.GetDataTable(strSQL1);
            if (dtname.Rows.Count > 0)
            {
                dname = dtname.Rows[0]["user_name"].ToString();
            }
            return dname;
        }

        private string GetDeptName(string deptCode)
        {
            string deptName = string.Empty;
            string strSQL = " select fun_getdeptname('" + deptCode + "') name from dual ";
            DbDataReader dbreader = BaseEntityer.Db.ZDExecReader(strSQL);
            while (dbreader.Read())
            {
                deptName = dbreader[0].ToString();
            }
            if (!dbreader.IsClosed)
            {
                dbreader.Close();
            }
            return deptName;
        }

        /// <summary>
        /// 是否启动PACS接口
        /// </summary>
        /// <returns></returns>
        public bool PacsInterfaceEnable()
        {
            string strSQL = @" SELECT t.PARAM_NAME, --参数名称
                                   t.PARAM_VALUE, --参数值
                                   t.PARAM_DESC, --描述
                                   t.MODEL, --所属功能模块
                                   t.CONTROL_TYPE, --显示在维护窗体中的控件类型，0--文本框，1--下拉框
                                   t.COMBOX_ITEMS --以竖线""隔开,作为下拉框的值的分隔符
                              FROM SYS_PARAM t --
                             WHERE t.param_name = 'PacsInterfaceEnable' ";
            DbDataReader reader = BaseEntityer.Db.ZDExecReader(strSQL);

            int result = 0;
            while (reader.Read())
            {
                result = Convert.ToInt32(reader[1].ToString());
            }
            if (!reader.IsClosed)
            {
                reader.Close();
            }
            return result == 1 ? true : false;
        }

        /// <summary>
        /// 插入数据到BrokerPatient
        /// </summary>
        /// <returns></returns>
        public int PacsInterfaceBrokerPatient(HisCommon.DataEntity.InPatientInfo patientInfo)
        {
            #region SQL

            string strSQL = @" INSERT into SCHEDULE_PATIENT t --
                                  (t.PATIENT_ID, --
                                   t.PATIENT_NAME, --
                                   t.PATIENT_ENGLISH_NAME, --
                                   t.IDNO, --
                                   t.PATIENT_BIRTH, --
                                   t.PATIENT_AGE, --
                                   t.PATIENT_SEX, --
                                   t.PATIENT_MOB, --
                                   t.PATIENT_TEL, --
                                   t.PATIENT_FAX, --
                                   t.ADDRESS, --
                                   t.HOME_TOWN, --
                                   t.EMAIL, --
                                   t.INSERT_DATETIME 
                                   )
                                VALUES
                                  ('{0}', --
                                   '{1}', --
                                   '{2}', --
                                   '{3}', --
                                   '{4}', --
                                   '{5}', --
                                   '{6}', --
                                   '{7}', --
                                   '{8}', --
                                   '{9}', --
                                   '{10}', --
                                   '{11}', --
                                   '{12}', --
                                   sysdate --
                                    
                                   ) ";

            #endregion

            int age = 0;
            TimeSpan t = DateTime.Now - patientInfo.patMasterIndex.DATE_OF_BIRTH.Value;
            age = Convert.ToInt32(t.TotalDays / 365);
            strSQL = string.Format(strSQL, patientInfo.patVisit.PATIENT_ID, patientInfo.patMasterIndex.NAME, "", patientInfo.patMasterIndex.ID_NO,
                patientInfo.patMasterIndex.DATE_OF_BIRTH, age, patientInfo.patMasterIndex.SEX, patientInfo.patMasterIndex.NEXT_OF_KIN_PHONE,
                "", "", patientInfo.patMasterIndex.NEXT_OF_KIN_ADDR, "", "", "" );

            return BaseEntityer.Db.ZDExecNonQuery("OraConPacsInterface", strSQL);
        }

        #endregion

        /// <summary>
        /// 根据患者住院visit_id和patient_id查询PAT_VISIT信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public PAT_VISIT GetPatVisitModel(string patientId, int visitId)
        {
            string sql = @"select * from PAT_VISIT t where t.patient_id='{0}' and t.visit_id={1}";
            sql = sql.SqlFormate(patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<PAT_VISIT>(ds).First();
            else
                return null;
        }

        /// <summary>
        ///  获取患者担保金额
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public decimal GetPatVisitAssureCostByID(string patientId, int visitId,ref string errMsg)
        {

            try
            {
                string sql = @"SELECT nvl(t.assure_costs,0)
  FROM pat_visit t
 WHERE t.patient_id = '{0}'
   AND t.visit_id = {1}
";
                sql = sql.SqlFormate(patientId, visitId);

                return BaseEntityer.Db.ExecuteScalar<decimal>(sql);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
                return 0;
            }
        }

        /// <summary>
        ///  更新患者
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <returns></returns>
        public int UpdatePatientInhosptionAdviceFlag(BaseEntityer db ,string patientID, string visitID, string flag, ref string errMsg)
        {
            try
            {
                string sql = @"
UPDATE pats_in_hospital r
   SET r.advice_orders = '{0}'
 WHERE r.patient_id = '{1}'
   AND r.visit_id = '{2}'
";
                sql = sql.SqlFormate(flag, patientID, visitID);

                return db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
                return 0;
            }
        }
    }
}
