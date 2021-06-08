using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraEditors;

namespace Erp.Pro.Utils.工具类
{
    public class FileHelper
    {
        private const string DELIMITER_BACKSLASH = @"\";
        private const string str_反斜杠 = @"/";
        private const string str_默认配置文件名 = "Erp.Main.exe.config";
        private const string str_EMPI_文件 = @"F:\realtimeConfig.properties";

        ////声明读写INI文件的API函数 
        [DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string
            key, string val, string filePath);

        //		Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long

        [DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string
            key, string def, StringBuilder retVal, int size, string filePath);


        /// <summary>
        /// 在初始化文件指定小节内设置一个字串
        /// </summary>
        /// <param name="filepath">ini文件路径</param>
        /// <param name="Section">小节名称</param>
        /// <param name="Key">具体项目名称</param>
        /// <param name="Value">指定为这个项写入的字串值</param>
        public void IniWriteValue(String filepath, string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, filepath);

        }

        /// <summary>
        /// 读取INI文件指定的条目取得字串
        /// </summary>
        /// <param name="filepath">ini文件路径</param>
        /// <param name="Section">小节名称</param>
        /// <param name="Key">具体项目名称</param>
        /// <returns>为初始化文件中指定的条目取得字串</returns>
        public string IniReadValue(String filepath, string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, filepath);
            return temp.ToString();

        }

        public static string Append_反斜杠(string path)
        {
            if (path.IndexOf(str_反斜杠, path.Length - 1) == -1)
            {
                return path + str_反斜杠;
            }
            else
            {
                return path;
            }
        }

        public static string AppendTerminalBackSlash(string path)
        {
            if (path.IndexOf(DELIMITER_BACKSLASH, path.Length - 1) == -1)
            {
                return path + DELIMITER_BACKSLASH;
            }
            else
            {
                return path;
            }
        }
        public static void CreateFile(string path, string filestr)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(filestr);
            writer.Flush();
            writer.Close();
            stream.Close();
        }
        public static string GetTemplete(string path)
        {
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("GB2312"));
                string r = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                return r;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Silverlight写日志文件
        /// </summary>
        /// <param name="filestr"></param>
        public static void WriteBakLog_SL(string filestr)
        {
            string str_日志文件 = Application.StartupPath + "\\SL_Log.txt";
            WriteBakLog(str_日志文件, filestr);
        }

        public static void WriteBakLog(string path, string filestr)
        {
            FileStream fs = new FileStream(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
            m_streamWriter.WriteLine(filestr);
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();
        }

        /// <summary>
        /// 调用文本文件
        /// </summary>
        /// <param name="FileName"></param>
        public static void OpenTxtFile(string FileName)
        {
            string sPath = AppendTerminalBackSlash(System.AppDomain.CurrentDomain.BaseDirectory);
            Process myProcess = new Process();
            myProcess.StartInfo.FileName = sPath + @"DataPath\" + FileName;
            myProcess.StartInfo.CreateNoWindow = true;
            //全屏打开
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            myProcess.Start();
        }
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="FileName"></param>
        public static void OpenFile(string FileName)
        {
            if (File.Exists(FileName))
            {
                string sPath = AppendTerminalBackSlash(System.AppDomain.CurrentDomain.BaseDirectory);
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = FileName;
                //myProcess.StartInfo.FileName = @"D:\Program Files\Microsoft Office\OFFICE11\WINWORD.EXE";
                //myProcess.StartInfo.Arguments = sPath + @"\DataPath\" + FileName;
                myProcess.StartInfo.CreateNoWindow = true;
                //全屏打开
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                myProcess.Start();
            }
            else
            {
                XtraMessageBox.Show("文件不存在！");
            }
        }

        #region 文件 文件夹增删操作
        public static void DeleteFile(string sDestFile)
        {
            try
            {
                if (File.Exists(sDestFile))
                    File.Delete(sDestFile);
                string sTPPath = sDestFile.Replace(Path.GetFileName(sDestFile), "");
                DirectoryInfo dir = new DirectoryInfo(sTPPath);
                if (dir.GetFiles().Length == 0)
                    Directory.Delete(sTPPath);
            }
            catch
            { }
        }

        public static void DeletePathAllFile(string sDestPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sDestPath);
                foreach (FileInfo file in dir.GetFiles())
                {
                    File.Delete(file.FullName);
                }
            }
            catch
            { }
        }

        public static string GetChartTempDirectory()
        {
            string ChartPath = AppendTerminalBackSlash(System.AppDomain.CurrentDomain.BaseDirectory) + @"ChartPath";
            //文件夹不存在,则创建
            if (!Directory.Exists(ChartPath))
                Directory.CreateDirectory(ChartPath);
            //文件夹下面有临时文件,则删除
            DeletePathAllFile(ChartPath);
            return ChartPath;
        }
        #endregion

        #region 操作appconfig文件
        public static string GetAppConfigFile()
        {
            return GetAppConfigFile(str_默认配置文件名);
        }
        public static string GetAppConfigFile(string str_配置文件名)
        {
            return System.IO.Path.Combine(Application.StartupPath, str_配置文件名);//此处配置文件在程序目录下
        }

        public static string GetConfigValue(string appKey)
        {
            return GetConfigValue(str_默认配置文件名, appKey);
        }

        public static string GetConfigValue(string str_配置文件名, string appKey)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(GetAppConfigFile(str_配置文件名));
                XmlNode xNode;
                XmlElement xElem;
                xNode = xDoc.SelectSingleNode("//appSettings");　　　　//补充，需要在你的app.config 文件中增加一下，<appSetting> </appSetting>
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
                if (xElem != null)
                    return xElem.GetAttribute("value");
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static void SetConfigValue(string AppKey, string AppValue)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(GetAppConfigFile());
            XmlNode xNode;
            XmlElement xElem1;
            XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null)
            {
                xElem1.SetAttribute("value", AppValue);
            }
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", AppKey);
                xElem2.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(GetAppConfigFile());
        }
        #region 获取Config中配置的IP地址
        //public static List<ListItem> GetConfigWebUrlList()
        //{
        //    return GetConfigWebUrlList("WebUrl.xml");
        //}

        //public static List<ListItem> GetConfigWebUrlList(string str_配置文件名)
        //{
        //    List<ListItem> lst_WebUrl = new List<ListItem>();
        //    try
        //    {
        //        XmlDocument xDoc = new XmlDocument();
        //        xDoc.Load(GetAppConfigFile(str_配置文件名));
        //        XmlNode xNode;
        //        xNode = xDoc.SelectSingleNode("//WebUrl");
        //        if (xNode != null)
        //        {
        //            foreach (XmlNode xchild_Node in xNode.ChildNodes)
        //            {
        //                ListItem item_node = new ListItem(
        //                    Get_AttributeValue(xchild_Node, "AilsName"),
        //                    Get_AttributeValue(xchild_Node, "text"),
        //                    Get_AttributeValue(xchild_Node, "value"),
        //                    "");
        //                lst_WebUrl.Add(item_node);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return lst_WebUrl;
        //}

        //public static string GetConfigWebUrl(string str_AilsName)
        //{
        //    return GetConfigWebUrl("WebUrl.xml", str_AilsName);
        //}

        //public static string GetConfigWebUrl(string str_配置文件名, string str_AilsName)
        //{
        //    try
        //    {
        //        XmlDocument xDoc = new XmlDocument();
        //        xDoc.Load(GetAppConfigFile(str_配置文件名));
        //        XmlNode xNode;
        //        xNode = xDoc.SelectSingleNode("//WebUrl");
        //        if (xNode != null)
        //        {
        //            foreach (XmlNode xchild_Node in xNode.ChildNodes)
        //            {
        //                if (Get_AttributeValue(xchild_Node, "AilsName") == str_AilsName)
        //                    return Get_AttributeValue(xchild_Node, "value");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return "";
        //}

        //private static string Get_AttributeValue(XmlNode x_Node, string str_属性名)
        //{
        //    string str_属性值 = "";
        //    if (x_Node.Attributes[str_属性名] != null)
        //        str_属性值 = x_Node.Attributes[str_属性名].InnerText;
        //    return str_属性值;
        //}
        #endregion

        #endregion

        /// <summary>
        /// 更新门诊EMPI配置文件内容
        /// </summary>
        public static void SaveEMPIConfigFile_门诊()
        {
            SaveEMPIConfigFile("1");
        }
        /// <summary>
        /// 更新住院EMPI配置文件内容
        /// </summary>
        public static void SaveEMPIConfigFile_住院()
        {
            SaveEMPIConfigFile("2");
        }
        /// <summary>
        /// 更新健康档案EMPI配置文件内容
        /// </summary>
        public static void SaveEMPIConfigFile_健康档案()
        {
            SaveEMPIConfigFile("3");
        }
        /// <summary>
        /// 更新EMPI配置文件内容
        /// </summary>
        /// <param name="str_EmpiType"></param>
        public static void SaveEMPIConfigFile(string str_EmpiType)
        {
            if (!File.Exists(str_EMPI_文件))
                return;

            FileStream stream = null;
            try
            {
                string str_Content = "";
                using (StreamReader sr = new StreamReader(str_EMPI_文件, Encoding.GetEncoding("GB2312")))
                {
                    str_Content = sr.ReadToEnd();
                    switch (str_EmpiType)
                    {
                        case "1"://门诊
                            str_Content = str_Content.Replace("outpatientFlag=0", "outpatientFlag=1");
                            break;
                        case "2"://住院
                            str_Content = str_Content.Replace("inhospatientFlag=0", "inhospatientFlag=1");
                            break;
                        case "3"://健康档案
                            str_Content = str_Content.Replace("pubhealthBasicInfoFlag=0", "pubhealthBasicInfoFlag=1");
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(str_Content))
                {
                    stream = new FileStream(str_EMPI_文件, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    StreamWriter m_streamWriter = new StreamWriter(stream);
                    m_streamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
                    m_streamWriter.WriteLine(str_Content);
                    m_streamWriter.Flush();
                    m_streamWriter.Close();
                    stream.Close();
                }
            }
            catch
            {
                if (stream != null)
                    stream.Close();
            }
        }

        /// <summary>
        /// 将字符串转换为byte数组
        /// </summary>
        /// <param name="type">文档 or 控件</param>
        /// <param name="str">目标字符串</param>
        /// <returns></returns>
        public static byte[] StringToByteAray(string type, string str)
        {

            if (type == "文档")
            {
                return Convert.FromBase64String(str);
            }
            else if (type == "UTF8")
            {
                return System.Text.Encoding.UTF8.GetBytes(str);
            }
            else
            {
                return System.Text.Encoding.GetEncoding("gb2312").GetBytes(str);
            }
        }

        public static string ByteArrayToString(string type, byte[] bt)
        {
            if (type == "文档")
            {
                return Convert.ToBase64String(bt);
            }
            else if (type == "UTF8")
            {
                return System.Text.Encoding.UTF8.GetString(bt);
            }
            else
            {
                return System.Text.Encoding.GetEncoding("gb2312").GetString(bt);
            }
        }

        /// <summary>
        /// 获取指定路径下的指定类型的文件列表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="extName"></param>
        /// <returns></returns>
        public static List<FileInfo> GetFiles(string path, string extName)
        {
            try
            {
                List<FileInfo> lst = new List<FileInfo>();
                string[] dir = Directory.GetDirectories(path); //文件夹列表   
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles();
                //FileInfo[] file = Directory.GetFiles(path); //文件列表   
                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
                {
                    foreach (FileInfo f in file) //显示当前目录所有文件   
                    {
                        if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                        {
                            lst.Add(f);
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断本地是否存在指定文件
        /// </summary>
        /// <param name="FullName">带路径文件名</param>
        /// <returns></returns>
        public static bool ExistsFile(String FullName)
        {
            return File.Exists(FullName);
        }
    }
}
