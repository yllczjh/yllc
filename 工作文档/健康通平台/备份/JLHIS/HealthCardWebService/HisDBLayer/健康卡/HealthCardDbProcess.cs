using HisCommon.DataEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HisDBLayer
{
    /// <summary>
    /// 居民健康的业务处理类
    /// </summary>
    public class HealthCardDbProcess
    {
        #region 变量
        HisDBLayer.OutpFeeManager outFeeManager = new HisDBLayer.OutpFeeManager();
        HisDBLayer.MedicalInsurance medicalmanager = new HisDBLayer.MedicalInsurance();
 
        #endregion

        #region 数据库访问公有方法
        public List<Healthcard_HosInfo> GetHosInfo(string hosID)
        {
            string sql = "SELECT t.HOS_ID,  t.NAME,  t.SHORT_NAME, t.ADDRESS,t.TEL, t.WEBSITE,t.WEIBO,t.LEVELS as LEVEL1, t.DESCS as  DESC1,  t.SPECIAL, t.LONGITUDE,  t.LATITUDE, t.MAX_REG_DAYS, t.START_REG_TIME, t.END_REG_TIME,  t.STOP_BOOK_TIMEA,  t.STOP_BOOK_TIMEP FROM Healthcard_HosInfo t where t.hos_id='{0}'";
            sql = string.Format(sql, hosID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<Healthcard_HosInfo>(ds).ToList();
        }

        public int InsertHealthPatInfo(HEALTHCARD_PATIENT_INFO _PATIENT_INFO, ref string patientID)
        {
            patientID = Common.GetSequence("patient_id_seq").PadLeft(10, '0');
            string sql = @"INSERT into HEALTHCARD_PATIENT_INFO
  (PATIENT_ID,
   HOS_ID,
   MOBILE,
   ADDRESS,
   HEALTH_CARD_ID,
   PATIENT_ID_TYPE,
   PATIENT_ID_CARD,
   PATIENT_NAME,SEX,BIRTHDAY)
VALUES
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}','{8}','{9}')";

            sql = string.Format(sql, patientID, _PATIENT_INFO.HOS_ID, _PATIENT_INFO.Mobile, _PATIENT_INFO.Address, _PATIENT_INFO.HEALTH_CARD_ID, _PATIENT_INFO.PATIENT_ID_TYPE, _PATIENT_INFO.PATIENT_ID_CARD, _PATIENT_INFO.PATIENT_NAME, _PATIENT_INFO.Sex, _PATIENT_INFO.BirthDay);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        public string GetHealthPatID(string healthCardID)
        {
            string sql = "select  c.patient_id from  HEALTHCARD_PATIENT_INFO c where c.health_card_id='{0}'";

            sql = string.Format(sql, healthCardID);
            DataTable dataTable = BaseEntityer.Db.GetDataTable(sql);

            if (dataTable == null || dataTable.Rows.Count <= 0)
                return "";
            else
            {
                return dataTable.Rows[0][0].ToString();
            }
        }

        public HEALTHCARD_PATIENT_INFO GetHealthPatInfo(string patientID)
        {
            string sql = "select   c.* from  HEALTHCARD_PATIENT_INFO c where c.patient_id='{0}'";

            sql = string.Format(sql, patientID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var lst= DataSetToEntity.DataSetToT<HEALTHCARD_PATIENT_INFO>(ds).ToList();

            if (lst == null || lst.Count <= 0)
                return null;
            else
                return lst.First();
        }

        public List<HealthCardDeptInfo> GetDeptInfo(string deptID)
        {
            //string sql = @"select c.dept_code as DEPT_ID,
            //                           c.dept_name as DEPT_NAME,
            //                           '-1' as PARENT_ID,
            //                           '' as ""DESC"",
            //                           '' AS EXPERTISE,
            //                           '' as ""LEVEL"",
            //                           '' AS ADDRESS,
            //                           '1' as status
            //                      from dept_dict c
            //                     where  (c.dept_code = '{0}'
            //                                or '-1' = '{0}') and c.is_spicu = '1'";

            string sql = @"select 'DEPT_ID' as DEPT_ID,
                                       'DEPT_NAME' as DEPT_NAME,
                                       '-1' as PARENT_ID,
                                       '' as ""DESC"",
                                       '' AS EXPERTISE,
                                       '' as ""LEVEL"",
                                       '' AS ADDRESS,
                                       '1' as status
                                  from dual c";
            //sql = string.Format(sql, deptID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HealthCardDeptInfo>(ds).ToList();
        }

        public List<HealthCardDoctorInfo> GetDoctorInfo(string deptID, string doctorID)
        {
            string sql = @"select c.user_dept       as DEPT_ID,
       c.user_id         as DOCTOR_ID,
       c.user_name       as NAME,
       c.id_card         as IDCARD,
       c.primary_illness AS  ""DESC"",
       c.Doc_Expert AS SPECIAL,
       (SELECT COM_DICTIONARY.NAME
          FROM COM_DICTIONARY
         WHERE COM_DICTIONARY.TYPE = 'PROFESSIONAL_TITILE'
           and valid_state = '1'
           and COM_DICTIONARY.CODE = c.job_title) AS JOB_TITLE,
       (select(e.regprice + e.diagprice) * 10 * 10
          from clinic_index q, clinic_type_dict e
         where q.clinic_type = e.clinic_type
           and q.doctor = c.user_name
           and rownum = 1) as REG_FEE,
       '1' as status,
       (CASE c.sex
         WHEN '1' THEN
          '1'
         WHEN '2' THEN
          '0'
         ELSE
          '3'
       END) as SEX,
       '' as BIRTHDAY,
       '' as MOBILE,
       '' as TEL
  from users_staff_dict c
 where (c.user_dept = '{0}' or '-1' = '{0}')
   and(c.user_id = '{1}' or '-1' = '{1}') and c.is_appoint_reg = '1'";
            sql = string.Format(sql, deptID, doctorID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HealthCardDoctorInfo>(ds).ToList();
        }

        public HealthCardRegInfo_OutParam GetRegInfo(string deptID, string doctorID, DateTime beginDate, DateTime endDate)
        {
            string sql = @"select * from  v_clinic_regist_info  c where  (c.clinic_dept='{0}' or '-1'='{0}')  and (c.doctor_code='{1}' or '-1'='{1}') and  c.clinic_date>=to_date('{2}','yyyy-mm-dd hh24:mi:ss') and  c.clinic_date<=to_date('{3}','yyyy-mm-dd hh24:mi:ss')";

            sql = string.Format(sql,deptID, doctorID, beginDate.ToString(), endDate.ToString());

            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            HealthCardRegInfo_OutParam info_OutParams = new HealthCardRegInfo_OutParam();
            List<HealthCardRegDoctorInfo> info_DoctorInfo = new List<HealthCardRegDoctorInfo>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    HealthCardRegDoctorInfo regDoctorInfo = info_DoctorInfo.Find(t => t.DOCTOR_ID == row["doctor_code"].ToString());
                    if (regDoctorInfo == null)
                    {
                        regDoctorInfo = new HealthCardRegDoctorInfo();
                        regDoctorInfo.DOCTOR_ID = row["doctor_code"].ToString();
                        regDoctorInfo.NAME = row["doctor"].ToString();
                        regDoctorInfo.JOB_TITLE = row["TITLE_NAME"].ToString();

                        List<HealthCardRegInfo> healthCardRegInfos = new List<HealthCardRegInfo>();

                        HealthCardRegInfo tempRegInfo = null;
                        CreateRegInfoByRow(row, ref tempRegInfo);
                        healthCardRegInfos.Add(tempRegInfo);
                        regDoctorInfo.RegLst = healthCardRegInfos;
                        info_DoctorInfo.Add(regDoctorInfo);
                    }
                    else
                    {
                        HealthCardRegInfo _healthCardRegInfo = regDoctorInfo.RegLst.Find(t => t.REG_DATE == Convert.ToDateTime(row["clinic_date"].ToString()).ToString("yyyy-MM-dd"));
                        if (_healthCardRegInfo != null)
                        {
                            CreateRegInfoByRow(row, ref _healthCardRegInfo);
                        }
                        else
                        {
                            CreateRegInfoByRow(row, ref _healthCardRegInfo);
                            regDoctorInfo.RegLst.Add(_healthCardRegInfo);
                        }
                    }
                }
            }

            info_OutParams.DEPT_ID = deptID;
            info_OutParams.DoctorLst = info_DoctorInfo;

            return info_OutParams;
        }

        public HealthCardTimeRegInfo_OutParam GetTimeRegInfo(string deptID, string doctorID, DateTime clinicDate, string timeFlag)
        {
            string sql = @"select *
  from v_clinic_regist_info c ,CLINIC_FOR_REGIST_YY B
 where  
 C.REGISTID = B.REGISTID
 AND c.clinic_dept = '{0}'
   and   (c.doctor_code='{1}' or '-1'='{1}') 
   and  c.clinic_date= to_date('{2}', 'yyyy-mm-dd')
";
            sql = string.Format(sql, deptID, doctorID, clinicDate.ToShortDateString());

            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            HealthCardTimeRegInfo_OutParam info_OutParams = new HealthCardTimeRegInfo_OutParam();
            List<HealthCardTimeRegInfo> info_TimeRegInfo = new List<HealthCardTimeRegInfo>();
            string[] strRegDate = clinicDate.ToString("yyyy-MM-dd").Split('-');


            if (dt != null && dt.Rows.Count > 0)
            {

                //                1 上午(06:00 - 12:00)
                //2 下午(12:00 - 18:00)
                //3 晚上(18:00 - 次日 06:00)
                //4 全天（06:00 - 18:00）
               
                foreach (DataRow row in dt.Rows)
                {
                    // string regID = row["doctor_code"].ToString() + "-" + strRegDate[0] + strRegDate[1].PadLeft(2, '0') + strRegDate[2].PadLeft(2, '0');
                    string TimeDesc = row["TIME_DESC"].ToString();
                    string TimeDescFlag = "1";
                    if (TimeDesc == "上午" )
                        TimeDescFlag  = "1";
                    else if (TimeDesc == "下午")
                        TimeDescFlag = "2";
                    else if (TimeDesc == "白天")
                        TimeDescFlag = "4";
                    else  
                        TimeDescFlag = "3";

                    string regID = row["REGISTID"].ToString() + "-" + row["ID"].ToString();
                   string limitNum = row["appointment_limits"].ToString();
                    info_TimeRegInfo.Add(GenerateTimeRegInfo(regID , TimeDescFlag, row["TIMESTART"].ToString(), row["TIMEEND"].ToString(), limitNum, limitNum));
                    //if (row["time_desc"].ToString() == "昼夜")
                    //{
                    //    switch (timeFlag)
                    //    {
                    //        case "-1":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "A", "1", "07:00", "11:30", limitNum, limitNum));
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "B", "2", "13:30", "16:00", limitNum, limitNum));
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "C", "3", "17:00", "22:00", limitNum, limitNum));
                    //            break;
                    //        case "1":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "A", "1", "07:00", "11:30", limitNum, limitNum));
                    //            break;
                    //        case "2":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "B", "2", "13:30", "16:00", limitNum, limitNum));
                    //            break;
                    //        case "3":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "C", "3", "17:00", "22:00", limitNum, limitNum));
                    //            break;
                    //        default:
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "A", "1", "07:00", "11:30", limitNum, limitNum));
                    //            break;
                    //    }

                    //}
                    //else if (row["time_desc"].ToString() == "白天")
                    //{
                    //    switch (timeFlag)
                    //    {
                    //        case "-1":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "1", "1", "07:00", "11:30", limitNum, limitNum));
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "2", "2", "13:30", "16:00", limitNum, limitNum));
                    //            break;
                    //        case "1":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "1", "1", "07:00", "11:30", limitNum, limitNum));
                    //            break;
                    //        case "2":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "2", "2", "13:30", "16:00", limitNum, limitNum));
                    //            break;
                    //        default:
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "1", "1", "07:00", "11:30", limitNum, limitNum));
                    //            break;
                    //    }
                    //}
                    //else if (row["time_desc"].ToString() == "上午")
                    //{
                    //    switch (timeFlag)
                    //    {
                    //        case "-1":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "1", "1", "07:00", "11:30", limitNum, limitNum));
                    //            break;
                    //        case "1":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "1", "1", "07:00", "11:30", limitNum, limitNum));
                    //            break;
                    //        default:
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "1", "1", "07:00", "11:30", limitNum, limitNum));
                    //            break;
                    //    }
                    //}
                    //else if (row["time_desc"].ToString() == "下午")
                    //{
                    //    switch (timeFlag)
                    //    {
                    //        case "-1":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "2", "2", "13:30", "16:00", limitNum, limitNum));
                    //            break;

                    //        case "2":
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "2", "2", "13:30", "16:00", limitNum, limitNum));
                    //            break;
                    //        default:
                    //            info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "2", "2", "13:30", "16:00", limitNum, limitNum));
                    //            break;
                    //    }
                    //}
                    //else
                    //{
                    //    info_TimeRegInfo.Add(GenerateTimeRegInfo(regID + "3", "3", "17:00", "22:00", limitNum, limitNum));
                    //}
                }
            }
            info_OutParams.TimeRegLst = info_TimeRegInfo;

            return info_OutParams;
        }


        public void GetSchedulingInfoByDoctor(string doctorID, DateTime visitDate, string descTime, ref Dictionary<string, string> dicSchedulInfo)
        {
            string sql = @"select * from  v_clinic_regist_info  c where      (c.doctor_code='{0}' or '-1'='{0}')  and   c.clinic_date =to_date('{1}','yyyy-mm-dd')";
            sql = string.Format(sql, doctorID, visitDate.ToShortDateString());

            DataTable dt = BaseEntityer.Db.GetDataTable(sql);

            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow row in dt.Rows)
            {
                if (row["time_desc"].ToString() == "昼夜" || (row["time_desc"].ToString() == "白天" && descTime.ToUpper() != "C") ||
                    (row["time_desc"].ToString() == "上午" && descTime.ToUpper() == "A") ||
                    (row["time_desc"].ToString() == "下午" && descTime.ToUpper() == "B"))
                {
                    dicSchedulInfo.Add("time_desc", row["time_desc"].ToString());
                    dicSchedulInfo.Add("clinic_dept", row["clinic_dept"].ToString());
                    dicSchedulInfo.Add("clinic_label", row["clinic_label"].ToString());
                    dicSchedulInfo.Add("clinic_date", row["clinic_date"].ToString());
                    dicSchedulInfo.Add("clinic_type", row["clinic_type"].ToString());
                    break;
                }
            }
        }

        public HealthCardOrderRegInfo_OutParam SaveOrderRegInfo(CLINIC_MASTER clinic_master, PAT_MASTER_INDEX master_Index, HealthCardOrderRegInfo_InParam regInfoInParam, Dictionary<string, string> dicErr)
        {
            HealthCardOrderRegInfo_OutParam outParam = new HealthCardOrderRegInfo_OutParam();

            try
            {
                BaseEntityer.DbBase.BeginTransaction();
                int rev = 0;

                string sqlsearch = @"SELECT count(*) FROM PAT_MASTER_INDEX WHERE PATIENT_ID = '{0}'";
                sqlsearch = string.Format(sqlsearch, clinic_master.PATIENT_ID);

                DataTable dataTable = BaseEntityer.Db.GetDataTable(sqlsearch);

                if (dataTable == null || int.Parse(dataTable.Rows[0][0].ToString()) <= 0)
                {
                    rev = outFeeManager.SaveOutPatientInfo(BaseEntityer.DbBase, master_Index);

                    if (rev <= 0)
                    {
                        BaseEntityer.DbBase.RollbackTransaction();
                        return null;
                    }
                }

                // 更新号表
                DataTable registrationNum = outFeeManager.GetRegistrationNum(BaseEntityer.DbBase, clinic_master.VISIT_DATE.ToShortDateString(),
                     clinic_master.CLINIC_LABEL, clinic_master.VISIT_TIME_DESC);
                int reg_num = Convert.ToInt32(registrationNum.Rows[0]["registration_num"].ToString());
                int current_no = Convert.ToInt32(registrationNum.Rows[0]["current_no"].ToString());
                //更新号表
                if (reg_num == 0)
                {
                    rev = outFeeManager.UpdateClinicForRegist(BaseEntityer.DbBase, clinic_master.VISIT_DATE.ToShortDateString(), clinic_master.CLINIC_LABEL, clinic_master.VISIT_TIME_DESC, 2, 1);
                }
                else
                {
                    rev = outFeeManager.UpdateClinicForRegist(BaseEntityer.DbBase, clinic_master.VISIT_DATE.ToShortDateString(), clinic_master.CLINIC_LABEL, clinic_master.VISIT_TIME_DESC, current_no + 1, reg_num + 1);
                }

                if (rev <= 0)
                {
                    BaseEntityer.DbBase.RollbackTransaction();
                    dicErr.Add("200710", "该排班下的当前号源已被占用");
                    return null;
                }

                // 插入挂号表
                rev = outFeeManager.InsertClinicMaster(BaseEntityer.DbBase, clinic_master);

                if (rev <= 0)
                {
                    BaseEntityer.DbBase.RollbackTransaction();
                    dicErr.Add("200708", "平台订单号已存在");
                    return null;
                }

                // 插入记录表
                string sql = @"INSERT into HEALTHCARDORDERREGINFO_INPARAM t
                        (t.ORDER_TIME,t.DEPT_ID,t.ORDER_ID,  t.HEALTH_CARD_ID,  t.CHANNEL_ID,    t.IS_REG,  t.REG_ID, t.REG_LEVEL,  t.HOS_ID,t.DOCTOR_ID,t.MOBILE, t.REG_DATE,t.TIME_FLAG, t.BEGIN_TIME, t.END_TIME, t.REG_FEE, t.TREAT_FEE,t.REG_TYPE,t.ORDER_STATUS,t.HOSP_ORDER_ID,t.HOSP_PATIENT_ID)
                    VALUES ('{0}',  '{1}', '{2}','{3}', '{4}', '{5}','{6}','{7}','{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}','{17}','{18}','{19}','{20}') ";
                sql = string.Format(sql, regInfoInParam.ORDER_TIME, regInfoInParam.DEPT_ID, regInfoInParam.ORDER_ID, regInfoInParam.HEALTH_CARD_ID, regInfoInParam.CHANNEL_ID, regInfoInParam.IS_REG, regInfoInParam.REG_ID, regInfoInParam.REG_LEVEL, regInfoInParam.HOS_ID, regInfoInParam.DOCTOR_ID, regInfoInParam.MOBILE, regInfoInParam.REG_DATE, regInfoInParam.TIME_FLAG, regInfoInParam.BEGIN_TIME, regInfoInParam.END_TIME, regInfoInParam.REG_FEE, regInfoInParam.TREAT_FEE, regInfoInParam.REG_TYPE, "2", clinic_master.BILLNO, clinic_master.PATIENT_ID);
                rev = BaseEntityer.DbBase.ExecuteNonQuery(sql);
                if (rev <= 0)
                {
                    BaseEntityer.DbBase.RollbackTransaction();
                    return null;
                }
                // 拼接输出参数
                outParam.HOSP_ORDER_ID = clinic_master.BILLNO;
                outParam.HEALTH_CARD_ID = regInfoInParam.HEALTH_CARD_ID;
                outParam.HOSP_PATIENT_ID = clinic_master.PATIENT_ID;
                outParam.HOSP_SERIAL_NUM = clinic_master.SERIAL_NO == null ? "" : clinic_master.SERIAL_NO.ToString();
                outParam.HOSP_MEDICAL_NUM = clinic_master.VISIT_NO.ToString();
                outParam.IS_CONCESSIONS = "0";

                BaseEntityer.DbBase.CommitTransaction();
                return outParam;
            }
            catch (Exception ex)
            {
                BaseEntityer.DbBase.RollbackTransaction();
                dicErr.Add("200708", "平台订单号已存在 异常信息 "+ex.ToString());
                return null;
            }
        }

        public HealthCardPayRegInfo_OutParam SavePayRegInfo(fin_invoiceinfo_record invoiceinfo_Record, HealthCardPayRegInfo_InParam payRegInfo, CLINIC_MASTER clinic_master, ref Dictionary<string, string> dicErr)
        {
            HealthCardPayRegInfo_OutParam outParam = new HealthCardPayRegInfo_OutParam();
         
            BaseEntityer.DbBase.BeginTransaction();

            //outFeeManager.InsertFinInvoiceinfoRecord(invoiceinfo_Record, BaseEntityer.DbBase);
            //string errStr = "";
            //if (invoiceinfo_Record.TRANS_TYPE == "1")
            //{
            //    outFeeManager.UpdateInvoiceUse(BaseEntityer.DbBase, invoiceinfo_Record.INVOICE_NO, invoiceinfo_Record.FEE_OPER_CODE, HisCommon.Enum.InvoiceKind.挂号发票, "fee", ref errStr);
            //}

            //string updateSql = @"update clinic_master  c  set  c.invoice_new='{0}' where  c.billno='{1}'";

            //updateSql = string.Format(updateSql, invoiceinfo_Record.INVOICE_NO, clinic_master.BILLNO);
            //int rev = BaseEntityer.DbBase.ExecuteNonQuery(updateSql);
            //if (rev < 0)
            //{
            //    dicErr.Add("200803", "挂号订单已关闭");
            //    BaseEntityer.DbBase.RollbackTransaction();
            //    return null;
            //}

          
            string insertSql = @"INSERT into HEALTHCARDPAYREGINFO_OUTPARAM t
                          (t.PAY_ACCOUNT,
                           t.BANK_NO,
                           t.TERMINAL_ID,
                           t.MERCHANT_ID,
                           t.PAY_RES_DESC,
                           t.PAY_RES_CODE,
                           t.PAY_FEE,
                           t.PAY_COPE_FEE,
                           t.PAY_TOTAL_FEE,
                           t.PAY_CHANNEL_ID,
                           t.PAY_TIME,
                           t.PAY_DATE,
                           t.SERIAL_NUM,
                           t.ORDER_ID,
                           t.HOS_ID)
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
                           '{14}')
                        ";
            insertSql = string.Format(insertSql, payRegInfo.PAY_ACCOUNT, payRegInfo.BANK_NO, payRegInfo.TERMINAL_ID, payRegInfo.MERCHANT_ID, payRegInfo.PAY_RES_DESC, payRegInfo.PAY_RES_CODE, payRegInfo.PAY_FEE, payRegInfo.PAY_COPE_FEE, payRegInfo.PAY_TOTAL_FEE, payRegInfo.PAY_CHANNEL_ID, payRegInfo.PAY_TIME, payRegInfo.PAY_DATE, payRegInfo.SERIAL_NUM, payRegInfo.ORDER_ID, payRegInfo.HOS_ID);

            int    rev = BaseEntityer.DbBase.ExecuteNonQuery(insertSql);

            if (rev < 0)
            {
                dicErr.Add("200803", "挂号订单已关闭");
                BaseEntityer.DbBase.RollbackTransaction();
                return null;
            }

            string updateStatusSql = @"UPDATE HEALTHCARDORDERREGINFO_INPARAM t
                                                           SET t.ORDER_STATUS = '{1}'
                                                         WHERE t.ORDER_ID = '{0}'";
            updateStatusSql = string.Format(updateStatusSql, payRegInfo.ORDER_ID, "6");

            rev = BaseEntityer.DbBase.ExecuteNonQuery(updateStatusSql);
            if (rev < 0)
            {
                dicErr.Add("200803", "挂号订单已关闭");
                BaseEntityer.DbBase.RollbackTransaction();
                return null;
            }
            outParam.HOSP_PAY_ID = clinic_master.BILLNO;

            string sql = @"Update CLINIC_MASTER set RETURNED_DATE = null, RETURNED_OPERATOR = null 
            where SERIALNUMBER  ='{0}'";
            sql = string.Format(sql,  payRegInfo.ORDER_ID);
            rev = BaseEntityer.DbBase.ExecuteNonQuery(sql);
            if (rev < 0)
            {
                dicErr.Add("200801", "挂号修改失败");
                BaseEntityer.DbBase.RollbackTransaction();
                return null;
            }

            BaseEntityer.DbBase.CommitTransaction();
            return outParam;
        }

        public bool SaveCancelRegInfo(HealthCardCancelRegInfo_InParam cancelRegInfo, string userID, ref Dictionary<string, string> dicErr)
        {
            CLINIC_MASTER clinic_master = GetClinicMaster(cancelRegInfo.ORDER_ID);

            string searchSql = @"select c.order_status from HEALTHCARDORDERREGINFO_INPARAM  c  where c.order_id='{0}'";
            searchSql = string.Format(searchSql, cancelRegInfo.ORDER_ID);

            DataTable   tempTable= BaseEntityer.Db.GetDataTable(searchSql);

            if (tempTable != null && tempTable.Rows.Count > 0 && tempTable.Rows[0][0].ToString() == "6")
            {
                dicErr.Add("200902", "挂号订单已支付");
                return false;
            }
            else  if(tempTable != null && tempTable.Rows.Count > 0 && tempTable.Rows[0][0].ToString() == "5")
            {
                dicErr.Add("200806", "挂号订单已退款");
                return false;
            }
           else  if (clinic_master == null)
            {
                dicErr.Add("200901", "挂号订单不存在");
                return false;
            }
            else if (clinic_master.ADMIS == "1")
            {
                dicErr.Add("200903", "挂号订单已关闭");
                return false;
            }
            else if (!string.IsNullOrEmpty(clinic_master.RETURNED_OPERATOR))
            {
                dicErr.Add("200805", "挂号订单已取消");

                return false;
            }
            BaseEntityer.DbBase.BeginTransaction();
            outFeeManager.UpdateClinicForRegist(BaseEntityer.DbBase, clinic_master.VISIT_DATE.ToShortDateString(),
                        clinic_master.CLINIC_LABEL, clinic_master.VISIT_TIME_DESC);
            int rev = outFeeManager.UpdateClinicMaster(BaseEntityer.DbBase, cancelRegInfo.CANCEL_DATE,
                  userID, clinic_master.VISIT_DATE.ToShortDateString(),
                   clinic_master.VISIT_NO.ToString());

            if (rev < 0)
            {
                dicErr.Add("200903", "挂号订单已关闭");
                BaseEntityer.DbBase.RollbackTransaction();
                return false;
            }

            string updateStatusSql = @"UPDATE HEALTHCARDORDERREGINFO_INPARAM t
                                                           SET t.ORDER_STATUS = '{1}'
                                                         WHERE t.ORDER_ID = '{0}'";
            updateStatusSql = string.Format(updateStatusSql, cancelRegInfo.ORDER_ID, "4");

            rev = BaseEntityer.DbBase.ExecuteNonQuery(updateStatusSql);
            if (rev < 0)
            {
                dicErr.Add("200803", "挂号订单已关闭");
                BaseEntityer.DbBase.RollbackTransaction();
                return false;
            }
            BaseEntityer.DbBase.CommitTransaction();
            return true;
        }

        public HealthCardRefundRegInfo_OutParam SaveRefundRegInfo(HealthCardRefundRegInfo_InParam refundRegInfo, fin_invoiceinfo_record fin_Invoiceinfo, ref Dictionary<string, string> dicErr)
        {
            CLINIC_MASTER clinic_master = GetClinicMaster(refundRegInfo.ORDER_ID);

            string searchSql = @"select c.order_status from HEALTHCARDORDERREGINFO_INPARAM  c  where c.order_id='{0}'";
            searchSql = string.Format(searchSql, refundRegInfo.ORDER_ID);

            DataTable tempTable = BaseEntityer.Db.GetDataTable(searchSql);

         
             if (tempTable != null && tempTable.Rows.Count > 0 && tempTable.Rows[0][0].ToString() == "5")
            {
                dicErr.Add("200806", "挂号订单已退款");
                return null;
            }
            else if (clinic_master == null)
            {
                dicErr.Add("201001", "挂号订单不存在");
                return null;
            }
            else if (clinic_master.ADMIS == "1")
            {
                dicErr.Add("201002", "挂号订单已关闭");
                return null;
            }
            else if (!string.IsNullOrEmpty(clinic_master.RETURNED_OPERATOR))
            {
                dicErr.Add("200805", "挂号订单已取消");
                return null;
            }
            else  if(clinic_master.CLINIC_CHARGE*100!= decimal.Parse( refundRegInfo.REFUND_FEE))
            {
                dicErr.Add("201003", "退款金额不正确");
                return null;
            }
            HealthCardRefundRegInfo_OutParam outParam = new HealthCardRefundRegInfo_OutParam();

            BaseEntityer.DbBase.BeginTransaction();

          //  int rev = outFeeManager.InsertFinInvoiceinfoRecord(fin_Invoiceinfo, BaseEntityer.DbBase);

            string insertSql = @"INSERT into HEALTHCARDREFUNDREGINFO
                                  (REFUND_REMARK,
                                   REFUND_RES_DESC,
                                   REFUND_RES_CODE,
                                   REFUND_TIME,
                                   REFUND_DATE,
                                   REFUND_FEE,
                                   TOTAL_FEE,
                                   REFUND_SERIAL_NUM,
                                   REFUND_ID,
                                   HOSP_ORDER_ID,
                                   ORDER_ID,
                                   HOS_ID)
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
                                   '{11}')
                                ";
            insertSql = string.Format(insertSql, refundRegInfo.REFUND_REMARK, refundRegInfo.REFUND_RES_DESC, refundRegInfo.REFUND_RES_CODE, refundRegInfo.REFUND_TIME, refundRegInfo.REFUND_DATE, refundRegInfo.REFUND_FEE, refundRegInfo.TOTAL_FEE, refundRegInfo.REFUND_SERIAL_NUM, refundRegInfo.REFUND_ID, refundRegInfo.HOSP_ORDER_ID, refundRegInfo.ORDER_ID, refundRegInfo.HOS_ID);
          int  rev = BaseEntityer.DbBase.ExecuteNonQuery(insertSql);

            if (rev < 0)
            {
                dicErr.Add("200807", "医院不允许退款");
                BaseEntityer.DbBase.RollbackTransaction();
                return null;
            }

            string updateSql = @"UPDATE HEALTHCARDORDERREGINFO_INPARAM t
   SET t.ORDER_STATUS = '{1}', t.refund_flag = '1', t.cancel_date = '{2}'
 WHERE t.ORDER_ID = '{0}'";
            updateSql = string.Format(updateSql, refundRegInfo.ORDER_ID, "5", Common.getServerTime().ToString());

            rev = BaseEntityer.DbBase.ExecuteNonQuery(insertSql);
            if (rev <= 0)
            {
                dicErr.Add("200807", "医院不允许退款");
                BaseEntityer.DbBase.RollbackTransaction();
                return null;
            }
            BaseEntityer.DbBase.CommitTransaction();

            outParam.HOSP_REFUND_ID = refundRegInfo.HOSP_ORDER_ID;
            outParam.REFUND_FLAG = "1";
            return outParam;
        }

        public HealthCardGetRegNumInfo_OutParam GetRegNumInfo(HealthCardGetRegNumInfo_InParam regNumInfo, ref Dictionary<string, string> dicErr)
        {
            HealthCardGetRegNumInfo_OutParam outParam = new HealthCardGetRegNumInfo_OutParam();
            string sql = " select c.serial_no from  clinic_master   c where  c.billno='{0}' and  c.returned_operator is null";
            sql = string.Format(sql, regNumInfo.ORDER_ID);
            DataTable dataTable = BaseEntityer.DbBase.GetDataTable(sql);

            if (dataTable == null || dataTable.Rows.Count <= 0)
            {
                dicErr.Add("201101", "挂号订单不存在");
                return null;
            }

            string updateSql = @"UPDATE HEALTHCARDORDERREGINFO_INPARAM t
   SET t.ORDER_STATUS = '{1}',
       t.GET_REGNO_DATE = '{2}'
 WHERE t.ORDER_ID = '{0}'";
            updateSql = string.Format(updateSql, regNumInfo.ORDER_ID, "3", HisDBLayer.Common.getServerTime().ToString());
            int rev = BaseEntityer.DbBase.ExecuteNonQuery(updateSql);

            if (rev <= 0)
            {
                dicErr.Add("201102", "挂号订单已关闭");
                return null;
            }

            outParam.HOSP_SERIAL_NUM = dataTable.Rows[0][0].ToString();
            outParam.REMARK = "";
            return outParam;
        }

        public IList<CLINIC_MASTER> QueryOrderRegInfoLst(string beginDate, string endDate, int page_count, int page_size,string  patientID)
        {
            string sql = @"select *
                                          from (select ROWNUM as rowno, c.*
                                                  from clinic_master c
                                                 where c.visit_date >= to_date('{0}', 'yyyy-mm-dd')
                                                   and c.visit_date <= to_date('{1}', 'yyyy-mm-dd')
                                                   and ROWNUM <= '{2}'
                                                   and c.patient_id='{4}')
                                         table_order 
                                         where table_order.rowno > '{3}'";
            sql = string.Format(sql, beginDate, endDate, page_count * page_size, (page_count - 1) * page_size,patientID);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(ds).ToList();
        }

        public DataTable GetPlatformOrderInfo(string hisOrderId, string hisPatientID)
        {
            string sql = @"select r.order_id,
       r.order_status,
       r.cancel_date,
       r.refund_flag,
       r.get_regno_date
  from HEALTHCARDORDERREGINFO_INPARAM r
 where r.hosp_order_id = '{0}'
   and r.hosp_patient_id = '{1}'
";
            sql = string.Format(sql, hisOrderId, hisPatientID);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public HealthCardPayDetailInfo_OutParam GetPayDetailInfoByOrderID(string orderID,string patientID,ref Dictionary<string,string>  dicErr)
        {
            HealthCardPayDetailInfo_OutParam outParam = new HealthCardPayDetailInfo_OutParam();

            // 获得未收费的信息
            if (orderID.Contains(patientID))
            {
                //
                string[] splitArray = orderID.Split('^').ToArray();

                string visitNO = splitArray[1];
                string  visitDate = HisDBLayer.Common.getServerTime().AddDays(-3).ToShortDateString();
                List<HisCommon.DataEntity.OUTP_ORDERS_DETAIL> detail = outFeeManager.QueryPatientOrderDetailByCurrentDay(visitNO, visitDate);

                if (detail == null || detail.Count <= 0)
                {
                    dicErr.Add("300201", "无效的缴费记录");
                    return null;
                }
                outParam.HOSP_PATIENT_ID = patientID;
                outParam.MEDICAL_INSURANNCE_TYPE = "自费";
                decimal totalFee1 = 0;
                decimal payFee1= 0;
                decimal ownFee1 = 0;
                decimal pubFee1 = 0;
                detail.ForEach(t =>
                {
                    totalFee1 += t.NOWCOST;
                    payFee1 += t.NOWCOST;
                    ownFee1 += t.NOWCOST;
                });
                outParam.PAY_TOTAL_FEE = (totalFee1* 100).ToString();
                outParam.PAY_BEHOOVE_FEE = (payFee1 * 100).ToString();
                outParam.PAY_ACTUAL_FEE = (ownFee1 * 100).ToString();
                outParam.PAY_MI_FEE = (pubFee1 * 100).ToString();

                List<PayDetailRecord> payDetailRecords1 = new List<PayDetailRecord>();

                foreach (var  item in detail)
                {
                    PayDetailRecord tempDetail = new PayDetailRecord();
                    tempDetail.DETAIL_TYPE = item.NOWCLASS_NAME;
                    tempDetail.DETAIL_NAME = item.ITEM_NAME;
                    tempDetail.DETAIL_ID = item.ITEM_NO.ToString();
                    tempDetail.DETAIL_UNIT = item.UNITS;
                    tempDetail.DETAIL_COUNT = item.NOWAMOUNT.ToString();
                    tempDetail.DETAIL_PRICE = (item.NOWPRICE*100).ToString();
                    tempDetail.DETAIL_SPEC = item.ITEM_SPEC;
                    tempDetail.DETAIL_AMOUT = (item.NOWCOST*100).ToString();

                    payDetailRecords1.Add(tempDetail);
                }

                outParam.PAY_DETAIL_LIST = payDetailRecords1;
                return outParam;
            }
            string sqlSearch = @" select n.rcpt_no,
                                                   n.tot_cost,
                                                   n.pay_cost,
                                                   n.own_cost,
                                                   n.pub_cost,
                                                   r.patient_id,
                                                   (select c.name
                                                      from pat_master_index c
                                                     where c.patient_id = r.patient_id
                                                       and rownum = 1) as patientname
                                              from fin_invoiceinfo_record n, OUTP_ORDER_DESC r
                                             where n.invoice_no = '{0}'
                                               and n.rcpt_no = r.rcpt_no";

            string payDetailSql = @"select (select r.subj_name
                                                      from TALLY_SUBJECT_DICT r
                                                     where r.subj_code = c.subj_code) as subj_name,
                                                   c.item_name,
                                                   c.item_no,
                                                   c.units,
                                                   c.amount,
                                                   c.price,
                                                   c.item_spec,
                                                   c.costs
                                              from OUTP_BILL_ITEMS c
                                             where c.invoice_new = '{0}'";

            sqlSearch = string.Format(sqlSearch, orderID);
            payDetailSql = string.Format(payDetailSql, orderID);

            DataTable dtTable = BaseEntityer.Db.GetDataTable(sqlSearch);
            DataTable   dtDetailTable= BaseEntityer.Db.GetDataTable(payDetailSql);

            if (dtTable == null || dtTable.Rows.Count <= 0)
            {
                dicErr.Add("300201", "无效的缴费记录");
                return null;
            }

            if(dtDetailTable==null|| dtDetailTable.Rows.Count <= 0)
            {
                dicErr.Add("300201", "无效的缴费记录");
                return null;
            }

            outParam.USER_NAME = dtTable.Rows[0]["patientname"]==null?"":dtTable.Rows[0]["patientname"].ToString();
            outParam.HOSP_PATIENT_ID = dtTable.Rows[0]["patient_id"] == null ? "" : dtTable.Rows[0]["patient_id"].ToString();
            outParam.MEDICAL_INSURANNCE_TYPE = "自费";
           
            decimal totalFee = 0;
            decimal payFee = 0;
            decimal ownFee = 0;
            decimal pubFee = 0;
           
            foreach (DataRow row in dtTable.Rows)
            {
                totalFee += row["tot_cost"] == null ? 0 : Convert.ToDecimal(row["tot_cost"].ToString());
                payFee += row["pay_cost"] == null ? 0 : Convert.ToDecimal(row["pay_cost"].ToString());
                ownFee += row["own_cost"] == null ? 0 : Convert.ToDecimal(row["own_cost"].ToString());
                pubFee += row["pub_cost"] == null ? 0 : Convert.ToDecimal(row["pub_cost"].ToString());
            }

            outParam.PAY_TOTAL_FEE = (totalFee * 100).ToString();
            outParam.PAY_BEHOOVE_FEE = (payFee * 100).ToString();
            outParam.PAY_ACTUAL_FEE = (ownFee * 100).ToString();
            outParam.PAY_MI_FEE = (payFee * 100).ToString();
            List<PayDetailRecord> payDetailRecords = new List<PayDetailRecord>();

            foreach (DataRow row in dtDetailTable.Rows)
            {
                PayDetailRecord tempDetail = new PayDetailRecord();
                tempDetail.DETAIL_TYPE = row["subj_name"] == null ? "" : row["subj_name"].ToString();
                tempDetail.DETAIL_NAME = row["item_name"] == null ? "" : row["item_name"].ToString();
                tempDetail.DETAIL_ID = row["item_no"] == null ? "" : row["item_no"].ToString();
                tempDetail.DETAIL_UNIT = row["units"] == null ? "" : row["units"].ToString();
                tempDetail.DETAIL_COUNT = row["amount"] == null ? "" : row["amount"].ToString();
                tempDetail.DETAIL_PRICE = row["price"] == null ? "0" : (Convert.ToDecimal(row["price"].ToString()) * 100).ToString();
                tempDetail.DETAIL_SPEC = row["item_spec"] == null ? "" : row["item_spec"].ToString();
                tempDetail.DETAIL_AMOUT = row["costs"] == null ? "0" : (Convert.ToDecimal(row["costs"].ToString()) * 100).ToString();

                payDetailRecords.Add(tempDetail);
            }
            outParam.PAY_DETAIL_LIST = payDetailRecords;
            return outParam;
        }

        public List<PayRecordInfo> GetPayListInfoByCondition(string patientID, string beginDate, string endDate, string queryType)
        {
            PatientManager pm = new PatientManager();

            IList<CLINIC_MASTER> masters = pm.GetPatientByNO(patientID, HisDBLayer.Common.getServerTime().AddDays(-3).ToShortDateString());

            List<PayRecordInfo> payRecordLst = new List<PayRecordInfo>();
            string sql = @"
                           select n.invoice_no,
                       fun_getdeptname(r.ordered_by_dept) as deptname,
                       n.tot_cost,
                       (select m.user_name
                          from users_staff_dict m
                         where m.user_id = r.ordered_by_doctor) as doctorname,
                       n.invoice_state,
                       r.rcpt_no,
                       n.fee_oper_date,
                       n.backfee_oper_date
                  from OUTP_ORDER_DESC r, fin_invoiceinfo_record n
                 where n.rcpt_no = r.rcpt_no
                   and n.invoice_kind = '01'
                   and r.patient_id = '{0}'
                   and n.fee_oper_date >= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                   and n.fee_oper_date <= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') ";
            
            sql = string.Format(sql, patientID, beginDate, endDate);

            DataTable dtTable = BaseEntityer.Db.GetDataTable(sql);

            if (dtTable !=null && dtTable.Rows.Count > 0)
            {
                foreach (DataRow row in dtTable.Rows)
                {
                    if (row["invoice_state"] != null &&
                        ((queryType == "2" && row["invoice_state"].ToString() == "0") ||
                        (queryType == "3" && row["invoice_state"].ToString() == "1")) ||
                        queryType == "0")
                    {
                        payRecordLst.Add(GeneratePayRecordInfo(row));
                    }
                }
            }

            if ((queryType == "0" || queryType == "1") && masters.Count > 0)
            {
                foreach (var p in masters)
                {
                    List<HisCommon.DataEntity.OUTP_ORDERS_DETAIL> detail = outFeeManager.QueryPatientOrderDetailByCurrentDay(p.VISIT_NO.ToString(), p.VISIT_DATE.ToShortDateString());

                    if (detail != null && detail.Count > 0)
                    {
                        // 当前订单金额
                        decimal totalCosts = 0;
                        string dept_name = string.Empty;
                        string doctorName = string.Empty;

                        detail.ForEach(t => totalCosts += t.NOWCOST);
                        USERS_STAFF_DICT  tempUser = HisDBLayer.Common.GetCurrentStaff(detail.First().ORDER_DOCTOR);

                        doctorName = tempUser==null?"": tempUser.USER_NAME;
                        dept_name = HisDBLayer.Common.GetDeptNameByID(detail.First().ORDERED_BY);
                        PayRecordInfo recordInfo = new PayRecordInfo();

                        // 患者ID ^^ 就诊序号^^项目第一条的序列号
                        recordInfo.HOSP_SEQUENCE = p.PATIENT_ID +"^"+ p.VISIT_NO+"^"+detail.First().SERIAL_NO;
                        recordInfo.DEPT_NAME = dept_name;
                        recordInfo.DOCTOR_NAME = doctorName;
                        recordInfo.PAY_AMOUT = (totalCosts * 100).ToString();
                        // 基本信息
                        recordInfo.ORDER_STATUS = "0";
                        recordInfo.PAY_REMARK = p.VISIT_DATE.ToString() + "/" + p.VISIT_NO;
                        recordInfo.RECEIPT_DATE = HisDBLayer.Common.getServerTime().ToString("yyyy-MM-dd HH:mm:ss");
                        payRecordLst.Add(recordInfo);
                    }
                }
            }
            return payRecordLst;
        }

        public bool SavePayOrderListInfo(HealthCardPayOrderInfo_InParam payOrderInParam, HisCommon.DataEntity.OUTP_CHARGEINFO o, ref Dictionary<string, string> dicErr)
        {
            try
            {
                BaseEntityer.DbBase.BeginTransaction();
                string errStr = "";
                foreach (var i in o.outpRcptMaster)
                {
                    medicalmanager.InsertOutpRcptMaster(i, BaseEntityer.DbBase);
                }
                foreach (var i in o.outpOrderDesc)
                {
                    medicalmanager.InsertOutpOrderDesc(i, BaseEntityer.DbBase);
                }
                foreach (var i in o.listOutpBillItems)
                {
                    medicalmanager.InsertOutpBillItems(i, BaseEntityer.DbBase);
                }
                foreach (var i in o.listFinInvoiceInfo)
                {
                    outFeeManager.InsertFinInvoiceinfoRecord(i, BaseEntityer.DbBase);
                    if (i.TRANS_TYPE == "1")
                    {
                        outFeeManager.UpdateInvoiceUse(BaseEntityer.DbBase, i.INVOICE_NO, i.FEE_OPER_CODE, HisCommon.Enum.InvoiceKind.门诊发票, "fee", ref errStr);
                    }
                }
                foreach (var i in o.listOutpPaymentsMoney)
                {
                    this.medicalmanager.InsertOutpPaymentsMoney(i, BaseEntityer.DbBase);
                }

                //更新
                //药品
                var drugCF = o.listOutpBillItems.FindAll(x =>
                    x.ITEM_CLASS == HisCommon.Enum.ItemType.A.ToString() ||
                    x.ITEM_CLASS == HisCommon.Enum.ItemType.B.ToString()).ToList();
                if (drugCF != null)
                {
                    foreach (var d in drugCF)
                    {
                        if (string.IsNullOrEmpty(d.SERIAL_NO))
                            continue;
                        medicalmanager.UpdateOutpPresc(o.patient.VISIT_DATE.ToShortDateString(), o.patient.VISIT_NO.ToString(), d.SERIAL_NO, d.D_ITEM_NO.ToString(), 1, BaseEntityer.DbBase);
                    }
                }
                //非药品
                var unDrugCF = o.listOutpBillItems.FindAll(x =>
                   x.ITEM_CLASS != HisCommon.Enum.ItemType.A.ToString() &&
                   x.ITEM_CLASS != HisCommon.Enum.ItemType.B.ToString()).ToList();
                if (unDrugCF != null)
                {
                    foreach (var d in unDrugCF)
                    {
                        if (string.IsNullOrEmpty(d.SERIAL_NO))
                            continue;
                        medicalmanager.UpdateOutpTreatPresc(o.patient.VISIT_DATE.ToShortDateString(), o.patient.VISIT_NO.ToString(), d.SERIAL_NO, d.D_ITEM_NO.ToString(), 1, BaseEntityer.DbBase);
                    }
                }
            
                var exam = (from x in o.listOutpBillItems
                            where x.CLINIC_ITEM_CLASS == HisCommon.Enum.ItemType.D.ToString()
                            select x.APPOINT_NO).Distinct().ToList();
                if (exam != null)
                {
                    foreach (var d in exam)
                    {
                        if (string.IsNullOrEmpty(d))
                            continue;
                        medicalmanager.UpdateExamAppoints(d, 1, BaseEntityer.DbBase);
                    }
                }
                //检验
                var lab = (from x in o.listOutpBillItems
                           where x.CLINIC_ITEM_CLASS == HisCommon.Enum.ItemType.C.ToString()
                           select x.APPOINT_NO).Distinct().ToList();
                if (lab != null)
                {
                    foreach (var d in lab)
                    {
                        if (string.IsNullOrEmpty(d))
                            continue;
                        medicalmanager.UpdateLabTestMaster(d, 1, BaseEntityer.DbBase);
                    }
                }
             
               
                string sql = @"
                         INSERT into HEALTHCARDPAYORDERINFO_INPARAM t
                                                  (t.RECEIPT_ID,
                                                   t.OPERATOR_ID,
                                                   t.PAY_ACCOUNT,
                                                   t.BANK_NO,
                                                   t.TERMINAL_ID,
                                                   t.MERCHANT_ID,
                                                   t.PAY_RES_DESC,
                                                   t.PAY_RES_CODE,
                                                   t.PAY_MI_FEE,
                                                   t.PAY_ACTUAL_FEE,
                                                   t.PAY_BEHOOVE_FEE,
                                                   t.PAY_TOTAL_FEE,
                                                   t.PAY_CHANNEL_ID,
                                                   t.PAY_TIME,
                                                   t.PAY_DATE,
                                                   t.SERIAL_NUM,
                                                   t.HOSP_SEQUENCE,
                                                   t.ORDER_ID,
                                                   t.HOS_ID)
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
                                                   '{18}')
                                                ";

                sql = string.Format(sql, payOrderInParam.RECEIPT_ID, payOrderInParam.OPERATOR_ID, payOrderInParam.PAY_ACCOUNT, payOrderInParam.BANK_NO, payOrderInParam.TERMINAL_ID, payOrderInParam.MERCHANT_ID, payOrderInParam.PAY_RES_DESC, payOrderInParam.PAY_RES_CODE, payOrderInParam.PAY_MI_FEE, payOrderInParam.PAY_ACTUAL_FEE, payOrderInParam.PAY_BEHOOVE_FEE, payOrderInParam.PAY_TOTAL_FEE, payOrderInParam.PAY_CHANNEL_ID, payOrderInParam.PAY_TIME, payOrderInParam.PAY_DATE, payOrderInParam.SERIAL_NUM, payOrderInParam.HOSP_SEQUENCE, payOrderInParam.ORDER_ID, payOrderInParam.HOS_ID);

                int rev = BaseEntityer.DbBase.ExecuteNonQuery(sql);

                if (rev > 0)
                {
                    BaseEntityer.DbBase.CommitTransaction();
                    return true;
                }
                else
                {
                    BaseEntityer.DbBase.RollbackTransaction();
                    return false;
                }
            }
            catch
            {
                BaseEntityer.DbBase.RollbackTransaction();
                return false;
            }
        }

        public DataTable QueryDepositPaymentInfo(string beginDate, string endDate, string cardID)
        {
            string sql = @"
select c.patient_id, fun_getdeptname( c.dept_code) as deptname, c.bed_no, c.admission_date_time,c.prepayments  from  pats_in_hospital c  ,pat_master_index r where  c.admission_date_time>= to_date('{0}','yyyy-mm-dd hh24:mi:ss') and  c.admission_date_time<= to_date('{1}','yyyy-mm-dd hh24:mi:ss')  and   c.patient_id=r.patient_id  and r.id_no='{2}'";
            sql = string.Format(sql, beginDate, endDate, cardID);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public bool SaveDepositPaymentInfo(fin_invoiceinfo_record finRecord, PREPAYMENT_RCPT prePaymentRcpt, HealthCardDepositPayInfo_InParam depositPayInfo, ref Dictionary<string, string> dicErr)
        {
            HisDBLayer.InpatientRegister register = new InpatientRegister();

            BaseEntityer.DbBase.BeginTransaction();
            int revInt = outFeeManager.InsertFinInvoiceinfoRecord(finRecord, BaseEntityer.DbBase);
            string errMsg = string.Empty;

            if (revInt <= 0)
            {
                dicErr.Add("300303", "缴费订单已关闭（插入发票记录表失败)");
                return false;
            }

            if (finRecord.INVOICE_STATE == "0" || finRecord.INVOICE_STATE == "2")
            {
                revInt = outFeeManager.UpdateInvoiceUse(BaseEntityer.DbBase, finRecord.INVOICE_NO, finRecord.FEE_OPER_CODE, HisCommon.Enum.InvoiceKind.住院预交金, "健康卡缴费", ref errMsg);

                if (revInt <= 0)
                {
                    dicErr.Add("300303", "缴费订单已关闭（修改发票使用记录表出错)");
                    BaseEntityer.DbBase.RollbackTransaction();
                    return false;
                }
            }
            // 插入收费信息表
            revInt = register.InsertPrepayment(BaseEntityer.DbBase, prePaymentRcpt, ref errMsg);
            if (revInt <= 0)
            {
                dicErr.Add("300303", "缴费订单已关闭（" + errMsg + ")");
                BaseEntityer.DbBase.RollbackTransaction();
                return false;
            }

            // 更新住院表的预交金金额
            string sql = @"update pats_in_hospital f set f.prepayments =  f.prepayments+ '{1}' where f.patient_id='{0}'";
            sql = string.Format(sql, prePaymentRcpt.PATIENT_ID, prePaymentRcpt.AMOUNT);
            revInt = BaseEntityer.DbBase.ExecuteNonQuery(sql);
            if (revInt <= 0)
            {
                dicErr.Add("300303", "缴费订单已关闭（更新患者预交金信息失败)");
                BaseEntityer.DbBase.RollbackTransaction();
                return false;
            }

            string insertSql = @"INSERT into HEALTHCARDDEPOSITPAYINFO t
                                              (t.RECEIPT_ID,
                                               t.OPERATOR_ID,
                                               t.PAY_ACCOUNT,
                                               t.BANK_NO,
                                               t.TERMINAL_ID,
                                               t.MERCHANT_ID,
                                               t.PAY_RES_DESC,
                                               t.PAY_RES_CODE,
                                               t.ADVANCE_PAYMENT,
                                               t.PAY_CHANNEL_ID,
                                               t.PAY_TIME,
                                               t.PAY_DATE,
                                               t.SERIAL_NUM,
                                               t.ORDER_ID,
                                               t.HOS_ID,
                                               t.HOSP_PATIENT_NO)
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
                                               '{15}')
                                            ";
            insertSql = string.Format(insertSql, depositPayInfo.RECEIPT_ID, depositPayInfo.OPERATOR_ID, depositPayInfo.PAY_ACCOUNT, depositPayInfo.BANK_NO, depositPayInfo.TERMINAL_ID, depositPayInfo.MERCHANT_ID, depositPayInfo.PAY_RES_DESC, depositPayInfo.PAY_RES_CODE, depositPayInfo.ADVANCE_PAYMENT, depositPayInfo.PAY_CHANNEL_ID, depositPayInfo.PAY_TIME, depositPayInfo.PAY_DATE, depositPayInfo.SERIAL_NUM, depositPayInfo.ORDER_ID, depositPayInfo.HOSP_PATIENT_NO);
            revInt = BaseEntityer.DbBase.ExecuteNonQuery(insertSql);
            if (revInt <= 0)
            {
                dicErr.Add("300303", "缴费订单已关闭（插入预交金本地信息表失败)");
                BaseEntityer.DbBase.RollbackTransaction();
                return false;
            }
            BaseEntityer.DbBase.CommitTransaction();
            return true;
        }
        public HealthCardLisCheckReport_OutParam QueryCheckReportInfo(string beginDate,string endDate ,HEALTHCARD_PATIENT_INFO patInfo, ref Dictionary<string, string> dicErr)
        {
            HealthCardLisCheckReport_OutParam outParam = new HealthCardLisCheckReport_OutParam();
            beginDate = beginDate + " 00:00:00";
            endDate = endDate + " 23:59:59";

            try
            {
                string sql = @"
                               SELECT
                                     DISTINCT 
                                    就诊卡号,
                                    就诊号,
                                     报告ID REPORT_ID  ,--String(32)  ,--√ 检查/检验报告单号
                                    临床诊断 DIAGNOSIS   ,--String(64)  ,--√ 诊断
                                    检验目的  ITEM_NAME   ,--String(64)  ,--√ 检查/检验项目
                                    标本类型名称 SPECIMEN_NAME   ,--String(64)   ,-- 标本名称
                                    标本类型代码  SPECIMEN_ID   ,-- String(64)    标本号
                                    报告时间 REPORT_TIME   ,--String(32) ,-- √ 报告时间，格式 YYYY-MM-DD HH24:MI:SS 
                                    申请科室 DEPT_NAME   ,--String(64)  √ 科室名称
                                    申请医生 DOCTOR_NAME   ,--String(64)  √ 医生姓名
                                    CASE 细菌标志
                                             WHEN '2' THEN '1'
                                    ELSE '0' END REPORT_TYPE   ,--Int √ 报告类型：0-普通检验报告1-药敏检验报告2-检查报告 
                                    '' REMARK   --  备注
                                    FROM 
                                    接口视图_检验报告结果     t   where  t.就诊卡号='{0}' and  REPORT_TIME>='{1}' and REPORT_TIME<='{2}'";
                sql = string.Format(sql, patInfo.PATIENT_ID, beginDate, endDate);
                DataTable lisReport = BaseEntityToSQL.Db.GetDataTable(sql);

                if (lisReport == null || lisReport.Rows.Count <= 0)
                {
                    dicErr.Add("800101", "检查/检验报告记录不存在，未查询到检查/检验报告记录");
                    return null;
                }
                outParam.PATIENT_IDCARD_TYPE = "1";
                outParam.PATIENT_IDCARD_NO = patInfo.IDNO;
                outParam.PATIENT_NAME = patInfo.PATIENT_NAME;
                outParam.PATIENT_SEX = patInfo.Sex;
                DateTime birthDay = DateTime.Now;
                if (!string.IsNullOrEmpty(patInfo.BirthDay) &&
                    patInfo.BirthDay.Length == 8)
                {
                    string strBirthDay = patInfo.BirthDay.Substring(0, 4) + "-" + patInfo.BirthDay.Substring(4, 2) + "-" + patInfo.BirthDay.Substring(6, 2);
                    DateTime.TryParse(strBirthDay, out birthDay);
                }
                else
                    DateTime.TryParse(patInfo.BirthDay, out birthDay);
                outParam.PATIENT_AGE = GetAge(birthDay).ToString();
                outParam.MEDICAL_INSURANNCE_TYPE = "自费";
                outParam.HOSP_PATIENT_ID = patInfo.PATIENT_ID;

                List<LisReportInfo> lisCheckLst = new List<LisReportInfo>();

                for (int i = 0; i < lisReport.Rows.Count; i++)
                {
                    LisReportInfo lisCheck = new LisReportInfo();

                    lisCheck.REPORT_ID = lisReport.Rows[0]["REPORT_ID"] == null ? "" : lisReport.Rows[0]["REPORT_ID"].ToString();
                    lisCheck.DIAGNOSIS = lisReport.Rows[i]["DIAGNOSIS"] == null ? "" : lisReport.Rows[i]["DIAGNOSIS"].ToString();
                    lisCheck.ITEM_NAME = lisReport.Rows[i]["ITEM_NAME"] == null ? "" : lisReport.Rows[i]["ITEM_NAME"].ToString();
                    lisCheck.SPECIMEN_NAME = lisReport.Rows[i]["SPECIMEN_NAME"] == null ? "" : lisReport.Rows[i]["SPECIMEN_NAME"].ToString();
                    lisCheck.SPECIMEN_ID = lisReport.Rows[i]["SPECIMEN_ID"] == null ? "" : lisReport.Rows[i]["SPECIMEN_ID"].ToString();
                    lisCheck.REPORT_TIME = lisReport.Rows[i]["REPORT_TIME"] == null ? "" : lisReport.Rows[i]["REPORT_TIME"].ToString();
                    lisCheck.DEPT_NAME = lisReport.Rows[i]["DIAGNOSIS"] == null ? "" : lisReport.Rows[i]["DIAGNOSIS"].ToString();
                    lisCheck.ITEM_NAME = lisReport.Rows[i]["ITEM_NAME"] == null ? "" : lisReport.Rows[i]["ITEM_NAME"].ToString();
                    lisCheck.SPECIMEN_NAME = lisReport.Rows[i]["SPECIMEN_NAME"] == null ? "" : lisReport.Rows[i]["SPECIMEN_NAME"].ToString();
                    lisCheck.SPECIMEN_ID = lisReport.Rows[i]["SPECIMEN_ID"] == null ? "" : lisReport.Rows[i]["SPECIMEN_ID"].ToString();
                    lisCheck.REPORT_TIME = lisReport.Rows[i]["REPORT_TIME"] == null ? "" : lisReport.Rows[i]["REPORT_TIME"].ToString();
                    lisCheck.DEPT_NAME = lisReport.Rows[i]["DEPT_NAME"] == null ? "" : lisReport.Rows[i]["DEPT_NAME"].ToString();
                    lisCheck.DOCTOR_NAME = lisReport.Rows[i]["DOCTOR_NAME"] == null ? "" : lisReport.Rows[i]["DOCTOR_NAME"].ToString();
                    lisCheck.REMARK = lisReport.Rows[i]["REMARK"] == null ? "" : lisReport.Rows[i]["REMARK"].ToString();
                    lisCheckLst.Add(lisCheck);
                }
                outParam.REPORT_INFO = lisCheckLst;
            }
            catch (Exception ex)
            {
                dicErr.Add("800101", "检查/检验报告记录不存在，未查询到检查/检验报告记录（" + ex.ToString() + ")");
                return null;
            }

            return outParam;
        }

        public HealthCardLisCheckReport_OutParam QueryCheckPacsReportInfo(string beginDate, string endDate, HEALTHCARD_PATIENT_INFO patInfo, ref Dictionary<string, string> dicErr)
        {
            HealthCardLisCheckReport_OutParam outParam = new HealthCardLisCheckReport_OutParam();
            beginDate = beginDate + " 00:00:00";
            endDate = endDate + " 23:59:59";

            try
            {
                string sql = @"
                             
select   patient_id, REPORT_ID,DIAGNOSIS,ITEM_NAME,SPECIMEN_NAME,SPECIMEN_ID,REPORT_TIME,DEPT_NAME,DOCTOR_NAME,REPORT_TYPE,REMARK from  (

select 
hisid patient_id,
Roomid  REPORT_ID,  --String(32)  √ 检查/检验报告单号
'' DIAGNOSIS, --String(64)  √ 诊断
'内窥镜' ITEM_NAME,  --String(64)  √ 检查/检验项目
'' SPECIMEN_NAME, --String(64)    标本名称
'' SPECIMEN_ID  ,--String(64)   标本号
reportdate REPORT_TIME  ,--String(32) √ 报告时间，格式 YYYY-MM-DD HH24:MI:SS 
keshi DEPT_NAME ,--String(64) √ 科室名称
d.reportusername DOCTOR_NAME  ,--String(64) √ 医生姓名
'2' REPORT_TYPE ,--Int  √ 报告类型：0-普通检验报告1-药敏检验报告2-检查报告
'' REMARK --String(128)   备注
from [ccjhbSC].[dbo].report d
union
select 
hisid patient_id,
Roomid  REPORT_ID,  --String(32)  √ 检查/检验报告单号
'' DIAGNOSIS, --String(64)  √ 诊断
'彩超' ITEM_NAME, --String(64)  √ 检查/检验项目
'' SPECIMEN_NAME, --String(64)    标本名称
'' SPECIMEN_ID  ,--String(64)   标本号
reportdate REPORT_TIME  ,--String(32) √ 报告时间，格式 YYYY-MM-DD HH24:MI:SS 
keshi DEPT_NAME ,--String(64) √ 科室名称
d.reportusername DOCTOR_NAME  ,--String(64) √ 医生姓名
'2' REPORT_TYPE ,--Int  √ 报告类型：0-普通检验报告1-药敏检验报告2-检查报告
'' REMARK --String(128)   备注
from [ccjhbUS].[dbo].report d
union

select 
hisid patient_id,
Roomid  REPORT_ID,  --String(32)  √ 检查/检验报告单号
'' DIAGNOSIS, --String(64)  √ 诊断
'放射' ITEM_NAME, --String(64)  √ 检查/检验项目
'' SPECIMEN_NAME, --String(64)    标本名称
'' SPECIMEN_ID  ,--String(64)   标本号
reportdate REPORT_TIME  ,--String(32) √ 报告时间，格式 YYYY-MM-DD HH24:MI:SS 
keshi DEPT_NAME ,--String(64) √ 科室名称
d.reportusername DOCTOR_NAME  ,--String(64) √ 医生姓名
'2' REPORT_TYPE ,--Int  √ 报告类型：0-普通检验报告1-药敏检验报告2-检查报告
'' REMARK --String(128)   备注
from [ccjhbXR].[dbo].report d)

 t  where t.patient_id='{0}' and  REPORT_TIME>='{1}' and REPORT_TIME<='{2}'";
                sql = string.Format(sql, patInfo.PATIENT_ID, beginDate, endDate);
                DataTable pacsReport = BaseEntityToPacsSQL.Db.GetDataTable(sql);

                if (pacsReport == null || pacsReport.Rows.Count <= 0)
                {
                    dicErr.Add("800101", "检查/检验报告记录不存在，未查询到检查/检验报告记录");
                    return null;
                }
                
                outParam.PATIENT_IDCARD_TYPE = "1";
                outParam.PATIENT_IDCARD_NO = patInfo.IDNO;
                outParam.PATIENT_NAME = patInfo.PATIENT_NAME;
                outParam.PATIENT_SEX = patInfo.Sex;
                DateTime birthDay = DateTime.Now;
                if (!string.IsNullOrEmpty(patInfo.BirthDay) &&
                    patInfo.BirthDay.Length == 8)
                {
                    string strBirthDay = patInfo.BirthDay.Substring(0, 4) + "-" + patInfo.BirthDay.Substring(4, 2) + "-" + patInfo.BirthDay.Substring(6, 2);
                    DateTime.TryParse(strBirthDay, out birthDay);
                }
                else
                    DateTime.TryParse(patInfo.BirthDay, out birthDay);
                outParam.PATIENT_AGE = GetAge(birthDay).ToString();
                outParam.MEDICAL_INSURANNCE_TYPE = "自费";
                outParam.HOSP_PATIENT_ID = patInfo.PATIENT_ID;

                List<LisReportInfo> lisCheckLst = new List<LisReportInfo>();

                for (int i = 0; i < pacsReport.Rows.Count; i++)
                {
                    LisReportInfo lisCheck = new LisReportInfo();

                    lisCheck.REPORT_ID = pacsReport.Rows[0]["REPORT_ID"] == null ? "" : pacsReport.Rows[0]["REPORT_ID"].ToString();
                    lisCheck.DIAGNOSIS = pacsReport.Rows[i]["DIAGNOSIS"] == null ? "" : pacsReport.Rows[i]["DIAGNOSIS"].ToString();
                    lisCheck.ITEM_NAME = pacsReport.Rows[i]["ITEM_NAME"] == null ? "" : pacsReport.Rows[i]["ITEM_NAME"].ToString();
                    lisCheck.SPECIMEN_NAME = pacsReport.Rows[i]["SPECIMEN_NAME"] == null ? "" : pacsReport.Rows[i]["SPECIMEN_NAME"].ToString();
                    lisCheck.SPECIMEN_ID = pacsReport.Rows[i]["SPECIMEN_ID"] == null ? "" : pacsReport.Rows[i]["SPECIMEN_ID"].ToString();
                    lisCheck.REPORT_TIME = pacsReport.Rows[i]["REPORT_TIME"] == null ? "" : pacsReport.Rows[i]["REPORT_TIME"].ToString();
                    lisCheck.DEPT_NAME = pacsReport.Rows[i]["DIAGNOSIS"] == null ? "" : pacsReport.Rows[i]["DIAGNOSIS"].ToString();
                    lisCheck.ITEM_NAME = pacsReport.Rows[i]["ITEM_NAME"] == null ? "" : pacsReport.Rows[i]["ITEM_NAME"].ToString();
                    lisCheck.SPECIMEN_NAME = pacsReport.Rows[i]["SPECIMEN_NAME"] == null ? "" : pacsReport.Rows[i]["SPECIMEN_NAME"].ToString();
                    lisCheck.SPECIMEN_ID = pacsReport.Rows[i]["SPECIMEN_ID"] == null ? "" : pacsReport.Rows[i]["SPECIMEN_ID"].ToString();
                    lisCheck.REPORT_TIME = pacsReport.Rows[i]["REPORT_TIME"] == null ? "" : pacsReport.Rows[i]["REPORT_TIME"].ToString();
                    lisCheck.DEPT_NAME = pacsReport.Rows[i]["DEPT_NAME"] == null ? "" : pacsReport.Rows[i]["DEPT_NAME"].ToString();
                    lisCheck.DOCTOR_NAME = pacsReport.Rows[i]["DOCTOR_NAME"] == null ? "" : pacsReport.Rows[i]["DOCTOR_NAME"].ToString();
                    lisCheck.REMARK = pacsReport.Rows[i]["REMARK"] == null ? "" : pacsReport.Rows[i]["REMARK"].ToString();
                    lisCheckLst.Add(lisCheck);
                }
                outParam.REPORT_INFO = lisCheckLst;
            }
            catch (Exception ex)
            {
                dicErr.Add("800101", "检查/检验报告记录不存在，未查询到检查/检验报告记录（" + ex.ToString() + ")");
                return null;
            }

            return outParam;
        }

        public HealthCardNormalCheckReport_OutParam QueryNormalReportInfo(string reportID, HEALTHCARD_PATIENT_INFO patInfo, ref Dictionary<string, string> dicErr)
        {
            HealthCardNormalCheckReport_OutParam outParam = new HealthCardNormalCheckReport_OutParam();

            try
            {
                string sql = @"
                                 SELECT
                                 DISTINCT 
                                就诊卡号,
                                就诊号,
                                 报告ID REPORT_ID  ,--String(32)  ,--√ 检查/检验报告单号
                                临床诊断 DIAGNOSIS   ,--String(64)  ,--√ 诊断
                                检验目的  ITEM_NAME   ,--String(64)  ,--√ 检查/检验项目
                                标本类型名称 SPECIMEN_NAME   ,--String(64)   ,-- 标本名称
                                标本类型代码  SPECIMEN_ID   ,-- String(64)    标本号
                                报告时间 REPORT_TIME   ,--String(32) ,-- √ 报告时间，格式 YYYY-MM-DD HH24:MI:SS 
                                '' DEPT_NAME    ,-- 检验科室名称
                                检验医生姓名 DOCTOR_NAME   ,--REPORT_INFO String(64)    检验医生名称
                                审核医生姓名 REVIEW_NAME   ,--REPORT_INFO String(32)    审核者
                                审核时间 REVIEW_TIME   ,--REPORT_INFO String(32)    审核时间，格式 YYYY-MM-DD HH24:MI:SS
                                '' REMARK      --REPORT_INFO  String(128)   备注
                                FROM 
                                接口视图_检验报告结果   t   where  t.就诊卡号='{0}' and t.REPORT_ID='{1}'";
                sql = string.Format(sql, reportID, patInfo.PATIENT_ID);
                DataTable normalReport = BaseEntityToSQL.Db.GetDataTable(sql);

                if (normalReport == null || normalReport.Rows.Count <= 0)
                {
                    dicErr.Add("800201", "检查/检验报告单号不存在");
                    return null;
                }
                List<HealthCardNormalCheckReport> normalCheckLst = new List<HealthCardNormalCheckReport>();

                for (int i = 0; i < normalReport.Rows.Count; i++)
                {
                    HealthCardNormalCheckReport normalCheck = new HealthCardNormalCheckReport();

                    normalCheck.HOSP_PATIENT_ID = patInfo.PATIENT_ID;
                    normalCheck.HEALTH_CARD_ID = patInfo.HEALTH_CARD_ID;
                    normalCheck.PATIENT_IDCARD_TYPE = "1";
                    normalCheck.PATIENT_IDCARD_NO = patInfo.IDNO;
                    normalCheck.PATIENT_NAME = patInfo.PATIENT_NAME;
                    normalCheck.PATIENT_SEX = patInfo.Sex;
                    DateTime birthDay = DateTime.Now;
                    if (!string.IsNullOrEmpty(patInfo.BirthDay) &&
                        patInfo.BirthDay.Length == 8)
                    {
                        string strBirthDay = patInfo.BirthDay.Substring(0, 4) + "-" + patInfo.BirthDay.Substring(4, 2) + "-" + patInfo.BirthDay.Substring(6, 2);
                        DateTime.TryParse(strBirthDay, out birthDay);
                    }
                    else
                        DateTime.TryParse(patInfo.BirthDay, out birthDay);
                    normalCheck.PATIENT_AGE = GetAge(birthDay).ToString();
                    normalCheck.MEDICAL_INSURANNCE_TYPE = "自费";
                    normalCheck.DIAGNOSIS = normalReport.Rows[i]["DIAGNOSIS"] == null ? "" : normalReport.Rows[i]["DIAGNOSIS"].ToString();
                    normalCheck.ITEM_NAME = normalReport.Rows[i]["ITEM_NAME"] == null ? "" : normalReport.Rows[i]["ITEM_NAME"].ToString();
                    normalCheck.SPECIMEN_NAME = normalReport.Rows[i]["SPECIMEN_NAME"] == null ? "" : normalReport.Rows[i]["SPECIMEN_NAME"].ToString();
                    normalCheck.SPECIMEN_ID = normalReport.Rows[i]["SPECIMEN_ID"] == null ? "" : normalReport.Rows[i]["SPECIMEN_ID"].ToString();
                    normalCheck.REPORT_TIME = normalReport.Rows[i]["REPORT_TIME"] == null ? "" : normalReport.Rows[i]["REPORT_TIME"].ToString();
                    normalCheck.DEPT_NAME = normalReport.Rows[i]["DEPT_NAME"] == null ? "" : normalReport.Rows[i]["DEPT_NAME"].ToString();
                    normalCheck.DOCTOR_NAME = normalReport.Rows[i]["DOCTOR_NAME"] == null ? "" : normalReport.Rows[i]["DOCTOR_NAME"].ToString();
                    normalCheck.REVIEW_NAME = normalReport.Rows[i]["REVIEW_NAME"] == null ? "" : normalReport.Rows[i]["REVIEW_NAME"].ToString();
                    normalCheck.REVIEW_TIME = normalReport.Rows[i]["REVIEW_TIME"] == null ? "" : normalReport.Rows[i]["REVIEW_TIME"].ToString();
                    normalCheck.REMARK = normalReport.Rows[i]["REMARK"] == null ? "" : normalReport.Rows[i]["REMARK"].ToString();
                    string detailSql = @"
 SELECT
 
就诊卡号,
就诊号,
 报告ID REPORT_ID  ,--String(32)  ,--√ 检查/检验报告单号
 
报告项目名称 CHECK_NAME  ,--DETAIL  String(64)  √ 检验项目
检验结果 RESULT  ,--DETAIL  String(64)  √ 检验结果
结果单位 UNIT  ,--DETAIL  String(32)    单位
'' NORMAL_FLAG   ,--DETAIL  String(32)    结果正常标志，例：正常、偏高、偏低等
 参考范围 REFERENCE_VALUE  ,--DETAIL  String(32)    参考值
'' DESC1   --DETAIL String(128)   说明
FROM 
接口视图_检验报告结果 WHERE 细菌标志<>'2' t   where t.就诊卡号='{0}' and t.REPORT_ID='{1}'  and  t.CHECK_NAME='{2}'";

                    detailSql = string.Format(detailSql, patInfo.PATIENT_ID, reportID, normalCheck.ITEM_NAME);

                    DataTable detailReport = BaseEntityToSQL.Db.GetDataTable(detailSql);

                    List<HealthCardLisCheckReportDetail> checkReportDetailLst = new List<HealthCardLisCheckReportDetail>();
                    foreach (DataRow dataRow in detailReport.Rows)
                    {
                        HealthCardLisCheckReportDetail checkReportDetail = new HealthCardLisCheckReportDetail();
                        checkReportDetail.CHECK_NAME = dataRow["CHECK_NAME"] == null ? "" : dataRow["CHECK_NAME"].ToString();
                        checkReportDetail.RESULT = dataRow["RESULT"] == null ? "" : dataRow["RESULT"].ToString();
                        checkReportDetail.UNIT = dataRow["UNIT"] == null ? "" : dataRow["UNIT"].ToString();
                        //  checkReportDetail.NORMAL_FLAG=dataRow[""]
                        checkReportDetail.REFERENCE_VALUE = dataRow["REFERENCE_VALUE"] == null ? "" : dataRow["REFERENCE_VALUE"].ToString();

                        checkReportDetailLst.Add(checkReportDetail);
                    }
                    normalCheck.DETAIL = checkReportDetailLst;
                    normalCheckLst.Add(normalCheck);
                }
                outParam.REPORT_INFO = normalCheckLst;
            }
            catch (Exception ex)
            {
                dicErr.Add("800201", "检查/检验报告单号不存在,异常信息（" + ex.ToString() + ")");
                return null;
            }

            return outParam;
        }

        public HealthCardDrugReportInfo_OutParam QueryDrugReportInfo(string reportID, HEALTHCARD_PATIENT_INFO patInfo, ref Dictionary<string, string> dicErr)
        {
            HealthCardDrugReportInfo_OutParam outParam = new HealthCardDrugReportInfo_OutParam();

            try
            {
                string sql = @"
                                 SELECT
                                 DISTINCT 
                                就诊卡号,
                                就诊号,
                                 报告ID REPORT_ID  ,--String(32)  ,--√ 检查/检验报告单号
                                临床诊断 DIAGNOSIS   ,--String(64)  ,--√ 诊断
                                检验目的  ITEM_NAME   ,--String(64)  ,--√ 检查/检验项目
                                标本类型名称 SPECIMEN_NAME   ,--String(64)   ,-- 标本名称
                                标本类型代码  SPECIMEN_ID   ,-- String(64)    标本号
                                报告时间 REPORT_TIME   ,--String(32) ,-- √ 报告时间，格式 YYYY-MM-DD HH24:MI:SS 
                                '' DEPT_NAME    ,-- 检验科室名称
                                检验医生姓名 DOCTOR_NAME   ,--REPORT_INFO String(64)    检验医生名称
                                审核医生姓名 REVIEW_NAME   ,--REPORT_INFO String(32)    审核者
                                审核时间 REVIEW_TIME   ,--REPORT_INFO String(32)    审核时间，格式 YYYY-MM-DD HH24:MI:SS
                                '' REMARK      --REPORT_INFO  String(128)   备注
                                FROM 
                                接口视图_检验报告结果   t   where  t.就诊卡号='{0}' and t.REPORT_ID='{1}'";
                sql = string.Format(sql, reportID, patInfo.PATIENT_ID);
                DataTable drugReport = BaseEntityToSQL.Db.GetDataTable(sql);

                if (drugReport == null || drugReport.Rows.Count <= 0)
                {
                    dicErr.Add("800301", "检查/检验报告单号不存在");
                    return null;
                }
                List<HealthCardDrugReport> drugCheckLst = new List<HealthCardDrugReport>();

                for (int i = 0; i < drugReport.Rows.Count; i++)
                {
                    HealthCardDrugReport normalCheck = new HealthCardDrugReport();

                    normalCheck.HOSP_PATIENT_ID = patInfo.PATIENT_ID;
                    normalCheck.HEALTH_CARD_ID = patInfo.HEALTH_CARD_ID;
                    normalCheck.PATIENT_IDCARD_TYPE = patInfo.PATIENT_ID_TYPE;
                    normalCheck.PATIENT_IDCARD_NO = patInfo.IDNO;
                    normalCheck.PATIENT_NAME = patInfo.PATIENT_NAME;
                    normalCheck.PATIENT_SEX = patInfo.Sex;
                    DateTime birthDay = DateTime.Now;
                    if (!string.IsNullOrEmpty(patInfo.BirthDay) &&
                        patInfo.BirthDay.Length == 8)
                    {
                        string strBirthDay = patInfo.BirthDay.Substring(0, 4) + "-" + patInfo.BirthDay.Substring(4, 2) + "-" + patInfo.BirthDay.Substring(6, 2);
                        DateTime.TryParse(strBirthDay, out birthDay);
                    }
                    else
                        DateTime.TryParse(patInfo.BirthDay, out birthDay);
                    normalCheck.PATIENT_AGE = GetAge(birthDay).ToString();
                    normalCheck.MEDICAL_INSURANNCE_TYPE = "自费";
                    normalCheck.DIAGNOSIS = drugReport.Rows[i]["DIAGNOSIS"] == null ? "" : drugReport.Rows[i]["DIAGNOSIS"].ToString();
                    normalCheck.ITEM_NAME = drugReport.Rows[i]["ITEM_NAME"] == null ? "" : drugReport.Rows[i]["ITEM_NAME"].ToString();
                    normalCheck.SPECIMEN_NAME = drugReport.Rows[i]["SPECIMEN_NAME"] == null ? "" : drugReport.Rows[i]["SPECIMEN_NAME"].ToString();
                    normalCheck.SPECIMEN_ID = drugReport.Rows[i]["SPECIMEN_ID"] == null ? "" : drugReport.Rows[i]["SPECIMEN_ID"].ToString();
                    normalCheck.REPORT_TIME = drugReport.Rows[i]["REPORT_TIME"] == null ? "" : drugReport.Rows[i]["REPORT_TIME"].ToString();
                    normalCheck.DEPT_NAME = drugReport.Rows[i]["DEPT_NAME"] == null ? "" : drugReport.Rows[i]["DEPT_NAME"].ToString();
                    normalCheck.DOCTOR_NAME = drugReport.Rows[i]["DOCTOR_NAME"] == null ? "" : drugReport.Rows[i]["DOCTOR_NAME"].ToString();
                    normalCheck.REVIEW_NAME = drugReport.Rows[i]["REVIEW_NAME"] == null ? "" : drugReport.Rows[i]["REVIEW_NAME"].ToString();
                    normalCheck.REVIEW_TIME = drugReport.Rows[i]["REVIEW_TIME"] == null ? "" : drugReport.Rows[i]["REVIEW_TIME"].ToString();
                    normalCheck.REMARK = drugReport.Rows[i]["REMARK"] == null ? "" : drugReport.Rows[i]["REMARK"].ToString();
                    string detailSql = @"
 SELECT
 
就诊卡号,
就诊号,
 报告ID REPORT_ID  ,--String(32)  ,--√ 检查/检验报告单号
 
报告项目名称 CHECK_NAME  ,--DETAIL  String(64)  √ 检验项目
检验结果 RESULT  ,--DETAIL  String(64)  √ 检验结果
结果单位 UNIT  ,--DETAIL  String(32)    单位
'' NORMAL_FLAG   ,--DETAIL  String(32)    结果正常标志，例：正常、偏高、偏低等
 参考范围 REFERENCE_VALUE  ,--DETAIL  String(32)    参考值
'' DESC1   --DETAIL String(128)   说明
FROM 
接口视图_检验报告结果 WHERE 细菌标志='2' t   where t.就诊卡号='{0}' and t.REPORT_ID='{1}'  and  t.CHECK_NAME='{2}'";

                    detailSql = string.Format(detailSql, patInfo.PATIENT_ID, reportID, normalCheck.ITEM_NAME);

                    DataTable detailReport = BaseEntityToSQL.Db.GetDataTable(detailSql);

                    List<HealthCardDrugReportDetail> drugReportDetailLst = new List<HealthCardDrugReportDetail>();
                    foreach (DataRow dataRow in detailReport.Rows)
                    {
                        HealthCardDrugReportDetail drugReportDetail = new HealthCardDrugReportDetail();
                        drugReportDetail.CHECK_NAME = dataRow["CHECK_NAME"] == null ? "" : dataRow["CHECK_NAME"].ToString();

                        string drugSql = @" SELECT
 报告ID REPORT_ID  ,--String(32)  ,--√ 检查/检验报告单号
药敏名称 DRUG_NAME	,--DRUG_INFO	String(64)	√	药品名称
药敏英文 DRUG_ENGLIST_NAME	,--DRUG_INFO	String(32)		药品英文名称
''MIC	,--DRUG_INFO	String(32)		最小抑菌浓度
''SENSITIVITY	,--DRUG_INFO	String(32)	√	敏感程度，例：耐药，中介，敏感等 
'' DESC1	 --DETAIL	String(128)		说明
FROM 
接口视图_院感_药敏结果  t where  t.REPORT_ID='{0}' ";
                        drugSql = string.Format(drugSql, dataRow["REPORT_ID"].ToString());

                        DataTable drugDetail = BaseEntityToSQL.Db.GetDataTable(drugSql);

                        if (drugDetail == null || drugDetail.Rows.Count <= 0)
                        {

                        }
                        List<HealthCardDrugInfo> itemLst = new List<HealthCardDrugInfo>();

                        foreach (DataRow item in drugDetail.Rows)
                        {
                            HealthCardDrugInfo drugInfo = new HealthCardDrugInfo();
                            drugInfo.DRUG_NAME = item["DRUG_NAME"] == null ? "" : item["DRUG_NAME"].ToString();
                            drugInfo.DRUG_ENGLIST_NAME = item["DRUG_ENGLIST_NAME"] == null ? "" : item["DRUG_ENGLIST_NAME"].ToString();
                            drugInfo.MIC = item["MIC"] == null ? "" : item["MIC"].ToString();
                            drugInfo.SENSITIVITY = item["SENSITIVITY"] == null ? "" : item["SENSITIVITY"].ToString();
                            drugInfo.DESC = item["DESC1"] == null ? "" : item["DESC1"].ToString();
                            itemLst.Add(drugInfo);
                        }
                        drugReportDetail.DRUG_INFO = itemLst;
                        drugReportDetailLst.Add(drugReportDetail);
                    }
                    normalCheck.REPORT_DETAIL = drugReportDetailLst;
                    drugCheckLst.Add(normalCheck);
                }
                outParam.REPORT_INFO = drugCheckLst;
            }
            catch (Exception ex)
            {
                dicErr.Add("800201", "检查/检验报告单号不存在,异常信息（" + ex.ToString() + ")");
                return null;
            }

            return outParam;
        }

        public HealthCardPacsCheckReport_OutParam QueryPacsReportInfo(string reportID, HEALTHCARD_PATIENT_INFO patInfo, ref Dictionary<string, string> dicErr)
        {
            HealthCardPacsCheckReport_OutParam outParam = new HealthCardPacsCheckReport_OutParam();

            try
            {
                string sql = @"
                                 
select  patient_id,SPECIMEN_ID,ITEM_NAME,COMPLAINT,DIAGNOSIS,SEEN,CONTENT,REPORT_TIME,DEPT_NAME,DOCTOR_NAME,REVIEW_NAME,REVIEW_TIME,REMARK from (
select 
Hospitalid patient_id, 
 '' SPECIMEN_ID , --String(64)    标本号''
 '' ITEM_NAME , --String(64)  √ 检查项目
''COMPLAINT , --String(128)   主诉
''DIAGNOSIS , --String(64)  √ 诊断
biaoxian SEEN , --String(64)  √ 检查所见
'' CONTENT   ,  --String(128)   报告内容
reportdate REPORT_TIME  , --String(32)  √ 报告时间，格式 YYYY-MM-DD HH24:MI:SS 
'' DEPT_NAME  , --String(64)    检查科室名称
d.reportusername DOCTOR_NAME  , --String(64)    检查医生名称
'' REVIEW_NAME  , --String(32)    审核者
'' REVIEW_TIME  , --String(32)    审核时间，格式 YYYY-MM-DD HH24:MI:SS
'' REMARK   --String(128)   备注
from [ccjhbSC].[dbo].report d
union
select 
Hospitalid patient_id, 
 '' SPECIMEN_ID , --String(64)    标本号''
 '' ITEM_NAME , --String(64)  √ 检查项目
''COMPLAINT , --String(128)   主诉
''DIAGNOSIS , --String(64)  √ 诊断
biaoxian SEEN , --String(64)  √ 检查所见
'' CONTENT   ,  --String(128)   报告内容
reportdate REPORT_TIME  , --String(32)  √ 报告时间，格式 YYYY-MM-DD HH24:MI:SS 
'' DEPT_NAME  , --String(64)    检查科室名称
d.reportusername DOCTOR_NAME  , --String(64)    检查医生名称
'' REVIEW_NAME  , --String(32)    审核者
'' REVIEW_TIME  , --String(32)    审核时间，格式 YYYY-MM-DD HH24:MI:SS
'' REMARK   --String(128)   备注
from [ccjhbUS].[dbo].report d
union

select 
Hospitalid patient_id, 
 '' SPECIMEN_ID , --String(64)    标本号''
 '' ITEM_NAME , --String(64)  √ 检查项目
''COMPLAINT , --String(128)   主诉
''DIAGNOSIS , --String(64)  √ 诊断
biaoxian SEEN , --String(64)  √ 检查所见
'' CONTENT   ,  --String(128)   报告内容
reportdate REPORT_TIME  , --String(32)  √ 报告时间，格式 YYYY-MM-DD HH24:MI:SS 
'' DEPT_NAME  , --String(64)    检查科室名称
d.reportusername DOCTOR_NAME  , --String(64)    检查医生名称
'' REVIEW_NAME  , --String(32)    审核者
'' REVIEW_TIME  , --String(32)    审核时间，格式 YYYY-MM-DD HH24:MI:SS
'' REMARK   --String(128)   备注
from [ccjhbXR].[dbo].report d) t
where  ";
                sql = string.Format(sql, reportID, patInfo.PATIENT_ID);
                DataTable pacsReport = BaseEntityToPacsSQL.Db.GetDataTable(sql);

                if (pacsReport == null || pacsReport.Rows.Count <= 0)
                {
                    dicErr.Add("800401", "检查/检验报告单号不存在");
                    return null;
                }

                outParam.DIAGNOSIS = pacsReport.Rows[0]["DIAGNOSIS"] == null ? "" : pacsReport.Rows[0]["DIAGNOSIS"].ToString();
                outParam.ITEM_NAME = pacsReport.Rows[0]["ITEM_NAME"] == null ? "" : pacsReport.Rows[0]["ITEM_NAME"].ToString();
                outParam.CONTENT = pacsReport.Rows[0]["CONTENT"] == null ? "" : pacsReport.Rows[0]["CONTENT"].ToString();
                outParam.COMPLAINT = pacsReport.Rows[0]["COMPLAINT"] == null ? "" : pacsReport.Rows[0]["COMPLAINT"].ToString();

                outParam.SPECIMEN_ID = pacsReport.Rows[0]["SPECIMEN_ID"] == null ? "" : pacsReport.Rows[0]["SPECIMEN_ID"].ToString();
                outParam.REPORT_TIME = pacsReport.Rows[0]["REPORT_TIME"] == null ? "" : pacsReport.Rows[0]["REPORT_TIME"].ToString();
                outParam.DEPT_NAME = pacsReport.Rows[0]["DEPT_NAME"] == null ? "" : pacsReport.Rows[0]["DEPT_NAME"].ToString();
                outParam.DOCTOR_NAME = pacsReport.Rows[0]["DOCTOR_NAME"] == null ? "" : pacsReport.Rows[0]["DOCTOR_NAME"].ToString();
                outParam.REVIEW_NAME = pacsReport.Rows[0]["REVIEW_NAME"] == null ? "" : pacsReport.Rows[0]["REVIEW_NAME"].ToString();
                outParam.REVIEW_TIME = pacsReport.Rows[0]["REVIEW_TIME"] == null ? "" : pacsReport.Rows[0]["REVIEW_TIME"].ToString();
                outParam.REMARK = pacsReport.Rows[0]["REMARK"] == null ? "" : pacsReport.Rows[0]["REMARK"].ToString();
                  
            }
            catch (Exception ex)
            {
                dicErr.Add("800401", "检查/检验报告单号不存在,异常信息（" + ex.ToString() + ")");
                return null;
            }

            return outParam;
        }
        #endregion

        #region 私有方法
        public CLINIC_MASTER GetClinicMaster(string billNO)
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
                       where CLINIC_MASTER.serialnumber = '{0}' ";

            sql = string.Format(sql, billNO);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count != 0)
                return DataSetToEntity.DataSetToT<CLINIC_MASTER>(ds).First();
            else
                return null;
        }

        private void CreateRegInfoByRow(DataRow row, ref HealthCardRegInfo healthCardRegInfo)
        {
            string regDate = Convert.ToDateTime(row["clinic_date"].ToString()).ToString("yyyy-MM-dd");
            string[] strRegDate = regDate.Split('-');
            string regID = row["doctor_code"].ToString() + "-" + strRegDate[0] + strRegDate[1].PadLeft(2, '0') + strRegDate[2].PadLeft(2, '0');
            string limitNum = row["appointment_limits"].ToString();
            if (healthCardRegInfo != null)
            {
                healthCardRegInfo.RegTimeLst.AddRange(GenerateRegTimeInfos(row, regID, limitNum, limitNum));
            }
            else
            {
                healthCardRegInfo = new HealthCardRegInfo();
                healthCardRegInfo.REG_DATE = regDate;
                healthCardRegInfo.REG_WEEKDAY = CaculateWeekDay(Convert.ToInt32(strRegDate[0]), Convert.ToInt32(strRegDate[1]), Convert.ToInt32(strRegDate[2]));
                healthCardRegInfo.RegTimeLst = GenerateRegTimeInfos(row, regID, limitNum, limitNum);
            }
        }

        private List<HealthCardRegTimeInfo> GenerateRegTimeInfos(DataRow row, string regID, string totalSource, string residueSourceNums)
        {
            List<HealthCardRegTimeInfo> healthCardRegTimeInfos = new List<HealthCardRegTimeInfo>();
            string regTime = row["time_desc"].ToString();
            string regFee = row["regprice"].ToString();
            string treateFee = row["diagprice"].ToString();

            // 1  上午 2  下午   3 晚上
            if (regTime == "昼夜")
            {
                healthCardRegTimeInfos.Add(GenerateRegTimeInfo(regID + "A", "1", regFee, treateFee, totalSource, residueSourceNums));
                healthCardRegTimeInfos.Add(GenerateRegTimeInfo(regID + "B", "2", regFee, treateFee, totalSource, residueSourceNums));
                healthCardRegTimeInfos.Add(GenerateRegTimeInfo(regID + "C", "3", regFee, treateFee, totalSource, residueSourceNums));
            }
            else if (regTime == "白天")
            {
                healthCardRegTimeInfos.Add(GenerateRegTimeInfo(regID + "A", "1", regFee, treateFee, totalSource, residueSourceNums));
                healthCardRegTimeInfos.Add(GenerateRegTimeInfo(regID + "B", "2", regFee, treateFee, totalSource, residueSourceNums));
            }
            else if (regTime == "上午")
            {
                healthCardRegTimeInfos.Add(GenerateRegTimeInfo(regID + "A", "1", regFee, treateFee, totalSource, residueSourceNums));
            }
            else if (regTime == "下午")
            {
                healthCardRegTimeInfos.Add(GenerateRegTimeInfo(regID + "B", "2", regFee, treateFee, totalSource, residueSourceNums));
            }
            else
            {
                healthCardRegTimeInfos.Add(GenerateRegTimeInfo(regID + "C", "3", regFee, treateFee, totalSource, residueSourceNums));
            }
            return healthCardRegTimeInfos;
        }

        private HealthCardRegTimeInfo GenerateRegTimeInfo(string regID, string regTime, string regFee, string treateFee, string totalSource, string residueSourceNums)
        {
            HealthCardRegTimeInfo healthCardRegTimeInfo = new HealthCardRegTimeInfo();
            healthCardRegTimeInfo.REG_ID = regID;
            healthCardRegTimeInfo.TIME_FLAG = regTime;
            healthCardRegTimeInfo.REG_STATUS = "1";
            healthCardRegTimeInfo.TOTAL = totalSource;
            healthCardRegTimeInfo.OVER_COUNT = residueSourceNums;
            healthCardRegTimeInfo.REG_FEE = (Convert.ToInt32(regFee) * 100).ToString();
            healthCardRegTimeInfo.TREAT_FEE = (Convert.ToInt32(treateFee) * 100).ToString();
            healthCardRegTimeInfo.ISTIME = "0";
            return healthCardRegTimeInfo;
        }

        private HealthCardTimeRegInfo GenerateTimeRegInfo(string regID, string timeFlag, string beginTime, string endTime, string totalSource, string residueSourceNums)
        {
            HealthCardTimeRegInfo healthCardTimeRegInfo = new HealthCardTimeRegInfo();
            healthCardTimeRegInfo.REG_ID = regID;
            healthCardTimeRegInfo.TIME_FLAG = timeFlag;

            healthCardTimeRegInfo.TOTAL = totalSource;
            healthCardTimeRegInfo.OVER_COUNT = residueSourceNums;
            healthCardTimeRegInfo.BEGIN_TIME = beginTime;
            healthCardTimeRegInfo.END_TIME = endTime;

            return healthCardTimeRegInfo;
        }

        /// <summary>
        /// 计算具体某个日期是星期几
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="d">日</param>
        /// <returns></returns>
        private string CaculateWeekDay(int y, int m, int d)
        {
            if (m == 1 || m == 2)
            {
                m += 12;
                y--;
            }
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7;
            string weekstr = "";
            switch (week)
            {
                case 0: weekstr = "星期一"; break;
                case 1: weekstr = "星期二"; break;
                case 2: weekstr = "星期三"; break;
                case 3: weekstr = "星期四"; break;
                case 4: weekstr = "星期五"; break;
                case 5: weekstr = "星期六"; break;
                case 6: weekstr = "星期七"; break;
            }
            return weekstr;
        }
 
        private   int GetAge(DateTime Birthdate)
        {
            DateTime curTime = HisDBLayer.Common.getServerTime();
            int age = curTime.Year - Birthdate.Year;
            if (curTime.Year < Birthdate.AddYears(age).Year)
                age--;
            return age;
        }

        private PayRecordInfo GeneratePayRecordInfo(DataRow row)
        {
            PayRecordInfo recordInfo = new PayRecordInfo();
            recordInfo.DEPT_NAME = row["deptname"] == null ? "" : row["deptname"].ToString();
            recordInfo.DOCTOR_NAME = row["doctorname"] == null ? "" : row["doctorname"].ToString();
            recordInfo.HOSP_SEQUENCE = row["invoice_no"] == null ? "" : row["invoice_no"].ToString();
            recordInfo.ORDER_STATUS = "1";
            recordInfo.PAY_AMOUT = row["tot_cost"] == null ? "" : row["tot_cost"].ToString();

            if (row["invoice_state"] != null && row["invoice_state"].ToString() == "1")
            {
                recordInfo.RECEIPT_DATE = row["backfee_oper_date"] == null ? "" : row["backfee_oper_date"].ToString();
            }
            else
                recordInfo.RECEIPT_DATE = row["fee_oper_date"] == null ? "" : row["fee_oper_date"].ToString();
            recordInfo.RECEIPT_ID = row["rcpt_no"] == null ? "" : row["rcpt_no"].ToString();
            return recordInfo;
        }
        #endregion
    }
}
