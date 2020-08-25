namespace 测试
{
    partial class Form2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpc_合并路径项目 = new System.Windows.Forms.GroupBox();
            this.grv_合并路径阶段项目 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.合并_分类 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.合并_项目内容 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.合并_执行方式 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.合并_变异原因 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.合并_选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_合并路径阶段选择 = new System.Windows.Forms.Button();
            this.grpc_合并路径项目.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv_合并路径阶段项目)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpc_合并路径项目
            // 
            this.grpc_合并路径项目.Controls.Add(this.panel1);
            this.grpc_合并路径项目.Controls.Add(this.grv_合并路径阶段项目);
            this.grpc_合并路径项目.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpc_合并路径项目.Location = new System.Drawing.Point(0, 0);
            this.grpc_合并路径项目.Margin = new System.Windows.Forms.Padding(2);
            this.grpc_合并路径项目.Name = "grpc_合并路径项目";
            this.grpc_合并路径项目.Padding = new System.Windows.Forms.Padding(2);
            this.grpc_合并路径项目.Size = new System.Drawing.Size(852, 247);
            this.grpc_合并路径项目.TabIndex = 13;
            this.grpc_合并路径项目.TabStop = false;
            this.grpc_合并路径项目.Text = "合并路径：老年性白内障";
            // 
            // grv_合并路径阶段项目
            // 
            this.grv_合并路径阶段项目.AllowUserToAddRows = false;
            this.grv_合并路径阶段项目.AllowUserToResizeRows = false;
            this.grv_合并路径阶段项目.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grv_合并路径阶段项目.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grv_合并路径阶段项目.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_合并路径阶段项目.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.合并_分类,
            this.合并_项目内容,
            this.合并_执行方式,
            this.合并_变异原因,
            this.合并_选择});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grv_合并路径阶段项目.DefaultCellStyle = dataGridViewCellStyle2;
            this.grv_合并路径阶段项目.Dock = System.Windows.Forms.DockStyle.Top;
            this.grv_合并路径阶段项目.EnableHeadersVisualStyles = false;
            this.grv_合并路径阶段项目.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grv_合并路径阶段项目.Location = new System.Drawing.Point(2, 16);
            this.grv_合并路径阶段项目.MultiSelect = false;
            this.grv_合并路径阶段项目.Name = "grv_合并路径阶段项目";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grv_合并路径阶段项目.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grv_合并路径阶段项目.RowHeadersVisible = false;
            this.grv_合并路径阶段项目.RowTemplate.Height = 23;
            this.grv_合并路径阶段项目.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grv_合并路径阶段项目.Size = new System.Drawing.Size(848, 186);
            this.grv_合并路径阶段项目.TabIndex = 5;
            this.grv_合并路径阶段项目.Tag = "诊疗字典";
            // 
            // 合并_分类
            // 
            this.合并_分类.DataPropertyName = "分类";
            this.合并_分类.HeaderText = "分类";
            this.合并_分类.Name = "合并_分类";
            this.合并_分类.ReadOnly = true;
            // 
            // 合并_项目内容
            // 
            this.合并_项目内容.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.合并_项目内容.DataPropertyName = "项目内容";
            this.合并_项目内容.HeaderText = "项目内容";
            this.合并_项目内容.Name = "合并_项目内容";
            this.合并_项目内容.ReadOnly = true;
            // 
            // 合并_执行方式
            // 
            this.合并_执行方式.DataPropertyName = "执行方式名称";
            this.合并_执行方式.HeaderText = "执行方式";
            this.合并_执行方式.Name = "合并_执行方式";
            this.合并_执行方式.ReadOnly = true;
            // 
            // 合并_变异原因
            // 
            this.合并_变异原因.DataPropertyName = "变异原因名称";
            this.合并_变异原因.HeaderText = "变异原因";
            this.合并_变异原因.Name = "合并_变异原因";
            this.合并_变异原因.ReadOnly = true;
            this.合并_变异原因.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 合并_选择
            // 
            this.合并_选择.DataPropertyName = "选择";
            this.合并_选择.FalseValue = "False";
            this.合并_选择.HeaderText = "选择";
            this.合并_选择.Name = "合并_选择";
            this.合并_选择.ReadOnly = true;
            this.合并_选择.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.合并_选择.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.合并_选择.TrueValue = "True";
            this.合并_选择.Width = 70;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_合并路径阶段选择);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 202);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 43);
            this.panel1.TabIndex = 8;
            // 
            // btn_合并路径阶段选择
            // 
            this.btn_合并路径阶段选择.Location = new System.Drawing.Point(7, 7);
            this.btn_合并路径阶段选择.Name = "btn_合并路径阶段选择";
            this.btn_合并路径阶段选择.Size = new System.Drawing.Size(130, 23);
            this.btn_合并路径阶段选择.TabIndex = 0;
            this.btn_合并路径阶段选择.Text = "合并路径阶段选择";
            this.btn_合并路径阶段选择.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 247);
            this.Controls.Add(this.grpc_合并路径项目);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Form2";
            this.grpc_合并路径项目.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grv_合并路径阶段项目)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpc_合并路径项目;
        private DevComponents.DotNetBar.Controls.DataGridViewX grv_合并路径阶段项目;
        private System.Windows.Forms.DataGridViewTextBoxColumn 合并_分类;
        private System.Windows.Forms.DataGridViewTextBoxColumn 合并_项目内容;
        private System.Windows.Forms.DataGridViewTextBoxColumn 合并_执行方式;
        private System.Windows.Forms.DataGridViewTextBoxColumn 合并_变异原因;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 合并_选择;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btn_合并路径阶段选择;
    }
}