using DevExpress.XtraEditors;
using Erp.Pro.Utils;
using Erp.Pro.Utils.公告窗体;
using Erp.Pro.Utils.工具类;
using Erp.Server.WebAPI;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Erp.Main
{
    public partial class F_Login : XtraForm
    {
        public F_Login()
        {
            InitializeComponent();
            #region 显示在屏幕中心
            this.StartPosition = FormStartPosition.Manual;
            int w = System.Windows.Forms.SystemInformation.WorkingArea.Width;
            int h = System.Windows.Forms.SystemInformation.WorkingArea.Height;
            this.Location = new Point(w / 2 - 250, h / 2 - 180);
            #endregion
        }

        #region 窗体移动

        Point mousePosition;
        Point formPosition;
        bool isMouseDown = false;//是否按下
        bool isMouseIn = false;//是否在窗体内

        private void F_Login_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isMouseDown = true;
            mousePosition = Control.MousePosition;  //鼠标位置
            formPosition = this.Location;
        }

        private void F_Login_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void F_Login_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point mousePoint = Control.MousePosition;   //移动的位置
            if (isMouseDown && isMouseIn)
                this.Location = new Point(mousePoint.X - mousePosition.X + formPosition.X, mousePoint.Y - mousePosition.Y + formPosition.Y);
        }

        private void F_Login_MouseEnter(object sender, System.EventArgs e)
        {
            isMouseIn = true;
        }

        private void F_Login_MouseLeave(object sender, System.EventArgs e)
        {
            isMouseIn = false;
        }
        #endregion


        private void sib_登录_Click(object sender, System.EventArgs e)
        {
            #region 验证非空
            if (string.IsNullOrEmpty(txt_用户.Text.Trim()))
            {
                XtraMessageBox.Show("请输入用户", "提示");
                txt_用户.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_密码.Text.Trim()))
            {
                XtraMessageBox.Show("请输入密码", "提示");
                txt_密码.Focus();
                return;
            }
            #endregion
            SimpleWaitBox F_等待 = null;
            F_等待 = new SimpleWaitBox("登录中,请稍候...");
            JObject login = new JObject();
            login.Add("username", txt_用户.Text);
            login.Add("password", txt_密码.Text);
            JObject json = HttpHelper.HTTP.HttpPost(login, "login");
            if (null != json["errcode"] && json["errcode"].ToString() == "0")
            {
                //成功后
                FileHelper.SetConfigValue("上次登录用户", txt_用户.Text);
                FileHelper.SetConfigValue("上次登录密码", txt_密码.Text);
                FileHelper.SetConfigValue("记住密码", chk_记住密码.Checked.ToString());
                C_实体信息.C_共享变量.系统ID = FileHelper.GetConfigValue("appid");
                C_实体信息.C_共享变量.用户ID = txt_用户.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show(json["msgtext"].ToString(), "提示");
            }
            F_等待.Close();
            F_等待.Dispose();
        }

        private void sib_取消_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void F_Login_Load(object sender, System.EventArgs e)
        {
            C_实体信息.C_共享变量.ServicesAddress = FileHelper.GetConfigValue("ServicesAddress");
            string str_上次登录用户 = FileHelper.GetConfigValue("上次登录用户");
            string str_上次登录密码 = FileHelper.GetConfigValue("上次登录密码");
            string str_记住密码 = FileHelper.GetConfigValue("记住密码");
            txt_用户.Text = str_上次登录用户;
            if (str_记住密码 == "True")
            {
                txt_密码.Text = str_上次登录密码;
                chk_记住密码.Checked = true;
            }
        }
    }
}
