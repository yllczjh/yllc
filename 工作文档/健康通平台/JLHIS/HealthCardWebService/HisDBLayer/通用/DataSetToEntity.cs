using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace HisDBLayer
{
    public static class DataSetToEntity
    {
        public static object ChangeType(this object value, Type conversionType)
        {

            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {

                if (value != null)
                {

                    NullableConverter nullableConverter = new NullableConverter(conversionType);

                    conversionType = nullableConverter.UnderlyingType;

                }

                else
                {

                    return null;

                }

            }



            return Convert.ChangeType(value, conversionType);

        }

        /// <summary>
        /// DataSet转换为实体类
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="p_DataSet">DataSet</param>
        /// <param name="p_TableIndex">待转换数据表索引</param>
        /// <returns>实体类</returns>
        public static IList<T> DataSetToT<T>(DataSet p_DataSet, int p_TableIndex)
        {
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return new List<T>();
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return new List<T>();
            if (p_TableIndex < 0)
                p_TableIndex = 0;
            if (p_DataSet.Tables[p_TableIndex].Rows.Count <= 0)
                return new List<T>();

            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化
            IList<T> result = new List<T>();
            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        try
                        {
                            // 属性与字段名称一致的进行赋值
                            if (pi.Name.ToLower().Equals(p_Data.Columns[i].ColumnName.ToLower()))
                            {
                                // 数据库NULL值单独处理
                                if (p_Data.Rows[j][i] != DBNull.Value)
                                {
                                    pi.SetValue(_t, p_Data.Rows[j][i].ChangeType(pi.PropertyType), null);
                                }
                                else
                                {
                                    pi.SetValue(_t, null, null);
                                }
                                break;
                            }
                        }
                        catch
                        {
                            Type type = p_Data.Rows[j][i].GetType();
                            object value = p_Data.Rows[j][i];
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }

        /// <summary>
        /// DataSet转换为实体类-默认第一个datatable
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="p_DataSet">DataSet</param>
        /// <returns>实体类</returns>
        public static IList<T> DataSetToT<T>(DataSet p_DataSet)
        {
            return DataSetToT<T>(p_DataSet, 0);
        }

    }
}
