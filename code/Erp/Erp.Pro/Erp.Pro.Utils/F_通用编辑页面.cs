using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Erp.Pro.Utils.自定义控件;
using Erp.Server.Init;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Erp.Server.Init.C_系统参数;
using static Erp.Pro.Utils.C_实体信息;

namespace Erp.Pro.Utils
{
    public partial class F_通用编辑页面 : XtraForm
    {
        ServerParams inParam = new ServerParams();
        ServerParams outParam = new ServerParams();
        CurrencyManager cm_绑定管理;

        U_通用列表编辑 f_父窗体;
        DataTable dt_数据源;
        int i_数据源行号 = 0;
        int i_每行显示列数 = 3;
        string str_操作类型 = "新增";
        C_控件参数[] 控件参数;

        int i_行号 = 1;
        int i_列号 = 1;
        public F_通用编辑页面()
        {
            InitializeComponent();
        }

        public F_通用编辑页面(U_通用列表编辑 f_父窗体)
        {
            InitializeComponent();

            this.f_父窗体 = f_父窗体;
            this.str_操作类型 = f_父窗体.P_操作类型;
            this.dt_数据源 = f_父窗体.GridControl.DataSource as DataTable;
            this.i_每行显示列数 = f_父窗体.P_每行显示列数;
            this.i_数据源行号 = f_父窗体.P_焦点行;
            this.控件参数 = f_父窗体.P_控件参数;
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
            }
            else
            {
                cm_绑定管理.Position = i_数据源行号;
            }
            int label_width = M_获取Label_Width(控件参数);
            this.Width = 200 * i_每行显示列数;
            Label l = new Label();
            for (int i = 0; i < 控件参数.Length; i++)
            {
                C_控件参数 entity = 控件参数[i];

                if (entity.是否显示)
                {
                    M_获取位置(i, ref x, ref y);
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
                        textBox.DataBindings.Add("Text", dt_数据源, entity.数据名称);
                        textBox.Show();
                        break;

                    case E_控件类型.Dev_Text:
                        TextEdit textEdit = new TextEdit();
                        textEdit.Name = entity.数据名称;
                        this.Controls.Add(textEdit);
                        textEdit.Location = (entity.是否显示) ? new Point(x + l.Width, y) : new Point(0, 0);
                        textEdit.Width = entity.是否填充 ? 120 + (i_每行显示列数 - i_列号) * 200 : 120;
                        textEdit.DataBindings.Add("Text", dt_数据源, entity.数据名称);
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
                        comboBoxEdit.Show();
                        break;

                    case E_控件类型.Win_DateTimePicker:
                        DateTimePicker dateTimePicker = new DateTimePicker();
                        dateTimePicker.Name = entity.数据名称;
                        this.Controls.Add(dateTimePicker);
                        dateTimePicker.Location = (entity.是否显示) ? new Point(x + l.Width, y) : new Point(0, 0);
                        dateTimePicker.Width = entity.是否填充 ? 120 + (i_每行显示列数 - i_列号) * 200 : 120;
                        dateTimePicker.DataBindings.Add("Text", dt_数据源, entity.数据名称);
                        dateTimePicker.Show();
                        break;

                    case E_控件类型.Dev_CheckEdit:
                        CheckEdit checkEdit = new CheckEdit();
                        checkEdit.Name = entity.数据名称;
                        this.Controls.Add(checkEdit);
                        checkEdit.Location = (entity.是否显示) ? new Point(x + l.Width, y + 3) : new Point(0, 0);
                        checkEdit.Width = entity.是否填充 ? 120 + (i_每行显示列数 - i_列号) * 200 : 120;
                        checkEdit.Text = "";
                        checkEdit.DataBindings.Add("Checked", dt_数据源, entity.数据名称);
                        checkEdit.Show();
                        break;
                }

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

        private void M_获取位置(int i, ref int x, ref int y)
        {
            //i_行号 = i / i_每行显示列数 + 1;
            //i_列号 = i % i_每行显示列数 + 1;
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
                inParam.p0 = E_模块名称.通用业务;
                inParam.p1 = f_父窗体.P_页面名称;
                inParam.p2 = "新增";
                inParam.p3 = (cm_绑定管理.List[i_数据源行号] as DataRowView).Row;
                outParam = C_Server.Call(inParam);

                if (outParam.p0.ToString() == "1")
                {
                    if (DialogResult.Yes == MessageBox.Show("添加成功......", "是否继续添加?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        cm_绑定管理.EndCurrentEdit();
                        cm_绑定管理.ResumeBinding();
                        cm_绑定管理.AddNew();
                        i_数据源行号 = cm_绑定管理.Position;
                        f_父窗体.P_焦点行 = cm_绑定管理.Position;
                    }
                    else
                    {
                        cm_绑定管理.EndCurrentEdit();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(outParam.p1.ToString(), "提示");
                    return;
                }
            }
            else
            {
                inParam.Clear();
                inParam.p0 = E_模块名称.通用业务;
                inParam.p1 = f_父窗体.P_页面名称;
                inParam.p2 = "修改";
                inParam.p3 = (cm_绑定管理.List[i_数据源行号] as DataRowView).Row;
                outParam = C_Server.Call(inParam);

                if (outParam.p0.ToString() == "1")
                {
                    MessageBox.Show("修改成功", "提示");
                    cm_绑定管理.ResumeBinding();
                }
                else
                {
                    MessageBox.Show(outParam.p1.ToString(), "提示");
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
            if (string.IsNullOrEmpty(f_父窗体.P_页面名称))
            {
                MessageBox.Show("未设置编辑界面名称,请联系开发人员");
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
                                MessageBox.Show(entity.显示名称 + "不能为空");
                                textBox.Focus();
                                return false;
                            }
                            break;
                        case E_控件类型.Dev_Text:
                            TextEdit textEdit = (TextEdit)array[0];
                            if (string.IsNullOrEmpty(textEdit.Text))
                            {
                                MessageBox.Show(entity.显示名称 + "不能为空");
                                textEdit.Focus();
                                return false;
                            }
                            break;
                        case E_控件类型.Dev_LookUpEdit:
                            LookUpEdit lookUpEdit = (LookUpEdit)array[0];
                            if (string.IsNullOrEmpty(lookUpEdit.EditValue?.ToString()))
                            {
                                MessageBox.Show(entity.显示名称 + "不能为空");
                                lookUpEdit.Focus();
                                return false;
                            }
                            break;

                    }

                }
            }
            return true;
        }
    }
}
