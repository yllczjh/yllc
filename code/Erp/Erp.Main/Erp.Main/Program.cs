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
            //DevExpress.UserSkins.OfficeSkins.Register();
            //DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");//皮肤主题
                                                                     //UserLookAndFeel.Default.SetSkinStyle("Office 2007 Green");//皮肤主题

            //设置默认字体,解决XtraGrid导出PDF中文乱码
            //DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("宋体", 9);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //LocalizationKeys.LocalizeString += LocalizationKeys_LocalizeString;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new F_主界面());
        }
    }
}
