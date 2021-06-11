using DevExpress.XtraEditors;
using Erp.Pro.Utils.工具类;
using Erp.Server.Helper;
using Erp.Server.Init;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using static Erp.Server.Helper.ServerHelper;

namespace Erp.Pro.Utils.公共窗体
{
    public partial class F_页面信息维护 : XtraForm
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();
        public F_页面信息维护(string str_模块id, string str_模块名)
        {
            InitializeComponent();
            txt_模块id.Text = str_模块id;
            txt_模块名.Text = str_模块名;
            txt_数据表.Text = "xt_yh";
        }

        private void btn_加载_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_数据表.Text))
            {
                XtraMessageBox.Show("请填写数据表", "提示");
                txt_数据表.Focus();
                return;
            }

            inParam.Clear();
            inParam.P_模块名 = E_模块名称.通用业务;
            inParam.P_页面名 = "页面信息维护";
            inParam.P_方法名 = "初始化";
            inParam.P1 = txt_数据表.Text;
            outParam = C_Server.Call(inParam);
            if (outParam.P_结果 == 1)
            {
                DataTable dt_数据源 = outParam.P_数据集;
                dt_数据源.Columns.Add("控件类型", typeof(string));
                dt_数据源.Columns.Add("是否显示", typeof(bool));
                dt_数据源.Columns.Add("是否填充", typeof(bool));
                dt_数据源.Columns.Add("值唯一", typeof(bool));
                dt_数据源.Columns.Add("只读", typeof(bool));
                dt_数据源.Columns.Add("默认值", typeof(string));
                dt_数据源.Columns.Add("自增", typeof(bool));
                dt_数据源.Columns.Add("是否必填", typeof(bool));
                foreach (DataRow row in dt_数据源.Rows)
                {
                    row["是否显示"] = true;
                    row["是否填充"] = false;
                    row["值唯一"] = false;
                    row["只读"] = false;
                    row["自增"] = false;
                    row["是否必填"] = Convert.ToBoolean(row["必填1"]);
                }
                dt_数据源.Columns.Remove("必填1");

                GridControl.DataSource = dt_数据源;
            }
            else
            {
                XtraMessageBox.Show(outParam.P_结果描述, "提示");
            }
        }

        private void btn_保存_Click(object sender, EventArgs e)
        {
            JObject j = new JObject();
            j.Add("tablename",txt_数据表.Text);
            j.Add("columesnum", num_每行显示列数.Text);
            j.Add("filter", txt_过滤条件.Text);

            DataTable dt = GridControl.DataSource as DataTable;
            string json = JsonConvert.SerializeObject(dt);
            j.Add("dataset", json);
            FileHelper.CreatNewJson(txt_模块id.Text + ".json", j.ToString());
        }

        private void btn_上移_Click(object sender, EventArgs e)
        {

        }

        private void btn_下移_Click(object sender, EventArgs e)
        {


        }

        private void F_页面信息维护_Load(object sender, EventArgs e)
        {
            //设置控件类型数据源
            Array temp = Enum.GetValues(typeof(E_控件类型));
            for (int i = 0; i < temp.Length; i++)
            {
                string info = temp.GetValue(i).ToString();
                rep_com_控件类型.Items.Add(info);
            }

            JObject o =FileHelper.Readjson(txt_模块id.Text + ".json");
            if (null != o)
            {
                txt_数据表.Text = o["tablename"]?.ToString();
                num_每行显示列数.Text = o["columesnum"]?.ToString();
                txt_过滤条件.Text = o["filter"]?.ToString();

                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(o["dataset"].ToString());
                GridControl.DataSource = dt;
            }
        }
    }
}
