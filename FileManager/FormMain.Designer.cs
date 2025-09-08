namespace FileManager
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panelPathPairs = new System.Windows.Forms.Panel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            buttonAdd = new System.Windows.Forms.Button();
            checkBoxIsShowIgnore = new System.Windows.Forms.CheckBox();
            buttonExec = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panelPathPairs, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(buttonExec, 0, 2);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.22881F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.77119F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanel1.Size = new System.Drawing.Size(617, 541);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelPathPairs
            // 
            panelPathPairs.AutoScroll = true;
            panelPathPairs.Dock = System.Windows.Forms.DockStyle.Fill;
            panelPathPairs.Location = new System.Drawing.Point(4, 5);
            panelPathPairs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            panelPathPairs.Name = "panelPathPairs";
            panelPathPairs.Size = new System.Drawing.Size(609, 405);
            panelPathPairs.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(buttonAdd, 1, 0);
            tableLayoutPanel2.Controls.Add(checkBoxIsShowIgnore, 0, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 417);
            tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new System.Drawing.Size(611, 62);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            buttonAdd.Location = new System.Drawing.Point(519, 15);
            buttonAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(88, 32);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "添加路径";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += ButtonAdd_Click;
            // 
            // checkBoxIsShowIgnore
            // 
            checkBoxIsShowIgnore.Anchor = System.Windows.Forms.AnchorStyles.Left;
            checkBoxIsShowIgnore.AutoSize = true;
            checkBoxIsShowIgnore.Location = new System.Drawing.Point(3, 20);
            checkBoxIsShowIgnore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            checkBoxIsShowIgnore.Name = "checkBoxIsShowIgnore";
            checkBoxIsShowIgnore.Size = new System.Drawing.Size(87, 21);
            checkBoxIsShowIgnore.TabIndex = 2;
            checkBoxIsShowIgnore.Text = "显示忽略项";
            checkBoxIsShowIgnore.UseVisualStyleBackColor = true;
            checkBoxIsShowIgnore.CheckedChanged += CheckBoxIsShowIgnore_CheckedChanged;
            // 
            // buttonExec
            // 
            buttonExec.Anchor = System.Windows.Forms.AnchorStyles.Right;
            buttonExec.Location = new System.Drawing.Point(525, 495);
            buttonExec.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            buttonExec.Name = "buttonExec";
            buttonExec.Size = new System.Drawing.Size(88, 32);
            buttonExec.TabIndex = 2;
            buttonExec.Text = "执行备份";
            buttonExec.UseVisualStyleBackColor = true;
            buttonExec.Click += ButtonExec_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(631, 577);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Location = new System.Drawing.Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(623, 547);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "备份";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new System.Drawing.Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(623, 547);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "删除";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(631, 577);
            Controls.Add(tabControl1);
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormMain";
            Text = "FileManager";
            Resize += FormMain_Resize;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Panel panelPathPairs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonExec;
        private System.Windows.Forms.CheckBox checkBoxIsShowIgnore;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

