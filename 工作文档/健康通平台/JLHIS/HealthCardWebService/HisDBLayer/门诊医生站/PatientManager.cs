using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HisCommon.DataEntity;
using HisDBLayer;
using HisCommon;
/// <summary>
/// [功能描述: 患者操作]<br></br>
/// [创 建 者: 王荣斌]<br></br>
/// [创建时间: 2012-07-12]<br></br>
/// <修改>
///		<修改人></修改人>
///		<修改时间></修改时间>
///		<修改说明></修改说明>
/// </修改>
/// </summary>
namespace HisDBLayer
{
    public class PatientManager
    {
        #region-------------查询患者列表--------------
        /// <summary>
        /// 根据条件查询挂号患者
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="dept">挂号部门</param>
        /// <param name="isJz">是否接诊</param>
        /// <returns></returns>
        public IList<CLINIC_MASTER> GetPatients(string beginDate, string endDate, string dept, int isJz)
        {
            //2013-12-10 by li 查询条件中VISIT_DATE不能更换为REGISTERING_DATE
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
   WHERE CLINIC_MASTER.VISIT_DATE >= to_date('{0}','yyyy-MM-dd hh24:mi:ss') AND  
         CLINIC_MASTER.VISIT_DATE <= to_date('{1}','yyyy-MM-dd hh24:mi:ss') AND  
         ( CLINIC_MASTER.VISIT_DEPT = '{2}'  or
         '{2}' is null) AND  
         nvl(CLINIC_MASTER.ADMIS,'0') = {3} and clinic_master.returned_operator is null";
            sql = string.Format(sql, new object[] { beginDate, endDate, dept, isJz });
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(patients);
        }
        /// <summary>
        /// 根据条件查询已接诊挂号患者
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="doctor">接诊医生</param>
        /// <returns></returns>
        public IList<CLINIC_MASTER> GetPatientsByDoctor(string beginDate, string endDate, string doctor, string patientName)
        {
            string sql =
          @"SELECT CLINIC_MASTER.VISIT_DATE,   
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
         CLINIC_MASTER.CHARGE_TYPE_CODE,  
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
       A.ADMIS_date,
(select count(*) from outp_presc p where A.visit_date=p.visit_date and A.visit_no=p.visit_no and p.charge_indicator=1)+
          (select count(*) from outp_treat_rec p where A.visit_date=p.visit_date and A.visit_no=p.visit_no and p.charge_indicator=1)
          as charge_indicator
  FROM CLINIC_ADMIS A, CLINIC_MASTER 
 where a.visit_date = CLINIC_MASTER.visit_date
   and a.visit_no = CLINIC_MASTER.visit_no
    and  a.admis_date >= trunc(to_date('{0}','yyyy-MM-dd hh24:mi:ss'))
    and  a.admis_date <= trunc(to_date('{1}','yyyy-MM-dd hh24:mi:ss'))+1
   and a.doctor = '{2}' and CLINIC_MASTER.NAME like '%{3}%'";
            sql = string.Format(sql, new object[] { beginDate, endDate, doctor, patientName });
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(patients);
        }
        // TODO: 在此添加您的服务操作
        /// <summary>
        /// 根据visit_no检索患者
        /// </summary>
        /// <param name="VisitNo">就诊序号</param>
        /// <param name="date">起始日期</param>
        /// <returns></returns>
        public IList<CLINIC_MASTER> GetPatientByNO(string VisitNo, string date)
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
         d.insurance_type_name,
         CLINIC_MASTER.INSURANCE_NO,   
         CLINIC_MASTER.UNIT_IN_CONTRACT,   
         CLINIC_MASTER.CLINIC_TYPE,  
         CLINIC_MASTER.FIRST_VISIT_INDICATOR,   
         CLINIC_MASTER.VISIT_DEPT,   
         CLINIC_MASTER.VISIT_SPECIAL_CLINIC,   
         CLINIC_MASTER.DOCTOR,   
         e.dept_name as admis_dept,
         m.doctor as admis_doctor,
         u.user_name as admis_doctor_name,
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
         CLINIC_MASTER.CLINC_TYPE
    FROM CLINIC_MASTER  
 left join INSURANCE_TYPE_DICT d on CLINIC_MASTER.Insurance_Type=d.insurance_type_code
   left join dept_dict e 
 on e.dept_code=clinic_master.visit_dept
 left join clinic_admis m 
 on clinic_master.visit_date=m.visit_date
 and clinic_master.visit_no=m.visit_no
left join  users_staff_dict u 
on u.user_id=m.doctor
 WHERE (CLINIC_MASTER.VISIT_DATE> =to_date('{1}','yyyy-MM-dd hh24:mi:ss') or '{1}' is null) AND (CLINIC_MASTER.VISIT_NO={0} or CLINIC_MASTER.PATIENT_ID='{0}') and CLINIC_MASTER.returned_operator IS  null
   ORDER BY CLINIC_MASTER.VISIT_DATE DESC";
            sql = string.Format(sql, new object[] { VisitNo, date });
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(patients);
        }
        //add by lql 2016-09-06 start
        public IList<CLINIC_MASTER> GetPatientByNOAndName(string VisitNo, string Name, string Idno)
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
         d.insurance_type_name,
         CLINIC_MASTER.INSURANCE_NO,   
         CLINIC_MASTER.UNIT_IN_CONTRACT,   
         CLINIC_MASTER.CLINIC_TYPE,  
         CLINIC_MASTER.FIRST_VISIT_INDICATOR,   
         CLINIC_MASTER.VISIT_DEPT,   
         CLINIC_MASTER.VISIT_SPECIAL_CLINIC,   
         CLINIC_MASTER.DOCTOR,   
         e.dept_name as admis_dept,
         m.doctor as admis_doctor,
         u.user_name as admis_doctor_name,
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
         CLINIC_MASTER.CLINC_TYPE,
       k.diag_desc
    FROM CLINIC_MASTER  
 left join INSURANCE_TYPE_DICT d on CLINIC_MASTER.Insurance_Type=d.insurance_type_code
   left join dept_dict e 
 on e.dept_code=clinic_master.visit_dept
 left join clinic_admis m 
 on clinic_master.visit_date=m.visit_date
 and clinic_master.visit_no=m.visit_no
left join  users_staff_dict u 
on u.user_id=m.doctor
left join  PAT_MASTER_INDEX p
on CLINIC_MASTER.Patient_Id=p.patient_id
  left join outp_mr k
    on CLINIC_MASTER.VISIT_DATE=k.visit_date
    and CLINIC_MASTER.VISIT_NO=k.visit_no
 WHERE  CLINIC_MASTER.returned_operator IS  null ";
            if (!string.IsNullOrEmpty(VisitNo))
            {
                sql = sql + " and  (CLINIC_MASTER.VISIT_NO=" + VisitNo + " or CLINIC_MASTER.PATIENT_ID='" + VisitNo + "') ";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                sql = sql + " and  (CLINIC_MASTER.Name='" + Name + "')";
            }
            if (!string.IsNullOrEmpty(Idno.Split('|')[0].ToString()))
            {
                sql = sql + " and  (p.id_no='" + Idno.Split('|')[0].ToString() + "') ";
            }
            if (!string.IsNullOrEmpty(Idno.Split('|')[1].ToString()))
            {
                sql = sql + " and  (k.diag_desc like'%" + Idno.Split('|')[1].ToString() + "%') ";
            }

            sql = sql + "ORDER BY CLINIC_MASTER.VISIT_DATE DESC";
            sql = string.Format(sql, new object[] { VisitNo });
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(patients);
        }
        //add by lql 2016-09-06 end
        public IList<CLINIC_MASTER> GetPatientByNONew(string VisitNo, string date)
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
         d.insurance_type_name,
         CLINIC_MASTER.INSURANCE_NO,   
         CLINIC_MASTER.UNIT_IN_CONTRACT,   
         CLINIC_MASTER.CLINIC_TYPE,  
         CLINIC_MASTER.FIRST_VISIT_INDICATOR,   
         CLINIC_MASTER.VISIT_DEPT,   
         CLINIC_MASTER.VISIT_SPECIAL_CLINIC,   
         CLINIC_MASTER.DOCTOR,   
         e.dept_name as admis_dept,
         m.doctor as admis_doctor,
         u.user_name as admis_doctor_name,
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
         CLINIC_MASTER.ADMIS
    FROM CLINIC_MASTER  
 left join INSURANCE_TYPE_DICT d on CLINIC_MASTER.Insurance_Type=d.insurance_type_code
   left join dept_dict e 
 on e.dept_code=clinic_master.visit_dept
 left join clinic_admis m 
 on clinic_master.visit_date=m.visit_date
 and clinic_master.visit_no=m.visit_no
left join  users_staff_dict u 
on u.user_id=m.doctor
 WHERE (CLINIC_MASTER.VISIT_DATE =to_date('{1}','yyyy-MM-dd hh24:mi:ss') or '{1}' is null) AND (CLINIC_MASTER.VISIT_NO={0})
   ORDER BY CLINIC_MASTER.VISIT_DATE DESC";
            sql = string.Format(sql, new object[] { VisitNo, date });
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(patients);
        }
        /// <summary>
        /// 根据就诊日期和就诊序号得到唯一患者
        /// </summary>
        /// <param name="visitNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public CLINIC_MASTER GetUniquePatient(string visitNo, string date)
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
         CLINIC_MASTER.INSURANCE_TYPE,   
         CLINIC_MASTER.INSURANCE_NO,   
         CLINIC_MASTER.UNIT_IN_CONTRACT,   
         CLINIC_MASTER.CLINIC_TYPE,   
         CLINIC_MASTER.CHARGE_TYPE_CODE,  
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
         ''  
    FROM CLINIC_MASTER  
   WHERE CLINIC_MASTER.VISIT_DATE=to_date('{1}','yyyy-MM-dd hh24:mi:ss') AND CLINIC_MASTER.VISIT_NO={0} and CLINIC_MASTER.returned_operator IS  null";
            sql = string.Format(sql, new object[] { visitNo, date });
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<CLINIC_MASTER>(patients);
            if (list.Count == 0)
                return null;
            return list[0];
        }
        /// <summary>
        /// 对患者进行接诊
        /// </summary>
        /// <param name="visitNo">就诊序号</param>
        /// <param name="date">就诊时间</param>
        /// <param name="doctor">操作医生</param>
        /// <returns></returns>
        public int AddClinicAdmis(string visitNo, string visitDate, string doctor, BaseEntityer db = null)
        {
            //添加就诊记录
            string insertSql = @"insert into clinic_admis(VISIT_DATE,VISIT_NO,ADMIS_DATE,DOCTOR)
  values(TO_DATE('{0}','yyyy-MM-dd hh24:mi:ss'),{1},sysdate,'{2}')";
            insertSql = string.Format(insertSql, new object[] { visitDate, visitNo, doctor });
            if (db == null)
                return BaseEntityer.Db.ExecuteNonQuery(insertSql);
            else return db.ExecuteNonQuery(insertSql);
        }
        /// <summary>
        /// 删除接诊记录
        /// </summary>
        /// /// <param name="visitNo">db事务控制用</param>
        /// <param name="visitNo">就诊序号</param>
        /// <param name="date">就诊时间</param>
        /// <param name="doctor">操作医生</param>
        /// <returns></returns>
        public int DelClinicAdmis(string visitNo, string visitDate, string doctor, BaseEntityer db = null)
        {
            //添加就诊记录
            string delSql = @"delete from clinic_admis
where VISIT_DATE=TO_DATE('{0}','yyyy-MM-dd hh24:mi:ss')
and VISIT_NO={1}
and DOCTOR='{2}' ";
            delSql = string.Format(delSql, new object[] { visitDate, visitNo, doctor });
            if (db == null)
                return BaseEntityer.Db.ExecuteNonQuery(delSql);
            else return db.ExecuteNonQuery(delSql);
        }
        /// <summary>
        /// 更改接诊状态
        /// </summary>
        /// <param name="visitNo">就诊序号</param>
        /// <param name="date">就诊时间</param>
        /// <param name="dept">操作部门</param>
        /// <returns></returns>
        public int UpdatePatientAdmis(string visitNo, string visitDate, string dept, int admis, BaseEntityer db = null)
        {
            string updateSql = @"update clinic_master
   set admis = '{3}', visit_dept = '{0}'
 where VISIT_DATE = TO_DATE('{1}','yyyy-MM-dd hh24:mi:ss')
   and VISIT_NO = {2}";
            updateSql = string.Format(updateSql, new object[] { dept, visitDate, visitNo, admis });
            if (db == null)
                return BaseEntityer.Db.ExecuteNonQuery(updateSql);
            else return db.ExecuteNonQuery(updateSql);
        }
        /// <summary>
        /// 查询指定医生对患者的接诊记录
        /// </summary>
        /// <param name="visitNo">就诊序号</param>
        /// <param name="date">就诊时间</param>
        /// <param name="doctor">操作医生</param>
        /// <returns></returns>
        public DataTable GetPatientAdmisRecord(string visitNo, string visitDate, string doctor)
        {
            string Sql = @"select * from CLINIC_ADMIS t
where t.visit_date=to_Date('{0}','yyyy-MM-dd hh24:mi:ss')
and t.visit_no='{1}'
and t.doctor='{2}'";
            Sql = string.Format(Sql, new object[] { visitDate, visitNo, doctor });
            return BaseEntityer.Db.GetDataTable(Sql);
        }
        #endregion
        #region--------------门诊病历----------------
        /// <summary>
        /// 根据就诊序号贺就诊日期查询患者病历
        /// </summary>
        /// <param name="visitNO">就诊序号</param>
        /// <param name="visitDate">就诊日期</param>
        /// <returns></returns>
        public OUTP_MR GetPatientMedicalRecord(string visitNO, string visitDate)
        {
            string sql = @"SELECT  *
        FROM OUTP_MR    
        WHERE       ( ( OUTP_MR.VISIT_NO = {0} ) )  and ( OUTP_MR.VISIT_DATE = to_date('{1}','yyyy-MM-dd hh24:mi:ss') ) ";
            sql = string.Format(sql, new object[] { visitNO, visitDate });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            var mr = DataSetToEntity.DataSetToT<OUTP_MR>(ds).First();
            return mr;
        }

        /// <summary>
        /// 查询患者病历（按照医生姓名）
        /// </summary>
        /// <param name="doctor">按照医生姓名</param>
        /// <returns></returns>
        public List<OUTP_MR> GetPatientMedicalRecordList(string doctor)
        {
            string sql = @"select * from outp_mr t where t.doctor = '{0}' order by t.visit_date desc";
            sql = string.Format(sql, new object[] { doctor });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            return DataSetToEntity.DataSetToT<OUTP_MR>(ds).ToList();
        }

        /// <summary>
        /// 查询患者病历（按照医生姓名）
        /// </summary>
        /// <param name="doctor">按照医生姓名</param>
        /// <returns></returns>
        public List<BringSpringObject> GetPatientMedicalRecordListpatient(string doctor)
        {
            string sql = @"select t.patient_id  ID,
                                   s.name        Name,
                                   t.visit_date  Memo,
                                   t.visit_no    User01,
                                   c.pact_name User02
                              from pat_master_index s, outp_mr t
                              left join siinfo c
                                on c.inpatient_id = t.patient_id
                               and c.shiftdate = t.visit_date
                               and c.visit_id = t.visit_no
                               and c.type_code = 1
                             where t.doctor = '{0}'
                               and t.patient_id = s.patient_id
                               and t.patient_id = c.inpatient_id
                               and c.medical_type = 18
                               and c.balance_state = 1
                               group by t.patient_id ,
                                   s.name,
                                   t.visit_date,
                                   t.visit_no,
                                   c.pact_name
                             order by t.visit_date desc";
            sql = string.Format(sql, new object[] { doctor });
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            return DataSetToEntity.DataSetToT<BringSpringObject>(ds).ToList();
        }

        /// <summary>
        ///增加患者电子病历
        /// </summary>
        /// <param name="mr">病历实体类</param>
        /// <returns></returns>
        public int InsertMedicalRecord(OUTP_MR mr, BaseEntityer db)
        {
            //2014-1-13 by li 增加慢性病标识赋值
            //2014-2-21 by li 四平医院增加门诊皮试结果赋值
            string Sql = @"insert into OUTP_MR
                              (PATIENT_ID,
                               ILLNESS_DESC,
                               ANAMNESIS,
                               FAMILY_ILL,
                               MARRITAL,
                               INDIVIDUDL,
                               MENSES,
                               MED_HISTORY,
                               BODY_EXAM,
                               DIAG_DESC,
                               ADVICE,
                               VISIT_DATE,
                               VISIT_NO,
                               DOCTOR,
                               DIAG_CODE,
                               DIAGNOSE_IDENTIFICATION,
                               CHECK_BODY,
                               PRINTING,
                               HANDLE,
                               TREAT,
                               INFORMED_CONSENTFORM,
                               RECORDPAGE,
                               OPERATE_DATE,
                               OPERATE_STARTDATE,
                               OPERATE_ENDDATE,
                               PREOPERATIVE_DIAG,
                               INTRAOPERATIVE_DIAG,
                               OPERATE_NAME,
                               OPERATE_OPER,
                               HELPER,
                               NURSE,
                               ANALGESIST,
                               ANALGESIA_METHOD,
                               OPERATE,
                               IS_CHRONIC_DISEASE,
                               SKIN_TEST_RESULT)
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
                               to_date('{11}', 'yyyy-MM-dd hh24:mi:ss'),
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
                               to_date('{22}', 'yyyy-MM-dd hh24:mi:ss'),
                               to_date('{23}', 'yyyy-MM-dd hh24:mi:ss'),
                               to_date('{24}', 'yyyy-MM-dd hh24:mi:ss'),
                               '{25}',
                               '{26}',
                               '{27}',
                               '{28}',
                               '{29}',
                               '{30}',
                               '{31}',
                               '{32}',
                               '{33}',
                               {34},
                               '{35}')";
            object[] param = new object[]
            {
               mr.PATIENT_ID, 
               mr.ILLNESS_DESC,
               mr.ANAMNESIS, 
               mr.FAMILY_ILL,
               mr.MARRITAL, 
               mr.INDIVIDUDL,
               mr.MENSES,
               mr.MED_HISTORY,
               mr.BODY_EXAM, 
               mr.DIAG_DESC,
               mr.ADVICE, 
               mr.VISIT_DATE, 
               mr.VISIT_NO,
               mr.DOCTOR,
               mr.DIAG_CODE,
               mr.DIAGNOSE_IDENTIFICATION,
                mr.CHECK_BODY,
                mr.PRINTING,
                mr.HANDLE,
                mr.TREAT,
                mr.INFORMED_CONSENTFORM,
                mr.RECORDPAGE,
                mr.OPERATE_DATE.ToString("yyyy-MM-dd HH:mm:ss"),
                mr.OPERATE_STARTDATE.ToString("yyyy-MM-dd HH:mm:ss"),
                mr.OPERATE_ENDDATE.ToString("yyyy-MM-dd HH:mm:ss"),
                mr.PREOPERATIVE_DIAG,
                mr.INTRAOPERATIVE_DIAG,
                mr.OPERATE_NAME,
                mr.OPERATE_OPER,
                mr.HELPER,
                mr.NURSE,
                mr.ANALGESIST,
                mr.ANALGESIA_METHOD,
                mr.OPERATE,
                mr.IS_CHRONIC_DISEASE,
                mr.SKIN_TEST_RESULT
            };
            Sql = string.Format(Sql, param);
            return db.ExecuteNonQuery(Sql);
        }
        /// <summary>
        /// 更新电子病历
        /// </summary>
        /// <param name="mr"></param>
        /// <returns>病历实体类</returns>
        public int UpdateMedicalRecord(OUTP_MR mr, BaseEntityer db)
        {
            //2013-12-20 by li 开毒麻药品患者保存于病历本中
            //2014-1-13 by li 增加慢性病标识赋值
            //2014-2-21 by li 四平医院增加门诊皮试结果赋值
            string sql = @"update OUTP_MR
                           set PATIENT_ID              = '{0}',
                               ILLNESS_DESC            = '{1}',
                               ANAMNESIS               = '{2}',
                               FAMILY_ILL              = '{3}',
                               MARRITAL                = '{4}',
                               INDIVIDUDL              = '{5}',
                               MENSES                  = '{6}',
                               MED_HISTORY             = '{7}',
                               BODY_EXAM               = '{8}',
                               DIAG_DESC               = '{9}',
                               ADVICE                  = '{10}',
                               VISIT_DATE              = to_date('{11}','yyyy-MM-dd hh24:mi:ss'),
                               VISIT_NO                = '{12}',
                               DOCTOR                  = '{13}',
                               DIAG_CODE               = '{14}',
                               DIAGNOSE_IDENTIFICATION = '{15}',
                               CHECK_BODY              = '{16}',
                               PRINTING                = '{17}',
                               HANDLE                  = '{18}',
                               TREAT                   = '{19}',
                               INFORMED_CONSENTFORM    = '{20}',
                               RECORDPAGE              = '{21}',
                               OPERATE_DATE            = to_date('{22}','yyyy-MM-dd hh24:mi:ss'),
                               OPERATE_STARTDATE       = to_date('{23}','yyyy-MM-dd hh24:mi:ss'),
                               OPERATE_ENDDATE         = to_date('{24}','yyyy-MM-dd hh24:mi:ss'),
                               PREOPERATIVE_DIAG       = '{25}',
                               INTRAOPERATIVE_DIAG     = '{26}',
                               OPERATE_NAME            = '{27}',
                               OPERATE_OPER            = '{28}',
                               HELPER                  = '{29}',
                               NURSE                   = '{30}',
                               ANALGESIST              = '{31}',
                               ANALGESIA_METHOD        = '{32}',
                               OPERATE                 = '{33}',
                               GET_DRUG_PERSON         = '{34}',
                               IS_CHRONIC_DISEASE      = {35},
                               SKIN_TEST_RESULT        = '{36}'
                        where OUTP_MR.Visit_Date = to_date('{11}','yyyy-MM-dd hh24:mi:ss')
                                  and OUTP_MR.Visit_NO = '{12}'";
            object[] param = new object[]
            {
               mr.PATIENT_ID, 
               mr.ILLNESS_DESC,
               mr.ANAMNESIS, 
               mr.FAMILY_ILL,
               mr.MARRITAL, 
               mr.INDIVIDUDL,
               mr.MENSES,
               mr.MED_HISTORY,
               mr.BODY_EXAM, 
               mr.DIAG_DESC,
               mr.ADVICE, 
               mr.VISIT_DATE, //11
               mr.VISIT_NO,
               mr.DOCTOR,
               mr.DIAG_CODE,
               mr.DIAGNOSE_IDENTIFICATION,
               mr.CHECK_BODY,
                mr.PRINTING,
                mr.HANDLE,
                mr.TREAT,
                mr.INFORMED_CONSENTFORM,
                mr.RECORDPAGE,
                mr.OPERATE_DATE.ToString("yyyy-MM-dd HH:mm:ss"),//22
                mr.OPERATE_STARTDATE.ToString("yyyy-MM-dd HH:mm:ss"),
                mr.OPERATE_ENDDATE.ToString("yyyy-MM-dd HH:mm:ss"),
                mr.PREOPERATIVE_DIAG,
                mr.INTRAOPERATIVE_DIAG,
                mr.OPERATE_NAME,
                mr.OPERATE_OPER,
                mr.HELPER,
                mr.NURSE,
                mr.ANALGESIST,
                mr.ANALGESIA_METHOD,
                mr.OPERATE,
                mr.GET_DRUG_PERSON,
                mr.IS_CHRONIC_DISEASE,
                mr.SKIN_TEST_RESULT
            };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 医生站更新病人体重
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="patient_id"></param>
        /// <returns></returns>
        public int UpdatePatMasterIndex(string weight, string patient_id, BaseEntityer db)
        {
            string sql = @"update pat_master_index
                          set weight='{0}'
                        where patient_id='{1}'";
            object[] param = new object[]
            {
               weight,patient_id
            };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 查询所有诊断信息
        /// </summary>
        /// <returns></returns>
        public IList<HisCommon.DataEntity.DIAGNOSIS_DICT> QueryDiagnose()
        {
            string sql = @"SELECT DIAGNOSIS_DICT.INPUT_CODE,
         DIAGNOSIS_DICT.DIAGNOSIS_NAME,
         DIAGNOSIS_DICT.DIAGNOSIS_CODE
         FROM DIAGNOSIS_DICT
         ORDER BY DIAGNOSIS_DICT.INPUT_CODE ASC";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            return DataSetToEntity.DataSetToT<DIAGNOSIS_DICT>(ds);

        }

        /// <summary>
        /// 查询所有诊断信息
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable QueryDtDiagnose(string chargeTypeCode)
        {

            //分离字符串

            string[] param = chargeTypeCode.Split(new char[1] { '|' });
            string sql = "";
            //2013-7-3 by li 医生站医保诊断体现出来一类、二类
            string chargeTypecode = param[0];
            //if (chargeTypecode != "7")
            //    chargeTypecode = "2";

            if (chargeTypecode == "2")
            {
                //                if ((param[1] == "11") || (param[1] == "15") || (param[1] == "27")) //普通门诊、健康体检、定点机构急诊,适用范围为：0
                //                {
                //                    sql = @"select t.diagnose_code,
                //       t.diagnose_name,
                //       t.diagnose_type as 诊断类别,
                //       t.diagnose_spell,
                //       t.diagnose_identification
                // from si_sydiagnose t
                // where  t.range='0' and  (t.charge_type_code is null or t.charge_type_code='{0}')  ";
                //                }
                //                else if (param[1] == "41") //生育门诊 //编码为：Z01.403的
                //                {
                //                    sql = @"select t.diagnose_code,
                //       t.diagnose_name,
                //       t.diagnose_type as 诊断类别,
                //       t.diagnose_spell,
                //      '20106' as  diagnose_identification
                // from si_sydiagnose t
                // where   t.diagnose_code='Z01.403' and  t.charge_type_code='{0}'   ";
                //                }
                //                else if (param[1] == "43") //节育门诊
                //                {
                //                    sql = @"select t.diagnose_code,
                //       t.diagnose_name,
                //       t.diagnose_type as 诊断类别,
                //       t.diagnose_spell,
                //       t.diagnose_identification
                // from si_sydiagnose t
                // where  t.range in ('3','4')  and   (t.charge_type_code is null or t.charge_type_code='{0}')  ";
                //                }
                //                else if (param[1] == "21" || param[1] == "22" || param[1] == "23" || param[1] == "24" || param[1] == "25" || param[1] == "29") //普通住院、转入住院、特殊转院、家庭病床、特殊住院、急诊住院
                //                {
                //                    sql = @"select t.diagnose_code,
                //       t.diagnose_name,
                //       t.diagnose_type as 诊断类别,
                //       t.diagnose_spell,
                //       t.diagnose_identification
                // from si_sydiagnose t
                // where ( t.range='0' and  t.diagnose_type in ('1','2')) or (t.range='4') and  (t.charge_type_code is null or t.charge_type_code='{0}')  ";
                //                }
                //                else if (param[1] == "42" || param[1] == "43" || param[1] == "45")  //生育住院、节育住院、生育转入住院
                //                {
                //                    sql = @"select t.diagnose_code,
                //       t.diagnose_name,
                //       t.diagnose_type as 诊断类别,
                //       t.diagnose_spell,
                //       t.diagnose_identification
                // from si_sydiagnose t
                // where (   t.range in ('3','4')) and  (t.charge_type_code is null or t.charge_type_code='{0}')  ";
                //                }
                //                else if (param[1] == "21" || param[1] == "22" || param[1] == "23" || param[1] == "24" || param[1] == "25" || param[1] == "29")  //普通住院、转入医院、特殊转院、特殊住院、定点急诊住院
                //                {
                //                    sql = @"select t.diagnose_code,
                //       t.diagnose_name,
                //       t.diagnose_type as 诊断类别,
                //       t.diagnose_spell,
                //        decode(range,'4','10560',diagnose_identification) diagnose_identification
                // from si_sydiagnose t
                // where   ((range='0' and diagnose_type in ('1','2')) or (range='4' ))and  (t.charge_type_code is null or t.charge_type_code='{0}')  ";
                //                }

                //                else if (param[1] == "21" || param[1] == "22" || param[1] == "23" || param[1] == "24" || param[1] == "25" || param[1] == "29")  //普通住院、转入医院、特殊转院、特殊住院、定点急诊住院
                //                {
                //                    sql = @"select t.diagnose_code,
                //       t.diagnose_name,
                //       t.diagnose_type as 诊断类别,
                //       t.diagnose_spell,
                //        decode(range,'4','10560',diagnose_identification) diagnose_identification
                // from si_sydiagnose t
                // where    where (range in('3','4') )and  (t.charge_type_code is null or t.charge_type_code='{0}')  ";
                //                }

                //2013-12-31 by li 诊断排序按照code排序

                sql = @"SELECT t.diagnose_code,
       t.diagnose_name,
       t.diagnose_type AS 诊断类别,
       t.diagnose_spell ||diagnose_code diagnose_spell ,
       t.diagnose_identification,DIAGNOSE_KIND
  FROM si_sydiagnose t
 WHERE t.charge_type_code = '{0}'
   AND t.special_type IS NULL
 ORDER BY t.diagnose_code
";

            }
            //2013-12-31 by li 诊断排序按照code排序
            else //其他类别则不判断
            {
                if (chargeTypecode == "7")
                {
                    sql = @"select t.diagnose_code,
                         T.DIAGNOSE_KIND||' '||T.DIAGNOSE_TYPE||'   '||t.diagnose_name diagnose_name,
                       t.diagnose_spell||diagnose_code||T.DIAGNOSE_TYPE||T.DIAGNOSE_KIND  diagnose_spell,
                       t.diagnose_identification,DIAGNOSE_KIND,diagnose_name icd_name
                 from si_sydiagnose t 
                 where  t.charge_type_code='7'  order by t.diagnose_code "; 
                }
                else
                { 
                    sql = @"select t.diagnose_code,
                       t.diagnose_name,
                       t.diagnose_spell||diagnose_code||T.DIAGNOSE_TYPE||T.DIAGNOSE_KIND  diagnose_spell,
                       t.diagnose_identification,DIAGNOSE_KIND,diagnose_name icd_name
 from si_sydiagnose t   where  t.charge_type_code='{0}' or t.charge_type_code='7'   order by t.diagnose_code  "; 
                }
              
            }
            
            sql = sql.SqlFormate(chargeTypecode);

            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            return dt;

        }

        #endregion
        #region-----医嘱--------
        #region------查询------
        /// <summary>
        /// 查询非药品项目字典
        /// </summary>
        /// <param name="itemType">项目类别(可选)</param>
        /// <returns></returns>
        public System.Data.DataTable GetAllTreatItem(string itemType)
        {
            string sql = @"SELECT  INPUT_CODE,   
         ITEM_NAME,   
          ITEM_CODE  
    FROM  clinic_item_name_dict   
   WHERE   '{0}' is null or ITEM_CLASS = '{0}'   
ORDER BY  INPUT_CODE ASC  ";
            sql = string.Format(sql, itemType);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 查询患者所开立的治疗项目信息，包括检查、化验、治疗、手术、卫材、麻醉
        /// </summary>
        /// <param name="VisitNo">就诊序号</param>
        /// <param name="VisitDate">就诊日期</param>
        /// <returns></returns>
        public IList<OUTP_TREAT_REC> GetPatientTreatItem(string VisitNo, string VisitDate)
        {
            string sql = @"SELECT OUTP_TREAT_REC.VISIT_DATE,   
         OUTP_TREAT_REC.SERIAL_NO,   
         OUTP_TREAT_REC.ITEM_NO,   
         OUTP_TREAT_REC.ITEM_CLASS,   
         OUTP_TREAT_REC.ITEM_CODE,   
         OUTP_TREAT_REC.ITEM_NAME,   
         OUTP_TREAT_REC.UNITS,   
         OUTP_TREAT_REC.AMOUNT,   
         OUTP_TREAT_REC.PERFORMED_BY,   
         OUTP_TREAT_REC.COSTS,   
         OUTP_TREAT_REC.CHARGES,   
         OUTP_TREAT_REC.CHARGE_INDICATOR,   
         OUTP_TREAT_REC.VISIT_NO,   
         ' ' del_indicator,   
         OUTP_TREAT_REC.FREQUENCY,   
         OUTP_TREAT_REC.ITEM_SPEC,   
         OUTP_TREAT_REC.APPOINT_NO,   
         OUTP_TREAT_REC.APPOINT_ITEM_NO,   
         OUTP_TREAT_REC.SERIAL_T,   
         '' center_class,   
         OUTP_TREAT_REC.ORDER_DOCTOR  
    FROM OUTP_TREAT_REC  
   WHERE  OUTP_TREAT_REC.VISIT_DATE = to_date('{1}','yyyy-MM-dd hh24:mi:ss')  AND  
         OUTP_TREAT_REC.VISIT_NO = {0}";
            sql = string.Format(sql, VisitNo, VisitDate);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_TREAT_REC>(ds);
        }
        /// <summary>
        /// 得到检查项目字典
        /// </summary>
        /// <param name="itemType">大类别（可选）</param>
        /// <param name="itemSubType">小类别（可选）</param>
        /// <returns></returns>
        public System.Data.DataTable QueryExamItem(string itemType, string itemSubType)
        {
            string sql = @"  SELECT EXAM_RPT_PATTERN.EXAM_CLASS,   
         EXAM_RPT_PATTERN.EXAM_SUB_CLASS,   
         EXAM_RPT_PATTERN.DESC_ITEM,   
         EXAM_RPT_PATTERN.DESC_NAME,   
         EXAM_RPT_PATTERN.DESCRIPTION,   
         EXAM_RPT_PATTERN.DESCRIPTION_CODE,   
         EXAM_RPT_PATTERN.INPUT_CODE  
    FROM EXAM_RPT_PATTERN  
   WHERE ('{0}' is null or EXAM_RPT_PATTERN.EXAM_CLASS = :ls_class ) AND  
         ('{1}' is null or EXAM_RPT_PATTERN.EXAM_SUB_CLASS = :ls_subclass ) AND  
         ( EXAM_RPT_PATTERN.DESC_ITEM = '检查项目' ) ";
            sql = string.Format(sql, itemType, itemSubType);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 得到检查项目大类别字典
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable QueryExamTypeDic()
        {
            string sql = @"  SELECT EXAM_CLASS_DICT.EXAM_CLASS_CODE,   
         EXAM_CLASS_DICT.EXAM_CLASS_NAME,   
         EXAM_CLASS_DICT.SERIAL_NO  
    FROM EXAM_CLASS_DICT  ";
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 得到检查项目子类别字典
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable QueryExamSubTypeDic(string type)
        {
            string sql = @"  SELECT EXAM_SUBCLASS_DICT.EXAM_SUBCLASS_NAME,   
         EXAM_SUBCLASS_DICT.SERIAL_NO  
    FROM EXAM_SUBCLASS_DICT  
   WHERE EXAM_SUBCLASS_DICT.EXAM_CLASS_NAME = '{0}'";
            sql = string.Format(sql, type);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 得到化验室
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable QueryLabDept()
        {
            string sql = @"  SELECT DISTINCT  LAB_SHEET_MASTER.PERFORMED_BY,   
          DEPT_DICT.DEPT_NAME,
          DEPT_DICT.DEPT_CODE  
    FROM  DEPT_DICT,   
          LAB_SHEET_MASTER  
   WHERE (  DEPT_DICT.DEPT_CODE =  LAB_SHEET_MASTER.PERFORMED_BY )  ";

            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 根据科室查询化验标本字典
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable QueryLabBiaoBen(string dept)
        {
            string sql = @"SELECT SPECIMAN_DICT.SPECIMAN_NAME,   
         SPECIMAN_DICT.DEPT_CODE,   
         SPECIMAN_DICT.SERIAL_NO  
    FROM SPECIMAN_DICT  
   WHERE SPECIMAN_DICT.DEPT_CODE = '{0}'";
            sql = string.Format(sql, dept);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        ///  查询化验标本字典
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable QueryAllLabSample()
        {
            string sql = @"SELECT speciman_dict.speciman_name,
       speciman_dict.dept_code,
       speciman_dict.serial_no,
       f_trans_pinyin_capital(speciman_dict.speciman_name) AS spellcode
  FROM speciman_dict
--  WHERE SPECIMAN_DICT.DEPT_CODE = '0203'
    ";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 查询项目医保对照信息
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable QueryItemCompare(string itemClass, string itemCode)
        {
            string sql = @"SELECT HIS_COMPARE.HIS_CLASS,   
         HIS_COMPARE.HIS_NAME,   
         HIS_COMPARE.HIS_CODE,   
         HIS_COMPARE.CENTER_TYPE  
    FROM CLINIC_VS_CHARGE,   
         HIS_COMPARE  
   WHERE ( clinic_vs_charge.charge_item_class = his_compare.his_class (+)) and  
         ( clinic_vs_charge.charge_item_code = his_compare.his_code (+)) and  
         ( ( CLINIC_VS_CHARGE.CLINIC_ITEM_CLASS = '{0}' ) AND  
         ( CLINIC_VS_CHARGE.CLINIC_ITEM_CODE = '{1}' ) ) ";
            sql = string.Format(itemClass, itemCode);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 查询项目开立后对应的执行科室列表
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable QueryItemExecDepts(string itemClass, string itemCode)
        {
            string sql = @"select distinct b.dept_name as name, a.performed_by as code
  from perform_dept a, dept_dict b
 where a.item_class in
       (select charge_item_class
          from CLINIC_VS_CHARGE
         where clinic_item_code = '{1}'
           and clinic_item_class = '{0}')
   and a.item_code in
       (select charge_item_code
          from CLINIC_VS_CHARGE
         where clinic_item_code = '{1}'
           and clinic_item_class = '{0}')
   and a.performed_by = b.dept_code
 order by b.dept_name desc
";
            sql = string.Format(sql, itemClass, itemCode);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 输入检验项目用，得到检查项目列表 
        /// </summary>
        /// <returns></returns>
        public DataTable GetLabList()
        {
            string sql = @"select * from outp_lab_list";
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 医嘱列表用，检索当前药品和非药品项目，除检查检验外
        /// 新建了视图outp_order_list
        /// </summary>
        /// <param name="currentDrugDept">当前默认取药科室代码</param>
        /// <returns></returns>
        public DataTable GetDrugAndUndrugItem(List<string> depts)
        {
            string sql = @"select * from outp_order_list t  
where ((t.deptcode in ({0})) or class not in ('A', 'B')) and class!='D' and class!='C'";
            var str = "";
            foreach (var d in depts)
            {
                var temp = "'" + d + "'";
                str = str + temp + ",";
            }
            str = str.TrimEnd(',');
            sql = string.Format(sql, str);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 输入检查项目用，从视图outp_exam_list得到检查项目列表 
        /// </summary>
        /// <returns></returns>
        public IList<HisCommon.DataEntity.OUTP_SELECT_EXAM> GetExamList()
        {
            string sql = @"select * from OUTP_EXAM_LIST";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<OUTP_SELECT_EXAM>(ds);
            return list;
        }
        /// <summary>
        /// 查询某检查项目申请明细
        /// </summary>
        /// <param name="examNo">申请单号</param>
        /// <returns></returns>
        public DataTable QueryExamItemsByExamNO(string examNo)
        {
            string sql = @"select *  from EXAM_ITEMS t where t.exam_no='{0}' ";
            sql = string.Format(sql, examNo);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 查询项目是否为注射室的项目
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemClass"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DataTable QueryZSSItem(string itemCode, string itemClass, string deptCode)
        {
            string sql = @"SELECT ZSS_DICT.ITEM_CODE,   
         ZSS_DICT.ITEM_NAME,   
         ZSS_DICT.ITEM_DEPT,   
         ZSS_DICT.ITEM_CLASS  
    FROM ZSS_DICT  
   WHERE ( ZSS_DICT.ITEM_CODE = '{0}') AND  
         ( ZSS_DICT.ITEM_CLASS = '{1}' ) AND  
         ( ZSS_DICT.ITEM_DEPT = '{2}')";
            sql = string.Format(sql, itemCode, itemClass, deptCode);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 查询项目是否为化验室的项目
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemClass"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DataTable QueryHYSItem(string itemCode, string itemClass, string deptCode)
        {
            string sql = @" SELECT ZSS_DICT.ITEM_CODE,   
         ZSS_DICT.ITEM_NAME,   
         ZSS_DICT.ITEM_DEPT,   
         ZSS_DICT.ITEM_CLASS  
    FROM ZSS_DICT  
   WHERE ( ZSS_DICT.ITEM_CODE = '{0}' ) AND  
         ( ZSS_DICT.ITEM_CLASS = '{1}' ) AND  
         ( ZSS_DICT.ITEM_DEPT = '{2}' )";
            sql = string.Format(sql, itemCode, itemClass, deptCode);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 根据患者就诊序号和就诊时间查询患者所有药品和非药品医嘱
        /// </summary>
        /// <param name="visitNo"></param>
        /// <param name="visitDate"></param>
        /// <returns></returns>
        public IList<HisCommon.DataEntity.OUTP_SELECT_ORDER> QueryPatientOrders(string visitNo, string visitDate)
        {
            string sql = @"select * from outp_patient_orders t
where t.visit_no='{0}' 
and t.visit_date=to_date('{1}','yyyy-MM-dd hh24:mi:ss')  ORDER BY t.zb     ASC,
          t.oper_date DESC,
          t.item_no   DESC
";
            sql = string.Format(sql, visitNo, visitDate);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<OUTP_SELECT_ORDER>(ds);
            return list;
        }
        /// <summary>
        /// 根据检验单号查询检验单明细
        /// </summary>
        /// <param name="visitNo"></param>
        /// <param name="visitDate"></param>
        /// <returns></returns>
        public IList<HisCommon.DataEntity.LAB_TEST_ITEMS> QueryLabItemsByTestNo(string testNO)
        {
            string sql = @"select *  from LAB_TEST_ITEMS f 
                  where f.test_no='{0}'";
            sql = string.Format(sql, testNO);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<LAB_TEST_ITEMS>(ds);
            return list;
        }
        /// <summary>
        /// 查询患者检验单
        /// </summary>
        /// <param name="visitNo">就诊序号</param>
        /// <param name="visitDate">就诊日期</param>
        /// <returns></returns>
        public IList<HisCommon.DataEntity.LAB_TEST_MASTER> QueryPatientLabs(string visitNo, string visitDate)
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
left join outp_treat_rec o on o.appoint_no=t.test_no
where o.visit_no='{0}' and o.visit_date=to_date('{1}','yyyy-MM-dd')
and o.appoint_no is not null and o.item_class='{2}'
order by t.test_no asc";
            sql = string.Format(sql, visitNo, visitDate, HisCommon.Enum.ItemType.C.ToString());
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<LAB_TEST_MASTER>(ds);
            return list;
        }
        /// <summary>
        /// 查询患者在OUTP_TREAT_REC中的检验项目
        /// </summary>
        /// <param name="VisitNo">就诊序号</param>
        /// <param name="VisitDate">就诊日期</param>
        /// <param name="testNo">检验单号</param>
        /// <returns></returns>
        public IList<OUTP_TREAT_REC> QueryPatientLabInTreatRecByTestNO(string VisitNo, string VisitDate, string testNo)
        {
            //2013-12-4 by li 非同科室医生开医嘱不能删除
            string sql = @"SELECT OUTP_TREAT_REC.*,o.ORDERED_BY 
    FROM OUTP_TREAT_REC 
    left join outp_orders o on OUTP_TREAT_REC.serial_no=o.serial_no
   WHERE  OUTP_TREAT_REC.VISIT_DATE = to_date('{1}','yyyy-MM-dd hh24:mi:ss')  AND  
         OUTP_TREAT_REC.VISIT_NO = {0}
         and  OUTP_TREAT_REC.Appoint_No='{2}'";

            sql = string.Format(sql, VisitNo, VisitDate, testNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_TREAT_REC>(ds);
        }
        #region 药品库存校验
        /// <summary>
        /// 药品库存校验
        /// </summary>
        /// <param name="execDept">执行科室代码</param>
        /// <param name="drugCode">药品编码</param>
        /// <param name="drugSpec">药品规格</param>
        /// <param name="frimId">厂家</param>
        /// <param name="packageSpec">包装规格</param>
        /// <returns></returns>
        public int QueryDurgNumStock(string execDept, string drugCode, string drugSpec, string frimId, string packageSpec)
        {
            //取药品当前库存数
            string sql = @"
SELECT sum(quantity)
  FROM drug_stock
 WHERE storage = '{0}'
   And drug_code = '{1}'
   And drug_spec = '{2}'
   And firm_id = '{3}'
   And package_spec = '{4}'";
            sql = string.Format(sql, execDept, drugCode, drugSpec, frimId, packageSpec);
            object num = BaseEntityer.Db.ExecuteScalar(sql);
            if (num == null)
            {
                return 0;

            }
            if (!string.IsNullOrEmpty(num.ToString()))
                return int.Parse(num.ToString());
            return 0;
        }
        //取当天未交费药品库存数
        public int QueryWJFDurgNum(string execDept, string drugCode, string drugSpec, string frimId, string packageSpec)
        {
            //取当天未交费药品库存数  by  dong_w  修改草药的带有付数的情况
            string sql = @"
select sum(amount*fs)
  from outp_presc
 where dispensary = '{0}'
   and drug_code = '{1}'
   and drug_spec = '{2}'
   and firm_id = '{3}'
   and min_spec = '{4}'
   and charge_indicator = 0
   and TO_CHAR(operator_date, 'yyyyMMdd') = to_char(sysdate, 'yyyyMMdd')";
            sql = string.Format(sql, execDept, drugCode, drugSpec, frimId, packageSpec);
            object num = BaseEntityer.Db.ExecuteScalar(sql);
            if (!string.IsNullOrEmpty(num.ToString()))
                return int.Parse(num.ToString());
            return 0;
        }
        //add by lql 2016-09-07 start 
        //取当天未交费药品盐水库存数（不看厂家）
        public int QueryYsWJFDurgNum(string execDept, string drugCode, string drugSpec, string packageSpec)
        {
            //取当天未交费药品库存数  by  dong_w  修改草药的带有付数的情况
            string sql = @"
select sum(amount*fs)
  from outp_presc
 where dispensary = '{0}'
   and drug_code = '{1}'
   and drug_spec = '{2}'
   and min_spec = '{3}'
   and charge_indicator = 0
   and TO_CHAR(operator_date, 'yyyyMMdd') = to_char(sysdate, 'yyyyMMdd')";
            sql = string.Format(sql, execDept, drugCode, drugSpec, packageSpec);
            object num = BaseEntityer.Db.ExecuteScalar(sql);
            if (!string.IsNullOrEmpty(num.ToString()))
                return int.Parse(num.ToString());
            return 0;
        }

        /// <summary>
        ///   门诊医嘱开药品数量(不看厂家)
        /// </summary>
        /// <param name="DRUG_SPEC"></param>
        /// <param name="FIRM_ID"></param>
        /// <param name="DRUG_CODE"></param>
        /// <param name="PERFORMED_BY"></param>
        /// <param name="PACKAGE_SPEC"></param>
        /// <param name="BATCHNO"></param>
        /// <returns></returns>
        public int GetYsDrugStore(string DRUG_SPEC, string DRUG_CODE, string PERFORMED_BY, string PACKAGE_SPEC, string BATCHNO)
        {
            string sql = @"select sum(quantity) as quantity
                          from drug_presc_detail_temp a, drug_presc_master_temp b
                         where (a.presc_date = b.presc_date and a.presc_no = b.presc_no)
                           and b.dispensary = '{0}'
                           and a.drug_code = '{1}'
                           and a.drug_spec = '{2}'";
            if (!string.IsNullOrEmpty(PACKAGE_SPEC))
                sql += @" and a.package_spec = '{3}'";
            if (!string.IsNullOrEmpty(BATCHNO))
                sql += @" and a.batchno = '{4}'";
            sql = string.Format(sql, PERFORMED_BY, DRUG_CODE, DRUG_SPEC, PACKAGE_SPEC, BATCHNO);
            var quantity = BaseEntityer.Db.ExecuteScalar(sql);
            if (quantity != DBNull.Value)
                return int.Parse(quantity.ToString());
            else return 0;
        }
        //add by lql 206-09-07 end
        //取已交费待发药药品库存数
        public int QueryYJFDurgNum(string execDept, string drugCode, string drugSpec, string frimId, string packageSpec)
        {
            //取已交费待发药药品库存数
            string sql = @"
  select sum(quantity)
    from drug_presc_detail_temp a, drug_presc_master_temp b
   where (a.presc_date = b.presc_date and a.presc_no = b.presc_no)
     and b.dispensary = '{0}'
     and a.drug_code = '{1}'
     and a.drug_spec = '{2}'
     and a.firm_id = '{3}'
     and a.package_spec = '{4}'";
            sql = string.Format(sql, execDept, drugCode, drugSpec, frimId, packageSpec);
            object num = BaseEntityer.Db.ExecuteScalar(sql);
            if (!string.IsNullOrEmpty(num.ToString()))
                return int.Parse(num.ToString());
            return 0;
        }
        #endregion
        #endregion
        #region-----保存-----
        /// <summary>
        /// 保存医嘱----OUTP_ORDERS
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int SaveOrders(HisCommon.DataEntity.OUTP_ORDERS o, BaseEntityer db)
        {
            string sql = @"insert into OUTP_ORDERS
   (
    patient_id,
    visit_date,
    visit_no ,
    serial_no,
    ordered_by,
    doctor,
    order_date,
    order_txt
   )
  values
 (
   '{0}',
   to_date('{1}','yyyy-MM-dd hh24:mi:ss'),
   {2},
   '{3}',
   '{4}',
   '{5}',
   to_date('{6}','yyyy-MM-dd hh24:mi:ss'),
   '{7}')";
            object[] param = new object[] {o.PATIENT_ID, 
                                          o.VISIT_DATE,
                                          o.VISIT_NO,
                                          o.SERIAL_NO, 
                                          o.ORDERED_BY,
                                          o.DOCTOR, 
                                          o.ORDER_DATE,
                                          o.ORDER_TXT};
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除药品医嘱----OUTP_PRESC
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int DelDrugOrders(HisCommon.DataEntity.OUTP_SELECT_ORDER o, BaseEntityer db)
        {
            string sql = @"delete from OUTP_PRESC t where 
                            t.serial_no='{0}'
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
        /// 删除非药品医嘱----OUTP_TREAT_REC
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int DelUnDrugOrders(HisCommon.DataEntity.OUTP_SELECT_ORDER o, BaseEntityer db)
        {
            string sql = @"delete from OUTP_TREAT_REC t where 
                            t.serial_no='{0}'
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
        /// 保存药品医嘱----OUTP_PRESC
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int SaveDrugOrders(HisCommon.DataEntity.OUTP_PRESC o, BaseEntityer db)
        {
            //2013-6-27 by li 增加药品限制字段
            //2013-11-18 by li 门诊医生站药品医嘱增加毒麻药品取药人
            string sql = @"insert into outp_presc 
                          (
                          visit_date,
                          visit_no,
                          serial_no,
                          presc_no ,
                          item_no,
                          item_class,
                          drug_code,
                          drug_name,
                          drug_spec,
                          firm_id,
                          units,
                          amount,
                          dosage,
                          dosage_units,
                          administration ,
                          frequency ,
                          provided_indicator,
                          costs,
                          charges,
                          charge_indicator,
                          dispensary,
                          presc_class,
                          price,
                          zb,
                          fs ,
                          batchno,
                          min_spec,
                          operator_date,
                          ts,
                          dcsl,
                          ys_ts ,
                          serial_t,
                          COMMON_FLAG,
                          SPECIAL_FLAG,
                          GET_DRUG_PERSON,
                          NWARN,
                          REMARK,
                          PS)
                          values
                          (
                            to_date('{0}','yyyy-MM-dd hh24:mi:ss'),
                          {1},
                         '{2}',
                          {3},
                        {4},
                        '{5}',
                        '{6}',
                        '{7}',
                        '{8}',
                        '{9}',
                        '{10}',
                        {11},
                        '{12}',
                        '{13}',
                        '{14}',
                        '{15}',
                        {16},
                        {17},
                        {18},
                        {19},
                        '{20}',
                        {21},
                        {22},
                        {23},
                        {24},
                        '{25}',
                        '{26}',
                        to_date('{27}','yyyy-MM-dd hh24:mi:ss'),
                        {28},
                        {29},
                        {30},
                        '{31}',
                        '{32}',
                        '{33}',
                        '{34}',
                        {35},
                        '{36}',
                        '{37}'
                          )";
            object[] param = new object[]
                           {
                               o.VISIT_DATE,
                               o.VISIT_NO,
                               o.SERIAL_NO,
                               o.PRESC_NO,
                               o.ITEM_NO,
                               o.ITEM_CLASS,
                               o.DRUG_CODE,
                               o.DRUG_NAME,
                               o.DRUG_SPEC,
                               o.FIRM_ID,
                               o.UNITS,
                               o.AMOUNT,
                               o.DOSAGE,
                               o.DOSAGE_UNITS,
                               o.ADMINISTRATION,
                               o.FREQUENCY,
                               o.PROVIDED_INDICATOR,
                               o.COSTS,
                               o.CHARGES==0?"null":o.CHARGES.ToString(),
                               o.CHARGE_INDICATOR,
                               o.DISPENSARY,
                               o.PRESC_CLASS==0?"null":o.PRESC_CLASS.ToString(),
                               o.PRICE,
                               o.ZB==0?"null":o.ZB.ToString(),
                               o.FS==0?"null":o.FS.ToString(),
                               o.BATCHNO,
                               o.MIN_SPEC,
                               o.OPERATOR_DATE,
                               o.TS,
                               o.DCSL,
                               o.YS_TS==0?"null":o.YS_TS.ToString(),
                               o.SERIAL_T
                               //o.ORDER_DOCTOR
                               ,o.COMMON_FLAG
                               ,o.SPECIAL_FLAG
                               ,o.GET_DRUG_PERSON
                               ,o.NWARN
                               ,o.REMARK
                               ,o.PS
                           };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 保存非药品医嘱----OUTP_TREAT_REC
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int SaveUnDrugOrders(HisCommon.DataEntity.OUTP_TREAT_REC o, BaseEntityer db)
        {
            string sql = @"insert into OUTP_TREAT_REC
  (visit_date,
   visit_no,
   serial_no,
   item_no,
   item_class,
   item_code,
   item_name,
   item_spec,
   units,
   amount,
   frequency,
   performed_by,
   costs,
   charges,
   charge_indicator,
   appoint_no,
   appoint_item_no,
   serial_t,
   REMARK,
   PS)
values
  (to_date('{0}', 'yyyy-MM-dd'),
   {1},
   '{2}',
   {3},
   '{4}',
   '{5}',
   '{6}',
   {7},
   {8},
   {9},
   {10},
   '{11}',
   {12},
   {13},
   {14},
   {15},
   {16},
   '{17}',
   '{18}',
    '{19}')";
            object[] param = new object[] { 
                o.VISIT_DATE.ToShortDateString(),
                o.VISIT_NO,
                o.SERIAL_NO,
                o.ITEM_NO,
                o.ITEM_CLASS,
                o.ITEM_CODE,
                o.ITEM_NAME,
                string.IsNullOrEmpty(o.ITEM_SPEC)?"null":o.ITEM_SPEC,
                string.IsNullOrEmpty(o.UNITS)?"null":o.UNITS,
                o.AMOUNT,
                string.IsNullOrEmpty(o.FREQUENCY)?"null":o.FREQUENCY,
                o.PERFORMED_BY,
                o.COSTS,
                o.CHARGES==0?"null":o.CHARGES.ToString(),
                o.CHARGE_INDICATOR,
                string.IsNullOrEmpty(o.APPOINT_NO)?"null":o.APPOINT_NO,
                o.APPOINT_ITEM_NO==0?"null":o.APPOINT_ITEM_NO.ToString(),
                o.SERIAL_T,
                // o.ORDER_DOCTOR
                o.REMARK,
                o.PS
            };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        #region----检查-----
        /// <summary>
        /// 保存检查项目----EXAM_APPOINTS
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int SaveEXAM_APPOINTS(HisCommon.DataEntity.EXAM_APPOINTS o, BaseEntityer db)
        {
            //2013-11-29 by li 实际ICU开单科室记录
            string sql = @"insert into EXAM_APPOINTS
                          (exam_no,
                           patient_id,
                           visit_id,
                           local_id_class,
                           patient_local_id,
                           name,
                           name_phonetic,
                           sex,
                           date_of_birth,
                           birth_place,
                           identity,
                           charge_type,
                           mailing_address,
                           zip_code,
                           phone_number,
                           exam_class,
                           exam_sub_class,
                           clin_symp,
                           phys_sign,
                           relevant_lab_test,
                           relevant_diag,
                           clin_diag,
                           exam_mode,
                           exam_group,
                           performed_by,
                           patient_source,
                           facility,
                           req_date_time,
                           req_dept,
                           req_physician,
                           req_memo,
                           scheduled_date,
                           notice,
                           costs,
                           charges,
                           billing_indicator,
                           ICU_DEPT_CODE)
                        values
                          ('{0}',
                           '{1}',
                           {2},
                           '{3}',
                           '{4}',
                           '{5}',
                           '{6}',
                           '{7}',
                           {8},
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
                           {27},
                           '{28}',
                           '{29}',
                           '{30}',
                           {31}, 
                           '{32}',
                           {33},
                           {34},
                           {35}, 
                           '{36}')";
            object[] param = new object[] {o.EXAM_NO,
                                           o.PATIENT_ID,
                                           o.VISIT_ID==null?"null":o.VISIT_ID.ToString(),
                                          o.LOCAL_ID_CLASS, 
                                          o.PATIENT_LOCAL_ID,
                                          o.NAME, 
                                          o.NAME_PHONETIC,
                                          o.SEX,
                                          o.DATE_OF_BIRTH==null?"null":"to_date('"+o.DATE_OF_BIRTH.ToString()+"', 'yyyy-MM-dd hh24:mi:ss')",
                                           o.BIRTH_PLACE,
                                          o.IDENTITY, 
                                          o.CHARGE_TYPE,
                                          o.MAILING_ADDRESS, 
                                          o.ZIP_CODE,
                                          o.PHONE_NUMBER,
                                           o.EXAM_CLASS,
                                           o.EXAM_SUB_CLASS,
                                          o.CLIN_SYMP, 
                                          o.PHYS_SIGN,
                                          o.RELEVANT_LAB_TEST, 
                                          o.RELEVANT_DIAG,
                                          o.CLIN_DIAG,
                                           o.EXAM_MODE,
                                           o.EXAM_GROUP,
                                          o.PERFORMED_BY, 
                                          o.PATIENT_SOURCE,
                                          o.FACILITY, 
                                          o.REQ_DATE_TIME==null?"null":"to_date('"+o.REQ_DATE_TIME.ToString()+"', 'yyyy-MM-dd hh24:mi:ss')",
                                          o.REQ_DEPT,
                                           o.REQ_PHYSICIAN,
                                           o.REQ_MEMO,
                                          o.SCHEDULED_DATE==null?"null":"to_date('"+o.SCHEDULED_DATE.ToString()+"', 'yyyy-MM-dd hh24:mi:ss')",
                                          o.NOTICE,
                                          o.COSTS==null?"null":o.COSTS.ToString(), 
                                          o.CHARGES==null?"null":o.CHARGES.ToString(), 
                                          o.BILLING_INDICATOR,
                                          o.ICU_DEPT_CODE
             };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 保存检查项目明细----EXAM_ITEMS
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int SaveEXAM_ITEMS(HisCommon.DataEntity.EXAM_ITEMS o, BaseEntityer db)
        {
            string sql = @"insert into EXAM_ITEMS (exam_no, exam_item_no, exam_item, exam_item_code, costs)
values ('{0}', {1}, '{2}', '{3}', {4})";
            object[] param = new object[] {o.EXAM_NO,
                                           o.EXAM_ITEM_NO,
                                           o.EXAM_ITEM,
                                          o.EXAM_ITEM_CODE, 
                                          o.COSTS};
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除检查项目----EXAM_APPOINTS
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int DelEXAM_APPOINTS(string examNo, BaseEntityer db)
        {
            string sql = @"delete  from  EXAM_APPOINTS t where t.exam_no='{0}'";
            object[] param = new object[]
                           {
                               examNo
                           };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除检查项目----EXAM_ITEMS
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int DelEXAM_ITEMS(HisCommon.DataEntity.OUTP_SELECT_EXAM o, BaseEntityer db)
        {
            string sql = @"delete from EXAM_ITEMS t where t.exam_no='{0}' and t.exam_item_no={1}";
            object[] param = new object[]
                           {
                               o.EXAM_NO,
                               o.EXAM_ITEM_NO,
                           };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 查询患者检查单
        /// </summary>
        /// <param name="visitNo">就诊序号</param>
        /// <param name="visitDate">就诊日期</param>
        /// <returns></returns>
        public IList<HisCommon.DataEntity.OUTP_SELECT_EXAM> QueryPatientExams(string visitNo, string visitDate)
        {
            //2013-12-3 by li 门诊医生站检查医嘱开单医生增加
            //2013-12-4 by li 门诊医生站检查医嘱开单科室增加
            string sql = @"  select t.item_code as DESCRIPTION_CODE,
        t.item_name as DESCRIPTION,
        o.EXAM_CLASS,
        o.EXAM_SUB_CLASS,
        o.DESC_ITEM,
        o.INPUT_CODE,
        o.price,
        t.performed_by,
        d.dept_name,
        t.appoint_no as EXAM_NO,
        t.appoint_item_no as EXAM_ITEM_NO,
        t.amount,
        t.costs,
        t.visit_date,
        t.visit_no,
        e.patient_id,
        'M' as oper,
        t.charge_indicator,
       t.serial_no,
        t.item_no,
         o.order_date as REQ_DATE_TIME,
         o.doctor,
    o.ORDERED_BY,remark  
  from outp_treat_rec t
  left join outp_orders o on t.serial_no=o.serial_no
  left join dept_dict d on t.performed_by=d.dept_code
  left join exam_appoints e on e.exam_no=t.appoint_no
    left join outp_exam_list o on o.DESCRIPTION_CODE=t.item_code 
        and o.DESCRIPTION=t.item_name 
        and o.EXAM_SUB_CLASS=e.exam_sub_class 
        and o.EXAM_CLASS=e.exam_class
  where  t.visit_no='{0}' and t.visit_date=to_date('{1}','yyyy-MM-dd')
      and t.item_class='{2}'
      and t.appoint_no is not null ";
            sql = string.Format(sql, visitNo, visitDate, HisCommon.Enum.ItemType.D);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<OUTP_SELECT_EXAM>(ds);
            return list;
        }
        #endregion
        /// <summary>
        /// 检验项目主表----LAB_TEST_MASTER
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int SaveLabTestMaster(HisCommon.DataEntity.LAB_TEST_MASTER T, BaseEntityer db)
        {
            //2013-11-29 by li 实际ICU开单科室记录
            string sql = @"insert into LAB_TEST_MASTER
                          (test_no,
                           priority_indicator,
                           patient_id,
                           visit_id,
                           working_id,
                           execute_date,
                           name,
                           name_phonetic,
                           charge_type,
                           sex,
                           age,
                           test_cause,
                           relevant_clinic_diag,
                           specimen,
                           notes_for_spcm,
                           spcm_received_date_time,
                           spcm_sample_date_time,
                           requested_date_time,
                           ordering_dept,
                           ordering_provider,
                           performed_by,
                           result_status,
                           results_rpt_date_time,
                           transcriptionist,
                           verified_by,
                           costs,
                           charges,
                           billing_indicator,
                           print_indicator,
                           ICU_DEPT_CODE)
                        values
                          ('{0}',
                           {1},
                           '{2}',
                           {3},
                           '{4}',
                           {5},
                           '{6}',
                           '{7}',
                           '{8}',
                           '{9}',
                           {10},
                           '{11}',
                           '{12}',
                           '{13}',
                           '{14}',
                           {15},
                           {16},
                           {17},
                           '{18}',
                           '{19}',
                           '{20}',
                           '{21}',
                           {22},
                           '{23}',
                           '{24}',
                           {25},
                           {26},
                           {27},
                           {28},
                           '{29}')";
            object[] param = new object[] {T.TEST_NO,
       T.PRIORITY_INDICATOR==null?"null":T.PRIORITY_INDICATOR.ToString(),
       T.PATIENT_ID,
       T.VISIT_ID==null?"null":T.VISIT_ID.ToString(),
       T.WORKING_ID,
       T.EXECUTE_DATE ==null?"null":"to_date('"+T.EXECUTE_DATE.ToString()+"', 'yyyy-MM-dd hh24:mi:ss')",
       T.NAME,
       T.NAME_PHONETIC,
       T.CHARGE_TYPE,
       T.SEX,
       T.AGE==null?"null":T.AGE.ToString(),
       T.TEST_CAUSE,
       T.RELEVANT_CLINIC_DIAG,
       T.SPECIMEN,
       T.NOTES_FOR_SPCM,
       T.SPCM_RECEIVED_DATE_TIME==null?"null":"to_date('"+T.SPCM_RECEIVED_DATE_TIME.ToString()+"', 'yyyy-MM-dd hh24:mi:ss')",
       T.SPCM_SAMPLE_DATE_TIME==null?"null":"to_date('"+T.SPCM_SAMPLE_DATE_TIME.ToString()+"', 'yyyy-MM-dd hh24:mi:ss')",
       T.REQUESTED_DATE_TIME==null?"null":"to_date('"+T.REQUESTED_DATE_TIME.ToString()+"', 'yyyy-MM-dd hh24:mi:ss')",
       T.ORDERING_DEPT,
       T.ORDERING_PROVIDER,
       T.PERFORMED_BY,
       T.RESULT_STATUS,
       T.RESULTS_RPT_DATE_TIME==null?"null":"to_date('"+T.RESULTS_RPT_DATE_TIME.ToString()+"', 'yyyy-MM-dd hh24:mi:ss')",
       T.TRANSCRIPTIONIST,
       T.VERIFIED_BY,
       T.COSTS.ToString()=="0"?"null": T.COSTS.ToString(),
       T.CHARGES.ToString()=="0"?"null": T.CHARGES.ToString(),
       T.BILLING_INDICATOR==null?"null":T.BILLING_INDICATOR.ToString(),
       T.PRINT_INDICATOR==null?"null":T.PRINT_INDICATOR.ToString(), T.ICU_DEPT_CODE};
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 检验项目明细表-----LAB_TEST_ITEMS
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int SaveLabTestItems(HisCommon.DataEntity.LAB_TEST_ITEMS o, BaseEntityer db)
        {
            string sql = @"insert into LAB_TEST_ITEMS (test_no, item_no, item_name, item_code)
                                         values ('{0}', {1}, '{2}', '{3}')";
            object[] param = new object[] {o.TEST_NO,
                                           o.ITEM_NO,
                                           o.ITEM_NAME,
                                          o.ITEM_CODE};
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除检验项目----Lab_Test_Master
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int DelLab_Test_Master(string testNo, BaseEntityer db)
        {
            string sql = @"delete  from  lab_test_master t where t.test_no='{0}'";
            object[] param = new object[]
                           {
                               testNo
                           };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除检验项目----LAB_TEST_ITEMS
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int DelLabITEMS(string TestNo, int itemNO, BaseEntityer db)
        {
            string sql = @"delete from LAB_TEST_ITEMS t where t.test_no='{0}' and t.item_no={1}";
            object[] param = new object[]
                           {
                               TestNo,
                               itemNO,
                           };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }





        /// <summary>
        /// 保存历史处方
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int SaveOutpPrescHis(HisCommon.DataEntity.OUTP_PRESC_HIS o, BaseEntityer db)
        {
            string sql = @"insert into OUTP_PRESC_HIS
  (visit_date,
   visit_no,
   serial_no,
   patient_id,
   name,
   sex,
   age,
   identity,
   charge_type,
   doctor,
   admis_date)
values
  (to_date('{0}', 'yyyy-MM-dd'),
   {1},
   '{2}',
   '{3}',
   '{4}',
   '{5}',
   {6},
   '{7}',
   '{8}',
   '{9}',
   to_date('{10}', 'yyyy-MM-dd hh24:mi:ss'))";
            object[] param = new object[] {o.VISIT_DATE.ToShortDateString(),
                                           o.VISIT_NO,
                                           o.SERIAL_NO,
                                          o.PATIENT_ID,
                                            o.NAME,
                                           o.SEX,
                                          o.AGE,
                                          o.IDENTITY,
                                           o.CHARGE_TYPE,
                                          o.DOCTOR,
                                          o.ADMIS_DATE};
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        #endregion
        #endregion
        #region---------查询报表----------
        public List<HisCommon.DataEntity.Report.OutPatientDetail> QueryOutPatientDetail(string admisDoctor, string orderDoctor, string orderDept, string dateBegin, string dateEnd, string patientName)
        {
            string sql = @" select r.patient_id,
       b.visit_no,
       r.name,
     ( select sex from clinic_master where patient_id= r.patient_id and rownum=1) sex,
      ( select age from clinic_master where patient_id= r.patient_id and rownum=1) sex,
       r.charge_type,
       b.order_doctor as admisDoctor,
       b.rcpt_no,
       d.class_name,
       b.item_name,
       b.amount,
       b.charges,
       b.order_doctor as OrderDoctor
 from  outp_bill_items b   
left join outp_rcpt_master r on b.rcpt_no = r.rcpt_no 
left join bill_item_class_dict d on b.item_class=d.class_code

where  
  (b.order_doctor='{0}' or '{0}' is null)--开单医生
and (b.order_dept='{2}' or '{2}' is null)--开单科室
and b.visit_date>=to_date('{3}','yyyy-MM-dd hh24:mi:ss')--挂号日期
and b.visit_date<=to_date('{4}','yyyy-MM-dd hh24:mi:ss')
and (r.name='{5}' or '{5}' is null)";
            sql = sql.SqlFormate(admisDoctor, orderDoctor, orderDept, dateBegin, dateEnd, patientName);
            var ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.OutPatientDetail>(ds).ToList();
        }
        #endregion

        /// <summary>
        /// 2013-8-30 by li 门诊医生站药品耗材静配中心执行科室查询
        /// </summary>
        /// <param name="clinic_item_class">诊疗项目类别</param>
        /// <param name="clinic_item_code">诊疗项目代码</param>
        /// <returns></returns>
        public DataTable GetInfusionHallDepts(string clinic_item_class, string clinic_item_code)
        {
            //2013-8-30 by li 静配中心执行科室与价表对照应为左连接 right-》left
            string sql = @"select distinct d.dept_name as name, i.dept_code as code
                              from (select c.charge_item_class,
                                                c.charge_item_code,
                                                c.charge_item_spec,
                                                c.units
                                           from CLINIC_VS_CHARGE c
                                          where c.clinic_item_class = '{0}'
                                            and c.clinic_item_code = '{1}') o
                             left join INFUSION_HALL_DEPT_COMPARE i
                                on (i.item_class = o.charge_item_class and
                                   i.item_code = o.charge_item_code and
                                   i.item_spec = o.charge_item_spec and i.units = o.units)
                              left join dept_dict d
                                on i.dept_code = d.dept_code";
            sql = string.Format(sql, clinic_item_class, clinic_item_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 通过住院编号读取在院病人信息
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <param name="BED_NO"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.NurseINPATIENTINFO GetNurseInpatientInfoByPatientCode(string patientId)
        {
            HisCommon.DataEntity.NurseINPATIENTINFO nurseInpatientInfo = null;
            string sql = @"select 
PATIENT_ID,
NAME,
SEX,
0 AGE,
CHARGE_TYPE INP_NO,
ID_NO  BED_NO,
(select charge_type_code  from charge_type_dict where charge_type_name = pmi.charge_type and rownum = 1) CHARGE_TYPE,
name_phonetic PERSONINFO
from pat_master_index pmi 
where pmi.patient_id = '{0}' and rownum = 1";
            sql = sql.SqlFormate(patientId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                nurseInpatientInfo = DataSetToEntity.DataSetToT<HisCommon.DataEntity.NurseINPATIENTINFO>(ds)[0];
            }
            return nurseInpatientInfo;
        }

        /// <summary>
        /// 2014-3-10 by li 门诊医嘱开药品数量
        /// </summary>
        /// <param name="DRUG_SPEC"></param>
        /// <param name="FIRM_ID"></param>
        /// <param name="DRUG_CODE"></param>
        /// <param name="PERFORMED_BY"></param>
        /// <param name="PACKAGE_SPEC"></param>
        /// <param name="BATCHNO"></param>
        /// <returns></returns>
        public int GetDrugStore(string DRUG_SPEC, string FIRM_ID, string DRUG_CODE, string PERFORMED_BY, string PACKAGE_SPEC, string BATCHNO)
        {
            string sql = @"select sum(quantity) as quantity
                          from drug_presc_detail_temp a, drug_presc_master_temp b
                         where (a.presc_date = b.presc_date and a.presc_no = b.presc_no)
                           and b.dispensary = '{0}'
                           and a.drug_code = '{1}'
                           and a.firm_id = '{2}'
                           and a.drug_spec = '{3}'";
            if (!string.IsNullOrEmpty(PACKAGE_SPEC))
                sql += @" and a.package_spec = '{4}'";
            if (!string.IsNullOrEmpty(BATCHNO))
                sql += @" and a.batchno = '{5}'";
            sql = string.Format(sql, PERFORMED_BY, DRUG_CODE, FIRM_ID, DRUG_SPEC, PACKAGE_SPEC, BATCHNO);
            var quantity = BaseEntityer.Db.ExecuteScalar(sql);
            if (quantity != DBNull.Value)
                return int.Parse(quantity.ToString());
            else return 0;
        }

        /// <summary>
        /// 获得门诊医生站-开单医生
        /// </summary>
        /// <param name="VISIT_DATE"></param>
        /// <param name="VISIT_NO"></param>
        /// <param name="SERIAL_NO"></param>
        /// <returns></returns>
        public OUTP_ORDERS GetOutp_Orders(string VISIT_DATE, string VISIT_NO, string SERIAL_NO)
        {
            HisCommon.DataEntity.OUTP_ORDERS outp_orders = null;
            string sql = @"select * from OUTP_ORDERS t where t.VISIT_DATE = '{0}' and t.VISIT_NO = '{1}' and t.SERIAL_NO='{2}'";
            sql = sql.SqlFormate(VISIT_DATE, VISIT_NO, SERIAL_NO);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                outp_orders = DataSetToEntity.DataSetToT<HisCommon.DataEntity.OUTP_ORDERS>(ds)[0];
            }
            return outp_orders;
        }



        /// <summary>
        ///  根据人员类别，项目编码获得限制属性内容
        /// </summary>
        /// <param name="chargeCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemClass"></param>
        /// <returns></returns>
        public HisCommon.BringObject GetDrugSpecialLimitProperty(string chargeCode, string itemCode, string itemClass)
        {
            string sql = @"
                            SELECT nvl(h.drug_taboo,''),
                              h.drug_special_limit_flag
                              FROM his_compare h
                             WHERE h.charge_type_code = '{1}'
                             
                             AND h.his_code in (select  cv.charge_item_code from clinic_vs_charge cv where cv.clinic_item_code='{0}')
                               and h.drug_special_limit_flag IN('1','3')";
            sql = sql.SqlFormate(itemCode, chargeCode);
            System.Data.Common.DbDataReader reader = BaseEntityer.Db.ExecuteReader(sql);
            HisCommon.BringObject bringSpring = new BringObject();
            while (reader.Read())
            {
                bringSpring.Exp01 = reader[0].ToString();
                bringSpring.Exp02 = reader[1].ToString();
            }
            if (!reader.IsClosed)
                reader.Close();

            return bringSpring;
        }
    }
}
