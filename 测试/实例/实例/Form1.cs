using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 实例
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();

            foreach(string s in args)
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + s;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //richTextBox1
        }
    }
}
