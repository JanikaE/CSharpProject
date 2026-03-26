using System.Drawing;
using System.Windows.Forms;

namespace ImageEditor
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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            pictureBoxImport = new PictureBox();
            pictureBoxExport = new PictureBox();
            buttonImport = new Button();
            buttonExport = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxExport).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.5F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(pictureBoxImport, 0, 0);
            tableLayoutPanel2.Controls.Add(pictureBoxExport, 1, 0);
            tableLayoutPanel2.Controls.Add(buttonImport, 0, 1);
            tableLayoutPanel2.Controls.Add(buttonExport, 1, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 93.46847F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 6.53153133F));
            tableLayoutPanel2.Size = new Size(598, 444);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // pictureBoxImport
            // 
            pictureBoxImport.Dock = DockStyle.Fill;
            pictureBoxImport.Location = new Point(3, 3);
            pictureBoxImport.Name = "pictureBoxImport";
            pictureBoxImport.Size = new Size(293, 409);
            pictureBoxImport.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImport.TabIndex = 0;
            pictureBoxImport.TabStop = false;
            // 
            // pictureBoxExport
            // 
            pictureBoxExport.Dock = DockStyle.Fill;
            pictureBoxExport.Location = new Point(302, 3);
            pictureBoxExport.Name = "pictureBoxExport";
            pictureBoxExport.Size = new Size(293, 409);
            pictureBoxExport.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxExport.TabIndex = 1;
            pictureBoxExport.TabStop = false;
            // 
            // buttonImport
            // 
            buttonImport.Dock = DockStyle.Right;
            buttonImport.Location = new Point(221, 418);
            buttonImport.Name = "buttonImport";
            buttonImport.Size = new Size(75, 23);
            buttonImport.TabIndex = 2;
            buttonImport.Text = "Import";
            buttonImport.UseVisualStyleBackColor = true;
            buttonImport.Click += ButtonImport_Click;
            // 
            // buttonExport
            // 
            buttonExport.Dock = DockStyle.Right;
            buttonExport.Location = new Point(520, 418);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(75, 23);
            buttonExport.TabIndex = 3;
            buttonExport.Text = "Export";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += ButtonExport_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            Text = "ImageEditor";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxImport).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxExport).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private PictureBox pictureBoxImport;
        private PictureBox pictureBoxExport;
        private Button buttonImport;
        private Button buttonExport;
    }
}
