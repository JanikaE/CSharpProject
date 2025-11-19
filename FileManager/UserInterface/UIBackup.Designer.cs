namespace FileManager.UserInterface
{
    partial class UIBackup
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panelPathPairs = new System.Windows.Forms.Panel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            buttonAdd = new System.Windows.Forms.Button();
            checkBoxIsShowIgnore = new System.Windows.Forms.CheckBox();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            buttonExec = new System.Windows.Forms.Button();
            comboBoxPolicy = new System.Windows.Forms.ComboBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panelPathPairs, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
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
            panelPathPairs.Location = new System.Drawing.Point(3, 3);
            panelPathPairs.Name = "panelPathPairs";
            panelPathPairs.Size = new System.Drawing.Size(611, 409);
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
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 418);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new System.Drawing.Size(611, 60);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            buttonAdd.Location = new System.Drawing.Point(520, 14);
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
            checkBoxIsShowIgnore.Location = new System.Drawing.Point(3, 19);
            checkBoxIsShowIgnore.Name = "checkBoxIsShowIgnore";
            checkBoxIsShowIgnore.Size = new System.Drawing.Size(87, 21);
            checkBoxIsShowIgnore.TabIndex = 2;
            checkBoxIsShowIgnore.Text = "显示忽略项";
            checkBoxIsShowIgnore.UseVisualStyleBackColor = true;
            checkBoxIsShowIgnore.CheckedChanged += CheckBoxIsShowIgnore_CheckedChanged;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel3.Controls.Add(buttonExec, 2, 0);
            tableLayoutPanel3.Controls.Add(comboBoxPolicy, 0, 0);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(3, 484);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new System.Drawing.Size(611, 54);
            tableLayoutPanel3.TabIndex = 5;
            // 
            // buttonExec
            // 
            buttonExec.Anchor = System.Windows.Forms.AnchorStyles.Right;
            buttonExec.Location = new System.Drawing.Point(520, 11);
            buttonExec.Name = "buttonExec";
            buttonExec.Size = new System.Drawing.Size(88, 32);
            buttonExec.TabIndex = 2;
            buttonExec.Text = "执行备份";
            buttonExec.UseVisualStyleBackColor = true;
            buttonExec.Click += ButtonExec_Click;
            // 
            // comboBoxPolicy
            // 
            comboBoxPolicy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            comboBoxPolicy.FormattingEnabled = true;
            comboBoxPolicy.Location = new System.Drawing.Point(3, 14);
            comboBoxPolicy.Name = "comboBoxPolicy";
            comboBoxPolicy.Size = new System.Drawing.Size(121, 25);
            comboBoxPolicy.TabIndex = 3;
            comboBoxPolicy.SelectedIndexChanged += ComboBoxPolicy_SelectedIndexChanged;
            // 
            // UIBackup
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UIBackup";
            Size = new System.Drawing.Size(617, 541);
            Resize += BackupPanel_Resize;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelPathPairs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.CheckBox checkBoxIsShowIgnore;
        private System.Windows.Forms.Button buttonExec;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox comboBoxPolicy;
    }
}