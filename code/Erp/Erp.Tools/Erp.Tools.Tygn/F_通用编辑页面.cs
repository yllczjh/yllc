using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp.Tools.Tygn
{
    public partial class F_通用编辑页面 : XtraForm
    {
        DataTable dt_数据源;
        int i_数据源行号 = 0;
        int i_每行显示列数 = 2;

        int i_行号 = 1;
        int i_列号 = 1;
        public F_通用编辑页面()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="控件参数"></param>
        /// <param name="i_行号">修改的行号</param>
        /// <param name="i_每行显示列数">每行显示列数</param>
        public F_通用编辑页面(DataTable dt_数据源, C_控件参数[] 控件参数, int i_数据源行号, int i_每行显示列数 = 2)
        {
            InitializeComponent();

            this.dt_数据源 = dt_数据源;
            this.i_每行显示列数 = i_每行显示列数;
            this.i_数据源行号 = i_数据源行号;
            显示页面(控件参数);
        }

        public void 显示页面(C_控件参数[] 控件参数)
        {
            int x = 0;
            int y = 0;
            CurrencyManager cm_绑定管理 = (CurrencyManager)this.BindingContext[dt_数据源];
            cm_绑定管理.Position = i_数据源行号;
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
                    case E_控件类型.文本框:
                        TextBox t = new TextBox();
                        t.Name = entity.数据名称;
                        this.Controls.Add(t);
                        t.Location = (entity.是否显示) ? new Point(x + l.Width, y) : new Point(0, 0);
                        t.Width = 120;
                        t.DataBindings.Add(new Binding("Text", dt_数据源, entity.数据名称));
                        t.Show();
                        break;
                }
            }
        }

        private void M_获取位置(int i, ref int x, ref int y)
        {
            i_行号 = i / i_每行显示列数 + 1;
            i_列号 = i % i_每行显示列数 + 1;
            x = (i_列号 - 1) * 200;
            y = i_行号 * 24+24;
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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
