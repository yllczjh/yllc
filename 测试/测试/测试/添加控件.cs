using DevComponents.DotNetBar.Controls;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace 测试
{
    public partial class 添加控件 : Form
    {
        public 添加控件()
        {
            InitializeComponent();
        }

        private void 添加控件_Load(object sender, EventArgs e)
        {
            string[] s = new string[] { "现金", "微信", "支付宝", "银联", "其他", "网上支付", "网上支付" };
            int x = 2;
            int y = 13;
            int i_行间距 = 10;
            int i_列间距 = 6;
            int i_行号 = 0;
            for (int i = 0; i < s.Length; i++)
            {
                //CheckEdit check = new CheckEdit();
                //check.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
                //check.Properties.AutoWidth = true;
                //check.Properties.RadioGroupIndex = 1;
                //check.Tag = i;
                //check.Text = i + "-" + s[i];
                //check.Font = new Font("宋体", 11F, FontStyle.Bold);
                //check.ForeColor = Color.DarkBlue;
                //if (x + check.Size.Width > panelEx1.Width)
                //{
                //    i_行号++;
                //    x = 2;
                //    y = 13 + check.Size.Height * i_行号 + i_行号 * i_行间距;
                //}
                //check.Location = new Point(x, y);
                //panelEx1.Controls.Add(check);
                //x = check.Location.X + check.Size.Width + i_列间距;


                CheckBoxX check = new CheckBoxX();
                check.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                check.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;




                check.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                //check.TabIndex = i;
                check.AutoSize = true;
                check.Tag = i;
                check.Text = i + "-" + s[i];
                check.Font = new Font("宋体", 11F, FontStyle.Bold);
                check.ForeColor = Color.DarkBlue;
                if (x + check.Size.Width > panelEx1.Width)
                {
                    i_行号++;
                    x = 2;
                    y = 13 + check.Size.Height * i_行号 + i_行号 * i_行间距;
                }
                check.Location = new Point(x, y);
                panelEx1.Controls.Add(check);
                x = check.Location.X + check.Size.Width + i_列间距;
            }
            if (i_行号 > 0)
            {
                panelEx1.Height = panelEx1.Height + i_行间距;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.Controls.CheckBoxX check_0 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Controls.Add(check_0);

            check_0.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            check_0.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            check_0.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            check_0.Checked = true;
            check_0.CheckState = System.Windows.Forms.CheckState.Checked;
            check_0.CheckValue = "1";
            check_0.CheckValueChecked = "1";
            check_0.CheckValueUnchecked = "0";
            check_0.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            check_0.Location = new System.Drawing.Point(3, 12);
            check_0.Name = "check_0";
            //check_0.Size = new System.Drawing.Size(70, 23);
            check_0.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            check_0.TabIndex = 0;
            check_0.AutoSize = true;
            check_0.Tag = "0";
            check_0.Text = "0-现金";
        }
        I_PayHelper i_PayHelper = null;
        string aaa = "测试.Class2";
        private void button2_Click(object sender, EventArgs e)
        {
           // i_PayHelper = Type.GetType(aaa,true,true);

            Type t= Type.GetType(aaa);
            i_PayHelper = (I_PayHelper)t.Assembly.CreateInstance(aaa);
            i_PayHelper.Pay();
        }
    }
}
