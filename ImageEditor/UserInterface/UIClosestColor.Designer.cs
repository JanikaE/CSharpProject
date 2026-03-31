namespace ImageEditor.UserInterface
{
    partial class UIClosestColor
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
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            comboBox = new System.Windows.Forms.ComboBox();
            buttonApply = new System.Windows.Forms.Button();
            panel = new System.Windows.Forms.Panel();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(comboBox, 0, 0);
            tableLayoutPanel3.Controls.Add(buttonApply, 0, 2);
            tableLayoutPanel3.Controls.Add(panel, 0, 1);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanel3.Size = new System.Drawing.Size(270, 399);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // comboBox
            // 
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.FormattingEnabled = true;
            comboBox.Location = new System.Drawing.Point(3, 3);
            comboBox.Name = "comboBox";
            comboBox.Size = new System.Drawing.Size(264, 25);
            comboBox.TabIndex = 0;
            // 
            // buttonApply
            // 
            buttonApply.Dock = System.Windows.Forms.DockStyle.Right;
            buttonApply.Location = new System.Drawing.Point(192, 372);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new System.Drawing.Size(75, 24);
            buttonApply.TabIndex = 1;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += ButtonApply_Click;
            // 
            // panel
            // 
            panel.BackColor = System.Drawing.SystemColors.Info;
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.Location = new System.Drawing.Point(3, 33);
            panel.Name = "panel";
            panel.Size = new System.Drawing.Size(264, 333);
            panel.TabIndex = 2;
            // 
            // UIClosestColor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel3);
            Name = "UIClosestColor";
            Size = new System.Drawing.Size(270, 399);
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Panel panel;
    }
}
