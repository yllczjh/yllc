using HealthCardUtil.XML;
using HisCommon.DataEntity;
using HttpToken.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace HealthCardUtil.Tool
{
    public class XmlHelper
    {
        public static string DataPackage(NoSortHashtable DataList)
        {
            XML xmlMgr = new XML();

            XmlDocument doc = new XmlDocument();
            //XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            //doc.AppendChild(dec);
            //根节点
            XmlElement root = doc.CreateElement("request");
            doc.AppendChild(root);

            XmlElement accessSet = xmlMgr.AddXmlNode(doc, root, "access", "");
            XmlElement datasSet = xmlMgr.AddXmlNode(doc, root, "datas", "");

            xmlMgr.AddXmlNode(doc, accessSet, "access_type_code", "1");
            xmlMgr.AddXmlNode(doc, accessSet, "access_no", "220113-41275532X-offline");
            xmlMgr.AddXmlNode(doc, accessSet, "trade_no", Guid.NewGuid().ToString("N"));
            foreach (var keyValuePair in DataList.Keys)
            {
                string nodeName = keyValuePair == null ? string.Empty : keyValuePair.ToString();
                string nodeValue = DataList[keyValuePair] == null ? string.Empty : DataList[keyValuePair].ToString();
                //构造数据
                xmlMgr.AddXmlNode(doc, datasSet, nodeName, nodeValue);
            }

            return doc.InnerXml;
        }

        /// <summary>
        /// 保存并拆分（查询用）字符串
        /// </summary>
        /// <param name="xmlContent">内容</param>
        /// <returns></returns>
        public static void SplitPackage(string xmlContent, ref NoSortHashtable dataLists, ref NoSortHashtable returnLists)
        {
            if (!string.IsNullOrEmpty(xmlContent))
            {
                xmlContent = "<?xml version=\"1.0\" encoding=\"utf - 8\"?>" + xmlContent;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent);// 加载数据
                Logger.Default.Info(xmlContent);
                XmlNodeList rootNodes = xmlDoc.SelectNodes("response");
                if (rootNodes.Count > 0)
                {
                    //读取一级节点
                    foreach (System.Xml.XmlNode firstNodes in rootNodes[0])
                    {
                        Logger.Default.Info("董巍 " + firstNodes.Name);
                        if (firstNodes.Name == "info")
                        {
                            //读取二级节点
                            foreach (System.Xml.XmlNode secondNodes in firstNodes.ChildNodes)
                            {
                                dataLists.Add(secondNodes.Name, secondNodes.InnerText);
                            }
                        }

                        if (firstNodes.Name == "process_result")
                        {
                            //读取二级节点
                            foreach (System.Xml.XmlNode secondNodes in firstNodes.ChildNodes)
                            {
                                returnLists.Add(secondNodes.Name, secondNodes.InnerText);
                            }
                        }
                    }
                }
            }
        }

        public static string DataPackage(NoSortHashtable DataList, string secondNodeName, string threeNodeName, List<NoSortHashtable> rowList)
        {
            XML xmlMgr = new XML();

            XmlDocument doc = new XmlDocument();
            // XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "GB2312", "yes");
            // doc.AppendChild(dec);
            //根节点
            XmlElement root = doc.CreateElement("RES");
            doc.AppendChild(root);

            foreach (var keyValuePair in DataList.Keys)
            {
                string nodeName = keyValuePair == null ? string.Empty : keyValuePair.ToString();
                string nodeValue = DataList[keyValuePair] == null ? string.Empty : DataList[keyValuePair].ToString();
                //构造数据
                xmlMgr.AddXmlNode(doc, root, nodeName, nodeValue);
            }

            if (rowList.Count > 0)
            {
                //创建节点
                XmlElement rowSet = xmlMgr.AddXmlNode(doc, root, secondNodeName, "");
                //构造ROWSET
                foreach (NoSortHashtable hsTb in rowList)
                {
                    if (!string.IsNullOrEmpty(threeNodeName))
                    {
                        //创建节点   
                        XmlElement row = xmlMgr.AddXmlNode(doc, rowSet, threeNodeName, "");
                        //构造ROW
                        foreach (var keyValuePair in hsTb.Keys)
                        {
                            string nodeName = keyValuePair == null ? string.Empty : keyValuePair.ToString();
                            string nodeValue = hsTb[keyValuePair] == null ? string.Empty : hsTb[keyValuePair].ToString();
                            xmlMgr.AddXmlNode(doc, row, nodeName, nodeValue);
                        }
                    }
                    else
                    {
                        //构造ROW
                        foreach (var keyValuePair in hsTb.Keys)
                        {
                            string nodeName = keyValuePair == null ? string.Empty : keyValuePair.ToString();
                            string nodeValue = hsTb[keyValuePair] == null ? string.Empty : hsTb[keyValuePair].ToString();
                            xmlMgr.AddXmlNode(doc, rowSet, nodeName, nodeValue);
                        }
                    }
                }
            }
            return doc.InnerXml;
        }

        public static void AnalysisXmlReqBaseInfo(string xmlInfo, ref string req_fun_code, ref string req_user_id, ref string req_sign, ref string req_sign_type, ref string req_encrypted)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            req_fun_code = root.SelectSingleNode("ROOT/FUN_CODE").InnerText;
            req_user_id = root.SelectSingleNode("ROOT/USER_ID").InnerText;
            req_sign = root.SelectSingleNode("ROOT/SIGN").InnerText;
            req_sign_type = root.SelectSingleNode("ROOT/SIGN_TYPE").InnerText;
            req_encrypted = root.SelectSingleNode("ROOT/REQ_ENCRYPTED").InnerText;
        }

        public static void AnalysisXmlReqIPInfo(string xmlInfo, ref string reqIP)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            reqIP = root.SelectSingleNode("REQ/IP").InnerText;
        }

        public static void AnalysisXmlReqPatInfo(string xmlInfo, ref HEALTHCARD_PATIENT_INFO _PATIENT_INFO)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _PATIENT_INFO.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _PATIENT_INFO.Address = root.SelectSingleNode("REQ/ADDRESS").InnerText;
            _PATIENT_INFO.Mobile = root.SelectSingleNode("REQ/MOBILE").InnerText;
            _PATIENT_INFO.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _PATIENT_INFO.PATIENT_ID_TYPE = root.SelectSingleNode("REQ/PARENT_ID_TYPE").InnerText;
            _PATIENT_INFO.PATIENT_ID_CARD = root.SelectSingleNode("REQ/PARENT_ID_CARD").InnerText;
            _PATIENT_INFO.PATIENT_NAME = root.SelectSingleNode("REQ/PARENT_NAME").InnerText;
        }

        public static void AnalysisXmlReqHosInfo(string xmlInfo, ref Healthcard_HosInfo _HosInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _HosInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
        }

        public static void AnalysisXmlReqDeptInfo(string xmlInfo, ref HealthCardDeptInfo_InParam _healthCardDeptInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardDeptInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardDeptInfo.DEPT_ID = root.SelectSingleNode("REQ/DEPT_ID").InnerText;
        }

        public static string AnalysisXmlResDeptInfo(HealthCardDeptInfo_OutParam outParam)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES> <HOS_ID>" + outParam.HOS_ID + "</HOS_ID>");
            foreach (var deptInfo in outParam.DEPT_LIST)
            {
                builder.Append("<DEPT_LIST>");
                builder.Append("<DEPT_ID>" + deptInfo.DEPT_ID + "</DEPT_ID>");
                builder.Append("<DEPT_NAME>" + deptInfo.DEPT_NAME + "</DEPT_NAME>");
                builder.Append("<PARENT_ID>" + deptInfo.PARENT_ID + "</PARENT_ID>");
                builder.Append("<DESC>" + deptInfo.DESC + "</DESC>");
                builder.Append("<EXPERTISE>" + deptInfo.EXPERTISE + "</EXPERTISE>");
                builder.Append("<LEVEL>" + deptInfo.LEVEL + "</LEVEL>");
                builder.Append("<ADDRESS>" + deptInfo.ADDRESS + "</ADDRESS>");
                builder.Append("<STATUS>" + deptInfo.STATUS + "</STATUS>");
                builder.Append("</DEPT_LIST>");
            }

            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqDoctorInfo(string xmlInfo, ref HealthCardDoctorInfo_InParam _healthCardDoctorInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardDoctorInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardDoctorInfo.DEPT_ID = root.SelectSingleNode("REQ/DEPT_ID").InnerText;
            _healthCardDoctorInfo.DOCTOR_ID = root.SelectSingleNode("REQ/DOCTOR_ID").InnerText;
        }

        public static string AnalysisXmlResDoctorInfo(HealthCardDoctorInfo_OutParam outParam)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES> <HOS_ID>" + outParam.HOS_ID + "</HOS_ID>");

            foreach (var doctorInfo in outParam.Doctor_LIST)
            {
                builder.Append("<DOCTOR_LIST>");
                builder.Append("<DEPT_ID>" + doctorInfo.DEPT_ID + "</DEPT_ID>");
                builder.Append("<DOCTOR_ID>" + doctorInfo.DOCTOR_ID + "</DOCTOR_ID>");
                builder.Append("<NAME>" + doctorInfo.NAME + "</NAME>");
                builder.Append("<DESC>" + doctorInfo.DESC + "</DESC>");
                builder.Append("<IDCARD>" + doctorInfo.IDCARD + "</IDCARD>");
                builder.Append("<DESC>" + doctorInfo.DESC + "</DESC>");
                builder.Append("<SPECIAL>" + doctorInfo.SPECIAL + "</SPECIAL>");
                builder.Append("<JOB_TITLE>" + doctorInfo.JOB_TITLE + "</JOB_TITLE>");
                builder.Append("<REG_FEE>" + doctorInfo.REG_FEE + "</REG_FEE>");
                builder.Append("<STATUS>" + doctorInfo.STATUS + "</STATUS>");
                builder.Append("<SEX>" + doctorInfo.SEX + "</SEX>");
                builder.Append("<BIRTHDAY>" + doctorInfo.BIRTHDAY + "</BIRTHDAY>");
                builder.Append("<MOBILE>" + doctorInfo.MOBILE + "</MOBILE>");
                builder.Append("<TEL>" + doctorInfo.TEL + "</TEL>");
                builder.Append("</DOCTOR_LIST>");
            }
            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqRegInfo(string xmlInfo, ref HealthCardRegInfo_InParam _healthCardRegInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardRegInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardRegInfo.DEPT_ID = root.SelectSingleNode("REQ/DEPT_ID").InnerText;
            _healthCardRegInfo.DOCTOR_ID = root.SelectSingleNode("REQ/DOCTOR_ID").InnerText;

            _healthCardRegInfo.START_DATE = Convert.ToDateTime(root.SelectSingleNode("REQ/START_DATE").InnerText);
            _healthCardRegInfo.END_DATE = Convert.ToDateTime(root.SelectSingleNode("REQ/END_DATE").InnerText);
        }

        public static string AnalysisXmlResRegInfo(HealthCardRegInfo_OutParam _healthCardRegInfoOutParam)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES> <HOS_ID>" + _healthCardRegInfoOutParam.HOS_ID + "</HOS_ID>");
            builder.Append("<DEPT_ID>" + _healthCardRegInfoOutParam.DEPT_ID + "</DEPT_ID>");

            foreach (var doctorInfo in _healthCardRegInfoOutParam.DoctorLst)
            {
                builder.Append("<REG_DOCTOR_LIST>");
                builder.Append("<DOCTOR_ID>" + doctorInfo.DOCTOR_ID + "</DOCTOR_ID>");
                builder.Append("<NAME>" + doctorInfo.NAME + "</NAME>");
                builder.Append("<JOB_TITLE>" + doctorInfo.JOB_TITLE + "</JOB_TITLE>");

                foreach (var regInfo in doctorInfo.RegLst)
                {
                    builder.Append("<REG_LIST>");
                    builder.Append("<REG_DATE>" + regInfo.REG_DATE + "</REG_DATE>");
                    builder.Append("<REG_WEEKDAY>" + regInfo.REG_WEEKDAY + "</REG_WEEKDAY>");
                    foreach (HealthCardRegTimeInfo regTimeInfo in regInfo.RegTimeLst)
                    {
                        builder.Append("<REG_TIME_LIST>");
                        builder.Append("<REG_ID>" + regTimeInfo.REG_ID + "</REG_ID>");
                        builder.Append("<TIME_FLAG>" + regTimeInfo.TIME_FLAG + "</TIME_FLAG>");
                        builder.Append("<REG_STATUS>" + regTimeInfo.REG_STATUS + "</REG_STATUS>");
                        builder.Append("<TOTAL>" + regTimeInfo.TOTAL + "</TOTAL>");
                        builder.Append("<OVER_COUNT>" + regTimeInfo.OVER_COUNT + "</OVER_COUNT>");
                        builder.Append("<REG_LEVEL>" + regTimeInfo.REG_LEVEL + "</REG_LEVEL>");
                        builder.Append("<REG_FEE>" + regTimeInfo.REG_FEE + "</REG_FEE>");
                        builder.Append("<TREAT_FEE>" + regTimeInfo.TREAT_FEE + "</TREAT_FEE>");
                        builder.Append("<ISTIME>" + regTimeInfo.ISTIME + "</ISTIME>");
                        builder.Append("</REG_TIME_LIST>");
                    }
                    builder.Append("</REG_LIST>");
                }

                builder.Append("</REG_DOCTOR_LIST>");
            }

            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqTimeRegInfo(string xmlInfo, ref HealthCardTimeRegInfo_InParam _healthCardTimeRegInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardTimeRegInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardTimeRegInfo.DEPT_ID = root.SelectSingleNode("REQ/DEPT_ID").InnerText;
            _healthCardTimeRegInfo.DOCTOR_ID = root.SelectSingleNode("REQ/DOCTOR_ID").InnerText;

            _healthCardTimeRegInfo.REG_DATE = Convert.ToDateTime(root.SelectSingleNode("REQ/REG_DATE").InnerText);
            _healthCardTimeRegInfo.TIME_FLAG = root.SelectSingleNode("REQ/TIME_FLAG").InnerText;
        }

        public static string AnalysisXmlResTimeRegInfo(HealthCardTimeRegInfo_OutParam _healthCardTimeRegInfoOutParam)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");

            foreach (var timeRegInfo in _healthCardTimeRegInfoOutParam.TimeRegLst)
            {
                builder.Append("<TIME_REG_LIST>");
                builder.Append("<TIME_FLAG>" + timeRegInfo.TIME_FLAG + "</TIME_FLAG>");
                builder.Append("<BEGIN_TIME>" + timeRegInfo.BEGIN_TIME + "</BEGIN_TIME>");
                builder.Append("<END_TIME>" + timeRegInfo.END_TIME + "</END_TIME>");
                builder.Append("<TOTAL>" + timeRegInfo.TOTAL + "</TOTAL>");
                builder.Append("<OVER_COUNT>" + timeRegInfo.OVER_COUNT + "</OVER_COUNT>");
                builder.Append("<REG_ID>" + timeRegInfo.REG_ID + "</REG_ID>");
                builder.Append("</TIME_REG_LIST>");
            }

            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqOrderRegInfo(string xmlInfo, ref HealthCardOrderRegInfo_InParam _healthCardOrderRegInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardOrderRegInfo.ORDER_ID = root.SelectSingleNode("REQ/ORDER_ID").InnerText;
            _healthCardOrderRegInfo.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardOrderRegInfo.HOSP_PATIENT_ID = root.SelectSingleNode("REQ/HOSP_PATIENT_ID").InnerText;
            _healthCardOrderRegInfo.CHANNEL_ID = root.SelectSingleNode("REQ/CHANNEL_ID").InnerText;
            _healthCardOrderRegInfo.IS_REG = root.SelectSingleNode("REQ/IS_REG").InnerText;
            _healthCardOrderRegInfo.REG_ID = root.SelectSingleNode("REQ/REG_ID").InnerText;
            _healthCardOrderRegInfo.REG_LEVEL = root.SelectSingleNode("REQ/REG_LEVEL").InnerText;
            _healthCardOrderRegInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardOrderRegInfo.DEPT_ID = root.SelectSingleNode("REQ/DEPT_ID").InnerText;
            _healthCardOrderRegInfo.DOCTOR_ID = root.SelectSingleNode("REQ/DOCTOR_ID").InnerText;
            _healthCardOrderRegInfo.REG_DATE = root.SelectSingleNode("REQ/REG_DATE").InnerText;
            _healthCardOrderRegInfo.TIME_FLAG = root.SelectSingleNode("REQ/TIME_FLAG").InnerText;
            _healthCardOrderRegInfo.BEGIN_TIME = root.SelectSingleNode("REQ/BEGIN_TIME").InnerText;
            _healthCardOrderRegInfo.END_TIME = root.SelectSingleNode("REQ/END_TIME").InnerText;
            _healthCardOrderRegInfo.REG_FEE = root.SelectSingleNode("REQ/REG_FEE").InnerText;
            _healthCardOrderRegInfo.TREAT_FEE = root.SelectSingleNode("REQ/TREAT_FEE").InnerText;
            _healthCardOrderRegInfo.REG_TYPE = root.SelectSingleNode("REQ/REG_TYPE").InnerText;
            _healthCardOrderRegInfo.MOBILE = root.SelectSingleNode("REQ/MOBILE").InnerText;
            _healthCardOrderRegInfo.ORDER_TIME = root.SelectSingleNode("REQ/ORDER_TIME").InnerText;
        }


        public static string AnalysisXmlResOrderRegInfo(HealthCardOrderRegInfo_OutParam _healthCardOrderRegInfoOutParam)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");

            builder.Append("<HOSP_ORDER_ID>" + _healthCardOrderRegInfoOutParam.HOSP_ORDER_ID + "</HOSP_ORDER_ID>");
            builder.Append("<HEALTH_CARD_ID>" + _healthCardOrderRegInfoOutParam.HEALTH_CARD_ID + "</HEALTH_CARD_ID>");
            builder.Append("<HOSP_PATIENT_ID>" + _healthCardOrderRegInfoOutParam.HOSP_PATIENT_ID + "</HOSP_PATIENT_ID>");
            builder.Append("<HOSP_SERIAL_NUM>" + _healthCardOrderRegInfoOutParam.HOSP_SERIAL_NUM + "</HOSP_SERIAL_NUM>");
            builder.Append("<HOSP_MEDICAL_NUM>" + _healthCardOrderRegInfoOutParam.HOSP_MEDICAL_NUM + "</HOSP_MEDICAL_NUM>");
            builder.Append("<HOSP_GETREG_DATE>" + _healthCardOrderRegInfoOutParam.HOSP_GETREG_DATE + "</HOSP_GETREG_DATE>");
            builder.Append("<HOSP_SEE_DOCT_ADDR>" + _healthCardOrderRegInfoOutParam.HOSP_SEE_DOCT_ADDR + "</HOSP_SEE_DOCT_ADDR>");
            builder.Append("<HOSP_CARD_NO>" + _healthCardOrderRegInfoOutParam.HOSP_CARD_NO + "</HOSP_CARD_NO>");
            builder.Append("<HOSP_REMARK>" + _healthCardOrderRegInfoOutParam.HOSP_REMARK + "</HOSP_REMARK>");
            builder.Append("<IS_CONCESSIONS>" + _healthCardOrderRegInfoOutParam.IS_CONCESSIONS + "</IS_CONCESSIONS>");

            if (_healthCardOrderRegInfoOutParam.IS_CONCESSIONS == "1")
            {
                foreach (var concession in _healthCardOrderRegInfoOutParam.CONCESSIONS)
                {
                    builder.Append("<CONCESSIONS>");
                    builder.Append("<CONCESSIONS_FEE>" + concession.CONCESSIONS_FEE + "</CONCESSIONS_FEE>");
                    builder.Append("<REAL_REG_FEE>" + concession.REAL_REG_FEE + "</REAL_REG_FEE>");
                    builder.Append("<REAL_TREAT_FEE>" + concession.REAL_TREAT_FEE + "</REAL_TREAT_FEE>");
                    builder.Append("<REAL_TOTAL_FEE>" + concession.REAL_TOTAL_FEE + "</REAL_TOTAL_FEE>");
                    builder.Append("<CONCESSIONS_TYPE>" + concession.CONCESSIONS_TYPE + "</CONCESSIONS_TYPE>");
                    builder.Append("</CONCESSIONS>");
                }
            }

            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqPayRegInfo(string xmlInfo, ref HealthCardPayRegInfo_InParam _healthCardPayRegInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardPayRegInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardPayRegInfo.ORDER_ID = root.SelectSingleNode("REQ/ORDER_ID").InnerText;
            _healthCardPayRegInfo.SERIAL_NUM = root.SelectSingleNode("REQ/SERIAL_NUM").InnerText;
            _healthCardPayRegInfo.PAY_DATE = root.SelectSingleNode("REQ/PAY_DATE").InnerText;
            _healthCardPayRegInfo.PAY_TIME = root.SelectSingleNode("REQ/PAY_TIME").InnerText;
            _healthCardPayRegInfo.PAY_CHANNEL_ID = root.SelectSingleNode("REQ/PAY_CHANNEL_ID").InnerText;
            _healthCardPayRegInfo.PAY_TOTAL_FEE = root.SelectSingleNode("REQ/PAY_TOTAL_FEE").InnerText;
            _healthCardPayRegInfo.PAY_COPE_FEE = root.SelectSingleNode("REQ/PAY_COPE_FEE").InnerText;
            _healthCardPayRegInfo.PAY_FEE = root.SelectSingleNode("REQ/PAY_FEE").InnerText;
            _healthCardPayRegInfo.PAY_RES_CODE = root.SelectSingleNode("REQ/PAY_RES_CODE").InnerText;
            _healthCardPayRegInfo.PAY_RES_DESC = root.SelectSingleNode("REQ/PAY_RES_DESC").InnerText;
            _healthCardPayRegInfo.MERCHANT_ID = root.SelectSingleNode("REQ/MERCHANT_ID").InnerText;
            _healthCardPayRegInfo.TERMINAL_ID = root.SelectSingleNode("REQ/TERMINAL_ID").InnerText;
            _healthCardPayRegInfo.BANK_NO = root.SelectSingleNode("REQ/BANK_NO").InnerText;
            _healthCardPayRegInfo.PAY_ACCOUNT = root.SelectSingleNode("REQ/PAY_ACCOUNT").InnerText;
        }


        public static string AnalysisXmlResPayRegInfo(HealthCardPayRegInfo_OutParam _healthCardPayRegInfoOutParam)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<HOSP_PAY_ID>" + _healthCardPayRegInfoOutParam.HOSP_PAY_ID + "</HOSP_PAY_ID>");
            builder.Append("<RECEIPT_ID>" + _healthCardPayRegInfoOutParam.RECEIPT_ID + "</RECEIPT_ID>");
            builder.Append("<HOSP_SERIAL_NUM>" + _healthCardPayRegInfoOutParam.HOSP_SERIAL_NUM + "</HOSP_SERIAL_NUM>");
            builder.Append("<HOSP_MEDICAL_NUM>" + _healthCardPayRegInfoOutParam.HOSP_MEDICAL_NUM + "</HOSP_MEDICAL_NUM>");
            builder.Append("<HOSP_GETREG_DATE>" + _healthCardPayRegInfoOutParam.HOSP_GETREG_DATE + "</HOSP_GETREG_DATE>");
            builder.Append("<HOSP_SEE_DOCT_ADDR>" + _healthCardPayRegInfoOutParam.HOSP_SEE_DOCT_ADDR + "</HOSP_SEE_DOCT_ADDR>");
            builder.Append("<HOSP_REMARK>" + _healthCardPayRegInfoOutParam.HOSP_REMARK + "</HOSP_REMARK>");
            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqCancelRegInfo(string xmlInfo, ref HealthCardCancelRegInfo_InParam _healthCardCancelRegInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardCancelRegInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardCancelRegInfo.ORDER_ID = root.SelectSingleNode("REQ/ORDER_ID").InnerText;
            _healthCardCancelRegInfo.CANCEL_DATE = root.SelectSingleNode("REQ/CANCEL_DATE").InnerText;
            _healthCardCancelRegInfo.CANCEL_REMARK = root.SelectSingleNode("REQ/CANCEL_REMARK").InnerText;
        }

        public static void AnalysisXmlReqRefundRegInfo(string xmlInfo, ref HealthCardRefundRegInfo_InParam _healthCardRefundRegInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardRefundRegInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardRefundRegInfo.ORDER_ID = root.SelectSingleNode("REQ/ORDER_ID").InnerText;
            _healthCardRefundRegInfo.HOSP_ORDER_ID = root.SelectSingleNode("REQ/HOSP_ORDER_ID").InnerText;
            _healthCardRefundRegInfo.REFUND_ID = root.SelectSingleNode("REQ/REFUND_ID").InnerText;
            _healthCardRefundRegInfo.REFUND_SERIAL_NUM = root.SelectSingleNode("REQ/REFUND_SERIAL_NUM").InnerText;
            _healthCardRefundRegInfo.TOTAL_FEE = root.SelectSingleNode("REQ/TOTAL_FEE").InnerText;
            _healthCardRefundRegInfo.REFUND_FEE = root.SelectSingleNode("REQ/REFUND_FEE").InnerText;
            _healthCardRefundRegInfo.REFUND_DATE = root.SelectSingleNode("REQ/REFUND_DATE").InnerText;
            _healthCardRefundRegInfo.REFUND_TIME = root.SelectSingleNode("REQ/REFUND_TIME").InnerText;
            _healthCardRefundRegInfo.REFUND_RES_CODE = root.SelectSingleNode("REQ/REFUND_RES_CODE").InnerText;
            _healthCardRefundRegInfo.REFUND_RES_DESC = root.SelectSingleNode("REQ/REFUND_RES_DESC").InnerText;
            _healthCardRefundRegInfo.REFUND_REMARK = root.SelectSingleNode("REQ/REFUND_REMARK").InnerText;
        }

        public static string AnalysisXmlResRefundRegInfo(HealthCardRefundRegInfo_OutParam _healthCardRefundRegInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<HOSP_REFUND_ID>" + _healthCardRefundRegInfo.HOSP_REFUND_ID + "</HOSP_REFUND_ID>");
            builder.Append("<REFUND_FLAG>" + _healthCardRefundRegInfo.REFUND_FLAG + "</REFUND_FLAG>");
            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqGetRegNumInfo(string xmlInfo, ref HealthCardGetRegNumInfo_InParam _healthCardGetRegNumInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardGetRegNumInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardGetRegNumInfo.ORDER_ID = root.SelectSingleNode("REQ/ORDER_ID").InnerText;
        }

        public static string AnalysisXmlResGetRegNumInfo(HealthCardGetRegNumInfo_OutParam _healthCardGetRegNumInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<HOSP_SERIAL_NUM>" + _healthCardGetRegNumInfo.HOSP_SERIAL_NUM + "</HOSP_SERIAL_NUM>");
            builder.Append("<REMARK>" + _healthCardGetRegNumInfo.REMARK + "</REMARK>");
            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqQueryRegRecordInfo(string xmlInfo, ref HealthCardQueryRecordInfo_InParam _healthCardQueryRecordInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardQueryRecordInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardQueryRecordInfo.ORDER_ID = root.SelectSingleNode("REQ/ORDER_ID").InnerText;
            _healthCardQueryRecordInfo.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardQueryRecordInfo.HOSP_PATIENT_ID = root.SelectSingleNode("REQ/HOSP_PATIENT_ID").InnerText;
            _healthCardQueryRecordInfo.BEGIN_DATE = root.SelectSingleNode("REQ/BEGIN_DATE").InnerText;
            _healthCardQueryRecordInfo.END_DATE = root.SelectSingleNode("REQ/END_DATE").InnerText;
            _healthCardQueryRecordInfo.PAGE_CURRENT = root.SelectSingleNode("REQ/PAGE_CURRENT").InnerText;
            _healthCardQueryRecordInfo.PAGE_SIZE = root.SelectSingleNode("REQ/PAGE_SIZE").InnerText;
        }

        public static string AnalysisXmlResQueryRegRecordInfo(HealthCardQueryRecordInfo_OutParam _healthCardQueryRecordInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<COUNT>" + _healthCardQueryRecordInfo.COUNT + "</COUNT>");

            if (_healthCardQueryRecordInfo != null &&
                _healthCardQueryRecordInfo.ORDER_LIST.Count > 0)
            {
                foreach (var order in _healthCardQueryRecordInfo.ORDER_LIST)
                {
                    builder.Append("<ORDER_LIST>");
                    builder.Append("<ORDER_ID>" + order.ORDER_ID + "</ORDER_ID>");
                    builder.Append("<ORDER_STATUS>" + order.ORDER_STATUS + "</ORDER_STATUS>");
                    builder.Append("<HOSP_SERIAL_NUM>" + order.HOSP_SERIAL_NUM + "</HOSP_SERIAL_NUM>");
                    builder.Append("<GET_REGNO_DATE>" + order.GET_REGNO_DATE + "</GET_REGNO_DATE>");
                    builder.Append("<HOSP_PAY_ID>" + order.HOSP_PAY_ID + "</HOSP_PAY_ID>");
                    builder.Append("<HOSP_MEDICAL_NUM>" + order.HOSP_MEDICAL_NUM + "</HOSP_MEDICAL_NUM>");
                    builder.Append("<HOSP_GETREG_DATE>" + order.HOSP_GETREG_DATE + "</HOSP_GETREG_DATE>");
                    builder.Append("<HOSP_REFUND_ID>" + order.HOSP_REFUND_ID + "</HOSP_REFUND_ID>");
                    builder.Append("<REFUND_FLAG>" + order.REFUND_FLAG + "</REFUND_FLAG>");
                    builder.Append("<CANCEL_DATE>" + order.CANCEL_DATE + "</CANCEL_DATE>");
                    builder.Append("</ORDER_LIST>");
                }
            }

            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqPayListInfo(string xmlInfo, ref HealthCardPayListInfo_InParam _healthCardPayListInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardPayListInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardPayListInfo.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardPayListInfo.HOSP_PATIENT_ID = root.SelectSingleNode("REQ/HOSP_PATIENT_ID").InnerText;
            _healthCardPayListInfo.QUERY_TYPE = root.SelectSingleNode("REQ/QUERY_TYPE").InnerText;
            _healthCardPayListInfo.BEGIN_DATE = root.SelectSingleNode("REQ/BEGIN_DATE").InnerText;
            _healthCardPayListInfo.END_DATE = root.SelectSingleNode("REQ/END_DATE").InnerText;
        }

        public static string AnalysisXmlResPayListInfo(HealthCardPayListInfo_OutParam _healthCardPayListInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<USER_NAME>" + _healthCardPayListInfo.USER_NAME + "</USER_NAME>");
            builder.Append("<HEALTH_CARD_ID>" + _healthCardPayListInfo.HEALTH_CARD_ID + "</HEALTH_CARD_ID>");
            builder.Append("<HOSP_PATIENT_ID>" + _healthCardPayListInfo.HOSP_PATIENT_ID + "</HOSP_PATIENT_ID>");
            builder.Append("<IDCARD_TYPE>" + _healthCardPayListInfo.IDCARD_TYPE + "</IDCARD_TYPE>");
            builder.Append("<IDCARD_NO>" + _healthCardPayListInfo.IDCARD_NO + "</IDCARD_NO>");
            builder.Append("<CARD_TYPE>" + _healthCardPayListInfo.CARD_TYPE + "</CARD_TYPE>");
            builder.Append("<CARD_NO>" + _healthCardPayListInfo.CARD_NO + "</CARD_NO>");

            if (_healthCardPayListInfo.PAY_LIST != null &&
               _healthCardPayListInfo.PAY_LIST.Count > 0)
            {
                foreach (var payInfo in _healthCardPayListInfo.PAY_LIST)
                {
                    builder.Append("<PAY_LIST>");
                    builder.Append("<HOSP_SEQUENCE>" + payInfo.HOSP_SEQUENCE + "</HOSP_SEQUENCE>");
                    builder.Append("<DEPT_NAME>" + payInfo.DEPT_NAME + "</DEPT_NAME>");
                    builder.Append("<DOCTOR_NAME>" + payInfo.DOCTOR_NAME + "</DOCTOR_NAME>");
                    builder.Append("<PAY_AMOUT>" + payInfo.PAY_AMOUT + "</PAY_AMOUT>");
                    builder.Append("<PAY_CHANNEL_ID>" + payInfo.PAY_CHANNEL_ID + "</PAY_CHANNEL_ID>");
                    builder.Append("<ORDER_STATUS>" + payInfo.ORDER_STATUS + "</ORDER_STATUS>");
                    builder.Append("<RECEIPT_ID>" + payInfo.RECEIPT_ID + "</RECEIPT_ID>");
                    builder.Append("<PAY_REMARK>" + payInfo.PAY_REMARK + "</PAY_REMARK>");
                    builder.Append("<RECEIPT_DATE>" + payInfo.RECEIPT_DATE + "</RECEIPT_DATE>");
                    builder.Append("</PAY_LIST>");
                }
            }

            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqPayDetailInfo(string xmlInfo, ref HealthCardPayDetailInfo_InParam _healthCardPayDetail)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardPayDetail.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardPayDetail.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardPayDetail.HOSP_PATIENT_ID = root.SelectSingleNode("REQ/HOSP_PATIENT_ID").InnerText;
            _healthCardPayDetail.HOSP_SEQUENCE = root.SelectSingleNode("REQ/HOSP_SEQUENCE").InnerText;
        }

        public static string AnalysisXmlResPayDetailInfo(HealthCardPayDetailInfo_OutParam _healthCardPayDetailInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<USER_NAME>" + _healthCardPayDetailInfo.USER_NAME + "</USER_NAME>");
            builder.Append("<HEALTH_CARD_ID>" + _healthCardPayDetailInfo.HEALTH_CARD_ID + "</HEALTH_CARD_ID>");
            builder.Append("<HOSP_PATIENT_ID>" + _healthCardPayDetailInfo.HOSP_PATIENT_ID + "</HOSP_PATIENT_ID>");
            builder.Append("<MEDICAL_INSURANNCE_TYPE>" + _healthCardPayDetailInfo.MEDICAL_INSURANNCE_TYPE + "</MEDICAL_INSURANNCE_TYPE>");
            builder.Append("<PAY_TOTAL_FEE>" + _healthCardPayDetailInfo.PAY_TOTAL_FEE + "</PAY_TOTAL_FEE>");
            builder.Append("<PAY_BEHOOVE_FEE>" + _healthCardPayDetailInfo.PAY_BEHOOVE_FEE + "</PAY_BEHOOVE_FEE>");
            builder.Append("<PAY_ACTUAL_FEE>" + _healthCardPayDetailInfo.PAY_ACTUAL_FEE + "</PAY_ACTUAL_FEE>");
            builder.Append("<PAY_MI_FEE>" + _healthCardPayDetailInfo.PAY_MI_FEE + "</PAY_MI_FEE>");
            builder.Append("<RECEIPT_ID>" + _healthCardPayDetailInfo.RECEIPT_ID + "</RECEIPT_ID>");

            if (_healthCardPayDetailInfo.PAY_DETAIL_LIST != null &&
               _healthCardPayDetailInfo.PAY_DETAIL_LIST.Count > 0)
            {
                foreach (var payDetail in _healthCardPayDetailInfo.PAY_DETAIL_LIST)
                {
                    builder.Append("<PAY_DETAIL_LIST>");
                    builder.Append("<DETAIL_TYPE>" + payDetail.DETAIL_TYPE + "</DETAIL_TYPE>");
                    builder.Append("<DETAIL_NAME>" + payDetail.DETAIL_NAME + "</DETAIL_NAME>");
                    builder.Append("<DETAIL_ID>" + payDetail.DETAIL_ID + "</DETAIL_ID>");
                    builder.Append("<DETAIL_UNIT>" + payDetail.DETAIL_UNIT + "</DETAIL_UNIT>");
                    builder.Append("<DETAIL_COUNT>" + payDetail.DETAIL_COUNT + "</DETAIL_COUNT>");
                    builder.Append("<DETAIL_PRICE>" + payDetail.DETAIL_PRICE + "</DETAIL_PRICE>");
                    builder.Append("<DETAIL_SPEC>" + payDetail.DETAIL_SPEC + "</DETAIL_SPEC>");
                    builder.Append("<DETAIL_AMOUT>" + payDetail.DETAIL_AMOUT + "</DETAIL_AMOUT>");
                    builder.Append("<DETAIL_MI>" + payDetail.DETAIL_MI + "</DETAIL_MI>");
                    builder.Append("</PAY_DETAIL_LIST>");
                }
            }

            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqPayOrderInfo(string xmlInfo, ref HealthCardPayOrderInfo_InParam _healthCardPayOrder)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardPayOrder.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardPayOrder.ORDER_ID = root.SelectSingleNode("REQ/ORDER_ID").InnerText;
            _healthCardPayOrder.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardPayOrder.HOSP_PATIENT_ID = root.SelectSingleNode("REQ/HOSP_PATIENT_ID").InnerText;
            _healthCardPayOrder.HOSP_SEQUENCE = root.SelectSingleNode("REQ/HOSP_SEQUENCE").InnerText;
            _healthCardPayOrder.SERIAL_NUM = root.SelectSingleNode("REQ/SERIAL_NUM").InnerText;
            _healthCardPayOrder.PAY_DATE = root.SelectSingleNode("REQ/PAY_DATE").InnerText;
            _healthCardPayOrder.PAY_TIME = root.SelectSingleNode("REQ/PAY_TIME").InnerText;
            _healthCardPayOrder.PAY_CHANNEL_ID = root.SelectSingleNode("REQ/PAY_CHANNEL_ID").InnerText;
            _healthCardPayOrder.PAY_TOTAL_FEE = root.SelectSingleNode("REQ/PAY_TOTAL_FEE").InnerText;
            _healthCardPayOrder.PAY_BEHOOVE_FEE = root.SelectSingleNode("REQ/PAY_BEHOOVE_FEE").InnerText;
            _healthCardPayOrder.PAY_MI_FEE = root.SelectSingleNode("REQ/PAY_MI_FEE").InnerText;
            _healthCardPayOrder.PAY_RES_CODE = root.SelectSingleNode("REQ/PAY_RES_CODE").InnerText;
            _healthCardPayOrder.PAY_RES_DESC = root.SelectSingleNode("REQ/PAY_RES_DESC").InnerText;
            _healthCardPayOrder.MERCHANT_ID = root.SelectSingleNode("REQ/MERCHANT_ID").InnerText;
            _healthCardPayOrder.TERMINAL_ID = root.SelectSingleNode("REQ/TERMINAL_ID").InnerText;
            _healthCardPayOrder.BANK_NO = root.SelectSingleNode("REQ/BANK_NO").InnerText;
            _healthCardPayOrder.PAY_ACCOUNT = root.SelectSingleNode("REQ/PAY_ACCOUNT").InnerText;
            _healthCardPayOrder.OPERATOR_ID = root.SelectSingleNode("REQ/OPERATOR_ID").InnerText;
            _healthCardPayOrder.RECEIPT_ID = root.SelectSingleNode("REQ/RECEIPT_ID").InnerText;
        }

        public static string AnalysisXmlResPayOrderInfo(HealthCardPayOrderInfo_OutParam _healthCardPayOrderInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<HOSP_ORDER_ID>" + _healthCardPayOrderInfo.HOSP_ORDER_ID + "</HOSP_ORDER_ID>");
            builder.Append("<RECEIPT_ID>" + _healthCardPayOrderInfo.RECEIPT_ID + "</RECEIPT_ID>");
            builder.Append("<HOSP_MEDICAL_NUM>" + _healthCardPayOrderInfo.HOSP_MEDICAL_NUM + "</HOSP_MEDICAL_NUM>");
            builder.Append("<HOSP_REMARK>" + _healthCardPayOrderInfo.HOSP_REMARK + "</HOSP_REMARK>");
            builder.Append("</RES>");
            return builder.ToString();
        }


        public static void AnalysisXmlReqGetDepositLstInfo(string xmlInfo, ref HealthCardDepositLst_InParam _healthCardDepositLst)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardDepositLst.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardDepositLst.HOSP_PATIENT_ID = root.SelectSingleNode("REQ/HOSP_PATIENT_ID").InnerText;
            _healthCardDepositLst.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardDepositLst.BEGIN_DATE = root.SelectSingleNode("REQ/BEGIN_DATE").InnerText;
            _healthCardDepositLst.END_DATE = root.SelectSingleNode("REQ/END_DATE").InnerText;
        }

        public static string AnalysisXmlResGetDepositLstInfo(HealthCardDepositLst_OutParam _healthCardDepositLst)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<HOSP_PATIENT_NO>" + _healthCardDepositLst.HOSP_PATIENT_NO + "</HOSP_PATIENT_NO>");
            builder.Append("<PATIENT_NAME>" + _healthCardDepositLst.PATIENT_NAME + "</PATIENT_NAME>");
            builder.Append("<TREAT_DATE>" + _healthCardDepositLst.TREAT_DATE + "</TREAT_DATE>");
            builder.Append("<DEPT_NAME>" + _healthCardDepositLst.DEPT_NAME + "</DEPT_NAME>");
            builder.Append("<BUNK_ID>" + _healthCardDepositLst.BUNK_ID + "</BUNK_ID>");
            builder.Append("<SUMMARY_ADVANCES>" + _healthCardDepositLst.SUMMARY_ADVANCES + "</SUMMARY_ADVANCES>");
            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqDepositPayInfo(string xmlInfo, ref HealthCardDepositPayInfo_InParam _healthCardDepositPayInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardDepositPayInfo.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardDepositPayInfo.HOSP_PATIENT_NO = root.SelectSingleNode("REQ/HOSP_PATIENT_NO").InnerText;
            _healthCardDepositPayInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardDepositPayInfo.ORDER_ID = root.SelectSingleNode("REQ/ORDER_ID").InnerText;
            _healthCardDepositPayInfo.SERIAL_NUM = root.SelectSingleNode("REQ/SERIAL_NUM").InnerText;
            _healthCardDepositPayInfo.PAY_DATE = root.SelectSingleNode("REQ/PAY_DATE").InnerText;
            _healthCardDepositPayInfo.PAY_TIME = root.SelectSingleNode("REQ/PAY_TIME").InnerText;
            _healthCardDepositPayInfo.PAY_CHANNEL_ID = root.SelectSingleNode("REQ/PAY_CHANNEL_ID").InnerText;
            _healthCardDepositPayInfo.ADVANCE_PAYMENT = root.SelectSingleNode("REQ/ADVANCE_PAYMENT").InnerText;
            _healthCardDepositPayInfo.PAY_RES_CODE = root.SelectSingleNode("REQ/PAY_RES_CODE").InnerText;
            _healthCardDepositPayInfo.PAY_RES_DESC = root.SelectSingleNode("REQ/PAY_RES_DESC").InnerText;
            _healthCardDepositPayInfo.MERCHANT_ID = root.SelectSingleNode("REQ/MERCHANT_ID").InnerText;
            _healthCardDepositPayInfo.TERMINAL_ID = root.SelectSingleNode("REQ/TERMINAL_ID").InnerText;
            _healthCardDepositPayInfo.BANK_NO = root.SelectSingleNode("REQ/BANK_NO").InnerText;
            _healthCardDepositPayInfo.PAY_ACCOUNT = root.SelectSingleNode("REQ/PAY_ACCOUNT").InnerText;
            _healthCardDepositPayInfo.OPERATOR_ID = root.SelectSingleNode("REQ/OPERATOR_ID").InnerText;
            _healthCardDepositPayInfo.RECEIPT_ID = root.SelectSingleNode("REQ/RECEIPT_ID").InnerText;
        }

        public static string AnalysisXmlResDepositPayInfo(HealthCardDepositPayInfo_OutParam _healthCardDepositPayInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<HOSP_ORDER_ID>" + _healthCardDepositPayInfo.HOSP_ORDER_ID + "</HOSP_ORDER_ID>");
            builder.Append("<RECEIPT_ID>" + _healthCardDepositPayInfo.RECEIPT_ID + "</RECEIPT_ID>");
            builder.Append("<HOSP_PATIENT_NO>" + _healthCardDepositPayInfo.HOSP_PATIENT_NO + "</HOSP_PATIENT_NO>");
            builder.Append("<HOSP_REMARK>" + _healthCardDepositPayInfo.HOSP_REMARK + "</HOSP_REMARK>");
            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqLisExamCheckInfo(string xmlInfo, ref HealthCardLisCheckReport_InParam _healthCardLisCheckReportInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardLisCheckReportInfo.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardLisCheckReportInfo.HOSP_PATIENT_ID = root.SelectSingleNode("REQ/HOSP_PATIENT_ID").InnerText;
            _healthCardLisCheckReportInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardLisCheckReportInfo.BEGIN_DATE = root.SelectSingleNode("REQ/BEGIN_DATE").InnerText;
            _healthCardLisCheckReportInfo.END_DATE = root.SelectSingleNode("REQ/END_DATE").InnerText;
        }

        public static string AnalysisXmlResLisExamCheckInfo(HealthCardLisCheckReport_OutParam _healthCardLisCheckReportInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<HOS_ID>" + _healthCardLisCheckReportInfo.HOS_ID + "</HOS_ID>");
            builder.Append("<HEALTH_CARD_ID>" + _healthCardLisCheckReportInfo.HEALTH_CARD_ID + "</HEALTH_CARD_ID>");
            builder.Append("<HOSP_PATIENT_ID>" + _healthCardLisCheckReportInfo.HOSP_PATIENT_ID + "</HOSP_PATIENT_ID>");
            builder.Append("<PATIENT_IDCARD_TYPE>" + _healthCardLisCheckReportInfo.PATIENT_IDCARD_TYPE + "</PATIENT_IDCARD_TYPE>");
            builder.Append("<PATIENT_CARD_NO>" + _healthCardLisCheckReportInfo.PATIENT_CARD_NO + "</PATIENT_CARD_NO>");
            builder.Append("<PATIENT_IDCARD_NO>" + _healthCardLisCheckReportInfo.PATIENT_IDCARD_NO + "</PATIENT_IDCARD_NO>");
            builder.Append("<PATIENT_NAME>" + _healthCardLisCheckReportInfo.PATIENT_NAME + "</PATIENT_NAME>");
            builder.Append("<PATIENT_SEX>" + _healthCardLisCheckReportInfo.PATIENT_SEX + "</PATIENT_SEX>");
            builder.Append("<PATIENT_AGE>" + _healthCardLisCheckReportInfo.PATIENT_AGE + "</PATIENT_AGE>");
            builder.Append("<VISIT_NUMBER>" + _healthCardLisCheckReportInfo.VISIT_NUMBER + "</VISIT_NUMBER>");
            builder.Append("<MEDICAL_INSURANNCE_TYPE>" + _healthCardLisCheckReportInfo.MEDICAL_INSURANNCE_TYPE + "</MEDICAL_INSURANNCE_TYPE>");

            if (_healthCardLisCheckReportInfo.REPORT_INFO != null &&
               _healthCardLisCheckReportInfo.REPORT_INFO.Count > 0)
            {
                builder.Append("<REPORT_LIST>");
                foreach (var reportInfo in _healthCardLisCheckReportInfo.REPORT_INFO)
                {

                    builder.Append("<REPORT_INFO>");
                    builder.Append("<REPORT_ID>" + reportInfo.REPORT_ID + "</REPORT_ID>");
                    builder.Append("<DIAGNOSIS>" + reportInfo.DIAGNOSIS + "</DIAGNOSIS>");
                    builder.Append("<ITEM_NAME>" + reportInfo.ITEM_NAME + "</ITEM_NAME>");
                    builder.Append("<SPECIMEN_NAME>" + reportInfo.SPECIMEN_NAME + "</SPECIMEN_NAME>");
                    builder.Append("<SPECIMEN_ID>" + reportInfo.SPECIMEN_ID + "</SPECIMEN_ID>");
                    builder.Append("<REPORT_TIME>" + reportInfo.REPORT_TIME + "</REPORT_TIME>");
                    builder.Append("<DEPT_NAME>" + reportInfo.DEPT_NAME + "</DEPT_NAME>");
                    builder.Append("<DOCTOR_NAME>" + reportInfo.DOCTOR_NAME + "</DOCTOR_NAME>");
                    builder.Append("<REPORT_TYPE>" + reportInfo.REPORT_TYPE + "</REPORT_TYPE>");
                    builder.Append("<REMARK>" + reportInfo.REMARK + "</REMARK>");
                    builder.Append("</REPORT_INFO>");

                }
                builder.Append("</REPORT_LIST>");
            }

            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqNoramlLisCheckInfo(string xmlInfo, ref HealthCardNormalCheckReport_InParam _healthCardNormalCheckReportInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardNormalCheckReportInfo.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardNormalCheckReportInfo.HOSP_PATIENT_ID = root.SelectSingleNode("REQ/HOSP_PATIENT_ID").InnerText;
            _healthCardNormalCheckReportInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardNormalCheckReportInfo.REPORT_ID = root.SelectSingleNode("REQ/REPORT_ID").InnerText;
        }

        public static string AnalysisXmlResLNormalLisCheckInfo(HealthCardNormalCheckReport_OutParam _healthCardNormalCheckReportInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<HOS_ID>" + _healthCardNormalCheckReportInfo.HOS_ID + "</HOS_ID>");

            if (_healthCardNormalCheckReportInfo.REPORT_INFO != null &&
               _healthCardNormalCheckReportInfo.REPORT_INFO.Count > 0)
            {
                foreach (var reportInfo in _healthCardNormalCheckReportInfo.REPORT_INFO)
                {
                    builder.Append("<REPORT_INFO>");
                    builder.Append("<HEALTH_CARD_ID>" + reportInfo.HEALTH_CARD_ID + "</HEALTH_CARD_ID>");
                    builder.Append("<HOSP_PATIENT_ID>" + reportInfo.HOSP_PATIENT_ID + "</HOSP_PATIENT_ID>");
                    builder.Append("<PATIENT_IDCARD_TYPE>" + reportInfo.PATIENT_IDCARD_TYPE + "</PATIENT_IDCARD_TYPE>");
                    builder.Append("<PATIENT_IDCARD_NO>" + reportInfo.PATIENT_IDCARD_NO + "</PATIENT_IDCARD_NO>");
                    builder.Append("<PATIENT_CARD_TYPE>" + reportInfo.PATIENT_CARD_TYPE + "</PATIENT_CARD_TYPE>");
                    builder.Append("<PATIENT_CARD_NO>" + reportInfo.PATIENT_CARD_NO + "</PATIENT_CARD_NO>");
                    builder.Append("<PATIENT_NAME>" + reportInfo.PATIENT_NAME + "</PATIENT_NAME>");
                    builder.Append("<PATIENT_SEX>" + reportInfo.PATIENT_SEX + "</PATIENT_SEX>");
                    builder.Append("<PATIENT_AGE>" + reportInfo.PATIENT_AGE + "</PATIENT_AGE>");
                    builder.Append("<VISIT_NUMBER>" + reportInfo.VISIT_NUMBER + "</VISIT_NUMBER>");
                    builder.Append("<MEDICAL_INSURANNCE_TYPE>" + reportInfo.MEDICAL_INSURANNCE_TYPE + "</MEDICAL_INSURANNCE_TYPE>");
                    builder.Append("<DIAGNOSIS>" + reportInfo.DIAGNOSIS + "</DIAGNOSIS>");
                    builder.Append("<ITEM_NAME>" + reportInfo.ITEM_NAME + "</ITEM_NAME>");
                    builder.Append("<SPECIMEN_NAME>" + reportInfo.SPECIMEN_NAME + "</SPECIMEN_NAME>");
                    builder.Append("<SPECIMEN_ID>" + reportInfo.SPECIMEN_ID + "</SPECIMEN_ID>");
                    builder.Append("<REPORT_TIME>" + reportInfo.REPORT_TIME + "</REPORT_TIME>");
                    builder.Append("<DEPT_NAME>" + reportInfo.DEPT_NAME + "</DEPT_NAME>");
                    builder.Append("<DOCTOR_NAME>" + reportInfo.DOCTOR_NAME + "</DOCTOR_NAME>");
                    builder.Append("<REVIEW_NAME>" + reportInfo.REVIEW_NAME + "</REVIEW_NAME>");
                    builder.Append("<REVIEW_TIME>" + reportInfo.REVIEW_TIME + "</REVIEW_TIME>");
                    builder.Append("<REMARK>" + reportInfo.REMARK + "</REMARK>");

                    if (reportInfo.DETAIL != null &&
                   reportInfo.DETAIL.Count > 0)
                    {
                        builder.Append("<CHECK_LIST>");
                        foreach (var itemInfo in reportInfo.DETAIL)
                        {

                            builder.Append("<DETAIL>");
                            builder.Append("<CHECK_NAME>" + itemInfo.CHECK_NAME + "</CHECK_NAME>");
                            builder.Append("<RESULT>" + itemInfo.RESULT + "</RESULT>");
                            builder.Append("<UNIT>" + itemInfo.UNIT + "</UNIT>");
                            builder.Append("<NORMAL_FLAG>" + itemInfo.NORMAL_FLAG + "</NORMAL_FLAG>");
                            builder.Append("<REFERENCE_VALUE>" + itemInfo.REFERENCE_VALUE + "</REFERENCE_VALUE>");
                            builder.Append("<DESC>" + itemInfo.DESC + "</DESC>");
                            builder.Append("</DETAIL>");
                        }
                        builder.Append("</CHECK_LIST>");
                    }
                    builder.Append("</REPORT_INFO>");
                }
            }

            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqDrugReportInfo(string xmlInfo, ref HealthCardDrugReportInfo_InParam _healthCardDrugReportInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardDrugReportInfo.HOSP_PATIENT_ID = root.SelectSingleNode("REQ/HOSP_PATIENT_ID").InnerText;
            _healthCardDrugReportInfo.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardDrugReportInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardDrugReportInfo.REPORT_ID = root.SelectSingleNode("REQ/REPORT_ID").InnerText;
        }

        public static string AnalysisXmlResDrugReportInfo(HealthCardDrugReportInfo_OutParam _healthCardDrugReportInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<HOS_ID>" + _healthCardDrugReportInfo.HOS_ID + "</HOS_ID>");

            if (_healthCardDrugReportInfo.REPORT_INFO != null &&
               _healthCardDrugReportInfo.REPORT_INFO.Count > 0)
            {
                foreach (var reportInfo in _healthCardDrugReportInfo.REPORT_INFO)
                {
                    builder.Append("<REPORT_INFO>");
                    builder.Append("<HEALTH_CARD_ID>" + reportInfo.HEALTH_CARD_ID + "</HEALTH_CARD_ID>");
                    builder.Append("<HOSP_PATIENT_ID>" + reportInfo.HOSP_PATIENT_ID + "</HOSP_PATIENT_ID>");
                    builder.Append("<PATIENT_IDCARD_TYPE>" + reportInfo.PATIENT_IDCARD_TYPE + "</PATIENT_IDCARD_TYPE>");
                    builder.Append("<PATIENT_IDCARD_NO>" + reportInfo.PATIENT_IDCARD_NO + "</PATIENT_IDCARD_NO>");
                    builder.Append("<PATIENT_CARD_TYPE>" + reportInfo.PATIENT_CARD_TYPE + "</PATIENT_CARD_TYPE>");
                    builder.Append("<PATIENT_CARD_NO>" + reportInfo.PATIENT_CARD_NO + "</PATIENT_CARD_NO>");
                    builder.Append("<PATIENT_NAME>" + reportInfo.PATIENT_NAME + "</PATIENT_NAME>");
                    builder.Append("<PATIENT_SEX>" + reportInfo.PATIENT_SEX + "</PATIENT_SEX>");
                    builder.Append("<PATIENT_AGE>" + reportInfo.PATIENT_AGE + "</PATIENT_AGE>");
                    builder.Append("<VISIT_NUMBER>" + reportInfo.VISIT_NUMBER + "</VISIT_NUMBER>");
                    builder.Append("<MEDICAL_INSURANNCE_TYPE>" + reportInfo.MEDICAL_INSURANNCE_TYPE + "</MEDICAL_INSURANNCE_TYPE>");
                    builder.Append("<DIAGNOSIS>" + reportInfo.DIAGNOSIS + "</DIAGNOSIS>");
                    builder.Append("<ITEM_NAME>" + reportInfo.ITEM_NAME + "</ITEM_NAME>");
                    builder.Append("<SPECIMEN_NAME>" + reportInfo.SPECIMEN_NAME + "</SPECIMEN_NAME>");
                    builder.Append("<SPECIMEN_ID>" + reportInfo.SPECIMEN_ID + "</SPECIMEN_ID>");
                    builder.Append("<REPORT_TIME>" + reportInfo.REPORT_TIME + "</REPORT_TIME>");
                    builder.Append("<DEPT_NAME>" + reportInfo.DEPT_NAME + "</DEPT_NAME>");
                    builder.Append("<DOCTOR_NAME>" + reportInfo.DOCTOR_NAME + "</DOCTOR_NAME>");
                    builder.Append("<REVIEW_NAME>" + reportInfo.REVIEW_NAME + "</REVIEW_NAME>");
                    builder.Append("<REVIEW_TIME>" + reportInfo.REVIEW_TIME + "</REVIEW_TIME>");
                    builder.Append("<REMARK>" + reportInfo.REMARK + "</REMARK>");

                    if (reportInfo.REPORT_DETAIL != null &&
                   reportInfo.REPORT_DETAIL.Count > 0)
                    {
                        foreach (var detail in reportInfo.REPORT_DETAIL)
                        {
                            builder.Append("<CHECK_LIST>");
                            builder.Append("<CHECK_NAME>" + detail.CHECK_NAME + "</CHECK_NAME>");
                            builder.Append("<DRUG_LIST>");
                            foreach (var drugInfo in detail.DRUG_INFO)
                            {
                                builder.Append("<DRUG_INFO>");
                                builder.Append("<DRUG_NAME>" + drugInfo.DRUG_NAME + "</DRUG_NAME>");
                                builder.Append("<DRUG_ENGLIST_NAME>" + drugInfo.DRUG_ENGLIST_NAME + "</DRUG_ENGLIST_NAME>");
                                builder.Append("<MIC>" + drugInfo.MIC + "</MIC>");
                                builder.Append("<SENSITIVITY>" + drugInfo.SENSITIVITY + "</SENSITIVITY>");
                                builder.Append("<DESC>" + drugInfo.DESC + "</DESC>");
                                builder.Append("</DRUG_INFO>");
                            }
                            builder.Append("</DRUG_LIST>");
                            builder.Append("</CHECK_LIST>");
                        }
                    }

                    builder.Append("</REPORT_INFO>");
                }
            }


            builder.Append("</RES>");
            return builder.ToString();
        }

        public static void AnalysisXmlReqPacsReportInfo(string xmlInfo, ref HealthCardPacsCheckReport_InParam _healthCardPacsReportInfo)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlInfo);
            _healthCardPacsReportInfo.HOSP_PATIENT_ID = root.SelectSingleNode("REQ/HOSP_PATIENT_ID").InnerText;
            _healthCardPacsReportInfo.HEALTH_CARD_ID = root.SelectSingleNode("REQ/HEALTH_CARD_ID").InnerText;
            _healthCardPacsReportInfo.HOS_ID = root.SelectSingleNode("REQ/HOS_ID").InnerText;
            _healthCardPacsReportInfo.REPORT_ID = root.SelectSingleNode("REQ/REPORT_ID").InnerText;
        }

        public static string AnalysisXmlResPacsReportInfo(HealthCardPacsCheckReport_OutParam _healthCardPacsReportInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<RES>");
            builder.Append("<HOS_ID>" + _healthCardPacsReportInfo.HOS_ID + "</HOS_ID>");
            builder.Append("<HEALTH_CARD_ID>" + _healthCardPacsReportInfo.HEALTH_CARD_ID + "</HEALTH_CARD_ID>");
            builder.Append("<PATIENT_IDCARD_TYPE>" + _healthCardPacsReportInfo.PATIENT_IDCARD_TYPE + "</PATIENT_IDCARD_TYPE>");
            builder.Append("<PATIENT_IDCARD_NO>" + _healthCardPacsReportInfo.PATIENT_IDCARD_NO + "</PATIENT_IDCARD_NO>");

            builder.Append("<PATIENT_CARD_TYPE>" + _healthCardPacsReportInfo.PATIENT_CARD_TYPE + "</PATIENT_CARD_TYPE>");
            builder.Append("<PATIENT_CARD_NO>" + _healthCardPacsReportInfo.PATIENT_CARD_NO + "</PATIENT_CARD_NO>");
            builder.Append("<PATIENT_NAME>" + _healthCardPacsReportInfo.PATIENT_NAME + "</PATIENT_NAME>");
            builder.Append("<PATIENT_SEX>" + _healthCardPacsReportInfo.PATIENT_SEX + "</PATIENT_SEX>");
            builder.Append("<PATIENT_AGE>" + _healthCardPacsReportInfo.PATIENT_AGE + "</PATIENT_AGE>");
            builder.Append("<VISIT_NUMBER>" + _healthCardPacsReportInfo.VISIT_NUMBER + "</VISIT_NUMBER>");
            builder.Append("<MEDICAL_INSURANNCE_TYPE>" + _healthCardPacsReportInfo.MEDICAL_INSURANNCE_TYPE + "</MEDICAL_INSURANNCE_TYPE>");
            builder.Append("<SPECIMEN_NAME>" + _healthCardPacsReportInfo.SPECIMEN_NAME + "</SPECIMEN_NAME>");
            builder.Append("<SPECIMEN_ID>" + _healthCardPacsReportInfo.SPECIMEN_ID + "</SPECIMEN_ID>");
            builder.Append("<ITEM_NAME>" + _healthCardPacsReportInfo.ITEM_NAME + "</ITEM_NAME>");
            builder.Append("<COMPLAINT>" + _healthCardPacsReportInfo.COMPLAINT + "</COMPLAINT>");
            builder.Append("<DIAGNOSIS>" + _healthCardPacsReportInfo.DIAGNOSIS + "</DIAGNOSIS>");
            builder.Append("<SEEN>" + _healthCardPacsReportInfo.SEEN + "</SEEN>");
            builder.Append("<CONTENT>" + _healthCardPacsReportInfo.CONTENT + "</CONTENT>");
            builder.Append("<REPORT_TIME>" + _healthCardPacsReportInfo.REPORT_TIME + "</REPORT_TIME>");
            builder.Append("<DEPT_NAME>" + _healthCardPacsReportInfo.DEPT_NAME + "</DEPT_NAME>");
            builder.Append("<DOCTOR_NAME>" + _healthCardPacsReportInfo.DOCTOR_NAME + "</DOCTOR_NAME>");
            builder.Append("<REVIEW_NAME>" + _healthCardPacsReportInfo.REVIEW_NAME + "</REVIEW_NAME>");
            builder.Append("<REVIEW_TIME>" + _healthCardPacsReportInfo.REVIEW_TIME + "</REVIEW_TIME>");
            builder.Append("<REMARK>" + _healthCardPacsReportInfo.REMARK + "</REMARK>");
            builder.Append("</RES>");
            return builder.ToString();
        }
        #region 私有方法
        /// <summary>
        /// 计算具体某个日期是星期几
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="d">日</param>
        /// <returns></returns>
        public string CaculateWeekDay(int y, int m, int d)
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
        #endregion


        public static string DataTableToXml(DataTable dt, string str_根节点)
        {
            if (dt.Rows.Count <= 0)
            {
                return "";
            }
            StringBuilder str_xml = new StringBuilder();


            str_xml.Append("<RES>");
            str_xml.Append("<HOS_ID>" + "522633020000001" + "</HOS_ID>");
            
            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(str_根节点))
                {
                    str_xml.Append("<" + str_根节点 + ">");
                }
                foreach (DataColumn col in dt.Columns)
                {
                    str_xml.Append("<" + col.ColumnName + ">" + row[col.ColumnName] + "</" + col.ColumnName + ">");
                }
                if (!string.IsNullOrEmpty(str_根节点))
                {
                    str_xml.Append("</" + str_根节点 + ">");
                }
            }

            return str_xml.ToString();
        }
    }
}
