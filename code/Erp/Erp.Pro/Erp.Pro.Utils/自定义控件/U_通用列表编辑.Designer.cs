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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_刷新 = new System.Windows.Forms.ToolStripButton();
            this.btn_新增 = new System.Windows.Forms.ToolStripButton();
            this.btn_修改 = new System.Windows.Forms.ToolStripButton();
            this.btn_导出 = new System.Windows.Forms.ToolStripButton();
            this.btn_保存样式 = new System.Windows.Forms.ToolStripButton();
            this.btn_删除 = new System.Windows.Forms.ToolStripButton();
            this.GridControl = new DevExpress.XtraGrid.GridControl();
            this.GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.menu_修改列别名 = new DevExpress.XtraBars.BarButtonItem();
            this.menu_隐藏列 = new DevExpress.XtraBars.BarButtonItem();
            this.menu_显示隐藏列 = new DevExpress.XtraBars.BarSubItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barListItem1 = new DevExpress.XtraBars.BarListItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(594, 25);
            this.toolStrip1.TabIndex = 0;
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
            this.GridControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridControl_MouseUp);
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
            this.GridView.OptionsCustomization.AllowSort = false;
            this.GridView.OptionsMenu.EnableColumnMenu = false;
            this.GridView.OptionsView.ColumnAutoWidth = false;
            this.GridView.OptionsView.ShowGroupPanel = false;
            this.GridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.GridView_CustomDrawRowIndicator);
            this.GridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridView_MouseDown);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menu_修改列别名),
            new DevExpress.XtraBars.LinkPersistInfo(this.menu_隐藏列),
            new DevExpress.XtraBars.LinkPersistInfo(this.menu_显示隐藏列)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // menu_修改列别名
            // 
            this.menu_修改列别名.Caption = "修改列别名";
            this.menu_修改列别名.Id = 1;
            this.menu_修改列别名.Name = "menu_修改列别名";
            this.menu_修改列别名.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menu_右键_ItemClick);
            // 
            // menu_隐藏列
            // 
            this.menu_隐藏列.Caption = "隐藏列";
            this.menu_隐藏列.Id = 0;
            this.menu_隐藏列.Name = "menu_隐藏列";
            this.menu_隐藏列.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menu_右键_ItemClick);
            // 
            // menu_显示隐藏列
            // 
            this.menu_显示隐藏列.Caption = "显示隐藏列";
            this.menu_显示隐藏列.Id = 4;
            this.menu_显示隐藏列.Name = "menu_显示隐藏列";
            this.menu_显示隐藏列.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menu_右键_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.menu_隐藏列,
            this.menu_修改列别名,
            this.barButtonItem3,
            this.barListItem1,
            this.menu_显示隐藏列});
            this.barManager1.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(594, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 242);
            this.barDockControlBottom.Size = new System.Drawing.Size(594, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 242);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(594, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 242);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barListItem1
            // 
            this.barListItem1.Caption = "barListItem1";
            this.barListItem1.Id = 3;
            this.barListItem1.Name = "barListItem1";
            // 
            // U_通用列表编辑
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "U_通用列表编辑";
            this.Size = new System.Drawing.Size(594, 242);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
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
        private System.Windows.Forms.ToolStripButton btn_导出;
        private System.Windows.Forms.ToolStripButton btn_刷新;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem menu_隐藏列;
        private DevExpress.XtraBars.BarButtonItem menu_修改列别名;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ToolStripButton btn_保存样式;
        private DevExpress.XtraBars.BarListItem barListItem1;
        private DevExpress.XtraBars.BarSubItem menu_显示隐藏列;
    }
}
