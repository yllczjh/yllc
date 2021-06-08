namespace Erp.Pro.Utils.公告窗体
{
    partial class SimpleWaitBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch
            {

            }

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.circularProgress1 = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.labHint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.panelControl1.Controls.Add(this.circularProgress1);
            this.panelControl1.Controls.Add(this.labHint);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(343, 65);
            this.panelControl1.TabIndex = 4;
            // 
            // circularProgress1
            // 
            // 
            // 
            // 
            this.circularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.circularProgress1.FocusCuesEnabled = false;
            this.circularProgress1.Location = new System.Drawing.Point(12, 13);
            this.circularProgress1.Name = "circularProgress1";
            this.circularProgress1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.circularProgress1.Size = new System.Drawing.Size(52, 37);
            this.circularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.circularProgress1.TabIndex = 5;
            // 
            // labHint
            // 
            this.labHint.AutoSize = true;
            this.labHint.Font = new System.Drawing.Font("宋体", 10F);
            this.labHint.Location = new System.Drawing.Point(70, 26);
            this.labHint.Name = "labHint";
            this.labHint.Size = new System.Drawing.Size(175, 14);
            this.labHint.TabIndex = 3;
            this.labHint.Text = "正在加载数据，请稍侯……";
            // 
            // SimpleWaitBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 65);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SimpleWaitBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "正在加载数据";
            this.Load += new System.EventHandler(this.frmWaitDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        public System.Windows.Forms.Label labHint;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress1;
    }
}