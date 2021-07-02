using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using Tool.Model;

namespace Tool.Help
{
    public class Helper
    {
        /// <summary>
        /// Dictionary转化为JObject
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static JObject DictionaryToJObject(Dictionary<string, object> param)
        {
            try
            {
                string str = JsonConvert.SerializeObject(param);
                JObject ob = (JObject)JsonConvert.DeserializeObject(str);
                return ob;
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
        /// Dynamic转化为Dictionary
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Dictionary<string, object> DynamicToDictionary(dynamic param)
        {
            try
            {
                string str_json = JsonConvert.SerializeObject(param);

                //实例化JavaScriptSerializer类的新实例
                JavaScriptSerializer jss = new JavaScriptSerializer();
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, object>>(str_json);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 主记录+明细 转为JSON
        /// </summary>
        /// <param name="row">主记录row</param>
        /// <param name="dt">明细记录</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToJson(DataRow row, DataTable dt)
        {
            Dictionary<string, object> m_values = new Dictionary<string, object>();
            DataTable d = row.Table;
            for (int k = 0; k < d.Columns.Count; k++)
            {
                string columnName = d.Columns[k].ColumnName.ToString();
                m_values.Add(columnName, row[columnName].ToString());
            }
            m_values.Add("datadetail", dt);
            return m_values;
        }
        public static Dictionary<string, object> JsonToDictionary(string jsonData, ref MessageModel msg)
        {
            //实例化JavaScriptSerializer类的新实例
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, object>> (jsonData);
            }
            catch (Exception ex)
            {
                Code.Result(ref msg, 编码.参数错误, ex.Message);
                return null;
            }
        }
    }
}
