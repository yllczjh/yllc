using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Erp.Pro.Utils.自定义控件;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Erp.Pro.Utils.C_实体信息;
using Erp.Server.Helper;
using static Erp.Server.Helper.ServerHelper;
using Erp.Server.Init;

namespace Erp.Pro.Utils.公共窗体
{
    public partial class F_通用编辑页面 : XtraForm
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();
        CurrencyManager cm_绑定管理;

        private U_通用列表编辑 f_父窗体0;
        private F_公共列表 f_父窗体1;
        private DataTable dt_数据源;
        private int i_数据源行号 = 0;
        private int i_每行显示列数 = 3;
        private string str_操作类型 = "新增";
        private C_控件参数[] 控件参数;
        private string str_页面名称;
        private int i_窗体 = 0;//

        public bool P_结果 = false;

        int i_行号 = 1;
        int i_列号 = 1;
        public F_通用编辑页面()
        {
            InitializeComponent();
        }

        public F_通用编辑页面(U_通用列表编辑 f_父窗体)
        {
            InitializeComponent();
            i_窗体 = 0;
            this.f_父窗体0 = f_父窗体;
            this.str_操作类型 = f_父窗体.P_操作类型;
            this.dt_数据源 = f_父窗体.GridControl.DataSource as DataTable;
            this.i_每行显示列数 = f_父窗体.P_每行显示列数;
            this.i_数据源行号 = f_父窗体.P_焦点行;
            this.控件参数 = f_父窗体.P_控件参数;

            显示页面();
        }

        public F_通用编辑页面(F_公共列表 f_父窗体)
        {
            InitializeComponent();
            i_窗体 = 1;
            this.f_父窗体1 = f_父窗体;
            this.str_操作类型 = f_父窗体.P_操作类型;
            this.dt_数据源 = f_父窗体.GridControl.DataSource as DataTable;
            this.i_每行显示列数 = f_父窗体.P_每行显示列数;
            this.i_数据源行号 = f_父窗体.P_焦点行;
            this.控件参数 = f_父窗体.P_控件参数;
            this.str_页面名称 = f_父窗体.P_页面名称;

            显示页面();
        }

        public void 显示页面()
        {
            int x = 0;
            int y = 0;
            cm_绑定管理 = (CurrencyManager)this.BindingContext[dt_数据源];
            if (str_操作类型 == "新增")
            {
                cm_绑定管理.AddNew();
                i_数据源行号 = cm_绑定管理.Position;
                if ((cm_绑定管理.List[i_数据源行号] as DataRowView).Row.Table.Columns.Contains("选择"))
                {
                    (cm_绑定管理.List[i_数据源行号] as DataRowView).Row["选择"] = false;
                }
            }
            else
            {
                cm_绑定管理.Position = i_数据源行号;
            }
            int label_width = M_获取Label_Width(控件参数);
            this.Width = 200 * i_每行显示列数 + 20;
            Label l = new Label();

            for (int i = 0; i < 控件参数.Length; i++)
            {
                C_控件参数 entity = 控件参数[i];

                if (entity.是否显示)
                {
                    M_获取位置(ref x, ref y);
                    l = new Label();
                    l.Text = entity.显示名称;
                    l.TextAlign = ContentAlignment.MiddleRight;
                    l.AutoSize = false;
                    this.Controls.Add(l);
                    l.Location = new Point(x, y);
                    l.Width = label_width;
                    l.Show();
                }

                switch (entity.控件类型)
                {
                    case E_控件类型.Win_Text:
                        TextBox textBox = new TextBox();
                        textBox.Name = entity.数据名称;
                        this.Controls.Add(textBox);
                        textBox.Location = (entity.是否显示) ? new Point(x + l.Width, y) : new Point(0, 0);
                        textBox.Width = entity.是否填充 ? 120 + (i_每行显示列数 - i_列号) * 200 : 120;
                        if (str_操作类型 == "新增")
                        {
                            (cm_绑定管理.List[i_数据源行号] as DataRowView).Row[entity.数据名称] = entity.默认值;
                        }
                        textBox.DataBindings.Add("Text", dt_数据源, entity.数据名称);
                        textBox.Enabled = !entity.只读;
                        textBox.Show();
                        break;

                    case E_控件类型.Dev_Text:
                        TextEdit textEdit = new TextEdit();
                        textEdit.Name = entity.数据名称;
                        this.Controls.Add(textEdit);
                        textEdit.Location = (entity.是否显示) ? new Point(x + l.Width, y) : new Point(0, 0);
                        textEdit.Width = entity.是否填充 ? 120 + (i_每行显示列数 - i_列号) * 200 : 120;
                        if (str_操作类型 == "新增")
                        {
                            (cm_绑定管理.List[i_数据源行号] as DataRowView).Row[entity.数据名称] = entity.默认值;
                        }
                        textEdit.DataBindings.Add("Text", dt_数据源, entity.数据名称);
                        textEdit.Enabled = !entity.只读;
                        textEdit.Show();
                        break;

                    case E_控件类型.Dev_LookUpEdit:
                        LookUpEdit lookUpEdit = new LookUpEdit();
                        lookUpEdit.Name = entity.数据名称;
                        this.Controls.Add(lookUpEdit);
                        lookUpEdit.Location = (entity.是否显示) ? new Point(x + l.Width, y) : new Point(0, 0);
                        lookUpEdit.Width = entity.是否填充 ? 120 + (i_每行显示列数 - i_列号) * 200 : 120;
                        C_通用方法.M_绑定控件(lookUpEdit, entity.数据源);
                        lookUpEdit.DataBindings.Add("EditValue", dt_数据源, entity.数据名称);
                        lookUpEdit.Enabled = !entity.只读;
                        lookUpEdit.Show();
                        break;

                    case E_控件类型.Dev_ComboBoxEdit:
                        ComboBoxEdit comboBoxEdit = new ComboBoxEdit();
                        comboBoxEdit.Name = entity.数据名称;
                        this.Controls.Add(comboBoxEdit);
                        comboBoxEdit.Location = (entity.是否显示) ? new Point(x + l.Width, y) : new Point(0, 0);
                        comboBoxEdit.Width = entity.是否填充 ? 120 + (i_每行显示列数 - i_列号) * 200 : 120;
                        C_通用方法.M_绑定控件(comboBoxEdit, entity.数据源);
                        comboBoxEdit.DataBindings.Add("EditValue", dt_数据源, entity.数据名称);
                        comboBoxEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                        comboBoxEdit.Enabled = !entity.只读;
                        comboBoxEdit.Show();
                        break;

                    case E_控件类型.Win_DateTimePicker:
                        DateTimePicker dateTimePicker = new DateTimePicker();
                        dateTimePicker.Name = entity.数据名称;
                        this.Controls.Add(dateTimePicker);
                        dateTimePicker.Location = (entity.是否显示) ? new Point(x + l.Width, y) : new Point(0, 0);
                        dateTimePicker.Width = entity.是否填充 ? 120 + (i_每行显示列数 - i_列号) * 200 : 120;
                        if (str_操作类型 == "新增")
                        {
                            (cm_绑定管理.List[i_数据源行号] as DataRowView).Row[entity.数据名称] = (null == entity.默认值 || entity.默认值 is System.DBNull) ? "1900-01-01" : entity.默认值;
                        }
                        dateTimePicker.DataBindings.Add("Text", dt_数据源, entity.数据名称);
                        dateTimePicker.Enabled = !entity.只读;
                        dateTimePicker.Show();
                        break;

                    case E_控件类型.Dev_CheckEdit:
                        CheckEdit checkEdit = new CheckEdit();
                        checkEdit.Name = entity.数据名称;
                        this.Controls.Add(checkEdit);
                        checkEdit.Location = (entity.是否显示) ? new Point(x + l.Width, y + 3) : new Point(0, 0);
                        checkEdit.Width = entity.是否填充 ? 120 + (i_每行显示列数 - i_列号) * 200 : 120;
                        checkEdit.Text = "";
                        if (str_操作类型 == "新增")
                        {
                            (cm_绑定管理.List[i_数据源行号] as DataRowView).Row[entity.数据名称] = (null == entity.默认值 || entity.默认值 is System.DBNull) ? false : entity.默认值;
                        }

                        checkEdit.DataBindings.Add("Checked", dt_数据源, entity.数据名称);
                        checkEdit.Enabled = !entity.只读;
                        checkEdit.Show();
                        break;
                }
                if (entity.是否显示)
                {
                    if (entity.是否填充)
                    {
                        i_行号 += 1;
                        i_列号 = 1;
                    }
                    else
                    {
                        if (i_列号 < i_每行显示列数)
                        {
                            i_列号 += 1;
                        }
                        else
                        {
                            i_列号 = 1;
                            i_行号 += 1;
                        }
                    }
                }
            }
        }

        private void M_获取位置(ref int x, ref int y)
        {
            x = (i_列号 - 1) * 200;
            y = i_行号 * 25 + 10;
        }

        private int M_获取Label_Width(C_控件参数[] 控件参数)
        {
            int width = 0;
            for (int i = 0; i < 控件参数.Length; i++)
            {
                C_控件参数 entity = 控件参数[i];
                Label l = new Label();
                l.Text = entity.显示名称;
                l.TextAlign = ContentAlignment.MiddleRight;
                l.AutoSize = true;
                this.panel1.Controls.Add(l);
                if (l.Width > width)
                {
                    width = l.Width;
                }
            }
            this.panel1.Controls.Clear();
            return width;
        }


        private void btn_保存_Click(object sender, EventArgs e)
        {
            if (!M_必填项验证()) return;
            t_结束编辑.Focus();
            if (str_操作类型 == "新增")
            {
                inParam.Clear();
                inParam.P_模块名 = E_模块名称.通用业务;
                inParam.P_页面名 = str_页面名称;
                inParam.P_方法名 = "新增";
                inParam.P_数据行 = (cm_绑定管理.List[i_数据源行号] as DataRowView).Row;
                outParam = C_Server.Call(inParam);

                if (outParam.P_结果 == 1)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("添加成功......", "是否继续添加?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        cm_绑定管理.EndCurrentEdit();
                        cm_绑定管理.ResumeBinding();
                        cm_绑定管理.AddNew();
                        i_数据源行号 = cm_绑定管理.Position;
                        if (i_窗体 == 0)
                        {
                            f_父窗体0.P_焦点行 = cm_绑定管理.Position;
                        }
                        else
                        {
                            f_父窗体1.P_焦点行 = cm_绑定管理.Position;
                        }

                        M_初始化();
                    }
                    else
                    {
                        cm_绑定管理.EndCurrentEdit();
                        this.Close();
                    }
                    P_结果 = true;
                }
                else
                {
                    XtraMessageBox.Show(outParam.P_结果描述, "提示");
                    return;
                }
            }
            else
            {
                inParam.Clear();
                inParam.P_模块名 = E_模块名称.通用业务;
                inParam.P_页面名 = str_页面名称;
                inParam.P_方法名 = "修改";
                inParam.P_数据行 = (cm_绑定管理.List[i_数据源行号] as DataRowView).Row;
                outParam = C_Server.Call(inParam);

                if (outParam.P_结果 == 1)
                {
                    XtraMessageBox.Show("修改成功", "提示");
                    cm_绑定管理.ResumeBinding();
                    P_结果 = true;
                }
                else
                {
                    XtraMessageBox.Show(outParam.P_结果描述, "提示");
                    return;
                }
                this.Close();
            }
        }

        private void btn_关闭_Click(object sender, EventArgs e)
        {
            cm_绑定管理.CancelCurrentEdit();
            cm_绑定管理.EndCurrentEdit();
            this.Close();
        }

        private void F_通用编辑页面_FormClosed(object sender, FormClosedEventArgs e)
        {
            cm_绑定管理.CancelCurrentEdit();
            cm_绑定管理.EndCurrentEdit();
            this.Close();
        }

        private void F_通用编辑页面_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(str_页面名称))
            {
                XtraMessageBox.Show("未设置编辑界面名称,请联系开发人员");
                cm_绑定管理.CancelCurrentEdit();
                cm_绑定管理.EndCurrentEdit();
                this.Close();
            }
        }

        private bool M_必填项验证()
        {
            for (int i = 0; i < 控件参数.Length; i++)
            {
                C_控件参数 entity = 控件参数[i];
                if (entity.是否必填)
                {
                    Control[] array = this.Controls.Find(entity.数据名称, false);
                    if (array.Length <= 0) continue;
                    switch (entity.控件类型)
                    {
                        case E_控件类型.Win_Text:
                            TextBox textBox = (TextBox)array[0];
                            if (string.IsNullOrEmpty(textBox.Text))
                            {
                                XtraMessageBox.Show(entity.显示名称 + "不能为空");
                                textBox.Focus();
                                return false;
                            }
                            break;
                        case E_控件类型.Dev_Text:
                            TextEdit textEdit = (TextEdit)array[0];
                            if (string.IsNullOrEmpty(textEdit.Text))
                            {
                                XtraMessageBox.Show(entity.显示名称 + "不能为空");
                                textEdit.Focus();
                                return false;
                            }
                            break;
                        case E_控件类型.Dev_LookUpEdit:
                            LookUpEdit lookUpEdit = (LookUpEdit)array[0];
                            if (string.IsNullOrEmpty(lookUpEdit.EditValue?.ToString()))
                            {
                                XtraMessageBox.Show(entity.显示名称 + "不能为空");
                                lookUpEdit.Focus();
                                return false;
                            }
                            break;
                        case E_控件类型.Dev_ComboBoxEdit:
                            ComboBoxEdit comboBoxEdit = (ComboBoxEdit)array[0];
                            if (string.IsNullOrEmpty(comboBoxEdit.Text))
                            {
                                XtraMessageBox.Show(entity.显示名称 + "不能为空");
                                comboBoxEdit.Focus();
                                return false;
                            }
                            break;
                    }
                }
            }
            return true;
        }

        private void M_初始化()
        {
            for (int i = 0; i < 控件参数.Length; i++)
            {
                C_控件参数 entity = 控件参数[i];
                //if (entity.是否必填)
                //{
                Control[] array = this.Controls.Find(entity.数据名称, false);
                if (array.Length <= 0) continue;
                switch (entity.控件类型)
                {
                    case E_控件类型.Win_Text:
                        TextBox textBox = (TextBox)array[0];
                        if (entity.自增)
                        {
                            int a;
                            int.TryParse(textBox.Text, out a);
                            textBox.Text = (a + 1).ToString();
                        }
                        else
                        {
                            textBox.Text = entity.默认值?.ToString();
                        }
                        break;
                    case E_控件类型.Dev_Text:
                        TextEdit textEdit = (TextEdit)array[0];
                        if (entity.自增)
                        {
                            int a;
                            int.TryParse(textEdit.Text, out a);
                            textEdit.Text = (a + 1).ToString();
                        }
                        else
                        {
                            textEdit.Text = entity.默认值?.ToString();
                        }
                        break;
                    case E_控件类型.Dev_LookUpEdit:
                        LookUpEdit lookUpEdit = (LookUpEdit)array[0];
                        //lookUpEdit.Text = entity.默认值?.ToString();
                        break;
                    case E_控件类型.Dev_CheckEdit:
                        CheckEdit checkEdit = (CheckEdit)array[0];
                        bool b = entity.默认值?.GetType() == typeof(bool) ? bool.Parse(entity.默认值.ToString()) : false;
                        checkEdit.Checked = b;
                        break;
                }
                //}
            }
        }
    }
}
