namespace Erp.Tools.Tygn
{
    partial class F_通用列表界面
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_新增 = new System.Windows.Forms.ToolStripButton();
            this.btn_修改 = new System.Windows.Forms.ToolStripButton();
            this.btn_删除 = new System.Windows.Forms.ToolStripButton();
            this.u_列表控件 = new Erp.Tools.Tygn.U_列表控件();
            this.toolStrip1.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(617, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_新增
            // 
            this.btn_新增.Image = global::Erp.Tools.Tygn.Properties.Resources.add_32;
            this.btn_新增.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_新增.Name = "btn_新增";
            this.btn_新增.Size = new System.Drawing.Size(52, 22);
            this.btn_新增.Text = "新增";
            this.btn_新增.Click += new System.EventHandler(this.btn_新增_Click);
            // 
            // btn_修改
            // 
            this.btn_修改.Image = global::Erp.Tools.Tygn.Properties.Resources.doc_edit_32;
            this.btn_修改.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_修改.Name = "btn_修改";
            this.btn_修改.Size = new System.Drawing.Size(52, 22);
            this.btn_修改.Text = "修改";
            this.btn_修改.Click += new System.EventHandler(this.btn_修改_Click);
            // 
            // btn_删除
            // 
            this.btn_删除.Image = global::Erp.Tools.Tygn.Properties.Resources.trash_32;
            this.btn_删除.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_删除.Name = "btn_删除";
            this.btn_删除.Size = new System.Drawing.Size(52, 22);
            this.btn_删除.Text = "删除";
            this.btn_删除.Click += new System.EventHandler(this.btn_删除_Click);
            // 
            // u_列表控件
            // 
            this.u_列表控件.Dock = System.Windows.Forms.DockStyle.Fill;
            this.u_列表控件.Location = new System.Drawing.Point(0, 25);
            this.u_列表控件.Name = "u_列表控件";
            this.u_列表控件.Size = new System.Drawing.Size(617, 279);
            this.u_列表控件.TabIndex = 1;
            // 
            // F_通用列表界面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 304);
            this.Controls.Add(this.u_列表控件);
            this.Controls.Add(this.toolStrip1);
            this.Name = "F_通用列表界面";
            this.Text = "F_通用操作菜单";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_新增;
        private System.Windows.Forms.ToolStripButton btn_修改;
        private System.Windows.Forms.ToolStripButton btn_删除;
        public U_列表控件 u_列表控件;
    }
}