namespace Diary
{
    partial class InvalidForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ListBoxInvalid = new ListBox();
            SuspendLayout();
            // 
            // ListBoxInvalid
            // 
            ListBoxInvalid.FormattingEnabled = true;
            ListBoxInvalid.ItemHeight = 17;
            ListBoxInvalid.Location = new Point(12, 12);
            ListBoxInvalid.Name = "ListBoxInvalid";
            ListBoxInvalid.Size = new Size(180, 293);
            ListBoxInvalid.TabIndex = 0;
            // 
            // InvalidForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(204, 316);
            Controls.Add(ListBoxInvalid);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InvalidForm";
            Text = "非法文件名";
            ResumeLayout(false);
        }

        #endregion

        private ListBox ListBoxInvalid;
    }
}