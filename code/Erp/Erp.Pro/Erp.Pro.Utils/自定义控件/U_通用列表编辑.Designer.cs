namespace Erp.Pro.Utils.自定义控件
{
    partial class U_通用列表编辑
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.GridControl = new DevExpress.XtraGrid.GridControl();
            this.GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_新增 = new System.Windows.Forms.ToolStripButton();
            this.btn_修改 = new System.Windows.Forms.ToolStripButton();
            this.btn_删除 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_新增,
            this.btn_修改,
            this.btn_删除});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(594, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // GridControl
            // 
            this.GridControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.GridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl.Location = new System.Drawing.Point(0, 25);
            this.GridControl.MainView = this.GridView;
            this.GridControl.Name = "GridControl";
            this.GridControl.Size = new System.Drawing.Size(594, 217);
            this.GridControl.TabIndex = 1;
            this.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView});
            // 
            // GridView
            // 
            this.GridView.GridControl = this.GridControl;
            this.GridView.Name = "GridView";
            this.GridView.OptionsBehavior.Editable = false;
            this.GridView.OptionsView.ShowGroupPanel = false;
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
            // btn_删除
            // 
            this.btn_删除.Image = global::Erp.Pro.Utils.Properties.Resources.trash_32;
            this.btn_删除.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_删除.Name = "btn_删除";
            this.btn_删除.Size = new System.Drawing.Size(52, 22);
            this.btn_删除.Text = "删除";
            this.btn_删除.Click += new System.EventHandler(this.btn_删除_Click);
            // 
            // U_通用列表编辑
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControl);
            this.Controls.Add(this.toolStrip1);
            this.Name = "U_通用列表编辑";
            this.Size = new System.Drawing.Size(594, 242);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public DevExpress.XtraGrid.GridControl GridControl;
        public DevExpress.XtraGrid.Views.Grid.GridView GridView;
        private System.Windows.Forms.ToolStripButton btn_新增;
        private System.Windows.Forms.ToolStripButton btn_修改;
        private System.Windows.Forms.ToolStripButton btn_删除;
    }
}
