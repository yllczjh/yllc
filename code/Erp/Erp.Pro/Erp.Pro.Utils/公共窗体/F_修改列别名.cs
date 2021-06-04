using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp.Pro.Utils.公共窗体
{
    public partial class F_修改列别名 : Form
    {
        private string P_列名 = string.Empty;
        public string P_别名 = string.Empty;
        public bool B_结果 = false;
        public F_修改列别名(string str)
        {
            InitializeComponent();
            P_列名 = str;
            label1.Text = "请输入列【" + str + "】的别名";
            txt_列别名.Text = str;
        }

        private void btn_确定_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_列别名.Text))
            {
                txt_列别名.Text = P_列名;
            }
            P_别名 = txt_列别名.Text;
            B_结果 = true;
            this.Close();
        }

        private void F_修改列别名_Load(object sender, EventArgs e)
        {

        }
    }
}
