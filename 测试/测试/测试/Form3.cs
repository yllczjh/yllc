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
           MessageBox.Show("a\r\nd");
        } 

        private void Form3_Load(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)this.GetType().GetField("aaaaa", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);

            tb.Text = "SUCCESS";
        }
    }
}
