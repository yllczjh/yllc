using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HisCommon.DataEntity;
using HisCommon;
using System.Data.Common;

namespace HisDBLayer
{
    public class OneCard
    {
        #region 初始化数据库
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InitDataBase(ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select sysdate from dual";
            int revString = 0;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                revString = dt.Rows.Count;
                if (revString == 0)
                    return -1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return revString;
        }
        #endregion

        #region 获取序号序列
        /// <summary>
        /// 获取序号序列
        /// </summary>
        /// <param name="Serial"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetPersonInfoSerial(ref string Serial, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select OneCardserial.nextval from dual";
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                Serial = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取最大卡状态序号
        /// <summary>
        /// 获取最大卡状态序号
        /// </summary>
        /// <param name="Serial"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetCardStateSerial(BaseEntityer db, string CardID, ref string Serial, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select nvl(max(t.card_serial),0) + 1 from ONECARD_STATELOG t where t.card_id='{0}'";
            try
            {
                sql = string.Format(sql, CardID);
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                Serial = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取交易流水序列
        /// <summary>
        /// 获取交易流水序列
        /// </summary>
        /// <param name="Bussiness"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetBussinessNoSerial(ref string Bussiness, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select BUSSINESSNO.nextval from dual";
            string ness = string.Empty;
            string date = string.Empty;
            try
            {
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                ness = dt.Rows[0][0].ToString();

                sql = "select sysdate from dual";
                dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                date = dt.Rows[0][0].ToString();

                Bussiness = DateTime.Parse(date).ToString("yyyyMMdd") + ness.PadLeft(12, '0');
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取卡号
        /// <summary>
        /// 获取卡号
        /// </summary>
        /// <param name="Bussiness"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetCardID(BaseEntityer db, int serial, ref decimal cardId, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select t.card_id from ONECARD_PATIENTINFO t where t.serial_no='{0}'";
            string ness = string.Empty;
            try
            {
                sql = string.Format(sql, serial);
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                    return -1;
                if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) == true)
                {
                    cardId = 0;
                }
                else
                {
                    cardId = decimal.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 插入开卡信息表
        /// <summary>
        /// 插入开卡信息表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="master"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertRegisterCard(BaseEntityer db, HisCommon.DataEntity.OneCard_PatientInfo info, ref string errMsg)
        {
            int IsSuccess = 0;
            string sql = @"insert into ONECARD_PATIENTINFO
                                      (CARD_ID,
                                       CARD_SERIAL,
                                       SERIAL_NO,
                                       NAME,
                                       PASSWORD,
                                       BIRTHDAY,
                                       AGE,
                                       SEX,
                                       ID_CARD,
                                       HOME_ADD,
                                       LINK_PERSON,
                                       PHONE,
                                       OUT_PATIENTID,
                                       IN_PATIENTID,
                                       ACC_OUTBALANCE,
                                       ACC_INBALANCE,
                                       CARD_TYPE,
                                       CARD_STATE,
                                       YH_CARDNO,
                                       HIS_BUSSINESSNO,
                                       BANK_BUSSINESSNO,
                                       OPER_ID,
                                       OPER_NAME,
                                       OPER_DATE,
                                       cash_pledge)
                                    values
                                      ('{22}',--(select nvl(max(card_id),1000000000) + 1 from ONECARD_PATIENTINFO),--CARD_ID
                                       1, 
                                       '{0}',--SERIAL_NO
                                       '{1}',--NAME
                                       '{2}',--PASSWORD
                                       to_date('{3}','yyyy-mm-dd hh24:mi:ss'),--BIRTHDAY
                                       '{4}',--AGE
                                       '{5}',--SEX
                                       '{6}',--ID_CARD
                                       '{7}',--HOME_ADD
                                       '{8}',--LINK_PERSON
                                       '{9}',--PHONE
                                       '{10}',--OUT_PATIENTID
                                       '{11}',--IN_PATIENTID
                                       '{12}',--ACC_OUTBALANCE
                                       '{13}',--ACC_INBALANCE
                                       '{14}',--CARD_TYPE
                                       '{15}',--CARD_STATE
                                       '{16}',--YH_CARDNO
                                       '{17}',--HIS_BUSSINESSNO
                                       '{18}',--BANK_BUSSINESSNO
                                       '{19}',--OPER_ID
                                       '{20}',--OPER_NAME
                                       to_date('{21}','yyyy-mm-dd hh24:mi:ss'),--OPER_DATE
                                       {23}--cash_pledge
                                        )
                                    ";
            try
            {
                sql = string.Format(sql, info.Serial,//0
                                  info.Name,//1
                                  info.Password,//2
                                  info.Birthday,//3
                                  info.Age,//4
                                  info.Sex,//5
                                  info.Id_card,//6
                                  info.Home_add,//7
                                  info.Link_person,//8
                                  info.Phone,//9
                                  info.Out_patientid,//10
                                  info.In_patientid,//11
                                  info.Acc_outbalance,//12
                                  info.Acc_inbalance,//13
                                  info.Card_type,//14
                                  info.Card_state,//15
                                  info.Yh_CardNo,//16
                                  info.His_bussinessno,//17
                                  info.Bank_bussinessno,//18
                                  info.Oper_id,//19
                                  info.Oper_name,//20
                                  info.Oper_date,//21
                                  info.Card_id,//22
                                  info.Cash_pledge//23
                                  );
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message + "^" + sql;
                return -1;
            }
            return IsSuccess;
        }
        #endregion

        #region 插入开卡信息表
        /// <summary>
        /// 插入开卡信息表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="master"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertRegisterCardOther(BaseEntityer db, HisCommon.DataEntity.OneCard_PatientInfo info, ref string errMsg)
        {
            int IsSuccess = 0;
            string sql = @"insert into ONECARD_PATIENTINFO
                                      (CARD_ID,
                                       CARD_SERIAL,
                                       SERIAL_NO,
                                       NAME,
                                       PASSWORD,
                                       BIRTHDAY,
                                       AGE,
                                       SEX,
                                       ID_CARD,
                                       HOME_ADD,
                                       LINK_PERSON,
                                       PHONE,
                                       OUT_PATIENTID,
                                       IN_PATIENTID,
                                       ACC_OUTBALANCE,
                                       ACC_INBALANCE,
                                       CARD_TYPE,
                                       CARD_STATE,
                                       YH_CARDNO,
                                       HIS_BUSSINESSNO,
                                       BANK_BUSSINESSNO,
                                       OPER_ID,
                                       OPER_NAME,
                                       OPER_DATE)
                                    values
                                      ('{0}',--CARD_ID
                                       (select nvl(max(CARD_SERIAL),0) + 1 from ONECARD_PATIENTINFO),  --CARD_SERIAL
                                       '{1}',--SERIAL_NO
                                       '{2}',--NAME
                                       '{3}',--PASSWORD
                                       to_date('{4}','yyyy-mm-dd hh24:mi:ss'),--BIRTHDAY
                                       '{5}',--AGE
                                       '{6}',--SEX
                                       '{7}',--ID_CARD
                                       '{8}',--HOME_ADD
                                       '{9}',--LINK_PERSON
                                       '{10}',--PHONE
                                       '{11}',--OUT_PATIENTID
                                       '{12}',--IN_PATIENTID
                                       '{13}',--ACC_OUTBALANCE
                                       '{14}',--ACC_INBALANCE
                                       '{15}',--CARD_TYPE
                                       '{16}',--CARD_STATE
                                       '{17}',--YH_CARDNO
                                       '{18}',--HIS_BUSSINESSNO
                                       '{19}',--BANK_BUSSINESSNO
                                       '{20}',--OPER_ID
                                       '{21}',--OPER_NAME
                                       to_date('{22}','yyyy-mm-dd hh24:mi:ss'))--OPER_DATE
                                    ";
            try
            {
                sql = string.Format(sql, info.Card_id, info.Serial,//0
                                  info.Name,//1
                                  info.Password,//2
                                  info.Birthday,//3
                                  info.Age,//4
                                  info.Sex,//5
                                  info.Id_card,//6
                                  info.Home_add,//7
                                  info.Link_person,//8
                                  info.Phone,//9
                                  info.Out_patientid,//10
                                  info.In_patientid,//11
                                  info.Acc_outbalance,//12
                                  info.Acc_inbalance,//13
                                  info.Card_type,//14
                                  info.Card_state,//15
                                  info.Yh_CardNo,//16
                                  info.His_bussinessno,//17
                                  info.Bank_bussinessno,//18
                                  info.Oper_id,//19
                                  info.Oper_name,//20
                                  info.Oper_date);//21
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message + "^" + sql;
                return -1;
            }
            return IsSuccess;
        }
        #endregion

        #region 插入卡状态日志表
        /// <summary>
        /// 插入卡状态日志表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="log"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertCardState(BaseEntityer db, HisCommon.DataEntity.ONECARD_STATELOG log, ref string errMsg)
        {
            int IsSuccess = 0;

            string sql = @"insert into ONECARD_STATELOG
                              (CARD_ID, CARD_SERIAL ,SERIAL, CARD_STATE, OPER_ID, OPER_NAME, OPER_DATE, HIS_BUSSINESSNO, BANK_BUSSINESSNO,BANK_NO)
                            values
                              ('{0}',
                               '{1}',
                               '{2}',
                               '{3}',
                               '{4}',
                               '{5}',
                               to_date('{6}', 'yyyy-mm-dd hh24:mi:ss'),
                               '{7}',
                               '{8}',
                               '{9}')
                                    ";
            try
            {
                sql = string.Format(sql, log.CARD_ID, log.CARD_SERIAL,//0
                                  log.SERIAL,//1
                                  log.CARD_STATE,//2
                                  log.OPER_ID,//3
                                  log.OPER_NAME,//4
                                  log.OPER_DATE,//5
                                  log.HIS_BUSSINESSNO,//6
                                  log.BANK_BUSSINESSNO,//7
                                  log.BANK_NO
                                  );
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message + "^" + sql;
                return -1;
            }
            return IsSuccess;
        }
        #endregion

        #region 获取患者信息
        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cardId"></param>
        /// <param name="info"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetPatientInfo(BaseEntityer db, decimal cardId, ref OneCard_PatientInfo info, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select CARD_ID,
                                   SERIAL_NO,
                                   NAME,
                                   PASSWORD,
                                   BIRTHDAY,
                                   AGE,
                                   SEX,
                                   ID_CARD,
                                   HOME_ADD,
                                   LINK_PERSON,
                                   PHONE,
                                   OUT_PATIENTID,
                                   IN_PATIENTID,
                                   ACC_OUTBALANCE,
                                   ACC_INBALANCE,
                                   CARD_TYPE,
                                   CARD_STATE,
                                   YH_CARDNO,
                                   HIS_BUSSINESSNO,
                                   BANK_BUSSINESSNO,
                                   OPER_ID,
                                   OPER_NAME,
                                   OPER_DATE,
                                   CARD_SERIAL
                              from ONECARD_PATIENTINFO t
                              where t.card_id='{0}'
                            ";
            string ness = string.Empty;
            try
            {
                sql = string.Format(sql, cardId);
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "没有查询到患者信息。";
                    return -1;
                }

                info = new OneCard_PatientInfo();
                info.Card_id = decimal.Parse(dt.Rows[0][0].ToString());
                info.Serial = int.Parse(dt.Rows[0][1].ToString());
                info.Name = dt.Rows[0][2].ToString();
                info.Password = dt.Rows[0][3].ToString();
                info.Birthday = DateTime.Parse(dt.Rows[0][4].ToString());
                info.Age = int.Parse(dt.Rows[0][5].ToString());
                info.Sex = dt.Rows[0][6].ToString();
                info.Id_card = dt.Rows[0][7].ToString();
                info.Home_add = dt.Rows[0][8].ToString();
                info.Link_person = dt.Rows[0][9].ToString();
                info.Phone = dt.Rows[0][10].ToString();
                info.Out_patientid = dt.Rows[0][11].ToString();
                info.In_patientid = dt.Rows[0][12].ToString();
                info.Acc_outbalance = decimal.Parse(dt.Rows[0][13].ToString());
                info.Acc_inbalance = decimal.Parse(dt.Rows[0][14].ToString());
                info.Card_type = int.Parse(dt.Rows[0][15].ToString());
                info.Card_state = dt.Rows[0][16].ToString();
                info.Yh_CardNo = dt.Rows[0][17].ToString();
                info.His_bussinessno = dt.Rows[0][18].ToString();
                info.Bank_bussinessno = dt.Rows[0][19].ToString();
                info.Oper_id = dt.Rows[0][20].ToString();
                info.Oper_name = dt.Rows[0][21].ToString();
                info.Oper_date = DateTime.Parse(dt.Rows[0][22].ToString());
                info.Card_Serial = int.Parse(dt.Rows[0][23].ToString());
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取患者密码
        /// <summary>
        /// 获取患者密码
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cardId"></param>
        /// <param name="password"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetPatientPassword(BaseEntityer db, decimal cardId, ref string password, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select t.password from ONECARD_PATIENTINFO t where t.card_id='{0}' and t.card_state in ('1','2')";
            string ness = string.Empty;
            try
            {
                sql = string.Format(sql, cardId);
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "没有查询到患者信息。";
                    return -1;
                }
                password = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取患者信息（通过身份证）
        /// <summary>
        /// 获取患者信息（通过身份证）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="Idcard"></param>
        /// <param name="Card_Type"
        /// <param name="infoList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetPatientInfoByIDCARD(BaseEntityer db, string Idcard, string Card_Type, ref List<OneCard_PatientInfo> infoList, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select CARD_ID,                                   
                                   SERIAL_NO,
                                   NAME,
                                   PASSWORD,
                                   BIRTHDAY,
                                   AGE,
                                   SEX,
                                   ID_CARD,
                                   HOME_ADD,
                                   LINK_PERSON,
                                   PHONE,
                                   OUT_PATIENTID,
                                   IN_PATIENTID,
                                   ACC_OUTBALANCE,
                                   ACC_INBALANCE,
                                   CARD_TYPE,
                                   CARD_STATE,
                                   YH_CARDNO,
                                   HIS_BUSSINESSNO,
                                   BANK_BUSSINESSNO,
                                   OPER_ID,
                                   OPER_NAME,
                                   OPER_DATE,
                                    CARD_SERIAL
                              from ONECARD_PATIENTINFO t
                              where t.ID_CARD='{0}' and (t.CARD_TYPE = '{1}' or 'ALL' = '{1}')
                            ";
            string ness = string.Empty;
            try
            {
                sql = string.Format(sql, Idcard, Card_Type);
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "没有查询到患者信息。";
                    return -1;
                }
                infoList = new List<OneCard_PatientInfo>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    OneCard_PatientInfo info = new OneCard_PatientInfo();
                    info.Card_id = decimal.Parse(dt.Rows[i][0].ToString());
                    info.Serial = int.Parse(dt.Rows[i][1].ToString());
                    info.Name = dt.Rows[i][2].ToString();
                    info.Password = dt.Rows[i][3].ToString();
                    info.Birthday = DateTime.Parse(dt.Rows[i][4].ToString());
                    info.Age = int.Parse(dt.Rows[i][5].ToString());
                    info.Sex = dt.Rows[i][6].ToString();
                    info.Id_card = dt.Rows[i][7].ToString();
                    info.Home_add = dt.Rows[i][8].ToString();
                    info.Link_person = dt.Rows[i][9].ToString();
                    info.Phone = dt.Rows[i][10].ToString();
                    info.Out_patientid = dt.Rows[i][11].ToString();
                    info.In_patientid = dt.Rows[i][12].ToString();
                    info.Acc_outbalance = decimal.Parse(dt.Rows[i][13].ToString());
                    info.Acc_inbalance = decimal.Parse(dt.Rows[i][14].ToString());
                    info.Card_type = int.Parse(dt.Rows[i][15].ToString());
                    info.Card_state = dt.Rows[i][16].ToString();
                    info.Yh_CardNo = dt.Rows[i][17].ToString();
                    info.His_bussinessno = dt.Rows[i][18].ToString();
                    info.Bank_bussinessno = dt.Rows[i][19].ToString();
                    info.Oper_id = dt.Rows[i][20].ToString();
                    info.Oper_name = dt.Rows[i][21].ToString();
                    info.Oper_date = DateTime.Parse(dt.Rows[i][22].ToString());
                    info.Card_Serial = int.Parse(dt.Rows[i][23].ToString());
                    infoList.Add(info);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取患者信息（通过银行卡号）
        /// <summary>
        /// 获取患者信息（通过银行卡号）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="YH_card"></param>
        /// <param name="Card_Type"></param>
        /// <param name="infoList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetPatientInfoByYHCARD(BaseEntityer db, string YH_card, string Card_Type, ref List<OneCard_PatientInfo> infoList, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select CARD_ID,
                                   SERIAL_NO,
                                   NAME,
                                   PASSWORD,
                                   BIRTHDAY,
                                   AGE,
                                   SEX,
                                   ID_CARD,
                                   HOME_ADD,
                                   LINK_PERSON,
                                   PHONE,
                                   OUT_PATIENTID,
                                   IN_PATIENTID,
                                   ACC_OUTBALANCE,
                                   ACC_INBALANCE,
                                   CARD_TYPE,
                                   CARD_STATE,
                                   YH_CARDNO,
                                   HIS_BUSSINESSNO,
                                   BANK_BUSSINESSNO,
                                   OPER_ID,
                                   OPER_NAME,
                                   OPER_DATE,
                                   CARD_SERIAL
                              from ONECARD_PATIENTINFO t
                              where t.YH_CARDNO='{0}' and (t.CARD_TYPE = '{1}' or 'ALL' = '{0}')
                            ";
            string ness = string.Empty;
            try
            {
                sql = string.Format(sql, YH_card, Card_Type);
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "没有查询到患者信息。";
                    return -1;
                }
                infoList = new List<OneCard_PatientInfo>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    OneCard_PatientInfo info = new OneCard_PatientInfo();
                    info.Card_id = decimal.Parse(dt.Rows[i][0].ToString());
                    info.Serial = int.Parse(dt.Rows[i][1].ToString());
                    info.Name = dt.Rows[i][2].ToString();
                    info.Password = dt.Rows[i][3].ToString();
                    info.Birthday = DateTime.Parse(dt.Rows[i][4].ToString());
                    info.Age = int.Parse(dt.Rows[i][5].ToString());
                    info.Sex = dt.Rows[i][6].ToString();
                    info.Id_card = dt.Rows[i][7].ToString();
                    info.Home_add = dt.Rows[i][8].ToString();
                    info.Link_person = dt.Rows[i][9].ToString();
                    info.Phone = dt.Rows[i][10].ToString();
                    info.Out_patientid = dt.Rows[i][11].ToString();
                    info.In_patientid = dt.Rows[i][12].ToString();
                    info.Acc_outbalance = decimal.Parse(dt.Rows[i][13].ToString());
                    info.Acc_inbalance = decimal.Parse(dt.Rows[i][14].ToString());
                    info.Card_type = int.Parse(dt.Rows[i][15].ToString());
                    info.Card_state = dt.Rows[i][16].ToString();
                    info.Yh_CardNo = dt.Rows[i][17].ToString();
                    info.His_bussinessno = dt.Rows[i][18].ToString();
                    info.Bank_bussinessno = dt.Rows[i][19].ToString();
                    info.Oper_id = dt.Rows[i][20].ToString();
                    info.Oper_name = dt.Rows[i][21].ToString();
                    info.Oper_date = DateTime.Parse(dt.Rows[i][22].ToString());
                    info.Card_Serial = int.Parse(dt.Rows[i][23].ToString());
                    infoList.Add(info);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 修改患者信息
        /// <summary>
        /// 2013-11-22 by li 更新一卡通患者信息
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="card_Info">一卡通患者信息</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns>1、成功 -1、异常</returns>
        public int UpdateOneCardPatientInfo(BaseEntityer db, OneCard_PatientInfo card_Info, ref string pErrMsg)
        {
            int revInt = 0;
            try
            {
                string sql = @"UPDATE ONECARD_PATIENTINFO
                                   SET SERIAL_NO        = {1},
                                       NAME             = '{2}',
                                       PASSWORD         = '{3}',
                                       BIRTHDAY         = to_date('{4}', 'yyyy-mm-dd hh24:mi:ss'),
                                       AGE              = {5},
                                       SEX              = '{6}',
                                       ID_CARD          = '{7}',
                                       HOME_ADD         = '{8}',
                                       LINK_PERSON      = '{9}',
                                       PHONE            = '{10}',
                                       OUT_PATIENTID    = '{11}',
                                       IN_PATIENTID     = '{12}',
                                       ACC_OUTBALANCE   = {13},
                                       ACC_INBALANCE    = {14},
                                       CARD_TYPE        = '{15}',
                                       CARD_STATE       = '{16}',
                                       YH_CARDNO        = '{17}',
                                       HIS_BUSSINESSNO  = '{18}',
                                       BANK_BUSSINESSNO = '{19}',
                                       OPER_ID          = '{20}',
                                       OPER_NAME        = '{21}',
                                       OPER_DATE        = to_date('{22}', 'yyyy-mm-dd hh24:mi:ss')
                                 WHERE CARD_ID = {0}";
                object[] param = new object[] { card_Info.Card_id, card_Info.Serial, card_Info.Name, 
                    card_Info.Password, card_Info.Birthday, card_Info.Age, card_Info.Sex, card_Info.Id_card, 
                    card_Info.Home_add, card_Info.Link_person, card_Info.Phone, card_Info.Out_patientid, 
                    card_Info.In_patientid, card_Info.Acc_outbalance, card_Info.Acc_inbalance, 
                    card_Info.Card_type, card_Info.Card_state, card_Info.Yh_CardNo, card_Info.His_bussinessno, 
                    card_Info.Bank_bussinessno, card_Info.Oper_id, card_Info.Oper_name, card_Info.Oper_date };
                sql = Utility.SqlFormate(sql, param);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }
        #endregion

        #region 插入患者信息日志表
        /// <summary>
        /// 插入患者信息日志表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="info"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertRegisterCardLog(BaseEntityer db, HisCommon.DataEntity.OneCard_PatientInfo info, string type, ref string errMsg)
        {
            int IsSuccess = 0;
            string sql = @"insert into LOG_PATIENTINFO
                                  (SERIAL, CARD_ID, TYPE, OPER_ID, OPER_NAME, OPER_DATE, HIS_BUSSINESSNO, BANK_BUSSINESSNO, REMARK)
                                values
                                  ('{0}',
                                   '{1}',
                                   '{2}',
                                   '{3}',
                                   '{4}',
                                   sysdate,
                                   '{5}',
                                   '{6}',
                                   '{7}')
                                    ";
            string sqlseq = @"select LOG_PATIENTINFO_SEQ.Nextval from dual";
            try
            {
                DataTable dt = db.GetDataTable(sqlseq);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "查询索引失败（LOG_PATIENTINFO_SEQ）";
                    return -1;
                }
                sql = string.Format(sql, dt.Rows[0][0].ToString(),//0
                                  info.Card_id,//1
                                  type,//2
                                  info.Oper_id,//3
                                  info.Oper_name,//4
                                  info.His_bussinessno,//5
                                  info.Bank_bussinessno,//6
                                  info.Home_add);//7
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message + "^" + sql;
                return -1;
            }
            return IsSuccess;
        }
        #endregion

        #region 获取充值发票号
        /// <summary>
        /// 获取充值发票号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="operCode"></param>
        /// <param name="rcptNo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetOneCardPaymentInvoice(BaseEntityer db, string operCode, ref string rcptNo, ref string errMsg)
        {
            string sqlDate = @"select nvl(max(to_number(c.invoice_no)),0) from onecard_payment c where c.oper_id='{0}'";
            try
            {
                sqlDate = string.Format(sqlDate, operCode);
                DataTable dt = db.GetDataTable(sqlDate);
                rcptNo = dt.Rows[0][0].ToString();
                if (rcptNo == "0")
                {
                    rcptNo = operCode + "00000001";
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message + "^" + sqlDate;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 插入充值记录表

        /// <summary>
        /// 插入充值记录表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="payment"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertOneCardPayment(BaseEntityer db, HisCommon.DataEntity.ONECARD_PAYMENT payment, ref string errMsg)
        {
            //2014-4-23 by li 银医卡接口增加真正银行端返回流水号和操作日期
            //2014-4-24 by li 一卡通增加开卡序号写入充值记录中
            int IsSuccess = 0;
            string sql = @"insert into ONECARD_PAYMENT
                                      (CARD_ID,
                                       SERIAL,
                                       CARD_TYPE,
                                       BANK_CARDNO,
                                       RECHARGE_TYPE,
                                       RECHARGE_COST,
                                       RECHARGE_CLASS,
                                       RECHARGE_MODE,
                                       RECHARGE_DATE,                                      
                                       REMARK,
                                       TRANS_TYPE,
                                       ACC_BALANCE,
                                       ACC_DATE,
                                       INVOICE_NO,
                                       HIS_BUSSINESSNO,
                                       BANK_BUSSINESSNO,
                                       OPER_ID,
                                       OPER_NAME,
                                       OPER_DATE,
                                       BANK_NO,
                                       BANK_DATE,
                                       BANK_ACCNO,
                                       BANK_ACCDATE,
                                       BANK_ACCFLAG,
                                       BANK_BUSSINESSNO_REAL,
                                       BANK_DATE_REAL,
                                       CARD_SERIAL
                                      )
                                    values
                                      ('{0}',  --CARD_ID
                                       (select nvl(max(SERIAL),0) + 1 from ONECARD_PAYMENT where CARD_ID='{0}'),  --SERIAL
                                       '{1}',  --CARD_TYPE
                                       '{2}',  --BANK_CARDNO
                                       '{3}',  --RECHARGE_TYPE
                                       '{4}',  --RECHARGE_COST
                                       '{5}',  --RECHARGE_CLASS
                                       '{6}',  --RECHARGE_MODE
                                        to_date('{7}','yyyy-mm-dd hh24:mi:ss'),  --RECHARGE_DATE                                      
                                       '{8}', --REMARK
                                       '{9}', --TRANS_TYPE
                                       '{10}', --ACC_BALANCE
                                       to_date('{11}','yyyy-mm-dd hh24:mi:ss'),--ACC_DATE
                                       '{12}', -- INVOICE_NO
                                       '{13}',  --HIS_BUSSINESSNO
                                       '{14}', --BANK_BUSSINESSNO
                                       '{15}', --OPER_ID
                                       '{16}', --OPER_NAME                                        
                                        to_date('{17}','yyyy-mm-dd hh24:mi:ss'), --OPER_DATE
                                       '{18}', --BANK_NO
                                        to_date('{19}','yyyy-mm-dd hh24:mi:ss'), --BANK_DATE
                                       '{20}', --BANK_ACCNO
                                       to_date('{21}','yyyy-mm-dd hh24:mi:ss'), --BANK_ACCDATE
                                       '{22}',--BANK_ACCFLAG
                                       '{23}',--BANK_BUSSINESSNO_REAL
                                        to_date('{24}','yyyy-mm-dd hh24:mi:ss'),--BANK_DATE_REAL
                                       {25}--CARD_SERIAL
)";
            string sqlDate = @"select sysdate from dual";
            try
            {
                DataTable dt = db.GetDataTable(sqlDate);

                sql = string.Format(sql, payment.CARD_ID,//0
                    payment.CARD_TYPE,//1
                    payment.BANK_CARDNO,//2
                    payment.RECHARGE_TYPE,//3
                    payment.RECHARGE_COST,//4
                    payment.RECHARGE_CLASS,//5
                    payment.RECHARGE_MODE,//6
                    payment.RECHARGE_DATE,//7                    
                    payment.REMARK,//8
                    payment.TRANS_TYPE,//9
                    payment.ACC_BALANCE,//10
                    payment.ACC_DATE,//11
                    payment.INVOICE_NO,//12
                    payment.HIS_BUSSINESSNO,//13
                    payment.BANK_BUSSINESSNO,//14
                    payment.OPER_ID,//15
                    payment.OPER_NAME,//16
                    dt.Rows[0][0].ToString(),//17
                    payment.BANK_NO, //18
                    payment.BANK_DATE,//19
                    payment.BANK_ACCNO,//20
                    payment.BANK_ACCDATE,//21
                    payment.BANK_ACCFLAG,//22
                    payment.BANK_BUSSINESSNO_REAL,//23
                    payment.BANK_DATE_REAL,//24
                    payment.CARD_SERIAL//25
                    );
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message + "^" + sql;
                return -1;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 插入充值记录表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="payment"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertOneCardPayment(HisCommon.DataEntity.ONECARD_PAYMENT payment, ref string errMsg)
        {
            #region SQL
            string strSQL = @"insert into ONECARD_PAYMENT
                                      (CARD_ID,
                                       SERIAL,
                                       CARD_TYPE,
                                       BANK_CARDNO,
                                       RECHARGE_TYPE,
                                       RECHARGE_COST,
                                       RECHARGE_CLASS,
                                       RECHARGE_MODE,
                                       RECHARGE_DATE,                                      
                                       REMARK,
                                       TRANS_TYPE,
                                       ACC_BALANCE,
                                       ACC_DATE,
                                       INVOICE_NO,
                                       HIS_BUSSINESSNO,
                                       BANK_BUSSINESSNO,
                                       OPER_ID,
                                       OPER_NAME,
                                       OPER_DATE,
                                       BANK_NO,
                                       BANK_DATE,
                                       BANK_ACCNO,
                                       BANK_ACCDATE,
                                       BANK_ACCFLAG,
                                       BANK_BUSSINESSNO_REAL,
                                       BANK_DATE_REAL,
                                       CARD_SERIAL
                                      )
                                    values
                                      ('{0}',  --CARD_ID
                                       (select nvl(max(SERIAL),0) + 1 from ONECARD_PAYMENT where CARD_ID='{0}'),  --SERIAL
                                       '{1}',  --CARD_TYPE
                                       '{2}',  --BANK_CARDNO
                                       '{3}',  --RECHARGE_TYPE
                                       '{4}',  --RECHARGE_COST
                                       '{5}',  --RECHARGE_CLASS
                                       '{6}',  --RECHARGE_MODE
                                        to_date('{7}','yyyy-mm-dd hh24:mi:ss'),  --RECHARGE_DATE                                      
                                       '{8}', --REMARK
                                       '{9}', --TRANS_TYPE
                                       '{10}', --ACC_BALANCE
                                       to_date('{11}','yyyy-mm-dd hh24:mi:ss'),--ACC_DATE
                                       '{12}', -- INVOICE_NO
                                       '{13}',  --HIS_BUSSINESSNO
                                       '{14}', --BANK_BUSSINESSNO
                                       '{15}', --OPER_ID
                                       '{16}', --OPER_NAME                                        
                                        sysdate, --OPER_DATE
                                       '{17}', --BANK_NO
                                        to_date('{18}','yyyy-mm-dd hh24:mi:ss'), --BANK_DATE
                                       '{19}', --BANK_ACCNO
                                       to_date('{20}','yyyy-mm-dd hh24:mi:ss'), --BANK_ACCDATE
                                       '{21}',--BANK_ACCFLAG
                                       '{22}',--BANK_BUSSINESSNO_REAL
                                        to_date('{23}','yyyy-mm-dd hh24:mi:ss'),--BANK_DATE_REAL
                                       {24}--CARD_SERIAL
                            )";
            #endregion

            int result = 0;
            try
            {
                strSQL = string.Format(strSQL, payment.CARD_ID,//0
                    payment.CARD_TYPE,//1
                    payment.BANK_CARDNO,//2
                    payment.RECHARGE_TYPE,//3
                    payment.RECHARGE_COST,//4
                    payment.RECHARGE_CLASS,//5
                    payment.RECHARGE_MODE,//6
                    payment.RECHARGE_DATE,//7                    
                    payment.REMARK,//8
                    payment.TRANS_TYPE,//9
                    payment.ACC_BALANCE,//10
                    payment.ACC_DATE,//11
                    payment.INVOICE_NO,//12
                    payment.HIS_BUSSINESSNO,//13
                    payment.BANK_BUSSINESSNO,//14
                    payment.OPER_ID,//15
                    payment.OPER_NAME,//16
                    payment.BANK_NO, //17
                    payment.BANK_DATE,//18
                    payment.BANK_ACCNO,//19
                    payment.BANK_ACCDATE,//20
                    payment.BANK_ACCFLAG,//21
                    payment.BANK_BUSSINESSNO_REAL,//22
                    payment.BANK_DATE_REAL,//23
                    payment.CARD_SERIAL//24
                    );
                result = BaseEntityer.Db.ZDExecNonQuery(strSQL);
            }
            catch (Exception e)
            {
                errMsg = e.Message + "^" + strSQL;
                return -1;
            }
            return result;
        }

        #endregion

        #region 修改患者卡余额
        /// <summary>
        /// 修改患者卡余额
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cardId">一卡通患者ID</param>
        /// <param name="acc_outbalance">门诊余额</param>
        /// <param name="acc_inbalance">住院余额</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns>1、成功 -1、异常</returns>
        public int UpdatePatientInfoCost(BaseEntityer db, string cardId, decimal acc_outbalance, decimal acc_inbalance, ref string pErrMsg)
        {
            int revInt = 0;
            try
            {
                string sql = @"update onecard_patientinfo t
                               set t.acc_outbalance = t.acc_outbalance + {1},
                                   t.acc_inbalance  = t.acc_inbalance + {2}
                            where t.card_id = '{0}'";

                sql = string.Format(sql, cardId, acc_outbalance, acc_inbalance);
                revInt = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }
        #endregion

        #region 获取科室出诊号表
        /// <summary>
        ///获取科室出诊号表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="infoList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetDeptRegTable(BaseEntityer db, string clinic_Date, ref List<ONECARD_DEPTREG> infoList, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select t.clinic_label,
                                   decode(t.time_desc,
                                          '上午',
                                          '1',
                                          '下午',
                                          '2',
                                          '白天',
                                          '3',
                                          '前夜',
                                          '4',
                                          '后夜',
                                          '5',
                                          '夜间',
                                          '6',
                                          '昼夜',
                                          '7',
                                          '9'),
                                   t.registration_limits,
                                   t.appointment_limits,
                                   t.current_no,
                                   t.registration_num,
                                   t.appointment_num,
                                   s.doctor,
                                   s.doctor_title,
                                   (select h.dept_name
                                      from dept_dict h
                                     where h.dept_code = s.clinic_dept),
                                   c.regprice,
                                   c.diagprice,
                                   s.clinic_type
                              from clinic_for_regist t, clinic_index s, clinic_type_dict c
                             where t.clinic_label = s.clinic_label
                               and s.clinic_type = c.clinic_type
                               and t.clinic_date = to_date('{0}', 'yyyy-mm-dd')
                             order by s.clinic_dept
                            ";
            string ness = string.Empty;
            try
            {
                sql = string.Format(sql, clinic_Date);
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "没有查询到号表信息。";
                    return -1;
                }
                infoList = new List<ONECARD_DEPTREG>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ONECARD_DEPTREG info = new ONECARD_DEPTREG();
                    info.Clinic_label = dt.Rows[i][0].ToString();
                    info.Timedesc = dt.Rows[i][1].ToString();
                    info.Limitreg = string.IsNullOrEmpty(dt.Rows[i][2].ToString()) == true ? 0 : int.Parse(dt.Rows[i][2].ToString());
                    info.Limitpre = string.IsNullOrEmpty(dt.Rows[i][3].ToString()) == true ? 0 : int.Parse(dt.Rows[i][3].ToString());
                    info.Current = string.IsNullOrEmpty(dt.Rows[i][4].ToString()) == true ? 0 : int.Parse((dt.Rows[i][4].ToString()));
                    info.Surplus = string.IsNullOrEmpty(dt.Rows[i][5].ToString()) == true ? 0 : int.Parse(dt.Rows[i][5].ToString());
                    info.Pre_reg = string.IsNullOrEmpty(dt.Rows[i][6].ToString()) == true ? 0 : int.Parse(dt.Rows[i][6].ToString());
                    info.Doctor = dt.Rows[i][7].ToString();
                    info.Doctorprofe = dt.Rows[i][8].ToString();
                    info.Deptname = dt.Rows[i][9].ToString();
                    info.Regfee = decimal.Parse(dt.Rows[i][10].ToString());
                    info.Clinicfee = decimal.Parse(dt.Rows[i][11].ToString());
                    info.Clinictype = dt.Rows[i][12].ToString();
                    infoList.Add(info);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取费别字典表信息
        /// <summary>
        /// 获取费别字典表信息
        /// </summary>
        /// <returns></returns>
        public List<CHARGE_TYPE_DICT> GetCHARGE_TYPE_DICTInfor()
        {
            string sql = @"SELECT CHARGE_TYPE_DICT.SERIAL_NO,
                           CHARGE_TYPE_DICT.CHARGE_TYPE_CODE,
                           CHARGE_TYPE_DICT.CHARGE_TYPE_NAME,
                           CHARGE_TYPE_DICT.CHARGE_PRICE_INDICATOR,
                           CHARGE_TYPE_DICT.CHARGE_PRICE,
                           CHARGE_TYPE_DICT.CHARGE_LOW,
                           CHARGE_TYPE_DICT.PRINT_MODEL,
                           CHARGE_TYPE_DICT.OUTP_DISPLAY,
                           CHARGE_TYPE_DICT.IS_UPLOAD,
                           CHARGE_TYPE_DICT.SPELL_CODE,
                           CHARGE_TYPE_DICT.WB_CODE 
                          FROM CHARGE_TYPE_DICT WHERE CHARGE_TYPE_CODE=1 ORDER BY CHARGE_TYPE_DICT.SERIAL_NO";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CHARGE_TYPE_DICT>(ds).ToList();
        }
        #endregion

        #region 获取挂号票据
        /// <summary>
        ///获取挂号票据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cardID"></param>
        /// <param name="infoList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetRegInfo(BaseEntityer db, int cardID, ref List<CLINIC_MASTER> infoList, ref string errMsg)
        {
            DataTable dt = new DataTable();
            string sql = @"select t.registering_date,
                           t.clinic_label,
                           t.charge_type,
                           (select c.dept_name from dept_dict c where c.dept_code = t.visit_dept) dept_name,
                           t.doctor,
                           t.regist_fee,
                           t.clinic_fee,
                           t.clinic_charge,
                           t.admis,
                           t.billno,
                           t.returned_operator
                      from clinic_master t
                      where t.patient_id='{0}'
                      order by t.registering_date
                            ";
            string ness = string.Empty;
            try
            {
                OneCard_PatientInfo pinfo = new OneCard_PatientInfo();
                int rev = this.GetPatientInfo(db, cardID, ref pinfo, ref errMsg);
                if (rev <= 0)
                {
                    return -1;
                }
                sql = string.Format(sql, pinfo.Out_patientid);
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count <= 0)
                {
                    errMsg = "没有查询到号表信息。";
                    return -1;
                }
                infoList = new List<CLINIC_MASTER>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CLINIC_MASTER info = new CLINIC_MASTER();
                    info.REGISTERING_DATE = DateTime.Parse(dt.Rows[i][0].ToString());
                    info.CLINIC_LABEL = dt.Rows[i][1].ToString();
                    info.CLINIC_TYPE = dt.Rows[i][2].ToString();
                    info.VISIT_DEPT = dt.Rows[i][3].ToString();
                    info.DOCTOR = (dt.Rows[i][4].ToString());
                    info.REGIST_FEE = decimal.Parse(dt.Rows[i][5].ToString());
                    info.CLINIC_FEE = decimal.Parse(dt.Rows[i][6].ToString());
                    info.CLINIC_CHARGE = decimal.Parse(dt.Rows[i][7].ToString());
                    info.ADMIS = dt.Rows[i][8].ToString();
                    info.BILLNO = dt.Rows[i][9].ToString();
                    info.RETURNED_OPERATOR = dt.Rows[i][10].ToString();
                    infoList.Add(info);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取挂号主表信息(按照时间段)
        /// <summary>
        /// 获取挂号主表信息(按照时间段)
        /// </summary>
        /// <param name="patientID">病人ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<CLINIC_MASTER> GetPatientByNO(string patientID, string startDate, string endDate)
        {
            string sql =
           @"select *
  from clinic_master c
 where c.patient_id = '{0}'
   and c.visit_date >= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
   and c.visit_date <= to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')
   ORDER BY c.visit_date DESC";
            sql = string.Format(sql, new object[] { patientID, startDate, endDate });
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(patients);
        }

        /// <summary>
        /// 获取挂号主表信息(按照时间段)
        /// </summary>
        /// <param name="patientID">病人ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<CLINIC_MASTER> GetPatientByNOVisit(string visitNo, string date)
        {
            string sql =
           @"select *
              from clinic_master c
             where c.visit_no = '{0}'
 and c.visit_date = to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')";
            sql = string.Format(sql, new object[] { visitNo, date });
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_MASTER>(patients);
        }
        #endregion

        #region 根据就诊序号，和日期限制查询病人要交款的医嘱明细
        /// <summary>
        /// 根据就诊序号，和日期限制查询病人要交款的医嘱明细
        /// </summary>
        /// <param name="visitNO"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<HisCommon.DataEntity.OUTP_ORDERS_DETAIL> QueryPatientOrderDetail(string visitNO, string date)
        {
            string sql = @"select * from outp_orders_detail t
                            where t.visit_no={0}
                            and t.visit_date =to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                            and t.charge_indicator=0 and t.oper_date +(select param_value from sys_param where param_name='CHARGE_CHECK_DAYS')>= sysdate";
            sql = string.Format(sql, visitNO, date);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_ORDERS_DETAIL>(ds).ToList();
        }
        #endregion

        #region 获取结算主表信息(按照时间段)
        /// <summary>
        /// 获取结算主表信息(按照时间段)
        /// </summary>
        /// <param name="patientID">病人ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public List<OUTP_RCPT_MASTER> GetOutBalanceMaster(string patientID, string startDate, string endDate)
        {
            string sql =
           @"select *
  from outp_rcpt_master t
 where t.patient_id = '{0}'
   and t.visit_date >= to_date('{1} 00:00:00', 'yyyy-mm-dd hh24:mi:ss')
   and t.visit_date <= to_date('{2} 23:59:59', 'yyyy-mm-dd hh24:mi:ss')
 order by t.visit_date desc";
            sql = string.Format(sql, new object[] { patientID, startDate, endDate });
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_RCPT_MASTER>(patients).ToList();
        }
        #endregion

        #region 获取已结算费用明细信息(按照发票号)
        /// <summary>
        /// 获取已结算费用明细信息(按照发票号)
        /// </summary>
        /// <param name="billNO">发票号</param>
        /// <returns></returns>
        public List<OUTP_BILL_ITEMS> GetOutBalanceFeeDetails(string billNO)
        {
            string sql =
           @"select * from outp_bill_items t where t.rcpt_no='{0}'
            order by t.visit_date desc";
            sql = string.Format(sql, billNO);
            DataSet patients = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<OUTP_BILL_ITEMS>(patients).ToList();
        }
        #endregion

        #region 查询充值记录（按时间段）
        /// <summary>
        /// 查询充值记录（按时间段）
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="rechargeType">充值类型（1.现金 2.银行卡）</param>
        /// <param name="payList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetOneCardPaymentByTime(string startDate, string endDate, string rechargeType, string oper, ref List<ONECARD_PAYMENT> payList, ref string errMsg)
        {
            //2014-3-28 by li 对账查询分现金和银行卡
            string sql = @"select *
                          from onecard_payment t
                         where t.recharge_date >=
                         to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                           and t.recharge_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                           and t.bank_accflag = 0
                           and t.oper_id='{2}'
                           and t.RECHARGE_TYPE='{3}' 
                         order by t.recharge_date";

            try
            {
                sql = string.Format(sql, new object[] { startDate, endDate, oper, rechargeType });
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                payList = DataSetToEntity.DataSetToT<ONECARD_PAYMENT>(ds).ToList();
            }
            catch (Exception e)
            {
                errMsg = e.Message + "GetOneCardPaymentByTime";
                return -1;
            }
            return 1;
        }
        #endregion

        #region 查询充值记录（按时间段）
        /// <summary>
        /// 查询充值记录（按时间段）
        /// </summary>
        /// <param name="BANK_ACCNO"></param>
        /// <param name="payList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetOneCardPaymentByAccTime(string BANK_ACCNO, ref List<ONECARD_PAYMENT> payList, ref string errMsg)
        {
            string sql = @"select *
                          from onecard_payment t
                         where t.BANK_ACCNO='{0}'
                         ";

            try
            {
                sql = string.Format(sql, new object[] { BANK_ACCNO });
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                payList = DataSetToEntity.DataSetToT<ONECARD_PAYMENT>(ds).ToList();
            }
            catch (Exception e)
            {
                errMsg = e.Message + "GetOneCardPaymentByAccTime";
                return -1;
            }
            return 1;
        }
        #endregion

        #region 查询消费记录（按时间段）
        /// <summary>
        /// 查询消费记录（按时间段）
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="payList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetOneCardSpendingByTime(string startDate, string endDate, string oper, ref List<ONECARD_SPENDING> spendList, ref string errMsg)
        {
            string sql =
            @"select *
              from onecard_spending t
             where t.spend_date >=
             to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
               and t.spend_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
               and t.bank_acc_flag = 0 
               and t.oper_id = '{2}'
             order by t.spend_date";

            try
            {
                sql = string.Format(sql, new object[] { startDate, endDate, oper });
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                spendList = DataSetToEntity.DataSetToT<ONECARD_SPENDING>(ds).ToList();
            }
            catch (Exception e)
            {
                errMsg = e.Message + "GetOneCardSpendingByTime";
                return -1;
            }
            return 1;
        }
        #endregion

        #region 查询消费记录（按时间段）
        /// <summary>
        /// 查询消费记录（按时间段）
        /// </summary>
        /// <param name="BANK_ACC_BALANCE"></param>
        /// <param name="payList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetOneCardSpendingByAccTime(string BANK_ACC_BALANCE, ref List<ONECARD_SPENDING> spendList, ref string errMsg)
        {
            string sql =
            @"select *
              from onecard_spending t
             where t.BANK_ACC_BALANCE = '{0}'
             order by t.spend_date";

            try
            {
                sql = string.Format(sql, new object[] { BANK_ACC_BALANCE });
                DataSet ds = BaseEntityer.Db.GetDataSet(sql);
                spendList = DataSetToEntity.DataSetToT<ONECARD_SPENDING>(ds).ToList();
            }
            catch (Exception e)
            {
                errMsg = e.Message + "GetOneCardSpendingByAccTime";
                return -1;
            }
            return 1;
        }
        #endregion

        #region 查询终端日结序列
        /// <summary>
        /// 查询终端日结序列
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetOneCardAccBalSeq(ref string errMsg)
        {
            string seq = string.Empty;
            try
            {
                seq = BaseEntityer.Db.GetDataTable("select ONECARD_BANKACC.NEXTVAL from dual").Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return "B" + seq;
        }
        #endregion

        #region 修改日结号
        /// <summary>
        /// 修改日结号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="rechargeType">充值类型（1.现金 2.银行卡）</param>
        /// <param name="oper"></param>
        /// <param name="accno"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int UpdateOneCardAccByPay(BaseEntityer db, string startDate, string endDate, string rechargeType, string oper, string accno, ref string pErrMsg)
        {
            int revInt = 0;
            try
            {
                string sql = @"update onecard_payment t set t.bank_accno='{4}'
                                 where t.recharge_date >=
                                 to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                                   and t.recharge_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                                   and t.bank_accflag = 0
                                   --and t.bank_accno is null
                                   and t.oper_id='{2}'
                                   and t.RECHARGE_TYPE='{3}' ";
                sql = string.Format(sql, new object[] { startDate, endDate, oper, rechargeType, accno });
                revInt = BaseEntityer.Db.ExecuteNonQuery(sql);
                if (revInt <= 0)
                {
                    pErrMsg = "修改onecard_payment出错。";
                    return -1;
                }
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }
        #endregion

        #region 修改日结号
        /// <summary>
        /// 修改日结号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="oper"></param>
        /// <param name="accno"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int UpdateOneCardAccBySpend(BaseEntityer db, string startDate, string endDate, string oper, string accno, ref string pErrMsg)
        {
            int revInt = 0;
            try
            {
                string sql = @"update onecard_spending t
                               set t.bank_acc_balance = '{3}'
                             where t.spend_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                               and t.spend_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               and t.bank_acc_flag = 0
                               --and t.acc_balance is null
                               and t.oper_id='{2}'";
                sql = string.Format(sql, new object[] { startDate, endDate, oper, accno });
                revInt = BaseEntityer.Db.ExecuteNonQuery(sql);
                if (revInt <= 0)
                {
                    pErrMsg = "修改onecard_spending出错。";
                    return -1;
                }
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }
        #endregion

        #region 修改日结标志
        /// <summary>
        /// 修改日结标志
        /// </summary>
        /// <param name="db"></param>
        /// <param name="accno"></param>
        /// <param name="operDate"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int UpdateOneCardAccByPayFlag(BaseEntityer db, string accno, string operDate, ref string pErrMsg)
        {
            int revInt = 0;
            try
            {
                string sql = @"update onecard_payment t
                               set t.bank_accdate = to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'),
                                   t.bank_accflag = '1'
                             where t.bank_accno = '{0}'";
                sql = string.Format(sql, new object[] { accno, operDate });
                revInt = BaseEntityer.Db.ExecuteNonQuery(sql);
                if (revInt <= 0)
                {
                    pErrMsg = "修改onecard_payment出错。";
                    return -1;
                }
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }
        #endregion

        #region 修改日结号
        /// <summary>
        /// 修改日结号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="accno"></param>
        /// <param name="operDate"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int UpdateOneCardAccBySpendFlag(BaseEntityer db, string accno, string operDate, ref string pErrMsg)
        {
            int revInt = 0;
            try
            {
                string sql = @"update Onecard_Spending s
                               set s.bank_acc_date = to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'),
                                   s.bank_acc_flag = '1'
                             where s.bank_acc_balance = '{0}'";
                sql = string.Format(sql, new object[] { accno, operDate });
                revInt = BaseEntityer.Db.ExecuteNonQuery(sql);
                if (revInt <= 0)
                {
                    pErrMsg = "修改onecard_spending出错。";
                    return -1;
                }
            }
            catch (Exception e)
            {
                pErrMsg = e.Message;
                revInt = -1;
            }
            return revInt;
        }
        #endregion

        #region 插入消费信息表
        /// <summary>
        /// 插入消费信息表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="spending"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertSpending(BaseEntityer db, HisCommon.DataEntity.ONECARD_SPENDING spending, ref string errMsg)
        {
            int IsSuccess = 0;
            //2014-4-23 by li 银医卡接口增加真正银行端返回流水号和操作日期
            //2014-4-24 by li 一卡通增加开卡序号写入充值记录中
            string sql = @"INSERT INTO ONECARD_SPENDING
                                      (CARD_ID,
                                       SERIAL,
                                       CARD_TYPE,
                                       BANK_CARDNO,
                                       SPEND_TYPE,
                                       SPEND_CLASS,
                                       SPEND_MODEL,
                                       SPEND_AMOUNT,
                                       SPEND_DATE,
                                       OPER_ID,
                                       OPER_NAME,
                                       OPER_DATE,
                                       SPEND_REMARK,
                                       TRANS_TYPE,
                                       ACC_BALANCE,
                                       ACC_DATE,
                                       INVOICE_NO,
                                       REFUND_INVOICENO,
                                       HIS_BUSSINESSNO,
                                       BANK_BUSSINESSNO,
                                       BANK_NO,
                                       BANK_DATE,
                                       BANK_ACC_BALANCE,
                                       BANK_ACC_DATE,
                                       BANK_ACC_FLAG,
                                       BANK_BUSSINESSNO_REAL,
                                       BANK_DATE_REAL,
                                       CARD_SERIAL)
                                    VALUES
                                      ('{0}',--CARD_ID
                                       (select nvl(max(SERIAL),0) + 1 from ONECARD_SPENDING where CARD_ID ='{0}'),--SERIAL
                                       '{1}',--CARD_TYPE
                                       '{2}',--BANK_CARDNO
                                       '{3}',--SPEND_TYPE
                                       '{4}',--SPEND_CLASS
                                       '{5}',--SPEND_MODEL
                                       '{6}',--SPEND_AMOUNT
                                       to_date('{7}','yyyy-mm-dd hh24:mi:ss'),--SPEND_DATE
                                       '{8}',--OPER_ID
                                       '{9}',--OPER_NAME
                                       to_date('{10}','yyyy-mm-dd hh24:mi:ss'),--OPER_DATE
                                       '{11}',--SPEND_REMARK
                                       '{12}',--TRANS_TYPE
                                       '{13}',--ACC_BALANCE
                                        to_date('{14}','yyyy-mm-dd hh24:mi:ss'),--ACC_DATE
                                       '{15}',--INVOICE_NO
                                       '{16}',--REFUND_INVOICENO
                                       '{17}',--HIS_BUSSINESSNO
                                       '{18}',--BANK_BUSSINESSNO
                                       '{19}',--BANK_NO
                                       to_date('{20}','yyyy-mm-dd hh24:mi:ss'),--BANK_DATE
                                       '{21}',--BANK_ACC_BALANCE
                                       to_date('{22}','yyyy-mm-dd hh24:mi:ss'),--BANK_ACC_DATE
                                       '{23}',--BANK_ACC_FLAG
                                       '{24}',--BANK_BUSSINESSNO_REAL
                                        to_date('{25}','yyyy-mm-dd hh24:mi:ss'),--BANK_DATE_REAL
                                       {26}--CARD_SERIAL
                                       )";
            try
            {
                sql = string.Format(sql,
                                  spending.CARD_ID,//0
                                  spending.CARD_TYPE,//1
                                  spending.BANK_CARDNO,//2
                                  spending.SPEND_TYPE,//3
                                  spending.SPEND_CLASS,//4
                                  spending.SPEND_MODEL,//5
                                  spending.SPEND_AMOUNT,//6
                                  spending.SPEND_DATE,//7
                                  spending.OPER_ID,//8
                                  spending.OPER_NAME,//9
                                  spending.OPER_DATE,//10
                                  spending.SPEND_REMARK,//11
                                  spending.TRANS_TYPE,//12
                                  spending.ACC_BALANCE,//13
                                  spending.ACC_DATE,//14
                                  spending.INVOICE_NO,//15
                                  spending.REFUND_INVOICENO,//16
                                  spending.HIS_BUSSINESSNO,//17
                                  spending.BANK_BUSSINESSNO,//18
                                  spending.BANK_NO,//19
                                  spending.BANK_DATE,//20
                                  spending.BANK_ACC_BALANCE,//21
                                  spending.BANK_ACC_DATE,//22
                                  spending.BANK_ACC_FLAG,//23
                                  spending.BANK_BUSSINESSNO_REAL,//24
                                  spending.BANK_DATE_REAL,//25
                                  spending.CARD_SERIAL//26
                                  );
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message + "^" + sql;
                return -1;
            }
            return IsSuccess;
        }
        #endregion

        #region 修改退费发票号（正交易退费发票号）
        /// <summary>
        /// 修改退费发票号（正交易退费发票号）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="spending"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateSpending(BaseEntityer db, HisCommon.DataEntity.ONECARD_SPENDING spending, ref string errMsg)
        {
            int IsSuccess = 0;
            string sql = @"UPDATE ONECARD_SPENDING
                           SET REFUND_INVOICENO = '{3}'
                         WHERE CARD_ID = '{0}'
                           AND SERIAL = '{1}'
                           AND INVOICE_NO = '{2}'
                           AND TRANS_TYPE = 1";
            try
            {
                sql = string.Format(sql,
                                  spending.CARD_ID,//0
                                  spending.SERIAL,//1
                                  spending.INVOICE_NO,//2                                  
                                  spending.REFUND_INVOICENO//3
                                  );
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message + "^" + sql;
                return -1;
            }
            return IsSuccess;
        }
        #endregion

        #region 挂号和收费后发票号写入充值记录（正交易发票号）
        /// <summary>
        /// 挂号和收费后发票号写入充值记录（正交易发票号）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="bank_bussinessno_real">银行端交易流水号</param>
        /// <param name="invoice_no">HIS正交易发票号</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public int UpdatePayment(BaseEntityer db, string bank_bussinessno_real, string invoice_no, ref string errMsg)
        {
            int IsSuccess = 0;
            string sql = @"UPDATE ONECARD_PAYMENT
                           SET INVOICE_NO = '{1}'
                         WHERE BANK_BUSSINESSNO_REAL = '{0}'";
            try
            {
                sql = string.Format(sql,
                                  bank_bussinessno_real,//0                                 
                                  invoice_no//1
                                  );
                IsSuccess = db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                errMsg = e.Message + "^" + sql;
                return -1;
            }
            return IsSuccess;
        }
        #endregion

        #region 获取所有消费明细(按就诊卡号)
        /// <summary>
        /// 获取所有消费明细(按就诊卡号)
        /// </summary>
        /// <param name="Db"></param>
        /// <param name="CardID"></param>
        /// <param name="spending"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int GetSpendingDetailsByCardID(BaseEntityer Db, string CardID, List<ONECARD_SPENDING> spending, ref string pErrMsg)
        {
            //2014-4-24 by li 银医卡接口增加真正银行端返回流水号和操作日期
            string sql =
           @"SELECT CARD_ID,
                   SERIAL,
                   CARD_TYPE,
                   BANK_CARDNO,
                   SPEND_TYPE,
                   SPEND_CLASS,
                   SPEND_MODEL,
                   SPEND_AMOUNT,
                   SPEND_DATE,
                   OPER_ID,
                   OPER_NAME,
                   OPER_DATE,
                   SPEND_REMARK,
                   TRANS_TYPE,
                   ACC_BALANCE,
                   ACC_DATE,
                   INVOICE_NO,
                   REFUND_INVOICENO,
                   HIS_BUSSINESSNO,
                   BANK_BUSSINESSNO,
                   BANK_NO,
                   BANK_DATE,
                   BANK_ACC_BALANCE,
                   BANK_ACC_DATE,
                   BANK_ACC_FLAG,
                   CARD_SERIAL,
                   BANK_BUSSINESSNO_REAL,
                   BANK_DATE_REAL 
              FROM ONECARD_SPENDING
             WHERE CARD_ID = '{0}'
             ORDER BY SERIAL";
            DataSet ds = new DataSet();
            try
            {
                sql = string.Format(sql, CardID);
                ds = Db.GetDataSet(sql);
                if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    pErrMsg = "没有查询到消费信息。";
                    return -1;
                }
                spending = DataSetToEntity.DataSetToT<ONECARD_SPENDING>(ds).ToList();
            }
            catch (Exception ex)
            {
                pErrMsg = ex.Message + "^" + sql;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取所有消费明细(按发票号)
        /// <summary>
        /// 获取所有消费明细(按发票号)
        /// </summary>
        /// <param name="Db"></param>
        /// <param name="InvoiceNO"></param>
        /// <param name="spending"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int GetSpendingDetailsByInvoice(BaseEntityer Db, string InvoiceNO, string model, ref List<ONECARD_SPENDING> spending, ref string pErrMsg)
        {
            string sql =
           @"SELECT *
              FROM ONECARD_SPENDING
             WHERE INVOICE_NO = '{0}'
             AND SPEND_MODEL = '{1}'
             ORDER BY TRANS_TYPE";
            DataSet ds = new DataSet();
            try
            {
                sql = string.Format(sql, InvoiceNO, model);
                ds = Db.GetDataSet(sql);
                if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    pErrMsg = "没有查询到消费信息。";
                    return -1;
                }
                spending = DataSetToEntity.DataSetToT<ONECARD_SPENDING>(ds).ToList();
            }
            catch (Exception ex)
            {
                pErrMsg = ex.Message + "^" + sql;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 挂号数据插入
        /// <summary>
        /// 挂号数据插入
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_master"></param>
        /// <returns></returns>
        public int InsertClinicMaster(BaseEntityer db, CLINIC_MASTER clinic_master)
        {
            string sql = @"insert into CLINIC_MASTER
                      (CLINIC_MASTER.VISIT_DATE,
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
                        CLINIC_MASTER.BILLNO,
                        CLINIC_MASTER.CHARGE_TYPE_CODE,
                        CLINIC_MASTER.INVOICE_NEW,
                        CLINIC_MASTER.CARD_FEE,
                        CLINIC_MASTER.TERMINAL_FLAG
                        )
                    values
                      (to_date('{0}', 'yyyy-mm-dd hh24:mi:ss'), {1}, '{2}', '{3}', {4}, '{5}', 
                       '{6}', '{7}', '{8}', {9}, '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', {16},
                       '{17}', '{18}', '{19}', {20}, {21}, to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'), 
                       '{23}', {24}, {25}, {26}, {27}, '{28}', '{29}', '{30}', '{31}', '{32}', '{33}')";
            object[] param = new object[] { clinic_master.VISIT_DATE.Date, clinic_master.VISIT_NO, clinic_master.CLINIC_LABEL, clinic_master.VISIT_TIME_DESC, clinic_master.SERIAL_NO, clinic_master.PATIENT_ID, clinic_master.NAME, clinic_master.NAME_PHONETIC, clinic_master.SEX, clinic_master.AGE, clinic_master.IDENTITY, clinic_master.CHARGE_TYPE, clinic_master.INSURANCE_TYPE, clinic_master.INSURANCE_NO, clinic_master.UNIT_IN_CONTRACT, clinic_master.CLINIC_TYPE, clinic_master.FIRST_VISIT_INDICATOR, clinic_master.VISIT_DEPT, clinic_master.VISIT_SPECIAL_CLINIC, clinic_master.DOCTOR, clinic_master.MR_PROVIDE_INDICATOR, clinic_master.REGISTRATION_STATUS, clinic_master.REGISTERING_DATE, clinic_master.SYMPTOM, clinic_master.REGIST_FEE, clinic_master.CLINIC_FEE, clinic_master.OTHER_FEE, clinic_master.CLINIC_CHARGE, clinic_master.OPERATOR, clinic_master.BILLNO, clinic_master.CHARGE_TYPE_CODE, clinic_master.INVOICE_NEW, clinic_master.CARD_FEE, clinic_master.TERMINAL_FLAG };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }
        #endregion

        #region 获取挂号信息(按发票号)
        /// <summary>
        /// 获取挂号信息(按发票号)
        /// </summary>
        /// <param name="Db"></param>
        /// <param name="InvoiceNO"></param>
        /// <param name="master"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int GetMasterByInvoice(BaseEntityer Db, string InvoiceNO, ref CLINIC_MASTER master, ref string pErrMsg)
        {
            string sql =
               @"SELECT * FROM CLINIC_MASTER WHERE BILLNO='{0}'";
            DataSet ds = new DataSet();
            try
            {
                sql = string.Format(sql, InvoiceNO);
                ds = Db.GetDataSet(sql);
                if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    pErrMsg = "没有查询到消费信息。";
                    return -1;
                }
                master = DataSetToEntity.DataSetToT<CLINIC_MASTER>(ds).ToList()[0];
            }
            catch (Exception ex)
            {
                pErrMsg = ex.Message + "^" + sql;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 获取门诊结算明细信息(按发票号)
        /// <summary>
        /// 获取门诊结算明细信息(按发票号)
        /// </summary>
        /// <param name="Db"></param>
        /// <param name="InvoiceNO"></param>
        /// <param name="items"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int GetOutItemsByInvoice(BaseEntityer Db, string InvoiceNO, ref List<OUTP_BILL_ITEMS> items, ref string pErrMsg)
        {
            string sql =
               @"select * from outp_bill_items t where t.rcpt_no='{0}'";
            DataSet ds = new DataSet();
            try
            {
                sql = string.Format(sql, InvoiceNO);
                ds = Db.GetDataSet(sql);
                if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    pErrMsg = "没有查询到消费信息。";
                    return -1;
                }
                items = DataSetToEntity.DataSetToT<OUTP_BILL_ITEMS>(ds).ToList();
            }
            catch (Exception ex)
            {
                pErrMsg = ex.Message + "^" + sql;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// OUTP_RCPT_MASTER 表更新收费状态
        /// </summary>
        /// <param name="db"></param>
        /// <param name="rcptNo"></param>
        /// <param name="RetunRcptNp"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateOutpRcptMaster(BaseEntityer db, string rcptNo, string RetunRcptNp, int state)
        {
            string sql = @"update outp_rcpt_master t 
set t.charge_indicator='{1}',t.refunded_rcpt_no='{2}'
where t.rcpt_no='{0}'
";
            object[] os = new object[] { rcptNo, state, RetunRcptNp };
            sql = sql.SqlFormate(os);
            return db.ExecuteNonQuery(sql);
        }
        #endregion

        #region 插入对账结算信息
        /// <summary>
        /// 插入对账结算信息
        /// </summary>
        /// <param name="Db"></param>
        /// <param name="accbal"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        public int InsertAccountBalance(BaseEntityer Db, ONECARD_ACCBAL accbal, ref string pErrMsg)
        {
            int IsSuccess = 0;
            string sql = @"INSERT INTO ONECARD_ACCBAL
                                  (ID,
                                   BALANCE_OPER,
                                   BEGIN_DATE,
                                   END_DATE,
                                   MAIN_TYPE,
                                   INFORMATION,
                                   OPER_CODE,
                                   OPER_DATE,
                                   CHECK_FLAG,
                                   CHECK_OPER,
                                   CHECK_DATE,
                                   RECHARGE_TYPE)
                                VALUES
                                  ('{0}',--ID
                                   '{1}',--BALANCE_OPER
                                   to_date('{2}','yyyy-mm-dd hh24:mi:ss'),--BEGIN_DATE
                                   to_date('{3}','yyyy-mm-dd hh24:mi:ss'),--END_DATE
                                   '{4}',--MAIN_TYPE
                                   '{5}',--INFORMATION
                                   '{6}',--OPER_CODE
                                   to_date('{7}','yyyy-mm-dd hh24:mi:ss'),--OPER_DATE
                                   '{8}',--CHECK_FLAG
                                   '{9}',--CHECK_OPER
                                   to_date('{10}','yyyy-mm-dd hh24:mi:ss')--CHECK_DATE
                                   ,'{11}'
                                )";
            try
            {
                sql = string.Format(sql,
                                  accbal.ID,//0
                                  accbal.BALANCE_OPER,//1
                                  accbal.BEGIN_DATE,//2
                                  accbal.END_DATE,//3
                                  accbal.MAIN_TYPE,//4
                                  accbal.INFORMATION,//5
                                  accbal.OPER_CODE,//6
                                  accbal.OPER_DATE,//7
                                  accbal.CHECK_FLAG,//8
                                  accbal.CHECK_OPER,//9
                                  accbal.CHECK_DATE,//10
                                  accbal.RECHARGE_TYPE);
                IsSuccess = Db.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                pErrMsg = e.Message + "^" + sql;
                return -1;
            }
            return IsSuccess;
        }
        #endregion

        #region 一卡通接口

        /// <summary>
        /// 根据编号获取接口配置信息
        /// </summary>
        /// <param name="CardID">编号</param>
        /// <returns></returns>
        public HisCommon.BringObject GetInterfaceMessage(int CardID)
        {
            string strSQL = string.Empty;
            strSQL = @" SELECT t.CARDID, --编码
                               t.CARDNAME, --一卡通名称
                               t.DLLNAME, --动态库名称
                               t.DESCRIBE --描述
                          FROM ONECARD_CONFIG t --一卡通配置
                         WHERE t.cardid = '{0}' ";
            strSQL = string.Format(strSQL, CardID);

            HisCommon.BringObject obj = new BringObject();

            System.Data.Common.DbDataReader dr = BaseEntityer.Db.ZDExecReader(strSQL);
            while (dr.Read())
            {
                obj.Id = CardID.ToString();
                obj.Name = dr[1].ToString();
                obj.Memo = dr[2].ToString();
                obj.Exp01 = dr[3].ToString();
            }
            if (!dr.IsClosed)
                dr.Close();
            return obj;
        }

        /// <summary>
        /// 患者ID获取一卡通患者信息
        /// </summary>
        /// <param name="patientID">患者ID</param>
        /// <param name="oneCard_PatientInfo">患者信息</param>
        /// <param name="pErrMsg">错误信息</param>
        /// <returns></returns>
        public int GetPatientInfoByPatientID(string patientID, ref OneCard_PatientInfo oneCard_PatientInfo, ref string pErrMsg)
        {
            string strSQL = string.Empty;
            int result = 0;

            #region SQL
            strSQL = @" SELECT CARD_ID,
                               CARD_SERIAL,
                               SERIAL_NO,
                               NAME,
                               PASSWORD,
                               BIRTHDAY,
                               AGE,
                               SEX,
                               ID_CARD,
                               HOME_ADD,
                               LINK_PERSON,
                               PHONE,
                               OUT_PATIENTID,
                               IN_PATIENTID,
                               ACC_OUTBALANCE,
                               ACC_INBALANCE,
                               CARD_TYPE,
                               CARD_STATE,
                               YH_CARDNO,
                               HIS_BUSSINESSNO,
                               BANK_BUSSINESSNO,
                               OPER_ID,
                               OPER_NAME,
                               OPER_DATE,
                               CASH_PLEDGE
                          FROM ONECARD_PATIENTINFO
                         WHERE OUT_PATIENTID = '{0}' ";
            #endregion

            try
            {
                strSQL = string.Format(strSQL, patientID);

                DataSet ds = BaseEntityer.Db.GetDataSet(strSQL);
                List<OneCard_PatientInfo> listPatientInfo = DataSetToEntity.DataSetToT<OneCard_PatientInfo>(ds).ToList();
                if (listPatientInfo.Count > 0)
                {
                    oneCard_PatientInfo = listPatientInfo[0];
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                pErrMsg = ex.Message;
                result = -1;
            }
            return result;
        }

        #endregion
    }
}