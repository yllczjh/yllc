namespace Erp.Main
{
    partial class F_Login
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
            this.sib_登录 = new DevExpress.XtraEditors.SimpleButton();
            this.sib_取消 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.密码 = new System.Windows.Forms.Label();
            this.用户 = new System.Windows.Forms.Label();
            this.txt_密码 = new DevExpress.XtraEditors.TextEdit();
            this.txt_用户 = new DevExpress.XtraEditors.TextEdit();
            this.chk_记住密码 = new DevExpress.XtraEditors.CheckEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_密码.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_用户.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_记住密码.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sib_登录
            // 
            this.sib_登录.Location = new System.Drawing.Point(143, 173);
            this.sib_登录.Name = "sib_登录";
            this.sib_登录.Size = new System.Drawing.Size(80, 27);
            this.sib_登录.TabIndex = 0;
            this.sib_登录.Text = "登录";
            this.sib_登录.Click += new System.EventHandler(this.sib_登录_Click);
            // 
            // sib_取消
            // 
            this.sib_取消.Location = new System.Drawing.Point(231, 173);
            this.sib_取消.Name = "sib_取消";
            this.sib_取消.Size = new System.Drawing.Size(80, 27);
            this.sib_取消.TabIndex = 1;
            this.sib_取消.Text = "取消";
            this.sib_取消.Click += new System.EventHandler(this.sib_取消_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.密码);
            this.panelControl1.Controls.Add(this.用户);
            this.panelControl1.Controls.Add(this.txt_密码);
            this.panelControl1.Controls.Add(this.txt_用户);
            this.panelControl1.Location = new System.Drawing.Point(46, 57);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(265, 100);
            this.panelControl1.TabIndex = 2;
            // 
            // 密码
            // 
            this.密码.AutoSize = true;
            this.密码.Location = new System.Drawing.Point(44, 62);
            this.密码.Name = "密码";
            this.密码.Size = new System.Drawing.Size(31, 14);
            this.密码.TabIndex = 3;
            this.密码.Text = "密码";
            // 
            // 用户
            // 
            this.用户.AutoSize = true;
            this.用户.Location = new System.Drawing.Point(44, 24);
            this.用户.Name = "用户";
            this.用户.Size = new System.Drawing.Size(31, 14);
            this.用户.TabIndex = 2;
            this.用户.Text = "用户";
            // 
            // txt_密码
            // 
            this.txt_密码.Location = new System.Drawing.Point(99, 59);
            this.txt_密码.Name = "txt_密码";
            this.txt_密码.Properties.PasswordChar = '*';
            this.txt_密码.Size = new System.Drawing.Size(100, 20);
            this.txt_密码.TabIndex = 1;
            // 
            // txt_用户
            // 
            this.txt_用户.Location = new System.Drawing.Point(99, 21);
            this.txt_用户.Name = "txt_用户";
            this.txt_用户.Size = new System.Drawing.Size(100, 20);
            this.txt_用户.TabIndex = 0;
            // 
            // chk_记住密码
            // 
            this.chk_记住密码.Location = new System.Drawing.Point(46, 176);
            this.chk_记住密码.Name = "chk_记住密码";
            this.chk_记住密码.Properties.Caption = "记住密码";
            this.chk_记住密码.Size = new System.Drawing.Size(75, 19);
            this.chk_记住密码.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Erp.Main.Properties.Resources.login;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 40);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "登录窗体 当前版本号1000";
            // 
            // F_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 231);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chk_记住密码);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.sib_取消);
            this.Controls.Add(this.sib_登录);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_Login";
            this.Text = "F_Login";
            this.Load += new System.EventHandler(this.F_Login_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.F_Login_MouseDown);
            this.MouseEnter += new System.EventHandler(this.F_Login_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.F_Login_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.F_Login_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.F_Login_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_密码.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_用户.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_记住密码.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sib_登录;
        private DevExpress.XtraEditors.SimpleButton sib_取消;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label 密码;
        private System.Windows.Forms.Label 用户;
        private DevExpress.XtraEditors.TextEdit txt_密码;
        private DevExpress.XtraEditors.TextEdit txt_用户;
        private DevExpress.XtraEditors.CheckEdit chk_记住密码;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}