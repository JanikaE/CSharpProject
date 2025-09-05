using System.Drawing;
using System.Windows.Forms;

namespace GenerateTree
{
    partial class SelectForm
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
            ListBoxXml = new ListBox();
            RichTextBoxContent = new RichTextBox();
            ButtonOpen = new Button();
            SuspendLayout();
            // 
            // ListBoxXml
            // 
            ListBoxXml.FormattingEnabled = true;
            ListBoxXml.ItemHeight = 17;
            ListBoxXml.Location = new Point(12, 12);
            ListBoxXml.Name = "ListBoxXml";
            ListBoxXml.Size = new Size(160, 361);
            ListBoxXml.TabIndex = 0;
            ListBoxXml.SelectedIndexChanged += ListBoxXml_SelectedIndexChanged;
            // 
            // RichTextBoxContent
            // 
            RichTextBoxContent.Location = new Point(178, 12);
            RichTextBoxContent.Name = "RichTextBoxContent";
            RichTextBoxContent.Size = new Size(283, 361);
            RichTextBoxContent.TabIndex = 1;
            RichTextBoxContent.Text = "";
            // 
            // ButtonOpen
            // 
            ButtonOpen.Location = new Point(386, 379);
            ButtonOpen.Name = "ButtonOpen";
            ButtonOpen.Size = new Size(75, 23);
            ButtonOpen.TabIndex = 2;
            ButtonOpen.Text = "Open";
            ButtonOpen.UseVisualStyleBackColor = true;
            ButtonOpen.Click += ButtonOpen_Click;
            // 
            // SelectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(473, 416);
            Controls.Add(ButtonOpen);
            Controls.Add(RichTextBoxContent);
            Controls.Add(ListBoxXml);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectForm";
            Text = "SelectForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox ListBoxXml;
        private RichTextBox RichTextBoxContent;
        private Button ButtonOpen;
    }
}