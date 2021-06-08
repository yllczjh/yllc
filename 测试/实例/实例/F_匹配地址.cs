using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 实例
{
    public partial class F_匹配地址 : Form
    {
        public F_匹配地址()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "浙江市西湖区塘苗路18号华星现代产业园";
            Match m = Regex.Match(str, @"(.*?(?:省|区|市))(.*?市|.*?州|.*?区)(.*?(?:区|县|市))(.*)");
            Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}",
                m.Groups[1].Value.Trim(),
                m.Groups[2].Value.Trim(),
                m.Groups[3].Value.Trim(),
                m.Groups[4].Value.Trim()
                ));
        }
    }
}
