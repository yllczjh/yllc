using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 测试
{
    class Class2 : I_PayHelper
    {
        public void Pay()
        {
            MessageBox.Show("Pay", "提示");
        }

        public void UnPay()
        {
            MessageBox.Show("UnPay", "提示");
        }
    }
}
