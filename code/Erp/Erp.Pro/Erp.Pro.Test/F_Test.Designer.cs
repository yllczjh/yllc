﻿namespace Erp.Pro.Test
{
    partial class F_Test
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
            this.u_列表控件1 = new Erp.Tools.Control.U_列表控件();
            this.SuspendLayout();
            // 
            // u_列表控件1
            // 
            this.u_列表控件1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.u_列表控件1.Location = new System.Drawing.Point(0, 0);
            this.u_列表控件1.Name = "u_列表控件1";
            this.u_列表控件1.Size = new System.Drawing.Size(872, 304);
            this.u_列表控件1.TabIndex = 0;
            // 
            // F_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 304);
            this.Controls.Add(this.u_列表控件1);
            this.Name = "F_Test";
            this.Text = "F_Test";
            this.Load += new System.EventHandler(this.F_Test_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Tools.Control.U_列表控件 u_列表控件1;
    }
}