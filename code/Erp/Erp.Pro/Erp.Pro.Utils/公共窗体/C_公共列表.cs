using System;

namespace Erp.Pro.Utils.公共窗体
{
    public class C_公共列表
    {
        /// <summary>
        /// 调用并执行指定类里面的函数
        /// </summary>
        /// <param name="className">需要调用的类名(包含其命名空间)</param>
        /// <param name="methodName">需要调用的方法名</param>
        /// <param name="parameters">传递的参数值</param>
        public void GetAndExecuteMethod(string className, string methodName, object[] parameters = null)
        {
            try
            {
                var type = Type.GetType(className);
                if (type == null)
                    throw new NullReferenceException("类" + className + "不存在");

                var obj = type.Assembly.CreateInstance(className);
                //调用其方法
                var method = type.GetMethod(methodName);
                if (method == null)
                    throw new NullReferenceException("方法" + methodName + "不存在");

                //执行方法
                method.Invoke(obj, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
