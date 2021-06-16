using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Erp.Pro.Utils.工具类;
using Erp.Server.Helper;
using Erp.Server.Init;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static Erp.Server.Helper.ServerHelper;

namespace Erp.Pro.Utils.公共窗体
{
    public partial class F_页面信息维护 : XtraForm
    {
        ServerHelper.Params inParam = new ServerHelper.Params();
        ServerHelper.Params outParam = new ServerHelper.Params();
        DataTable dt_数据源;
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
                dt_数据源 = outParam.P_数据集;
                dt_数据源.Columns.Add("控件类型", typeof(string));
                dt_数据源.Columns.Add("显示名称", typeof(string));
                dt_数据源.Columns.Add("是否显示", typeof(bool));
                dt_数据源.Columns.Add("是否填充", typeof(bool));
                dt_数据源.Columns.Add("值唯一", typeof(bool));
                dt_数据源.Columns.Add("只读", typeof(bool));
                dt_数据源.Columns.Add("默认值", typeof(string));
                dt_数据源.Columns.Add("自增", typeof(bool));
                foreach (DataRow row in dt_数据源.Rows)
                {
                    row["显示名称"] = row["字段名"];
                    row["是否显示"] = true;
                    row["是否填充"] = false;
                    row["值唯一"] = false;
                    row["只读"] = false;
                    row["自增"] = false;
                    row["控件类型"] = "Dev_Text";
                }

                GridControl.DataSource = dt_数据源;
            }
            else
            {
                XtraMessageBox.Show(outParam.P_结果描述, "提示");
            }
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

            JObject o = FileHelper.Readjson(txt_模块id.Text + ".json");
            if (null != o)
            {
                txt_数据表.Text = o["tablename"]?.ToString();
                num_每行显示列数.Text = o["columesnum"]?.ToString();
                txt_过滤条件.Text = o["filter"]?.ToString();

                dt_数据源 = JsonConvert.DeserializeObject<DataTable>(o["dataset"].ToString());
                if (!dt_数据源.Columns.Contains("是否主键"))
                {
                    dt_数据源.Columns.Add("是否主键", typeof(bool));
                    foreach (DataRow row in dt_数据源.Rows)
                    {
                        row["是否主键"] = false;
                    }
                }
                GridControl.DataSource = dt_数据源;
            }
        }

        private void btn_保存_Click(object sender, EventArgs e)
        {
            try
            {
                GridView.CloseEditor();
                JObject j = new JObject();
                j.Add("menuname", txt_模块名.Text);
                j.Add("tablename", txt_数据表.Text);
                j.Add("columesnum", num_每行显示列数.Text);
                j.Add("filter", txt_过滤条件.Text);

                string json = JsonConvert.SerializeObject(dt_数据源);
                j.Add("dataset", json);
                FileHelper.CreatNewJson(txt_模块id.Text + ".json", j.ToString());
                XtraMessageBox.Show("保存成功", "提示");
            }
            catch (Exception e1)
            {
                XtraMessageBox.Show(e1.Message, "提示");
            }
        }

        private void btn_上移_Click(object sender, EventArgs e)
        {
            int i_行号 = GridView.FocusedRowHandle;
            if (i_行号 > 0)
            {
                DataRow newdata = dt_数据源.NewRow();
                newdata.ItemArray = dt_数据源.Rows[i_行号].ItemArray;
                dt_数据源.Rows.RemoveAt(i_行号);
                dt_数据源.Rows.InsertAt(newdata, i_行号 - 1);
                dt_数据源.AcceptChanges();
                GridControl.RefreshDataSource();
                M_设置选中状态(i_行号 - 1);
            }
        }

        private void btn_下移_Click(object sender, EventArgs e)
        {

            int i_行号 = GridView.FocusedRowHandle;
            if (i_行号 >= 0 && i_行号 < dt_数据源.Rows.Count - 1)
            {
                DataRow newdata = dt_数据源.NewRow();
                newdata.ItemArray = dt_数据源.Rows[i_行号].ItemArray;
                dt_数据源.Rows.RemoveAt(i_行号);
                dt_数据源.Rows.InsertAt(newdata, i_行号 + 1);
                dt_数据源.AcceptChanges();
                GridControl.RefreshDataSource();
                M_设置选中状态(i_行号 + 1);
            }
        }
        private void M_设置选中状态(int i_行号)
        {
            int[] arr_行号 = GridView.GetSelectedRows();
            if (arr_行号.Length > 0)
            {
                foreach (int ii_行号 in arr_行号)
                {
                    GridView.UnselectRow(ii_行号);
                }
            }
            GridView.FocusedRowHandle = i_行号;
            GridView.SelectRow(i_行号);
        }

        private void GridView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //GridView gridView = (GridView)sender;
            //if (e.Button == MouseButtons.Left && e.Clicks == 1)
            //{
            //    GridHitInfo hitInfo = gridView.CalcHitInfo(e.Location);
            //    if (hitInfo.InRowCell )
            //    {
            //        //&& hitInfo.Column == this.gridColumn
            //        //MessageBox.Show("Click " + hitInfo.RowHandle);
            //    }
            //}


           
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                GridHitInfo hInfo = GridView.CalcHitInfo(e.Location);
                //判断光标是否在行范围内
                if (hInfo.InRowCell)
                {
                    //btn_修改_Click(null, null);
                }
            }

        }
    }
}
