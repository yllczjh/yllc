using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 业务管理.数据库
{
    public class StringHelper
    {
        /// <summary>
        /// <summary>
        /// 字符串转Unicode
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unicode编码后的字符串</returns>
        public static string String2Unicode(string source)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Unicode转字符串
        /// </summary>
        /// <param name="source">经过Unicode编码的字符串</param>
        /// <returns>正常字符串</returns>
        public static string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }

        /// <summary>
        /// 将十六进制字符串转换为对应的字节数组，如遇转换失败字符，将视为Unicode字符，转换为对应字节数组
        /// </summary>
        /// <param name="shex">源十六进制字符串</param>
        /// <returns></returns>
        public static byte[] GetByteArray(string shex)
        {
            string[] ssArray = shex.Split(' ');
            List<byte> bytList = new List<byte>();
            foreach (var s in ssArray)
            {
                try
                {
                    bytList.Add(Convert.ToByte(s, 16));
                }
                catch
                {
                    byte[] utf8 = Encoding.GetEncoding("utf-8").GetBytes(s);
                    foreach (byte u in utf8)
                    {
                        bytList.Add(u);
                    }
                }
            }    //返回字节数组          
            return bytList.ToArray();
        }

        /// <summary>
        /// 将Unicode字节数组转换为Unicode字符串
        /// </summary>
        /// <param name="array">源Unicode数组</param>
        /// <returns>字符串</returns>
        public static string BytesToUTF8(byte[] array)
        {
            Encoding utf8 = Encoding.GetEncoding(65001);

            return utf8.GetString(array);
        }

        /// <summary>
        /// GB2312字符串转换至Unicode,其中[Hex]格式串转换为对应Unicode字符(串)
        /// </summary>
        /// <param name="GB2312"></param>
        /// <returns></returns>
        public static string GB2312ToUTF8(string GB2312)
        {
            var cha = Regex.Matches(GB2312, "～\\[[\\d a-fA-F]+?\\]＄");
            for (int i = cha.Count - 1; i >= 0; i--)
            {
                GB2312 = GB2312.Replace(cha[i].Value, BytesToUTF8(GetByteArray(cha[i].Value.Replace("～[", "").Replace("]＄", ""))));
            }
            return GB2312;
        }

        /// <summary>
        /// unicode转GB2312，对于不兼容GB2312的字符以[十六进制]形式拼接
        /// </summary>
        /// <param name="source">源Unicode字符串</param>
        /// <returns>GB2312字符串</returns>
        public static string UTF8ToGB2312(string source)
        {
            char[] ch = source.ToCharArray();

            StringBuilder sb = new StringBuilder();
            foreach (char c in ch)
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] tmpGb2312 = gb2312.GetBytes(c.ToString());

                if (gb2312.GetString(tmpGb2312) == c.ToString())
                    sb.Append(c);
                else
                {
                    sb.Append("～[" + GetHexFromChs(c.ToString()) + "]＄");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 从汉字转换到16进制
        /// </summary>
        /// <param name="s">需要转换的字符串</param>
        /// <returns>返回16进制</returns>
        public static string GetHexFromChs(string s)
        {
            Encoding chs = Encoding.GetEncoding("utf-8");
            byte[] bytes = chs.GetBytes(s);
            StringBuilder Text = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                Text.Append(string.Format("{0:X}", bytes[i]));
                Text.Append(" ");
            }

            string ggg = Text.ToString();
            string ggf = ggg.Substring(ggg.Length - 1, 1);
            if (ggf == " ")
            {
                ggg = ggg.Remove(ggg.Length - 1, 1);
            }
            return ggg;
        }
    }
}
