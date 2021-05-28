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

            //pictureEdit1.Image = new Bitmap((new System.Net.WebClient()).OpenRead(uri));


            WebClient web = new WebClient();
            web.Credentials = new NetworkCredential("ftp", "ftp");
            var bytes = web.DownloadData(fileUploadPath + "/" + fileName);
            //pictureEdit1.Image = Bitmap.FromStream(new MemoryStream(bytes));
            pictureBox1.Image = Bitmap.FromStream(new MemoryStream(bytes));

            M_map_bufferpic =(Bitmap) Bitmap.FromStream(new MemoryStream(bytes));
            pictureBox1.Image = M_map_bufferpic;


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
                    //this.pictureBox1.Image = Image.FromFile(FileName);
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
            //e.Graphics.DrawImage(pictureBox1.Image, 20, 20);
        }


        private void FTP_Load(object sender, EventArgs e)
        {
            
        }

      


       

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Title = "图片保存";
            saveImageDialog.Filter = @"jpeg|*.jpg|bmp|*.bmp";
            saveImageDialog.FileName = "1212";
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveImageDialog.FileName.ToString();
                if (fileName != "" && fileName != null)
                {
                    string fileExtName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToString();
                    System.Drawing.Imaging.ImageFormat imgformat = null;

                    if (fileExtName != "")
                    {
                        switch (fileExtName)
                        {
                            case "jpg":
                                imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                            case "bmp":
                                imgformat = System.Drawing.Imaging.ImageFormat.Bmp;
                                break;
                            default:
                                imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                        }


                        try
                        {
                            pictureBox1.Image.Save(fileName, imgformat);
                        }
                        catch
                        {


                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            F_心电图显示 f = new F_心电图显示("1212","23234");
            f.ShowDialog();
            f.Dispose();
        }


        Point M_pot_p = new Point();//原始位置
        int M_int_mx = 0, M_int_my = 0;//下次能继续
        int M_int_maxX, M_int_maxY;//加快读取用
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            M_pot_p = e.Location;
            M_int_maxX = pictureBox1.Width - M_map_bufferpic.Width;
            M_int_maxY = pictureBox1.Height - M_map_bufferpic.Height;
            Cursor = Cursors.SizeAll;
        }

        int driftX = 0, driftY = 0;
        int mx = 0, my = 0;

        private void button5_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            string[] ss = s.Split(',');
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            
        }
        Bitmap M_map_bufferpic;//加快GDI读取用缓存图片
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//当按左键的时候
            {
                //算差值
                M_int_mx = M_int_mx - M_pot_p.X + e.X;
                M_int_my = M_int_my - M_pot_p.Y + e.Y;
                //锁定范围
                M_int_mx = Math.Min(0, Math.Max(M_int_maxX, M_int_mx));
                M_int_my = Math.Min(0, Math.Max(M_int_maxY, M_int_my));

                Graphics g = pictureBox1.CreateGraphics();
                g.DrawImage(M_map_bufferpic, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height), new Rectangle(-M_int_mx, -M_int_my, pictureBox1.Width, pictureBox1.Height), GraphicsUnit.Pixel);

                M_pot_p = e.Location;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
