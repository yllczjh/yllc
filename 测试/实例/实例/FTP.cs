using System;
using System.Drawing;
using System.IO;
using System.Net;
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

            pictureEdit1.Image = new Bitmap((new System.Net.WebClient()).OpenRead(uri));

            DevExpress.XtraEditors.VScrollBar vScrl = null;
            DevExpress.XtraEditors.HScrollBar hScrl = null;
            // 查找水平和垂直滚动条
            foreach (Control ctrl in pictureEdit1.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.VScrollBar)
                    vScrl = ctrl as DevExpress.XtraEditors.VScrollBar;
                if (ctrl is DevExpress.XtraEditors.HScrollBar)
                    hScrl = ctrl as DevExpress.XtraEditors.HScrollBar;
            }
            // 设置滚动条值，让图片中心显示在PictureEdit中心！
            //vScrl.Value = (pictureEdit1.Image.Height - pictureEdit1.ClientSize.Height) / 2;
            //hScrl.Value = (pictureEdit1.Image.Width - pictureEdit1.ClientSize.Width) / 2;
            hScrl.Visible = true;
            vScrl.Visible = true;
            vScrl.Location = new Point(pictureEdit1.Width - vScrl.Width, 0);
            hScrl.Location = new Point(0, pictureEdit1.Height - hScrl.Height);
            hScrl.Width = pictureEdit1.Width;
            vScrl.Height = pictureEdit1.Height;


            return;
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

                    //PrintDialog MyPrintDg = new PrintDialog();
                    //MyPrintDg.Document = printDocument1;
                    //if (MyPrintDg.ShowDialog() == DialogResult.OK)
                    //{
                    //    try
                    //    {
                    //        printDocument1.Print();
                    //    }
                    //    catch
                    //    {   // 停止打印
                    //        printDocument1.PrintController.OnEndPrint(printDocument1, new System.Drawing.Printing.PrintEventArgs());
                    //    }
                    //}
                    //pictureEdit1.Image = new Bitmap((new System.Net.WebClient()).OpenRead(uri));
                    //pictureEdit1.Image = Image.FromFile(FileName);
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

        private void button2_Click(object sender, EventArgs e)
        {
            UploadFtp("E:\\", "111.png", "localhost", "ftp", "ftp");
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pictureBox1.Image, 20, 20);
        }


        private void FTP_Load(object sender, EventArgs e)
        {
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.AllowScrollViaMouseDrag = true;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
            this.pictureEdit1.Properties.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureEdit1_Properties_MouseWheel);
            this.pictureEdit1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureEdit1_MouseClick);
            this.pictureEdit1.AllowDrop = true;
        }

        private void pictureEdit1_Properties_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.pictureEdit1.Properties.ZoomPercent += 5;
            }
            else if (e.Delta < 0)
            {
                this.pictureEdit1.Properties.ZoomPercent -= 5;
            }
        }
        private void pictureEdit1_MouseClick(object sender, MouseEventArgs e)
        {
            return;
            Point p = e.Location;

            Point center=new Point();
            center.X = pictureEdit1.ClientSize.Width / 2;
            center.Y = pictureEdit1.ClientSize.Height / 2;


            DevExpress.XtraEditors.VScrollBar vScrl = null;
            DevExpress.XtraEditors.HScrollBar hScrl = null;

            foreach (Control ctrl in pictureEdit1.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.VScrollBar)
                    vScrl = ctrl as DevExpress.XtraEditors.VScrollBar;
                if (ctrl is DevExpress.XtraEditors.HScrollBar)
                    hScrl = ctrl as DevExpress.XtraEditors.HScrollBar;
            }

            p.X += hScrl.Value;
            p.Y += vScrl.Value;

            int deltaX = p.X - center.X;
            int deltaY = p.Y - center.Y;

            hScrl.Value = deltaX;
            vScrl.Value = deltaY;
            //hScrl.Height = pictureEdit1.Height;
            //hScrl.Width = pictureEdit1.Width;
            hScrl.Visible = true;
            vScrl.Visible = true;
        }
    }
}
