using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp.Tools.Tygn.自定义控件
{
    public partial class U_通用列表 : UserControl
    {
        public C_控件参数[] _P_控件参数;
        public string P_操作类型;
        public int P_焦点行 = 0;
        public int P_每行显示列数 = 2;


        public C_控件参数[] P_控件参数
        {
            get { return _P_控件参数; }
            set { _P_控件参数 = value; }
        }

        public U_通用列表()
        {
            InitializeComponent();
        }

        private void btn_新增_Click(object sender, EventArgs e)
        {
            P_操作类型 = "新增";
            P_焦点行 = 0;
            F_通用编辑页面 f_编辑 = new F_通用编辑页面(this);
            f_编辑.StartPosition = FormStartPosition.CenterParent;
            f_编辑.ShowDialog();
            f_编辑.Dispose();
            //OnAction_新增(null,null);
        }

        private void btn_修改_Click(object sender, EventArgs e)
        {
            P_操作类型 = "修改";
            P_焦点行 = GridView.GetFocusedDataSourceRowIndex();
            F_通用编辑页面 f_编辑 = new F_通用编辑页面(this);
            f_编辑.StartPosition = FormStartPosition.CenterParent;
            f_编辑.ShowDialog();
            f_编辑.Dispose();
        }

        private void btn_删除_Click(object sender, EventArgs e)
        {

        }




        //public event EventHandler 删除处理;

        //protected void OnAction_删除(object sender, EventArgs e)
        //{
        //    if (删除处理 != null)
        //    {
        //        删除处理(sender, e);
        //    }
        //}
    }
}
