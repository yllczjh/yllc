namespace 实例
{
    partial class F_心电图显示
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_心电图显示));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_保存 = new System.Windows.Forms.ToolStripButton();
            this.btn_关闭 = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_保存,
            this.btn_关闭});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(939, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_保存
            // 
            this.btn_保存.Image = ((System.Drawing.Image)(resources.GetObject("btn_保存.Image")));
            this.btn_保存.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_保存.Name = "btn_保存";
            this.btn_保存.Size = new System.Drawing.Size(52, 22);
            this.btn_保存.Text = "保存";
            this.btn_保存.Click += new System.EventHandler(this.btn_保存_Click);
            // 
            // btn_关闭
            // 
            this.btn_关闭.Image = ((System.Drawing.Image)(resources.GetObject("btn_关闭.Image")));
            this.btn_关闭.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_关闭.Name = "btn_关闭";
            this.btn_关闭.Size = new System.Drawing.Size(52, 22);
            this.btn_关闭.Text = "关闭";
            this.btn_关闭.Click += new System.EventHandler(this.btn_关闭_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(939, 236);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // F_心电图显示
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 261);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_心电图显示";
            this.Text = "F_心电图显示";
            this.Load += new System.EventHandler(this.F_心电图显示_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_保存;
        private System.Windows.Forms.ToolStripButton btn_关闭;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}