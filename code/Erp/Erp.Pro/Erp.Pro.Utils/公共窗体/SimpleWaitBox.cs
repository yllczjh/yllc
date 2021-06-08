using System;
using System.Drawing;
using System.Windows.Forms;

namespace Erp.Pro.Utils.公告窗体
{
    public partial class SimpleWaitBox : Form
    {
        public string lable = "";
        public SimpleWaitBox()
        {
            InitializeComponent();
        }

        public SimpleWaitBox(string sHint)
        {
            InitializeComponent();

            circularProgress1.IsRunning = true;
            labHint.Text = sHint;
            lable = sHint;

            this.Show();
            this.Refresh();
        }

        public void SetHint(string sHint)
        {
            labHint.Text = sHint;
            lable = sHint;
        }

        private void frmWaitDialog_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(207, 221, 238);
            //loadingCircle1.NumberSpoke = 12;
            //loadingCircle1.SpokeThickness = 2;
            //loadingCircle1.InnerCircleRadius = 5;
            //loadingCircle1.OuterCircleRadius = 20;
            //loadingCircle1.RotationSpeed = 100;
            //loadingCircle1.Active = true;
            if (!string.IsNullOrEmpty(lable))
                this.labHint.Text = lable;
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            if (m.Msg == WM_SYSCOMMAND)
                if ((int)m.WParam == SC_CLOSE)
                    return; // ctrl+F4关不了，副作用：关闭按钮也关不了
            base.WndProc(ref m);
        }

        public void Dispose()
        {
            try
            {
                if(!this.IsDisposed)
                {
                    this.Close();
                }
            }
            catch 
            {
              
            }
        }
    }
}