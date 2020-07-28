using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HisCommon;
using HisCommon.DataEntity;
using HisCommon.DataEntity.Report;
using System.Data.Common;


namespace HisDBLayer
{
    public class InpatientNurse
    {
        /// <summary>
        /// 查询患者一日清单信息
        /// </summary>
        /// <param name="wardCode">护理单元号</param>
        /// <param name="beginDateTime">收费开始时间</param>
        /// <param name="endDateTime">收费截止时间</param>
        /// <returns></returns>
        public List<InPatientOneDayItems> QueryInPatientOneDayItems(string wardCode, string beginDateTime, string endDateTime, string patientID)
        {
            //2013-10-24 by li 护士站计价项目排序用增加项目编码
            string sql = @"select t.patient_id,
       t.visit_id,
       t.name,
       t.bed_no,
       t.item_name,
       t.item_class,
       t.item_code,
       t.price,
       sum(t.amount) as amount,
       t.units,
       sum(t.charges) as charges,
       sum(t.costs) as costs,
       t.now_money,
       t.prepayments,
       t.DEPT_NAME,
       t.GRIDE,t.item_spec,t.memo
  from  
               ( select t.patient_id,
               t.visit_id,
              (SELECT NAME FROM PAT_MASTER_INDEX P WHERE P.PATIENT_ID=T.PATIENT_ID) name,
               (select b.bed_label from bed_rec b where  h.bed_no=b.bed_no and h.ward_code=b.ward_code) as bed_no,
               t.item_name,
               t.item_class,
               t.item_code,
               round(t.costs / t.amount,2) as price,
               t.amount as amount,
               t.units,
               t.charges as charges,
               t.costs as costs,
               nvl(h.prepayments, 0) - nvl(h.total_charges, 0) as now_money,
               h.prepayments as prepayments,
               (select d.dept_name from dept_dict d where d.dept_code=h.DEPT_CODE) as DEPT_NAME,
               (select decode(c.FEE_ITEMGRADE,'1','甲','2','乙','3','丙','')  from his_compare c where c.charge_type_code='2' and c.his_code=t.ITEM_CODE and rownum=1) as GRIDE
              ,t.item_spec,
               t.Memo
from inp_bill_detail t ,pats_in_hospital h 
         
         where 
  H.PATIENT_ID =T.PATIENT_ID
          AND T.VISIT_ID=H.VISIT_ID AND   
( '{1}' is null or t.billing_date_time >=
               to_date('{1}', 'yyyy-MM-dd hh24:mi:ss'))
           and ('{2}' is null or t.billing_date_time <=
               to_date('{2}', 'yyyy-MM-dd hh24:mi:ss'))
           and ('{0}' is null or h.ward_code = '{0}')
           and (t.patient_id='{3}' or '{3}' is null)) t
 group by t.patient_id,
       t.visit_id,
       t.name,
       t.bed_no,
       t.item_name,
       t.item_class,
       t.item_code,
       t.price,
       t.units,
       t.now_money,
       t.prepayments,
       t.DEPT_NAME,
       t.GRIDE,
       t.item_spec,
       t.Memo";
            sql = string.Format(sql, wardCode, beginDateTime, endDateTime, patientID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.InPatientOneDayItems>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }

        /// <summary>
        /// 查询患者需要分解的医嘱信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="VisitID"></param>
        /// <param name="DecomposeDate"></param>
        /// <returns></returns>
        public List<ORDERS> QueryInPatientDecomposeOrders(string patientID, string VisitID, string DecomposeDate)
        {
            //1、长期医嘱2、临时医嘱、3停止医嘱
            string sql = @"SELECT *
                              FROM orders o
                             WHERE o.order_class NOT IN ('C', 'D')
                               AND o.billing_attr IN ('0', '3')
                               AND o.repeat_indicator = '1'
                               AND o.order_status IN ('2')
                               AND patient_id = '{0}'
                               AND visit_id = '{1}'
                               AND (o.
                                    last_decompose_date_time < to_date('{2}', 'yyyy-MM-dd hh24:mi:ss:') OR o.
                                    last_decompose_date_time IS NULL)
                            UNION ALL
                            SELECT *
                              FROM orders o
                             WHERE o.order_class NOT IN ('C', 'D')
                               AND o.billing_attr IN ('0', '3')
                               AND o.repeat_indicator = '0'
                               AND o.order_status IN ('2')
                               AND patient_id = '{0}'
                               AND visit_id = '{1}'
                               AND o. last_decompose_date_time IS NULL
                            UNION ALL
                            SELECT *
                              FROM orders o
                             WHERE o.order_class NOT IN ('C', 'D')
                               AND o.billing_attr IN ('0', '3')
                               AND o.order_status IN ('3')
                               AND patient_id = '{0}'
                               AND visit_id = '{1}'
                               AND (o.
                                    last_decompose_date_time < to_date('{2}', 'yyyy-MM-dd hh24:mi:ss:') OR o.
                                    last_decompose_date_time IS NULL)
                               AND ((o. last_decompose_date_time < o.stop_date_time) OR o.
                                    last_decompose_date_time IS NULL)
                               AND o.stop_date_time > o.start_date_time
                            ";
            sql = string.Format(sql, patientID, VisitID, DecomposeDate);
            return DataSetToEntity.DataSetToT<ORDERS>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public List<InPatientOneDayItems> QueryInPatientOutItems(string patientID, string VisitID)
        {
            string sql = @"select t.patient_id,
               t.visit_id,
               t.name,
               t.item_name,
               t.item_class,
               t.price,
               sum(t.amount) as amount,
               t.units,
               sum(t.charges) as charges,
               sum(t.costs) as costs
          from (select t.patient_id,
                       t.visit_id,
                       p.name,
                       t.item_name,
                       t.item_class,
                       round(t.costs / t.amount,2) as price,
                       t.amount as amount,
                       t.units,
                       t.charges as charges,
                       t.costs as costs
             
                  from inp_bill_detail t
                  left join pat_master_index p
                    on t.patient_id = p.patient_id
                 where  (t.patient_id='{0}' and t.visit_id='{1}')) t
         group by t.patient_id,
               t.visit_id,
               t.name,
               t.item_name,
                t.item_class,
               t.price,
               t.units";
            sql = string.Format(sql, patientID, VisitID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.InPatientOneDayItems>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 读取护理单元床位信息
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BED_REC> GetNurseBedInfo(string WARD_CODE)
        {
            string sql = @"select * from bed_rec where WARD_CODE='{0}' order by bed_no";
            sql = string.Format(sql, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BED_REC>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 得到床位列表
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BED_REC> GetBedListInfo(string WARD_CODE)
        {
            string sql = @"select b.WARD_CODE,
              b.BED_NO,
              b.BED_LABEL,
              b.ROOM_NO,
              b.DEPT_CODE,
              b.BED_APPROVED_TYPE,
              b.BED_SEX_TYPE,
              b.BED_CLASS,
              b.BED_STATUS,
              b.MEDICARE_BED,
              (select d.dept_name from dept_dict d where d.dept_code=b.dept_code) as DEPT_CODE_NAME,
              (select a.bed_approved_type_name from BED_APPROVED_TYPE_DICT a where a.bed_approved_type_code=b.BED_APPROVED_TYPE)  as BED_APPROVED_TYPE_NAME,
              (select T.BED_TYPE_NAME from BED_TYPE_DICT T where T.BED_TYPE_CODE=b.BED_SEX_TYPE) as BED_SEX_TYPE_NAME,
               (select C.BED_CLASS_NAME from BED_CLASS_DICT C where C.BED_CLASS_CODE=b.BED_CLASS) as BED_CLASS_NAME, 
               decode(b.BED_STATUS,'0','是','1','否',b.BED_STATUS) AS BED_STATUS_NAME from bed_rec b where b.WARD_CODE='{0}' order by bed_no";
            sql = string.Format(sql, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BED_REC>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }

        /// <summary>
        /// 根据科室编号,读取护理单元床位信息
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public string GetNurseBedInfoByDeptCode(string DEPT_CODE)
        {
            string sql = @"select ward_code from dept_vs_ward where DEPT_CODE='{0}'";
            sql = string.Format(sql, DEPT_CODE);
            var wardcode = BaseEntityer.Db.ExecuteScalar(sql);
            if (wardcode != null)
                return wardcode.ToString();
            else
                return null;

        }
        /// <summary>
        /// 读取护理单元的病人和床位信息。
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public DataTable GetNurseBedPatient(string WARD_CODE)
        {
            string sql = @" SELECT BED_REC.BED_NO,  -- 床号
         BED_REC.BED_LABEL,   --护理单元代码
         BED_REC.WARD_CODE,   --床标号
         PATS_IN_HOSPITAL.PATIENT_ID ,--病人ID,   
         PATS_IN_HOSPITAL.VISIT_ID ,--住院次数,   
         PAT_MASTER_INDEX.NAME ,--患者姓名,   
         PAT_MASTER_INDEX.SEX,   --性别
         PAT_MASTER_INDEX.DATE_OF_BIRTH,   --出生日期
        (to_char(PATS_IN_HOSPITAL.admission_date_time,'yyyy')-to_char(PAT_MASTER_INDEX.DATE_OF_BIRTH,'yyyy')) as Age,--年龄
         PATS_IN_HOSPITAL.ADMISSION_DATE_TIME,   --入院时间
    ( select user_name from users_staff_dict where user_id= PATS_IN_HOSPITAL.DOCTOR_IN_CHARGE) DOCTOR_IN_CHARGE,--主治医生
         PATS_IN_HOSPITAL.PATIENT_CONDITION,   --病情(详见PATIENT_STATUS_DICT)
         PATS_IN_HOSPITAL.NURSING_CLASS , --护理等级,   
         PATS_IN_HOSPITAL.DIAGNOSIS ,--诊断,   
         BED_REC.BED_STATUS ,--床状态(0:空床，1：已使用),   
         BED_REC.BED_SEX_TYPE, --床位是否限制男女,   
         BED_REC.DEPT_CODE ,--床位所在的科室, 
         BED_REC.BED_NO,
         PATS_IN_HOSPITAL.ADM_WARD_DATE_TIME,   --入科时间
         BED_REC.BED_LABEL,
       (pat_master_index.charge_type ||
       (SELECT t.clinicdept_name
           FROM siinfo t
          WHERE t.inpatient_id = pats_in_hospital.patient_id
            AND t.visit_id = pats_in_hospital.visit_id
            AND rownum = 1)) AS charge_type,--费别
          decode(PAT_MASTER_INDEX.SEX,'1','0','2','1','男','0','女','1','') bmp ,--(根据性别显示不同图片)
   decode(  BED_REC.BED_STATUS,'1',
 decode(PATS_IN_HOSPITAL.nursing_class ,
  '0','5',
  '1','1',
  '2','2',
  '3','3',
  '4','4',
 '0') 
  ,'0','0','') bak  ---（根据护理等级，显示不同的颜色图片。 5紫色(特级)；1红色(一级)；2绿色(二级)3灰色(三级)   ,
         ,nvl(nvl(pats_in_hospital.prepayments,0) - nvl(pats_in_hospital.total_charges,0)-nvl((SELECT nvl(SUM(t.charges),0)
                          FROM orders_drug_decomposer t
                         WHERE t.patient_id = PATS_IN_HOSPITAL.PATIENT_ID
                           AND t.visit_id =PATS_IN_HOSPITAL.Visit_Id
                           AND t.charge_indicator = '0'),0)
                           ,0)  ye, -- 患者余额,
   inp_no  as PatientNO --住院号
    FROM BED_REC ,---床位信息表,   
         PATS_IN_HOSPITAL ,--患者在院信息表,   
         PAT_MASTER_INDEX  --病人主索引
   WHERE ( BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE(+) ) AND  
         ( BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO(+) ) AND  
         ( PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID(+) ) AND  
         ( ( BED_REC.WARD_CODE = '{0}') )  order by   BED_REC.BED_NO";
            sql = string.Format(sql, WARD_CODE);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 读取病情字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.PATIENT_STATUS_DICT> GetStatusInfo()
        {
            string sql = @"select * from  PATIENT_STATUS_DICT";
            //sql = string.Format(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PATIENT_STATUS_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }

        /// <summary>
        /// 读取护理等级字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.HULI_COMPARE> GetHuLiCompareInfo()
        {
            string sql = @"select * from  HULI_COMPARE";
            //sql = string.Format(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.HULI_COMPARE>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 获取科室医生列表
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public DataTable GetStationDoctor(string WARD_CODE)
        {
//            string sql = @" select user_name ,user_id  from users_staff_dict a , JOB_CLASS_DICT b
//                        ,DEPT_VS_WARD C
//                        where    a.job=b.job_class_code and 
//                        A.USER_DEPT= C.DEPT_CODE AND 
//                        b.job_class_name='医生' and C.WARD_CODE='{0}'";
            string sql = @"  select user_name ,user_id  from users_staff_dict a , JOB_CLASS_DICT b 
                        where    a.job=b.job_class_code and  
                        b.job_class_name='医生' and  a.user_id in( 
                        select userid from users_group_dict u where u.group_dept  
                         in (select  dw.dept_code from DEPT_VS_WARD dw where dw.ward_code='{0}')
                        ) ";
            sql = string.Format(sql, WARD_CODE);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 获取护理单元科室信息列表
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public DataTable GetStationDept(string WARD_CODE)
        {
            string sql = @"  SELECT  DEPT_VS_WARD.DEPT_CODE , DEPT_DICT.DEPT_NAME ,      
                  DEPT_DICT.DEPT_CODE     FROM DEPT_DICT , DEPT_VS_WARD   
                    WHERE ( DEPT_DICT.DEPT_CODE = DEPT_VS_WARD.DEPT_CODE ) 
             and          ( (DEPT_VS_WARD.WARD_CODE ='{0}' ) ) ";
            sql = string.Format(sql, WARD_CODE);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 获取待入科患者列表
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public DataTable GetPreStationPerson(string WARD_CODE)
        {
            string sql = @" SELECT PAT_MASTER_INDEX.NAME,   
         PAT_MASTER_INDEX.PATIENT_ID,   
         PATS_IN_TRANSFERRING.DEPT_TRANSFERED_FROM,   
         PATS_IN_TRANSFERRING.DEPT_TRANSFERED_TO,   
         DEPT_VS_WARD.WARD_CODE  
         FROM PAT_MASTER_INDEX,   
         DEPT_VS_WARD,   
         PATS_IN_TRANSFERRING,   
         DEPT_DICT  
         WHERE ( dept_vs_ward.dept_code (+) = dept_dict.dept_code) and  
         ( PAT_MASTER_INDEX.PATIENT_ID = PATS_IN_TRANSFERRING.PATIENT_ID ) and  
         ( PATS_IN_TRANSFERRING.DEPT_TRANSFERED_TO = DEPT_DICT.DEPT_CODE ) and  
         ( ( PATS_IN_TRANSFERRING.DEPT_TRANSFERED_FROM is NULL ) ) and (WARD_CODE='{0}'  or '{0}' is null) ";
            sql = sql.SqlFormate(WARD_CODE);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 获取待入科患者列表
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public DataTable GetPreNoStationPerson(string WARD_CODE)
        {
            string sql = @" SELECT PAT_MASTER_INDEX.NAME,   
         PAT_MASTER_INDEX.PATIENT_ID,   
         PATS_IN_TRANSFERRING.DEPT_TRANSFERED_FROM,   
         PATS_IN_TRANSFERRING.DEPT_TRANSFERED_TO,   
         DEPT_VS_WARD.WARD_CODE  
         FROM PAT_MASTER_INDEX,   
         DEPT_VS_WARD,   
         PATS_IN_TRANSFERRING,   
         DEPT_DICT  
         WHERE ( dept_vs_ward.dept_code (+) = dept_dict.dept_code) and  
         ( PAT_MASTER_INDEX.PATIENT_ID = PATS_IN_TRANSFERRING.PATIENT_ID ) and  
         ( PATS_IN_TRANSFERRING.DEPT_TRANSFERED_TO = DEPT_DICT.DEPT_CODE ) and  
         ( ( PATS_IN_TRANSFERRING.DEPT_TRANSFERED_FROM is NULL ) ) and (WARD_CODE<>'{0}') ";
            sql = sql.SqlFormate(WARD_CODE);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 读取未使用的床位
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BED_REC> GetNurseUnUseBedInfo(string WARD_CODE)
        {
            string sql = @"select * from bed_rec where bed_status='0' and WARD_CODE='{0}' order by bed_no";
            sql = string.Format(sql, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BED_REC>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取pat_visit
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.PAT_VISIT> GetNursePatVisitInfo(string WARD_CODE)
        {
            string sql = @"select  v.patient_id,v.visit_id,v.charge_type_code from pat_visit v ,PATS_IN_HOSPITAL  p where v.patient_id=p.patient_id and p.visit_id=v.visit_id and p.ward_code='{0}'";
            sql = string.Format(sql, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PAT_VISIT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取pat_visit
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.PAT_VISIT GetPatVisit(string patientid, string visitid)
        {
            string sql = @"select  v.patient_id,v.visit_id,v.charge_type_code from pat_visit v where v.patient_id={0} and v.visit_id={1}";
            sql = string.Format(sql, patientid, visitid);

            PAT_VISIT patvisit = new PAT_VISIT();
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                patvisit = DataSetToEntity.DataSetToT<HisCommon.DataEntity.PAT_VISIT>(ds)[0];
            }
            return patvisit;
        }

        /// <summary>
        /// 读取待入科患者信息
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <returns></returns>
        public DataTable GetPrePersonInfo(string PATIENT_ID)
        {
            string sql = @" SELECT PATS_IN_HOSPITAL.BED_NO,
           PAT_MASTER_INDEX.NAME,
           decode(PAT_MASTER_INDEX.SEX,'1','男','2','女','男','男','女','女','') as SEX,

           (to_char(pat_visit.admission_date_time,'yyyy')-to_char(PAT_MASTER_INDEX.DATE_OF_BIRTH,'yyyy')) as Age,--年龄
           --round(MONTHS_BETWEEN(SYSDATE, PAT_MASTER_INDEX.DATE_OF_BIRTH), 2) as Age,
           PATS_IN_HOSPITAL.ADMISSION_DATE_TIME,
           PATS_IN_HOSPITAL.ADM_WARD_DATE_TIME,
           PATS_IN_HOSPITAL.DIAGNOSIS,
           PATS_IN_HOSPITAL.NURSING_CLASS,
           PATS_IN_HOSPITAL.OPERATING_DATE,
           PATS_IN_HOSPITAL.DOCTOR_IN_CHARGE,
           PATS_IN_HOSPITAL.PATIENT_CONDITION,
           (PAT_VISIT.charge_type ||
       (SELECT t.clinicdept_name
           FROM siinfo t
          WHERE t.inpatient_id = pats_in_hospital.patient_id
            AND t.visit_id = pats_in_hospital.visit_id
            AND rownum = 1)) AS charge_type,
           PATS_IN_HOSPITAL.WARD_CODE,
           PATS_IN_HOSPITAL.PATIENT_ID,
           PATS_IN_HOSPITAL.VISIT_ID,
           PATS_IN_HOSPITAL.DEPT_CODE,
           PAT_MASTER_INDEX.INP_NO,
           PATS_IN_HOSPITAL.PREPAYMENTS,
           PATS_IN_HOSPITAL.TOTAL_CHARGES,
           PATS_IN_HOSPITAL.TOTAL_COSTS
        FROM PATS_IN_HOSPITAL, PAT_MASTER_INDEX, PAT_VISIT
        WHERE (PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID)
        and (PAT_MASTER_INDEX.PATIENT_ID = PAT_VISIT.PATIENT_ID)
        and (PAT_VISIT.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID)
        and ((PATS_IN_HOSPITAL.PATIENT_ID ='{0}'))";
            sql = sql.SqlFormate(PATIENT_ID);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 判断在科
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <returns></returns>
        public DataTable GetPersonDeptInfo(string PATIENT_ID)
        {
            string sql = @"SELECT PATIENT_ID,VISIT_ID,(select d.dept_name from DEPT_DICT d where d.dept_code = p.ward_code) as ward_code,
            (select b.bed_label
            from bed_rec b
            where b.ward_code = p.ward_code
            and b.bed_no = p.bed_no) as  bed_label
            FROM pats_in_hospital p
            WHERE p.Patient_id = '{0}'";
            sql = sql.SqlFormate(PATIENT_ID);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public List<HisCommon.DataEntity.PATS_IN_HOSPITAL> GetPersonInHospitalInfo(string PATIENT_ID)
        {
            string sql = @"SELECT *  FROM pats_in_hospital WHERE Patient_id = '{0}'";
            sql = sql.SqlFormate(PATIENT_ID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PATS_IN_HOSPITAL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 判断床位是否被占
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BED_REC> GetBedStateInfo(string ward_code, string bed_no)
        {
            string sql = @"  SELECT *
		FROM bed_rec
			where (ward_code ='{0}') and (bed_no = '{1}')";
            sql = sql.SqlFormate(ward_code, bed_no);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BED_REC>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 获得床位名称
        /// </summary>
        /// <param name="ward_code"></param>
        /// <param name="bed_no"></param>
        /// <returns></returns>
        public string GetBedNameByBedNOAndWard(string ward_code, string bed_no)
        {
            string sql = @"SELECT t.bed_label
    FROM bed_rec t
      where (ward_code ='{0}') and (bed_no = '{1}')";
            sql = sql.SqlFormate(ward_code, bed_no);
            return BaseEntityer.Db.ExecuteScalar<string>(sql);
        }

        /// <summary>
        /// 在科记录
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="VisitID"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.TRANSFER> GetPersonTransferInfo(string PatientId, string VisitID)
        {
            string sql = @" sELECT *   FROM transfer
	where patient_id='{0}'  AND Visit_id='{1}' AND discharge_date_time IS NULL";
            sql = sql.SqlFormate(PatientId, VisitID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.TRANSFER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 入院信息
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="VisitID"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.PAT_VISIT> GetPersonVisitInfo(string PatientId, string VisitID)
        {
            string sql = @" select * from pat_visit
	        where patient_id='{0}'  AND Visit_id='{1}'";
            sql = sql.SqlFormate(PatientId, VisitID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PAT_VISIT>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }

        /// <summary>
        /// 更新在院信息表
        /// </summary>
        /// <param name="patsIn"></param>
        public void UpdatePATS_IN_HOSPITAL(HisCommon.DataEntity.PATS_IN_HOSPITAL patsIn, BaseEntityer Db)
        {
            string sql = @"update  PATS_IN_HOSPITAL set WARD_CODE='{0}',DEPT_CODE='{1}',
           BED_NO='{2}',PATIENT_CONDITION='{3}',
            NURSING_CLASS='{4}',DOCTOR_IN_CHARGE='{5}',OPERATING_DATE=to_date('{6}', 'yyyy-MM-dd hh24:mi:ss'),ADM_WARD_DATE_TIME=to_date('{9}', 'yyyy-MM-dd hh24:mi:ss'),ADMIS=0 where PATIENT_ID='{7}' and VISIT_ID='{8}' ";
            sql = string.Format(sql, patsIn.WARD_CODE, patsIn.DEPT_CODE, patsIn.BED_NO,
                 patsIn.PATIENT_CONDITION, patsIn.NURSING_CLASS,
                 patsIn.DOCTOR_IN_CHARGE, patsIn.OPERATING_DATE,
                 patsIn.PATIENT_ID, patsIn.VISIT_ID, patsIn.ADM_WARD_DATE_TIME);
            Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新床位状态
        /// </summary>
        /// <param name="bedrec"></param>
        public void UpdateBedState(HisCommon.DataEntity.BED_REC bedrec, BaseEntityer Db)
        {
            string sql = @"update  BED_REC set BED_STATUS='{0}' where WARD_CODE='{1}' and BED_NO='{2}' ";
            sql = string.Format(sql, bedrec.BED_STATUS, bedrec.WARD_CODE, bedrec.BED_NO);
            Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 插入转科记录
        /// </summary>
        /// <param name="transfer"></param>
        public void InsertTransfer(HisCommon.DataEntity.TRANSFER transfer, BaseEntityer Db)
        {
            string sql = @"insert into TRANSFER (PATIENT_ID, VISIT_ID, DEPT_STAYED, ADMISSION_DATE_TIME, DOCTOR_IN_CHARGE)
                     values ('{0}', {1}, '{2}', to_date('{3}', 'yyyy-MM-dd hh24:mi:ss'), '{4}') ";
            sql = string.Format(sql, transfer.PATIENT_ID, transfer.VISIT_ID, transfer.DEPT_STAYED, transfer.ADMISSION_DATE_TIME, transfer.DOCTOR_IN_CHARGE);
            Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 插入RK转科记录
        /// </summary>
        /// <param name="transfer"></param>
        public void InsertRKTransfer(HisCommon.DataEntity.TRANSFER transfer, BaseEntityer Db)
        {
            string sql = @"insert into TRANSFER (PATIENT_ID, VISIT_ID, DEPT_STAYED, ADMISSION_DATE_TIME, DOCTOR_IN_CHARGE,FLAG)
                     values ('{0}', {1}, '{2}', to_date('{3}', 'yyyy-MM-dd hh24:mi:ss'), '{4}','0') ";
            sql = string.Format(sql, transfer.PATIENT_ID, transfer.VISIT_ID, transfer.DEPT_STAYED, transfer.ADMISSION_DATE_TIME, transfer.DOCTOR_IN_CHARGE);
            Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// pats_in_transferring
        /// </summary>
        /// <param name="dept"></param>
        /// <param name="PatientId"></param>
        /// <param name="Db"></param>
        public void DeletePATS_IN_TRANSFERRING(HisCommon.DataEntity.PATS_IN_TRANSFERRING transferring, BaseEntityer Db)
        {
            string sql = @"delete from  pats_in_transferring where  Patient_id='{0}' ";
            sql = string.Format(sql, transferring.PATIENT_ID);
            Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// adtLog
        /// </summary>
        /// <param name="adtLog"></param>
        /// <param name="Db"></param>
        public void InsertadtLog(HisCommon.DataEntity.ADT_LOG adtLog, BaseEntityer Db)
        {
            string sql = @"insert into adt_log (WARD_CODE, DEPT_CODE, LOG_DATE_TIME, PATIENT_ID, VISIT_ID, ACTION)
             values ('{0}', '{1}', to_date('{2}', 'yyyy-MM-dd hh24:mi:ss'), '{3}', {4}, '{5}')";
            sql = string.Format(sql, adtLog.WARD_CODE, adtLog.DEPT_CODE, adtLog.LOG_DATE_TIME, adtLog.PATIENT_ID, adtLog.VISIT_ID, adtLog.ACTION);
            Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// PAT_VISIT
        /// </summary>
        /// <param name="patVisit"></param>
        /// <param name="Db"></param>
        public void UpatePatVisit(HisCommon.DataEntity.PAT_VISIT patVisit, BaseEntityer Db)
        {
            string sql = @"update PAT_VISIT set DEPT_ADMISSION_TO='{0}',DOCTOR_IN_CHARGE='{1}', STATE='{2}' where PATIENT_ID='{3}' and VISIT_ID='{4}'";
            sql = string.Format(sql, patVisit.DEPT_ADMISSION_TO, patVisit.DOCTOR_IN_CHARGE, patVisit.STATE, patVisit.PATIENT_ID, patVisit.VISIT_ID);
            Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 读取医嘱本信息
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <param name="starDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetDoctorOrdersBook(string WARD_CODE, string starDate, string endDate)
        {
            //2013-10-1 by li 增加执行科室转抄
            //2013-11-29 by li 实际ICU开单科室记录
            //2014-1-9 by lql 住院医生站草药付数存储
            string sql = @" SELECT DOCTOR_ORDERS.ENTER_DATE_TIME,  --下达医嘱日期及时间
               PAT_MASTER_INDEX.NAME,  --患者姓名
               PATS_IN_HOSPITAL.BED_NO,  --床号
               DOCTOR_ORDERS.ORDER_TEXT,  --医嘱正文
               DOCTOR_ORDERS.DOSAGE,   --药品一次使用剂量
               DOCTOR_ORDERS.DOSAGE_UNITS, --剂量单位
               DOCTOR_ORDERS.ADMINISTRATION, --给药途径和方法
               DOCTOR_ORDERS.FREQUENCY, --执行频率描述
               DOCTOR_ORDERS.DOCTOR, --开医嘱医生
               DOCTOR_ORDERS.NURSE, --校对护士
               DOCTOR_ORDERS.DURATION,--持续时间
               DOCTOR_ORDERS.DURATION_UNITS, --持续时间单位
               DOCTOR_ORDERS.FREQ_DETAIL,--执行时间详细描述
               DOCTOR_ORDERS.ORDER_NO, --医嘱序号
               DOCTOR_ORDERS.ORDER_SUB_NO, --医嘱子序号
               DOCTOR_ORDERS.ORDER_STATUS,--医嘱状态
               DOCTOR_ORDERS.PATIENT_ID, --病人id
               DOCTOR_ORDERS.VISIT_ID,   --就诊序号
               DOCTOR_ORDERS.ORDER_CLASS, --医嘱类别
               DOCTOR_ORDERS.REPEAT_INDICATOR, --长期医嘱标志
               DOCTOR_ORDERS.START_STOP_INDICATOR, --新开停止医嘱标志
               DOCTOR_ORDERS.FREQ_INTERVAL,  --频率间隔
               DOCTOR_ORDERS.FREQ_INTERVAL_UNIT, --频率间隔单位
               DOCTOR_ORDERS.ORDERING_DEPT,   --开医嘱科室
               DOCTOR_ORDERS.ORDER_PRINT_INDICATOR, --医嘱本打印标志
               DOCTOR_ORDERS.RELATED_ORDER_NO,  --相关医嘱号
               DOCTOR_ORDERS.RELATED_ORDER_SUB_NO, --相关医嘱子号
               DOCTOR_ORDERS.ORDER_CODE,  --医嘱代码
               DOCTOR_ORDERS.PROCESSING_DATE_TIME, --处理日期及时间
               DOCTOR_ORDERS.BILLING_ATTR,  --药品计价属性1-自带药
               PAT_MASTER_INDEX.NAME_PHONETIC, --
               DOCTOR_ORDERS.START_DATE_TIME,  --医嘱开始生效时间
               DOCTOR_ORDERS.DRUG_BILLING_ATTR, --药品计价属性
               DOCTOR_ORDERS.FREQ_COUNTER, --频率次数
               BED_REC.BED_LABEL,
               DOCTOR_ORDERS.TEST_NO,
               DOCTOR_ORDERS.DRUG_SPEC,
               DOCTOR_ORDERS.COMMON_FLAG,
               DOCTOR_ORDERS.SPECIAL_FLAG,
               DOCTOR_ORDERS.PERFORMED_BY, --执行科室
               DOCTOR_ORDERS.ORDER_COSTS, --医嘱费用
               DOCTOR_ORDERS.ICU_DEPT_CODE, --ICU开单科室记录
               DOCTOR_ORDERS.FS, --住院医生站草药付数存储
               DOCTOR_ORDERS.MEMO,-- 医嘱备注
               DOCTOR_ORDERS.OPERATION_ORDER-- 术后医嘱
          FROM DOCTOR_ORDERS, PAT_MASTER_INDEX, PATS_IN_HOSPITAL, BED_REC
         WHERE (PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID)
           and (PATS_IN_HOSPITAL.PATIENT_ID = DOCTOR_ORDERS.PATIENT_ID)
           and (PATS_IN_HOSPITAL.VISIT_ID = DOCTOR_ORDERS.VISIT_ID)
           and (PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE)
           and (PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO)
           and (PATS_IN_HOSPITAL.WARD_CODE = '{0}')
           AND (DOCTOR_ORDERS.START_DATE_TIME >=
               to_date('{1}','yyyy-MM-dd hh24:mi:ss') or '{1}' is null  or  substr('{1}',1,4)='0001')
           AND (DOCTOR_ORDERS.START_DATE_TIME <
               to_date('{2}','yyyy-MM-dd hh24:mi:ss') or '{2}' is null  or  substr('{2}',1,4)='0001')
           AND DOCTOR_ORDERS.ORDER_STATUS = '1'
  order by   PATS_IN_HOSPITAL.BED_NO, DOCTOR_ORDERS.ORDER_NO,  DOCTOR_ORDERS.ORDER_SUB_NO ";
            sql = string.Format(sql, WARD_CODE, starDate, endDate);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 更新停止医嘱信息
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="Db"></param>
        /// <returns> 1 成功 1 失败 0 没有更新到数据</returns>
        public int UpateStopOrders(HisCommon.DataEntity.ORDERS orders, BaseEntityer Db)
        {
            string sql = @"UPDATE orders
                               SET stop_date_time       = to_date('{0}', 'yyyy-MM-dd hh24:mi:ss'),
                                   stop_doctor          = '{1}',
                                   stop_nurse           = '{2}',
                                   stop_order_date_time = to_date('{3}', 'yyyy-MM-dd hh24:mi:ss')
                             WHERE patient_id = '{4}'
                               AND visit_id = '{5}'
                               AND order_no = '{6}'
                               AND order_sub_no = '{7}'
                               AND stop_date_time IS NULL
                            ";
            sql = string.Format(sql, orders.STOP_DATE_TIME, orders.STOP_DOCTOR, orders.STOP_NURSE, orders.STOP_ORDER_DATE_TIME, orders.PATIENT_ID, orders.VISIT_ID, orders.ORDER_NO, orders.ORDER_SUB_NO);
            return Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新医生医嘱为停止状态
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="Db"></param>
        /// <returns>1 成功 1 失败 0 没有更新到数据 </returns>
        public int UpateStopDoctorOrders(HisCommon.DataEntity.DOCTOR_ORDERS orders, BaseEntityer Db)
        {
            string sql = @"update DOCTOR_ORDERS Set processing_date_time =to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') , order_status ='{1}' , NURSE ='{2}'  where patient_id ='{3}' and visit_id ='{4}' and Order_No ='{5}' and order_sub_no ='{6}' ";
            sql = string.Format(sql, orders.PROCESSING_DATE_TIME, orders.ORDER_STATUS, orders.NURSE, orders.PATIENT_ID, orders.VISIT_ID, orders.ORDER_NO, orders.ORDER_SUB_NO);
            return Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 读取当前患者的最大医嘱号。
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <returns></returns>
        public int GetMaxOrdersNO(string PATIENT_ID, string VISIT_ID)
        {
            int OrderNO = 0;
            string sql = @" select Max(ORDER_NO) as ORDER_NO from orders  where PATIENT_ID='{0}' and VISIT_ID='{1}'";
            sql = sql.SqlFormate(PATIENT_ID, VISIT_ID);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString().Trim() != "")
                {
                    OrderNO = int.Parse(dt.Rows[0][0].ToString());
                }
            }
            return OrderNO;
        }

        /// <summary>
        /// 护士执行时间
        /// </summary>
        /// <param name="FREQ_DESC">执行频率描述</param>
        /// <param name="ADMINISTRATION">给药途径和方法</param>
        /// <returns></returns>
        public string GetPerformSchedul(string FREQ_DESC, string ADMINISTRATION)
        {
            string PerformSchedul = "";
            string sql = @"select  *  from perform_default_schedule  where freq_desc='{0}' 
                         and(administration='{1}' or ('{1}' is null and administration is null))";
            sql = sql.SqlFormate(FREQ_DESC, ADMINISTRATION);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                PerformSchedul = dt.Rows[0]["DEFAULT_SCHEDULE"].ToString();
            }
            return PerformSchedul;
        }

        /// <summary>
        /// 插入医嘱信息。
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertOrders(HisCommon.DataEntity.ORDERS o, BaseEntityer db)
        {
            //2013-10-1 by li 医嘱转抄增加执行科室
            //2013-10-22 by li 住院医生站增加新开医嘱--单医嘱项目金额
            //2013-11-29 by li 实际ICU开单科室记录
            //2014-01-09 by LQL 住院医生站草药付数存储
            string sql = @"insert into ORDERS
              (PATIENT_ID,
               VISIT_ID,
               ORDER_NO,
               ORDER_SUB_NO,
               REPEAT_INDICATOR,
               ORDER_CLASS,
               ORDER_TEXT,
               ORDER_CODE,
               DOSAGE,
               DOSAGE_UNITS,
               ADMINISTRATION,
               DURATION,
               DURATION_UNITS,
               START_DATE_TIME,
               STOP_DATE_TIME,
               FREQUENCY,
               FREQ_COUNTER,
               FREQ_INTERVAL,
               FREQ_INTERVAL_UNIT,
               FREQ_DETAIL,
               PERFORM_SCHEDULE,
               PERFORM_RESULT,
               ORDERING_DEPT,
               DOCTOR,
               STOP_DOCTOR,
               NURSE,
               STOP_NURSE,
               ENTER_DATE_TIME,
               STOP_ORDER_DATE_TIME,
               ORDER_STATUS,
               DRUG_BILLING_ATTR,
               BILLING_ATTR,
               LAST_PERFORM_DATE_TIME,
               LAST_ACCTING_DATE_TIME,
               TEST_NO,
               DRUG_SPEC,
               COMMON_FLAG,
               SPECIAL_FLAG,
               PERFORMED_BY,
               ORDER_COSTS,
               ICU_DEPT_CODE,
               FS,
               MEMO,
               OPERATION_ORDER)
            values
              ('{0}',
               {1},
                {2},
                {3},
                {4},
                '{5}',
               '{6}',
               '{7}',
                {8},
               '{9}',
               '{10}',
                {11},
               '{12}',
              to_date('{13}', 'yyyy-MM-dd  hh24:mi:ss'),
              to_date('{14}', 'yyyy-MM-dd  hh24:mi:ss'),
               '{15}',
                {16},
                {17},
               '{18}',
               '{19}',
               '{20}',
               '{21}',
               '{22}',
               '{23}',
               '{24}',
               '{25}',
               '{26}',
               to_date('{27}', 'yyyy-MM-dd  hh24:mi:ss'),
               to_date('{28}', 'yyyy-MM-dd  hh24:mi:ss'),
               '{29}',
                {30},
                {31},
                to_date('{32}', 'yyyy-MM-dd  hh24:mi:ss'),
                to_date('{33}', 'yyyy-MM-dd  hh24:mi:ss'),
               '{34}',
               '{35}', '{36}', '{37}', '{38}', {39}, '{40}', {41},'{42}','{43}')";
            object[] obs = new object[] 
               {
               o.PATIENT_ID,
               o.VISIT_ID,
               o.ORDER_NO,
               o.ORDER_SUB_NO,
               o.REPEAT_INDICATOR,
               o.ORDER_CLASS,
               o.ORDER_TEXT,
               o.ORDER_CODE,
               o.DOSAGE,
               o.DOSAGE_UNITS,
               o.ADMINISTRATION,
               o.DURATION,
               o.DURATION_UNITS,
               o.START_DATE_TIME,
               o.STOP_DATE_TIME,
               o.FREQUENCY,
               o.FREQ_COUNTER,
               o.FREQ_INTERVAL,
               o.FREQ_INTERVAL_UNIT,
               o.FREQ_DETAIL,
               o.PERFORM_SCHEDULE,
               o.PERFORM_RESULT,
               o.ORDERING_DEPT,
               o.DOCTOR,
               o.STOP_DOCTOR,
               o.NURSE,
               o.STOP_NURSE,
               o.ENTER_DATE_TIME,
               o.STOP_ORDER_DATE_TIME,
               o.ORDER_STATUS,
               o.DRUG_BILLING_ATTR,
               o.BILLING_ATTR,
               o.LAST_PERFORM_DATE_TIME,
               o.LAST_ACCTING_DATE_TIME,
               o.TEST_NO,
               o.DRUG_SPEC,o.COMMON_FLAG,o.SPECIAL_FLAG,o.PERFORMED_BY,o.ORDER_COSTS,o.ICU_DEPT_CODE,o.FS,o.Memo,o.OPERATION_ORDER};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新医生医嘱为转抄状态
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="Db"></param>
        public int UpateNewDoctorOrders(HisCommon.DataEntity.DOCTOR_ORDERS orders, BaseEntityer Db)
        {
            string sql = @"UPDATE doctor_orders
                           SET processing_date_time = to_date('{0}', 'yyyy-MM-dd hh24:mi:ss'),
                               order_status         = '{1}',
                               nurse                = '{2}',
                               related_order_no     = '{3}',
                               related_order_sub_no = '{4}'
                         WHERE patient_id = '{5}'
                           AND visit_id = '{6}'
                           AND order_no = '{7}'
                           AND order_sub_no = '{8}'
                           AND order_status = '1'
                        ";
            sql = string.Format(sql, orders.PROCESSING_DATE_TIME,
                 orders.ORDER_STATUS,
                 orders.NURSE,
                 orders.RELATED_ORDER_NO,
                 orders.RELATED_ORDER_SUB_NO,
                 orders.PATIENT_ID,
                 orders.VISIT_ID,
                 orders.ORDER_NO,
                 orders.ORDER_SUB_NO);
            return Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 读取价表项目
        /// </summary>
        /// <param name="ordertext"></param>
        /// <param name="orderclass"></param>
        /// <param name="administration"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.CLINIC_VS_CHARGE_NAME> GetClinicVSCharge(string ordertext, string orderclass, string administration)
        {
            string sql = @" SELECT PRICE_ITEM_NAME_DICT.ITEM_NAME,   
         CLINIC_VS_CHARGE.CHARGE_ITEM_CLASS,   
         CLINIC_VS_CHARGE.CHARGE_ITEM_SPEC,   
         CLINIC_VS_CHARGE.CHARGE_ITEM_CODE,   
         CLINIC_VS_CHARGE.UNITS,   
         CLINIC_VS_CHARGE.AMOUNT  
    FROM CLINIC_VS_CHARGE,   
         CLINIC_ITEM_NAME_DICT,   
         PRICE_ITEM_NAME_DICT  
   WHERE ( CLINIC_VS_CHARGE.CLINIC_ITEM_CLASS = CLINIC_ITEM_NAME_DICT.ITEM_CLASS ) and  
         ( CLINIC_VS_CHARGE.CLINIC_ITEM_CODE = CLINIC_ITEM_NAME_DICT.ITEM_CODE ) and  
         ( CLINIC_VS_CHARGE.CHARGE_ITEM_CLASS = PRICE_ITEM_NAME_DICT.ITEM_CLASS ) and  
         ( CLINIC_VS_CHARGE.CHARGE_ITEM_CODE = PRICE_ITEM_NAME_DICT.ITEM_CODE ) and  
         (( CLINIC_ITEM_NAME_DICT.ITEM_NAME ='{0}'  AND CLINIC_ITEM_NAME_DICT.ITEM_CLASS = '{1}' )
        or (CLINIC_ITEM_NAME_DICT.ITEM_NAME = '{2}' AND CLINIC_ITEM_NAME_DICT.ITEM_CLASS = 'E' ))  AND  
         PRICE_ITEM_NAME_DICT.STD_INDICATOR = 1 ";
            sql = sql.SqlFormate(ordertext, orderclass, administration);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.CLINIC_VS_CHARGE_NAME>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 药品库存
        /// </summary>
        /// <param name="drugcode"></param>
        /// <param name="dispensary"></param>
        /// <returns></returns>
        public DataTable GetDrugStock(string drugcode, string dispensary)
        {
            string sql = @"SELECT DRUG_STOCK.DRUG_CODE,   
            DRUG_STOCK.DRUG_SPEC,   
            DRUG_STOCK.FIRM_ID  
            FROM DRUG_STOCK  
            WHERE ( DRUG_STOCK.DRUG_CODE = '{0}' ) AND  
           ( DRUG_STOCK.STORAGE = '{0}' ) AND  
           ( DRUG_STOCK.SUPPLY_INDICATOR = 1) ";
            sql = sql.SqlFormate(drugcode, dispensary);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 得到药品价表项目
        /// </summary>
        /// <param name="phamCode"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public DataTable GetDrugPriceList(string phamCode, string spec)
        {
            string sql = @"SELECT  distinct a.units,a.dose_per_unit,a.dose_units,a.DRUG_SPEC
                         FROM drug_dict a,drug_price_list b
                         WHERE a.drug_code = b.drug_code and b.drug_code ='{0}' AND a.drug_spec||b.firm_id='{1}' ";
            sql = sql.SqlFormate(phamCode, spec);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 插入医嘱明细表
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertOrdersCosts(HisCommon.DataEntity.ORDERS_COSTS o, BaseEntityer db)
        {
            string sql = @"insert into Orders_Costs
              (PATIENT_ID,
                VISIT_ID,
                ORDER_NO,
                ORDER_SUB_NO,
                ITEM_NO,
                ITEM_CLASS,
                ITEM_NAME,
                ITEM_CODE,
                ITEM_SPEC,
                UNITS,
                AMOUNT,
                TOTAL_AMOUNT,
                COSTS
                )
            values
              ('{0}',
                {1},
                {2},
                {3},
                {4},
                '{5}',
                '{6}',
                '{7}',
                '{8}',
                '{9}',
                {10},
                {11},
                {12}
               )";
            object[] obs = new object[] 
               {
                o.PATIENT_ID,
                o.VISIT_ID,
                o.ORDER_NO,
                o.ORDER_SUB_NO,
                o.ITEM_NO,
                o.ITEM_CLASS,
                o.ITEM_NAME,
                o.ITEM_CODE,
                o.ITEM_SPEC,
                o.UNITS,
                o.AMOUNT,
                o.TOTAL_AMOUNT,
                o.COSTS
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 读取患者的医嘱列表信息
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ORDERS> GetInPatientOrdersInfo(string PatientId, int VisitId)
        {
            string sql = @" SELECT *  
            FROM ORDERS  
           WHERE ( ORDERS.PATIENT_ID = '{0}' ) AND  ( ORDERS.VISIT_ID =  '{1}' )   ";
            sql = string.Format(sql, PatientId, VisitId);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 查询执行单
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="VisitId"></param>
        /// <param name="start_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ORDERS> GetInPatientExecutePrintInfo(string PatientId, int VisitId, string start_time, string end_time)
        {
            string sql = @" SELECT ORDERS.PATIENT_ID,   
             ORDERS.VISIT_ID,   
             ORDERS.ORDER_CLASS,   
             ORDERS.ORDER_TEXT,   
             ORDERS.ORDER_CODE,   
             ORDERS.DOSAGE,   
             ORDERS.DOSAGE_UNITS,   
             ORDERS.ADMINISTRATION,   
             ORDERS.FREQUENCY,   
             ORDERS.FREQ_DETAIL,   
             ORDERS.NURSE,   
             ORDERS.ORDER_STATUS,   
             ORDERS.ORDER_NO,   
             ORDERS.ORDER_SUB_NO,   
             ORDERS.PERFORM_SCHEDULE,   
             ORDERS.START_DATE_TIME,   
             ORDERS.REPEAT_INDICATOR,   
             ORDERS.BILLING_ATTR,   
             ORDERS.STOP_DATE_TIME,   
             ORDERS.DRUG_BILLING_ATTR,   
             ORDERS.FREQ_COUNTER,   
             ORDERS.FREQ_INTERVAL,   
             ORDERS.FREQ_INTERVAL_UNIT,   
             ORDERS.LAST_PERFORM_DATE_TIME,   
             ORDERS.LAST_ACCTING_DATE_TIME,   
             ORDERS.DURATION,   
             ORDERS.DURATION_UNITS,
             ORDERS.DOCTOR,
             REPEAT_INDICATOR
        FROM ORDERS  
       WHERE ( ORDERS.PATIENT_ID =  '{0}'  ) AND  
             ( ORDERS.VISIT_ID =  '{1}' ) AND  
             ( ORDERS.ORDER_CLASS in ('A','E','H') ) AND  
             ( ORDERS.ORDER_STATUS in (1,2) ) AND  
             (ORDERS.REPEAT_INDICATOR    =1)    
                 union 
               SELECT ORDERS.PATIENT_ID,   
             ORDERS.VISIT_ID,   
             ORDERS.ORDER_CLASS,   
             ORDERS.ORDER_TEXT,   
             ORDERS.ORDER_CODE,   
             ORDERS.DOSAGE,   
             ORDERS.DOSAGE_UNITS,   
             ORDERS.ADMINISTRATION,   
             ORDERS.FREQUENCY,   
             ORDERS.FREQ_DETAIL,   
             ORDERS.NURSE,   
             ORDERS.ORDER_STATUS,   
             ORDERS.ORDER_NO,   
             ORDERS.ORDER_SUB_NO,   
             ORDERS.PERFORM_SCHEDULE,   
             ORDERS.START_DATE_TIME,   
             ORDERS.REPEAT_INDICATOR,   
             ORDERS.BILLING_ATTR,   
             ORDERS.STOP_DATE_TIME,   
             ORDERS.DRUG_BILLING_ATTR,   
             ORDERS.FREQ_COUNTER,   
             ORDERS.FREQ_INTERVAL,   
             ORDERS.FREQ_INTERVAL_UNIT,   
             ORDERS.LAST_PERFORM_DATE_TIME,   
             ORDERS.LAST_ACCTING_DATE_TIME,   
             ORDERS.DURATION,   
             ORDERS.DURATION_UNITS,
             ORDERS.DOCTOR,
             REPEAT_INDICATOR  
             FROM ORDERS  
             WHERE  ( ORDERS.PATIENT_ID =  '{0}' ) AND  
             ( ORDERS.VISIT_ID = '{1}' ) AND  
             ( ORDERS.ORDER_CLASS in ('A','E','H') ) AND  
             ( ORDERS.ORDER_STATUS in (1,2) ) AND  
             (ORDERS.REPEAT_INDICATOR  =0)     and 
               start_date_time >= to_date('{2}','yyyy-MM-dd hh24:mi:ss') and  start_date_time  <=  to_date( '{3}','yyyy-MM-dd hh24:mi:ss')";
            sql = string.Format(sql, PatientId, VisitId, start_time, end_time);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 通过床号读取患者信息。
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <param name="BED_NO"></param>
        /// <returns></returns>
        public DataTable GetInpatientInfoByBed(string WARD_CODE, string BED_NO)
        {
            string sql = @" SELECT PATS_IN_HOSPITAL.BED_NO,
             PAT_MASTER_INDEX.NAME,
             --(select sex_name from sex_dict where sex_code=PAT_MASTER_INDEX.SEX ) as SEX,
              decode(PAT_MASTER_INDEX.SEX,'1','男','2','女','男','男','女','女','') as SEX,
             (to_char(PATS_IN_HOSPITAL.admission_date_time,'yyyy')-to_char(PAT_MASTER_INDEX.DATE_OF_BIRTH,'yyyy')) as Age,
             --floor(MONTHS_BETWEEN(sysdate,PAT_MASTER_INDEX.DATE_OF_BIRTH)/12) as Age,
             PATS_IN_HOSPITAL.PATIENT_ID,
             PATS_IN_HOSPITAL.DIAGNOSIS,
             PATS_IN_HOSPITAL.DOCTOR_IN_CHARGE,
             PATS_IN_HOSPITAL.VISIT_ID,
             PATS_IN_HOSPITAL.ADMISSION_DATE_TIME,
             PAT_MASTER_INDEX.INP_NO,
             PATS_IN_HOSPITAL.TOTAL_COSTS,
             PATS_IN_HOSPITAL.TOTAL_CHARGES,
             PATS_IN_HOSPITAL.PREPAYMENTS,
             '        ',
           (pat_master_index.charge_type ||
       (SELECT t.clinicdept_name
           FROM siinfo t
          WHERE t.inpatient_id = pats_in_hospital.patient_id
            AND t.visit_id = pats_in_hospital.visit_id
            AND rownum = 1)) AS charge_type
        FROM BED_REC, PATS_IN_HOSPITAL, PAT_MASTER_INDEX
       WHERE (BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE)
         and (PATS_IN_HOSPITAL.PATIENT_ID =
             PAT_MASTER_INDEX.PATIENT_ID)
         and (BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO)
         and ((BED_REC.WARD_CODE =  '{0}') and (BED_REC.BED_NO = '{1}'))";
            sql = sql.SqlFormate(WARD_CODE, BED_NO);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 读取患者的医嘱信息
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ORDERS> GetOrdersInfoByInpatient(string PATIENT_ID, string VISIT_ID)
        {
            string sql = @"SELECT *
             FROM ORDERS  
             WHERE ( ORDERS.PATIENT_ID = '{0}' ) AND  
             ( ORDERS.VISIT_ID =  '{1}' )";
            sql = sql.SqlFormate(PATIENT_ID, VISIT_ID);
            //return BaseEntityer.Db.GetDataTable(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 通过床位名称得到床位信息
        /// </summary>
        /// <param name="bed_label"></param>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BED_REC> GetBedInfoByBedLabel(string bed_label, string WARD_CODE)
        {
            string sql = @" select * from bed_rec where bed_label = '{0}' and ward_code = '{1}'";
            sql = string.Format(sql, bed_label, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BED_REC>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 更新医嘱审核
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateCheckOrders(HisCommon.DataEntity.ORDERS o, BaseEntityer db)
        {
                string sql = @" UPDATE orders
                                   SET nurse        = '{0}',
                                       order_status = '{1}'
                                 WHERE patient_id = '{2}'
                                   AND visit_id = '{3}'
                                   AND order_no = '{4}'
                                   AND order_sub_no = '{5}'
                                   AND order_status <> '4'
                                ";
            object[] obs = new object[] 
               {
                o.NURSE,
                o.ORDER_STATUS,
                o.PATIENT_ID,
                o.VISIT_ID,
                o.ORDER_NO,
                o.ORDER_SUB_NO
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 处理检验申请
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateLabCheckOrders(HisCommon.DataEntity.ORDERS o, BaseEntityer db)
        {
            DateTime addDateTime = Common.getServerTime();
            string sql = @" update lab_test_master set result_status='2',
                   EXECUTE_DATE=to_date('{3}', 'yyyy-MM-dd hh24:mi:ss'),
                   billing_indicator='1'
            WHERE patient_id = '{0}' and visit_id='{1}' and test_no='{2}'";
            object[] obs = new object[] 
               {
                o.PATIENT_ID,
                o.VISIT_ID,
                o.TEST_NO,
                addDateTime
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询医嘱明细
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ORDERS_COSTS> GetOrdersCostsByOrders(HisCommon.DataEntity.ORDERS o)
        {
            string sql = @" select * from ORDERS_COSTS WHERE PATIENT_ID = '{0}'
                            AND VISIT_ID = '{1}'
                            AND ORDER_NO = '{2}'";
            object[] obs = new object[] 
               {
                o.PATIENT_ID,
                o.VISIT_ID,
                o.ORDER_NO
               };
            sql = sql.SqlFormate(obs);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS_COSTS>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 按照医嘱号读取明细
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ORDERS_COSTS> GetOrdersCostsByOrdersSubNo(HisCommon.DataEntity.ORDERS o)
        {
            string sql = @" select * from ORDERS_COSTS WHERE PATIENT_ID = '{0}'
                            AND VISIT_ID = '{1}'
                            AND ORDER_NO = '{2}' and ORDER_SUB_NO='{3}'";
            object[] obs = new object[] 
               {
                o.PATIENT_ID,
                o.VISIT_ID,
                o.ORDER_NO,
                o.ORDER_SUB_NO
               };
            sql = sql.SqlFormate(obs);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS_COSTS>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 作废医嘱
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateCancelOrders(HisCommon.DataEntity.ORDERS o, BaseEntityer db)
        {
            string sql = @" UPDATE ORDERS
                            SET  ORDER_STATUS = '{0}'
                            WHERE PATIENT_ID = '{1}'
                            AND VISIT_ID = '{2}'
                            AND ORDER_NO = '{3}' ";
            object[] obs = new object[] 
               {
                o.ORDER_STATUS,
                o.PATIENT_ID,
                o.VISIT_ID,
                o.ORDER_NO
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 作废医生医嘱
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateCancelDoctor_Orders(HisCommon.DataEntity.ORDERS o, BaseEntityer db)
        {
            string sql = @" UPDATE Doctor_Orders
                            SET  ORDER_STATUS = '{0}'
                            WHERE PATIENT_ID = '{1}'
                            AND VISIT_ID = '{2}'
                            AND RELATED_ORDER_NO = '{3}'  ";  //转抄后的医嘱号码
            object[] obs = new object[] 
               {
                o.ORDER_STATUS,
                o.PATIENT_ID,
                o.VISIT_ID,
                o.ORDER_NO
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 作废医生医嘱
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateCancelOrdersByDoctID(BaseEntityer db,string patientID, string visitID, string orderState, string orderNO)
        {
            string sql = @" UPDATE doctor_orders
                               SET order_status = '{0}'
                             WHERE patient_id = '{1}'
                               AND visit_id = '{2}'
                               AND order_no = '{3}'
                              ";  //转抄后的医嘱号码
            object[] obs = new object[] 
               {
                
                 orderState,
                 patientID,
                 visitID,orderNO
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除检查，检验的申请信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="itemClass"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="testNO"></param>
        /// <returns></returns>
        public int DeleteExamOrLabApply(BaseEntityer db, string itemClass, string patientID, string visitID, string testNO)
        {
            string sql = string.Empty;

            if (itemClass == "C")
            {
                sql = @" /*作废医嘱，删除检验申请信息*/
                     DELETE FROM lab_test_master
                      WHERE patient_id = '{0}'
                        AND visit_id = '{1}'
                        AND test_no = '{2}'";
            }
            else if (itemClass == "D")
            {
                sql = @"  
                     /*作废医嘱，删除检查申请信息*/
                     DELETE FROM exam_appoints
                      WHERE patient_id = '{0}'
                        AND visit_id = '{1}'
                        AND exam_no = '{2}'
                    ";
            }
            else
                return 1;

            sql = string.Format(sql, patientID, visitID, testNO);
            return db.ExecuteNonQuery(sql);
        }

        public HisCommon.DataEntity.ORDERS GetOrdersInfoByKey(string PATIENT_ID, string VISIT_ID, string ORDER_NO, string ORDER_SUB_NO)
        {
            string sql = @"SELECT *
             FROM ORDERS  
             WHERE  ORDERS.PATIENT_ID = '{0}'  AND  
                    ORDERS.VISIT_ID =  '{1}'  and 
                    ORDERS.ORDER_NO =  '{2}'  and 
                    ORDERS.ORDER_SUB_NO =  '{3}'  ";
            sql = sql.SqlFormate(PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS>(BaseEntityer.Db.GetDataSet(sql))[0];

        }

        /// <summary>
        /// 读取住院科室
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.DEPT_DICT> GetInpatientDept()
        {
            string sql = @"    SELECT * 
                        FROM DEPT_DICT
                        WHERE (clinic_attr = '0')
                        and (DEPT_DICT.OUTP_OR_INP in( '1','2'))";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DEPT_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取使用的床位
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BED_REC> GetNurseUseBedInfo(string WARD_CODE)
        {
            string sql = @"select * from bed_rec where bed_status='1' and WARD_CODE='{0}' order by bed_no";
            sql = string.Format(sql, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BED_REC>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取护理单元
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BED_REC> GetAllBedInfo(string WARD_CODE)
        {
            string sql = @"select * from bed_rec where WARD_CODE='{0}' order by bed_no";
            sql = string.Format(sql, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BED_REC>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 通过床位号读取在院病人信息
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <param name="BED_NO"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.NurseINPATIENTINFO GetNurseInpatientInfoByBed(string WARD_CODE, string BED_NO)
        {
            HisCommon.DataEntity.NurseINPATIENTINFO nurseInpatientInfo = null;
            string sql = @" SELECT PATS_IN_HOSPITAL.BED_NO,
             PAT_MASTER_INDEX.NAME,
             --(select sex_name from sex_dict where sex_code=PAT_MASTER_INDEX.SEX ) as SEX,
             decode(PAT_MASTER_INDEX.SEX,'1','男','2','女','男','男','女','女','') as SEX,
             (to_char(PATS_IN_HOSPITAL.admission_date_time,'yyyy')-to_char(PAT_MASTER_INDEX.DATE_OF_BIRTH,'yyyy')) as Age,--年龄
             --floor(MONTHS_BETWEEN(sysdate,PAT_MASTER_INDEX.DATE_OF_BIRTH)/12) as Age,
             PATS_IN_HOSPITAL.PATIENT_ID,
             PATS_IN_HOSPITAL.DIAGNOSIS,
             PATS_IN_HOSPITAL.DOCTOR_IN_CHARGE,
             PATS_IN_HOSPITAL.VISIT_ID,
             PATS_IN_HOSPITAL.ADMISSION_DATE_TIME,
             PATS_IN_HOSPITAL.ADM_WARD_DATE_TIME,
             PAT_MASTER_INDEX.INP_NO,
             PATS_IN_HOSPITAL.TOTAL_COSTS,
             PATS_IN_HOSPITAL.TOTAL_CHARGES,
             PATS_IN_HOSPITAL.PREPAYMENTS, 
             (pat_master_index.charge_type ||
           (SELECT t.clinicdept_name
           FROM siinfo t
          WHERE t.inpatient_id = pats_in_hospital.patient_id
            AND t.visit_id = pats_in_hospital.visit_id
            AND rownum = 1)) AS CHARGE_TYPE,
             PAT_MASTER_INDEX.INP_NO,
             PATS_IN_HOSPITAL.PERSONINFO,
             (select d.dept_name from DEPT_DICT d where d.dept_code=BED_REC.DEPT_CODE) as DEPT_NAME,
             BED_REC.DEPT_CODE
        FROM BED_REC, PATS_IN_HOSPITAL, PAT_MASTER_INDEX
       WHERE (BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE)
         and (PATS_IN_HOSPITAL.PATIENT_ID =
             PAT_MASTER_INDEX.PATIENT_ID)
         and (BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO)
         and ((BED_REC.WARD_CODE =  '{0}') and (BED_REC.BED_NO = '{1}'))";
            sql = sql.SqlFormate(WARD_CODE, BED_NO);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                nurseInpatientInfo = DataSetToEntity.DataSetToT<HisCommon.DataEntity.NurseINPATIENTINFO>(ds)[0];
            }
            return nurseInpatientInfo;
        }

        /// <summary>
        /// 通过住院号和科室编号获取在院病人信息
        /// </summary>
        /// <param name="patientNO"></param>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.NurseINPATIENTINFO GetNurseInpatientInfoByPatientNO(string patientNO, string WARD_CODE)
        {
            HisCommon.DataEntity.NurseINPATIENTINFO nurseInpatientInfo = null;
            string sql = @" SELECT PATS_IN_HOSPITAL.BED_NO,
                                   PAT_MASTER_INDEX.NAME,
                                   decode(PAT_MASTER_INDEX.SEX,
                                          '1',
                                          '男',
                                          '2',
                                          '女',
                                          '男',
                                          '男',
                                          '女',
                                          '女',
                                          '') as SEX,
                                   (to_char(PATS_IN_HOSPITAL.admission_date_time, 'yyyy') -
                                   to_char(PAT_MASTER_INDEX.DATE_OF_BIRTH, 'yyyy')) as Age, --年龄
                                   PATS_IN_HOSPITAL.PATIENT_ID,
                                   PATS_IN_HOSPITAL.DIAGNOSIS,
                                   PATS_IN_HOSPITAL.DOCTOR_IN_CHARGE,
                                   PATS_IN_HOSPITAL.VISIT_ID,
                                   PATS_IN_HOSPITAL.ADMISSION_DATE_TIME,
                                   PATS_IN_HOSPITAL.ADM_WARD_DATE_TIME,
                                   PAT_MASTER_INDEX.INP_NO,
                                   PATS_IN_HOSPITAL.TOTAL_COSTS,
                                   PATS_IN_HOSPITAL.TOTAL_CHARGES,
                                   PATS_IN_HOSPITAL.PREPAYMENTS,
                                   PAT_MASTER_INDEX.CHARGE_TYPE,
                                   PAT_MASTER_INDEX.INP_NO,
                                   PATS_IN_HOSPITAL.PERSONINFO,
                                   (select d.dept_name
                                      from DEPT_DICT d
                                     where d.dept_code = BED_REC.DEPT_CODE) as DEPT_NAME,
                                   BED_REC.DEPT_CODE
                              FROM BED_REC, PATS_IN_HOSPITAL, PAT_MASTER_INDEX
                             WHERE (BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE)
                               and (PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID)
                               and (BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO)
                               and PATS_IN_HOSPITAL.patient_id = '{0}'
                               and BED_REC.WARD_CODE = '{1}' ";
            sql = sql.SqlFormate(patientNO, WARD_CODE);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                nurseInpatientInfo = DataSetToEntity.DataSetToT<HisCommon.DataEntity.NurseINPATIENTINFO>(ds)[0];
            }
            return nurseInpatientInfo;
        }

        /// <summary>
        /// 通过住院编号读取在院病人信息
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <param name="BED_NO"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.NurseINPATIENTINFO GetNurseInpatientInfoByPatientV(string patientId, string visitId, string WARD_CODE, string BED_NO)
        {
            HisCommon.DataEntity.NurseINPATIENTINFO nurseInpatientInfo = null;
            string sql = @"select 
pih.patient_id,
pih.visit_id,
pih.ward_code dept_code,
pv.charge_type,
(select pih.prepayments-nvl(ctd.charge_low,ctd.charge_price)-pih.total_charges
from charge_type_dict ctd where ctd.charge_type_code = pv.charge_type_code and rownum = 1) prepayments
from pats_in_hospital pih,pat_visit pv 
where pih.patient_id = pv.patient_id and pih.visit_id = pv.visit_id 
and pih.patient_id = '{0}' and pih.visit_id = '{1}' 
and pih.ward_code = '{2}' and pih.bed_no = '{3}' and rownum = 1";
            sql = sql.SqlFormate(patientId, visitId, WARD_CODE, BED_NO);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                nurseInpatientInfo = DataSetToEntity.DataSetToT<HisCommon.DataEntity.NurseINPATIENTINFO>(ds)[0];
            }
            return nurseInpatientInfo;
        }

        /// <summary>
        /// 通过PATIENT_ID读取在院病人信息
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.NurseINPATIENTINFO GetNurseInpatientInfoByPatientID(string PATIENT_ID, string VISIT_ID)
        {
            HisCommon.DataEntity.NurseINPATIENTINFO nurseInpatientInfo = null;
            string sql = @" SELECT PATS_IN_HOSPITAL.BED_NO,
             PAT_MASTER_INDEX.NAME,
             --(select sex_name from sex_dict where sex_code=PAT_MASTER_INDEX.SEX ) as SEX,
              decode(PAT_MASTER_INDEX.SEX,'1','男','2','女','男','男','女','女','') as SEX,
             (to_char(PATS_IN_HOSPITAL.admission_date_time,'yyyy')-to_char(PAT_MASTER_INDEX.DATE_OF_BIRTH,'yyyy')) as Age,
             --floor(MONTHS_BETWEEN(sysdate,PAT_MASTER_INDEX.DATE_OF_BIRTH)/12) as Age,
             PATS_IN_HOSPITAL.PATIENT_ID,
             PATS_IN_HOSPITAL.DIAGNOSIS,
             PATS_IN_HOSPITAL.DOCTOR_IN_CHARGE,
             PATS_IN_HOSPITAL.VISIT_ID,
             PATS_IN_HOSPITAL.ADMISSION_DATE_TIME,
             PATS_IN_HOSPITAL.ADM_WARD_DATE_TIME,
             PAT_MASTER_INDEX.INP_NO,
             PATS_IN_HOSPITAL.TOTAL_COSTS,
             PATS_IN_HOSPITAL.TOTAL_CHARGES,
             PATS_IN_HOSPITAL.PREPAYMENTS, 
             PAT_MASTER_INDEX.CHARGE_TYPE,
             PAT_MASTER_INDEX.INP_NO,
             PATS_IN_HOSPITAL.PERSONINFO,
             (select d.dept_name from DEPT_DICT d where d.dept_code=BED_REC.DEPT_CODE) as DEPT_NAME,
             BED_REC.DEPT_CODE
        FROM BED_REC, PATS_IN_HOSPITAL, PAT_MASTER_INDEX
       WHERE (BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE)
         and (PATS_IN_HOSPITAL.PATIENT_ID =
             PAT_MASTER_INDEX.PATIENT_ID)
         and (BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO)
         and ((PATS_IN_HOSPITAL.PATIENT_ID =  '{0}') and (PATS_IN_HOSPITAL.VISIT_ID = '{1}'))";
            sql = sql.SqlFormate(PATIENT_ID, VISIT_ID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                nurseInpatientInfo = DataSetToEntity.DataSetToT<HisCommon.DataEntity.NurseINPATIENTINFO>(ds)[0];
            }
            return nurseInpatientInfo;
        }

        /// <summary>
        /// 得到当前病人未停止的医嘱数量
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="Visit_id"></param>
        /// <returns></returns>
        public int GetOrdersNoStopCount(string PATIENT_ID, string Visit_id)
        {
            int count = 0;
            string sql = @" SELECT Count(*)
            FROM orders
            WHERE patient_id =  '{0}'
            AND Visit_id = '{1}'
            AND order_status = '2'
            AND repeat_indicator = 1
            AND stop_date_time IS NULL";
            sql = string.Format(sql, PATIENT_ID, Visit_id);
            count = int.Parse(BaseEntityer.Db.GetDataSet(sql).Tables[0].Rows[0][0].ToString());
            return count;
        }

        /// <summary>
        /// 更新停止医嘱
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="Visit_id"></param>
        /// <param name="StropDate"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateStopAllOrders(string PATIENT_ID, int Visit_id, string StropDate, BaseEntityer db)
        {
            string sql = @" update orders
                   SET stop_date_time = to_date('{2}', 'yyyy-MM-dd hh24:mi:ss'), order_status = '3'
                   WHERE patient_id =  '{0}'
                   AND Visit_id =  '{1}'
                   AND order_status ='2'
                   AND repeat_indicator = 1
                   AND stop_date_time IS NULL ";  //停止未停止的所有长医嘱
            object[] obs = new object[] 
               {
                PATIENT_ID,
                Visit_id,
                StropDate};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新停止医嘱
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="Visit_id"></param>
        /// <param name="StropDate"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateStopAllOrdersAndLastDecomDate(string PATIENT_ID, int Visit_id, string StropDate, BaseEntityer db)
        {
            string sql = @" UPDATE orders
                               SET stop_date_time           = to_date('{2}', 'yyyy-MM-dd hh24:mi:ss'),
                                   last_decompose_date_time = to_date('{2}', 'yyyy-MM-dd hh24:mi:ss'),
                                   order_status             = '3'
                             WHERE patient_id = '{0}'
                               AND visit_id = '{1}'
                               AND order_status = '2'
                               AND repeat_indicator = 1
                               AND stop_date_time IS NULL
";  //停止未停止的所有长医嘱

            string sql2 = @"UPDATE orders
   SET last_decompose_date_time = to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')
 WHERE patient_id = '{0}'
   AND visit_id = '{1}'
   AND order_status = '2'
   AND repeat_indicator = 0
   AND stop_date_time IS NULL";// 停止未执行的临时医嘱

            object[] obs = new object[] 
               {
                PATIENT_ID,
                Visit_id,
                StropDate,
                 StropDate};
            sql = sql.SqlFormate(obs);

            sql2 = string.Format(sql2, PATIENT_ID, Visit_id, StropDate);
            int rev = db.ExecuteNonQuery(sql);

            if (rev > 0)
            {
                int tempRev = db.ExecuteNonQuery(sql2);

                if (tempRev <= 0)
                    db.RollbackTransaction();
                return 1;
            }

            return -1;
        }

        /// <summary>
        /// 更新床位信息为未使用状态
        /// </summary>
        /// <param name="ward_code"></param>
        /// <param name="bed_no"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateBedInfo(string ward_code, string bed_no, BaseEntityer db)
        {
            string sql = @"  update bed_rec SET bed_status ='0' where ward_code =  '{0}' and bed_no = {1} ";  //停止所有长医嘱
            object[] obs = new object[] 
               {
                ward_code,
                bed_no};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新入科的转科信息
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="Visit_id"></param>
        /// <param name="DISCHARGE_DATE_TIME"></param>
        /// <param name="TRANSFERED_TO"></param>
        /// <param name="DEPT_STAYED"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateTransferInfo(string PATIENT_ID, int Visit_id, string DISCHARGE_DATE_TIME, string TRANSFERED_TO, string DEPT_STAYED, BaseEntityer db)
        {
            string sql = @"    
                UPDATE TRANSFER
                SET DISCHARGE_DATE_TIME = to_date('{2}', 'yyyy-MM-dd hh24:mi:ss'), DEPT_TRANSFERED_TO ='{3}'
                WHERE PATIENT_ID = '{0}'
                AND VISIT_ID = '{1}'
                AND DEPT_STAYED = '{4}' 
                AND DISCHARGE_DATE_TIME IS NULL
                AND DEPT_TRANSFERED_TO IS NULL ";  //转科记录
            object[] obs = new object[] 
               {
                PATIENT_ID,
                Visit_id,
                DISCHARGE_DATE_TIME,
                TRANSFERED_TO,
                DEPT_STAYED};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新在院患者的信息
        /// </summary>
        /// <param name="ward_code"></param>
        /// <param name="bed_no"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateOutPatInHospitalInfo(string ward_code, string bed_no, BaseEntityer db)
        {
            string sql = @"update pats_in_hospital
                        SET ward_code          = NULL,
                        bed_no             = NULL,
                        patient_condition  = '3',
                        adm_ward_date_time = null,
                        dept_code          = null,
                        doctor_in_charge   = null
                        where ward_code =  '{0}'
                        and bed_no =  {1}  ";
            object[] obs = new object[] 
               {
                ward_code,
                bed_no };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 插入转科病人信息表
        /// </summary>
        /// <param name="transferring"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertPatInTransferringInfo(HisCommon.DataEntity.PATS_IN_TRANSFERRING transferring, BaseEntityer db)
        {
            string sql = @"INSERT INTO pats_in_transferring
                        (patient_id,
                        dept_transfered_from,
                        dept_transfered_to,
                        transfer_date_time)
                        VALUES  ('{0}', '{1}','{2}',to_date('{3}', 'yyyy-MM-dd hh24:mi:ss'))";
            object[] obs = new object[] 
               {
                transferring.PATIENT_ID,
                transferring.DEPT_TRANSFERED_FROM,
                transferring.DEPT_TRANSFERED_TO,
                transferring.TRANSFER_DATE_TIME};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 读取已经转科人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetTransferInpatientInfo()
        {
            string sql = @"  SELECT PAT_MASTER_INDEX.NAME,
             PATS_IN_TRANSFERRING.PATIENT_ID,
             PATS_IN_TRANSFERRING.DEPT_TRANSFERED_FROM,
             PATS_IN_TRANSFERRING.DEPT_TRANSFERED_TO,
             PATS_IN_TRANSFERRING.TRANSFER_DATE_TIME,
             DEPT_DICT_A.DEPT_NAME as DEPT_NAME_from,
             DEPT_DICT_B.DEPT_NAME as DEPT_NAME_to
        FROM PATS_IN_TRANSFERRING,
             PAT_MASTER_INDEX,
             DEPT_DICT DEPT_DICT_A,
             DEPT_DICT DEPT_DICT_B
       WHERE (PATS_IN_TRANSFERRING.DEPT_TRANSFERED_FROM =
             DEPT_DICT_A.DEPT_CODE(+))
         and (PATS_IN_TRANSFERRING.DEPT_TRANSFERED_TO =
             DEPT_DICT_B.DEPT_CODE(+))
         and (PATS_IN_TRANSFERRING.PATIENT_ID =
             PAT_MASTER_INDEX.PATIENT_ID(+))
         and ((PATS_IN_TRANSFERRING.DEPT_TRANSFERED_FROM is not NULL))";
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 处理转科入科信息
        /// </summary>
        /// <param name="transfer"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateTransfer(HisCommon.DataEntity.TRANSFER transfer, BaseEntityer db)
        {
            string sql = @"update transfer set dept_transfered_to = '{0}'
                    WHERE patient_id =  '{1}'
                    AND discharge_date_time = (SELECT Max(discharge_date_time)
                    FROM transfer WHERE patient_id =  '{1}' AND Visit_ID =  {2})";  //处理转科的最后一条记录
            object[] obs = new object[] 
               {
                transfer.DEPT_STAYED,
                transfer.PATIENT_ID,
                transfer.VISIT_ID};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新在院患者床位
        /// </summary>
        /// <param name="PatsInHospital"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdatePatsInHospitalBedInfo(HisCommon.DataEntity.PATS_IN_HOSPITAL PatsInHospital, BaseEntityer db)
        {
            string sql = @"update pats_in_hospital SET bed_no ='{0}'  where patient_id ='{1}'";
            object[] obs = new object[] 
               {
                PatsInHospital.BED_NO,
                PatsInHospital.PATIENT_ID};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 床位编制类型字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BED_APPROVED_TYPE_DICT> Get_BED_APPROVED_TYPE_DICT()
        {
            string sql = @"select * from BED_APPROVED_TYPE_DICT";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BED_APPROVED_TYPE_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        ///床位类型字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BED_TYPE_DICT> Get_BED_TYPE_DICT()
        {
            string sql = @"select * from BED_TYPE_DICT";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BED_TYPE_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 床位等级字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BED_CLASS_DICT> Get_BED_CLASS_DICT()
        {
            string sql = @"select * from BED_CLASS_DICT";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BED_CLASS_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 插入床位信息
        /// </summary>
        /// <param name="bed_rec"></param>
        /// <returns></returns>
        public int InsertBedRec(HisCommon.DataEntity.BED_REC bed_rec, BaseEntityer db)
        {
            string sql = @" INSERT INTO BED_REC
                      (BED_NO,
                       ROOM_NO,
                       WARD_CODE,
                       DEPT_CODE,
                       BED_SEX_TYPE,
                       BED_CLASS,
                       BED_STATUS,
                       BED_APPROVED_TYPE,
                       BED_LABEL,
                       MEDICARE_BED)
                    VALUES
                      ('{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')";
            object[] obs = new object[] 
               {
                bed_rec.BED_NO,
                bed_rec.ROOM_NO,
                bed_rec.WARD_CODE,
                bed_rec.DEPT_CODE,
                bed_rec.BED_SEX_TYPE,
                bed_rec.BED_CLASS,
                bed_rec.BED_STATUS,
                bed_rec.BED_APPROVED_TYPE,
                bed_rec.BED_LABEL,
                bed_rec.MEDICARE_BED};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除床位信息
        /// </summary>
        /// <param name="bed_rec"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteBecRec(HisCommon.DataEntity.BED_REC bed_rec, BaseEntityer db)
        {
            string sql = @" delete  from  BED_REC where BED_NO={0} and WARD_CODE='{1}'";
            object[] obs = new object[] 
               {
                bed_rec.BED_NO,
                bed_rec.WARD_CODE};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 查询住院患者收费明细
        /// </summary>
        /// <param name="Patient_Id"></param>
        /// <param name="Visit_Id"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.INP_BILL_DETAIL> GetInpBillDetailList(string Patient_Id, int Visit_Id)
        {
            string sql = @" select PATIENT_ID,VISIT_ID,ITEM_NO,ITEM_CLASS,
                        ITEM_NAME,ITEM_CODE,ITEM_SPEC,AMOUNT ,UNITS,ORDERED_BY  ,
                        PERFORMED_BY  ,COSTS  ,CHARGES  ,BILLING_DATE_TIME,OPERATOR_NO  ,RCPT_NO  
                        ,UP_FLAG ,UP_TIME_DATE ,UP_OPERATOR_NO ,FORMULARYNO,DOCTOR  ,CHECKFLAG,CLASS_ON_INP_RCPT,
                        (select d.dept_name from dept_dict d where d.dept_code=ORDERED_BY)	 as Ordered_by_name,
                        (select d.dept_name from dept_dict d where d.dept_code=PERFORMED_BY)	 as Performed_by_name,
                        (select s.user_name  from users_staff_dict s where s.user_id = OPERATOR_NO)	 as Operator_no_name,
                        (select s.user_name  from users_staff_dict s where s.user_id = DOCTOR)	 as Doctor_name,
                         memo
                         from INP_BILL_DETAIL where PATIENT_ID='{0}' and VISIT_ID='{1}'";
            object[] obs = new object[] 
               {
                Patient_Id,
                Visit_Id};
            sql = sql.SqlFormate(obs);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.INP_BILL_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public List<HisCommon.DataEntity.INP_BILL_DETAIL> GetOtherDeptInpBillDetailList(string startdate, string enddata, string dept)
        {
            string sql = @" select (select p.name from pat_master_index p where  p.patient_id=PATIENT_ID and  rownum=1) as name,
                          PATIENT_ID,VISIT_ID,ITEM_NO,ITEM_CLASS,
                        ITEM_NAME,ITEM_CODE,ITEM_SPEC,AMOUNT ,UNITS,ORDERED_BY  ,
                        PERFORMED_BY  ,COSTS  ,CHARGES  ,BILLING_DATE_TIME,OPERATOR_NO  ,RCPT_NO  
                        ,UP_FLAG ,UP_TIME_DATE ,UP_OPERATOR_NO ,FORMULARYNO,DOCTOR  ,CHECKFLAG,CLASS_ON_INP_RCPT,
                        (select d.dept_name from dept_dict d where d.dept_code=ORDERED_BY)	 as Ordered_by_name,
                        (select d.dept_name from dept_dict d where d.dept_code=PERFORMED_BY)	 as Performed_by_name,
                        (select s.user_name  from users_staff_dict s where s.user_id = OPERATOR_NO)	 as Operator_no_name,
                        (select s.user_name  from users_staff_dict s where s.user_id = DOCTOR)	 as Doctor_name
                         from INP_BILL_DETAIL i where i.BILLING_DATE_TIME >
                                           to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                                       AND i.BILLING_DATE_TIME <=
                                           to_date('{1}', 'yyyy-MM-dd hh24:mi:ss') 
                         and ORDERED_BY<>'{2}' and PERFORMED_BY='{3}'";
            object[] obs = new object[] 
               {
                startdate,
                enddata,
                dept,
                dept};
            sql = sql.SqlFormate(obs);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.INP_BILL_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 查询要退费住院患者收费明细
        /// </summary>
        /// <param name="Patient_Id"></param>
        /// <param name="Visit_Id"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.INP_BILL_DETAIL> GetNoReturnInpBillDetailList(string Patient_Id, int Visit_Id)
        {
            //2013-11-29 by li 实际ICU开单科室记录
            string sql = @"select PATIENT_ID,VISIT_ID,ITEM_NO,ITEM_CLASS,
                        ITEM_NAME,ITEM_CODE,ITEM_SPEC,AMOUNT ,UNITS,ORDERED_BY  ,
                        PERFORMED_BY  ,COSTS  ,CHARGES  ,BILLING_DATE_TIME,OPERATOR_NO  ,RCPT_NO  
                        ,UP_FLAG ,UP_TIME_DATE ,UP_OPERATOR_NO ,FORMULARYNO,DOCTOR  ,CHECKFLAG,SUBJ_CODE,
                        CLASS_ON_INP_RCPT,CLASS_ON_MR,CLASS_ON_RECKONING,ORDERS_NO,
                       RETURN_NUM,RETURN_FLAG, (select d.dept_name from dept_dict d where d.dept_code=ORDERED_BY)   as Ordered_by_name,
                        (select d.dept_name from dept_dict d where d.dept_code=PERFORMED_BY)   as Performed_by_name,
                        (select s.user_name  from users_staff_dict s where s.user_id = OPERATOR_NO)   as Operator_no_name,
                        (select s.user_name  from users_staff_dict s where s.user_id = DOCTOR)   as Doctor_name,
                      COMMON_FLAG,
                      SPECIAL_FLAG,
                      PRICE,CLINIC_ITEM_CLASS,CLINIC_ITEM_CODE,OUT_NO,ICU_DEPT_CODE,MEMO,BED_NO,PAT_DEPT_CODE
                         from INP_BILL_DETAIL where (RETURN_FLAG='0' or RETURN_FLAG is null)  and ITEM_CLASS not in ('A','B') and  AMOUNT>0 and  PATIENT_ID='{0}' and VISIT_ID='{1}' order by BILLING_DATE_TIME";
            object[] obs = new object[] 
               {
                Patient_Id,
                Visit_Id};
            sql = sql.SqlFormate(obs);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.INP_BILL_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 插入护士站收费费用明细
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertInpBillDetail(HisCommon.DataEntity.INP_BILL_DETAIL detail, BaseEntityer db)
        {
            //2013-11-29 by li 实际ICU开单科室记录
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
                     CLINIC_ITEM_CLASS,
                     CLINIC_ITEM_CODE,out_no,ICU_DEPT_CODE,MEMO,BED_NO,PAT_DEPT_CODE)
                    VALUES
                      ('{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}'
                       ,'{10}','{11}','{12}',to_date('{13}', 'yyyy-MM-dd  hh24:mi:ss'),'{14}',
                      '{15}','{16}',to_date('{17}', 'yyyy-MM-dd  hh24:mi:ss'),'{18}','{19}',
                      '{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}')";
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
                detail.CLINIC_ITEM_CLASS,
                detail.CLINIC_ITEM_CODE,
                detail.OUT_NO,
                detail.ICU_DEPT_CODE,
                detail.Memo,
                detail.Bed_NO,
                detail.Pat_Dept_Code
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        public int UpateInpBillDetail(HisCommon.DataEntity.INP_BILL_DETAIL detail, BaseEntityer db)
        {
            string sql = @"update INP_BILL_DETAIL set RETURN_FLAG='1',RETURN_NUM=RETURN_NUM+{0} where  PATIENT_ID='{1}' and VISIT_ID='{2}' and ITEM_NO='{3}'  ";
            object[] obs = new object[] 
               {
                detail.RETURN_NUM,
                detail.PATIENT_ID ,
                detail.VISIT_ID ,
                detail.ITEM_NO,
                detail.Memo};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 得到最大的计费itemNO
        /// </summary>
        /// <param name="Patient_Id"></param>
        /// <param name="Visit_Id"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int GetInpBillDetailMaxItemNo(string Patient_Id, int Visit_Id, BaseEntityer db)
        {
            int itemNo = 1;
            string sql = @" select max(ITEM_NO)
                         from INP_BILL_DETAIL where PATIENT_ID='{0}' and VISIT_ID='{1}'";
            object[] obs = new object[] 
               {
                Patient_Id,
                Visit_Id};
            sql = sql.SqlFormate(obs);

            DataTable dt = db.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString().Trim() != "")
                {
                    itemNo = int.Parse(dt.Rows[0][0].ToString()) + 1;
                }
            }
            return itemNo;
        }
        /// <summary>
        /// 读取价表项目
        /// </summary>
        /// <returns></returns>
        public DataTable GetCurrentPriceList()
        {
            string sql = @"select
                        p.INPUT_CODE as 输入码,
                        p.ITEM_NAME as 项目名称,
                        p.ITEM_CODE as 项目编号,
                        c.ITEM_SPEC as 项目规格,
                        C.PREFER_PRICE AS NOWRATE,
                        c.UNITS as 项目单位,
                        c.PRICE as 项目单价,
                        c.PERFORMED_BY as 执行科室,
                        c.ITEM_CLASS  as 项目类别编码,
                        (select i.type_name from item_type i where i.type_code=c.item_class) as 项目类别名称,
                        c.PREFER_PRICE as 优惠价格,
                        c.FOREIGNER_PRICE as 外宾价格,
                      c.class_on_inp_rcpt as 住院票据类别,
                       c.class_on_outp_rcpt as 门诊票据类别,
                      c.class_on_reckoning as 会计科目类别,
                      c.subj_code as 财务核算类别,
                      c. class_on_mr as 病案首页类别
                        from current_price_list c,PRICE_ITEM_NAME_DICT p 
                        where c.item_class=p.item_class and c.item_code=p.item_code and c.ITEM_CLASS not in('A','B') ";
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CHARGE_ITEM_CLASS"></param>
        /// <param name="CHARGE_ITEM_CODE"></param>
        /// <param name="CHARGE_ITEM_SPEC"></param>
        /// <param name="UNITS"></param>
        /// <returns></returns>
        public DataTable GetCurrentPriceByOrderCosts(string CHARGE_ITEM_CLASS, string CHARGE_ITEM_CODE, string CHARGE_ITEM_SPEC)
        {
            string sql = @"
                        select c.UNITS as 项目单位,
                        c.PRICE as 项目单价,
                        c.PERFORMED_BY as 执行科室,
                        c.ITEM_CLASS  as 项目类别编码,
                        (select i.type_name from item_type i where i.type_code=c.item_class) as 项目类别名称,
                        c.item_name as 项目名称,
                        c.PREFER_PRICE as 优惠价格,
                        c.FOREIGNER_PRICE as 外宾价格,
                       c.class_on_inp_rcpt as 住院票据类别,
                       c.class_on_outp_rcpt as 门诊票据类别,
                       c.class_on_reckoning as 会计科目类别,
                       c.subj_code as 财务核算类别,
                       c. class_on_mr as 病案首页类别,
                       c.item_code as 项目编码,
                       c.item_class as 项目类别
                        from current_price_list c where c.item_class='{0}' and c.item_code='{1}' and c.item_spec='{2}'";
            sql = string.Format(sql, CHARGE_ITEM_CLASS, CHARGE_ITEM_CODE, CHARGE_ITEM_SPEC);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 读取发药药品
        /// </summary>
        /// <param name="Patient_Id"></param>
        /// <param name="Visit_Id"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.DRUG_DISPENSE_REC_NAME> GetInpDurgList(string Patient_Id, int Visit_Id, string deptCode)
        {
            string sql = @" SELECT PATIENT_ID,
                                   VISIT_ID,
                                   DRUG_CODE ITEM_CODE,
                                   (SELECT ITEM_NAME
                                      FROM PRICE_ITEM_NAME_DICT
                                     WHERE ITEM_CLASS in ('A', 'B')
                                       and std_indicator = 1
                                       and PRICE_ITEM_NAME_DICT.ITEM_CODE = DRUG_CODE
                                       and rownum = 1) as ITEM_NAME,
                                   package_spec || firm_id ITEM_SPEC,
                                   to_char(a.ORDER_NO) || to_char(a.ORDER_SUB_NO) ||
                                   to_char(a.dispensing_date_time, 'yyyymmddHH24:mi:ss') || batch_no item_no,
                                   DISPENSE_AMOUNT -
                                   (select nvl(sum(dispense_amount), 0)
                                      from DRUG_DISPENSE_REGRET_REQ
                                     where patient_id = a.Patient_Id
                                       and visit_id = a.Visit_Id
                                       and item_No = to_char(a.ORDER_NO) || to_char(a.ORDER_SUB_NO) ||
                                           to_char(a.dispensing_date_time,
                                                             'yyyymmddHH24:mi:ss') || a.batch_no
                                       and dispensary = a.DISPENSARY) amount,
                                   drug_units,
                                   PACKAGE_UNITS UNITS,
                                   package_spec drug_spec,
                                   firm_id,
                                   COSTS -
                                   (select nvl(sum(costs), 0)
                                      from DRUG_DISPENSE_REGRET_REQ
                                     where patient_id = a.Patient_Id
                                       and visit_id = a.Visit_Id
                                       and item_No = to_char(a.ORDER_NO) || to_char(a.ORDER_SUB_NO) ||
                                           to_char(a.dispensing_date_time,
                                                             'yyyymmddHH24:mi:ss') || a.batch_no
                                       and dispensary = a.DISPENSARY) costs,
                                   CHARGES - (select nvl(sum(charges), 0)
                                                from DRUG_DISPENSE_REGRET_REQ
                                               where patient_id = Patient_Id
                                                 and visit_id = Visit_Id
                                                 and item_No = Item_No
                                                 and dispensary = a.DISPENSARY) charges,
                                   DISPENSARY PERFORMED_BY,
                                   (select d.dept_name from dept_dict d where d.dept_code = DISPENSARY) as PERFORMED_BY_NAME,
                                   dispensing_date_time,
                                   batch_no,
                                   ORDER_NO,
                                   ORDER_SUB_NO,
                                   round((a.costs / a.DISPENSE_AMOUNT), 6) as price,
                                   (SELECT input_code
                                      FROM PRICE_ITEM_NAME_DICT
                                     WHERE ITEM_CLASS in ('A', 'B')
                                       and std_indicator = 1
                                       and PRICE_ITEM_NAME_DICT.ITEM_CODE = DRUG_CODE
                                       and rownum = 1) Input_code,
                                   MEMO,
                                   ORDERED_BY
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
                                            a.batch_no
                                        and dispensary = a.DISPENSARY)) > 0
                               AND ORDERED_BY = '{2}' ";
            object[] obs = new object[] 
               {
                Patient_Id,
                Visit_Id,
                deptCode};
            sql = sql.SqlFormate(obs);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DRUG_DISPENSE_REC_NAME>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取发药药品
        /// </summary>
        /// <param name="Patient_Id"></param>
        /// <param name="Visit_Id"></param>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public decimal GetInpDurgListCount(string Patient_Id, int Visit_Id, string DeptCode)
        {
            string sql = @" SELECT COUNT(1)
                              FROM drug_dispense_rec a
                             WHERE (patient_id = '{0}')
                               AND (visit_id = '{1}')
                               AND (dispense_amount -
                                   (SELECT nvl(SUM(dispense_amount), 0)
                                       FROM drug_dispense_regret_req
                                      WHERE patient_id = a.patient_id
                                        AND visit_id = a.visit_id
                                        AND item_no =
                                            to_char(a.order_no) || to_char(a.order_sub_no) ||
                                            to_char(a.dispensing_date_time, 'yyyymmddHH24:mi:ss') ||
                                            a.batch_no
                                        AND dispensary = a.dispensary)) > 0
                               AND ordered_by = '{2}'
                            ";
            object[] obs = new object[] 
               {
                Patient_Id,
                Visit_Id,
                DeptCode};
            sql = sql.SqlFormate(obs);
            return BaseEntityer.Db.ExecuteScalar<decimal>(sql);
        }

        /// <summary>
        /// 读取退药记录
        /// </summary>
        /// <param name="Patient_Id"></param>
        /// <param name="Visit_Id"></param>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.DRUG_DISPENSE_REGRET_REQ> GetInpReturnDurgList(string Patient_Id, int Visit_Id)
        {
            string sql = @"SELECT  WARD,
              DISPENSARY,
              POST_DATE_TIME,
              PATIENT_ID,
              VISIT_ID,
              DRUG_CODE,
              DRUG_SPEC,
              DRUG_UNITS,
              FIRM_ID,
              DISPENSE_AMOUNT,
              APPLICANT,
              RETAIL_PRICE,
              nvl(DRUG_DISPENSE_REGRET_REQ.REGRET, 0) regret,
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
           (SELECT  ITEM_NAME FROM PRICE_ITEM_NAME_DICT  WHERE  ITEM_CLASS in ('A', 'B') and std_indicator = 1 and PRICE_ITEM_NAME_DICT.ITEM_CODE= DRUG_CODE and rownum=1) as ITEM_NAME,
          MEMO
        FROM DRUG_DISPENSE_REGRET_REQ
       WHERE (patient_id =  '{0}')
         and (visit_id =  '{1}')";
            object[] obs = new object[] 
               {
                Patient_Id,
                Visit_Id};
            sql = sql.SqlFormate(obs);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DRUG_DISPENSE_REGRET_REQ>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 插入退药申请表
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertInpDrugDispenseRegretReq(HisCommon.DataEntity.DRUG_DISPENSE_REGRET_REQ regeretReq, BaseEntityer db)
        {
            string sql = @" INSERT INTO DRUG_DISPENSE_REGRET_REQ ( 
                        WARD ,
                        DISPENSARY ,
                        POST_DATE_TIME ,
                        PATIENT_ID ,
                        VISIT_ID ,
                        DRUG_CODE ,
                        DRUG_SPEC ,
                        DRUG_UNITS ,
                        FIRM_ID ,
                        DISPENSE_AMOUNT ,
                        APPLICANT ,
                        REGRET ,
                        RETAIL_PRICE ,
                        COSTS ,
                        CHARGES ,
                        ITEM_NO ,
                        TOTAL_AMOUNT ,
                        DISPENSING_DATE_TIME ,
                        BATCH_NO ,
                        ORDER_NO ,
                        ORDER_SUB_NO ,
                        DEPT_CODE ,
                         MEMO) VALUES ('{0}','{1}', to_date('{2}', 'yyyy-MM-dd  hh24:mi:ss'), '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',to_date('{17}', 'yyyy-MM-dd  hh24:mi:ss'),'{18}','{19}','{20}','{21}','{22}')";
            object[] obs = new object[] 
               {regeretReq.WARD ,
                regeretReq.DISPENSARY ,
                regeretReq.POST_DATE_TIME ,
                regeretReq.PATIENT_ID ,
                regeretReq.VISIT_ID ,
                regeretReq.DRUG_CODE ,
                regeretReq.DRUG_SPEC ,
                regeretReq.DRUG_UNITS ,
                regeretReq.FIRM_ID ,
                regeretReq.DISPENSE_AMOUNT ,
                regeretReq.APPLICANT ,
                regeretReq.REGRET ,
                regeretReq.RETAIL_PRICE ,
                regeretReq.COSTS ,
                regeretReq.CHARGES ,
                regeretReq.ITEM_NO ,
                regeretReq.TOTAL_AMOUNT ,
                regeretReq.DISPENSING_DATE_TIME ,
                regeretReq.BATCH_NO ,
                regeretReq.ORDER_NO ,
                regeretReq.ORDER_SUB_NO ,
                regeretReq.DEPT_CODE,regeretReq.Memo };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除申请
        /// </summary>
        /// <param name="regeretReq"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteInpDrugDispenseRegretReq(HisCommon.DataEntity.DRUG_DISPENSE_REGRET_REQ regeretReq, BaseEntityer db)
        {
            string sql = @"DELETE FROM DRUG_DISPENSE_REGRET_REQ
                WHERE WARD = '{0}'
                AND DISPENSARY = '{1}'
                AND POST_DATE_TIME = to_date('{2}', 'yyyy-MM-dd  hh24:mi:ss')
                AND PATIENT_ID = '{3}'
                AND VISIT_ID = '{4}'
                AND DRUG_CODE = '{5}'
                AND DRUG_SPEC = '{6}'
                AND FIRM_ID = '{7}'
                AND ITEM_NO = '{8}'
                AND DISPENSING_DATE_TIME = to_date('{9}', 'yyyy-MM-dd  hh24:mi:ss')";
            object[] obs = new object[] 
               {regeretReq.WARD ,
                regeretReq.DISPENSARY ,
                regeretReq.POST_DATE_TIME ,
                regeretReq.PATIENT_ID ,
                regeretReq.VISIT_ID ,
                regeretReq.DRUG_CODE ,
                regeretReq.DRUG_SPEC ,
                regeretReq.FIRM_ID ,
                regeretReq.ITEM_NO ,
                regeretReq.DISPENSING_DATE_TIME};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 读取取药科室
        /// </summary>
        /// <param name="WardCode"></param>
        /// <returns></returns>
        public DataTable GetDrugDeptCode(string WardCode)
        {
            string sql = @"select distinct (c.drug_dept_code)
                        from drug_compare_dept c, dept_vs_ward v
                        where c.dept_code = v.dept_code
                        and v.ward_code = '{0}'";
            sql = string.Format(sql, WardCode);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 删除摆药申请
        /// </summary>
        /// <param name="req"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteDrugDispenseReq(HisCommon.DataEntity.DRUG_DISPENSE_REQ req, BaseEntityer db)
        {
            string sql = @"  DELETE FROM DRUG_DISPENSE_REQ WHERE DRUG_DISPENSE_REQ.WARD = '{0}' ";
            object[] obs = new object[] { req.WARD };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 插入摆药申请
        /// </summary>
        /// <param name="req"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertDrugDispenseReq(HisCommon.DataEntity.DRUG_DISPENSE_REQ req, BaseEntityer db)
        {
            string sql = @"INSERT INTO DRUG_DISPENSE_REQ (WARD, POST_DATE_TIME, DISPENSARY)
            VALUES ('{0}', to_date('{1}', 'yyyy-MM-dd  hh24:mi:ss'), '{2}')";
            object[] obs = new object[] 
               {req.WARD ,
                req.POST_DATE_TIME ,
                req.DISPENSARY};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 读取药品在医嘱中是否存在
        /// </summary>
        /// <param name="WardCode"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        public int GetDrugCount(string WardCode, string storage)
        {
            string sql = @" select count(distinct storage)
            from drug_stock d, orders o
            where d.drug_code = o.order_code
            and ordering_dept in (select dept_code from dept_vs_ward where ward_code =  '{0}')
            and d.storage = '{1}'";
            sql = string.Format(sql, WardCode, storage);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            int i = int.Parse(dt.Rows[0][0].ToString());
            return i;
        }
        /// <summary>
        /// 查询是否有新开医嘱（待审核的）
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="Visit_id"></param>
        /// <returns></returns>
        public int GetOrdersNoExcCount(string patient_id, string Visit_id)
        {
            string sql = @" SELECT Count(*)
                            FROM orders
                            WHERE patient_id = '{0}'
                            AND Visit_id ='{1}'
                            AND (order_status = '1' OR
                            (order_status = '2' and repeat_indicator = 1 and
                            stop_date_time is not null))";
            sql = string.Format(sql, patient_id, Visit_id);
            int rev= int.Parse(BaseEntityer.Db.GetDataTable(sql).Rows[0][0].ToString());

            if (rev <= 0)
            {
                string sql1 = @" SELECT COUNT(*)
                                  FROM doctor_orders
                                 WHERE patient_id = '{0}'
                                   AND visit_id = '{1}'
                                   AND order_status = '1'";
                sql1 = string.Format(sql1, patient_id, Visit_id);
                return int.Parse(BaseEntityer.Db.GetDataTable(sql1).Rows[0][0].ToString());
            }
            else
                return rev;
        }
        /// <summary>
        /// 未转抄的医嘱
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="Visit_id"></param>
        /// <returns></returns>
        public int GetDoctorOrdersNoExcCount(string patient_id, string Visit_id)
        {
            string sql = @" SELECT Count(*)
                            FROM DOCTOR_ORDERS
                            WHERE patient_id = '{0}'
                            AND Visit_id ='{1}'
                            AND ORDER_STATUS = '1'";
            sql = string.Format(sql, patient_id, Visit_id);
            return int.Parse(BaseEntityer.Db.GetDataTable(sql).Rows[0][0].ToString());
        }
        /// <summary>
        /// 读取未检查确认的项目提示
        /// </summary>
        /// <param name="patient_id"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.EXAM_APPOINTS> GetNoExamLst(string patient_id, string Visit_id)
        {
            string sql = @" SELECT * FROM exam_appoints WHERE patient_id = '{0}' and VISIT_ID='{1}'  ";
            sql = string.Format(sql, patient_id, Visit_id);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXAM_APPOINTS>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
   
        /// <summary>
        /// 查询手术室向药房发药申请，但是药房没有发药记录
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <returns></returns>
        public IList<ORDERS_DRUG> QueryNoSendOrderDrugInfo(string patientID, string visitID)
        {
            string sql = @" SELECT t.*
                              FROM orders_drug t

                             WHERE t.patient_id = '{0}'
                               AND t.visit_id = '{1}'
                               and t.charge_indicator='1'
                            ";
            sql = string.Format(sql, patientID, visitID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS_DRUG>(BaseEntityer.Db.GetDataSet(sql));
        }

        /// <summary>
        /// 该病人还有未结帐的检验申请,请校对后再出院!
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="Visit_id"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.LAB_TEST_MASTER> GetLabTestLst(string patient_id, string Visit_id)
        {
            string sql = @" SELECT *
            FROM lab_test_master
            WHERE patient_id = '{0}' and visit_id='{1}' and  nvl(result_status,0)<2";
            sql = string.Format(sql, patient_id, Visit_id);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.LAB_TEST_MASTER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        /// <summary>
        /// 该病人医生未录入诊断，,请填写后再出院!
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="Visit_id"></param>
        /// <returns></returns>
        public int GetDiagnosisCount(string patient_id, string Visit_id)
        {
            string sql = @"SELECT count(*)  FROM diagnosis
		   WHERE patient_id ='{0}' and visit_id='{1}' and  diagnosis_type=3";
            sql = string.Format(sql, patient_id, Visit_id);
            return int.Parse(BaseEntityer.Db.GetDataTable(sql).Rows[0][0].ToString());
        }
        /// <summary>
        /// 读取出院方式字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.DISCHARGE_DISPOSITION_DICT> DischargeDispositionDict()
        {
            string sql;
            sql = "select * from DISCHARGE_DISPOSITION_DICT order by SERIAL_NO ";
            //sql = sql.SqlFormate(obs);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DISCHARGE_DISPOSITION_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }


        /// <summary>
        /// 更新出院患者在patVisit
        /// </summary>
        /// <param name="patVisit"></param>
        /// <param name="Db"></param>
        public void UpatePatVisitOut(HisCommon.DataEntity.PAT_VISIT patVisit, BaseEntityer Db)
        {
            string sql = @"update PAT_VISIT set DEPT_DISCHARGE_FROM='{0}',DISCHARGE_DATE_TIME=to_date('{1}', 'yyyy-MM-dd  hh24:mi:ss'),
               DISCHARGE_DISPOSITION='{2}',STATE='{3}',personinfo='{4}',charge='{5}',CATALOGER = '{8}' where PATIENT_ID='{6}' and VISIT_ID='{7}'";
            sql = string.Format(sql, patVisit.DEPT_DISCHARGE_FROM, patVisit.DISCHARGE_DATE_TIME,
                patVisit.DISCHARGE_DISPOSITION, patVisit.STATE, patVisit.PERSONINFO, patVisit.CHARGE, patVisit.PATIENT_ID, patVisit.VISIT_ID, patVisit.CATALOGER);
            Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除出院申请记录
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="Db"></param>
        public void DelPreDischgedPats(string patient_id, BaseEntityer Db)
        {
            string sql = @"DELETE pre_dischged_pats WHERE patient_id = '{0}'";
            sql = string.Format(sql, patient_id);
            Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 出院更新转科记录表
        /// </summary>
        /// <param name="transfer"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateTransferOut(HisCommon.DataEntity.TRANSFER transfer, BaseEntityer db)
        {
            string sql = @"  update transfer
            SET discharge_date_time =to_date('{0}', 'yyyy-MM-dd  hh24:mi:ss'),
            doctor_in_charge  = '{1}',
            dept_transfered_to  = NULL
            where patient_id = '{2}'
            AND visit_id = {3}
            AND discharge_date_time IS NULL";  //处理转科的最后一条记录
            object[] obs = new object[] 
               {
                transfer.DISCHARGE_DATE_TIME,
                transfer.DOCTOR_IN_CHARGE,
                transfer.PATIENT_ID,
                transfer.VISIT_ID};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除在院记录
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="Db"></param>
        public void DeletePatsInHospital(string patient_id, BaseEntityer Db)
        {
            string sql = @"delete from  pats_in_hospital where  Patient_id='{0}' ";
            sql = string.Format(sql, patient_id);
            Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 读取出院患者信息
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.InpationInfoOut> GetInpatientInfoOut(string WARD_CODE)
        {
            string sql = @"select p.patient_id,v.visit_id,p.NAME,decode(p.SEX,'1','男','2','女','男','男','女','女','') as SEX,
           --floor(MONTHS_BETWEEN(sysdate,p.DATE_OF_BIRTH)/12) as Age,
           (to_char(v.admission_date_time,'yyyy')-to_char(p.DATE_OF_BIRTH,'yyyy')) as Age,
           v.DISCHARGE_DATE_TIME,
           v.DEPT_DISCHARGE_FROM,
           v.admission_date_time,
           v.doctor_in_charge,
           v.PAT_ADM_CONDITION,
           v.personinfo,
           p.INP_NO ,
           (select d.dept_name from DEPT_DICT d where d.dept_code=v.DEPT_DISCHARGE_FROM) as DEPT_NAME,
           p.CHARGE_TYPE
           from PAT_VISIT v ,PAT_MASTER_INDEX p where v.patient_id=p.patient_id and 
           v.STATE='B' and v.DEPT_DISCHARGE_FROM 
           in( select dept_code  from dept_vs_ward where ward_code ='{0}')";
            sql = sql.SqlFormate(WARD_CODE);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.InpationInfoOut>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 取消出院：插入患者在院信息
        /// </summary>
        /// <param name="pats"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertPatsInHostital(HisCommon.DataEntity.PATS_IN_HOSPITAL_OUT pats, BaseEntityer db)
        {
            string sql = @"insert into pats_in_hospital (PATIENT_ID, VISIT_ID, WARD_CODE, DEPT_CODE, BED_NO, ADMISSION_DATE_TIME, ADM_WARD_DATE_TIME, DIAGNOSIS, PATIENT_CONDITION, NURSING_CLASS, DOCTOR_IN_CHARGE, OPERATING_DATE, BILLING_DATE_TIME, PREPAYMENTS, TOTAL_COSTS, TOTAL_CHARGES, GUARANTOR, GUARANTOR_ORG, GUARANTOR_PHONE_NUM, BILL_CHECKED_DATE_TIME, SETTLED_INDICATOR, DIAGID, DIAGNAME, PERSONINFO, ADMIS)
             values ('{0}', {1}, '{2}', '{3}',
            {4}, to_date('{5}', 'yyyy-MM-dd  hh24:mi:ss'), 
            to_date('{6}', 'yyyy-MM-dd  hh24:mi:ss'), 
            '{7}', '{8}', '9', '{10}', {11}, 
             to_date('{12}', 'yyyy-MM-dd  hh24:mi:ss'),
             {13}, {14}, {15}, '{16}', '{17}', '{18}',
              to_date('{19}', 'yyyy-MM-dd  hh24:mi:ss'), {20}, '{21}', '{22}','{23}', {24})";
            object[] obs = new object[] 
               {pats.PATIENT_ID,
                pats.VISIT_ID,
                pats.WARD_CODE,
                pats.DEPT_CODE,
                pats.BED_NO,
                pats.ADMISSION_DATE_TIME,
                pats.ADM_WARD_DATE_TIME,
                pats.DIAGNOSIS,
                pats.PATIENT_CONDITION,
                pats.NURSING_CLASS,
                pats.DOCTOR_IN_CHARGE,
                pats.OPERATING_DATE,
                pats.BILLING_DATE_TIME,
                pats.PREPAYMENTS,
                pats.TOTAL_COSTS,
                pats.TOTAL_CHARGES,
                pats.GUARANTOR,
                pats.GUARANTOR_ORG,
                pats.GUARANTOR_PHONE_NUM,
                pats.BILL_CHECKED_DATE_TIME,
                pats.SETTLED_INDICATOR,
                pats.DIAGID,
                pats.DIAGNAME,
                pats.PERSONINFO,
                pats.ADMIS};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 取消出院:更新PatVisit入院状态
        /// </summary>
        /// <param name="patVisit"></param>
        /// <param name="Db"></param>
        public void UpatePatVisitReturnOut(HisCommon.DataEntity.PAT_VISIT patVisit, BaseEntityer Db)
        {
            string sql = @"update pat_visit SET discharge_date_time =NULL , dept_discharge_from =NULL,STATE='{2}' where patient_id = '{0}' and visit_id ={1}";
            sql = string.Format(sql, patVisit.PATIENT_ID, patVisit.VISIT_ID, patVisit.STATE);
            Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 取消出院:更新转科
        /// </summary>
        /// <param name="transfer"></param>
        /// <param name="Db"></param>
        public void UpateTransferReturnOut(HisCommon.DataEntity.TRANSFER transfer, BaseEntityer Db)
        {
            string sql = @" update transfer SET discharge_date_time =NULL  where patient_id = '{0}' and visit_id ={1} and admission_date_time =to_date('{2}', 'yyyy-MM-dd  hh24:mi:ss') ";
            sql = string.Format(sql, transfer.PATIENT_ID, transfer.VISIT_ID, transfer.ADMISSION_DATE_TIME);
            Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 取消取药：更新日志
        /// </summary>
        /// <param name="adt"></param>
        /// <param name="Db"></param>
        public void DelteAdtLogReturnOut(HisCommon.DataEntity.ADT_LOG adt, BaseEntityer Db)
        {
            string sql = @"  DELETE from adt_log where patient_id ='{0}' AND visit_id ={1} AND ( log_date_time = to_date('{2}', 'yyyy-MM-dd  hh24:mi:ss') OR action in ( 'F' , 'G' , 'H' ) ) ";
            sql = string.Format(sql, adt.PATIENT_ID, adt.VISIT_ID, adt.LOG_DATE_TIME);
            Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 取消出院：读取预交金
        /// </summary>
        /// <param name="patient_id"></param>
        /// <returns></returns>
        public DataTable GetPrepayment(string patient_id)
        {
            string sql = @"SELECT sum(amount) as amount  FROM prepayment_rcpt WHERE patient_id = '{0}' and transact_type <> '作废' ";
            sql = string.Format(sql, patient_id);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 取消出院:读取费用
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public DataTable GetCostsCharges(string patient_id, string visit_id)
        {
            string sql = @" SELECT sum (costs) as costs, sum(charges) as charges FROM inp_bill_detail WHERE patient_id = '{0}' and visit_id ={1} ";
            sql = string.Format(sql, patient_id, visit_id);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 取消出院:读取日志最大日期
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public DataTable GetAdtLogDateTime(string patient_id, string visit_id)
        {
            string sql = @" SELECT max (log_date_time ) as log_date_time  FROM adt_log WHERE patient_id = '{0}' and  visit_id ={1} and ( Action ='F' or action ='G' or action ='H' ) ";
            sql = string.Format(sql, patient_id, visit_id);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 读取转科最大日期记录
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public DataTable GetTransferDateTime(string patient_id, string visit_id)
        {
            string sql = @"SELECT max(admission_date_time ) as admission_date_time FROM transfer WHERE patient_id = '{0}' AND visit_id ={1} ";
            sql = string.Format(sql, patient_id, visit_id);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 读取多人未审核医嘱
        /// </summary>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ORDERS> GetOrdersInfoByWardCode(string WARD_CODE)
        {
            string sql = @" SELECT PAT_MASTER_INDEX.NAME,
             PATS_IN_HOSPITAL.WARD_CODE,
             ORDERS.ORDER_NO,
             ORDERS.ORDER_SUB_NO,
             PATS_IN_HOSPITAL.BED_NO,
             ORDERS.PATIENT_ID,
             ORDERS.VISIT_ID,
             ORDERS.REPEAT_INDICATOR,
             ORDERS.ORDER_CLASS,
             ORDERS.ORDER_TEXT,
             ORDERS.DOSAGE,
             ORDERS.DOSAGE_UNITS,
             ORDERS.ADMINISTRATION,
             ORDERS.START_DATE_TIME,
             ORDERS.STOP_DATE_TIME,
             ORDERS.FREQUENCY,
             ORDERS.FREQ_COUNTER,
             ORDERS.FREQ_INTERVAL,
             ORDERS.FREQ_INTERVAL_UNIT,
             ORDERS.DURATION,
             ORDERS.DURATION_UNITS,
             ORDERS.FREQ_DETAIL,
             ORDERS.PERFORM_SCHEDULE,
             ORDERS.DOCTOR,
             ORDERS.STOP_DOCTOR,
             ORDERS.NURSE,
             ORDERS.ENTER_DATE_TIME,
             ORDERS.ORDER_STATUS,
             ORDERS.BILLING_ATTR,
             ORDERS.LAST_ACCTING_DATE_TIME,
             ORDERS.ORDERING_DEPT,
             ORDERS.STOP_NURSE,
             ORDERS.STOP_ORDER_DATE_TIME,
             ORDERS.DRUG_BILLING_ATTR,
             ORDERS.ORDER_CODE,
             ORDERS.TEST_NO,
             ORDERS.DRUG_SPEC,
             ORDERS.PERFORMED_BY
        FROM PATS_IN_HOSPITAL, PAT_MASTER_INDEX, ORDERS
       WHERE (PATS_IN_HOSPITAL.PATIENT_ID =
             PAT_MASTER_INDEX.PATIENT_ID)
         and (PAT_MASTER_INDEX.PATIENT_ID = ORDERS.PATIENT_ID)
         and (PATS_IN_HOSPITAL.VISIT_ID = ORDERS.VISIT_ID)
         AND (PATS_IN_HOSPITAL.WARD_CODE = '{0}')
         and ((ORDERS.ORDER_STATUS = '1') OR
             (ORDERS.ORDER_STATUS = '2' and
             ORDERS.STOP_DATE_TIME IS NOT NULL) and
             ORDERS.REPEAT_INDICATOR = 1)";
            sql = sql.SqlFormate(WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 护士修改医嘱的停止时间
        /// </summary>
        /// <param name="orders"></param>
        public int UpateStopOrdersDate(HisCommon.DataEntity.ORDERS orders)
        {
            string sql = @"update orders Set stop_date_time =to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') , stop_doctor ='{1}' , stop_nurse ='{2}' , stop_order_date_time =to_date('{3}', 'yyyy-MM-dd hh24:mi:ss') where patient_id ='{4}' and visit_id ='{5}' and Order_No ='{6}' ";

            if (orders.LAST_DECOMPOSE_DATE_TIME == null && orders.STOP_DATE_TIME <= orders.START_DATE_TIME)
            {
                sql = @"update orders Set LAST_DECOMPOSE_DATE_TIME=START_DATE_TIME ,stop_date_time =to_date('{0}', 'yyyy-MM-dd hh24:mi:ss') , stop_doctor ='{1}' , stop_nurse ='{2}' , stop_order_date_time =to_date('{3}', 'yyyy-MM-dd hh24:mi:ss') where patient_id ='{4}' and visit_id ='{5}' and Order_No ='{6}' ";
            }

            sql = string.Format(sql, orders.STOP_DATE_TIME, orders.STOP_DOCTOR, orders.STOP_NURSE, orders.STOP_ORDER_DATE_TIME, orders.PATIENT_ID, orders.VISIT_ID, orders.ORDER_NO);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 读取要取消转科的人员列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTransferInpatientReturnInfo(string WARD_CODE)
        {
            string sql = @" SELECT PAT_MASTER_INDEX.NAME,
             PATS_IN_TRANSFERRING.PATIENT_ID,
             PATS_IN_TRANSFERRING.DEPT_TRANSFERED_FROM,
             PATS_IN_TRANSFERRING.DEPT_TRANSFERED_TO,
             PATS_IN_TRANSFERRING.TRANSFER_DATE_TIME,
             (select d.DEPT_NAME  from DEPT_DICT d where d.dept_code= PATS_IN_TRANSFERRING.DEPT_TRANSFERED_FROM) as DEPT_NAME_from,
             (select d.DEPT_NAME  from DEPT_DICT d where d.dept_code= PATS_IN_TRANSFERRING.DEPT_TRANSFERED_TO) as DEPT_NAME_to
        FROM PATS_IN_TRANSFERRING,
             PAT_MASTER_INDEX
       WHERE PATS_IN_TRANSFERRING.PATIENT_ID =  PAT_MASTER_INDEX.PATIENT_ID(+)
         and PATS_IN_TRANSFERRING.DEPT_TRANSFERED_FROM in (select DEPT_CODE from dept_vs_ward where  WARD_CODE='{0}' )";
            sql = string.Format(sql, WARD_CODE);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 取消转科:读取日志最大日期
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public DataTable GetTransferAdtLogDateTime(string patient_id, string visit_id)
        {
            string sql = @" SELECT max (log_date_time ) as log_date_time  FROM adt_log WHERE patient_id = '{0}' and  visit_id ={1} and  action ='E' ";
            sql = string.Format(sql, patient_id, visit_id);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 取消转科:更新转科记录表
        /// </summary>
        /// <param name="transfer"></param>
        /// <param name="Db"></param>
        public void UpateTransferReturnIn(HisCommon.DataEntity.TRANSFER transfer, BaseEntityer Db)
        {
            string sql = @" update transfer SET discharge_date_time =NULL, dept_transfered_to = null  where patient_id = '{0}' and visit_id ={1} and admission_date_time =to_date('{2}', 'yyyy-MM-dd  hh24:mi:ss') ";
            sql = string.Format(sql, transfer.PATIENT_ID, transfer.VISIT_ID, transfer.ADMISSION_DATE_TIME);
            Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 取消转科：删除日志
        /// </summary>
        /// <param name="adt"></param>
        /// <param name="Db"></param>
        public void DelteAdtLogReturnIn(HisCommon.DataEntity.ADT_LOG adt, BaseEntityer Db)
        {
            string sql = @"  DELETE from adt_log where patient_id ='{0}' AND visit_id ={1} AND log_date_time = to_date('{2}', 'yyyy-MM-dd  hh24:mi:ss') ";
            sql = string.Format(sql, adt.PATIENT_ID, adt.VISIT_ID, adt.LOG_DATE_TIME);
            Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 取消转科：更新在科信息
        /// </summary>
        /// <param name="patsIn"></param>
        /// <param name="Db"></param>
        public void UpdatePatsInHospitalTransferIn(HisCommon.DataEntity.PATS_IN_HOSPITAL patsIn, BaseEntityer Db)
        {
            string sql = @"update  PATS_IN_HOSPITAL set WARD_CODE='{0}',DEPT_CODE='{1}',
            BED_NO='{2}', NURSING_CLASS='{3}',DOCTOR_IN_CHARGE='{4}',ADM_WARD_DATE_TIME=to_date('{5}', 'yyyy-MM-dd hh24:mi:ss'),ADMIS=0 where PATIENT_ID='{6}' and VISIT_ID='{7}' ";
            sql = string.Format(sql, patsIn.WARD_CODE, patsIn.DEPT_CODE, patsIn.BED_NO,
                 patsIn.NURSING_CLASS, patsIn.DOCTOR_IN_CHARGE, patsIn.ADM_WARD_DATE_TIME, patsIn.PATIENT_ID, patsIn.VISIT_ID);
            Db.ExecuteNonQuery(sql);

        }
        /// <summary>
        /// --取消入科--读取日志的状态和时间。
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public DataTable GetAdtLogAction(string patient_id, string visit_id)
        {
            string sql = @"SELECT log_date_time, action
                        FROM adt_log
                        where patient_id ='{0}' and VISIT_ID='{1}' AND (action = 'C' or action = 'D') AND
                        Log_date_time = (Select Max(Log_date_time) FROM adt_log
                        WHERE patient_id ='{0}' and VISIT_ID='{1}'and
                        (action = 'C' or action = 'D'))";
            sql = string.Format(sql, patient_id, visit_id);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 取消入科--医嘱开立
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="Visit_id"></param>
        /// <returns></returns>
        public int GetOrdersCount(string patient_id, string Visit_id, string Log_DateTime)
        {
            string sql = @" SELECT Count(*)
                            FROM orders
                            WHERE patient_id = '{0}'
                            AND Visit_id ='{1}' and  start_date_time >= to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')";
            sql = string.Format(sql, patient_id, Visit_id, Log_DateTime);
            return int.Parse(BaseEntityer.Db.GetDataTable(sql).Rows[0][0].ToString());
        }
        /// <summary>
        ///  取消入科--收费项目
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="Visit_id"></param>
        /// <param name="Log_DateTime"></param>
        /// <returns></returns>
        public int GetBillingCount(string patient_id, string Visit_id, string Log_DateTime)
        {
            string sql = @" SELECT count(*) from inp_bill_detail
                            WHERE patient_id = '{0}'
                            AND Visit_id ='{1}' and  billing_date_time >= to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')";
            sql = string.Format(sql, patient_id, Visit_id, Log_DateTime);
            return int.Parse(BaseEntityer.Db.GetDataTable(sql).Rows[0][0].ToString());
        }
        /// <summary>
        /// 取消入科--更新取消入科操作
        /// </summary>
        /// <param name="ward_code"></param>
        /// <param name="bed_no"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int UpdateAddReturnPatInHospitalInfo(HisCommon.DataEntity.PATS_IN_HOSPITAL pat, BaseEntityer db)
        {
            string sql = @"update pats_in_hospital
                        SET ward_code          = NULL,
                        bed_no             = NULL,
                        dept_code          = null,
                        doctor_in_charge   = null
                        where ward_code =  '{0}'
                        and bed_no =  {1}  ";
            object[] obs = new object[] 
               {
                pat.WARD_CODE,
                pat.BED_NO };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 取消入科--删除日志
        /// </summary>
        /// <param name="adt"></param>
        /// <param name="Db"></param>
        public void DelteAdtLogAddReturn(HisCommon.DataEntity.ADT_LOG adt, BaseEntityer Db)
        {
            string sql = @"  DELETE from adt_log where patient_id ='{0}' AND visit_id ={1}";
            sql = string.Format(sql, adt.PATIENT_ID, adt.VISIT_ID, adt.LOG_DATE_TIME);
            Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 取消入科--删除转科日志
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="Db"></param>
        public void DelteTransferAddReturn(HisCommon.DataEntity.TRANSFER tran, BaseEntityer Db)
        {
            string sql = @"  DELETE from TRANSFER where patient_id ='{0}' AND visit_id ={1}  AND discharge_date_time IS NULL";
            sql = string.Format(sql, tran.PATIENT_ID, tran.VISIT_ID);
            Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 读取转科之前的科室信息
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public DataTable GetDeptTransferFrom(string patient_id, string visit_id)
        {
            string sql = @"SELECT dept_stayed
                            FROM transfer 
                            WHERE Patient_id='{0}' AND discharge_date_time = (
                            SELECT Max(discharge_date_time) 
                            from transfer
                            WHERE Patient_id ='{0}' AND visit_id='{1}')";
            sql = string.Format(sql, patient_id, visit_id);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 判读上传医保是否已经上传完成。
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public DataTable GetPersonIsUpLoad(string patient_id, string visit_id)
        {
            string sql = @" select p.patient_id,p.visit_id, is_upload from pat_visit p  left join charge_type_dict c on p.CHARGE_TYPE_CODE=c.charge_type_code
              where p.patient_id='{0}' and p.visit_id='{1}'";
            sql = string.Format(sql, patient_id, visit_id);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 患者为上传费用列表
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public DataTable GetPersonUnUpLoad(string patient_id, string visit_id)
        {
            string sql = @" select *  from inp_bill_detail i
              where i.patient_id='{0}' and i.visit_id='{1}' and i.up_flag='0'";
            sql = string.Format(sql, patient_id, visit_id);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 更新患者信息
        /// </summary>
        /// <param name="patsIn"></param>
        /// <param name="Db"></param>
        public void UpdateInpationPatsInHostital(HisCommon.DataEntity.PATS_IN_HOSPITAL patsIn, BaseEntityer Db)
        {
            string sql = @"update  PATS_IN_HOSPITAL set  PATIENT_CONDITION='{0}',
            NURSING_CLASS='{1}',DOCTOR_IN_CHARGE='{2}',OPERATING_DATE=to_date('{3}', 'yyyy-MM-dd hh24:mi:ss'),ADM_WARD_DATE_TIME=to_date('{4}', 'yyyy-MM-dd hh24:mi:ss'),DIAGNOSIS='{5}' where PATIENT_ID='{6}' and VISIT_ID='{7}' ";
            sql = string.Format(sql,
                   patsIn.PATIENT_CONDITION,
                   patsIn.NURSING_CLASS,
                   patsIn.DOCTOR_IN_CHARGE,
                   patsIn.OPERATING_DATE,
                   patsIn.ADM_WARD_DATE_TIME,
                   patsIn.DIAGNOSIS,
                   patsIn.PATIENT_ID,
                   patsIn.VISIT_ID);
            Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="parmName"></param>
        /// <returns></returns>
        public string GetSysConfig(string parmName)
        {
            string sql = @"select PARAM_VALUE from sys_param t 
where t.param_name='{0}'";
            string seq = "";
            sql = string.Format(sql, parmName);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    seq = dt.Rows[0][0].ToString();
            return seq;
        }
        /// <summary>
        /// 检验结果查询
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <param name="TEST_NO"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.V_LAB_RESULT> GetLabResultList(string PATIENT_ID, string VISIT_ID, string TEST_NO)
        {
            List<HisCommon.DataEntity.V_LAB_RESULT> list = new List<HisCommon.DataEntity.V_LAB_RESULT>();
            try
            {
                string sql = "";

                if (VISIT_ID.ToString() == "")
                {
                    sql = @"select * from V_LAB_RESULT t where t.PATIENT_ID='{0}'  and t.TEST_NO='{1}'order by  TEST_ITEM_NO";
                    sql = string.Format(sql, PATIENT_ID, TEST_NO);
                }
                else
                {
                    sql = @"select * from V_LAB_RESULT t where t.PATIENT_ID='{0}' and t.VISIT_ID='{1}' and t.TEST_NO='{2}'order by  TEST_ITEM_NO";
                    sql = string.Format(sql, PATIENT_ID, VISIT_ID, TEST_NO);
                }
                System.Data.DataSet ds = BaseEntityToSQL.Db.GetDataSet(sql);
                list = DataSetToEntity.DataSetToT<V_LAB_RESULT>(ds).ToList();

            }
            catch
            {

            }
            return list;
        }

        /// <summary>
        /// 门诊输液未确认查询
        /// </summary>
        /// <param name="dateBegin">缴费时间起始位置</param>
        /// <param name="dateEnd">缴费时间终止位置</param>
        /// <param name="name">患者姓名</param>
        /// <param name="patientID">患者ID</param>
        /// <param name="visit_no">就诊序号</param>
        /// <param name="administration">药品用法模糊语句</param>
        /// <param name="performed_by">执行科室</param>
        /// <param name="item_name">耗材名称模糊语句</param>
        /// <returns>未确认查询病人列表</returns>
        public DataTable GetAllInfusionNoConfirmQuery(string dateBegin, string dateEnd, string name, string patientID, string visit_no, string dept_code, string administration, string performed_by, string item_name)
        {
            string sql = @"select distinct m.name,m.sex,m.age,d.dept_name,o.patient_id,o.visit_date,o.visit_no,o.serial_no,o.ordered_by,o.doctor,o.order_date 
                           from outp_orders o 
                           left join dept_dict d on d.dept_code = o.ordered_by 
                           left join outp_presc c on c.serial_no = o.serial_no and c.visit_no = o.visit_no 
                           left join outp_bill_items b on b.serial_no = o.serial_no 
                           left join clinic_master m ON m.patient_id = o.patient_id and m.visit_no = b.visit_no 
                           where o.order_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                           and o.order_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') 
                           and c.charge_indicator = '1' 
                           and b.serial_no in 
                           (select distinct i.serial_no 
                           from outp_bill_items i 
                           left join outp_rcpt_master r on r.rcpt_no = i.rcpt_no 
                           where r.charge_indicator = '0')";
            if (name != string.Empty)
            {
                sql += " and m.name = '{2}'";
            }
            if (patientID != string.Empty)
            {
                sql += " and m.patient_id = '{3}'";
            }
            if (visit_no != string.Empty)
            {
                sql += " and o.visit_no = '{4}'";
            }
            if (administration != string.Empty && item_name != string.Empty)
            {
                sql += " and ((c.qrbz = '0' and ({6})) or (b.sysbj is null and {8}))";
            }
            else if (administration != string.Empty && item_name == string.Empty)
            {
                sql += " and ((c.qrbz = '0' and ({6})) or b.sysbj is null)";
            }
            else if (administration == string.Empty && item_name != string.Empty)
            {
                sql += " and (c.qrbz = '0' or (b.sysbj is null and {8}))";
            }
            if (performed_by != string.Empty)
            {
                sql += " and ({7})";
            }
            sql += " order by o.visit_date";
            sql = string.Format(sql, dateBegin, dateEnd, name, patientID, visit_no, dept_code, administration, performed_by, item_name);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 门诊输液已确认查询
        /// </summary>
        /// <param name="dateBegin">缴费时间起始位置</param>
        /// <param name="dateEnd">缴费时间终止位置</param>
        /// <param name="name">患者姓名</param>
        /// <param name="patientID">患者ID</param>
        /// <param name="visit_no">就诊序号</param>
        /// <param name="administration">药品用法模糊语句</param>
        /// <param name="performed_by">执行科室</param>
        /// <param name="item_name">耗材名称模糊语句</param>
        /// <returns>已确认查询病人列表</returns>
        public DataTable GetAllInfusionConfirmQuery(string dateBegin, string dateEnd, string name, string patientID, string visit_no, string dept_code, string administration, string performed_by, string item_name)
        {
            string sql = @"select distinct m.name,m.sex,m.age,d.dept_name,o.patient_id,o.visit_date,o.visit_no,o.serial_no,o.ordered_by,o.doctor,o.order_date 
                           from outp_orders o 
                           left join dept_dict d on d.dept_code = o.ordered_by 
                           left join outp_presc c on c.serial_no = o.serial_no and c.visit_no = o.visit_no 
                           left join outp_bill_items b on b.serial_no = o.serial_no 
                           left join clinic_master m ON m.patient_id = o.patient_id and m.visit_no = b.visit_no 
                           where o.order_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                           and o.order_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') 
                           and c.charge_indicator = '1' 
                           and (c.qrbz = '1' or b.sysbj is not null)
                           and b.serial_no in 
                           (select distinct i.serial_no 
                           from outp_bill_items i 
                           left join outp_rcpt_master r on r.rcpt_no = i.rcpt_no 
                           where r.charge_indicator = '0')";
            if (name != string.Empty)
            {
                sql += " and m.name = '{2}'";
            }
            if (patientID != string.Empty)
            {
                sql += " and m.patient_id = '{3}'";
            }
            if (visit_no != string.Empty)
            {
                sql += " and o.visit_no = '{4}'";
            }
            if (administration != string.Empty && item_name != string.Empty)
            {
                sql += " and ({6} or {8})";
            }
            else if (administration != string.Empty && item_name == string.Empty)
            {
                sql += " and ({6})";
            }
            else if (administration == string.Empty && item_name != string.Empty)
            {
                sql += " and ({8})";
            }
            if (performed_by != string.Empty)
            {
                sql += " and ({7})";
            }
            sql += " order by o.visit_date";
            sql = string.Format(sql, dateBegin, dateEnd, name, patientID, visit_no, dept_code, administration, performed_by, item_name);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 耗材明细查询（未确认）
        /// </summary>
        /// <param name="patientID">患者ID</param>
        /// <param name="visit_no">就诊序号</param>
        /// <param name="serial_no">处方号</param>
        /// <param name="dateBegin">缴费时间起始位置</param>
        /// <param name="dateEnd">缴费时间终止位置</param>
        /// <returns>未确认耗材明细列表</returns>
        public DataTable GetAllBillItem_I_NoConfirmQuery(string patientID, string visit_no, string dateBegin, string dateEnd, string dept_code, string performed_by, string item_name)
        {
            string sql = @"select distinct i.class_name,b.item_name,b.item_spec,b.amount,b.units,b.qr_time,b.rcpt_no,b.visit_no,b.serial_no,b.visit_date,b.item_no,
                                       case b.sysbj when null then '未确认' when '1' then '已确认' else '未确认' END sysbj,
                                       case when b.qr_opper IS NULL then ' ' else b.qr_opper END qr_opper 
                                       from outp_bill_items b 
                                       left join outp_rcpt_master r on b.rcpt_no = r.rcpt_no 
                                       left join BILL_ITEM_CLASS_DICT i on b.item_class = i.class_code 
                                       where (b.item_class = 'I' or b.item_class = 'E') 
                                       and b.costs >=0 and r.charge_indicator = '0' 
                                       and b.sysbj is null 
                                       and b.visit_date >= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') 
                                       and b.visit_date <= to_date('{3}', 'yyyy-mm-dd hh24:mi:ss')";
            if (patientID != string.Empty)
            {
                sql += " and r.patient_id = '{0}'";
            }
            if (visit_no != string.Empty)
            {
                sql += " and b.visit_no = '{1}'";
            }
            if (performed_by != string.Empty)
            {
                sql += " and ({5})";
            }
            if (item_name != string.Empty)
            {
                sql += " and ({6})";
            }
            sql += " order by b.visit_date,b.item_no";
            sql = string.Format(sql, patientID, visit_no, dateBegin, dateEnd, dept_code, performed_by, item_name);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 药品明细查询（未确认）
        /// </summary>
        /// <param name="patientID">患者ID</param>
        /// <param name="visit_no">就诊序号</param>
        /// <param name="serial_no">处方号</param>
        /// <param name="dateBegin">缴费时间起始位置</param>
        /// <param name="dateEnd">缴费时间终止位置</param>
        /// <returns>未确认药品明细列表</returns>
        public DataTable GetAllOupt_Presc_NoConfirmQuery(string patientID, string visit_no, string serial_no, string dateBegin, string dateEnd, string administration)
        {
            string sql = @"select c.drug_name,c.drug_spec,c.amount,c.units,c.qr_time,c.visit_no,c.serial_no,c.visit_date,c.item_no,
                           case c.qrbz when '0' then '未确认' when '1' then '已确认' END qrbz,
                           case when c.qr_opper IS NULL then ' ' else c.qr_opper END qr_opper 
                           from outp_presc c 
                           left join outp_orders o on o.serial_no = c.serial_no and o.visit_no = c.visit_no 
                           where c.charge_indicator = '1' 
                           and c.qrbz = '0' 
                           and c.operator_date >= to_date('{3}', 'yyyy-mm-dd hh24:mi:ss') 
                           and c.operator_date <= to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')";
            if (patientID != string.Empty)
            {
                sql += " and o.patient_id = '{0}'";
            }
            if (visit_no != string.Empty)
            {
                sql += " and c.visit_no = '{1}'";
            }
            if (serial_no != string.Empty)
            {
                sql += " and c.serial_no = '{2}'";
            }
            if (administration != string.Empty)
            {
                sql += " and ({5})";
            }
            sql += " order by c.visit_date,c.item_no,c.zb";
            sql = string.Format(sql, patientID, visit_no, serial_no, dateBegin, dateEnd, administration);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 耗材明细查询（已确认）
        /// </summary>
        /// <param name="patientID">患者ID</param>
        /// <param name="visit_no">就诊序号</param>
        /// <param name="serial_no">处方号</param>
        /// <param name="dateBegin">缴费时间起始位置</param>
        /// <param name="dateEnd">缴费时间终止位置</param>
        /// <returns>已确认耗材明细列表</returns>
        public DataTable GetAllBillItem_I_ConfirmQuery(string patientID, string visit_no, string dateBegin, string dateEnd, string dept_code, string performed_by, string item_name)
        {
            string sql = @"select distinct i.class_name,b.item_name,b.item_spec,b.amount,b.units,b.qr_time,b.rcpt_no,b.visit_no,b.serial_no,b.visit_date,b.item_no,
                           case b.sysbj when null then '未确认' when '1' then '已确认' else '未确认' END sysbj,
                           case when b.qr_opper IS NULL then ' ' else b.qr_opper END qr_opper 
                           from outp_bill_items b 
                           left join outp_rcpt_master r on b.rcpt_no = r.rcpt_no 
                           left join BILL_ITEM_CLASS_DICT i on b.item_class = i.class_code 
                           where (b.item_class = 'I' or b.item_class = 'E') 
                           and b.costs >=0 and r.charge_indicator = '0' 
                           and b.sysbj is not null 
                           and b.visit_date >= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') 
                           and b.visit_date <= to_date('{3}', 'yyyy-mm-dd hh24:mi:ss')";
            if (patientID != string.Empty)
            {
                sql += " and r.patient_id = '{0}'";
            }
            if (visit_no != string.Empty)
            {
                sql += " and b.visit_no = '{1}'";
            }
            if (performed_by != string.Empty)
            {
                sql += " and ({5})";
            }
            if (item_name != string.Empty)
            {
                sql += " and ({6})";
            }
            sql += " order by b.visit_date,b.item_no";
            sql = string.Format(sql, patientID, visit_no, dateBegin, dateEnd, dept_code, performed_by, item_name);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 药品明细查询（已确认）
        /// </summary>
        /// <param name="patientID">患者ID</param>
        /// <param name="visit_no">就诊序号</param>
        /// <param name="serial_no">处方号</param>
        /// <param name="dateBegin">缴费时间起始位置</param>
        /// <param name="dateEnd">缴费时间终止位置</param>
        /// <returns>已确认药品明细列表</returns>
        public DataTable GetAllOupt_Presc_ConfirmQuery(string patientID, string visit_no, string serial_no, string dateBegin, string dateEnd, string administration)
        {
            string sql = @"select c.drug_name,c.drug_spec,c.amount,c.units,c.qr_time,c.visit_no,c.serial_no,c.visit_date,c.item_no,
                           case c.qrbz when '0' then '未确认' when '1' then '已确认' END qrbz,
                           case when c.qr_opper IS NULL then ' ' else c.qr_opper END qr_opper 
                           from outp_presc c 
                           left join outp_orders o on o.serial_no = c.serial_no and o.visit_no = c.visit_no 
                           where c.charge_indicator = '1' 
                           and c.qrbz = '1' 
                           and c.operator_date >= to_date('{3}', 'yyyy-mm-dd hh24:mi:ss') 
                           and c.operator_date <= to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')";
            if (patientID != string.Empty)
            {
                sql += " and o.patient_id = '{0}'";
            }
            if (visit_no != string.Empty)
            {
                sql += " and c.visit_no = '{1}'";
            }
            if (serial_no != string.Empty)
            {
                sql += " and c.serial_no = '{2}'";
            }
            if (administration != string.Empty)
            {
                sql += " and ({5})";
            }
            sql += " order by c.visit_date,c.item_no,c.zb";
            sql = string.Format(sql, patientID, visit_no, serial_no, dateBegin, dateEnd, administration);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 药品明细查询_输液药品（已确认）
        /// </summary>
        /// <param name="patientID">患者ID</param>
        /// <param name="visit_no">就诊序号</param>
        /// <param name="serial_no">处方号</param>
        /// <param name="dateBegin">缴费时间起始位置</param>
        /// <param name="dateEnd">缴费时间终止位置</param>
        /// <returns>已确认药品明细列表</returns>
        public DataTable GetAllOupt_Presc_ConfirmQuery_Print(string patientID, string visit_no, string serial_no, string dateBegin, string dateEnd, string administration)
        {
            string sql = @"select c.zb,c.drug_name,c.administration,c.frequency,c.dosage,c.dosage_units,c.drug_spec,c.amount,c.units,o.doctor 
                           from outp_presc c 
                           left join outp_orders o on o.serial_no = c.serial_no and o.visit_no = c.visit_no 
                           where c.charge_indicator = '1' 
                           and c.qrbz = '1' 
                           and c.operator_date >= to_date('{3}', 'yyyy-mm-dd hh24:mi:ss') 
                           and c.operator_date <= to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')";
            if (patientID != string.Empty)
            {
                sql += " and o.patient_id = '{0}'";
            }
            if (visit_no != string.Empty)
            {
                sql += " and c.visit_no = '{1}'";
            }
            if (serial_no != string.Empty)
            {
                sql += " and c.serial_no = '{2}'";
            }
            if (administration != string.Empty)
            {
                sql += " and ({5})";
            }
            sql += " order by c.visit_date,c.item_no,c.zb";
            sql = string.Format(sql, patientID, visit_no, serial_no, dateBegin, dateEnd, administration);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 将未确认的耗材修改为已确认
        /// </summary>
        /// <param name="outp_bill_items">耗材信息</param>
        /// <param name="Db"></param>
        public void ModifyOutpBillItemsForSysbj(OUTP_BILL_ITEMS outp_bill_items, BaseEntityer Db)
        {
            string sql = @"update outp_bill_items set sysbj='{0}',qr_time=to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'),qr_opper='{2}' 
                           where visit_no='{3}' and serial_no='{4}' and rcpt_no='{5}' and item_no='{6}' ";
            sql = string.Format(sql,
                   outp_bill_items.SYSBJ,
                   outp_bill_items.QR_TIME,
                   outp_bill_items.QR_OPPER,
                   outp_bill_items.VISIT_NO,
                   outp_bill_items.SERIAL_NO,
                   outp_bill_items.RCPT_NO,
                   outp_bill_items.ITEM_NO);
            Db.ExecuteNonQuery(sql);
        }

        public void ModifyOutpPrescForQRBZ(OUTP_PRESC oupt_presc, BaseEntityer Db)
        {
            string sql = @"update outp_presc set qrbz='{0}',qr_time=to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'),qr_opper='{2}' 
                           where visit_no='{3}' and serial_no='{4}' and item_no='{5}' ";
            sql = string.Format(sql,
                   oupt_presc.QRBZ,
                   oupt_presc.QR_TIME,
                   oupt_presc.QR_OPPER,
                   oupt_presc.VISIT_NO,
                   oupt_presc.SERIAL_NO,
                   oupt_presc.ITEM_NO);
            Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 获取所有耗材的价表项目
        /// </summary>
        /// <returns></returns>
        public IList<CurrentPriceList> GetAllCurrentPriceList_I(string dept_code)
        {
            string sql = @"select c.* from current_price_list c 
                           left join INFUSION_HALL_DEPT_COMPARE i on i.item_class = c.item_class and i.item_code = c.item_code and i.item_spec = c.item_spec and i.units = c.units 
                           where (c.item_class='I' or c.item_class= 'E') 
                           and (i.item_no is null or i.item_no not in (select item_no from INFUSION_HALL_DEPT_COMPARE where dept_code = '{0}')) 
                           order by c.item_code";
            sql = string.Format(sql, dept_code);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<CurrentPriceList>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }
        /// <summary>
        /// 获取属于当前登录部门的耗材项目
        /// </summary>
        /// <returns></returns>
        public IList<CurrentPriceList> GetCurrentPriceListByDeptCode(string dept_code)
        {
            string sql = @"select c.* from current_price_list c 
                           left join INFUSION_HALL_DEPT_COMPARE i on i.item_class = c.item_class and i.item_code = c.item_code and i.item_spec = c.item_spec and i.units = c.units 
                           where c.item_class='I' 
                           and i.dept_code = '{0}' 
                           order by c.item_code";
            sql = string.Format(sql, dept_code);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<CurrentPriceList>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }
        /// <summary>
        /// 删除当前科室耗材维护项目列表数据
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteItemDict(BaseEntityer db, string dept_code)
        {
            string sql = @"DELETE FROM infusion_hall_dept_compare WHERE DEPT_CODE = {0}";
            sql = string.Format(sql, dept_code);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 新增当前科室耗材维护项目数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="item_dict"></param>
        /// <returns></returns>
        public int InsertItemDict(BaseEntityer db, InfusionHallDeptCompare item_dict)
        {
            string sql = @"INSERT INTO infusion_hall_dept_compare
                        (
                           item_no ,
                           item_class ,
                           item_code ,
                           item_spec ,
                           units ,
                           dept_code 
                        )
                        VALUES 
                        (
                           '{0}','{1}','{2}','{3}','{4}','{5}'
                        )";
            object[] param = new object[] { item_dict.Item_no, item_dict.Item_class, 
                item_dict.Item_code, item_dict.Item_spec, item_dict.Units, item_dict.Dept_code };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 静配中心--查询执行科室为本部门的所有确认的耗材和治疗费
        /// </summary>
        /// <param name="dateBegin">缴费开始日期</param>
        /// <param name="dateEnd">缴费结束日期</param>
        /// <param name="Type">查询类型</param>
        /// <param name="QR_OPPER">确认人</param>
        /// <param name="patient_Name">患者姓名</param>
        /// <param name="patient_ID">患者ID</param>
        /// <param name="dept_code">本部门code</param>
        /// <returns></returns>
        public DataTable GetAllBillItemByMyDeptCode(string dateBegin, string dateEnd, string Type, string QR_OPPER, string patient_Name, string patient_ID, string dept_code)
        {
            string sql = @"select i.class_name,o.item_name,o.item_spec,o.units,o.amount,o.charges,o.visit_date,o.qr_time,o.qr_opper,o.visit_no,o.serial_no,o.rcpt_no,o.item_no,r.patient_id,r.name from outp_bill_items o 
                           left join outp_rcpt_master r on o.rcpt_no = r.rcpt_no 
                           left join BILL_ITEM_CLASS_DICT i on o.item_class = i.class_code 
                           where o.qr_time >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                           and o.qr_time <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') 
                           and sysbj='1' ";
            if (Type != string.Empty)
            {
                sql += " and o.item_class = '{2}'";
            }
            if (QR_OPPER != string.Empty)
            {
                sql += " and o.qr_opper = '{3}'";
            }
            if (patient_Name != string.Empty)
            {
                sql += " and r.name = '{4}'";
            }
            if (patient_ID != string.Empty)
            {
                sql += " and r.patient_id = '{5}'";
            }
            if (dept_code != string.Empty)
            {
                sql += " and o.performed_by = '{6}'";
            }
            sql += " order by o.qr_time DESC";
            sql = string.Format(sql, dateBegin, dateEnd, Type, QR_OPPER, patient_Name, patient_ID, dept_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 获取所有本部门护士名单
        /// </summary>
        /// <param name="dept_code">本部门code</param>
        /// <returns></returns>
        public IList<USERS_STAFF_DICT> GetAllNurseByMyDeptCode(string dept_code)
        {
            string sql = @"select u.* from users_staff_dict u 
                           where u.user_dept = '{0}'";
            sql = string.Format(sql, dept_code);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<USERS_STAFF_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }
        //读取全部医生
        public List<BringSpringObject> GetDoctorInfoByAll()
        {
            DataTable dt = new DataTable();
            List<BringSpringObject> objlist = new List<BringSpringObject>();
            string sql = @"select user_id,user_name,F_TRANS_PINYIN_CAPITAL(user_name) name_spell from users_staff_dict where JOB='A1'";
            //sql = string.Format(sql, deptNo);
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
                    obj.Memo = dt.Rows[i][2].ToString();
                    objlist.Add(obj);
                }
            }
            catch
            {
                return null;
            }
            return objlist;
        }

        public List<HisCommon.DataEntity.EXAM_APPOINTS> GetExamAppointsInfo(string EXAM_NO)
        {
            string sql = @"SELECT *  FROM EXAM_APPOINTS WHERE EXAM_NO = '{0}'";
            sql = sql.SqlFormate(EXAM_NO);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXAM_APPOINTS>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 通过医嘱得到药品信息
        /// </summary>
        /// <param name="or"></param>
        /// <returns></returns>
        public DataTable GetDrugInfoByOrders(ORDERS or)
        {
            //包装规格PACKGE_SPEC
            string sql = @" select * from OUTP_ORDER_LIST o where o.DRUG_SPEC||o.FIRM_ID='{0}' 
                          and o.code='{1}' and o.deptcode='{2}' and o.class='{3}'";
            sql = string.Format(sql, or.DRUG_SPEC, or.ORDER_CODE, or.PERFORMED_BY, or.ORDER_CLASS);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 更新分解完成的医嘱日期
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="Db"></param>
        /// <returns>更新医嘱分解时间</returns>
        public int UpateDecomposeDateOrders(string patientID, string visitID, string orderNO, string orderSubNO, DateTime? lastDecomposeDateTime, DateTime? preUpateDecomposeDateTiem, BaseEntityer Db)
        {
            string sql = @"
                        UPDATE orders
                           SET last_decompose_date_time = to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')

                         WHERE patient_id = '{1}'
                           AND visit_id = '{2}'
                           AND order_no = '{3}'
                           AND order_sub_no = '{4}'
                           AND (last_decompose_date_time = to_date('{5}', 'yyyy-MM-dd hh24:mi:ss') OR
                               last_decompose_date_time IS NULL)
                         ";
            if (preUpateDecomposeDateTiem == null)
                preUpateDecomposeDateTiem = Convert.ToDateTime("2000-10-10 23:59:59");

            sql = string.Format(sql, lastDecomposeDateTime, patientID, visitID, orderNO, orderSubNO,
                preUpateDecomposeDateTiem);
            return Db.ExecuteNonQuery(sql);
        }

        public int InsertOrdersDrug(ORDERS_DRUG_DECOMPOSER orderDrug, BaseEntityer db)
        {
            //2013-11-29 by li 实际ICU开单科室记录
            string sql = @"INSERT INTO ORDERS_DRUG_DECOMPOSER
                        (
                               ORDERS_DRUG_DECOMPOSER.PRESC_NO,
                               ORDERS_DRUG_DECOMPOSER.PATIENT_ID,
                               ORDERS_DRUG_DECOMPOSER.VISIT_ID,
                               ORDERS_DRUG_DECOMPOSER.ORDER_ID,
                               ORDERS_DRUG_DECOMPOSER.SUB_ORDER_ID,
                               ORDERS_DRUG_DECOMPOSER.VISIT_DATE,
                               ORDERS_DRUG_DECOMPOSER.VISIT_NO,
                               ORDERS_DRUG_DECOMPOSER.SERIAL_NO,
                               ORDERS_DRUG_DECOMPOSER.ITEM_NO,
                               ORDERS_DRUG_DECOMPOSER.ITEM_CLASS,
                               ORDERS_DRUG_DECOMPOSER.DRUG_CODE,
                               ORDERS_DRUG_DECOMPOSER.DRUG_NAME,
                               ORDERS_DRUG_DECOMPOSER.DRUG_SPEC,
                               ORDERS_DRUG_DECOMPOSER.FIRM_ID,
                               ORDERS_DRUG_DECOMPOSER.UNITS,
                               ORDERS_DRUG_DECOMPOSER.AMOUNT,
                               ORDERS_DRUG_DECOMPOSER.DOSAGE,
                               ORDERS_DRUG_DECOMPOSER.DOSAGE_UNITS,
                               ORDERS_DRUG_DECOMPOSER.ADMINISTRATION,
                               ORDERS_DRUG_DECOMPOSER.FREQUENCY,
                               ORDERS_DRUG_DECOMPOSER.PROVIDED_INDICATOR,
                               ORDERS_DRUG_DECOMPOSER.COSTS,
                               ORDERS_DRUG_DECOMPOSER.CHARGES,
                               ORDERS_DRUG_DECOMPOSER.CHARGE_INDICATOR,
                               ORDERS_DRUG_DECOMPOSER.DISPENSARY,
                               ORDERS_DRUG_DECOMPOSER.PRICE,
                               ORDERS_DRUG_DECOMPOSER.ZB,
                               ORDERS_DRUG_DECOMPOSER.FS,
                               ORDERS_DRUG_DECOMPOSER.MIN_SPEC,
                               ORDERS_DRUG_DECOMPOSER.OPERATOR_DATE,
                               ORDERS_DRUG_DECOMPOSER.DOCTOR,
                               ORDERS_DRUG_DECOMPOSER.ORDERING_DEPT,
                               ORDERS_DRUG_DECOMPOSER.COMMON_FLAG,
                               ORDERS_DRUG_DECOMPOSER.SPECIAL_FLAG,
                               ORDERS_DRUG_DECOMPOSER.WARD_CODE,
                               ORDERS_DRUG_DECOMPOSER.ICU_DEPT_CODE,
                               ORDERS_DRUG_DECOMPOSER.MEMO,
                                ORDERS_DRUG_DECOMPOSER.ORDER_BEGDATE,
                                ORDERS_DRUG_DECOMPOSER.ORDER_ENDDATE
                        )
                        VALUES
                        (
                               {0},'{1}',{2},{3},{4},to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'),
                                {6},'{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}',{15},
                                '{16}','{17}','{18}','{19}',{20},{21},{22},{23},'{24}',{25},
                                {26},{27},'{28}',to_date('{29}', 'yyyy-mm-dd hh24:mi:ss'),
                                '{30}','{31}','{32}','{33}','{34}','{35}','{36}',to_date('{37}', 'yyyy-mm-dd hh24:mi:ss'),to_date('{38}', 'yyyy-mm-dd hh24:mi:ss'))";
            object[] param = new object[] { orderDrug.PRESC_NO, orderDrug.PATIENT_ID, orderDrug.VISIT_ID, 
                orderDrug.ORDER_ID, orderDrug.SUB_ORDER_ID, orderDrug.VISIT_DATE, orderDrug.VISIT_NO, 
                orderDrug.SERIAL_NO, orderDrug.ITEM_NO, orderDrug.ITEM_CLASS, orderDrug.DRUG_CODE, 
                orderDrug.DRUG_NAME, orderDrug.DRUG_SPEC, orderDrug.FIRM_ID, orderDrug.UNITS, orderDrug.AMOUNT, 
                orderDrug.DOSAGE, orderDrug.DOSAGE_UNITS, orderDrug.ADMINISTRATION, orderDrug.FREQUENCY, 
                orderDrug.PROVIDED_INDICATOR, orderDrug.COSTS, orderDrug.CHARGES, orderDrug.CHARGE_INDICATOR, 
                orderDrug.DISPENSARY, orderDrug.PRICE, orderDrug.ZB, orderDrug.FS, 
                orderDrug.MIN_SPEC, orderDrug.OPERATOR_DATE, orderDrug.DOCTOR, orderDrug.ORDERING_DEPT, 
                orderDrug.COMMON_FLAG, orderDrug.SPECIAL_FLAG,orderDrug.WARD_CODE,orderDrug.ICU_DEPT_CODE,orderDrug.Memo,
                orderDrug.ORDER_BEGDATE,orderDrug.ORDER_ENDDATE};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        public int InsertOrdersExecute( HisCommon.DataEntity.DECOMPOSER_ITEM_INFO orderDrug,DateTime performDatetime, BaseEntityer db)
        {
            //2013-11-29 by li 实际ICU开单科室记录
            //           insert into orders_execute
            // (   perform_schedule, perform_result, 
                
            //values
            //  (  v_repeat_indicator, v_order_class, v_order_text, v_order_code, v_dosage, v_dosage_units, v_administration, v_duration, v_duration_units, v_frequency, v_freq_counter, v_freq_interval, v_freq_interval_unit, v_freq_detail, v_perform_schedule, v_perform_result, v_conversion_nurse, v_conversion_date, v_execute_nurse, v_execute_date, v_drug_billing_attr, v_costs, v_is_execute, v_print_flag, v_print_flag1, v_end_nurse, v_end_date, v_check_nurse, v_check_date, v_check_nurse_name, v_confirm_nurse, v_confirm_date, v_confirm_nurse_name, v_order_state, v_ward_code, v_extend1, v_extend2, v_extend3, v_oper_code, v_oper_date, v_create_date, v_orders_finish_date, v_remark, v_stop);

            string sql = @"INSERT INTO ORDERS_EXECUTE
                        (patient_id, visit_id, order_no, order_sub_no, schedule_perform_time
,repeat_indicator,order_class,order_text, order_code,
dosage, dosage_units, administration,duration, duration_units
,frequency ,
order_state, ward_code, extend1, extend2, 
            extend3, oper_code, oper_date, 
            create_date,  
             stop
)
  VALUES
   (
 {0},'{1}',{2},{3},to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')
 , '{5}' , '{6}','{7}','{8}'   
 , '{9}' , '{10}','{11}','{12}' ,'{13}' ,'{14}', '{15}' , '{16}','{17}','{18}' ,'{19}' ,'{20}',to_date('{21}', 'yyyy-mm-dd hh24:mi:ss') ,
to_date('{22}', 'yyyy-mm-dd hh24:mi:ss') ,'{23}'

)
 "; 

            object[] param = new object[] {

                orderDrug.PATIENT_ID, orderDrug.VISIT_ID,
                orderDrug.ORDER_NO, orderDrug.ORDER_SUB_NO,
                performDatetime,orderDrug.REPEAT_INDICATOR,
            orderDrug.ITEM_CLASS,
            orderDrug.ITEM_NAME,
            orderDrug.ITEM_CODE,
            orderDrug.OrdersDrugDecomposer.DOSAGE,
            orderDrug.OrdersDrugDecomposer.DOSAGE_UNITS,
            orderDrug.OrdersDrugDecomposer.ADMINISTRATION,
            "","",orderDrug.FREQUENCY
            ,"1","","","","","",DateTime.Now,DateTime.Now, "1" 
            };
            //orderDrug.VISIT_NO,
            //    orderDrug.SERIAL_NO, orderDrug.ITEM_NO, orderDrug.ITEM_CLASS, orderDrug.DRUG_CODE,
            //    orderDrug.DRUG_NAME, orderDrug.DRUG_SPEC, orderDrug.FIRM_ID, orderDrug.UNITS, orderDrug.AMOUNT,
            //    orderDrug.DOSAGE, orderDrug.DOSAGE_UNITS, orderDrug.ADMINISTRATION, orderDrug.FREQUENCY,
            //    orderDrug.PROVIDED_INDICATOR, orderDrug.COSTS, orderDrug.CHARGES, orderDrug.CHARGE_INDICATOR,
            //    orderDrug.DISPENSARY, orderDrug.PRICE, orderDrug.ZB, orderDrug.FS,
            //    orderDrug.MIN_SPEC, orderDrug.OPERATOR_DATE, orderDrug.DOCTOR, orderDrug.ORDERING_DEPT,
            //    orderDrug.COMMON_FLAG, orderDrug.SPECIAL_FLAG,orderDrug.WARD_CODE,orderDrug.ICU_DEPT_CODE,orderDrug.Memo,
            //    orderDrug.ORDER_BEGDATE,orderDrug.ORDER_ENDDATE
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.NurseINPATIENTINFO GetInpatientByPatientID(string patientId, string visitId)
        {
            HisCommon.DataEntity.NurseINPATIENTINFO nurseInpatientInfo = null;
            string sql = @"select 
                        pih.patient_id,
                        pih.visit_id,
                        pih.ward_code dept_code,
                        pv.charge_type,
                        (select pih.prepayments-nvl(ctd.charge_low,ctd.charge_price)-pih.total_charges
                        from charge_type_dict ctd where ctd.charge_type_code = pv.charge_type_code and rownum = 1) prepayments,
                        pih.total_charges,
                        pih.total_costs
                        from pats_in_hospital pih,pat_visit pv 
                        where pih.patient_id = pv.patient_id and pih.visit_id = pv.visit_id 
                        and pih.patient_id = '{0}' and pih.visit_id = '{1}' 
                        and rownum = 1";
            sql = sql.SqlFormate(patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                nurseInpatientInfo = DataSetToEntity.DataSetToT<HisCommon.DataEntity.NurseINPATIENTINFO>(ds)[0];
            }
            return nurseInpatientInfo;
        }
        /// <summary>
        /// 读取药品的库存信息记录
        /// </summary>
        /// <param name="DRUG_SPEC"></param>
        /// <param name="FIRM_ID"></param>
        /// <param name="DRUG_CODE"></param>
        /// <param name="PERFORMED_BY"></param>
        /// <param name="ITEM_CLASS"></param>
        /// <returns></returns>
        public DataTable GetDrugInfoByDecomposer(string DRUG_SPEC, string FIRM_ID, string DRUG_CODE, string PERFORMED_BY, string ITEM_CLASS)
        {
            //包装规格PACKGE_SPEC
            string sql = @" select * from OUTP_ORDER_LIST o where o.DRUG_SPEC='{0}' and o.FIRM_ID='{1}' 
                          and o.code='{2}' and o.deptcode='{3}' and o.class='{4}'";
            sql = string.Format(sql, DRUG_SPEC, FIRM_ID, DRUG_CODE, PERFORMED_BY, ITEM_CLASS);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        // add by lql 2016-09-07 start 
        /// <summary>
        /// 读取药品的库存信息记录(盐水库,不看厂家)
        /// </summary>
        /// <param name="DRUG_SPEC"></param>
        /// <param name="FIRM_ID"></param>
        /// <param name="DRUG_CODE"></param>
        /// <param name="PERFORMED_BY"></param>
        /// <param name="ITEM_CLASS"></param>
        /// <returns></returns>
        public DataTable GetYsDrugInfoByDecomposer(string DRUG_SPEC, string DRUG_CODE, string PERFORMED_BY, string ITEM_CLASS)
        {
            //包装规格PACKGE_SPEC
            string sql = @" select * from OUTP_ORDER_LIST o where o.DRUG_SPEC='{0}' 
                          and o.code='{1}' and o.deptcode='{2}' and o.class='{3}'";
            sql = string.Format(sql, DRUG_SPEC, DRUG_CODE, PERFORMED_BY, ITEM_CLASS);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 读取未发药的药品申请记录(不看厂家)
        /// </summary>
        /// <param name="DRUG_SPEC"></param>
        /// <param name="FIRM_ID"></param>
        /// <param name="DRUG_CODE"></param>
        /// <param name="PERFORMED_BY"></param>
        /// <param name="ITEM_CLASS"></param>
        /// <returns></returns>
        public List<ORDERS_DRUG_DECOMPOSER> GetYsDrugInfoFromDecomposer(string DRUG_SPEC, string DRUG_CODE, string PERFORMED_BY, string ITEM_CLASS)
        {
            //规格MIN_SPEC
            string sql = @" select * from ORDERS_DRUG_DECOMPOSER o where o.MIN_SPEC='{0}' 
                          and o.DRUG_CODE='{1}' and o.DISPENSARY='{2}' and o.ITEM_CLASS='{3}' and o.CHARGE_INDICATOR='0'";
            sql = string.Format(sql, DRUG_SPEC, DRUG_CODE, PERFORMED_BY, ITEM_CLASS);
            //return BaseEntityer.Db.GetDataTable(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS_DRUG_DECOMPOSER>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DRUG_SPEC"></param>
        /// <param name="FIRM_ID"></param>
        /// <param name="DRUG_CODE"></param>
        /// <param name="PERFORMED_BY"></param>
        /// <param name="ITEM_CLASS"></param>
        /// <returns></returns>
        public List<ORDERS_DRUG> GetYsDrugInfoFromORDERS_DRUG(string DRUG_SPEC, string DRUG_CODE, string PERFORMED_BY, string ITEM_CLASS)
        {
            //规格MIN_SPEC
            string sql = @" select * from ORDERS_DRUG o where o.MIN_SPEC='{0}'
                          and o.DRUG_CODE='{1}' and o.DISPENSARY='{2}' and o.ITEM_CLASS='{3}' and o.CHARGE_INDICATOR='1' and o.Bz = '0'  --药房拒收 标示记录作废 值为 1 正常状态为 0
";
            sql = string.Format(sql, DRUG_SPEC, DRUG_CODE, PERFORMED_BY, ITEM_CLASS);
            //return BaseEntityer.Db.GetDataTable(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS_DRUG>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        //add by lql 2016-09-07 end
        /// <summary>
        /// 读取未发药的药品申请记录
        /// </summary>
        /// <param name="DRUG_SPEC"></param>
        /// <param name="FIRM_ID"></param>
        /// <param name="DRUG_CODE"></param>
        /// <param name="PERFORMED_BY"></param>
        /// <param name="ITEM_CLASS"></param>
        /// <returns></returns>
        public List<ORDERS_DRUG_DECOMPOSER> GetDrugInfoFromDecomposer(string DRUG_SPEC, string FIRM_ID, string DRUG_CODE, string PERFORMED_BY, string ITEM_CLASS)
        {
            //规格MIN_SPEC
            string sql = @" select * from ORDERS_DRUG_DECOMPOSER o where o.MIN_SPEC='{0}' and o.FIRM_ID='{1}' 
                          and o.DRUG_CODE='{2}' and o.DISPENSARY='{3}' and o.ITEM_CLASS='{4}' and o.CHARGE_INDICATOR='0'";
            sql = string.Format(sql, DRUG_SPEC, FIRM_ID, DRUG_CODE, PERFORMED_BY, ITEM_CLASS);
            //return BaseEntityer.Db.GetDataTable(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS_DRUG_DECOMPOSER>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DRUG_SPEC"></param>
        /// <param name="FIRM_ID"></param>
        /// <param name="DRUG_CODE"></param>
        /// <param name="PERFORMED_BY"></param>
        /// <param name="ITEM_CLASS"></param>
        /// <returns></returns>
        public List<ORDERS_DRUG> GetDrugInfoFromORDERS_DRUG(string DRUG_SPEC, string FIRM_ID, string DRUG_CODE, string PERFORMED_BY, string ITEM_CLASS)
        {

            //规格MIN_SPEC
            string sql = @" select * from ORDERS_DRUG o where o.MIN_SPEC='{0}' and o.FIRM_ID='{1}' 
                          and o.DRUG_CODE='{2}' and o.DISPENSARY='{3}' and o.ITEM_CLASS='{4}' and o.CHARGE_INDICATOR='1' and o.Bz = '0'  --药房拒收 标示记录作废 值为 1 正常状态为 0
";
            sql = string.Format(sql, DRUG_SPEC, FIRM_ID, DRUG_CODE, PERFORMED_BY, ITEM_CLASS);
            //return BaseEntityer.Db.GetDataTable(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS_DRUG>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 读取未发药的药品申请记录
        /// </summary>
        /// <returns></returns>
        public List<ORDERS_DRUG_DECOMPOSER> GetUnSedDrugInfoFromDecomposer(string PATIENT_ID, string VISIT_ID)
        {
            string sql = @"select * from ORDERS_DRUG_DECOMPOSER o where o.PATIENT_ID='{0}' and o.VISIT_ID='{1}' and o.CHARGE_INDICATOR='0'";
            sql = string.Format(sql, PATIENT_ID, VISIT_ID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS_DRUG_DECOMPOSER>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }

        /// <summary>
        /// 读取未发药的药品申请记录
        /// </summary>
        /// <returns></returns>
        public decimal GetUnSedDrugCostFromDecomposer(string PATIENT_ID, string VISIT_ID)
        {
            string sql = @"SELECT nvl(SUM(t.charges),0)
                          FROM orders_drug_decomposer t
                         WHERE t.patient_id = '{0}'
                           AND t.visit_id = '{1}'
                           AND t.charge_indicator = '0'";
            sql = string.Format(sql, PATIENT_ID, VISIT_ID);
            return BaseEntityer.Db.ExecuteScalar<decimal>(sql);
        }

        /// <summary>
        /// 得到最大的计费itemNO
        /// </summary>
        /// <param name="Patient_Id"></param>
        /// <param name="Visit_Id"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int GetDECOMPOSERMaxItemNo(string Patient_Id, int Visit_Id, int ORDER_ID, BaseEntityer db)
        {
            int itemNo = 1;
            string sql = @" select max(SUB_ORDER_ID)
                         from ORDERS_DRUG_DECOMPOSER where PATIENT_ID='{0}' and VISIT_ID='{1}' and ORDER_ID='{2}'";
            object[] obs = new object[] 
               {
                Patient_Id,
                Visit_Id,
                ORDER_ID};
            sql = sql.SqlFormate(obs);

            DataTable dt = db.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString().Trim() != "")
                {
                    itemNo = int.Parse(dt.Rows[0][0].ToString()) + 1;
                }
            }
            return itemNo;
        }

        public int InsertFeeReturnApply(BaseEntityer db, HisCommon.DataEntity.FEE_RETURN_APPLY feeReturnObj)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into fee_return_apply
(
APPLY_NO,
EXAMINE_STATE,
PATIENT_ID,
VISIT_ID,
ITEM_NO,
ITEM_CLASS,
ITEM_NAME,
ITEM_CODE,
ITEM_SPEC,
AMOUNT,
UNITS,
ORDERED_BY,
PERFORMED_BY,
COSTS, 
CHARGES,
BILLING_DATE_TIME,
OPERATOR_NO,
DOCTOR,
APPLY_OPER,
APPLY_DEPT,
APPLY_DATE,
EXAMINE_OPER,
EXAMINE_DATE,
PRICE,
MEMO,
SPECIAL_FLAG
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
 '{10}',
 '{11}',
 '{12}',
 '{13}',
 '{14}',
 to_date('{15}', 'yyyy-mm-dd hh24:mi:ss'),
 '{16}',
 '{17}',
 '{18}',
 '{19}',
 to_date('{20}', 'yyyy-mm-dd hh24:mi:ss'),
 '{21}',
 to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'),
 '{23}',
'{24}',
'{25}'
)";
                sql = string.Format(sql, GetFeeReturnParam(feeReturnObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        private string[] GetFeeReturnParam(HisCommon.DataEntity.FEE_RETURN_APPLY feeReturnObj)
        {
            string[] para = new string[]{
                                        feeReturnObj.APPLY_NO,
                                        feeReturnObj.EXAMINE_STATE,
                                        feeReturnObj.PATIENT_ID,
                                        feeReturnObj.VISIT_ID.ToString(),
                                        feeReturnObj.ITEM_NO.ToString(),
                                        feeReturnObj.ITEM_CLASS,
                                        feeReturnObj.ITEM_NAME,
                                        feeReturnObj.ITEM_CODE,
                                        feeReturnObj.ITEM_SPEC,
                                        feeReturnObj.AMOUNT.ToString(),
                                        feeReturnObj.UNITS,
                                        feeReturnObj.ORDERED_BY,
                                        feeReturnObj.PERFORMED_BY,
                                        feeReturnObj.COSTS.ToString(),
                                        feeReturnObj.CHARGES.ToString(),
                                        feeReturnObj.BILLING_DATE_TIME.ToString(),
                                        feeReturnObj.OPERATOR_NO,
                                        feeReturnObj.DOCTOR,
                                        feeReturnObj.APPLY_OPER,
                                        feeReturnObj.APPLY_DEPT,
                                        feeReturnObj.APPLY_DATE.ToString(),
                                        feeReturnObj.EXAMINE_OPER,
                                        feeReturnObj.EXAMINE_DATE.ToString(),
                                        feeReturnObj.PRICE.ToString(),
                                        feeReturnObj.MEMO,
                                        feeReturnObj.SPECIAL_FLAG
            };
            return para;
        }

        public List<HisCommon.DataEntity.FEE_RETURN_APPLY> GetFeeReturnList(string dateBegin, string dateEnd, string deptCode, string amount, bool isApply, string deptClass)
        {
            string sqlWhere = string.Empty;
            string sqlDept = " and (fra.APPLY_DEPT = '" + deptCode + "' or 'All' = '" + deptCode + "')";
            if (!isApply)
            {
                sqlWhere = " and EXAMINE_STATE in ('0','1','4') ";
                if (deptClass == "OrderDept")
                {
                    sqlDept = " and (fra.ORDERED_BY = '" + deptCode + "' or 'All' = '" + deptCode + "')";
                }
                else if (deptClass == "ExecDept")
                {
                    sqlDept = " and (fra.PERFORMED_BY = '" + deptCode + "' or 'All' = '" + deptCode + "')";
                }
            }
            string amountCon = string.Empty;
            if (!string.IsNullOrEmpty(amount))
            {
                string amountCopy =
                    amount.Replace("小于", "<").Replace("大于", ">").Replace("等于", "=").Replace("大于等于", ">=").Replace("小于等于", "<=");
                if (!amountCopy.Contains("|"))
                {
                    amountCon = " and costs" + amountCopy;
                }
                else
                {
                    amountCon = " and (";
                    string[] str = amountCopy.Split('|');
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (i > 0)
                        { amountCon += " and "; }
                        amountCon += " costs" + str[i];
                    }
                    amountCon += ")";
                }
            }
            string sql = @"select 
APPLY_NO,
EXAMINE_STATE,
decode(EXAMINE_STATE,'0','申请','-1','审核未通过','1','审核通过','2','确认退费',3,'超期申请','4','一级审批通过',5,'一级审批未通过','异常') EXAMINE_STATE_NAME,
PATIENT_ID,
VISIT_ID,
(select p.name from pat_master_index p where p.patient_id = fra.patient_id and rownum = 1) PATIENT_NAME ,
ITEM_NO,
ITEM_CLASS,
(select i.type_name from item_type i where i.type_code = fra.ITEM_CLASS and rownum = 1) ITEM_CLASS_NAME,
ITEM_NAME,
ITEM_CODE,
ITEM_SPEC,
AMOUNT,
PRICE,
UNITS,
ORDERED_BY,
(select d.dept_name from dept_dict d where d.dept_code = fra.ORDERED_BY and rownum = 1) ORDERED_BY_NAME,
PERFORMED_BY,
(select d.dept_name from dept_dict d where d.dept_code = fra.PERFORMED_BY and rownum = 1) PERFORMED_BY_NAME,
COSTS, 
CHARGES,
BILLING_DATE_TIME,
OPERATOR_NO,
(select u.user_name from users_staff_dict u where u.user_id = fra.OPERATOR_NO and rownum = 1) OPERATOR_NO_NAME,
DOCTOR,
(select u.user_name from users_staff_dict u where u.user_id = fra.DOCTOR and rownum = 1) DOCTOR_NAME,
APPLY_OPER,
(select u.user_name from users_staff_dict u where u.user_id = fra.APPLY_OPER and rownum = 1) APPLY_OPER_NAME,
APPLY_DEPT,
(select d.dept_name from dept_dict d where d.dept_code = fra.APPLY_DEPT and rownum = 1) APPLY_DEPT_NAME,
APPLY_DATE,
EXAMINE_OPER,
(select u.user_name from users_staff_dict u where u.user_id = fra.EXAMINE_OPER and rownum = 1) EXAMINE_OPER_NAME,
 (select i.item_name from clinic_vs_charge v , clinic_item_dict i where v.clinic_item_code=i.item_code and v.charge_item_code=fra.item_code and rownum=1 ) as CLINIC_ITEM_NAME,
EXAMINE_DATE,
'NoNew' ADD_FLAG,
MEMO
from FEE_RETURN_APPLY fra
where fra.APPLY_DATE >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
and fra.APPLY_DATE <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
and exists (select c.patient_id from    pats_in_hospital  c  where  c.patient_id=fra.patient_id and  c.visit_id=fra.visit_id)
{2} {3} {4} order by PATIENT_ID,BILLING_DATE_TIME";
            sql = string.Format(sql, dateBegin, dateEnd, sqlDept, sqlWhere, amountCon);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.FEE_RETURN_APPLY>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public int DeleteFeeReturn(BaseEntityer db, string applyCode)
        {
            string sql = @"delete from FEE_RETURN_APPLY f where f.APPLY_NO = '{0}'";
            sql = string.Format(sql, applyCode);
            return db.ExecuteNonQuery(sql);
        }

        public int UpdateFeeReturnState(BaseEntityer db, string applyCode, string state, string operCode, string operDate)
        {
            int exec = 0;
            try
            {
                string sql = @"update FEE_RETURN_APPLY set EXAMINE_STATE = '{1}',EXAMINE_OPER = '{2}',EXAMINE_DATE = to_date('{3}', 'yyyy-mm-dd hh24:mi:ss')
where APPLY_NO = '{0}'";
                sql = string.Format(sql, applyCode, state, operCode, operDate);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        public List<HisCommon.DataEntity.INP_BILL_DETAIL> GetNoReturnInpBillDetailFlagList(string Patient_Id, int Visit_Id, string amount, string examClass, string deptCode, DateTime validTime)
        {
            //string sqlWhere = " (select count(*) from price_list p where p.item_class = i.item_class and p.item_code = i.item_code and p.item_spec = i.item_spec and p.units = i.units and p.special_flag = '1')>0 ";

            string sqlWhere = " (((select count(*) from price_list p where p.item_class = i.item_class and p.item_code = i.item_code and p.item_spec = i.item_spec and p.units = i.units and p.special_flag = '1') > 0  and i.billing_date_time >=to_date('" + validTime + "', 'yyyy-mm-dd hh24:mi:ss')) or(i.billing_date_time <to_date('" + validTime + "', 'yyyy-mm-dd hh24:mi:ss')))";

            string amountCon = string.Empty;
            if (!string.IsNullOrEmpty(amount))
            {
                string amountCopy =
                    amount.Replace("小于", "<").Replace("大于", ">").Replace("等于", "=").Replace("大于等于", ">=").Replace("小于等于", "<=");
                if (!amountCopy.Contains("|"))
                {
                    amountCon = " and CHARGES" + amountCopy;
                }
                else
                {
                    amountCon = " and (";
                    string[] str = amountCopy.Split('|');
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (i > 0)
                        { amountCon += " and "; }
                        amountCon += " CHARGES" + str[i];
                    }
                    amountCon += ")";
                }
            }

            if (examClass == "UnLocalDept")
            {
                sqlWhere = " ORDERED_BY  != '" + deptCode + "' ";
            }

            if (examClass == "All")
            {
                sqlWhere += " or ORDERED_BY  != '" + deptCode + "' ";
                sqlWhere = "(" + sqlWhere + ")";
            }

            sqlWhere = " and " + sqlWhere;

            //2013-11-29 by li 实际ICU开单科室记录
            string sql = @"SELECT patient_id,
                               visit_id,
                               item_no,
                               item_class,
                               item_name,
                               f_trans_pinyin_capital(item_name) AS pinyin,
                               item_code,
                               item_spec,
                               amount,
                               units,
                               ordered_by,
                               performed_by,
                               costs,
                               charges,
                               billing_date_time,
                               operator_no,
                               rcpt_no,
                               up_flag,
                               up_time_date,
                               up_operator_no,
                               formularyno,
                               doctor,
                               checkflag,
                               subj_code,
                               class_on_inp_rcpt,
                               class_on_mr,
                               class_on_reckoning,
                               orders_no,
                               return_num,
                               return_flag,
                               (SELECT d.dept_name FROM dept_dict d WHERE d.dept_code = ordered_by) AS ordered_by_name,
                               (SELECT d.dept_name FROM dept_dict d WHERE d.dept_code = performed_by) AS performed_by_name,
                               (SELECT s.user_name
                                  FROM users_staff_dict s
                                 WHERE s.user_id = operator_no) AS operator_no_name,
                               (SELECT s.user_name FROM users_staff_dict s WHERE s.user_id = doctor) AS doctor_name,
                               common_flag,
                               special_flag,
                               price,
                               clinic_item_class,
                               clinic_item_code || '@' ||
                               (SELECT nvl(m.item_name, ' ') NAME
                                  FROM clinic_item_name_dict m
                                 WHERE m.item_code = i.clinic_item_code
                                   AND m.item_class NOT IN ('A', 'B')
                                   AND m.item_class = i.clinic_item_class
                                   AND m.std_indicator = '1') clinic_item_code,
                               out_no,
                               icu_dept_code,
                               memo
                          FROM inp_bill_detail i
                         WHERE (return_flag = '0' OR return_flag IS NULL)
                           AND item_class NOT IN ('A', 'B')
                           AND amount > 0
                           AND patient_id = '{0}'
                           AND visit_id = '{1}' {2}
                           AND patient_id || visit_id || item_no NOT IN
                               (SELECT patient_id || visit_id || item_no
                                  FROM fee_return_apply
                                 WHERE patient_id = '{0}'
                                   AND visit_id = '{1}') {3}
                         ORDER BY billing_date_time
                        ";
            object[] obs = new object[] 
               {
                Patient_Id,
                Visit_Id,
                amountCon,
                sqlWhere
               };
            sql = sql.SqlFormate(obs);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.INP_BILL_DETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 根据诊疗项目分组显示
        /// Add By ZhanGD 2014-07-10
        /// </summary>
        /// <param name="Patient_Id"></param>
        /// <param name="Visit_Id"></param>
        /// <param name="amount"></param>
        /// <param name="examClass"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.INP_BILL_DETAIL> GetNoReturnInpBillDetailFlagListGroupByClinicCode(string Patient_Id, int Visit_Id, string amount, string examClass, string deptCode, DateTime validTime)
        {
            List<HisCommon.DataEntity.INP_BILL_DETAIL> lstDetail = new List<INP_BILL_DETAIL>();

            string strSQL = string.Empty;
            strSQL = @" SELECT t.patient_id,
       t.visit_id,
       SUM(t.costs) costs,
       SUM(t.amount) cnt,
       t.orders_no,
       t.clinic_item_class,
       t.clinic_item_code,
       m.item_name,
       f_trans_pinyin_capital(m.item_name) AS pinyin,
       t.memo,
       t.ordered_by,
       (SELECT d.dept_name FROM dept_dict d WHERE d.dept_code = ordered_by) AS ordered_by_name,
       t.special_flag 
  FROM inp_bill_detail       t,
       clinic_item_name_dict m
 WHERE (t.return_flag = '0' OR t.return_flag IS NULL)
   AND t.item_class NOT IN ('A', 'B')
   AND t.amount > 0
   AND t.clinic_item_code = m.item_code
   AND t.
 patient_id = '{0}'
   AND t.visit_id = '{1}'
   AND t.patient_id || t.visit_id || t.item_no NOT IN
       (SELECT patient_id || visit_id || item_no
          FROM fee_return_apply
         WHERE patient_id = '{0}'
           AND visit_id = '{1}')
   AND (((SELECT COUNT(*)
            FROM price_list p
           WHERE p.item_class = t.item_class
             AND p.item_code = t.item_code
             AND p.item_spec = t.item_spec
             AND p.units = t.units
             AND p.special_flag = '1') > 0 AND
       t.billing_date_time >= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'))
       
       OR (t.billing_date_time < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')))
 GROUP BY t.patient_id,
          t.clinic_item_code,
          m.item_name,
          t.orders_no,
          t.clinic_item_class,
          t.visit_id,
          t.memo,
          t.ordered_by,
          special_flag
 ORDER BY t.orders_no
";
            strSQL = string.Format(strSQL, Patient_Id, Visit_Id, validTime);

            DbDataReader dr = BaseEntityer.Db.ExecuteReader(strSQL, 0);
            while (dr.Read())
            {
                HisCommon.DataEntity.INP_BILL_DETAIL detail = new INP_BILL_DETAIL();
                detail.PATIENT_ID = dr[0].ToString();
                detail.VISIT_ID = Convert.ToInt32(dr[1].ToString());
                detail.COSTS = Convert.ToDecimal(dr[2].ToString());
                detail.AMOUNT = Convert.ToDecimal(dr[3].ToString());
                detail.ORDERS_NO = dr[4].ToString();
                detail.CLINIC_ITEM_CLASS = dr[5].ToString();
                detail.CLINIC_ITEM_CODE = dr[6].ToString();
                detail.ITEM_NAME = dr[7].ToString();
                detail.PinYin = dr[8].ToString();
                detail.Memo = dr[9].ToString();
                detail.ORDERED_BY = dr[10].ToString();
                detail.Ordered_by_name = dr[11].ToString();// 科室名称
                detail.Special_flag = dr[12].ToString();// 药品限制属性
                lstDetail.Add(detail);
            }
            if (!dr.IsClosed)
                dr.Close();
            return lstDetail;
        }

        /// <summary>
        /// 根据诊疗项目编码和日期查询明细
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <param name="order_no"></param>
        /// <param name="item_no"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.INP_BILL_DETAIL> GetInBillDetail(string patient_id, string visit_id, string order_no, string item_no)
        {
            string strSQL = string.Empty;
            strSQL = @" select PATIENT_ID,
                               VISIT_ID,
                               ITEM_NO,
                               ITEM_CLASS,
                               ITEM_NAME,
                               ITEM_CODE,
                               ITEM_SPEC,
                               AMOUNT,
                               UNITS,
                               ORDERED_BY,
                               PERFORMED_BY,
                               COSTS,
                               CHARGES,
                               BILLING_DATE_TIME,
                               OPERATOR_NO,
                               RCPT_NO,
                               UP_FLAG,
                               UP_TIME_DATE,
                               UP_OPERATOR_NO,
                               FORMULARYNO,
                               DOCTOR,
                               CHECKFLAG,
                               SUBJ_CODE,
                               CLASS_ON_INP_RCPT,
                               CLASS_ON_MR,
                               CLASS_ON_RECKONING,
                               ORDERS_NO,
                               RETURN_NUM,
                               RETURN_FLAG,
                               fun_getdeptname(ORDERED_BY) Ordered_by_name,
                               fun_getdeptname(PERFORMED_BY) Performed_by_name,
                               fun_getusername(OPERATOR_NO) Operator_no_name,
                               fun_getusername(DOCTOR) Doctor_name,
                               COMMON_FLAG,
                               SPECIAL_FLAG,
                               PRICE,
                               CLINIC_ITEM_CLASS,
                               CLINIC_ITEM_CODE || '@' ||
                               (select nvl(m.item_name, ' ') name
                                  from clinic_item_name_dict m
                                 where m.item_code = t.clinic_item_code
                                   and m.ITEM_CLASS not in ('A', 'B')
                                   and m.item_class = t.clinic_item_class
                                   and m.std_indicator = '1') CLINIC_ITEM_CODE,
                               OUT_NO,
                               ICU_DEPT_CODE
                          from INP_BILL_DETAIL t
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}'
                           and t.orders_no = '{2}'
                           and t.clinic_item_code = '{3}' ";
            strSQL = string.Format(strSQL, patient_id, visit_id, order_no, item_no);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.INP_BILL_DETAIL>(BaseEntityer.Db.GetDataSet(strSQL)).ToList();
        }

        public int GetSpecialFlagNum(string itemClass, string itemCode, string itemSpec, string units)
        {
            string sql = @"select count(*) from price_list pl where
  pl.item_class = '{0}' and pl.item_code = '{1}' and
  pl.item_spec = '{2}' and pl.units = '{3}' and pl.special_flag = '1'";
            sql = string.Format(sql, itemClass, itemCode, itemSpec, units);
            object obj = BaseEntityer.Db.ExecuteScalar(sql);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            else
            {
                return 0;
            }
        }

        public int GetFeeReturnApplyNum(string Patient_Id, int Visit_Id, string Item_No, bool isAll)
        {
            string whereSpl = " and examine_state in ('0','-1','3') ";
            if (isAll)
            {
                whereSpl = string.Empty;
            }
            string sql = @"select count(*) from  FEE_RETURN_APPLY
 where patient_id = '{0}' and visit_id = '{1}' {3}
 and (item_no = '{2}' or 'All' = '{2}')";
            sql = string.Format(sql, Patient_Id, Visit_Id, Item_No, whereSpl);
            object obj = BaseEntityer.Db.ExecuteScalar(sql);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            else
            {
                return 0;
            }
           
        }
        public List<HisCommon.DataEntity.FEE_RETURN_APPLY> GetFeeReturnApply(string Patient_Id, int Visit_Id, string Item_No, bool isAll)
        {
            string whereSpl = " and examine_state in ('0','-1','3') ";
            if (isAll)
            {
                whereSpl = string.Empty;
            }
            string sql = @"select * from  FEE_RETURN_APPLY
 where patient_id = '{0}' and visit_id = '{1}' {3}
 and (item_no = '{2}' or 'All' = '{2}')";
            sql = string.Format(sql, Patient_Id, Visit_Id, Item_No, whereSpl);

            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.FEE_RETURN_APPLY>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 判断是否存在eaxm
        /// </summary>
        /// <param name="db"></param>
        /// <param name="examNo"></param>
        /// <returns></returns>
        public List<EXAM_MASTER> GetExamMaster(BaseEntityer db, string examNo)
        {
            string sql = @"select *  from EXAM_MASTER where exam_no = {0}";
            sql = string.Format(sql, examNo);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXAM_MASTER>(db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 是否进行手术接诊标识,0为未接诊,1为已接诊(yx)
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <param name="VISIT_ID"></param>
        /// <returns></returns>
        public List<PATS_IN_HOSPITAL> GetPatsInHospital(string PATIENT_ID, string VISIT_ID)
        {
            string sql = @" select * from PATS_IN_HOSPITAL o where o.PATIENT_ID='{0}' and o.VISIT_ID='{1}'";
            sql = string.Format(sql, PATIENT_ID, VISIT_ID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PATS_IN_HOSPITAL>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 读取分解的药品信息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="WARD_CODE"></param>
        /// <returns></returns>
        public List<ORDERS_DRUG_DECOMPOSER> GetAllDrugInfoFromDecomposer(string startDate, string EndDate, string WARD_CODE)
        {
            string sql = @"select * from ORDERS_DRUG_DECOMPOSER d,pat_master_index p where d.patient_id=p.patient_id and 
            d.operator_date >= to_date('{0}','yyyy-MM-dd hh24:mi:ss')
            AND d.operator_date < to_date('{1}','yyyy-MM-dd hh24:mi:ss')
            and d.ward_code='{2}'";
            sql = string.Format(sql, startDate, EndDate, WARD_CODE);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS_DRUG_DECOMPOSER>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 转科判断是否有未发药的记录。
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<ORDERS_DRUG_DECOMPOSER> GetUnSedDrugInfoByTransfer(string deptCode, string PATIENT_ID, string VISIT_ID)
        {
            string sql = @" select * from ORDERS_DRUG_DECOMPOSER o where o.ordering_dept='{0}' and o.PATIENT_ID='{1}' and o.VISIT_ID='{2}'  and o.CHARGE_INDICATOR='0'";
            sql = string.Format(sql, deptCode, PATIENT_ID, VISIT_ID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.ORDERS_DRUG_DECOMPOSER>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }
        /// <summary>
        /// 通过可是编码查询是否是四平的ICU LQL
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.DEPT_DICT GetIsIcuDeptByDeptID(string deptCode)
        {
            HisCommon.DataEntity.DEPT_DICT IsIcuDeptInfo = null;
            string sql = @"select * from DEPT_DICT  where  DEPT_CODE = '{0}'";
            sql = sql.SqlFormate(deptCode);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsIcuDeptInfo = DataSetToEntity.DataSetToT<HisCommon.DataEntity.DEPT_DICT>(ds)[0];
            }
            return IsIcuDeptInfo;
        }
        /// <summary>
        /// 如果是icu的科室的话查找来源科室 lql。
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.PAT_VISIT GetFromDeptByPatientID(string patientId, string visitId)
        {
            HisCommon.DataEntity.PAT_VISIT FromDeptInfo = null;
            string sql = @"select * from pat_visit p where p.patient_id = '{0}' and p.visit_id = '{1}' ";
            sql = sql.SqlFormate(patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                FromDeptInfo = DataSetToEntity.DataSetToT<HisCommon.DataEntity.PAT_VISIT>(ds)[0];
            }
            return FromDeptInfo;
        }
        /// <summary>
        /// 更新来源科室
        /// </summary>
        /// <param name="patVisit"></param>
        /// <param name="Db"></param>
        public void UpatePatVisitFromDept(HisCommon.DataEntity.PAT_VISIT patVisit, BaseEntityer Db)
        {
            string sql = @"update PAT_VISIT set FROM_DEPT='{0}'  where PATIENT_ID='{1}' and VISIT_ID='{2}'";
            sql = string.Format(sql, patVisit.FROM_DEPT, patVisit.PATIENT_ID, patVisit.VISIT_ID);
            Db.ExecuteNonQuery(sql);

            string sql1 = @" INSERT INTO changeFrom_log(PATIENT_ID , VISIT_ID , FROM_DEPT ,  CHANGEFROM_USER ,  CHANGEFROM_DATE
                         ) VALUES ('{0}','{1}', '{2}', '{3}',sysdate)";
            object[] obs = new object[] 
               {patVisit.PATIENT_ID ,
                patVisit.VISIT_ID ,
                patVisit.FROM_DEPT ,
                patVisit.ChangeFrom_User};
            sql1 = sql1.SqlFormate(obs);
            Db.ExecuteNonQuery(sql1);
        }
        /// <summary>
        /// 手术室
        /// </summary>
        /// <returns></returns>
        public DataTable GetOperationDept()
        {
            string sql = @"   SELECT  DEPT_DICT.DEPT_CODE ,           DEPT_DICT.DEPT_NAME     FROM DEPT_DICT      WHERE ( dept_dict.clinic_attr = 1 ) And
  ( dept_dict.outp_or_inp = 1 ) And  ( dept_dict.internal_or_sergery = 1 )  ";
            //sql = string.Format(sql, WARD_CODE);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 康复科主任--统计未分配的治疗项目
        /// </summary>
        /// <returns></returns>
        public DataTable GetPatientKf()
        {
            string sql = @"  SELECT PATIENT_ID,  
          ITEM_NAME,  
          ITEM_CODE,  
          AMOUNT,  
          ORDERED_BY,  
         
          CHARGES,  
          BILLING_DATE_TIME,  
         
          DOCTOR,  item_class,item_no,visit_id,
        
          DOCTOR_KF,  
         (select name from pat_master_index where patient_id = v.patient_id) name    FROM  INP_BILL_MASTER_KF  v where v.doctor_kf is null";
            return BaseEntityer.Db.GetDataTable(sql);

        }

        public DataTable GetPatientKfOld(string startDate, string EndDate)
        {
            string sql = @"  SELECT PATIENT_ID,  
          ITEM_NAME,  
          ITEM_CODE,  
          AMOUNT,  
          ORDERED_BY,  
          CHARGES,  
          BILLING_DATE_TIME,  
          DOCTOR,  item_class,item_no,visit_id,
          DOCTOR_KF,  
          (select name from pat_master_index where patient_id = v.patient_id) name    FROM  INP_BILL_MASTER_KF  v where v.doctor_kf is not null and v .BILLING_DATE_TIME >= to_date('{0}','yyyy-MM-dd hh24:mi:ss')
            AND v .BILLING_DATE_TIME < to_date('{1}','yyyy-MM-dd hh24:mi:ss')";
            object[] obj =
            new object[] {startDate,
                EndDate };

            sql = sql.SqlFormate(obj);

            return BaseEntityer.Db.GetDataTable(sql);

        }

        public void UpdatePatientKf(string doctorkf, string pid, string vid, string item_code, string item_no, string item_class)
        {
            string sql = @" update inp_bill_master_kf set doctor_kf='{0}',update_date = sysdate
where patient_id='{1}' and visit_id='{2}' and item_code='{3}' and item_no='{4}' and  item_class='{5}' ";
            object[] obs = new object[] { doctorkf, pid, vid, item_code, item_no, item_class };
            sql = sql.SqlFormate(obs);
            BaseEntityer.Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 查询药品分解明细
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="bd"></param>
        /// <param name="ed"></param>
        /// <returns></returns>
        public List<ORDERS_DRUG_DECOMPOSER> Query_DrugDecomposer(string patientid, string visitid, string bd, string ed)
        {
            string sql = @"select item_class,drug_code,patient_id
,drug_name,drug_spec,firm_id,
units,amount,administration,visit_id,order_id,sub_order_id,
charges,operator_date,doctor,
decode(charge_indicator,'0','未取药','2','已发药','3','作废','4','拒发') charge_indicator,
ordering_dept,fs,dispensary from  orders_drug_decomposer  where 
patient_id in ({0})  and visit_id in   ({1})  and operator_date >=to_date('{2}','yyyy-MM-dd hh24:mi:ss')
and operator_date <=to_date('{3}','yyyy-MM-dd hh24:mi:ss')
";
            object[] obj =
             new object[] {patientid,
                visitid,
                bd,
                ed};

            sql = sql.SqlFormate(obj);

            return DataSetToEntity.DataSetToT<ORDERS_DRUG_DECOMPOSER>(BaseEntityer.Db.GetDataSet(sql)).ToList();


        }
        /// <summary>
        /// 修改医嘱分解明细的状态
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="Db"></param>
        public void UpateDecomposeState(HisCommon.DataEntity.ORDERS_DRUG_DECOMPOSER orders, BaseEntityer Db)
        {
            string sql = @"update orders_drug_decomposer Set   
nurse ='{0}', 
perform_time =to_date('{5}',
'yyyy-MM-dd hh24:mi:ss'),
charge_indicator='3'
                   where patient_id ='{1}' and visit_id ='{2}' and Order_id ='{3}' and sub_order_id ='{4}' ";
            sql = string.Format(sql, orders.NURSE, orders.PATIENT_ID, orders.VISIT_ID, orders.ORDER_ID, orders.SUB_ORDER_ID, orders.PERFORM_TIME);
            Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取患者打印输液标签
        /// </summary>
        /// <param name="pateint_id"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="printFlag"></param>
        /// <param name="deptCode"></param>
        /// <param name="isTransfusioCard">是否输液卡</param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.ORDERS_DRUG_DECOMPOSER> QueryPatientPrintLabel(string pateint_id, DateTime dtBegin, DateTime dtEnd, string printFlag, string deptCode,bool isTransfusioCard)
        {
            List<HisCommon.DataEntity.ORDERS_DRUG_DECOMPOSER> lstDetail = new List<ORDERS_DRUG_DECOMPOSER>();
            string strSQL = string.Empty;
            #region SQL
            strSQL = @" SELECT t.VISIT_DATE, --
                               t.VISIT_NO, --
                               (select u.name
                                  from PAT_MASTER_INDEX u
                                 where u.patient_id = t.patient_id) SERIAL_NO, --
                               t.PRESC_NO, --
                               t.ITEM_NO, --
                               t.ITEM_CLASS, --
                               t.DRUG_CODE, --
                               t.DRUG_NAME, --
                               t.DRUG_SPEC, --
                               t.FIRM_ID, --
                               t.UNITS, --
                               t.AMOUNT, --
                               t.DOSAGE, --
                               t.DOSAGE_UNITS, --
                               t.ADMINISTRATION, --
                               t.FREQUENCY, --
                               t.PROVIDED_INDICATOR, --
                               t.COSTS, --
                               t.CHARGES, --
                               t.CHARGE_INDICATOR, --
                               t.DISPENSARY, --
                               t.PRESC_CLASS, --
                               t.PRICE, --
                               t.ZB, --
                               t.FS, --
                               t.MIN_SPEC, --
                               t.OPERATOR_DATE, --
                               t.PATIENT_ID, --
                               t.VISIT_ID, --
                               t.ORDER_ID, --
                               t.SUB_ORDER_ID, --
                               t.DOCTOR, --
                               t.ORDERING_DEPT, --
                               t.NURSE, --
                               t.PERFORM_TIME, --
                               t.COMMON_FLAG, --
                               t.SPECIAL_FLAG, --
                               t.WARD_CODE, --
                               t.ICU_DEPT_CODE, --患者转移到ICU疗区对应的科室代码
                               t.PRINTFLAG ,--标志是否已打印过输液标签
                               t.freqprect, -- 打印的次数
                               t.PRINTFLAG1 ,--标志是否已打印过输液标卡
                               t.freqprect1, -- 打印的次数输液标卡
                               t.Memo
                          FROM  v_ORDERS_DRUG_DECOMPOSER t,
                               (select * from ord_usage o where o.dept_code = '{4}') m
                         where t.patient_id in ({0})
                           and t.operator_date between to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and
                               to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
                           and t.administration =
                               (select n.administration_name
                                  from ADMINISTRATION_DICT n
                                 where n.administration_code = m.usage_id) ";

            string strWhere = string.Empty;
            if (printFlag == "0")
            {
                if (isTransfusioCard)
                {
                    strWhere = @" and (nvl(t.printflag1, 0) = '0'  or (t.printflag1<>'0' and  substr(t.freqprect1,instr(t.freqprect1,'/',1,1)+1)<>substr(t.freqprect1,0,instr(t.freqprect1,'/',1,1)-1)))  order by t.operator_date, t.VISIT_DATE, t.order_id, t.sub_order_id";
                }
                else
                    strWhere = @" and (nvl(t.printflag, 0) = '0'  or (t.printflag<>'0' and  substr(t.freqprect,instr(t.freqprect,'/',1,1)+1)<>substr(t.freqprect,0,instr(t.freqprect,'/',1,1)-1)))  order by t.operator_date,t.VISIT_DATE, t.order_id, t.sub_order_id";
            }
            else
            {
                if(isTransfusioCard)
                    strWhere = @"  and  t.printflag1<>'0'   order by t.operator_date, t.VISIT_DATE, t.order_id, t.sub_order_id";
                else
                    strWhere = @"  and  t.printflag<>'0'   order by t.operator_date, t.VISIT_DATE, t.order_id, t.sub_order_id";
            }
            #endregion

            strSQL = string.Format(strSQL + strWhere, pateint_id, dtBegin.ToString(), dtEnd.ToString(), printFlag, deptCode);

            return DataSetToEntity.DataSetToT<ORDERS_DRUG_DECOMPOSER>(BaseEntityer.Db.GetDataSet(strSQL)).ToList();
        }

        /// <summary>
        ///  查询用法
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BringSpringObject> QueryUsage(string deptCode)
        {
            List<HisCommon.DataEntity.BringSpringObject> lstDetail = new List<HisCommon.DataEntity.BringSpringObject>();
            string strSQL = string.Empty;
            strSQL = @" select t.SERIAL_NO, --序号
                               t.ADMINISTRATION_CODE, --给药途径代码
                               t.ADMINISTRATION_NAME, --给药途径名称
                               t.INPUT_CODE --输入码
                          from ADMINISTRATION_DICT t
                         where t.administration_code not in
                               (select m.usage_id from ORD_USAGE m where m.dept_code = '{0}') ";
            strSQL = string.Format(strSQL, deptCode);

            DbDataReader dr = BaseEntityer.Db.ZDExecReader(strSQL);
            while (dr.Read())
            {
                HisCommon.DataEntity.BringSpringObject detail = new BringSpringObject();
                detail.Name = dr[1].ToString();
                detail.Memo = dr[2].ToString();
                lstDetail.Add(detail);
            }
            if (!dr.IsClosed)
                dr.Close();
            return lstDetail;
        }

        /// <summary>
        /// 根据科室查询用法
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BringSpringObject> QueryDeptUsage(string deptCode)
        {
            List<HisCommon.DataEntity.BringSpringObject> lstDetail = new List<HisCommon.DataEntity.BringSpringObject>();
            string strSQL = string.Empty;
            strSQL = @" SELECT t.DEPT_CODE, --科室编码
                               t.usage_id, --用法编码
                               m.administration_name --用法名称
                          FROM ORD_USAGE t, ADMINISTRATION_DICT m
                         WHERE t.dept_code = '{0}'
                           and t.usage_id = m.administration_code ";
            strSQL = string.Format(strSQL, deptCode);

            DbDataReader dr = BaseEntityer.Db.ZDExecReader(strSQL);
            while (dr.Read())
            {
                HisCommon.DataEntity.BringSpringObject detail = new BringSpringObject();
                detail.ID = dr[0].ToString();
                detail.Name = dr[1].ToString();
                detail.Memo = dr[2].ToString();
                lstDetail.Add(detail);
            }
            if (!dr.IsClosed)
                dr.Close();
            return lstDetail;
        }

        /// <summary>
        /// 保存科室用法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertUsage(HisCommon.DataEntity.BringSpringObject obj)
        {
            string strSQL = string.Empty;
            strSQL = @" INSERT into ORD_USAGE t --科室常用用法
                          (t.DEPT_CODE, --科室编码
                           t.USAGE_ID --用法编码
                           )
                        VALUES
                          ('{0}', --科室编码
                           '{1}' --用法编码
                           ) ";
            strSQL = string.Format(strSQL, obj.ID, obj.Name);
            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 删除科室用法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int DeleteUsage(HisCommon.DataEntity.BringSpringObject obj)
        {
            string strSQL = string.Empty;
            strSQL = @" DELETE ORD_USAGE t --科室常用用法
                     WHERE t.dept_code = '{0}'
                       and t.usage_id = '{1}' ";
            strSQL = string.Format(strSQL, obj.ID, obj.Name);
            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 打印后更新打印标识
        /// </summary>
        /// <param name="print_flag"></param>
        /// <param name="patient_id"></param>
        /// <param name="drug_code"></param>
        /// <param name="order_id"></param>
        /// <param name="dtOperDate"></param>
        /// <param name="isTransfusionCard"></param>
        /// <param name="printNum"></param>
        /// <returns></returns>
        public int UpdatePrintLabelFlag(string print_flag, string patient_id, string drug_code, string order_id, DateTime dtOperDate, string printNum,bool isTransfusionCard)
        {
            string strSQL = string.Empty;
            if (isTransfusionCard)
                strSQL = @"update ORDERS_DRUG_DECOMPOSER t
                           set t.printflag1 = '{0}',t.freqprect1='{5}'
                         where t.patient_id = '{1}'
                           and t.drug_code = '{2}'
                           and t.order_id = '{3}'
                           and t.operator_date = to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')";
            else
                strSQL = @" update ORDERS_DRUG_DECOMPOSER t
                           set t.printflag = '{0}',t.freqprect='{5}'
                         where t.patient_id = '{1}'
                           and t.drug_code = '{2}'
                           and t.order_id = '{3}'
                           and t.operator_date = to_date('{4}', 'yyyy-mm-dd hh24:mi:ss') ";
            strSQL = string.Format(strSQL, print_flag, patient_id, drug_code, order_id, dtOperDate, printNum);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 查询住院费用信息是否已经退费
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="itemNO"></param>
        /// <returns></returns>
        public int QueryInpBillItemIsReturn(string patientID, string visitID, string itemNO)
        {
            string sql = @"SELECT  count(*) 
  FROM   fee_return_apply r
 WHERE r.patient_id = '{0}'
   AND r.visit_id = '{1}'
   AND r.item_no = '{2}'
  
";
            sql = string.Format(sql, patientID, visitID, itemNO);

            return BaseEntityer.Db.ExecuteScalar<int>(sql);
        }

        #region 2014年8月29日 处理停药的医嘱方法

        /// <summary>
        ///  处理停药的医嘱信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="orderNO"></param>
        /// <param name="orderSubNO"></param>
        /// <returns></returns>
        public int ProcessStopDrugOrder(string patientID, string visitID, string orderNO, string orderSubNO)
        {
            string strSQL = string.Empty;
            strSQL = @" UPDATE orders t
                   SET t.stop_date_time = t.start_date_time - 1,
                       t.order_status   = '3'
                 WHERE t.patient_id = '{0}'
                   AND t.visit_id = '{1}'
                   AND t.order_no = '{2}'
                   AND t.order_sub_no = '{3}'
                    ";
            strSQL = string.Format(strSQL, patientID, visitID, orderNO, orderSubNO);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 查询未打印的检验患者基本信息
        /// </summary>
        /// <param name="curDeptCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable QueryUnPrintLabSheetPatientInfo(string curDeptCode, ref string errMsg)
        {
            try
            {
                string strSQL = @"
            
SELECT t.name AS 姓名,
       t.visit_id AS 住院次数,
       t.sex AS 性别,
       t.age AS 年龄,
       t.patient_id AS 患者id,
       t.test_no AS 申请序号,
       t.specimen AS 样本,
       (SELECT r.bed_no
          FROM pats_in_hospital r
         WHERE r.patient_id = m.patient_id
           AND r.visit_id = m.visit_id) AS 床号,
          t.ORDERING_PROVIDER as  申请医生
  FROM lab_test_master t,
       pat_visit       m
 WHERE t.patient_id = m.patient_id
   AND t.visit_id = m.visit_id
   AND (t.print_indicator IS NULL OR t.print_indicator = '0')
   AND t.ordering_dept = '{0}'
   AND m.state = 'I'
                    ";
                strSQL = string.Format(strSQL, curDeptCode);

                return BaseEntityer.Db.GetDataTable(strSQL);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 查询已经打印的申请单患者信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable QueryPrintedLabSheetPatientInfo(string patientID, string visitID, ref string errMsg)
        {
            try
            {
                string strSQL = @"
            SELECT t.name       AS 姓名,
       t.visit_id   AS 住院次数,
       t.sex        AS 性别,
       t.age        AS 年龄,
       t.patient_id AS 患者id,
       t.test_no    AS 申请序号,
       t.specimen   AS 样本,
       t.SPCM_RECEIVED_DATE_TIME as 打印日期
  FROM lab_test_master t

 WHERE t.patient_id = '{0}'
   AND t.visit_id = '{1}'
   AND (t.print_indicator = '1')
                    ";
                strSQL = string.Format(strSQL, patientID, visitID);

                return BaseEntityer.Db.GetDataTable(strSQL);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 查询检验的申请单信息，根据患者信息
        /// </summary>
        /// <param name="applyNO">申请单信息</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable QueryUnPrintLabApplySheet(string applyNO, ref string errMsg)
        {
            try
            {
                string strSQL = @" SELECT r.item_no AS 项目顺序号,
                       r.item_name AS 项目名称,
                       r.item_code AS 项目编码,
                       (SELECT item_subclass
                          FROM clinic_item_dict a
                         WHERE a.item_code = r.item_code
                           AND a.item_class = 'C') AS 项目分类
                  FROM lab_test_items r
                 WHERE r.test_no = '{0}'

                    ";
                strSQL = string.Format(strSQL, applyNO);
                return BaseEntityer.Db.GetDataTable(strSQL);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 更新打印标记
        /// </summary>
        /// <param name="visitID"></param>
        /// <param name="patientID"></param>
        /// <param name="applyNo"></param>
        /// <returns></returns>
        public int UpdateLabPrintFlagInfo(string visitID, string patientID, string applyNo, ref string errMsg)
        {
            try
            {
                string strSQL = string.Empty;
                strSQL = @" UPDATE lab_test_master
   SET print_indicator         = '1', --打印标志
       spcm_received_date_time = SYSDATE --采样时间
 WHERE visit_id = '{0}' --本次住院标识
   AND patient_id = '{1}' --病人标识号
   AND test_no = '{2}' --申请序号
                    ";
                strSQL = string.Format(strSQL, visitID, patientID, applyNo);

                return BaseEntityer.Db.ZDExecNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                throw;
            }
        }
        #endregion


        #region 2015年3月19日超过退费有效时间，申请业务处理

        /// <summary>
        /// 根据时间范围判断需要待审核退费药品
        /// </summary>
        /// <param name="state"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IList<DRUG_DISPENSE_REGRET_REQ> QueryWaitApproveDrugLst(string state, DateTime beginDate, DateTime endDate)
        {
            string strSql = @"SELECT
                                t.MEMO,   --医嘱备注
                                t.DEPT_CODE,   --护理单元
                                t.ORDER_SUB_NO,   --
                                t.ORDER_NO,   --
                                t.BATCH_NO,   --
                                t.DISPENSING_DATE_TIME,   --
                                t.TOTAL_AMOUNT,   --
                                t.ITEM_NO,   --
                                t.CHARGES,   --
                                t.COSTS,   --
                                t.RETAIL_PRICE,   --
                                t.REGRET,   --
                                t.APPLICANT,   --
                                t.DISPENSE_AMOUNT,   --
                                t.FIRM_ID,   --
                                t.DRUG_UNITS,   --
                                t.DRUG_SPEC,   --
                                t.DRUG_CODE,   --
                                t.VISIT_ID,   --
                                t.PATIENT_ID,   --
                                t.POST_DATE_TIME,   --
                                t.DISPENSARY,   --
                                t.WARD ,  --申请科室
                                   (SELECT ITEM_NAME
                                      FROM PRICE_ITEM_NAME_DICT
                                     WHERE ITEM_CLASS in ('A', 'B')
                                       and std_indicator = 1
                                       and PRICE_ITEM_NAME_DICT.ITEM_CODE = DRUG_CODE
                                       and rownum = 1) as ITEM_NAME
                                FROM
                                DRUG_DISPENSE_REGRET_REQ  t   --
                                 WHERE t.regret = '{2}'
                                        AND t.post_date_time > to_date('{0}', 'YYYY-MM-DD HH24:MI:SS')
                                        AND t.post_date_time <= to_date('{1}', 'YYYY-MM-DD HH24:MI:SS')";

            strSql = string.Format(strSql, beginDate.ToShortDateString() + " 00:00:00", endDate.ToShortDateString() + " 23:59:59", state);
            DataSet ds = BaseEntityer.Db.GetDataSet(strSql);
            return DataSetToEntity.DataSetToT<DRUG_DISPENSE_REGRET_REQ>(ds);
        }

        /// <summary>
        /// 查询待审批费药品列表
        /// </summary>
        /// <param name="state"></param>
        /// <param name="begDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IList<FEE_RETURN_APPLY> QueryWaitApplyUnDrugLst(string state, DateTime begDate, DateTime endDate)
        {
            string strSql = @"SELECT t.memo, --
                               t.apply_dept, --
                               t.price, --
                               t.examine_date, --
                               t.examine_oper, --
                               t.apply_date, --
                               t.apply_oper, --
                               t.doctor, --
                               t.operator_no, --
                               t.billing_date_time, --
                               t.charges, --
                               t.costs, --
                               t.performed_by, --
                               t.ordered_by, --
                               t.units, --
                               t.amount, --
                               t.item_spec, --
                               t.item_code, --
                               t.item_name, --
                               t.item_class, --
                               t.item_no, --
                               t.visit_id, --
                               t.patient_id, --
                               t.examine_state, --
                               t.apply_no --
                          FROM fee_return_apply t --
                         WHERE t.examine_state = '{0}'
                           AND t.apply_date > to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                           AND t.apply_date <= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
                        ";
            strSql = string.Format(strSql, state, begDate.ToShortDateString() + " 00:00:00", endDate.ToShortDateString() + " 23:59:59");
            DataSet ds = BaseEntityer.Db.GetDataSet(strSql);
            return DataSetToEntity.DataSetToT<FEE_RETURN_APPLY>(ds);
        }

        /// <summary>
        ///  更新药品的退费状态
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="state"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="ward"></param>
        /// <param name="postDateTime"></param>
        /// <param name="drugCode"></param>
        /// <param name="drugSpecs"></param>
        /// <param name="dispensary"></param>
        /// <param name="dispensaryDateTime"></param>
        /// <param name="firmID"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public int UpdateDrugReturnFeeState(BaseEntityer dby, string state, string patientID, string visitID, string ward, DateTime postDateTime, string drugCode, string drugSpecs, string dispensary, DateTime dispensaryDateTime, string firmID, string memo)
        {
            string strSql = @"
                        UPDATE drug_dispense_regret_req t
                           SET t.regret = '{0}',
                               t.memo='{10}'
                         WHERE t.patient_id = '{1}'
                           AND t.visit_id = '{2}'
                           AND t.ward = '{3}'
                           AND t.drug_code = '{4}'
                           AND t.drug_spec = '{5}'
                           AND t.post_date_time = to_date('{6}', 'yyyy-mm-dd hh24:mi:ss')
                           AND t.dispensary = '{7}'
                           AND t.dispensing_date_time = to_date('{8}', 'yyyy-mm-dd hh24:mi:ss')
                           and t.Firm_Id='{9}'
                           and t.regret='9'";
            strSql = string.Format(strSql, state, patientID, visitID, ward, drugCode, drugSpecs, postDateTime.ToString(), dispensary, dispensaryDateTime.ToString(), firmID, memo);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// 更新非药品的退费申请状态
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="state"></param>
        /// <param name="applyNO"></param>
        /// <param name="memo"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateUnDrugReturnFeeState(BaseEntityer dby, string state, string applyNO, string memo, string operCode, DateTime operDate)
        {
            string strSql = @"  UPDATE fee_return_apply t
      SET t.examine_state = '{0}',
          t.examine_oper  = '{1}',
          t.Examine_Date  = to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'),
          t.memo='{4}'
    WHERE t.apply_no = '{3}'
";

            strSql = string.Format(strSql, state, operCode, operDate.ToString(), applyNO, memo);
            return dby.ExecuteNonQuery(strSql);
        }
        #endregion

        #region 更新护士站医嘱的执行人和执行时间点

        /// <summary>
        ///  更新护士站医嘱的执行人和执行时间点
        /// </summary>
        /// <param name="patientID"> 患者ID</param>
        /// <param name="visitID"></param>
        /// <param name="orderNO"></param>
        /// <param name="orderSubNO"></param>
        /// <param name="nurse"></param>
        /// <param name="perfSchedule"></param>
        /// <returns></returns>
        public int UpdateOrderExecNurseAndTime(string patientID, string visitID, string orderNO, string orderSubNO, string nurse, string perfSchedule,bool isLongOrder,BaseEntityer db)
        {

            string sql = string.Empty;
            if (isLongOrder)
            {
               sql = @"
UPDATE orders t
   SET t.enter_date_time = to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'),
       t.stop_nurse            = '{1}'
 WHERE t.patient_id = '{2}'
   AND t.visit_id = '{3}'
   AND t.order_no = '{4}'
";
            }
            else
                sql = @"
UPDATE orders t
   SET t.enter_date_time = to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'),
       t.nurse            = '{1}'
 WHERE t.patient_id = '{2}'
   AND t.visit_id = '{3}'
   AND t.order_no = '{4}'
";

            sql = string.Format(sql, perfSchedule, nurse, patientID, visitID, orderNO);
            return db.ExecuteNonQuery(sql);
        }
        #endregion

        #region  三测单 DLQ 2014.07.28

        /// <summary>
        /// 获得住院病人信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>JOSN格式的字符串</returns>
        public string GetPat(string strWhere)
        {
            string json = "";

            string sql = @" select  
         pm.inp_no,
         pi.BED_NO,--床号 
         pm.NAME ,--患者姓名, 
        pi.PATIENT_ID ,--病人ID,  
         pi.VISIT_ID ,--住院次数,
         
         pi.ADMISSION_DATE_TIME,   --入院时间   
       
         pm.SEX,   --性别
        
         pi.nursing_class ,
         pi.adm_ward_date_time, --入科时间
         pi.dept_code,--所在科室
         dd.dept_name,--所在科室名称
        (to_char(pi.admission_date_time,'yyyy')-to_char(pm.DATE_OF_BIRTH,'yyyy')) as Age--年龄
     FROM   
         PATS_IN_HOSPITAL pi,--患者在院信息表,   
         PAT_MASTER_INDEX pm,  --病人主索引
         dept_dict dd-- 科室表
    where 
         pm.patient_id=pi.patient_id and pi.dept_code=dd.dept_code and pm.patient_id is not null  " + strWhere;

            json = Common.QueryTOJson(sql);









            return json;
        }

        /// <summary>
        /// 获得:病人病人大小便(其他)记录信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>JOSN格式的字符串</returns>
        public string GetEmr_vital_signs_dx(string strWhere)
        {
            string json = "";

            string sql = @"select *  from emr_vital_signs_dx  where  " + strWhere;

            json = Common.QueryTOJson(sql);
            return json;
        }

        /// <summary>
        /// 获得:三测单记录框架表信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>JOSN格式的字符串</returns>
        public string GetEmr_framefortt(string strWhere)
        {
            string json = "";

            string sql = @"select * from emr_framefortt  where  " + strWhere;

            json = Common.QueryTOJson(sql);
            return json;
        }


        /// <summary>
        /// 获得:病人体征记录信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>JOSN格式的字符串</returns>
        public string GetEmr_vital_signs_rec(string strWhere)
        {
            string json = "";

            string sql = @"select *  from emr_vital_signs_rec  where  " + strWhere;

            json = Common.QueryTOJson(sql);
            return json;
        }

        /// <summary>
        /// 获得:大小便选项信息
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>JOSN格式的字符串</returns>
        public string GetEmr_dxtype(string strWhere)
        {
            string json = "";

            string sql = @"select * from emr_dxtype where  " + strWhere;

            json = Common.QueryTOJson(sql);
            return json;
        }

        /// <summary>
        /// 删除病人病人大小便(其他)记录信息
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public int DelEmr_vital_signs_dx(string strWhere)
        {
            string sql = " delete from emr_vital_signs_dx where " + strWhere;
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 添加病人病人大小便(其他)记录信息
        /// </summary>
        /// <param name="vitalSignDxe">model</param>
        /// <returns></returns>
        public int AddEmr_vital_signs_dx(HisCommon.DataEntity.emr_vital_signs_dx vitalSignDx)
        {
            string sql = @"insert into EMR_VITAL_SIGNS_DX (PATIENT_ID, VISIT_ID, RECORDING_DATE, VITAL_SIGNS,VITAL_SIGNS_VALUES, UNITS, VITAL_ID, ID)
                        values ('" + vitalSignDx.patient_id
                                   + "', " + vitalSignDx.visit_id
                                   + ", to_date('" + vitalSignDx.recording_date.ToString("yyyy-MM-dd")
                                   + "', 'yyyy-mm-dd'), '" + vitalSignDx.vital_signs
                                   + "', '" + vitalSignDx.vital_signs_values
                                   + "', '" + vitalSignDx.units
                                   + "', '" + vitalSignDx.vital_id
                                   + "', '" + vitalSignDx.id + "')";
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }





        /// <summary>
        /// 删除病人体征记录信息
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public int DelEmr_vital_signs_rec(string strWhere)
        {
            string sql = " delete from emr_vital_signs_rec where " + strWhere;
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 添加病人体征记录信息
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public int AddEmr_vital_signs_rec(string patient_id,
                                           string visit_id,
                                           DateTime recording_date,
                                           DateTime time_point,
                                           string vital_signs,
                                           double vital_signs_values,
                                           string units,
                                           string timeflag,
                                           string vital_id, string id, string lowp, string whereout)
        {
            string sql = string.Format(@"insert into emr_vital_signs_rec(patient_id, 
                                            visit_id, 
                                            recording_date, 
                                            time_point, 
                                            vital_signs, 
                                            vital_signs_values, 
                                            units, 
                                            timeflag, 
                                            vital_id,id ,lowp, whereout) values('{0}','{1}',to_date('{2}','yyyy-MM-dd HH24:MI:SS'),to_date('{3}','yyyy-MM-dd HH24:MI:SS'),'{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}')",
patient_id, visit_id, recording_date, time_point, vital_signs, vital_signs_values, units, timeflag, vital_id, id, lowp, whereout);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }




        /// <summary>
        /// 更新病人体征记录信息
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public int UpdateEmr_vital_signs_rec(Decimal lowp, string strWhere)
        {
            string sql = string.Format("update  emr_vital_signs_rec set lowp=" + lowp + " where " + strWhere);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获得医院信息
        /// </summary>
        /// <returns></returns>
        public string GetHospital()
        {
            string json = "";
            string sql = @"select * from hospital_config";
            json = Common.QueryTOJson(sql);
            return json;

        }

        /// <summary>
        /// 获得其他批注信息
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public string GetEmr_DetailsOther(string strWhere)
        {
            string json = "";
            string sql = "select * from  emr_DetailsOther where  " + strWhere;
            json = Common.QueryTOJson(sql);
            return json;

        }

        /// <summary>
        /// 添加其他批注信息
        /// </summary>
        /// <param name="pz_date"></param>
        /// <param name="pz_nr"></param>
        /// <param name="wy"></param>
        /// <param name="remark"></param>
        /// <param name="type"></param>
        /// <param name="patinet_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public int AddEmr_DetailsOther(DateTime pz_date, string pz_nr, int wy, string remark, string type, string patinet_id, string visit_id)
        {
            string sql = @"insert into EMR_DETAILSOTHER (ID, PZ_DATE, PZ_NR, WY, REMARK, TYPE, PATIENT_ID, VISIT_ID)
                            values ('" + Guid.NewGuid().ToString("N")
                + "', to_date('" + pz_date + "', 'yyyy-mm-dd hh24:mi:ss'),'"
                + pz_nr + "', '" + wy + "', '" + remark + "', '" + type + "', '" + patinet_id + "', " + visit_id + ")";
            return BaseEntityer.Db.ExecuteNonQuery(sql);

        }

        /// <summary>
        /// 更新其他批注信息
        /// </summary>
        /// <param name="pz_date">时间 </param>
        /// <param name="pz_nr">内容</param>
        /// <param name="wy">位移</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public int UpdateEmr_DetailsOther(DateTime pz_date, string pz_nr, int wy, string id)
        {
            string sql = "update  EMR_DETAILSOTHER set pz_date= to_date('"
                + pz_date + "', 'yyyy-mm-dd hh24:mi:ss'),pz_nr='"
                + pz_nr + "',wy='" + wy + "' where id='" + id + "'";
            return BaseEntityer.Db.ExecuteNonQuery(sql);

        }

        /// <summary>
        /// 删除其他批注信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelEmr_DetailsOther(string id)
        {
            string sql = "delete from EMR_DETAILSOTHER where id='" + id + "'";
            return BaseEntityer.Db.ExecuteNonQuery(sql);

        }

        #endregion

        #region 皮试结果录入 by yan_x {39C11D5A-9A0B-4A1E-B640-CB310320050E}


        public int InpatientSkinTest(HisCommon.DataEntity.ORDERS obj, string skin,ref string errMsg)
        {
            try
            {

                string strSQL = string.Empty;
                strSQL = @" update ORDERS t
                           set t.perform_result = '{4}'
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}'
                           and t.order_no = '{2}'
                           and t.order_sub_no = '{3}'";
                strSQL = string.Format(strSQL, obj.PATIENT_ID, obj.VISIT_ID, obj.ORDER_NO, obj.ORDER_SUB_NO, skin);

                return BaseEntityer.Db.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                throw;
            }
        }

        #endregion
        #region 血糖值管理
        /// <summary>
        /// 插入血糖值管理
        /// </summary>
        /// <param name="?"></param>
        /// <param name="skin"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertInpatientBloodSugar(HisCommon.DataEntity.BLOOD_SUGAR_MANAGER obj)
        {
            string sql = @"INSERT INTO blood_sugar_manager
                      (serial_no,
                       patient_id,
                       visit_id,
                       time_zone,
                       blood_sugar_value,
                       injection_solution,
                       memo,
                       ack001,
                       ack002,
                       ack003,
                       oper_code,
                       oper_date)
                    VALUES
                      ( lpad(SEQ_BLOOD_SUGAR_MANAGER.Nextval,10,'0') ,
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
                       to_date('{10}', 'YYYY-MM-DD HH24:MI:SS'))
                    ";
            sql= string.Format(sql, obj.PATIENT_ID, obj.VISIT_ID, obj.TIME_ZONE, obj.BLOOD_SUGAR_VALUE, obj.INJECTION_SOLUTION, obj.MEMO, obj.ACK001, obj
                 .ACK002, obj.ACK003, obj.OPER_CODE, obj.OPER_DATE.ToString());
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新血糖值管理
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateInpatientBloodSugar(HisCommon.DataEntity.BLOOD_SUGAR_MANAGER obj)
        {
            string sql = @"UPDATE blood_sugar_manager
                       SET 
                           time_zone          = '{1}',
                           blood_sugar_value  = '{2}',
                           injection_solution = '{3}',
                           memo               = '{4}',
                           ack001             = '{5}',
                           ack002             = '{6}',
                           ack003             = '{7}',
                           oper_code          = '{8}',
                           oper_date          = to_date('{9}', 'YYYY-MM-DD HH24:MI:SS')
                     WHERE serial_no = '{0}'
                    ";
            sql = string.Format(sql,obj.SERIAL_NO,obj.TIME_ZONE,obj.BLOOD_SUGAR_VALUE,obj.INJECTION_SOLUTION,obj.MEMO,obj.ACK001,obj.ACK002,obj.ACK003,obj.OPER_CODE,obj.OPER_DATE.ToString());
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除血糖值管理
        /// </summary>
        /// <param name="serialNO">主建</param>
        /// <returns></returns>
        public int DeleteInpatientBloodSugar(string serialNO)
        {
            string sql = @"DELETE blood_sugar_manager b WHERE b.serial_no = '{0}'";
            sql = string.Format(sql, serialNO);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }
 
        /// <summary>
        ///  查询患者血糖值记录
        /// </summary>
        /// <param name="pateintID"></param>
        /// <param name="visitID"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BLOOD_SUGAR_MANAGER> QueryInpatientBloodSugarLst(string pateintID, string visitID)
        {
            string sql = @"SELECT serial_no,
       time_zone,
       blood_sugar_value,
       injection_solution,
       memo,
       ack001,
       ack002,
       ack003,
       oper_code,
       oper_date
  FROM blood_sugar_manager
 WHERE patient_id = '{0}'
   AND visit_id = '{1}'";
            sql = string.Format(sql, pateintID, visitID);

            return DataSetToEntity.DataSetToT<BLOOD_SUGAR_MANAGER>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        #endregion

        #region 南京输液大厅 by yan_x

        /// <summary>
        /// 插入签到患者信息
        /// </summary>
        /// <param name="NJ_INFUSION_PATIENT"></param>
        /// <returns></returns>
        public int InsertInfusionPatient(HisCommon.DataEntity.NJ_INFUSION_PATIENT obj, BaseEntityer Db)
        {
            string strSQL = string.Empty;
            string infusion_code = HisDBLayer.Common.GetSequence("SEQ_NJ_INFUSION_PATIENT");
            string sql = @"INSERT into nj_infusion_patient  
                        (
                        infusion_code,
                        CARD_NO,
                        PATIENT_NO,
                        PATIENT_NAME,
                        SEX,
                        AGE,
                        BLOOD_TYPE,
                        CARD_TYPE,
                        TEL,
                        CONTACT_INFO,
                        ORDER_DEPT,
                        EXEC_DEPT,
                        IN_OPER,
                        IN_DATE,
                        CANCEL_OPER,
                        CANCEL_DATE,
                        OUT_OPER,
                        OUT_DATE,
                        OPER_CODE,
                        OPER_DATE,
                        BED_NO,
                        bed_label,
                        STATE,
                        EXTEND_03,
                        EXTEND_04,
                        EXTEND_05
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
                        '{8}',
                        '{9}',
                        '{10}',
                        '{11}',
                        '{12}',
                        TO_DATE('{13}','YYYY-MM-DD HH24:MI:SS'),
                        '{14}',
                        TO_DATE('{15}','YYYY-MM-DD HH24:MI:SS'),
                        '{16}',
                        TO_DATE('{17}','YYYY-MM-DD HH24:MI:SS'),
                        '{18}',
                        TO_DATE('{19}','YYYY-MM-DD HH24:MI:SS'),
                        '{20}',
                        '{21}',
                        '{22}',
                        TO_DATE('{23}','YYYY-MM-DD HH24:MI:SS'),
                        '{24}',
                        '{25}'
                        ) ";

            object[] param = new object[] 
            { infusion_code.PadLeft(10,'0'),
              obj.Card_no, 
              obj.Patient_no, 
              obj.Patient_name, 
              obj.Sex, 
              obj.Age, 
              obj.Blood_type, 
              obj.Card_type,
              obj.Tel,
              obj.Contact_info,
              obj.Order_dept,
              obj.Exec_dept,
              obj.In_oper,
              obj.In_date,
              obj.Cancel_oper,
              obj.Cancel_date,
              obj.Out_oper,
              obj.Out_date,
              obj.Oper_code,
              obj.Oper_date,
              obj.Bed_no,
              obj.Bed_label,
              obj.State,
              obj.Extend_03,
              obj.Extend_04,
              obj.Extend_05};
            sql = string.Format(sql, param);
            //sql = string.Format(sql, sqlRecord_no1.PadLeft(10, '0'), obj.PATIENT_ID,obj.VISIT_ID.ToString(),obj.OWN_COST.ToString(),obj.INVALID, obj.OPER_CODE,obj.OPER_DATE.ToString(), obj.PATIENT_NAME);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 取消签到患者信息
        /// </summary>
        /// <param name="NJ_INFUSION_PATIENT"></param>
        /// <returns></returns>
        public int CancelInfusionPatient(HisCommon.DataEntity.NJ_INFUSION_PATIENT obj, BaseEntityer Db)
        {
            string strSQL = string.Empty;
            string sql = @"update nj_infusion_patient t
                           set t.CANCEL_OPER = '{3}',t.CANCEL_DATE=to_date('{4}','yyyy-mm-dd hh24:mi:ss'),
                               t.OPER_CODE='{5}',t.OPER_DATE=to_date('{6}','yyyy-mm-dd hh24:mi:ss'),
                               t.STATE='{7}'
                         where t.infusion_code='{0}'
                           and t.PATIENT_NO = '{1}'
                           and t.EXEC_DEPT = '{2}'
                           and t.state='1'";

            object[] param = new object[] 
            { obj.Infusion_code,
              obj.Patient_no, 
              obj.Exec_dept,
              obj.Cancel_oper,
              obj.Cancel_date,
              obj.Oper_code,
              obj.Oper_date,
              obj.State};
            sql = string.Format(sql, param);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 释放座位患者信息
        /// </summary>
        /// <param name="NJ_INFUSION_PATIENT"></param>
        /// <returns></returns>
        public int OutInfusionPatient(HisCommon.DataEntity.NJ_INFUSION_PATIENT obj, BaseEntityer Db)
        {
            string strSQL = string.Empty;
            string sql = @"update nj_infusion_patient t
                           set t.out_oper = '{3}',t.out_DATE=to_date('{4}','yyyy-mm-dd hh24:mi:ss'),
                               t.OPER_CODE='{5}',t.OPER_DATE=to_date('{6}','yyyy-mm-dd hh24:mi:ss'),
                               t.STATE='{7}'
                         where t.infusion_code='{0}'
                           and t.PATIENT_NO = '{1}'
                           and t.EXEC_DEPT = '{2}'
                           and t.state in ('1','2','3')";

            object[] param = new object[] 
            { obj.Infusion_code,
              obj.Patient_no, 
              obj.Exec_dept,
              obj.Out_oper,
              obj.Out_date,
              obj.Oper_code,
              obj.Oper_date,
              obj.State};
            sql = string.Format(sql, param);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新患者状态
        /// </summary>
        /// <param name="NJ_INFUSION_PATIENT"></param>
        /// <returns></returns>
        public int UpdateInfusionPatient(HisCommon.DataEntity.NJ_INFUSION_PATIENT obj, BaseEntityer Db)
        {
            string strSQL = string.Empty;
            string sql = @"update nj_infusion_patient a
                            set a.state = '2'
                            where a.infusion_code = '{0}'";
            sql = string.Format(sql, obj.Patient_no);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 插入处方信息
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        /// 
        string sqlRecord_no1 = string.Empty;
        public int InsertRecipes(HisCommon.DataEntity.NJ_RECIPE o, BaseEntityer db)
        {
            sqlRecord_no1 = HisDBLayer.Common.GetSequence("SEQ_NJ_INFUSION_RECIPE");
            string sql = @"insert into NJ_INFUSION_RECIPE
              (RECIPE_CODE,
               RECIPE_CD,
               DEPT_CD,
               DEPT_NAME,
               PATIENT_CD,
               PATIENT_NAME,
               DOCTOR_CD,
               DOCTOR_NAME,
               VERIFY_OPER_CD,
               VERIFY_OPER_NAME,
               START_TIME,
               END_TIME,
               EXPECT_START_TIME,
               NOTES,
               RECIPE_STATUS,
               IS_INFUSION_BRING_BACK,
               IS_OWN,
               OPER_CODE,
               OPER_DATE,
               oper_dept)
            values
              ( '{0}',
                '{1}',
                '{2}',
                '{3}',
                '{4}',
                '{5}',
                '{6}',
                '{7}',
                '{8}',
                '{9}',
              to_date('{10}', 'yyyy-MM-dd  hh24:mi:ss'),
              to_date('{11}', 'yyyy-MM-dd  hh24:mi:ss'),
              to_date('{12}', 'yyyy-MM-dd  hh24:mi:ss'),
                '{13}',
                '{14}',
                '{15}',
                '{16}',
                '{17}',
              to_date('{18}', 'yyyy-MM-dd  hh24:mi:ss'),
                '{19}')";
            object[] obs = new object[] 
               {
               sqlRecord_no1.PadLeft(10, '0'),
               o.RECIPECD,
               o.DEPTCD,
               o.DEPTNAME,
               o.PATIENTCD,
               o.PATIENTNAME,
               o.DOCTORCD,
               o.DOCTORNAME,
               o.VERIFYOPERCD,
               o.VERIFYOPERNAME,
               o.STARTTIME,
               o.ENDTIME,
               o.EXPECTSTARTTIME,
               o.NOTES,
               o.RECIPESTATUS,
               o.ISINFUSIONBRINGBACK,
               o.ISOWN,
               o.OPERCODE,
               o.OPERDATE,
               o.S1};
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 插入处方明细表
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertRecipesCosts(HisCommon.DataEntity.NJ_RECIPE_DETAIL o, BaseEntityer db)
        {
            string sql = @"insert into NJ_INFUSION_RECIPE_DETAIL
              (RECIPE_CODE,
                RECIPE_CD,
                EXECUTION_CD,
                GROUP_NO,
                GROUP_SERIAL_NO,
                FREQ_CD,
                FREQ_INFO,
                EXEC_DAYS,
                USE_METHOD,
                DRUG_CD,
                DRUG_NAME,
                DRUG_SPECI_CD,
                DRUG_SPECI_NAME,
                DRUG_USE_TIMES_NUMBER,
                INFUSION_TIMES,
                DRUG_NUMBER,
                DRUG_UNIT,
                DRUG_USE_DOSE,
                DOSE_UNIT_NAME,
                IS_NUTRIENT,
                SKIN_TEST,
                OPER_CODE,
                OPER_DATE,
                EXPECT_START_TIME,
                OPER_DEPT
                )
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
                to_date('{22}', 'yyyy-MM-dd  hh24:mi:ss'),
                to_date('{23}', 'yyyy-MM-dd  hh24:mi:ss'),
                '{24}'
               )";
            object[] obs = new object[] 
               {
                o.S4,
                o.RECIPECD,
                o.EXECUTIONCD,
                o.GROUPNO,
                o.GROUPSERIALNO,
                o.FREQCD,
                o.FREQINFO,
                o.EXECDAYS,
                o.USEMETHOD,
                o.DRUGCD,
                o.DRUGNAME,
                o.DRUGSPECICD,
                o.DRUGSPECINAME,
                o.DRUGUSETIMESNUMBER,
                o.INFUSIONTIMES,
                o.DRUGNUMBER,
                o.DRUGUNIT,
                o.DRUGUSEDOSE,
                o.DOSEUNITNAME,
                o.ISNUTRIENT,
                o.SKINTEST,
                o.OPERCODE,
                o.OPERDATE,
                o.S2,
                o.S3
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 插入输液计划表
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        string sqlRecord_no2 = string.Empty;
        public int InsertInfusionPlan(HisCommon.DataEntity.NJ_RECIPE_PT o, BaseEntityer db)
        {
            string sql = @"insert into nj_infusion_pt
              ( srial_id,
                PATIENT_CD,
                RECIPE_CD,
                GROUP_NO,
                EXPECT_START_TIME,
                CONFIRM_OPER,
                CONFIRM_DATE,
                CONFIRM_DEPT,                
                SORT_ID,
                sort_print,
                in_date
                )
            values
              (seq_nj_infusion_plan.nextval,
                '{1}',
                '{2}',
                '{3}',
                 to_date('{4}', 'yyyy-MM-dd  hh24:mi:ss'),
                '{5}',
                 to_date('{6}', 'yyyy-MM-dd  hh24:mi:ss'),
                '{7}',
                '{8}',
                '{9}',
                to_date('{10}', 'yyyy-MM-dd  hh24:mi:ss')
               )";
            object[] obs = new object[] 
               {
                sqlRecord_no2.PadLeft(10, '0'),
                o.PATIENTCD,
                o.RECIPECD,
                o.GROUPNO,
                o.EXPECT_START_TIME,
                o.OPERCODE,
                o.OPERDATE,
                o.DEPTCD,
                o.SORTID,
                o.SORTPRINT,
                o.INDATE
               };
            sql = string.Format(sql, obs);
            return db.ExecuteNonQuery(sql);
        }

        #endregion


 
    }
}
