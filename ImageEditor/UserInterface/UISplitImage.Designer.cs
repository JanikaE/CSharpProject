namespace ImageEditor.UserInterface
{
    partial class UISplitImage
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            labelSplitSize = new System.Windows.Forms.Label();
            numericUpDownSplit = new System.Windows.Forms.NumericUpDown();
            labelExportSize = new System.Windows.Forms.Label();
            numericUpDownExport = new System.Windows.Forms.NumericUpDown();
            labelFileName = new System.Windows.Forms.Label();
            textBoxFileName = new System.Windows.Forms.TextBox();
            labelGridInfo = new System.Windows.Forms.Label();
            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            buttonApply = new System.Windows.Forms.Button();
            buttonExport = new System.Windows.Forms.Button();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSplit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownExport).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(labelSplitSize, 0, 0);
            tableLayoutPanel.Controls.Add(numericUpDownSplit, 1, 0);
            tableLayoutPanel.Controls.Add(labelExportSize, 0, 1);
            tableLayoutPanel.Controls.Add(numericUpDownExport, 1, 1);
            tableLayoutPanel.Controls.Add(labelFileName, 0, 2);
            tableLayoutPanel.Controls.Add(textBoxFileName, 1, 2);
            tableLayoutPanel.Controls.Add(labelGridInfo, 0, 3);
            tableLayoutPanel.Controls.Add(flowLayoutPanel, 0, 4);
            tableLayoutPanel.Controls.Add(buttonApply, 0, 5);
            tableLayoutPanel.Controls.Add(buttonExport, 1, 5);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanel.Size = new System.Drawing.Size(176, 408);
            tableLayoutPanel.TabIndex = 0;
            // 
            // labelSplitSize
            // 
            labelSplitSize.AutoSize = true;
            labelSplitSize.Location = new System.Drawing.Point(3, 0);
            labelSplitSize.Name = "labelSplitSize";
            labelSplitSize.Size = new System.Drawing.Size(63, 17);
            labelSplitSize.TabIndex = 0;
            labelSplitSize.Text = "Split Size:";
            // 
            // numericUpDownSplit
            // 
            numericUpDownSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            numericUpDownSplit.Location = new System.Drawing.Point(83, 3);
            numericUpDownSplit.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            numericUpDownSplit.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownSplit.Name = "numericUpDownSplit";
            numericUpDownSplit.Size = new System.Drawing.Size(90, 23);
            numericUpDownSplit.TabIndex = 1;
            numericUpDownSplit.Value = new decimal(new int[] { 120, 0, 0, 0 });
            // 
            // labelExportSize
            // 
            labelExportSize.AutoSize = true;
            labelExportSize.Location = new System.Drawing.Point(3, 30);
            labelExportSize.Name = "labelExportSize";
            labelExportSize.Size = new System.Drawing.Size(50, 30);
            labelExportSize.TabIndex = 2;
            labelExportSize.Text = "Export Size:";
            // 
            // numericUpDownExport
            // 
            numericUpDownExport.Dock = System.Windows.Forms.DockStyle.Fill;
            numericUpDownExport.Location = new System.Drawing.Point(83, 33);
            numericUpDownExport.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            numericUpDownExport.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownExport.Name = "numericUpDownExport";
            numericUpDownExport.Size = new System.Drawing.Size(90, 23);
            numericUpDownExport.TabIndex = 3;
            numericUpDownExport.Value = new decimal(new int[] { 120, 0, 0, 0 });
            // 
            // labelFileName
            // 
            labelFileName.AutoSize = true;
            labelFileName.Location = new System.Drawing.Point(3, 60);
            labelFileName.Name = "labelFileName";
            labelFileName.Size = new System.Drawing.Size(69, 17);
            labelFileName.TabIndex = 4;
            labelFileName.Text = "File Name:";
            // 
            // textBoxFileName
            // 
            textBoxFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxFileName.Location = new System.Drawing.Point(83, 63);
            textBoxFileName.Name = "textBoxFileName";
            textBoxFileName.Size = new System.Drawing.Size(90, 23);
            textBoxFileName.TabIndex = 5;
            textBoxFileName.Text = "测试";
            // 
            // labelGridInfo
            // 
            labelGridInfo.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(labelGridInfo, 2);
            labelGridInfo.Location = new System.Drawing.Point(3, 90);
            labelGridInfo.Name = "labelGridInfo";
            labelGridInfo.Size = new System.Drawing.Size(131, 17);
            labelGridInfo.TabIndex = 6;
            labelGridInfo.Text = "No image processed";
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.AutoScroll = true;
            tableLayoutPanel.SetColumnSpan(flowLayoutPanel, 2);
            flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel.Location = new System.Drawing.Point(3, 117);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new System.Drawing.Size(170, 258);
            flowLayoutPanel.TabIndex = 7;
            // 
            // buttonApply
            // 
            buttonApply.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonApply.Location = new System.Drawing.Point(3, 381);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new System.Drawing.Size(74, 24);
            buttonApply.TabIndex = 8;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += ButtonApply_Click;
            // 
            // buttonExport
            // 
            buttonExport.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonExport.Location = new System.Drawing.Point(83, 381);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new System.Drawing.Size(90, 24);
            buttonExport.TabIndex = 9;
            buttonExport.Text = "Export";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += ButtonExport_Click;
            // 
            // UISplitImage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel);
            Name = "UISplitImage";
            Size = new System.Drawing.Size(176, 408);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSplit).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownExport).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelSplitSize;
        private System.Windows.Forms.NumericUpDown numericUpDownSplit;
        private System.Windows.Forms.Label labelExportSize;
        private System.Windows.Forms.NumericUpDown numericUpDownExport;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label labelGridInfo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonExport;
    }
}
