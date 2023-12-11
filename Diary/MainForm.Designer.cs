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
            RichTextBox = new RichTextBox();
            ListBoxFile = new ListBox();
            ComboBoxYear = new ComboBox();
            ComboBoxMonth = new ComboBox();
            ButtonInvalid = new Button();
            SuspendLayout();
            // 
            // RichTextBox
            // 
            RichTextBox.Enabled = false;
            RichTextBox.Location = new Point(210, 78);
            RichTextBox.Name = "RichTextBox";
            RichTextBox.ReadOnly = true;
            RichTextBox.Size = new Size(521, 338);
            RichTextBox.TabIndex = 1;
            RichTextBox.Text = "";
            // 
            // ListBoxFile
            // 
            ListBoxFile.FormattingEnabled = true;
            ListBoxFile.ItemHeight = 17;
            ListBoxFile.Location = new Point(30, 78);
            ListBoxFile.Name = "ListBoxFile";
            ListBoxFile.Size = new Size(160, 293);
            ListBoxFile.TabIndex = 2;
            ListBoxFile.SelectedIndexChanged += ListBoxFile_SelectedIndexChanged;
            // 
            // ComboBoxYear
            // 
            ComboBoxYear.FormattingEnabled = true;
            ComboBoxYear.Location = new Point(30, 33);
            ComboBoxYear.Name = "ComboBoxYear";
            ComboBoxYear.Size = new Size(121, 25);
            ComboBoxYear.TabIndex = 3;
            ComboBoxYear.SelectionChangeCommitted += ComboBoxYear_SelectionChangeCommitted;
            // 
            // ComboBoxMonth
            // 
            ComboBoxMonth.FormattingEnabled = true;
            ComboBoxMonth.Location = new Point(157, 33);
            ComboBoxMonth.Name = "ComboBoxMonth";
            ComboBoxMonth.Size = new Size(121, 25);
            ComboBoxMonth.TabIndex = 4;
            ComboBoxMonth.SelectionChangeCommitted += ComboBoxMonth_SelectionChangeCommitted;
            // 
            // ButtonInvalid
            // 
            ButtonInvalid.Location = new Point(43, 387);
            ButtonInvalid.Name = "ButtonInvalid";
            ButtonInvalid.Size = new Size(135, 30);
            ButtonInvalid.TabIndex = 5;
            ButtonInvalid.Text = "非法文件名";
            ButtonInvalid.UseVisualStyleBackColor = true;
            ButtonInvalid.Click += ButtonInvalid_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(762, 443);
            Controls.Add(ButtonInvalid);
            Controls.Add(ComboBoxMonth);
            Controls.Add(ComboBoxYear);
            Controls.Add(ListBoxFile);
            Controls.Add(RichTextBox);
            Name = "MainForm";
            Text = "Diary";
            Resize += MainForm_Resize;
            ResumeLayout(false);
        }

        #endregion
        private RichTextBox RichTextBox;
        private ListBox ListBoxFile;
        private ComboBox ComboBoxYear;
        private ComboBox ComboBoxMonth;
        private Button ButtonInvalid;
    }
}