namespace Diary
{
    partial class MainForm
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
            BodyTextBox = new RichTextBox();
            FileListBox = new ListBox();
            SuspendLayout();
            // 
            // BodyTextBox
            // 
            BodyTextBox.Enabled = false;
            BodyTextBox.Location = new Point(330, 84);
            BodyTextBox.Name = "BodyTextBox";
            BodyTextBox.ReadOnly = true;
            BodyTextBox.Size = new Size(458, 338);
            BodyTextBox.TabIndex = 1;
            BodyTextBox.Text = "";
            // 
            // FileListBox
            // 
            FileListBox.FormattingEnabled = true;
            FileListBox.ItemHeight = 17;
            FileListBox.Location = new Point(12, 84);
            FileListBox.Name = "FileListBox";
            FileListBox.Size = new Size(139, 344);
            FileListBox.TabIndex = 2;
            FileListBox.SelectedIndexChanged += FileListBox_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(FileListBox);
            Controls.Add(BodyTextBox);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion
        private RichTextBox BodyTextBox;
        private ListBox FileListBox;
    }
}