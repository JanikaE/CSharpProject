namespace Reversi
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
            ChessBoard = new PictureBox();
            StartButton = new Button();
            Turn = new TextBox();
            BlackNum = new TextBox();
            WhiteNum = new TextBox();
            State = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)ChessBoard).BeginInit();
            SuspendLayout();
            // 
            // ChessBoard
            // 
            ChessBoard.Location = new Point(12, 12);
            ChessBoard.Name = "ChessBoard";
            ChessBoard.Size = new Size(320, 320);
            ChessBoard.TabIndex = 0;
            ChessBoard.TabStop = false;
            ChessBoard.MouseClick += ChessBoard_MouseClick;
            ChessBoard.MouseMove += ChessBoard_MouseMove;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(378, 290);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(75, 23);
            StartButton.TabIndex = 1;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // Turn
            // 
            Turn.Location = new Point(423, 63);
            Turn.Name = "Turn";
            Turn.ReadOnly = true;
            Turn.Size = new Size(56, 23);
            Turn.TabIndex = 2;
            // 
            // BlackNum
            // 
            BlackNum.Location = new Point(423, 92);
            BlackNum.Name = "BlackNum";
            BlackNum.ReadOnly = true;
            BlackNum.Size = new Size(56, 23);
            BlackNum.TabIndex = 3;
            // 
            // WhiteNum
            // 
            WhiteNum.Location = new Point(423, 121);
            WhiteNum.Name = "WhiteNum";
            WhiteNum.ReadOnly = true;
            WhiteNum.Size = new Size(56, 23);
            WhiteNum.TabIndex = 4;
            // 
            // State
            // 
            State.Location = new Point(423, 150);
            State.Name = "State";
            State.ReadOnly = true;
            State.Size = new Size(56, 23);
            State.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(360, 66);
            label1.Name = "label1";
            label1.Size = new Size(34, 17);
            label1.TabIndex = 6;
            label1.Text = "Turn";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(346, 95);
            label2.Name = "label2";
            label2.Size = new Size(71, 17);
            label2.TabIndex = 7;
            label2.Text = "Black Num";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(346, 124);
            label3.Name = "label3";
            label3.Size = new Size(73, 17);
            label3.TabIndex = 8;
            label3.Text = "White Num";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(360, 153);
            label4.Name = "label4";
            label4.Size = new Size(37, 17);
            label4.TabIndex = 9;
            label4.Text = "State";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(505, 344);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(State);
            Controls.Add(WhiteNum);
            Controls.Add(BlackNum);
            Controls.Add(Turn);
            Controls.Add(StartButton);
            Controls.Add(ChessBoard);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Reversi";
            ((System.ComponentModel.ISupportInitialize)ChessBoard).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox ChessBoard;
        private Button StartButton;
        private TextBox Turn;
        private TextBox BlackNum;
        private TextBox WhiteNum;
        private TextBox State;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}