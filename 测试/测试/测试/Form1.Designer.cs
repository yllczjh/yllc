namespace 测试
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.balloonTip1 = new DevComponents.DotNetBar.BalloonTip();
            this.dotNetBarManager1 = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.dockSite4 = new DevComponents.DotNetBar.DockSite();
            this.dockSite1 = new DevComponents.DotNetBar.DockSite();
            this.dockSite2 = new DevComponents.DotNetBar.DockSite();
            this.dockSite8 = new DevComponents.DotNetBar.DockSite();
            this.dockSite5 = new DevComponents.DotNetBar.DockSite();
            this.dockSite6 = new DevComponents.DotNetBar.DockSite();
            this.dockSite7 = new DevComponents.DotNetBar.DockSite();
            this.dockSite3 = new DevComponents.DotNetBar.DockSite();
            this.dockContainerItem1 = new DevComponents.DotNetBar.DockContainerItem();
            this.dockContainerItem2 = new DevComponents.DotNetBar.DockContainerItem();
            this.dockContainerItem3 = new DevComponents.DotNetBar.DockContainerItem();
            this.dockContainerItem4 = new DevComponents.DotNetBar.DockContainerItem();
            this.radialMenuItem2 = new DevComponents.DotNetBar.RadialMenuItem();
            this.radialMenuItem3 = new DevComponents.DotNetBar.RadialMenuItem();
            this.radialMenuItem4 = new DevComponents.DotNetBar.RadialMenuItem();
            this.radialMenuItem6 = new DevComponents.DotNetBar.RadialMenuItem();
            this.radialMenuItem7 = new DevComponents.DotNetBar.RadialMenuItem();
            this.radialMenuItem8 = new DevComponents.DotNetBar.RadialMenuItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.styleManagerAmbient1 = new DevComponents.DotNetBar.StyleManagerAmbient(this.components);
            this.buttonEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.styleManager2 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.styleManagerAmbient2 = new DevComponents.DotNetBar.StyleManagerAmbient(this.components);
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
            this.touchKeyboard1 = new DevComponents.DotNetBar.Keyboard.TouchKeyboard();
            this.advTree1 = new DevComponents.AdvTree.AdvTree();
            this.node1 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.node2 = new DevComponents.AdvTree.Node();
            this.node3 = new DevComponents.AdvTree.Node();
            this.node4 = new DevComponents.AdvTree.Node();
            this.node5 = new DevComponents.AdvTree.Node();
            this.node6 = new DevComponents.AdvTree.Node();
            this.node7 = new DevComponents.AdvTree.Node();
            this.node8 = new DevComponents.AdvTree.Node();
            this.node9 = new DevComponents.AdvTree.Node();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader3 = new DevComponents.AdvTree.ColumnHeader();
            this.node10 = new DevComponents.AdvTree.Node();
            this.node11 = new DevComponents.AdvTree.Node();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advTree1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(313, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(161, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2(&{F6})";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.balloonTip1.SetBalloonCaption(this.buttonX1, "标题");
            this.balloonTip1.SetBalloonText(this.buttonX1, "这是一个按钮");
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(669, 12);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 4;
            this.buttonX1.Text = "buttonX1";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // dotNetBarManager1
            // 
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlY);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this.dotNetBarManager1.BottomDockSite = this.dockSite4;
            this.dotNetBarManager1.EnableFullSizeDock = false;
            this.dotNetBarManager1.LeftDockSite = this.dockSite1;
            this.dotNetBarManager1.ParentForm = this;
            this.dotNetBarManager1.RightDockSite = this.dockSite2;
            this.dotNetBarManager1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.dotNetBarManager1.ToolbarBottomDockSite = this.dockSite8;
            this.dotNetBarManager1.ToolbarLeftDockSite = this.dockSite5;
            this.dotNetBarManager1.ToolbarRightDockSite = this.dockSite6;
            this.dotNetBarManager1.ToolbarTopDockSite = this.dockSite7;
            this.dotNetBarManager1.TopDockSite = this.dockSite3;
            // 
            // dockSite4
            // 
            this.dockSite4.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite4.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite4.Location = new System.Drawing.Point(0, 464);
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.Size = new System.Drawing.Size(921, 0);
            this.dockSite4.TabIndex = 23;
            this.dockSite4.TabStop = false;
            // 
            // dockSite1
            // 
            this.dockSite1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite1.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite1.Location = new System.Drawing.Point(0, 0);
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.Size = new System.Drawing.Size(0, 464);
            this.dockSite1.TabIndex = 20;
            this.dockSite1.TabStop = false;
            // 
            // dockSite2
            // 
            this.dockSite2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite2.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite2.Location = new System.Drawing.Point(921, 0);
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.Size = new System.Drawing.Size(0, 464);
            this.dockSite2.TabIndex = 21;
            this.dockSite2.TabStop = false;
            // 
            // dockSite8
            // 
            this.dockSite8.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite8.Location = new System.Drawing.Point(0, 464);
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.Size = new System.Drawing.Size(921, 0);
            this.dockSite8.TabIndex = 27;
            this.dockSite8.TabStop = false;
            // 
            // dockSite5
            // 
            this.dockSite5.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite5.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite5.Location = new System.Drawing.Point(0, 0);
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.Size = new System.Drawing.Size(0, 464);
            this.dockSite5.TabIndex = 24;
            this.dockSite5.TabStop = false;
            // 
            // dockSite6
            // 
            this.dockSite6.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite6.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite6.Location = new System.Drawing.Point(921, 0);
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.Size = new System.Drawing.Size(0, 464);
            this.dockSite6.TabIndex = 25;
            this.dockSite6.TabStop = false;
            // 
            // dockSite7
            // 
            this.dockSite7.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite7.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite7.Location = new System.Drawing.Point(0, 0);
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.Size = new System.Drawing.Size(921, 0);
            this.dockSite7.TabIndex = 26;
            this.dockSite7.TabStop = false;
            // 
            // dockSite3
            // 
            this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite3.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite3.Location = new System.Drawing.Point(0, 0);
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.Size = new System.Drawing.Size(921, 0);
            this.dockSite3.TabIndex = 22;
            this.dockSite3.TabStop = false;
            // 
            // dockContainerItem1
            // 
            this.dockContainerItem1.Name = "dockContainerItem1";
            this.dockContainerItem1.Text = "dockContainerItem1";
            // 
            // dockContainerItem2
            // 
            this.dockContainerItem2.Name = "dockContainerItem2";
            this.dockContainerItem2.Text = "dockContainerItem2";
            // 
            // dockContainerItem3
            // 
            this.dockContainerItem3.Name = "dockContainerItem3";
            this.dockContainerItem3.Text = "dockContainerItem3";
            // 
            // dockContainerItem4
            // 
            this.dockContainerItem4.Name = "dockContainerItem4";
            this.dockContainerItem4.Text = "dockContainerItem4";
            // 
            // radialMenuItem2
            // 
            this.radialMenuItem2.Name = "radialMenuItem2";
            this.radialMenuItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.radialMenuItem3});
            this.radialMenuItem2.Text = "aaa1";
            // 
            // radialMenuItem3
            // 
            this.radialMenuItem3.Name = "radialMenuItem3";
            this.radialMenuItem3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.radialMenuItem4});
            this.radialMenuItem3.Text = "aaa11";
            // 
            // radialMenuItem4
            // 
            this.radialMenuItem4.Name = "radialMenuItem4";
            this.radialMenuItem4.Text = "aaa111";
            // 
            // radialMenuItem6
            // 
            this.radialMenuItem6.Name = "radialMenuItem6";
            this.radialMenuItem6.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.radialMenuItem7,
            this.radialMenuItem8});
            this.radialMenuItem6.Text = "bbb1";
            // 
            // radialMenuItem7
            // 
            this.radialMenuItem7.Name = "radialMenuItem7";
            this.radialMenuItem7.Text = "bbb11";
            // 
            // radialMenuItem8
            // 
            this.radialMenuItem8.Name = "radialMenuItem8";
            this.radialMenuItem8.Text = "bbb22";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(150, 244);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.buttonEdit1.Properties.NullText = "";
            this.buttonEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.buttonEdit1.Size = new System.Drawing.Size(261, 20);
            this.buttonEdit1.TabIndex = 40;
            // 
            // styleManager2
            // 
            this.styleManager2.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
            this.styleManager2.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // superTooltip1
            // 
            this.superTooltip1.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
            // 
            // superValidator1
            // 
            this.superValidator1.ContainerControl = this;
            this.superValidator1.ErrorProvider = this.errorProvider1;
            this.superValidator1.Highlighter = this.highlighter1;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider1.Icon")));
            // 
            // highlighter1
            // 
            this.highlighter1.ContainerControl = this;
            // 
            // touchKeyboard1
            // 
            this.touchKeyboard1.FloatingLocation = new System.Drawing.Point(0, 0);
            this.touchKeyboard1.Location = new System.Drawing.Point(0, 0);
            this.touchKeyboard1.Text = "";
            // 
            // advTree1
            // 
            this.advTree1.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advTree1.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advTree1.BackgroundStyle.Class = "TreeBorderKey";
            this.advTree1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advTree1.Location = new System.Drawing.Point(488, 185);
            this.advTree1.Name = "advTree1";
            this.advTree1.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
            this.advTree1.NodesConnector = this.nodeConnector1;
            this.advTree1.NodeStyle = this.elementStyle1;
            this.advTree1.PathSeparator = ";";
            this.advTree1.Size = new System.Drawing.Size(396, 211);
            this.advTree1.Styles.Add(this.elementStyle1);
            this.advTree1.TabIndex = 41;
            this.advTree1.Text = "advTree1";
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node2,
            this.node5,
            this.node6});
            this.node1.Text = "node1";
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // node2
            // 
            this.node2.Expanded = true;
            this.node2.Name = "node2";
            this.node2.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node3,
            this.node4});
            this.node2.Text = "node2";
            // 
            // node3
            // 
            this.node3.Expanded = true;
            this.node3.Name = "node3";
            this.node3.Text = "node3";
            // 
            // node4
            // 
            this.node4.Expanded = true;
            this.node4.Name = "node4";
            this.node4.Text = "node4";
            // 
            // node5
            // 
            this.node5.Expanded = true;
            this.node5.Name = "node5";
            this.node5.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node7});
            this.node5.Text = "node5";
            // 
            // node6
            // 
            this.node6.Expanded = true;
            this.node6.Name = "node6";
            this.node6.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node8});
            this.node6.Text = "node6";
            // 
            // node7
            // 
            this.node7.Expanded = true;
            this.node7.Name = "node7";
            this.node7.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node10,
            this.node11});
            this.node7.NodesColumns.Add(this.columnHeader2);
            this.node7.NodesColumns.Add(this.columnHeader3);
            this.node7.Text = "node7";
            // 
            // node8
            // 
            this.node8.Expanded = true;
            this.node8.Name = "node8";
            this.node8.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node9});
            this.node8.Text = "node8";
            // 
            // node9
            // 
            this.node9.Expanded = true;
            this.node9.Name = "node9";
            this.node9.NodesColumns.Add(this.columnHeader1);
            this.node9.Text = "node9";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Column";
            this.columnHeader1.Width.Absolute = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "Column";
            this.columnHeader2.Width.Absolute = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "Column";
            this.columnHeader3.Width.Absolute = 150;
            // 
            // node10
            // 
            this.node10.Name = "node10";
            // 
            // node11
            // 
            this.node11.Name = "node11";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 464);
            this.Controls.Add(this.advTree1);
            this.Controls.Add(this.dockSite2);
            this.Controls.Add(this.dockSite1);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dockSite3);
            this.Controls.Add(this.dockSite4);
            this.Controls.Add(this.dockSite5);
            this.Controls.Add(this.dockSite6);
            this.Controls.Add(this.dockSite7);
            this.Controls.Add(this.dockSite8);
            this.Controls.Add(this.buttonEdit1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advTree1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.BalloonTip balloonTip1;
        private DevComponents.DotNetBar.DotNetBarManager dotNetBarManager1;
        private DevComponents.DotNetBar.DockSite dockSite4;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem3;
        private DevComponents.DotNetBar.DockSite dockSite1;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem1;
        private DevComponents.DotNetBar.DockSite dockSite2;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem2;
        private DevComponents.DotNetBar.DockSite dockSite3;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem4;
        private DevComponents.DotNetBar.DockSite dockSite5;
        private DevComponents.DotNetBar.DockSite dockSite6;
        private DevComponents.DotNetBar.DockSite dockSite7;
        private DevComponents.DotNetBar.DockSite dockSite8;
        private DevComponents.DotNetBar.RadialMenuItem radialMenuItem2;
        private DevComponents.DotNetBar.RadialMenuItem radialMenuItem3;
        private DevComponents.DotNetBar.RadialMenuItem radialMenuItem4;
        private DevComponents.DotNetBar.RadialMenuItem radialMenuItem6;
        private DevComponents.DotNetBar.RadialMenuItem radialMenuItem7;
        private DevComponents.DotNetBar.RadialMenuItem radialMenuItem8;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.StyleManagerAmbient styleManagerAmbient1;
        private DevExpress.XtraEditors.LookUpEdit buttonEdit1;
        private DevComponents.DotNetBar.StyleManager styleManager2;
        private DevComponents.DotNetBar.StyleManagerAmbient styleManagerAmbient2;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private DevComponents.DotNetBar.Validator.SuperValidator superValidator1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
        private DevComponents.DotNetBar.Keyboard.TouchKeyboard touchKeyboard1;
        private DevComponents.AdvTree.AdvTree advTree1;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.AdvTree.Node node2;
        private DevComponents.AdvTree.Node node3;
        private DevComponents.AdvTree.Node node4;
        private DevComponents.AdvTree.Node node5;
        private DevComponents.AdvTree.Node node7;
        private DevComponents.AdvTree.Node node6;
        private DevComponents.AdvTree.Node node8;
        private DevComponents.AdvTree.Node node9;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.AdvTree.Node node10;
        private DevComponents.AdvTree.Node node11;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.AdvTree.ColumnHeader columnHeader3;
    }
}

