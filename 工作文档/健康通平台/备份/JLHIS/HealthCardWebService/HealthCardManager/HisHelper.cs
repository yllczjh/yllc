using Cloud.Db;
using HisCommon.DataEntity;
using HttpToken;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;

namespace HealthCardManager
{
    public  class HisHelper
    {
        #region 局部静态变量
        private static HisDBLayer.HealthCardDbProcess healthCardManager = new HisDBLayer.HealthCardDbProcess();

        private static HisDBLayer.OutpFeeManager outFeeManager = new HisDBLayer.OutpFeeManager();
        private static IDbHelper dbhelper = Db_Common.Get_DbHelper();
        /// <summary>
        /// 预约挂号的账号
        /// </summary>
        private static string userID = "9966";

        #endregion

        #region 公有方法
        public static string CreatePatInfo(HEALTHCARD_PATIENT_INFO _PATIENT_INFO)
        {
            string patientID = healthCardManager.GetHealthPatID(_PATIENT_INFO.HEALTH_CARD_ID);

            // 通过电子健康卡获取患者信息
            Dictionary<string, string> dicHealthCardInfo = new Dictionary<string, string>();

            TokenBis.GetCardInfo(_PATIENT_INFO.HEALTH_CARD_ID, "", ref dicHealthCardInfo);

            _PATIENT_INFO.BirthDay = dicHealthCardInfo["birthday"];
            _PATIENT_INFO.Sex = dicHealthCardInfo["gender_code"];
            _PATIENT_INFO.Address = dicHealthCardInfo["home_address"];

            _PATIENT_INFO.PATIENT_ID_TYPE = dicHealthCardInfo["id_card_type_code"];
            _PATIENT_INFO.PATIENT_ID_CARD = dicHealthCardInfo["id_card_value"];
            _PATIENT_INFO.PATIENT_NAME = dicHealthCardInfo["name"];
            if (string.IsNullOrEmpty(patientID))
            {
                healthCardManager.InsertHealthPatInfo(_PATIENT_INFO, ref patientID);
            }
            return patientID;
        }

        public static Healthcard_HosInfo GetHosInfo(string hosID)
        {
            List<Healthcard_HosInfo> lst = healthCardManager.GetHosInfo(hosID);

            if (lst.Count > 0)
                return lst.First();
            else
                return null;
        }

        //public static HealthCardDeptInfo_OutParam GetDeptInfo(string deptID)
        //{
        //    HealthCardDeptInfo_OutParam deptInfo_OutParam = new
        //        HealthCardDeptInfo_OutParam();
        //    List<HealthCardDeptInfo> healthCardDeptInfos = new List<HealthCardDeptInfo>();

        //    List<HealthCardDeptInfo> lst = healthCardManager.GetDeptInfo(deptID);

        //    foreach (HealthCardDeptInfo item in lst)
        //    {
        //        string strDeptID = item.DEPT_ID.Substring(0, 2);
        //        if (item != null && strDeptID.Length > 0)
        //        {
        //            var parentDept = lst.Find(t => t.DEPT_ID == strDeptID);
        //            item.PARENT_ID = parentDept == null ? "-1" : parentDept.DEPT_ID;
        //        }
        //        else
        //            item.PARENT_ID = "-1";

        //        healthCardDeptInfos.Add(item);
        //    }

        //    deptInfo_OutParam.DEPT_LIST = healthCardDeptInfos;
        //    return deptInfo_OutParam;
        //}

        public static string GetDeptInfo(string req_fun_code, string req_bxml)
        {
            OracleParameter[] pra = new OracleParameter[] {
                new OracleParameter("Str_传入编号",OracleType.VarChar,50),
                    new OracleParameter("Str_传入",OracleType.VarChar,500),
                    new OracleParameter("lob_传出",OracleType.Clob)
                };
            pra[0].Direction = ParameterDirection.Input;
            pra[0].Value = req_fun_code;
            pra[1].Direction = ParameterDirection.Input;
            pra[1].Value = req_bxml;
            pra[2].Direction = ParameterDirection.Output;


            dbhelper.RunProcedure("PR_互联互通_科室查询", pra);

            return pra[2].Value.ToString();

        }

        public static string M_获取响应参数(string str_功能号,string str_请求参数,ref int res_code, ref string res_msg)
        {
            OracleParameter[] pra = new OracleParameter[] {
                new OracleParameter("str_功能号",OracleType.VarChar,50),
                    new OracleParameter("str_请求参数",OracleType.VarChar,500),
                    new OracleParameter("lob_响应参数",OracleType.Clob),
                    new OracleParameter("res_code",OracleType.Int16),
                    new OracleParameter("res_msg",OracleType.VarChar,100)
                };
            pra[0].Direction = ParameterDirection.Input;
            pra[0].Value = str_功能号;
            pra[1].Direction = ParameterDirection.Input;
            pra[1].Value = str_请求参数;
            pra[2].Direction = ParameterDirection.Output;
            pra[3].Direction = ParameterDirection.Output;
            pra[4].Direction = ParameterDirection.Output;


            dbhelper.RunProcedure("PR_互联互通_总线调用", pra);
            res_code = int.Parse(pra[3].Value.ToString());
            res_msg = pra[4].Value.ToString();
            return pra[2].Value.ToString();
        }


        public static string GetDoctorInfo(string req_fun_code, string req_bxml)
        {
            OracleParameter[] pra = new OracleParameter[] {
                new OracleParameter("Str_传入编号",OracleType.VarChar,50),
                    new OracleParameter("Str_传入",OracleType.VarChar,500),
                    new OracleParameter("Str_传出",OracleType.VarChar,2000)
                };
            pra[0].Direction = ParameterDirection.Input;
            pra[0].Value = req_fun_code;
            pra[1].Direction = ParameterDirection.Input;
            pra[1].Value = req_bxml;
            pra[2].Direction = ParameterDirection.Output;

            dbhelper.RunProcedure("PR_病案首页_测试用", pra);
            return pra[2].Value.ToString();
        }
        #region 不要了
        
        //public static HealthCardDoctorInfo_OutParam GetDoctorInfo(string deptID, string doctorID)
        //{
        //    HealthCardDoctorInfo_OutParam doctorInfo_OutParam = new
        //        HealthCardDoctorInfo_OutParam();
        //    List<HealthCardDoctorInfo> healthCardDoctorInfos = new List<HealthCardDoctorInfo>();

        //    List<HealthCardDoctorInfo> lst = healthCardManager.GetDoctorInfo(deptID, doctorID);

        //    doctorInfo_OutParam.Doctor_LIST = lst;
        //    return doctorInfo_OutParam;
        //}
        #endregion
        public static HealthCardRegInfo_OutParam GetRegInfo(string deptID, string doctorID, DateTime beginDate, DateTime endDate)
        {
            return healthCardManager.GetRegInfo(deptID, doctorID, beginDate, endDate);
        }

        public static HealthCardTimeRegInfo_OutParam GetTimeRegInfo(string deptID, string doctorID, DateTime clinicDate, string timeFlag)
        {
            return healthCardManager.GetTimeRegInfo(deptID, doctorID, clinicDate, timeFlag);
        }

        public static HealthCardOrderRegInfo_OutParam SaveOrderRegInfo(HealthCardOrderRegInfo_InParam regInfo_InParam,ref Dictionary<string,string> dicErr)
        {
            //Dictionary<string, string> dicHealthCardInfo = new Dictionary<string, string>();

            //TokenBis.GetCardInfo(regInfo_InParam.HEALTH_CARD_ID, "", ref dicHealthCardInfo);

            var patientInfo = healthCardManager.GetHealthPatInfo(regInfo_InParam.HOSP_PATIENT_ID);
            if (string.IsNullOrEmpty(regInfo_InParam.HOSP_PATIENT_ID)||patientInfo==null)
            {
                dicErr.Add("200703", "用户建档失败");
                return null;
            }

            var clinic_master = ConvertMaster(regInfo_InParam, patientInfo);
            var master_Index = ConvertMasterIndex(regInfo_InParam, patientInfo);
 
            return healthCardManager.SaveOrderRegInfo(clinic_master, master_Index,regInfo_InParam,dicErr);
        }
        
        public  static  HealthCardPayRegInfo_OutParam  SavePayRegInfo(HealthCardPayRegInfo_InParam payRegInfo, ref Dictionary<string, string>  dicErr)
        {
            fin_invoiceinfo_record invoiceinfo_Record = ConvertFinInvoiceRecord(payRegInfo);

            CLINIC_MASTER clinic_master = ConvertClinicMasterByFinInvoice(payRegInfo);
        
            return healthCardManager.SavePayRegInfo(invoiceinfo_Record, payRegInfo, clinic_master,ref dicErr);
        }

        public static bool SaveCancelRegInfo(HealthCardCancelRegInfo_InParam cancelRegInfo, ref Dictionary<string, string> dicErr)
        {
            return healthCardManager.SaveCancelRegInfo(cancelRegInfo, userID, ref dicErr);
        }

        public static HealthCardRefundRegInfo_OutParam SaveRefundRegInfo(HealthCardRefundRegInfo_InParam refundRegInfo, ref Dictionary<string, string> dicErr)
        {
            fin_invoiceinfo_record invoiceinfo_Record = ConvertRefundFinInvoiceRecord(refundRegInfo);

            return healthCardManager.SaveRefundRegInfo(refundRegInfo, invoiceinfo_Record, ref dicErr);
        }

        public static HealthCardGetRegNumInfo_OutParam GetRegInfo(HealthCardGetRegNumInfo_InParam regNumInfo, ref Dictionary<string, string> dicErr)
        {
            return healthCardManager.GetRegNumInfo(regNumInfo, ref dicErr);
        }

        public  static HealthCardQueryRecordInfo_OutParam QueryOrderRegRecordInfo(HealthCardQueryRecordInfo_InParam recordInfo_InParam ,ref  Dictionary<string,string> dicErr)
        {
            HealthCardQueryRecordInfo_OutParam outParam = new HealthCardQueryRecordInfo_OutParam();

            IList<CLINIC_MASTER>  clinicMasterLst=  healthCardManager.QueryOrderRegInfoLst(recordInfo_InParam.BEGIN_DATE, recordInfo_InParam.END_DATE, int.Parse(recordInfo_InParam.PAGE_CURRENT), int.Parse(recordInfo_InParam.PAGE_SIZE),recordInfo_InParam.HOSP_PATIENT_ID);

            if(clinicMasterLst != null&& clinicMasterLst.Count > 0)
            {
                outParam.COUNT = clinicMasterLst.Count.ToString();
                List<HealthCardQueryRecordInfo> lst = new List<HealthCardQueryRecordInfo>();

                foreach (var tempClinic in clinicMasterLst)
                {
                    HealthCardQueryRecordInfo recordInfo = new HealthCardQueryRecordInfo();
                    
                      DataTable  dtOrderID   =healthCardManager.GetPlatformOrderInfo(tempClinic.BILLNO, tempClinic.PATIENT_ID);
                    if (dtOrderID != null && dtOrderID.Rows.Count > 0)
                    {
                        recordInfo.ORDER_ID = dtOrderID.Rows[0]["order_id"].ToString();
                        recordInfo.ORDER_STATUS= dtOrderID.Rows[0]["order_status"].ToString();
                        recordInfo.CANCEL_DATE= dtOrderID.Rows[0]["cancel_date"].ToString();
                        recordInfo.REFUND_FLAG = dtOrderID.Rows[0]["refund_flag"].ToString();
                    }
                    recordInfo.HOSP_SERIAL_NUM = tempClinic.SERIAL_NO.ToString();
                    recordInfo.HOSP_MEDICAL_NUM = tempClinic.PATIENT_ID;
                    lst.Add(recordInfo);
                }
                outParam.ORDER_LIST = lst;
            }
            else
            {
                dicErr.Add("201201", "未查询到挂号订单记录");
                outParam.COUNT = "0";
            }
            return outParam;
        }

        public static HealthCardPayListInfo_OutParam GetPayListInfo(HealthCardPayListInfo_InParam payListInfo_InParam, ref Dictionary<string, string> dicErr)
        {

            HealthCardPayListInfo_OutParam outParam = new HealthCardPayListInfo_OutParam();
            //Dictionary<string, string> dicHealthCardInfo = new Dictionary<string, string>();

            //TokenBis.GetCardInfo(payListInfo_InParam.HEALTH_CARD_ID, "", ref dicHealthCardInfo);

            //string patientID = healthCardManager.GetHealthPatID(payListInfo_InParam.HEALTH_CARD_ID);
            var patientInfo = outFeeManager.GetPatientInforByID(payListInfo_InParam.HOSP_PATIENT_ID);
            if (patientInfo==null)
            {
                dicErr.Add("200703", "用户建档失败");
                return null;
            }
            outParam.HEALTH_CARD_ID = payListInfo_InParam.HEALTH_CARD_ID;
            outParam.USER_NAME = patientInfo.NAME;
            outParam.IDCARD_TYPE = "1";
            outParam.IDCARD_NO = patientInfo.ID_NO;
            outParam.CARD_TYPE = "1";
            outParam.CARD_NO = payListInfo_InParam.HEALTH_CARD_ID;
            outParam.HOSP_PATIENT_ID = patientInfo.PATIENT_ID;

             List< PayRecordInfo >  payRecords= healthCardManager.GetPayListInfoByCondition(patientInfo.PATIENT_ID, payListInfo_InParam.BEGIN_DATE + " 00:00:00", payListInfo_InParam.END_DATE + " 23:59:59", payListInfo_InParam.QUERY_TYPE);

            if (payRecords.Count <= 0)
            {
                dicErr.Add("300101", "缴费记录不存在，未查询到缴费订单记录");
                return outParam;
            }
            outParam.PAY_LIST = payRecords;

            return outParam;
        }

        public static HealthCardPayDetailInfo_OutParam GetFeePayDetailInfo(HealthCardPayDetailInfo_InParam payDetailInfo_InParam, ref Dictionary<string, string> dicErr)
        {
            var patientInfo = outFeeManager.GetPatientInforByID(payDetailInfo_InParam.HOSP_PATIENT_ID);
            if (patientInfo == null)
            {
                dicErr.Add("200703", "用户建档失败");
                return null;
            }

            var  outParam=  healthCardManager.GetPayDetailInfoByOrderID(payDetailInfo_InParam.HOSP_SEQUENCE, payDetailInfo_InParam.HOSP_PATIENT_ID,ref dicErr);
            outParam.USER_NAME = patientInfo.NAME;
            return outParam;
        }

        public static HealthCardPayOrderInfo_OutParam GetFeePayOrderInfo(HealthCardPayOrderInfo_InParam payOrderInfo_InParam, ref Dictionary<string, string> dicErr)
        {
            HealthCardPayOrderInfo_OutParam outParam = new HealthCardPayOrderInfo_OutParam();
            string errMsg = string.Empty;
            string patientID = string.Empty;
            int visitNO = 0;
             string [] tempArray =payOrderInfo_InParam.HOSP_SEQUENCE.Split('^');
            string date = HisDBLayer.Common.getServerTime().AddDays(-5).ToShortDateString();
            if(tempArray.Length==3)
            {
                patientID = tempArray[0];
                visitNO = Convert.ToInt32(tempArray[1]);
            }
            else
            {
                dicErr.Add("300303", "缴费订单已关闭");
                return null;
            }
            string currentRcptNO = outFeeManager.GetNewRcptNoByUserId(userID);

            if (string.IsNullOrEmpty(currentRcptNO))
            {
                currentRcptNO = userID + "000001";
            }
            string invoiceNew = outFeeManager.GetInvoiceNo(userID, HisCommon.Enum.InvoiceKind.门诊发票);

            var patientLst = QueryListPatientVisitInfo(patientID, date);

             if(patientLst==null|| patientLst.Count <= 0)
            {
                dicErr.Add("300301", "缴费订单不存在");
                return null;
            }
          
            OUTP_PATIENT_VISITINFO patientInfo = patientLst.Find(t =>t.patient.VISIT_NO==visitNO&&t.patient.PATIENT_ID==patientID);

            if (patientInfo == null)
            {
                dicErr.Add("300301", "缴费订单不存在");
                return null;
            }

            var  chargeInfo= ConvertChargeInfo(payOrderInfo_InParam, patientInfo, currentRcptNO, invoiceNew);

            // 增加校验信息
            decimal fin_total_fee = 0;
            decimal order_total_fee = 0;

            chargeInfo.listFinInvoiceInfo.ForEach(t => fin_total_fee += t.TOT_COST);
            chargeInfo.outpRcptMaster.ForEach(t => order_total_fee += t.TOTAL_COSTS);

            if (fin_total_fee != order_total_fee)
            {
                dicErr.Add("300304", "缴费金额不正确");
                return null;
            }
             bool isSuccess= healthCardManager.SavePayOrderListInfo(payOrderInfo_InParam, chargeInfo, ref dicErr);
            if (isSuccess)
            {
                outParam.HOSP_MEDICAL_NUM = invoiceNew;
                outParam.RECEIPT_ID = currentRcptNO;
                outParam.HOSP_MEDICAL_NUM = patientID;
            }
            return outParam;
        }

        public static HealthCardDepositLst_OutParam QueryDepositRecordInfo(HealthCardDepositLst_InParam depositInfo_InParam, ref Dictionary<string, string> dicErr)
        {

            HealthCardDepositLst_OutParam outParam = new HealthCardDepositLst_OutParam();
            //Dictionary<string, string> dicHealthCardInfo = new Dictionary<string, string>();

            //TokenBis.GetCardInfo(depositInfo_InParam.HEALTH_CARD_ID, "", ref dicHealthCardInfo);

            //outParam.PATIENT_NAME = dicHealthCardInfo["name"];
            //string idCardValue = dicHealthCardInfo["id_card_value"];
           var patientInfo=  outFeeManager.GetPatientInforByID(depositInfo_InParam.HOSP_PATIENT_ID);

            if(patientInfo==null)
            {
                dicErr.Add("300303", "缴费订单已关闭(当前患者信息获取失败)");
                return null;
            }
            outParam.PATIENT_NAME = patientInfo.NAME;

            DataTable dtDepositInfo = healthCardManager.QueryDepositPaymentInfo(depositInfo_InParam.BEGIN_DATE+" 00:00:00", depositInfo_InParam.END_DATE+" 23:59:59", patientInfo.ID_NO);

            if (dtDepositInfo == null || dtDepositInfo.Rows.Count <= 0)
            {
                dicErr.Add("300303", "缴费订单已关闭(当前患者搜索时间范围内无住院记录！)");
                return null;
            }
            else if (dtDepositInfo.Rows.Count > 1)
            {
                dicErr.Add("300303", "缴费订单已关闭(当前患者搜索时间范围内大于一条住院记录！)");
                return null;
            }

            outParam.BUNK_ID = dtDepositInfo.Rows[0]["bed_no"]==null? "":dtDepositInfo.Rows[0]["bed_no"].ToString();
            outParam.SUMMARY_ADVANCES = dtDepositInfo.Rows[0]["prepayments"] == null ? "" : dtDepositInfo.Rows[0]["prepayments"].ToString();
            outParam.DEPT_NAME = dtDepositInfo.Rows[0]["deptname"] == null ? "" : dtDepositInfo.Rows[0]["deptname"].ToString();
            outParam.TREAT_DATE = dtDepositInfo.Rows[0]["admission_date_time"] == null ? "" : Convert.ToDateTime( dtDepositInfo.Rows[0]["admission_date_time"].ToString()).ToString("yyyy-MM-dd");
            outParam.HOSP_PATIENT_NO = dtDepositInfo.Rows[0]["patient_id"] == null ? "" : dtDepositInfo.Rows[0]["patient_id"].ToString();
            return outParam;
        }

        public static HealthCardDepositPayInfo_OutParam SaveDepositPaymentInfo(HealthCardDepositPayInfo_InParam depositPayInfo_InParam, ref Dictionary<string, string> dicErr)
        {
            HealthCardDepositPayInfo_OutParam outParam = new HealthCardDepositPayInfo_OutParam();

            fin_invoiceinfo_record finRecord = ConvertImpPaymentInvoiceRecord(depositPayInfo_InParam);
            HisCommon.DataEntity.PREPAYMENT_RCPT prePaymentRcpt = ConvertPrePaymentInfo(depositPayInfo_InParam, finRecord.INVOICE_NO);

            bool isSuccess= healthCardManager.SaveDepositPaymentInfo(finRecord, prePaymentRcpt, depositPayInfo_InParam, ref dicErr);

            if (!isSuccess)
            {
                return null;
            }
            outParam.HOSP_ORDER_ID = finRecord.INVOICE_NO;
            outParam.RECEIPT_ID = prePaymentRcpt.RCPT_NO;

            return outParam;
        }

        public static HealthCardLisCheckReport_OutParam QueryCheckReportInfo(HealthCardLisCheckReport_InParam lisCheckReport_InParam, ref Dictionary<string, string> dicErr)
        {

            string beginDate = lisCheckReport_InParam.BEGIN_DATE;
            string endDate = lisCheckReport_InParam.END_DATE;

            var patInfo = healthCardManager.GetHealthPatInfo(lisCheckReport_InParam.HOSP_PATIENT_ID);

            HealthCardLisCheckReport_OutParam lisOutParam= healthCardManager.QueryCheckReportInfo(beginDate, endDate, patInfo, ref dicErr);
            HealthCardLisCheckReport_OutParam pacsOutParam = healthCardManager.QueryCheckPacsReportInfo(beginDate, endDate, patInfo, ref dicErr);
          
            if (lisOutParam == null && pacsOutParam == null)
            {
                return null;
            }
             if (lisOutParam == null && pacsOutParam != null)
            {
                pacsOutParam.HOS_ID = lisCheckReport_InParam.HOS_ID;
                pacsOutParam.HEALTH_CARD_ID = lisCheckReport_InParam.HEALTH_CARD_ID;
                dicErr.Clear();
                return pacsOutParam;
            }
             if (lisOutParam != null && pacsOutParam == null)
            {
                lisOutParam.HOS_ID = lisCheckReport_InParam.HOS_ID;
                lisOutParam.HEALTH_CARD_ID = lisCheckReport_InParam.HEALTH_CARD_ID;
                dicErr.Clear();
                return lisOutParam; 
            }
           else if (lisOutParam != null && pacsOutParam != null)
            {
                lisOutParam.REPORT_INFO.AddRange(pacsOutParam.REPORT_INFO);
                lisOutParam.HOS_ID = lisCheckReport_InParam.HOS_ID;
                lisOutParam.HEALTH_CARD_ID = lisCheckReport_InParam.HEALTH_CARD_ID;
                dicErr.Clear();
                return lisOutParam;
            }
            return null;
        }

        public static HealthCardNormalCheckReport_OutParam QueryNormalReportInfo(HealthCardNormalCheckReport_InParam normalCheckReport_InParam, ref Dictionary<string, string> dicErr)
        {

            string reportID = normalCheckReport_InParam.REPORT_ID;
            var patInfo = healthCardManager.GetHealthPatInfo(normalCheckReport_InParam.HOSP_PATIENT_ID);

            HealthCardNormalCheckReport_OutParam outParam = healthCardManager.QueryNormalReportInfo(reportID,   patInfo, ref dicErr);

            if (outParam != null)
            {
                outParam.HOS_ID = normalCheckReport_InParam.HOS_ID;
             
                return outParam;
            }
            else
            {
                return null;
            }
        }

        public static HealthCardDrugReportInfo_OutParam QueryDrugReportInfo(HealthCardDrugReportInfo_InParam drugCheckReport_InParam, ref Dictionary<string, string> dicErr)
        {

            string reportID = drugCheckReport_InParam.REPORT_ID;
            var patInfo = healthCardManager.GetHealthPatInfo(drugCheckReport_InParam.HOSP_PATIENT_ID);

            HealthCardDrugReportInfo_OutParam outParam = healthCardManager.QueryDrugReportInfo(reportID, patInfo, ref dicErr);

            if (outParam != null)
            {
                outParam.HOS_ID = drugCheckReport_InParam.HOS_ID;

                return outParam;
            }
            else
            {
                return null;
            }
        }

        public static HealthCardPacsCheckReport_OutParam QueryPacsReportInfo(HealthCardPacsCheckReport_InParam pacsCheckReport_InParam, ref Dictionary<string, string> dicErr)
        {

            string reportID = pacsCheckReport_InParam.REPORT_ID;
            var patInfo = healthCardManager.GetHealthPatInfo(pacsCheckReport_InParam.HOSP_PATIENT_ID);

            HealthCardPacsCheckReport_OutParam outParam = healthCardManager.QueryPacsReportInfo(reportID, patInfo, ref dicErr);

            if (outParam != null)
            {
                outParam.HOS_ID = pacsCheckReport_InParam.HOS_ID;
                outParam.PATIENT_NAME = patInfo.PATIENT_NAME;
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

                outParam.HEALTH_CARD_ID = patInfo.HEALTH_CARD_ID;
                outParam.PATIENT_SEX = patInfo.Sex;
                outParam.PATIENT_IDCARD_TYPE = patInfo.PATIENT_ID_TYPE;
                outParam.PATIENT_IDCARD_NO = patInfo.PATIENT_ID_CARD;
                outParam.MEDICAL_INSURANNCE_TYPE = "自费";
                return outParam;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 私有方法
        private static CLINIC_MASTER ConvertMaster(HealthCardOrderRegInfo_InParam regInfo_InParam, HEALTHCARD_PATIENT_INFO  patInfo)
        {
            CLINIC_MASTER _Master = new CLINIC_MASTER();

            // 患者ID自动获取
            _Master.VISIT_NO = Convert.ToInt32(HisDBLayer.Common.GetSequence(HisCommon.Enum.Sequences.VISIT_NO.ToString()));
            _Master.PATIENT_ID = patInfo.PATIENT_ID;
            _Master.ADMIS = "0";
            DateTime birthDay = DateTime.Now;
            if (!string.IsNullOrEmpty(patInfo.BirthDay) &&
                patInfo.BirthDay.Length == 8)
            {
                string strBirthDay = patInfo.BirthDay.Substring(0, 4) + "-" + patInfo.BirthDay.Substring(4, 2) + "-" + patInfo.BirthDay.Substring(6, 2);
                DateTime.TryParse(strBirthDay, out birthDay);
            }
            else
                DateTime.TryParse(patInfo.BirthDay, out birthDay);
 
            _Master.AGE = GetAge(birthDay);
            _Master.NAME = patInfo.PATIENT_NAME;
            _Master.SEX = ConvertHISSexCode(patInfo.Sex);
           // _Master.NAME_PHONETIC = regInfo_InParam.MOBILE;
            // 默认都不是复诊患者
            _Master.FIRST_VISIT_INDICATOR = 1;
            // todo  预约挂号的人员ID
            _Master.OPERATOR = userID;
            // 费用信息
            _Master.REGIST_FEE = Convert.ToDecimal(regInfo_InParam.REG_FEE) / 100;
            _Master.CLINIC_FEE = Convert.ToDecimal(regInfo_InParam.TREAT_FEE) / 100;
            _Master.OTHER_FEE = 0;
            _Master.CLINIC_CHARGE = _Master.REGIST_FEE + _Master.CLINIC_FEE + _Master.OTHER_FEE;
            _Master.CHARGE_TYPE_CODE = "1";
            // 如下取值最好通过排版获取
            var strArray = regInfo_InParam.REG_ID.Split('-');
            string doctorID = strArray[0];
            string strRegDate = strArray[1].Substring(0, 4) + "-" + strArray[1].Substring(4, 2) + "-" + strArray[1].Substring(6, 2);
            string timeDesc = strArray[1].Substring(strArray[1].Length-1, 1);

            Dictionary<string, string> dicSchedulingInfo = new Dictionary<string, string>();
            healthCardManager.GetSchedulingInfoByDoctor(doctorID, Convert.ToDateTime(strRegDate), timeDesc, ref dicSchedulingInfo);
            _Master.DOCTOR = doctorID;
            _Master.VISIT_DEPT = dicSchedulingInfo["clinic_dept"];
            _Master.VISIT_TIME_DESC = dicSchedulingInfo["time_desc"];
            _Master.CLINIC_LABEL = dicSchedulingInfo["clinic_label"];
            _Master.VISIT_DATE = Convert.ToDateTime(dicSchedulingInfo["clinic_date"]);
            // Other 赋值
            _Master.CLINIC_TYPE = dicSchedulingInfo["clinic_type"];
            _Master.CLINC_TYPE = "1";
            _Master.MR_PROVIDE_INDICATOR = 0;
            // 预约挂号 0
            _Master.REGISTRATION_STATUS = 0;
            _Master.REGISTERING_DATE = Convert.ToDateTime(regInfo_InParam.ORDER_TIME);
            _Master.SERIALNUMBER = regInfo_InParam.ORDER_ID;
            _Master.BILLNO = GetBillNo(userID);
            _Master.SYMPTOM = "0";

            DataTable registrationNum =  outFeeManager.GetRegistrationNum(HisDBLayer.BaseEntityer.Db, _Master.VISIT_DATE.ToShortDateString(), dicSchedulingInfo["clinic_label"], dicSchedulingInfo["time_desc"]);

            _Master.SERIAL_NO = registrationNum.Rows.Count > 0 ?
                        Convert.ToInt32(registrationNum.Rows[0]["current_no"].ToString()) : 1;
            _Master.IDENTITY = "居民";
            _Master.CHARGE_TYPE = "自费";
            return _Master;
        }

        private static PAT_MASTER_INDEX ConvertMasterIndex(HealthCardOrderRegInfo_InParam regInfo_InParam, HEALTHCARD_PATIENT_INFO patInfo)
        {
            PAT_MASTER_INDEX _MASTER_INDEX = new PAT_MASTER_INDEX();
            DateTime birthDay = DateTime.Now;
            if (!string.IsNullOrEmpty(patInfo.BirthDay) &&
                patInfo.BirthDay.Length == 8)
            {
                string strBirthDay = patInfo.BirthDay.Substring(0, 4) + "-" + patInfo.BirthDay.Substring(4, 2) + "-" + patInfo.BirthDay.Substring(6, 2);
                DateTime.TryParse(strBirthDay, out birthDay);
            }
            else
                DateTime.TryParse(patInfo.BirthDay, out birthDay);
            _MASTER_INDEX.DATE_OF_BIRTH = birthDay;
            _MASTER_INDEX.NAME = patInfo.PATIENT_NAME;
            _MASTER_INDEX.SEX = ConvertHISSexCode(patInfo.Sex);
            _MASTER_INDEX.NAME_PHONETIC = regInfo_InParam.MOBILE;
            _MASTER_INDEX.MAILING_ADDRESS = patInfo.Address;
            _MASTER_INDEX.OPERATOR = userID;
            _MASTER_INDEX.NATION = "";
            _MASTER_INDEX.ID_NO = patInfo.PATIENT_ID_CARD;
            _MASTER_INDEX.IDENTITY = "居民";
            _MASTER_INDEX.PHONE_NUMBER_HOME = patInfo.Mobile;
            _MASTER_INDEX.CHARGE_TYPE = "自费";
            // Other
            _MASTER_INDEX.VIP_INDICATOR = 0;
            _MASTER_INDEX.CREATE_DATE = HisDBLayer.Common.getServerTime();
            _MASTER_INDEX.PATIENT_ID = patInfo.PATIENT_ID;
       
            return _MASTER_INDEX;
        }

        private static string GetInvoiceNo(string operId, HisCommon.Enum.InvoiceKind invoiceType, ref string mess)
        {
            HisDBLayer.OutpFeeManager manager = new HisDBLayer.OutpFeeManager();
            string invoiceNo = string.Empty;
            try
            {
                invoiceNo = manager.GetInvoiceNo(operId, invoiceType);
                if (invoiceNo == "beyond")
                {
                    mess = "票号生产异常";
                    invoiceNo = string.Empty;
                }
                else if (string.IsNullOrEmpty(invoiceNo))
                {
                    mess = "请领取发票";
                }
                return invoiceNo;
            }
            catch (Exception e)
            {
                mess = e.Message;
                return invoiceNo;
            }
        }

        private static string GetBillNo(string  userID)
        {
            HisDBLayer.OutpFeeManager manager = new HisDBLayer.OutpFeeManager();
            DataTable dtbillno = manager.GetBillNo(userID);
            if (dtbillno.Rows.Count > 0)
            {
                if (dtbillno.Rows[0]["billno"].ToString() == string.Empty)
                {
                    return userID + "00000001";
                }
                else
                {
                    //发票号共12位：4位操作员号+8位格式化序列号
                    return userID + System.Convert.ToString(
                        Convert.ToInt32(dtbillno.Rows[0]["billno"].ToString().Substring(5)) + 1).PadLeft(8, '0');
                }
            }
            else
            {
                return userID + "00000001";
            }
        }

        private static   fin_invoiceinfo_record  ConvertFinInvoiceRecord(HealthCardPayRegInfo_InParam healthCardPayReg)
        {
            fin_invoiceinfo_record invoiceinfo_Record = new fin_invoiceinfo_record();
            HisDBLayer.OutpFeeManager manager = new HisDBLayer.OutpFeeManager();
            // 操作员编码
            string operCode = userID;
            string errMsg = string.Empty;
            invoiceinfo_Record.INVOICE_NO = GetInvoiceNo(operCode, HisCommon.Enum.InvoiceKind.挂号发票,ref errMsg);
            invoiceinfo_Record.TRANS_TYPE = "1";
            invoiceinfo_Record.TOT_COST = Convert.ToDecimal( healthCardPayReg.PAY_TOTAL_FEE)/100 ;
            //自费金额 医保时重新赋值
            invoiceinfo_Record.OWN_COST = Convert.ToDecimal(healthCardPayReg.PAY_COPE_FEE) / 100;
            invoiceinfo_Record.INVOICE_SEQ = "1";
            invoiceinfo_Record.FEE_OPER_CODE = operCode;
            invoiceinfo_Record.FEE_OPER_DATE = HisDBLayer.Common.getServerTime();
            invoiceinfo_Record.INVOICE_STATE = "0";
            invoiceinfo_Record.INVOICE_KIND = ((int)HisCommon.Enum.InvoiceKind.挂号发票).ToString().PadLeft(2, '0');
            invoiceinfo_Record.DAYBALANCED_FLAG = "0";
            invoiceinfo_Record.VALID_FLAG = "1";
            invoiceinfo_Record.PACT_CODE = "1";
            invoiceinfo_Record.PACT_NAME = "自费";
            return invoiceinfo_Record;
        }

        private static fin_invoiceinfo_record ConvertRefundFinInvoiceRecord(HealthCardRefundRegInfo_InParam healthCardRefundReg)
        {
            fin_invoiceinfo_record invoiceinfo_Record = new fin_invoiceinfo_record();
            // 操作员编码
            string operCode = userID;
            string errMsg = string.Empty;
            invoiceinfo_Record.INVOICE_NO = healthCardRefundReg.HOSP_ORDER_ID; ;
            invoiceinfo_Record.TRANS_TYPE = "2";
            invoiceinfo_Record.TOT_COST = -Convert.ToDecimal(healthCardRefundReg.TOTAL_FEE) / 100;
            //自费金额 医保时重新赋值
            invoiceinfo_Record.OWN_COST = -Convert.ToDecimal(healthCardRefundReg.REFUND_FEE) / 100;
            invoiceinfo_Record.INVOICE_SEQ = "1";
            invoiceinfo_Record.FEE_OPER_CODE = operCode;
            invoiceinfo_Record.FEE_OPER_DATE = HisDBLayer.Common.getServerTime();
            invoiceinfo_Record.INVOICE_STATE = "0";
            invoiceinfo_Record.INVOICE_KIND = ((int)HisCommon.Enum.InvoiceKind.挂号发票).ToString().PadLeft(2, '0');
            invoiceinfo_Record.DAYBALANCED_FLAG = "0";
            invoiceinfo_Record.VALID_FLAG = "1";
            invoiceinfo_Record.PACT_CODE = "1";
            invoiceinfo_Record.PACT_NAME = "自费";

            invoiceinfo_Record.BACKFEE_INVOICE_NO= healthCardRefundReg.HOSP_ORDER_ID;
            invoiceinfo_Record.BACKFEE_INVOICE_SEQ = "1";
            invoiceinfo_Record.BACKFEE_OPER_CODE = userID;
            invoiceinfo_Record.BACKFEE_OPER_DATE = HisDBLayer.Common.getServerTime();


            return invoiceinfo_Record;
        }

        private static  fin_invoiceinfo_record  ConvertImpPaymentInvoiceRecord(HealthCardDepositPayInfo_InParam depositPayInfo)
        {
            fin_invoiceinfo_record invoiceinfo_Record = new fin_invoiceinfo_record();
            // 操作员编码
            string operCode = userID;
            string errMsg = string.Empty;
            invoiceinfo_Record.INVOICE_NO = GetInvoiceNo(operCode, HisCommon.Enum.InvoiceKind.住院预交金,ref  errMsg);
            invoiceinfo_Record.TRANS_TYPE = "1";
            invoiceinfo_Record.TOT_COST = Convert.ToDecimal(depositPayInfo.ADVANCE_PAYMENT) / 100;
            //自费金额 医保时重新赋值
            invoiceinfo_Record.OWN_COST = Convert.ToDecimal(depositPayInfo.ADVANCE_PAYMENT) / 100;
            invoiceinfo_Record.INVOICE_SEQ = "1";
            invoiceinfo_Record.FEE_OPER_CODE = operCode;
            invoiceinfo_Record.FEE_OPER_DATE = HisDBLayer.Common.getServerTime();
            invoiceinfo_Record.INVOICE_STATE = "0";
            invoiceinfo_Record.INVOICE_KIND = ((int)HisCommon.Enum.InvoiceKind.住院预交金).ToString().PadLeft(2, '0');
            invoiceinfo_Record.DAYBALANCED_FLAG = "0";
            invoiceinfo_Record.VALID_FLAG = "1";
            invoiceinfo_Record.PACT_CODE = "1";
            invoiceinfo_Record.PACT_NAME = "自费";
            invoiceinfo_Record.ECO_COST = 0;
            invoiceinfo_Record.REB_COST = 0;

            return invoiceinfo_Record;
        }

        private static HisCommon.DataEntity.PREPAYMENT_RCPT  ConvertPrePaymentInfo(HealthCardDepositPayInfo_InParam depositPayInfo,string invoiceNo)
        {
            HisCommon.DataEntity.PREPAYMENT_RCPT prePayment = new HisCommon.DataEntity.PREPAYMENT_RCPT();
            HisDBLayer.InpatientRegister gap = new HisDBLayer.InpatientRegister();

            string errMsg = string.Empty;
            string maxvalue = gap.GetPrepayOperatorMaxInvoice(userID, ref errMsg).PadLeft(8, '0');

            prePayment.AMOUNT= Convert.ToDecimal(depositPayInfo.ADVANCE_PAYMENT) / 100;
            prePayment.BANK = "5";
            prePayment.CHECK_NO = "0";
            prePayment.OPERATOR_NO = userID;
            prePayment.PATIENT_ID = ""; // to do  通过健康卡号获取patientId号
            prePayment.PAY_WAY = "4";// 银行卡
         
            prePayment.RCPT_NO = userID + maxvalue;
            prePayment.TRANSACT_DATE= HisDBLayer.Common.getServerTime();
            prePayment.TRANSACT_TYPE= ((int)HisCommon.Enum.PrepayType.交款).ToString();
            prePayment.VISIT_ID = 0;// 就诊序号
            prePayment.ACC_FLAG = "0";
            prePayment.IS_FLAG = 1;
            prePayment.INVOICE = invoiceNo;

            prePayment.MEMO = "";
            prePayment.PREPAY_OPER = depositPayInfo.OPERATOR_ID;
         
            return prePayment;
        }
        private static CLINIC_MASTER ConvertClinicMasterByFinInvoice( HealthCardPayRegInfo_InParam payRegInfo_InParam)
        {
            CLINIC_MASTER clinic_master = new CLINIC_MASTER();
            clinic_master.BILLNO = payRegInfo_InParam.ORDER_ID;
            return clinic_master;
        }

        private static string ConvertHISSexCode(string genderCode)
        {
            if (genderCode == "1")
                return "男";
            else if (genderCode == "2")
                return "女";
            else
                return "未知";
        }

        private static int GetAge(DateTime Birthdate)
        {
            DateTime curTime = HisDBLayer.Common.getServerTime();
            int age = curTime.Year - Birthdate.Year;
            if (curTime.Year < Birthdate.AddYears(age).Year)
                age--;
            return age;
        }

        private  static  List<HisCommon.DataEntity.OUTP_PATIENT_VISITINFO> QueryListPatientVisitInfo(string visitNO, string date)
        {
            //得到挂号信息
            HisDBLayer.PatientManager pm = new HisDBLayer.PatientManager();
            var listp = pm.GetPatientByNO(visitNO, date).ToList();
            //如果查询不到患者，返回空值
            if (listp == null || listp.Count == 0)
            {
                return null;
            }
            List<HisCommon.DataEntity.OUTP_PATIENT_VISITINFO> listPatientVisitInfo = new List<OUTP_PATIENT_VISITINFO>();
            foreach (var p in listp)
            {
                visitNO = p.VISIT_NO.ToString();
                date = p.VISIT_DATE.ToString();
                List<HisCommon.DataEntity.OUTP_ORDERS_DETAIL> detail = outFeeManager.QueryPatientOrderDetail(visitNO, date);

                HisCommon.DataEntity.OUTP_PATIENT_VISITINFO visitInfo = new HisCommon.DataEntity.OUTP_PATIENT_VISITINFO();
                visitInfo.patient = p;
                visitInfo.listOutpOrdersDetail = detail;
                listPatientVisitInfo.Add(visitInfo);

            }
            return listPatientVisitInfo;
        }

        private  static OUTP_CHARGEINFO ConvertChargeInfo(HealthCardPayOrderInfo_InParam payOrder_InParam, OUTP_PATIENT_VISITINFO patientInfo, string rcpt_no,string invoiceNO)
        {
            OUTP_PAYMENTS_MONEY outPaymentsMoney = new OUTP_PAYMENTS_MONEY();
            OUTP_CHARGEINFO chargeInfo = new OUTP_CHARGEINFO();

            outPaymentsMoney.PAYMENT_NO = 20;
            outPaymentsMoney.MONEY_TYPE = "线上支付";
            outPaymentsMoney.PAYMENT_AMOUNT = Convert.ToDecimal(payOrder_InParam.PAY_BEHOOVE_FEE) / 100;
            outPaymentsMoney.REFUNDED_AMOUNT = 0;
            outPaymentsMoney.RCPT_NO = rcpt_no;
            outPaymentsMoney.INVOICE_NEW = invoiceNO;

            chargeInfo.listOutpPaymentsMoney = new List<OUTP_PAYMENTS_MONEY>();
            chargeInfo.listOutpPaymentsMoney.Add(outPaymentsMoney);

            chargeInfo.patient = patientInfo.patient;
            chargeInfo.listOutpOrdersDetail = patientInfo.listOutpOrdersDetail;
            chargeInfo.outpRcptMaster.Add(GetOutpRectMaster(patientInfo, rcpt_no, invoiceNO));
            chargeInfo.outpOrderDesc.Add(GetOutpOrderDesc(patientInfo, rcpt_no));
            chargeInfo.listOutpBillItems.AddRange(GetOutpBillItems(patientInfo, rcpt_no, invoiceNO));
            chargeInfo.listFinInvoiceInfo.Add(GetFinInvoiceInfo(patientInfo, payOrder_InParam, invoiceNO, rcpt_no));

            return chargeInfo;
        }

        private static  OUTP_RCPT_MASTER GetOutpRectMaster(OUTP_PATIENT_VISITINFO patientVisitInfo,string rcptNO,string invoiceNO)
        {
            OUTP_RCPT_MASTER o = new OUTP_RCPT_MASTER();
            CLINIC_MASTER patient = patientVisitInfo.patient;
            List<OUTP_ORDERS_DETAIL> listOrderDetail = patientVisitInfo.listOutpOrdersDetail;
            o.RCPT_NO = rcptNO;
            o.INVOICE_NEW = invoiceNO;//票据管理的发票号
            o.PATIENT_ID = patient.PATIENT_ID;
            o.NAME = patient.NAME;
            o.NAME_PHONETIC = patient.NAME_PHONETIC;
            o.IDENTITY = patient.CHARGE_TYPE;
            o.CHARGE_TYPE = patient.CHARGE_TYPE;
            o.UNIT_IN_CONTRACT = patient.UNIT_IN_CONTRACT;
            //收费时间
            o.VISIT_DATE = HisDBLayer.Common.getServerTime();
            o.TOTAL_COSTS = decimal.Parse(listOrderDetail.Sum(x => Math.Round(x.NOWCOST, 2)).ToString());
            //此处有疑问，总是和总额相等？
            //2013-05-15修改打折，此处暂时存储打折费用
            o.TOTAL_CHARGES = o.TOTAL_COSTS;
            o.OPERATOR_NO = userID;
            //已收费
            o.CHARGE_INDICATOR = 0;
            o.REFUNDED_RCPT_NO = null;
            o.ACCT_NO = null;
            o.INSURANCE_TYPE = patient.INSURANCE_TYPE;
            o.REG_NO = patient.VISIT_NO;
            o.REG_DATE = patient.VISIT_DATE;
            //疑问，这个怎么赋值?
            o.TB_FLAG = null;
            return o;
        }

        private static OUTP_ORDER_DESC GetOutpOrderDesc(OUTP_PATIENT_VISITINFO patientVisitInfo,string rcptNO)
        {
            OUTP_ORDER_DESC o = new OUTP_ORDER_DESC();
            CLINIC_MASTER patient = patientVisitInfo.patient;
            List<OUTP_ORDERS_DETAIL> listOrderDetail = patientVisitInfo.listOutpOrdersDetail;
            //这个也是收费时间别混了...囧
            o.VISIT_DATE = HisDBLayer.Common.getServerTime();
            o.VISIT_NO = patient.VISIT_NO;
            o.PATIENT_ID = patient.PATIENT_ID;

            // 0-该单不含药品，1-该单含西药处方，2-该单含中药处方
            o.PRESC_INDICATOR = 0;
            if (listOrderDetail.Find(x => x.NOWCLASS == HisCommon.Enum.ItemType.A.ToString()) != null)
                o.PRESC_INDICATOR = 1;
            if (listOrderDetail.Find(x => x.NOWCLASS == HisCommon.Enum.ItemType.B.ToString()) != null)
                o.PRESC_INDICATOR = 2;
            //收据号
            o.RCPT_NO = rcptNO;
            o.ORDERED_BY_DOCTOR = listOrderDetail[0].ORDER_DOCTOR;
            o.ORDERED_BY_DEPT = listOrderDetail[0].ORDERED_BY;
            return o;
        }

        private static List<OUTP_BILL_ITEMS> GetOutpBillItems(OUTP_PATIENT_VISITINFO patientVisitInfo, string rcptNO, string invoiceNO)
        {
            List<OUTP_BILL_ITEMS> os = new List<OUTP_BILL_ITEMS>();
            CLINIC_MASTER patient = patientVisitInfo.patient;

            List<OUTP_ORDERS_DETAIL> listOrderDetail = patientVisitInfo.listOutpOrdersDetail;
            int itemNo = 0;
            foreach (var i in listOrderDetail)
            {
                OUTP_BILL_ITEMS o = new OUTP_BILL_ITEMS();
                //这是收费时间，kd啊囧
                o.VISIT_DATE = HisDBLayer.Common.getServerTime();
                o.VISIT_NO = patient.VISIT_NO;
                o.RCPT_NO = rcptNO;
                o.INVOICE_NEW = invoiceNO;//发票管理发票号
                o.ITEM_NO = ++itemNo;
                o.ITEM_CLASS = i.NOWCLASS;
                o.CLASS_ON_RCPT = i.CLASS_ON_OUTP_RCPT;
                o.SUBJ_CODE = i.SUBJ_CODE;
                o.CLASS_ON_RECKONING = i.CLASS_ON_RECKONING;
                o.ITEM_CODE = i.NOWCODE;
                o.CLINIC_ITEM_CLASS = i.CLASS;//医嘱类别
                o.CLINIC_ITEM_CODE = i.CODE;//诊疗项目编码
                o.ITEM_NAME = i.ITEM_NAME;
                o.ITEM_SPEC = i.ITEM_SPEC;
                o.AMOUNT = i.NOWAMOUNT;
                o.UNITS = i.UNITS;
                o.PERFORMED_BY = i.DEPTCODE;
                o.COSTS = i.NOWCOST;
                o.Memo = i.Remark;
                //应收=总额？
                //2013-05-15处理打折

                o.CHARGES = i.NOWCOST * i.NOWRATE;

                o.SERIAL_NO = i.SERIAL_NO;
                o.BATCHNO = i.BATCHNO;
                //没明白什么东西
                o.D_ITEM_NO = i.ITEM_NO;
                o.APPOINT_NO = i.APPOINT_NO;
                o.ORDER_DEPT = i.ORDERED_BY;
                o.ORDER_DOCTOR = i.ORDER_DOCTOR;
                o.CHECKFLAG = "0";
                o.SYSBJ = null;
                //o.QR_TIME
                o.QR_OPPER = null;
                o.SUBJ_CODE = i.SUBJ_CODE;
                o.COMMON_FLAG = i.COMMON_FLAG;
                o.SPECIAL_FLAG = i.SPECIAL_FLAG;
                o.PRICE = i.NOWPRICE;

                o.CLINIC_ITEM_NAME = i.NAME;//诊疗项目名称
                o.CLINIC_ITEM_AMOUNT = i.AMOUNT;//诊疗项目数量

                if (o.ITEM_CLASS == "B") //草药
                {
                    o.CLINIC_ITEM_PRICE = i.NOWCOST;
                }
                else
                {
                    o.CLINIC_ITEM_PRICE = i.PRICE;//诊疗项目价总价格
                }
                os.Add(o);
            }
            return os;
        }

        public static fin_invoiceinfo_record GetFinInvoiceInfo(OUTP_PATIENT_VISITINFO patientVisitInfo, HealthCardPayOrderInfo_InParam payOrderInfo,string invoiceNO,string rcptNO)
        {
            fin_invoiceinfo_record o = new fin_invoiceinfo_record();
            CLINIC_MASTER patient = patientVisitInfo.patient;
            List<OUTP_ORDERS_DETAIL> listOrderDetail = patientVisitInfo.listOutpOrdersDetail;

            o.INVOICE_NO = invoiceNO;
            o.TRANS_TYPE = "1";
            o.TOT_COST = decimal.Parse(listOrderDetail.Sum(x => x.NOWCOST).ToString());
            o.PUB_COST = decimal.Parse(payOrderInfo.PAY_MI_FEE) / 100;
            o.PAY_COST = decimal.Parse(payOrderInfo.PAY_BEHOOVE_FEE) / 100;
            if (patient.CHARGE_TYPE_CODE == "1" || patient.CHARGE_TYPE_CODE == "34")
                o.OWN_COST = o.TOT_COST;
            else
                o.OWN_COST = decimal.Parse(payOrderInfo.PAY_ACCOUNT) / 100;
            o.INVOICE_SEQ = "1";

            o.FEE_OPER_CODE = userID;
            o.FEE_OPER_DATE = HisDBLayer.Common.getServerTime();
            o.INVOICE_STATE = "0";
          
            o.INVOICE_KIND = "01";
            o.DAYBALANCED_FLAG = "0";
            //o.	DAYBALANCED_OPER_CODE	=
            //o.	DAYBALANCED_NO	=
            o.VALID_FLAG = "1";
            o.PACT_CODE = patient.CHARGE_TYPE_CODE;
            o.PACT_NAME = patient.CHARGE_TYPE;
            //o.	REPRINT_INVOICE_NO	=
            //o.	REPRINT_INVOICE_SEQ	=
            o.RCPT_NO = rcptNO;
            return o;
        }
        //public static decimal GetChargeAfterSale(string chargeTypeCode, List<OUTP_ORDERS_DETAIL> listOrderDetail)
        //{
        //    return listOrderDetail.Sum(x => Math.Round(x.NOWCOST, 2) * (GetSaleChargeRate(chargeTypeCode, x.NOWCLASS) == 1 ? 1 : Math.Round(x.NOWCOST, 2)));
        //}
        //private  static   decimal GetSaleChargeRate(string chargeTypeCode, string itemClass)
        // {
        //     try
        //     {
        //         listChargeTemplet = null;
        //         if (listChargeTemplet == null)
        //         {
        //             MyServer.ServiceOutpFeeManager.OutpFeeManagerClient poxy = new MyServer.ServiceOutpFeeManager.OutpFeeManagerClient();
        //             listChargeTemplet = poxy.GetChargeTempletList();
        //             poxy.Close();
        //         }
        //         //打折信息为空，不打折
        //         if (listChargeTemplet == null || listChargeTemplet.Count == 0)
        //         {
        //             return 1;
        //         }
        //         //存在符合条件的打折信息
        //         if (listChargeTemplet.Exists(x => x.CHARGE_TYPE_CODE == chargeTypeCode
        //                                     && x.charge_templet_detail.Exists(y => y.FEE_TYPE == itemClass))
        //             )
        //         { return 0; }
        //         //不存在符合条件的打折信息
        //         return 1;
        //     }
        //     catch (Exception ex)
        //     {
        //         DevComponents.DotNetBar.MessageBoxEx.Show("获取打折信息失败：" + ex.Message + "，按原价收费");
        //         return 1;
        //     }
        // }
        #endregion

    }
}
