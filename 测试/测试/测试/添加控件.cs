using DevComponents.DotNetBar.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 测试
{
    public partial class 添加控件 : Form
    {
        public 添加控件()
        {
            InitializeComponent();
        }
        #region ""

        private void 添加控件_Load(object sender, EventArgs e)
        {
            string[] s = new string[] { "现金", "微信", "支付宝", "银联", "其他", "网上支付", "网上支付" };
            int x = 2;
            int y = 13 - 10;
            int i_行间距 = 10 - 5;
            int i_列间距 = 6;
            int i_行号 = 0;
            for (int i = 0; i < s.Length; i++)
            {
                CheckBoxX check = new CheckBoxX();
                check.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                check.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;


                check.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                check.AutoSize = true;
                check.Tag = i;
                check.Text = i + "-" + s[i];
                check.Font = new Font("宋体", 11F, FontStyle.Bold);
                check.ForeColor = Color.DarkBlue;
                if (x + check.Size.Width > panelEx1.Width)
                {
                    i_行号++;
                    x = 2;
                    y = 13 - 10 + check.Size.Height * i_行号 + i_行号 * i_行间距;
                    check.Font = new Font("宋体", 9F, FontStyle.Bold);
                }
                else
                {
                    check.Font = new Font("宋体", 9F, FontStyle.Bold);
                }
                check.Location = new Point(x, y);
                panelEx1.Controls.Add(check);
                x = check.Location.X + check.Size.Width + i_列间距;
            }
            //if (i_行号 > 0)
            //{
            //    panelEx1.Height = panelEx1.Height + i_行间距;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.Controls.CheckBoxX check_0 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Controls.Add(check_0);

            check_0.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            check_0.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            check_0.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            check_0.Checked = true;
            check_0.CheckState = System.Windows.Forms.CheckState.Checked;
            check_0.CheckValue = "1";
            check_0.CheckValueChecked = "1";
            check_0.CheckValueUnchecked = "0";
            check_0.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            check_0.Location = new System.Drawing.Point(3, 12);
            check_0.Name = "check_0";
            //check_0.Size = new System.Drawing.Size(70, 23);
            check_0.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            check_0.TabIndex = 0;
            check_0.AutoSize = true;
            check_0.Tag = "0";
            check_0.Text = "0-现金";
        }
        I_PayHelper i_PayHelper = null;
        string aaa = "测试.Class2";
        private void button2_Click(object sender, EventArgs e)
        {
            // i_PayHelper = Type.GetType(aaa,true,true);

            Type t = Type.GetType(aaa);
            i_PayHelper = (I_PayHelper)t.Assembly.CreateInstance(aaa);
            i_PayHelper.Pay();
        }

        #endregion
        #region "测试dll"

        private static object _comObject = null;
        static object comObject
        {
            get
            {
                if (_comObject == null)
                    _comObject = Activator.CreateInstance(comObjectName);

                if (_comObject == null)
                    throw new Exception("创建医保接口实例失败！");
                else
                    return _comObject;
            }
        }//com组件实例
        private static Type _comObjectName = null;
        static Type comObjectName
        {
            get
            {
                if (_comObjectName == null)
                    _comObjectName = Type.GetTypeFromProgID(progId);

                if (_comObjectName == null)
                    throw new Exception("获取医保接口注册信息失败，请确认动态库是否正确注册，注册后启动程序重试！");
                else
                    return _comObjectName;
            }
        }//com组件类
        static string progId = "ClassLibrary1";
        //static string progId = "sei3";
        static object comResult;

        private void button3_Click(object sender, EventArgs e)
        {
            comResult = comObjectName.InvokeMember("aaa", BindingFlags.InvokeMethod, null, comObject, null);
            if (comResult != null)
                MessageBox.Show(comResult.ToString(), "提示");
            else
                throw new Exception("清空医保参数异常！");
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool b = Regex.IsMatch(textEdit1.Text, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d)$");
            MessageBox.Show(b.ToString(), "提示");
            //String time = "12:34";
            //Pattern p = Pattern.compile("^([01][0-9]|2[0-3]):([0-5][0-9])$");
            //Matcher m = p.matcher(time);
            //if (m.find())
            //{
            //    System.out.println("HH:" + m.group(1));
            //    System.out.println("mm:" + m.group(2));
            //    System.out.println("HH:mm:" + m.group());
            //}
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AA");
            dt.Columns.Add("BB");
            DataRow row = dt.NewRow();
            row["AA"] = "AA1";
            row["BB"] = "BB1";
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["AA"] = "AA2";
            row["BB"] = "BB2";
            dt.Rows.Add(row);

            gridControl1.DataSource = dt;
            gridView1.FocusedRowHandle = 1;
            gridView1.FocusedColumn = gridView1.Columns["AA"];
            //GridColumn c = gridView1.Columns["AA"];
            //gridView1.SelectCell(1, c);
            gridView1.ShowEditor();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string str_上次小类编码 = textEdit2.Text;
            string str_当前行小类编码 = textEdit3.Text;
            if ((str_上次小类编码 != str_当前行小类编码 &&
                                                        (str_上次小类编码 == "3" 
                                                        || str_当前行小类编码 == "3" 
                                                        || str_上次小类编码 == "4" 
                                                        || str_当前行小类编码 == "4" 
                                                        || str_上次小类编码 == "12" 
                                                        || str_当前行小类编码 == "12"))
                 || (str_上次小类编码 == "3" && str_当前行小类编码 == "3") 
                 || (str_上次小类编码 == "12" && str_当前行小类编码 == "12")
                               )
            {
                MessageBox.Show("1", "提示");
            }
            //西成、中分别产生处方号 
            //1 西药
            //2 成药
            //3 草药
            //4 卫材
            //12 中药颗粒
            if (!(
                        (str_上次小类编码 == "1" && (str_当前行小类编码 == "1" || str_当前行小类编码 == "2"))
                        || (str_上次小类编码 == "2" && (str_当前行小类编码 == "1" || str_当前行小类编码 == "2"))
                        || (str_上次小类编码 == "4" && str_当前行小类编码 == "4"))
                || (str_上次小类编码 == "3" && str_当前行小类编码 == "3")
                || (str_上次小类编码 == "12" && str_当前行小类编码 == "12")
                              )
            {
                MessageBox.Show("2", "提示");
            }
        }
    }
}
