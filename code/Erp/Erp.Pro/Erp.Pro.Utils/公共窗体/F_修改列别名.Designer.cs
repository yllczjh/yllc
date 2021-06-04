namespace Erp.Pro.Utils.公共窗体
{
    partial class F_修改列别名
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_列别名 = new System.Windows.Forms.TextBox();
            this.btn_确定 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // txt_列别名
            // 
            this.txt_列别名.Location = new System.Drawing.Point(28, 33);
            this.txt_列别名.Name = "txt_列别名";
            this.txt_列别名.Size = new System.Drawing.Size(202, 21);
            this.txt_列别名.TabIndex = 1;
            // 
            // btn_确定
            // 
            this.btn_确定.Location = new System.Drawing.Point(155, 69);
            this.btn_确定.Name = "btn_确定";
            this.btn_确定.Size = new System.Drawing.Size(75, 23);
            this.btn_确定.TabIndex = 2;
            this.btn_确定.Text = "确定";
            this.btn_确定.UseVisualStyleBackColor = true;
            this.btn_确定.Click += new System.EventHandler(this.btn_确定_Click);
            // 
            // F_修改列别名
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 107);
            this.Controls.Add(this.btn_确定);
            this.Controls.Add(this.txt_列别名);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "F_修改列别名";
            this.Text = "修改列别名";
            this.Load += new System.EventHandler(this.F_修改列别名_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_列别名;
        private System.Windows.Forms.Button btn_确定;
    }
}