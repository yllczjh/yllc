using DevExpress.XtraEditors;
using Erp.Tools.Tygn.自定义控件;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Erp.Tools.Tygn
{
    public partial class F_通用编辑页面 : XtraForm
    {
        CurrencyManager cm_绑定管理;

        DataTable dt_数据源;
        int i_数据源行号 = 0;
        int i_每行显示列数 = 2;
        string str_操作类型="新增";

        int i_行号 = 1;
        int i_列号 = 1;
        public F_通用编辑页面()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str_操作类型">新增、修改</param>
        /// <param name="dt">数据源</param>
        /// <param name="控件参数"></param>
        /// <param name="i_行号">修改的行号</param>
        /// <param name="i_每行显示列数">每行显示列数</param>
        public F_通用编辑页面(F_通用列表界面 f_父窗体, string str_操作类型, DataTable dt_数据源, C_控件参数[] 控件参数, int i_数据源行号, int i_每行显示列数 = 2)
        {
            InitializeComponent();

            this.str_操作类型 = str_操作类型;
            this.dt_数据源 = dt_数据源;
            this.i_每行显示列数 = i_每行显示列数;
            this.i_数据源行号 = i_数据源行号;
            显示页面(控件参数);
        }

        public F_通用编辑页面(F_通用列表界面 f_父窗体)
        {
            InitializeComponent();

            this.str_操作类型 = f_父窗体.P_操作类型;
            this.dt_数据源 = f_父窗体.u_列表控件.GridControl.DataSource as DataTable  ;
            this.i_每行显示列数 = f_父窗体.P_每行显示列数;
            this.i_数据源行号 = f_父窗体.P_焦点行;
            显示页面(f_父窗体.P_控件参数);
        }

        public F_通用编辑页面(U_通用列表 f_父窗体)
        {
            InitializeComponent();

            //this.str_操作类型 = f_父窗体.P_操作类型;
            //this.dt_数据源 = f_父窗体.u_列表控件.GridControl.DataSource as DataTable;
            //this.i_每行显示列数 = f_父窗体.P_每行显示列数;
            //this.i_数据源行号 = f_父窗体.P_焦点行;
            //显示页面(f_父窗体.P_控件参数);
        }

        public void 显示页面(C_控件参数[] 控件参数)
        {
            int x = 0;
            int y = 0;
            cm_绑定管理 = (CurrencyManager)this.BindingContext[dt_数据源];
            if (str_操作类型 == "新增")
            {
                cm_绑定管理.AddNew();
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
                        textBox.Width = 120;
                        textBox.DataBindings.Add("Text", dt_数据源, entity.数据名称);
                        textBox.Show();
                        break;
                    case E_控件类型.Dev_Text:
                        TextEdit textEdit = new TextEdit();
                        textEdit.Name = entity.数据名称;
                        this.Controls.Add(textEdit);
                        textEdit.Location = (entity.是否显示) ? new Point(x + l.Width, y) : new Point(0, 0);
                        textEdit.Width = 120;
                        textEdit.DataBindings.Add("Text", dt_数据源, entity.数据名称);
                        textEdit.Show();
                        break;
                    case E_控件类型.Dev_LookUpEdit:
                        LookUpEdit lookUpEdit = new LookUpEdit();
                        lookUpEdit.Name = entity.数据名称;
                        this.Controls.Add(lookUpEdit);
                        lookUpEdit.Location = (entity.是否显示) ? new Point(x + l.Width, y) : new Point(0, 0);
                        lookUpEdit.Width = 120;
                        C_通用方法.M_绑定控件(lookUpEdit, entity.数据源);
                        lookUpEdit.DataBindings.Add("EditValue", dt_数据源, entity.数据名称);
                        lookUpEdit.Show();
                        break;
                }
            }
        }

        private void M_获取位置(int i, ref int x, ref int y)
        {
            i_行号 = i / i_每行显示列数 + 1;
            i_列号 = i % i_每行显示列数 + 1;
            x = (i_列号 - 1) * 200;
            y = i_行号 * 24+10;
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
            if (str_操作类型 == "新增")
            {
                if (DialogResult.Yes == MessageBox.Show("添加成功......", "是否继续添加?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    cm_绑定管理.EndCurrentEdit();
                    cm_绑定管理.ResumeBinding();
                    cm_绑定管理.AddNew();

                    //M_初始化();
                }
                else
                {
                    cm_绑定管理.EndCurrentEdit();
                    this.Close();
                }
            }
            else
            {
                cm_绑定管理.EndCurrentEdit();
                cm_绑定管理.ResumeBinding();
                MessageBox.Show("修改成功");
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
    }
}
