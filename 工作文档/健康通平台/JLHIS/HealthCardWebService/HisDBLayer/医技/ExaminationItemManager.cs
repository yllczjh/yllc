using System.Collections.Generic;
using System.Linq;
using System.Data;
using HisCommon.DataEntity;
using HisCommon;
using System;
using System.Data.Common;
using HisCommon.Enum;

namespace HisDBLayer
{
    /// <summary>
    /// [功能描述: 医技管理数据库查询]<br></br>
    /// [创 建 者: 李琳]<br></br>
    /// [创建时间: 2012-10-22]<br></br>
    /// <修改>
    ///		<修改人></修改人>
    ///		<修改时间></修改时间>
    ///		<修改说明></修改说明>
    /// </修改>
    /// </summary>
    public class ExaminationItemManager
    {
        #region 检查项目维护功能

        /// <summary>
        /// 查询所有检查项目列表
        /// </summary>
        /// <returns>检查项目列表</returns>
        public List<HisCommon.DataEntity.CLINIC_ITEM_DICT> GetExaminationItem()
        {
            string sql = @"SELECT * 
                        FROM clinic_item_dict   
                        where item_class = 'D' or item_class = 'E'";
            sql = string.Format(sql);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.CLINIC_ITEM_DICT>(ds).ToList();
        }

        /// <summary>
        /// 根据拼音码检索包含对应拼音码的检查项目列表
        /// </summary>
        /// <param name="pinYinCode">输入的拼音码</param>
        /// <returns>检查项目列表</returns>
        public List<HisCommon.DataEntity.CLINIC_ITEM_DICT> GetExaminationItemByPinYinCode(string pinYinCode)
        {
            string sql = @"SELECT * 
                        FROM clinic_item_dict   
                        where (item_class = 'D' or item_class = 'E')
                        and INPUT_CODE like '%{0}%'";
            sql = string.Format(sql, pinYinCode);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.CLINIC_ITEM_DICT>(ds).ToList();
        }

        /// <summary>
        /// 根据项目名称检索包含对应检查项目名称字符的检查项目列表
        /// </summary>
        /// <param name="itemName">检查项目名称</param>
        /// <returns>检查项目列表</returns>
        public List<HisCommon.DataEntity.CLINIC_ITEM_DICT> GetExaminationItemByItemName(string itemName)
        {
            string sql = @"SELECT *
                        FROM clinic_item_dict   
                        where (item_class = 'D' or item_class = 'E')
                        and ITEM_NAME like '%{0}%'";
            sql = string.Format(sql, itemName);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.CLINIC_ITEM_DICT>(ds).ToList();
        }

        /// <summary>
        /// 根据检查类别查询当前科室的检查子类列表
        /// </summary>
        /// <param name="examClass">检查类别</param>
        /// <returns>当前科室的检查子类列表</returns>
        public List<HisCommon.DataEntity.EXAM_SUBCLASS_DICT> GetExaminationSubClass(string examClass)
        {
            string sql = @"select * from EXAM_SUBCLASS_DICT where EXAM_SUBCLASS_DICT.EXAM_CLASS_NAME = '{0}' 
                        order by EXAM_SUBCLASS_DICT.SERIAL_NO";
            sql = string.Format(sql, examClass);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXAM_SUBCLASS_DICT>(ds).ToList();
        }

        /// <summary>
        /// 根据检查类别以及检查子类查询当前科室的检查项目列表
        /// </summary>
        /// <param name="examClass">检查类别</param>
        /// <param name="examSubClass">检查子类</param>
        /// <param name="descItem">可见级别</param>
        /// <returns>当前科室的检查子类的检查项目列表</returns>
        public List<HisCommon.DataEntity.EXAM_RPT_PATTERN> GetExaminationItemInExamClass(string examClass, string examSubClass, string descItem)
        {
            string sql = @"SELECT  exam_rpt_pattern.exam_class ,
                       exam_rpt_pattern.exam_sub_class ,
                       exam_rpt_pattern.desc_item ,
                       exam_rpt_pattern.desc_name ,
                       exam_rpt_pattern.description ,
                       exam_rpt_pattern.description_code ,
                       exam_rpt_pattern.input_code     
                    FROM exam_rpt_pattern where (EXAM_RPT_PATTERN.EXAM_CLASS = '{0}')      
                    and (EXAM_RPT_PATTERN.EXAM_SUB_CLASS = '{1}')      
                    and (EXAM_RPT_PATTERN.DESC_ITEM = '{2}')";
            sql = string.Format(sql, examClass, examSubClass, descItem);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXAM_RPT_PATTERN>(ds).ToList();
        }

        /// <summary>
        /// 保存检查项目
        /// </summary>
        /// <param name="listExamGroup">要保存的检查组项目列表</param>
        /// <returns></returns>
        public int SaveExaminationItem(HisCommon.DataEntity.EXAM_RPT_PATTERN examItem, BaseEntityer db)
        {
            string sql = @"insert into EXAM_RPT_PATTERN
                       (
                        EXAM_RPT_PATTERN.exam_class ,
                       EXAM_RPT_PATTERN.exam_sub_class ,
                       EXAM_RPT_PATTERN.desc_item ,
                       EXAM_RPT_PATTERN.desc_name ,
                       EXAM_RPT_PATTERN.description ,
                       EXAM_RPT_PATTERN.description_code ,
                       EXAM_RPT_PATTERN.input_code     
                       )
                      values
                     (
                       '{0}',
                       '{1}',
                        '{2}',
                        '{3}',
                        '{4}',
                        '{5}',
                        '{6}')";
            object[] param = new object[] { examItem.EXAM_CLASS, examItem.EXAM_SUB_CLASS, examItem.DESC_ITEM, 
                examItem.DESC_NAME, examItem.DESCRIPTION, examItem.DESCRIPTION_CODE, examItem.INPUT_CODE };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除所有满足条件的检查项目，为重新进行数据保存做准备
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DelExaminationItem(BaseEntityer db, string examClass, string examSubClass, string descItem)
        {
            string sql = @"delete from EXAM_RPT_PATTERN where (EXAM_RPT_PATTERN.EXAM_CLASS = '{0}')      
                    and (EXAM_RPT_PATTERN.EXAM_SUB_CLASS = '{1}')      
                    and (EXAM_RPT_PATTERN.DESC_ITEM = '{2}')";
            sql = string.Format(sql, examClass, examSubClass, descItem);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据价表项目代码查询对应项目的价格信息
        /// </summary>
        /// <param name="itemCode">对应价表项目代码</param>
        /// <returns>对应价表项目价格列表</returns>
        public List<HisCommon.DataEntity.EXAM_ITEM_PRICE> GetExaminationItemPriceByItemCode(string itemCode)
        {
            string sql = @"SELECT CLINIC_VS_CHARGE.CLINIC_ITEM_CODE,   
                             current_price_list.ITEM_CODE,   
                             current_price_list.ITEM_NAME,   
                             current_price_list.ITEM_SPEC,   
                             current_price_list.UNITS,   
                             current_price_list.PRICE,  
                             current_price_list.ITEM_CLASS,
                             current_price_list.PERFORMED_BY,  
                             CLINIC_VS_CHARGE.AMOUNT  
                        FROM CLINIC_VS_CHARGE,   
                             current_price_list
                       WHERE ( CLINIC_VS_CHARGE.CHARGE_ITEM_CLASS = current_price_list.ITEM_CLASS ) and  
                             ( CLINIC_VS_CHARGE.CHARGE_ITEM_CODE = current_price_list.ITEM_CODE ) and  
                             ( CLINIC_VS_CHARGE.CHARGE_ITEM_SPEC = current_price_list.ITEM_SPEC ) and  
                             
                             ((CLINIC_VS_CHARGE.CLINIC_ITEM_CLASS = 'D' ) or
                             ( CLINIC_VS_CHARGE.CLINIC_ITEM_CLASS = 'E' ) )AND  
                             ( CLINIC_VS_CHARGE.CLINIC_ITEM_CODE = '{0}' )";
            sql = string.Format(sql, itemCode);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXAM_ITEM_PRICE>(ds).ToList();
        }

        #endregion

        #region 检查组维护功能

        /// <summary>
        /// 获取所有检查组项目
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.EXAM_GROUP_DICT> GetExaminationGroup()
        {
            string sql = @"SELECT EXAM_GROUP_DICT.EXAM_GROUP,   
                         EXAM_GROUP_DICT.EXAM_CLASS  
                         FROM EXAM_GROUP_DICT  
                         ORDER BY EXAM_GROUP_DICT.EXAM_CLASS ASC,   
                         EXAM_GROUP_DICT.EXAM_GROUP ASC";
            sql = string.Format(sql);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXAM_GROUP_DICT>(ds).ToList();
        }

        /// <summary>
        /// 获取所有检查类别项目
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.EXAM_CLASS_DICT> GetExaminationClass()
        {
            string sql = @"SELECT EXAM_CLASS_DICT.Serial_No,EXAM_CLASS_DICT.Exam_Class_Code,
                        EXAM_CLASS_DICT.EXAM_CLASS_NAME,EXAM_CLASS_DICT.INPUT_CODE 
                        from EXAM_CLASS_DICT ORDER BY EXAM_CLASS_DICT.Serial_No ASC";
            sql = string.Format(sql);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXAM_CLASS_DICT>(ds).ToList();
        }

        /// <summary>
        /// 保存检查组项目
        /// </summary>
        /// <param name="listExamGroup">要保存的检查组项目列表</param>
        /// <returns></returns>
        public int SaveExaminationGroup(HisCommon.DataEntity.EXAM_GROUP_DICT examGroup, BaseEntityer db)
        {
            string sql = @"insert into EXAM_GROUP_DICT
                       (
                        EXAM_GROUP,
                        EXAM_CLASS
                       )
                      values
                     (
                       '{0}',
                       '{1}')";
            object[] param = new object[] { examGroup.EXAM_GROUP, examGroup.EXAM_CLASS };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除所有检查项目组列表，为重新进行数据保存做准备
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DelExaminationGroup(BaseEntityer db)
        {
            string sql = @"delete from EXAM_GROUP_DICT";
            sql = string.Format(sql);
            return db.ExecuteNonQuery(sql);
        }

        #endregion

        #region 检查报告模版维护功能

        /// <summary>
        /// 更新检查报告模版
        /// </summary>
        /// <param name="examItem">检查报告项目</param>
        /// <param name="db">db事务</param>
        /// <returns></returns>
        public int UpdateExaminationItem(HisCommon.DataEntity.EXAM_RPT_PATTERN examItem, HisCommon.DataEntity.EXAM_RPT_PATTERN oldExamItem, BaseEntityer db)
        {
            string sql = @"update EXAM_RPT_PATTERN
                       SET
                        EXAM_RPT_PATTERN.EXAM_CLASS = '{0}',
                       EXAM_RPT_PATTERN.EXAM_SUB_CLASS = '{1}',
                       EXAM_RPT_PATTERN.DESC_ITEM = '{2}',
                       EXAM_RPT_PATTERN.DESC_NAME = '{3}',
                       EXAM_RPT_PATTERN.description = '{4}',
                       EXAM_RPT_PATTERN.description_code = '{5}',
                       EXAM_RPT_PATTERN.input_code = '{6}' 
                       where (EXAM_RPT_PATTERN.EXAM_CLASS = '{7}')      
                    and (EXAM_RPT_PATTERN.EXAM_SUB_CLASS = '{8}')      
                    and (EXAM_RPT_PATTERN.DESC_ITEM = '{9}')
                    and (EXAM_RPT_PATTERN.DESC_NAME = '{10}')";
            object[] param = new object[] { examItem.EXAM_CLASS, examItem.EXAM_SUB_CLASS, examItem.DESC_ITEM, 
                examItem.DESC_NAME, examItem.DESCRIPTION, examItem.DESCRIPTION_CODE, examItem.INPUT_CODE,
                oldExamItem.EXAM_CLASS, oldExamItem.EXAM_SUB_CLASS, oldExamItem.DESC_ITEM, oldExamItem.DESC_NAME};
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除检查报告模版项目
        /// </summary>
        /// <param name="db">db事务</param>
        /// <param name="examClass">检查类别</param>
        /// <param name="examSubClass">检查子类</param>
        /// <param name="descItem">描述项目</param>
        /// <param name="descName">描述名称</param>
        /// <returns></returns>
        public int DelExaminationItem(BaseEntityer db, string examClass, string examSubClass, string descItem, string descName)
        {
            string sql = @"delete from EXAM_RPT_PATTERN where (EXAM_RPT_PATTERN.EXAM_CLASS = '{0}')      
                    and (EXAM_RPT_PATTERN.EXAM_SUB_CLASS = '{1}')      
                    and (EXAM_RPT_PATTERN.DESC_ITEM = '{2}')
                    and (EXAM_RPT_PATTERN.DESC_NAME = '{3}')";
            sql = string.Format(sql, examClass, examSubClass, descItem, descName);
            return db.ExecuteNonQuery(sql);
        }

        #endregion

        #region 统计分析

        /// <summary>
        /// 查询病人来源项目列表
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.PATIENT_SOURCE_DICT> GetPatientSource()
        {
            string sql = @"SELECT  patient_source_dict.serial_no ,
                           patient_source_dict.patient_source_code ,
                           patient_source_dict.patient_source_name ,
                           patient_source_dict.input_code     
                        FROM patient_source_dict";
            sql = string.Format(sql);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PATIENT_SOURCE_DICT>(ds).ToList();
        }

        /// <summary>
        /// 根据科室临床属性名称获取部门列表，如【辅诊】
        /// </summary>
        /// <param name="clinicAttrName">科室临床属性名称，如【辅诊】</param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.DEPT_DICT> getDeptApplication(string clinicAttrName)
        {
            string sql = @"  SELECT DEPT_DICT.SERIAL_NO, 
                             DEPT_DICT.DEPT_CODE, 
                             DEPT_DICT.DEPT_NAME, 	 
                             DEPT_DICT.DEPT_ALIAS, 
                             DEPT_DICT.CLINIC_ATTR, 
                             DEPT_DICT.OUTP_OR_INP, 
                             DEPT_DICT.INTERNAL_OR_SERGERY,
                             a.clinic_attr_name
                             FROM DEPT_DICT left join DEPT_CLINIC_ATTR_DICT a 
                             on DEPT_DICT.Clinic_Attr = a.clinic_attr_code 
                             where a.clinic_attr_name = '{0}' or '{0}' is null
                             ORDER BY DEPT_DICT.DEPT_CODE";
            sql = string.Format(sql, clinicAttrName);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DEPT_DICT>(ds).ToList();
        }

        /// <summary>
        /// 检查申请查询
        /// </summary>
        /// <param name="dateBegin">申请时间起始位置</param>
        /// <param name="dateEnd">申请时间终止位置</param>
        /// <param name="patientFrom">病人来源</param>
        /// <param name="deptApplication">申请科室</param>
        /// <param name="doctorName">申请医生</param>
        /// <param name="deptCode">查询人员科室代码</param>
        /// <returns>检查申请查询列表</returns>
        public DataTable ExaminationApplicationQuery(string dateBegin, string dateEnd, string patientFrom, string deptApplication, string doctorName, string deptCode)
        {
            string sql = @"SELECT  exam_appoints.exam_no ,
                       exam_appoints.name ,
                       exam_appoints.sex ,
                       exam_appoints.exam_sub_class ,
                       b.EXAM_ITEM as device ,
                       exam_appoints.req_date_time ,
                       d.patient_source_name ,
                       exam_appoints.patient_id ,
                       c.dept_name ,
                       exam_appoints.req_physician ,
                       exam_appoints.notice 
                    FROM exam_appoints left join patient_source_dict d on exam_appoints.patient_source = d.patient_source_code
                    left join EXAM_ITEMS b on exam_appoints.exam_no = b.EXAM_NO
                    left join DEPT_DICT c on exam_appoints.req_dept = c.dept_code 
                    where trunc(req_date_time) >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                    and trunc(req_date_time) <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                    and performed_by = '{5}'";
            if (patientFrom != string.Empty)
            {
                if (patientFrom.Equals("1"))
                    sql += " and (exam_appoints.patient_source = '{2}' or patient_source is null)";
                else
                    sql += " and (exam_appoints.patient_source = '{2}')";
            }
            if (deptApplication != string.Empty)
            {
                sql += " and (exam_appoints.req_dept = '{3}')";
            }
            if (doctorName != string.Empty)
            {
                sql += " and (exam_appoints.req_physician like '%{4}%')";
            }
            sql += " order by exam_no";
            sql = string.Format(sql, dateBegin, dateEnd, patientFrom, deptApplication, doctorName, deptCode);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 检查确认查询
        /// </summary>
        /// <param name="dateBegin">检查时间起始位置</param>
        /// <param name="dateEnd">检查时间终止位置</param>
        /// <param name="patientFrom">病人来源</param>
        /// <param name="deptApplication">申请科室</param>
        /// <param name="doctorName">申请医生</param>
        /// <param name="deptCode">查询人员科室代码</param>
        /// <returns>检查确认查询列表</returns>
        public DataTable ExaminationConfirmQuery(string dateBegin, string dateEnd, string patientFrom, string deptApplication, string doctorName, string deptCode)
        {
            string sql = @"SELECT exam_master.exam_no ,
                           exam_master.patient_id ,
                           exam_master.visit_id ,
                           exam_master.name ,
                           exam_master.sex ,
                           exam_master.date_of_birth ,
                           exam_master.exam_class ,
                           exam_master.exam_sub_class ,
                           exam_master.exam_date_time ,
                           exam_master.costs ,
                           exam_master.charges ,
                           exam_master.clin_symp ,
                           a.exam_result_status_name ,
                           d.patient_source_name ,
                           c.dept_name ,
                           exam_master.req_physician ,
                           exam_master.phys_sign ,
                           exam_master.relevant_lab_test ,
                           exam_master.relevant_diag ,
                           exam_master.clin_diag ,
                           exam_master.notice     
                        FROM exam_master left join patient_source_dict d on exam_master.patient_source = d.patient_source_code
                        left join DEPT_DICT c on exam_master.req_dept = c.dept_code 
                        left join EXAM_RESULT_STATUS_DICT a on exam_master.result_status = a.exam_result_status_code
                        where trunc(exam_date_time) >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                        and trunc(exam_date_time) <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                        and performed_by = '{5}'";
            if (patientFrom != string.Empty)
            {
                if (patientFrom.Equals("1"))
                    sql += " and (exam_master.patient_source = '{2}' or patient_source is null)";
                else
                    sql += " and (exam_master.patient_source = '{2}')";
            }
            if (deptApplication != string.Empty)
            {
                sql += " and (exam_master.req_dept = '{3}')";
            }
            if (doctorName != string.Empty)
            {
                sql += " and (exam_master.req_physician like '%{4}%')";
            }
            sql += " order by exam_master.exam_no";
            sql = string.Format(sql, dateBegin, dateEnd, patientFrom, deptApplication, doctorName, deptCode);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 检查报告查询
        /// </summary>
        /// <param name="dateBegin">报告时间起始位置</param>
        /// <param name="dateEnd">报告时间终止位置</param>
        /// <param name="patientFrom">病人来源</param>
        /// <param name="deptApplication">申请科室</param>
        /// <param name="doctorName">申请医生</param>
        /// <param name="deptCode">查询人员科室代码</param>
        /// <returns>检查报告查询列表</returns>
        public DataTable ExaminationReportQuery(string dateBegin, string dateEnd, string patientFrom, string deptApplication, string doctorName, string deptCode)
        {
            string sql = @"SELECT  exam_master.exam_no ,
                           exam_master.patient_id ,
                           exam_master.visit_id ,
                           exam_master.name ,
                           exam_master.sex ,
                           exam_master.date_of_birth ,
                           exam_master.exam_class ,
                           exam_master.exam_sub_class ,
                           exam_master.report_date_time ,
                           exam_master.exam_date_time ,
                           exam_master.costs ,
                           exam_master.charges ,
                           exam_master.clin_symp ,
                           a.exam_result_status_name ,
                           e.is_abnormal ,
                           e.memo ,
                           d.patient_source_name ,
                           c.dept_name ,
                           exam_master.req_physician     
                        FROM exam_master inner join exam_report e on exam_master.exam_no = e.exam_no
                        left join patient_source_dict d on exam_master.patient_source = d.patient_source_code
                        left join EXAM_ITEMS b on exam_master.exam_no = b.EXAM_NO
                        left join DEPT_DICT c on exam_master.req_dept = c.dept_code 
                        left join EXAM_RESULT_STATUS_DICT a on exam_master.result_status = a.exam_result_status_code
                        where trunc(REPORT_DATE_TIME) >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')
                        and trunc(REPORT_DATE_TIME) <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                        and performed_by = '{5}'";
            if (patientFrom != string.Empty)
            {
                if (patientFrom.Equals("1"))
                    sql += " and (exam_master.patient_source = '{2}' or patient_source is null)";
                else
                    sql += " and (exam_master.patient_source = '{2}')";
            }
            if (deptApplication != string.Empty)
            {
                sql += " and (exam_master.req_dept = '{3}')";
            }
            if (doctorName != string.Empty)
            {
                sql += " and (exam_master.req_physician like '%{4}%')";
            }
            sql += " order by exam_master.exam_no";
            sql = string.Format(sql, dateBegin, dateEnd, patientFrom, deptApplication, doctorName, deptCode);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 病房检查费用查询
        /// </summary>
        /// <param name="dateBegin">计价时间起始位置</param>
        /// <param name="dateEnd">计价时间终止位置</param>
        /// <param name="deptCode">查询人员科室代码</param>
        /// <param name="patientName">病人住院号</param>
        /// <returns>病房检查费用查询列表</returns>
        public DataTable ExaminationFeeQuery(string dateBegin, string dateEnd, string deptCode, string patientName)
        {
            //2014-4-10 by li 修正数据查询基础表连接错误
            string sql = @"SELECT inp_bill_detail.patient_id,
       (SELECT t.name
          FROM pat_master_index t
         WHERE t.patient_id = inp_bill_detail.patient_id) AS NAME,
       --inp_bill_detail.visit_id ,
       --inp_bill_detail.item_no ,
       --inp_bill_detail.item_class ,
       inp_bill_detail.item_name,
       --inp_bill_detail.item_code ,
       --inp_bill_detail.item_spec ,
       inp_bill_detail.amount,
       --inp_bill_detail.units ,
       --inp_bill_detail.ordered_by ,
       --inp_bill_detail.performed_by ,
       inp_bill_detail.costs,
       inp_bill_detail.charges,
       inp_bill_detail.billing_date_time,
       --inp_bill_detail.operator_no 
       --inp_bill_detail.rcpt_no ,
       --inp_bill_detail.up_flag ,
       --inp_bill_detail.up_time_date ,
       --inp_bill_detail.up_operator_no ,
       --inp_bill_detail.formularyno 
       fun_getdeptname(inp_bill_detail.ordered_by) AS dept_name,
       fun_getusername(inp_bill_detail.doctor) AS doctor,
       fun_getusername(inp_bill_detail.operator_no) AS user_name,
       f_trans_pinyin_capital((SELECT t.name
          FROM pat_master_index t
         WHERE t.patient_id = inp_bill_detail.patient_id)) AS nameSpell,
       (SELECT t.inp_no
          FROM pat_master_index t
         WHERE t.patient_id = inp_bill_detail.patient_id) AS inp_no,
         f_trans_pinyin_capital(fun_getdeptname(inp_bill_detail.ordered_by)) as deptSpell,
         f_trans_pinyin_capital(fun_getusername(inp_bill_detail.doctor) ) as doctSpell
  FROM inp_bill_detail
                         WHERE (INP_BILL_DETAIL.PERFORMED_BY = '{2}')
                           and (trunc(INP_BILL_DETAIL.BILLING_DATE_TIME) >=
                               to_date('{0}', 'yyyy-MM-dd hh24:mi:ss'))
                           and (trunc(INP_BILL_DETAIL.BILLING_DATE_TIME) <
                               to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')) ";
            if (patientName != null)
            {
                sql += " and INP_BILL_DETAIL.PATIENT_ID = '{3}' ";
            }
            sql += " order by inp_bill_detail.billing_date_time desc";
            sql = string.Format(sql, dateBegin, dateEnd, deptCode, patientName);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 门诊患者费用查询
        /// </summary>
        /// <param name="dateBegin">就诊时间起始位置</param>
        /// <param name="dateEnd">就诊时间终止位置</param>
        /// <param name="deptCode">查询人员科室代码</param>
        /// <param name="patientName">病人姓名</param>
        /// <returns>门诊患者费用查询列表</returns>
        public DataTable OutpatientFeeQuery(string dateBegin, string dateEnd, string deptCode, string patientName)
        {
            //2013-12-20 by li costs--》charge 医技科室费用统计按打折后统计
            string sql = @"SELECT OUTP_BILL_ITEMS.VISIT_DATE as 就诊时间,
                               OUTP_BILL_ITEMS.VISIT_NO   as 就诊序号,
                               OUTP_RCPT_MASTER.NAME      as 姓名,
                               --OUTP_BILL_ITEMS.ITEM_NO,   
                               --OUTP_BILL_ITEMS.ITEM_CLASS,   
                               --OUTP_BILL_ITEMS.CLASS_ON_RCPT,   
                               OUTP_BILL_ITEMS.ITEM_CODE,
                               OUTP_BILL_ITEMS.ITEM_NAME as 项目名称,
                               --OUTP_BILL_ITEMS.ITEM_SPEC,   
                               OUTP_BILL_ITEMS.AMOUNT as 数量,
                               --OUTP_BILL_ITEMS.UNITS,   
                               --OUTP_BILL_ITEMS.PERFORMED_BY,   
                               OUTP_BILL_ITEMS.Charges as 费用,
                               --OUTP_BILL_ITEMS.CHARGES,   
                               --OUTP_BILL_ITEMS.SERIAL_NO,   
                               --OUTP_BILL_ITEMS.BATCHNO,   
                               --OUTP_BILL_ITEMS.D_ITEM_NO,   
                               --OUTP_BILL_ITEMS.APPOINT_NO,
                               --OUTP_BILL_ITEMS.ORDER_DOCTOR,
                               users_staff_dict.user_name as 开单医生,
                               D.DEPT_NAME                AS 开单科室,
                               OUTP_BILL_ITEMS.RCPT_NO    as 收据号
                          FROM OUTP_BILL_ITEMS
                          LEFT JOIN DEPT_DICT D
                            ON OUTP_BILL_ITEMS.ORDER_DEPT = D.DEPT_CODE
                          LEFT JOIN OUTP_RCPT_MASTER
                            ON OUTP_BILL_ITEMS.RCPT_NO = OUTP_RCPT_MASTER.RCPT_NO
                          LEFT JOIN users_staff_dict
                            ON OUTP_BILL_ITEMS.ORDER_DOCTOR = users_staff_dict.user_id
                         WHERE ((OUTP_BILL_ITEMS.PERFORMED_BY = '{2}') AND
                               (OUTP_RCPT_MASTER.VISIT_DATE >=
                               to_date('{0}', 'yyyy-MM-dd hh24:mi:ss')) AND
                               (OUTP_RCPT_MASTER.VISIT_DATE <
                               to_date('{1}', 'yyyy-MM-dd hh24:mi:ss'))) ";
            if (patientName != null)
            {
                sql += " and OUTP_RCPT_MASTER.NAME = '{3}' ";
            }
            sql += " order by OUTP_BILL_ITEMS.VISIT_DATE desc";
            sql = string.Format(sql, dateBegin, dateEnd, deptCode, patientName);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        #endregion

        #region 检查管理

        /// <summary>
        /// 检查确认中未确认查询
        /// </summary>
        /// <param name="dateBegin">申请时间起始位置</param>
        /// <param name="dateEnd">申请时间终止位置</param>
        /// <param name="deptCode">查询人员科室代码</param>
        /// <param name="examClass">检查类别</param>
        /// <param name="name">查询病人姓名</param>
        /// <param name="patientID">病人ID</param>
        /// <param name="examNO">检查号</param>
        /// <param name="Source">来源1、门诊 2、住院  ALL、全院</param>
        /// <returns>未确认查询病人列表</returns>
        public DataTable ExamNoConfirmQuery(string dateBegin, string dateEnd, string deptCode, string examClass, string name, string patientID, string examNO, string Source)
        {
            //2013-12-2 by li 实际ICU开单科室记录
            string sql = @"SELECT DEPT_DICT.DEPT_NAME,
                            (select bed.bed_label from  bed_rec  bed  where bed.ward_code= pats_in_hospital.ward_code and
    bed.bed_no =   PATS_IN_HOSPITAL.BED_NO) bed_no,
                             EXAM_APPOINTS.NAME,
                             EXAM_APPOINTS.SEX,
                             ROUND(MONTHS_BETWEEN(SYSDATE,EXAM_APPOINTS.DATE_OF_BIRTH)/12,0) AGE,
                             EXAM_APPOINTS.REQ_DATE_TIME,
                             EXAM_APPOINTS.EXAM_SUB_CLASS,
                             EXAM_APPOINTS.COSTS,
                             EXAM_APPOINTS.REQ_PHYSICIAN,
                             EXAM_APPOINTS.NOTICE,
                             CASE EXAM_APPOINTS.EXAM_MODE WHEN 'A' THEN '病房' WHEN 'B' THEN '检查科室' END EXAM_MODE,
                             EXAM_APPOINTS.PATIENT_LOCAL_ID,
                             EXAM_APPOINTS.EXAM_NO,
                             CASE WHEN exam_appoints.patient_source IS NULL THEN '门诊' ELSE d.patient_source_name END patient_source,
                             EXAM_APPOINTS.PATIENT_ID, 
                             EXAM_APPOINTS.REQ_DEPT, 
                             EXAM_APPOINTS.VISIT_ID ,
                             EXAM_APPOINTS.Charge_Type,
                             EXAM_APPOINTS.Local_Id_Class, 
                             EXAM_APPOINTS.Name_Phonetic,
                             EXAM_APPOINTS.Date_Of_Birth,
                             EXAM_APPOINTS.Birth_Place,
                             EXAM_APPOINTS.Identity,
                             EXAM_APPOINTS.Mailing_Address,
                             EXAM_APPOINTS.Zip_Code,
                             EXAM_APPOINTS.Phone_Number ,
                            EXAM_APPOINTS.Exam_Class,
                            EXAM_APPOINTS.Clin_Symp,
                            EXAM_APPOINTS.Phys_Sign,
                            EXAM_APPOINTS.Relevant_Lab_Test,
                            EXAM_APPOINTS.Relevant_Diag,
                            EXAM_APPOINTS.Clin_Diag,
                            EXAM_APPOINTS.Exam_Mode as Exam_Mode_No,
                            EXAM_APPOINTS.Performed_By,
                            EXAM_APPOINTS.Patient_Source as Patient_SourceNo,
                            EXAM_APPOINTS.Facility,
                            EXAM_APPOINTS.Req_Memo,
                            EXAM_APPOINTS.Charges,
                            EXAM_APPOINTS.Scheduled_Date,
                            EXAM_APPOINTS.Exam_Group,
                            EXAM_APPOINTS.ICU_DEPT_CODE
                        FROM EXAM_APPOINTS LEFT JOIN PATS_IN_HOSPITAL 
                        ON exam_appoints.patient_id = pats_in_hospital.patient_id 
                        AND exam_appoints.visit_id = pats_in_hospital.visit_id 
                        LEFT JOIN DEPT_DICT ON DEPT_DICT.DEPT_CODE = EXAM_APPOINTS.REQ_DEPT 
                        left join patient_source_dict d on exam_appoints.patient_source = d.patient_source_code 
                       WHERE exam_appoints.exam_class in({3}) and exam_appoints.PERFORMED_BY = '{2}' 
                              and exam_appoints.BILLING_INDICATOR = 1 
                              and exam_appoints.REQ_DATE_TIME >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                              and exam_appoints.REQ_DATE_TIME <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')";
            if (name != string.Empty)
            {
                sql += " and exam_appoints.name like '%{4}%'";
            }
            if (patientID != string.Empty)
            {
                sql += " and exam_appoints.patient_id = '{5}'";
            }
            if (examNO != string.Empty)
            {
                sql += " and exam_appoints.exam_no = '{6}'";
            }

            if (Source == "1")
            {
                sql += " and exam_appoints.visit_id is null";
            }
            else if (Source == "2")
            {
                sql += " and exam_appoints.visit_id is not null";
            }

            sql += " order by exam_appoints.req_date_time";
            sql = string.Format(sql, dateBegin, dateEnd, deptCode, examClass, name, patientID, examNO);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 检查确认中已确认查询
        /// </summary>
        /// <param name="dateBegin">检查时间起始位置</param>
        /// <param name="dateEnd">检查时间终止位置</param>
        /// <param name="deptCode">查询人员科室代码</param>
        /// <param name="examClass">检查类别</param>
        /// <param name="name">查询病人姓名</param>
        /// <param name="patientID">病人ID</param>
        /// <param name="examNO">检查号</param>
        /// <param name="Source">来源1、门诊 2、住院  ALL、全院</param>
        /// <returns>已确认查询病人列表</returns>
        public DataTable ExamConfirmQuery(string dateBegin, string dateEnd, string deptCode, string examClass, string name, string patientID, string examNO, string Source)
        {
            //2013-12-2 by li 实际ICU开单科室记录
            string sql = @"SELECT DEPT_DICT.DEPT_NAME,
                           pats_in_hospital.bed_no,
                           exam_master.name ,
                           exam_master.sex ,
                           ROUND(MONTHS_BETWEEN(SYSDATE,exam_master.date_of_birth)/12,0) AGE,
                           exam_master.date_of_birth ,
                           exam_master.req_physician ,
                           exam_master.exam_class ,
                           exam_master.exam_sub_class ,
                           exam_master.exam_date_time ,
                           c.DEPT_NAME as DEPT ,
                           exam_master.costs ,
                           exam_master.exam_no ,
                           exam_master.patient_local_id ,
                           exam_master.patient_id ,
                           CASE exam_master.EXAM_MODE WHEN 'A' THEN '病房' WHEN 'B' THEN '检查科室' END EXAM_MODE,
                           exam_master.notice,
                           exam_master.ICU_DEPT_CODE
                        FROM exam_master LEFT OUTER JOIN pats_in_hospital 
                        ON exam_master.patient_id = pats_in_hospital.patient_id AND exam_master.visit_id = pats_in_hospital.visit_id 
                        LEFT JOIN DEPT_DICT ON DEPT_DICT.DEPT_CODE = exam_master.REQ_DEPT 
                        left join patient_source_dict d on exam_master.patient_source = d.patient_source_code 
                        left join DEPT_DICT c on c.DEPT_CODE = exam_master.PERFORMED_BY 
                        WHERE exam_master.exam_class in ({3}) and exam_master.PERFORMED_BY = '{2}' 
                          and exam_master.EXAM_DATE_TIME >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                          and exam_master.EXAM_DATE_TIME <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')";
            if (name != string.Empty)
            {
                sql += " and exam_master.name like '%{4}%'";
            }
            if (patientID != string.Empty)
            {
                sql += " and exam_master.patient_id = '{5}'";
            }
            if (examNO != string.Empty)
            {
                sql += " and exam_master.patient_local_id = '{6}'";
            }

            if (Source == "1")
            {
                sql += " and exam_master.visit_id is null";
            }
            else if (Source == "2")
            {
                sql += " and exam_master.visit_id is not null";
            }

            sql += " order by exam_master.EXAM_DATE_TIME desc";
            sql = string.Format(sql, dateBegin, dateEnd, deptCode, examClass, name, patientID, examNO);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 根据检查号获取检查项目列表
        /// </summary>
        /// <param name="examNo"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.EXAM_ITEMS> GetExamItemsByExamNo(string examNo)
        {
            string sql = @"SELECT  exam_items.exam_no ,
                           exam_items.exam_item_no ,
                           exam_items.exam_item ,
                           exam_items.exam_item_code ,
                           exam_items.costs 
                        FROM exam_items      
                        WHERE ( EXAM_ITEMS.EXAM_NO = '{0}' )  
                        ORDER BY exam_items.exam_item_no ASC";
            sql = string.Format(sql, examNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXAM_ITEMS>(ds).ToList();
        }

        /// <summary>
        /// 根据检查号获取检查划价子项目列表
        /// </summary>
        /// <param name="examNo"></param>
        /// <returns></returns>
        public List<EXAM_BILL_ITEMS> GetExamBillItemsByExamNo(string examNo)
        {
            string sql = @"SELECT  exam_bill_items.exam_no ,
                           exam_bill_items.exam_item_no ,
                           exam_bill_items.charge_item_no ,
                           exam_bill_items.patient_id ,
                           exam_bill_items.visit_id ,
                           exam_bill_items.item_class ,
                           BILL_ITEM_CLASS_DICT.CLASS_NAME ,
                           exam_bill_items.item_name ,
                           exam_bill_items.item_code ,
                           exam_bill_items.item_spec ,
                           exam_bill_items.amount ,
                           exam_bill_items.units ,
                           exam_bill_items.ordered_by ,
                           exam_bill_items.performed_by ,
                           exam_bill_items.costs ,
                           exam_bill_items.charges ,
                           exam_bill_items.billing_date_time ,
                           exam_bill_items.operator_no ,
                           exam_bill_items.verified_indicator  
                        FROM exam_bill_items      
                        LEFT JOIN BILL_ITEM_CLASS_DICT ON
                        BILL_ITEM_CLASS_DICT.CLASS_CODE = exam_bill_items.item_class
                        WHERE EXAM_BILL_ITEMS.EXAM_NO = '{0}' 
                        ORDER BY EXAM_BILL_ITEMS.EXAM_NO,
                        EXAM_BILL_ITEMS.EXAM_ITEM_NO,EXAM_BILL_ITEMS.CHARGE_ITEM_NO";
            sql = string.Format(sql, examNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<EXAM_BILL_ITEMS>(ds).ToList();
        }

        /// <summary>
        /// 划价操作中根据价表项目代码查询对应
        /// </summary>
        /// <param name="itemCode">价表项目代码</param>
        /// <param name="itemClass">价表项目类别</param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.EXAM_ITEM_PRICE> GetExamItemPriceByItemCode(string itemCode, string itemClass)
        {
            string sql = @"SELECT CLINIC_VS_CHARGE.CLINIC_ITEM_CODE,   
                             current_price_list.ITEM_CODE,   
                             current_price_list.ITEM_NAME,   
                             current_price_list.ITEM_SPEC,   
                             current_price_list.UNITS,   
                             current_price_list.PRICE, 
                             BILL_ITEM_CLASS_DICT.CLASS_NAME, 
                             current_price_list.PERFORMED_BY,  
                             current_price_list.ITEM_CLASS,
                             CLINIC_VS_CHARGE.AMOUNT  
                        FROM CLINIC_VS_CHARGE,   
                             current_price_list,
                             BILL_ITEM_CLASS_DICT
                       WHERE ( CLINIC_VS_CHARGE.CHARGE_ITEM_CLASS = current_price_list.ITEM_CLASS ) and  
                             ( CLINIC_VS_CHARGE.CHARGE_ITEM_CODE = current_price_list.ITEM_CODE ) and  
                             ( CLINIC_VS_CHARGE.CHARGE_ITEM_SPEC = current_price_list.ITEM_SPEC ) and  
                             ( CLINIC_VS_CHARGE.UNITS = current_price_list.UNITS ) and 
                             ( current_price_list.ITEM_CLASS = BILL_ITEM_CLASS_DICT.CLASS_CODE) and 
                             ( CLINIC_VS_CHARGE.CLINIC_ITEM_CLASS = '{1}' ) AND  
                             ( CLINIC_VS_CHARGE.CLINIC_ITEM_CODE = '{0}' )";
            sql = string.Format(sql, itemCode, itemClass);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.EXAM_ITEM_PRICE>(ds).ToList();
        }

        /// <summary>
        /// 删除原检查划价项目记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="examNo"></param>
        /// <returns></returns>
        public int DelExamBillItemsByExamNo(BaseEntityer db, string examNo)
        {
            string sql = @"delete from EXAM_BILL_ITEMS where exam_no = {0}";
            sql = string.Format(sql, examNo);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新检查项目费用
        /// </summary>
        /// <param name="db"></param>
        /// <param name="examItem"></param>
        /// <returns></returns>
        public int UpdateExamItems(BaseEntityer db, EXAM_ITEMS examItem)
        {
            string sql = @"update exam_items
                       SET
                        exam_items.costs = '{0}' 
                       where (EXAM_ITEMS.EXAM_NO = '{1}')      
                    and (exam_items.exam_item_no = '{2}')";
            object[] param = new object[] { examItem.COSTS, examItem.EXAM_NO, examItem.EXAM_ITEM_NO };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 保存检查计价项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="examBillItem"></param>
        /// <returns></returns>
        public int SaveExamBillItems(BaseEntityer db, EXAM_BILL_ITEMS examBillItem)
        {
            string sql = string.Empty;
            if (examBillItem.VISIT_ID == Int32.MinValue)
            {
                sql = @"insert into exam_bill_items
                        (
                            exam_bill_items.exam_no ,
                            exam_bill_items.exam_item_no ,
                            exam_bill_items.charge_item_no ,
                            exam_bill_items.patient_id ,
                            exam_bill_items.item_class ,
                            exam_bill_items.item_name ,
                            exam_bill_items.item_code ,
                            exam_bill_items.item_spec ,
                            exam_bill_items.amount ,
                            exam_bill_items.units ,
                            exam_bill_items.ordered_by ,
                            exam_bill_items.performed_by ,
                            exam_bill_items.costs ,
                            exam_bill_items.charges ,
                            exam_bill_items.billing_date_time ,
                            exam_bill_items.operator_no ,
                            exam_bill_items.verified_indicator)
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
                            {14},
                            '{15}',
                            '{16}')";
            }
            else
                sql = @"insert into exam_bill_items
                        (
                            exam_bill_items.exam_no ,
                            exam_bill_items.exam_item_no ,
                            exam_bill_items.charge_item_no ,
                            exam_bill_items.patient_id ,
                            exam_bill_items.item_class ,
                            exam_bill_items.item_name ,
                            exam_bill_items.item_code ,
                            exam_bill_items.item_spec ,
                            exam_bill_items.amount ,
                            exam_bill_items.units ,
                            exam_bill_items.ordered_by ,
                            exam_bill_items.performed_by ,
                            exam_bill_items.costs ,
                            exam_bill_items.charges ,
                            exam_bill_items.billing_date_time ,
                            exam_bill_items.operator_no ,
                            exam_bill_items.verified_indicator ,
                            exam_bill_items.visit_id)
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
                            {14},
                            '{15}',
                            '{16}',
                            '{17}')";
            object[] param = new object[] { examBillItem.EXAM_NO, examBillItem.EXAM_ITEM_NO, examBillItem.CHARGE_ITEM_NO, 
                examBillItem.PATIENT_ID, examBillItem.ITEM_CLASS, examBillItem.ITEM_NAME, examBillItem.ITEM_CODE, examBillItem.ITEM_SPEC, 
                examBillItem.AMOUNT, examBillItem.UNITS, examBillItem.ORDERED_BY, examBillItem.PERFORMED_BY, examBillItem.COSTS, 
                examBillItem.CHARGES, examBillItem.BILLING_DATE_TIME==null?"null":"to_date('"+ examBillItem.BILLING_DATE_TIME.ToString() 
                +"', 'yyyy-MM-dd hh24:mi:ss')", examBillItem.OPERATOR_NO, examBillItem.VERIFIED_INDICATOR, examBillItem.VISIT_ID };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 将划价保存到申请表中
        /// </summary>
        /// <param name="db"></param>
        /// <param name="exam_no"></param>
        /// <param name="costsAll"></param>
        /// <param name="chargesAll"></param>
        /// <returns></returns>
        public int UpdateExamAppoints(BaseEntityer db, string exam_no, double costsAll, double chargesAll)
        {
            string sql = @"update EXAM_APPOINTS
                       SET
                        EXAM_APPOINTS.COSTS = '{0}',
                       EXAM_APPOINTS.CHARGES = '{1}'  
                       where (EXAM_APPOINTS.EXAM_NO = '{2}')";
            object[] param = new object[] { costsAll, chargesAll, exam_no };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查看当前病人是否预交金余额可以计价
        /// </summary>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <param name="Charge_Type">预交金类别名称</param>
        /// <returns></returns>
        public bool CheckPayment(string patient_id, string visit_id, string Charge_Type)
        {
            bool isCheck = false;
            //读取患者当前押金总额，费用总额相减
            string sql = @"SELECT nvl( Prepayments,0)-nvl(total_charges,0) as charge From pats_in_hospital 
                        where patient_id='{0}' and visit_id='{1}'";
            sql = string.Format(sql, patient_id, visit_id);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                //读取当前患者费别的费用警戒线
                string sql1 = @"select  CHARGE_LOW from charge_type_dict where charge_type_name= '{0}'";
                sql1 = string.Format(sql1, Charge_Type);
                DataTable dt1 = BaseEntityer.Db.GetDataTable(sql1);
                if (dt1.Rows.Count > 0)
                {
                    //公式：押金  - 费用总额 < 警戒线， 则不允许计价。
                    if (Convert.ToDouble(dt.Rows[0]["charge"].ToString()) - Convert.ToDouble(dt1.Rows[0]["CHARGE_LOW"].ToString()) >= 0)
                        isCheck = true;
                    else
                        isCheck = false;
                }
                else
                {
                    //2013-9-23 by li 当前患者费别无警戒线则返回true
                    isCheck = true;
                }
            }
            return isCheck;
        }

        /// <summary>
        /// 是否存在病人主索引记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="exam_pat_mi"></param>
        /// <returns></returns>
        public DbDataReader SearchExamPatMi(BaseEntityer db, EXAM_PAT_MI exam_pat_mi)
        {
            string sqlsearch = @"SELECT patient_local_id INTO  :ss
                            FROM  exam_pat_mi
                            WHERE patient_local_id = '{0}'
                            AND   local_id_class = '{1}'";
            sqlsearch = string.Format(sqlsearch, exam_pat_mi.PATIENT_LOCAL_ID, exam_pat_mi.LOCAL_ID_CLASS);
            return db.ExecuteReader(sqlsearch);
        }

        /// <summary>
        /// 修改exam_pat_mi记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="exam_pat_mi"></param>
        /// <param name="exist"></param>
        /// <returns></returns>
        public int UpdateExamPatMi(BaseEntityer db, EXAM_PAT_MI exam_pat_mi, bool exist)
        {
            string sql = string.Empty;
            if (exist)
            {
                //update
                sql = @"Update exam_pat_mi
                              Set  patient_id='{0}', name='{1}', name_phonetic='{2}',
                              sex='{3}', date_of_birth={4}, birth_place='{5}',
                              identity='{6}', charge_type='{7}',
                              mailing_address='{8}', zip_code='{9}',
                              phone_number='{10}'
                              WHERE local_id_class='{11}'
                              AND  patient_local_id='{12}'";

            }
            else
            {
                //insert
                sql = @"INSERT INTO exam_pat_mi
                                (patient_id, 
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
                                local_id_class, 
                                patient_local_id) 
                            VALUES( '{0}', '{1}', '{2}', '{3}', 
                              {4}, '{5}', '{6}', '{7}', '{8}', 
                              '{9}', '{10}', '{11}', '{12}' )";
            }
            object[] param = new object[] { exam_pat_mi.PATIENT_LOCAL_ID, exam_pat_mi.NAME, exam_pat_mi.NAME_PHONETIC,
                exam_pat_mi.SEX, exam_pat_mi.DATE_OF_BIRTH == null ? "null" : "to_date('"+exam_pat_mi.DATE_OF_BIRTH.ToString()+"', 'yyyy-MM-dd hh24:mi:ss')", 
                exam_pat_mi.BIRTH_PLACE, exam_pat_mi.IDENTITY, exam_pat_mi.CHARGE_TYPE, exam_pat_mi.MAILING_ADDRESS, 
                exam_pat_mi.ZIP_CODE, exam_pat_mi.PHONE_NUMBER, exam_pat_mi.LOCAL_ID_CLASS, exam_pat_mi.PATIENT_LOCAL_ID};
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 在表exam_master中插入记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="exam_master"></param>
        /// <returns></returns>
        public int InsertExamMaster(BaseEntityer db, EXAM_MASTER exam_master)
        {
            string sql = @"INSERT INTO exam_master
                            (exam_no, 
                            local_id_class, 
                            patient_local_id,
                            patient_id, 
                            name, 
                            sex, 
                            date_of_birth, 
                            exam_class, 
                            exam_sub_class,
                            clin_symp, 
                            phys_sign, 
                            relevant_lab_test, 
                            relevant_diag,
                            clin_diag, 
                            exam_mode, 
                            performed_by, 
                            patient_source, 
                            facility,
                            req_date_time, 
                            req_dept, 
                            req_physician,
                            req_memo,
                            scheduled_date_time,
                            notice, 
                            costs, 
                            charges, 
                            exam_date_time, 
                            result_status, 
                            exam_group,
                            visit_id,
                            device)
                        VALUES( '{0}', '{1}', '{2}', '{3}',
                            '{4}', '{5}', to_date('{6}', 'yyyy-mm-dd hh24:mi:ss'), '{7}', '{8}',
                            '{9}', '{10}', '{11}', '{12}',
                            '{13}', '{14}', '{15}', '{16}', '{17}',
                            to_date('{18}', 'yyyy-mm-dd hh24:mi:ss'), '{19}', '{20}', '{21}', to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'),
                            '{23}', '{24}', '{25}', to_date('{26}', 'yyyy-mm-dd hh24:mi:ss'), '2', '{27}',
                            '{28}', '{29}')";
            object[] param = new object[] { exam_master.EXAM_NO, exam_master.LOCAL_ID_CLASS, exam_master.PATIENT_LOCAL_ID, 
                exam_master.PATIENT_ID, exam_master.NAME, exam_master.SEX, exam_master.DATE_OF_BIRTH, exam_master.EXAM_CLASS,
                exam_master.EXAM_SUB_CLASS, exam_master.CLIN_SYMP, exam_master.PHYS_SIGN, exam_master.RELEVANT_LAB_TEST,
                exam_master.RELEVANT_DIAG, exam_master.CLIN_DIAG, exam_master.EXAM_MODE, exam_master.PERFORMED_BY, exam_master.PATIENT_SOURCE,
                exam_master.FACILITY, exam_master.REQ_DATE_TIME, exam_master.REQ_DEPT, exam_master.REQ_PHYSICIAN, exam_master.REQ_MEMO,
                exam_master.SCHEDULED_DATE_TIME, exam_master.NOTICE, exam_master.COSTS, exam_master.CHARGES, exam_master.EXAM_DATE_TIME,
                exam_master.EXAM_GROUP, exam_master.VISIT_ID, exam_master.DEVICE };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除对应的预约记录exam_appoints
        /// </summary>
        /// <param name="db"></param>
        /// <param name="examNo"></param>
        /// <returns></returns>
        public int DelExamAppoints(BaseEntityer db, string examNo)
        {
            string sql = @"delete from exam_appoints where exam_no = {0}";
            sql = string.Format(sql, examNo);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取item_no
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patient_id"></param>
        /// <param name="visit_id"></param>
        /// <returns></returns>
        public DbDataReader GetMaxItemNo(BaseEntityer db, string patient_id, string visit_id)
        {
            string sqlsearch = @"SELECT	Max(item_no) as MaxItemNo 
                                INTO	   :item_no
                                FROM	   inp_bill_detail
                                WHERE	   patient_id = '{0}'
                                AND	   visit_id = '{1}'";
            sqlsearch = string.Format(sqlsearch, patient_id, visit_id);
            return db.ExecuteReader(sqlsearch);
        }

        /// <summary>
        /// 获取住院病人费用信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="examNo"></param>
        /// <returns></returns>
        public DataTable GetExamBillItems(BaseEntityer db, string examNo)
        {
            string sql = @"SELECT patient_id,
                               e.visit_id,
                               e.item_class,
                               e.item_name,
                               e.item_code,
                               e.item_spec,
                               e.amount,
                               e.units,
                               e.ordered_by,
                               e.performed_by,
                               e.costs,
                               e.charges
                          FROM exam_bill_items e
	                    where exam_no = '{0}'";
            sql = string.Format(sql, examNo);
            return db.GetDataTable(sql);
        }

        /// <summary>
        /// 插入住院病人费用信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="inpBillDetail"></param>
        /// <returns></returns>
        public int InsertInpBillDetail(BaseEntityer db, INP_BILL_DETAIL detail)
        {
            //2013-7-25 by li 检查检验收费明细增加五项分类等字段数据
            //2013-9-15 by li 检查确认住院收费明细中增加诊疗项目类别和编码
            //2013-12-3 by li 实际ICU开单科室记录
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
                      MATERIAL_SITECODE,
                        CLINIC_ITEM_CLASS,
                        CLINIC_ITEM_CODE,
                        ICU_DEPT_CODE,
                         BED_NO,
                       PAT_DEPT_CODE)
                    VALUES
                      ('{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}'
                       ,'{10}','{11}','{12}',to_date('{13}', 'yyyy-MM-dd  hh24:mi:ss'),'{14}',
                      '{15}','{16}',to_date('{17}', 'yyyy-MM-dd  hh24:mi:ss'),'{18}','{19}','{20}','{21}',
                    '{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}',
                    '{32}','{33}','{34}','{35}','{36}','{37}','{38}')";
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
                detail.MATERIAL_SITECODE,
                detail.CLINIC_ITEM_CLASS,
                detail.CLINIC_ITEM_CODE,
                detail.ICU_DEPT_CODE,
                detail.Bed_NO,
                detail.Pat_Dept_Code
               };
            sql = sql.SqlFormate(obs);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2013-11-20 by li 获取检查诊断的类别和CODE，为住院写入计价明细用
        /// </summary>
        /// <param name="db"></param>
        /// <param name="examNo"></param>
        /// <returns></returns>
        public DataTable GetExamClinicItem(BaseEntityer db, string examNo)
        {
            string sql = @"select d.order_class, d.order_code
                              from doctor_orders d
                             where d.test_no = '{0}'";
            sql = string.Format(sql, examNo);
            return db.GetDataTable(sql);
        }

        #endregion

        #region 检验管理

        /// <summary>
        /// 根据科室代码获取维护项目列表
        /// </summary>
        /// <param name="dept_code"></param>
        /// <returns></returns>
        public List<ZSS_DICT> GetZssDictByDeptCode(string dept_code)
        {
            string sql = @"SELECT ZSS_DICT.ITEM_CODE ,
                           ZSS_DICT.ITEM_NAME ,
                           ZSS_DICT.ITEM_DEPT ,
                           ZSS_DICT.ITEM_CLASS 
                           FROM ZSS_DICT 
                           WHERE ZSS_DICT.ITEM_DEPT = '{0}' 
                           ORDER BY ZSS_DICT.ITEM_CODE";
            sql = string.Format(sql, dept_code);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<ZSS_DICT>(ds).ToList();
        }

        /// <summary>
        /// 新增当前科室检验维护项目数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="exam_master"></param>
        /// <returns></returns>
        public int InsertZssDict(BaseEntityer db, ZSS_DICT zss_dict)
        {
            string sql = @"INSERT INTO ZSS_DICT
                        (
                           ZSS_DICT.ITEM_CODE ,
                           ZSS_DICT.ITEM_NAME ,
                           ZSS_DICT.ITEM_DEPT ,
                           ZSS_DICT.ITEM_CLASS 
                        )
                        VALUES 
                        (
                           '{0}','{1}','{2}','{3}'
                        )";
            object[] param = new object[] { zss_dict.ITEM_CODE, zss_dict.ITEM_NAME, 
                zss_dict.ITEM_DEPT, zss_dict.ITEM_CLASS };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除当前科室检验维护项目列表数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="zss_dict"></param>
        /// <returns></returns>
        public int DeleteZssDict(BaseEntityer db, string dept_code)
        {
            string sql = @"DELETE FROM ZSS_DICT WHERE ZSS_DICT.ITEM_DEPT = {0}";
            sql = string.Format(sql, dept_code);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 检验查询
        /// </summary>
        /// <param name="testQueryType">查询类别</param>
        /// <param name="dateBegin">申请时间起始位置</param>
        /// <param name="dateEnd">申请时间终止位置</param>
        /// <param name="deptCode">查询人员科室代码</param>
        /// <param name="name">查询病人姓名</param>
        /// <param name="patientID">病人ID</param>
        /// <param name="testNO">检验号</param>
        /// <returns></returns>
        public DataTable TestConfirmQuery(TestQueryType testQueryType, string dateBegin, string dateEnd, string deptCode, string name, string patientID, string testNO)
        {
            //2013-12-2 by li 实际ICU开单科室记录
            string sql = @"SELECT LAB_TEST_MASTER.PRIORITY_INDICATOR ,
                           LAB_TEST_MASTER.NAME ,
                           LAB_TEST_MASTER.ORDERING_DEPT ,
                           PATS_IN_HOSPITAL.BED_NO ,
                           LAB_TEST_MASTER.SEX ,
                           LAB_TEST_MASTER.AGE ,
                           LAB_TEST_MASTER.TEST_NO ,
                           LAB_TEST_MASTER.PATIENT_ID ,
                           LAB_TEST_MASTER.WORKING_ID ,
                           LAB_TEST_MASTER.RELEVANT_CLINIC_DIAG ,
                           LAB_TEST_MASTER.SPECIMEN ,
                           LAB_TEST_MASTER.NOTES_FOR_SPCM ,
                           LAB_TEST_MASTER.COSTS ,
                           LAB_TEST_MASTER.CHARGES ,
                           LAB_TEST_MASTER.REQUESTED_DATE_TIME ,
                           LAB_TEST_MASTER.ORDERING_PROVIDER ,
                           LAB_TEST_MASTER.VISIT_ID ,
                           LAB_TEST_MASTER.EXECUTE_DATE ,
                           LAB_TEST_MASTER.NAME_PHONETIC ,
                           LAB_TEST_MASTER.CHARGE_TYPE ,
                           LAB_TEST_MASTER.TEST_CAUSE ,
                           LAB_TEST_MASTER.SPCM_RECEIVED_DATE_TIME ,
                           LAB_TEST_MASTER.SPCM_SAMPLE_DATE_TIME ,
                           LAB_TEST_MASTER.PERFORMED_BY ,
                           LAB_TEST_MASTER.RESULT_STATUS ,
                           LAB_TEST_MASTER.RESULTS_RPT_DATE_TIME ,
                           LAB_TEST_MASTER.TRANSCRIPTIONIST ,
                           LAB_TEST_MASTER.VERIFIED_BY ,
                           LAB_TEST_MASTER.BILLING_INDICATOR ,
                           LAB_TEST_MASTER.PRINT_INDICATOR ,
                           LAB_TEST_MASTER.ICU_DEPT_CODE 
                           FROM LAB_TEST_MASTER LEFT OUTER JOIN PATS_IN_HOSPITAL 
                           ON LAB_TEST_MASTER.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID 
                           AND LAB_TEST_MASTER.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID";
            switch (testQueryType)
            {
                case TestQueryType.病房申请:
                    sql += @" where REQUESTED_DATE_TIME >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                           and REQUESTED_DATE_TIME <= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
                           and LAB_TEST_MASTER.PERFORMED_BY='{2}' 
                           and LAB_TEST_MASTER.RESULT_STATUS is null 
                           and LAB_TEST_MASTER.visit_id is not null
                           and (LAB_TEST_MASTER.BILLING_INDICATOR = 1 or LAB_TEST_MASTER.BILLING_INDICATOR is null)";
                    break;
                case TestQueryType.门诊申请:
                    sql += @" where REQUESTED_DATE_TIME >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                           and REQUESTED_DATE_TIME <= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
                           and LAB_TEST_MASTER.PERFORMED_BY = '{2}' 
                           and LAB_TEST_MASTER.RESULT_STATUS is null 
                           and LAB_TEST_MASTER.visit_id is null 
                           and LAB_TEST_MASTER.BILLING_INDICATOR = 1";
                    break;
                case TestQueryType.已经检验:
                    sql += @" where REQUESTED_DATE_TIME >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                           and REQUESTED_DATE_TIME <= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
                           and LAB_TEST_MASTER.PERFORMED_BY = '{2}' 
                           and RESULT_STATUS = '2'";
                    break;
                default:
                    sql += @" where REQUESTED_DATE_TIME >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                           and REQUESTED_DATE_TIME <= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
                           and LAB_TEST_MASTER.PERFORMED_BY = '{2}' 
                           and LAB_TEST_MASTER.RESULT_STATUS is null 
                           and LAB_TEST_MASTER.visit_id is not null";
                    break;
            }
            if (name != string.Empty)
            {
                sql += " and LAB_TEST_MASTER.NAME like '%{3}%'";
            }
            if (patientID != string.Empty)
            {
                sql += " and LAB_TEST_MASTER.PATIENT_ID = '{4}'";
            }
            if (testNO != string.Empty)
            {
                sql += " and LAB_TEST_MASTER.TEST_NO = '{5}'";
            }
            sql += " order by LAB_TEST_MASTER.REQUESTED_DATE_TIME";
            sql = string.Format(sql, dateBegin, dateEnd, deptCode, name, patientID, testNO);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 根据检验号获取检验项目列表
        /// </summary>
        /// <param name="testNO"></param>
        /// <returns></returns>
        public List<LAB_TEST_ITEMS> GetTestItemsByNo(BaseEntityer db, string testNO)
        {
            string sql = @"SELECT LAB_TEST_ITEMS.TEST_NO ,
                           LAB_TEST_ITEMS.item_no ,
                           LAB_TEST_ITEMS.item_name ,
                           LAB_TEST_ITEMS.item_code,
                            (select nvl(c.item_subclass,LAB_TEST_ITEMS.item_code)
                            from clinic_item_dict c
                            where c.item_code = LAB_TEST_ITEMS.item_code
                            and rownum = 1) item_subclass   
                           FROM LAB_TEST_ITEMS      
                           WHERE ( LAB_TEST_ITEMS.TEST_NO = '{0}' ) 
                           ORDER BY LAB_TEST_ITEMS.item_no ASC";
            sql = string.Format(sql, testNO);
            DataSet ds = db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<LAB_TEST_ITEMS>(ds).ToList();
        }

        /// <summary>
        /// 根据检验号获取检验划价子项目列表
        /// </summary>
        /// <param name="examNo"></param>
        /// <returns></returns>
        public DataTable GetTestBillItemsByTestNo(BaseEntityer db, string item_code)
        {
            string sql = @"SELECT current_price_list.item_class ,
                           current_price_list.item_code ,
                           current_price_list.item_name ,
                           current_price_list.item_spec ,
                           current_price_list.units ,
                           current_price_list.price ,
                           current_price_list.prefer_price ,
                           current_price_list.foreigner_price ,
                           current_price_list.performed_by ,
                           current_price_list.fee_type_mask ,
                           current_price_list.class_on_inp_rcpt ,
                           current_price_list.class_on_outp_rcpt ,
                           current_price_list.class_on_reckoning ,
                           current_price_list.subj_code ,
                           current_price_list.class_on_mr ,
                           current_price_list.memo ,
                           current_price_list.operator ,
                           current_price_list.enter_date ,
                           clinic_vs_charge.amount ,
                           clinic_vs_charge.clinic_item_class ,
                           clinic_vs_charge.clinic_item_code 
                           FROM clinic_vs_charge ,
                                current_price_list     
                           WHERE ( clinic_vs_charge.units = current_price_list.units ) and 
                                 ( clinic_vs_charge.charge_item_class = current_price_list.item_class ) and 
                                 ( clinic_vs_charge.charge_item_code = current_price_list.item_code ) and 
                                 ( clinic_vs_charge.charge_item_spec = current_price_list.item_spec ) and 
                                 ( ( CLINIC_VS_CHARGE.CLINIC_ITEM_CLASS = 'C' ) and 
                                 ( CLINIC_VS_CHARGE.CLINIC_ITEM_CODE = '{0}' ) ) 
                           ORDER BY current_price_list.item_class, current_price_list.item_code";
            sql = string.Format(sql, item_code);
            return db.GetDataTable(sql);
        }

        /// <summary>
        /// 划价成功更新申请表
        /// </summary>
        /// <param name="testNO"></param>
        /// <param name="costsAll"></param>
        /// <returns></returns>
        public int UpdateTestPrice(BaseEntityer db, string testNO, decimal costsAll)
        {
            string sql = @"update LAB_TEST_MASTER 
                            set costs = {1},charges = {2}
                            where test_no = '{0}'";
            sql = Utility.SqlFormate(sql, testNO, costsAll, costsAll);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新检验项目确认状态
        /// </summary>
        /// <param name="db"></param>
        /// <param name="testNO"></param>
        /// <param name="executeDate"></param>
        /// <returns></returns>
        public int UpdateTestConfirm(BaseEntityer db, string testNO, string executeDate)
        {
            string sql = @"UPDATE LAB_TEST_MASTER 
                            SET RESULT_STATUS = '2',EXECUTE_DATE = to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
                            WHERE TEST_NO = '{0}'";
            sql = Utility.SqlFormate(sql, testNO, executeDate);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2013-11-13 by li 根据检查检验单号查询是否存在住院病人医嘱号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="test_no"></param>
        /// <returns></returns>
        public DbDataReader SearchInpOrdersNo(BaseEntityer db, string test_no)
        {
            string sqlsearch = @"select d.order_no from doctor_orders d where d.test_no='{0}'";
            sqlsearch = string.Format(sqlsearch, test_no);
            return db.ExecuteReader(sqlsearch);
        }

        #endregion

        #region 查询住院患者信息

        /// <summary>
        ///  获得患者住院号和所在科室编码
        /// </summary>
        /// <param name="PATIENT_ID"></param>
        /// <returns></returns>
        public DataTable GetPersonInHospitalBedAndDeptCode(string PATIENT_ID)
        {
            string sql = @"SELECT bed_no,dept_code FROM pats_in_hospital WHERE Patient_id = '{0}' and  rownum=1";
            sql = sql.SqlFormate(PATIENT_ID);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        #endregion
    }
}
