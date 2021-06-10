
using Erp.Pro.Utils;
using Erp.Server.Helper;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Erp.Pro.Utils.C_实体信息;

namespace Erp.Pro.Jcxx
{
    public partial class F_用户信息 : Form
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();

        public F_用户信息()
        {
            InitializeComponent();
        }

        private void F_用户信息_Load(object sender, EventArgs e)
        {
            u_通用列表编辑2.P_页面名称 = "用户信息";
            u_通用列表编辑2.M_加载列表数据();
        }

        private void u_通用列表编辑2_新增处理(object sender, EventArgs e)
        {
            C_控件参数[] P_控件参数 = M_设置控件参数();
            u_通用列表编辑2.P_每行显示列数 = 3;
            u_通用列表编辑2.P_控件参数 = P_控件参数;
        }

        private void u_通用列表编辑2_修改处理(object sender, EventArgs e)
        {
            C_控件参数[] P_控件参数 = M_设置控件参数();
            u_通用列表编辑2.P_每行显示列数 = 3;
            u_通用列表编辑2.P_控件参数 = P_控件参数;
        }

        private C_控件参数[] M_设置控件参数()
        {
            C_控件参数[] P_控件参数 = new C_控件参数[8];
            P_控件参数[0] = new C_控件参数() { 数据名称 = "用户ID", 控件类型 = E_控件类型.Dev_Text, 是否必填 = true };
            P_控件参数[1] = new C_控件参数() { 数据名称 = "密码", 控件类型 = E_控件类型.Dev_Text, 是否必填 = true };
            P_控件参数[2] = new C_控件参数() { 数据名称 = "性别", 控件类型 = E_控件类型.Dev_ComboBoxEdit, 数据源 = new C_数据源(new List<string> { "男", "女" }) };
            P_控件参数[3] = new C_控件参数() { 数据名称 = "出生日期", 控件类型 = E_控件类型.Win_DateTimePicker };
            P_控件参数[4] = new C_控件参数() { 数据名称 = "手机号码", 控件类型 = E_控件类型.Dev_Text };
            P_控件参数[5] = new C_控件参数() { 数据名称 = "头像地址", 控件类型 = E_控件类型.Dev_Text };
            P_控件参数[6] = new C_控件参数() { 数据名称 = "现住址", 控件类型 = E_控件类型.Dev_Text, 是否填充 = true };
            P_控件参数[7] = new C_控件参数() { 数据名称 = "禁用", 控件类型 = E_控件类型.Dev_CheckEdit};
            return P_控件参数;
        }
    }
}
