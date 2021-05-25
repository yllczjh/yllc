namespace Erp.Tools.Tygn
{
    partial class F_通用编辑页面
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_保存 = new System.Windows.Forms.ToolStripButton();
            this.btn_关闭 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(61, 12);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_保存,
            this.btn_关闭});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(512, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_保存
            // 
            this.btn_保存.Image = global::Erp.Tools.Tygn.Properties.Resources.doc_save_32;
            this.btn_保存.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_保存.Name = "btn_保存";
            this.btn_保存.Size = new System.Drawing.Size(52, 22);
            this.btn_保存.Text = "保存";
            this.btn_保存.Click += new System.EventHandler(this.btn_保存_Click);
            // 
            // btn_关闭
            // 
            this.btn_关闭.Image = global::Erp.Tools.Tygn.Properties.Resources.close_32;
            this.btn_关闭.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_关闭.Name = "btn_关闭";
            this.btn_关闭.Size = new System.Drawing.Size(52, 22);
            this.btn_关闭.Text = "关闭";
            this.btn_关闭.Click += new System.EventHandler(this.btn_关闭_Click);
            // 
            // F_通用编辑页面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 261);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_通用编辑页面";
            this.Text = "F_通用编辑页面";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.F_通用编辑页面_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_保存;
        private System.Windows.Forms.ToolStripButton btn_关闭;
    }
}