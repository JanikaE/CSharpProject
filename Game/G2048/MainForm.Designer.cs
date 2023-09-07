using System.ComponentModel;
using System.Windows.Forms;

namespace G2048Form
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Up = new System.Windows.Forms.Button();
            this.Left = new System.Windows.Forms.Button();
            this.Right = new System.Windows.Forms.Button();
            this.Down = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.l00 = new System.Windows.Forms.Label();
            this.l01 = new System.Windows.Forms.Label();
            this.l03 = new System.Windows.Forms.Label();
            this.l02 = new System.Windows.Forms.Label();
            this.l13 = new System.Windows.Forms.Label();
            this.l12 = new System.Windows.Forms.Label();
            this.l11 = new System.Windows.Forms.Label();
            this.l10 = new System.Windows.Forms.Label();
            this.l32 = new System.Windows.Forms.Label();
            this.l31 = new System.Windows.Forms.Label();
            this.l30 = new System.Windows.Forms.Label();
            this.l23 = new System.Windows.Forms.Label();
            this.l22 = new System.Windows.Forms.Label();
            this.l21 = new System.Windows.Forms.Label();
            this.l20 = new System.Windows.Forms.Label();
            this.l33 = new System.Windows.Forms.Label();
            this.Score = new System.Windows.Forms.Label();
            this.Result = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Up
            // 
            this.Up.Location = new System.Drawing.Point(450, 50);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(50, 50);
            this.Up.TabIndex = 0;
            this.Up.TabStop = false;
            this.Up.Text = "Up";
            this.Up.UseVisualStyleBackColor = true;
            this.Up.Click += new System.EventHandler(this.Up_Click);
            // 
            // Left
            // 
            this.Left.Location = new System.Drawing.Point(400, 100);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(50, 50);
            this.Left.TabIndex = 1;
            this.Left.TabStop = false;
            this.Left.Text = "Left";
            this.Left.UseVisualStyleBackColor = true;
            this.Left.Click += new System.EventHandler(this.Left_Click);
            // 
            // Right
            // 
            this.Right.Location = new System.Drawing.Point(500, 100);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(50, 50);
            this.Right.TabIndex = 2;
            this.Right.TabStop = false;
            this.Right.Text = "Right";
            this.Right.UseVisualStyleBackColor = true;
            this.Right.Click += new System.EventHandler(this.Right_Click);
            // 
            // Down
            // 
            this.Down.Location = new System.Drawing.Point(450, 150);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(50, 50);
            this.Down.TabIndex = 3;
            this.Down.TabStop = false;
            this.Down.Text = "Down";
            this.Down.UseVisualStyleBackColor = true;
            this.Down.Click += new System.EventHandler(this.Down_Click);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(435, 250);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(80, 30);
            this.Start.TabIndex = 4;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // l00
            // 
            this.l00.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l00.Location = new System.Drawing.Point(50, 50);
            this.l00.MaximumSize = new System.Drawing.Size(50, 50);
            this.l00.Name = "l00";
            this.l00.Size = new System.Drawing.Size(50, 50);
            this.l00.TabIndex = 5;
            this.l00.Text = "0";
            this.l00.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l01
            // 
            this.l01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l01.Location = new System.Drawing.Point(100, 50);
            this.l01.MaximumSize = new System.Drawing.Size(50, 50);
            this.l01.Name = "l01";
            this.l01.Size = new System.Drawing.Size(50, 50);
            this.l01.TabIndex = 6;
            this.l01.Text = "0";
            this.l01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l03
            // 
            this.l03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l03.Location = new System.Drawing.Point(200, 50);
            this.l03.MaximumSize = new System.Drawing.Size(50, 50);
            this.l03.Name = "l03";
            this.l03.Size = new System.Drawing.Size(50, 50);
            this.l03.TabIndex = 8;
            this.l03.Text = "0";
            this.l03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l02
            // 
            this.l02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l02.Location = new System.Drawing.Point(150, 50);
            this.l02.MaximumSize = new System.Drawing.Size(50, 50);
            this.l02.Name = "l02";
            this.l02.Size = new System.Drawing.Size(50, 50);
            this.l02.TabIndex = 7;
            this.l02.Text = "0";
            this.l02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l13
            // 
            this.l13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l13.Location = new System.Drawing.Point(200, 100);
            this.l13.MaximumSize = new System.Drawing.Size(50, 50);
            this.l13.Name = "l13";
            this.l13.Size = new System.Drawing.Size(50, 50);
            this.l13.TabIndex = 12;
            this.l13.Text = "0";
            this.l13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l12
            // 
            this.l12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l12.Location = new System.Drawing.Point(150, 100);
            this.l12.MaximumSize = new System.Drawing.Size(50, 50);
            this.l12.Name = "l12";
            this.l12.Size = new System.Drawing.Size(50, 50);
            this.l12.TabIndex = 11;
            this.l12.Text = "0";
            this.l12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l11
            // 
            this.l11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l11.Location = new System.Drawing.Point(100, 100);
            this.l11.MaximumSize = new System.Drawing.Size(50, 50);
            this.l11.Name = "l11";
            this.l11.Size = new System.Drawing.Size(50, 50);
            this.l11.TabIndex = 10;
            this.l11.Text = "0";
            this.l11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l10
            // 
            this.l10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l10.Location = new System.Drawing.Point(50, 100);
            this.l10.MaximumSize = new System.Drawing.Size(50, 50);
            this.l10.Name = "l10";
            this.l10.Size = new System.Drawing.Size(50, 50);
            this.l10.TabIndex = 9;
            this.l10.Text = "0";
            this.l10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l32
            // 
            this.l32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l32.Location = new System.Drawing.Point(150, 200);
            this.l32.MaximumSize = new System.Drawing.Size(50, 50);
            this.l32.Name = "l32";
            this.l32.Size = new System.Drawing.Size(50, 50);
            this.l32.TabIndex = 19;
            this.l32.Text = "0";
            this.l32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l31
            // 
            this.l31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l31.Location = new System.Drawing.Point(100, 200);
            this.l31.MaximumSize = new System.Drawing.Size(50, 50);
            this.l31.Name = "l31";
            this.l31.Size = new System.Drawing.Size(50, 50);
            this.l31.TabIndex = 18;
            this.l31.Text = "0";
            this.l31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l30
            // 
            this.l30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l30.Location = new System.Drawing.Point(50, 200);
            this.l30.MaximumSize = new System.Drawing.Size(50, 50);
            this.l30.Name = "l30";
            this.l30.Size = new System.Drawing.Size(50, 50);
            this.l30.TabIndex = 17;
            this.l30.Text = "0";
            this.l30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l23
            // 
            this.l23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l23.Location = new System.Drawing.Point(200, 150);
            this.l23.MaximumSize = new System.Drawing.Size(50, 50);
            this.l23.Name = "l23";
            this.l23.Size = new System.Drawing.Size(50, 50);
            this.l23.TabIndex = 16;
            this.l23.Text = "0";
            this.l23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l22
            // 
            this.l22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l22.Location = new System.Drawing.Point(150, 150);
            this.l22.MaximumSize = new System.Drawing.Size(50, 50);
            this.l22.Name = "l22";
            this.l22.Size = new System.Drawing.Size(50, 50);
            this.l22.TabIndex = 15;
            this.l22.Text = "0";
            this.l22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l21
            // 
            this.l21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l21.Location = new System.Drawing.Point(100, 150);
            this.l21.MaximumSize = new System.Drawing.Size(50, 50);
            this.l21.Name = "l21";
            this.l21.Size = new System.Drawing.Size(50, 50);
            this.l21.TabIndex = 14;
            this.l21.Text = "0";
            this.l21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l20
            // 
            this.l20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l20.Location = new System.Drawing.Point(50, 150);
            this.l20.MaximumSize = new System.Drawing.Size(50, 50);
            this.l20.Name = "l20";
            this.l20.Size = new System.Drawing.Size(50, 50);
            this.l20.TabIndex = 13;
            this.l20.Text = "0";
            this.l20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l33
            // 
            this.l33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.l33.Location = new System.Drawing.Point(200, 200);
            this.l33.MaximumSize = new System.Drawing.Size(50, 50);
            this.l33.Name = "l33";
            this.l33.Size = new System.Drawing.Size(50, 50);
            this.l33.TabIndex = 20;
            this.l33.Text = "0";
            this.l33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Score
            // 
            this.Score.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Score.Location = new System.Drawing.Point(100, 300);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(100, 30);
            this.Score.TabIndex = 21;
            this.Score.Text = "label1";
            // 
            // Result
            // 
            this.Result.Location = new System.Drawing.Point(260, 125);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(100, 50);
            this.Result.TabIndex = 22;
            this.Result.Text = "Defeat";
            this.Result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Result.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(600, 350);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.Score);
            this.Controls.Add(this.l33);
            this.Controls.Add(this.l32);
            this.Controls.Add(this.l31);
            this.Controls.Add(this.l30);
            this.Controls.Add(this.l23);
            this.Controls.Add(this.l22);
            this.Controls.Add(this.l21);
            this.Controls.Add(this.l20);
            this.Controls.Add(this.l13);
            this.Controls.Add(this.l12);
            this.Controls.Add(this.l11);
            this.Controls.Add(this.l10);
            this.Controls.Add(this.l03);
            this.Controls.Add(this.l02);
            this.Controls.Add(this.l01);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Down);
            this.Controls.Add(this.Right);
            this.Controls.Add(this.Left);
            this.Controls.Add(this.Up);
            this.Controls.Add(this.l00);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "2048";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private Button Up;
        private new Button Left;
        private new Button Right;
        private Button Down;
        private Button Start;

        private Label l00;
        private Label l02;
        private Label l01;
        private Label l03;
        private Label l13;
        private Label l12;
        private Label l11;
        private Label l10;
        private Label l32;
        private Label l31;
        private Label l30;
        private Label l23;
        private Label l22;
        private Label l21;
        private Label l20;
        private Label l33;
        private Label Score;
        private Label Result;
    }
}

