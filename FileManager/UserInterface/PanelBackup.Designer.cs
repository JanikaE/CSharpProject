namespace FileManager.UserInterface
{
    partial class PanelBackup
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelPathPairs = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.checkBoxIsShowIgnore = new System.Windows.Forms.CheckBox();
            this.buttonExec = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.22881F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.77119F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(617, 541);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelPathPairs
            // 
            this.panelPathPairs.AutoScroll = true;
            this.panelPathPairs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPathPairs.Location = new System.Drawing.Point(3, 3);
            this.panelPathPairs.Name = "panelPathPairs";
            this.panelPathPairs.Size = new System.Drawing.Size(611, 405);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 414);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(611, 62);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonAdd.Location = new System.Drawing.Point(519, 15);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(88, 32);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "添加路径";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // checkBoxIsShowIgnore
            // 
            this.checkBoxIsShowIgnore.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxIsShowIgnore.AutoSize = true;
            this.checkBoxIsShowIgnore.Location = new System.Drawing.Point(3, 20);
            this.checkBoxIsShowIgnore.Name = "checkBoxIsShowIgnore";
            this.checkBoxIsShowIgnore.Size = new System.Drawing.Size(87, 21);
            this.checkBoxIsShowIgnore.TabIndex = 2;
            this.checkBoxIsShowIgnore.Text = "显示忽略项";
            this.checkBoxIsShowIgnore.UseVisualStyleBackColor = true;
            this.checkBoxIsShowIgnore.CheckedChanged += new System.EventHandler(this.CheckBoxIsShowIgnore_CheckedChanged);
            // 
            // buttonExec
            // 
            this.buttonExec.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonExec.Location = new System.Drawing.Point(525, 495);
            this.buttonExec.Name = "buttonExec";
            this.buttonExec.Size = new System.Drawing.Size(88, 32);
            this.buttonExec.TabIndex = 2;
            this.buttonExec.Text = "执行备份";
            this.buttonExec.UseVisualStyleBackColor = true;
            this.buttonExec.Click += new System.EventHandler(this.ButtonExec_Click);
            // 
            // BackupPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BackupPanel";
            this.Size = new System.Drawing.Size(617, 541);
            this.Resize += new System.EventHandler(this.BackupPanel_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelPathPairs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.CheckBox checkBoxIsShowIgnore;
        private System.Windows.Forms.Button buttonExec;
    }
}