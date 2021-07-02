namespace Erp.Pro.Jcxx
{
    partial class F_权限分配
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_权限分配));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_确定 = new System.Windows.Forms.ToolStripButton();
            this.btn_取消 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_主 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_确定,
            this.btn_取消});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(336, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_确定
            // 
            this.btn_确定.Image = ((System.Drawing.Image)(resources.GetObject("btn_确定.Image")));
            this.btn_确定.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_确定.Name = "btn_确定";
            this.btn_确定.Size = new System.Drawing.Size(52, 22);
            this.btn_确定.Text = "确定";
            this.btn_确定.Click += new System.EventHandler(this.btn_确定_Click);
            // 
            // btn_取消
            // 
            this.btn_取消.Image = ((System.Drawing.Image)(resources.GetObject("btn_取消.Image")));
            this.btn_取消.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_取消.Name = "btn_取消";
            this.btn_取消.Size = new System.Drawing.Size(52, 22);
            this.btn_取消.Text = "取消";
            this.btn_取消.Click += new System.EventHandler(this.btn_取消_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 40);
            this.panel1.TabIndex = 1;
            // 
            // panel_主
            // 
            this.panel_主.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_主.Location = new System.Drawing.Point(0, 65);
            this.panel_主.Name = "panel_主";
            this.panel_主.Size = new System.Drawing.Size(336, 459);
            this.panel_主.TabIndex = 2;
            // 
            // F_权限分配
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 524);
            this.Controls.Add(this.panel_主);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "F_权限分配";
            this.Text = "F_权限分配";
            this.Load += new System.EventHandler(this.F_权限分配_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_确定;
        private System.Windows.Forms.ToolStripButton btn_取消;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_主;
    }
}