using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;
using O2S.Components.PDFRender4NET;
using System.Linq;

namespace 实例
{
    public partial class F_心电图显示新 : Form
    {
        public F_心电图显示新()
        {
            InitializeComponent();
        }
        Bitmap M_map_bufferpic;//加快GDI读取用缓存图片
        private void F_心电图显示新_Load(object sender, EventArgs e)
        {
            txt_ftp地址.Text = "ftp://localhost";
            txt_账号.Text = "ftp";
            txt_密码.Text = "ftp";
            txt_文件名.Text = "111.png";

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ftpServer = txt_ftp地址.Text;
            //登入到ftp的账号
            string ftpUserName = txt_账号.Text;
            //登入到ftp的密码
            string ftpUserPwd = txt_密码.Text;
            //需要下载的文件名
            string fileName = txt_文件名.Text;

            WebClient web = new WebClient();
            web.Credentials = new NetworkCredential(ftpUserName, ftpUserPwd);
            var bytes = web.DownloadData(ftpServer + "/" + fileName);
            M_map_bufferpic = (Bitmap)Bitmap.FromStream(new MemoryStream(bytes));
            pictureBox1.Image = M_map_bufferpic;
        }
        //图片的清晰度，数字越大越清晰
      

        private void button2_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("ftp://localhost/Upload/111.pdf");
            System.Diagnostics.Process.Start(textBox1.Text);
            //using (WebClient client = new WebClient())
            //{
            //    client.Credentials = new NetworkCredential();
            //    client.DownloadFile("ftp://localhost/Upload/111.pdf", @"E:\Download\111.pdf");

            //}


            var now = DateTime.Now;
            foreach (var f in Directory.GetFileSystemEntries(Directory.GetCurrentDirectory() + @"\ecg\").Where(f => File.Exists(f)))
            {
                var t = File.GetCreationTime(f);
              
                var elapsedTicks = now.Ticks - t.Ticks;
                var elaspsedSpan = new TimeSpan(elapsedTicks);
                if (elaspsedSpan.TotalDays > 0)
                {
                    File.Delete(f);
                }
            }

        }
        ///// <summary>
        ///// 下载文件
        ///// </summary>
        ///// <param name="filePath">本地目录</param>
        ///// <param name="fileName">远程路径</param>
        //public void DownloadFile(string filePath, string fileName)
        //{
        //    FtpWebRequest reqFTP;
        //    try
        //    {
        //        FileStream fileStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

        //        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + RemoteHost + "/" + fileName));
        //        reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
        //        reqFTP.UseBinary = true;
        //        reqFTP.Credentials = new NetworkCredential(RemoteUser, RemotePass);

        //        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
        //        Stream responseStream = response.GetResponseStream();//从ftp响应中获得响应流

        //        //long cl = response.ContentLength;
        //        byte[] buffer = new byte[1024];
        //        int readCount;

        //        readCount = responseStream.Read(buffer, 0, buffer.Length);//从ftp的responseStream读取数据到buffer中
        //        while (readCount > 0)
        //        {
        //            fileStream.Write(buffer, 0, readCount);//从buffer读取数据到fileStream中，完成下载
        //            readCount = responseStream.Read(buffer, 0, buffer.Length);
        //        }

        //        responseStream.Close();
        //        fileStream.Close();
        //        response.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

    }

}
