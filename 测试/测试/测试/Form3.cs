using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 测试
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void dateEdit1_Leave(object sender, EventArgs e)
        {
            //dateEdit1.Text = dateEdit1.Text.Substring(0,7);
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            dateEdit1.Text = dateEdit1.Text.Substring(0, 7);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           MessageBox.Show(DateTime.Now.ToString("HH"));
        } 

        private void Form3_Load(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)this.GetType().GetField("aaaaa", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);

            tb.Text = "SUCCESS";
        }
    }
    public enum M_单据类型
    {
        入库单 = 20,
        调配单 = 22,
        归还单 = 24,
        退库单 = 21,
        调配确认单 = 23,
        归还确认单 = 25,
        调配申请单 = 26,
        调配审核单 = 27,
        归还申请单 = 28,
        归还审核单 = 29
    }
}
