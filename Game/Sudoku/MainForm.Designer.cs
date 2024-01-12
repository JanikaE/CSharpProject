namespace Sudoku
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
            ButtonSolve = new Button();
            PictureBoxRow = new PictureBox();
            PictureBoxCol = new PictureBox();
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
            // ButtonSolve
            // 
            ButtonSolve.Location = new Point(499, 370);
            ButtonSolve.Name = "ButtonSolve";
            ButtonSolve.Size = new Size(75, 23);
            ButtonSolve.TabIndex = 3;
            ButtonSolve.Text = "Solve";
            ButtonSolve.UseVisualStyleBackColor = true;
            ButtonSolve.Click += ButtonSolve_Click;
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(661, 405);
            Controls.Add(PictureBoxCol);
            Controls.Add(PictureBoxRow);
            Controls.Add(ButtonSolve);
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
        private Button ButtonSolve;
        private PictureBox PictureBoxRow;
        private PictureBox PictureBoxCol;
    }
}