

using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 实例
{
    public partial class F_匹配地址 : Form
    {
        public F_匹配地址()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "浙江市西湖区塘苗路18号华星现代产业园";
            Match m = Regex.Match(str, @"(.*?(?:省|区|市))(.*?市|.*?州|.*?区)(.*?(?:区|县|市))(.*)");
            Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}",
                m.Groups[1].Value.Trim(),
                m.Groups[2].Value.Trim(),
                m.Groups[3].Value.Trim(),
                m.Groups[4].Value.Trim()
                ));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string className = "TestClass";
            //方法名
            string MethodName = "TestMethod";
            ////函数体
            //string expressionText = "return Math.Pow(x,2);";
            ////C#代码字符串
            //string txt = string.Format(@"using System;sealed class {0}{{public double {1} (double x){{{2}}}}}", className, MethodName, expressionText);
            //函数体
            string expressionText = "return Guid.NewGuid().ToString();";
            //C#代码字符串
            string txt = string.Format(@"using System;sealed class {0}{{public string {1} (double x){{{2}}}}}", className, MethodName, expressionText);
            //构造一个CSharp代码生成器
            CSharpCodeProvider provider = new CSharpCodeProvider();
            //构造一个编译器参数设置
            CompilerParameters para = new CompilerParameters();
            /*
             //para可以需要按照实际要求进行设置，比如添加程序集引用，如下
               para.ReferencedAssemblies.Add("System.dll");
             */

            //编译到程序集,有重载，也可以从文件加载，传入参数为文件路径字符串数组
            var rst = provider.CompileAssemblyFromSource(para, new string[] { txt });

            //判断是否有编译错误
            if (rst.Errors.Count > 0)
            {
                foreach (CompilerError item in rst.Errors)
                {
                    Console.WriteLine(item.Line + ":" + item.ErrorText);
                }
                return;
            }

            //获取程序集
            var assemble = rst.CompiledAssembly;

            //通过反射获取类类型
            Type t = assemble.GetType(className);

            //通过类型构造实例
            var instance = Activator.CreateInstance(t);

            //通过反射获取类型方法
            MethodInfo method = t.GetMethod(MethodName);

            //调用实例上的方法
            var val = method.Invoke(instance, new object[] { 4 });

            Console.WriteLine(val);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetAndExecuteMethod("实例.C_公共类", "aaa");
        }

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
                var val = method.Invoke(obj, parameters);
                Console.WriteLine(val);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
