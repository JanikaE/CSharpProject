namespace FileManager.Controls
{
    partial class UserControlDeletePattern
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            textBoxSourcePath = new WinFormUtils.Controls.TextBoxFolderBrowser();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            textBoxMatchFile = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            textBoxMatchFolder = new System.Windows.Forms.TextBox();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            buttonDelete = new System.Windows.Forms.Button();
            checkBoxIgnoreCase = new System.Windows.Forms.CheckBox();
            checkBoxFullMatch = new System.Windows.Forms.CheckBox();
            buttonExec = new System.Windows.Forms.Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(textBoxSourcePath, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new System.Drawing.Size(449, 115);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // textBoxSourcePath
            // 
            textBoxSourcePath.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxSourcePath.Location = new System.Drawing.Point(3, 3);
            textBoxSourcePath.Name = "textBoxSourcePath";
            textBoxSourcePath.Size = new System.Drawing.Size(443, 32);
            textBoxSourcePath.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel2.Controls.Add(textBoxMatchFile, 3, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(label2, 2, 0);
            tableLayoutPanel2.Controls.Add(textBoxMatchFolder, 1, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 41);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(443, 32);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // textBoxMatchFile
            // 
            textBoxMatchFile.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxMatchFile.Location = new System.Drawing.Point(333, 3);
            textBoxMatchFile.Name = "textBoxMatchFile";
            textBoxMatchFile.Size = new System.Drawing.Size(107, 23);
            textBoxMatchFile.TabIndex = 3;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(23, 7);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(84, 17);
            label1.TabIndex = 0;
            label1.Text = "MatchFolder:";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(261, 7);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(66, 17);
            label2.TabIndex = 1;
            label2.Text = "MatchFile:";
            // 
            // textBoxMatchFolder
            // 
            textBoxMatchFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxMatchFolder.Location = new System.Drawing.Point(113, 3);
            textBoxMatchFolder.Name = "textBoxMatchFolder";
            textBoxMatchFolder.Size = new System.Drawing.Size(104, 23);
            textBoxMatchFolder.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.1666679F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.1666679F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.8333359F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.833334F));
            tableLayoutPanel3.Controls.Add(buttonDelete, 2, 0);
            tableLayoutPanel3.Controls.Add(checkBoxIgnoreCase, 1, 0);
            tableLayoutPanel3.Controls.Add(checkBoxFullMatch, 0, 0);
            tableLayoutPanel3.Controls.Add(buttonExec, 3, 0);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(3, 79);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new System.Drawing.Size(443, 33);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // buttonDelete
            // 
            buttonDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonDelete.Location = new System.Drawing.Point(261, 3);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new System.Drawing.Size(86, 27);
            buttonDelete.TabIndex = 3;
            buttonDelete.Text = "删除配置";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // checkBoxIgnoreCase
            // 
            checkBoxIgnoreCase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            checkBoxIgnoreCase.AutoSize = true;
            checkBoxIgnoreCase.Location = new System.Drawing.Point(132, 6);
            checkBoxIgnoreCase.Name = "checkBoxIgnoreCase";
            checkBoxIgnoreCase.Size = new System.Drawing.Size(94, 21);
            checkBoxIgnoreCase.TabIndex = 1;
            checkBoxIgnoreCase.Text = "IgnoreCase";
            checkBoxIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // checkBoxFullMatch
            // 
            checkBoxFullMatch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            checkBoxFullMatch.AutoSize = true;
            checkBoxFullMatch.Location = new System.Drawing.Point(3, 6);
            checkBoxFullMatch.Name = "checkBoxFullMatch";
            checkBoxFullMatch.Size = new System.Drawing.Size(82, 21);
            checkBoxFullMatch.TabIndex = 0;
            checkBoxFullMatch.Text = "FullMatch";
            checkBoxFullMatch.UseVisualStyleBackColor = true;
            // 
            // buttonExec
            // 
            buttonExec.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonExec.Location = new System.Drawing.Point(353, 3);
            buttonExec.Name = "buttonExec";
            buttonExec.Size = new System.Drawing.Size(87, 27);
            buttonExec.TabIndex = 2;
            buttonExec.Text = "执行配置";
            buttonExec.UseVisualStyleBackColor = true;
            buttonExec.Click += ButtonExec_Click;
            // 
            // UserControlDeletePattern
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UserControlDeletePattern";
            Size = new System.Drawing.Size(449, 115);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private WinFormUtils.Controls.TextBoxFolderBrowser textBoxSourcePath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMatchFile;
        private System.Windows.Forms.TextBox textBoxMatchFolder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox checkBoxFullMatch;
        private System.Windows.Forms.CheckBox checkBoxIgnoreCase;
        private System.Windows.Forms.Button buttonExec;
        private System.Windows.Forms.Button buttonDelete;
    }
}
