using System.Windows.Forms;

namespace AutoUpdateTool
{
    partial class UpdateForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            probar = new ProgressBar();
            txtlab = new Label();
            titlePanel = new Panel();
            titleLabel = new Label();
            titlePanel.SuspendLayout();
            SuspendLayout();
            // 
            // probar
            // 
            probar.Location = new System.Drawing.Point(10, 73);
            probar.Name = "probar";
            probar.Size = new System.Drawing.Size(343, 23);
            probar.Style = ProgressBarStyle.Continuous;
            probar.TabIndex = 3;
            // 
            // txtlab
            // 
            txtlab.AutoSize = true;
            txtlab.Location = new System.Drawing.Point(10, 44);
            txtlab.Name = "txtlab";
            txtlab.Size = new System.Drawing.Size(132, 17);
            txtlab.TabIndex = 2;
            txtlab.Text = "正在检查更新, 请稍后...";
            // 
            // titlePanel
            // 
            titlePanel.BackColor = System.Drawing.Color.FromArgb(10, 112, 176);
            titlePanel.Controls.Add(titleLabel);
            titlePanel.Location = new System.Drawing.Point(0, 0);
            titlePanel.Name = "titlePanel";
            titlePanel.Size = new System.Drawing.Size(363, 33);
            titlePanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            titleLabel.ForeColor = System.Drawing.Color.White;
            titleLabel.Location = new System.Drawing.Point(10, 5);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(122, 26);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "新版本更新";
            // 
            // UpdateForm
            // 
            ClientSize = new System.Drawing.Size(363, 105);
            Controls.Add(titlePanel);
            Controls.Add(txtlab);
            Controls.Add(probar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "UpdateForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "新版本更新";
            titlePanel.ResumeLayout(false);
            titlePanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private ProgressBar probar;
        private Label txtlab;
        private Panel titlePanel;
        private Label titleLabel;

        #endregion
    }
}
