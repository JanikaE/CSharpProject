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
            tabControlMain = new TabControl();
            tabPageNormal = new TabPage();
            buttonBlackWhite = new Button();
            buttonMonochrome = new Button();
            buttonInvertColor = new Button();
            tabPageClosestColor = new TabPage();
            uiClosestColor = new ImageEditor.UserInterface.UIClosestColor();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxExport).BeginInit();
            tabControlMain.SuspendLayout();
            tabPageNormal.SuspendLayout();
            tabPageClosestColor.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.5F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tabControlMain, 1, 0);
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
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.Size = new Size(598, 444);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // pictureBoxImport
            // 
            pictureBoxImport.Dock = DockStyle.Fill;
            pictureBoxImport.Location = new Point(3, 3);
            pictureBoxImport.Name = "pictureBoxImport";
            pictureBoxImport.Size = new Size(293, 408);
            pictureBoxImport.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImport.TabIndex = 0;
            pictureBoxImport.TabStop = false;
            // 
            // pictureBoxExport
            // 
            pictureBoxExport.Dock = DockStyle.Fill;
            pictureBoxExport.Location = new Point(302, 3);
            pictureBoxExport.Name = "pictureBoxExport";
            pictureBoxExport.Size = new Size(293, 408);
            pictureBoxExport.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxExport.TabIndex = 1;
            pictureBoxExport.TabStop = false;
            // 
            // buttonImport
            // 
            buttonImport.Dock = DockStyle.Right;
            buttonImport.Location = new Point(221, 417);
            buttonImport.Name = "buttonImport";
            buttonImport.Size = new Size(75, 24);
            buttonImport.TabIndex = 2;
            buttonImport.Text = "Import";
            buttonImport.UseVisualStyleBackColor = true;
            buttonImport.Click += ButtonImport_Click;
            // 
            // buttonExport
            // 
            buttonExport.Dock = DockStyle.Right;
            buttonExport.Location = new Point(520, 417);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(75, 24);
            buttonExport.TabIndex = 3;
            buttonExport.Text = "Export";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += ButtonExport_Click;
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabPageNormal);
            tabControlMain.Controls.Add(tabPageClosestColor);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(607, 3);
            tabControlMain.Multiline = true;
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(190, 444);
            tabControlMain.TabIndex = 1;
            // 
            // tabPageNormal
            // 
            tabPageNormal.Controls.Add(buttonBlackWhite);
            tabPageNormal.Controls.Add(buttonMonochrome);
            tabPageNormal.Controls.Add(buttonInvertColor);
            tabPageNormal.Location = new Point(4, 26);
            tabPageNormal.Name = "tabPageNormal";
            tabPageNormal.Padding = new Padding(3);
            tabPageNormal.Size = new Size(182, 414);
            tabPageNormal.TabIndex = 0;
            tabPageNormal.Text = "Normal";
            tabPageNormal.UseVisualStyleBackColor = true;
            // 
            // buttonBlackWhite
            // 
            buttonBlackWhite.Location = new Point(6, 64);
            buttonBlackWhite.Name = "buttonBlackWhite";
            buttonBlackWhite.Size = new Size(92, 23);
            buttonBlackWhite.TabIndex = 2;
            buttonBlackWhite.Text = "BlackWhite";
            buttonBlackWhite.UseVisualStyleBackColor = true;
            buttonBlackWhite.Click += ButtonBlackWhite_Click;
            // 
            // buttonMonochrome
            // 
            buttonMonochrome.Location = new Point(6, 35);
            buttonMonochrome.Name = "buttonMonochrome";
            buttonMonochrome.Size = new Size(92, 23);
            buttonMonochrome.TabIndex = 1;
            buttonMonochrome.Text = "Monochrome";
            buttonMonochrome.UseVisualStyleBackColor = true;
            buttonMonochrome.Click += ButtonMonochrome_Click;
            // 
            // buttonInvertColor
            // 
            buttonInvertColor.Location = new Point(6, 6);
            buttonInvertColor.Name = "buttonInvertColor";
            buttonInvertColor.Size = new Size(92, 23);
            buttonInvertColor.TabIndex = 0;
            buttonInvertColor.Text = "InvertColor";
            buttonInvertColor.UseVisualStyleBackColor = true;
            buttonInvertColor.Click += ButtonInvertColor_Click;
            // 
            // tabPageClosestColor
            // 
            tabPageClosestColor.Controls.Add(uiClosestColor);
            tabPageClosestColor.Location = new Point(4, 26);
            tabPageClosestColor.Name = "tabPageClosestColor";
            tabPageClosestColor.Padding = new Padding(3);
            tabPageClosestColor.Size = new Size(182, 414);
            tabPageClosestColor.TabIndex = 1;
            tabPageClosestColor.Text = "ClosestColor";
            tabPageClosestColor.UseVisualStyleBackColor = true;
            // 
            // uiClosestColor
            // 
            uiClosestColor.Dock = DockStyle.Fill;
            uiClosestColor.Location = new Point(3, 3);
            uiClosestColor.Name = "uiClosestColor";
            uiClosestColor.Size = new Size(176, 408);
            uiClosestColor.TabIndex = 0;
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
            tabControlMain.ResumeLayout(false);
            tabPageNormal.ResumeLayout(false);
            tabPageClosestColor.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private PictureBox pictureBoxImport;
        private PictureBox pictureBoxExport;
        private Button buttonImport;
        private Button buttonExport;
        private TabControl tabControlMain;
        private TabPage tabPageNormal;
        private TabPage tabPageClosestColor;
        private Button buttonInvertColor;
        private Button buttonBlackWhite;
        private Button buttonMonochrome;
        private UserInterface.UIClosestColor uiClosestColor;
    }
}
