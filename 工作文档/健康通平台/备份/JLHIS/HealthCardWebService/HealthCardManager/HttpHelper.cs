using HealthCardUtil;
using HealthCardUtil.Security;
using HealthCardUtil.Tool;
using HisCommon.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCardManager
{
    /// <summary>
    /// http帮助类
    /// </summary>
    public static class HttpHelper
    {

        private static string userID = "ln_12320wx";

        private static string key = "2098D32C4D1399EC";

        //private static string HosID = "41275532X";
        private static string HosID = "522633020000001";
        public static string HealthCardService(string reqXml)
        {
            /*
             请求xml示例：
             <?xml version="1.0" encoding="UTF-8"?>
             <ROOT>
               <FUN_CODE><![CDATA[]]></FUN_CODE>
               <USER_ID><![CDATA[]]></USER_ID>
               <SIGN_TYPE><![CDATA[MD5]]></SIGN_TYPE>
               <SIGN><![CDATA[]]></SIGN>
               <REQ_ENCRYPTED><![CDATA[]]></REQ_ENCRYPTED>
             </ROOT>
          */
            int res_code = 0;
            string res_msg = string.Empty;
            string res_sign = string.Empty;
            string res_encrypted = string.Empty;

            string res_xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            res_xml += @"<ROOT>
                           <RETURN_CODE><![CDATA[{0}]]></RETURN_CODE>
                           <RETURN_MSG><![CDATA[{1}]]></RETURN_MSG>
                           <SIGN_TYPE><![CDATA[{2}]]></SIGN_TYPE>
                           <SIGN><![CDATA[{3}]]></SIGN>
                           <RES_ENCRYPTED><![CDATA[{4}]]></RES_ENCRYPTED>
                         </ROOT>";

            try
            {
                string req_fun_code = string.Empty;
                string req_user_id = string.Empty;
                string req_sign = string.Empty;
                string req_sign_type = string.Empty;
                string req_encrypted = string.Empty;

                XmlHelper.AnalysisXmlReqBaseInfo(reqXml, ref req_fun_code, ref req_user_id, ref req_sign, ref req_sign_type, ref req_encrypted);

                if (req_user_id != userID)
                {
                    res_code = 1;
                    res_msg = "用户名不正确";
                }
                else
                {
                    //对请求串进行md5验签
                    string sign = Common.GetRequsetSign(req_fun_code, req_user_id, req_encrypted, key);
                    if (req_sign != sign || req_sign_type != "MD5")
                    {
                        res_code = 2;
                        res_msg = "签名不正确";
                    }
                    else
                    {
                        M_响应接口操作(req_fun_code, req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);

                        #region 不要了

                        //switch (req_fun_code)
                        //{
                        //    // 网络通讯测试
                        //    case "1001":
                        //        NetTestFunc(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        //M_网络通讯测试(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);

                        //        break;
                        //    // 用户信息注册
                        //    case "1002":
                        //        CreatePatInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        //M_用户信息注册(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);

                        //        break;

                        //    // 用户信息查询
                        //    case "1003":
                        //        //M_用户信息查询(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);

                        //        break;
                        //    //医院信息查询接口
                        //    case "1004":
                        //        GetHosInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //       // M_用户信息查询(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);

                        //        break;
                        //    // 用户卡验证
                        //    case "1005":
                        //        //M_用户卡验证(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 科室信息查询
                        //    case "2001":
                        //        GetDeptInfo(req_fun_code,req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //      //  M_科室信息查询(req_fun_code, req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 医生查询
                        //    case "2002":
                        //        GetDoctorInfo(req_fun_code, req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //       // M_医生信息查询(req_fun_code, req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 排版信息查询
                        //    case "2003":
                        //        GetRegInfo(req_fun_code,req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        //M_排版信息查询(req_fun_code, req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 排班分时查询
                        //    case "2004":
                        //        GetTimeRegInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        //M_排版分时查询(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 号源锁定
                        //    case "2005":
                        //        //M_号源锁定(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 解除号源锁定
                        //    case "2006":
                        //        //M_解除号源锁定(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 预约挂号接口
                        //    case "2007":
                        //        PostOrderRegInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        //M_预挂号(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 挂号支付接口
                        //    case "2008":
                        //        PostPayRegInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        //M_挂号支付(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 取消挂号接口
                        //    case "2009":
                        //        PostCancelRegInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //       // M_取消挂号(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 退款挂号接口
                        //    case "2010":
                        //        PostRefundRegInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //       // M_退款挂号(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 取号接口
                        //    case "2011":
                        //        GetRegNumInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //       // M_取号(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 挂号记录查询接口
                        //    case "2012":
                        //        QueryRegRecordInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //       // M_挂号记录查询(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 挂号记录查询接口
                        //    case "2020":
                        //        QueryRegRecordInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        // M_挂号记录查询(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 缴费记录查询接口
                        //    case "3001":
                        //        GetPayListInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    //缴费明细查询接口
                        //    case "3002":
                        //        GetPayDetailInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 待缴费记录支付接口
                        //    case "3003":
                        //        GetPayOrderInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    //缴费订单查询接口
                        //    case "3004": break;
                        //    // 排队列表查询接口
                        //    case "4001": break;
                        //    // 检查/检验列表查询
                        //    case "8001":
                        //        GetLisExamReportInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 检验报告查询(普通检验）
                        //    case "8002":
                        //        GetNormalLisReportInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    //检验报告查询（药敏检验）
                        //    case "8003":
                        //        GetDrugLisReportInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 检查报告查询
                        //    case "8004":
                        //        GetPacsReportInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 押金列表查询
                        //    case "9101":
                        //        GetDepositRecordInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 住院押金缴费
                        //    case "9102":
                        //        PostDepositPaymentInfo(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //    // 住院医嘱信息列表
                        //    case "9201": break;
                        //    // 患者医嘱明细
                        //    case "9202": break;

                        //    default:
                        //        NetTestFunc(req_encrypted, ref res_code, ref res_msg, ref res_encrypted, ref res_sign);
                        //        break;
                        //}
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                res_code = -1;
                res_msg = string.Format("异常：{0}", ex.Message);
            }
            if (res_code != 0)
            {
                res_encrypted = AESHelper.EncryptForAES("", key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }

            return string.Format(res_xml, res_code, res_msg, "MD5", res_sign, res_encrypted);

        }

        private static void M_响应接口操作(string req_fun_code, string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = "";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);
                res_bxml = HisHelper.M_获取响应参数(req_fun_code, req_bxml, ref res_code, ref res_msg);

                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
            catch (Exception ex)
            {
                res_code = 9999;
                res_msg = req_fun_code+"交易失败" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }




        #region 不要了


        /// <summary>
        /// 网络测试
        /// </summary>
        /// <param name="req_encrypted"></param>
        /// <param name="res_code"></param>
        /// <param name="res_msg"></param>
        /// <param name="res_encrypted"></param>
        /// <param name="res_sign"></param>
        private static void NetTestFunc(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            //获取请求业务参数
            string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

            string ip = string.Empty;
            XmlHelper.AnalysisXmlReqIPInfo(req_bxml, ref ip);

            //设置返回业务参数
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //设置返回串
            string res_bxml = @"<RES>
                                              <SYSDATE>{0}</SYSDATE>
                                            </RES>";
            res_bxml = string.Format(res_bxml, now);
            res_code = 0;
            res_msg = "交易成功";
            res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
            res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
        }

        /// <summary>
        /// 创建患者
        /// </summary>
        /// <param name="req_encrypted"></param>
        /// <param name="res_code"></param>
        /// <param name="res_msg"></param>
        /// <param name="res_encrypted"></param>
        /// <param name="res_sign"></param>
        private static void CreatePatInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            //设置返回串
            string res_bxml = @"<RES>
                                              <HOSP_PATIENT_ID>{0}</HOSP_PATIENT_ID>
                                            </RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                //string hosID = string.Empty;
                //string address = string.Empty;
                //string mobile = string.Empty;
                //string healthCardID = string.Empty;
                //string healthIDType = string.Empty;
                //string parentIDType = string.Empty;
                //string parentIDCard = string.Empty;
                //string parentIDName = string.Empty;
                HEALTHCARD_PATIENT_INFO patInfo = new HEALTHCARD_PATIENT_INFO();
                XmlHelper.AnalysisXmlReqPatInfo(req_bxml, ref patInfo);

                //设置返回业务参数
                string patientID = HisHelper.CreatePatInfo(patInfo);

                res_bxml = string.Format(res_bxml, patientID);
                res_code = 0;
                res_msg = "交易成功";
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
            catch (Exception ex)
            {
                res_code = 100201;
                res_msg = "用户注册信息失败" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        /// <summary>
        /// 获取医院信息
        /// </summary>
        /// <param name="req_encrypted"></param>
        /// <param name="res_code"></param>
        /// <param name="res_msg"></param>
        /// <param name="res_encrypted"></param>
        /// <param name="res_sign"></param>
        private static void GetHosInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES> <HOS_ID>{0}</HOS_ID>
	<NAME>{1}</NAME>
	<SHORT_NAME>{2}</SHORT_NAME>
	<ADDRESS>{3}</ADDRESS>
	<TEL>{4}</TEL>
	<WEBSITE>{5}</WEBSITE>
	<WEIBO>{6}</WEIBO>
	<LEVEL>{7}</LEVEL>
	<DESC>{8}</DESC>
	<SPECIAL>{9}</SPECIAL>
	<LONGITUDE>{10}</LONGITUDE>
	<LATITUDE>{11}</LATITUDE>
	<MAX_REG_DAYS>{12}</MAX_REG_DAYS>
	<START_REG_TIME>{13}</START_REG_TIME>
	<END_REG_TIME>{14}</END_REG_TIME>
	<STOP_BOOK_TIMEA>{15}</STOP_BOOK_TIMEA>
	<STOP_BOOK_TIMEP>{16}</STOP_BOOK_TIMEP> </RES>"
;
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                //string hosID = string.Empty;
                //string address = string.Empty;
                //string mobile = string.Empty;
                //string healthCardID = string.Empty;
                //string healthIDType = string.Empty;
                //string parentIDType = string.Empty;
                //string parentIDCard = string.Empty;
                //string parentIDName = string.Empty;
                Healthcard_HosInfo hosInfo = new Healthcard_HosInfo();
                XmlHelper.AnalysisXmlReqHosInfo(req_bxml, ref hosInfo);

                //设置返回业务参数
                hosInfo = HisHelper.GetHosInfo(hosInfo.HOS_ID);
                res_bxml = string.Format(res_bxml, hosInfo.HOS_ID, hosInfo.NAME, hosInfo.SHORT_NAME, hosInfo.ADDRESS, hosInfo.TEL, hosInfo.WEBSITE, hosInfo.WEIBO, hosInfo.LEVEL, hosInfo.DESC, hosInfo.SPECIAL, hosInfo.LONGITUDE, hosInfo.LATITUDE, hosInfo.MAX_REG_DAYS, hosInfo.START_REG_TIME, hosInfo.END_REG_TIME, hosInfo.STOP_BOOK_TIMEA, hosInfo.STOP_BOOK_TIMEP);
                res_code = 0;
                res_msg = "交易成功";
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
            catch (Exception ex)
            {
                res_code = 100401;
                res_msg = "获取医院信息失败" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }


        private static void GetDeptInfo(string req_fun_code, string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = "";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);
                //res_bxml = HisHelper.M_获取响应参数(req_fun_code, req_bxml);

                if (!string.IsNullOrEmpty(res_bxml))
                {
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = 200101;
                    res_msg = "科室不存在，未查询到科室记录";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 200101;
                res_msg = "获取医院科室信息失败" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }
        #region 不要了

        //private static void GetDoctorInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        //{
        //    string res_bxml = @"<RES><HOS_ID>" + HosID + "</HOS_ID></RES>";
        //    try
        //    {
        //        //获取请求业务参数
        //        string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

        //        HealthCardDoctorInfo_InParam doctorInfo_InParam = new HealthCardDoctorInfo_InParam();
        //        XmlHelper.AnalysisXmlReqDoctorInfo(req_bxml, ref doctorInfo_InParam);

        //        var healthCardDoctorInfo_Out = HisHelper.GetDoctorInfo(doctorInfo_InParam.DEPT_ID, doctorInfo_InParam.DOCTOR_ID);

        //        if (healthCardDoctorInfo_Out.Doctor_LIST.Count > 0)
        //        {
        //            res_bxml = XmlHelper.AnalysisXmlResDoctorInfo(healthCardDoctorInfo_Out);
        //            res_code = 0;
        //            res_msg = "交易成功";
        //            res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
        //            res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
        //        }
        //        else
        //        {
        //            res_code = 200202;
        //            res_msg = "医生不存在，未查询到医生记录";
        //            res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
        //            res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res_code = 200202;
        //        res_msg = "获取医院医生信息失败" + ex.ToString();
        //        res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
        //        res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
        //    }
        //}
        #endregion
        private static void GetDoctorInfo(string req_fun_code, string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = "";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);
                res_bxml = HisHelper.GetDoctorInfo(req_fun_code, req_bxml);
                if (!string.IsNullOrEmpty(res_bxml))
                {
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = 200202;
                    res_msg = "医生不存在，未查询到医生记录";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 200202;
                res_msg = "获取医院医生信息失败" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }
        #region 不要了

        //private static void GetRegInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        //{
        //    string res_bxml = @"<RES><HOS_ID>" + HosID + "</HOS_ID><DEPT_ID>{0}</DEPT_ID></RES>";
        //    try
        //    {
        //        //获取请求业务参数
        //        string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

        //        HealthCardRegInfo_InParam regInfo_InParam = new HealthCardRegInfo_InParam();
        //        XmlHelper.AnalysisXmlReqRegInfo(req_bxml, ref regInfo_InParam);

        //        var healthCardRegInfo_Out = HisHelper.GetRegInfo(regInfo_InParam.DEPT_ID, regInfo_InParam.DOCTOR_ID, regInfo_InParam.START_DATE, regInfo_InParam.END_DATE);

        //        if (healthCardRegInfo_Out.DoctorLst.Count > 0)
        //        {
        //            res_bxml = XmlHelper.AnalysisXmlResRegInfo(healthCardRegInfo_Out);
        //            res_code = 0;
        //            res_msg = "交易成功";
        //            res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
        //            res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
        //        }
        //        else
        //        {
        //            res_code = 200303;
        //            res_msg = "排班不存在，未查询到排班信息";
        //            res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
        //            res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res_code = 200303;
        //        res_msg = "排班不存在，未查询到排班信息" + ex.ToString();
        //        res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
        //        res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
        //    }
        //}
        #endregion
        private static void GetRegInfo(string req_fun_code, string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES><HOS_ID>" + HosID + "</HOS_ID><DEPT_ID>{0}</DEPT_ID></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardRegInfo_InParam regInfo_InParam = new HealthCardRegInfo_InParam();
                XmlHelper.AnalysisXmlReqRegInfo(req_bxml, ref regInfo_InParam);

                var healthCardRegInfo_Out = HisHelper.GetRegInfo(regInfo_InParam.DEPT_ID, regInfo_InParam.DOCTOR_ID, regInfo_InParam.START_DATE, regInfo_InParam.END_DATE);

                if (healthCardRegInfo_Out.DoctorLst.Count > 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResRegInfo(healthCardRegInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = 200303;
                    res_msg = "排班不存在，未查询到排班信息";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 200303;
                res_msg = "排班不存在，未查询到排班信息" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void GetTimeRegInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardTimeRegInfo_InParam regInfo_InParam = new HealthCardTimeRegInfo_InParam();
                XmlHelper.AnalysisXmlReqTimeRegInfo(req_bxml, ref regInfo_InParam);

                var healthCardTimeRegInfo_Out = HisHelper.GetTimeRegInfo(regInfo_InParam.DEPT_ID, regInfo_InParam.DOCTOR_ID, regInfo_InParam.REG_DATE, regInfo_InParam.TIME_FLAG);

                if (healthCardTimeRegInfo_Out.TimeRegLst.Count > 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResTimeRegInfo(healthCardTimeRegInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = 200404;
                    res_msg = "排班分时不存在，未查询到排班分时信息";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 200404;
                res_msg = "排班分时不存在，未查询到排班分时信息" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void PostOrderRegInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardOrderRegInfo_InParam regInfo_InParam = new HealthCardOrderRegInfo_InParam();
                XmlHelper.AnalysisXmlReqOrderRegInfo(req_bxml, ref regInfo_InParam);

                HealthCardOrderRegInfo_OutParam healthCardOrderReg_Out = HisHelper.SaveOrderRegInfo(regInfo_InParam, ref dicErr);

                if (healthCardOrderReg_Out != null)
                {
                    res_bxml = XmlHelper.AnalysisXmlResOrderRegInfo(healthCardOrderReg_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = 200702;
                    res_msg = "不符合科室挂号规则";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 200404;
                res_msg = "不符合科室挂号规则,异常信息" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void PostPayRegInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardPayRegInfo_InParam regInfo_InParam = new HealthCardPayRegInfo_InParam();
                XmlHelper.AnalysisXmlReqPayRegInfo(req_bxml, ref regInfo_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardPayRegInfo_OutParam healthCardPayRegInfo_Out = HisHelper.SavePayRegInfo(regInfo_InParam, ref dicErr);

                if (healthCardPayRegInfo_Out != null)
                {
                    res_bxml = XmlHelper.AnalysisXmlResPayRegInfo(healthCardPayRegInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 200803;
                res_msg = "挂号订单已关闭" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void PostCancelRegInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardCancelRegInfo_InParam cancelRegInfo_InParam = new HealthCardCancelRegInfo_InParam();
                XmlHelper.AnalysisXmlReqCancelRegInfo(req_bxml, ref cancelRegInfo_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                bool isSuccess = HisHelper.SaveCancelRegInfo(cancelRegInfo_InParam, ref dicErr);

                if (isSuccess)
                {
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 200903;
                res_msg = "挂号订单已关闭" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void PostRefundRegInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardRefundRegInfo_InParam refundRegInfo_InParam = new HealthCardRefundRegInfo_InParam();
                XmlHelper.AnalysisXmlReqRefundRegInfo(req_bxml, ref refundRegInfo_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardRefundRegInfo_OutParam healthCardRefundRegInfo_Out = HisHelper.SaveRefundRegInfo(refundRegInfo_InParam, ref dicErr);

                if (healthCardRefundRegInfo_Out != null)
                {
                    res_bxml = XmlHelper.AnalysisXmlResRefundRegInfo(healthCardRefundRegInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 200807;
                res_msg = "医院不允许退款" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void GetRegNumInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardGetRegNumInfo_InParam regNumInfo_InParam = new HealthCardGetRegNumInfo_InParam();
                XmlHelper.AnalysisXmlReqGetRegNumInfo(req_bxml, ref regNumInfo_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardGetRegNumInfo_OutParam healthCardGetRegNumInfo_Out = HisHelper.GetRegInfo(regNumInfo_InParam, ref dicErr);

                if (healthCardGetRegNumInfo_Out != null)
                {
                    res_bxml = XmlHelper.AnalysisXmlResGetRegNumInfo(healthCardGetRegNumInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 201102;
                res_msg = "挂号订单已关闭" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void QueryRegRecordInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardQueryRecordInfo_InParam queryRecord_InParam = new HealthCardQueryRecordInfo_InParam();
                XmlHelper.AnalysisXmlReqQueryRegRecordInfo(req_bxml, ref queryRecord_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardQueryRecordInfo_OutParam healthCardQueryRecordRegInfo_Out = HisHelper.QueryOrderRegRecordInfo(queryRecord_InParam, ref dicErr);

                if (dicErr.Count <= 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResQueryRegRecordInfo(healthCardQueryRecordRegInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 201201;
                res_msg = "未查询到挂号订单记录" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void GetPayListInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardPayListInfo_InParam payListInfo_InParam = new HealthCardPayListInfo_InParam();
                XmlHelper.AnalysisXmlReqPayListInfo(req_bxml, ref payListInfo_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardPayListInfo_OutParam healthCardpayListInfo_Out = HisHelper.GetPayListInfo(payListInfo_InParam, ref dicErr);

                if (dicErr.Count <= 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResPayListInfo(healthCardpayListInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 300101;
                res_msg = "缴费记录不存在，未查询到缴费订单记录" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void GetPayDetailInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardPayDetailInfo_InParam payDetailInfo_InParam = new HealthCardPayDetailInfo_InParam();
                XmlHelper.AnalysisXmlReqPayDetailInfo(req_bxml, ref payDetailInfo_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardPayDetailInfo_OutParam healthCardPayDetailInfo_Out = HisHelper.GetFeePayDetailInfo(payDetailInfo_InParam, ref dicErr);

                if (dicErr.Count <= 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResPayDetailInfo(healthCardPayDetailInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 201201;
                res_msg = "未查询到挂号订单记录" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void GetPayOrderInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardPayOrderInfo_InParam payOrder_InParam = new HealthCardPayOrderInfo_InParam();
                XmlHelper.AnalysisXmlReqPayOrderInfo(req_bxml, ref payOrder_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardPayOrderInfo_OutParam healthCardPayOrderInfo_Out = HisHelper.GetFeePayOrderInfo(payOrder_InParam, ref dicErr);

                if (dicErr.Count <= 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResPayOrderInfo(healthCardPayOrderInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 300301;
                res_msg = "缴费订单不存在" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void GetDepositRecordInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardDepositLst_InParam depositLst_InParam = new HealthCardDepositLst_InParam();
                XmlHelper.AnalysisXmlReqGetDepositLstInfo(req_bxml, ref depositLst_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardDepositLst_OutParam healthCardQueryRecordRegInfo_Out = HisHelper.QueryDepositRecordInfo(depositLst_InParam, ref dicErr);

                if (dicErr.Count <= 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResGetDepositLstInfo(healthCardQueryRecordRegInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 300301;
                res_msg = "缴费订单不存在" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void PostDepositPaymentInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardDepositPayInfo_InParam depositPayInfo_InParam = new HealthCardDepositPayInfo_InParam();
                XmlHelper.AnalysisXmlReqDepositPayInfo(req_bxml, ref depositPayInfo_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardDepositPayInfo_OutParam healthCardQueryRecordRegInfo_Out = HisHelper.SaveDepositPaymentInfo(depositPayInfo_InParam, ref dicErr);

                if (dicErr.Count <= 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResDepositPayInfo(healthCardQueryRecordRegInfo_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 300301;
                res_msg = "缴费订单不存在" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void GetLisExamReportInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardLisCheckReport_InParam LisCheckReport_InParam = new HealthCardLisCheckReport_InParam();
                XmlHelper.AnalysisXmlReqLisExamCheckInfo(req_bxml, ref LisCheckReport_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardLisCheckReport_OutParam healthCardLisCheck_OutParam = HisHelper.QueryCheckReportInfo(LisCheckReport_InParam, ref dicErr);

                if (dicErr.Count <= 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResLisExamCheckInfo(healthCardLisCheck_OutParam);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 800101;
                res_msg = "检查/检验报告记录不存在，未查询到检查/检验报告记录" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void GetNormalLisReportInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardNormalCheckReport_InParam normalCheckReport_InParam = new HealthCardNormalCheckReport_InParam();
                XmlHelper.AnalysisXmlReqNoramlLisCheckInfo(req_bxml, ref normalCheckReport_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardNormalCheckReport_OutParam healthCardNormalCheckReport_Out = HisHelper.QueryNormalReportInfo(normalCheckReport_InParam, ref dicErr);

                if (dicErr.Count <= 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResLNormalLisCheckInfo(healthCardNormalCheckReport_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 800201;
                res_msg = "检查/检验报告单号不存在" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }

        private static void GetDrugLisReportInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardDrugReportInfo_InParam drugReport_InParam = new HealthCardDrugReportInfo_InParam();
                XmlHelper.AnalysisXmlReqDrugReportInfo(req_bxml, ref drugReport_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardDrugReportInfo_OutParam healthCardDrugReport_Out = HisHelper.QueryDrugReportInfo(drugReport_InParam, ref dicErr);

                if (dicErr.Count <= 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResDrugReportInfo(healthCardDrugReport_Out);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 800301;
                res_msg = "检查/检验报告单号不存在" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }
        static log4net.ILog log2 = log4net.LogManager.GetLogger("log4net");
        private static void GetPacsReportInfo(string req_encrypted, ref int res_code, ref string res_msg, ref string res_encrypted, ref string res_sign)
        {
            string res_bxml = @"<RES></RES>";
            try
            {
                //获取请求业务参数
                string req_bxml = AESHelper.DecryptForAES(req_encrypted, key);

                HealthCardPacsCheckReport_InParam pacsReport_InParam = new HealthCardPacsCheckReport_InParam();
                XmlHelper.AnalysisXmlReqPacsReportInfo(req_bxml, ref pacsReport_InParam);
                Dictionary<string, string> dicErr = new Dictionary<string, string>();
                HealthCardPacsCheckReport_OutParam healthCardDrugReport_Out = HisHelper.QueryPacsReportInfo(pacsReport_InParam, ref dicErr);

                if (dicErr.Count <= 0)
                {
                    res_bxml = XmlHelper.AnalysisXmlResPacsReportInfo(healthCardDrugReport_Out);
                    log2.Debug(res_bxml);
                    res_code = 0;
                    res_msg = "交易成功";
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
                else
                {
                    res_code = Convert.ToInt32(dicErr.First().Key);
                    res_msg = dicErr.First().Value;
                    res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                    res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
                }
            }
            catch (Exception ex)
            {
                res_code = 800401;
                res_msg = "检查/检验报告单号不存在" + ex.ToString();
                res_encrypted = AESHelper.EncryptForAES(res_bxml, key);
                res_sign = Common.GetResponseSign(res_code, res_msg, res_encrypted, key);
            }
        }
        #endregion
    }
}
