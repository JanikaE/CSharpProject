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
            ListViewItem listViewItem1 = new ListViewItem("日期_类别");
            FileList = new ListView();
            SuspendLayout();
            // 
            // FileList
            // 
            FileList.Items.AddRange(new ListViewItem[] { listViewItem1 });
            FileList.Location = new Point(43, 84);
            FileList.Name = "FileList";
            FileList.Size = new Size(121, 338);
            FileList.Sorting = SortOrder.Ascending;
            FileList.TabIndex = 0;
            FileList.UseCompatibleStateImageBehavior = false;
            FileList.View = View.List;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(FileList);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion

        private ListView FileList;
    }
}