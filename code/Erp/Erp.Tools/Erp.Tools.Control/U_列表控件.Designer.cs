namespace Erp.Tools.Control
{
    partial class U_列表控件
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
            this.GridControl = new DevExpress.XtraGrid.GridControl();
            this.GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GridControl
            // 
            this.GridControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.GridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl.Location = new System.Drawing.Point(0, 0);
            this.GridControl.MainView = this.GridView;
            this.GridControl.Name = "GridControl";
            this.GridControl.Size = new System.Drawing.Size(666, 278);
            this.GridControl.TabIndex = 0;
            this.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView});
            // 
            // GridView
            // 
            this.GridView.GridControl = this.GridControl;
            this.GridView.Name = "GridView";
            this.GridView.OptionsView.ShowGroupPanel = false;
            // 
            // U_列表控件
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControl);
            this.Name = "U_列表控件";
            this.Size = new System.Drawing.Size(666, 278);
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView GridView;
        public DevExpress.XtraGrid.GridControl GridControl;
    }
}
