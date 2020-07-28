using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using HisCommon;
using HisCommon.DataEntity;
using System;
using System.Text;
using HisCommon.DataEntity.Report;
using System.Data.OracleClient;
using System.Collections;

namespace HisDBLayer
{
    /// <summary>
    /// 系统维护数据访问类
    /// </summary>
    public class SystemManagement
    {
        #region 权限管理

        /// <summary>
        /// 根据权限组获取权限组下菜单列表
        /// </summary>
        /// <param name="mnu_group">权限组名称</param>
        /// <returns></returns>
        public List<MENU_GROUP_DICT_NEW> GetMenuGroupDicts(BaseEntityer db, string mnu_group)
        {
            string sql = @"SELECT  MENU_GROUP_DICT_NEW.mnu_group ,
                            MENU_GROUP_DICT_NEW.mnu_col ,
                            MENU_GROUP_DICT_NEW.mnu_row ,
                            MENU_GROUP_DICT_NEW.mnu_parm ,
                            MENU_GROUP_DICT_NEW.mnu_name ,
                            MENU_GROUP_DICT_NEW.module_name ,
                            MENU_GROUP_DICT_NEW.mnu_win ,
                            MENU_GROUP_DICT_NEW.MNU_TYPE ,
                            MENU_GROUP_DICT_NEW.MNU_TAG 
                            FROM MENU_GROUP_DICT_NEW      
                            WHERE (MENU_GROUP_DICT_NEW.MNU_GROUP = '{0}')";
            sql = string.Format(sql, mnu_group);
            DataSet ds = db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<MENU_GROUP_DICT_NEW>(ds).ToList();
        }

        /// <summary>
        /// 获取权限组列表
        /// </summary>
        /// <returns></returns>
        public List<MENU_GROUP> GetMenuGroup()
        {
            string sql = @"SELECT menu_group.MENU_GROUP as MENU_GROUP_NAME ,
                           menu_group.menu_group_desc ,
                           menu_group.module_name     
                           FROM menu_group";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<MENU_GROUP>(ds).ToList();
        }

        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="menu_group"></param>
        /// <returns></returns>
        public DbDataReader GetMenuGroupData(BaseEntityer db, MENU_GROUP menu_group)
        {
            string sql = @"SELECT * FROM MENU_GROUP 
                            where MENU_GROUP.MENU_GROUP = '{0}' and MENU_GROUP.MODULE_NAME = '{1}'";
            sql = string.Format(sql, menu_group.MENU_GROUP_NAME, menu_group.MODULE_NAME);
            return db.ExecuteReader(sql);
        }

        /// <summary>
        /// 新增权限组
        /// </summary>
        /// <param name="db"></param>
        /// <param name="menu_group"></param>
        /// <returns></returns>
        public int InsertMenuGroup(BaseEntityer db, MENU_GROUP menu_group)
        {
            string sql = @"INSERT INTO MENU_GROUP
                        (
                               MENU_GROUP.MENU_GROUP,
                               MENU_GROUP.MODULE_NAME,
                               MENU_GROUP.MENU_GROUP_DESC
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}'
                        )";
            object[] param = new object[] { menu_group.MENU_GROUP_NAME, menu_group.MODULE_NAME, 
                menu_group.MENU_GROUP_DESC };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定权限组数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="menu_group"></param>
        /// <returns></returns>
        public int DeleteMenuGroup(BaseEntityer db, string menu_group)
        {
            string sql = @"delete from MENU_GROUP where MENU_GROUP.MENU_GROUP = '{0}'";
            sql = string.Format(sql, menu_group);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 判断菜单数据是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="menu_group_dict_new"></param>
        /// <returns></returns>
        public DbDataReader GetMenuGroupDictData(BaseEntityer db, MENU_GROUP_DICT_NEW menu_group_dict_new)
        {
            string sql = @"SELECT * FROM MENU_GROUP_DICT_NEW 
                            where MENU_GROUP_DICT_NEW.MNU_GROUP = '{0}' 
                            and MENU_GROUP_DICT_NEW.MNU_COL = '{1}' and MENU_GROUP_DICT_NEW.MNU_ROW = '{2}'";
            sql = string.Format(sql, menu_group_dict_new.MNU_GROUP, menu_group_dict_new.MNU_COL,
                menu_group_dict_new.MNU_ROW);
            return db.ExecuteReader(sql);
        }

        /// <summary>
        /// 新增菜单项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="menu_group_dict_new"></param>
        /// <returns></returns>
        public int InsertMenuGroupDicts(BaseEntityer db, MENU_GROUP_DICT_NEW menu_group_dict_new)
        {
            string sql = @"INSERT INTO MENU_GROUP_DICT_NEW
                        (
                               MENU_GROUP_DICT_NEW.MNU_NAME,
                               MENU_GROUP_DICT_NEW.MNU_GROUP,
                               MENU_GROUP_DICT_NEW.MNU_ROW,
                               MENU_GROUP_DICT_NEW.MNU_COL,
                               MENU_GROUP_DICT_NEW.MNU_PARM,
                               MENU_GROUP_DICT_NEW.MNU_TYPE,
                               MENU_GROUP_DICT_NEW.MNU_WIN,
                               MENU_GROUP_DICT_NEW.MNU_TAG,
                               MENU_GROUP_DICT_NEW.MODULE_NAME
                        )
                        VALUES
                        (
                               '{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}','{8}'
                        )";
            object[] param = new object[] { menu_group_dict_new.MNU_NAME, menu_group_dict_new.MNU_GROUP, 
                menu_group_dict_new.MNU_ROW, menu_group_dict_new.MNU_COL, menu_group_dict_new.MNU_PARM, 
                menu_group_dict_new.MNU_TYPE, menu_group_dict_new.MNU_WIN, menu_group_dict_new.MNU_TAG, 
                menu_group_dict_new.MODULE_NAME };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新菜单项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dept_dict_new"></param>
        /// <param name="menu_group_dict_old"></param>
        /// <returns></returns>
        public int UpdateMenuGroupDicts(BaseEntityer db, MENU_GROUP_DICT_NEW menu_group_dict_new, MENU_GROUP_DICT_NEW menu_group_dict_old)
        {
            string sql = @"UPDATE MENU_GROUP_DICT_NEW 
                        SET 
                               MENU_GROUP_DICT_NEW.MNU_NAME = '{0}',
                               MENU_GROUP_DICT_NEW.MNU_GROUP = '{1}',
                               MENU_GROUP_DICT_NEW.MNU_ROW = {2},
                               MENU_GROUP_DICT_NEW.MNU_COL = {3},
                               MENU_GROUP_DICT_NEW.MNU_PARM = '{4}',
                               MENU_GROUP_DICT_NEW.MNU_TYPE = '{5}',
                               MENU_GROUP_DICT_NEW.MNU_WIN = '{6}',
                               MENU_GROUP_DICT_NEW.MNU_TAG = '{7}',
                               MENU_GROUP_DICT_NEW.MODULE_NAME = '{8}' 
                        WHERE MENU_GROUP_DICT_NEW.MNU_GROUP = '{9}' 
                            and MENU_GROUP_DICT_NEW.MNU_COL = '{10}' and MENU_GROUP_DICT_NEW.MNU_ROW = '{11}'";
            object[] param = new object[] { menu_group_dict_new.MNU_NAME, menu_group_dict_new.MNU_GROUP, 
                menu_group_dict_new.MNU_ROW, menu_group_dict_new.MNU_COL, menu_group_dict_new.MNU_PARM, 
                menu_group_dict_new.MNU_TYPE, menu_group_dict_new.MNU_WIN, menu_group_dict_new.MNU_TAG, 
                menu_group_dict_new.MODULE_NAME, menu_group_dict_old.MNU_GROUP, menu_group_dict_old.MNU_COL, 
                menu_group_dict_old.MNU_ROW };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定菜单数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="menu_group_dict_new"></param>
        /// <returns></returns>
        public int DeleteMenuGroupDicts(BaseEntityer db, MENU_GROUP_DICT_NEW menu_group_dict_new)
        {
            string sql = @"delete from MENU_GROUP_DICT_NEW where MENU_GROUP_DICT_NEW.MNU_GROUP = '{0}' 
                            and MENU_GROUP_DICT_NEW.MNU_COL = '{1}' and MENU_GROUP_DICT_NEW.MNU_ROW = '{2}'";
            sql = string.Format(sql, menu_group_dict_new.MNU_GROUP, menu_group_dict_new.MNU_COL,
                menu_group_dict_new.MNU_ROW);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取某菜单下级菜单最大row值
        /// </summary>
        /// <param name="mnu_group"></param>
        /// <param name="mnu_col"></param>
        /// <returns></returns>
        public int GetMaxMenuRow(string mnu_group, int mnu_col)
        {
            string sql = @"select max(MENU_GROUP_DICT_NEW.MNU_ROW) as MNU_ROW from MENU_GROUP_DICT_NEW 
                            where MENU_GROUP_DICT_NEW.MNU_GROUP = '{0}' and MENU_GROUP_DICT_NEW.MNU_COL = {1}";
            sql = string.Format(sql, mnu_group, mnu_col);
            DbDataReader reader = BaseEntityer.Db.ExecuteReader(sql);
            if (reader.Read())
            {
                if (reader["MNU_ROW"].ToString() != string.Empty)
                {
                    var tempValue = reader["MNU_ROW"].ToString();
                    if (!reader.IsClosed)
                        reader.Close();
                    return Convert.ToInt32(tempValue);
                }
                else
                {
                    if (!reader.IsClosed)
                        reader.Close();
                    return 0;
                }
            }
            else
            {
                if (!reader.IsClosed)
                    reader.Close();
                return 0;
            }
        }

        /// <summary>
        /// 获取某菜单下级菜单最大row值
        /// </summary>
        /// <param name="mnu_group"></param>
        /// <param name="mnu_col"></param>
        /// <returns></returns>
        public int GetMaxMenuCol(string mnu_group, int mnu_row)
        {
            string sql = @"select max(MENU_GROUP_DICT_NEW.MNU_COL) as MNU_COL from MENU_GROUP_DICT_NEW 
                            where MENU_GROUP_DICT_NEW.MNU_GROUP = '{0}' and MENU_GROUP_DICT_NEW.MNU_ROW = {1}";
            sql = string.Format(sql, mnu_group, mnu_row);
            DbDataReader reader = BaseEntityer.Db.ExecuteReader(sql);
            if (reader.Read())
            {
                if (reader["MNU_COL"].ToString() != string.Empty)
                {
                    int rev = Convert.ToInt32(reader["MNU_COL"].ToString());

                    if (!reader.IsClosed)
                        reader.Close();
                    return rev;
                }
                else
                {
                    if (!reader.IsClosed)
                        reader.Close();
                    return 0;
                }
            }
            else
            {
                if (!reader.IsClosed)
                    reader.Close();
                return 0;
            }
        }

        #endregion

        #region 字典维护

        /// <summary>
        /// 科室属性项目集合菜单列表
        /// </summary>
        /// <returns></returns>
        public IList<DEPT_CLINIC_ATTR_DICT> GetDeptClinicAttrDict()
        {
            string sql = @"SELECT  dept_clinic_attr_dict.serial_no ,
                            dept_clinic_attr_dict.clinic_attr_code ,
                            dept_clinic_attr_dict.clinic_attr_name ,
                            dept_clinic_attr_dict.input_code 
                            FROM dept_clinic_attr_dict ORDER BY dept_clinic_attr_dict.clinic_attr_code";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<DEPT_CLINIC_ATTR_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 内科外科标志项目集合菜单列表
        /// </summary>
        /// <returns></returns>
        public IList<DEPT_IS_ATTR_DICT> GetDeptIsAttrDict()
        {
            string sql = @"SELECT  dept_is_attr_dict.serial_no ,
                            dept_is_attr_dict.is_attr_code ,
                            dept_is_attr_dict.is_attr_name ,
                            dept_is_attr_dict.input_code 
                            FROM dept_is_attr_dict ORDER BY dept_is_attr_dict.is_attr_code";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<DEPT_IS_ATTR_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 门诊住院标志项目集合菜单列表
        /// </summary>
        /// <returns></returns>
        public IList<DEPT_OI_ATTR_DICT> GetDeptOiAttrDict()
        {
            string sql = @"SELECT  dept_oi_attr_dict.serial_no ,
                            dept_oi_attr_dict.oi_attr_code ,
                            dept_oi_attr_dict.oi_attr_name ,
                            dept_oi_attr_dict.input_code 
                            FROM dept_oi_attr_dict ORDER BY dept_oi_attr_dict.oi_attr_code";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<DEPT_OI_ATTR_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 门诊科室字典(维护用)
        /// </summary>
        /// <returns></returns>
        public IList<DEPT_DICT> GetDeptDict()
        {
            //2013-12-1 by li 默认执行科室及核算科室增加
            string sql = @"SELECT dept_dict.serial_no ,
                           dept_dict.dept_code ,
                           dept_dict.dept_name ,
                           dept_dict.dept_alias ,
                           dept_dict.clinic_attr ,
                           dept_dict.outp_or_inp ,
                           dept_dict.internal_or_sergery ,
                           dept_dict.input_code ,
                           dept_dict.NH_DEPT_CODE ,
                           dept_dict.DEFAULT_PERFORMED_BY ,
                           dept_dict.DEPT_CODE_RECKING ,
                           dept_dict.IS_SPICU 
                           FROM dept_dict ORDER BY dept_dict.dept_code";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<DEPT_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 农合科室对照字典
        /// </summary>
        /// <returns></returns>
        public IList<NH_DEPT_CODE> GetNHDeptDict()
        {
            string sql = @"select NH_DEPT_CODE.code,
                           NH_DEPT_CODE.name,
                           NH_DEPT_CODE.pycode,
                           NH_DEPT_CODE.remark
                      from NH_DEPT_CODE ORDER BY NH_DEPT_CODE.code";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<NH_DEPT_CODE>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 返回科室字典的最大号
        /// </summary>
        /// <returns></returns>
        public DbDataReader GetDeptSerialNo()
        {
            string sql = @"SELECT NVL(Max(serial_no),0) as serial_no 
                            from dept_dict ";
            return BaseEntityer.Db.ExecuteReader(sql);
        }

        /// <summary>
        /// 新增科室项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dept_dict_new"></param>
        /// <returns></returns>
        public int InsertDeptDict(BaseEntityer db, DEPT_DICT dept_dict_new)
        {
            string sql = @"INSERT INTO DEPT_DICT
                        (
                               DEPT_DICT.serial_no,
                               DEPT_DICT.dept_code,
                               DEPT_DICT.dept_name,
                               DEPT_DICT.dept_alias,
                               DEPT_DICT.clinic_attr,
                               DEPT_DICT.outp_or_inp,
                               DEPT_DICT.internal_or_sergery,
                               DEPT_DICT.input_code,
                               DEPT_DICT.NH_DEPT_CODE,
                               DEPT_DICT.DEFAULT_PERFORMED_BY,
                               DEPT_DICT.DEPT_CODE_RECKING,
                               DEPT_DICT.IS_SPICU
                        )
                        VALUES
                        (
                               {0},'{1}','{2}','{3}',{4},{5},{6},'{7}','{8}',{9},'{10}','{11}'
                        )";
            object[] param = new object[] { dept_dict_new.SERIAL_NO, dept_dict_new.DEPT_CODE, 
                dept_dict_new.DEPT_NAME, dept_dict_new.DEPT_ALIAS, dept_dict_new.CLINIC_ATTR, 
                dept_dict_new.OUTP_OR_INP, dept_dict_new.INTERNAL_OR_SERGERY, dept_dict_new.INPUT_CODE, 
                dept_dict_new.NH_DEPT_CODE, dept_dict_new.DEFAULT_PERFORMED_BY, dept_dict_new.DEPT_CODE_RECKING,dept_dict_new.IS_SPICU};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新科室项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dept_dict_new"></param>
        /// <param name="serial_no"></param>
        /// <returns></returns>
        public int UpdateDeptDict(BaseEntityer db, DEPT_DICT dept_dict_new, int serial_no)
        {
            string sql = @"UPDATE DEPT_DICT 
                        SET 
                               DEPT_DICT.serial_no = {0},
                               DEPT_DICT.dept_code = '{1}',
                               DEPT_DICT.dept_name = '{2}',
                               DEPT_DICT.dept_alias = '{3}',
                               DEPT_DICT.clinic_attr = {4},
                               DEPT_DICT.outp_or_inp = {5},
                               DEPT_DICT.internal_or_sergery = {6},
                               DEPT_DICT.input_code = '{7}',
                               DEPT_DICT.NH_DEPT_CODE = '{8}',
                               DEPT_DICT.DEFAULT_PERFORMED_BY = {10},
                               DEPT_DICT.DEPT_CODE_RECKING = '{11}',
    DEPT_DICT.IS_SPICU = '{12}'
                        WHERE DEPT_DICT.serial_no = {9}";
            object[] param = new object[] { dept_dict_new.SERIAL_NO, dept_dict_new.DEPT_CODE,
                dept_dict_new.DEPT_NAME, dept_dict_new.DEPT_ALIAS, dept_dict_new.CLINIC_ATTR,
                dept_dict_new.OUTP_OR_INP, dept_dict_new.INTERNAL_OR_SERGERY,
                dept_dict_new.INPUT_CODE, dept_dict_new.NH_DEPT_CODE, serial_no,
                dept_dict_new.DEFAULT_PERFORMED_BY, dept_dict_new.DEPT_CODE_RECKING,dept_dict_new.IS_SPICU };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定科室数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dept_code"></param>
        /// <returns></returns>
        public int DeleteDeptDict(BaseEntityer db, string dept_code)
        {
            string sql = @"delete from DEPT_DICT where DEPT_DICT.dept_code = '{0}'";
            sql = string.Format(sql, dept_code);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 判断科室编码数据是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dept_code"></param>
        /// <param name="serial_no"></param>
        /// <returns></returns>
        public DbDataReader GetDeptDictData(BaseEntityer db, string dept_code, int serial_no)
        {
            string sql = @"SELECT * FROM DEPT_DICT 
                            where DEPT_DICT.dept_code = '{0}' and DEPT_DICT.serial_no != {1}";
            sql = string.Format(sql, dept_code, serial_no);
            return db.ExecuteReader(sql);
        }

        /// <summary>
        /// 人员字典(维护用)
        /// </summary>
        /// <returns></returns>
        public IList<USERS_STAFF_DICT> GetUserDict()
        {
            string sql = @"SELECT users_staff_dict.user_id ,
                           users_staff_dict.user_name ,
                           users_staff_dict.user_dept ,
                           users_staff_dict.create_date ,
                           users_staff_dict.job ,
                           users_staff_dict.title ,
                           users_staff_dict.menu_group ,
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
                           users_staff_dict.IS_CASHIER,
                         --  users_staff_dict.SIGNATURE,
                           users_staff_dict.DOC_EXPERT,
                             users_staff_dict.IS_APPOINT_REG
                           FROM users_staff_dict ORDER BY users_staff_dict.user_id";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<USERS_STAFF_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }
         
        /// <summary>
        /// 获取医生签名信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IList<USERS_STAFF_DICT> GetUserSignatureInfo(string userid)
        {
            string sql = @"SELECT t.signature FROM users_staff_dict t where t.user_id = '{0}'";
            sql = string.Format(sql, userid);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<USERS_STAFF_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 人员字典(维护用)
        /// </summary>
        /// <returns></returns>
        public IList<USERS_STAFF_DICT> GetUserNameIDDict()
        {
            string sql = @"SELECT users_staff_dict.user_id,
                                   users_staff_dict.user_name,
                                   users_staff_dict.user_dept,
                                   users_staff_dict.sex
                              FROM users_staff_dict
                             ORDER BY users_staff_dict.user_id
                            ";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<USERS_STAFF_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 判断人员数据是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="user_id">人员主键</param>
        /// <returns></returns>
        public DbDataReader GetUserDictData(BaseEntityer db, string user_id)
        {
            string sql = @"SELECT * FROM users_staff_dict 
                            where users_staff_dict.user_id = '{0}'";
            sql = string.Format(sql, user_id);
            return db.ExecuteReader(sql);
        }

        /// <summary>
        /// 新增人员项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="users_staff_dict_new"></param>
        /// <returns></returns>
        public int InsertUserDict(BaseEntityer db, USERS_STAFF_DICT users_staff_dict_new, string OperCode, DateTime OperDate)
        {
            string sql = @"INSERT INTO users_staff_dict
                        (
                               users_staff_dict.user_id,
                               users_staff_dict.user_name,
                               users_staff_dict.user_dept,
                               users_staff_dict.create_date,
                               users_staff_dict.job,
                               users_staff_dict.title,
                               users_staff_dict.menu_group,
                               users_staff_dict.user_pass,
                               users_staff_dict.dis,
                               users_staff_dict.toxi,
                               USERS_STAFF_DICT.certificate_code,
                                USERS_STAFF_DICT.sex,
                                USERS_STAFF_DICT.doctor_type,
                                USERS_STAFF_DICT.job_title,
                                USERS_STAFF_DICT.professional_titile,
                                USERS_STAFF_DICT.school,
                                USERS_STAFF_DICT.id_card,
                                USERS_STAFF_DICT.profess_type,
                                USERS_STAFF_DICT.profess_range,
                                USERS_STAFF_DICT.certificate_date,
                                USERS_STAFF_DICT.primary_illness,
                                USERS_STAFF_DICT.hos_staff_type,
                                USERS_STAFF_DICT.star_level,
                                USERS_STAFF_DICT.certificate_place,
                                USERS_STAFF_DICT.oper_code,
                                USERS_STAFF_DICT.IS_CASHIER,
                                USERS_STAFF_DICT.OPERCODE,
                                USERS_STAFF_DICT.OPERDATE,
USERS_STAFF_DICT.doc_expert,
USERS_STAFF_DICT.IS_APPOINT_REG
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}',to_date('{3}', 'yyyy-mm-dd hh24:mi:ss'),
                                '{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',
                                '{15}','{16}','{17}','{18}',to_date('{19}', 'yyyy-mm-dd hh24:mi:ss'),
                                '{20}','{21}','{22}','{23}','{24}','{25}','{26}',to_date('{27}', 'yyyy-mm-dd hh24:mi:ss'),'{28}','{29}'
                        )";
            object[] param = new object[] { users_staff_dict_new.USER_ID, users_staff_dict_new.USER_NAME, 
                users_staff_dict_new.USER_DEPT, users_staff_dict_new.CREATE_DATE, users_staff_dict_new.JOB, 
                users_staff_dict_new.TITLE, users_staff_dict_new.MENU_GROUP, users_staff_dict_new.USER_PASS, 
                users_staff_dict_new.DIS, users_staff_dict_new.TOXI, users_staff_dict_new.CERTIFICATE_CODE, 
                users_staff_dict_new.SEX, users_staff_dict_new.DOCTOR_TYPE, users_staff_dict_new.JOB_TITLE, 
                users_staff_dict_new.PROFESSIONAL_TITILE, users_staff_dict_new.SCHOOL, users_staff_dict_new.ID_CARD, 
                users_staff_dict_new.PROFESS_TYPE, users_staff_dict_new.PROFESS_RANGE, 
                users_staff_dict_new.CERTIFICATE_DATE, users_staff_dict_new.PRIMARY_ILLNESS, 
                users_staff_dict_new.HOS_STAFF_TYPE, users_staff_dict_new.STAR_LEVEL, 
                users_staff_dict_new.CERTIFICATE_PLACE, users_staff_dict_new.OPER_CODE, users_staff_dict_new.IS_CASHIER ,OperCode,OperDate,users_staff_dict_new.DOC_EXPERT,users_staff_dict_new.IS_APPOINT_REG};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新人员项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="users_staff_dict_new"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int UpdateUserDict(BaseEntityer db, USERS_STAFF_DICT users_staff_dict_new, string user_id, string operCode, DateTime operDate)
        {
            string sql = @"UPDATE users_staff_dict 
                        SET 
                               users_staff_dict.user_id = '{0}',
                               users_staff_dict.user_name = '{1}',
                               users_staff_dict.user_dept = '{2}',
                               users_staff_dict.create_date = to_date('{3}', 'yyyy-mm-dd hh24:mi:ss'),
                               users_staff_dict.job = '{4}',
                               users_staff_dict.title = '{5}',
                               users_staff_dict.menu_group = '{6}',
                               users_staff_dict.user_pass = '{7}',
                               users_staff_dict.dis = '{8}',
                               users_staff_dict.toxi = '{9}',
                                USERS_STAFF_DICT.certificate_code = '{10}',
                                USERS_STAFF_DICT.sex = '{11}',
                                USERS_STAFF_DICT.doctor_type = '{12}',
                                USERS_STAFF_DICT.job_title = '{13}',
                                USERS_STAFF_DICT.professional_titile = '{14}',
                                USERS_STAFF_DICT.school = '{15}',
                                USERS_STAFF_DICT.id_card = '{16}',
                                USERS_STAFF_DICT.profess_type = '{17}',
                                USERS_STAFF_DICT.profess_range = '{18}',
                                USERS_STAFF_DICT.certificate_date = to_date('{19}', 'yyyy-mm-dd hh24:mi:ss'),
                                USERS_STAFF_DICT.primary_illness = '{20}',
                                USERS_STAFF_DICT.hos_staff_type = '{21}',
                                USERS_STAFF_DICT.star_level = '{22}',
                                USERS_STAFF_DICT.certificate_place = '{23}',
                                USERS_STAFF_DICT.oper_code = '{24}' ,
                                USERS_STAFF_DICT.IS_CASHIER = '{25}',
                                USERS_STAFF_DICT.OPERCODE = '{27}' ,
                                USERS_STAFF_DICT.OPERDATE = to_date('{28}', 'yyyy-mm-dd hh24:mi:ss'),
                                USERS_STAFF_DICT.DOC_EXPERT='{29}',
                                USERS_STAFF_DICT.IS_APPOINT_REG='{30}'
                        WHERE users_staff_dict.user_id = '{26}'";
            object[] param = new object[] { users_staff_dict_new.USER_ID, users_staff_dict_new.USER_NAME, 
                users_staff_dict_new.USER_DEPT, users_staff_dict_new.CREATE_DATE, users_staff_dict_new.JOB, 
                users_staff_dict_new.TITLE, users_staff_dict_new.MENU_GROUP, users_staff_dict_new.USER_PASS, 
                users_staff_dict_new.DIS, users_staff_dict_new.TOXI, users_staff_dict_new.CERTIFICATE_CODE, 
                users_staff_dict_new.SEX, users_staff_dict_new.DOCTOR_TYPE, users_staff_dict_new.JOB_TITLE, 
                users_staff_dict_new.PROFESSIONAL_TITILE, users_staff_dict_new.SCHOOL, users_staff_dict_new.ID_CARD, 
                users_staff_dict_new.PROFESS_TYPE, users_staff_dict_new.PROFESS_RANGE, 
                users_staff_dict_new.CERTIFICATE_DATE, users_staff_dict_new.PRIMARY_ILLNESS, 
                users_staff_dict_new.HOS_STAFF_TYPE, users_staff_dict_new.STAR_LEVEL, 
                users_staff_dict_new.CERTIFICATE_PLACE, users_staff_dict_new.OPER_CODE, 
                users_staff_dict_new.IS_CASHIER, user_id,operCode,operDate ,users_staff_dict_new.DOC_EXPERT,users_staff_dict_new.IS_APPOINT_REG};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定人员数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int DeleteUserDict(BaseEntityer db, string user_id)
        {
            string sql = @"delete from users_staff_dict where users_staff_dict.user_id = '{0}'";
            sql = string.Format(sql, user_id);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 人员医护信息上传部分属性值列表
        /// </summary>
        /// <param name="attribute_type"></param>
        /// <returns></returns>
        public IList<COM_DICTIONARY> GetUserAttributeDict(string attribute_type)
        {
            string sql = @"SELECT COM_DICTIONARY.TYPE ,
                           COM_DICTIONARY.CODE ,
                           COM_DICTIONARY.NAME ,
                           COM_DICTIONARY.MARK ,
                           COM_DICTIONARY.SPELL_CODE ,
                           COM_DICTIONARY.WB_CODE ,
                           COM_DICTIONARY.INPUT_CODE ,
                           COM_DICTIONARY.SORT_ID ,
                           COM_DICTIONARY.VALID_STATE ,
                           COM_DICTIONARY.OPER_CODE ,
                           COM_DICTIONARY.OPER_DATE 
                           FROM COM_DICTIONARY 
                           WHERE ( COM_DICTIONARY.TYPE = '{0}' ) 
                           ORDER BY COM_DICTIONARY.CODE";
            sql = string.Format(sql, attribute_type);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<COM_DICTIONARY>(ds);
        }

        /// <summary>
        /// 人员工作类别项目集合菜单列表
        /// </summary>
        /// <returns></returns>
        public IList<JOB_CLASS_DICT> GetUserJobClassDict()
        {
            string sql = @"SELECT job_class_dict.serial_no ,
                           job_class_dict.job_class_code ,
                           job_class_dict.job_class_name ,
                           job_class_dict.input_code     
                           FROM job_class_dict 
                    ORDER BY job_class_dict.job_class_code";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<JOB_CLASS_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 人员职务类别项目集合菜单列表
        /// </summary>
        /// <returns></returns>
        public IList<TITLE_DICT> GetUserTitleDict()
        {
            string sql = @"SELECT title_dict.serial_no ,
                           title_dict.title_code ,
                           title_dict.title_name ,
                           title_dict.input_code     
                           FROM title_dict 
                    ORDER BY title_dict.title_code";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<TITLE_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 人员权限列表(维护用)
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public IList<USERS_GROUP_DICT> GetUserGroupDict(string user_id)
        {
            string sql = @"SELECT USERS_GROUP_DICT.USERID ,
                           USERS_GROUP_DICT.MENU_GROUP ,
                           USERS_GROUP_DICT.GROUP_DEPT ,
                           USERS_GROUP_DICT.FLAG     
                           FROM USERS_GROUP_DICT      
                           WHERE ( USERS_GROUP_DICT.USERID = '{0}' ) ORDER BY USERS_GROUP_DICT.FLAG DESC";
            sql = string.Format(sql, user_id);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<USERS_GROUP_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 新增人员权限项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="users_group_dict_new"></param>
        /// <returns></returns>
        public int InsertUserGroupDict(BaseEntityer db, USERS_GROUP_DICT users_group_dict_new, string operCode, DateTime operdate)
        {
            string sql = @"INSERT INTO USERS_GROUP_DICT
                        (
                               USERS_GROUP_DICT.USERID,
                               USERS_GROUP_DICT.MENU_GROUP,
                               USERS_GROUP_DICT.GROUP_DEPT,
                               USERS_GROUP_DICT.FLAG,
                               USERS_GROUP_DICT.OPERCODE,
                               USERS_GROUP_DICT.OPERDATE
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}',{3},'{4}',to_date('{5}', 'yyyy-mm-dd hh24:mi:ss')
                        )";
            object[] param = new object[] { users_group_dict_new.USERID, users_group_dict_new.MENU_GROUP, 
                users_group_dict_new.GROUP_DEPT, users_group_dict_new.FLAG,operCode,operdate };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定人员权限数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int DeleteUserGroupDict(BaseEntityer db, string user_id)
        {
            string sql = @"delete from USERS_GROUP_DICT where USERS_GROUP_DICT.USERID = '{0}'";
            sql = string.Format(sql, user_id);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取字典表列表集合
        /// </summary>
        /// <returns></returns>
        public IList<METADICT> GetMetaDict()
        {
            string sql = @"SELECT metadict.table_name ,
                           metadict.table_description     
                           FROM metadict 
                           ORDER BY metadict.table_description";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<METADICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 读取字典表数据
        /// </summary>
        /// <param name="tableName">字典表名称</param>
        /// <param name="dictAttributes">字典表属性及显示名称</param>
        /// <returns></returns>
        public DataTable GetDictTable(string tableName, Dictionary<string, Dictionary<string, string>> dictAttributes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            int i = 0;
            foreach (var item in dictAttributes)
            {
                sb.Append(item.Key + " AS " + item.Value.Keys.First());
                i++;
                if (i != dictAttributes.Count)
                {
                    sb.Append(",");
                }
            }
            sb.Append(" FROM " + tableName);
            return BaseEntityer.Db.GetDataTable(sb.ToString());
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tableName">表名</param>
        /// <param name="dictAttributes">列名与显示名称对应字典</param>
        /// <param name="rowData">显示名称与值对应字典</param>
        /// <returns></returns>
        public int InsertDictTable(BaseEntityer db, string tableName, Dictionary<string, Dictionary<string, string>> dictAttributes, Dictionary<string, string> rowData)
        {
            StringBuilder sqlStr = new StringBuilder("INSERT INTO " + tableName + "(");
            StringBuilder valueStr = new StringBuilder(" VALUES (");
            int i = 0;
            try
            {
                foreach (var item in dictAttributes)
                {
                    sqlStr.Append(tableName + "." + item.Key);
                    if (item.Value.Values.First() == typeof(System.DateTime).ToString())
                    {
                        //to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 时间类型需要特殊处理
                        valueStr.Append("to_date('" + rowData.First(x => x.Key == item.Value.Keys.First()).Value
                            + "', 'yyyy-mm-dd hh24:mi:ss')");
                    }
                    else
                    {
                        valueStr.Append("'" + rowData.First(x => x.Key == item.Value.Keys.First()).Value + "'");
                    }
                    i++;
                    if (i != dictAttributes.Count)
                    {
                        sqlStr.Append(",");
                        valueStr.Append(",");
                    }
                }
            }
            catch//(Exception ex)
            {

            }
            string insertSql = sqlStr.ToString() + ")" + valueStr.ToString() + ")";
            return db.ExecuteNonQuery(insertSql);
        }

        /// <summary>
        /// 插入字典表数据之前删除所有字典表数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int DeleteDictTable(BaseEntityer db, string tableName)
        {
            string sql = @"delete from " + tableName;
            return db.ExecuteNonQuery(sql);
        }

        #endregion

        #region 价表管理

        /// <summary>
        /// 价表项目住院收据类别项目类别集合
        /// </summary>
        /// <returns></returns>
        public IList<BILL_ITEM_CLASS_DICT> GetBillItemClassDict()
        {
            string sql = @"SELECT BILL_ITEM_CLASS_DICT.SERIAL_NO ,
                           BILL_ITEM_CLASS_DICT.CLASS_CODE ,
                           BILL_ITEM_CLASS_DICT.CLASS_NAME ,
                           BILL_ITEM_CLASS_DICT.INPUT_CODE 
                           FROM BILL_ITEM_CLASS_DICT 
                           ORDER BY BILL_ITEM_CLASS_DICT.SERIAL_NO ASC ";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<BILL_ITEM_CLASS_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 价表项目住院收据类别项目集合
        /// </summary>
        /// <returns></returns>
        public IList<INP_RCPT_FEE_DICT> GetInpRectFeeDict()
        {
            string sql = @"SELECT INP_RCPT_FEE_DICT.SERIAL_NO ,
                           INP_RCPT_FEE_DICT.FEE_CLASS_CODE , 
                           INP_RCPT_FEE_DICT.FEE_CLASS_NAME ,
                           INP_RCPT_FEE_DICT.INPUT_CODE
                           FROM INP_RCPT_FEE_DICT 
                           ORDER BY INP_RCPT_FEE_DICT.FEE_CLASS_CODE ASC ";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<INP_RCPT_FEE_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 价表项目门诊收据类别项目集合
        /// </summary>
        /// <returns></returns>
        public IList<OUTP_RCPT_FEE_DICT> GetOutpRectFeeDict()
        {
            string sql = @"SELECT OUTP_RCPT_FEE_DICT.SERIAL_NO, 
                           OUTP_RCPT_FEE_DICT.FEE_CLASS_CODE, 
                           OUTP_RCPT_FEE_DICT.FEE_CLASS_NAME, 
                           OUTP_RCPT_FEE_DICT.INPUT_CODE 
                           FROM OUTP_RCPT_FEE_DICT 
                           ORDER BY OUTP_RCPT_FEE_DICT.FEE_CLASS_CODE ASC ";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<OUTP_RCPT_FEE_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 价表项目会计科目类别项目集合
        /// </summary>
        /// <returns></returns>
        public IList<TALLY_SUBJECT_DICT> GetTallySubjectDict()
        {
            string sql = @"SELECT TALLY_SUBJECT_DICT.SERIAL_NO, 
                           TALLY_SUBJECT_DICT.SUBJ_CODE, 
                           TALLY_SUBJECT_DICT.SUBJ_NAME, 
                           TALLY_SUBJECT_DICT.INPUT_CODE 
                           FROM TALLY_SUBJECT_DICT 
                           ORDER BY TALLY_SUBJECT_DICT.SUBJ_CODE ASC ";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<TALLY_SUBJECT_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 价表项目核算项目类别项目集合
        /// </summary>
        /// <returns></returns>
        public IList<RECK_ITEM_CLASS_DICT> GetReckItemClassDict()
        {
            string sql = @"SELECT RECK_ITEM_CLASS_DICT.SERIAL_NO, 
                           RECK_ITEM_CLASS_DICT.CLASS_CODE, 
                           RECK_ITEM_CLASS_DICT.CLASS_NAME , 
                           RECK_ITEM_CLASS_DICT.INPUT_CODE
                           FROM RECK_ITEM_CLASS_DICT 
                           ORDER BY RECK_ITEM_CLASS_DICT.SERIAL_NO ASC ";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<RECK_ITEM_CLASS_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 价表项目病案首页类别项目集合
        /// </summary>
        /// <returns></returns>
        public IList<MR_FEE_CLASS_DICT> GetMrFeeClassDict()
        {
            string sql = @"SELECT MR_FEE_CLASS_DICT.SERIAL_NO , 
                           MR_FEE_CLASS_DICT.MR_FEE_CLASS_CODE , 
                           MR_FEE_CLASS_DICT.MR_FEE_CLASS_NAME , 
                           MR_FEE_CLASS_DICT.INPUT_CODE 
                           FROM MR_FEE_CLASS_DICT 
                           ORDER BY MR_FEE_CLASS_DICT.SERIAL_NO ASC ";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<MR_FEE_CLASS_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 价表项目计价单位项目集合
        /// </summary>
        /// <returns></returns>
        public IList<BILL_UNITS_DICT> GetBillUnitsDict()
        {
            string sql = @"SELECT BILL_UNITS_DICT.SERIAL_NO ,
                           BILL_UNITS_DICT.BILL_UNITS_CODE ,
                           BILL_UNITS_DICT.BILL_UNITS_NAME ,
                           BILL_UNITS_DICT.INPUT_CODE 
                           FROM BILL_UNITS_DICT
                           ORDER BY BILL_UNITS_DICT.SERIAL_NO ASC ";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<BILL_UNITS_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 价表项目集合
        /// </summary>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <param name="_item_spec"></param>
        /// <param name="_units"></param>
        /// <param name="_start_date"></param>
        /// <param name="_item_name"></param>
        /// <param name="_performed_by"></param>
        /// <returns></returns>
        public IList<PRICE_LIST> GetPriceList(string _item_class, string _item_code, string _item_spec, string _units, string _start_date, string _item_name, string _performed_by, bool check)
        {
            //2013-12-19 by li 价表查询表连接保证数据唯一性
            string sql = @"SELECT PRICE_LIST.ITEM_CLASS ,
                           PRICE_LIST.ITEM_CODE ,
                           PRICE_LIST.ITEM_NAME ,
                           PRICE_LIST.ITEM_SPEC ,
                           PRICE_LIST.UNITS ,
                           PRICE_LIST.PRICE ,
                           PRICE_LIST.PREFER_PRICE ,
                           PRICE_LIST.PERFORMED_BY ,
                           PRICE_LIST.FEE_TYPE_MASK ,
                           PRICE_LIST.CLASS_ON_INP_RCPT ,
                           PRICE_LIST.CLASS_ON_OUTP_RCPT ,
                           PRICE_LIST.CLASS_ON_RECKONING ,
                           PRICE_LIST.SUBJ_CODE ,
                           PRICE_LIST.CLASS_ON_MR ,
                           PRICE_LIST.MEMO ,
                           PRICE_LIST.START_DATE ,
                           PRICE_LIST.STOP_DATE ,
                           PRICE_LIST.OPERATOR ,
                           PRICE_LIST.ENTER_DATE ,
                           --PRICE_LIST.ZFYP ,
                           --PRICE_LIST.SBZFYP ,
                           --PRICE_LIST.SBGKBZ ,
                           PRICE_LIST.Foreigner_Price ,
                           (select PRICE_ITEM_NAME_DICT.Input_Code
                              from PRICE_ITEM_NAME_DICT
                             where ";
            string sqladd = @" and PRICE_ITEM_NAME_DICT.STD_INDICATOR = 1
                               and rownum = 1) as Input_Code,
                           PRICE_LIST.PRODUCE_FACTORY ,
                           PRICE_LIST.PRODUCE_REG_NO,
                           PRICE_LIST.SPECIAL_FLAG,
                           PRICE_LIST.TOUCHSCREEN 
                           FROM PRICE_LIST 
                           WHERE 1 = 1 ";
            string sql1 = @" PRICE_ITEM_NAME_DICT.ITEM_CODE = '{1}' ";
            string sql2 = @" PRICE_ITEM_NAME_DICT.ITEM_CODE = PRICE_LIST.Item_Code ";
            if (_item_class == string.Empty)
            {
                sql = sql + sql2 + sqladd;
                sql += " ORDER BY PRICE_LIST.ITEM_CLASS ASC ";
            }
            else
            {
                sql = sql + sql1 + sqladd;
                sql += " AND PRICE_LIST.ITEM_CLASS = '{0}' ";
                if (_item_code != string.Empty)
                {
                    sql += " AND PRICE_LIST.ITEM_CODE = '{1}' ";
                }
                if (_item_spec != string.Empty)
                {
                    sql += " AND PRICE_LIST.ITEM_SPEC = '{2}' ";
                }
                if (_units != string.Empty)
                {
                    sql += " AND PRICE_LIST.UNITS = '{3}' ";
                }
                if (_start_date != string.Empty)
                {
                    sql += " AND PRICE_LIST.START_DATE = to_date('{4}', 'yyyy-mm-dd hh24:mi:ss') ";
                }
                if (_item_name != string.Empty)
                {
                    sql += " AND PRICE_LIST.ITEM_NAME like '%{5}%' ";
                }
                if (_performed_by != string.Empty)
                {
                    sql += " AND PRICE_LIST.PERFORMED_BY = '{6}' ";
                }
                if (check)
                {
                    sql += " AND 1=1 ";
                }
                else
                {
                    sql += " AND PRICE_LIST.STOP_DATE is null";
                }
                sql += " ORDER BY PRICE_LIST.ITEM_CLASS ASC ";
            }
            sql = string.Format(sql, _item_class, _item_code, _item_spec,
                _units, _start_date, _item_name, _performed_by);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<PRICE_LIST>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 根据备注信息获取价表集合
        /// </summary>
        /// <param name="strMemo"></param>
        /// <returns></returns>
        public IList<PRICE_LIST> QueryPriceListByMemo(string strMemo)
        {
            string sql = @"

SELECT
t.COUNTRY_CODE,   --
t.PRODUCE_FACTORY_DAILI,   --
t.PRODUCE_REG_NAME,   --
t.SPECIAL_FLAG,   --退费时需申请标记
t.PRODUCE_REG_NO,   --产品注册证号(材料)
t.PRODUCE_FACTORY,   --生产厂家（材料）
t.RATIFY_NO,   --
t.SBGKBZ,   --
t.SBZFYP,   --
t.ZFYP,   --
t.ENTER_DATE,   --
t.OPERATOR,   --
t.STOP_DATE,   --
t.START_DATE,   --
t.MEMO,   --
t.CLASS_ON_MR,   --
t.SUBJ_CODE,   --
t.CLASS_ON_RECKONING,   --
t.CLASS_ON_OUTP_RCPT,   --
t.CLASS_ON_INP_RCPT,   --
t.FEE_TYPE_MASK,   --
t.PERFORMED_BY,   --
t.FOREIGNER_PRICE,   --
t.PREFER_PRICE,   --
t.PRICE,   --
t.UNITS,   --
t.ITEM_SPEC,   --
t.ITEM_NAME,   --
t.ITEM_CODE,   --
t.ITEM_CLASS   --
FROM
PRICE_LIST  t   --
WHERE
   instr(t.memo,'|'||'{0}'||'|',1,1) >0 ";

            sql = string.Format(sql, strMemo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<PRICE_LIST>(ds);

        }

        /// <summary>
        /// 根据分类和编码查询价表项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <param name="_item_spec"></param>
        /// <param name="_units"></param>
        /// <returns></returns>
        public IList<string> GetPriceListByClassAndCode(BaseEntityer db, string _item_class, string _item_code, string _item_spec, string _units)
        {
            string sql = @"SELECT DISTINCT PRICE_LIST.ITEM_NAME
                           FROM PRICE_LIST
                           WHERE PRICE_LIST.ITEM_CLASS = '{0}' AND PRICE_LIST.ITEM_CODE = '{1}' ";
            if (_item_spec != null && _units != null)
                sql += @"  AND ((ITEM_SPEC = '{2}') OR ((ITEM_SPEC IS NULL) AND  ('{2}' IS NULL)))
                           AND ((UNITS = '{3}' ) OR ((UNITS IS NULL) AND ('{3}' IS NULL)))
                           AND PRICE_LIST.STOP_DATE is NULL";
            sql = string.Format(sql, _item_class, _item_code, _item_spec, _units);
            DataSet ds = db.GetDataSet(sql);
            List<string> list = new List<string>();
            if (ds.Tables.Count > 0)
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    list.Add(item["ITEM_NAME"].ToString());
                }
            return list;
        }

        /// <summary>
        /// 新增价表项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="users_staff_dict_new"></param>
        /// <returns></returns>
        public int InsertPriceList(BaseEntityer db, PRICE_LIST price_list_item_new)
        {
            string sql = @"INSERT INTO PRICE_LIST
                        (
                               PRICE_LIST.ITEM_CLASS ,
                               PRICE_LIST.ITEM_CODE ,
                               PRICE_LIST.ITEM_NAME ,
                               PRICE_LIST.ITEM_SPEC ,
                               PRICE_LIST.UNITS ,
                               PRICE_LIST.PRICE ,
                               PRICE_LIST.PREFER_PRICE ,
                               PRICE_LIST.PERFORMED_BY ,
                               PRICE_LIST.FEE_TYPE_MASK ,
                               PRICE_LIST.CLASS_ON_INP_RCPT ,
                               PRICE_LIST.CLASS_ON_OUTP_RCPT ,
                               PRICE_LIST.CLASS_ON_RECKONING ,
                               PRICE_LIST.SUBJ_CODE ,
                               PRICE_LIST.CLASS_ON_MR ,
                               PRICE_LIST.MEMO ,
                               PRICE_LIST.START_DATE ,
                               PRICE_LIST.STOP_DATE ,
                               PRICE_LIST.OPERATOR ,
                               PRICE_LIST.ENTER_DATE ,
                               --PRICE_LIST.ZFYP ,
                               --PRICE_LIST.SBZFYP ,
                               --PRICE_LIST.SBGKBZ ,
                               PRICE_LIST.Foreigner_Price ,
                               PRICE_LIST.PRODUCE_FACTORY ,
                               PRICE_LIST.PRODUCE_REG_NO,
                               PRICE_LIST.PRODUCE_REG_NAME   , 
                               PRICE_LIST.PRODUCE_FACTORY_DAILI  ,
                               PRICE_LIST.COUNTRY_CODE,
                               PRICE_LIST.SPECIAL_FLAG,
                               PRICE_LIST.TOUCHSCREEN
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}',{8},'{9}','{10}','{11}',
                                '{12}','{13}','{14}',to_date('{15}', 'yyyy-mm-dd hh24:mi:ss'),
                                to_date('{16}', 'yyyy-mm-dd hh24:mi:ss'),'{17}',
                                to_date('{18}', 'yyyy-mm-dd hh24:mi:ss'),{19},'{20}','{21}',{22},'{23}','{24}','{25}','{26}'
                        )";
            object[] param = new object[] { price_list_item_new.ITEM_CLASS, price_list_item_new.ITEM_CODE, 
                price_list_item_new.ITEM_NAME, price_list_item_new.ITEM_SPEC, price_list_item_new.UNITS, 
                price_list_item_new.PRICE, price_list_item_new.PREFER_PRICE, price_list_item_new.PERFORMED_BY, 
                price_list_item_new.FEE_TYPE_MASK, price_list_item_new.CLASS_ON_INP_RCPT, 
                price_list_item_new.CLASS_ON_OUTP_RCPT, price_list_item_new.CLASS_ON_RECKONING, 
                price_list_item_new.SUBJ_CODE, price_list_item_new.CLASS_ON_MR, price_list_item_new.MEMO, 
                price_list_item_new.START_DATE, price_list_item_new.STOP_DATE, price_list_item_new.OPERATOR, 
                price_list_item_new.ENTER_DATE, price_list_item_new.FOREIGNER_PRICE, 
                price_list_item_new.PRODUCE_FACTORY, price_list_item_new.PRODUCE_REG_NO,
                 price_list_item_new.PRODUCE_REG_NAME,price_list_item_new.PRODUCE_FACTORY_DAILI,price_list_item_new.COUNTRY_CODE,price_list_item_new.SPECIAL_FLAG,price_list_item_new.TouchScreen
            };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新价表项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="users_staff_dict_new"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int UpdatePriceList(BaseEntityer db, PRICE_LIST price_list_item_new, PRICE_LIST price_list_item)
        {
            string sql = @"UPDATE PRICE_LIST 
                        SET 
                               PRICE_LIST.ITEM_CLASS = '{0}',
                               PRICE_LIST.ITEM_CODE = '{1}',
                               PRICE_LIST.ITEM_NAME = '{2}',
                               PRICE_LIST.ITEM_SPEC = '{3}',
                               PRICE_LIST.UNITS = '{4}',
                               PRICE_LIST.PRICE = {5},
                               PRICE_LIST.PREFER_PRICE = {6},
                               PRICE_LIST.PERFORMED_BY = '{7}',
                               PRICE_LIST.FEE_TYPE_MASK = {8},
                               PRICE_LIST.CLASS_ON_INP_RCPT = '{9}',
                               PRICE_LIST.CLASS_ON_OUTP_RCPT = '{10}',
                               PRICE_LIST.CLASS_ON_RECKONING = '{11}',
                               PRICE_LIST.SUBJ_CODE = '{12}',
                               PRICE_LIST.CLASS_ON_MR = '{13}',
                               PRICE_LIST.MEMO = '{14}',
                               PRICE_LIST.START_DATE = to_date('{15}', 'yyyy-mm-dd hh24:mi:ss'),
                               PRICE_LIST.STOP_DATE = to_date('{16}', 'yyyy-mm-dd hh24:mi:ss'),
                               PRICE_LIST.OPERATOR = '{17}',
                               PRICE_LIST.ENTER_DATE = to_date('{18}', 'yyyy-mm-dd hh24:mi:ss'),
                               PRICE_LIST.Foreigner_Price = {19},
                               PRICE_LIST.PRODUCE_FACTORY = '{25}',
                               PRICE_LIST.PRODUCE_REG_NO = '{26}' ,
                               PRICE_LIST.PRODUCE_REG_NAME = '{27}' , 
                               PRICE_LIST.PRODUCE_FACTORY_DAILI = '{28}',
                               PRICE_LIST.COUNTRY_CODE = '{29}',
                               PRICE_LIST.SPECIAL_FLAG = '{30}',
                               PRICE_LIST.TOUCHSCREEN = '{31}'
                        WHERE PRICE_LIST.ITEM_CLASS = '{20}' AND PRICE_LIST.ITEM_CODE = '{21}'
                            AND PRICE_LIST.ITEM_SPEC = '{22}' AND PRICE_LIST.UNITS = '{23}' 
                            AND PRICE_LIST.START_DATE = to_date('{24}', 'yyyy-mm-dd hh24:mi:ss')";
            object[] param = new object[] { price_list_item_new.ITEM_CLASS, price_list_item_new.ITEM_CODE, 
                price_list_item_new.ITEM_NAME, price_list_item_new.ITEM_SPEC, price_list_item_new.UNITS, 
                price_list_item_new.PRICE, price_list_item_new.PREFER_PRICE, price_list_item_new.PERFORMED_BY, 
                price_list_item_new.FEE_TYPE_MASK, price_list_item_new.CLASS_ON_INP_RCPT, 
                price_list_item_new.CLASS_ON_OUTP_RCPT, price_list_item_new.CLASS_ON_RECKONING, 
                price_list_item_new.SUBJ_CODE, price_list_item_new.CLASS_ON_MR, price_list_item_new.MEMO, 
                price_list_item_new.START_DATE, price_list_item_new.STOP_DATE, price_list_item_new.OPERATOR, 
                price_list_item_new.ENTER_DATE, price_list_item_new.FOREIGNER_PRICE, price_list_item.ITEM_CLASS, 
                price_list_item.ITEM_CODE, price_list_item.ITEM_SPEC, price_list_item.UNITS, 
                price_list_item.START_DATE, price_list_item_new.PRODUCE_FACTORY, price_list_item_new.PRODUCE_REG_NO,
                price_list_item_new.PRODUCE_REG_NAME,price_list_item_new.PRODUCE_FACTORY_DAILI,price_list_item_new.COUNTRY_CODE,price_list_item_new.SPECIAL_FLAG,price_list_item_new.TouchScreen
            };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新价表项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="users_staff_dict_new"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int UpdatePriceListMutex(BaseEntityer db, PRICE_LIST price_list_item_new, PRICE_LIST price_list_item)
        {
            string sql = @"UPDATE PRICE_LIST SET ";
            sql += " PRICE_LIST.STOP_DATE = to_date('" + DateTime.Now.ToString() + "', 'yyyy-mm-dd hh24:mi:ss') ";
            sql += " WHERE PRICE_LIST.ITEM_CLASS = '" + price_list_item.ITEM_CLASS + "' AND PRICE_LIST.ITEM_CODE = '" + price_list_item.ITEM_CODE + "' ";
            sql += " AND PRICE_LIST.ITEM_SPEC = '" + price_list_item.ITEM_SPEC + "' AND PRICE_LIST.UNITS = '" + price_list_item.UNITS + "' ";

            db.ExecuteNonQuery(sql);

            sql = @"UPDATE PRICE_LIST 
                        SET 
                               PRICE_LIST.ITEM_CLASS = '{0}',
                               PRICE_LIST.ITEM_CODE = '{1}',
                               PRICE_LIST.ITEM_NAME = '{2}',
                               PRICE_LIST.ITEM_SPEC = '{3}',
                               PRICE_LIST.UNITS = '{4}',
                               PRICE_LIST.PRICE = {5},
                               PRICE_LIST.PREFER_PRICE = {6},
                               PRICE_LIST.PERFORMED_BY = '{7}',
                               PRICE_LIST.FEE_TYPE_MASK = {8},
                               PRICE_LIST.CLASS_ON_INP_RCPT = '{9}',
                               PRICE_LIST.CLASS_ON_OUTP_RCPT = '{10}',
                               PRICE_LIST.CLASS_ON_RECKONING = '{11}',
                               PRICE_LIST.SUBJ_CODE = '{12}',
                               PRICE_LIST.CLASS_ON_MR = '{13}',
                               PRICE_LIST.MEMO = '{14}',
                               PRICE_LIST.START_DATE = to_date('{15}', 'yyyy-mm-dd hh24:mi:ss'),
                               PRICE_LIST.STOP_DATE = to_date('{16}', 'yyyy-mm-dd hh24:mi:ss'),
                               PRICE_LIST.OPERATOR = '{17}',
                               PRICE_LIST.ENTER_DATE = to_date('{18}', 'yyyy-mm-dd hh24:mi:ss'),
                               PRICE_LIST.Foreigner_Price = {19},
                               PRICE_LIST.PRODUCE_FACTORY = '{25}',
                               PRICE_LIST.PRODUCE_REG_NO = '{26}' ,
                               PRICE_LIST.PRODUCE_REG_NAME = '{27}' , 
                               PRICE_LIST.PRODUCE_FACTORY_DAILI = '{28}',
                               PRICE_LIST.COUNTRY_CODE = '{29}',
                               PRICE_LIST.SPECIAL_FLAG = '{30}',
                               PRICE_LIST.TOUCHSCREEN='{31}' 
                        WHERE PRICE_LIST.ITEM_CLASS = '{20}' AND PRICE_LIST.ITEM_CODE = '{21}'
                            AND PRICE_LIST.ITEM_SPEC = '{22}' AND PRICE_LIST.UNITS = '{23}' 
                            AND PRICE_LIST.START_DATE = to_date('{24}', 'yyyy-mm-dd hh24:mi:ss')";
            object[] param = new object[] { price_list_item_new.ITEM_CLASS, price_list_item_new.ITEM_CODE, 
                price_list_item_new.ITEM_NAME, price_list_item_new.ITEM_SPEC, price_list_item_new.UNITS, 
                price_list_item_new.PRICE, price_list_item_new.PREFER_PRICE, price_list_item_new.PERFORMED_BY, 
                price_list_item_new.FEE_TYPE_MASK, price_list_item_new.CLASS_ON_INP_RCPT, 
                price_list_item_new.CLASS_ON_OUTP_RCPT, price_list_item_new.CLASS_ON_RECKONING, 
                price_list_item_new.SUBJ_CODE, price_list_item_new.CLASS_ON_MR, price_list_item_new.MEMO, 
                price_list_item_new.START_DATE, price_list_item_new.STOP_DATE, price_list_item_new.OPERATOR, 
                price_list_item_new.ENTER_DATE, price_list_item_new.FOREIGNER_PRICE, price_list_item.ITEM_CLASS, 
                price_list_item.ITEM_CODE, price_list_item.ITEM_SPEC, price_list_item.UNITS, 
                price_list_item.START_DATE, price_list_item_new.PRODUCE_FACTORY, price_list_item_new.PRODUCE_REG_NO,
                price_list_item_new.PRODUCE_REG_NAME,price_list_item_new.PRODUCE_FACTORY_DAILI,price_list_item_new.COUNTRY_CODE,price_list_item_new.SPECIAL_FLAG,price_list_item_new.TouchScreen
            };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        public IList<PRICE_LIST> SelectPriceListSTOP_DATE(BaseEntityer db, PRICE_LIST price_list_item)
        {
            string sql = @"select
                        PRICE_LIST.ITEM_CLASS,
                               PRICE_LIST.ITEM_CODE,
                               PRICE_LIST.ITEM_NAME,
                               PRICE_LIST.ITEM_SPEC,
                               PRICE_LIST.UNITS,
                               PRICE_LIST.PRICE,
                               PRICE_LIST.PREFER_PRICE,
                               PRICE_LIST.PERFORMED_BY,
                               PRICE_LIST.FEE_TYPE_MASK,
                               PRICE_LIST.CLASS_ON_INP_RCPT,
                               PRICE_LIST.CLASS_ON_OUTP_RCPT,
                               PRICE_LIST.CLASS_ON_RECKONING,
                               PRICE_LIST.SUBJ_CODE,
                               PRICE_LIST.CLASS_ON_MR,
                               PRICE_LIST.MEMO,
                               PRICE_LIST.START_DATE,
                               PRICE_LIST.STOP_DATE,
                               PRICE_LIST.OPERATOR,
                               PRICE_LIST.ENTER_DATE,
                               PRICE_LIST.Foreigner_Price,
                               PRICE_LIST.PRODUCE_FACTORY,
                               PRICE_LIST.PRODUCE_REG_NO,
                               PRICE_LIST.PRODUCE_REG_NAME, 
                               PRICE_LIST.PRODUCE_FACTORY_DAILI,
                               PRICE_LIST.COUNTRY_CODE
                        from PRICE_LIST
                        WHERE PRICE_LIST.ITEM_CLASS = '{0}' AND PRICE_LIST.ITEM_CODE = '{1}'
                            AND PRICE_LIST.ITEM_SPEC = '{2}' AND PRICE_LIST.UNITS = '{3}' ";
            object[] param = new object[] {
                price_list_item.ITEM_CLASS, 
                price_list_item.ITEM_CODE, 
                price_list_item.ITEM_SPEC, 
                price_list_item.UNITS           
            };
            sql = Utility.SqlFormate(sql, param);

            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.PRICE_LIST>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 删除指定价表项目数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="price_list_item_new"></param>
        /// <returns></returns>
        public int DeletePriceList(BaseEntityer db, PRICE_LIST price_list_item_new)
        {
            string sql = @"delete from PRICE_LIST where PRICE_LIST.ITEM_CLASS = '{0}' AND PRICE_LIST.ITEM_CODE = '{1}'
                            AND PRICE_LIST.ITEM_SPEC = '{2}' AND PRICE_LIST.UNITS = '{3}' 
                            AND PRICE_LIST.START_DATE = to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')";
            object[] param = new object[] { price_list_item_new.ITEM_CLASS, 
                price_list_item_new.ITEM_CODE, price_list_item_new.ITEM_SPEC, 
                price_list_item_new.UNITS, price_list_item_new.START_DATE };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据分类和编码查询价表名称表项目
        /// </summary>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <returns></returns>
        public IList<PRICE_ITEM_NAME_DICT> GetPriceItemNameByClassAndCode(BaseEntityer db, string _item_class, string _item_code)
        {
            string sql = @"select PRICE_ITEM_NAME_DICT.ITEM_CLASS ,
                           PRICE_ITEM_NAME_DICT.ITEM_NAME ,
                           PRICE_ITEM_NAME_DICT.ITEM_CODE ,
                           PRICE_ITEM_NAME_DICT.STD_INDICATOR ,
                           PRICE_ITEM_NAME_DICT.INPUT_CODE 
                           from PRICE_ITEM_NAME_DICT 
                           WHERE PRICE_ITEM_NAME_DICT.ITEM_CLASS = '{0}' AND PRICE_ITEM_NAME_DICT.ITEM_CODE = '{1}' 
                           ORDER BY PRICE_ITEM_NAME_DICT.ITEM_CLASS ASC ";
            sql = string.Format(sql, _item_class, _item_code);
            DataSet ds = db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<PRICE_ITEM_NAME_DICT>(ds);
        }

        /// <summary>
        /// 新增价表项目名称字典项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="price_item_name_dict_new"></param>
        /// <returns></returns>
        public int InsertPriceItemNameDict(BaseEntityer db, PRICE_ITEM_NAME_DICT price_item_name_dict_new)
        {
            string sql = @"INSERT INTO PRICE_ITEM_NAME_DICT
                        (
                               PRICE_ITEM_NAME_DICT.ITEM_CLASS,
                               PRICE_ITEM_NAME_DICT.ITEM_NAME,
                               PRICE_ITEM_NAME_DICT.ITEM_CODE,
                               PRICE_ITEM_NAME_DICT.STD_INDICATOR,
                               PRICE_ITEM_NAME_DICT.INPUT_CODE
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}',{3},'{4}'
                        )";
            object[] param = new object[] { price_item_name_dict_new.ITEM_CLASS, 
                price_item_name_dict_new.ITEM_NAME, price_item_name_dict_new.ITEM_CODE, 
                price_item_name_dict_new.STD_INDICATOR, price_item_name_dict_new.INPUT_CODE };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定价表项目名称字典数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <returns></returns>
        public int DeletePriceItemNameDict(BaseEntityer db, string _item_class, string _item_code)
        {
            string sql = @"delete from PRICE_ITEM_NAME_DICT where PRICE_ITEM_NAME_DICT.ITEM_CLASS = '{0}' 
                            AND PRICE_ITEM_NAME_DICT.ITEM_CODE = '{1}'";
            sql = string.Format(sql, _item_class, _item_code);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据分类和编码查询执行科室项目
        /// </summary>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <returns></returns>
        public IList<PERFORM_DEPT> GetPerformDeptByClassAndCode(BaseEntityer db, string _item_class, string _item_code)
        {
            string sql = @"SELECT PERFORM_DEPT.ITEM_CLASS ,
                           PERFORM_DEPT.ITEM_CODE ,
                           PERFORM_DEPT.PERFORMED_BY 
                           FROM PERFORM_DEPT 
                           WHERE PERFORM_DEPT.ITEM_CLASS = '{0}' AND PERFORM_DEPT.ITEM_CODE = '{1}' 
                           ORDER BY PERFORM_DEPT.ITEM_CLASS ASC ";
            sql = string.Format(sql, _item_class, _item_code);
            DataSet ds = db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<PERFORM_DEPT>(ds);
        }

        /// <summary>
        /// 新增价表项目执行科室项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="perform_dept_new"></param>
        /// <returns></returns>
        public int InsertPerformDept(BaseEntityer db, PERFORM_DEPT perform_dept_new)
        {
            string sql = @"INSERT INTO PERFORM_DEPT
                        (
                               PERFORM_DEPT.ITEM_CLASS,
                               PERFORM_DEPT.ITEM_CODE,
                               PERFORM_DEPT.PERFORMED_BY
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}'
                        )";
            object[] param = new object[] { perform_dept_new.ITEM_CLASS, perform_dept_new.ITEM_CODE, 
                perform_dept_new.PERFORMED_BY };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定价表项目执行科室数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <returns></returns>
        public int DeletePerformDept(BaseEntityer db, string _item_class, string _item_code)
        {
            string sql = @"delete from PERFORM_DEPT where PERFORM_DEPT.ITEM_CLASS = '{0}' 
                            AND PERFORM_DEPT.ITEM_CODE = '{1}'";
            sql = string.Format(sql, _item_class, _item_code);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 判断该价表是否对应诊疗项目
        /// </summary>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <param name="_item_spec"></param>
        /// <param name="_units"></param>
        /// <returns></returns>
        public DbDataReader GetClinicVSCharge(BaseEntityer db, string _item_class, string _item_code, string _item_spec, string _units)
        {
            string sql = @"SELECT COUNT(*) AS ITEMCOUNT 
                            FROM CLINIC_VS_CHARGE
                            WHERE CHARGE_ITEM_CLASS = '{0}' AND 
                                  CHARGE_ITEM_CODE = '{1}'   AND 
                                  CHARGE_ITEM_SPEC = '{2}'   AND 
                                  UNITS = '{3}'";
            sql = string.Format(sql, _item_class, _item_code, _item_spec, _units);
            return db.ExecuteReader(sql);
        }

        /// <summary>
        /// 诊疗项目类别项目类别集合
        /// </summary>
        /// <returns></returns>
        public IList<CLINIC_ITEM_CLASS_DICT> GetClinicItemClassDict()
        {
            string sql = @"SELECT CLINIC_ITEM_CLASS_DICT.SERIAL_NO ,
                           CLINIC_ITEM_CLASS_DICT.CLASS_CODE ,
                           CLINIC_ITEM_CLASS_DICT.CLASS_NAME ,
                           CLINIC_ITEM_CLASS_DICT.INPUT_CODE 
                           FROM CLINIC_ITEM_CLASS_DICT 
                           ORDER BY CLINIC_ITEM_CLASS_DICT.SERIAL_NO ASC ";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<CLINIC_ITEM_CLASS_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 临床诊疗项目集合
        /// </summary>
        /// <param name="_item_class">类别</param>
        /// <param name="_item_code">编码</param>
        /// <param name="_item_name">编码或者输入码</param>
        /// <param name="noclinicvscharge">查询未对照项目</param>
        /// <returns></returns>
        public IList<CLINIC_ITEM_DICT> GetClinicItemList(string _item_class, string _item_code, string _item_name, bool noclinicvscharge)
        {
            string sql = @"SELECT CLINIC_ITEM_DICT.ITEM_CLASS , 
                           CLINIC_ITEM_DICT.ITEM_SUBCLASS , 
                           CLINIC_ITEM_DICT.ITEM_CODE , 
                           CLINIC_ITEM_DICT.ITEM_NAME , 
                           CLINIC_ITEM_DICT.INPUT_CODE ,
                           CLINIC_ITEM_DICT.M_FLAG ,
                           CLINIC_ITEM_DICT.ITEM_SPEC,
                           CLINIC_ITEM_DICT.LAB_SAMPLE 
                           FROM CLINIC_ITEM_DICT ";
            if (_item_class == string.Empty)
            {
                //需要查询未对照项目时，增加查询条件
                if (noclinicvscharge)
                {
                    sql += @" where CLINIC_ITEM_DICT.ITEM_CODE not in ( 
                           select CLINIC_VS_CHARGE.CLINIC_ITEM_CODE from CLINIC_VS_CHARGE 
                           where CLINIC_ITEM_DICT.ITEM_CODE = CLINIC_VS_CHARGE.CLINIC_ITEM_CODE) ";
                }
                sql += @" ORDER BY CLINIC_ITEM_DICT.ITEM_CLASS ASC ";
            }
            else
            {
                sql += " WHERE CLINIC_ITEM_DICT.ITEM_CLASS = '{0}' ";
                if (_item_code != string.Empty)
                {
                    sql += " AND CLINIC_ITEM_DICT.ITEM_CODE = '{1}' ";
                }
                else if (_item_name != string.Empty)
                {
                    sql += " AND (CLINIC_ITEM_DICT.ITEM_CODE = '{2}' OR CLINIC_ITEM_DICT.ITEM_NAME like '%{2}%') ";
                }
                //需要查询未对照项目时，增加查询条件
                if (noclinicvscharge)
                {
                    sql += @" AND CLINIC_ITEM_DICT.ITEM_CODE not in ( 
                           select CLINIC_VS_CHARGE.CLINIC_ITEM_CODE from CLINIC_VS_CHARGE 
                           where CLINIC_ITEM_DICT.ITEM_CODE = CLINIC_VS_CHARGE.CLINIC_ITEM_CODE) ";
                }
                sql += " ORDER BY CLINIC_ITEM_DICT.ITEM_CLASS ASC ";
            }
            sql = string.Format(sql, _item_class, _item_code, _item_name);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<CLINIC_ITEM_DICT>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 新增临床诊疗项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_item_dict_new"></param>
        /// <returns></returns>
        public int InsertClinicItemDict(BaseEntityer db, CLINIC_ITEM_DICT clinic_item_dict_new, string OperCode, DateTime OperDate)
        {
            string sql = @"INSERT INTO CLINIC_ITEM_DICT
                        (
                               CLINIC_ITEM_DICT.ITEM_CLASS , 
                               CLINIC_ITEM_DICT.ITEM_SUBCLASS ,
                               CLINIC_ITEM_DICT.ITEM_CODE , 
                               CLINIC_ITEM_DICT.ITEM_NAME , 
                               CLINIC_ITEM_DICT.INPUT_CODE ,
                               CLINIC_ITEM_DICT.M_FLAG ,
                               CLINIC_ITEM_DICT.ITEM_SPEC,
                               CLINIC_ITEM_DICT.OPER_CODE ,
                               CLINIC_ITEM_DICT.OPER_DATE
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',to_date('{8}', 'yyyy-mm-dd hh24:mi:ss')
                        )";
            object[] param = new object[] { clinic_item_dict_new.ITEM_CLASS, clinic_item_dict_new.ITEM_SUBCLASS, clinic_item_dict_new.ITEM_CODE, 
                clinic_item_dict_new.ITEM_NAME, clinic_item_dict_new.INPUT_CODE, clinic_item_dict_new.M_FLAG,clinic_item_dict_new.ITEM_SPEC,OperCode,OperDate,clinic_item_dict_new};
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新临床诊疗项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_item_dict_new"></param>
        /// <param name="clinic_item_dict"></param>
        /// <returns></returns>
        public int UpdateClinicItemDict(BaseEntityer db, CLINIC_ITEM_DICT clinic_item_dict_new, CLINIC_ITEM_DICT clinic_item_dict, string OperCode, DateTime OperDate)
        {
            string sql = @"UPDATE CLINIC_ITEM_DICT 
                        SET 
                               CLINIC_ITEM_DICT.ITEM_CLASS = '{0}',
                               CLINIC_ITEM_DICT.ITEM_SUBCLASS = '{1}',
                               CLINIC_ITEM_DICT.ITEM_CODE = '{2}',
                               CLINIC_ITEM_DICT.ITEM_NAME = '{3}',
                               CLINIC_ITEM_DICT.INPUT_CODE = '{4}',
                               CLINIC_ITEM_DICT.M_FLAG = {5} ,
                               CLINIC_ITEM_DICT.ITEM_SPEC = '{6}',
                               CLINIC_ITEM_DICT.OPER_CODE = {9} ,
                               CLINIC_ITEM_DICT.OPER_DATE = to_date('{10}', 'yyyy-mm-dd hh24:mi:ss') 
                             
                        WHERE CLINIC_ITEM_DICT.ITEM_CLASS = '{7}' AND CLINIC_ITEM_DICT.ITEM_CODE = '{8}'";
            object[] param = new object[] { clinic_item_dict_new.ITEM_CLASS, clinic_item_dict_new.ITEM_SUBCLASS,clinic_item_dict_new.ITEM_CODE, 
                clinic_item_dict_new.ITEM_NAME, clinic_item_dict_new.INPUT_CODE, clinic_item_dict_new.M_FLAG, clinic_item_dict_new.ITEM_SPEC, 
                clinic_item_dict.ITEM_CLASS, clinic_item_dict.ITEM_CODE,OperCode,OperDate };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定临床诊疗项目数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="_item_class">类别</param>
        /// <param name="_item_code">编码</param>
        /// <returns></returns>
        public int DeleteClinicItemDict(BaseEntityer db, string _item_class, string _item_code)
        {
            string sql = @"DELETE FROM CLINIC_ITEM_DICT WHERE CLINIC_ITEM_DICT.ITEM_CLASS = '{0}' 
                            AND CLINIC_ITEM_DICT.ITEM_CODE = '{1}'";
            sql = string.Format(sql, _item_class, _item_code);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据分类和编码查询临床诊疗项目名称
        /// </summary>
        /// <param name="db"></param>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <returns></returns>
        public IList<CLINIC_ITEM_NAME_DICT> GetClinicItemNameDictByClassAndCode(BaseEntityer db, string _item_class, string _item_code)
        {
            string sql = @"SELECT CLINIC_ITEM_NAME_DICT.ITEM_CLASS ,
                           CLINIC_ITEM_NAME_DICT.ITEM_SUBCLASS ,
                           CLINIC_ITEM_NAME_DICT.ITEM_NAME ,
                           CLINIC_ITEM_NAME_DICT.ITEM_CODE ,
                           CLINIC_ITEM_NAME_DICT.STD_INDICATOR ,
                           CLINIC_ITEM_NAME_DICT.INPUT_CODE ,
                           CLINIC_ITEM_NAME_DICT.ITEM_SPEC,
                           CLINIC_ITEM_NAME_DICT.LAB_SAMPLE
                           FROM CLINIC_ITEM_NAME_DICT 
                           WHERE ( CLINIC_ITEM_NAME_DICT.ITEM_CLASS = '{0}' ) and 
                           ( CLINIC_ITEM_NAME_DICT.ITEM_CODE = '{1}' ) 
                           ORDER BY CLINIC_ITEM_NAME_DICT.ITEM_CLASS ASC ";
            sql = string.Format(sql, _item_class, _item_code);
            DataSet ds = db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_ITEM_NAME_DICT>(ds);
        }

        /// <summary>
        /// 新增临床诊疗项目名称
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_item_name_dict_new"></param>
        /// <returns></returns>
        public int InsertClinicItemNameDict(BaseEntityer db, CLINIC_ITEM_NAME_DICT clinic_item_name_dict_new)
        {
            string sql = @"INSERT INTO CLINIC_ITEM_NAME_DICT
                        (
                               CLINIC_ITEM_NAME_DICT.ITEM_CLASS ,
                               CLINIC_ITEM_NAME_DICT.ITEM_SUBCLASS ,
                               CLINIC_ITEM_NAME_DICT.ITEM_NAME ,
                               CLINIC_ITEM_NAME_DICT.ITEM_CODE ,
                               CLINIC_ITEM_NAME_DICT.STD_INDICATOR ,
                               CLINIC_ITEM_NAME_DICT.INPUT_CODE ,
                               CLINIC_ITEM_NAME_DICT.ITEM_SPEC ,
                               CLINIC_ITEM_NAME_DICT.LAB_SAMPLE 
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}'
                        )";
            object[] param = new object[] { clinic_item_name_dict_new.ITEM_CLASS, clinic_item_name_dict_new.ITEM_SUBCLASS, 
                clinic_item_name_dict_new.ITEM_NAME, clinic_item_name_dict_new.ITEM_CODE, 
                clinic_item_name_dict_new.STD_INDICATOR, clinic_item_name_dict_new.INPUT_CODE,clinic_item_name_dict_new.ITEM_SPEC,clinic_item_name_dict_new.LAB_SAMPLE };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定价表项目执行科室数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <returns></returns>
        public int DeleteClinicItemNameDict(BaseEntityer db, string _item_class, string _item_code)
        {
            string sql = @"DELETE FROM CLINIC_ITEM_NAME_DICT WHERE CLINIC_ITEM_NAME_DICT.ITEM_CLASS = '{0}' 
                            AND CLINIC_ITEM_NAME_DICT.ITEM_CODE = '{1}'";
            sql = string.Format(sql, _item_class, _item_code);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询诊疗项目对应价表项目列表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <returns></returns>
        public IList<CLINIC_VS_CHARGE> GetClinicVSChargeList(BaseEntityer db, string _item_class, string _item_code)
        {
            string sql = @"SELECT DISTINCT CLINIC_VS_CHARGE.CHARGE_ITEM_NO ,
                           CLINIC_VS_CHARGE.CHARGE_ITEM_CLASS ,
                           CLINIC_VS_CHARGE.CHARGE_ITEM_CODE ,
                           PRICE_LIST.ITEM_NAME ,
                           CLINIC_VS_CHARGE.CHARGE_ITEM_SPEC ,
                           CLINIC_VS_CHARGE.AMOUNT ,
                           CLINIC_VS_CHARGE.UNITS ,
                           PRICE_LIST.PRICE ,
                           CLINIC_VS_CHARGE.CLINIC_ITEM_CLASS ,
                           CLINIC_VS_CHARGE.CLINIC_ITEM_CODE ,
                           PRICE_LIST.PERFORMED_BY 
                           FROM CLINIC_VS_CHARGE INNER JOIN PRICE_LIST 
                           ON ( CLINIC_VS_CHARGE.CHARGE_ITEM_CLASS = PRICE_LIST.ITEM_CLASS ) AND 
                           ( CLINIC_VS_CHARGE.CHARGE_ITEM_CODE = PRICE_LIST.ITEM_CODE ) AND 
                           ( CLINIC_VS_CHARGE.CHARGE_ITEM_SPEC = PRICE_LIST.ITEM_SPEC )
                           WHERE (SYSDATE >= PRICE_LIST.START_DATE AND 
                           (SYSDATE<PRICE_LIST.STOP_DATE OR PRICE_LIST.STOP_DATE IS NULL)) AND 
                           ( CLINIC_VS_CHARGE.CLINIC_ITEM_CLASS = '{0}' ) AND
                           ( CLINIC_VS_CHARGE.CLINIC_ITEM_CODE = '{1}' ) 
                           ORDER BY CLINIC_VS_CHARGE.CHARGE_ITEM_NO ASC ";
            sql = string.Format(sql, _item_class, _item_code);
            DataSet ds = db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<CLINIC_VS_CHARGE>(ds);
        }

        /// <summary>
        /// 新增临床诊疗项目与价表项目对应关系
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_vs_charge"></param>
        /// <returns></returns>
        public int InsertClinicVSCharge(BaseEntityer db, CLINIC_VS_CHARGE clinic_vs_charge)
        {
            string sql = @"INSERT INTO CLINIC_VS_CHARGE
                        (
                               CLINIC_VS_CHARGE.CLINIC_ITEM_CLASS ,
                               CLINIC_VS_CHARGE.CLINIC_ITEM_CODE ,
                               CLINIC_VS_CHARGE.CHARGE_ITEM_NO ,
                               CLINIC_VS_CHARGE.CHARGE_ITEM_CLASS ,
                               CLINIC_VS_CHARGE.CHARGE_ITEM_CODE ,
                               CLINIC_VS_CHARGE.CHARGE_ITEM_SPEC ,
                               CLINIC_VS_CHARGE.AMOUNT ,
                               CLINIC_VS_CHARGE.UNITS 
                        )
                        VALUES
                        (
                               '{0}','{1}',{2},'{3}','{4}','{5}',{6},'{7}'
                        )";
            object[] param = new object[] { clinic_vs_charge.CLINIC_ITEM_CLASS, clinic_vs_charge.CLINIC_ITEM_CODE, 
                clinic_vs_charge.CHARGE_ITEM_NO, clinic_vs_charge.CHARGE_ITEM_CLASS, clinic_vs_charge.CHARGE_ITEM_CODE, 
                clinic_vs_charge.CHARGE_ITEM_SPEC, clinic_vs_charge.AMOUNT, clinic_vs_charge.UNITS };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定临床诊疗项目与价表项目对应关系
        /// </summary>
        /// <param name="db"></param>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <returns></returns>
        public int DeleteClinicVSCharge(BaseEntityer db, string _item_class, string _item_code)
        {
            string sql = @"DELETE FROM CLINIC_VS_CHARGE WHERE CLINIC_VS_CHARGE.CLINIC_ITEM_CLASS = '{0}' 
                            AND CLINIC_VS_CHARGE.CLINIC_ITEM_CODE = '{1}'";
            sql = string.Format(sql, _item_class, _item_code);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2013-12-10 by li 插入价表变化日志
        /// </summary>
        /// <param name="db"></param>
        /// <param name="clinic_vs_charge"></param>
        /// <returns></returns>
        public int InsertPriceListLog(BaseEntityer db, PRICE_LIST price_list_item_new,
            string item_class_old, string item_code_old, string item_spec_old, string units_old)
        {
            string sql = @"INSERT INTO PRICE_LIST_LOG
                        (
                               PRICE_LIST_LOG.ITEM_CLASS_OLD ,
                               PRICE_LIST_LOG.ITEM_CODE_OLD ,
                               PRICE_LIST_LOG.ITEM_SPEC_OLD ,
                               PRICE_LIST_LOG.UNITS_OLD ,

                               PRICE_LIST_LOG.ITEM_CLASS ,
                               PRICE_LIST_LOG.ITEM_CODE ,
                               PRICE_LIST_LOG.ITEM_NAME ,
                               PRICE_LIST_LOG.ITEM_SPEC ,
                               PRICE_LIST_LOG.UNITS ,
                               PRICE_LIST_LOG.PRICE ,
                               PRICE_LIST_LOG.PREFER_PRICE ,
                               PRICE_LIST_LOG.PERFORMED_BY ,
                               PRICE_LIST_LOG.FEE_TYPE_MASK ,
                               PRICE_LIST_LOG.CLASS_ON_INP_RCPT ,
                               PRICE_LIST_LOG.CLASS_ON_OUTP_RCPT ,
                               PRICE_LIST_LOG.CLASS_ON_RECKONING ,
                               PRICE_LIST_LOG.SUBJ_CODE ,
                               PRICE_LIST_LOG.CLASS_ON_MR ,
                               PRICE_LIST_LOG.MEMO ,
                               PRICE_LIST_LOG.START_DATE ,
                               PRICE_LIST_LOG.STOP_DATE ,
                               PRICE_LIST_LOG.OPERATOR ,
                               PRICE_LIST_LOG.ENTER_DATE ,
                               --PRICE_LIST_LOG.ZFYP ,
                               --PRICE_LIST_LOG.SBZFYP ,
                               --PRICE_LIST_LOG.SBGKBZ ,
                               PRICE_LIST_LOG.Foreigner_Price ,
                               PRICE_LIST_LOG.PRODUCE_FACTORY ,
                               PRICE_LIST_LOG.PRODUCE_REG_NO 
                        )
                        VALUES
                        (
                               '{22}','{23}','{24}','{25}', '{0}','{1}','{2}','{3}',
                                '{4}',{5},{6},'{7}',{8},'{9}','{10}','{11}',
                                '{12}','{13}','{14}',to_date('{15}', 'yyyy-mm-dd hh24:mi:ss'),
                                to_date('{16}', 'yyyy-mm-dd hh24:mi:ss'),'{17}',
                                to_date('{18}', 'yyyy-mm-dd hh24:mi:ss'),{19},'{20}','{21}'
                        )";
            object[] param = new object[] { price_list_item_new.ITEM_CLASS, price_list_item_new.ITEM_CODE, 
                price_list_item_new.ITEM_NAME, price_list_item_new.ITEM_SPEC, price_list_item_new.UNITS, 
                price_list_item_new.PRICE, price_list_item_new.PREFER_PRICE, price_list_item_new.PERFORMED_BY, 
                price_list_item_new.FEE_TYPE_MASK, price_list_item_new.CLASS_ON_INP_RCPT, 
                price_list_item_new.CLASS_ON_OUTP_RCPT, price_list_item_new.CLASS_ON_RECKONING, 
                price_list_item_new.SUBJ_CODE, price_list_item_new.CLASS_ON_MR, price_list_item_new.MEMO, 
                price_list_item_new.START_DATE, price_list_item_new.STOP_DATE, price_list_item_new.OPERATOR, 
                price_list_item_new.ENTER_DATE, price_list_item_new.FOREIGNER_PRICE, 
                price_list_item_new.PRODUCE_FACTORY, price_list_item_new.PRODUCE_REG_NO, 
                item_class_old, item_code_old, item_spec_old, units_old };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2013-12-10 by li 价表日志项目集合
        /// </summary>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <param name="_item_spec"></param>
        /// <param name="_units"></param>
        /// <returns></returns>
        public IList<PRICE_LIST_LOG> GetPriceListLog(string _item_class, string _item_code, string _item_spec, string _units)
        {
            string sql = @"SELECT PRICE_LIST_LOG.ITEM_CLASS_OLD,
                               PRICE_LIST_LOG.ITEM_CODE_OLD,
                               PRICE_LIST_LOG.ITEM_SPEC_OLD,
                               PRICE_LIST_LOG.UNITS_OLD,
                               PRICE_LIST_LOG.ITEM_CLASS,
                               PRICE_LIST_LOG.ITEM_CODE,
                               PRICE_LIST_LOG.ITEM_NAME,
                               PRICE_LIST_LOG.ITEM_SPEC,
                               PRICE_LIST_LOG.UNITS,
                               PRICE_LIST_LOG.PRICE,
                               PRICE_LIST_LOG.PREFER_PRICE,
                               PRICE_LIST_LOG.PERFORMED_BY,
                               PRICE_LIST_LOG.FEE_TYPE_MASK,
                               PRICE_LIST_LOG.CLASS_ON_INP_RCPT,
                               PRICE_LIST_LOG.CLASS_ON_OUTP_RCPT,
                               PRICE_LIST_LOG.CLASS_ON_RECKONING,
                               PRICE_LIST_LOG.SUBJ_CODE,
                               PRICE_LIST_LOG.CLASS_ON_MR,
                               PRICE_LIST_LOG.MEMO,
                               PRICE_LIST_LOG.START_DATE,
                               PRICE_LIST_LOG.STOP_DATE,
                               PRICE_LIST_LOG.OPERATOR,
                               PRICE_LIST_LOG.ENTER_DATE,
                               --PRICE_LIST_LOG.ZFYP ,
                               --PRICE_LIST_LOG.SBZFYP ,
                               --PRICE_LIST_LOG.SBGKBZ ,
                               PRICE_LIST_LOG.Foreigner_Price,
                               PRICE_LIST_LOG.PRODUCE_FACTORY,
                               PRICE_LIST_LOG.PRODUCE_REG_NO,
                               PRICE_LIST_LOG.OPERATE_DATE
                          FROM PRICE_LIST_LOG
                         WHERE PRICE_LIST_LOG.ITEM_CLASS_OLD = '{0}'
                           and PRICE_LIST_LOG.ITEM_CODE_OLD = '{1}'
                           and PRICE_LIST_LOG.ITEM_SPEC_OLD = '{2}'
                           and PRICE_LIST_LOG.UNITS_OLD = '{3}'
                         order by PRICE_LIST_LOG.OPERATE_DATE DESC";
            sql = string.Format(sql, _item_class, _item_code, _item_spec, _units);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<PRICE_LIST_LOG>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        /// <summary>
        /// 2013-12-10 by li 价表日志项目集合
        /// </summary>
        /// <param name="_item_class"></param>
        /// <param name="_item_code"></param>
        /// <param name="_item_spec"></param>
        /// <param name="_units"></param>
        /// <returns></returns>
        public IList<PRICE_LIST_LOG> GetPriceListLogs(string dateStart, string dateEnd)
        {
            string sql = @"SELECT PRICE_LIST_LOG.ITEM_CLASS_OLD,
                               PRICE_LIST_LOG.ITEM_CODE_OLD,
                               PRICE_LIST_LOG.ITEM_SPEC_OLD,
                               PRICE_LIST_LOG.UNITS_OLD,
                               PRICE_LIST_LOG.ITEM_CLASS,
                               PRICE_LIST_LOG.ITEM_CODE,
                               PRICE_LIST_LOG.ITEM_NAME,
                               PRICE_LIST_LOG.ITEM_SPEC,
                               PRICE_LIST_LOG.UNITS,
                               PRICE_LIST_LOG.PRICE,
                               PRICE_LIST_LOG.PREFER_PRICE,
                               PRICE_LIST_LOG.PERFORMED_BY,
                               PRICE_LIST_LOG.FEE_TYPE_MASK,
                               PRICE_LIST_LOG.CLASS_ON_INP_RCPT,
                               PRICE_LIST_LOG.CLASS_ON_OUTP_RCPT,
                               PRICE_LIST_LOG.CLASS_ON_RECKONING,
                               PRICE_LIST_LOG.SUBJ_CODE,
                               PRICE_LIST_LOG.CLASS_ON_MR,
                               PRICE_LIST_LOG.MEMO,
                               PRICE_LIST_LOG.START_DATE,
                               PRICE_LIST_LOG.STOP_DATE,
                               PRICE_LIST_LOG.OPERATOR,
                               PRICE_LIST_LOG.ENTER_DATE,
                               --PRICE_LIST_LOG.ZFYP ,
                               --PRICE_LIST_LOG.SBZFYP ,
                               --PRICE_LIST_LOG.SBGKBZ ,
                               PRICE_LIST_LOG.Foreigner_Price,
                               PRICE_LIST_LOG.PRODUCE_FACTORY,
                               PRICE_LIST_LOG.PRODUCE_REG_NO,
                               PRICE_LIST_LOG.OPERATE_DATE
                          FROM PRICE_LIST_LOG
                         WHERE PRICE_LIST_LOG.OPERATE_DATE >=
                               to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                           and PRICE_LIST_LOG.OPERATE_DATE <=
                               to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                         order by PRICE_LIST_LOG.OPERATE_DATE DESC";
            sql = string.Format(sql, dateStart, dateEnd);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<PRICE_LIST_LOG>(ds);
            if (list.Count > 0)
                return list;
            else return null;
        }

        #endregion

        #region 医保信息对照

        /// <summary>
        /// 获取药品未对照数据列表
        /// </summary>
        /// <param name="fee_type">药品类别编码</param>
        /// <returns></returns>
        public DataTable GetMedicineNoneCompareHisDrug(string charge_type_code, string fee_type)
        {
            string sql = @"SELECT PRICE_LIST_NEW.ITEM_CLASS ,
                           DRUG_DICT.DRUG_INDICATOR ,
                           PRICE_LIST_NEW.ITEM_CODE ,
                           PRICE_LIST_NEW.ITEM_NAME ,
                          (SELECT m.Ratify_No  FROM  drug_price_list m  WHERE m.Drug_Code=PRICE_LIST_NEW.ITEM_CODE and  Rownum=1  and m.stop_date is null) as RATIFY_NO,
                           PRICE_LIST_NEW.ITEM_SPEC ,
                           PRICE_LIST_NEW.Units ,
                           PRICE_LIST_NEW.Price ,
                           DRUG_DICT.DRUG_FORM ,
                           PRICE_LIST_NEW.Input_Code ,
                           PRICE_LIST_NEW.Start_Date ,
                           PRICE_LIST_NEW.Stop_Date ,

                           PRICE_LIST_NEW.Memo 
                           FROM
                           (
                           SELECT PRICE_LIST.*,
                           PRICE_ITEM_NAME_DICT.Input_Code 
                           FROM PRICE_LIST LEFT JOIN
                                PRICE_ITEM_NAME_DICT ON
                                PRICE_LIST.ITEM_CODE = PRICE_ITEM_NAME_DICT.Item_Code 
                                AND PRICE_LIST.ITEM_CLASS = PRICE_ITEM_NAME_DICT.ITEM_CLASS 
                           WHERE ( PRICE_LIST.ITEM_CLASS = 'A' or ( PRICE_LIST.ITEM_CLASS = 'B') ) and 
                                 ( PRICE_LIST.Stop_Date is null and PRICE_LIST.price>0) and 
                                 ( PRICE_ITEM_NAME_DICT.STD_INDICATOR = 1 ) and 
                                 ( PRICE_LIST.ITEM_CODE NOT in 
                                 ( SELECT HIS_CODE FROM HIS_COMPARE WHERE CHARGE_TYPE_CODE = '{1}' ) )) 
                                 PRICE_LIST_NEW LEFT JOIN DRUG_DICT ON
                                 PRICE_LIST_NEW.ITEM_CODE = DRUG_DICT.DRUG_CODE 
                                 WHERE DRUG_DICT.DRUG_INDICATOR = {0}";
            if (charge_type_code == "2" || charge_type_code == "3")
            {
                sql = @"SELECT PRICE_LIST_NEW.ITEM_CLASS ,
                           DRUG_DICT.DRUG_INDICATOR ,
                           PRICE_LIST_NEW.ITEM_CODE ,
                           PRICE_LIST_NEW.ITEM_NAME ,
                          (SELECT m.Ratify_No  FROM  drug_price_list m  WHERE m.Drug_Code=PRICE_LIST_NEW.ITEM_CODE and     m.drug_spec || m.firm_id = price_list_new.item_spec and  Rownum=1  and m.stop_date is null) as RATIFY_NO,
                           PRICE_LIST_NEW.ITEM_SPEC ,
                           PRICE_LIST_NEW.Units ,
                           PRICE_LIST_NEW.Price ,
                           DRUG_DICT.DRUG_FORM ,
                           PRICE_LIST_NEW.Input_Code ,
                           PRICE_LIST_NEW.Start_Date ,
                           PRICE_LIST_NEW.Stop_Date ,

                           PRICE_LIST_NEW.Memo 
                           FROM
                           (
                           SELECT PRICE_LIST.*,
                           PRICE_ITEM_NAME_DICT.Input_Code 
                           FROM PRICE_LIST LEFT JOIN
                                PRICE_ITEM_NAME_DICT ON
                                PRICE_LIST.ITEM_CODE = PRICE_ITEM_NAME_DICT.Item_Code 
                                AND PRICE_LIST.ITEM_CLASS = PRICE_ITEM_NAME_DICT.ITEM_CLASS 
                           WHERE ( PRICE_LIST.ITEM_CLASS = 'A' or ( PRICE_LIST.ITEM_CLASS = 'B') ) and 
                                 ( PRICE_LIST.Stop_Date is null and PRICE_LIST.price>0) and 
                                 ( PRICE_ITEM_NAME_DICT.STD_INDICATOR = 1 ) and 
                                 ( PRICE_LIST.ITEM_CODE || ITEM_SPEC NOT in 
                                 ( SELECT HIS_CODE || HIS_SPECS FROM HIS_COMPARE WHERE CHARGE_TYPE_CODE = '{1}' ) )) 
                                 PRICE_LIST_NEW LEFT JOIN DRUG_DICT ON
                                 PRICE_LIST_NEW.ITEM_CODE = DRUG_DICT.DRUG_CODE 
                                 WHERE DRUG_DICT.DRUG_INDICATOR = {0}";
            }
            sql = string.Format(sql, fee_type, charge_type_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取未对照诊疗项目数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetNoneCompareHisClinic(string charge_type_code)
        {
            string sql = @"SELECT PRICE_LIST.ITEM_CLASS ,
                           PRICE_LIST.ITEM_CODE ,
                           PRICE_LIST.ITEM_NAME ,
                           PRICE_LIST.PRODUCE_FACTORY ,
                           PRICE_LIST.PRODUCE_REG_NO ,
                           PRICE_LIST.ITEM_SPEC ,
                           PRICE_LIST.Units ,
                           PRICE_LIST.Price ,
                           PRICE_ITEM_NAME_DICT.Input_Code ,
                           PRICE_LIST.Start_Date ,
                           PRICE_LIST.Stop_Date ,
                           PRICE_LIST.Memo 
                           FROM PRICE_LIST ,
                                PRICE_ITEM_NAME_DICT 
                           WHERE ( PRICE_LIST.ITEM_CODE = PRICE_ITEM_NAME_DICT.Item_Code ) and 
                                 ( PRICE_LIST.ITEM_CLASS = 'C' or 
                                 ( PRICE_LIST.ITEM_CLASS = 'D' ) or 
                                 ( PRICE_LIST.ITEM_CLASS = 'E' ) or 
                                 ( PRICE_LIST.ITEM_CLASS = 'F' ) or 
                                 ( PRICE_LIST.ITEM_CLASS = 'G' ) or 
                                 ( PRICE_LIST.ITEM_CLASS = 'H' ) or 
                                 ( PRICE_LIST.ITEM_CLASS = 'I') ) and 
                                 ( PRICE_LIST.Stop_Date is null) and 
                                 ( PRICE_ITEM_NAME_DICT.STD_INDICATOR = 1 ) and 
                                 ( PRICE_LIST.ITEM_CODE not in 
                                 ( select his_code from his_compare where his_class = PRICE_LIST.item_class 
                                   AND CHARGE_TYPE_CODE = '{0}' ) )";
            sql = string.Format(sql, charge_type_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取未对照服务项目数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetNoneCompareHisService(string charge_type_code)
        {
            string sql = @"SELECT PRICE_LIST.ITEM_CLASS ,
                           PRICE_LIST.ITEM_CODE ,
                           PRICE_LIST.ITEM_NAME ,
                           PRICE_LIST.PRODUCE_FACTORY ,
                           PRICE_LIST.PRODUCE_REG_NO ,
                           PRICE_LIST.ITEM_SPEC ,
                           PRICE_LIST.Units ,
                           PRICE_LIST.Price ,
                           PRICE_ITEM_NAME_DICT.Input_Code ,
                           PRICE_LIST.Start_Date ,
                           PRICE_LIST.Stop_Date ,
                           PRICE_LIST.Memo 
                           FROM PRICE_LIST ,
                                PRICE_ITEM_NAME_DICT 
                           WHERE ( PRICE_LIST.ITEM_CODE = PRICE_ITEM_NAME_DICT.Item_Code ) and 
                                 ( PRICE_LIST.ITEM_CLASS = 'J' or 
                                 ( PRICE_LIST.ITEM_CLASS = 'K' ) or 
                                 ( PRICE_LIST.ITEM_CLASS = 'L' ) or 
                                 ( PRICE_LIST.ITEM_CLASS = 'Z') ) and 
                                 ( PRICE_LIST.Stop_Date is null) and 
                                 ( PRICE_ITEM_NAME_DICT.STD_INDICATOR = 1 ) and 
                                 ( PRICE_LIST.ITEM_CODE not in 
                                 ( select his_code from his_compare where his_class = price_list.item_class 
                                   AND CHARGE_TYPE_CODE = '{0}' ) )";
            sql = string.Format(sql, charge_type_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取病种未对照数据列表
        /// </summary>
        /// <param name="fee_type">对照类别编码</param>
        /// <returns></returns>
        public DataTable GetNoneCompareHisDiagnosis(string charge_type_code, string fee_type)
        {
            string sql = @"SELECT DIAGNOSIS_DICT.DIAGNOSIS_CODE ,
                           DIAGNOSIS_DICT.DIAGNOSIS_NAME ,
                           DIAGNOSIS_DICT.STD_INDICATOR ,
                           DIAGNOSIS_DICT.APPROVED_INDICATOR ,
                           DIAGNOSIS_DICT.CREATE_DATE ,
                           DIAGNOSIS_DICT.INPUT_CODE 
                           FROM DIAGNOSIS_DICT 
                           WHERE ( DIAGNOSIS_DICT.STD_INDICATOR = 1 ) and 
                                 ( DIAGNOSIS_DICT.DIAGNOSIS_CODE not in 
                                 ( select his_code from his_compare where fee_type = '{0}' 
                                   AND CHARGE_TYPE_CODE = '{1}' ) )";
            sql = string.Format(sql, fee_type, charge_type_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取药品未对照中心数据列表
        /// </summary>
        /// <param name="charge_type_code"></param>
        /// <returns></returns>
        public DataTable GetNoneCompareCenterDrug(string charge_type_code)
        {
            string sql = @"SELECT 
  SI_SYDRUG.drug_no,
  SI_SYDRUG.common_name,
  SI_SYDRUG.english_name,
  SI_SYDRUG.trade_name,
  SI_SYDRUG.trade_input,
  SI_SYDRUG.common_input,
  SI_SYDRUG.sidrug_no,
  SI_SYDRUG.fda_no,
  SI_SYDRUG.fda_standardcode,
  SI_SYDRUG.fee_type,
  SI_SYDRUG.fee_itemgrade,
  SI_SYDRUG.prescription_flag,
  SI_SYDRUG.dosage,
  SI_SYDRUG.dosage_unit,
  SI_SYDRUG.once_dosage,
  SI_SYDRUG.frequency,
  SI_SYDRUG.unit,
  SI_SYDRUG.specification,
  SI_SYDRUG.hosp_preparation,
  SI_SYDRUG.hospital_id,
  SI_SYDRUG.specialdrug_flag,
  SI_SYDRUG.usage,
  SI_SYDRUG.limit_days,
  SI_SYDRUG.produce_unit,
  SI_SYDRUG.country_med_accuate,
  SI_SYDRUG.produce_area,
  SI_SYDRUG.valuation_unit,
  SI_SYDRUG.drug_small_class,
  SI_SYDRUG.drug_big_class,
  SI_SYDRUG.injure_flag,
  SI_SYDRUG.birth_flag,
  SI_SYDRUG.basemedical_flag,
  SI_SYDRUG.account_use_flag,
  SI_SYDRUG.drugstore_use_flag,
  SI_SYDRUG.out_pub_use_flag,
  SI_SYDRUG.universalcode_flag,
  SI_SYDRUG.drug_type,
  SI_SYDRUG.domestic_or_import,
  SI_SYDRUG.drug_special_limit_flag,
  SI_SYDRUG.drug_special_limit_range,
  SI_SYDRUG.drug_onetype,
  SI_SYDRUG.drug_twotype,
  SI_SYDRUG.drug_thirdtype,
  SI_SYDRUG.drug_fourthtype,
  SI_SYDRUG.begin_date,
  SI_SYDRUG.end_date,
  SI_SYDRUG.vaild_falg,
  SI_SYDRUG.remark,
  SI_SYDRUG.oper_coer,
  SI_SYDRUG.oper_date,
  SI_SYDRUG.max_limit_price,
  SI_SYDRUG.over_own_ratio,
  SI_SYDRUG.pub_own_ratio,
  SI_SYDRUG.birth_own_ratio,
  SI_SYDRUG.overtake_over_ratio,
  SI_SYDRUG.transfer_flag,
  SI_SYDRUG.drug_common_limit_range,
  SI_SYDRUG.drug_common_limit_flag,
  SI_SYDRUG.approve_number,
  SI_SYDRUG.update_date,
  SI_SYDRUG.charge_code
                           FROM SI_SYDRUG WHERE SI_SYDRUG.CHARGE_CODE = '{0}' 
                           ORDER BY SI_SYDRUG.TRADE_NAME";
            sql = string.Format(sql, charge_type_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        #region 贵阳医保对照药品中心数据列表

        /// <summary>
        /// 获取贵阳药品目录
        /// </summary>
        /// <param name="charge_type_code"></param>
        /// <param name="input_code"></param>
        /// <param name="drug_name"></param>
        /// <returns></returns>
        public DataTable GetGYNoneCompareCenterDrug(string charge_type_code, string input_code, string drug_name)
        {
            string sql = @"SELECT SI_SYDRUG.DRUG_NO ,
                           SI_SYDRUG.COMMON_NAME ,
                           SI_SYDRUG.TRADE_NAME ,
                           SI_SYDRUG.FEE_TYPE ,
                           SI_SYDRUG.APPROVE_NUMBER ,
                           SI_SYDRUG.SPECIFICATION ,
                           SI_SYDRUG.UNIT ,
                           SI_SYDRUG.MAX_LIMIT_PRICE ,
                           SI_SYDRUG.FEE_ITEMGRADE ,
                           SI_SYDRUG.DOSAGE ,
                           SI_SYDRUG.DOSAGE_UNIT ,
                           SI_SYDRUG.PRODUCE_UNIT ,
                           SI_SYDRUG.DRUG_SMALL_CLASS ,
                           SI_SYDRUG.DRUG_BIG_CLASS ,
                           SI_SYDRUG.INJURE_FLAG ,
                           SI_SYDRUG.BIRTH_FLAG ,
                           SI_SYDRUG.BASEMEDICAL_FLAG ,
                           SI_SYDRUG.OUT_PUB_USE_FLAG ,
                           SI_SYDRUG.DOMESTIC_OR_IMPORT ,
                           SI_SYDRUG.DRUG_SPECIAL_LIMIT_RANGE ,
                           SI_SYDRUG.DRUG_ONETYPE ,
                           SI_SYDRUG.DRUG_TWOTYPE ,
                           SI_SYDRUG.DRUG_THIRDTYPE ,
                           SI_SYDRUG.DRUG_FOURTHTYPE ,
                           SI_SYDRUG.DRUG_COMMON_LIMIT_RANGE ,
                           SI_SYDRUG.DRUG_COMMON_LIMIT_FLAG ,
                           SI_SYDRUG.PRODUCE_AREA ,
                           SI_SYDRUG.USAGE ,
                           SI_SYDRUG.ONCE_DOSAGE ,
                           SI_SYDRUG.FREQUENCY ,
                           SI_SYDRUG.DRUG_SPECIAL_LIMIT_FLAG ,
                           SI_SYDRUG.TRADE_INPUT ,
                           SI_SYDRUG.OPER_DATE ,
                           SI_SYDRUG.REMARK,
                           SI_SYDRUG.Pub_Own_Ratio  
                           FROM SI_SYDRUG WHERE SI_SYDRUG.CHARGE_CODE = '{0}' 
                           and (SI_SYDRUG.TRADE_INPUT like '%{1}%'
                           or SI_SYDRUG.Trade_Name like '%{2}%')
                           ORDER BY SI_SYDRUG.TRADE_NAME";
            sql = string.Format(sql, charge_type_code, input_code, drug_name);
            return BaseEntityer.Db.GetDataTable(sql);
        }


        #endregion

        /// <summary>
        /// 获取未对照服务项目/诊疗中心数据列表
        /// </summary>
        /// <param name="charge_type_code"></param>
        /// <returns></returns>
        public DataTable GetNoneCompareCenterService(string charge_type_code)
        {
            string sql = @" SELECT SI_SYUNDRUG.ITEM_NO,
                                   SI_SYUNDRUG.ITEM_NAME,
                                   SI_SYUNDRUG.FEE_TYPE,
                                   SI_SYUNDRUG.FEE_ITEMGRADE,
                                   SI_SYUNDRUG.SPECIFICATION,
                                   SI_SYUNDRUG.UNIT,
                                   SI_SYUNDRUG.PRICE,
                                   SI_SYUNDRUG.PRODEUCE_AREA,
                                   SI_SYUNDRUG.MATERIAL_CATALOG,
                                   SI_SYUNDRUG.MATERIAL_MED_NAME,
                                   SI_SYUNDRUG.INJURE_FLAG,
                                   SI_SYUNDRUG.BIRTH_FLAG,
                                   SI_SYUNDRUG.BASEMEDICAL_FLAG,
                                   SI_SYUNDRUG.DRUGSTORE_USE_FLAG,
                                   SI_SYUNDRUG.PRODUCE_FACTORY,
                                   SI_SYUNDRUG.LIMIT_USE_RANGE,
                                   SI_SYUNDRUG.PRODUCE_REG_NO,
                                   SI_SYUNDRUG.PRODUCE_REG_NAME,
                                   SI_SYUNDRUG.BRAND,
                                   SI_SYUNDRUG.AGENCY,
                                   SI_SYUNDRUG.MATERIAL_LIMITUSE_FLAG,
                                   SI_SYUNDRUG.ISNEED_SITECODE,
                                   SI_SYUNDRUG.ITEM_SPELL,
                                   SI_SYUNDRUG.OPER_DATE,
                                   SI_SYUNDRUG.REMARK,
                                   SI_SYUNDRUG.Max_Limit_Price,
                                   SI_SYUNDRUG.Pub_Own_Ratio,
                                   SI_SYUNDRUG.OVER_OWN_RATIO,
                                   SI_SYUNDRUG.BIRTH_FLAG,
                                   SI_SYUNDRUG.INJURE_FLAG,
                                   SI_SYUNDRUG.SPECIALITEM_PERSON,
                                   SI_SYUNDRUG.ALONE_ITEM_TYPE,
                                   SI_SYUNDRUG.BEGIN_DATE,
                                   SI_SYUNDRUG.END_DATE
                              FROM SI_SYUNDRUG
                             WHERE SI_SYUNDRUG.CHARGE_CODE = '{0}'
                            UNION
                            SELECT SI_SYSERVER.SERVER_CODE AS ITEM_NO,
                                   SI_SYSERVER.SERVER_NAME AS ITEM_NAME,
                                   SI_SYSERVER.FEE_TYPE,
                                   SI_SYSERVER.FEE_ITEMGRADE,
                                   '' AS SPECIFICATION,
                                   '' AS UNIT,
                                   SI_SYSERVER.MAX_LIMIT_PRICE AS PRICE,
                                   '' AS PRODEUCE_AREA,
                                   '' AS MATERIAL_CATALOG,
                                   '' AS MATERIAL_MED_NAME,
                                   '' AS INJURE_FLAG,
                                   '' AS BIRTH_FLAG,
                                   '' AS BASEMEDICAL_FLAG,
                                   '' AS DRUGSTORE_USE_FLAG,
                                   '' AS PRODUCE_FACTORY,
                                   '' AS LIMIT_USE_RANGE,
                                   '' AS PRODUCE_REG_NO,
                                   '' AS PRODUCE_REG_NAME,
                                   '' AS BRAND,
                                   '' AS AGENCY,
                                   '' AS MATERIAL_LIMITUSE_FLAG,
                                   '' AS ISNEED_SITECODE,
                                   SI_SYSERVER.SERVER_SPELL AS ITEM_SPELL,
                                   SI_SYSERVER.OPER_DATE,
                                   '' AS REMARK,
                                   SI_SYSERVER.Max_Limit_Price,
                                   SI_SYSERVER.Pub_Own_Ratio,
                                   '' OVER_OWN_RATIO,
                                   '' BIRTH_FLAG,
                                   '' INJURE_FLAG,
                                   '' SPECIALITEM_PERSON,
                                   '' ALONE_ITEM_TYPE,
                                   '' BEGIN_DATE,
                                   '' END_DATE
                              FROM SI_SYSERVER
                             WHERE SI_SYSERVER.CHARGE_CODE = '{0}'
                             ORDER BY ITEM_NO ";
            sql = string.Format(sql, charge_type_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取病种未对照中心数据列表
        /// </summary>
        /// <param name="charge_type_code"></param>
        /// <returns></returns>
        public DataTable GetNoneCompareCenterDiagnosis(string charge_type_code)
        {
            string sql = @"SELECT SI_SYDIAGNOSE.DIAGNOSE_CODE ,
                           SI_SYDIAGNOSE.DIAGNOSE_NAME ,
                           SI_SYDIAGNOSE.DIAGNOSE_TYPE ,
                           SI_SYDIAGNOSE.DIAGNOSE_SPELL ,
                           SI_SYDIAGNOSE.OPER_DATE ,
                           SI_SYDIAGNOSE.REMARK 
                           FROM SI_SYDIAGNOSE WHERE SI_SYDIAGNOSE.CHARGE_CODE = '{0}' 
                           ORDER BY SI_SYDIAGNOSE.DIAGNOSE_NAME ";
            sql = string.Format(sql, charge_type_code);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 医保已对照信息查询
        /// </summary>
        /// <param name="charge_type_code">费别</param>
        /// <param name="fee_type">对照类别</param>
        /// <returns></returns>
        public IList<HIS_COMPARE> GetHisCompare(string charge_type_code, string fee_type)
        {
            //2013-6-13 by li 四平农合对照项目增加
            string sqlcoloum = string.Empty;
            string sqltablename = string.Empty;
            string sqlwhere = string.Empty;
            string sql1 = @"SELECT HIS_COMPARE.His_Code ,
                           HIS_COMPARE.CENTER_CODE ,
                           HIS_COMPARE.Fee_Type ,
                           HIS_COMPARE.Center_Name ,
                           HIS_COMPARE.Center_Specs ,
                           HIS_COMPARE.CENTER_UNIT ,
                           HIS_COMPARE.Center_Price ,
                           HIS_COMPARE.Center_Type ,
                           HIS_COMPARE.Center_Rate ,
                           HIS_COMPARE.Center_Pack ,
                           HIS_COMPARE.Center_Place ,
                           HIS_COMPARE.His_Name ,
                           HIS_COMPARE.His_Spell ,
                           HIS_COMPARE.His_Specs ,
                           HIS_COMPARE.His_Unit ,
                           HIS_COMPARE.His_Price ,
                           HIS_COMPARE.HIS_TYPE ,
                           HIS_COMPARE.His_Pack ,
                           HIS_COMPARE.His_Place ,
                           HIS_COMPARE.Oper_Code ,
                           HIS_COMPARE.Oper_Date ,
                           HIS_COMPARE.Applyflag ,
                           HIS_COMPARE.Personrate ,
                           HIS_COMPARE.His_Wb_Code ,
                           HIS_COMPARE.His_User_Code ,
                           HIS_COMPARE.Trans ,
                           HIS_COMPARE.His_Class ,
                           HIS_COMPARE.Center_Class ,
                           HIS_COMPARE.CHARGE_TYPE_CODE ,
                           HIS_COMPARE.DRUG_TABOO ,
                           HIS_COMPARE.UNTOWARD_REACTION ,
                           HIS_COMPARE.PRECAUTIONS ,
                           HIS_COMPARE.FEE_ITEMGRADE ,
                           HIS_COMPARE.DOSAGE ,
                           HIS_COMPARE.USAGE ,
                           HIS_COMPARE.DOSAGE_UNIT ,
                           HIS_COMPARE.ONCE_DOSAGE ,
                           HIS_COMPARE.FREQUENCY ,
                           HIS_COMPARE.DRUG_COMMON_LIMIT_FLAG ,
                           HIS_COMPARE.DRUG_SPECIAL_LIMIT_FLAG ,
                           HIS_COMPARE.MATERIAL_LIMITUSE_FLAG ,
                           HIS_COMPARE.ISNEED_SITECODE ,
                           HIS_COMPARE.COMPENSATE ,
                           HIS_COMPARE.NO_COMPENSATE ,
                           HIS_COMPARE.COMPENSATE_RATE,
                           (select distinct end_date
                              from si_syundrug m
                             where m.item_no = His_Compare.Center_Code
                               and m.charge_code = '{0}') END_DATE ";
            string sql2 = @" FROM HIS_COMPARE ";
            string sql3 = @" WHERE ( HIS_COMPARE.CHARGE_TYPE_CODE = '{0}' ) 
                           AND HIS_COMPARE.FEE_TYPE = '{1}' ";
            if (fee_type == "1" || fee_type == "2" || fee_type == "3")
            {
                sqlcoloum = @", drug_name_dict.input_code ";
                sqltablename = @", drug_name_dict ";
                sqlwhere = @" and ( HIS_COMPARE.His_Code = drug_name_dict.drug_code ) 
                             and ( drug_name_dict.std_indicator = 1 )";
            }
            else if (fee_type == "4" || fee_type == "5")
            {
                sqlcoloum = @", PRICE_ITEM_NAME_DICT.Input_Code";
                sqltablename = @", PRICE_ITEM_NAME_DICT ";
                sqlwhere = @" and ( HIS_COMPARE.His_Code = PRICE_ITEM_NAME_DICT.Item_Code ) 
                             and ( PRICE_ITEM_NAME_DICT.STD_INDICATOR = 1 )";
            }
            else
            {
                sqlcoloum = "";
                sqltablename = "";
                sqlwhere = "";
            }
            string sql = sql1 + sqlcoloum + sql2 + sqltablename + sql3 + sqlwhere;
            sql = string.Format(sql, charge_type_code, fee_type);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<HIS_COMPARE>(ds);
            return list;
        }

        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="menu_group"></param>
        /// <returns></returns>
        public DbDataReader GetHisCompare(BaseEntityer db, HIS_COMPARE his_compare)
        {
            string sql = @"SELECT * FROM HIS_COMPARE 
                            where HIS_COMPARE.CHARGE_TYPE_CODE = '{0}' and HIS_COMPARE.HIS_CODE = '{1}' 
                             and HIS_COMPARE.HIS_CLASS = '{2}' ";
            sql = string.Format(sql, his_compare.CHARGE_TYPE_CODE, his_compare.HIS_CODE, his_compare.HIS_CLASS);

            if (his_compare.CHARGE_TYPE_CODE == "2" || his_compare.CHARGE_TYPE_CODE == "3")
            {
                sql = @"SELECT * FROM HIS_COMPARE 
                            where HIS_COMPARE.CHARGE_TYPE_CODE = '{0}' and HIS_COMPARE.HIS_CODE = '{1}' 
                             and HIS_COMPARE.HIS_CLASS = '{2}' and  his_specs ='{3}'  ";
                sql = string.Format(sql, his_compare.CHARGE_TYPE_CODE, his_compare.HIS_CODE, his_compare.HIS_CLASS, his_compare.HIS_SPECS);
         
            }
            return db.ExecuteReader(sql);
        }

        /// <summary>
        /// 新增对照项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="his_compare"></param>
        /// <returns></returns>
        public int InsertHisCompare(BaseEntityer db, HIS_COMPARE his_compare)
        {
            //2013-6-13 by li 四平农合对照项目增加
            string sql = @"INSERT INTO HIS_COMPARE
                        (
                               HIS_COMPARE.His_Code ,
                               HIS_COMPARE.CENTER_CODE ,
                               HIS_COMPARE.Fee_Type ,
                               HIS_COMPARE.Center_Name ,
                               HIS_COMPARE.Center_Specs ,
                               HIS_COMPARE.CENTER_UNIT ,
                               HIS_COMPARE.Center_Price ,
                               HIS_COMPARE.Center_Type ,
                               HIS_COMPARE.Center_Rate ,
                               HIS_COMPARE.Center_Pack ,
                               HIS_COMPARE.Center_Place ,
                               HIS_COMPARE.His_Name ,
                               HIS_COMPARE.His_Spell ,
                               HIS_COMPARE.His_Specs ,
                               HIS_COMPARE.His_Unit ,
                               HIS_COMPARE.His_Price ,
                               HIS_COMPARE.HIS_TYPE ,
                               HIS_COMPARE.His_Pack ,
                               HIS_COMPARE.His_Place ,
                               HIS_COMPARE.Oper_Code ,
                               HIS_COMPARE.Oper_Date ,
                               HIS_COMPARE.Applyflag ,
                               HIS_COMPARE.Personrate ,
                               HIS_COMPARE.His_Wb_Code ,
                               HIS_COMPARE.His_User_Code ,
                               HIS_COMPARE.Trans ,
                               HIS_COMPARE.His_Class ,
                               HIS_COMPARE.Center_Class ,
                               HIS_COMPARE.CHARGE_TYPE_CODE ,
                               HIS_COMPARE.DRUG_TABOO ,
                               HIS_COMPARE.UNTOWARD_REACTION ,
                               HIS_COMPARE.PRECAUTIONS ,
                               HIS_COMPARE.FEE_ITEMGRADE ,
                               HIS_COMPARE.DOSAGE ,
                               HIS_COMPARE.USAGE ,
                               HIS_COMPARE.DOSAGE_UNIT ,
                               HIS_COMPARE.ONCE_DOSAGE ,
                               HIS_COMPARE.FREQUENCY ,
                               HIS_COMPARE.DRUG_COMMON_LIMIT_FLAG ,
                               HIS_COMPARE.DRUG_SPECIAL_LIMIT_FLAG ,
                               HIS_COMPARE.MATERIAL_LIMITUSE_FLAG ,
                               HIS_COMPARE.ISNEED_SITECODE ,
                               HIS_COMPARE.COMPENSATE ,
                               HIS_COMPARE.NO_COMPENSATE ,
                               HIS_COMPARE.COMPENSATE_RATE 
                        )
                        VALUES
                        (
                               '{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}',{8},{9},'{10}','{11}',
                                '{12}','{13}','{14}',{15},'{16}',{17},'{18}','{19}',
                                to_date('{20}', 'yyyy-mm-dd hh24:mi:ss'),'{21}',{22},
                                '{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}'
                                ,'{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}',{42},{43},{44}
                        )";
            object[] param = new object[] { his_compare.HIS_CODE, his_compare.CENTER_CODE, his_compare.FEE_TYPE, 
                his_compare.CENTER_NAME, his_compare.CENTER_SPECS, his_compare.CENTER_UNIT, his_compare.CENTER_PRICE, 
                his_compare.CENTER_TYPE, his_compare.CENTER_RATE, his_compare.CENTER_PACK, his_compare.CENTER_PLACE, 
                his_compare.HIS_NAME.Replace("'",""), his_compare.HIS_SPELL.Replace("'",""), his_compare.HIS_SPECS, his_compare.HIS_UNIT, 
                his_compare.HIS_PRICE, his_compare.HIS_TYPE, his_compare.HIS_PACK, his_compare.HIS_PLACE, 
                his_compare.OPER_CODE, his_compare.OPER_DATE, his_compare.APPLYFLAG, his_compare.PERSONRATE, 
                his_compare.HIS_WB_CODE, his_compare.HIS_USER_CODE, his_compare.TRANS, his_compare.HIS_CLASS, 
                his_compare.CENTER_CLASS, his_compare.CHARGE_TYPE_CODE, his_compare.DRUG_TABOO, 
                his_compare.UNTOWARD_REACTION, his_compare.PRECAUTIONS, his_compare.FEE_ITEMGRADE, 
                his_compare.DOSAGE, his_compare.USAGE, his_compare.DOSAGE_UNIT, his_compare.ONCE_DOSAGE, 
                his_compare.FREQUENCY, his_compare.DRUG_COMMON_LIMIT_FLAG, his_compare.DRUG_SPECIAL_LIMIT_FLAG, 
                his_compare.MATERIAL_LIMITUSE_FLAG, his_compare.ISNEED_SITECODE, his_compare.COMPENSATE, 
                his_compare.NO_COMPENSATE, his_compare.COMPENSATE_RATE };

            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除指定医保信息已对照项目数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="charge_type_code"></param>
        /// <param name="his_code"></param>
        /// <param name="his_class"></param>
        /// <returns></returns>
        public int DeleteHisCompare(BaseEntityer db, string charge_type_code, string his_code, string his_class)
        {
            string sql = @"delete from HIS_COMPARE where HIS_COMPARE.CHARGE_TYPE_CODE = '{0}' 
                            AND HIS_COMPARE.HIS_CODE = '{1}' AND HIS_COMPARE.HIS_CLASS = '{2}'";
            object[] param = new object[] { charge_type_code, his_code, his_class };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据诊疗项目代码查询对应部位码列表
        /// </summary>
        /// <param name="item_code"></param>
        /// <returns></returns>
        public IList<SI_SYBODY> GetSiSybody(string item_code)
        {
            string sql = @"SELECT SI_SYBODY.BODY_CODE,
                           SI_SYBODY.BODY_NAME,
                           SI_SYBODY.ITEM_CODE,
                           SI_SYBODY.ITEM_NAME,
                           SI_SYBODY.BEGIN_DATE,
                           SI_SYBODY.END_DATE,
                           SI_SYBODY.PUB_OWN_RATIO,
                           SI_SYBODY.OVER_OWN_RATIO,
                           SI_SYBODY.PAY_STANDARD,
                           SI_SYBODY.OPER_CODE,
                           SI_SYBODY.OPER_DATE 
                           FROM SI_SYBODY 
                           WHERE SI_SYBODY.ITEM_CODE = '{0}'";
            sql = string.Format(sql, item_code);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<SI_SYBODY>(ds);
            return list;
        }

        #endregion

        #region 取药科室维护
        /// <summary>
        /// 得到所有取药科室维护信息
        /// </summary>
        /// <returns></returns>
        public List<DRUG_COMPARE_DEPT> GetDrugGetDeptsByDeptCode(string deptCode)
        {
            string sql = @"select * from DRUG_COMPARE_DEPT t where t.dept_code='{0}'";
            sql = sql.SqlFormate(deptCode);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<DRUG_COMPARE_DEPT>(ds).ToList();
        }
        /// <summary>
        /// 得到所有取药科室维护信息
        /// </summary>
        /// <returns></returns>
        public List<DRUG_COMPARE_DEPT> GetDrugCompareDepts()
        {
            string sql = @"select * from DRUG_COMPARE_DEPT t";
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<DRUG_COMPARE_DEPT>(ds).ToList();
        }
        /// <summary>
        /// 插入一条取药科室维护信息
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InertDrugCompareDept(DRUG_COMPARE_DEPT o, BaseEntityer db)
        {
            string sql = @"insert into DRUG_COMPARE_DEPT values('{0}','{1}') ";
            sql = sql.SqlFormate(o.DRUG_DEPT_CODE, o.DEPT_CODE);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除一条取药科室维护信息
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteDrugCompareDept(DRUG_COMPARE_DEPT o, BaseEntityer db)
        {
            string sql = @"delete from DRUG_COMPARE_DEPT t where t.drug_dept_code='{0}' and t.dept_code='{1}'";
            sql = sql.SqlFormate(o.DRUG_DEPT_CODE, o.DEPT_CODE);
            return db.ExecuteNonQuery(sql);
        }
        #endregion

        #region 系统参数表维护
        /// <summary>
        /// 插入一条参数记录
        /// </summary>
        /// <param name="p"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertSysParam(HisCommon.DataEntity.SYS_PARAM p, BaseEntityer db)
        {
            string sql = @"insert into sys_param
  (PARAM_NAME, PARAM_VALUE, PARAM_DESC, MODEL, CONTROL_TYPE, COMBOX_ITEMS)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, '{5}')";
            sql = sql.SqlFormate(p.PARAM_NAME, p.PARAM_VALUE, p.PARAM_DESC, p.MODEl, p.CONTROL_TYPE, p.COMBOX_ITEMS);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除一条参数记录
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteSysparam(HisCommon.DataEntity.SYS_PARAM p, BaseEntityer db)
        {
            string sql = @"delete from sys_param t where t.PARAM_NAME='{0}'";
            sql = sql.SqlFormate(p.PARAM_NAME);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 检查是否已经存在此数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="PACT_CODE">费用类别编码</param>
        /// <param name="CONFIG_TYPE">配置类别</param>
        /// <returns></returns>
        public DbDataReader GetSYS_CONFIGData(BaseEntityer db, string PACT_CODE, string CONFIG_TYPE)
        {
            string sql = @"SELECT * FROM sys_config s
                            where s.PACT_CODE='{0}' and s.CONFIG_TYPE='{1}'";
            sql = string.Format(sql, PACT_CODE, CONFIG_TYPE);
            return db.ExecuteReader(sql);
        }
        /// <summary>
        /// 插入一条参数记录
        /// </summary>
        /// <param name="p"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int InsertSYS_CONFIG(HisCommon.DataEntity.SYS_CONFIG p, BaseEntityer db)
        {
            string sql = @"insert into sys_config
  (PACT_CODE,PACT_NAME,CONFIG_NAME,CONFIG_VALUE,DESCRIPTION,CONFIG_TYPE,PAGE_WIDTH,PAGE_HEIGHT,PBL)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')";
            sql = sql.SqlFormate(p.PACT_CODE, p.PACT_NAME, p.CONFIG_NAME, p.CONFIG_VALUE, p.DESCRIPTION, p.CONFIG_TYPE, p.PAGE_WIDTH, p.PAGE_HEIGHT, p.PBL);
            return db.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除一条参数记录
        /// </summary>
        /// <param name="o"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int DeleteSYS_CONFIG(string PACT_CODE, string CONFIG_TYPE, BaseEntityer db)
        {
            string sql = @"delete from sys_config s where s.PACT_CODE='{0}' and s.CONFIG_TYPE='{1}'";
            sql = sql.SqlFormate(PACT_CODE, CONFIG_TYPE);
            return db.ExecuteNonQuery(sql);
        }
        #endregion

        #region 自动更新
        /// <summary>
        /// 从数据库获得更新文件的信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetUpdateContent()
        {
            string sql = @"select 
ID,
BBH,
FILENAME,
UPDATE_TIME,
PATH,
BT,
ZT,
MODULE
 from zdupdatenew where module='rkhis'";
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            return dt;
        }

        /// <summary>
        /// 从数据库获得更新文件的信息,包括文件内容
        /// </summary>
        /// <returns></returns>
        public DataTable GetUpdateContainContent()
        {
            string sql = @"select *
 from zdupdateNEW where module='rkhis'";
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            return dt;
        }
        #endregion

        #region 优惠维护 DLQ 131205

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <param name="templet_id"></param>
        /// <param name="charge_type_code"></param>
        /// <param name="sale"></param>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int Add_Charge_templet(string id, string templet_id, string charge_type_code, string sale, DateTime start_date, DateTime end_date, string state)
        {
            int i = 0;
            string sql = @" insert into charge_templet (ID, TEMPLET_ID, CHARGE_TYPE_CODE, SALE, START_DATE, END_DATE, STATE)
            values ('" + id + "', '" + templet_id + "', '" + charge_type_code + "', " + sale
                      + ", to_date('" + start_date + "', 'yyyy-mm-dd hh24:mi:ss'), to_date('" + end_date + "', 'yyyy-mm-dd hh24:mi:ss'), '" + state + "')";
            i = BaseEntityer.Db.ExecuteNonQuery(sql);

            return i;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="templet_id"></param>
        /// <param name="charge_type_code"></param>
        /// <param name="sale"></param>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int Update_Charge_templet(string id, string templet_id, string charge_type_code, string sale, DateTime start_date, DateTime end_date, string state)
        {
            int i = 0;
            string sql = @"update Charge_templet set templet_id='" + templet_id + "',charge_type_code='" + charge_type_code + "', sale=" + sale + ",start_date= to_date('" + start_date
                 + "', 'yyyy-mm-dd hh24:mi:ss'),end_date=to_date('" + end_date + "', 'yyyy-mm-dd hh24:mi:ss'), state='" + state + "' where id='" + id + "'";
            //            string sql = @"update Charge_templet set templet_id='{2}',charge_type_code='{3}', sale={4},statr_date= '{5}',
            //                           end_date='{6}', state='{7}' where id='{1}'";
            //            sql = sql.SqlFormate(id, templet_id,charge_type_code, sale, start_date, end_date,state);
            i = BaseEntityer.Db.ExecuteNonQuery(sql);

            return i;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="templet_id"></param>
        /// <param name="charge_type_code"></param>
        /// <param name="sale"></param>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="state"></param>
        /// <returns>0失败，1插入2修改</returns>
        public int Save_Charge_templet(string id, string templet_id, string charge_type_code, string sale, DateTime start_date, DateTime end_date, string state)
        {
            int i = 0;
            int x = 0;
            string sql = "select * from charge_templet where id='" + id + "'";
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                x = Update_Charge_templet(id, templet_id, charge_type_code, sale, start_date, end_date, state);
                i = 2;
            }
            else
            {
                x = Add_Charge_templet(id, templet_id, charge_type_code, sale, start_date, end_date, state);
                i = 1;
            }
            if (x <= 0)
            {
                i = 0;
            }
            return i;

        }

        public int Del_Charge_templet(string id)
        {
            int i = 0;
            string sql = @"delete from Charge_templet where id='" + id + "'";
            i = BaseEntityer.Db.ExecuteNonQuery(sql);
            return i;

        }





        public int Add_Charge_templet_detail(string templet_id, string fee_type, string fee_type_name)
        {
            int i = 0;
            string sql = @" INSERT INTO CHARGE_TEMPLET_DETAIL ( 
                            TEMPLET_ID ,
                            FEE_TYPE ,
                            FEE_TYPE_NAME ) VALUES ('" + templet_id + "','" + fee_type + "','" + fee_type_name + "')";
            i = BaseEntityer.Db.ExecuteNonQuery(sql);

            return i;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templet_id"></param>
        /// <param name="fee_type"></param>
        /// <param name="fee_type_name"></param>
        /// <returns></returns>
        public int Update_Charge_templet_detail(string templet_id, string fee_type, string fee_type_name)
        {
            int i = 0;
            string sql = @"update CHARGE_TEMPLET_DETAIL set 
     
                        FEE_TYPE_NAME = '" + fee_type_name + "' where templet_id='" + templet_id + "' and  fee_type='" + fee_type + "'";

            i = BaseEntityer.Db.ExecuteNonQuery(sql);

            return i;
        }



        public int Save_Charge_templet_detail(string templet_id, string fee_type, string fee_type_name)
        {
            int i = 0;
            int x = 0;
            string sql = "select * from charge_templet_detail where templet_id='" + templet_id + "' and  fee_type='" + fee_type + "'";
            DataTable dt = BaseEntityer.Db.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                x = Update_Charge_templet_detail(templet_id, fee_type, fee_type_name);
                i = 2;
            }
            else
            {
                x = Add_Charge_templet_detail(templet_id, fee_type, fee_type_name);
                i = 1;
            }
            if (x <= 0)
            {
                i = 0;
            }
            return i;

        }

        public int Del_Charge_templet_detail(string templet_id, string fee_type)
        {
            int i = 0;
            string sql = @"delete from Charge_templet_detail where templet_id='" + templet_id + "' and  fee_type='" + fee_type + "'";
            i = BaseEntityer.Db.ExecuteNonQuery(sql);
            return i;

        }

        public DataTable GetDataTable(string sql)
        {
            return BaseEntityer.Db.GetDataTable(sql);
        }

        #endregion

        #region 四平维护床位关于累计表
        /// <summary>
        /// 查询全院沉淀表
        /// </summary>
        /// <returns></returns>
        public DataTable Getinhosdayreport()
        {
            string sql = @"select v.dept_code,v.nurse_station_code ,v.stat_date, --显示不可修改,
v.bed_standard,v.bed_add,v.bed_free  --可以修改，输入数量
,v.oper_code,v.oper_date  --显示不可编辑，如果记录修改，将此信息保存为当前用户及当前日期
 from rk_mrs_inhosdayreport v
 where v.stat_date=(select max(stat_date)-1 from rk_mrs_inhosdayreport where rownum=1 )";
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 修改日结沉淀表的床数--四平
        /// </summary>
        /// <param name="stat_date"></param>
        /// <param name="dept_code"></param>
        /// <param name="bed_standard"></param>
        /// <param name="bed_add"></param>
        /// <param name="bed_free"></param>
        /// <param name="oper_code"></param>
        public void Updateinhosdayreport(string stat_date, string dept_code,
            int bed_standard, int bed_add, int bed_free, string oper_code)
        {
            string sql = @"update rk_mrs_inhosdayreport v set
v.bed_standard='{2}' ,
bed_add ='{3}' ,
v.bed_free='{4}',
v.oper_code='{5}',
v.oper_date=sysdate
where stat_date=to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and dept_code='{1}'";
            sql = sql.SqlFormate(
                stat_date,
                dept_code,
                bed_standard,
                bed_add,
                bed_free,
                oper_code

                );
            BaseEntityer.Db.ExecuteNonQuery(sql);
        }
        #endregion

        #region 触摸屏 科室 医生信息维护
        /// <summary>
        /// 获取科室医生信息
        /// </summary>
        /// <param name="type">科室  医生</param>
        /// <returns></returns>
        public DataSet GetDeptDocList(string deptCode)
        {
            if (string.IsNullOrEmpty(deptCode))
            {
                string sql = @"select distinct dept_code code, dept_name name,'' as sex  from dept_dict where clinic_attr = '0' and outp_or_inp = '1' order by name";
                return BaseEntityer.Db.GetDataSet(sql);
            }
            else
            {
                string sql = @"select distinct a.user_id code,a.user_name name,sex from users_staff_dict a where a.user_dept = '" + deptCode + "'";
                return BaseEntityer.Db.GetDataSet(sql);
            }
        }

        /// <summary>
        /// 获取具体 医院、科室、医生  信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetDeptDocInfo(string code)
        {
            string sql = @"select a.code,a.main_info,a.detail_info1,a.detail_info2,a.detail_info3,a.message,a.photo from cmp_info_edit a where a.code = '" + code + "'";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取医院消息列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetHospitalInfo()
        {
            string sql = @"select a.no,a.title,a.content,a.tzdate from cmp_hospital_info a order by no desc  ";
            return BaseEntityer.Db.GetDataSet(sql);
        }

        /// <summary>
        /// 保存医院、科室、医生信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Boolean SaveDeptDoctorInfo(BaseEntityer db, DataTable dt)
        {
            string _code, _maininfo, _detailinfo1, _detailinfo2, _detailinfo3, _messae;
            if (dt.Rows.Count == 0)
                return false;
            _code = dt.Rows[0]["code"].ToString();
            _maininfo = dt.Rows[0]["main_info"].ToString();
            _detailinfo1 = dt.Rows[0]["detail_info1"].ToString();
            _detailinfo2 = dt.Rows[0]["detail_info2"].ToString();
            _detailinfo3 = dt.Rows[0]["detail_info3"].ToString();
            _messae = dt.Rows[0]["message"].ToString();

            //先删除
            string sql = @"delete from CMP_INFO_EDIT where CODE = '{0}'";
            sql = string.Format(sql, _code);
            if (db.ExecuteNonQuery(sql) < 0)
                return false;
            //再插入
            sql = @"INSERT INTO CMP_INFO_EDIT
                        (code,main_info,detail_info1,detail_info2,detail_info3,message )
                        VALUES ('{0}','{1}','{2}', '{3}','{4}','{5}')";
            object[] param = new object[] { _code, _maininfo, _detailinfo1, _detailinfo2, _detailinfo3, _messae };
            sql = Utility.SqlFormate(sql, param);
            if (db.ExecuteNonQuery(sql) <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// 保存医生头像
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Boolean SaveDoctorImage(BaseEntityer db, byte[] image, string doctorCode)
        {
            //更新照片   

            string sql = " update CMP_INFO_EDIT SET CMP_INFO_EDIT.PHOTO = :PHOTO where CMP_INFO_EDIT.CODE = :CODE";
            OracleParameter[] param = new OracleParameter[2];
            param[0] = new OracleParameter("PHOTO", OracleType.LongRaw);
            param[0].Value = image;
            param[1] = new OracleParameter("CODE", OracleType.VarChar, 8);
            param[1].Value = doctorCode;

            if (db.ExecuteSql(sql, param) <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// 保存医院通知信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="no">序号</param>
        /// <param name="title">标题</param>
        /// <param name="tzdate">通知日期</param>
        /// <param name="conten">内容</param>
        /// <returns></returns>
        public Boolean SaveHospitalInfo(BaseEntityer db, string no, string title, DateTime tzdate, string content)
        {

            //先删除
            string sql = @"delete from cmp_hospital_info where no = '" + no + "'";
            if (db.ExecuteNonQuery(sql) < 0)
                return false;

            sql = @"INSERT INTO cmp_hospital_info
                        (no,title,content,tzdate )
                        VALUES (" + no + ",'" + title + "','" + content + "',to_date('" + tzdate.ToShortDateString() + "','yyyy-mm-dd'))";
            if (db.ExecuteNonQuery(sql) < 0)
                return false;
            return true;
        }

        /// <summary>
        /// 删除医院通知信息
        /// </summary>
        /// <param name="no">序号</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Boolean DelHospitalInfo(BaseEntityer db, string no)
        {
            //先删除
            string sql = @"delete from cmp_hospital_info where no = '" + no + "'";
            if (db.ExecuteNonQuery(sql) < 0)
                return false;

            return true;
        }

        /// <summary>
        /// 获取医生列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDoctorInfo(string doctorCode)
        {
            string sql = @"select a.user_id,a.user_name,b.dept_name,c.sex_name,d.title_name  " +
                " from users_staff_dict a,dept_dict b,sex_dict c,doctor_title_dict d " +
                "where a.user_dept = b.dept_code and a.sex = c.sex_code(+) and a.doctor_type = d.title_code(+) and a.user_id = '" + doctorCode + "'";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取诊疗项目价格列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetClinicItemPrictList(string inputCode)
        {
            string sql = "select b.item_name,F_TRANS_PINYIN_CAPITAL(b.item_name) as input_code,b.item_spec,b.units,b.price,c.class_name " +
                         " from CLINIC_ITEM_DICT s,CLINIC_VS_CHARGE a,price_list b,CLINIC_ITEM_CLASS_DICT c " +
                         " where s.item_code = a.clinic_item_code " +
                          "  and a.charge_item_code = b.item_code(+) " +
                          "  and a.charge_item_class = c.class_code(+) " +
                          "  and s.item_class not in ('A', 'B') " +
                          "  and (b.stop_date is null or b.stop_date > sysdate) " +
                          "  and F_TRANS_PINYIN_CAPITAL(b.item_name) like '" + inputCode + "%'" +
                          "  and b.TOUCHSCREEN ='0'" +
                         " order by c.class_name,s.item_name";
            return BaseEntityer.Db.GetDataTable(sql);
        }



        /// <summary>
        /// 触摸屏（获取药品价列表）
        /// </summary>
        /// <returns></returns>
        public DataTable GetClinicDrugPrictList(string inputCode)
        {
            string sql = "select b.item_name,F_TRANS_PINYIN_CAPITAL(b.item_name) as input_code,b.item_spec,b.units,b.price,c.class_name " +
                         " from CLINIC_ITEM_DICT s,CLINIC_VS_CHARGE a,price_list b,CLINIC_ITEM_CLASS_DICT c " +
                         " where s.item_code = a.clinic_item_code " +
                          "  and a.charge_item_code = b.item_code(+) " +
                          "  and a.charge_item_class = c.class_code(+) " +
                          "  and s.item_class in ('A', 'B') " +
                          "  and (b.stop_date is null or b.stop_date > sysdate) " +
                          "  and  F_TRANS_PINYIN_CAPITAL(b.item_name) like '" + inputCode + "%'" +
                          "  and b.TOUCHSCREEN ='0'" +
                         " order by c.class_name,s.item_name";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 查询患者一日清单信息
        /// </summary>
        /// <param name="wardCode">护理单元号</param>
        /// <param name="beginDateTime">收费开始时间</param>
        /// <param name="endDateTime">收费截止时间</param>
        /// <returns></returns>
        public List<InPatientOneDayItems> QueryInPatientOneDayDetail(string wardCode, string beginDateTime, string endDateTime, string patientID)
        {
            string sql = @"select t.patient_id,
                   t.visit_id,
                   t.name,
                   t.bed_no,
                   t.item_name,
                   t.item_class,
                   t.item_code,
                   t.price,
                   t.amount,
                   t.units,
                   t.charges,
                   t.costs,
                   t.billing_date_time,
                   t.now_money,
                   t.prepayments,
                   t.DEPT_NAME,
                   t.GRIDE,t.item_spec
              from (select t.billing_date_time,t.patient_id,
                           t.visit_id,
                           p.name,
                           (select b.bed_label from bed_rec b where  h.bed_no=b.bed_no and h.ward_code=b.ward_code) as bed_no,
                           t.item_name,
                           t.item_class,
                           t.item_code,
                           round(t.costs / t.amount,2) as price,
                           t.amount as amount,
                           t.units,
                           t.charges as charges,
                           t.costs as costs,
                           nvl(h.prepayments, 0) - nvl(h.total_charges, 0) as now_money,
                           h.prepayments as prepayments,
                           (select d.dept_name from dept_dict d where d.dept_code=h.DEPT_CODE) as DEPT_NAME,
                           (select decode(c.FEE_ITEMGRADE,'1','甲','2','乙','3','丙','')  from his_compare c where c.charge_type_code='2' and c.his_code=t.ITEM_CODE and rownum=1) as GRIDE
                          ,t.item_spec
            from inp_bill_detail t
                     right join pats_in_hospital h
                        on t.patient_id = h.patient_id
                       and t.visit_id = h.visit_id
                      left join pat_master_index p
                        on t.patient_id = p.patient_id
                     where ( '{1}' is null or t.billing_date_time >=
                           to_date('{1}', 'yyyy-MM-dd hh24:mi:ss'))
                       and ('{2}' is null or t.billing_date_time <=
                           to_date('{2}', 'yyyy-MM-dd hh24:mi:ss'))
                       and ('{0}' is null or h.ward_code = '{0}')
                       and (t.patient_id='{3}' or '{3}' is null)) t ";
            sql = string.Format(sql, wardCode, beginDateTime, endDateTime, patientID);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.Report.InPatientOneDayItems>(BaseEntityer.Db.GetDataSet(sql)).ToList();

        }


        #endregion

        #region 读取项目子类字典
        /// <summary>
        /// 读取项目子类字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.DataEntity.CLINIC_SUBCLASS_DICT> GetItemSubClass()
        {
            string sql = @"select * from  CLINIC_SUBCLASS_DICT";
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.CLINIC_SUBCLASS_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 读取项目子类字典名称
        /// </summary>
        /// <returns></returns>
        public string GetItemSubClassName(string CODE)
        {
            string sql = @"select NAME from  CLINIC_SUBCLASS_DICT WHERE CODE = '" + CODE + "'";
            object obj = BaseEntityer.Db.ExecuteScalar(sql);
            if (obj != null)
                return obj.ToString();
            else
                return string.Empty;
        }
        #endregion

        #region 贵阳电网医保目录上传
        /// <summary>
        /// 获取已对照未上传药品数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetHisCompareUploadDrug(string datetime, string charge_type_code, bool b)
        {
            string sqlcoloum = string.Empty;
            string sqltablename = string.Empty;
            string sqlwhere = string.Empty;
            string sql1 = @"SELECT HIS_COMPARE.His_Code ,
                           HIS_COMPARE.CENTER_CODE ,
                           HIS_COMPARE.Fee_Type ,
                           HIS_COMPARE.Center_Name ,
                           HIS_COMPARE.Center_Specs ,
                           HIS_COMPARE.CENTER_UNIT ,
                           HIS_COMPARE.Center_Price ,
                           HIS_COMPARE.Center_Type ,
                           HIS_COMPARE.Center_Rate ,
                           HIS_COMPARE.Center_Pack ,
                           HIS_COMPARE.Center_Place ,
                           HIS_COMPARE.His_Name ,
                           HIS_COMPARE.His_Spell ,
                           HIS_COMPARE.His_Specs ,
                           HIS_COMPARE.His_Unit ,
                           HIS_COMPARE.His_Price ,
                           HIS_COMPARE.HIS_TYPE ,
                           HIS_COMPARE.His_Pack ,
                           HIS_COMPARE.His_Place ,
                           HIS_COMPARE.Oper_Code ,
                           HIS_COMPARE.Oper_Date ,
                           HIS_COMPARE.Applyflag ,
                           HIS_COMPARE.Personrate ,
                           HIS_COMPARE.His_Wb_Code ,
                           HIS_COMPARE.His_User_Code ,
                           HIS_COMPARE.Trans ,
                           HIS_COMPARE.His_Class ,
                           HIS_COMPARE.Center_Class ,
                           HIS_COMPARE.CHARGE_TYPE_CODE ,
                           HIS_COMPARE.DRUG_TABOO ,
                           HIS_COMPARE.UNTOWARD_REACTION ,
                           HIS_COMPARE.PRECAUTIONS ,
                           HIS_COMPARE.FEE_ITEMGRADE ,
                           HIS_COMPARE.DOSAGE ,
                           HIS_COMPARE.USAGE ,
                           HIS_COMPARE.DOSAGE_UNIT ,
                           HIS_COMPARE.ONCE_DOSAGE ,
                           HIS_COMPARE.FREQUENCY ,
                           HIS_COMPARE.DRUG_COMMON_LIMIT_FLAG ,
                           HIS_COMPARE.DRUG_SPECIAL_LIMIT_FLAG ,
                           HIS_COMPARE.MATERIAL_LIMITUSE_FLAG ,
                           HIS_COMPARE.ISNEED_SITECODE ,
                           HIS_COMPARE.COMPENSATE ,
                           HIS_COMPARE.NO_COMPENSATE ,
                           HIS_COMPARE.COMPENSATE_RATE ";
            string sql2 = @" FROM HIS_COMPARE ";
            string sql3 = @" WHERE ( HIS_COMPARE.CHARGE_TYPE_CODE = '{0}' ) 
                           AND HIS_COMPARE.FEE_TYPE = '1' ";

            sqlcoloum = @", drug_name_dict.input_code ";
            sqltablename = @", drug_name_dict ";
            sqlwhere = @" and ( HIS_COMPARE.His_Code = drug_name_dict.drug_code ) 
                             and ( drug_name_dict.std_indicator = 1 )";

            string sql = sql1 + sqlcoloum + sql2 + sqltablename + sql3 + sqlwhere;

            if (!string.IsNullOrEmpty(datetime))
            {
                sql += " AND HIS_COMPARE.OPER_DATE >= to_date('{1} 00:00:00', 'yyyy-mm-dd hh24:mi:ss')";
            }
            if (b)
            {
                sql += " AND HIS_COMPARE.APPLYFLAG='0'";
            }
            else
            {
                sql += " AND HIS_COMPARE.APPLYFLAG='1'";
            }

            sql = string.Format(sql, charge_type_code, datetime);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取已对照未上传诊疗项目数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetHisCompareUploadItem(string datetime, string charge_type_code, bool b)
        {
            string sqlcoloum = string.Empty;
            string sqltablename = string.Empty;
            string sqlwhere = string.Empty;
            string sql1 = @"SELECT HIS_COMPARE.His_Code ,
                           HIS_COMPARE.CENTER_CODE ,
                           HIS_COMPARE.Fee_Type ,
                           HIS_COMPARE.Center_Name ,
                           HIS_COMPARE.Center_Specs ,
                           HIS_COMPARE.CENTER_UNIT ,
                           HIS_COMPARE.Center_Price ,
                           HIS_COMPARE.Center_Type ,
                           HIS_COMPARE.Center_Rate ,
                           HIS_COMPARE.Center_Pack ,
                           HIS_COMPARE.Center_Place ,
                           HIS_COMPARE.His_Name ,
                           HIS_COMPARE.His_Spell ,
                           HIS_COMPARE.His_Specs ,
                           HIS_COMPARE.His_Unit ,
                           HIS_COMPARE.His_Price ,
                           HIS_COMPARE.HIS_TYPE ,
                           HIS_COMPARE.His_Pack ,
                           HIS_COMPARE.His_Place ,
                           HIS_COMPARE.Oper_Code ,
                           HIS_COMPARE.Oper_Date ,
                           HIS_COMPARE.Applyflag ,
                           HIS_COMPARE.Personrate ,
                           HIS_COMPARE.His_Wb_Code ,
                           HIS_COMPARE.His_User_Code ,
                           HIS_COMPARE.Trans ,
                           HIS_COMPARE.His_Class ,
                           HIS_COMPARE.Center_Class ,
                           HIS_COMPARE.CHARGE_TYPE_CODE ,
                           HIS_COMPARE.DRUG_TABOO ,
                           HIS_COMPARE.UNTOWARD_REACTION ,
                           HIS_COMPARE.PRECAUTIONS ,
                           HIS_COMPARE.FEE_ITEMGRADE ,
                           HIS_COMPARE.DOSAGE ,
                           HIS_COMPARE.USAGE ,
                           HIS_COMPARE.DOSAGE_UNIT ,
                           HIS_COMPARE.ONCE_DOSAGE ,
                           HIS_COMPARE.FREQUENCY ,
                           HIS_COMPARE.DRUG_COMMON_LIMIT_FLAG ,
                           HIS_COMPARE.DRUG_SPECIAL_LIMIT_FLAG ,
                           HIS_COMPARE.MATERIAL_LIMITUSE_FLAG ,
                           HIS_COMPARE.ISNEED_SITECODE ,
                           HIS_COMPARE.COMPENSATE ,
                           HIS_COMPARE.NO_COMPENSATE ,
                           HIS_COMPARE.COMPENSATE_RATE ";
            string sql2 = @" FROM HIS_COMPARE ";
            string sql3 = @" WHERE ( HIS_COMPARE.CHARGE_TYPE_CODE = '{0}' ) 
                           AND HIS_COMPARE.FEE_TYPE = '4' ";

            sqlcoloum = @", PRICE_ITEM_NAME_DICT.Input_Code";
            sqltablename = @", PRICE_ITEM_NAME_DICT ";
            sqlwhere = @" and ( HIS_COMPARE.His_Code = PRICE_ITEM_NAME_DICT.Item_Code ) 
                             and ( PRICE_ITEM_NAME_DICT.STD_INDICATOR = 1 )";

            string sql = sql1 + sqlcoloum + sql2 + sqltablename + sql3 + sqlwhere;

            if (!string.IsNullOrEmpty(datetime))
            {
                sql += " AND HIS_COMPARE.OPER_DATE >= to_date('{1} 00:00:00', 'yyyy-mm-dd hh24:mi:ss')";
            }
            if (b)
            {
                sql += " AND APPLYFLAG='0' ";
            }
            else
            {
                sql += " AND APPLYFLAG='1' ";
            }
            sql = string.Format(sql, charge_type_code, datetime);
            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 获取病种未对照数据列表
        /// </summary>
        /// <param name="fee_type">对照类别编码</param>
        /// <returns></returns>
        public DataTable GetUploadDiagnosis(string datetime, string charge_type_code, bool b)
        {
            string sql = @"select 
  t.diagnose_code,
  t.diagnose_name,
  t.diagnose_identification,
  t.begin_date,
  t.end_date,
  t.diagnose_spell,
  t.diagnose_wb,
  t.valid_flag,
  t.injure_flag,
  t.birth_flag,
  t.diagnose_kind,
  t.diagnose_type,
  t.special_type,
  t.isuse_treatment_plan,
  t.resident_special_range,
  t.range,
  t.depart_type,
  t.spouse_quota,
  t.oper_code,
  t.oper_date,
  t.remark,
  t.charge_type_code

from SI_SYDIAGNOSE t WHERE t.CHARGE_TYPE_CODE='{0}' ";
            if (b)
            {
                sql += " AND t.VALID_FLAG='0' ";
            }
            else
            {
                sql += " AND t.VALID_FLAG='1' ";
            }
            //if (!string.IsNullOrEmpty(datetime))
            //{
            //    sql += " AND t.OPER_DATE >= to_date('{1} 00:00:00', 'yyyy-mm-dd hh24:mi:ss')";
            //}
            sql = string.Format(sql, charge_type_code, datetime);
            return BaseEntityer.Db.GetDataTable(sql);
        }
        /// <summary>
        /// 医保已对照信息查询
        /// </summary>
        /// <param name="charge_type_code">费别</param>
        /// <param name="fee_type">对照类别</param>
        /// <returns></returns>
        public IList<HIS_COMPARE> GetHisCompareGuiYang(string charge_type_code, string fee_type)
        {
            string sqlcoloum = string.Empty;
            string sqltablename = string.Empty;
            string sqlwhere = string.Empty;
            string sql1 = @"SELECT HIS_COMPARE.His_Code ,
                           HIS_COMPARE.CENTER_CODE ,
                           HIS_COMPARE.Fee_Type ,
                           HIS_COMPARE.Center_Name ,
                           HIS_COMPARE.Center_Specs ,
                           HIS_COMPARE.CENTER_UNIT ,
                           HIS_COMPARE.Center_Price ,
                           HIS_COMPARE.Center_Type ,
                           HIS_COMPARE.Center_Rate ,
                           HIS_COMPARE.Center_Pack ,
                           HIS_COMPARE.Center_Place ,
                           HIS_COMPARE.His_Name ,
                           HIS_COMPARE.His_Spell ,
                           HIS_COMPARE.His_Specs ,
                           HIS_COMPARE.His_Unit ,
                           HIS_COMPARE.His_Price ,
                           HIS_COMPARE.HIS_TYPE ,
                           HIS_COMPARE.His_Pack ,
                           HIS_COMPARE.His_Place ,
                           HIS_COMPARE.Oper_Code ,
                           HIS_COMPARE.Oper_Date ,
                           HIS_COMPARE.Applyflag ,
                           HIS_COMPARE.Personrate ,
                           HIS_COMPARE.His_Wb_Code ,
                           HIS_COMPARE.His_User_Code ,
                           HIS_COMPARE.Trans ,
                           HIS_COMPARE.His_Class ,
                           HIS_COMPARE.Center_Class ,
                           HIS_COMPARE.CHARGE_TYPE_CODE ,
                           HIS_COMPARE.DRUG_TABOO ,
                           HIS_COMPARE.UNTOWARD_REACTION ,
                           HIS_COMPARE.PRECAUTIONS ,
                           HIS_COMPARE.FEE_ITEMGRADE ,
                           HIS_COMPARE.DOSAGE ,
                           HIS_COMPARE.USAGE ,
                           HIS_COMPARE.DOSAGE_UNIT ,
                           HIS_COMPARE.ONCE_DOSAGE ,
                           HIS_COMPARE.FREQUENCY ,
                           HIS_COMPARE.DRUG_COMMON_LIMIT_FLAG ,
                           HIS_COMPARE.DRUG_SPECIAL_LIMIT_FLAG ,
                           HIS_COMPARE.MATERIAL_LIMITUSE_FLAG ,
                           HIS_COMPARE.ISNEED_SITECODE ,
                           HIS_COMPARE.COMPENSATE ,
                           HIS_COMPARE.NO_COMPENSATE ,
                           HIS_COMPARE.COMPENSATE_RATE ";
            string sql2 = @" FROM HIS_COMPARE ";
            string sql3 = @" WHERE ( HIS_COMPARE.CHARGE_TYPE_CODE = '{0}' ) 
                           AND HIS_COMPARE.FEE_TYPE = '{1}' ";
            if (fee_type == "1")
            {
                sqlcoloum = @", drug_name_dict.input_code ";
                sqltablename = @", drug_name_dict ";
                sqlwhere = @" and ( HIS_COMPARE.His_Code = drug_name_dict.drug_code ) 
                             and ( drug_name_dict.std_indicator = 1 )";
            }
            else if (fee_type == "2")
            {
                sqlcoloum = @", PRICE_ITEM_NAME_DICT.Input_Code";
                sqltablename = @", PRICE_ITEM_NAME_DICT ";
                sqlwhere = @" and ( HIS_COMPARE.His_Code = PRICE_ITEM_NAME_DICT.Item_Code ) 
                             and ( PRICE_ITEM_NAME_DICT.STD_INDICATOR = 1 )";
            }
            else
            {
                sqlcoloum = "";
                sqltablename = "";
                sqlwhere = "";
            }

            string sql = sql1 + sqlcoloum + sql2 + sqltablename + sql3 + sqlwhere;

            sql += " AND APPLYFLAG='0' ";

            sql = string.Format(sql, charge_type_code, fee_type);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            var list = DataSetToEntity.DataSetToT<HIS_COMPARE>(ds);
            return list;
        }

        /// <summary>
        /// 修改已上传项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="his_compare"></param>
        /// <returns></returns>
        public int UpdateHisCompare(BaseEntityer db, HIS_COMPARE his_compare)
        {
            string sql = @"UPDATE HIS_COMPARE set
                               HIS_COMPARE.His_Code='{0}',
                               HIS_COMPARE.CENTER_CODE='{1}' ,
                               HIS_COMPARE.Fee_Type='{2}' ,
                               HIS_COMPARE.Center_Name='{3}' ,
                               HIS_COMPARE.Center_Specs='{4}' ,
                               HIS_COMPARE.CENTER_UNIT='{5}' ,
                               HIS_COMPARE.Center_Price={6} ,
                               HIS_COMPARE.Center_Type='{7}' ,
                               HIS_COMPARE.Center_Rate={8} ,
                               HIS_COMPARE.Center_Pack={9} ,
                               HIS_COMPARE.Center_Place='{10}' ,
                               HIS_COMPARE.His_Name='{11}' ,
                               HIS_COMPARE.His_Spell='{12}' ,
                               HIS_COMPARE.His_Specs='{13}' ,
                               HIS_COMPARE.His_Unit='{14}' ,
                               HIS_COMPARE.His_Price='{15}' ,
                               HIS_COMPARE.HIS_TYPE='{16}' ,
                               HIS_COMPARE.His_Pack='{17}' ,
                               HIS_COMPARE.His_Place='{18}' ,
                               HIS_COMPARE.Oper_Code='{19}' ,
                               HIS_COMPARE.Oper_Date=to_date('{20}', 'yyyy-mm-dd hh24:mi:ss'),
                               HIS_COMPARE.Applyflag='{21}' ,
                               HIS_COMPARE.Personrate={22} ,
                               HIS_COMPARE.His_Wb_Code='{23}' ,
                               HIS_COMPARE.His_User_Code='{24}' ,
                               HIS_COMPARE.Trans='{25}' ,
                               HIS_COMPARE.His_Class='{26}' ,
                               HIS_COMPARE.Center_Class='{27}' ,
                               HIS_COMPARE.CHARGE_TYPE_CODE='{28}' ,
                               HIS_COMPARE.DRUG_TABOO='{29}' ,
                               HIS_COMPARE.UNTOWARD_REACTION='{30}' ,
                               HIS_COMPARE.PRECAUTIONS='{31}',
                               HIS_COMPARE.FEE_ITEMGRADE='{32}' ,
                               HIS_COMPARE.DOSAGE='{33}' ,
                               HIS_COMPARE.USAGE='{34}' ,
                               HIS_COMPARE.DOSAGE_UNIT='{35}' ,
                               HIS_COMPARE.ONCE_DOSAGE='{36}' ,
                               HIS_COMPARE.FREQUENCY='{37}' ,
                               HIS_COMPARE.DRUG_COMMON_LIMIT_FLAG='{38}' ,
                               HIS_COMPARE.DRUG_SPECIAL_LIMIT_FLAG='{39}' ,
                               HIS_COMPARE.MATERIAL_LIMITUSE_FLAG='{40}' ,
                               HIS_COMPARE.ISNEED_SITECODE='{41}' ,
                               HIS_COMPARE.COMPENSATE={42} ,
                               HIS_COMPARE.NO_COMPENSATE={43} ,
                               HIS_COMPARE.COMPENSATE_RATE={44} 
                         WHERE HIS_COMPARE.HIS_CODE='{0}' AND HIS_COMPARE.CHARGE_TYPE_CODE='{28}' AND HIS_COMPARE.HIS_CLASS='{26}'";
            object[] param = new object[] { his_compare.HIS_CODE, his_compare.CENTER_CODE, his_compare.FEE_TYPE, 
                his_compare.CENTER_NAME, his_compare.CENTER_SPECS, his_compare.CENTER_UNIT, his_compare.CENTER_PRICE, 
                his_compare.CENTER_TYPE, his_compare.CENTER_RATE, his_compare.CENTER_PACK, his_compare.CENTER_PLACE, 
                his_compare.HIS_NAME, his_compare.HIS_SPELL, his_compare.HIS_SPECS, his_compare.HIS_UNIT, 
                his_compare.HIS_PRICE, his_compare.HIS_TYPE, his_compare.HIS_PACK, his_compare.HIS_PLACE, 
                his_compare.OPER_CODE, his_compare.OPER_DATE, his_compare.APPLYFLAG, his_compare.PERSONRATE, 
                his_compare.HIS_WB_CODE, his_compare.HIS_USER_CODE, his_compare.TRANS, his_compare.HIS_CLASS, 
                his_compare.CENTER_CLASS, his_compare.CHARGE_TYPE_CODE, his_compare.DRUG_TABOO, 
                his_compare.UNTOWARD_REACTION, his_compare.PRECAUTIONS, his_compare.FEE_ITEMGRADE, 
                his_compare.DOSAGE, his_compare.USAGE, his_compare.DOSAGE_UNIT, his_compare.ONCE_DOSAGE, 
                his_compare.FREQUENCY, his_compare.DRUG_COMMON_LIMIT_FLAG, his_compare.DRUG_SPECIAL_LIMIT_FLAG, 
                his_compare.MATERIAL_LIMITUSE_FLAG, his_compare.ISNEED_SITECODE, his_compare.COMPENSATE, 
                his_compare.NO_COMPENSATE, his_compare.COMPENSATE_RATE,his_compare.HIS_CODE };
            sql = Utility.SqlFormate(sql, param);
            return db.ExecuteNonQuery(sql);
        }

        public DataTable GetSIINFOALL(BaseEntityer db, DateTime dt1, DateTime dt2)
        {
            try
            {
                //pact_code ='6' and
                string sql = @"select * from siinfo where  OPER_DATE >= to_date('{0}','yyyy-MM-dd hh24:mi:ss') and OPER_DATE <= to_date('{1}','yyyy-MM-dd hh24:mi:ss')";
                sql = string.Format(sql, dt1.ToShortDateString(), dt2.ToShortDateString());
                DataTable dt = db.GetDataTable(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 患者基本信息修改
        /// <summary>
        /// 更新患者基本信息
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="name"></param>
        /// <param name="sex"></param>
        /// <param name="birthDay"></param>
        /// <returns></returns>
        public int UpdatePatientInfoByPatientID(BaseEntityer db, string patientID, string name, string sex, string idenNO, string birthDay)
        {
            string sql = "";
            var name01 = name.Split('?');
            string phone = "";
            name = name01[0];
            try
            {
                phone = name01[1];
            }
            catch (Exception e)
            {
            }
            if (string.IsNullOrEmpty(phone))
            {
                sql = @"UPDATE pat_master_index t
   SET t.name          = '{0}',
       t.sex           = '{1}',
       t.id_no         = '{2}',
       t.date_of_birth = to_date('{3}', 'yyyy-mm-dd hh24:mi:ss') 
      
 WHERE t.patient_id = '{4}'
";
                sql = string.Format(sql, name, sex, idenNO, birthDay, patientID );
                return db.ExecuteNonQuery(sql);
            }
            else
            {
                sql = @"UPDATE pat_master_index t
   SET t.name          = '{0}',
       t.sex           = '{1}',
       t.id_no         = '{2}',
       t.date_of_birth = to_date('{3}', 'yyyy-mm-dd hh24:mi:ss'),
      t.PHONE_NUMBER_HOME='{5}'
 WHERE t.patient_id = '{4}'
";
                sql = string.Format(sql, name, sex, idenNO, birthDay, patientID, phone);
                return db.ExecuteNonQuery(sql);
            }
        }

        /// <summary>
        /// 更新门诊患者挂号信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="patientID"></param>
        /// <param name="name"></param>
        /// <param name="sex"></param>
        /// <param name="age"></param>
        /// <param name="validDay"></param>
        /// <returns></returns>
        public int UpdateClinicPatientInfoByPateint(BaseEntityer db, string patientID, string name, string sex, string age, int validDay)
        {
            var name01 = name.Split('?');
            string phone = "";
            name = name01[0];
            try
            {
                phone = name01[1];
            }
            catch (Exception e)
            {
            }
            string sql = @"UPDATE clinic_master t
   SET t.name = '{0}',
       t.sex  = '{1}',
       t.age  = '{2}'
 WHERE t.visit_date > SYSDATE - {3}
   AND t.patient_id = '{4}'
";
            sql = string.Format(sql, name, sex, age, validDay, patientID);
            return db.ExecuteNonQuery(sql);
        }
        #endregion

        /// <summary>
        /// 保存签名图片
        /// </summary>
        /// <param name="db"></param>
        /// <param name="user_id"></param>
        /// <param name="mybyte"></param>
        /// <returns></returns>
        public int SaveSignature(string user_id, byte[] mybyte, bool b)
        {
            if (b)
            {
                string sql = "update users_staff_dict set SIGNATURE =:image where USER_ID=:user_id";
                System.Data.OracleClient.OracleParameter[] param = {
                new System.Data.OracleClient.OracleParameter(":image",System.Data.OracleClient.OracleType.Blob,mybyte.Length),
                new System.Data.OracleClient.OracleParameter(":user_id",System.Data.OracleClient.OracleType.VarChar,100)
                                                               };
                param[0].Value = mybyte;
                param[1].Value = user_id;
                return BaseEntityer.Db.ExecuteSql(sql, param);
            }
            else
            {
                string sql = "update users_staff_dict set SIGNATURE ='' where USER_ID=:user_id";
                System.Data.OracleClient.OracleParameter[] param = {               
                new System.Data.OracleClient.OracleParameter(":user_id",System.Data.OracleClient.OracleType.VarChar,100)
                                                               };
                param[0].Value = user_id;
                return BaseEntityer.Db.ExecuteSql(sql, param);
            }
        }

        #region 日志记录

        /// <summary>
        /// 插入系统维护界面的日志记录
        /// </summary>
        /// <param name="record_type"></param>
        /// <param name="changed_text"></param>
        /// <param name="relation_table"></param>
        /// <param name="oper_code"></param>
        /// <param name="index_info"></param>
        /// <param name="warning_info"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public int SaveSystemLogRecord(string record_type, string changed_text, string relation_table, string oper_code,
            string index_info, string warning_info, BaseEntityer db)
        {

            string sql = @"INSERT INTO meu_change_record
                          (serialno,
                           record_type,
                           changed_text,
                           relation_table,
                           oper_code,
                           oper_date,
                           index_info,
                           warning_info)
                        VALUES
                          ( SEQ_MEU_CHANGE_RECORD.Nextval,
                           '{0}',
                           '{1}',
                           '{2}',
                           '{3}',
                           sysdate,
                           '{4}',
                           '{5}')
                        ";
            sql = string.Format(sql, record_type, changed_text, relation_table, oper_code, index_info, warning_info);

            return db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获得价表根据主键
        /// </summary>
        /// <param name="price_list_item_new"></param>
        /// <returns></returns>
        public PRICE_LIST GetPriceLstByPK(PRICE_LIST price_list_item_new)
        {
            string sql = @"SELECT *
                              FROM price_list
                             WHERE price_list.item_class = '{0}'
                               AND price_list.item_code = '{1}'
                               AND price_list.item_spec = '{2}'
                               AND price_list.units = '{3}'
                               AND price_list.start_date = to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')
                            ";
            object[] param = new object[] { price_list_item_new.ITEM_CLASS, 
                price_list_item_new.ITEM_CODE, price_list_item_new.ITEM_SPEC, 
                price_list_item_new.UNITS, price_list_item_new.START_DATE };
            sql = Utility.SqlFormate(sql, param);
            var lst = DataSetToEntity.DataSetToT<PRICE_LIST>(BaseEntityer.Db.GetDataSet(sql));

            if (lst != null && lst.Count > 0)
                return lst.First();
            else
                return new PRICE_LIST();
        }

        /// <summary>
        /// 获得员工信息根据主键
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public USERS_STAFF_DICT GetUserStaffDictByPK(string userID)
        {
            string sql = @"SELECT
                            t.OPERDATE,
                            t.OPERCODE,
                            t.IS_NH,
                            t.IS_CASHIER,
                            t.OPER_CODE,
                            t.CERTIFICATE_PLACE,
                            t.STAR_LEVEL,
                            t.HOS_STAFF_TYPE,
                            t.PRIMARY_ILLNESS,
                            t.CERTIFICATE_DATE,
                            t.PROFESS_RANGE,
                            t.PROFESS_TYPE,
                            t.ID_CARD,
                            t.SCHOOL,
                            t.PROFESSIONAL_TITILE,
                            t.JOB_TITLE,
                            t.DOCTOR_TYPE,
                            t.SEX,
                            t.CERTIFICATE_CODE,
                            t.TOXI,
                            t.DIS,
                            t.USER_PASS,
                            t.MENU_GROUP,
                            t.TITLE,
                            t.JOB,
                            t.CREATE_DATE,
                            t.USER_DEPT,
                            t.USER_NAME,
                            t.USER_ID
                            FROM
                            USERS_STAFF_DICT  t
                            WHERE
                               t.user_id = '{0}'
                            ";
            object[] param = new object[] { userID };
            sql = Utility.SqlFormate(sql, param);
            var lst = DataSetToEntity.DataSetToT<USERS_STAFF_DICT>(BaseEntityer.Db.GetDataSet(sql));

            if (lst != null && lst.Count > 0)
                return lst.First();
            else
                return new USERS_STAFF_DICT();
        }

        /// <summary>
        /// 获得人员权限信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<USERS_GROUP_DICT> GetUserGroupDictInfoByPK(string userID)
        {
            string sql = @"SELECT * from USERS_GROUP_DICT where USERS_GROUP_DICT.USERID = '{0}'";
            sql = string.Format(sql, userID);

            return DataSetToEntity.DataSetToT<USERS_GROUP_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        
        /// <summary>
        /// 获得诊疗项目名称的字典
        /// </summary>
        /// <param name="itemClass"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public List<CLINIC_ITEM_NAME_DICT> GetClinicItemNameDictByPK(string itemClass,string itemCode)
        {
            string sql = @"
                        SELECT *
                            FROM clinic_item_name_dict
                            WHERE clinic_item_name_dict.item_class = '{0}'
                            AND clinic_item_name_dict.item_code = '{1}'
                        ";
            sql = string.Format(sql, itemClass, itemCode);

            return DataSetToEntity.DataSetToT<CLINIC_ITEM_NAME_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

           /// <summary>
        /// 获得诊疗项目的字典
        /// </summary>
        /// <param name="itemClass"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public List<CLINIC_ITEM_DICT> GetClinicItemDictByPK(string itemClass, string itemCode)
        {
            string sql = @"
                       SELECT *
                           FROM clinic_item_dict
                          WHERE clinic_item_dict.item_class = '{0}'
                            AND clinic_item_dict.item_code = '{1}'
                        ";
            sql = string.Format(sql, itemClass, itemCode);

            return DataSetToEntity.DataSetToT<CLINIC_ITEM_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 获得科室字典信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="serialNO"></param>
        /// <returns></returns>
        public List<DEPT_DICT> GetDeptDictByPK(string deptCode,string serialNO)
        {
            string sql = @" SELECT * FROM dept_dict  r WHERE r.dept_code='{0}'";

            if (!string.IsNullOrEmpty(deptCode))
                sql = string.Format(sql, deptCode);
            else
            {
                sql = "SELECT * FROM dept_dict  r WHERE r.serial_no='{0}'";
                sql = string.Format(sql, serialNO);
            }

            return DataSetToEntity.DataSetToT<DEPT_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 查询费用字典信息
        /// </summary>
        /// <returns></returns>
        public List<CHARGE_TYPE_DICT> QueryChargeTypeDictInfo()
        {
            string sql = @"SELECT * from   charge_type_dict";

            return DataSetToEntity.DataSetToT<CHARGE_TYPE_DICT>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }
        #endregion

        
        public int UpdateModel(BaseEntityer db, EMR_PATMODEL MODEL )
        {

            //修改pat_model表
            string sql = @"update emr.pat_model set 
                finish='{1}',
                CAserial = '{2}',
              CAtimestamp = '{3}'
               where pat_modelid='{0}'";


          //      "" + finish + "',CAserial='" + CAserial + "',CAtimestamp='" + CAtimestamp + "'" +
          //    " where pat_modelid='" + _nowPat_modelid + "'";
          //  //  DBEMRHelper.ExecuteSql(sql);
          //  MyServer.ServerManager.UseService(
          //(MyServer.ServiceCommon.ICommon emr) =>
          //(emr.ExecuteNonQuery(sql)));

 
            sql = string.Format(sql, 
                MODEL.PAT_MODELID,
                MODEL.FINISH,
                MODEL.CASERIAL,
                MODEL.CATIMESTAMP );
            return db.ExecuteNonQuery(sql);
            
         //   return BaseEntityer.Db.ExecuteSql(sql, param);

        }
        public int UpdateModelBlob(BaseEntityer db, string modelid, byte[] mybyte,string  txtContents )
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update emr.PAT_MODEL_BLOB set ");
            //strSql.Append("BLOB=:BLOB,TXT=:TXT");
            //strSql.Append(" where PAT_MODELID=:PAT_MODELID ;");
            //OracleParameter[] parameters = {
            //    new OracleParameter(":BLOB",OracleType.LongRaw),
            //    new OracleParameter(":TXT",OracleType.Clob),
            //    new OracleParameter(":PAT_MODELID", OracleType.VarChar,32)};

            //parameters[0].Value = lbytContents;
            //parameters[1].Value = txtContents;
            //parameters[2].Value = _nowPat_modelid;

            string sql = @"update emr.PAT_MODEL_BLOB set" +
                " BLOB=:BLOB   where PAT_MODELID=:PAT_MODELID ";
                 OracleParameter[] param = {
                new  OracleParameter(":BLOB", OracleType.LongRaw ), 
                new  OracleParameter(":PAT_MODELID",OracleType.VarChar,100)
                                                               };
                param[0].Value = mybyte;
          
        //    param[1].Value = txtContents.Substring(0,4000);
                param[1].Value = modelid;
            return db.ExecuteSql(sql, param);
             
        }
        public int UpdateModelBlobPDF(string modelid, byte[] mybytePDF)
        {

           
            //    strSql.Append("update emr.PAT_MODEL_BLOB_PDF set ");
            //    strSql.Append("BLOB=:BLOB");
            //    strSql.Append(" where PAT_MODELID=:PAT_MODELID ;");
            //    OracleParameter[] parameters1 = {
            //    new OracleParameter(":BLOB", OracleType.LongRaw),
            //    new OracleParameter(":PAT_MODELID", OracleType.VarChar,32)};
            //    parameters1[0].Value = by_pdf;
            //    parameters1[1].Value = _nowPat_modelid;
            //    SQLStringList.Add(strSql.ToString(), parameters1);
            string sql = @"update emr.PAT_MODEL_BLOB_PDF set" +
                " BLOB=:BLOB   where PAT_MODELID=:PAT_MODELID ";
            OracleParameter[] param = {
                new  OracleParameter(":BLOB", OracleType.LongRaw ), 
                new  OracleParameter(":PAT_MODELID",OracleType.VarChar,100)
                                                               };
            param[0].Value = mybytePDF; 
            param[1].Value = modelid;
            return BaseEntityer.Db.ExecuteSql(sql, param);

        }
        public int UpdateModelBlobTXT(string modelid, string txtContents)
        {
            string sql = @"update emr.PAT_MODEL_BLOB set" +
              " BLOB=:BLOB  where PAT_MODELID=:PAT_MODELID ";
            System.Data.OracleClient.OracleParameter[] param = {
                new System.Data.OracleClient.OracleParameter(":BLOB", OracleType.Clob ),
                new System.Data.OracleClient.OracleParameter(":PAT_MODELID",OracleType.VarChar,100)
                                                               };
            param[0].Value = txtContents;
            param[1].Value = modelid;
            return BaseEntityer.Db.ExecuteSql(sql, param);
        }


        public int InsertModel(BaseEntityer db, EMR_PATMODEL MODEL)
        {

            //pat_model表 
            string sql = @"INSERT INTO emr.PAT_MODEL ( 
                                    PAT_MODELID ,
                                    PAT_CODE ,
                                    MODEL_ID ,
                                    MODEL_NAME ,
                                    CREATE_TIME ,

                                    BZ ,
                                    PAT_MODELNAME ,
                                    CJR ,
                                    VISIT_ID ,
                                    EMR_TYPE_C ,

                                    COMPARE_TYPE ,
                                    REQUIRED ,
                                    INTERVAL_IN ,
                                    INTERVAL_OUT ,
                                    FINISH ,

                                    PRINTMODE,
                                    CAserial,
                                    CAtimestamp ) 
                                   VALUES (
'{0}','{1}','{2}','{3}',to_date('{4}', 'yyyy-mm-dd hh24:mi:ss'),
'{5}','{6}','{7}','{8}','{9}',
'{10}','{11}','{12}','{13}','{14}',
'{15}','{16}','{17}')";

            sql = string.Format(sql,
                MODEL.PAT_MODELID,
                MODEL.PAT_CODE,
                MODEL.MODEL_ID,
                MODEL.MODEL_NAME,
                MODEL.CREATE_TIME,

                MODEL.BZ,
                MODEL.PAT_MODELNAME,
                MODEL.CJR,
                MODEL.VISIT_ID,
                MODEL.EMR_TYPE_C,

                MODEL.COMPARE_TYPE,
                MODEL.REQUIRED,
                MODEL.INTERVAL_IN,
                MODEL.INTERVAL_OUT,
                MODEL.FINISH,

                MODEL.PRINTMODE,
                MODEL.CASERIAL,
                MODEL.CATIMESTAMP 
                );

            return db.ExecuteNonQuery(sql); 


            //   return BaseEntityer.Db.ExecuteSql(sql, param);

        }
        public int InsertModelBlob(BaseEntityer db, string modelid, byte[] mybyte, string txtContents)
        {

            //strSql.Append("insert into emr.PAT_MODEL_BLOB(");
            //strSql.Append("PAT_MODELID,BLOB,TXT)");
            //strSql.Append(" values (");
            //strSql.Append(":PAT_MODELID,:BLOB,:TXT)");
            string sql = @"insert into emr.PAT_MODEL_BLOB   
                    (PAT_MODELID ) VALUES (
                     '{0}' )";

            //// new OracleParameter(":TXT", OracleType.Clob),
            //OracleParameter[] param = { 
            //     new  OracleParameter(":PAT_MODELID",OracleType.VarChar,100) 
            //   };
            //param[0].Value = modelid;
           
            //if (txtContents.Length>4000)
            //    txtContents= txtContents.Substring(0, 4000);
            //param[1].Value = txtContents ;
           

            //string sql = @"update emr.PAT_MODEL_BLOB set" +
            //             " BLOB=:BLOB ,TXT=:TXT where PAT_MODELID=:PAT_MODELID ";
          sql = string.Format(sql, modelid);
          return db.ExecuteNonQuery(sql);
            //  return db.ExecuteSql(sql, param);

        }
        public int InsertModelBlobPDF(string modelid, byte[] mybytePDF)
        {


            //    strSql.Append("update emr.PAT_MODEL_BLOB_PDF set ");
            //    strSql.Append("BLOB=:BLOB");
            //    strSql.Append(" where PAT_MODELID=:PAT_MODELID ;");
            //    OracleParameter[] parameters1 = {
            //    new OracleParameter(":BLOB", OracleType.LongRaw),
            //    new OracleParameter(":PAT_MODELID", OracleType.VarChar,32)};
            //    parameters1[0].Value = by_pdf;
            //    parameters1[1].Value = _nowPat_modelid;
            //    SQLStringList.Add(strSql.ToString(), parameters1);
            string sql = @"update emr.PAT_MODEL_BLOB_PDF set" +
                " BLOB=:BLOB   where PAT_MODELID=:PAT_MODELID ";
            OracleParameter[] param = {
                new  OracleParameter(":BLOB", OracleType.LongRaw ),
                new  OracleParameter(":PAT_MODELID",OracleType.VarChar,100)
                                                               };
            param[0].Value = mybytePDF;
            param[1].Value = modelid;
            return BaseEntityer.Db.ExecuteSql(sql, param);

        }
        public int InsertModelBlobTXT(string modelid, string txtContents)
        {
            string sql = @"update emr.PAT_MODEL_BLOB set" +
              " BLOB=:BLOB  where PAT_MODELID=:PAT_MODELID ";
            System.Data.OracleClient.OracleParameter[] param = {
                new System.Data.OracleClient.OracleParameter(":BLOB", OracleType.Clob ),
                new System.Data.OracleClient.OracleParameter(":PAT_MODELID",OracleType.VarChar,100)
                                                               };
            param[0].Value = txtContents;
            param[1].Value = modelid;
            return BaseEntityer.Db.ExecuteSql(sql, param);
        }


    }
}