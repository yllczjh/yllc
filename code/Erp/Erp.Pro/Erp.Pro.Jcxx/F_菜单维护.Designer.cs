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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_菜单维护));
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu();
            this.tree_菜单 = new System.Windows.Forms.TreeView();
            this.imageList2 = new System.Windows.Forms.ImageList();
            this.u_菜单信息维护 = new Erp.Pro.Utils.自定义控件.U_通用列表编辑();
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
            this.tree_菜单.ImageIndex = 3;
            this.tree_菜单.ImageList = this.imageList2;
            this.tree_菜单.Location = new System.Drawing.Point(0, 0);
            this.tree_菜单.Name = "tree_菜单";
            this.tree_菜单.SelectedImageIndex = 4;
            this.tree_菜单.Size = new System.Drawing.Size(200, 394);
            this.tree_菜单.TabIndex = 2;
            this.tree_菜单.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_菜单_AfterSelect);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "folder_add.png");
            this.imageList2.Images.SetKeyName(1, "folder_delete.png");
            this.imageList2.Images.SetKeyName(2, "folder_edit.png");
            this.imageList2.Images.SetKeyName(3, "folder.png");
            this.imageList2.Images.SetKeyName(4, "folder_go.png");
            // 
            // u_菜单信息维护
            // 
            this.u_菜单信息维护.Dock = System.Windows.Forms.DockStyle.Fill;
            this.u_菜单信息维护.Location = new System.Drawing.Point(200, 0);
            this.u_菜单信息维护.Name = "u_菜单信息维护";
            this.u_菜单信息维护.P_控件参数 = null;
            this.u_菜单信息维护.P_每行显示列数 = 2;
            this.u_菜单信息维护.P_页面名称 = null;
            this.u_菜单信息维护.Size = new System.Drawing.Size(784, 394);
            this.u_菜单信息维护.TabIndex = 3;
            this.u_菜单信息维护.新增处理 += new System.EventHandler(this.u_菜单信息维护_新增处理);
            this.u_菜单信息维护.修改处理 += new System.EventHandler(this.u_菜单信息维护_修改处理);
            // 
            // F_菜单维护
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 394);
            this.Controls.Add(this.u_菜单信息维护);
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
        private Utils.自定义控件.U_通用列表编辑 u_菜单信息维护;
        private System.Windows.Forms.ImageList imageList2;
    }
}