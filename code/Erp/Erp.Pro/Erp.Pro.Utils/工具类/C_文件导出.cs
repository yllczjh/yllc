using System;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;

namespace Erp.Pro.Utils.工具类
{
    public class C_文件导出
    {
        #region XtraGrid导出

        private bool _bShowGridTitle = false;
        /// <summary>
        /// 是否需要标题行
        /// </summary>
        public bool bShowGridTitle
        {
            get { return _bShowGridTitle; }
            set { _bShowGridTitle = value; }
        }

        private bool _bUserExport = false;
        /// <summary>
        /// 手工编写导出文本
        /// </summary>
        public bool bUserExport
        {
            get { return _bUserExport; }
            set { _bUserExport = value; }
        }
        private string _strQuotes = "\"";
        /// <summary>
        /// 导出的文本内容需用包括起来
        /// </summary>
        public string strQuotes
        {
            get { return _strQuotes; }
            set { _strQuotes = value; }
        }


        private void ExportToEx(ColumnView exportView, String filename, string ext)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (ext == "rtf") exportView.ExportToRtf(filename);
                if (ext == "pdf") exportView.ExportToPdf(filename);
                if (ext == "mht") exportView.ExportToMht(filename);
                if (ext == "htm") exportView.ExportToHtml(filename);
                if (ext == "csv") exportView.ExportToCsv(filename);
                if (ext == "txt")
                {
                    if (bUserExport)
                    {
                        ExportTxt(exportView, filename);
                    }
                    else
                    {
                        //导出Excel,更改默认的导出分隔符
                        if (bShowGridTitle)
                        {
                            DevExpress.XtraPrinting.TextExportOptions txtOption = new DevExpress.XtraPrinting.TextExportOptions();
                            txtOption.Separator = ",";
                            txtOption.QuoteStringsWithSeparators = true;
                            exportView.ExportToText(filename, txtOption);
                        }
                        else
                        {
                            exportView.ExportToText(filename);
                        }
                    }
                }
                if (ext == "xls") exportView.ExportToXls(filename);
                if (ext == "xlsx") exportView.ExportToXlsx(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出文件错误,原因:" + ex.Message, "提示");
            }

            Cursor.Current = currentCursor;
        }

        private void ExportTxt(ColumnView exportView, String filename)
        {
            StringBuilder sbTxt = new StringBuilder();
            GridView gv = exportView as GridView;
            string Separator = ",";
            for (int iRowCount = 0; iRowCount < gv.RowCount; iRowCount++)
            {
                string strRowCellValue = "";
                for (int iColumnCount = 0; iColumnCount < gv.Columns.Count; iColumnCount++)
                {
                    string sColumnName = gv.Columns[iColumnCount].FieldName;
                    object oRowCellValue = gv.GetRowCellValue(iRowCount, sColumnName);
                    string sRowCellValue = "";
                    if (oRowCellValue != null && oRowCellValue != DBNull.Value)
                        sRowCellValue = oRowCellValue.ToString();
                    sRowCellValue = strQuotes + sRowCellValue + strQuotes;
                    if (iColumnCount == 0)
                        strRowCellValue = sRowCellValue;
                    else
                        strRowCellValue = strRowCellValue + Separator + sRowCellValue;
                }
                sbTxt.AppendLine(strRowCellValue);
            }
            FileHelper.CreateFile(filename, sbTxt.ToString());
        }

        protected void OpenFile(string fileName)
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show("导出完成，是否需要打开该导出文件?", "导出", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("没有发现打开此文件的程序.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        public void ExportGridDataEx(GridControl gridcontrol, string expType, string filePath)
        {
            if (filePath != "")
            {
                ExportToEx(gridcontrol.MainView as ColumnView, filePath, expType);
                OpenFile(filePath);
            }
        }
    }
}
