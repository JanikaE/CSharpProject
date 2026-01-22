using System.Drawing;
using System.Windows.Forms;

namespace SudokuForm
{
    partial class MainForm
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
            SudokuBoard = new PictureBox();
            Generate = new Button();
            ListBoxStep = new ListBox();
            ButtonSolveArts = new Button();
            PictureBoxRow = new PictureBox();
            PictureBoxCol = new PictureBox();
            ButtonSolveBuster = new Button();
            ((System.ComponentModel.ISupportInitialize)SudokuBoard).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBoxRow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBoxCol).BeginInit();
            SuspendLayout();
            // 
            // SudokuBoard
            // 
            SudokuBoard.Location = new Point(33, 33);
            SudokuBoard.Name = "SudokuBoard";
            SudokuBoard.Size = new Size(360, 360);
            SudokuBoard.TabIndex = 0;
            SudokuBoard.TabStop = false;
            // 
            // Generate
            // 
            Generate.Location = new Point(408, 370);
            Generate.Name = "Generate";
            Generate.Size = new Size(75, 23);
            Generate.TabIndex = 1;
            Generate.Text = "Generate";
            Generate.UseVisualStyleBackColor = true;
            Generate.Click += Generate_Click;
            // 
            // ListBoxStep
            // 
            ListBoxStep.FormattingEnabled = true;
            ListBoxStep.ItemHeight = 17;
            ListBoxStep.Location = new Point(408, 12);
            ListBoxStep.Name = "ListBoxStep";
            ListBoxStep.Size = new Size(241, 344);
            ListBoxStep.TabIndex = 2;
            ListBoxStep.SelectedIndexChanged += ListBoxStep_SelectedIndexChanged;
            // 
            // ButtonSolveArts
            // 
            ButtonSolveArts.Location = new Point(499, 370);
            ButtonSolveArts.Name = "ButtonSolveArts";
            ButtonSolveArts.Size = new Size(75, 23);
            ButtonSolveArts.TabIndex = 3;
            ButtonSolveArts.Text = "Arts";
            ButtonSolveArts.UseVisualStyleBackColor = true;
            ButtonSolveArts.Click += ButtonSolveArts_Click;
            // 
            // PictureBoxRow
            // 
            PictureBoxRow.Location = new Point(2, 33);
            PictureBoxRow.Name = "PictureBoxRow";
            PictureBoxRow.Size = new Size(25, 360);
            PictureBoxRow.TabIndex = 4;
            PictureBoxRow.TabStop = false;
            // 
            // PictureBoxCol
            // 
            PictureBoxCol.Location = new Point(33, 2);
            PictureBoxCol.Name = "PictureBoxCol";
            PictureBoxCol.Size = new Size(360, 25);
            PictureBoxCol.TabIndex = 5;
            PictureBoxCol.TabStop = false;
            // 
            // ButtonSolveBuster
            // 
            ButtonSolveBuster.Location = new Point(580, 370);
            ButtonSolveBuster.Name = "ButtonSolveBuster";
            ButtonSolveBuster.Size = new Size(75, 23);
            ButtonSolveBuster.TabIndex = 6;
            ButtonSolveBuster.Text = "Buster";
            ButtonSolveBuster.UseVisualStyleBackColor = true;
            ButtonSolveBuster.Click += ButtonSolveBuster_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(661, 405);
            Controls.Add(ButtonSolveBuster);
            Controls.Add(PictureBoxCol);
            Controls.Add(PictureBoxRow);
            Controls.Add(ButtonSolveArts);
            Controls.Add(ListBoxStep);
            Controls.Add(Generate);
            Controls.Add(SudokuBoard);
            Name = "MainForm";
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)SudokuBoard).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBoxRow).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBoxCol).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox SudokuBoard;
        private Button Generate;
        private ListBox ListBoxStep;
        private Button ButtonSolveArts;
        private PictureBox PictureBoxRow;
        private PictureBox PictureBoxCol;
        private Button ButtonSolveBuster;
    }
}