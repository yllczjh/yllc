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

namespace HisDBLayer
{
    /// <summary>
    /// 血液管理
    /// </summary>
    public class BloodManagement
    {
        #region 用血申请

        /// <summary>
        /// 获取序列值
        /// </summary>
        /// <param name="seqName"></param>
        /// <returns></returns>
        public int GetSequence(string seqName)
        {
            string strSQL = " select {0}.NEXTVAL from dual ";
            strSQL = string.Format(strSQL, seqName);
            string value = BaseEntityer.Db.ExecuteScalar(strSQL).ToString();
            return Convert.ToInt32(value);
        }

        #region 查询全表信息
        /// <summary>
        /// 查询全表信息
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BLOOD_APPLY> GetALLBloodApply()
        {
            string strSQL = string.Empty;

            #region SQL语句

            strSQL = @" SELECT
                        t.STATUS,   --状态:1申请2核准3配血4发血
                        t.SOURCE_FLAG,   --来源标识:0住院1门诊
                        t.PATIENT_ID,   --病人ID
                        t.USE_DATE,   --预计用血时间
                        t.DEPT_NAME,   --申请科室名称
                        t.EXPEND10,   --扩展字段十
                        t.EXPEND9,   --扩展字段九
                        t.EXPEND8,   --扩展字段八
                        t.EXPEND7,   --扩展字段七
                        t.EXPEND6,   --扩展字段六
                        t.EXPEND5,   --扩展字段五
                        t.EXPEND4,   --扩展字段四
                        t.EXPEND3,   --扩展字段三
                        t.EXPEND2,   --扩展字段二
                        t.EXPEND1,   --扩展字段一
                        t.PRICE,   --划价标识 1-划价
                        t.DOCTOR,   --医师
                        t.PHYSICIAN,   --主治医师
                        t.DIRECTOR,   --科主任
                        t.DISCHARGE_ID,   --发血执行人
                        t.DISCHARGE_DATE,   --发血时间
                        t.EQUIP_ID,   --配血执行人
                        t.EQUIP_DATE,   --配血时间
                        t.GATHER_ID,   --核准人标识
                        t.GATHER_DATE,   --血库收到时间（核准时间）
                        t.C_MOMO,   --备注
                        t.C_FILTER,   --Ⅰ、Ⅱ、Ⅲ
                        t.PAT_AGE,   --受血者年龄
                        t.C_ANTI_HIV,   --阴、阳
                        t.C_ANTI_HCV,   --阴、阳
                        t.C_ANTI_HIV1_2,   --阴、阳
                        t.C_SYPHILIS,   --阴、阳
                        t.C_HBEAB,   --阴、阳
                        t.C_HBSAB,   --阴、阳
                        t.C_BLOOD_RH,   --阴、阳
                        t.C_BLOOD_TYPE,   --A, B, AB, O
                        t.B_ANTI_HIV,   --阴、阳
                        t.B_ANTI_HCV,   --阴、阳
                        t.B_ANTI_HIV1_2,   --阴、阳
                        t.B_SYPHILIS,   --阴、阳
                        t.B_HBEAB,   --阴、阳
                        t.B_HBSAB,   --阴、阳
                        t.B_HBCAB,   --阴、阳
                        t.B_HBEAG,   --阴、阳
                        t.B_HBSAG,   --阴、阳
                        t.B_PLATELET,   --
                        t.B_ALT,   --
                        t.B_HCT,   --
                        t.B_HB,   --
                        t.PAT_STATUS,   --0孕0产
                        t.BLOOD_ALLERGY,   --输血过敏反应
                        t.BLOOD_HISTORY,   --既往输血史
                        t.BLOOD_PROPERTY,   --输血性质：常规
                        t.BLOOD_UNIT,   --ml、袋
                        t.BLOOD_INGREDIENT,   --血浆、血细胞
                        t.BLOOD_RH,   --阴、阳
                        t.BLOOD_TYPE,   --A,B,AB,O
                        t.BLOOD_GOAL,   --用血目的
                        t.HELP_FEE,   --受血互助金
                        t.BED_NO,   --受血病人所在床位
                        t.C_HBCAB,   --阴、阳
                        t.C_HBEAG,   --阴、阳
                        t.C_HBSAG,   --阴、阳
                        t.C_PLATELET,   --
                        t.C_ALT,   --
                        t.APPLY_DATE,   --预约申请时间
                        t.BLOOD_SUM,   --输血总量
                        t.C_HCT,   --
                        t.C_HB,   --
                        t.B_BLOOD_RH,   --阴、阳
                        t.B_BLOOD_TYPE,   --A, B, AB, O
                        t.BLOOD_TABOO,   --描述受血者的过敏史及禁尽症
                        t.BLOOD_DIAGNOSE,   --诊断及输血适应症
                        t.PAT_PLACE,   --受血者属地
                        t.BIRTHDAY,   --受血者生日
                        t.PAT_SEX,   --受血者性别
                        t.PAT_NAME,   --受血者姓名
                        t.DEPT_CODE,   --申请科室代码
                        t.INP_NO,   --病人住院标识号
                        t.APPLY_NUM,   --顺序号
                        t.VISIT_ID,
                        t.APPLY_ID
                        FROM
                        BLOOD_APPLY  t   --
                        ";
            #endregion

            //strSQL = string.Format(strSQL, PATIENT_ID);
            DataSet ds = BaseEntityer.Db.GetDataSet(strSQL);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BLOOD_APPLY>(ds).ToList();
        }


        /// <summary>
        /// 获取申请列表用于查询列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetApplyList()
        {
            string sql = @" select t.apply_id,t.pat_name as name ,t.pat_sex as sex,t.patient_id as patient_id ,t.pat_place as name_phonetic from blood_apply t  where t.STATUS='1' and t.source_flag='0'";
            return BaseEntityer.Db.GetDataTable(sql);
        }
        #endregion

        /// <summary>
        /// 2014-8-22 BY XU 查询住院患者用血申请单项目
        /// </summary>
        /// <param name="patientId">患者标识</param>
        /// <param name="source_flag">来源标识</param>
        /// <returns></returns>
        public List<BLOOD_APPLY> GetListByPatientId(string patientId, string visitId)
        {
            string sql = @"SELECT *
                          FROM BLOOD_APPLY O
                         WHERE O.PATIENT_ID = '{0}' and O.VISIT_ID = '{1}' ";
            sql = sql.SqlFormate(patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<BLOOD_APPLY>(ds).ToList();
            else
                return new List<BLOOD_APPLY>();
        }
        /// <summary>
        /// 2014-8-22 BY XU 根据apply_num查询输血申请单
        /// </summary>
        /// <param name="apply_num"></param>
        /// <returns></returns>
        public BLOOD_APPLY GetModel(string apply_num, string patientId, string visitId)
        {
            string sql = @"select * from BLOOD_APPLY t where t.apply_num='{0}' and t.patient_id='{1}' and t.visit_id='{2}' ";
            sql = sql.SqlFormate(apply_num, patientId, visitId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<BLOOD_APPLY>(ds).FirstOrDefault();
            else
                return new BLOOD_APPLY();
        }

        #region 插入申请表信息
        /// <summary>
        /// 查询申请树信息
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BLOOD_APPLY> GetALLBloodApplyForTree(string ApplyDate)
        {
            string strSQL = string.Empty;

            #region SQL语句

            strSQL = @" SELECT
                        t.APPLY_ID,   --申请单号
                        t.VISIT_ID,   --
                        t.PAT_CHAN,   --产
                        t.BLOOD_HELP_FEE,   --输血互助金
                        t.BLOOD_COMPONENT,   --输血成分
                        t.STATUS,   --状态:1申请2核准3配血4发血
                        t.SOURCE_FLAG,   --来源标识:0住院1门诊
                        t.PATIENT_ID,   --病人ID
                        t.USE_DATE,   --预计用血时间
                        t.DEPT_NAME,   --申请科室名称
                        t.EXPEND10,   --扩展字段十
                        t.EXPEND9,   --扩展字段九
                        t.EXPEND8,   --扩展字段八
                        t.EXPEND7,   --扩展字段七
                        t.EXPEND6,   --扩展字段六
                        t.EXPEND5,   --扩展字段五
                        t.EXPEND4,   --扩展字段四
                        t.EXPEND3,   --扩展字段三
                        t.EXPEND2,   --扩展字段二
                        t.EXPEND1,   --扩展字段一
                        t.PRICE,   --划价标识 1-划价
                        t.DOCTOR,   --医师
                        t.PHYSICIAN,   --主治医师
                        t.DIRECTOR,   --科主任
                        t.DISCHARGE_ID,   --发血执行人
                        t.DISCHARGE_DATE,   --发血时间
                        t.EQUIP_ID,   --配血执行人
                        t.EQUIP_DATE,   --配血时间
                        t.GATHER_ID,   --核准人标识
                        t.GATHER_DATE,   --血库收到时间（核准时间）
                        t.C_MOMO,   --备注
                        t.C_FILTER,   --Ⅰ、Ⅱ、Ⅲ
                        t.PAT_AGE,   --受血者年龄
                        t.C_ANTI_HIV,   --阴、阳
                        t.C_ANTI_HCV,   --阴、阳
                        t.C_ANTI_HIV1_2,   --阴、阳
                        t.C_SYPHILIS,   --阴、阳
                        t.C_HBEAB,   --阴、阳
                        t.C_HBSAB,   --阴、阳
                        t.C_BLOOD_RH,   --阴、阳
                        t.C_BLOOD_TYPE,   --A, B, AB, O
                        t.B_ANTI_HIV,   --阴、阳
                        t.B_ANTI_HCV,   --阴、阳
                        t.B_ANTI_HIV1_2,   --阴、阳
                        t.B_SYPHILIS,   --阴、阳
                        t.B_HBEAB,   --阴、阳
                        t.B_HBSAB,   --阴、阳
                        t.B_HBCAB,   --阴、阳
                        t.B_HBEAG,   --阴、阳
                        t.B_HBSAG,   --阴、阳
                        t.B_PLATELET,   --
                        t.B_ALT,   --
                        t.B_HCT,   --
                        t.B_HB,   --
                        t.PAT_YUN,   --孕
                        t.BLOOD_ALLERGY,   --输血过敏反应
                        t.BLOOD_HISTORY,   --既往输血史
                        t.BLOOD_PROPERTY,   --输血性质：常规
                        t.BLOOD_UNIT,   --ml、袋
                        t.BLOOD_INGREDIENT,   --血浆、血细胞
                        t.BLOOD_RH,   --阴、阳
                        t.BLOOD_TYPE,   --A,B,AB,O
                        t.BLOOD_GOAL,   --用血目的
                        t.HELP_FEE,   --受血互助金
                        t.BED_NO,   --受血病人所在床位
                        t.C_HBCAB,   --阴、阳
                        t.C_HBEAG,   --阴、阳
                        t.C_HBSAG,   --阴、阳
                        t.C_PLATELET,   --
                        t.C_ALT,   --
                        t.APPLY_DATE,   --申请填写时间
                        t.BLOOD_SUM,   --输血总量
                        t.C_HCT,   --
                        t.C_HB,   --
                        t.B_BLOOD_RH,   --阴、阳
                        t.B_BLOOD_TYPE,   --A, B, AB, O
                        t.BLOOD_TABOO,   --描述受血者的过敏史及禁尽症
                        t.BLOOD_DIAGNOSE,   --诊断及输血适应症
                        t.PAT_PLACE,   --受血者属地
                        t.BIRTHDAY,   --受血者生日
                        t.PAT_SEX,   --受血者性别
                        t.PAT_NAME,   --受血者姓名
                        t.DEPT_CODE,   --申请科室代码
                        t.INP_NO,   --病人住院标识号
                        t.APPLY_NUM,   --顺序号
                        t.APPLY_ID,
                        t.VISIT_ID
                        FROM
                        BLOOD_APPLY  t   --

                        
                        WHERE   t.APPLY_DATE>=TO_DATE('{0}','YYYY-MM-DD HH24:MI:SS')
                        
                        AND t.STATUS='1' ";
                        
                        //AND t.SOURCE_FLAG='0'";
            #endregion

            strSQL = string.Format(strSQL, ApplyDate);
            DataSet ds = BaseEntityer.Db.GetDataSet(strSQL);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BLOOD_APPLY>(ds).ToList();
        }


        /// <summary>
        /// 查询申请树信息
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BLOOD_APPLY> GetBloodApplyForPatientID(string ApplyCode)
        {
            string strSQL = string.Empty;

            #region SQL语句

            strSQL = @" SELECT
                        t.APPLY_ID,   --申请单号
                        t.VISIT_ID,   --
                        t.PAT_CHAN,   --产
                        t.BLOOD_HELP_FEE,   --输血互助金
                        t.BLOOD_COMPONENT,   --输血成分
                        t.STATUS,   --状态:1申请2核准3配血4发血
                        t.SOURCE_FLAG,   --来源标识:0住院1门诊
                        t.PATIENT_ID,   --病人ID
                        t.USE_DATE,   --预计用血时间
                        t.DEPT_NAME,   --申请科室名称
                        t.EXPEND10,   --扩展字段十
                        t.EXPEND9,   --扩展字段九
                        t.EXPEND8,   --扩展字段八
                        t.EXPEND7,   --扩展字段七
                        t.EXPEND6,   --扩展字段六
                        t.EXPEND5,   --扩展字段五
                        t.EXPEND4,   --扩展字段四
                        t.EXPEND3,   --扩展字段三
                        t.EXPEND2,   --扩展字段二
                        t.EXPEND1,   --扩展字段一
                        t.PRICE,   --划价标识 1-划价
                        t.DOCTOR,   --医师
                        t.PHYSICIAN,   --主治医师
                        t.DIRECTOR,   --科主任
                        t.DISCHARGE_ID,   --发血执行人
                        t.DISCHARGE_DATE,   --发血时间
                        t.EQUIP_ID,   --配血执行人
                        t.EQUIP_DATE,   --配血时间
                        t.GATHER_ID,   --核准人标识
                        t.GATHER_DATE,   --血库收到时间（核准时间）
                        t.C_MOMO,   --备注
                        t.C_FILTER,   --Ⅰ、Ⅱ、Ⅲ
                        t.PAT_AGE,   --受血者年龄
                        t.C_ANTI_HIV,   --阴、阳
                        t.C_ANTI_HCV,   --阴、阳
                        t.C_ANTI_HIV1_2,   --阴、阳
                        t.C_SYPHILIS,   --阴、阳
                        t.C_HBEAB,   --阴、阳
                        t.C_HBSAB,   --阴、阳
                        t.C_BLOOD_RH,   --阴、阳
                        t.C_BLOOD_TYPE,   --A, B, AB, O
                        t.B_ANTI_HIV,   --阴、阳
                        t.B_ANTI_HCV,   --阴、阳
                        t.B_ANTI_HIV1_2,   --阴、阳
                        t.B_SYPHILIS,   --阴、阳
                        t.B_HBEAB,   --阴、阳
                        t.B_HBSAB,   --阴、阳
                        t.B_HBCAB,   --阴、阳
                        t.B_HBEAG,   --阴、阳
                        t.B_HBSAG,   --阴、阳
                        t.B_PLATELET,   --
                        t.B_ALT,   --
                        t.B_HCT,   --
                        t.B_HB,   --
                        t.PAT_YUN,   --孕
                        t.BLOOD_ALLERGY,   --输血过敏反应
                        t.BLOOD_HISTORY,   --既往输血史
                        t.BLOOD_PROPERTY,   --输血性质：常规
                        t.BLOOD_UNIT,   --ml、袋
                        t.BLOOD_INGREDIENT,   --血浆、血细胞
                        t.BLOOD_RH,   --阴、阳
                        t.BLOOD_TYPE,   --A,B,AB,O
                        t.BLOOD_GOAL,   --用血目的
                        t.HELP_FEE,   --受血互助金
                        t.BED_NO,   --受血病人所在床位
                        t.C_HBCAB,   --阴、阳
                        t.C_HBEAG,   --阴、阳
                        t.C_HBSAG,   --阴、阳
                        t.C_PLATELET,   --
                        t.C_ALT,   --
                        t.APPLY_DATE,   --申请填写时间
                        t.BLOOD_SUM,   --输血总量
                        t.C_HCT,   --
                        t.C_HB,   --
                        t.B_BLOOD_RH,   --阴、阳
                        t.B_BLOOD_TYPE,   --A, B, AB, O
                        t.BLOOD_TABOO,   --描述受血者的过敏史及禁尽症
                        t.BLOOD_DIAGNOSE,   --诊断及输血适应症
                        t.PAT_PLACE,   --受血者属地
                        t.BIRTHDAY,   --受血者生日
                        t.PAT_SEX,   --受血者性别
                        t.PAT_NAME,   --受血者姓名
                        t.DEPT_CODE,   --申请科室代码
                        t.INP_NO,   --病人住院标识号
                        t.APPLY_NUM,   --顺序号
                        t.APPLY_ID,
                        t.VISIT_ID
                        FROM
                        BLOOD_APPLY  t   --

                        
                        WHERE  t.APPLY_ID='{0}'
                        
                        AND t.STATUS='1'
                        
                        AND t.SOURCE_FLAG='0'";
            #endregion

            strSQL = string.Format(strSQL, ApplyCode);
            DataSet ds = BaseEntityer.Db.GetDataSet(strSQL);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BLOOD_APPLY>(ds).ToList();
        }

        /// <summary>
        /// 插入申请表信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public int InsertBloodApply(BaseEntityer db, BLOOD_APPLY detail)
        {
            #region SQL
            string sql = @"INSERT INTO BLOOD_APPLY  t   --
                            (
                            t.STATUS,   --状态:1申请2核准3配血4发血
                            t.SOURCE_FLAG,   --来源标识:0住院1门诊
                            t.PATIENT_ID,   --病人ID
                            t.USE_DATE,   --预计用血时间
                            t.DEPT_NAME,   --申请科室名称
                            t.EXPEND10,   --扩展字段十
                            t.EXPEND9,   --扩展字段九
                            t.EXPEND8,   --扩展字段八
                            t.EXPEND7,   --扩展字段七
                            t.EXPEND6,   --扩展字段六
                            t.EXPEND5,   --扩展字段五
                            t.EXPEND4,   --扩展字段四
                            t.EXPEND3,   --扩展字段三
                            t.EXPEND2,   --扩展字段二
                            t.EXPEND1,   --扩展字段一
                            t.PRICE,   --划价标识 1-划价
                            t.DOCTOR,   --医师
                            t.PHYSICIAN,   --主治医师
                            t.DIRECTOR,   --科主任
                            t.DISCHARGE_ID,   --发血执行人
                            t.DISCHARGE_DATE,   --发血时间
                            t.EQUIP_ID,   --配血执行人
                            t.EQUIP_DATE,   --配血时间
                            t.GATHER_ID,   --核准人标识
                            t.GATHER_DATE,   --血库收到时间（核准时间）
                            t.C_MOMO,   --备注
                            t.C_FILTER,   --Ⅰ、Ⅱ、Ⅲ
                            t.PAT_AGE,   --受血者年龄
                            t.C_ANTI_HIV,   --阴、阳
                            t.C_ANTI_HCV,   --阴、阳
                            t.C_ANTI_HIV1_2,   --阴、阳
                            t.C_SYPHILIS,   --阴、阳
                            t.C_HBEAB,   --阴、阳
                            t.C_HBSAB,   --阴、阳
                            t.C_BLOOD_RH,   --阴、阳
                            t.C_BLOOD_TYPE,   --A, B, AB, O
                            t.B_ANTI_HIV,   --阴、阳
                            t.B_ANTI_HCV,   --阴、阳
                            t.B_ANTI_HIV1_2,   --阴、阳
                            t.B_SYPHILIS,   --阴、阳
                            t.B_HBEAB,   --阴、阳
                            t.B_HBSAB,   --阴、阳
                            t.B_HBCAB,   --阴、阳
                            t.B_HBEAG,   --阴、阳
                            t.B_HBSAG,   --阴、阳
                            t.B_PLATELET,   --
                            t.B_ALT,   --
                            t.B_HCT,   --
                            t.B_HB,   --
                            t.PAT_YUN,   --孕
                            t.BLOOD_ALLERGY,   --输血过敏反应
                            t.BLOOD_HISTORY,   --既往输血史
                            t.BLOOD_PROPERTY,   --输血性质：常规
                            t.BLOOD_UNIT,   --ml、袋
                            t.BLOOD_INGREDIENT,   --血浆、血细胞
                            t.BLOOD_RH,   --阴、阳
                            t.BLOOD_TYPE,   --A,B,AB,O
                            t.BLOOD_GOAL,   --用血目的
                            t.HELP_FEE,   --受血互助金
                            t.BED_NO,   --受血病人所在床位
                            t.C_HBCAB,   --阴、阳
                            t.C_HBEAG,   --阴、阳
                            t.C_HBSAG,   --阴、阳
                            t.C_PLATELET,   --
                            t.C_ALT,   --
                            t.APPLY_DATE,   --预约申请时间
                            t.BLOOD_SUM,   --输血总量
                            t.C_HCT,   --
                            t.C_HB,   --
                            t.B_BLOOD_RH,   --阴、阳
                            t.B_BLOOD_TYPE,   --A, B, AB, O
                            t.BLOOD_TABOO,   --描述受血者的过敏史及禁尽症
                            t.BLOOD_DIAGNOSE,   --诊断及输血适应症
                            t.PAT_PLACE,   --受血者属地
                            t.BIRTHDAY,   --受血者生日
                            t.PAT_SEX,   --受血者性别
                            t.PAT_NAME,   --受血者姓名
                            t.DEPT_CODE,   --申请科室代码
                            t.INP_NO,   --病人住院标识号
                            t.APPLY_NUM,   --顺序号
                            t.BLOOD_COMPONENT,  -- 血液成分
                            t.BLOOD_HELP_FEE,     --输血互助金 
                            t.PAT_CHAN,
                            t.VISIT_ID,
                            t.APPLY_ID
                            ) 
                            VALUES
                            (
                            '{0}',   --状态:1申请2核准3配血4发血
                            '{1}',   --来源标识:0住院1门诊
                            '{2}',   --病人ID
                            TO_DATE('{3}','YYYY-MM-DD HH24:MI:SS'),   --预计用血时间
                            '{4}',   --申请科室名称
                            '{5}',   --扩展字段十
                            '{6}',   --扩展字段九
                            '{7}',   --扩展字段八
                            '{8}',   --扩展字段七
                            '{9}',   --扩展字段六
                            '{10}',   --扩展字段五
                            '{11}',   --扩展字段四
                            '{12}',   --扩展字段三
                            '{13}',   --扩展字段二
                            '{14}',   --扩展字段一
                            '{15}',   --划价标识 1-划价
                            '{16}',   --医师
                            '{17}',   --主治医师
                            '{18}',   --科主任
                            '{19}',   --发血执行人
                            '{20}',   --TO_DATE('{20}','YYYY-MM-DD HH24:MI:SS'),   发血时间
                            '{21}',   --配血执行人
                            '{22}',   --TO_DATE('{22}','YYYY-MM-DD HH24:MI:SS'),   --配血时间
                            '{23}',   --核准人标识
                            '{24}',   --TO_DATE('{24}','YYYY-MM-DD HH24:MI:SS'),   --血库收到时间（核准时间）
                            '{25}',   --备注
                            '{26}',   --Ⅰ、Ⅱ、Ⅲ
                            '{27}',   --受血者年龄
                            '{28}',   --阴、阳
                            '{29}',   --阴、阳
                            '{30}',   --阴、阳
                            '{31}',   --阴、阳
                            '{32}',   --阴、阳
                            '{33}',   --阴、阳
                            '{34}',   --阴、阳
                            '{35}',   --A, B, AB, O
                            '{36}',   --阴、阳
                            '{37}',   --阴、阳
                            '{38}',   --阴、阳
                            '{39}',   --阴、阳
                            '{40}',   --阴、阳
                            '{41}',   --阴、阳
                            '{42}',   --阴、阳
                            '{43}',   --阴、阳
                            '{44}',   --阴、阳
                            '{45}',   --
                            '{46}',   --
                            '{47}',   --
                            '{48}',   --
                            '{49}',   --孕
                            '{50}',   --输血过敏反应
                            '{51}',   --既往输血史
                            '{52}',   --输血性质：常规
                            '{53}',   --ml、袋
                            '{54}',   --血浆、血细胞
                            '{55}',   --阴、阳
                            '{56}',   --A,B,AB,O
                            '{57}',   --用血目的
                            '{58}',   --受血互助金
                            '{59}',   --受血病人所在床位
                            '{60}',   --阴、阳
                            '{61}',   --阴、阳
                            '{62}',   --阴、阳
                            '{63}',   --
                            '{64}',   --
                            TO_DATE('{65}','YYYY-MM-DD HH24:MI:SS'),   --预约申请时间
                            '{66}',   --输血总量
                            '{67}',   --
                            '{68}',   --
                            '{69}',   --阴、阳
                            '{70}',   --A, B, AB, O
                            '{71}',   --描述受血者的过敏史及禁尽症
                            '{72}',   --诊断及输血适应症
                            '{73}',   --受血者属地
                            {74},   --受血者生日
                            '{75}',   --受血者性别
                            '{76}',   --受血者姓名
                            '{77}',   --申请科室代码
                            '{78}',   --病人住院标识号
                            '{79}',   --顺序号
                            '{80}',   --输血成分
                            '{81}',    --输血互助金
                            '{82}',
                            '{83}',
                            '{84}'
                            )  ";
            #endregion

            string value = GetSequence("BLOOD_APPLY_ID").ToString();

            if (value.Length < 10)
            {
                value = value.PadLeft(10, '0');
            }

            string birthday = string.Empty;
            if (detail.BIRTHDAY.ToString() == "0001-1-1 00:00")
            {
                birthday = "''";
            }
            else
            {
                birthday = "TO_DATE('" + detail.BIRTHDAY + "','YYYY-MM-DD HH24:MI:SS')";
            }

            //放置实体
            object[] obs = new object[] 
               {
                            detail.STATUS,   //状态:1申请2核准3配血4发血
                            detail.SOURCE_FLAG,   //来源标识:0住院1门诊
                            detail.PATIENT_ID,   //病人ID
                            detail.USE_DATE,   //预计用血时间
                            detail.DEPT_NAME,   //申请科室名称
                            detail.EXPEND10,   //扩展字段十
                            detail.EXPEND9,   //扩展字段九
                            detail.EXPEND8,   //扩展字段八
                            detail.EXPEND7,   //扩展字段七
                            detail.EXPEND6,   //扩展字段六
                            detail.EXPEND5,   //扩展字段五
                            detail.EXPEND4,   //扩展字段四
                            detail.EXPEND3,   //扩展字段三
                            detail.EXPEND2,   //扩展字段二
                            detail.EXPEND1,   //扩展字段一
                            detail.PRICE,   //划价标识 1-划价
                            detail.DOCTOR,   //医师
                            detail.PHYSICIAN,   //主治医师
                            detail.DIRECTOR,   //科主任
                            detail.DISCHARGE_ID,   //发血执行人
                            null,//detail.DISCHARGE_DATE,   //发血时间
                            detail.EQUIP_ID,   //配血执行人
                            null,//detail.EQUIP_DATE,   //配血时间
                            detail.GATHER_ID,   //核准人标识
                            null,//detail.GATHER_DATE,   //血库收到时间（核准时间）
                            detail.C_MOMO,   //备注
                            detail.C_FILTER,   //Ⅰ、Ⅱ、Ⅲ
                            detail.PAT_AGE,   //受血者年龄
                            detail.C_ANTI_HIV,   //阴、阳
                            detail.C_ANTI_HCV,   //阴、阳
                            detail.C_ANTI_HIV1_2,   //阴、阳
                            detail.C_SYPHILIS,   //阴、阳
                            detail.C_HBEAB,   //阴、阳
                            detail.C_HBSAB,   //阴、阳
                            detail.C_BLOOD_RH,   //阴、阳
                            detail.C_BLOOD_TYPE,   //A, B, AB, O
                            detail.B_ANTI_HIV,   //阴、阳
                            detail.B_ANTI_HCV,   //阴、阳
                            detail.B_ANTI_HIV1_2,   //阴、阳
                            detail.B_SYPHILIS,   //阴、阳
                            detail.B_HBEAB,   //阴、阳
                            detail.B_HBSAB,   //阴、阳
                            detail.B_HBCAB,   //阴、阳
                            detail.B_HBEAG,   //阴、阳
                            detail.B_HBSAG,   //阴、阳
                            detail.B_PLATELET,   //
                            detail.B_ALT,   //
                            detail.B_HCT,   //
                            detail.B_HB,   //
                            detail.PAT_YUN,   //孕
                            detail.BLOOD_ALLERGY,   //输血过敏反应
                            detail.BLOOD_HISTORY,   //既往输血史
                            detail.BLOOD_PROPERTY,   //输血性质：常规
                            detail.BLOOD_UNIT,   //ml、袋
                            detail.BLOOD_INGREDIENT,   //血浆、血细胞
                            detail.BLOOD_RH,   //阴、阳
                            detail.BLOOD_TYPE,   //A,B,AB,O
                            detail.BLOOD_GOAL,   //用血目的
                            detail.HELP_FEE,   //受血互助金
                            detail.BED_NO,   //受血病人所在床位
                            detail.C_HBCAB,   //阴、阳
                            detail.C_HBEAG,   //阴、阳
                            detail.C_HBSAG,   //阴、阳
                            detail.C_PLATELET,   //
                            detail.C_ALT,   //
                            detail.APPLY_DATE,  // 申请单生成时间
                            detail.BLOOD_SUM,   //输血总量
                            detail.C_HCT,   //
                            detail.C_HB,   //
                            detail.B_BLOOD_RH,   //阴、阳
                            detail.B_BLOOD_TYPE,   //A, B, AB, O
                            detail.BLOOD_TABOO,   //描述受血者的过敏史及禁尽症
                            detail.BLOOD_DIAGNOSE,   //诊断及输血适应症
                            detail.PAT_PLACE,   //受血者属地
                            birthday,  //受血者生日
                            detail.PAT_SEX,   //受血者性别
                            detail.PAT_NAME,   //受血者姓名
                            detail.DEPT_CODE,   //申请科室代码
                            detail.INP_NO,   //病人住院标识号
                            detail.APPLY_NUM,   //顺序号
                            detail.BLOOD_COMPONENT, // 输血成分
                            detail.BLOOD_HELP_FEE,  // 输血互助金
                            detail.PAT_CHAN, //产
                            detail.VISIT_ID,
                            value
               };

            sql = string.Format(sql, obs);

            return db.ExecuteNonQuery(sql);
        }
        #endregion

        #region 更改申请表信息，添加复查结果
        /// <summary>
        /// 更改申请表信息，添加复查结果
        /// </summary>
        /// <param name="db"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public int UpdateBloodApply(BaseEntityer db, BLOOD_APPLY detail)
        {
            string sql = @"UPDATE BLOOD_APPLY t --
                           SET t.STATUS        = '{0}', --状态:1申请2核准3配血4发血
                               t.GATHER_ID     = '{1}', --核准人标识
                               t.GATHER_DATE   = TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --血库收到时间（核准时间）
                               t.C_MOMO        = '{3}', --备注
                               t.C_FILTER      = '{4}', --Ⅰ、Ⅱ、Ⅲ
                               t.C_ANTI_HIV    = '{5}', --阴、阳
                               t.C_ANTI_HCV    = '{6}', --阴、阳
                               t.C_ANTI_HIV1_2 = '{7}', --阴、阳
                               t.C_SYPHILIS    = '{8}', --阴、阳
                               t.C_HBEAB       = '{9}', --阴、阳
                               t.C_HBSAB       = '{10}', --阴、阳
                               t.C_BLOOD_RH    = '{11}', --阴、阳
                               t.C_BLOOD_TYPE  = '{12}', --A, B, AB, O
                               t.C_HBSAG       = '{13}', --阴、阳
                               t.C_HB          = '{14}',
                               t.C_HCT         = '{15}',
                               t.C_PLATELET    = '{16}',
                               t.C_ALT         = '{17}'
                         WHERE t.apply_id = '{18}' --申请ID";

            object[] param = new object[] { 
                                            detail.STATUS,//状态:1申请2核准3配血4发血
                                            detail.GATHER_ID,//核准人标识
                                            detail.GATHER_DATE, //血库收到时间（核准时间）
                                            detail.C_MOMO,   //备注
                                            detail.C_FILTER,   //Ⅰ、Ⅱ、Ⅲ
                                            detail.C_ANTI_HIV,   //阴、阳
                                            detail.C_ANTI_HCV,   //阴、阳
                                            detail.C_ANTI_HIV1_2,   //阴、阳
                                            detail.C_SYPHILIS,   //阴、阳
                                            detail.C_HBEAB,   //阴、阳
                                            detail.C_HBSAB,   //阴、阳
                                            detail.C_BLOOD_RH,   //阴、阳
                                            detail.C_BLOOD_TYPE,   //A, B, AB, O 
                                            detail.C_HBSAG, //阴、阳
                                            detail.C_HB,
                                            detail.C_HCT,
                                            detail.C_PLATELET,
                                            detail.C_ALT,
                                            detail.APPLY_ID //病人ID";
                                         };

            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        #endregion

        /// <summary>
        /// 2014-3-3 BY LI 删除输血申请单项目
        /// </summary>
        /// <param name="patientID">患者标识</param>
        /// <param name="scheduleID">手术安排标识</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DelBloodSchedule(string patientID, string visitId, int scheduleID, BaseEntityer db)
        {
            string sql = @"DELETE FROM BLOOD_APPLY O
                         WHERE O.PATIENT_ID = '{0}' AND O.VISIT_ID = {1}
                           AND O.APPLY_NUM = {2} ";
            object[] param = new object[] { patientID, visitId, scheduleID };
            sql = string.Format(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2014-8-25 BY XU 查询最大输血申请号
        /// </summary>
        /// <param name="patientID">患者标识</param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int GetBloodMaxScheduleID(string patient_id, string visit_id, BaseEntityer db)
        {
            string sql = @"SELECT MAX(O.APPLY_NUM)
                          FROM BLOOD_APPLY O where o.PATIENT_ID = '{0}' and o.VISIT_ID = '{1}'";
            sql = string.Format(sql, patient_id, visit_id);
            var schedule = db.ExecuteScalar(sql);
            if (schedule != DBNull.Value)
                return int.Parse(schedule.ToString());
            else return 0;
        }

        /// <summary>
        /// 读取血型字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BLOOD_TYPE_DICT> GetBlood_TypeInfo()
        {
            string sql = @"select * from  BLOOD_TYPE_DICT";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BLOOD_TYPE_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取用血目的字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BLOOD_GOAL_DICT> GetBlood_GoalInfo()
        {
            string sql = @"select * from  BLOOD_GOAL_DICT";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BLOOD_GOAL_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取血液成分字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BLOOD_COMPONENT_DICT> GetBlood_ComponentInfo()
        {
            string sql = @"select * from  BLOOD_COMPONENT_DICT";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BLOOD_COMPONENT_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取输血性质字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BLOOD_PROPERTY_DICT> GetBlood_PropertyInfo()
        {
            string sql = @"select * from  BLOOD_PROPERTY_DICT";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BLOOD_PROPERTY_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取互助金字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BLOOD_HELP_FEE_DICT> GetBlood_Help_FeeInfo()
        {
            string sql = @"select * from  BLOOD_HELP_FEE_DICT";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BLOOD_HELP_FEE_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取用血量单位字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.BLOOD_UNIT_DICT> GetBlood_UnitInfo()
        {
            string sql = @"select * from  BLOOD_UNIT_DICT";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BLOOD_UNIT_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        #endregion

        #region 护士站的申请记录 by  dong_w

        /// <summary>
        /// 获得用血申请记录根据时间范围和执行科室
        /// </summary>
        /// <param name="begDate"></param>
        /// <param name="endDate"></param>
        /// <param name="applyDept"></param>
        /// <returns>用血申请记录</returns>
        public IList<BLOOD_APPLY> QueryBloodApplyRecordByDateAndDept(string begDate, string endDate, string applyDept)
        {
            string sql = @" SELECT
                        t.STATUS,   --状态:1申请2核准3配血4发血
                        t.SOURCE_FLAG,   --来源标识:0住院1门诊
                        t.PATIENT_ID,   --病人ID
                        t.USE_DATE,   --预计用血时间
                        t.DEPT_NAME,   --申请科室名称
                        t.EXPEND10,   --扩展字段十
                        t.EXPEND9,   --扩展字段九
                        t.EXPEND8,   --扩展字段八
                        t.EXPEND7,   --扩展字段七
                        t.EXPEND6,   --扩展字段六
                        t.EXPEND5,   --扩展字段五
                        t.EXPEND4,   --扩展字段四
                        t.EXPEND3,   --扩展字段三
                        t.EXPEND2,   --扩展字段二
                        t.EXPEND1,   --扩展字段一
                        t.PRICE,   --划价标识 1-划价
                        t.DOCTOR,   --医师
                        t.PHYSICIAN,   --主治医师
                        t.DIRECTOR,   --科主任
                        t.DISCHARGE_ID,   --发血执行人
                        t.DISCHARGE_DATE,   --发血时间
                        t.EQUIP_ID,   --配血执行人
                        t.EQUIP_DATE,   --配血时间
                        t.GATHER_ID,   --核准人标识
                        t.GATHER_DATE,   --血库收到时间（核准时间）
                        t.C_MOMO,   --备注
                        t.C_FILTER,   --Ⅰ、Ⅱ、Ⅲ
                        t.PAT_AGE,   --受血者年龄
                        t.C_ANTI_HIV,   --阴、阳
                        t.C_ANTI_HCV,   --阴、阳
                        t.C_ANTI_HIV1_2,   --阴、阳
                        t.C_SYPHILIS,   --阴、阳
                        t.C_HBEAB,   --阴、阳
                        t.C_HBSAB,   --阴、阳
                        t.C_BLOOD_RH,   --阴、阳
                        t.C_BLOOD_TYPE,   --A, B, AB, O
                        t.B_ANTI_HIV,   --阴、阳
                        t.B_ANTI_HCV,   --阴、阳
                        t.B_ANTI_HIV1_2,   --阴、阳
                        t.B_SYPHILIS,   --阴、阳
                        t.B_HBEAB,   --阴、阳
                        t.B_HBSAB,   --阴、阳
                        t.B_HBCAB,   --阴、阳
                        t.B_HBEAG,   --阴、阳
                        t.B_HBSAG,   --阴、阳
                        t.B_PLATELET,   --
                        t.B_ALT,   --
                        t.B_HCT,   --
                        t.B_HB,   --
                        --t.PAT_STATUS,   --0孕0产
                        t.BLOOD_ALLERGY,   --输血过敏反应
                        t.BLOOD_HISTORY,   --既往输血史
                        t.BLOOD_PROPERTY,   --输血性质：常规
                        t.BLOOD_UNIT,   --ml、袋
                        t.BLOOD_INGREDIENT,   --血浆、血细胞
                        t.BLOOD_RH,   --阴、阳
                        t.BLOOD_TYPE,   --A,B,AB,O
                        t.BLOOD_GOAL,   --用血目的
                        t.HELP_FEE,   --受血互助金
                        t.BED_NO,   --受血病人所在床位
                        t.C_HBCAB,   --阴、阳
                        t.C_HBEAG,   --阴、阳
                        t.C_HBSAG,   --阴、阳
                        t.C_PLATELET,   --
                        t.C_ALT,   --
                        t.APPLY_DATE,   --预约申请时间
                        t.BLOOD_SUM,   --输血总量
                        t.C_HCT,   --
                        t.C_HB,   --
                        t.B_BLOOD_RH,   --阴、阳
                        t.B_BLOOD_TYPE,   --A, B, AB, O
                        t.BLOOD_TABOO,   --描述受血者的过敏史及禁尽症
                        t.BLOOD_DIAGNOSE,   --诊断及输血适应症
                        t.PAT_PLACE,   --受血者属地
                        t.BIRTHDAY,   --受血者生日
                        t.PAT_SEX,   --受血者性别
                        t.PAT_NAME,   --受血者姓名
                        t.DEPT_CODE,   --申请科室代码
                        t.INP_NO,   --病人住院标识号
                        t.APPLY_NUM,   --顺序号
                        t.APPLY_ID  -- 申请单号
                        FROM
                        BLOOD_APPLY  t   --
                        WHERE t.apply_date >= to_date('{0}', 'yyyy-MM-dd hh24:mi:ss:')
                        AND t.apply_date <= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss:')
                        AND t.dept_code = '{2}'
                        AND t.status = '1'
                        AND t.source_flag = '0'
                        ";
            object[] param = new object[] { begDate, endDate, applyDept };
            sql = string.Format(sql, param);

            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.BLOOD_APPLY>(ds).ToList();
        }
        #endregion

    }
}
