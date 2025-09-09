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
            buttonExec = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // buttonExec
            // 
            buttonExec.Location = new System.Drawing.Point(169, 309);
            buttonExec.Name = "buttonExec";
            buttonExec.Size = new System.Drawing.Size(75, 23);
            buttonExec.TabIndex = 0;
            buttonExec.Text = "执行删除";
            buttonExec.UseVisualStyleBackColor = true;
            buttonExec.Click += ButtonExec_Click;
            // 
            // UIDelete
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(buttonExec);
            Name = "UIDelete";
            Size = new System.Drawing.Size(261, 350);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button buttonExec;
    }
}
