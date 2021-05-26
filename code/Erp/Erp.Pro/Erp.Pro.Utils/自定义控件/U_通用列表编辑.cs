using System;
using System.ComponentModel;
using System.Windows.Forms;
using static Erp.Server.Init.C_系统参数;

namespace Erp.Pro.Utils.自定义控件
{
    public partial class U_通用列表编辑 : UserControl
    {
        public string P_操作类型;
        public int P_焦点行 = 0;

        public C_控件参数[] _P_控件参数;
        [Description("设置编辑界面控件相关信息,在代码中赋值"), Category("#通用列表自定义属性")]
        public C_控件参数[] P_控件参数
        {
            get { return _P_控件参数; }
            set { _P_控件参数 = value; }
        }

        public int _P_每行显示列数 = 2;
        [Browsable(true)]
        [Description("设置编辑界面上每行显示的控件列数"), Category("#通用列表自定义属性")]
        public int P_每行显示列数
        {
            get { return _P_每行显示列数; }
            set { _P_每行显示列数 = value; }
        }

        public E_模块名称 _P_模块名称;
        [Browsable(true)]
        [Description("模块名称(通过[页面名称_按钮显示名]来定义服务名)"), Category("#通用列表自定义属性")]
        public E_模块名称 P_模块名称
        {
            get { return _P_模块名称; }
            set { _P_模块名称 = value; }
        }

        public string _P_列表页名称;
        [Browsable(true)]
        [Description("列表页名称(通过[页面名称_按钮显示名]来定义服务名)"), Category("#通用列表自定义属性")]
        public string P_列表页名称
        {
            get { return _P_列表页名称; }
            set { _P_列表页名称 = value; }
        }

        public string _P_编辑页名称;
        [Browsable(true)]
        [Description("编辑页名称(通过[页面名称_按钮显示名]来定义服务名)"), Category("#通用列表自定义属性")]
        public string P_编辑页名称
        {
            get { return _P_编辑页名称; }
            set { _P_编辑页名称 = value; }
        }

        public U_通用列表编辑()
        {
            InitializeComponent();
        }

        private void btn_新增_Click(object sender, EventArgs e)
        {
            OnAction_新增(null, null);

            P_操作类型 = "新增";
            P_焦点行 = 0;
            F_通用编辑页面 f_编辑 = new F_通用编辑页面(this);
            f_编辑.StartPosition = FormStartPosition.CenterParent;
            f_编辑.ShowDialog();
            f_编辑.Dispose();
        }

        private void btn_修改_Click(object sender, EventArgs e)
        {
            OnAction_修改(null, null);

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


        public event EventHandler 新增处理;

        protected void OnAction_新增(object sender, EventArgs e)
        {
            if (新增处理 != null)
            {
                新增处理(sender, e);
            }
        }

        public event EventHandler 修改处理;

        protected void OnAction_修改(object sender, EventArgs e)
        {
            if (修改处理 != null)
            {
                修改处理(sender, e);
            }
        }

        public event EventHandler 删除处理;

        protected void OnAction_删除(object sender, EventArgs e)
        {
            if (删除处理 != null)
            {
                删除处理(sender, e);
            }
        }
    }
}
