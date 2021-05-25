using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace Erp.Tools.Tygn
{
    public partial class F_通用列表界面 : XtraForm
    {
        public C_控件参数[] P_控件参数;
        public string P_操作类型;
        public int P_焦点行 = 0;
        public int P_每行显示列数 = 2;

        public F_通用列表界面()
        {
            InitializeComponent();
        }

        private void btn_新增_Click(object sender, EventArgs e)
        {
            M_新增();
        }
        public virtual void M_新增()
        {
            P_操作类型 = "新增";
            P_焦点行 = 0;
            F_通用编辑页面 f_编辑 = new F_通用编辑页面(this);
            f_编辑.StartPosition = FormStartPosition.CenterParent;
            f_编辑.ShowDialog();
            f_编辑.Dispose();
        }

        private void btn_修改_Click(object sender, EventArgs e)
        {
            M_修改();
        }
        public virtual void M_修改()
        {
            P_操作类型 = "修改";
            P_焦点行 = u_列表控件.GridView.GetFocusedDataSourceRowIndex();
            F_通用编辑页面 f_编辑 = new F_通用编辑页面(this);
            f_编辑.StartPosition = FormStartPosition.CenterParent;
            f_编辑.ShowDialog();
            f_编辑.Dispose();
        }
        private void btn_删除_Click(object sender, EventArgs e)
        {
            M_删除();
        }
        public virtual void M_删除()
        {

        }
    }
}
