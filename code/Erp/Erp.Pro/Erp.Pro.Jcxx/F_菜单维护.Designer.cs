namespace Erp.Pro.Jcxx
{
    partial class F_菜单维护
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
            this.components = new System.ComponentModel.Container();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.tree_菜单 = new System.Windows.Forms.TreeView();
            this.u_通用列表编辑1 = new Erp.Pro.Utils.自定义控件.U_通用列表编辑();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // tree_菜单
            // 
            this.tree_菜单.Dock = System.Windows.Forms.DockStyle.Left;
            this.tree_菜单.Location = new System.Drawing.Point(0, 0);
            this.tree_菜单.Name = "tree_菜单";
            this.tree_菜单.Size = new System.Drawing.Size(200, 394);
            this.tree_菜单.TabIndex = 2;
            // 
            // u_通用列表编辑1
            // 
            this.u_通用列表编辑1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.u_通用列表编辑1.Location = new System.Drawing.Point(200, 0);
            this.u_通用列表编辑1.Name = "u_通用列表编辑1";
            this.u_通用列表编辑1.P_控件参数 = null;
            this.u_通用列表编辑1.P_每行显示列数 = 2;
            this.u_通用列表编辑1.P_页面名称 = null;
            this.u_通用列表编辑1.Size = new System.Drawing.Size(784, 394);
            this.u_通用列表编辑1.TabIndex = 3;
            // 
            // F_菜单维护
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 394);
            this.Controls.Add(this.u_通用列表编辑1);
            this.Controls.Add(this.tree_菜单);
            this.Name = "F_菜单维护";
            this.Text = "F_菜单编辑";
            this.Load += new System.EventHandler(this.F_菜单编辑_Load);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private System.Windows.Forms.TreeView tree_菜单;
        private Utils.自定义控件.U_通用列表编辑 u_通用列表编辑1;
    }
}