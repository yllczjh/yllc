using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 测试
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.AutoScroll = true;
            Form2 f2 = new Form2();
            f2.Name = "f2";
            f2.TopLevel = false;
            //f2.Dock =DockStyle.Top;
            f2.btn_合并路径阶段选择.Click += aaa;
            panel1.Controls.Add(f2);
            f2.Show();

            Form2 f22 = new Form2();
            f22.Name = "f22";
            f22.TopLevel = false;
            //f22.Dock = DockStyle.Top;
            f22.btn_合并路径阶段选择.Click += aaa;
            panel1.Controls.Add(f22);
            f22.Show();
            f22.Location = new Point(0,f2.Location.Y + f2.Height+10);
        }

        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show(((Button)sender).Parent.Parent.Parent.Name);
        }
    }
}
