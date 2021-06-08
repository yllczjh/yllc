using System;
using DevExpress.LookAndFeel;
using System.Windows.Forms;

namespace Erp.Main
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");//皮肤主题

            //设置默认字体,解决XtraGrid导出PDF中文乱码
            //DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("宋体", 9);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //LocalizationKeys.LocalizeString += LocalizationKeys_LocalizeString;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            F_Login myLogin = new F_Login();//加载登录窗体
            if (myLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new F_主界面());//如果登录成功则打开主窗体
            }
            else
            {
                GC.Collect();
            }
        }
    }
}
