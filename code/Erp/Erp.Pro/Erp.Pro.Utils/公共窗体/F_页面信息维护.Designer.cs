namespace Erp.Pro.Utils.公共窗体
{
    partial class F_页面信息维护
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.num_每行显示列数 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_过滤条件 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_数据表 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_模块名 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_模块id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.GridControl = new DevExpress.XtraGrid.GridControl();
            this.GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.字段名 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.类型 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.长度 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.控件类型 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rep_com_控件类型 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.是否显示 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.是否必填 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.是否填充 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.值唯一 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.只读 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.自增 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.默认值 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.数据源 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_加载 = new System.Windows.Forms.ToolStripButton();
            this.btn_保存 = new System.Windows.Forms.ToolStripButton();
            this.btn_上移 = new System.Windows.Forms.ToolStripButton();
            this.btn_下移 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_每行显示列数)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_com_控件类型)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.num_每行显示列数);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Controls.Add(this.txt_过滤条件);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.txt_数据表);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.txt_模块名);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.txt_模块id);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1153, 121);
            this.groupControl2.TabIndex = 12;
            this.groupControl2.Text = "基础设置";
            // 
            // num_每行显示列数
            // 
            this.num_每行显示列数.Location = new System.Drawing.Point(140, 91);
            this.num_每行显示列数.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num_每行显示列数.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.num_每行显示列数.Name = "num_每行显示列数";
            this.num_每行显示列数.ReadOnly = true;
            this.num_每行显示列数.Size = new System.Drawing.Size(82, 22);
            this.num_每行显示列数.TabIndex = 27;
            this.num_每行显示列数.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 14);
            this.label5.TabIndex = 25;
            this.label5.Text = "编辑页每行显示列数:";
            // 
            // txt_过滤条件
            // 
            this.txt_过滤条件.Location = new System.Drawing.Point(339, 64);
            this.txt_过滤条件.Name = "txt_过滤条件";
            this.txt_过滤条件.Size = new System.Drawing.Size(391, 22);
            this.txt_过滤条件.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 14);
            this.label4.TabIndex = 23;
            this.label4.Text = "过滤条件:";
            // 
            // txt_数据表
            // 
            this.txt_数据表.Location = new System.Drawing.Point(69, 61);
            this.txt_数据表.Name = "txt_数据表";
            this.txt_数据表.Size = new System.Drawing.Size(153, 22);
            this.txt_数据表.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 14);
            this.label3.TabIndex = 21;
            this.label3.Text = "数据表:";
            // 
            // txt_模块名
            // 
            this.txt_模块名.Location = new System.Drawing.Point(339, 32);
            this.txt_模块名.Name = "txt_模块名";
            this.txt_模块名.ReadOnly = true;
            this.txt_模块名.Size = new System.Drawing.Size(178, 22);
            this.txt_模块名.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 14);
            this.label2.TabIndex = 19;
            this.label2.Text = "模块名:";
            // 
            // txt_模块id
            // 
            this.txt_模块id.Location = new System.Drawing.Point(69, 32);
            this.txt_模块id.Name = "txt_模块id";
            this.txt_模块id.ReadOnly = true;
            this.txt_模块id.Size = new System.Drawing.Size(153, 22);
            this.txt_模块id.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "模块ID:";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.GridControl);
            this.groupControl1.Controls.Add(this.toolStrip1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 121);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1153, 437);
            this.groupControl1.TabIndex = 13;
            this.groupControl1.Text = "设置列表编辑界面";
            // 
            // GridControl
            // 
            this.GridControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.GridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl.Location = new System.Drawing.Point(2, 47);
            this.GridControl.MainView = this.GridView;
            this.GridControl.Name = "GridControl";
            this.GridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rep_com_控件类型,
            this.repositoryItemCheckEdit1});
            this.GridControl.Size = new System.Drawing.Size(1149, 388);
            this.GridControl.TabIndex = 13;
            this.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView});
            // 
            // GridView
            // 
            this.GridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.字段名,
            this.类型,
            this.长度,
            this.控件类型,
            this.是否显示,
            this.是否必填,
            this.是否填充,
            this.值唯一,
            this.只读,
            this.自增,
            this.默认值,
            this.数据源});
            this.GridView.GridControl = this.GridControl;
            this.GridView.Name = "GridView";
            this.GridView.OptionsCustomization.AllowFilter = false;
            this.GridView.OptionsCustomization.AllowGroup = false;
            this.GridView.OptionsCustomization.AllowQuickHideColumns = false;
            this.GridView.OptionsCustomization.AllowSort = false;
            this.GridView.OptionsMenu.EnableColumnMenu = false;
            this.GridView.OptionsView.ColumnAutoWidth = false;
            this.GridView.OptionsView.ShowGroupPanel = false;
            // 
            // 字段名
            // 
            this.字段名.Caption = "字段名";
            this.字段名.FieldName = "字段名";
            this.字段名.Name = "字段名";
            this.字段名.OptionsColumn.AllowEdit = false;
            this.字段名.Visible = true;
            this.字段名.VisibleIndex = 0;
            this.字段名.Width = 130;
            // 
            // 类型
            // 
            this.类型.Caption = "类型";
            this.类型.FieldName = "类型";
            this.类型.Name = "类型";
            this.类型.OptionsColumn.AllowEdit = false;
            this.类型.Visible = true;
            this.类型.VisibleIndex = 1;
            // 
            // 长度
            // 
            this.长度.Caption = "长度";
            this.长度.FieldName = "长度";
            this.长度.Name = "长度";
            this.长度.OptionsColumn.AllowEdit = false;
            this.长度.Visible = true;
            this.长度.VisibleIndex = 2;
            // 
            // 控件类型
            // 
            this.控件类型.Caption = "控件类型";
            this.控件类型.ColumnEdit = this.rep_com_控件类型;
            this.控件类型.FieldName = "控件类型";
            this.控件类型.Name = "控件类型";
            this.控件类型.Visible = true;
            this.控件类型.VisibleIndex = 3;
            this.控件类型.Width = 153;
            // 
            // rep_com_控件类型
            // 
            this.rep_com_控件类型.AutoHeight = false;
            this.rep_com_控件类型.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rep_com_控件类型.Name = "rep_com_控件类型";
            this.rep_com_控件类型.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // 是否显示
            // 
            this.是否显示.Caption = "是否显示";
            this.是否显示.ColumnEdit = this.repositoryItemCheckEdit1;
            this.是否显示.FieldName = "是否显示";
            this.是否显示.Name = "是否显示";
            this.是否显示.Visible = true;
            this.是否显示.VisibleIndex = 4;
            this.是否显示.Width = 60;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // 是否必填
            // 
            this.是否必填.Caption = "是否必填";
            this.是否必填.ColumnEdit = this.repositoryItemCheckEdit1;
            this.是否必填.FieldName = "是否必填";
            this.是否必填.Name = "是否必填";
            this.是否必填.Visible = true;
            this.是否必填.VisibleIndex = 6;
            this.是否必填.Width = 59;
            // 
            // 是否填充
            // 
            this.是否填充.Caption = "是否填充";
            this.是否填充.ColumnEdit = this.repositoryItemCheckEdit1;
            this.是否填充.FieldName = "是否填充";
            this.是否填充.Name = "是否填充";
            this.是否填充.Visible = true;
            this.是否填充.VisibleIndex = 7;
            this.是否填充.Width = 60;
            // 
            // 值唯一
            // 
            this.值唯一.Caption = "值唯一";
            this.值唯一.ColumnEdit = this.repositoryItemCheckEdit1;
            this.值唯一.FieldName = "值唯一";
            this.值唯一.Name = "值唯一";
            this.值唯一.Visible = true;
            this.值唯一.VisibleIndex = 8;
            this.值唯一.Width = 55;
            // 
            // 只读
            // 
            this.只读.Caption = "只读";
            this.只读.ColumnEdit = this.repositoryItemCheckEdit1;
            this.只读.FieldName = "只读";
            this.只读.Name = "只读";
            this.只读.Visible = true;
            this.只读.VisibleIndex = 5;
            this.只读.Width = 50;
            // 
            // 自增
            // 
            this.自增.Caption = "自增";
            this.自增.ColumnEdit = this.repositoryItemCheckEdit1;
            this.自增.FieldName = "自增";
            this.自增.Name = "自增";
            this.自增.Visible = true;
            this.自增.VisibleIndex = 9;
            this.自增.Width = 50;
            // 
            // 默认值
            // 
            this.默认值.Caption = "默认值";
            this.默认值.FieldName = "默认值";
            this.默认值.Name = "默认值";
            this.默认值.Visible = true;
            this.默认值.VisibleIndex = 10;
            this.默认值.Width = 150;
            // 
            // 数据源
            // 
            this.数据源.Caption = "数据源";
            this.数据源.Name = "数据源";
            this.数据源.Visible = true;
            this.数据源.VisibleIndex = 11;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_加载,
            this.btn_保存,
            this.btn_上移,
            this.btn_下移});
            this.toolStrip1.Location = new System.Drawing.Point(2, 22);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1149, 25);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_加载
            // 
            this.btn_加载.Image = global::Erp.Pro.Utils.Properties.Resources.yd_refresh;
            this.btn_加载.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_加载.Name = "btn_加载";
            this.btn_加载.Size = new System.Drawing.Size(88, 22);
            this.btn_加载.Text = "加载表结构";
            this.btn_加载.Click += new System.EventHandler(this.btn_加载_Click);
            // 
            // btn_保存
            // 
            this.btn_保存.Image = global::Erp.Pro.Utils.Properties.Resources.doc_save_32;
            this.btn_保存.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_保存.Name = "btn_保存";
            this.btn_保存.Size = new System.Drawing.Size(52, 22);
            this.btn_保存.Text = "保存";
            this.btn_保存.Click += new System.EventHandler(this.btn_保存_Click);
            // 
            // btn_上移
            // 
            this.btn_上移.Image = global::Erp.Pro.Utils.Properties.Resources.up_32;
            this.btn_上移.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_上移.Name = "btn_上移";
            this.btn_上移.Size = new System.Drawing.Size(52, 22);
            this.btn_上移.Text = "上移";
            this.btn_上移.Click += new System.EventHandler(this.btn_上移_Click);
            // 
            // btn_下移
            // 
            this.btn_下移.Image = global::Erp.Pro.Utils.Properties.Resources.duan_32;
            this.btn_下移.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_下移.Name = "btn_下移";
            this.btn_下移.Size = new System.Drawing.Size(52, 22);
            this.btn_下移.Text = "下移";
            this.btn_下移.Click += new System.EventHandler(this.btn_下移_Click);
            // 
            // F_页面信息维护
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 558);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupControl2);
            this.Name = "F_页面信息维护";
            this.Text = "F_页面信息维护";
            this.Load += new System.EventHandler(this.F_页面信息维护_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_每行显示列数)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep_com_控件类型)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.TextBox txt_过滤条件;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_数据表;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_模块名;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_模块id;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraGrid.GridControl GridControl;
        public DevExpress.XtraGrid.Views.Grid.GridView GridView;
        private DevExpress.XtraGrid.Columns.GridColumn 字段名;
        private DevExpress.XtraGrid.Columns.GridColumn 类型;
        private DevExpress.XtraGrid.Columns.GridColumn 长度;
        private DevExpress.XtraGrid.Columns.GridColumn 控件类型;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rep_com_控件类型;
        private DevExpress.XtraGrid.Columns.GridColumn 是否显示;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn 是否必填;
        private DevExpress.XtraGrid.Columns.GridColumn 是否填充;
        private DevExpress.XtraGrid.Columns.GridColumn 值唯一;
        private DevExpress.XtraGrid.Columns.GridColumn 只读;
        private DevExpress.XtraGrid.Columns.GridColumn 自增;
        private DevExpress.XtraGrid.Columns.GridColumn 默认值;
        private DevExpress.XtraGrid.Columns.GridColumn 数据源;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_保存;
        private System.Windows.Forms.ToolStripButton btn_上移;
        private System.Windows.Forms.ToolStripButton btn_下移;
        private System.Windows.Forms.ToolStripButton btn_加载;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown num_每行显示列数;
    }
}