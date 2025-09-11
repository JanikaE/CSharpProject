namespace FileManager.UserInterface
{
    partial class UIDelete
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
            panelDeletePatterns = new System.Windows.Forms.Panel();
            buttonAdd = new System.Windows.Forms.Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(buttonAdd, 0, 1);
            tableLayoutPanel1.Controls.Add(panelDeletePatterns, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.14286F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.8571424F));
            tableLayoutPanel1.Size = new System.Drawing.Size(261, 350);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelDeletePatterns
            // 
            panelDeletePatterns.Dock = System.Windows.Forms.DockStyle.Fill;
            panelDeletePatterns.Location = new System.Drawing.Point(3, 3);
            panelDeletePatterns.Name = "panelDeletePatterns";
            panelDeletePatterns.Size = new System.Drawing.Size(255, 306);
            panelDeletePatterns.TabIndex = 0;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            buttonAdd.Location = new System.Drawing.Point(187, 318);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(71, 26);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "新增配置";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += ButtonAdd_Click;
            // 
            // UIDelete
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UIDelete";
            Size = new System.Drawing.Size(261, 350);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelDeletePatterns;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonSave;
    }
}
