namespace 业务整理
{
    partial class F_主界面
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
            this.menu_主菜单 = new DevComponents.DotNetBar.ExplorerBar();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.menu_主菜单)).BeginInit();
            this.SuspendLayout();
            // 
            // menu_主菜单
            // 
            this.menu_主菜单.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.menu_主菜单.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.menu_主菜单.BackStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ExplorerBarBackground2;
            this.menu_主菜单.BackStyle.BackColorGradientAngle = 90;
            this.menu_主菜单.BackStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ExplorerBarBackground;
            this.menu_主菜单.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.menu_主菜单.Dock = System.Windows.Forms.DockStyle.Left;
            this.menu_主菜单.GroupImages = null;
            this.menu_主菜单.Images = null;
            this.menu_主菜单.Location = new System.Drawing.Point(0, 0);
            this.menu_主菜单.Name = "menu_主菜单";
            this.menu_主菜单.Size = new System.Drawing.Size(143, 418);
            this.menu_主菜单.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.SystemColors;
            this.menu_主菜单.TabIndex = 0;
            this.menu_主菜单.Text = "explorerBar1";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(143, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 418);
            this.panel1.TabIndex = 1;
            // 
            // F_主界面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 418);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menu_主菜单);
            this.Name = "F_主界面";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.F_主界面_Load);
            ((System.ComponentModel.ISupportInitialize)(this.menu_主菜单)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExplorerBar menu_主菜单;
        private System.Windows.Forms.Panel panel1;
    }
}

