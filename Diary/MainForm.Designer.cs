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
            RichTextBoxShow = new RichTextBox();
            ListBoxFile = new ListBox();
            ComboBoxYear = new ComboBox();
            ComboBoxMonth = new ComboBox();
            ButtonInvalid = new Button();
            TabControl = new TabControl();
            TabPageView = new TabPage();
            ButtonCancel = new Button();
            ButtonSave = new Button();
            ButtonEdit = new Button();
            ButtonRefresh = new Button();
            TabPageAdd = new TabPage();
            ButtonCreate = new Button();
            RichTextBoxInput = new RichTextBox();
            TextBoxTopic = new TextBox();
            DateTimePicker = new DateTimePicker();
            TabControl.SuspendLayout();
            TabPageView.SuspendLayout();
            TabPageAdd.SuspendLayout();
            SuspendLayout();
            // 
            // RichTextBoxShow
            // 
            RichTextBoxShow.Location = new Point(147, 37);
            RichTextBoxShow.Name = "RichTextBoxShow";
            RichTextBoxShow.ReadOnly = true;
            RichTextBoxShow.Size = new Size(438, 344);
            RichTextBoxShow.TabIndex = 1;
            RichTextBoxShow.Text = "";
            // 
            // ListBoxFile
            // 
            ListBoxFile.FormattingEnabled = true;
            ListBoxFile.ItemHeight = 17;
            ListBoxFile.Location = new Point(6, 37);
            ListBoxFile.Name = "ListBoxFile";
            ListBoxFile.Size = new Size(135, 344);
            ListBoxFile.TabIndex = 2;
            ListBoxFile.SelectedIndexChanged += ListBoxFile_SelectedIndexChanged;
            // 
            // ComboBoxYear
            // 
            ComboBoxYear.FormattingEnabled = true;
            ComboBoxYear.Location = new Point(6, 6);
            ComboBoxYear.Name = "ComboBoxYear";
            ComboBoxYear.Size = new Size(121, 25);
            ComboBoxYear.TabIndex = 3;
            ComboBoxYear.SelectionChangeCommitted += ComboBoxYear_SelectionChangeCommitted;
            // 
            // ComboBoxMonth
            // 
            ComboBoxMonth.FormattingEnabled = true;
            ComboBoxMonth.Location = new Point(133, 6);
            ComboBoxMonth.Name = "ComboBoxMonth";
            ComboBoxMonth.Size = new Size(121, 25);
            ComboBoxMonth.TabIndex = 4;
            ComboBoxMonth.SelectionChangeCommitted += ComboBoxMonth_SelectionChangeCommitted;
            // 
            // ButtonInvalid
            // 
            ButtonInvalid.Location = new Point(6, 392);
            ButtonInvalid.Name = "ButtonInvalid";
            ButtonInvalid.Size = new Size(135, 25);
            ButtonInvalid.TabIndex = 5;
            ButtonInvalid.Text = "非法文件名";
            ButtonInvalid.UseVisualStyleBackColor = true;
            ButtonInvalid.Click += ButtonInvalid_Click;
            // 
            // TabControl
            // 
            TabControl.Controls.Add(TabPageView);
            TabControl.Controls.Add(TabPageAdd);
            TabControl.Location = new Point(-1, 0);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(605, 457);
            TabControl.TabIndex = 6;
            // 
            // TabPageView
            // 
            TabPageView.Controls.Add(ButtonCancel);
            TabPageView.Controls.Add(ButtonSave);
            TabPageView.Controls.Add(ButtonEdit);
            TabPageView.Controls.Add(ButtonRefresh);
            TabPageView.Controls.Add(ComboBoxMonth);
            TabPageView.Controls.Add(ButtonInvalid);
            TabPageView.Controls.Add(ComboBoxYear);
            TabPageView.Controls.Add(RichTextBoxShow);
            TabPageView.Controls.Add(ListBoxFile);
            TabPageView.Location = new Point(4, 26);
            TabPageView.Name = "TabPageView";
            TabPageView.Padding = new Padding(3);
            TabPageView.Size = new Size(597, 427);
            TabPageView.TabIndex = 0;
            TabPageView.Text = "查看";
            TabPageView.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            ButtonCancel.Location = new Point(348, 392);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new Size(75, 25);
            ButtonCancel.TabIndex = 9;
            ButtonCancel.Text = "取消";
            ButtonCancel.UseVisualStyleBackColor = true;
            ButtonCancel.Visible = false;
            ButtonCancel.Click += ButtonCancel_Click;
            // 
            // ButtonSave
            // 
            ButtonSave.Location = new Point(511, 392);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(75, 25);
            ButtonSave.TabIndex = 8;
            ButtonSave.Text = "保存";
            ButtonSave.UseVisualStyleBackColor = true;
            ButtonSave.Visible = false;
            ButtonSave.Click += ButtonSave_Click;
            // 
            // ButtonEdit
            // 
            ButtonEdit.Location = new Point(429, 392);
            ButtonEdit.Name = "ButtonEdit";
            ButtonEdit.Size = new Size(75, 25);
            ButtonEdit.TabIndex = 7;
            ButtonEdit.Text = "编辑";
            ButtonEdit.UseVisualStyleBackColor = true;
            ButtonEdit.Click += ButtonEdit_Click;
            // 
            // ButtonRefresh
            // 
            ButtonRefresh.Location = new Point(498, 6);
            ButtonRefresh.Name = "ButtonRefresh";
            ButtonRefresh.Size = new Size(88, 25);
            ButtonRefresh.TabIndex = 6;
            ButtonRefresh.Text = "刷新";
            ButtonRefresh.UseVisualStyleBackColor = true;
            ButtonRefresh.Click += ButtonRefresh_Click;
            // 
            // TabPageAdd
            // 
            TabPageAdd.Controls.Add(ButtonCreate);
            TabPageAdd.Controls.Add(RichTextBoxInput);
            TabPageAdd.Controls.Add(TextBoxTopic);
            TabPageAdd.Controls.Add(DateTimePicker);
            TabPageAdd.Location = new Point(4, 26);
            TabPageAdd.Name = "TabPageAdd";
            TabPageAdd.Padding = new Padding(3);
            TabPageAdd.Size = new Size(597, 427);
            TabPageAdd.TabIndex = 1;
            TabPageAdd.Text = "新增";
            TabPageAdd.UseVisualStyleBackColor = true;
            // 
            // ButtonCreate
            // 
            ButtonCreate.Location = new Point(510, 387);
            ButtonCreate.Name = "ButtonCreate";
            ButtonCreate.Size = new Size(75, 25);
            ButtonCreate.TabIndex = 3;
            ButtonCreate.Text = "保存";
            ButtonCreate.UseVisualStyleBackColor = true;
            ButtonCreate.Click += ButtonCreate_Click;
            // 
            // RichTextBoxInput
            // 
            RichTextBoxInput.Location = new Point(9, 35);
            RichTextBoxInput.Name = "RichTextBoxInput";
            RichTextBoxInput.Size = new Size(576, 347);
            RichTextBoxInput.TabIndex = 2;
            RichTextBoxInput.Text = "";
            // 
            // TextBoxTopic
            // 
            TextBoxTopic.Location = new Point(172, 6);
            TextBoxTopic.Name = "TextBoxTopic";
            TextBoxTopic.Size = new Size(145, 23);
            TextBoxTopic.TabIndex = 1;
            // 
            // DateTimePicker
            // 
            DateTimePicker.Location = new Point(9, 6);
            DateTimePicker.Name = "DateTimePicker";
            DateTimePicker.Size = new Size(130, 23);
            DateTimePicker.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 450);
            Controls.Add(TabControl);
            Name = "MainForm";
            Text = "Diary";
            Resize += MainForm_Resize;
            TabControl.ResumeLayout(false);
            TabPageView.ResumeLayout(false);
            TabPageAdd.ResumeLayout(false);
            TabPageAdd.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private RichTextBox RichTextBoxShow;
        private ListBox ListBoxFile;
        private ComboBox ComboBoxYear;
        private ComboBox ComboBoxMonth;
        private Button ButtonInvalid;
        private TabControl TabControl;
        private TabPage TabPageView;
        private TabPage TabPageAdd;
        private TextBox TextBoxTopic;
        private DateTimePicker DateTimePicker;
        private RichTextBox RichTextBoxInput;
        private Button ButtonCreate;
        private Button ButtonRefresh;
        private Button ButtonEdit;
        private Button ButtonSave;
        private Button ButtonCancel;
    }
}