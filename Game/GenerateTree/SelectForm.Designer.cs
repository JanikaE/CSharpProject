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
            listBox1 = new ListBox();
            richTextBox1 = new RichTextBox();
            ButtonOpen = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(99, 361);
            listBox1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(117, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(344, 361);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // ButtonOpen
            // 
            ButtonOpen.Location = new Point(386, 379);
            ButtonOpen.Name = "ButtonOpen";
            ButtonOpen.Size = new Size(75, 23);
            ButtonOpen.TabIndex = 2;
            ButtonOpen.Text = "Open";
            ButtonOpen.UseVisualStyleBackColor = true;
            // 
            // SelectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(473, 416);
            Controls.Add(ButtonOpen);
            Controls.Add(richTextBox1);
            Controls.Add(listBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectForm";
            Text = "SelectForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private RichTextBox richTextBox1;
        private Button ButtonOpen;
    }
}