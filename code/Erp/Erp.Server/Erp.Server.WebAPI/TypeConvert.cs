using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Text;

namespace Erp.Server.WebAPI
{
    public class TypeConvert
    {
        /// <summary>
        /// dataset(不含明细)节点转为DatTable
        /// </summary>
        /// <param name="Object">返回的JObject</param>
        /// <returns></returns>
        public static DataTable JObjectToDataTable(JObject Object)
        {
            try
            {
                JArray Array = (JArray)Object.GetValue("dataset");
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(Array.ToString());
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 将DataTable转为插入用的JSON串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static JArray DataTableToJArray(DataTable dt)
        {
            try
            {
                string str = JsonConvert.SerializeObject(dt);
                JArray Array = (JArray)JsonConvert.DeserializeObject(str);
                return Array;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// DataTable转为insert或update语句
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToSQL(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                StringBuilder sb1 = new StringBuilder();
                if (null == row["rowid"])
                {

                }else
                {
                    foreach(DataColumn column in dt.Columns)
                    {

                    }
                }
            }
            return null;
        }
    }
}
