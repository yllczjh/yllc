using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp.Tools.Tygn
{
    public partial class U_列表控件 : UserControl
    {
        public U_列表控件()
        {
            InitializeComponent();
        }

        public void DataSource(DataTable dt)
        {
            GridControl.DataSource = dt;
        }
    }
}
