using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace HisDBLayer
{
    /// <summary>
    /// [功能描述: 住院申请单]
    /// [创 建 者: 张顶]
    /// [创建时间: 2014-05-21]
    /// </summary>
    public class ApplyManager
    {
        private string err;

        public string Err
        {
            get { return err; }
            set { err = value; }
        }

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

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public int Insert(HisCommon.DataEntity.fin_opr_apply apply)
        {
            #region SQL
            string strSQL = @" INSERT INTO FIN_OPR_APPLY t --住院申请单
                              (t.APPLY_NO, --申请编号
                               t.CLINIC_NO, --门诊号
                               t.APPLY_DATE, --申请日期
                               t.OPER_CODE, --医生编号
                               t.OPER_DATE, --操作日期
                               t.APPLY_DIAGNOSE_ID1, --诊断编号
                               t.APPLY_DIAGNOSE_NAME1, --诊断名称
                               t.APPLY_DEPT, --申请入院科室
                               t.REMARK, --备注
                               t.tel, --电话
                               t.APPLY_DIAGNOSE_ID2, --诊断编号
                               t.APPLY_DIAGNOSE_NAME2, --诊断名称
                               t.APPLY_DIAGNOSE_ID3, --诊断编号
                               t.APPLY_DIAGNOSE_NAME3 --诊断名称
                               )
                            VALUES
                              ('{0}', --申请编号
                               '{1}', --门诊号
                               TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --申请日期
                               '{3}', --医生编号
                               sysdate, --操作日期
                               '{4}', --诊断编号
                               '{5}', --诊断名称
                               '{6}', --申请入院科室
                               '{7}', --备注
                               '{8}', --电话
                               '{9}', --诊断编号
                               '{10}', --诊断名称
                               '{11}', --诊断编号
                               '{12}' --诊断名称
                                )";
            #endregion

            try
            {
                strSQL = string.Format(strSQL, apply.Apply_no, apply.Clinic_no, apply.Apply_date, apply.Oper_code, apply.Apply_diagnose_id1,
                    apply.Apply_diagnose_name1, apply.Apply_dept, apply.Remark, apply.Tel, apply.Apply_diagnose_id2, apply.Apply_diagnose_name2,
                    apply.Apply_diagnose_id3, apply.Apply_diagnose_name3);
            }
            catch (Exception ex)
            {
                this.err = "匹配SQL出错" + ex.Message;
                return -1;
            }

            if (BaseEntityer.Db.ZDExecNonQuery(strSQL) == -1)
            {
                this.err = "保存失败！" + BaseEntityer.Db.Err;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 门诊医生修改
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public int Update(HisCommon.DataEntity.fin_opr_apply apply)
        {
            string strSQL = @" UPDATE FIN_OPR_APPLY t --住院申请单
                                   SET t.CLINIC_NO            = '{1}', --门诊号
                                       t.APPLY_DATE           = TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --申请日期
                                       t.OPER_CODE            = '{3}', --医生编号
                                       t.OPER_DATE            = TO_DATE('{4}', 'YYYY-MM-DD HH24:MI:SS'), --操作日期
                                       t.APPLY_DIAGNOSE_ID1   = '{5}', --诊断编号
                                       t.APPLY_DIAGNOSE_NAME1 = '{6}', --诊断名称
                                       t.REMARK               = '{7}', --备注
                                       t.APPLY_DEPT           = '{8}', --申请入院科室
                                       t.tel                  = '{9}',
                                       t.apply_diagnose_id2   = '{10}', --诊断编号
                                       t.apply_diagnose_name2 = '{11}', --诊断名称
                                       t.apply_diagnose_id3   = '{12}', --诊断编号
                                       t.apply_diagnose_name3 = '{13}' --诊断名称
                                 WHERE t.clinic_no = '{0}' ";
            try
            {
                strSQL = string.Format(strSQL, apply.Apply_no, apply.Clinic_no, apply.Apply_date, apply.Oper_code, apply.Oper_date,
                    apply.Apply_diagnose_id1, apply.Apply_diagnose_name1, apply.Remark, apply.Apply_dept, apply.Tel, apply.Apply_diagnose_id2,
                    apply.Apply_diagnose_name2, apply.Apply_diagnose_id3, apply.Apply_diagnose_name3);
            }
            catch (Exception ex)
            {
                this.err = "匹配SQL出错" + ex.Message;
                return -1;
            }

            if (BaseEntityer.Db.ZDExecNonQuery(strSQL) == -1)
            {
                this.err = "修改失败！" + BaseEntityer.Db.Err;
                return 1;
            }

            return 1;
        }

        /// <summary>
        /// 患者收住院
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public int Update2(HisCommon.DataEntity.fin_opr_apply apply)
        {
            string strSQL = @" UPDATE FIN_OPR_APPLY t --住院申请单
                                   SET t.IN_STATE     = '1', --收住状态 0未收住 1已收住
                                       t.IN_OPER_CODE = '{1}', --收住操作员
                                       t.IN_OPER_DATE = TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS') --收住日期
                                 WHERE t.apply_no = '{0}' ";
            try
            {
                strSQL = string.Format(strSQL, apply.Apply_no, apply.In_oper_code, apply.In_oper_date);
            }
            catch (Exception ex)
            {
                this.err = "匹配SQL出错" + ex.Message;
                return -1;
            }

            if (BaseEntityer.Db.ZDExecNonQuery(strSQL) == -1)
            {
                this.err = "修改失败！" + BaseEntityer.Db.Err;
                return 1;
            }

            return 1;
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public int Delete(HisCommon.DataEntity.fin_opr_apply apply)
        {
            string strSQL = @" update fin_opr_apply t
                               set t.valid         = '0',
                                   t.CANCEL_CODE   = '{1}', --作废人
                                   t.CANCEL_DATE   = TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --作废日期
                                   t.cancel_reason = '{3}' --作废原因
                             where t.apply_no = '{0}' ";
            try
            {
                strSQL = string.Format(strSQL, apply.Apply_no, apply.Cancel_code, apply.Cancel_date, apply.Cancel_reason);
            }
            catch(Exception ex)
            {
                this.err = "匹配SQL出错" + ex.Message;
                return -1;
            }

            if (BaseEntityer.Db.ZDExecNonQuery(strSQL) == -1)
            {
                this.err = "修改失败！" + BaseEntityer.Db.Err;
                return 1;
            }

            return 1;
        }

        /// <summary>
        /// 查询所有申请患者
        /// </summary>
        /// <param name="isValid">1：正常 0：作废 All：全部</param>
        /// <param name="inState">1: 已收住 0：未收住 All：全部</param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.fin_opr_apply> QueryAll(string isValid, string inState)
        {
            List<HisCommon.DataEntity.fin_opr_apply> applys = new List<HisCommon.DataEntity.fin_opr_apply>();

            string strSQL = @" SELECT t.APPLY_NO, --申请编号（门诊流水号）
                               t.APPLY_DATE, --申请日期
                               t.OPER_CODE, --医生编号
                               t.OPER_DATE, --操作日期
                               t.APPLY_DIAGNOSE_ID1, --诊断编号
                               t.APPLY_DIAGNOSE_NAME1, --诊断名称
                               t.APPLY_DEPT, --申请入院科室
                               t.REMARK, --备注
                               t.VALID, --状态 1正常0作废
                               t.CANCEL_CODE, --作废人
                               t.CANCEL_DATE, --作废日期
                               t.IN_STATE, --收住状态 0未收住 1已收住
                               t.IN_OPER_CODE, --收住操作员
                               t.IN_OPER_DATE, --收住日期
                               t.CANCEL_REASON, --作废原因
                               t.tel, --电话
                               t.apply_diagnose_id2, --诊断编号
                               t.apply_diagnose_name2, --诊断名称
                               t.apply_diagnose_id3, --诊断编号
                               t.apply_diagnose_name3, --诊断名称
                               m.name, --姓名
                               m.sex, --性别
                               m.age, --年龄
                               (select n.user_name
                                  from users_staff_dict n
                                 where n.user_id = t.oper_code) DoctorName, --医生名称
                               (select w.dept_name from dept_dict w where w.dept_code = t.oper_code) deptName --科室名称
                          FROM FIN_OPR_APPLY t, clinic_master m --住院申请单
                         where (t.valid = '{0}' or 'All' = '{0}')
                            or (t.in_state = '{1}' or 'All' = '{1}')
                           and t.clinic_no = m.patient_id ";

            strSQL = string.Format(strSQL, isValid, inState);

            DbDataReader dr = BaseEntityer.Db.ZDExecReader(strSQL);
            if (dr != null)
            {
                while (dr.Read())
                {
                    HisCommon.DataEntity.fin_opr_apply apply = new HisCommon.DataEntity.fin_opr_apply();
                    //申请编号
                    apply.Apply_no = Convert.ToInt32(dr[0].ToString());
                    //申请日期
                    apply.Apply_date = Convert.ToDateTime(dr[1].ToString());
                    //医生编号
                    apply.Oper_code = dr[2].ToString();
                    //操作日期
                    apply.Oper_date = Convert.ToDateTime(dr[3].ToString());
                    //诊断编号
                    apply.Apply_diagnose_id1 = dr[4].ToString();
                    //诊断名称
                    apply.Apply_diagnose_name1 = dr[5].ToString();
                    //申请入院科室
                    apply.Apply_dept = dr[6].ToString();
                    //备注
                    apply.Remark = dr[7].ToString();
                    //状态 1正常0作废
                    apply.Valid = dr[8].ToString() == "1" ? true : false;
                    //作废人
                    apply.Cancel_code = dr[9].ToString();
                    //作废日期
                    apply.Cancel_date = string.IsNullOrEmpty(dr[10].ToString()) ? DateTime.MinValue : Convert.ToDateTime(dr[10].ToString());
                    //收住状态 0未收住 1已收住
                    apply.In_state = Convert.ToInt32(dr[11].ToString());
                    //收住操作员
                    apply.In_oper_code = dr[12].ToString();
                    //收住日期
                    apply.In_oper_date = string.IsNullOrEmpty(dr[13].ToString()) ? DateTime.MinValue : Convert.ToDateTime(dr[13].ToString());
                    //作废原因
                    apply.Cancel_reason = dr[14].ToString();
                    //电话
                    apply.Tel = dr[15].ToString();
                    //诊断编号
                    apply.Apply_diagnose_id2 = dr[16].ToString();
                    //诊断名称
                    apply.Apply_diagnose_name2 = dr[17].ToString();
                    //诊断编号
                    apply.Apply_diagnose_id3 = dr[18].ToString();
                    //诊断名称
                    apply.Apply_diagnose_name3 = dr[19].ToString();
                    //姓名
                    apply.Clinic_master.NAME = dr[20].ToString();
                    //性别
                    apply.Clinic_master.SEX = dr[21].ToString();
                    //年龄
                    apply.Clinic_master.AGE = Convert.ToInt32(dr[22].ToString());
                    //医生名称
                    apply.Clinic_master.DOCTOR = dr[23].ToString();
                    //科室名称
                    apply.Clinic_master.VISIT_DEPT = dr[24].ToString();
                    applys.Add(apply);
                }
                if (!dr.IsClosed)
                    dr.Close();
            }
            return applys;
        }

        /// <summary>
        /// 根据门诊号查询收住信息
        /// </summary>
        /// <param name="applyID"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public HisCommon.DataEntity.fin_opr_apply QueryApplyByID(string applyID)
        {
            HisCommon.DataEntity.fin_opr_apply apply = new HisCommon.DataEntity.fin_opr_apply();

            string strSQL = @" SELECT t.APPLY_NO, --申请编号（门诊流水号）
                                   t.APPLY_DATE, --申请日期
                                   t.OPER_CODE, --医生编号
                                   t.OPER_DATE, --操作日期
                                   t.APPLY_DIAGNOSE_ID1, --诊断编号
                                   t.APPLY_DIAGNOSE_NAME1, --诊断名称
                                   t.APPLY_DEPT, --申请入院科室
                                   t.REMARK, --备注
                                   t.VALID, --状态 1正常0作废
                                   t.CANCEL_CODE, --作废人
                                   t.CANCEL_DATE, --作废日期
                                   t.IN_STATE, --收住状态 0未收住 1已收住
                                   t.IN_OPER_CODE, --收住操作员
                                   t.IN_OPER_DATE, --收住日期
                                   t.CANCEL_REASON, --作废原因
                                   t.tel, --电话
                                   t.apply_diagnose_id2, --诊断编号
                                   t.apply_diagnose_name2, --诊断名称
                                   t.apply_diagnose_id3, --诊断编号
                                   t.apply_diagnose_name3, --诊断名称
                                   m.name, --姓名
                                   m.sex, --性别
                                   m.age, --年龄
                                   (select n.user_name
                                      from users_staff_dict n
                                     where n.user_id = t.oper_code) DoctorName, --医生名称
                                   (select w.dept_name from dept_dict w where w.dept_code = t.oper_code) deptName --科室名称
                              FROM FIN_OPR_APPLY t, clinic_master m --住院申请单
                             WHERE t.CLINIC_NO = '{0}'
                               and t.valid = '1'
                               and t.clinic_no = m.patient_id ";

            strSQL = string.Format(strSQL, applyID);

            DbDataReader dr = BaseEntityer.Db.ZDExecReader(strSQL);
            if (dr != null)
            {
                while (dr.Read())
                {
                    //申请编号
                    apply.Apply_no = Convert.ToInt32(dr[0].ToString());
                    //申请日期
                    apply.Apply_date = Convert.ToDateTime(dr[1].ToString());
                    //医生编号
                    apply.Oper_code = dr[2].ToString();
                    //操作日期
                    apply.Oper_date = Convert.ToDateTime(dr[3].ToString());
                    //诊断编号
                    apply.Apply_diagnose_id1 = dr[4].ToString();
                    //诊断名称
                    apply.Apply_diagnose_name1 = dr[5].ToString();
                    //申请入院科室
                    apply.Apply_dept = dr[6].ToString();
                    //备注
                    apply.Remark = dr[7].ToString();
                    //状态 1正常0作废
                    apply.Valid = dr[8].ToString() == "1" ? true : false;
                    //作废人
                    apply.Cancel_code = dr[9].ToString();
                    //作废日期
                    apply.Cancel_date = string.IsNullOrEmpty(dr[10].ToString()) ? DateTime.MinValue : Convert.ToDateTime(dr[10].ToString());
                    //收住状态 0未收住 1已收住
                    apply.In_state = Convert.ToInt32(dr[11].ToString());
                    //收住操作员
                    apply.In_oper_code = dr[12].ToString();
                    //收住日期
                    apply.In_oper_date = string.IsNullOrEmpty(dr[13].ToString()) ? DateTime.MinValue : Convert.ToDateTime(dr[13].ToString());
                    //作废原因
                    apply.Cancel_reason = dr[14].ToString();
                    //电话
                    apply.Tel = dr[15].ToString();
                    //诊断编号
                    apply.Apply_diagnose_id2 = dr[16].ToString();
                    //诊断名称
                    apply.Apply_diagnose_name2 = dr[17].ToString();
                    //诊断编号
                    apply.Apply_diagnose_id3 = dr[18].ToString();
                    //诊断名称
                    apply.Apply_diagnose_name3 = dr[19].ToString();
                    //姓名
                    apply.Clinic_master.NAME = dr[20].ToString();
                    //性别
                    apply.Clinic_master.SEX = dr[21].ToString();
                    //年龄
                    apply.Clinic_master.AGE = Convert.ToInt32(dr[22].ToString());
                    //医生名称
                    apply.Clinic_master.DOCTOR = dr[23].ToString();
                    //科室名称
                    apply.Clinic_master.VISIT_DEPT = dr[24].ToString();
                }
                if (!dr.IsClosed)
                    dr.Close();
            }
            return apply;
        }
    }
}
