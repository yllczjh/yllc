namespace Erp.Pro.Jcxx
{
    partial class F_用户信息
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
            this.u_通用列表编辑2 = new Erp.Pro.Utils.自定义控件.U_通用列表编辑();
            this.SuspendLayout();
            // 
            // u_通用列表编辑2
            // 
            this.u_通用列表编辑2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.u_通用列表编辑2.Location = new System.Drawing.Point(0, 0);
            this.u_通用列表编辑2.Name = "u_通用列表编辑2";
            this.u_通用列表编辑2.P_列表页名称 = null;
            this.u_通用列表编辑2.P_控件参数 = null;
            this.u_通用列表编辑2.P_模块名称 = Erp.Server.Init.C_系统参数.E_模块名称.基础业务;
            this.u_通用列表编辑2.P_每行显示列数 = 2;
            this.u_通用列表编辑2.P_编辑页名称 = null;
            this.u_通用列表编辑2.Size = new System.Drawing.Size(615, 261);
            this.u_通用列表编辑2.TabIndex = 0;
            this.u_通用列表编辑2.新增处理 += new System.EventHandler(this.u_通用列表编辑2_新增处理);
            this.u_通用列表编辑2.修改处理 += new System.EventHandler(this.u_通用列表编辑2_修改处理);
            // 
            // F_用户信息
            // 
            this.ClientSize = new System.Drawing.Size(615, 261);
            this.Controls.Add(this.u_通用列表编辑2);
            this.Name = "F_用户信息";
            this.Load += new System.EventHandler(this.F_用户信息_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Utils.自定义控件.U_通用列表编辑 u_通用列表编辑2;
    }
}