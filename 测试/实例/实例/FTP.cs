using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 实例
{
    public partial class FTP : Form
    {
        public FTP()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ftp方式上传 
        /// </summary>
        public static int UploadFtp(string filePath, string filename, string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            FileInfo fileInf = new FileInfo(filePath + "\\" + filename);
            string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileInf.Name));
            try
            {
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                reqFTP.ContentLength = fileInf.Length;
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;

                FileStream fs = fileInf.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);

                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                strm.Close();
                fs.Close();
                return 0;
            }
            catch (Exception ex)
            {
                reqFTP.Abort();
                return -2;
            }
        }

        ///// <summary>
        ///// ftp方式下载 
        ///// </summary>
        //public static int DownloadFtp(string filePath, string fileName, string ftpServerIP, string ftpUserID, string ftpPassword)
        //{
        //    FtpWebRequest reqFTP;
        //    try
        //    {
        //        FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

        //        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));
        //        reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
        //        reqFTP.UseBinary = true;
        //        reqFTP.KeepAlive = false;
        //        reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
        //        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
        //        Stream ftpStream = response.GetResponseStream();
        //        long cl = response.ContentLength;
        //        int bufferSize = 2048;
        //        int readCount;
        //        byte[] buffer = new byte[bufferSize];

        //        readCount = ftpStream.Read(buffer, 0, bufferSize);
        //        while (readCount > 0)
        //        {
        //            outputStream.Write(buffer, 0, readCount);
        //            readCount = ftpStream.Read(buffer, 0, bufferSize);
        //        }

        //        ftpStream.Close();
        //        outputStream.Close();
        //        response.Close();
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        return -2;
        //    }
        //}

        /// <summary>
        /// ftp方式下载 
        /// </summary>
        public static int DownloadFtp(string filePath, string fileName, string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            FtpWebRequest reqFTP;

            //创建一个文件流
            FileStream fs = null;
            Stream responseStream = null;
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.KeepAlive = false;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                responseStream = response.GetResponseStream();
                //判断本地文件是否存在，如果存在，则打开和重写本地文件
                if (File.Exists(filePath + "\\aaa\\"+ fileName))
                {
                    //if (LocalFileExistsOperation == "write")
                    //{
                        fs = File.Open(filePath + "\\aaa\\"+ fileName, FileMode.Open, FileAccess.ReadWrite);
                    //}
                }
                //判断本地文件是否存在，如果不存在，则创建本地文件
                else
                {
                    fs = File.Create(filePath + "\\aaa\\"+ fileName);
                }
                if (fs != null)
                {
                    int buffer_count = 65536;
                    byte[] buffer = new byte[buffer_count];
                    int size = 0;
                    while ((size = responseStream.Read(buffer, 0, buffer_count)) > 0)
                    {
                        fs.Write(buffer, 0, size);
                    }
                    fs.Flush();
                    fs.Close();
                    responseStream.Close();
                }
                return 0;

            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DownloadFtp("D:\\Work\\ftp", "111.png","localhost", "ftp", "ftp");

            //ftp服务器路径
            string ftpServer = "ftp://localhost";
            //ftp本地路径
            string ftpDefaultUrl = "/Upload";
            //登入到ftp的账号
            string ftpUserName = "ftp";
            //登入到ftp的密码
            string ftpUserPwd = "ftp";
            //下载后的文件存放路径
            //string downloadUrl = "/Download";
            //需要下载的文件名
            string fileName = "111.png";
            //需要现在的文件在ftp上的完整路径
            string fileUploadPath = ftpServer + ftpDefaultUrl;
            Uri uri = new Uri(fileUploadPath + "/" + fileName);
            //下载后存放的路径
            string FileName = string.Empty;
            //string FileName = Path.GetFullPath(downloadUrl) + Path.DirectorySeparatorChar.ToString() + Path.GetFileName(uri.LocalPath);

            //创建文件流
            FileStream fs = null;
            Stream responseStream = null;
            try
            {
                //创建一个与FTP服务器联系的FtpWebRequest对象
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                //设置请求的方法是FTP文件下载
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                //连接登录FTP服务器
                request.Credentials = new NetworkCredential(ftpUserName, ftpUserPwd);
                //获取一个请求响应对象
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //获取请求的响应流
                responseStream = response.GetResponseStream();


                FolderBrowserDialog sfd = new FolderBrowserDialog();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //获取保存路径url
                    FileName = sfd.SelectedPath + Path.GetFileName(uri.LocalPath);
                    //判断本地文件是否存在，如果存在，则打开和重写本地文件
                    if (File.Exists(FileName))
                    {
                        fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite);
                    }
                    else
                    {
                        fs = File.Create(FileName);
                    }

                    if (fs != null)
                    {
                        int buffer_count = 65536;
                        byte[] buffer = new byte[buffer_count];
                        int size = 0;
                        while ((size = responseStream.Read(buffer, 0, buffer_count)) > 0)
                        {
                            fs.Write(buffer, 0, size);
                        }
                        fs.Flush();
                        fs.Close();
                        responseStream.Close();
                    }
                    this.pictureBox1.Image = Image.FromFile(FileName);
                }
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                if (responseStream != null)
                    responseStream.Close();
            }
        }
    }
}
