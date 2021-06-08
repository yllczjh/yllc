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
            
        }

        
    }
}
