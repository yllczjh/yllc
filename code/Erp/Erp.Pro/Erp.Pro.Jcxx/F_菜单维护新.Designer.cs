namespace Erp.Pro.Jcxx
{
    partial class F_菜单维护新
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
            this.tree_菜单 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tree_菜单
            // 
            this.tree_菜单.Dock = System.Windows.Forms.DockStyle.Left;
            this.tree_菜单.Location = new System.Drawing.Point(0, 0);
            this.tree_菜单.Name = "tree_菜单";
            this.tree_菜单.Size = new System.Drawing.Size(200, 398);
            this.tree_菜单.TabIndex = 3;
            this.tree_菜单.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_菜单_AfterSelect);
            // 
            // F_菜单维护新
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 398);
            this.Controls.Add(this.tree_菜单);
            this.Name = "F_菜单维护新";
            this.Text = "F_菜单维护新";
            this.Load += new System.EventHandler(this.F_菜单维护新_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tree_菜单;
    }
}