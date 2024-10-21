namespace BackupTool
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.panelPathPairs = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonExec = new System.Windows.Forms.Button();
            this.checkBoxIsShowIgnore = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panelPathPairs, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonExec, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.83051F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.16949F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(721, 509);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonAdd.Location = new System.Drawing.Point(611, 7);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(100, 28);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "添加路径";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // panelPathPairs
            // 
            this.panelPathPairs.AutoScroll = true;
            this.panelPathPairs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPathPairs.Location = new System.Drawing.Point(4, 4);
            this.panelPathPairs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelPathPairs.Name = "panelPathPairs";
            this.panelPathPairs.Size = new System.Drawing.Size(713, 416);
            this.panelPathPairs.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonAdd, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxIsShowIgnore, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 427);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(715, 42);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // buttonExec
            // 
            this.buttonExec.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonExec.Location = new System.Drawing.Point(617, 476);
            this.buttonExec.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExec.Name = "buttonExec";
            this.buttonExec.Size = new System.Drawing.Size(100, 28);
            this.buttonExec.TabIndex = 2;
            this.buttonExec.Text = "执行备份";
            this.buttonExec.UseVisualStyleBackColor = true;
            this.buttonExec.Click += new System.EventHandler(this.ButtonExec_Click);
            // 
            // checkBoxIsShowIgnore
            // 
            this.checkBoxIsShowIgnore.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxIsShowIgnore.AutoSize = true;
            this.checkBoxIsShowIgnore.Location = new System.Drawing.Point(3, 11);
            this.checkBoxIsShowIgnore.Name = "checkBoxIsShowIgnore";
            this.checkBoxIsShowIgnore.Size = new System.Drawing.Size(104, 19);
            this.checkBoxIsShowIgnore.TabIndex = 2;
            this.checkBoxIsShowIgnore.Text = "显示忽略项";
            this.checkBoxIsShowIgnore.UseVisualStyleBackColor = true;
            this.checkBoxIsShowIgnore.CheckedChanged += new System.EventHandler(this.CheckBoxIsShowIgnore_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 509);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "备份工具";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Panel panelPathPairs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonExec;
        private System.Windows.Forms.CheckBox checkBoxIsShowIgnore;
    }
}

