using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Collections;
using HisCommon.DataEntity;
using System.Data.Common;
using System.Data.OleDb;

namespace HisDBLayer
{
    /// <summary>
    /// [功能描述: 住院登记数据库查询]<br></br>
    /// [创 建 者: 马斯伦]<br></br>
    /// [创建时间: 2012-11-16]<br></br>
    /// <修改>
    ///		<修改人></修改人>
    ///		<修改时间></修改时间>
    ///		<修改说明></修改说明>
    /// </修改>
    /// </summary>
    public class InpatientRegister
    {
        #region 住院登记/无费退院
        public DataTable GetALLPatient()
        {
            string sql = @"SELECT e.patient_id,e.inp_no,e.name,e.sex,e.nation,e.id_no,e.identity,e.charge_type,e.mailing_address,e.phone_number_home,e.phone_number_business FROM PAT_MASTER_INDEX e";
            //sql = string.Format(sql);

            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取患者信息 0 全院 1 门诊 2 住院
        /// </summary>
        /// <returns></returns>
        public DataTable GetPatient(int Flag)
        {
            string sql = string.Empty;
            if (Flag == 0)
            {
                sql = @"SELECT e.patient_id,e.inp_no,e.name,e.sex,e.nation,e.id_no,e.identity,e.charge_type,e.mailing_address,e.phone_number_home,e.phone_number_business FROM PAT_MASTER_INDEX e";
                //sql = string.Format(sql);
            }
            else if (Flag == 1)
            {
                sql = @"SELECT e.patient_id,e.inp_no,e.name,e.sex,e.nation,e.id_no,e.identity,e.charge_type,e.mailing_address,e.phone_number_home,e.phone_number_business FROM PAT_MASTER_INDEX e where e.inp_no is  null";
            }
            else if (Flag == 2)
            {
                sql = @"SELECT e.patient_id,e.inp_no,e.name,e.sex,e.nation,e.id_no,e.identity,e.charge_type,e.mailing_address,e.phone_number_home,e.phone_number_business FROM PAT_MASTER_INDEX e where e.inp_no is not null";
            }

            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 通过住院号查询病人ID
        /// </summary>
        /// <returns></returns>
        public DataTable GetPatientForInpNo(string InpNo)
        {
            string sql = @"select i.patient_id from pat_master_index i where i.inp_no='{0}'";

            sql = string.Format(sql, InpNo);

            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获得患者信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLPatientForUtil()
        {
            string sql = @"SELECT e.patient_id,e.inp_no,e.name,e.sex,e.nation,e.id_no,e.identity,e.charge_type,e.Date_Of_Birth FROM PAT_MASTER_INDEX e";
            //sql = string.Format(sql);

            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取所有病人信息PAT_MASTER_INDEX(按病人标识号)
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.PAT_MASTER_INDEX> GetPatientPMI_ByPatientID(string patientID)
        {
            string sql = @"SELECT s.patient_id,s.inp_no,s.name,s.sex,s.date_of_birth,s.citizenship,s.nation,
s.id_no,s.identity,s.charge_type,s.pact_code,s.unit_in_contract,s.mailing_address,
s.zip_code,s.phone_number_home,s.phone_number_business,s.next_of_kin,s.relationship,
s.next_of_kin_addr,s.next_of_kin_zip_code,s.next_of_kin_phone,s.last_visit_date,
s.vip_indicator,s.create_date,s.operator,s.pat_bed_bmp,s.pay_way
FROM PAT_MASTER_INDEX s where s.patient_id = '{0}'";
            sql = string.Format(sql, patientID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PAT_MASTER_INDEX>(ds).ToList();
        }

        /// <summary>
        /// 获取所有病人信息PAT_VISIT(按病人标识号)
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.PAT_VISIT> GetPatientPV_ByPatientID(string patientID)
        {
            string sql = @"select s.patient_id,s.visit_id,s.dept_admission_to,s.admission_date_time,s.dept_discharge_from,
s.discharge_date_time,s.occupation,s.marital_status,s.identity,s.armed_services,s.duty,
s.top_unit,s.service_system_indicator,s.unit_in_contract,s.charge_type,s.working_status,
s.insurance_type,s.insurance_no,s.service_agency,s.mailing_address,s.zip_code,s.next_of_kin,
s.relationship,s.next_of_kin_addr,s.next_of_kin_zipcode,s.next_of_kin_phone,s.patient_class,
s.admission_cause,s.consulting_date,s.pat_adm_condition,s.consulting_doctor,s.admitted_by,
s.emer_treat_times,s.esc_emer_times,s.serious_cond_days,s.critical_cond_days,s.icu_days,
s.ccu_days,s.spec_level_nurs_days,s.first_level_nurs_days,s.second_level_nurs_days,s.autopsy_indicator,
s.blood_type,s.blood_type_rh,s.infusion_react_times,s.blood_tran_times,s.blood_tran_vol,
s.blood_tran_react_times,s.decubital_ulcer_times,s.alergy_drugs,s.adverse_reaction_drugs,
s.mr_value,s.mr_quality,s.follow_indicator,s.follow_interval,s.follow_interval_units,s.director,
s.attending_doctor,s.doctor_in_charge,s.discharge_disposition,s.total_costs,s.total_payments,
s.catalog_date,s.cataloger,s.cost_alarm,s.personinfo,s.charge,s.state,s.outdiagnosis,
s.charge_type_code,s.icu_dept_code,s.validate_code
 from pat_visit s where s.patient_id = '{0}' order by s.visit_id";
            sql = string.Format(sql, patientID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PAT_VISIT>(ds).ToList();
        }

        /// <summary>
        /// 获取所有病人信息PAT_VISIT(按病人标识号)
        /// </summary>
        /// <returns></returns>
        public HisCommon.DataEntity.PAT_VISIT GetPatientPV_ByPatientIDOnly(string patientID, string visit)
        {
            string sql = @"select s.patient_id,s.visit_id,nvl((select p.dept_code from pats_in_hospital p where p.patient_id=s.patient_id and p.visit_id=s.visit_id)
,s.dept_admission_to) dept_admission_to,s.admission_date_time,s.dept_discharge_from,
s.discharge_date_time,s.occupation,s.marital_status,s.identity,s.armed_services,
s.top_unit,nvl(s.service_system_indicator,0),s.unit_in_contract,s.charge_type,nvl(s.working_status,0),
s.insurance_type,s.insurance_no,s.service_agency,s.mailing_address, s.zip_code,s.next_of_kin,
s.relationship,s.next_of_kin_addr,s.next_of_kin_zipcode,s.next_of_kin_phone,s.patient_class,
s.admission_cause,s.consulting_date,s.pat_adm_condition,s.consulting_doctor,s.admitted_by,
nvl(s.emer_treat_times,0),nvl(s.esc_emer_times,0),nvl(s.serious_cond_days,0),nvl(s.critical_cond_days,0),nvl(s.icu_days,0),
nvl(s.ccu_days,0),nvl(s.spec_level_nurs_days,0),nvl(s.first_level_nurs_days,0),nvl(s.second_level_nurs_days,0),nvl(s.autopsy_indicator,0),
s.blood_type,s.blood_type_rh,nvl(s.infusion_react_times,0),nvl(s.blood_tran_times,0),nvl(s.blood_tran_vol,0),
nvl(s.blood_tran_react_times,0),nvl(s.decubital_ulcer_times,0),s.alergy_drugs,s.adverse_reaction_drugs,
s.mr_value,s.mr_quality,nvl(s.follow_indicator,0),nvl(s.follow_interval,0),s.follow_interval_units,s.director,
s.attending_doctor,s.doctor_in_charge,s.discharge_disposition,nvl(s.total_costs,0),nvl(s.total_payments,0),
s.catalog_date,s.cataloger,nvl(s.cost_alarm,0),s.personinfo,nvl(s.charge,0),s.state,s.OUTDIAGNOSIS,s.duty,s.charge_type_code
 from pat_visit s where s.patient_id = '{0}' and s.visit_id = '{1}'";
            sql = string.Format(sql, patientID, visit);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            HisCommon.DataEntity.PAT_VISIT pt = new PAT_VISIT();
            #region
            try
            {
                pt.PATIENT_ID = dt.Rows[0][0].ToString();
                pt.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                pt.DEPT_ADMISSION_TO = dt.Rows[0][2].ToString();
                pt.ADMISSION_DATE_TIME = DateTime.Parse(dt.Rows[0][3].ToString());
                pt.DEPT_DISCHARGE_FROM = dt.Rows[0][4].ToString();
                pt.DISCHARGE_DATE_TIME = DateTime.Parse(string.IsNullOrEmpty(dt.Rows[0][5].ToString()) == true ? "0001-01-01 00:00:00" : dt.Rows[0][5].ToString());
            }
            catch
            {
                return null;
            }
            pt.OCCUPATION = dt.Rows[0][6].ToString();
            pt.MARITAL_STATUS = dt.Rows[0][7].ToString();
            pt.IDENTITY = dt.Rows[0][8].ToString();
            pt.ARMED_SERVICES = dt.Rows[0][9].ToString();
            pt.TOP_UNIT = dt.Rows[0][10].ToString();
            pt.SERVICE_SYSTEM_INDICATOR = int.Parse(dt.Rows[0][11].ToString());
            pt.UNIT_IN_CONTRACT = dt.Rows[0][12].ToString();
            pt.CHARGE_TYPE = dt.Rows[0][13].ToString();
            pt.WORKING_STATUS = dt.Rows[0][14].ToString();
            pt.INSURANCE_TYPE = dt.Rows[0][15].ToString();
            pt.INSURANCE_NO = dt.Rows[0][16].ToString();
            pt.SERVICE_AGENCY = dt.Rows[0][17].ToString();
            pt.MAILING_ADDRESS = dt.Rows[0][18].ToString();
            pt.ZIP_CODE = dt.Rows[0][19].ToString();
            pt.NEXT_OF_KIN = dt.Rows[0][20].ToString();
            pt.RELATIONSHIP = dt.Rows[0][21].ToString();
            pt.NEXT_OF_KIN_ADDR = dt.Rows[0][22].ToString();
            pt.NEXT_OF_KIN_ZIPCODE = dt.Rows[0][23].ToString();
            pt.NEXT_OF_KIN_PHONE = dt.Rows[0][24].ToString();
            pt.PATIENT_CLASS = dt.Rows[0][25].ToString();
            pt.ADMISSION_CAUSE = dt.Rows[0][26].ToString();
            pt.CONSULTING_DATE = string.IsNullOrEmpty(dt.Rows[0][27].ToString()) == true ? DateTime.MinValue : DateTime.Parse(dt.Rows[0][27].ToString());
            pt.PAT_ADM_CONDITION = dt.Rows[0][28].ToString();
            pt.CONSULTING_DOCTOR = dt.Rows[0][29].ToString();
            pt.ADMITTED_BY = dt.Rows[0][30].ToString();
            pt.EMER_TREAT_TIMES = int.Parse(dt.Rows[0][31].ToString());
            pt.ESC_EMER_TIMES = int.Parse(dt.Rows[0][32].ToString());
            pt.SERIOUS_COND_DAYS = int.Parse(dt.Rows[0][33].ToString());
            pt.CRITICAL_COND_DAYS = int.Parse(dt.Rows[0][34].ToString());
            pt.ICU_DAYS = int.Parse(dt.Rows[0][35].ToString());
            pt.CCU_DAYS = int.Parse(dt.Rows[0][36].ToString());
            pt.SPEC_LEVEL_NURS_DAYS = int.Parse(dt.Rows[0][37].ToString());
            pt.FIRST_LEVEL_NURS_DAYS = int.Parse(dt.Rows[0][38].ToString());
            pt.SECOND_LEVEL_NURS_DAYS = int.Parse(dt.Rows[0][39].ToString());
            pt.AUTOPSY_INDICATOR = int.Parse(dt.Rows[0][40].ToString());
            pt.BLOOD_TYPE = dt.Rows[0][41].ToString();
            pt.BLOOD_TYPE_RH = dt.Rows[0][42].ToString();
            pt.INFUSION_REACT_TIMES = int.Parse(dt.Rows[0][43].ToString());
            pt.BLOOD_TRAN_TIMES = int.Parse(dt.Rows[0][44].ToString());
            pt.BLOOD_TRAN_VOL = int.Parse(dt.Rows[0][45].ToString());
            pt.BLOOD_TRAN_REACT_TIMES = int.Parse(dt.Rows[0][46].ToString());
            pt.DECUBITAL_ULCER_TIMES = int.Parse(dt.Rows[0][47].ToString());
            pt.ALERGY_DRUGS = dt.Rows[0][48].ToString();
            pt.ADVERSE_REACTION_DRUGS = dt.Rows[0][49].ToString();
            pt.MR_VALUE = dt.Rows[0][50].ToString();
            pt.MR_QUALITY = dt.Rows[0][51].ToString();
            pt.FOLLOW_INDICATOR = int.Parse(dt.Rows[0][52].ToString());
            pt.FOLLOW_INTERVAL = int.Parse(dt.Rows[0][53].ToString());
            pt.FOLLOW_INTERVAL_UNITS = dt.Rows[0][54].ToString();
            pt.DIRECTOR = dt.Rows[0][55].ToString();
            pt.ATTENDING_DOCTOR = dt.Rows[0][56].ToString();
            pt.DOCTOR_IN_CHARGE = dt.Rows[0][57].ToString();
            pt.DISCHARGE_DISPOSITION = dt.Rows[0][58].ToString();
            pt.TOTAL_COSTS = decimal.Parse(dt.Rows[0][59].ToString());
            pt.TOTAL_PAYMENTS = decimal.Parse(dt.Rows[0][60].ToString());
            //pt.CATALOG_DATE = DateTime.Parse(dt.Rows[0][61].ToString());
            pt.CATALOGER = dt.Rows[0][62].ToString();
            pt.COST_ALARM = decimal.Parse(dt.Rows[0][63].ToString());
            pt.PERSONINFO = dt.Rows[0][64].ToString();
            pt.CHARGE = decimal.Parse(dt.Rows[0][65].ToString());
            pt.STATE = dt.Rows[0][66].ToString();
            pt.OUTDIAGNOSIS = dt.Rows[0][67].ToString();
            pt.DUTY = dt.Rows[0][68].ToString();
            pt.CHARGE_TYPE_CODE = dt.Rows[0][69].ToString();
            #endregion
            return pt;
        }

        /// <summary>
        /// 获取所有病人信息PATS_IN_HOSPITAL(按病人标识号)
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.PATS_IN_HOSPITAL> GetPatientPIH_ByPatientID(string patientID)
        {
            string sql = @"SELECT s.patient_id,s.visit_id,s.ward_code,s.dept_code,s.bed_no,s.admission_date_time,
s.adm_ward_date_time,s.diagnosis,s.patient_condition,s.nursing_class,s.doctor_in_charge,
s.operating_date,s.billing_date_time,s.prepayments,s.total_costs,s.total_charges,
s.guarantor,s.guarantor_org,s.guarantor_phone_num,s.billing_date_time,s.settled_indicator,
s.diagid,s.diagname,s.personinfo FROM PATS_IN_HOSPITAL s where s.patient_id = '{0}'";
            #region 

            #endregion
            sql = string.Format(sql, patientID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PATS_IN_HOSPITAL>(ds).ToList();
        }

        /// <summary>
        /// 获取所有病人信息(按病人住院号)
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.PAT_MASTER_INDEX> GetPatientPMI_ByInpNo(string patientID)
        {
            string sql = @"SELECT s.patient_id,s.inp_no,s.name,s.sex,s.date_of_birth,s.citizenship,s.nation,
s.id_no,s.identity,s.charge_type,s.pact_code,s.unit_in_contract,s.mailing_address,
s.zip_code,s.phone_number_home,s.phone_number_business,s.next_of_kin,s.relationship,
s.next_of_kin_addr,s.next_of_kin_zip_code,s.next_of_kin_phone,s.last_visit_date,
s.vip_indicator,s.create_date,s.operator,s.pat_bed_bmp,s.pay_way FROM PAT_MASTER_INDEX s where s.inp_no like '%{0}%'";
            sql = string.Format(sql, patientID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PAT_MASTER_INDEX>(ds).ToList();
        }

        /// <summary>
        /// 获取所有病人信息(按病人姓名)
        /// </summary>
        /// <returns></returns>
        public DataTable GetPatientByName(string name)
        {
            string sql = @"SELECT s.patient_id,s.inp_no,s.name,s.sex,s.date_of_birth,s.citizenship,s.nation,
s.id_no,s.identity,s.charge_type,s.pact_code,s.unit_in_contract,s.mailing_address,
s.zip_code,s.phone_number_home,s.phone_number_business,s.next_of_kin,s.relationship,
s.next_of_kin_addr,s.next_of_kin_zip_code,s.next_of_kin_phone,s.last_visit_date,
s.vip_indicator,s.create_date,s.operator,s.pat_bed_bmp,s.pay_way FROM PAT_MASTER_INDEX s where s.name like '%{0}%'";
            sql = string.Format(sql, name);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取所有病人信息(按病人床号)
        /// </summary>
        /// <returns></returns>
        public DataTable GetPatientByBEDNO(string BED_NO)
        {
            string sql = @"SELECT t.name,s.patient_id,s.visit_id,s.ward_code,s.dept_code,s.bed_no,s.admission_date_time,
s.adm_ward_date_time,s.diagnosis,s.patient_condition,s.nursing_class,s.doctor_in_charge,
s.operating_date,s.billing_date_time,s.prepayments,s.total_costs,s.total_charges,
s.guarantor,s.guarantor_org,s.guarantor_phone_num,s.billing_date_time,s.settled_indicator,
s.diagid,s.diagname,s.personinfo
FROM PATS_IN_HOSPITAL s,pat_master_index t where t.patient_id = s.patient_id and s.BED_NO like '%{0}%'";
            sql = string.Format(sql, BED_NO);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取所有病人信息(按病人科室)
        /// </summary>
        /// <returns></returns>
        public DataTable GetPatientByDEPT(string dept_code)
        {
            string sql = @"SELECT a.dept_name,f.name,s.patient_id,s.visit_id,s.ward_code,s.dept_code,s.bed_no,s.admission_date_time,
s.adm_ward_date_time,s.diagnosis,s.patient_condition,s.nursing_class,s.doctor_in_charge,
s.operating_date,s.billing_date_time,s.prepayments,s.total_costs,s.total_charges,
s.guarantor,s.guarantor_org,s.guarantor_phone_num,s.billing_date_time,s.settled_indicator,
s.diagid,s.diagname,s.personinfo 
FROM PATS_IN_HOSPITAL s,dept_dict a,pat_master_index f where s.dept_code = a.dept_code and f.patient_id = s.patient_id and DEPT_NAME like '%{0}%'";
            sql = string.Format(sql, dept_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取性别列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLSexList()
        {
            string sql = @"select s.sex_code as 编码,s.sex_name as 名称,s.input_code as 输入码 from SEX_DICT s order by to_number(s.sex_code)";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取民族列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLNationList()
        {
            string sql = @"select s.nation_code as 编码,s.nation_name as 名称,s.input_code as 输入码 from NATION_DICT s order by to_number(s.nation_code)";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取人员类别列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLStaffTypeList()
        {
            string sql = @"select s.identity_code as 编码,s.identity_name as 名称,s.input_code as 输入码 from IDENTITY_DICT s order by to_number(s.identity_code)";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取合同单位名称
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLPactCodeList()
        {
            string sql = @"select s.charge_type_code as 编码,s.charge_type_name as 名称,s.spell_code as 拼音码,s.wb_code as 五笔码 from CHARGE_TYPE_DICT s order by to_number(s.charge_type_code)";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取职业列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLProfessionList()
        {
            string sql = @"select s.occupation_code as 编码,s.occupation_name as 名称,s.input_code as 输入码 from OCCUPATION_DICT s ";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取婚姻状况列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLMarriageList()
        {
            string sql = @"select s.marital_status_code as 编码,s.marital_status_name as 名称,s.input_code as 输入码 from MARITAL_STATUS_DICT s order by to_number(s.marital_status_code)";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取国籍列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLNationalityList()
        {
            string sql = @"select s.country_code as 编码,s.country_name as 名称,s.input_code as 输入码 from COUNTRY_DICT s order by s.country_code";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取出生地&籍贯列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLBirthPlaceOriginList()
        {
            string sql = @"select s.area_code as 编码,s.area_name as 名称,s.input_code as 输入码 from AREA_DICT s order by s.area_code";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取联系人关系列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLlinkRelationList()
        {
            string sql = @"select s.relationship_code as 编码,s.relationship_name as 名称,s.input_code as 输入码 from RELATIONSHIP_DICT s order by to_number(s.relationship_code)";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取入院方式列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLInpatientTypeList()
        {
            string sql = @"select s.patient_class_code as 编码,s.patient_class_name as 名称,s.input_code as 输入码 from PATIENT_CLASS_DICT s order by to_number(s.patient_class_code)";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取病情列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLIllnessStateList()
        {
            string sql = @"select s.patient_status_code as 编码,s.patient_status_name as 名称,s.input_code as 输入码 from PATIENT_STATUS_DICT s order by to_number(s.patient_status_code)";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取入院科室列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLDeptList()
        {
            string sql = @"select a.dept_code as 编码,a.dept_name as 名称,a.input_code as 输入码 from DEPT_DICT a where a.clinic_attr=0 and a.outp_or_inp in(1,2) order by a.dept_code";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 按入院科室编码获取科室名称
        /// </summary>
        /// <returns></returns>
        public string GetDeptNameByDeptCode(string code)
        {
            string sql = @"select dept_dict.dept_name from dept_dict 
                    where dept_dict.dept_code = '{0}'";
            sql = string.Format(sql, code);
            System.Data.DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            string name = string.Empty;
            if (dt != null)
                name = dt.Rows[0][0].ToString();
            return name;
        }

        /// <summary>
        /// 获取收住医师列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMZDoctorList()
        {
            string sql = @"select s.user_id as 编码,s.user_name as 名称 , a.input_code  as 输入码, a.dept_name as 科室 from USERS_STAFF_DICT s,DEPT_DICT a where a.dept_code = s.user_dept and a.clinic_attr = 0 and s.certificate_code is not null/*a.outp_or_inp in ('0','2')*/ order by a.dept_code";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取支付方式列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetPayTypeList()
        {
            string sql = @"select s.id as 编码,s.name as 名称,s.input as 输入码 from PAYTYPE s order by s.id";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取支付方式列表
        /// </summary>
        /// <returns></returns>
        public List<PAYTYPE> GetPayTypeList2()
        {
            string sql = @"select * from PAYTYPE s order by s.id";
            System.Data.DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<PAYTYPE>(ds).ToList();
        }

        /// <summary>
        /// 获取银行列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetBankList()
        {
            string sql = @"select s.bank_no as 编码,s.bank_name as 名称,s.input_code as 输入码 from bank_dict s order by s.bank_no";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取担保方式列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSecuredTypeList()
        {
            string sql = @"select t.code as 编码,t.name as 名称,t.spell_code as 拼音码,t.wb_code as 五笔码 from COM_DICTIONARY t where t.type = 'SURETYPE' and t.valid_state = 1 order by t.code";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取担保人列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSecuredManList()
        {
            string sql = @"select s.user_id as 编码,s.user_name as 名称 from USERS_STAFF_DICT s order by s.user_name";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取诊断列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDiagnoseList()
        {
            string sql = @"select t.diagnose_code  as 编码,
                                   t.diagnose_name  as 名称,
                                   t.diagnose_spell as 输入码
                              from si_sydiagnose t
                             where t.charge_type_code = 1
                             order by t.diagnose_spell";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取农合诊断列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDiagnoseNHList()
        {
            string sql = @"select t.diagnose_code  as 编码,
                                   t.diagnose_name  as 名称,
                                   t.diagnose_spell as 输入码
                              from si_sydiagnose t
                             where t.charge_type_code = 7
                             order by t.diagnose_code";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 住院登记获取患者ID索引
        /// </summary>
        /// <returns></returns>
        public string GetPatientIDSeq()
        {
            System.Data.DataTable dt = BaseEntityer.Db.GetDataTable(@"select patient_id_seq.nextval from dual");
            string seq = string.Empty;
            if (dt != null)
                seq = dt.Rows[0][0].ToString();
            return seq;
        }

        /// <summary>
        /// 住院登记获取最大患者ID
        /// </summary>
        /// <returns></returns>
        public int GetPatientIDMax()
        {
            System.Data.DataTable dt = BaseEntityer.Db.GetDataTable(@"select max(to_number(substr(patient_id,2,9))) from pat_visit");
            int MaxID = 0;
            if (dt != null)
                MaxID = int.Parse(dt.Rows[0][0].ToString());
            return MaxID;
        }

        /// <summary>
        /// 住院登记获取最大担保ID
        /// </summary>
        /// <returns></returns>
        public static string GetPatientMaxGuarantID(string patientID, string visitID)
        {
            string sql = @"select max(guarant_id) from com_guarant where patient_ID = '{0}' and visit_ID = '{1}'";
            sql = string.Format(sql, patientID, visitID);
            System.Data.DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            string seq = string.Empty;
            if (dt != null)
                seq = dt.Rows[0][0].ToString();
            return seq;
        }


        /// <summary>
        /// 住院登记获取最大住院号
        /// </summary>
        /// <returns></returns>
        public static string GetInpNOMaxGuarantID()
        {
            string sql = @"(select nvl(to_number(max(to_number(s.inp_no))),0) + 1 from pat_master_index s)";
            System.Data.DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            string seq = string.Empty;
            if (dt != null)
                seq = dt.Rows[0][0].ToString();
            return seq;
        }

        /// <summary>
        /// 住院登记插入患者基本信息表
        /// </summary>
        /// <returns></returns>
        public int InsertInpatientRegisterPatientInfo(BaseEntityer db, ref string err, params string[] args)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into pat_master_index c (c.patient_id,c.inp_no,c.name,c.name_phonetic,
c.sex,c.date_of_birth,c.birth_place,c.citizenship,c.nation,c.id_no,c.identity,c.charge_type,
c.unit_in_contract,c.mailing_address,c.phone_number_home,c.phone_number_business,
c.next_of_kin,c.relationship,c.next_of_kin_addr,c.next_of_kin_phone,c.pay_way,c.operator,c.pact_code) 
values ('{0}','{1}','{2}','{3}','{4}',to_date('{5}','yyyy-MM-dd hh24:mi:ss'),'{6}','{7}','{8}','{9}','{10}','{11}','{12}',
'{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";
                sql = string.Format(sql, args);
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return exec;
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

//            //获取最大住院号
//            string InpNo = GetInpNOMaxGuarantID();
//            //住院号提出来
//            pat_master_index.INP_NO = InpNo;

            string sql = string.Empty;
            DbDataReader dbr = db.ExecuteReader(sqlsearch);
            if (dbr.Read())
            {
                //2014-4-25 by li 住院登记患者住院号自动获取方法修改
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
                       PAT_MASTER_INDEX.PAT_BED_BMP,
                       PAT_MASTER_INDEX.PACT_CODE,
                       PAY_WAY)
                    values
                      ('{0}', '{1}', '{2}', '{3}', '{4}', to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'), 
                       '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}',
                       '{17}', '{18}', '{19}', '{20}', '{21}', to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'), 
                       '{23}', to_date('{24}', 'yyyy-mm-dd hh24:mi:ss'), '{25}', '{26}','{27}','{28}')";
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
                pat_master_index.OPERATOR, pat_master_index.PAT_BED_BMP,pat_master_index.PACT_CODE,pat_master_index.PAY_WAY };
            sql = HisCommon.Utility.SqlFormate(sql, param);

            if (!dbr.IsClosed)
                dbr.Close();
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        ///  1.查看患者的住院号是否重复，如果重复了，更新患者的住院号的信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="inpNO"></param>
        /// <returns></returns>
        public int UpdateInpatientInpNO(string patientID, string inpNO)
        {
            string sql = "SELECT count(*) FROM pat_master_index b  WHERE b.inp_no='{0}'";
            sql = string.Format(sql, inpNO);
            int count = BaseEntityer.Db.ExecuteScalar<int>(sql);
            if (count > 1)
            {
                string sql1 = "update  pat_master_index r set  r.inp_no='{0}' WHERE  r.patient_id='{1}' and  r.inp_no='{2}'";
                //获取最大住院号
                string maxInpNO = GetInpNOMaxGuarantID();
                sql1 = string.Format(sql1, maxInpNO, patientID, inpNO);

                return BaseEntityer.Db.ExecuteNonQuery(sql1);
            }
            return 1;
        }

        /// <summary>
        /// 住院登记插入诊断表
        /// </summary>
        /// <returns></returns>
        public int InsertInpatientRegisterDiagnose(BaseEntityer db, ref string Err, params string[] args)
        {
            int rev = 0;
            try
            {
                string sql = @"insert into Diagnosis g (g.patient_id,g.visit_id,g.diagnosis_type,g.diagnosis_no,
g.diagnosis_desc,g.diagnosis_date,g.diag_code) 
values ('{0}','{1}','{2}','{3}','{4}',to_date('{5}','yyyy-MM-dd hh24:mi:ss'),'{6}')";
                sql = string.Format(sql, args);
                rev = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                Err = e.Message;
            }
            return rev;
        }

        /// <summary>
        /// 住院登记插入担保信息表
        /// </summary>
        /// <returns></returns>
        public int InsertInpatientRegisterGuarant(BaseEntityer db, ref string Err, params string[] args)
        {
            int rev = 0;
            try
            {
                string sql = @"insert into com_guarant d (d.patient_id,d.visit_id,d.guarant_id,d.guarant_type,
d.guarant_name,d.guarant_money,d.guarant_phone,d.guarant_bussiness,d.guarant_mark) 
values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
                sql = string.Format(sql, args);
                rev = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                Err = e.Message;
            }
            return rev;
        }

        /// <summary>
        /// 住院登记插入变更记录表
        /// </summary>
        /// <returns></returns>
        public int InsertInpatientRegisterTransfer(BaseEntityer db, ref string Err, params string[] args)
        {
            int rev = 0;
            try
            {
                string sql = @"insert into pats_in_transferring f (f.patient_id,f.dept_transfered_to,f.transfer_date_time) 
    values ('{0}','{1}',sysdate)";
                sql = string.Format(sql, args);
                rev = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                Err = e.Message;
            }
            return rev;
        }

        /// <summary>
        /// 住院登记插入就诊信息表
        /// </summary>
        /// <returns></returns>
        public int InsertInpatientRegisterVisit(BaseEntityer db, ref string Err, params string[] args)
        {
            int rev = 0;
            try
            {
                string sql = @"insert into pat_visit r (r.patient_id,r.visit_id,r.dept_admission_to,
    r.occupation,r.marital_status,r.identity,r.service_system_indicator,r.unit_in_contract,
    r.charge_type,r.working_status,r.insurance_type,r.patient_class,r.pat_adm_condition,
    r.consulting_doctor,r.admitted_by,r.state,r.outdiagnosis,r.admission_date_time,r.CHARGE_TYPE_CODE,r.validate_code) 
    values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',
    '{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',to_date('{17}','yyyy-mm-dd hh24:mi:ss'),'{18}','{19}')";
                sql = string.Format(sql, args);
                rev = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                Err = e.Message;
            }
            return rev;
        }

        /// <summary>
        /// 住院登记插入在院信息表
        /// </summary>
        /// <returns></returns>
        public int InsertInpatientRegisterInHospital(BaseEntityer db, ref string Err, params string[] args)
        {
            int rev = 0;
            try
            {
                string sql = @"insert into pats_in_hospital w (w.patient_id,w.visit_id,w.admission_date_time,w.diagnosis,
    w.patient_condition,w.doctor_in_charge,w.prepayments,w.settled_indicator,w.diagid,w.diagname,w.dept_code,w.total_costs,w.total_charges)
    values ('{0}','{1}',to_date('{2}','yyyy-MM-dd hh24:mi:ss'),'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')";
                sql = string.Format(sql, args);
                rev = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                Err = e.Message;
            }
            return rev;
        }

        /// <summary>
        /// 住院登记插入在院信息表
        /// </summary>
        /// <returns></returns>
        public int InsertReInpatientRegisterInHospital(BaseEntityer db, ref string Err, params string[] args)
        {
            int rev = 0;
            try
            {
                string sql = @"insert into pats_in_hospital w (w.patient_id,w.visit_id,w.admission_date_time,w.diagnosis,
    w.patient_condition,w.doctor_in_charge,w.prepayments,w.settled_indicator,w.diagid,w.diagname,w.dept_code,w.total_costs,w.total_charges)
    values ('{0}','{1}',to_date('{2}','yyyy-MM-dd hh24:mi:ss'),'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')";
                sql = string.Format(sql, args);
                rev = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                Err = e.Message;
            }
            return rev;
        }

        /// <summary>
        /// 查询当日住院登记信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetInpatientRegisterInfo()
        {
            string sql = @"select t.patient_id as 病人ID,
       t.visit_id as 住院次数, 
       s.inp_no as 住院号,
       s.name as 病人名字,
       d.dept_name as 科室,
       s.date_of_birth as 出生日期,
       t.admission_date_time as 入院日期,
       decode(t.state,
              'R', '住院登记','I','病房接诊',
              'B', '出院登记',
              'O', '出院结算', 'N','无费退院','其他') as 状态
,s.pay_way
  from pat_visit t, pat_master_index s, dept_dict d
 where t.patient_id = s.patient_id
   and t.dept_admission_to = d.dept_code
   and (trunc(t.admission_date_time) = trunc(sysdate) or
       t.state = 'R')
 order by t.admission_date_time desc";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetInpatientRegisterInfoforUpdate(string number, string fre)
        {
            string sql = @"select t.patient_id,t.visit_id,c.inp_no,t.working_status,c.name,c.sex,
       c.nation,t.unit_in_contract,c.pact_code,t.occupation,t.marital_status,c.CITIZENSHIP,c.date_of_birth,
       c.birth_place,c.id_no,c.mailing_address,c.phone_number_home,t.insurance_type,c.phone_number_business,
       c.next_of_kin,c.relationship,c.next_of_kin_addr,c.next_of_kin_phone,t.patient_class,
       t.pat_adm_condition,t.admission_date_time,t.dept_admission_to,s.diag_code,s.diagnosis_desc,c.pay_way
  from pat_visit t, pat_master_index c,Diagnosis s
 where t.patient_id = c.patient_id
   and s.patient_id = t.patient_id
   and s.visit_id = t.visit_id
   and t.patient_id = '{0}'
   and t.visit_id = '{1}'
   and (trunc(sysdate) = trunc(t.admission_date_time) or t.state = 'R')";
            sql = string.Format(sql, number, fre);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetInpatientRegisterInfoforSelect(string number, string fre)
        {
            string sql = @"select t.patient_id,t.visit_id,c.inp_no,t.working_status,c.name,c.sex,
       c.nation,t.unit_in_contract,c.pact_code,t.occupation,t.marital_status,c.CITIZENSHIP,c.date_of_birth,
       c.birth_place,c.id_no,c.mailing_address,c.phone_number_home,t.insurance_type,c.phone_number_business,
       c.next_of_kin,c.relationship,c.next_of_kin_addr,c.next_of_kin_phone,t.patient_class,
       t.pat_adm_condition,t.admission_date_time,t.dept_admission_to
  from pat_visit t, pat_master_index c
 where t.patient_id = c.patient_id
   and t.patient_id = '{0}'
   and t.visit_id = '{1}'";
            sql = string.Format(sql, number, fre);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <returns></returns>
        public PATS_IN_HOSPITAL GetReInpatientRegisterInfo(string patientid, string visitid)
        {
            PATS_IN_HOSPITAL visit = new PATS_IN_HOSPITAL();
            DataTable dt = new DataTable();
            string sql = @"select f.admission_date_time,
                                   f.outdiagnosis,
                                   f.pat_adm_condition,
                                   f.consulting_doctor,
                                   (select sum(prepayment_rcpt.amount)
                                      from prepayment_rcpt
                                     where prepayment_rcpt.patient_id = '{0}'
                                       and prepayment_rcpt.visit_id = '{1}'),
                                   '0',
                                   f.dept_discharge_from
                              from pat_visit f
                             where f.patient_id = '{0}' and f.visit_id='{1}'";
            sql = string.Format(sql, patientid, visitid);
            dt = BaseEntityer.Db.GetDataTable(sql);
            visit.ADMISSION_DATE_TIME = DateTime.Parse(dt.Rows[0][0].ToString());
            visit.DIAGNOSIS = dt.Rows[0][1].ToString();
            visit.PATIENT_CONDITION = dt.Rows[0][2].ToString();
            visit.DOCTOR_IN_CHARGE = dt.Rows[0][3].ToString();
            visit.PREPAYMENTS = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][4].ToString()) == true ? "0" : dt.Rows[0][4].ToString());
            visit.DEPT_CODE = dt.Rows[0][6].ToString();

            return visit;
        }

        /// <summary>
        /// 是否有患者信息
        /// </summary>
        /// <returns></returns>
        public bool IsGetInpatientRegisterInfo(string number)
        {
            string sql = @"select count(*) from pat_master_index where patient_id='{0}'";
            sql = string.Format(sql, number);
            DataTable dt = new DataTable();
            dt = BaseEntityer.Db.GetDataTable(sql);
            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否有患者信息
        /// </summary>
        /// <returns></returns>
        public bool IsGetInpatientRegisterVisit(string number)
        {
            string sql = @"select count(*) from pat_visit where patient_id='{0}'";
            sql = string.Format(sql, number);
            DataTable dt = new DataTable();
            dt = BaseEntityer.Db.GetDataTable(sql);
            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除患者基本信息
        /// </summary>
        /// <returns></returns>
        public int DeleteInpatientRegisterInfo(BaseEntityer db, string number)
        {
            int info = 0;
            try
            {
                string sql = @"delete from pat_master_index where patient_id='{0}'";
                sql = string.Format(sql, number);
                info = db.ExecuteNonQuery(sql);
            }
            catch
            {

            }
            return info;
        }

        /// <summary>
        /// 住院登记修改就诊信息表
        /// </summary>
        /// <returns></returns>
        public int UpdateInpatientRegisterVisit(BaseEntityer db, string patientID, string visitID, string dept)
        {
            string sql = @"update pat_visit t set t.dept_admission_to='{2}' where t.patient_id = '{0}' and t.visit_id = '{1}'";
            sql = string.Format(sql, patientID, visitID, dept);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 住院登记修改入院诊断信息表
        /// </summary>
        /// <returns></returns>
        public int UpdateInpatientRegisterDiagnose(BaseEntityer db, string patientID, string visitID, string dept)
        {
            string sql = @"update pat_visit t set t.outdiagnosis='{2}' where t.patient_id = '{0}' and t.visit_id = '{1}'";
            sql = string.Format(sql, patientID, visitID, dept);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 住院登记修改转科信息表
        /// </summary>
        /// <returns></returns>
        public int UpdateInpatientRegisterTransfer(BaseEntityer db, string patientID, string dept)
        {
            string sql = @"update pats_in_transferring s set s.dept_transfered_to = '{1}' where s.patient_id='{0}'";
            sql = string.Format(sql, patientID, dept);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询是否有住院登记信息
        /// </summary>
        /// <returns></returns>
        public bool IsInpatientRegisterInfo(string number)
        {
            string sql = @"select count(*) from pat_visit t where t.patient_id='{0}'";
            sql = string.Format(sql, number);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// 查询是否有未出院信息
        /// </summary>
        /// <returns></returns>
        public bool IsInpatientRegisterInHos(string number)
        {
            string sql = @"select count(*) from pat_visit t where t.patient_id='{0}' and t.state not in ('O','N') ";
            sql = string.Format(sql, number);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// 查询住院次数
        /// </summary>
        /// <returns></returns>
        public int GetInpatientRegisterFre(string number)
        {
            string sql = @"select nvl(max(t.visit_id),0) from pat_visit t where t.patient_id='{0}'";
            sql = string.Format(sql, number);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                return 0;
            return int.Parse(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// 查询门诊挂号信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetOutRegisterInfo(string number)
        {
            string sql = @"select max(t.visit_date) from clinic_master t where t.patient_id='{0}'";
            sql = string.Format(sql, number);
            DataTable dt = new DataTable();
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) == true)
                    return null;
                string dtime = DateTime.Parse(dt.Rows[0][0].ToString()).ToString("yyyy-MM-dd");
                //string dvisit = dt.Rows[0][1].ToString();
                sql = @"select s.name,s.sex,s.age from clinic_master s where s.visit_date = to_date('{0}','yyyy-mm-dd') and s.patient_id='{1}'";
                sql = string.Format(sql, dtime, number);
                dt = new DataTable();
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
            }
            catch
            { }
            return dt;
        }

        /// <summary>
        /// 更新床位状态
        /// </summary>
        /// <param name="bedrec"></param>
        public void UpdateBedState(BaseEntityer db, string patientid, string visitid, ref string errMsg)
        {
            string sql2 = @"select t.ward_code,t.bed_no from pats_in_hospital t where t.patient_id='{0}' and t.visit_id='{1}'";
            string sql = @"update  BED_REC set BED_STATUS='0' where WARD_CODE='{0}' and BED_NO='{1}' ";
            sql2 = string.Format(sql2, patientid, visitid);
            try
            {
                DataTable dt = db.GetDataTable(sql2);
                if (dt.Rows.Count <= 0)
                {
                    return;
                }
                sql = string.Format(sql, dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString());
                db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message + "^" + ex.StackTrace;
                return;
            }



        }

        /// <summary>
        /// 查询无费退院患者信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCancelRegisterInfo(string number)
        {
            string sql = @"select a.name,a.sex,a.date_of_birth,c.bed_no,f.dept_name,c.admission_date_time,c.prepayments,nvl(c.total_costs,0),c.visit_id
                from pats_in_hospital c,dept_dict f,pat_master_index a 
                where c.patient_id = '{0}' and c.dept_code = f.dept_code and a.patient_id = c.patient_id";
            sql = string.Format(sql, number);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 删除在院信息与入院科室表
        /// </summary>
        /// <returns></returns>
        public int DeleteInHospitalInfo(BaseEntityer db, string patientID, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"delete from pats_in_transferring where patient_id='{0}'";
                sql = string.Format(sql, patientID);
                IsSuccess = db.ExecuteNonQuery(sql);
                sql = @"delete from pats_in_hospital where patient_id='{0}'";
                sql = string.Format(sql, patientID);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改在院标志
        /// </summary>
        /// <returns></returns>
        public int UpdateVisitState(BaseEntityer db, string patientID, string visitID, string type, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"update pat_visit t set t.discharge_date_time = sysdate,t.state = '{2}' where t.patient_id = '{0}' and t.visit_id = '{1}'";
                sql = string.Format(sql, patientID, visitID, type);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 查询患者合同单位编码
        /// </summary>
        /// <returns></returns>
        public string GetInpatientPactCode(string number, string visit)
        {
            string sql = @"select s.charge_type_code from pat_visit t,charge_type_dict s where t.patient_id='{0}' and t.visit_id='{1}' and s.charge_type_name = t.charge_type";
            sql = string.Format(sql, number, visit);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            string pactcode = dt.Rows[0][0].ToString();
            return pactcode;
        }

        /// <summary>
        /// 获取支付方式名字
        /// </summary>
        /// <returns></returns>
        public string GetPayTypeName(string name)
        {
            string sql = @"select s.id from PAYTYPE s where s.name = '{0}'";
            sql = string.Format(sql, name);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// 获取支付方式ID
        /// </summary>
        /// <returns></returns>
        public string GetPayTypeID(string id)
        {
            string sql = @"select s.name from PAYTYPE s where s.id = '{0}'";
            sql = string.Format(sql, id);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// 获取合同单位列表(自费)
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLPactCodeListByOwn()
        {
            string sql = @"select s.charge_type_code as 编码,s.charge_type_name as 名称,s.spell_code as 拼音码,s.wb_code as 五笔码 from CHARGE_TYPE_DICT s where s.charge_type_code = 1 order by to_number(s.charge_type_code)";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取合同单位列表(医保)
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLPactCodeListByPublic()
        {
            string sql = @"select s.charge_type_code as 编码,s.charge_type_name as 名称,s.spell_code as 拼音码,s.wb_code as 五笔码 from CHARGE_TYPE_DICT s where s.charge_type_code <> 1 order by to_number(s.charge_type_code)";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取患者在院列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetInStateList()
        {
            string sql = @"select s.state_id as 编码,s.state_name as 名称,s.state_spell as 拼音码 from instate_dict s order by s.sno";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取患者基本信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public PAT_MASTER_INDEX GetInpatientBaseInfo(string patientID, ref string errMsg)
        {
            DataTable dt = new DataTable();
            PAT_MASTER_INDEX index = new PAT_MASTER_INDEX();
            string sql = @"select t.inp_no,
                               t.name,
                               t.sex,
                               t.date_of_birth,
                               t.id_no,
                               t.mailing_address,
                               t.phone_number_home,
                               t.phone_number_business,
                               t.next_of_kin,
                               t.next_of_kin_phone,
                               t.next_of_kin_addr
                          from pat_master_index t
                         where t.patient_id = '{0}'";
            try
            {
                sql = string.Format(sql, patientID);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return null;
                index.PATIENT_ID = patientID;
                index.INP_NO = dt.Rows[0][0].ToString();
                index.NAME = dt.Rows[0][1].ToString();
                index.SEX = dt.Rows[0][2].ToString();
                index.DATE_OF_BIRTH = string.IsNullOrEmpty(dt.Rows[0][3].ToString()) == true ? DateTime.MinValue : DateTime.Parse(dt.Rows[0][3].ToString());
                index.ID_NO = dt.Rows[0][4].ToString();
                index.MAILING_ADDRESS = dt.Rows[0][5].ToString();
                index.PHONE_NUMBER_HOME = dt.Rows[0][6].ToString();
                index.PHONE_NUMBER_BUSINESS = dt.Rows[0][7].ToString();
                index.NEXT_OF_KIN = dt.Rows[0][8].ToString();
                index.NEXT_OF_KIN_PHONE = dt.Rows[0][9].ToString();
                index.NEXT_OF_KIN_ADDR = dt.Rows[0][10].ToString();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
            return index;
        }

        /// <summary>
        /// 患者基本信息修改
        /// </summary>
        /// <returns></returns>
        public int UpdateInpatientBase(BaseEntityer db, string patientID, PAT_MASTER_INDEX index, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"update pat_master_index t set t.inp_no='{1}',
                                               t.name='{2}',
                                               t.sex='{3}',
                                               t.date_of_birth = to_date('{4}','yyyy-mm-dd hh24:mi:ss'),
                                               t.id_no='{5}',
                                               t.mailing_address='{6}',
                                               t.phone_number_home='{7}',
                                               t.phone_number_business='{8}',
                                               t.next_of_kin='{9}',
                                               t.next_of_kin_phone='{10}',
                                               t.next_of_kin_addr='{11}'
                                         where t.patient_id = '{0}'";
                sql = string.Format(sql, patientID,
                index.INP_NO,
                index.NAME,
                index.SEX,
                index.DATE_OF_BIRTH == DateTime.MinValue.Date ? null : index.DATE_OF_BIRTH,
                index.ID_NO,
                index.MAILING_ADDRESS,
                index.PHONE_NUMBER_HOME,
                index.PHONE_NUMBER_BUSINESS,
                index.NEXT_OF_KIN,
                index.NEXT_OF_KIN_PHONE,
                index.NEXT_OF_KIN_ADDR);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 入院诊断变更
        /// </summary>
        /// <returns></returns>
        public int UpdateInpatientOutDiagnose(BaseEntityer db, string patientID, string visitID, string diagnooseCode, string diagnoseName, string operCode, ref string err)
        {
            try
            {
                string sql = @"UPDATE pat_visit t
                                SET t.outdiagnosis = '{2}'
                                WHERE t.patient_id = '{0}'
                                AND t.visit_id = '{1}'
                            ";

                string sql2 = @"     UPDATE diagnosis t
                                       SET t.diagnosis_desc = '{2}',
                                           t.diag_code      = '{3}'  ,
                                           t.oper_code='{4}'
                                     WHERE t.patient_id = '{0}'
                                       AND t.visit_id = '{1}'
                                       AND t.diagnosis_type = '1'
                    ";

                string sql3 = @"UPDATE pats_in_hospital t
                                   SET t.diagnosis = '{2}'
                                 WHERE t.patient_id = '{0}'
                                   AND t.visit_id = '{1}'
                                ";
                sql = string.Format(sql, patientID, visitID, diagnoseName);

                sql2 = string.Format(sql2, patientID, visitID, diagnoseName, diagnooseCode, operCode);

                sql3 = string.Format(sql3, patientID, visitID, diagnoseName);

                int rev = db.ExecuteNonQuery(sql);
                if (rev > 0)
                {
                    int rev3 = db.ExecuteNonQuery(sql2);

                    if (rev3 > 0)
                    {
                        return db.ExecuteNonQuery(sql3);
                    }
                    else
                        return -1;
                }
                else
                    return -1;
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 费别变更
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="pactcode"></param>
        /// <param name="pactname"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdatePactCode(BaseEntityer db, string patientID, string visitID, string pactcode, string pactname, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"update pat_visit t
                               set t.charge_type_code = '{2}', t.charge_type = '{3}'
                             where t.patient_id = '{0}'
                               and t.visit_id = '{1}'";
                sql = string.Format(sql, patientID,
                visitID,
                pactcode,
                pactname);
                IsSuccess = db.ExecuteNonQuery(sql);


                //更改检查申请中的费用类别
                string sql3 = @"update LAB_TEST_MASTER t
                                   set t.CHARGE_TYPE = '{2}' 
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'";
                sql3 = string.Format(sql3, patientID, visitID, pactname);

                IsSuccess = db.ExecuteNonQuery(sql3);

                //更改检验申请中的费用类别
                string sql4 = @"update EXAM_APPOINTS t
                                   set t.CHARGE_TYPE = '{2}' 
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'";
                sql4 = string.Format(sql4, patientID, visitID, pactname);

                IsSuccess = db.ExecuteNonQuery(sql4);


                string sql2 = @"update pat_master_index t
                                   set t.charge_type = '{1}',t.pact_code='" + pactcode + @"' 
                                 where t.patient_id = '{0}'";
                sql2 = string.Format(sql2, patientID, pactname);

                IsSuccess = db.ExecuteNonQuery(sql2);


            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 插入费别变更日志表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="pactcode"></param>
        /// <param name="pactname"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertChangeUpdate(BaseEntityer db, CHANGEPACTCODELOG log, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"insert into CHANGEPACTCODELOG
                                          (PATIENT_ID,
                                           VISIT_ID,
                                           SERIAL_NO,
                                           OLD_CHARGE_CODE,
                                           OLD_CHARGE_NAME,
                                           NEW_CHARGE_CODE,
                                           NEW_CHARGE_NAME,
                                           OPER_CODE,
                                           OPER_DATE,
                                           REMARK)
                                        values
                                          ('{0}',
                                           '{1}',
                                           (select nvl(max(t.SERIAL_NO) + 1, 1)
                                              from CHANGEPACTCODELOG t
                                             where t.patient_id = '{0}'
                                               and t.visit_id = '{1}'),
                                           '{2}',
                                           '{3}',
                                           '{4}',
                                           '{5}',
                                           '{6}',
                                           to_date('{7}', 'yyyy-mm-dd hh24:mi:ss'),
                                           '{8}')
";
                sql = string.Format(sql,
                log.PATIENT_ID,
                log.VISIT_ID,
                log.OLD_CHARGE_CODE,
                log.OLD_CHARGE_NAME,
                log.NEW_CHARGE_CODE,
                log.NEW_CHARGE_NAME,
                log.OPER_CODE,
                log.OPER_DATE,
                log.REMARK);
                IsSuccess = db.ExecuteNonQuery(sql);
                if (IsSuccess == 0)
                {
                    err = "插入日志表错误。";
                    return -1;
                }
            }
            catch (Exception e)
            {
                err = e.Message;
                return -1;
            }
            return IsSuccess;
        }
        #endregion

        #region 预交金管理
        /// <summary>
        /// 获取最大预交金发票(按操作员)
        /// </summary>
        /// <returns></returns>
        public string GetPrepayOperatorMaxInvoice(string operatorID, ref string errmsg)
        {
            string invoice = string.Empty;
            try
            {
                string sql = @"select nvl(max(to_number(substr(t.rcpt_no,5,10))),0) from prepayment_rcpt t where t.operator_no='{0}'";
                sql = string.Format(sql, operatorID);
                DataTable dt = BaseEntityer.Db.GetDataTable(sql);
                invoice = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return invoice;
        }

        /// <summary>
        /// 插入预交金金额
        /// </summary>
        /// <param name="db"></param>
        /// <param name="prepay"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertPrepayment(BaseEntityer db, PREPAYMENT_RCPT prepay, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"insert into prepayment_rcpt s (s.patient_id,s.rcpt_no,s.amount,s.pay_way,s.bank,s.check_no,
                    s.transact_date,s.operator_no,s.refunded_rcpt_no,s.acct_no,s.visit_id,s.TRANSACT_TYPE,s.BALANCE_INVOICE,s.is_flag,s.ACC_FLAG,s.INVOICE,s.MEMO,s.PREPAY_OPER) values
                    ('{0}','{1}','{2}','{3}','{4}','{5}',to_date('{6}','yyyy-mm-dd hh24:mi:ss'),'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')";
                sql = string.Format(sql, prepay.PATIENT_ID,//0
                    prepay.RCPT_NO,//1
                    prepay.AMOUNT,//2
                    prepay.PAY_WAY,//3
                    prepay.BANK,//4
                    prepay.CHECK_NO,//5
                    prepay.TRANSACT_DATE,//6
                    prepay.OPERATOR_NO,//7
                    prepay.REFUNDED_RCPT_NO,//8
                    prepay.ACCT_NO,//9
                    prepay.VISIT_ID,//10
                    prepay.TRANSACT_TYPE,//11
                    prepay.BALANCE_INVOICE,//12
                    prepay.IS_FLAG,//13
                    prepay.ACC_FLAG,//14
                    prepay.INVOICE,//15
                    prepay.MEMO,//16
                    prepay.PREPAY_OPER//17
                    );
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改预交金有效标志
        /// </summary>
        /// <param name="db"></param>
        /// <param name="prepay"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdatePrepaymentFlag(BaseEntityer db, string rcpt, string isflag, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"update prepayment_rcpt set prepayment_rcpt.is_flag='{1}' where prepayment_rcpt.rcpt_no='{0}'";
                sql = string.Format(sql, rcpt, isflag);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 获取预交金明细byrcpt_no
        /// </summary>
        /// <returns></returns>
        public PREPAYMENT_RCPT GetPrepayDetail(string rcpt_no, ref string errmsg)
        {
            PREPAYMENT_RCPT prepay = new PREPAYMENT_RCPT();
            try
            {
                string sql = @"select s.patient_id,s.rcpt_no,s.amount,s.pay_way,s.bank,s.check_no,s.transact_date,
                    s.operator_no,s.refunded_rcpt_no,s.acct_no,s.visit_id,s.invoice from prepayment_rcpt s 
                    where s.rcpt_no='{0}'";
                sql = string.Format(sql, rcpt_no);
                DataTable dt = new DataTable();
                dt = BaseEntityer.Db.GetDataTable(sql);
                prepay.PATIENT_ID = dt.Rows[0][0].ToString();
                prepay.RCPT_NO = dt.Rows[0][1].ToString();
                prepay.AMOUNT = decimal.Parse(dt.Rows[0][2].ToString());
                prepay.PAY_WAY = dt.Rows[0][3].ToString();
                prepay.BANK = dt.Rows[0][4].ToString();
                prepay.CHECK_NO = dt.Rows[0][5].ToString();
                prepay.TRANSACT_DATE = DateTime.Parse(dt.Rows[0][6].ToString());
                prepay.OPERATOR_NO = dt.Rows[0][7].ToString();
                prepay.REFUNDED_RCPT_NO = dt.Rows[0][8].ToString();
                prepay.ACCT_NO = dt.Rows[0][9].ToString();
                prepay.VISIT_ID = decimal.Parse(dt.Rows[0][10].ToString());
                prepay.INVOICE = dt.Rows[0][11].ToString();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return prepay;
        }

        /// <summary>
        /// 获取预交金明细byDataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetPrepayDetailsByDataTable(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select s.patient_id,s.rcpt_no,s.amount,s.pay_way,s.bank,s.check_no,s.transact_type,s.transact_date,
                    s.operator_no,s.refunded_rcpt_no,s.acct_no,s.visit_id from prepayment_rcpt s 
                    where s.patient_id = '{0}' and s.visit_id = '{1}' and s.is_flag = 1";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取预交金明细byDataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetPrepayDetailsByDataTable2(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select s.patient_id as 患者ID,s.rcpt_no as 记录号,s.amount as 金额,s.pay_way as 支付方式,s.bank as 银行,s.check_no as 支票号,decode(s.transact_type,'1','交款','2','退款','其他') as 交易类型,s.transact_date as 交易日期,
                    s.operator_no as 操作员,s.refunded_rcpt_no as 退费发票号,s.invoice as 发票号, PREPAY_OPER as 交款人 from prepayment_rcpt s 
                    where s.patient_id = '{0}' and s.visit_id = '{1}' and s.is_flag = 1";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取预交金明细bylist
        /// </summary>
        /// <returns></returns>
        public List<PREPAYMENT_RCPT> GetPrepayDetailsByList(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<PREPAYMENT_RCPT> list = new List<PREPAYMENT_RCPT>();
            PREPAYMENT_RCPT rcpt = null;
            try
            {
                string sql = @"select s.patient_id,s.rcpt_no,s.amount,s.pay_way,s.bank,s.check_no,s.transact_type,s.transact_date,
                    s.operator_no,s.refunded_rcpt_no,s.acct_no,s.visit_id,s.balance_invoice,s.is_flag,s.invoice from prepayment_rcpt s 
                    where s.patient_id = '{0}' and s.visit_id = '{1}' and s.balance_invoice is not null";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    rcpt = new PREPAYMENT_RCPT();
                    rcpt.PATIENT_ID = dr.ItemArray[0].ToString();
                    rcpt.RCPT_NO = dr.ItemArray[1].ToString();
                    rcpt.AMOUNT = decimal.Parse(dr.ItemArray[2].ToString());
                    rcpt.PAY_WAY = dr.ItemArray[3].ToString();
                    rcpt.BANK = dr.ItemArray[4].ToString();
                    rcpt.CHECK_NO = dr.ItemArray[5].ToString();
                    rcpt.TRANSACT_TYPE = dr.ItemArray[6].ToString();
                    rcpt.TRANSACT_DATE = DateTime.Parse(dr.ItemArray[7].ToString());
                    rcpt.OPERATOR_NO = dr.ItemArray[8].ToString();
                    rcpt.REFUNDED_RCPT_NO = dr.ItemArray[9].ToString();
                    rcpt.ACCT_NO = dr.ItemArray[10].ToString();
                    rcpt.VISIT_ID = decimal.Parse(dr.ItemArray[11].ToString());
                    rcpt.BALANCE_INVOICE = dr.ItemArray[12].ToString();
                    rcpt.IS_FLAG = int.Parse(dr.ItemArray[13].ToString());
                    rcpt.INVOICE = dr.ItemArray[14].ToString();
                    list.Add(rcpt);
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return list;
        }

        /// <summary>
        /// 获取预交金明细bylist(结算用)
        /// </summary>
        /// <returns></returns>
        public List<PREPAYMENT_RCPT> GetPrepayDetailsByBalance(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<PREPAYMENT_RCPT> list = new List<PREPAYMENT_RCPT>();
            PREPAYMENT_RCPT rcpt = null;
            try
            {
                string sql = @"select s.patient_id,s.rcpt_no,s.amount,s.pay_way,s.bank,s.check_no,s.transact_type,s.transact_date,
                    s.operator_no,s.refunded_rcpt_no,s.acct_no,s.visit_id,s.balance_invoice,s.is_flag,s.invoice from prepayment_rcpt s 
                    where s.patient_id = '{0}' and s.visit_id = '{1}' and s.balance_invoice is null and s.is_flag = 1";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    rcpt = new PREPAYMENT_RCPT();
                    rcpt.PATIENT_ID = dr.ItemArray[0].ToString();
                    rcpt.RCPT_NO = dr.ItemArray[1].ToString();
                    rcpt.AMOUNT = decimal.Parse(dr.ItemArray[2].ToString());
                    rcpt.PAY_WAY = dr.ItemArray[3].ToString();
                    rcpt.BANK = dr.ItemArray[4].ToString();
                    rcpt.CHECK_NO = dr.ItemArray[5].ToString();
                    rcpt.TRANSACT_TYPE = dr.ItemArray[6].ToString();
                    rcpt.TRANSACT_DATE = DateTime.Parse(dr.ItemArray[7].ToString());
                    rcpt.OPERATOR_NO = dr.ItemArray[8].ToString();
                    rcpt.REFUNDED_RCPT_NO = dr.ItemArray[9].ToString();
                    rcpt.ACCT_NO = dr.ItemArray[10].ToString();
                    rcpt.VISIT_ID = decimal.Parse(dr.ItemArray[11].ToString());
                    rcpt.BALANCE_INVOICE = dr.ItemArray[12].ToString();
                    rcpt.IS_FLAG = int.Parse(dr.ItemArray[13].ToString());
                    rcpt.INVOICE = dr.ItemArray[14].ToString();
                    list.Add(rcpt);
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return list;
        }

        /// <summary>
        /// 获取预交金明细bylist(结算召回用)
        /// </summary>
        /// <returns></returns>
        public List<PREPAYMENT_RCPT> GetRePrepayDetailsByBalance(string patientid, string visitid, string balanceInvoice, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<PREPAYMENT_RCPT> list = new List<PREPAYMENT_RCPT>();
            PREPAYMENT_RCPT rcpt = null;
            //{4F2F6C17-914D-4C46-A7AC-6EDEBF5BDAC2} 修改业务方法 加入transact_type =‘9’ 门诊收费预交金
            try
            {
                string sql = @"select s.patient_id,s.rcpt_no,s.amount,s.pay_way,s.bank,s.check_no,s.transact_type,s.transact_date,
                    s.operator_no,s.refunded_rcpt_no,s.acct_no,s.visit_id,s.balance_invoice,s.is_flag,s.invoice from prepayment_rcpt s 
                    where s.patient_id = '{0}' and s.visit_id = '{1}' and s.balance_invoice ='{2}' and s.transact_type in ('1','2','9') and s.is_flag = 1 order by s.transact_type";
                sql = string.Format(sql, patientid, visitid, balanceInvoice);
                dt = BaseEntityer.Db.GetDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    rcpt = new PREPAYMENT_RCPT();
                    rcpt.PATIENT_ID = dr.ItemArray[0].ToString();
                    rcpt.RCPT_NO = dr.ItemArray[1].ToString();
                    rcpt.AMOUNT = decimal.Parse(dr.ItemArray[2].ToString());
                    rcpt.PAY_WAY = dr.ItemArray[3].ToString();
                    rcpt.BANK = dr.ItemArray[4].ToString();
                    rcpt.CHECK_NO = dr.ItemArray[5].ToString();
                    rcpt.TRANSACT_TYPE = dr.ItemArray[6].ToString();
                    rcpt.TRANSACT_DATE = DateTime.Parse(dr.ItemArray[7].ToString());
                    rcpt.OPERATOR_NO = dr.ItemArray[8].ToString();
                    rcpt.REFUNDED_RCPT_NO = dr.ItemArray[9].ToString();
                    rcpt.ACCT_NO = dr.ItemArray[10].ToString();
                    rcpt.VISIT_ID = decimal.Parse(dr.ItemArray[11].ToString());
                    rcpt.BALANCE_INVOICE = dr.ItemArray[12].ToString();
                    rcpt.IS_FLAG = int.Parse(dr.ItemArray[13].ToString());
                    rcpt.INVOICE = dr.ItemArray[14].ToString();
                    list.Add(rcpt);
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return list;
        }

        /// <summary>
        /// 获取预交金明细(限预交金管理DataGridview用)
        /// </summary>
        /// <returns></returns>
        public DataTable GetPrepayDetailsByDataGridview(string patientid, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @" select e.patient_id as 病人ID,
                                       e.rcpt_no as 收据号,
                                       e.amount as 预交金额,
                                       t.name as 支付方式,
                                       decode(e.transact_type, '1', '交费', '2', '退费', '其他') as 操作类型,
                                       e.refunded_rcpt_no as 退费发票号,
                                       e.transact_date as 操作日期,
                                       e.operator_no as 操作员,
                                       e.bank as 开户行,
                                       e.check_no as 支票号,
                                       e.invoice as 发票号,
                                       e.memo as 备注,
                                       prepay_oper as  交款人
                                  from prepayment_rcpt e, paytype t
                                 where e.patient_id = '{0}'
                                   and e.visit_id = '{1}'
                                   and t.id = e.pay_way
                                   and e.balance_invoice is null
                                   and e.is_flag = 1
                                 order by e.transact_date desc ";
                sql = string.Format(sql, patientid, visitID);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取预交金总额
        /// </summary>
        /// <returns></returns>
        public string GetPrepayTotal(string patientid, string visitid, ref string errmsg)
        {
            string sum = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                string sql = @"select nvl(sum(c.amount),0) from prepayment_rcpt c where c.patient_id='{0}' and c.visit_id='{1}' and c.is_flag=1 and c.transact_type in ('1','2','3','4','9')";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
                sum = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return sum;
        }

        /// <summary>
        /// 修改预交金总额
        /// </summary>
        /// <returns></returns>
        public int UpdatePrepayInpatient(BaseEntityer db, string patientID, string prepay, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"update pats_in_hospital f set f.prepayments = '{1}' where f.patient_id='{0}'";
                sql = string.Format(sql, patientID, prepay);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改预交金正记录标志（退费用）
        /// </summary>
        /// <returns></returns>
        public int UpdatePrepayInfoByDisCharge(BaseEntityer db, string rcpt_no, string refunded_rcpt_no, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"update prepayment_rcpt c set c.refunded_rcpt_no='{1}' where c.rcpt_no='{0}' and c.is_flag=1";
                sql = string.Format(sql, rcpt_no, refunded_rcpt_no);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改预交金标记（结算用）
        /// </summary>
        /// <returns></returns>
        public int UpdatePrepayInfoByBalInvoice(BaseEntityer db, string patientid, string visitid, string balInvoice, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                //{4F2F6C17-914D-4C46-A7AC-6EDEBF5BDAC2} 修改业务方法 加入transact_type =‘9’ 门诊收费预交金
                string sql = @"update prepayment_rcpt t
                                   set t.balance_invoice = '{2}'
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'
                                   and t.transact_type in ('1', '2','9') -- 9为门诊收预交金
                                   and t.balance_invoice is null
                                   and t.is_flag = 1";
                sql = string.Format(sql, patientid, visitid, balInvoice);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改预交金标记（结算用）
        /// </summary>
        /// <returns></returns>
        public int UpdatePrepayInfoByNotNullBalInvoice(BaseEntityer db, string patientid, string visitid, string balInvoice, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                //{4F2F6C17-914D-4C46-A7AC-6EDEBF5BDAC2} 修改业务方法 加入transact_type =‘9’ 门诊收费预交金
                string sql = @"update prepayment_rcpt t
                                   set t.balance_invoice = '{2}'
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'
                                   and t.transact_type in ('1', '2','9')
                                   and t.balance_invoice is not null
                                   and t.is_flag = 1";
                sql = string.Format(sql, patientid, visitid, balInvoice);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }
        #endregion

        #region 出院结算

        /// <summary>
        /// 在院状态
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public string InState(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            string inState = string.Empty;
            try
            {
                string sql = @"select t.state from pat_visit t where t.patient_id = '{0}' and t.visit_id = '{1}'";
                sql = string.Format(sql, patientID, visitID);
                dt = BaseEntityer.Db.GetDataTable(sql);
                inState = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return inState;
        }

        /// <summary>
        /// 是否有未停的长期医嘱
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public bool IsStopLongOrders(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            bool isHave = false;
            try
            {
                string sql = @"SELECT count(*)  
      FROM ORDERS  
      WHERE ( ORDERS.PATIENT_ID = '{0}' ) AND  
            ( ORDERS.VISIT_ID = '{1}' ) AND  
            ( ORDERS.REPEAT_INDICATOR = 1 ) AND 
            ( ORDERS.BILLING_ATTR = 0 OR ORDERS.BILLING_ATTR = 2) AND 
            ( ORDERS.ORDER_STATUS <> '4') AND
            ( ORDERS.STOP_DATE_TIME is NULL ) ";
                sql = string.Format(sql, patientID, visitID);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows[0][0].ToString() == "0")
                {
                    isHave = false;
                }
                else
                {
                    isHave = true;
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return isHave;
        }

        /// <summary>
        /// 是否有未计价的医嘱
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public bool IsNOValuationOrders(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            bool isHave = false;
            try
            {
                string sql = @"SELECT count(*) FROM ORDERS
                     WHERE (PATIENT_ID = '{0}')
                       AND (VISIT_ID = '{1}')
                       AND (ORDERS.ORDER_STATUS <> '4')
                       AND (BILLING_ATTR = 2 OR BILLING_ATTR = 0)
                       AND STOP_DATE_TIME IS NULL
                       AND REPEAT_INDICATOR = 1";
                sql = string.Format(sql, patientID, visitID);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows[0][0].ToString() == "0")
                {
                    isHave = false;
                }
                else
                {
                    isHave = true;
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return isHave;
        }

        /// <summary>
        /// 是否有未做的检查申请
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public bool IsNOCheckApplication(string patientID,string visit_no , ref string errmsg)
        {
            DataTable dt = new DataTable();
            bool isHave = false;
            try
            {
                string sql = @"SELECT count(*)
                              FROM Exam_Appoints
                             WHERE (PATIENT_ID = '{0}')
                               AND (VISIT_ID = '{1}')";
                sql = string.Format(sql, patientID, visit_no);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows[0][0].ToString() == "0")
                {
                    isHave = false;
                }
                else
                {
                    isHave = true;
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return isHave;
        }

        /// <summary>
        /// 是否有未做的化验申请
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public bool IsNOLabApplication(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            bool isHave = false;
            try
            {
                string sql = @"SELECT count(*)
                              FROM LAB_TEST_MASTER
                             WHERE (PATIENT_ID = '{0}')
                               AND (VISIT_ID = '{1}')
                               AND (BILLING_INDICATOR = 0)";
                sql = string.Format(sql, patientID, visitID);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows[0][0].ToString() == "0")
                {
                    isHave = false;
                }
                else
                {
                    isHave = true;
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return isHave;
        }

        /// <summary>
        /// 获取预交金明细(出院结算用)
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public DataTable GetPrepayDetailbyBalance(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @" select m.name as 支付方式,
                                       t.amount as 预交金额,
                                       decode(t.transact_type, '1', '交费', '2', '退费', '其他') as 类型,
                                       t.operator_no as 操作员
                                  from PREPAYMENT_RCPT t, paytype m
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'
                                   and t.balance_invoice is null
                                   and t.is_flag = 1
                                   and t.pay_way = m.id
                                 order by t.transact_date ";
                sql = string.Format(sql, patientID, visitID);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取已结费用(按费用大类)
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public DataTable GetBalanceDetailByFeeType(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();

            try
            {
                string sql = @"
select s.fee_class_name,
                               sum(t.charges)       
                          from INP_BILL_DETAIL t, INP_RCPT_FEE_DICT s
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}'
                           and t.class_on_inp_rcpt = s.fee_class_code
                           and t.rcpt_no is not null
                         group by s.fee_class_name, t.rcpt_no 
                         order by s.fee_class_name
";
                //select s.fee_class_name,
                //               sum(t.charges)       
                //          from INP_BILL_DETAIL t, inp_rcpt_fee_dict s
                //         where t.patient_id = '{0}'
                //           and t.visit_id = '{1}'
                //           and t.item_class = s.fee_class_code
                //           and t.rcpt_no is not null
                //         group by s.fee_class_name, t.item_class,t.rcpt_no 
                //         order by t.item_class
                sql = string.Format(sql, patientID, visitID);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取未结费用(按费用大类)
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public DataTable GetBalanceDetailByNoFeeType(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();

            try
            {
                string sql = @"
select s.fee_class_name,
                               sum(t.charges)       
                          from INP_BILL_DETAIL t, INP_RCPT_FEE_DICT s
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}'
                           and t.class_on_inp_rcpt = s.fee_class_code
                           and t.rcpt_no is null
                         group by s.fee_class_name, t.rcpt_no 
                         order by s.fee_class_name
";
                //                string sql = @"select s.fee_class_name,
                //                               sum(t.charges)       
                //                          from INP_BILL_DETAIL t, inp_rcpt_fee_dict s
                //                         where t.patient_id = '{0}'
                //                           and t.visit_id = '{1}'
                //                           and t.item_class = s.fee_class_code
                //                           and t.rcpt_no is null
                //                         group by s.fee_class_name, t.item_class,t.rcpt_no 
                //                         order by t.item_class";
                sql = string.Format(sql, patientID, visitID);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取费用明细(按照系统类别分组)
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<INP_SETTLE_DETAIL> GetBalanceBillDetails(string patientID, string visitID, string rcptno, string invoice, DateTime date, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<INP_SETTLE_DETAIL> list = new List<INP_SETTLE_DETAIL>();
            try
            {
                #region
                string sql = @"select t.class_on_inp_rcpt, s.fee_class_name, sum(t.costs), sum(t.charges)
                                  from INP_BILL_DETAIL t, inp_rcpt_fee_dict s
                                 where t.class_on_inp_rcpt = s.fee_class_code
                                   and t.patient_id = '{0}'
                                   and t.visit_id = '{1}'
                                   and t.rcpt_no is null
                                   and t.billing_date_time <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                 group by t.class_on_inp_rcpt, s.fee_class_name
                                 order by t.class_on_inp_rcpt";
                sql = string.Format(sql, patientID, visitID, date.ToString());
                #endregion

                dt = BaseEntityer.Db.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    #region 赋值
                    INP_SETTLE_DETAIL detail = new INP_SETTLE_DETAIL();
                    detail.FEE_CLASS_ID = dr.ItemArray[0].ToString();
                    detail.FEE_CLASS_NAME = dr.ItemArray[1].ToString();
                    detail.COSTS = decimal.Parse(dr.ItemArray[2].ToString());
                    detail.PAYMENTS = decimal.Parse(dr.ItemArray[3].ToString());
                    detail.RCPT_NO = rcptno;
                    detail.INVOICE = invoice;
                    list.Add(detail);
                    #endregion
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return list;
        }

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
                                       t.SUBJ_CODE,
                                       t.CLASS_ON_INP_RCPT
                                  from INP_BILL_DETAIL t
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'
                                   and t.rcpt_no is null";
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
                    bill.BILLING_DATE_TIME = string.IsNullOrEmpty(dr.ItemArray[13].ToString()) == true ? DateTime.MinValue : DateTime.Parse(dr.ItemArray[13].ToString());
                    bill.OPERATOR_NO = dr.ItemArray[14].ToString();
                    bill.RCPT_NO = dr.ItemArray[15].ToString();
                    bill.UP_FLAG = dr.ItemArray[16].ToString();
                    bill.UP_TIME_DATE = string.IsNullOrEmpty(dr.ItemArray[17].ToString()) == true ? DateTime.MinValue : DateTime.Parse(dr.ItemArray[17].ToString());
                    bill.UP_OPERATOR_NO = dr.ItemArray[18].ToString();
                    bill.FORMULARYNO = dr.ItemArray[19].ToString();
                    bill.DOCTOR = dr.ItemArray[20].ToString();
                    bill.SUBJ_CODE = dr.ItemArray[21].ToString();
                    bill.CLASS_ON_INP_RCPT = dr.ItemArray[22].ToString();
                    list.Add(bill);
                    #endregion
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return list;
        }

        /// <summary>
        /// 获取所有费用明细
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="rcpt"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<INP_BILL_DETAIL> GetBalanceDetailsByRcpt(string patientID, string visitID, string rcpt, ref string errmsg)
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
                                       t.doctor
                                  from INP_BILL_DETAIL t
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'
                                   and t.rcpt_no = '{2}'";
                sql = string.Format(sql, patientID, visitID, rcpt);
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
                    bill.BILLING_DATE_TIME = string.IsNullOrEmpty(dr.ItemArray[13].ToString()) == true ? DateTime.MinValue : DateTime.Parse(dr.ItemArray[13].ToString()); ;
                    bill.OPERATOR_NO = dr.ItemArray[14].ToString();
                    bill.RCPT_NO = dr.ItemArray[15].ToString();
                    bill.UP_FLAG = dr.ItemArray[16].ToString();
                    bill.UP_TIME_DATE = string.IsNullOrEmpty(dr.ItemArray[17].ToString()) == true ? DateTime.MinValue : DateTime.Parse(dr.ItemArray[17].ToString());
                    bill.UP_OPERATOR_NO = dr.ItemArray[18].ToString();
                    bill.FORMULARYNO = dr.ItemArray[19].ToString();
                    bill.DOCTOR = dr.ItemArray[20].ToString();
                    list.Add(bill);
                    #endregion
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return list;
        }

        /// <summary>
        /// 获取所有费用明细
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="rcpt"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<INP_BILL_DETAIL> GetBalanceDetailsByRcptISNULL(string patientID, string visitID, ref string errmsg)
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
                                       t.SUBJ_CODE,
                                       t.CLASS_ON_INP_RCPT
                                  from INP_BILL_DETAIL t
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'
                                   and t.rcpt_no is null";
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
                    bill.BILLING_DATE_TIME = string.IsNullOrEmpty(dr.ItemArray[13].ToString()) == true ? DateTime.MinValue : DateTime.Parse(dr.ItemArray[13].ToString()); ;
                    bill.OPERATOR_NO = dr.ItemArray[14].ToString();
                    bill.RCPT_NO = dr.ItemArray[15].ToString();
                    bill.UP_FLAG = dr.ItemArray[16].ToString();
                    bill.UP_TIME_DATE = string.IsNullOrEmpty(dr.ItemArray[17].ToString()) == true ? DateTime.MinValue : DateTime.Parse(dr.ItemArray[17].ToString());
                    bill.UP_OPERATOR_NO = dr.ItemArray[18].ToString();
                    bill.FORMULARYNO = dr.ItemArray[19].ToString();
                    bill.DOCTOR = dr.ItemArray[20].ToString();
                    bill.SUBJ_CODE = dr.ItemArray[21].ToString();
                    bill.CLASS_ON_INP_RCPT = dr.ItemArray[22].ToString();
                    list.Add(bill);
                    #endregion
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return list;
        }

        /// <summary>
        /// 获取所有医嘱
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<ORDERS> GetBalanceOrders(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<ORDERS> list = new List<ORDERS>();
            try
            {
                #region
                string sql = @"select t.patient_id,t.visit_id,
                                       t.order_no,t.order_sub_no,
                                       t.repeat_indicator,t.order_class,
                                       t.order_text,t.order_code,
                                       t.dosage,t.dosage_units,
                                       t.administration,t.duration,
                                       t.duration_units,t.start_date_time,
                                       t.stop_date_time,t.frequency,
                                       t.freq_counter,t.freq_interval,
                                       t.freq_interval_unit,t.freq_detail,
                                       t.perform_schedule,t.perform_result,
                                       t.ordering_dept,t.doctor,
                                       t.stop_doctor,t.nurse,
                                       t.stop_nurse,t.enter_date_time,
                                       t.stop_order_date_time,t.order_status,
                                       t.drug_billing_attr,t.billing_attr,
                                       t.last_perform_date_time,t.last_accting_date_time
                                  from Orders t
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'";
                sql = string.Format(sql, patientID, visitID);
                #endregion

                dt = BaseEntityer.Db.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    #region 赋值
                    ORDERS order = new ORDERS();
                    order.PATIENT_ID = dr.ItemArray[0].ToString();
                    order.VISIT_ID = int.Parse(dr.ItemArray[1].ToString());
                    order.ORDER_NO = int.Parse(dr.ItemArray[2].ToString());
                    order.ORDER_SUB_NO = int.Parse(dr.ItemArray[3].ToString());
                    order.REPEAT_INDICATOR = int.Parse(dr.ItemArray[4].ToString());
                    order.ORDER_CLASS = dr.ItemArray[5].ToString();
                    order.ORDER_TEXT = dr.ItemArray[6].ToString();
                    order.ORDER_CODE = dr.ItemArray[7].ToString();
                    order.DOSAGE = decimal.Parse(dr.ItemArray[8].ToString());
                    order.DOSAGE_UNITS = dr.ItemArray[9].ToString();
                    order.ADMINISTRATION = dr.ItemArray[10].ToString();
                    order.DURATION = int.Parse(dr.ItemArray[11].ToString());
                    order.DURATION_UNITS = dr.ItemArray[12].ToString();
                    order.START_DATE_TIME = DateTime.Parse(dr.ItemArray[13].ToString()).Date;
                    order.STOP_DATE_TIME = DateTime.Parse(dr.ItemArray[14].ToString()).Date;
                    order.FREQUENCY = dr.ItemArray[15].ToString();
                    order.FREQ_COUNTER = int.Parse(dr.ItemArray[16].ToString());
                    order.FREQ_INTERVAL = int.Parse(dr.ItemArray[17].ToString());
                    order.FREQ_INTERVAL_UNIT = dr.ItemArray[18].ToString();
                    order.FREQ_DETAIL = dr.ItemArray[19].ToString();
                    order.PERFORM_SCHEDULE = dr.ItemArray[20].ToString();
                    order.PERFORM_RESULT = dr.ItemArray[21].ToString();
                    order.ORDERING_DEPT = dr.ItemArray[22].ToString();
                    order.DOCTOR = dr.ItemArray[23].ToString();
                    order.STOP_DOCTOR = dr.ItemArray[24].ToString();
                    order.NURSE = dr.ItemArray[25].ToString();
                    order.STOP_NURSE = dr.ItemArray[26].ToString();
                    order.ENTER_DATE_TIME = DateTime.Parse(dr.ItemArray[27].ToString()).Date;
                    order.STOP_ORDER_DATE_TIME = DateTime.Parse(dr.ItemArray[28].ToString()).Date;
                    order.ORDER_STATUS = dr.ItemArray[29].ToString();
                    order.DRUG_BILLING_ATTR = int.Parse(dr.ItemArray[30].ToString());
                    order.BILLING_ATTR = int.Parse(dr.ItemArray[31].ToString());
                    order.LAST_PERFORM_DATE_TIME = DateTime.Parse(dr.ItemArray[32].ToString()).Date;
                    order.LAST_ACCTING_DATE_TIME = DateTime.Parse(dr.ItemArray[33].ToString()).Date;
                    list.Add(order);
                    #endregion
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return list;
        }

        /// <summary>
        /// 获取出院结算发票号
        /// </summary>
        /// <param name="oper_code"></param>
        /// <returns></returns>
        public string GetBalanceInvoice(string oper_code, ref string errmsg)
        {
            DataTable dt = new DataTable();
            string invoice = string.Empty;
            try
            {
                string sql = @"SELECT decode(nvl(Max(to_number(RCPT_NO)),1),'1',1,substr(nvl(Max(to_number(RCPT_NO)),1),5,8))
                                    FROM inp_settle_master
                                   WHERE OPERATOR_NO = '{0}'";
                sql = string.Format(sql, oper_code);
                dt = BaseEntityer.Db.GetDataTable(sql);
                invoice = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return invoice;
        }

        /// <summary>
        /// 修改费用明细的rcpt为结算发票号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="rcptno"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateDetailRcpt(BaseEntityer db, string patientid, string visitid, string rcptno, DateTime date, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"UPDATE INP_BILL_DETAIL  
                              SET RCPT_NO = '{2}'
                                WHERE ( PATIENT_ID = '{0}' ) AND
                                 ( VISIT_ID = '{1}' ) AND 
                                 RCPT_NO IS NULL AND
                                 billing_date_time <= to_date('{3}','yyyy-mm-dd hh24:mi:ss')";
                sql = string.Format(sql, patientid, visitid, rcptno, date.ToString());
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 插入结算主表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="master"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertBalanceMaster(BaseEntityer db, HisCommon.DataEntity.INP_SETTLE_MASTER master, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"INSERT INTO INP_SETTLE_MASTER
                              (RCPT_NO,
                               PATIENT_ID,
                               VISIT_ID,
                               SETTLING_DATE,
                               COSTS,
                               CHARGES,
                               PAYMENTS,
                               REDUCED_CAUSE,
                               TRANSACT_TYPE,
                               OPERATOR_NO,
                               INVOICE)
                            VALUES
                              ('{0}', '{1}', '{2}', sysdate, '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')";
                sql = string.Format(sql, master.RCPT_NO,
                                   master.PATIENT_ID,
                                   master.VISIT_ID,
                                   master.COSTS,
                                   master.CHARGES,
                                   master.PAYMENTS,
                                   master.REDUCED_CAUSE,
                                   master.TRANSACT_TYPE,
                                   master.OPERATOR_NO,
                                   master.INVOICE);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 插入结算明细表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="master"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertBalanceDetail(BaseEntityer db, HisCommon.DataEntity.INP_SETTLE_DETAIL detail, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"INSERT INTO INP_SETTLE_DETAIL
                                  (RCPT_NO, FEE_CLASS_ID, FEE_CLASS_NAME, COSTS, PAYMENTS, INVOICE)
                                VALUES
                                  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')";
                sql = string.Format(sql, detail.RCPT_NO,
                                  detail.FEE_CLASS_ID,
                                  detail.FEE_CLASS_NAME,
                                  detail.COSTS,
                                  detail.PAYMENTS,
                                  detail.INVOICE);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改病人住院记录表
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateVisitByMoney(BaseEntityer db, string patientid, string visitid, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"UPDATE PAT_VISIT  
                                  SET (TOTAL_COSTS,   
                                      TOTAL_PAYMENTS)=
                              (SELECT SUM(COSTS),SUM(CHARGES) FROM INP_BILL_DETAIL
                                WHERE ( INP_BILL_DETAIL.PATIENT_ID = '{0}' ) AND  
                                      ( INP_BILL_DETAIL.VISIT_ID = '{1}' )
                               )
                               WHERE ( PATIENT_ID = '{0}' ) AND  
                                     ( VISIT_ID = '{1}' ) ";
                sql = string.Format(sql, patientid, visitid);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 删除费用记录表
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DeleteMedicalClass(BaseEntityer db, string patientid, string visitid, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"DELETE MEDICAL_COSTS
                                 WHERE PATIENT_ID = '{0}'
                                   AND VISIT_ID = '{1}'";
                sql = string.Format(sql, patientid, visitid);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 读取费用明细
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public DataTable GetBillDetail(string patientID, string visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT INP_BILL_DETAIL.CLASS_ON_INP_RCPT as Class,   
                                 INP_BILL_DETAIL.ITEM_CODE as Code,
                                 SUM(INP_BILL_DETAIL.COSTS) as Cost  
                             FROM INP_BILL_DETAIL  
                             WHERE ( INP_BILL_DETAIL.PATIENT_ID = '{0}' ) AND  
                                   ( INP_BILL_DETAIL.VISIT_ID = '{1}' ) 
                             GROUP BY INP_BILL_DETAIL.CLASS_ON_INP_RCPT,INP_BILL_DETAIL.ITEM_CODE";
                sql = string.Format(sql, patientID, visitID);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 读取价表类别
        /// </summary>
        /// <param name="itemclass"></param>
        /// <param name="itemcode"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public string GetPriceListMaxMR(string fee_class_code, ref string errmsg)
        {
            DataTable dt = new DataTable();
            string rev = string.Empty;
            try
            {
                //                string sql = @"SELECT MAX(PRICE_LIST.CLASS_ON_MR)
                //                                  FROM PRICE_LIST
                //                                 WHERE (PRICE_LIST.ITEM_CLASS = '{0}')
                //                                   AND (PRICE_LIST.ITEM_CODE = '{1}')";
                string sql = @"select c.fee_class_name from inp_rcpt_fee_dict c where c.fee_class_code='{0}'";
                sql = string.Format(sql, fee_class_code);
                dt = BaseEntityer.Db.GetDataTable(sql);
                rev = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return rev;
        }

        /// <summary>
        /// 插入费用类别表
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertMedicalCost(BaseEntityer db, HisCommon.DataEntity.MEDICAL_COSTS medical, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"INSERT INTO MEDICAL_COSTS
                                  (PATIENT_ID, VISIT_ID, FEE_TYPE, COSTS)
                                VALUES
                                  ('{0}', '{1}', '{2}', '{3}')";
                sql = string.Format(sql, medical.PATIENT_ID,
                                            medical.VISIT_ID,
                                            medical.FEE_TYPE,
                                            medical.COSTS);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 插入结算支付类型表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="money"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertPaymentsMoney(BaseEntityer db, HisCommon.DataEntity.INP_PAYMENTS_MONEY money, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"INSERT INTO INP_PAYMENTS_MONEY
                                  (RCPT_NO, MONEY_TYPE, PAYMENT_NO, PAYMENT_AMOUNT, REFUNDED_AMOUNT)
                                VALUES
                                  ('{0}', '{1}', '{2}', '{3}', '{4}')";
                sql = string.Format(sql, money.RCPT_NO,
                                            money.MONEY_TYPE,
                                            money.PAYMENT_NO,
                                            money.PAYMENT_AMOUNT,
                                            money.REFUNDED_AMOUNT);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 插入结算支付方式表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="payway"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertPayway(BaseEntityer db, HisCommon.DataEntity.PAY_WAY payway, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"INSERT INTO PAY_WAY t
                              (t.RCPT_NO,
                               t.SEQ_NO,
                               t.PATIENT_ID,
                               t.VISIT_ID,
                               t.MONEY_TYPE_ID,
                               t.MONEY_TYPE_NAME,
                               t.PREPAYMENT_AMOUNT,
                               t.RECEIVABLE_AMOUNT,
                               t.RETURN_AMOUNT,
                               t.BANK_NO,
                               t.CHECK_NO,
                               t.isflag,
                               t.operdate,
                               t.opercode,
                               t.acct_no,
                               t.acct_flag,
                               t.INVOICE)
                            VALUES
                              ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}','{8}','{9}','{10}','{11}',to_date('{12}','yyyy-mm-dd hh24:mi:ss'),'{13}','{14}','{15}','{16}')";
                sql = string.Format(sql, payway.RCPT_NO,
                                            payway.SEQ_NO,
                                            payway.PATIENT_ID,
                                            payway.VISIT_ID,
                                            payway.MONEY_TYPE_ID,
                                            payway.MONEY_TYPE_NAME,
                                            payway.PREPAYMENT_AMOUNT,
                                            payway.RECEIVABLE_AMOUNT,
                                            payway.RETURN_AMOUNT,
                                            payway.BANK_NO,
                                            payway.CHECK_NO,
                                            payway.ISFLAG,
                                            payway.OPERDATE,
                                            payway.OPERCODE,
                                            payway.ACCT_NO,
                                            payway.ACCT_FLAG,
                                            payway.INVOICE);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改结算支付方式表为有效性
        /// </summary>
        /// <param name="db"></param>
        /// <param name="payway"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdatePaywayFlag(BaseEntityer db, string rcptNo, int typeID, string isFlag, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"update PAY_WAY set PAY_WAY.Isflag = '{2}' where PAY_WAY.RCPT_NO = '{0}' and PAY_WAY.SEQ_NO='{1}'";
                sql = string.Format(sql, rcptNo, typeID, isFlag);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 获取最近一次入出转记录
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public ADT_LOG GetRecentlyADT(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            ADT_LOG adtlog = new ADT_LOG();
            try
            {
                string sql = @"select t.ward_code, t.dept_code
                                      from ADT_LOG t
                                     where t.patient_id = '{0}'
                                       and t.visit_id = '{1}'
                                       and t.log_date_time = (select max(s.log_date_time)
                                                                from ADT_LOG s
                                                               where s.patient_id = '{0}'
                                                                 and s.visit_id = '{1}')";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
                adtlog.WARD_CODE = dt.Rows[0][0].ToString();
                adtlog.DEPT_CODE = dt.Rows[0][1].ToString();
                adtlog.PATIENT_ID = patientid;
                adtlog.VISIT_ID = int.Parse(visitid);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return adtlog;
        }

        /// <summary>
        /// 获取病情信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public string GetPatientCondition(string patientid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            string rev = string.Empty;
            try
            {
                string sql = @"SELECT patient_condition FROM pats_in_hospital WHERE patient_id = '{0}'";
                sql = string.Format(sql, patientid);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return string.Empty;
                rev = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return rev;
        }

        /// <summary>
        /// 删除预约出院记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DelPreOutHospital(BaseEntityer db, string patientid, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"DELETE pre_dischged_pats WHERE patient_id = '{0}'";
                sql = string.Format(sql, patientid);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 插入出入转记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="adt"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertAdtLog(BaseEntityer db, HisCommon.DataEntity.ADT_LOG adt, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"INSERT INTO adt_log
                              (ward_code, dept_code, log_date_time, patient_id, visit_id, action)
                            VALUES
                              ('{0}', '{1}', SYSDATE, '{2}', '{3}', '{4}')";
                sql = string.Format(sql, adt.WARD_CODE,
                                        adt.DEPT_CODE,
                                        adt.PATIENT_ID,
                                        adt.VISIT_ID,
                                        adt.ACTION);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改出院转科记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="opercode"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateOutTransfer(BaseEntityer db, string patientid, string visitid, string opercode, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"UPDATE transfer
                               SET discharge_date_time = SYSDATE,
                                   doctor_in_charge    = '{2}',
                                   dept_transfered_to  = NULL
                             where patient_id = '{0}'
                               AND visit_id = '{1}'
                               AND discharge_date_time IS NULL";
                sql = string.Format(sql, patientid, visitid, opercode);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 删除在院信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DeleteInHospital(BaseEntityer db, string patientid, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"DELETE pats_in_hospital WHERE patient_id = '{0}'";
                sql = string.Format(sql, patientid);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 删除转科记录表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DeleteIntTransferring(BaseEntityer db, string patientid, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"DELETE pats_in_transferring WHERE patient_id = '{0}'";
                sql = string.Format(sql, patientid);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改出院科室与时间
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientid"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdatePVisit(BaseEntityer db, string patientid, string visitid, string dept, string state, DateTime balanceDate, ref string err)
        {
            int IsSuccess = 0;
            string sql = string.Empty;
            try
            {
                if (balanceDate.Equals(DateTime.MinValue) == true)
                {
                    sql = @"UPDATE PAT_VISIT
                                   SET state = '{2}', BALANCE_DATE = ''
                                 WHERE PATIENT_ID = '{0}'
                                   AND VISIT_ID = '{1}'";
                    sql = string.Format(sql, patientid, visitid, state);
                }
                else
                {
                    sql = @"UPDATE PAT_VISIT
                                   SET DEPT_DISCHARGE_FROM = '{2}', state = '{3}', BALANCE_DATE = to_date('{4}','yyyy-mm-dd hh24:mi:ss')
                                 WHERE PATIENT_ID = '{0}'
                                   AND VISIT_ID = '{1}'";
                    sql = string.Format(sql, patientid, visitid, dept, state, balanceDate.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改在院患者预交金及费用
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="prepay"></param>
        /// <param name="sum"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdatePrepayVisitByMidBalance(BaseEntityer db, string patientID, string prepay, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"update pats_in_hospital f set f.prepayments = '{1}',f.total_costs = 0,f.total_charges = 0 where f.patient_id='{0}'";
                sql = string.Format(sql, patientID, prepay);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }
        #endregion

        #region 结算召回
        /// <summary>
        /// 获取结算主表信息（召回用）
        /// </summary>
        /// <param name="RCPT_NO"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public INP_SETTLE_MASTER GetReBalanceMaster(string RCPT_NO, ref string errmsg)
        {
            DataTable dt = new DataTable();
            INP_SETTLE_MASTER master = new INP_SETTLE_MASTER();
            try
            {
                string sql = @"SELECT INP_SETTLE_MASTER.RCPT_NO,
                               INP_SETTLE_MASTER.PATIENT_ID,
                               INP_SETTLE_MASTER.VISIT_ID,
                               INP_SETTLE_MASTER.SETTLING_DATE,
                               INP_SETTLE_MASTER.Costs,
                               INP_SETTLE_MASTER.Charges,
                               INP_SETTLE_MASTER.PAYMENTS,
                               INP_SETTLE_MASTER.Reduced_Cause,
                               INP_SETTLE_MASTER.TRANSACT_TYPE,
                               INP_SETTLE_MASTER.OPERATOR_NO,
                               INP_SETTLE_MASTER.ACCT_NO,
                               INP_SETTLE_MASTER.INVOICE
                          FROM INP_SETTLE_MASTER
                          where INP_SETTLE_MASTER.RCPT_NO = '{0}'
                                AND INP_SETTLE_MASTER.Refunded_Rcpt_No is null
                        ";
                sql = string.Format(sql, RCPT_NO);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    errmsg = "该患者没有结算信息";
                    return null;
                }
                master.RCPT_NO = dt.Rows[0][0].ToString();
                master.PATIENT_ID = dt.Rows[0][1].ToString();
                master.VISIT_ID = int.Parse(dt.Rows[0][2].ToString());
                master.SETTLING_DATE = DateTime.Parse(dt.Rows[0][3].ToString()).ToLocalTime();
                master.COSTS = decimal.Parse(dt.Rows[0][4].ToString());
                master.CHARGES = decimal.Parse(dt.Rows[0][5].ToString());
                master.PAYMENTS = decimal.Parse(dt.Rows[0][6].ToString());
                master.REDUCED_CAUSE = dt.Rows[0][7].ToString();
                master.TRANSACT_TYPE = dt.Rows[0][8].ToString();
                master.OPERATOR_NO = dt.Rows[0][9].ToString();
                master.ACCT_NO = dt.Rows[0][10].ToString();
                master.INVOICE = dt.Rows[0][11].ToString();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return null;
            }
            return master;
        }

        /// <summary>
        /// 获取结算明细信息（召回用）
        /// </summary>
        /// <param name="RCPT_NO"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<INP_SETTLE_DETAIL> GetReBalanceDetail(string RCPT_NO, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<INP_SETTLE_DETAIL> details = new List<INP_SETTLE_DETAIL>();
            INP_SETTLE_DETAIL detail = null;
            try
            {
                string sql = @"SELECT   INP_SETTLE_DETAIL.FEE_CLASS_ID,
                                         INP_SETTLE_DETAIL.FEE_CLASS_NAME,   
                                         nvl(INP_SETTLE_DETAIL.PAYMENTS,0),   
                                         nvl(INP_SETTLE_DETAIL.COSTS,0)  
                                    FROM INP_SETTLE_DETAIL  
                                   WHERE INP_SETTLE_DETAIL.RCPT_NO = '{0}'";
                sql = string.Format(sql, RCPT_NO);
                dt = BaseEntityer.Db.GetDataTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    detail = new INP_SETTLE_DETAIL();
                    detail.FEE_CLASS_ID = dt.Rows[i][0].ToString();
                    detail.FEE_CLASS_NAME = dt.Rows[i][1].ToString();
                    detail.PAYMENTS = decimal.Parse(dt.Rows[i][2].ToString());
                    detail.COSTS = decimal.Parse(dt.Rows[i][3].ToString());
                    details.Add(detail);
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return null;
            }
            return details;
        }

        /// <summary>
        /// 获取结算明细信息DataTable（召回用）
        /// </summary>
        /// <param name="RCPT_NO"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public DataTable GetReBalanceDetailByDataTable(string RCPT_NO, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT   INP_SETTLE_DETAIL.FEE_CLASS_ID as 类别编码,
                                         INP_SETTLE_DETAIL.FEE_CLASS_NAME as 类别名称,   
                                         INP_SETTLE_DETAIL.PAYMENTS as 应交金额,   
                                         INP_SETTLE_DETAIL.COSTS as 总金额 
                                    FROM INP_SETTLE_DETAIL  
                                   WHERE INP_SETTLE_DETAIL.RCPT_NO = '{0}'";
                sql = string.Format(sql, RCPT_NO);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 获取结算支付信息（召回用）
        /// </summary>
        /// <param name="RCPT_NO"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<INP_PAYMENTS_MONEY> GetReBalancePay(string RCPT_NO, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<INP_PAYMENTS_MONEY> money = new List<INP_PAYMENTS_MONEY>();
            try
            {
                string sql = @"SELECT 
                                   INP_PAYMENTS_MONEY.PAYMENT_NO,
                                   INP_PAYMENTS_MONEY.MONEY_TYPE,
                                   INP_PAYMENTS_MONEY.PAYMENT_AMOUNT,
                                   INP_PAYMENTS_MONEY.REFUNDED_AMOUNT
                              FROM INP_PAYMENTS_MONEY
                             WHERE INP_PAYMENTS_MONEY.RCPT_NO = '{0}'
                             ORDER BY INP_PAYMENTS_MONEY.PAYMENT_NO ASC ";
                sql = string.Format(sql, RCPT_NO);
                dt = BaseEntityer.Db.GetDataTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    INP_PAYMENTS_MONEY detail = new INP_PAYMENTS_MONEY();
                    detail.PAYMENT_NO = int.Parse(dt.Rows[i][0].ToString());
                    detail.MONEY_TYPE = dt.Rows[i][1].ToString();
                    detail.PAYMENT_AMOUNT = decimal.Parse(dt.Rows[i][2].ToString());
                    detail.REFUNDED_AMOUNT = decimal.Parse(dt.Rows[i][3].ToString());
                    money.Add(detail);
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return null;
            }
            return money;
        }

        /// <summary>
        /// 获取结算预交金明细（召回用）
        /// </summary>
        /// <param name="balance_invoice"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public DataTable GetReBalancePrepay(string balance_invoice, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @" select e.patient_id as 病人ID,
                                       e.rcpt_no as 发票号,
                                       e.amount as 预交金额,
                                       t.name as 支付方式,
                                       decode(e.transact_type, '1', '交费', '2', '退费', '其他') as 操作类型,
                                       e.refunded_rcpt_no as 退费发票号,
                                       e.transact_date as 操作日期,
                                       e.operator_no as 操作员,
                                       e.bank as 开户行,
                                       e.check_no as 支票号
                                  from prepayment_rcpt e, paytype t
                                 where e.balance_invoice = '{0}'
                                   and e.transact_type in ('1', '2','9') --‘9’为贵阳需求加入门诊扣预交金功能
                                   and e.is_flag = 1
                                   and e.pay_way = t.id
                                 order by e.transact_date ";
                sql = string.Format(sql, balance_invoice);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取结算支付方式信息（召回用）
        /// </summary>
        /// <param name="RCPT_NO"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<PAY_WAY> GetReBalancePayWay(string RCPT_NO, string flag, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<PAY_WAY> payways = new List<PAY_WAY>();
            PAY_WAY payway = new PAY_WAY();
            try
            {
                string sql = @"select PAY_WAY.PATIENT_ID,
                                       PAY_WAY.VISIT_ID,
                                       PAY_WAY.MONEY_TYPE_ID,
                                       PAY_WAY.MONEY_TYPE_NAME,
                                       PAY_WAY.PREPAYMENT_AMOUNT,
                                       PAY_WAY.RECEIVABLE_AMOUNT,
                                       PAY_WAY.RETURN_AMOUNT,
                                       PAY_WAY.BANK_NO,
                                       PAY_WAY.CHECK_NO,
                                       PAY_WAY.SEQ_NO
                                  from PAY_WAY
                                 where PAY_WAY.RCPT_NO = '{0}'
                                 and PAY_WAY.ISFLAG = '{1}'";
                sql = string.Format(sql, RCPT_NO, flag);
                dt = BaseEntityer.Db.GetDataTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    payway = new PAY_WAY();
                    payway.PATIENT_ID = dt.Rows[i][0].ToString();
                    payway.VISIT_ID = int.Parse(dt.Rows[i][1].ToString());
                    payway.MONEY_TYPE_ID = int.Parse(dt.Rows[i][2].ToString());
                    payway.MONEY_TYPE_NAME = dt.Rows[i][3].ToString();
                    payway.PREPAYMENT_AMOUNT = decimal.Parse(dt.Rows[i][4].ToString());
                    payway.RECEIVABLE_AMOUNT = decimal.Parse(dt.Rows[i][5].ToString());
                    payway.RETURN_AMOUNT = decimal.Parse(dt.Rows[i][6].ToString());
                    payway.BANK_NO = int.Parse(string.IsNullOrEmpty(dt.Rows[i][7].ToString()) == true ? "0" : dt.Rows[i][7].ToString());
                    payway.CHECK_NO = int.Parse(string.IsNullOrEmpty(dt.Rows[i][8].ToString()) == true ? "0" : dt.Rows[i][8].ToString());
                    payway.SEQ_NO = int.Parse(string.IsNullOrEmpty(dt.Rows[i][9].ToString()) == true ? "0" : dt.Rows[i][9].ToString());
                    payways.Add(payway);
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return null;
            }
            return payways;
        }

        /// <summary>
        /// 获取结算患者信息（召回用）
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public DataTable GetReBalancePatientInfo(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT PAT_MASTER_INDEX.INP_NO,
                                       PAT_MASTER_INDEX.NAME,
                                       charge_type_dict.charge_type_code,
                                       charge_type_dict.charge_type_name,
                                       PAT_VISIT.ADMISSION_DATE_TIME,
                                       PAT_VISIT.DEPT_DISCHARGE_FROM
                                  FROM PAT_MASTER_INDEX, PAT_VISIT,charge_type_dict
                                 WHERE PAT_MASTER_INDEX.PATIENT_ID = PAT_VISIT.PATIENT_ID
                                   AND PAT_VISIT.State = 'O'
                                   AND charge_type_dict.charge_type_name = PAT_VISIT.CHARGE_TYPE
                                   AND PAT_VISIT.PATIENT_ID = '{0}'
                                   AND PAT_VISIT.VISIT_ID = '{1}'";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取结算患者信息（召回用）
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="visitid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public int GetReBalancePrintInfo(string patientid, string visitid, string rcpt, ref List<PAT_MASTER_INDEX> index, ref List<PAT_VISIT> visit, ref  List<PAY_WAY> way, ref  List<INP_BILL_DETAIL> details, ref List<SIInfo> info, ref string errmsg)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select * from pat_visit t where t.patient_id='{0}' and t.visit_id='{1}'";
                sql = string.Format(sql, patientid, visitid);
                ds = BaseEntityer.Db.GetDataSet(sql);
                visit = DataSetToEntity.DataSetToT<PAT_VISIT>(ds).ToList();

                sql = @"select * from pat_master_index s where s.patient_id='{0}'";
                sql = string.Format(sql, patientid);
                ds = new DataSet();
                ds = BaseEntityer.Db.GetDataSet(sql);
                index = DataSetToEntity.DataSetToT<PAT_MASTER_INDEX>(ds).ToList();

                sql = @"select * from pay_way c where c.rcpt_no='{0}'";
                sql = string.Format(sql, rcpt);
                ds = new DataSet();
                ds = BaseEntityer.Db.GetDataSet(sql);
                way = DataSetToEntity.DataSetToT<PAY_WAY>(ds).ToList();

                sql = @"select * from inp_bill_detail a where a.patient_id='{0}' and a.visit_id='{1}'";
                sql = string.Format(sql, patientid, visitid);
                ds = new DataSet();
                ds = BaseEntityer.Db.GetDataSet(sql);
                List<INP_BILL_DETAIL> bill = DataSetToEntity.DataSetToT<INP_BILL_DETAIL>(ds).ToList();
                details = new List<INP_BILL_DETAIL>();
                foreach (INP_BILL_DETAIL det in bill)
                {
                    det.AMOUNT = -det.AMOUNT;
                    det.COSTS = -det.COSTS;
                    det.CHARGES = -det.CHARGES;
                    details.Add(det);
                }

                sql = @"select * from siinfo f where f.inpatient_id='{0}' and f.visit_id='{1}' and f.isvalid = 1 and f.balance_state = 1 and f.type_code = 2 and f.trans_type = 2 order by f.balance_date desc";
                sql = string.Format(sql, patientid, visitid);
                ds = new DataSet();
                ds = BaseEntityer.Db.GetDataSet(sql);
                info = DataSetToEntity.DataSetToT<SIInfo>(ds).ToList();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 修改结算主表退费发票号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="oldRcptNo"></param>
        /// <param name="newRcptNo"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateReMasterRefundedRcpt(BaseEntityer db, string oldRcptNo, string newRcptNo, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"UPDATE INP_SETTLE_MASTER
                                   SET INP_SETTLE_MASTER.REFUNDED_RCPT_NO = '{1}'
                                 WHERE INP_SETTLE_MASTER.RCPT_NO = '{0}'";
                sql = string.Format(sql, oldRcptNo, newRcptNo);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改费用明细发票号（置为空）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="RCPT_NO"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateReBillDetailRcpt(BaseEntityer db, string RCPT_NO, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"UPDATE INP_BILL_DETAIL  
                                 SET INP_BILL_DETAIL.RCPT_NO = ''  
                               WHERE INP_BILL_DETAIL.RCPT_NO = '{0}'";
                sql = string.Format(sql, RCPT_NO);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 修改费用明细发票号（置为空）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="RCPT_NO"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertReMaster(BaseEntityer db, string oldRcptNo, string newRcptNo, string opercode, ref string err)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"INSERT INTO INP_SETTLE_MASTER
                                      (RCPT_NO,
                                       PATIENT_ID,
                                       VISIT_ID,
                                       SETTLING_DATE,
                                       COSTS,
                                       CHARGES,
                                       PAYMENTS,
                                       REDUCED_CAUSE,
                                       TRANSACT_TYPE,
                                       OPERATOR_NO,
                                       ACCT_NO)
                                      SELECT '{1}',
                                             INP_SETTLE_MASTER.PATIENT_ID,
                                             INP_SETTLE_MASTER.VISIT_ID,
                                             sysdate,
                                             0 - INP_SETTLE_MASTER.COSTS,
                                             0 - INP_SETTLE_MASTER.CHARGES,
                                             0 - INP_SETTLE_MASTER.PAYMENTS,
                                             '',
                                             '4',
                                             '{2}',
                                             ''
                                        FROM INP_SETTLE_MASTER
                                       WHERE INP_SETTLE_MASTER.RCPT_NO = '{0}'
                                    ";
                sql = string.Format(sql, oldRcptNo, newRcptNo, opercode);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return IsSuccess;
        }
        #endregion

        #region 住院发票重打
        /// <summary>
        /// 获取住院主表信息（按发票号）
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public string GetInvoiceByRcpt(string invoice, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select c.rcpt_no
                          from inp_settle_master c
                         where c.invoice = '{0}' 
                         and c.transact_type = 3";
                sql = string.Format(sql, invoice);
                dt = BaseEntityer.Db.GetDataTable(sql);
                return dt.Rows[0][0] == null ? "" : dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return "";
            }
        }

        /// <summary>
        /// 获取发票记录表的发票重打的信息（按发票号）
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public string GetInvoiceByReInvoice(string invoice, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"SELECT c.rcpt_no
  FROM fin_invoiceinfo_record c WHERE c.invoice_no = '{0}' and  c.invoice_state in ('0','2')
   and c.invoice_kind = '03' and rownum=1";
                sql = string.Format(sql, invoice);
               return  BaseEntityer.Db.ExecuteScalar<string>(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return "";
            }
        }

        /// <summary>
        /// 获取住院主表信息（按发票号）
        /// </summary>
        /// <param name="rcpt"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public INP_SETTLE_MASTER GetInpatientMasterByRcpt(string rcpt, ref string errmsg)
        {
            DataTable dt = new DataTable();
            INP_SETTLE_MASTER master = new INP_SETTLE_MASTER();
            try
            {
                string sql = @"select c.patient_id,
                               c.visit_id,
                               c.settling_date,
                               c.costs,
                               c.charges,
                               c.payments,
                               c.transact_type
                          from inp_settle_master c
                         where c.rcpt_no = '{0}'";
                sql = string.Format(sql, rcpt);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    errmsg = "按照发票号没找到住院结算信息。";
                    return null;
                }
                else if (dt.Rows.Count > 1)
                {
                    errmsg = "该发票号存在结算召回，不能重打。";
                    return null;
                }
                else
                {
                    master.PATIENT_ID = dt.Rows[0][0].ToString();
                    master.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                    master.SETTLING_DATE = DateTime.Parse(string.IsNullOrEmpty(dt.Rows[0][2].ToString()) == true ? DateTime.MinValue.ToString() : dt.Rows[0][2].ToString());
                    master.COSTS = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][3].ToString()) == true ? "0" : dt.Rows[0][3].ToString());
                    master.CHARGES = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][4].ToString()) == true ? "0" : dt.Rows[0][4].ToString());
                    master.PAYMENTS = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][5].ToString()) == true ? "0" : dt.Rows[0][5].ToString());
                    master.TRANSACT_TYPE = dt.Rows[0][6].ToString();
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return master;
        }

        /// <summary>
        /// 获取住院费用明细表信息（按发票号）
        /// </summary>
        /// <param name="rcpt"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<INP_BILL_DETAIL> GetInpatientBillByRcpt(string patientID, string rcpt, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<INP_BILL_DETAIL> bill = new List<INP_BILL_DETAIL>();
            try
            {
                string sql = string.Empty;
                if (string.IsNullOrEmpty(rcpt) == true)
                {
                    sql = @"select PATIENT_ID,
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
                                     RETURN_FLAG
                                      from inp_bill_detail
                                     where RCPT_NO is null and PATIENT_ID = '{0}'";
                    sql = string.Format(sql, patientID);
                }
                else
                {
                    sql = @"select PATIENT_ID,
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
                                     RETURN_FLAG
                                      from inp_bill_detail
                                     where RCPT_NO ='{1}' and PATIENT_ID = '{0}'";
                    sql = string.Format(sql, patientID, rcpt);
                }

                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errmsg = "按照发票号没找到住院费用明细信息。";
                    return null;
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        INP_BILL_DETAIL detail = new INP_BILL_DETAIL();
                        detail.PATIENT_ID = dt.Rows[i][0].ToString();
                        detail.VISIT_ID = int.Parse(dt.Rows[i][1].ToString());
                        detail.ITEM_NO = int.Parse(dt.Rows[i][2].ToString());
                        detail.ITEM_CLASS = dt.Rows[i][3].ToString();
                        detail.ITEM_NAME = dt.Rows[i][4].ToString();
                        detail.ITEM_CODE = dt.Rows[i][5].ToString();
                        detail.ITEM_SPEC = dt.Rows[i][6].ToString();
                        detail.AMOUNT = decimal.Parse(dt.Rows[i][7].ToString());
                        detail.UNITS = dt.Rows[i][8].ToString();
                        detail.ORDERED_BY = dt.Rows[i][9].ToString();
                        detail.PERFORMED_BY = dt.Rows[i][10].ToString();
                        detail.COSTS = decimal.Parse(dt.Rows[i][11].ToString());
                        detail.CHARGES = decimal.Parse(dt.Rows[i][12].ToString());
                        detail.BILLING_DATE_TIME = DateTime.Parse(dt.Rows[i][13].ToString());
                        detail.OPERATOR_NO = dt.Rows[i][14].ToString();
                        detail.RCPT_NO = dt.Rows[i][15].ToString();
                        detail.UP_FLAG = dt.Rows[i][16].ToString();
                        detail.UP_TIME_DATE = string.IsNullOrEmpty(dt.Rows[i][17].ToString()) == true ? DateTime.MinValue : DateTime.Parse(dt.Rows[i][17].ToString());
                        detail.UP_OPERATOR_NO = dt.Rows[i][18].ToString();
                        detail.FORMULARYNO = dt.Rows[i][19].ToString();
                        detail.DOCTOR = dt.Rows[i][20].ToString();
                        detail.CHECKFLAG = dt.Rows[i][21].ToString();
                        detail.SUBJ_CODE = dt.Rows[i][22].ToString();
                        detail.CLASS_ON_INP_RCPT = dt.Rows[i][23].ToString();
                        bill.Add(detail);
                    }
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return bill;
        }

        /// <summary>
        /// 获取住院费用明细表信息（按发票号）
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public List<INP_BILL_DETAIL> GetInpatientBillByRcptVisit(string patientID, int visitID, ref string errmsg)
        {
            DataTable dt = new DataTable();
            List<INP_BILL_DETAIL> bill = new List<INP_BILL_DETAIL>();
            try
            {
                string sql = string.Empty;

                sql = @"select PATIENT_ID,
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
                                     RETURN_FLAG
                                      from inp_bill_detail
                                     where VISIT_ID ='{1}' and PATIENT_ID = '{0}'";
                sql = string.Format(sql, patientID, visitID.ToString());


                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errmsg = "按照发票号没找到住院费用明细信息。";
                    return null;
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        INP_BILL_DETAIL detail = new INP_BILL_DETAIL();
                        detail.PATIENT_ID = dt.Rows[i][0].ToString();
                        detail.VISIT_ID = int.Parse(dt.Rows[i][1].ToString());
                        detail.ITEM_NO = int.Parse(dt.Rows[i][2].ToString());
                        detail.ITEM_CLASS = dt.Rows[i][3].ToString();
                        detail.ITEM_NAME = dt.Rows[i][4].ToString();
                        detail.ITEM_CODE = dt.Rows[i][5].ToString();
                        detail.ITEM_SPEC = dt.Rows[i][6].ToString();
                        detail.AMOUNT = decimal.Parse(dt.Rows[i][7].ToString());
                        detail.UNITS = dt.Rows[i][8].ToString();
                        detail.ORDERED_BY = dt.Rows[i][9].ToString();
                        detail.PERFORMED_BY = dt.Rows[i][10].ToString();
                        detail.COSTS = decimal.Parse(dt.Rows[i][11].ToString());
                        detail.CHARGES = decimal.Parse(dt.Rows[i][12].ToString());
                        detail.BILLING_DATE_TIME = DateTime.Parse(dt.Rows[i][13].ToString());
                        detail.OPERATOR_NO = dt.Rows[i][14].ToString();
                        detail.RCPT_NO = dt.Rows[i][15].ToString();
                        detail.UP_FLAG = dt.Rows[i][16].ToString();
                        detail.UP_TIME_DATE = string.IsNullOrEmpty(dt.Rows[i][17].ToString()) == true ? DateTime.MinValue : DateTime.Parse(dt.Rows[i][17].ToString());
                        detail.UP_OPERATOR_NO = dt.Rows[i][18].ToString();
                        detail.FORMULARYNO = dt.Rows[i][19].ToString();
                        detail.DOCTOR = dt.Rows[i][20].ToString();
                        detail.CHECKFLAG = dt.Rows[i][21].ToString();
                        detail.SUBJ_CODE = dt.Rows[i][22].ToString();
                        detail.CLASS_ON_INP_RCPT = dt.Rows[i][23].ToString();
                        bill.Add(detail);
                    }
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return bill;
        }

        /// <summary>
        /// 获取住院主表信息（按发票号）
        /// </summary>
        /// <param name="rcpt"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public INP_SETTLE_MASTER GetInpatientPayWayByRcpt(string rcpt, ref string errmsg)
        {
            DataTable dt = new DataTable();
            INP_SETTLE_MASTER master = new INP_SETTLE_MASTER();
            try
            {
                string sql = @"select c.patient_id,
                               c.visit_id,
                               c.settling_date,
                               c.costs,
                               c.charges,
                               c.payments
                          from inp_settle_master c
                         where c.rcpt_no = '{0}'";
                sql = string.Format(sql, rcpt);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    errmsg = "按照发票号没找到住院结算信息。";
                    return null;
                }
                else if (dt.Rows.Count > 1)
                {
                    errmsg = "该发票号存在结算召回，不能重打。";
                    return null;
                }
                else
                {
                    master.PATIENT_ID = dt.Rows[0][0].ToString();
                    master.VISIT_ID = int.Parse(dt.Rows[0][1].ToString());
                    master.SETTLING_DATE = DateTime.Parse(string.IsNullOrEmpty(dt.Rows[0][2].ToString()) == true ? DateTime.MinValue.ToString() : dt.Rows[0][2].ToString());
                    master.COSTS = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][3].ToString()) == true ? "0" : dt.Rows[0][3].ToString());
                    master.CHARGES = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][4].ToString()) == true ? "0" : dt.Rows[0][4].ToString());
                    master.PAYMENTS = decimal.Parse(string.IsNullOrEmpty(dt.Rows[0][5].ToString()) == true ? "0" : dt.Rows[0][5].ToString());
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return master;
        }
        #endregion

        #region 住院日结
        /// <summary>
        /// 查询日结
        /// </summary>
        /// <param name="db"></param>
        /// <param name="opercode"></param>
        /// <param name="operDate"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="balanceID"></param>
        /// <param name="dpc"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int QueryDayReport(BaseEntityer db, string opercode, string operDate, string beginDate, string endDate, ref string balanceID, ref string errCode, ref string errMsg)
        {
            string sql = @"PKG_INPATIENT_DAYBANLANCE.PRC_BALANCE_BY_SINGLE_OPER";
            DbParameterCollection dpc = null;
            try
            {
                #region 参数赋值
                DbParameter dp1 = db.Cmd.CreateParameter();
                dp1.DbType = DbType.String;
                dp1.Direction = ParameterDirection.Input;
                dp1.ParameterName = "BALANCEOPERCODE";
                dp1.Value = opercode;
                dp1.Size = 1024;

                DbParameter dp2 = db.Cmd.CreateParameter();
                dp2.DbType = DbType.String;
                dp2.Direction = ParameterDirection.Input;
                dp2.ParameterName = "BEGINTIME";
                dp2.Value = beginDate;
                dp2.Size = 1024;

                DbParameter dp3 = db.Cmd.CreateParameter();
                dp3.DbType = DbType.String;
                dp3.Direction = ParameterDirection.Input;
                dp3.ParameterName = "ENDTIME";
                dp3.Value = endDate;
                dp3.Size = 1024;

                DbParameter dp4 = db.Cmd.CreateParameter();
                dp4.DbType = DbType.String;
                dp4.Direction = ParameterDirection.Input;
                dp4.ParameterName = "OPERCODE";
                dp4.Value = opercode;
                dp4.Size = 1024;

                DbParameter dp5 = db.Cmd.CreateParameter();
                dp5.DbType = DbType.String;
                dp5.Direction = ParameterDirection.Input;
                dp5.ParameterName = "OPERTIME";
                dp5.Value = operDate;
                dp5.Size = 1024;

                DbParameter dp6 = db.Cmd.CreateParameter();
                dp6.DbType = DbType.String;
                dp6.Direction = ParameterDirection.Output;
                dp6.ParameterName = "BALANCEID";
                dp6.Value = balanceID;
                dp6.Size = 1024;

                DbParameter dp7 = db.Cmd.CreateParameter();
                dp7.DbType = DbType.String;
                dp7.Direction = ParameterDirection.Output;
                dp7.ParameterName = "ERRCODE";
                dp7.Value = errCode;
                dp7.Size = 1024;

                DbParameter dp8 = db.Cmd.CreateParameter();
                dp8.DbType = DbType.String;
                dp8.Direction = ParameterDirection.Output;
                dp8.ParameterName = "ERRMSG";
                dp8.Value = errMsg;
                dp8.Size = 1024;
                #endregion

                dpc = db.ExecuteProc(sql, new DbParameter[] { dp1, dp2, dp3, dp4, dp5, dp6, dp7, dp8 });

                balanceID = db.Cmd.Parameters[5].Value.ToString();
                errCode = db.Cmd.Parameters[6].Value.ToString();
                errMsg = db.Cmd.Parameters[7].Value.ToString();
            }
            catch (Exception e)
            {
                errCode = "-1";
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 查询日结
        /// </summary>
        /// <param name="db"></param>
        /// <param name="opercode"></param>
        /// <param name="operDate"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="balanceID"></param>
        /// <param name="dpc"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int QueryWKDayReport(BaseEntityer db, string opercode, string operDate, string beginDate, string endDate, ref string balanceID, ref string errCode, ref string errMsg)
        {
            string sql = @"PKG_DAYBANLANCE.PRC_BALANCE_BY_SINGLE_OPER";
            DbParameterCollection dpc = null;
            try
            {
                #region 参数赋值
                DbParameter dp1 = db.Cmd.CreateParameter();
                dp1.DbType = DbType.String;
                dp1.Direction = ParameterDirection.Input;
                dp1.ParameterName = "BALANCEOPERCODE";
                dp1.Value = opercode;
                dp1.Size = 1024;

                DbParameter dp2 = db.Cmd.CreateParameter();
                dp2.DbType = DbType.String;
                dp2.Direction = ParameterDirection.Input;
                dp2.ParameterName = "BEGINTIME";
                dp2.Value = beginDate;
                dp2.Size = 1024;

                DbParameter dp3 = db.Cmd.CreateParameter();
                dp3.DbType = DbType.String;
                dp3.Direction = ParameterDirection.Input;
                dp3.ParameterName = "ENDTIME";
                dp3.Value = endDate;
                dp3.Size = 1024;

                DbParameter dp4 = db.Cmd.CreateParameter();
                dp4.DbType = DbType.String;
                dp4.Direction = ParameterDirection.Input;
                dp4.ParameterName = "OPERCODE";
                dp4.Value = opercode;
                dp4.Size = 1024;

                DbParameter dp5 = db.Cmd.CreateParameter();
                dp5.DbType = DbType.String;
                dp5.Direction = ParameterDirection.Input;
                dp5.ParameterName = "OPERTIME";
                dp5.Value = operDate;
                dp5.Size = 1024;

                DbParameter dp6 = db.Cmd.CreateParameter();
                dp6.DbType = DbType.String;
                dp6.Direction = ParameterDirection.Output;
                dp6.ParameterName = "BALANCEID";
                dp6.Value = balanceID;
                dp6.Size = 1024;

                DbParameter dp7 = db.Cmd.CreateParameter();
                dp7.DbType = DbType.String;
                dp7.Direction = ParameterDirection.Output;
                dp7.ParameterName = "ERRCODE";
                dp7.Value = errCode;
                dp7.Size = 1024;

                DbParameter dp8 = db.Cmd.CreateParameter();
                dp8.DbType = DbType.String;
                dp8.Direction = ParameterDirection.Output;
                dp8.ParameterName = "ERRMSG";
                dp8.Value = errMsg;
                dp8.Size = 1024;
                #endregion

                dpc = db.ExecuteProc(sql, new DbParameter[] { dp1, dp2, dp3, dp4, dp5, dp6, dp7, dp8 });

                balanceID = db.Cmd.Parameters[5].Value.ToString();
                errCode = db.Cmd.Parameters[6].Value.ToString();
                errMsg = db.Cmd.Parameters[7].Value.ToString();
            }
            catch (Exception e)
            {
                errCode = "-1";
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 插入日结
        /// </summary>
        /// <param name="db"></param>
        /// <param name="opercode"></param>
        /// <param name="operDate"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="balanceID"></param>
        /// <param name="dpc"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertDayReport(BaseEntityer db, string opercode, string operDate, string beginDate, string endDate, ref string balanceID, ref string errCode, ref string errMsg)
        {
            string sql = @"PKG_INPATIENT_DAYBANLANCE.PRC_SAVE_SINGLE";
            DbParameterCollection dpc = null;
            try
            {
                #region 参数赋值
                DbParameter dp1 = db.Cmd.CreateParameter();
                dp1.DbType = DbType.String;
                dp1.Direction = ParameterDirection.Input;
                dp1.ParameterName = "BALANCEOPERCODE";
                dp1.Value = opercode;
                dp1.Size = 1024;

                DbParameter dp2 = db.Cmd.CreateParameter();
                dp2.DbType = DbType.String;
                dp2.Direction = ParameterDirection.Input;
                dp2.ParameterName = "BEGINTIME";
                dp2.Value = beginDate;
                dp2.Size = 1024;

                DbParameter dp3 = db.Cmd.CreateParameter();
                dp3.DbType = DbType.String;
                dp3.Direction = ParameterDirection.Input;
                dp3.ParameterName = "ENDTIME";
                dp3.Value = endDate;
                dp3.Size = 1024;

                DbParameter dp4 = db.Cmd.CreateParameter();
                dp4.DbType = DbType.String;
                dp4.Direction = ParameterDirection.Input;
                dp4.ParameterName = "OPERCODE";
                dp4.Value = opercode;
                dp4.Size = 1024;

                DbParameter dp5 = db.Cmd.CreateParameter();
                dp5.DbType = DbType.String;
                dp5.Direction = ParameterDirection.Input;
                dp5.ParameterName = "OPERTIME";
                dp5.Value = operDate;
                dp5.Size = 1024;

                DbParameter dp6 = db.Cmd.CreateParameter();
                dp6.DbType = DbType.String;
                dp6.Direction = ParameterDirection.Output;
                dp6.ParameterName = "BALANCEID";
                dp6.Value = balanceID;
                dp6.Size = 1024;

                DbParameter dp7 = db.Cmd.CreateParameter();
                dp7.DbType = DbType.String;
                dp7.Direction = ParameterDirection.Output;
                dp7.ParameterName = "ERRCODE";
                dp7.Value = errCode;
                dp7.Size = 1024;

                DbParameter dp8 = db.Cmd.CreateParameter();
                dp8.DbType = DbType.String;
                dp8.Direction = ParameterDirection.Output;
                dp8.ParameterName = "ERRMSG";
                dp8.Value = errMsg;
                dp8.Size = 1024;
                #endregion

                dpc = db.ExecuteProc(sql, new DbParameter[] { dp1, dp2, dp3, dp4, dp5, dp6, dp7, dp8 });

                balanceID = db.Cmd.Parameters[5].Value.ToString();
                errCode = db.Cmd.Parameters[6].Value.ToString();
                errMsg = db.Cmd.Parameters[7].Value.ToString();
            }
            catch (Exception e)
            {
                errCode = "-1";
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 插入日结
        /// </summary>
        /// <param name="db"></param>
        /// <param name="opercode"></param>
        /// <param name="operDate"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="balanceID"></param>
        /// <param name="dpc"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertWKDayReport(BaseEntityer db, string opercode, string operDate, string beginDate, string endDate, ref string balanceID, ref string errCode, ref string errMsg)
        {
            string sql = @"PKG_DAYBANLANCE.PRC_SAVE_SINGLE";
            DbParameterCollection dpc = null;
            try
            {
                #region 参数赋值
                DbParameter dp1 = db.Cmd.CreateParameter();
                dp1.DbType = DbType.String;
                dp1.Direction = ParameterDirection.Input;
                dp1.ParameterName = "BALANCEOPERCODE";
                dp1.Value = opercode;
                dp1.Size = 1024;

                DbParameter dp2 = db.Cmd.CreateParameter();
                dp2.DbType = DbType.String;
                dp2.Direction = ParameterDirection.Input;
                dp2.ParameterName = "BEGINTIME";
                dp2.Value = beginDate;
                dp2.Size = 1024;

                DbParameter dp3 = db.Cmd.CreateParameter();
                dp3.DbType = DbType.String;
                dp3.Direction = ParameterDirection.Input;
                dp3.ParameterName = "ENDTIME";
                dp3.Value = endDate;
                dp3.Size = 1024;

                DbParameter dp4 = db.Cmd.CreateParameter();
                dp4.DbType = DbType.String;
                dp4.Direction = ParameterDirection.Input;
                dp4.ParameterName = "OPERCODE";
                dp4.Value = opercode;
                dp4.Size = 1024;

                DbParameter dp5 = db.Cmd.CreateParameter();
                dp5.DbType = DbType.String;
                dp5.Direction = ParameterDirection.Input;
                dp5.ParameterName = "OPERTIME";
                dp5.Value = operDate;
                dp5.Size = 1024;

                DbParameter dp6 = db.Cmd.CreateParameter();
                dp6.DbType = DbType.String;
                dp6.Direction = ParameterDirection.Output;
                dp6.ParameterName = "BALANCEID";
                dp6.Value = balanceID;
                dp6.Size = 1024;

                DbParameter dp7 = db.Cmd.CreateParameter();
                dp7.DbType = DbType.String;
                dp7.Direction = ParameterDirection.Output;
                dp7.ParameterName = "ERRCODE";
                dp7.Value = errCode;
                dp7.Size = 1024;

                DbParameter dp8 = db.Cmd.CreateParameter();
                dp8.DbType = DbType.String;
                dp8.Direction = ParameterDirection.Output;
                dp8.ParameterName = "ERRMSG";
                dp8.Value = errMsg;
                dp8.Size = 1024;
                #endregion

                dpc = db.ExecuteProc(sql, new DbParameter[] { dp1, dp2, dp3, dp4, dp5, dp6, dp7, dp8 });

                balanceID = db.Cmd.Parameters[5].Value.ToString();
                errCode = db.Cmd.Parameters[6].Value.ToString();
                errMsg = db.Cmd.Parameters[7].Value.ToString();
            }
            catch (Exception e)
            {
                errCode = "-1";
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 取消日结
        /// </summary>
        /// <param name="db"></param>
        /// <param name="balanceID"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int CancelDayReport(BaseEntityer db, string balanceID, ref string errCode, ref string errMsg)
        {
            string sql = @"PKG_INPATIENT_DAYBANLANCE.PRC_CANCEL_SINGLE";
            DbParameterCollection dpc = null;
            try
            {
                DbParameter dp1 = db.Cmd.CreateParameter();
                dp1.DbType = DbType.String;
                dp1.Direction = ParameterDirection.Input;
                dp1.ParameterName = "BALANCEID";
                dp1.Value = balanceID;
                dp1.Size = 1024;

                DbParameter dp2 = db.Cmd.CreateParameter();
                dp2.DbType = DbType.String;
                dp2.Direction = ParameterDirection.Output;
                dp2.ParameterName = "ERRCODE";
                dp2.Value = errCode;
                dp2.Size = 1024;

                DbParameter dp3 = db.Cmd.CreateParameter();
                dp3.DbType = DbType.String;
                dp3.Direction = ParameterDirection.Output;
                dp3.ParameterName = "ERRMSG";
                dp3.Value = errMsg;
                dp3.Size = 1024;

                dpc = db.ExecuteProc(sql, new DbParameter[] { dp1, dp2, dp3 });

                errCode = db.Cmd.Parameters[1].Value.ToString();
                errMsg = db.Cmd.Parameters[2].Value.ToString();
            }
            catch (Exception e)
            {
                errCode = "-1";
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 取消日结
        /// </summary>
        /// <param name="db"></param>
        /// <param name="balanceID"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int CancelWKDayReport(BaseEntityer db, string balanceID, ref string errCode, ref string errMsg)
        {
            string sql = @"PKG_DAYBANLANCE.PRC_CANCEL_SINGLE";
            DbParameterCollection dpc = null;
            try
            {
                DbParameter dp1 = db.Cmd.CreateParameter();
                dp1.DbType = DbType.String;
                dp1.Direction = ParameterDirection.Input;
                dp1.ParameterName = "BALANCEID";
                dp1.Value = balanceID;
                dp1.Size = 1024;

                DbParameter dp2 = db.Cmd.CreateParameter();
                dp2.DbType = DbType.String;
                dp2.Direction = ParameterDirection.Output;
                dp2.ParameterName = "ERRCODE";
                dp2.Value = errCode;
                dp2.Size = 1024;

                DbParameter dp3 = db.Cmd.CreateParameter();
                dp3.DbType = DbType.String;
                dp3.Direction = ParameterDirection.Output;
                dp3.ParameterName = "ERRMSG";
                dp3.Value = errMsg;
                dp3.Size = 1024;

                dpc = db.ExecuteProc(sql, new DbParameter[] { dp1, dp2, dp3 });

                errCode = db.Cmd.Parameters[1].Value.ToString();
                errMsg = db.Cmd.Parameters[2].Value.ToString();
            }
            catch (Exception e)
            {
                errCode = "-1";
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取操作员最后一次日结时间
        /// </summary>
        /// <param name="db"></param>
        /// <param name="balanceID"></param>
        /// <param name="dpc"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetDayReportMaxDate(BaseEntityer db, string opercode, ref DateTime maxDate, ref string errMsg)
        {
            string sql = @"PKG_INPATIENT_DAYBANLANCE.PRC_GET_MAX_BALANCDATE";
            DbParameterCollection dpc = null;
            try
            {
                DbParameter dp1 = db.Cmd.CreateParameter();
                dp1.DbType = DbType.String;
                dp1.Direction = ParameterDirection.Input;
                dp1.ParameterName = "BALANCEOPERCODE";
                dp1.Value = opercode;
                dp1.Size = 1024;

                DbParameter dp2 = db.Cmd.CreateParameter();
                dp2.DbType = DbType.String;
                dp2.Direction = ParameterDirection.Output;
                dp2.ParameterName = "MAXDATE";
                dp2.Value = maxDate;
                dp2.Size = 1024;

                dpc = db.ExecuteProc(sql, new DbParameter[] { dp1, dp2 });
                maxDate = DateTime.Parse(db.Cmd.Parameters[1].Value.ToString());
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取历史日结信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="opercode"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="ds"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetDayReportHistory(BaseEntityer db, string opercode, DateTime beginDate, DateTime endDate, ref DataSet ds, ref string errMsg)
        {
            string sql = @"SELECT DISTINCT t.oper_date,
                                            t.ID,
                                            t.begin_date,
                                            t.end_date,
                                            t.information reportno
                              FROM inp_acct_dayreport t
                             WHERE t.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                               AND t.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               and (t.balance_oper = '{2}' or 'ALL' = '{2}')
                               and t.main_type = 'A0000'
                             ORDER BY t.oper_date desc
";
            sql = string.Format(sql, beginDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"), opercode);
            try
            {
                ds = new DataSet();
                ds = db.GetDataSet(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取历史日结信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="opercode"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="ds"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetDayReportHistory_Inpatient(BaseEntityer db, string opercode, DateTime beginDate, DateTime endDate, ref DataSet ds, ref string errMsg)
        {
            string sql = "";
            if (opercode == "")
            {
                sql = @"SELECT DISTINCT t.oper_date 操作日期,
                                            t.ID 日结序号,
                                            t.balance_oper 日结人,
                                            t.begin_date 日结开始时间,
                                            t.end_date 日结结束时间,
                                            t.information 日结信息 
                              FROM inp_acct_dayreport t
                             WHERE t.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                               AND t.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               and t.main_type = 'A0000'
                             ORDER BY t.oper_date desc
";
            }
            else
            {
                sql = @"SELECT DISTINCT t.oper_date 操作日期,
                                            t.ID 日结序号,
                                            t.balance_oper 日结人,
                                            t.begin_date 日结开始时间,
                                            t.end_date 日结结束时间,
                                            t.information 日结信息 
                              FROM inp_acct_dayreport t
                             WHERE t.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                               AND t.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               and (t.balance_oper = '{2}' or 'ALL' = '{2}')
                               and t.main_type = 'A0000'
                             ORDER BY t.oper_date desc
";
            }
            sql = string.Format(sql, beginDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"), opercode);
            try
            {
                ds = new DataSet();
                ds = db.GetDataSet(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 按日结编号获取日结信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="rcptID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetDayReportByID(BaseEntityer db, string balanceID, ref DataTable ds, ref string errMsg)
        {
            string sql = @"select * from inp_acct_dayreport q where q.id = '{0}'";
            sql = string.Format(sql, balanceID);
            try
            {
                ds = new DataTable();
                ds = db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 是否做过日结
        /// </summary>
        /// <param name="db"></param>
        /// <param name="rcptID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool IsDayReport(BaseEntityer db, string opercode, DateTime operDate, ref string errMsg)
        {
            string sql = @"SELECT case
                             when count(*) > 0 then
                              '1'
                             else
                              '0'
                           end
                      FROM inp_acct_dayreport t
                     where t.oper_code = '{0}'
                       and t.oper_date = to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
";
            sql = string.Format(sql, opercode, operDate.ToString());
            int revInt = 0;
            try
            {
                DataTable dt = new DataTable();
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                    revInt = int.Parse(dt.Rows[0][0].ToString());
                else
                    revInt = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
            if (revInt <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetDayReportMaxDate(BaseEntityer db, string opercode, ref string errMsg)
        {
            string sql = @"SELECT MAX(t.end_date)
                              FROM inp_acct_dayreport t
                             WHERE t.balance_oper = '{0}'";
            sql = string.Format(sql, opercode);
            string rev = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                dt = db.GetDataTable(sql);
                rev = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return string.Empty;
            }
            return rev;
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
        public int GetDayReportCollectMaxDate(BaseEntityer db, string opercode, ref DateTime maxDate, ref string errMsg)
        {
            string sql = @"SELECT NVL(TO_CHAR(MAX(t.check_date + 1 / 86400), 'yyyy-mm-dd hh24:mi:ss'), '2013-01-01 00:00:00')
                      FROM inp_acct_dayreport t
                     WHERE t.check_oper = '{0}'";

            try
            {
                sql = string.Format(sql, opercode);
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "没有查询到上次日结汇总日期。";
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

        /// <summary>
        /// 获取操作员最后一次日结汇总开始时间
        /// </summary>
        /// <param name="db"></param>
        /// <param name="balanceID"></param>
        /// <param name="dpc"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetDayReportCollectStartDate(BaseEntityer db, string opercode, string checkDate, ref DateTime maxDate, ref string errMsg)
        {
            string sql = @"select NVL(TO_CHAR(startDate + 1 / 86400, 'yyyy-mm-dd hh24:mi:ss'),
                                       '2013-01-01 00:00:00')
                              from (select t.check_date startDate
                                      from inp_acct_dayreport t
                                     where t.check_oper = '{0}' or 'ALL' = '{0}'
                                       and t.check_date <
                                           to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                                       and t.check_flag = 1
                                     group by t.check_date
                                     order by t.check_date desc)
                             where rownum < 2";
            try
            {
                sql = string.Format(sql, opercode, checkDate);
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    maxDate = DateTime.Parse("2013-01-01 00:00:00");
                    return 1;
                }
                else
                    if (dt.Rows.Count <= 0)
                    {
                        errMsg = "没有查询到上次日结汇总日期。";
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

        /// <summary>
        /// 查询日结汇总
        /// </summary>
        /// <param name="db"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable QueryDayReportCollect(BaseEntityer db, string beginDate, string endDate, ref string errMsg)
        {
            string sql = @"
select t.id Id,(select c.user_name from users_staff_dict c where c.user_id=t.balance_oper) Oper,t.main_type Main_Type,t.information Information from inp_acct_dayreport t 
where t.oper_date>= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
and  t.oper_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
and t.check_flag <> 1
group by t.id,t.balance_oper,t.main_type,t.information
order by t.id,t.main_type
";
            DataTable ds = new DataTable();
            try
            {
                sql = string.Format(sql, beginDate, endDate);
                ds = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 查询日结汇总（维康）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable QueryDayReportWKCollect(BaseEntityer db, string beginDate, string endDate, ref string errMsg)
        {
            string sql = @"
select t.id Id,(select c.user_name from users_staff_dict c where c.user_id=t.balance_oper) Oper,t.main_type Main_Type,t.information Information,t.sec_type SEC_TYPE from inp_acct_dayreport t 
where t.oper_date>= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
and  t.oper_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
and t.check_flag =0
group by t.id,t.balance_oper,t.main_type,t.information,t.sec_type
order by t.id,t.main_type
";
            DataTable ds = new DataTable();
            try
            {
                sql = string.Format(sql, beginDate, endDate);
                ds = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 查询历史日结汇总
        /// </summary>
        /// <param name="db"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable QueryDayReportCollectHistory(BaseEntityer db, string beginDate, string endDate, string oper, ref string errMsg)
        {
            string sql = @"
select t.id Id,(select c.user_name from users_staff_dict c where c.user_id=t.balance_oper) Oper,t.main_type Main_Type,t.information Information from inp_acct_dayreport t 
where t.check_date= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
--and  t.check_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
and t.check_flag = 1
and (t.check_oper = '{1}' or 'ALL' = '{1}')
group by t.id,t.balance_oper,t.main_type,t.information
order by t.id,t.main_type
";
            DataTable ds = new DataTable();
            try
            {
                sql = string.Format(sql, endDate, oper);
                ds = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 查询历史日结汇总
        /// </summary>
        /// <param name="db"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable QueryDayReportCollectWKHistory(BaseEntityer db, string beginDate, string endDate, string oper, ref string errMsg)
        {
            string sql = @"
select t.id Id,(select c.user_name from users_staff_dict c where c.user_id=t.balance_oper) Oper,t.main_type Main_Type,t.information Information,t.sec_type SEC_TYPE from inp_acct_dayreport t 
where t.check_date= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
--and  t.check_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
and t.check_flag != 0
and (t.check_oper = '{1}' or 'ALL' = '{1}')
group by t.id,t.balance_oper,t.main_type,t.information,t.sec_type
order by t.id,t.main_type
";
            DataTable ds = new DataTable();
            try
            {
                sql = string.Format(sql, endDate, oper);
                ds = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 查询历史日结汇总(主管用)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable QueryCollectPrepayHistory(BaseEntityer db, string beginDate, string endDate, ref string errMsg)
        {
            string sql = @"
select (select c.user_name
          from users_staff_dict c
         where c.user_id = t.check_oper) Oper,
       t.check_date Check_Date
  from inp_acct_dayreport t
 where t.check_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
   and t.check_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
   and t.check_flag = 1
 group by t.check_oper,t.check_date
 order by t.check_date desc
";
            DataTable ds = new DataTable();
            try
            {
                sql = string.Format(sql, beginDate, endDate);
                ds = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 修改日结汇总标志
        /// </summary>
        /// <param name="db"></param>
        /// <param name="prepay"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DayBalanceCollectSave(BaseEntityer db, string userID, string startDate, string endDate, ref string errMsg)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"update inp_acct_dayreport t 
                                set t.check_flag = 1,
                                       t.check_oper = '{2}',
                                       t.check_date = to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') 
                                 where t.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                                   and t.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                                    and (t.check_flag = 0 or t.check_flag = '')
                                ";
                sql = string.Format(sql, startDate, endDate, userID);//14
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return IsSuccess;
        }


        /// <summary>
        /// 修改日结汇总标志(贵阳医院专用)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="prepay"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DayGYBalanceCollectSave(BaseEntityer db, string userID, string startDate, string endDate, ref string errMsg)
        {
            int IsSuccess = 0;

            DateTime dt = DateTime.Now;

            string sub_no = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
            try
            {
                string sql = @"update inp_acct_dayreport t 
                                set t.check_flag = '{3}',
                                       t.check_oper = '{2}',
                                       t.check_date = to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') 
                                 where t.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                                   and t.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                                    and (t.check_flag = 0 or t.check_flag = '')
                                ";
                sql = string.Format(sql, startDate, endDate, userID, sub_no);//14
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return IsSuccess;
        }


        /// <summary>
        /// 获取历史日结汇总信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="opercode"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="ds"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetDayReportCollectHistory(BaseEntityer db, string opercode, DateTime beginDate, DateTime endDate, ref DataSet ds, ref string errMsg)
        {
            string sql = @"SELECT t.check_date 日结汇总日期,
                                  t.check_oper as 结算员ID
                          FROM inp_acct_dayreport t
                         WHERE t.check_date>= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                           and t.check_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                           and t.check_flag !=0
                           and (t.check_oper = '{0}' or 'ALL' = '{0}')
                         group by t.check_date,t.check_oper
                         ORDER BY t.check_date desc
";
            sql = string.Format(sql, opercode, beginDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                ds = new DataSet();
                ds = db.GetDataSet(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取历史日结汇总开始时间
        /// </summary>
        /// <param name="db"></param>
        /// <param name="opercode"></param>
        /// <param name="endDate"></param>
        /// <param name="startDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetDayReportMaxCollectDate(BaseEntityer db, string opercode, string endDate, ref string startDate, ref string errMsg)
        {
            string sql = @"SELECT NVL(TO_CHAR(MAX(s.check_date + 1 / 86400), 'yyyy-mm-dd hh24:mi:ss'), '2013-01-01 00:00:00')
                              FROM inp_acct_dayreport s
                             WHERE s.check_date <
                                   (SELECT max(t.check_date)
                                      FROM inp_acct_dayreport t
                                     WHERE
                                       t.check_date <
                                           to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                                       and t.check_flag = 1
                                       and t.check_oper = '{0}')
                               and s.check_flag <>0
                               and s.check_oper = '{0}'";
            try
            {
                sql = string.Format(sql, opercode, endDate);
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "没有查询到日结汇总开始时间。";
                    return -1;
                }
                startDate = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取历史日结汇总开始时间(四平用)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="endDate"></param>
        /// <param name="startDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetDayReportMaxCollectDateSP(BaseEntityer db, string endDate, ref string startDate, ref string errMsg)
        {
            string sql = @"SELECT NVL(TO_CHAR(MAX(s.check_date + 1 / 86400), 'yyyy-mm-dd hh24:mi:ss'), '2013-01-01 00:00:00')
                              FROM inp_acct_dayreport s
                             WHERE s.check_date <=
                                   (SELECT max(t.check_date)
                                      FROM inp_acct_dayreport t
                                     WHERE
                                       t.check_date <
                                           to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                                       and t.check_flag = 1)
                               and s.check_flag = 1";
            try
            {
                sql = string.Format(sql, endDate);
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "没有查询到日结汇总开始时间。";
                    return -1;
                }
                startDate = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 是否做过日结汇总
        /// </summary>
        /// <param name="db"></param>
        /// <param name="rcptID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool IsDayReportCollect(BaseEntityer db, string opercode, DateTime operDate, ref string errMsg)
        {
            string sql = @"SELECT case
                             when count(*) > 0 then
                              '1'
                             else
                              '0'
                           end
                      FROM inp_acct_dayreport t
                     where t.check_oper = '{0}'
                       and t.check_flag = 1
                       and t.check_date = to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
";
            sql = string.Format(sql, opercode, operDate.ToString());
            int revInt = 0;
            try
            {
                DataTable dt = new DataTable();
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                    revInt = int.Parse(dt.Rows[0][0].ToString());
                else
                    revInt = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
            if (revInt <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 是否做过日结汇总（四平日结汇总用）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="operDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool IsTodayReportCollection(BaseEntityer db, DateTime operDate, ref string errMsg)
        {
            string sql = @"SELECT case
                         when count(*) > 0 then
                          '1'
                         else
                          '0'
                       end
                  FROM inp_acct_dayreport t
                 where t.check_flag = 1
                   and to_char(t.check_date,'yyyy-mm-dd') = to_char('{0}')
";
            sql = string.Format(sql, operDate.ToString("yyyy-mm-dd"));
            int revInt = 0;
            try
            {
                DataTable dt = new DataTable();
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                    revInt = int.Parse(dt.Rows[0][0].ToString());
                else
                    revInt = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
            if (revInt <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 取消日结汇总
        /// </summary>
        /// <param name="db"></param>
        /// <param name="Oper"></param>
        /// <param name="checkDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int CancelDayReportCollect(BaseEntityer db, string Oper, DateTime checkDate, ref string errMsg)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"update inp_acct_dayreport t
                               set t.check_flag = '0',
                                   t.check_oper = '',
                                   t.check_date = ''
                             where t.check_date = to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               and t.check_oper = '{0}'
                               and t.check_flag = 1
                                ";
                sql = string.Format(sql, Oper, checkDate);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 预交金明细查询
        /// </summary>
        /// <param name="db"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="opercode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<PREPAYMENT_RCPT> GetPrepaymentDetail(BaseEntityer db, string startDate, string endDate, string opercode, ref string errMsg)
        {
            string sql = @"select t.patient_id,s.name,f.dept_name,t.transact_date,t.amount,t.operator_no,decode(t.transact_type,'1','交款','2','退款','其他'),t.check_no,a.charge_type
                              from dept_dict f,prepayment_rcpt t
                              left join pat_master_index s on t.patient_id = s.patient_id
                              left join pat_visit a on t.patient_id = a.patient_id and t.visit_id = a.visit_id
                             where t.transact_date > to_date('{0}', 'yyyy-mm-dd HH24:mi:ss')
                               and t.transact_date <= to_date('{1}', 'yyyy-mm-dd HH24:mi:ss')
                               and t.transact_type in ('1', '2')
                               and f.dept_code = a.dept_admission_to
                               and t.operator_no = '{2}'
                                order by t.transact_date";
            List<PREPAYMENT_RCPT> rcpt = new List<PREPAYMENT_RCPT>();
            try
            {
                sql = string.Format(sql, startDate, endDate, opercode);
                DataTable dt = db.GetDataTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PREPAYMENT_RCPT prepay = new PREPAYMENT_RCPT();
                    prepay.PATIENT_ID = dt.Rows[i][0].ToString();
                    prepay.BALANCE_INVOICE = dt.Rows[i][1].ToString();
                    prepay.BANK = dt.Rows[i][2].ToString();
                    prepay.TRANSACT_DATE = DateTime.Parse(dt.Rows[i][3].ToString());
                    prepay.AMOUNT = decimal.Parse(dt.Rows[i][4].ToString());
                    prepay.OPERATOR_NO = dt.Rows[i][5].ToString();
                    prepay.TRANSACT_TYPE = dt.Rows[i][6].ToString();
                    prepay.CHECK_NO = dt.Rows[i][7].ToString();
                    prepay.RCPT_NO = dt.Rows[i][8].ToString();
                    rcpt.Add(prepay);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return rcpt;
        }

        /// <summary>
        /// 预交金明细汇总查询
        /// </summary>
        /// <param name="db"></param>
        /// <param name="endDate"></param>
        /// <param name="opercode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<PREPAYMENT_RCPT> GetPrepaymentCollectDetail(BaseEntityer db, string endDate, string opercode, ref string errMsg)
        {
            string sql = @"select t.patient_id,
                               s.name,
                               f.dept_name,
                               t.transact_date,
                               t.amount,
                               t.operator_no,
                               decode(t.pay_way, '1', '现金', '2', '支票', '医保挂账'),
                               t.check_no,
                               a.charge_type
                          from dept_dict f, prepayment_rcpt t
                          left join pat_master_index s
                            on t.patient_id = s.patient_id
                          left join pat_visit a
                            on t.patient_id = a.patient_id
                           and t.visit_id = a.visit_id
                           where f.dept_code = a.dept_admission_to
                           and t.transact_type in ('1', '2')
                           and  t.acct_no in
                        (select f.id
                          from inp_acct_dayreport f
                         where f.check_date = to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                         and (f.check_oper = '{1}' or 'ALL' = '{1}') and f.check_flag = 1
                         group by f.id)"
                ;
            List<PREPAYMENT_RCPT> rcpt = new List<PREPAYMENT_RCPT>();
            try
            {
                sql = string.Format(sql, endDate, opercode);
                DataTable dt = db.GetDataTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PREPAYMENT_RCPT prepay = new PREPAYMENT_RCPT();
                    prepay.PATIENT_ID = dt.Rows[i][0].ToString();
                    prepay.BALANCE_INVOICE = dt.Rows[i][1].ToString();
                    prepay.BANK = dt.Rows[i][2].ToString();
                    prepay.TRANSACT_DATE = DateTime.Parse(dt.Rows[i][3].ToString());
                    prepay.AMOUNT = decimal.Parse(dt.Rows[i][4].ToString());
                    prepay.OPERATOR_NO = dt.Rows[i][5].ToString();
                    prepay.TRANSACT_TYPE = dt.Rows[i][6].ToString();
                    prepay.CHECK_NO = dt.Rows[i][7].ToString();
                    prepay.RCPT_NO = dt.Rows[i][8].ToString();
                    rcpt.Add(prepay);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return rcpt;
        }

        /// <summary>
        /// 插入一条交班时刻在院患者费用情况日结记录
        /// </summary>
        /// <param name="spInDayReportDetail">交班时刻在院患者费用情况日结记录</param>
        /// <param name="Db"></param>
        public int InsertSpInDayReportDetail(SpInDayReportDetail spInDayReportDetail, BaseEntityer Db, ref string errMsg)
        {
            int rev = -1;
            string sql = @"insert into SP_INDAYREPORTDETAIL (SERIAL_NO, PATIENT_ID, VISIT_ID, 
                           NAME, TOT_COST, PREPAY_COST, REMAIN_COST, INHOSPITAL_DATE, CHARGE_TYPE_CODE, 
                           CHARGE_TYPE_NAME, OPERDATE, OPER_CODE)
                         values ('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}',  
                                 to_date('{7}', 'yyyy-MM-dd hh24:mi:ss'), '{8}', '{9}', 
                                 to_date('{10}', 'yyyy-MM-dd hh24:mi:ss'), '{11}') ";
            try
            {
                sql = string.Format(sql, spInDayReportDetail.SERIAL_NO,
                             spInDayReportDetail.PATIENT_ID, spInDayReportDetail.VISIT_ID,
                             spInDayReportDetail.NAME, spInDayReportDetail.TOT_COST,
                             spInDayReportDetail.PREPAY_COST, spInDayReportDetail.REMAIN_COST,
                             spInDayReportDetail.INHOSPITAL_DATE, spInDayReportDetail.CHARGE_TYPE_CODE,
                             spInDayReportDetail.CHARGE_TYPE_NAME, spInDayReportDetail.OPERDATE,
                             spInDayReportDetail.OPER_CODE);
                rev = Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return rev;
        }

        /// <summary>
        /// 删除交班时刻在院患者费用情况日结记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="Oper"></param>
        /// <param name="checkDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int DeleteSpInDayReportDetail(BaseEntityer db, string Oper, DateTime checkDate, ref string errMsg)
        {
            int IsSuccess = 0;
            try
            {
                string sql = @"delete from SP_INDAYREPORTDETAIL t
                                 where t.oper_code = '{0}'
                                   and t.operdate = to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                                ";
                sql = string.Format(sql, Oper, checkDate);
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 获取交班日结住院患者费用情况
        /// </summary>
        /// <returns></returns>
        public List<SuccessionDaily> GetSuccessionDaily(string date)
        {
            string sql = string.Empty;
            sql = @"select p.patient_id,
                   p.visit_id,
                   p.admission_date_time,
                   p.charge_type,
                   p.charge_type_code,
                   p.visit_id,
                   (select m.name
                      from pat_master_index m
                     where m.patient_id = p.patient_id) patient_name,
                   nvl((select sum(charges)
                      from inp_bill_detail i
                     where i.patient_id = p.patient_id
                       and i.visit_id = p.visit_id and i.billing_date_time <= to_date('{0}','yyyy-MM-dd hh24:mi:ss')),0) charges,
                   nvl((select sum(amount)
                      from prepayment_rcpt r
                     where r.patient_id = p.patient_id
                       and r.visit_id = p.visit_id and r.transact_date <= to_date('{0}','yyyy-MM-dd hh24:mi:ss')),0) amount,
                   nvl(((
                       nvl((select sum(amount)
                       from prepayment_rcpt r
                      where r.patient_id = p.patient_id
                        and r.visit_id = p.visit_id and r.transact_date <= to_date('{0}','yyyy-MM-dd hh24:mi:ss')),0)
                      ) -
                   (
                       nvl((select sum(charges)
                       from inp_bill_detail i
                      where i.patient_id = p.patient_id
                        and i.visit_id = p.visit_id and i.billing_date_time <= to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                      ),0)
                   )),0) balance
              from pat_visit p
             where  p.state in ('I','B','R') and p.admission_date_time <= to_date('{0}','yyyy-MM-dd hh24:mi:ss')
 ";
            sql = string.Format(sql, date);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.SuccessionDaily>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 根据日结日期与日结ID获取某一批数据，用于报表显示
        /// </summary>
        /// <param name="OperDate"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<SpInDayReportDetail> GetSpInDayReportDetailByOperDateAndID(string OperDate, string ID)
        {
            string sql = string.Empty;
            sql = @"select * from SP_INDAYREPORTDETAIL s 
                    where s.operdate = to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                    and (s.oper_code = '{1}' or 'ALL' = '{1}')
                    order by s.serial_no DESC";
            sql = string.Format(sql, OperDate, ID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.SpInDayReportDetail>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 根据日结日期与日结ID获取某一批数据，用于报表显示
        /// </summary>
        /// <param name="OperDate"></param>
        /// <returns></returns>
        public List<SpInDayReportDetail> GetSpInDayReportDetailByManager(string OperDate)
        {
            string sql = string.Empty;
            sql = @"select * from SP_INDAYREPORTDETAIL s 
                    where s.operdate = to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                    order by s.serial_no DESC";
            sql = string.Format(sql, OperDate);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.SpInDayReportDetail>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 根据日结ID获取某一批数据，用于报表显示
        /// </summary>
        /// <param name="OperDate"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<SpInDayReportDetail> GetSpInDayReportDetailByID(string ID)
        {
            string sql = string.Empty;
            sql = @"select * from SP_INDAYREPORTDETAIL s 
                    where s.id = '{0}'
                    order by s.serial_no DESC";
            sql = string.Format(sql, ID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.SpInDayReportDetail>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 获取预交金发票流水号（四平用）
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetSpRcptSerialNo(ref string errMsg)
        {
            string sql = string.Empty;
            string serialNo = string.Empty;
            DataTable dt = new DataTable();
            sql = @"select SPPREPAYSERIAL.NEXTVAL from dual";
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                serialNo = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message + "|" + e.StackTrace;
                return string.Empty;
            }
            return serialNo;
        }
        #endregion

        #region 查询统计

        /// <summary>
        /// 获取发票项目类别金额
        /// </summary>
        /// <returns></returns>
        public DataTable GetInvoiceDetails(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select t.class_on_inp_rcpt as 项目编码,
       (select inp_rcpt_fee_dict.fee_class_name
          from inp_rcpt_fee_dict
         where inp_rcpt_fee_dict.fee_class_code = t.class_on_inp_rcpt) as 项目类别,
       
       sum(t.costs) as 金额

  from inp_bill_detail t
 where t.patient_id = '{0}'
   and t.visit_id = '{1}'

 group by t.class_on_inp_rcpt
 order by t.class_on_inp_rcpt";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取非药品明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnDrugDetails(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select (select BILL_ITEM_CLASS_DICT.CLASS_NAME
                                          from BILL_ITEM_CLASS_DICT
                                         where BILL_ITEM_CLASS_DICT.CLASS_CODE = t.item_class) as 项目类别,
                               t.item_code as 项目编号,
                               t.item_name as 项目名称,
                               t.item_spec || '/' || t.units as 规格,
                               round(decode(t.costs,0,0,t.costs / t.amount),4)  as 单价,
                               t.amount as 数量,
                               t.costs as 金额,
                               t.charges as 优惠后金额,
                               t.billing_date_time as 计价时间,
                               (select USERS_STAFF_DICT.USER_NAME
                                  from USERS_STAFF_DICT
                                 where USERS_STAFF_DICT.User_Id = t.operator_no) as 计费人
                          from inp_bill_detail t
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}'
                           and t.item_class not in ('A', 'B')
                         order by t.item_class, t.item_code";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取药品明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetDrugDetails(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select 
                                (select BILL_ITEM_CLASS_DICT.CLASS_NAME from BILL_ITEM_CLASS_DICT 
                                    where BILL_ITEM_CLASS_DICT.CLASS_CODE = t.item_class) as 项目类别,
                               t.item_code as 项目编号,
                               t.item_name as 项目名称,
                               t.item_spec || '/' || t.units as 规格,
                               round(decode(t.costs,0,0,t.costs / t.amount),4) as 单价,
                               t.amount as 数量,
                               t.costs as 金额,
                               t.charges as 优惠后金额,
                               t.billing_date_time as 计价时间,
                               (select USERS_STAFF_DICT.USER_NAME
                                  from USERS_STAFF_DICT
                                 where USERS_STAFF_DICT.User_Id = t.operator_no) as 计费人
                          from inp_bill_detail t
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}'
                           and t.item_class in ('A', 'B')
                         order by t.item_class, t.item_code";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取入出转信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetTransfer(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select (select t.dept_name
                                  from dept_dict t
                                 where t.dept_code = c.dept_stayed) as 转入科室,
                               c.admission_date_time as 转入日期,
                               (select s.dept_name
                                  from dept_dict s
                                 where s.dept_code = c.dept_transfered_to) as 转出科室,
                               c.discharge_date_time as 转出日期
                          from TRANSFER c
                         where c.patient_id = '{0}'
                           and c.visit_id = '{1}'
                         order by c.dept_stayed";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取诊断信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetDiagnose(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select s.diagnosis_type_name as 诊断类型,
                                   t.diagnosis_no as 诊断序号,
                                   t.diag_code as 诊断编码,
                                   t.diagnosis_desc as 诊断名称,
                                   t.diagnosis_date as 诊断日期
                              from DIAGNOSIS t, DIAGNOSIS_TYPE_DICT s
                             where t.patient_id = '{0}'
                               and t.visit_id = '{1}'
                               and s.diagnosis_type_code = t.diagnosis_type";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取担保信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetGuarant(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select t.guarant_type      as 担保类型,
                               t.guarant_name      as 担保人,
                               t.guarant_money     as 担保金额,
                               t.guarant_phone     as 担保电话,
                               t.guarant_bussiness as 担保人地址,
                               t.guarant_mark      as 担保备注
                          from com_guarant t
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}' ";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取结算信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetBalanceInfo(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select count(*) from siinfo s where s.inpatient_id='{0}' and s.visit_id='{1}' and s.BALANCE_STATE = 1";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows[0][0].ToString() == "0")
                {
                    sql = @"select distinct t.rcpt_no as 收据号,t.INVOICE as 发票号 ,nvl(t.costs,0) as  HIS总额,nvl(t.charges,0) as HIS自费,nvl(t.costs-t.charges,0) as HIS统筹,'0' as 医保总额,'0' as 医保统筹,'0' as 医保账户,'0' as 医保自费,'0' as 医保公务员,'0' as 医保大额,t.settling_date as 结算时间
                                from inp_settle_master t
                                where t.patient_id = '{0}'
                                and t.visit_id = '{1}'
                                order by to_number(t.rcpt_no)";
                    sql = string.Format(sql, patientid, visitid);
                    dt = new DataTable();
                    dt = BaseEntityer.Db.GetDataTable(sql);
                }
                else
                {
                    sql = @"select distinct t.rcpt_no as 收据号,t.INVOICE as 发票号,nvl(t.costs,0) as  HIS总额,nvl(t.charges,0) as HIS自费,nvl(t.costs-t.charges,0) as HIS统筹,nvl(s.tot_cost,0) as 医保总额,nvl(s.pub_cost,0) as 医保统筹,nvl(s.pay_cost,0) as 医保账户,nvl(s.own_cost,0) as 医保自费,nvl(s.official_cost,0) as 医保公务员,nvl(s.over_cost,0) as 医保大额,t.settling_date as 结算时间
                                  from inp_settle_master t, siinfo s
                                 where t.patient_id = '{0}'
                                   and t.visit_id = '{1}'
                                   and t.patient_id = s.inpatient_id(+)
                                   and t.visit_id = s.visit_id(+)
                                   and t.costs = s.tot_cost
                                   and s.BALANCE_STATE = 1
                                   order by to_number(t.rcpt_no)";
                    sql = string.Format(sql, patientid, visitid);
                    dt = new DataTable();
                    dt = BaseEntityer.Db.GetDataTable(sql);
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 患者基本信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetQueryPatientByDataGridView(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select t.patient_id as 患者ID,
                                   t.inp_no as 住院号,
                                    t.name as 姓名,
                                   t.sex as 性别,
                                    --f.sex_name as 性别,
                                   trunc(to_char(sysdate, 'yyyy')) -
                                   trunc(to_char(t.date_of_birth, 'yyyy')) as 年龄,
                                   t.id_no as 身份证号,
                                   s.charge_type as 费别,
                                   (select e.dept_name from dept_dict e where e.dept_code = c.ward_code) as 护理单元,
                                   decode(s.state,
                                          'I',
                                          (select e.dept_name
                                             from dept_dict e
                                            where e.dept_code = c.dept_code),
                                          'R',
                                          (select e.dept_name
                                             from dept_dict e
                                            where e.dept_code = s.dept_admission_to),
                                          (select e.dept_name
                                             from dept_dict e
                                            where e.dept_code = s.dept_discharge_from)) as 科室,
                                   decode(s.state, 'I', c.bed_no, '') as 床号,
                                   to_char(s.admission_date_time, 'yyyy-mm-dd') as 入院日期,
                                   to_char(s.discharge_date_time, 'yyyy-mm-dd') as 出院日期,
                                   (select g.state_name from instate_dict g where g.state_id = s.state) as 在院状态,
                                   (select nvl(sum(a.costs), 0)
                                      from inp_bill_detail a
                                     where a.patient_id = s.patient_id
                                       and a.visit_id = s.visit_id) as 总费用,
                                   (select nvl(sum(h.amount),0)
                                      from prepayment_rcpt h
                                     where h.patient_id = s.patient_id
                                       and h.visit_id = s.visit_id) as 预交金额,
                                   (select nvl(sum(a.costs), 0)
                                      from inp_bill_detail a
                                     where a.patient_id = s.patient_id
                                       and a.visit_id = s.visit_id
                                       and a.rcpt_no is null) as 未清金额,
                                   (select nvl(sum(a.costs), 0)
                                      from inp_bill_detail a
                                     where a.patient_id = s.patient_id
                                       and a.visit_id = s.visit_id
                                       and a.rcpt_no is not null) as 已清金额,
                                   nvl(c.prepayments - c.total_charges,0) as 余额,
                                   t.next_of_kin as 联系人姓名,
                                   t.next_of_kin_addr || t.mailing_address as 联系人地址,
                                   t.next_of_kin_phone || t.phone_number_home as 联系人电话,
                                   s.insurance_type as 工作单位,
                                   t.phone_number_business as 单位电话,
                                   s.working_status as 医保卡号,
                                   c.total_charges as 自费金额
                              from pat_master_index t, pat_visit s, pats_in_hospital c--, sex_dict f
                             where t.patient_id = s.patient_id
                               and t.patient_id = c.patient_id(+)
                               --and t.sex = f.sex_code
                               and s.visit_id = '{1}'
                               and t.patient_id = '{0}'
";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 患者基本信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetQueryPatient(string patientid, string visitid, ref string errmsg)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @"select t.name as 姓名,
                            t.sex as 性别,
                           --f.sex_name as 性别,
                           trunc(to_char(sysdate, 'yyyy')) -
                           trunc(to_char(t.date_of_birth, 'yyyy')) as 年龄,
                           t.inp_no as 住院号,
                           s.charge_type as 费别,
                           (select e.dept_name from dept_dict e where e.dept_code = c.ward_code) as 护理单元,
                           decode(s.state,
                                  'I',
                                  (select e.dept_name
                                     from dept_dict e
                                    where e.dept_code = c.dept_code),
                                  'R',
                                  (select e.dept_name
                                     from dept_dict e
                                    where e.dept_code = s.dept_admission_to),
                                  (select e.dept_name
                                     from dept_dict e
                                    where e.dept_code = s.dept_discharge_from)) as 科室,
                           decode(s.state, 'I', c.bed_no, '') as 床号,
                           to_char(s.admission_date_time, 'yyyy-mm-dd') as 入院日期,
                           to_char(s.discharge_date_time, 'yyyy-mm-dd') as 出院日期,
                           (select g.state_name from instate_dict g where g.state_id = s.state) as 在院状态,
                           (select nvl(sum(a.costs), 0)
                              from inp_bill_detail a
                             where a.patient_id = s.patient_id
                               and a.visit_id = s.visit_id) as 总费用,
                           (select nvl(sum(h.amount),0)
                              from prepayment_rcpt h
                             where h.patient_id = s.patient_id
                               and h.visit_id = s.visit_id and h.transact_type in ('1','2')) as 预交金额,
                           (select nvl(sum(a.costs), 0)
                              from inp_bill_detail a
                             where a.patient_id = s.patient_id
                               and a.visit_id = s.visit_id
                               and a.rcpt_no is null) as 未清金额,
                           (select nvl(sum(a.costs), 0)
                              from inp_bill_detail a
                             where a.patient_id = s.patient_id
                               and a.visit_id = s.visit_id
                               and a.rcpt_no is not null) as 已清金额,
                           nvl((select nvl(sum(h.amount),0)
                              from prepayment_rcpt h
                             where h.patient_id = s.patient_id
                               and h.visit_id = s.visit_id and h.transact_type in ('1','2')) - (select nvl(sum(a.costs), 0)
                              from inp_bill_detail a
                             where a.patient_id = s.patient_id
                               and a.visit_id = s.visit_id),0) as 余额
                      from pat_master_index t, pat_visit s, pats_in_hospital c--, sex_dict f
                     where t.patient_id = s.patient_id
                       and t.patient_id = c.patient_id(+)
                       --and t.sex = f.sex_code
                       and s.visit_id = '{1}'
                       and t.patient_id = '{0}'";
                sql = string.Format(sql, patientid, visitid);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 患者基本信息条件
        /// </summary>
        /// <param name="charge_type"></param>
        /// <param name="state"></param>
        /// <param name="dept_stayed"></param>
        /// <param name="indatefrom"></param>
        /// <param name="indateto"></param>
        /// <param name="outdatefrom"></param>
        /// <param name="outdateto"></param>
        /// <param name="baldatefrom"></param>
        /// <param name="baldateto"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public DataTable GetQueryPatientConditions1(string charge_type, string state, string dept_stayed, string indatefrom, string indateto, string outdatefrom, string outdateto, string baldatefrom, string baldateto, ref string errmsg)
        {
            DataTable dt = new DataTable();
            //2013-5-22 by li 增加住院天数统计列
            //2014-4-25 by li 入院日期和出院日期由日期型修改为带时间类型
            try
            {
                string sql = @"select t.patient_id as 患者ID,
       s.visit_id   as 序号,
       t.inp_no     as 住院号,
       t.name       as 姓名,
       t.sex        as 性别,
       trunc(to_char(sysdate, 'yyyy')) -
       trunc(to_char(t.date_of_birth, 'yyyy')) as 年龄,
       t.id_no as 身份证号,
       s.charge_type as 费别,
       (trunc(s.discharge_date_time) - trunc(s.admission_date_time)) as 住院天数,
       (select e.dept_name from dept_dict e where e.dept_code = c.ward_code) as 护理单元,
       (select e.dept_name from dept_dict e where e.dept_code = c.dept_code) as 科室,
       decode(s.state, 'I', c.bed_no, '') as 床号,
       to_char(s.admission_date_time, 'yyyy-mm-dd hh24:mi:ss') as 入院日期,
       to_char(s.discharge_date_time, 'yyyy-mm-dd hh24:mi:ss') as 出院日期,
       (select g.state_name from instate_dict g where g.state_id = s.state) as 在院状态,
       (select nvl(sum(a.costs), 0)
          from inp_bill_detail a
         where a.patient_id = s.patient_id
           and a.visit_id = s.visit_id) as 总费用,
       (select nvl(sum(h.amount), 0)
          from prepayment_rcpt h
         where h.patient_id = s.patient_id
           and h.visit_id = s.visit_id) as 预交金额,
       (select nvl(sum(a.costs), 0)
          from inp_bill_detail a
         where a.patient_id = s.patient_id
           and a.visit_id = s.visit_id
           and a.rcpt_no is null) as 未清金额,
       (select nvl(sum(a.costs), 0)
          from inp_bill_detail a
         where a.patient_id = s.patient_id
           and a.visit_id = s.visit_id
           and a.rcpt_no is not null) as 已清金额,
       nvl(c.prepayments - c.total_charges, 0) as 余额,
       s.working_status as 医保卡号,
       t.next_of_kin as 联系人姓名,
       t.next_of_kin_addr || t.mailing_address as 联系人地址,
       t.next_of_kin_phone || t.phone_number_home as 联系人电话,
       s.insurance_type as 单位地址,
       t.phone_number_business as 单位电话
  from pat_master_index t, pat_visit s, pats_in_hospital c
 where t.patient_id = s.patient_id
   and t.patient_id = c.patient_id(+)
   and (s.charge_type in ({0}) or 'ALL' in ({0}))
   and (s.state = '{1}' or 'ALL' = '{1}')
   and (c.dept_code = '{2}' or 'ALL' = '{2}')
   and (s.admission_date_time >= to_date('{3} 00:00:00', 'yyyy-mm-dd hh24:mi:ss') or
       'ALL' = '{3}')
   and (s.admission_date_time <= to_date('{4} 23:59:59', 'yyyy-mm-dd hh24:mi:ss') or
       'ALL' = '{4}')
   and (s.discharge_date_time >= to_date('{5} 00:00:00', 'yyyy-mm-dd hh24:mi:ss') or
       'ALL' = '{5}')
   and (s.discharge_date_time <= to_date('{6} 23:59:59', 'yyyy-mm-dd hh24:mi:ss') or
       'ALL' = '{6}')
   and (s.balance_date >= to_date('{7} 00:00:00', 'yyyy-mm-dd hh24:mi:ss') or
       'ALL' = '{7}')
   and (s.balance_date <= to_date('{8} 23:59:59', 'yyyy-mm-dd hh24:mi:ss') or
       'ALL' = '{8}')";
                //if (baldatefrom == "ALL" && baldateto == "ALL")
                //    sql = string.Format(sql, charge_type, state, dept_stayed, indatefrom, indateto, outdatefrom, outdateto, baldatefrom, baldateto, "ALL");
                //else
                //sql = string.Format(sql, charge_type, state, dept_stayed, indatefrom, indateto, outdatefrom, outdateto, baldatefrom, baldateto, "3");
                sql = string.Format(sql, charge_type, state, dept_stayed, indatefrom, indateto, outdatefrom, outdateto, baldatefrom, baldateto);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }
        /// <summary>
        /// 患者基本信息条件
        /// </summary>
        /// <param name="charge_type"></param>
        /// <param name="state"></param>
        /// <param name="dept_stayed"></param>
        /// <param name="indate"></param>
        /// <param name="outdate"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public DataTable GetQueryPatientConditions2(string charge_type, string state, string dept_stayed, string indate, string outdate, ref string errmsg)
        {
            DataTable dt = new DataTable();
            //2013-5-22 by li 增加住院天数统计列
            //2014-4-25 by li 入院日期和出院日期由日期型修改为带时间类型
            try
            {
                string sql = @"select t.patient_id as 患者ID,
                                   s.visit_id as 序号,
                                   t.inp_no as 住院号,
                                    t.name as 姓名,
                                   t.sex as 性别,
                                   trunc(to_char(sysdate, 'yyyy')) -
                                   trunc(to_char(t.date_of_birth, 'yyyy')) as 年龄,
                                   t.id_no as 身份证号,
                                   s.charge_type as 费别,
                                   (trunc(s.discharge_date_time)-trunc(s.admission_date_time)) as 住院天数,
                                   (select e.dept_name from dept_dict e where e.dept_code = c.ward_code) as 护理单元,       
                                   (select e.dept_name
                                             from dept_dict e
                                            where e.dept_code = c.dept_code) as 科室,
                                   decode(s.state, 'I', c.bed_no, '') as 床号,
                                   to_char(s.admission_date_time, 'yyyy-mm-dd hh24:mi:ss') as 入院日期,
                                   to_char(s.discharge_date_time, 'yyyy-mm-dd hh24:mi:ss') as 出院日期,
                                   (select g.state_name from instate_dict g where g.state_id = s.state) as 在院状态,
                                   (select nvl(sum(a.costs), 0)
                                      from inp_bill_detail a
                                     where a.patient_id = s.patient_id
                                       and a.visit_id = s.visit_id) as 总费用,
                                   (select nvl(sum(h.amount),0)
                                      from prepayment_rcpt h
                                     where h.patient_id = s.patient_id
                                       and h.visit_id = s.visit_id) as 预交金额,
                                   (select nvl(sum(a.costs), 0)
                                      from inp_bill_detail a
                                     where a.patient_id = s.patient_id
                                       and a.visit_id = s.visit_id
                                       and a.rcpt_no is null) as 未清金额,
                                   (select nvl(sum(a.costs), 0)
                                      from inp_bill_detail a
                                     where a.patient_id = s.patient_id
                                       and a.visit_id = s.visit_id
                                       and a.rcpt_no is not null) as 已清金额,
                                   nvl(c.prepayments - c.total_charges,0) as 余额,
                                   s.working_status as 医保卡号,
                                   t.next_of_kin as 联系人姓名,
                                   t.next_of_kin_addr as 联系人地址,
                                   t.next_of_kin_phone as 联系人电话,
                                   s.insurance_type as 单位地址,
                                   t.phone_number_business as 单位电话
                              from pat_master_index t, pat_visit s, pats_in_hospital c
                             where t.patient_id = s.patient_id
                               and t.patient_id = c.patient_id(+)
                               and s.charge_type <> '{0}'
                               and (s.state = '{1}' or 'ALL' = '{1}')
                               and (c.dept_code = '{2}' or 'ALL' = '{2}')
                               and (s.admission_date_time >= to_date('{3}','yyyy-mm-dd') or 'ALL' = '{3}')
                               and (s.discharge_date_time >= to_date('{4}','yyyy-mm-dd') or 'ALL' = '{4}')";
                sql = string.Format(sql, charge_type, state, dept_stayed, indate, outdate);
                dt = BaseEntityer.Db.GetDataTable(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询患者的统筹和警戒线费用信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryPatientPubCostAndAlarmInfo()
        {
            string sql = @"
SELECT t.patient_id AS 患者id,
       t.visit_id AS 住院次数,
       (SELECT c.name
          FROM pat_master_index c
         WHERE c.patient_id = t.patient_id) AS 姓名,
       t.dept_admission_to AS 入院科室编码,
       (SELECT m.dept_name
          FROM dept_dict m
         WHERE m.dept_code = t.dept_admission_to) AS 入院科室,
       t.pub_cost AS 统筹金额,
       t.pub_alarm AS 最低警戒线,
       t.cost_alarm AS 警戒线,
       (SELECT r.diagnosis_desc
          FROM diagnosis r
         WHERE r.patient_id = t.patient_id
           AND r.visit_id = t.visit_id
           AND r.diagnosis_type = '2'
           AND rownum = 1) AS 入院诊断,
       t.service_system_indicator AS 费别编码,
       (SELECT n.charge_type_name
          FROM charge_type_dict n
         WHERE n.charge_type_code = t.service_system_indicator) AS 费别名称
  FROM pat_visit t
 WHERE t.state = 'I'";

            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 更新患者统筹金额和最低警戒线信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <param name="pubAlarm"></param>
        /// <param name="alarmCost"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <param name="alarmCosted"></param>
        /// <param name="pubCosted"></param>
        /// <param name="db">数据操作对象</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdatePatientPubCostAndAlarmInfo(string patientID, string visitID, string pubAlarm, string alarmCost, string operCode, DateTime operDate, string pubCosted, string alarmCosted, BaseEntityer db, ref string errMsg)
        {

            try
            {
                string sql = @"UPDATE pat_visit t
   SET t.cost_alarm = '{0}',
       t.pub_alarm  = '{1}'
 WHERE t.patient_id = '{2}'
   AND t.visit_id = '{3}'
";
                string sqlInsert = @"INSERT into PAT_VISIT_ALARMCOST_LOG     --患者最低警戒线金额日志
(
VISIT_ID,   --病人本次住院标识
PATIENTID,   --患者ID
OPERDATE,   --操作时间
OPERCODE,   --操作员编码
PUB_ALARMED,   --更新前最低警戒线金额
COST_ALARMED,   --更新前警戒线金额
PUB_ALARM,   --更新后最低警戒线金额
COST_ALARM,   --更新后警戒线金额
ID   --序号
) 
VALUES
(
'{0}',   --病人本次住院标识
'{1}',   --患者ID
TO_DATE('{2}','YYYY-MM-DD HH24:MI:SS'),   --操作时间
'{3}',   --操作员编码
'{4}',   --更新前最低警戒线金额
'{5}',   --更新前警戒线金额
'{6}',   --更新后最低警戒线金额
'{7}',   --更新后警戒线金额
lpad(ALARMCOST_LOG_SEQ.Nextval,10,0) --序号
) ";
                sql = string.Format(sql, alarmCost, pubAlarm, patientID, visitID);
                int rev = db.ExecuteNonQuery(sql);

                if (rev > 0)
                {
                    sqlInsert = string.Format(sqlInsert, visitID, patientID, operDate.ToString(), operCode, pubCosted, alarmCosted, pubAlarm, alarmCost);

                    int revInsert = db.ExecuteNonQuery(sqlInsert);

                    if (revInsert <= 0)
                    {
                        throw new Exception("插入更新日志失败！！！");
                    }
                }
                else
                    return -1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 患者状态变更

        /// <summary>
        /// 患者状态变更
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        public int PatientStateChange(HisCommon.DataEntity.INP_PAT_CHANGE change)
        {
            string strSQL = string.Empty;

            #region SQL

            strSQL = @" INSERT INTO INP_PAT_CHANGE t --住院患者状态变更
                          (t.PATIENT_ID, --住院号
                           t.VISIT_ID, --流水号
                           t.SERIAL_NO, --业务流水号
                           t.OPER_DATE, --操作日期
                           t.OPER_CODE, --操作员
                           t.NEW_STATE, --新状态(入院登记-R 无费退院 N 病房接诊-I 转科-C 出院登记-B 结算-O 结算召回-U 召回科内-Q)
                           t.OLD_STATE, --旧状态(入院登记-R 无费退院 N 病房接诊-I 转科-C 出院登记-B 结算-O 结算召回-U 召回科内-Q)
                           t.REMARK, --备注
                           t.EXTEND1, --扩展字段1
                           t.EXTEND2, --扩展字段2
                           t.EXTEND3 --扩展字段3
                           )
                        VALUES
                          ('{0}', --住院号
                           '{1}', --流水号
                           (select nvl(max(m.SERIAL_NO), 10000) + 1
                              from INP_PAT_CHANGE m
                             where m.patient_id = '{0}'
                               and m.visit_id = '{1}'), --业务流水号
                           sysdate, --操作日期
                           '{2}', --操作员
                           '{3}', --新状态(入院登记-R 无费退院 N 病房接诊-I 转科-C 出院登记-B 结算-O 结算召回-U 召回科内-Q)
                           '{4}', --旧状态(入院登记-R 无费退院 N 病房接诊-I 转科-C 出院登记-B 结算-O 结算召回-U 召回科内-Q)
                           '{5}', --备注
                           '{6}', --扩展字段1
                           '{7}', --扩展字段2
                           '{8}' --扩展字段3
                           ) ";

            #endregion

            string state = this.GetPatientState(change.Patient_id, change.Visit_id);
            if (string.IsNullOrEmpty(change.Old_state))
            {
                change.Old_state = state;
            }

            strSQL = string.Format(strSQL, change.Patient_id, change.Visit_id, change.Oper_code, change.New_state, change.Old_state, change.Remark, change.Extend1, change.Extend2, change.Extend3);

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 获取患者当前状态
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <returns></returns>
        public string GetPatientState(string patientID, string visitID)
        {
            string strSQL = string.Empty;

            #region SQL

            strSQL = @" select t.NEW_STATE
                          from inp_pat_change t
                         where t.patient_id = '{0}'
                           and t.visit_id = '{1}'
                           and rownum = 1
                         order by t.serial_no desc ";

            #endregion

            strSQL = string.Format(strSQL, patientID, visitID);

            DbDataReader dbr = BaseEntityer.Db.ZDExecReader(strSQL);

            string state = string.Empty;
            while (dbr.Read())
            {
                state = dbr[0].ToString();
            }
            if (!dbr.IsClosed)
                dbr.Close();
            return state;
        }

        /// <summary>
        /// 获取是否第一次出院登记
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        /// <returns></returns>
        public string GetState(string patientID, string visitID)
        {
            string strSQL = string.Empty;
            string state = "STAT";

            strSQL = @" select 1 from inp_pat_change t where t.patient_id = '{0}' and t.visit_id = '{1}' and t.new_state = 'B' and t.old_state = 'I' ";
            strSQL = string.Format(strSQL, patientID, visitID);

            DbDataReader dbr = BaseEntityer.Db.ZDExecReader(strSQL);
            while (dbr.Read())
            {
                state = string.Empty;
            }
            if (!dbr.IsClosed)
                dbr.Close();
            return state;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visitID"></param>
        public void UpdateState(string patientID, string visitID)
        {
            string strSQL = string.Empty;

            strSQL = @" update inp_pat_change t set t.extend3 = '' where t.patient_id = '{0}' and t.visit_id = '{1}' and t.new_state = 'B' and t.old_state = 'I' ";
            strSQL = string.Format(strSQL, patientID, visitID);

            BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        #endregion

        #region 患者欠费额度 by yan_x {3AAE729F-F385-45D8-B42C-0B37DA67B5F2}

        /// <summary>
        /// 插入患者欠费额度信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="owe_lines_setting"></param>
        /// <returns></returns>
        public int InsertPatOwe(BaseEntityer db, HisCommon.DataEntity.OWE_LINES_SETTING obj)
        {
            string strSQL = string.Empty;

            //string sqlRecord_no = @"select SEQ_OWE_LINES_SETTING.Nextval  from dual ";
            string sqlRecord_no1 = HisDBLayer.Common.GetSequence("SEQ_OWE_LINES_SETTING");
            string sql = @"INSERT into OWE_LINES_SETTING  
                        (
                        RECORD_NO,
                        PATIENT_ID,
                        VISIT_ID,
                        OWN_COST,
                        INVALID,
                        OPER_CODE,
                        OPER_DATE,
                        PATIENT_NAME,
                        SECURE_CODE,
                        SECURE_NAME,
                        SECURE_CONTACT,
                        SECURE_TYPE_CODE,
                        SECURE_TYPE
                        ) 
                        VALUES
                        (
                        '{0}',
                        '{1}',
                        '{2}',
                        '{3}',
                        '{4}',
                        '{5}',
                        TO_DATE('{6}','YYYY-MM-DD HH24:MI:SS'),
                        '{7}',
                        '{8}',
                        '{9}',
                        '{10}',
                        '{11}',
                        '{12}'
                        ) ";

            object[] param = new object[] 
            { sqlRecord_no1.PadLeft(10, '0'), 
              obj.PATIENT_ID, 
              obj.VISIT_ID, 
              obj.OWN_COST, 
              obj.INVALID, 
              obj.OPER_CODE, 
              obj.OPER_DATE, 
              obj.PATIENT_NAME,
              obj.SECURE_CODE,
              obj.SECURE_NAME,
              obj.SECURE_CONTACT,
              obj.SECURE_TYPE_CODE,
              obj.SECURE_TYPE};

            sql = string.Format(sql, param);
            //sql = string.Format(sql, sqlRecord_no1.PadLeft(10, '0'), obj.PATIENT_ID,obj.VISIT_ID.ToString(),obj.OWN_COST.ToString(),obj.INVALID, obj.OPER_CODE,obj.OPER_DATE.ToString(), obj.PATIENT_NAME);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 通过流水号更新患者欠费额度信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="owe_lines_setting"></param>
        /// <returns></returns>
        public int UpdatePatOwe(BaseEntityer db, HisCommon.DataEntity.OWE_LINES_SETTING obj)
        {
            string sql = @"UPDATE OWE_LINES_SETTING  t   --
                            SET                          
                            t.OWN_COST='{1}',   --担保欠费金额                           
                            t.OPER_CODE='{2}',   --操作员
                            t.OPER_DATE=TO_DATE('{3}','YYYY-MM-DD HH24:MI:SS'),
                            t.SECURE_CODE='{4}',
                            t.SECURE_NAME='{5}',
                            t.SECURE_CONTACT='{6}',
                            t.PATIENT_ID='{7}'
                            WHERE t.RECORD_NO='{0}'   --记录流水号
                            ";
            object[] param = new object[] 
            { obj.RECORD_NO, 
              obj.OWN_COST, 
              obj.OPER_CODE, 
              obj.OPER_DATE,
              obj.SECURE_CODE,
              obj.SECURE_NAME,
              obj.SECURE_CONTACT,
              obj.PATIENT_ID};

            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);

        }

        /// <summary>
        /// 通过流水号作废患者欠费额度信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="owe_lines_setting"></param>
        /// <returns></returns>
        public int CancelPatOwe(BaseEntityer db, HisCommon.DataEntity.OWE_LINES_SETTING obj)
        {
            string sql = @"UPDATE OWE_LINES_SETTING  t   --
                            SET
                            t.INVALID='{1}',   --有效性（0 无效, 1 有效）
                            t.OPER_CODE='{2}',   --操作员
                            t.OPER_DATE=TO_DATE('{3}','YYYY-MM-DD HH24:MI:SS'),   --操作时间
                            t.PATIENT_ID='{4}'
                            WHERE t.RECORD_NO='{0}' and t.INVALID='1'  --记录流水号
                           ";
            object[] param = new object[] 
            { obj.RECORD_NO,
              obj.INVALID,
              obj.OPER_CODE, 
              obj.OPER_DATE,
              obj.PATIENT_ID};

            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 通过病人ID查询患者欠费额度信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<OWE_LINES_SETTING> QueryPatientInfoByID(string ID, ref string errmsg)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = @"select a.record_no,
                                a.patient_name,
                                a.own_cost,
                                a.invalid,
                                a.oper_code,
                                a.oper_date,
                                a.SECURE_TYPE,
                                a.SECURE_NAME,
                                a.SECURE_CONTACT  
                            from owe_lines_setting a
                            where a.patient_id = '{0}'
                            order by a.oper_date desc";
                sql = string.Format(sql, ID);
                ds = BaseEntityer.Db.GetDataSet(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return DataSetToEntity.DataSetToT<OWE_LINES_SETTING>(ds).ToList();
        }

        /// <summary>
        /// 通过流水号查询欠费担保人和担保人联系方式
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<OWE_LINES_SETTING> QueryPatientDBInfoByRecordID(string ID, ref string errmsg)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = @"select a.SECURE_code,
                                   a.SECURE_name,
                                   a.SECURE_contact,
                                   a.SECURE_type_code,
                                   a.SECURE_type
                              from owe_lines_setting a
                             where a.record_no = '{0}'";
                sql = string.Format(sql, ID);
                ds = BaseEntityer.Db.GetDataSet(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return DataSetToEntity.DataSetToT<OWE_LINES_SETTING>(ds).ToList();
        }


        /// <summary>
        /// 过滤患者欠费额度信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="valid"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public List<OWE_LINES_SETTING> QueryPatientInfoByCondition(string patient_id, string valid, string range, ref string errmsg)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = @"select a.record_no,
                                a.patient_name,
                                a.own_cost,
                                a.invalid,
                                a.oper_code,
                                a.oper_date
                            from owe_lines_setting a
                            where (a.invalid = '{1}' or 'ALL'= '{1}') and (a.oper_code = '{2}' or 'ALL'='{2}')
                                and a.patient_id={0}";
                sql = string.Format(sql, patient_id, valid, range);
                ds = BaseEntityer.Db.GetDataSet(sql);
            }
            catch (Exception e)
            {
                errmsg = e.Message;
            }
            return DataSetToEntity.DataSetToT<OWE_LINES_SETTING>(ds).ToList();
        }

        /// <summary>
        /// 通过患者ID更新患者欠费额度信息存入pat_visit表中的assure_cost字段
        /// </summary>
        /// <param name="db"></param>
        /// <param name="owe_lines_setting"></param>
        /// <returns></returns>
        public int UpdatePatVisit(BaseEntityer db, HisCommon.DataEntity.OWE_LINES_SETTING obj)
        {
            string sqlUpdate = @"update pat_visit f
                                    set f.assure_costs =
                                     (select sum(a.own_cost)
                                       from owe_lines_setting a
                                          where a.invalid = '1'
                                          and a.patient_id = '{0}')
                                 where f.patient_id = '{0}'";

            sqlUpdate = string.Format(sqlUpdate, obj.PATIENT_ID);
            return db.ExecuteNonQuery(sqlUpdate);
        }

        #endregion
    }
}
