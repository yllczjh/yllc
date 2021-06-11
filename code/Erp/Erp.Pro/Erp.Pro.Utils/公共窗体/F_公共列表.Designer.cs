namespace Erp.Pro.Utils.公共窗体
{
    partial class F_公共列表
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GridControl = new DevExpress.XtraGrid.GridControl();
            this.GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_刷新 = new System.Windows.Forms.ToolStripButton();
            this.btn_新增 = new System.Windows.Forms.ToolStripButton();
            this.btn_修改 = new System.Windows.Forms.ToolStripButton();
            this.btn_导出 = new System.Windows.Forms.ToolStripButton();
            this.btn_保存样式 = new System.Windows.Forms.ToolStripButton();
            this.btn_删除 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridControl
            // 
            this.GridControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.GridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl.Location = new System.Drawing.Point(0, 25);
            this.GridControl.MainView = this.GridView;
            this.GridControl.Name = "GridControl";
            this.GridControl.Size = new System.Drawing.Size(775, 322);
            this.GridControl.TabIndex = 3;
            this.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView});
            // 
            // GridView
            // 
            this.GridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridView.GridControl = this.GridControl;
            this.GridView.Name = "GridView";
            this.GridView.OptionsCustomization.AllowFilter = false;
            this.GridView.OptionsCustomization.AllowGroup = false;
            this.GridView.OptionsCustomization.AllowQuickHideColumns = false;
            this.GridView.OptionsMenu.EnableColumnMenu = false;
            this.GridView.OptionsView.ColumnAutoWidth = false;
            this.GridView.OptionsView.ShowGroupPanel = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_刷新,
            this.btn_新增,
            this.btn_修改,
            this.btn_导出,
            this.btn_保存样式,
            this.btn_删除});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(775, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_刷新
            // 
            this.btn_刷新.Image = global::Erp.Pro.Utils.Properties.Resources.yd_refresh;
            this.btn_刷新.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_刷新.Name = "btn_刷新";
            this.btn_刷新.Size = new System.Drawing.Size(52, 22);
            this.btn_刷新.Text = "刷新";
            this.btn_刷新.Click += new System.EventHandler(this.btn_刷新_Click);
            // 
            // btn_新增
            // 
            this.btn_新增.Image = global::Erp.Pro.Utils.Properties.Resources.add_32;
            this.btn_新增.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_新增.Name = "btn_新增";
            this.btn_新增.Size = new System.Drawing.Size(52, 22);
            this.btn_新增.Text = "新增";
            this.btn_新增.Click += new System.EventHandler(this.btn_新增_Click);
            // 
            // btn_修改
            // 
            this.btn_修改.Image = global::Erp.Pro.Utils.Properties.Resources.doc_edit_32;
            this.btn_修改.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_修改.Name = "btn_修改";
            this.btn_修改.Size = new System.Drawing.Size(52, 22);
            this.btn_修改.Text = "修改";
            this.btn_修改.Click += new System.EventHandler(this.btn_修改_Click);
            // 
            // btn_导出
            // 
            this.btn_导出.Image = global::Erp.Pro.Utils.Properties.Resources.doc_continuPrint1;
            this.btn_导出.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_导出.Name = "btn_导出";
            this.btn_导出.Size = new System.Drawing.Size(52, 22);
            this.btn_导出.Text = "导出";
            this.btn_导出.Click += new System.EventHandler(this.btn_导出_Click);
            // 
            // btn_保存样式
            // 
            this.btn_保存样式.Image = global::Erp.Pro.Utils.Properties.Resources.doc_save_32;
            this.btn_保存样式.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_保存样式.Name = "btn_保存样式";
            this.btn_保存样式.Size = new System.Drawing.Size(76, 22);
            this.btn_保存样式.Text = "保存样式";
            this.btn_保存样式.Click += new System.EventHandler(this.btn_保存样式_Click);
            // 
            // btn_删除
            // 
            this.btn_删除.Image = global::Erp.Pro.Utils.Properties.Resources.trash_32;
            this.btn_删除.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_删除.Name = "btn_删除";
            this.btn_删除.Size = new System.Drawing.Size(52, 22);
            this.btn_删除.Text = "删除";
            this.btn_删除.Click += new System.EventHandler(this.btn_删除_Click);
            // 
            // F_公共列表
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 347);
            this.Controls.Add(this.GridControl);
            this.Controls.Add(this.toolStrip1);
            this.Name = "F_公共列表";
            this.Text = "F_公共列表";
            this.Load += new System.EventHandler(this.F_公共列表_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraGrid.GridControl GridControl;
        public DevExpress.XtraGrid.Views.Grid.GridView GridView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_刷新;
        private System.Windows.Forms.ToolStripButton btn_新增;
        private System.Windows.Forms.ToolStripButton btn_修改;
        private System.Windows.Forms.ToolStripButton btn_导出;
        private System.Windows.Forms.ToolStripButton btn_保存样式;
        private System.Windows.Forms.ToolStripButton btn_删除;
    }
}