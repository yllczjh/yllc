using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 业务整理.数据库;
using 业务管理.数据库;

namespace 业务整理
{
    public partial class F_主界面 : Form
    {
        private WcfHelper.ParmObj InObj = new WcfHelper.ParmObj();
        private WcfHelper.ParmObj OutObj = new WcfHelper.ParmObj();
        public F_主界面()
        {
            InitializeComponent();
        }

    
        private void F_主界面_Load(object sender, EventArgs e)
        {
            OutObj = 数据库操作.M_获取_校对医嘱记录(InObj); ;
        }
    }
}
