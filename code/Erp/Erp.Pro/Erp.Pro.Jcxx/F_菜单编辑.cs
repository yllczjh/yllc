using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Erp.Server.Init;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Erp.Server.Init.C_系统参数;

namespace Erp.Pro.Jcxx
{
    public partial class F_菜单编辑 : XtraForm
    {
        ServerParams inParam = new ServerParams();
        ServerParams outParam = new ServerParams();

        public F_菜单编辑()
        {
            InitializeComponent();
        }

        private void F_菜单编辑_Load(object sender, System.EventArgs e)
        {
            inParam.p0 = E_模块名称.基础业务;
            inParam.p1 = "菜单信息_初始化";
            outParam = C_Server.Call(inParam);
            if (outParam.p0.ToString() == "1")
            {
                DataTable dt_菜单 = outParam.p2 as DataTable;
                ribbon.Pages.Clear();

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

                        //int index = images.Images.IndexOf(images.Images["images\\" + dr_二级["图标"].ToString()]);
                        //if (index == -1)
                        //{
                        //    index = images.Images.IndexOf(images.Images["images\\moren.png"]);
                        //}
                        //buttonItem.LargeImageIndex = index;

                        buttonItem.Name = dr_二级["模块ID"].ToString();
                        group.ItemLinks.Add(buttonItem);
                        page.Groups.Add(group);
                        //buttonItem.ItemClick += new ItemClickEventHandler(buttonItem_Click);
                    }
                }
            }
            else
            {
                MessageBox.Show(outParam.p1.ToString(), "提示");
            }
        }

        private void ribbon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //有窗体标题，设置Y轴偏移量
                if (this.FormBorderStyle != FormBorderStyle.None)
                {
                    Point p = e.Location;
                    p.Y += 22;//弹出菜单的位置刚好在光标位置
                    popupMenu1.ShowPopup(p);
                }
                else
                {
                    popupMenu1.ShowPopup(e.Location);
                }
            }
        }
    }
}
