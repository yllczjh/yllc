using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace 实例
{
    public partial class F_心电图显示 : Form
    {
        private string P_病历号;
        private string P_申请单;
        public F_心电图显示()
        {
            InitializeComponent();
            //双缓存
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
        }
        public F_心电图显示(string str_病历号, string str_申请单)
        {
            InitializeComponent();
            this.P_病历号 = str_病历号;
            this.P_申请单 = str_申请单;
        }

        private void btn_关闭_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_保存_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Title = "图片保存";
            saveImageDialog.Filter = @"jpeg|*.jpg|bmp|*.bmp";
            saveImageDialog.FileName = "心电图_" + P_病历号 + "_" + P_申请单;
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveImageDialog.FileName.ToString();
                if (!string.IsNullOrEmpty(fileName))
                {
                    string fileExtName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToString();
                    System.Drawing.Imaging.ImageFormat imgformat = null;
                    if (!string.IsNullOrEmpty(fileExtName))
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

                        pictureBox1.Image.Save(fileName, imgformat);
                    }
                }
            }
        }

        private void F_心电图显示_Load(object sender, EventArgs e)
        {
            //ftp服务器路径
            string ftpServer = "ftp://localhost";
            //ftp本地路径
            string ftpDefaultUrl = "/Upload";
            //登入到ftp的账号
            string ftpUserName = "ftp";
            //登入到ftp的密码
            string ftpUserPwd = "ftp";
            //需要下载的文件名
            string fileName = "111.png";
            //需要现在的文件在ftp上的完整路径
            string fileUploadPath = ftpServer + ftpDefaultUrl;
            Uri uri = new Uri(fileUploadPath + "/" + fileName);

            WebClient web = new WebClient();
            web.Credentials = new NetworkCredential(ftpUserName, ftpUserPwd);
            var bytes = web.DownloadData(fileUploadPath + "/" + fileName);
            //pictureBox1.Image = Bitmap.FromStream(new MemoryStream(bytes));
            M_map_bufferpic = (Bitmap)Bitmap.FromStream(new MemoryStream(bytes));
            pictureBox1.Image = M_map_bufferpic;
        }

        Point M_pot_p = new Point();//原始位置
        int M_int_mx = 0, M_int_my = 0;//下次能继续
        int M_int_maxX, M_int_maxY;//加快读取用
        Bitmap M_map_bufferpic;//加快GDI读取用缓存图片
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            M_pot_p = e.Location;
            M_int_maxX = pictureBox1.Width - M_map_bufferpic.Width;
            M_int_maxY = pictureBox1.Height - M_map_bufferpic.Height;
            Cursor = Cursors.SizeAll;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //wselected = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
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
