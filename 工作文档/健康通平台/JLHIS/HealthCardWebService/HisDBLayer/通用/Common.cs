using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HisDBLayer;
using System.Data;
using HisCommon.DataEntity;
using HisCommon;
using System.Data.Common;

using System.Web.Script.Serialization;
using System.Collections;




/// <summary>
/// [功能描述: 通用功能类]<br></br>
/// [创 建 者: 王荣斌]<br></br>
/// [创建时间: 2012-07-12]<br></br>
/// <修改>
///		<修改人></修改人>
///		<修改时间></修改时间>
///		<修改说明></修改说明>
/// </修改>
/// </summary> 
namespace HisDBLayer
{
    public static class Common
    {    
        #region  dlq 2014-7-1 快速度 查询  JOSN   

        public static int ExecuteNonQuery(string sql)
        {
            int rows = BaseEntityer.Db.ExecuteNonQuery(sql);
            return rows;
        }

        public static bool ExcetueNoQuery(List<string> sqls)
        {
          BaseEntityer db=  BaseEntityer.Db;
          db.BeginTransaction();
            bool isSuccess=true;
            try
            {
                foreach (string sql in sqls)
                {
                    db.ExecuteNonQuery(sql);
                }
            }
            catch
            {
                db.RollbackTransaction();
                isSuccess= false;
            }
            db.CommitTransaction();
            return isSuccess;
        }

        /// <summary>
        /// 文件名:DataTable 和Json 字符串互转
        /// </summary> 
        #region DataTable 转换为Json 字符串
        /// <summary>
        /// DataTable 对象 转换为Json 字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(DataTable dt)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }

            return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
        }
        #endregion

        #region Json 字符串 转换为 DataTable数据集合
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
        #endregion



        #region  通用
        public static DataTable Query(string sql)
        {

            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        ///  获得表数据信息通过链接字符串
        /// </summary>
        /// <param name="conString"></param>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataTable ZDGetDataTable(string conString, string strSQL)
        {
            return BaseEntityer.Db.ZDGetDataTable(conString, strSQL);
        }

        public static DataSet QueryDataSet(string sql)
        {
            return BaseEntityer.Db.GetDataSet(sql);
        }

        public static string QueryTOJson(string sql)
        {
            return ToJson(BaseEntityer.Db.GetDataTable(sql));
        }




        public static string GetString(string obj)
        {


            if (obj == null)
            {
                obj = "";
            }

            return obj;

        }
        #endregion
        #region  辽源医保
        public static DataTable LYQuery(string sql)
        {

            return BaseEntityerToDFYB.Db.GetDataTable(sql);
        }

        
        #endregion
      #endregion

        

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="NewPassWord"></param>
        /// <returns></returns>
        public static int UpdatePassWord(string UserId, string NewPassWord)
        {
            string sql = @"update users_staff_dict u
set u.user_pass='{1}'
where u.user_id='{0}' ";
            sql = sql.SqlFormate(UserId, NewPassWord);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 根据用户id查询用户所在的权限组
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>返回用户权限组列表</returns>
        public static List<HisCommon.DataEntity.USERS_GROUP_DICT> GetUserGroupByUser(string userId)
        {
            //2013-12-31 by li 权限组查询排除PB老系统中权限组，只用于新系统权限组登录
            string sql = @"select *
                              from USERS_GROUP_DICT t
                             where t.userid = '{0}'
                               and t.menu_group in (SELECT distinct m.MENU_GROUP
                                                      FROM menu_group m
                                                     right join MENU_GROUP_DICT_NEW a
                                                        on m.menu_group = a.mnu_group)";
            sql = string.Format(sql, userId);
            System.Data.DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<USERS_GROUP_DICT>(ds).ToList();
            return list;
        }
        /// <summary>
        /// 查询所有用户所在的权限组
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>返回用户权限组列表</returns>
        public static List<HisCommon.DataEntity.USERS_STAFF_DICT> GetUserGroupALL()
        {
            string sql = @"select * from USERS_STAFF_DICT t";
            System.Data.DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<USERS_STAFF_DICT>(ds).ToList();
            return list;
        }
        /// <summary>
        /// 根据权限组名称查询具体菜单列表
        /// </summary>
        /// <param name="userId">权限组名称</param>
        /// <returns>返回权限组对应功能菜单列表</returns>
        public static List<HisCommon.DataEntity.MENU_GROUP_DICT_NEW> GetMenuByGroup(string menuGroup)
        {
            string sql = @"select t.*,m.mnu_tag from menu_group_dict_new t
                            left join menu_module m 
                            on t.module_name =m.module_name
                            and t.mnu_name=m.mnu_name
                            and t.mnu_win=m.mnu_win
                            where t.mnu_group='{0}'
                            order by t.mnu_col";
            sql = string.Format(sql, menuGroup);
            System.Data.DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<MENU_GROUP_DICT_NEW>(ds).ToList();
            return list;
        }
        /// <summary>
        /// 根据权限组名称查询具体菜单列表,新的权限系统
        /// </summary>
        /// <param name="userId">权限组名称</param>
        /// <returns>返回权限组对应功能菜单列表</returns>
        public static List<HisCommon.DataEntity.MENU_GROUP_DICT_NEW> GetNewMenuByGroup(string menuGroup)
        {
            string sql = @"select * from menu_group_dict_new t
where t.mnu_group='{0}'
order by t.mnu_col";
            sql = string.Format(sql, menuGroup);
            System.Data.DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<MENU_GROUP_DICT_NEW>(ds).ToList();
            return list;
        }
        /// <summary>
        ///根据登录编号，查找操作员
        /// </summary>
        /// <returns></returns>
        public static HisCommon.DataEntity.USERS_STAFF_DICT GetCurrentStaff(string userId)
        {
            //2013-11-18 by li 禁用人员登录无效修改
            string sql = @"select user_id,
                           user_name,
                           user_dept,
                           create_date,
                           job,
                           title,
                           menu_group,
                           user_pass,
                           dis,
                           toxi
                      from users_staff_dict
                     where user_id = '{0}'
                       and (dis = '0' or dis is null)";
            sql = string.Format(sql, userId);
            System.Data.DataSet ds = BaseEntityer.Db.GetDataSet(sql);

            var list = DataSetToEntity.DataSetToT<USERS_STAFF_DICT>(ds);
            if (list.Count > 0)
                return list[0];
            else return null;
        }

        /// <summary>
        /// 返回所有部门
        /// </summary>
        /// <returns></returns>
        public static IList<HisCommon.DataEntity.DEPT_DICT> getAllDept()
        {
            System.Data.DataSet ds = BaseEntityer.Db.GetDataSet(@"Select serial_no,
       dept_code,
       dept_name,
       dept_alias,
       clinic_attr,
       outp_or_inp,
       internal_or_sergery,
       input_code
  from DEPT_DICT ");
            var list = DataSetToEntity.DataSetToT<DEPT_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 获得科室字典
        /// </summary>
        /// <returns></returns>
        public static IList<DEPT_DICT> GetDeptIDAndNameDict()
        {
            //2013-12-1 by li 默认执行科室及核算科室增加
            string sql = @"SELECT dept_dict.dept_code,
                               dept_dict.dept_name
                          FROM dept_dict
                         ORDER BY dept_dict.dept_code
                        ";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<DEPT_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 返回所有门诊部门非临床科室
        /// </summary>
        /// <returns></returns>
        public static IList<HisCommon.DataEntity.DEPT_DICT> getAllOutPDept()
        {
            System.Data.DataSet ds = BaseEntityer.Db.GetDataSet(@"Select serial_no,
                           dept_code,
                           dept_name,
                           dept_alias,
                           clinic_attr,
                           outp_or_inp,
                           internal_or_sergery,
                           input_code
                      from DEPT_DICT WHERE outp_or_inp in(0,2) and  clinic_attr =0 ");
            var list = DataSetToEntity.DataSetToT<DEPT_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }
        /// <summary>
        /// 返回医生职称字典
        /// </summary>
        /// <returns></returns>
        public static IList<HisCommon.DataEntity.DOCTOR_TITLE_DICT> getDoctorTitle()
        {
            System.Data.DataSet ds = BaseEntityer.Db.GetDataSet(@"select DOCTOR_TITLE_DICT.Serial_No,
                   DOCTOR_TITLE_DICT.TITLE_CODE,
                   DOCTOR_TITLE_DICT.TITLE_NAME,
                   DOCTOR_TITLE_DICT.INPUT_CODE from DOCTOR_TITLE_DICT");
            var list = DataSetToEntity.DataSetToT<DOCTOR_TITLE_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }
        /// <summary>
        /// 得到所有人员信息，包括部门名称
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllUserStaff()
        {
            //2014-4-16 by li 全人员信息查询增加权限及对应科室字段
            string sql = @"select a.menu_group,
                               a.group_dept,
                               u.*,
                               F_TRANS_PINYIN_CAPITAL(u.user_name) name_spell,
                               d.dept_name,
                               d.input_code
                          from users_staff_dict u
                          left join dept_dict d
                            on u.user_dept = d.dept_code
                          left join users_group_dict a
                            on u.user_id = a.userid
                         order by d.dept_name";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 得到所有人员信息，包括部门名称
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllUserStaffNoExistSign()
        {
            //2014-4-16 by li 全人员信息查询增加权限及对应科室字段
            string sql = @"SELECT a.menu_group,
       a.group_dept,
       u.user_id,
       u.user_name,
       u.user_dept,
       u.create_date,
       u.job,
       u.title,
       u.is_cashier,
       u.user_pass,
       u.dis,
       u.toxi,
       u.certificate_code,
       u.sex,
       u.doctor_type,
       u.job_title,
       u.professional_titile,
       u.school,
       u.id_card,
       u.profess_type,
       u.profess_range,
       u.certificate_date,
       u.primary_illness,
       u.hos_staff_type,
       u.star_level,
       u.certificate_place,
       f_trans_pinyin_capital(u.user_name) name_spell,
       d.dept_name,
       d.input_code
  FROM users_staff_dict u
  LEFT JOIN dept_dict d
    ON u.user_dept = d.dept_code
  LEFT JOIN users_group_dict a
    ON u.user_id = a.userid
 ORDER BY d.dept_name
";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 返回数据库时间
        /// </summary>
        /// <returns></returns>
        public static DateTime getServerTime()
        {
            System.Data.DataTable dt = BaseEntityer.Db.GetDataTable(@"select sysdate from dual");
            DateTime time = new DateTime();
            if (dt != null)
                time = DateTime.Parse(dt.Rows[0][0].ToString());
            return time;
        }
        /// <summary>
        /// 字典
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDict(HisCommon.Enum.AllDictionary d)
        {
            string sql = "";
            switch (d)
            {
                case HisCommon.Enum.AllDictionary.剂量单位:
                    sql = @"SELECT DOSAGE_UNITS_DICT.DOSAGE_UNITS as display,   
         DOSAGE_UNITS_DICT.INPUT_CODE  
    FROM DOSAGE_UNITS_DICT ";
                    break;
                case HisCommon.Enum.AllDictionary.频次:
                    sql = @"SELECT PERFORM_FREQ_DICT.SERIAL_NO,
       replace( PERFORM_FREQ_DICT.FREQ_DESC ,'''','分') as display,
       PERFORM_FREQ_DICT.FREQ_COUNTER,
       PERFORM_FREQ_DICT.FREQ_INTERVAL,
       PERFORM_FREQ_DICT.FREQ_INTERVAL_UNITS
  FROM PERFORM_FREQ_DICT ";
                    break;
                case HisCommon.Enum.AllDictionary.用法:
                    sql = @"SELECT ADMINISTRATION_NAME as display, SERIAL_NO, ADMINISTRATION_CODE, INPUT_CODE
  FROM ADMINISTRATION_DICT ORDER BY SERIAL_NO";
                    break;
                case HisCommon.Enum.AllDictionary.科室属性:
                    sql = @"select t.clinic_attr_code as code,
      t.clinic_attr_name as display from  DEPT_CLINIC_ATTR_DICT t";
                    break;
                case HisCommon.Enum.AllDictionary.项目类型:
                    sql = @"select t.type_code as code,t.type_name as name from ITEM_TYPE t";
                    break;
            }
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 根据字典名称查询字典明细
        /// </summary>
        /// <param name="d">字典枚举</param>
        /// <param name="pinYinCode">拼音码</param>
        /// <param name="itemName">项目名称部分</param>
        /// <returns>返回指定字典明细</returns>
        public static DataTable GetDictionary(HisCommon.Enum.AllDictionary d, string pinYinCode, string itemName)
        {
            string sql = "";
            string tableName = "";
            string tableItemName = "";
            switch (d)
            {
                case HisCommon.Enum.AllDictionary.国家及地区字典:
                    sql = @"SELECT COUNTRY_DICT.INPUT_CODE AS 输入码,
                           COUNTRY_DICT.COUNTRY_NAME AS 国家简称,
                           COUNTRY_DICT.COUNTRY_CODE AS 国家代码,
                           COUNTRY_DICT.SERIAL_NO FROM COUNTRY_DICT";
                    tableName = "COUNTRY_DICT";
                    tableItemName = "COUNTRY_NAME";
                    break;
                case HisCommon.Enum.AllDictionary.行政区字典:
                    sql = @"SELECT AREA_DICT.INPUT_CODE AS 输入码,
                           AREA_DICT.AREA_NAME AS 地区名称,
                           AREA_DICT.AREA_CODE AS 地区代码,
                           AREA_DICT.SERIAL_NO from AREA_DICT";
                    tableName = "AREA_DICT";
                    tableItemName = "AREA_NAME";
                    break;
                case HisCommon.Enum.AllDictionary.民族字典:
                    sql = @"SELECT NATION_DICT.INPUT_CODE AS 输入码,
                           NATION_DICT.NATION_NAME AS 民族名称,
                           NATION_DICT.NATION_CODE AS 民族代码,
                           NATION_DICT.SERIAL_NO FROM NATION_DICT";
                    tableName = "NATION_DICT";
                    tableItemName = "NATION_NAME";
                    break;
            }
            if (pinYinCode != null)
            {
                sql += " WHERE " + tableName + ".INPUT_CODE like '%{0}%'";
            }
            else if (itemName != null)
            {
                sql += " WHERE " + tableName + "." + tableItemName + " like '%{1}%'";
            }
            sql += " ORDER BY " + tableName + ".SERIAL_NO";
            sql = string.Format(sql, pinYinCode, itemName);
            return BaseEntityer.Db.GetDataTable(sql);

        }
        /// <summary>
        /// 得到序列号
        /// </summary>
        /// <returns></returns>
        public static string GetSequence(string seqName)
        {
            string sql = "SELECT {0}.NEXTVAL From dual";
            string seq = "";
            sql = string.Format(sql, seqName);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    seq = dt.Rows[0][0].ToString();
            return seq;

        }
        /// <summary>
        /// 得到医院名称
        /// </summary>
        /// <returns></returns>
        public static string GetHospitalName()
        {
            string sql = "select t.hospital from hospital_config t";
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    return dt.Rows[0][0].ToString();

            return string.Empty;
        }
        /// <summary>
        /// 项目类型字典
        /// </summary>
        public static Dictionary<string, string> DictItemType()
        {
            string sql = "SELECT * From item_type";
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            return dt.AsEnumerable().ToDictionary((DataRow dr) => dr["type_code"].ToString(), (DataRow dr) => dr["type_name"].ToString());

        }
        /// <summary>
        /// 核算科目字典
        /// </summary>
        public static Dictionary<string, string> DictSubj()
        {
            string sql = @"SELECT t.SUBJ_CODE,
               t.SUBJ_NAME
          FROM TALLY_SUBJECT_DICT t ORDER BY t.SUBJ_CODE";
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            return dt.AsEnumerable().ToDictionary((DataRow dr) => dr["SUBJ_CODE"].ToString(), (DataRow dr) => dr["SUBJ_NAME"].ToString());

        }
        /// <summary>
        /// 支付方式字典
        /// </summary>
        public static List<PAY_WAY_DICT> DictPayWay()
        {
            string sql = "SELECT * From pay_way_dict";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<PAY_WAY_DICT>(ds).ToList();
            return list;
        }
        /// <summary>
        /// 获取系统错误日志记录
        /// </summary>
        public static DataTable GetErrorLog(string ip, string dateStart, string dateEnd)
        {
            string sql = @"select * from SYS_LOG t 
where (t.clientip like '%{0}%' or '{0}' is null)
and t.logtime>=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
and t.logtime<=to_date('{2}','yyyy-MM-dd hh24:mi:ss')
order by t.logtime desc";

            sql = sql.SqlFormate(ip, dateStart, dateEnd);
            return BaseEntityer.Db.GetDataTable(sql);
        }



        #region---------------系统配置值---------------
        /// <summary>
        /// 读取系统配置
        /// </summary>
        /// <param name="parmName"></param>
        /// <returns></returns>
        public static string GetSysConfig(string parmName)
        {
            try
            {
                //2013-7-1 by li 取参数值之前sql语句出错
                string sql = @"select t.param_value from sys_param t where t.param_name='{0}'";
                string seq = "";
                sql = string.Format(sql, parmName);
                DataTable dt = BaseEntityer.Db.GetDataTable(sql);
                if (dt != null)
                    if (dt.Rows.Count > 0)
                        seq = dt.Rows[0][0].ToString();
                return seq;
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 读取全部系统配置
        /// </summary>
        /// <returns></returns>
        public static List<HisCommon.DataEntity.SYS_PARAM> GetAllSysConfig()
        {
            string sql = @"select * from sys_param t ";
            var ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<SYS_PARAM>(ds).ToList();
            if (list.Count > 0)
                return list;
            else return null;
        }
        /// <summary>
        /// 读取sys_config
        /// </summary>
        /// <returns></returns>
        public static List<HisCommon.DataEntity.SYS_CONFIG> GetRunTimeSysConfig()
        {
            string sql = @"select t.*,F_TRANS_PINYIN_CAPITAL(t.CONFIG_NAME) NAME_SPELL from sys_config t ";
            var ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<SYS_CONFIG>(ds).ToList();
            if (list.Count > 0)
                return list;
            else return null;
        }
        /// <summary>
        /// 返回默认查询多长时间刷新一次,单位ms
        /// </summary>
        /// <returns></returns>
        public static int GetRefreshTimePatient()
        {
            return 30000;
        }
        #endregion

        /// <summary>
        /// 读取sys_config
        /// </summary>
        /// <param name="parmName"></param>
        /// <returns></returns>
        public static string GetSysPBL(string PACT_CODE, string CONFIG_NAME)
        {
            //2013-10-9 by yu 取参数值之前sql语句出错
            string sql = @"select t.PBL from sys_config t 
where t.PACT_CODE='{0}' and t.CONFIG_NAME='{1}'";
            string seq = "";
            sql = string.Format(sql, PACT_CODE, CONFIG_NAME);
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    seq = dt.Rows[0][0].ToString();
            return seq;
        }

        /// <summary>
        /// 2014-1-2 by li 人员字典(包含权限)
        /// </summary>
        /// <returns></returns>
        public static IList<USERS_STAFF_DICT> GetUserDict()
        {
            string sql = @"SELECT distinct users_staff_dict.user_id ,
                           users_staff_dict.user_name ,
                           users_staff_dict.user_dept ,
                           users_staff_dict.create_date ,
                           users_staff_dict.job ,
                           users_staff_dict.title ,
                           a.menu_group ,
                           users_staff_dict.user_pass ,
                           users_staff_dict.dis ,
                           users_staff_dict.toxi ,
                           users_staff_dict.certificate_code,
                           users_staff_dict.sex,
                           users_staff_dict.doctor_type,
                           users_staff_dict.job_title,
                           users_staff_dict.professional_titile,
                           users_staff_dict.school,
                           users_staff_dict.id_card,
                           users_staff_dict.profess_type,
                           users_staff_dict.profess_range,
                           users_staff_dict.certificate_date,
                           users_staff_dict.primary_illness,
                           users_staff_dict.hos_staff_type,
                           users_staff_dict.star_level,
                           users_staff_dict.certificate_place,
                           users_staff_dict.oper_code ,
                           users_staff_dict.IS_CASHIER , a.group_dept, a.flag,
                           F_TRANS_PINYIN_CAPITAL(users_staff_dict.user_name) name_spell 
                           FROM users_staff_dict left join USERS_GROUP_DICT a 
                           on users_staff_dict.user_id = a.userid 
                           ORDER BY users_staff_dict.user_id";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<USERS_STAFF_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <returns></returns>
        public static List<HisCommon.BringObject> GetDictionaryList()
        {
            List<HisCommon.BringObject> list = new List<BringObject>();
            string strSQL = @" select t.type,t.name From com_dictionary_type t order by t.name";
            DbDataReader dr = BaseEntityer.Db.ExecuteReader(strSQL, 0);
            while (dr.Read())
            {
                HisCommon.BringObject obj = new BringObject();
                obj.Id = dr[0].ToString();
                obj.Name = dr[1].ToString();
                list.Add(obj);
            }
            if (!dr.IsClosed)
                dr.Close();
            return list;
        }

        /// <summary>
        /// 获取字典详细
        /// </summary>
        /// <returns></returns>
        public static List<HisCommon.BringObject> GetDictionaryDetail(string type)
        {
            List<HisCommon.BringObject> detail = new List<BringObject>();
            string strSQL = @" select t.type,
                               t.code,
                               t.name,
                               t.mark,
                               t.spell_code,
                               t.wb_code,
                               t.input_code,
                               t.sort_id,
                               t.oper_code,
                               t.oper_date,
                               t.valid_state
                          From com_dictionary t
                         where t.valid_state = '1'
                           and t.type = '{0}' 
                           order by t.sort_id ";
            strSQL = string.Format(strSQL, type);
            DbDataReader dr = BaseEntityer.Db.ExecuteReader(strSQL, 0);
            while (dr.Read())
            {
                HisCommon.BringObject obj = new BringObject();
                obj.Id = dr[1].ToString();
                obj.Name = dr[2].ToString();
                obj.Memo = dr[3].ToString();
                obj.SpellCode = dr[4].ToString();
                obj.WbCode = dr[5].ToString();
                obj.UserCode = dr[6].ToString();
                obj.Exp01 = dr[8].ToString();
                obj.Exp02 = dr[9].ToString();
                obj.Exp03 = dr[10].ToString();
                obj.Exp04 = Convert.ToInt32(dr[7].ToString());
                detail.Add(obj);
            }
            if (!dr.IsClosed)
                dr.Close();
            return detail;
        }

        public static HisCommon.BringObject GetDictionaryDetail2(string type, string code)
        {
            HisCommon.BringObject bo = new BringObject();
            string strSQL = @" select t.type,
                                       t.code,
                                       t.name,
                                       t.mark,
                                       t.spell_code,
                                       t.wb_code,
                                       t.input_code,
                                       t.sort_id,
                                       t.oper_code,
                                       t.oper_date,
                                       t.valid_state
                                  From com_dictionary t
                                 where t.valid_state = '1'
                                   and t.type = '{0}'
                                   and t.code = '{1}'
                                 order by t.sort_id ";

            strSQL = string.Format(strSQL, type, code);
            DbDataReader dr = BaseEntityer.Db.ExecuteReader(strSQL, 0);
            while (dr.Read())
            {
                bo.Id = dr[1].ToString();
                bo.Name = dr[2].ToString();
                bo.Memo = dr[3].ToString();
                bo.SpellCode = dr[4].ToString();
                bo.WbCode = dr[5].ToString();
                bo.UserCode = dr[6].ToString();
                bo.Exp01 = dr[8].ToString();
                bo.Exp02 = dr[9].ToString();
                bo.Exp03 = dr[10].ToString();
                bo.Exp04 = Convert.ToInt32(dr[7].ToString());
            }
            if (!dr.IsClosed)
                dr.Close();
            return bo;
        }

        /// <summary>
        /// 获取拼音码和五笔码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HisCommon.BringObject GetSpellAndWBCode(string name)
        {
            HisCommon.BringObject obj = new BringObject();
            string strSQL = string.Empty;

            for (int i = 0; i < name.Length; i++)
            {
                string word = name.Substring(i,1);
                strSQL = @" select spell_code,wb_code from com_spellbase where name = '{0}' ";
                strSQL = string.Format(strSQL, word);
                DbDataReader dr = BaseEntityer.Db.ExecuteReader(strSQL, 0);
                while(dr.Read())
                {
                    obj.SpellCode += dr[0].ToString();
                    obj.WbCode += dr[1].ToString();
                }
                if (!dr.IsClosed)
                    dr.Close();
            }
            return obj;
        }

        /// <summary>
        /// 添加种类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int DictionaryAddType(HisCommon.BringObject type)
        {
            
            string strSQL = string.Empty;
            strSQL = @" insert into com_dictionary_type
                      (type, name, oper_code, oper_date)
                    values
                      ('{0}', '{1}', '{2}', sysdate) ";
            strSQL = string.Format(strSQL, type.Id, type.Name, type.Memo);
            return BaseEntityer.Db.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 删除种类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int DictionaryDeleteType(HisCommon.BringObject type)
        {
            string strSQL = string.Empty;
            strSQL = " delete from com_dictionary_type where type = '{0}' and name = '{1}' ";
            strSQL = string.Format(strSQL, type.Id, type.Name);
            return BaseEntityer.Db.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 添加种类明细
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int DictionaryAddDetail(HisCommon.BringObject type)
        {
            string strSQL = string.Empty;
            strSQL = @" insert into com_dictionary
                          (type,
                           code,
                           name,
                           mark,
                           spell_code,
                           wb_code,
                           input_code,
                           sort_id,
                           oper_code,
                           oper_date,
                           valid_state)
                        values
                          ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', sysdate,'{9}') ";
            strSQL = string.Format(strSQL, type.Id, type.Name, type.Memo, type.Exp01, type.SpellCode, type.WbCode, type.UserCode, type.Exp04, type.Exp02, type.Exp03);
            return BaseEntityer.Db.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 删除种类明细
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int DictionaryDeleteDetail(HisCommon.BringObject type)
        {
            string strSQL = string.Empty;
            strSQL = " delete from com_dictionary where type = '{0}' and code = '{1}' ";
            strSQL = string.Format(strSQL, type.Id, type.Name);
            return BaseEntityer.Db.ExecuteNonQuery(strSQL);
        }

        #region 打印纸张设置

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static int ComPageInsert(HisCommon.DataEntity.COM_PAGE page)
        {
            string strSQL = string.Empty;
            strSQL = @" INSERT into COM_PAGE t --打印纸张设置
                      (t.PAGE_NAME, --纸张名称
                       t.LEFT, --左边距
                       t.TOP, --上边距
                       t.HEIGHT, --宽
                       t.WIDTH, --高
                       t.OPER_CODE, --操作人
                       t.OPER_DATE --操作日期
                       )
                    VALUES
                      ('{0}', --纸张名称
                       '{1}', --左边距
                       '{2}', --上边距
                       '{3}', --宽
                       '{4}', --高
                       '{5}', --操作人
                       TO_DATE('{6}', 'YYYY-MM-DD HH24:MI:SS') --操作日期
                       ) ";
            strSQL = string.Format(strSQL, page.Page_name, page.Left, page.Top, page.Height, page.Width, page.Oper_code, page.Oper_date);
            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static int ComPageDelete(HisCommon.DataEntity.COM_PAGE page)
        {
            string strSQL = string.Empty;
            strSQL = @" delete from COM_PAGE where PAGE_NAME = '{0}' ";
            strSQL = string.Format(strSQL, page.Page_name);
            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 获取纸张
        /// </summary>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public static HisCommon.DataEntity.COM_PAGE GetPage(string pageName)
        {
            HisCommon.DataEntity.COM_PAGE page = new COM_PAGE();
            string strSQL = string.Empty;
            strSQL = @" SELECT t.PAGE_NAME, --纸张名称
                               t.LEFT, --左边距
                               t.TOP, --上边距
                               t.HEIGHT, --宽
                               t.WIDTH, --高
                               t.OPER_CODE, --操作人
                               t.OPER_DATE --操作日期
                          FROM COM_PAGE t --打印纸张设置
                         WHERE t.page_name = '{0}' ";
            strSQL = string.Format(strSQL, pageName);

            DbDataReader dr = BaseEntityer.Db.ZDExecReader(strSQL);

            while (dr.Read())
            {
                page.Page_name = dr[0].ToString();
                page.Left = int.Parse(dr[1].ToString());
                page.Top = int.Parse(dr[2].ToString());
                page.Height = int.Parse(dr[3].ToString());
                page.Width = int.Parse(dr[4].ToString());
                page.Oper_code = dr[5].ToString();
                page.Oper_date = Convert.ToDateTime(dr[6].ToString());
            }
            if (!dr.IsClosed)
                dr.Close();
            return page;
        }

        #endregion

        /// <summary>
        /// 根据操作员编号获取操作员姓名
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static string GetUserNameByID(string UserID)
        {
            string strSQL = string.Empty;
            strSQL = @" select Fun_GetUserName('{0}') from dual ";
            strSQL = string.Format(strSQL, UserID);

            string userName = string.Empty;
            DbDataReader dbr = BaseEntityer.Db.ZDExecReader(strSQL);
            while(dbr.Read())
            {
                userName = dbr[0].ToString();
            }
            if (!dbr.IsClosed)
                dbr.Close();
            return userName;
        }

        /// <summary>
        /// 根据操作员编号获取操作员姓名
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static string GetDeptNameByID(string DeptID)
        {
            string strSQL = string.Empty;
            strSQL = @" select Fun_GetDeptName('{0}') from dual ";
            strSQL = string.Format(strSQL, DeptID);

            string deptName = string.Empty;
            DbDataReader dbr = BaseEntityer.Db.ZDExecReader(strSQL);
            while (dbr.Read())
            {
                deptName = dbr[0].ToString();
            }
            if (!dbr.IsClosed)
                dbr.Close();
            return deptName;
        }

        public static string GetSQL(string sqlID)
        {
            string strSQL = " select sqlContent from sys_com_sql t where t.sqlId = '{0}' ";
            strSQL = string.Format(strSQL, sqlID);

            string sqlContent = string.Empty;
            DbDataReader dbr = BaseEntityer.Db.ZDExecReader(strSQL);
            while (dbr.Read())
            {
                sqlContent = dbr[0].ToString();
            }
            if (!dbr.IsClosed)
                dbr.Close();
            return sqlContent;
        }
    }
}
