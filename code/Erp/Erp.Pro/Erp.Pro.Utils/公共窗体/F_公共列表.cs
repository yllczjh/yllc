using DevExpress.XtraEditors;
using Erp.Pro.Utils.工具类;
using Erp.Server.Helper;
using Erp.Server.Init;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using static Erp.Pro.Utils.C_实体信息;
using static Erp.Server.Helper.ServerHelper;

namespace Erp.Pro.Utils.公共窗体
{
    public partial class F_公共列表 : XtraForm
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();
        JObject j_界面信息 = new JObject();

        public string P_操作类型;
        public int P_焦点行 = 0;
        public C_控件参数[] P_控件参数;
        private DataTable dt_数据源;
        public int P_每行显示列数 = 3;
        public string P_页面名称;


        public F_公共列表()
        {
            InitializeComponent();
        }

        private void F_公共列表_Load(object sender, System.EventArgs e)
        {
            j_界面信息 = FileHelper.Readjson("2aa63cb7298b45c3a5ab5631faf90134.json");

            P_页面名称 = j_界面信息["menuname"].ToString();
            P_每行显示列数 = int.Parse(j_界面信息["columesnum"].ToString());

            inParam.Clear();
            inParam.P_模块名 = E_模块名称.通用业务;
            inParam.P_页面名 = "公共列表";
            inParam.P_方法名 = "初始化";
            inParam.P1 = j_界面信息["tablename"].ToString();
            inParam.P2 = j_界面信息["filter"].ToString();
            outParam = C_Server.Call(inParam);
            if (outParam.P_结果 == 1)
            {
                dt_数据源 = outParam.P_数据集;
                GridControl.DataSource = dt_数据源;
            }
            else
            {
                XtraMessageBox.Show(outParam.P_结果描述, "提示");
            }
        }

        private void btn_刷新_Click(object sender, System.EventArgs e)
        {

        }

        private void btn_新增_Click(object sender, System.EventArgs e)
        {
            P_控件参数 = M_设置控件参数();
            if (null != j_界面信息["columesnum"])
            {
                P_每行显示列数 = int.Parse(j_界面信息["columesnum"].ToString());
            }
            P_操作类型 = "新增";
            P_焦点行 = 0;
            if (null == P_控件参数)
            {
                XtraMessageBox.Show("未设置编辑界面控件参数,请联系开发人员", "提示");
                return;
            }
            F_通用编辑页面 f_编辑 = new F_通用编辑页面(this);
            f_编辑.StartPosition = FormStartPosition.CenterParent;
            f_编辑.ShowDialog();
            f_编辑.Dispose();
            if (f_编辑.P_结果)
            {
                //M_加载列表数据();
                GridView.FocusedRowHandle = P_焦点行;
            }
        }

        private void btn_修改_Click(object sender, System.EventArgs e)
        {
            P_控件参数 = M_设置控件参数();
            if (null != j_界面信息["columesnum"])
            {
                P_每行显示列数 = int.Parse(j_界面信息["columesnum"].ToString());
            }
            P_操作类型 = "修改";
            P_焦点行 = GridView.GetFocusedDataSourceRowIndex();
            if (null == P_控件参数)
            {
                XtraMessageBox.Show("未设置编辑界面控件参数,请联系开发人员", "提示");
                return;
            }
            F_通用编辑页面 f_编辑 = new F_通用编辑页面(this);
            f_编辑.StartPosition = FormStartPosition.CenterParent;
            f_编辑.ShowDialog();
            f_编辑.Dispose();
            if (f_编辑.P_结果)
            {
                //M_加载列表数据();
                GridView.FocusedRowHandle = P_焦点行;
            }
        }

        private void btn_导出_Click(object sender, System.EventArgs e)
        {
            C_文件导出 exportUtil = new C_文件导出();
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.RestoreDirectory = true;
            saveDialog.FilterIndex = 1;

            saveDialog.FileName = P_页面名称 + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";

            saveDialog.Title = "保存文件";
            saveDialog.Filter = "Excel files(*.xls)|*.xls|Excel files(*.xlsx)|*.xlsx|All files|*.*";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string str_文件类型 = Path.GetExtension(saveDialog.FileName).Split('.')[1].ToLower();
                exportUtil.ExportGridDataEx(GridControl, str_文件类型, saveDialog.FileName);
            }
        }

        private void btn_保存样式_Click(object sender, System.EventArgs e)
        {
            if (C_样式设置.Save(GridView, P_页面名称))
            {
                XtraMessageBox.Show("保存成功", "提示");
            }
        }

        private void btn_删除_Click(object sender, System.EventArgs e)
        {
            GridView.CloseEditor();
            dt_数据源.AcceptChanges();
            DataTable dt_删除行 = dt_数据源.Copy();
            dt_删除行.DefaultView.RowFilter = "选择='True'";
            dt_删除行 = dt_删除行.DefaultView.ToTable();
            //有复选框选择的则删除选中的，没有则删除焦点行的
            if (dt_删除行.Rows.Count <= 0)
            {
                P_焦点行 = GridView.FocusedRowHandle;
                DataRow dr_删除行 = GridView.GetDataRow(P_焦点行);
                if (null == dr_删除行)
                {
                    XtraMessageBox.Show("请选择要删除的数据!", "提示");
                    return;
                }
                dt_删除行.Rows.Add(dr_删除行.ItemArray);
            }

            if (DialogResult.Yes == XtraMessageBox.Show("是否要删除?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                inParam.P_模块名 = E_模块名称.通用业务;
                inParam.P_页面名 = "公共列表";
                inParam.P_方法名 = "删除";
                inParam.P_数据集 = dt_删除行;
                inParam.P1 = j_界面信息["tablename"].ToString();
                outParam = C_Server.Call(inParam);

                if (outParam.P_结果 == 1)
                {
                    XtraMessageBox.Show("删除成功!", "提示");
                    //M_加载列表数据();

                    GridView.FocusedRowHandle = P_焦点行;
                }
                else
                {
                    XtraMessageBox.Show(outParam.P_结果描述, "提示");
                }
            }
        }

        private C_控件参数[] M_设置控件参数()
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(j_界面信息["dataset"].ToString());

            C_控件参数[] P_控件参数 = new C_控件参数[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                P_控件参数[i] = new C_控件参数()
                {
                    数据名称 = row["字段名"].ToString(),
                    显示名称 = row["显示名称"].ToString(),
                    控件类型 = string.IsNullOrEmpty(row["控件类型"].ToString()) ? E_控件类型.Dev_Text : (E_控件类型)System.Enum.Parse(typeof(E_控件类型), row["控件类型"].ToString()),
                    是否显示 = Boolean.Parse(row["是否显示"].ToString()),
                    是否必填 = Boolean.Parse(row["是否必填"].ToString()),
                    是否填充 = Boolean.Parse(row["是否填充"].ToString()),
                    值唯一 = Boolean.Parse(row["值唯一"].ToString()),
                    只读 = Boolean.Parse(row["只读"].ToString()),
                    自增 = Boolean.Parse(row["自增"].ToString()),
                    默认值 = row["默认值"]
                };
            }
            return P_控件参数;
        }
    }
}
