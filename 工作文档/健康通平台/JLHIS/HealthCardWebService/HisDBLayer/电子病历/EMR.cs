using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using HisCommon.DataEntity;
using System.Data.Common;
using System.Data.OracleClient;

namespace HisDBLayer
{
    public class EMR
    {


        /// <summary>
        /// 文件名:DataTable 和Json 字符串互转
        /// </summary> 
        #region DataTable 转换为Json 字符串
        /// <summary>
        /// DataTable 对象 转换为Json 字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ToJson(DataTable dt)
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
        public DataTable ToDataTable(string json)
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
        public DataTable Query(string sql)
        {
            
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public DataSet QueryDataSet(string sql)
        {
            return BaseEntityer.Db.GetDataSet(sql);
        }

        public string QueryTOJson(string sql)
        {
            return ToJson(BaseEntityer.Db.GetDataTable(sql));
        }

        public DataTable QueryQx()
        {
            string sql = @"select * from emr_menu_module where menu_parent='0' and module_code='ED2927C2-9EC5-4cb4-B6BC-EB843114B00F' 
                            order by menu_order";

            return BaseEntityer.Db.GetDataTable(sql);


        }

        public DataTable QueryBntQx()
        {
            string sql = @"select emr_menu_module.* 
                           from emr_menu_group_dict,emr_menu_module
                           where emr_menu_group_dict.menu_code=emr_menu_module.menu_code
                           and emr_menu_group_dict.menu_group_code='396B981E-D765-40c2-9FF6-753711198955' 
                           and  menu_parent='E98B505F-FFCC-4ad7-9A83-B992EFE1AC3B' order by menu_order";
            return BaseEntityer.Db.GetDataTable(sql);
        }

        public string GetString(string obj)
        {


            if (obj == null)
            {
                obj = "";
            }

            return obj;

        }
        #endregion




        #region   模板保存 emr_model,model_bolb

        public int Add_Emr_Model(EMR_MODEL model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EMR_MODEL set ");
            strSql.Append("MODEL_NAME=:MODEL_NAME,");
            strSql.Append("MODEL_PROPERTY_C=:MODEL_PROPERTY_C,");
            strSql.Append("MODEL_TYPE_C=:MODEL_TYPE_C,");
            strSql.Append("DEPT_CODE=:DEPT_CODE,");
            strSql.Append("DIAGNOSIS_CODE=:DIAGNOSIS_CODE,");
            strSql.Append("CREATE_TIME=:CREATE_TIME,");
            strSql.Append("FZR=:FZR,");
            strSql.Append("IS_FLAG=:IS_FLAG,");
            strSql.Append("BZ=:BZ,");
            strSql.Append("EMR_TYPE_C=:EMR_TYPE_C,");
            strSql.Append("COMPARE_TYPE=:COMPARE_TYPE,");
            strSql.Append("INTERVAL_IN=:INTERVAL_IN,");
            strSql.Append("INTERVAL_OUT=:INTERVAL_OUT,");
            strSql.Append("REQUIRED=:REQUIRED,");
            strSql.Append("ISCREATE=:ISCREATE,");
            strSql.Append("SORT=:SORT,");
            strSql.Append("SORT_OUT=:SORT_OUT,");
            strSql.Append("SORT_IN=:SORT_IN,");
            strSql.Append("OUTO_LOCK=:OUTO_LOCK,");
            strSql.Append("NUM=:NUM,");
            strSql.Append("PARENT_MODEL_ID=:PARENT_MODEL_ID");
            strSql.Append(" where MODEL_ID=:MODEL_ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":MODEL_NAME", OracleType.VarChar,200),
					new OracleParameter(":MODEL_PROPERTY_C", OracleType.VarChar,32),
					new OracleParameter(":MODEL_TYPE_C", OracleType.VarChar,1),
					new OracleParameter(":DEPT_CODE", OracleType.VarChar,8),
					new OracleParameter(":DIAGNOSIS_CODE", OracleType.VarChar,16),
					new OracleParameter(":CREATE_TIME", OracleType.DateTime),
					new OracleParameter(":FZR", OracleType.VarChar,10),
					new OracleParameter(":IS_FLAG", OracleType.VarChar,1),
					new OracleParameter(":BZ", OracleType.VarChar,500),
					new OracleParameter(":EMR_TYPE_C", OracleType.VarChar,32),
					new OracleParameter(":COMPARE_TYPE", OracleType.VarChar,32),
					new OracleParameter(":INTERVAL_IN", OracleType.VarChar,32),
					new OracleParameter(":INTERVAL_OUT", OracleType.VarChar,32),
					new OracleParameter(":REQUIRED", OracleType.VarChar,32),
					new OracleParameter(":ISCREATE", OracleType.Number,4),
					new OracleParameter(":SORT", OracleType.VarChar,32),
					new OracleParameter(":SORT_OUT", OracleType.VarChar,32),
					new OracleParameter(":SORT_IN", OracleType.VarChar,32),
					new OracleParameter(":OUTO_LOCK", OracleType.VarChar,32),
					new OracleParameter(":NUM", OracleType.VarChar,32),
					new OracleParameter(":PARENT_MODEL_ID", OracleType.VarChar,50),
					new OracleParameter(":MODEL_ID", OracleType.VarChar,32)};
            parameters[0].Value = model.MODEL_NAME;
            parameters[1].Value = model.MODEL_PROPERTY_C;
            parameters[2].Value = model.MODEL_TYPE_C;
            parameters[3].Value = model.DEPT_CODE;
            parameters[4].Value = model.DIAGNOSIS_CODE;
            parameters[5].Value = model.CREATE_TIME;
            parameters[6].Value = model.FZR;
            parameters[7].Value = model.IS_FLAG;
            parameters[8].Value = model.BZ;
            parameters[9].Value = model.EMR_TYPE_C;
            parameters[10].Value = model.COMPARE_TYPE;
            parameters[11].Value = model.INTERVAL_IN;
            parameters[12].Value = model.INTERVAL_OUT;
            parameters[13].Value = model.REQUIRED;
            parameters[14].Value = model.ISCREATE;
            parameters[15].Value = model.SORT;
            parameters[16].Value = model.SORT_OUT;
            parameters[17].Value = model.SORT_IN;
            parameters[18].Value = model.OUTO_LOCK;
            parameters[19].Value = model.NUM;
            parameters[20].Value = model.PARENT_MODEL_ID;
            parameters[21].Value = model.MODEL_ID;

            int rows = BaseEntityer.Db.ExecuteSql(strSql.ToString(), parameters);
            return rows;
 
        }

        public int Update_Emr_Model(EMR_MODEL model)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("update EMR_MODEL set ");
			strSql.Append("MODEL_NAME=:MODEL_NAME,");
			strSql.Append("MODEL_PROPERTY_C=:MODEL_PROPERTY_C,");
			strSql.Append("MODEL_TYPE_C=:MODEL_TYPE_C,");
			strSql.Append("DEPT_CODE=:DEPT_CODE,");
			strSql.Append("DIAGNOSIS_CODE=:DIAGNOSIS_CODE,");
			strSql.Append("CREATE_TIME=:CREATE_TIME,");
			strSql.Append("FZR=:FZR,");
			strSql.Append("IS_FLAG=:IS_FLAG,");
			strSql.Append("BZ=:BZ,");
			strSql.Append("EMR_TYPE_C=:EMR_TYPE_C,");
			strSql.Append("COMPARE_TYPE=:COMPARE_TYPE,");
			strSql.Append("INTERVAL_IN=:INTERVAL_IN,");
			strSql.Append("INTERVAL_OUT=:INTERVAL_OUT,");
			strSql.Append("REQUIRED=:REQUIRED,");
			strSql.Append("ISCREATE=:ISCREATE,");
			strSql.Append("SORT=:SORT,");
			strSql.Append("SORT_OUT=:SORT_OUT,");
			strSql.Append("SORT_IN=:SORT_IN,");
			strSql.Append("OUTO_LOCK=:OUTO_LOCK,");
			strSql.Append("NUM=:NUM,");
			strSql.Append("PARENT_MODEL_ID=:PARENT_MODEL_ID");
			strSql.Append(" where MODEL_ID=:MODEL_ID ");
			OracleParameter[] parameters = {
					new OracleParameter(":MODEL_NAME", OracleType.VarChar,200),
					new OracleParameter(":MODEL_PROPERTY_C", OracleType.VarChar,32),
					new OracleParameter(":MODEL_TYPE_C", OracleType.VarChar,1),
					new OracleParameter(":DEPT_CODE", OracleType.VarChar,8),
					new OracleParameter(":DIAGNOSIS_CODE", OracleType.VarChar,16),
					new OracleParameter(":CREATE_TIME", OracleType.DateTime),
					new OracleParameter(":FZR", OracleType.VarChar,10),
					new OracleParameter(":IS_FLAG", OracleType.VarChar,1),
					new OracleParameter(":BZ", OracleType.VarChar,500),
					new OracleParameter(":EMR_TYPE_C", OracleType.VarChar,32),
					new OracleParameter(":COMPARE_TYPE", OracleType.VarChar,32),
					new OracleParameter(":INTERVAL_IN", OracleType.VarChar,32),
					new OracleParameter(":INTERVAL_OUT", OracleType.VarChar,32),
					new OracleParameter(":REQUIRED", OracleType.VarChar,32),
					new OracleParameter(":ISCREATE", OracleType.Number,4),
					new OracleParameter(":SORT", OracleType.VarChar,32),
					new OracleParameter(":SORT_OUT", OracleType.VarChar,32),
					new OracleParameter(":SORT_IN", OracleType.VarChar,32),
					new OracleParameter(":OUTO_LOCK", OracleType.VarChar,32),
					new OracleParameter(":NUM", OracleType.VarChar,32),
					new OracleParameter(":PARENT_MODEL_ID", OracleType.VarChar,50),
					new OracleParameter(":MODEL_ID", OracleType.VarChar,32)};
			parameters[0].Value = model.MODEL_NAME;
			parameters[1].Value = model.MODEL_PROPERTY_C;
			parameters[2].Value = model.MODEL_TYPE_C;
			parameters[3].Value = model.DEPT_CODE;
			parameters[4].Value = model.DIAGNOSIS_CODE;
			parameters[5].Value = model.CREATE_TIME;
			parameters[6].Value = model.FZR;
			parameters[7].Value = model.IS_FLAG;
			parameters[8].Value = model.BZ;
			parameters[9].Value = model.EMR_TYPE_C;
			parameters[10].Value = model.COMPARE_TYPE;
			parameters[11].Value = model.INTERVAL_IN;
			parameters[12].Value = model.INTERVAL_OUT;
			parameters[13].Value = model.REQUIRED;
			parameters[14].Value = model.ISCREATE;
			parameters[15].Value = model.SORT;
			parameters[16].Value = model.SORT_OUT;
			parameters[17].Value = model.SORT_IN;
			parameters[18].Value = model.OUTO_LOCK;
			parameters[19].Value = model.NUM;
			parameters[20].Value = model.PARENT_MODEL_ID;
			parameters[21].Value = model.MODEL_ID;

			int rows=BaseEntityer.Db.ExecuteSql(strSql.ToString(),parameters);
            return rows;
        }



        public int Update_Emr_Model_Blob(EMR_MODEL_BLOB model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EMR_MODEL_BLOB set ");
            strSql.Append("BLOB=:BLOB");
            strSql.Append(" where MODEL_ID=:MODEL_ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":BLOB", OracleType.LongRaw),
					new OracleParameter(":MODEL_ID", OracleType.VarChar,32)};
            parameters[0].Value = model.BLOB;
            parameters[1].Value = model.MODEL_ID;

            int rows = BaseEntityer.Db.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        ///  俩个表一起插入(事务)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="model_blob"></param>
        /// <returns></returns>
        public bool Add_Emr_Model_Model_Bolb(EMR_MODEL model, EMR_MODEL_BLOB model_blob)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into EMR_MODEL(");
            strSql.Append("MODEL_ID,MODEL_NAME,MODEL_PROPERTY_C,MODEL_TYPE_C,DEPT_CODE,DIAGNOSIS_CODE,CREATE_TIME,FZR,IS_FLAG,BZ,EMR_TYPE_C,COMPARE_TYPE,INTERVAL_IN,INTERVAL_OUT,REQUIRED,ISCREATE,SORT,SORT_OUT,SORT_IN,OUTO_LOCK,NUM,PARENT_MODEL_ID)");
            strSql.Append(" values (");
            strSql.Append(":MODEL_ID,:MODEL_NAME,:MODEL_PROPERTY_C,:MODEL_TYPE_C,:DEPT_CODE,:DIAGNOSIS_CODE,:CREATE_TIME,:FZR,:IS_FLAG,:BZ,:EMR_TYPE_C,:COMPARE_TYPE,:INTERVAL_IN,:INTERVAL_OUT,:REQUIRED,:ISCREATE,:SORT,:SORT_OUT,:SORT_IN,:OUTO_LOCK,:NUM,:PARENT_MODEL_ID)");
            OracleParameter[] parameters = {
					new OracleParameter(":MODEL_ID", OracleType.VarChar,32),
					new OracleParameter(":MODEL_NAME", OracleType.VarChar,200),
					new OracleParameter(":MODEL_PROPERTY_C", OracleType.VarChar,32),
					new OracleParameter(":MODEL_TYPE_C", OracleType.VarChar,1),
					new OracleParameter(":DEPT_CODE", OracleType.VarChar,8),
					new OracleParameter(":DIAGNOSIS_CODE", OracleType.VarChar,16),
					new OracleParameter(":CREATE_TIME", OracleType.DateTime),
					new OracleParameter(":FZR", OracleType.VarChar,10),
					new OracleParameter(":IS_FLAG", OracleType.VarChar,1),
					new OracleParameter(":BZ", OracleType.VarChar,500),
					new OracleParameter(":EMR_TYPE_C", OracleType.VarChar,32),
					new OracleParameter(":COMPARE_TYPE", OracleType.VarChar,32),
					new OracleParameter(":INTERVAL_IN", OracleType.VarChar,32),
					new OracleParameter(":INTERVAL_OUT", OracleType.VarChar,32),
					new OracleParameter(":REQUIRED", OracleType.VarChar,32),
					new OracleParameter(":ISCREATE", OracleType.Number,4),
					new OracleParameter(":SORT", OracleType.VarChar,32),
					new OracleParameter(":SORT_OUT", OracleType.VarChar,32),
					new OracleParameter(":SORT_IN", OracleType.VarChar,32),
					new OracleParameter(":OUTO_LOCK", OracleType.VarChar,32),
					new OracleParameter(":NUM", OracleType.VarChar,32),
					new OracleParameter(":PARENT_MODEL_ID", OracleType.VarChar,50)};
            parameters[0].Value = model.MODEL_ID;
            parameters[1].Value = model.MODEL_NAME;
            parameters[2].Value = model.MODEL_PROPERTY_C;
            parameters[3].Value = model.MODEL_TYPE_C;
            parameters[4].Value = model.DEPT_CODE;
            parameters[5].Value = model.DIAGNOSIS_CODE;
            parameters[6].Value = model.CREATE_TIME;
            parameters[7].Value = model.FZR;
            parameters[8].Value = model.IS_FLAG;
            parameters[9].Value = model.BZ;
            parameters[10].Value = model.EMR_TYPE_C;
            parameters[11].Value = model.COMPARE_TYPE;
            parameters[12].Value = model.INTERVAL_IN;
            parameters[13].Value = model.INTERVAL_OUT;
            parameters[14].Value = model.REQUIRED;
            parameters[15].Value = model.ISCREATE;
            parameters[16].Value = model.SORT;
            parameters[17].Value = model.SORT_OUT;
            parameters[18].Value = model.SORT_IN;
            parameters[19].Value = model.OUTO_LOCK;
            parameters[20].Value = model.NUM;
            parameters[21].Value = model.PARENT_MODEL_ID;

            Hashtable hd = new Hashtable();
            hd.Add(strSql.ToString(), parameters);
            strSql.Clear();
            strSql = new StringBuilder();
            strSql.Append("insert into EMR_MODEL_BLOB(");
            strSql.Append("MODEL_ID,BLOB)");
            strSql.Append(" values (");
            strSql.Append(":MODEL_ID,:BLOB)");
            OracleParameter[] parameters1 = {
					new OracleParameter(":MODEL_ID", OracleType.VarChar,32),
					new OracleParameter(":BLOB", OracleType.LongRaw)};
            parameters1[0].Value = model_blob.MODEL_ID;
            parameters1[1].Value = model_blob.BLOB;
            hd.Add(strSql.ToString(), parameters1);
            return BaseEntityer.Db.ExecuteSqlTran(hd);
        }

        /// <summary>
        /// 删除俩个表(事务),删除模板
        /// </summary>
        /// <param name="model_id"></param>
        /// <returns></returns>
        public bool Del_Model_Model_Bolb(string model_id)
        {
            //BaseEntityer.Db.ExecuteSqlTran(
            ArrayList li = new ArrayList();
            string sql = "delete from emr_Model where model_id='" + model_id + "'";
            li.Add(sql);
            sql = "delete from emr_Model_blob where model_id='" + model_id + "'";
            li.Add(sql);
            return BaseEntityer.Db.ExecuteSqlTran(li);
 
        }
        
        #endregion

        #region  日志 EMR_LOGWORKS

        /// <summary>
        /// 增加一条数据 日志 
        /// </summary>
        public int Add_Emr_Logworks(EMR_LOGWORKS model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into EMR_LOGWORKS(");
            strSql.Append("LOGID,LOGTYPE,LOGNAME,LOGOPER,LOGDATE,LOGPRO,LOG_IP,LOG_M_NAME)");
            strSql.Append(" values (");
            strSql.Append(":LOGID,:LOGTYPE,:LOGNAME,:LOGOPER,:LOGDATE,:LOGPRO,:LOG_IP,:LOG_M_NAME)");
            OracleParameter[] parameters = {
					new OracleParameter(":LOGID", OracleType.VarChar,50),
					new OracleParameter(":LOGTYPE", OracleType.VarChar,50),
					new OracleParameter(":LOGNAME", OracleType.VarChar,255),
					new OracleParameter(":LOGOPER", OracleType.VarChar,20),
					new OracleParameter(":LOGDATE", OracleType.DateTime),
					new OracleParameter(":LOGPRO", OracleType.VarChar,50),
					new OracleParameter(":LOG_IP", OracleType.VarChar,255),
					new OracleParameter(":LOG_M_NAME", OracleType.VarChar,255)};
            parameters[0].Value = model.LOGID;
            parameters[1].Value = model.LOGTYPE;
            parameters[2].Value = model.LOGNAME;
            parameters[3].Value = model.LOGOPER;
            parameters[4].Value = model.LOGDATE;
            parameters[5].Value = model.LOGPRO;
            parameters[6].Value = model.LOG_IP;
            parameters[7].Value = model.LOG_M_NAME;

            int rows = BaseEntityer.Db.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        #endregion
        #region   元素 EMR_element_folder,emr_element,emr_elem_complex

        public int Add_Emr_Element_Folder(string type_id,string type_name,string type_parent)
        {
            string sql = String.Format("insert into emr_element_folder(type_id,type_name,type_parent) values('{0}','{1}','{2}')", type_id, type_name, type_parent);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        public int Update_Emr_Element_Folder(string type_name,string type_id)
        {
            string sql = String.Format("update emr_element_folder set type_name='{0}' where type_id='{1}'", type_name, type_id);
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }
        public int Del_Emr_Element_Folder(string type_id)
        {
            string sql = "delete from Emr_Element_Folder where type_id='" + type_id + "'";
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }


        public int Add_Emr_Element(EMR_ELEMENT model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into EMR_ELEMENT(");
            strSql.Append("ELEM_ID,ELEM_NAME,ELEM_TYPE,IS_FRAME,LEFT_FRAME,RIGHT_FRAME,IS_DEL,IS_MODIFY,CHOOSE_ITEM,MAX,MIN,DECI,TOP_TITLE,IS_MUST_FILL,IS_SECRET,UNITS,CON_B,IS_HIDE,IS_XML,DATE_B,DATE_E,DATE_FORM,MAXLENGTH,IS_COMFIRM,TYPE_ID,FZR)");
            strSql.Append(" values (");
            strSql.Append(":ELEM_ID,:ELEM_NAME,:ELEM_TYPE,:IS_FRAME,:LEFT_FRAME,:RIGHT_FRAME,:IS_DEL,:IS_MODIFY,:CHOOSE_ITEM,:MAX,:MIN,:DECI,:TOP_TITLE,:IS_MUST_FILL,:IS_SECRET,:UNITS,:CON_B,:IS_HIDE,:IS_XML,:DATE_B,:DATE_E,:DATE_FORM,:MAXLENGTH,:IS_COMFIRM,:TYPE_ID,:FZR)");
            OracleParameter[] parameters = {
					new OracleParameter(":ELEM_ID", OracleType.VarChar,32),
					new OracleParameter(":ELEM_NAME", OracleType.VarChar,2000),
					new OracleParameter(":ELEM_TYPE", OracleType.VarChar,32),
					new OracleParameter(":IS_FRAME", OracleType.VarChar,1),
					new OracleParameter(":LEFT_FRAME", OracleType.VarChar,50),
					new OracleParameter(":RIGHT_FRAME", OracleType.VarChar,50),
					new OracleParameter(":IS_DEL", OracleType.VarChar,1),
					new OracleParameter(":IS_MODIFY", OracleType.VarChar,1),
					new OracleParameter(":CHOOSE_ITEM", OracleType.VarChar,2000),
					new OracleParameter(":MAX", OracleType.Number,9),
					new OracleParameter(":MIN", OracleType.Number,9),
					new OracleParameter(":DECI", OracleType.Number,2),
					new OracleParameter(":TOP_TITLE", OracleType.VarChar,500),
					new OracleParameter(":IS_MUST_FILL", OracleType.VarChar,1),
					new OracleParameter(":IS_SECRET", OracleType.VarChar,1),
					new OracleParameter(":UNITS", OracleType.VarChar,50),
					new OracleParameter(":CON_B", OracleType.VarChar,500),
					new OracleParameter(":IS_HIDE", OracleType.VarChar,1),
					new OracleParameter(":IS_XML", OracleType.VarChar,1),
					new OracleParameter(":DATE_B", OracleType.DateTime),
					new OracleParameter(":DATE_E", OracleType.DateTime),
					new OracleParameter(":DATE_FORM", OracleType.VarChar,50),
					new OracleParameter(":MAXLENGTH", OracleType.Number,9),
					new OracleParameter(":IS_COMFIRM", OracleType.VarChar,2),
					new OracleParameter(":TYPE_ID", OracleType.VarChar,10),
					new OracleParameter(":FZR", OracleType.VarChar,50)};
            parameters[0].Value = model.ELEM_ID;
            parameters[1].Value = model.ELEM_NAME;
            parameters[2].Value = model.ELEM_TYPE;
            parameters[3].Value = model.IS_FRAME;
            parameters[4].Value = model.LEFT_FRAME;
            parameters[5].Value = model.RIGHT_FRAME;
            parameters[6].Value = model.IS_DEL;
            parameters[7].Value = model.IS_MODIFY;
            parameters[8].Value = model.CHOOSE_ITEM;
            parameters[9].Value = model.MAX;
            parameters[10].Value = model.MIN;
            parameters[11].Value = model.DECI;
            parameters[12].Value = model.TOP_TITLE;
            parameters[13].Value = model.IS_MUST_FILL;
            parameters[14].Value = model.IS_SECRET;
            parameters[15].Value = model.UNITS;
            parameters[16].Value = model.CON_B;
            parameters[17].Value = model.IS_HIDE;
            parameters[18].Value = model.IS_XML;
            parameters[19].Value = model.DATE_B;
            parameters[20].Value = model.DATE_E;
            parameters[21].Value = model.DATE_FORM;
            parameters[22].Value = model.MAXLENGTH;
            parameters[23].Value = model.IS_COMFIRM;
            parameters[24].Value = model.TYPE_ID;
            parameters[25].Value = model.FZR;

            int rows = BaseEntityer.Db.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        public int Update_Emr_Element(EMR_ELEMENT model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EMR_ELEMENT set ");
            strSql.Append("ELEM_NAME=:ELEM_NAME,");
            strSql.Append("ELEM_TYPE=:ELEM_TYPE,");
            strSql.Append("IS_FRAME=:IS_FRAME,");
            strSql.Append("LEFT_FRAME=:LEFT_FRAME,");
            strSql.Append("RIGHT_FRAME=:RIGHT_FRAME,");
            strSql.Append("IS_DEL=:IS_DEL,");
            strSql.Append("IS_MODIFY=:IS_MODIFY,");
            strSql.Append("CHOOSE_ITEM=:CHOOSE_ITEM,");
            strSql.Append("MAX=:MAX,");
            strSql.Append("MIN=:MIN,");
            strSql.Append("DECI=:DECI,");
            strSql.Append("TOP_TITLE=:TOP_TITLE,");
            strSql.Append("IS_MUST_FILL=:IS_MUST_FILL,");
            strSql.Append("IS_SECRET=:IS_SECRET,");
            strSql.Append("UNITS=:UNITS,");
            strSql.Append("CON_B=:CON_B,");
            strSql.Append("IS_HIDE=:IS_HIDE,");
            strSql.Append("IS_XML=:IS_XML,");
            strSql.Append("DATE_B=:DATE_B,");
            strSql.Append("DATE_E=:DATE_E,");
            strSql.Append("DATE_FORM=:DATE_FORM,");
            strSql.Append("MAXLENGTH=:MAXLENGTH,");
            strSql.Append("IS_COMFIRM=:IS_COMFIRM,");
            strSql.Append("TYPE_ID=:TYPE_ID,");
            strSql.Append("FZR=:FZR");
            strSql.Append(" where ELEM_ID=:ELEM_ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":ELEM_NAME", OracleType.VarChar,2000),
					new OracleParameter(":ELEM_TYPE", OracleType.VarChar,32),
					new OracleParameter(":IS_FRAME", OracleType.VarChar,1),
					new OracleParameter(":LEFT_FRAME", OracleType.VarChar,50),
					new OracleParameter(":RIGHT_FRAME", OracleType.VarChar,50),
					new OracleParameter(":IS_DEL", OracleType.VarChar,1),
					new OracleParameter(":IS_MODIFY", OracleType.VarChar,1),
					new OracleParameter(":CHOOSE_ITEM", OracleType.VarChar,2000),
					new OracleParameter(":MAX", OracleType.Number,9),
					new OracleParameter(":MIN", OracleType.Number,9),
					new OracleParameter(":DECI", OracleType.Number,2),
					new OracleParameter(":TOP_TITLE", OracleType.VarChar,500),
					new OracleParameter(":IS_MUST_FILL", OracleType.VarChar,1),
					new OracleParameter(":IS_SECRET", OracleType.VarChar,1),
					new OracleParameter(":UNITS", OracleType.VarChar,50),
					new OracleParameter(":CON_B", OracleType.VarChar,500),
					new OracleParameter(":IS_HIDE", OracleType.VarChar,1),
					new OracleParameter(":IS_XML", OracleType.VarChar,1),
					new OracleParameter(":DATE_B", OracleType.DateTime),
					new OracleParameter(":DATE_E", OracleType.DateTime),
					new OracleParameter(":DATE_FORM", OracleType.VarChar,50),
					new OracleParameter(":MAXLENGTH", OracleType.Number,9),
					new OracleParameter(":IS_COMFIRM", OracleType.VarChar,2),
					new OracleParameter(":TYPE_ID", OracleType.VarChar,10),
					new OracleParameter(":FZR", OracleType.VarChar,50),
					new OracleParameter(":ELEM_ID", OracleType.VarChar,32)};
            parameters[0].Value = GetString(model.ELEM_NAME);
            parameters[1].Value =  GetString(model.ELEM_TYPE);
            parameters[2].Value =  GetString(model.IS_FRAME);
            parameters[3].Value =  GetString(model.LEFT_FRAME);
            parameters[4].Value =  GetString(model.RIGHT_FRAME);
            parameters[5].Value =  GetString(model.IS_DEL);
            parameters[6].Value =  GetString(model.IS_MODIFY);
            parameters[7].Value =  GetString(model.CHOOSE_ITEM);
            parameters[8].Value = model.MAX;
            parameters[9].Value = model.MIN;
            parameters[10].Value = model.DECI;
            parameters[11].Value =  GetString(model.TOP_TITLE);
            parameters[12].Value =  GetString(model.IS_MUST_FILL);
            parameters[13].Value =  GetString(model.IS_SECRET);
            parameters[14].Value =  GetString(model.UNITS);
            parameters[15].Value =  GetString(model.CON_B);
            parameters[16].Value =  GetString(model.IS_HIDE);
            parameters[17].Value =  GetString(model.IS_XML);
            parameters[18].Value = model.DATE_B;
            parameters[19].Value = model.DATE_E;
            parameters[20].Value =  GetString(model.DATE_FORM);
            parameters[21].Value = model.MAXLENGTH;
            parameters[22].Value =  GetString(model.IS_COMFIRM);
            parameters[23].Value =  GetString(model.TYPE_ID);
            parameters[24].Value =  GetString(model.FZR);
            parameters[25].Value =  GetString(model.ELEM_ID);

            int rows = BaseEntityer.Db.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }


        public int Del_Emr_Element(string elem_id)
        {
            string sql = "delete from emr_element where elem_id='" + elem_id + "'";
            return BaseEntityer.Db.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EMR_ELEMENT Get_Emr_Element_Model(string ELEM_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ELEM_ID,ELEM_NAME,ELEM_TYPE,IS_FRAME,LEFT_FRAME,RIGHT_FRAME,IS_DEL,IS_MODIFY,CHOOSE_ITEM,MAX,MIN,DECI,TOP_TITLE,IS_MUST_FILL,IS_SECRET,UNITS,CON_B,IS_HIDE,IS_XML,DATE_B,DATE_E,DATE_FORM,MAXLENGTH,IS_COMFIRM,TYPE_ID,FZR from EMR_ELEMENT ");
            strSql.Append(" where ELEM_ID='" + ELEM_ID + "' ");
       		
            

            EMR_ELEMENT model = new EMR_ELEMENT();
            DataSet ds = BaseEntityer.Db.GetDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataSetToEntity.DataSetToT<EMR_ELEMENT>(ds).ToList()[0]; 
            }
            else
            {
                return null;
            }
        }



        public bool Del_Emr_Element_Emr_Element_Folder(string type_id)
        {


            ArrayList li = new ArrayList();
            //-- 删除所以 type_ID = 2000000012 文件夹 下面的元素
            string sql =  @"delete from emr_element where type_id in 
                        (SELECT type_id FROM emr_element_folder  START WITH type_ID = '"+type_id+"' CONNECT BY PRIOR type_ID=type_parent  )";
            li.Add(sql);
            //-- 删除所以 type_ID = 2000000012 文件夹下的文件夹 和 本身
            sql = @"delete from emr_element_folder where type_id in 
                (SELECT type_id FROM emr_element_folder  START WITH type_ID = '" + type_id + "'  CONNECT BY PRIOR type_ID=type_parent )";
            li.Add(sql);
            return BaseEntityer.Db.ExecuteSqlTran(li);
        }

        public int Add_Emr_Elem_Complex(EMR_ELEM_COMPLEX model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into EMR_ELEM_COMPLEX(");
            strSql.Append("ELEM_C_ID,ELEM_C_NAME,BLOB,COMPLEX_PARENTID,PARENTIDTYPE,DEPT_DICT,CJR,ELEM_C_TYPE,INTERVAL_IN)");
            strSql.Append(" values (");
            strSql.Append(":ELEM_C_ID,:ELEM_C_NAME,:BLOB,:COMPLEX_PARENTID,:PARENTIDTYPE,:DEPT_DICT,:CJR,:ELEM_C_TYPE,:INTERVAL_IN)");
            OracleParameter[] parameters = {
					new OracleParameter(":ELEM_C_ID", OracleType.VarChar,32),
					new OracleParameter(":ELEM_C_NAME", OracleType.VarChar,200),
					new OracleParameter(":BLOB", OracleType.LongRaw),
					new OracleParameter(":COMPLEX_PARENTID", OracleType.VarChar,32),
					new OracleParameter(":PARENTIDTYPE", OracleType.VarChar,32),
					new OracleParameter(":DEPT_DICT", OracleType.VarChar,32),
					new OracleParameter(":CJR", OracleType.VarChar,32),
					new OracleParameter(":ELEM_C_TYPE", OracleType.VarChar,50),
					new OracleParameter(":INTERVAL_IN", OracleType.VarChar,50)};
            parameters[0].Value =GetString(model.ELEM_C_ID);
            parameters[1].Value =GetString(model.ELEM_C_NAME);
            parameters[2].Value = model.BLOB;
            parameters[3].Value = GetString(model.COMPLEX_PARENTID);
            parameters[4].Value = GetString(model.PARENTIDTYPE);
            parameters[5].Value = GetString(model.DEPT_DICT);
            parameters[6].Value = GetString(model.CJR);
            parameters[7].Value = GetString(model.ELEM_C_TYPE);
            parameters[8].Value = GetString(model.INTERVAL_IN);

            int rows = BaseEntityer.Db.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        public int Update_Emr_Elem_Complex(EMR_ELEM_COMPLEX model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EMR_ELEM_COMPLEX set ");
            strSql.Append("ELEM_C_NAME=:ELEM_C_NAME,");
            strSql.Append("BLOB=:BLOB,");
            strSql.Append("COMPLEX_PARENTID=:COMPLEX_PARENTID,");
            strSql.Append("PARENTIDTYPE=:PARENTIDTYPE,");
            strSql.Append("DEPT_DICT=:DEPT_DICT,");
            strSql.Append("CJR=:CJR,");
            strSql.Append("ELEM_C_TYPE=:ELEM_C_TYPE,");
            strSql.Append("INTERVAL_IN=:INTERVAL_IN");
            strSql.Append(" where ELEM_C_ID=:ELEM_C_ID ");
            OracleParameter[] parameters = {
					new OracleParameter(":ELEM_C_NAME", OracleType.VarChar,200),
					new OracleParameter(":BLOB", OracleType.LongRaw),
					new OracleParameter(":COMPLEX_PARENTID", OracleType.VarChar,32),
					new OracleParameter(":PARENTIDTYPE", OracleType.VarChar,32),
					new OracleParameter(":DEPT_DICT", OracleType.VarChar,32),
					new OracleParameter(":CJR", OracleType.VarChar,32),
					new OracleParameter(":ELEM_C_TYPE", OracleType.VarChar,50),
					new OracleParameter(":INTERVAL_IN", OracleType.VarChar,50),
					new OracleParameter(":ELEM_C_ID", OracleType.VarChar,32)};
            parameters[0].Value = GetString(model.ELEM_C_NAME);
            parameters[1].Value = model.BLOB;
            parameters[2].Value = GetString(model.COMPLEX_PARENTID);
            parameters[3].Value = GetString(model.PARENTIDTYPE);
            parameters[4].Value = GetString(model.DEPT_DICT);
            parameters[5].Value = GetString(model.CJR);
            parameters[6].Value = GetString(model.ELEM_C_TYPE);
            parameters[7].Value = GetString(model.INTERVAL_IN);
            parameters[8].Value = GetString(model.ELEM_C_ID);
            int rows = BaseEntityer.Db.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EMR_ELEM_COMPLEX Get_Emr_Elem_Complex_Model(string ELEM_C_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ELEM_C_ID,ELEM_C_NAME,BLOB,COMPLEX_PARENTID,PARENTIDTYPE,DEPT_DICT,CJR,ELEM_C_TYPE,INTERVAL_IN from emr.ELEM_COMPLEX ");
            strSql.Append(" where ELEM_C_ID='" + ELEM_C_ID + "' ");
 



            EMR_ELEM_COMPLEX model = new EMR_ELEM_COMPLEX();
            DataSet ds = BaseEntityer.Db.GetDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataSetToEntity.DataSetToT<EMR_ELEM_COMPLEX>(ds).ToList()[0];
            }
            else
            {
                return null;
            }
        }

        #endregion
        #region  EMR_ELEMENT_SQL
        /// <summary>
        ///  添加或更新 新元素关联表(字体大小，长度)
        /// </summary>
        public int InsertEmr_Element_Sql(string sql, string e_len, string e_size, string e_size_small, string element_id)
        {
            int i = 0;
            string sqlstr = "select * from emr_element_sql where ELEMENT_ID='" + element_id + "'";
            DataTable dt = Query(sqlstr);
            if (dt.Rows.Count > 0)
            {
                string insertSQL = @"update emr_element_sql  set SQL='" + sql + "',e_len='" + e_len + "',e_size='" + e_size + "',e_size_small='" + e_size_small + "' where ELEMENT_ID='" + element_id + "'";
                i=BaseEntityer.Db.ExecuteSql(insertSQL); 
            }
            else
            {
                string guid = Guid.NewGuid().ToString("N");
                string insertSQL = @"insert into emr_element_sql (EECID, SQL,ELEMENT_ID,e_len,e_size,e_size_small)
                                 values ('" + guid + "', '" + sql + "','" + element_id + "','" + e_len + "','" + e_size + "','" + e_size_small + "')";
               i= BaseEntityer.Db.ExecuteSql(insertSQL);
            }
            return i;

        }


        #endregion

        #region  权限  MENU_GROUP_CODE,emr_menu_module,emr_module_dict
        public int Add_Menu_Group_code(string id,string menu_code,string groupcode)
        {
            string str = @"insert into  emr_menu_group_dict(id, 
                                                                                    menu_code,                                         
                                                                                    MENU_GROUP_CODE)
                                                                    values(  '" + id + @"', 
                                                                             '" + menu_code + @"',                                   
                                                                             '" + groupcode + @"')";

            return  BaseEntityer.Db.ExecuteSql(str); 
        }

        public int Delete_Menu_Group_code(string id)
        {
            string str = @"delete from emr_menu_group_dict where id='" + id + "'";

            return BaseEntityer.Db.ExecuteSql(str);
        }

        public int Add_Emr_Menu_Module(string menu_code, string module_code, string mnu_win,string mnu_pbl,string menu_parent,string menu_sort,string menu_ico,string menu_order,string menu_event,string menu_event_args,string menu_desc)
        {
            string sql = String.Format(@"insert into emr_menu_module
                                                                            (menu_code, module_code, mnu_win, mnu_pbl, menu_parent, menu_sort,menu_ico,menu_order, menu_event, menu_event_args,menu_desc)
                                                                            values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                          menu_code, module_code, mnu_win, mnu_pbl, menu_parent, menu_sort, menu_ico, menu_order, menu_event, menu_event_args, menu_desc);
            return BaseEntityer.Db.ExecuteSql(sql);
        }

        public int Update_Emr_Menu_Module( string module_code, string mnu_win, string mnu_pbl, string menu_parent, string menu_sort, string menu_ico, string menu_order, string menu_event, string menu_event_args, string menu_desc,string menu_code)
        {
            string sql = String.Format(@"update emr_menu_module set
                                                                            module_code='{0}', mnu_win='{1}', mnu_pbl='{2}', 
                                                                            menu_parent='{3}', menu_sort='{4}',menu_ico='{5}',menu_order='{6}', 
                                                                            menu_event='{7}', menu_event_args='{8}',menu_desc='{9}' where menu_code='{10}'",
                           module_code, mnu_win, mnu_pbl, menu_parent, menu_sort, menu_ico, menu_order, menu_event, menu_event_args, menu_desc, menu_code);
            return BaseEntityer.Db.ExecuteSql(sql);
        }

        public int Add_Emr_module_dict(string code,string name)
        {
            string sql = String.Format("insert into emr_module_dict(module_code,module_name) values('{0}','{1}')", code, name);
            return BaseEntityer.Db.ExecuteSql(sql);
        }

        public int Update_Emr_module_dict(string code, string name)
        {
            string sql = String.Format("update emr_module_dict set module_name='{0}' where module_code='{1}'",name, code);
            return BaseEntityer.Db.ExecuteSql(sql);
        }
        //public int Delete_Emr_module_dict(string code)
        //{
              //ED2927C2-9EC5-4cb4-B6BC-EB843114B00F
        //    string sql = String.Format("insert into emr_module_dict(module_code,module_name) values('{0}','{1}')", code);
        //    return BaseEntityer.Db.ExecuteSql(sql);
        //} 

        public int Add_Emr_menu_group(string menu_group_code, string menu_group_c, string module_code)
        {
            string sql = String.Format("insert into emr_menu_group(menu_group_code,menu_group_c,module_code) values('{0}','{1}','{2}')", menu_group_code, menu_group_c, module_code);
            return BaseEntityer.Db.ExecuteSql(sql);
        }

         public int Update_Emr_menu_group(string menu_group_code, string menu_group_c, string module_code)
         {
             string sql = String.Format("update emr_menu_group set menu_group_c='{1}',module_code='{2}' where menu_group_code='{0}'", menu_group_code, menu_group_c, module_code);
             return BaseEntityer.Db.ExecuteSql(sql);
         }

         public int Delete_Emr_menu_group(string menu_group_code)
         {
             string sql = String.Format("delete from  emr_menu_group  where menu_group_code='{0}'", menu_group_code);
             return BaseEntityer.Db.ExecuteSql(sql);
         }
        #endregion


        #region  流程相关

         public bool Delete_Emr_Ws_Set(string dept_id)
         {

             ArrayList li = new ArrayList();
             string sql = String.Format("delete from  Emr_Ws_Set  where dept_id='{0}'", dept_id);
             li.Add(sql);
             sql = "delete from emr_ws_users where dept_id='" + dept_id + "'";
             li.Add(sql);
             return BaseEntityer.Db.ExecuteSqlTran(li);

            
         }
         public int Add_Emr_Ws_Set(string ws_set_id,string dept_id,string ws_id,string upper_id,string bz,int isleap)
         {



             string sql = String.Format(@"INSERT INTO EMR_WS_SET ( 
                                            WS_SET_ID ,
                                            DEPT_ID ,
                                            WS_ID ,
                                            UPPER_ID ,
                                            BZ ,
                                            ISLEAP ) VALUES ('{0}','{1}','{2}','{3}','{4}',{5})", ws_set_id, dept_id, ws_id, upper_id, bz, isleap);
             return BaseEntityer.Db.ExecuteSql(sql);
         }

         public int Delete_Emr_Ws_Users(string ws_user_id)
         {

             string sql = "delete from emr_ws_users where ws_user_id='" + ws_user_id + "'";
             return BaseEntityer.Db.ExecuteSql(sql);


         }
         public int Add_Emr_Ws_Users(string ws_user_id, string ws_set_id, string user_id, string bz, string groupname, string dept_id)
         {
             string sql = String.Format(@"insert into emr_ws_users (WS_USER_ID, WS_SET_ID, USER_ID, BZ, GROUPNAME, DEPT_ID) 
VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')",  ws_user_id,  ws_set_id,user_id,  bz,  groupname, dept_id);
             return BaseEntityer.Db.ExecuteSql(sql);
         }
         public int Add_Emr_Ws_Oper(string oper_id, string ws_set_id, string patient_id,string zt, string bz, int visit_id, string par_spr,string par_ws_set_id,int setcount,DateTime opertime,string n_ws_name,string d_ws_name,string n_spr,string d_spr)
         {
             string sql = String.Format(@"insert into emr_ws_oper (OPER_ID, WS_SET_ID, PATIENT_ID, ZT, BZ, VISIT_ID, 
                                        PAR_SPR, PAR_WS_SET_ID, SETCOUNT, OPERTIME, N_WS_NAME, D_WS_NAME, N_SPR, D_SPR)
                                        values ('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}' , '{7}', {8}, to_date('{9}', 'yyyy-mm-dd hh24:mi:ss'),
                                        '{10}', '{11}', '{12}', '{13}')", oper_id, ws_set_id,patient_id,zt,bz,visit_id,par_spr,par_ws_set_id,setcount,opertime.ToString("yyyy-MM-dd HH:mm:ss"), n_ws_name,d_ws_name,n_spr,d_spr);
             return BaseEntityer.Db.ExecuteSql(sql);
         }


        #endregion
        #region  通用
        #endregion
        #region  通用
        #endregion

        #region  通用
        #endregion
        #region  通用
        #endregion
        #region  通用
        #endregion
        #region  通用
        #endregion




        #region  通用
        #endregion
        #region  通用
        #endregion
        #region  通用
        #endregion
        #region  通用
        #endregion
        #region  通用
        #endregion

    }
}
