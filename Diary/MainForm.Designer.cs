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
            TextBox = new RichTextBox();
            ListBoxFile = new ListBox();
            ComboBoxYear = new ComboBox();
            ComboBoxMonth = new ComboBox();
            ButtonInvalid = new Button();
            SuspendLayout();
            // 
            // TextBox
            // 
            TextBox.Enabled = false;
            TextBox.Location = new Point(234, 78);
            TextBox.Name = "TextBox";
            TextBox.ReadOnly = true;
            TextBox.Size = new Size(521, 338);
            TextBox.TabIndex = 1;
            TextBox.Text = "";
            // 
            // ListBoxFile
            // 
            ListBoxFile.FormattingEnabled = true;
            ListBoxFile.ItemHeight = 17;
            ListBoxFile.Location = new Point(30, 78);
            ListBoxFile.Name = "ListBoxFile";
            ListBoxFile.Size = new Size(139, 310);
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
            ComboBoxMonth.Location = new Point(182, 33);
            ComboBoxMonth.Name = "ComboBoxMonth";
            ComboBoxMonth.Size = new Size(121, 25);
            ComboBoxMonth.TabIndex = 4;
            ComboBoxMonth.SelectionChangeCommitted += ComboBoxMonth_SelectionChangeCommitted;
            // 
            // ButtonInvalid
            // 
            ButtonInvalid.Location = new Point(50, 394);
            ButtonInvalid.Name = "ButtonInvalid";
            ButtonInvalid.Size = new Size(91, 23);
            ButtonInvalid.TabIndex = 5;
            ButtonInvalid.Text = "非法文件名";
            ButtonInvalid.UseVisualStyleBackColor = true;
            ButtonInvalid.Click += ButtonInvalid_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ButtonInvalid);
            Controls.Add(ComboBoxMonth);
            Controls.Add(ComboBoxYear);
            Controls.Add(ListBoxFile);
            Controls.Add(TextBox);
            Name = "MainForm";
            Text = "Diary";
            ResumeLayout(false);
        }

        #endregion
        private RichTextBox TextBox;
        private ListBox ListBoxFile;
        private ComboBox ComboBoxYear;
        private ComboBox ComboBoxMonth;
        private Button ButtonInvalid;
    }
}