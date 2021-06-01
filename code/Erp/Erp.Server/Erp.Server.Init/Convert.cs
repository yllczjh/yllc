using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace Erp.Server.Init
{
    public class Convert
    {

        public static DataTable JArrayToDataTable(JArray Array)
        {
            try
            {
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
    }
}
