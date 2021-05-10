using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

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
    }
}
