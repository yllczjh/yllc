using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTab;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Erp.Pro.Jcxx
{
    public class C_菜单加载
    {
        private static XtraTabControl xttc_主界面;
        public static void Init(XtraTabControl xttc, RibbonControl ribbon, DataTable dt_菜单)
        {
            ImageCollection images = new ImageCollection();
            Image img;
            string[] files = Directory.GetFiles("images", "*.png");
            foreach (string file in files)
            {
                img = Image.FromFile(file);
                images.AddImage(img, file);
            }
            ribbon.LargeImages = images;


            xttc_主界面 = xttc;
            dt_菜单.DefaultView.RowFilter = "上级ID='0'";
            dt_菜单.DefaultView.Sort = "排序";
            DataTable dt_一级 = dt_菜单.DefaultView.ToTable();
            foreach (DataRow dr_一级 in dt_一级.Rows)
            {
                RibbonPage page = new RibbonPage(dr_一级["模块名"].ToString());
                ribbon.Pages.Add(page);

                dt_菜单.DefaultView.RowFilter = "上级ID='" + dr_一级["模块ID"] + "'";
                dt_菜单.DefaultView.Sort = "排序";
                DataTable dt_二级 = dt_菜单.DefaultView.ToTable();

                RibbonPageGroup group = new RibbonPageGroup();
                foreach (DataRow dr_二级 in dt_二级.Rows)
                {
                    BarButtonItem buttonItem = ribbon.Items.CreateButton(dr_二级["模块名"].ToString());

                    int index = images.Images.IndexOf(images.Images["images\\" + dr_二级["图标"].ToString()]);
                    if (index == -1)
                    {
                        index = images.Images.IndexOf(images.Images["images\\moren.png"]);
                    }
                    buttonItem.LargeImageIndex = index;

                    buttonItem.Name = dr_二级["模块ID"].ToString();
                    group.ItemLinks.Add(buttonItem);
                    page.Groups.Add(group);
                    buttonItem.ItemClick += new ItemClickEventHandler(buttonItem_Click);
                }
            }
        }

        public static void buttonItem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (XtraTabPage p in xttc_主界面.TabPages)
            {
                if (p.Name == "Erp.Pro.Jcxx.F_用户信息")
                {
                    xttc_主界面.SelectedTabPage = p;
                    return;
                }
            }

            Form form = GetForm("Erp.Pro.Jcxx.dll", "Erp.Pro.Jcxx", "F_用户信息");
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            XtraTabPage page = new XtraTabPage();
            page.Name = "Erp.Pro.Jcxx.F_用户信息";
            page.Text = "F_用户信息";
            page.Controls.Add(form);
            form.Show();
            form.Dock = DockStyle.Fill;
            xttc_主界面.TabPages.Add(page);
            xttc_主界面.SelectedTabPage = page;
        }

        private static Form GetForm(string LibName, string NameSpace, string formName)
        {
            try
            {
                string allName;
                string LibFilePath = Application.StartupPath + "\\" + LibName;
                allName = NameSpace + '.' + formName;
                Assembly FrmAss = Assembly.LoadFrom(LibFilePath);
                Form form = FrmAss.CreateInstance(allName) as Form;
                return form;
            }
            catch
            {
                return null;
            }
        }
    }
}
