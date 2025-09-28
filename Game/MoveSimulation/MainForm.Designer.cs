namespace MoveSimulation
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
            components = new System.ComponentModel.Container();
            timerUpdate = new System.Windows.Forms.Timer(components);
            pictureBoxPoint = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            textBoxFriction = new System.Windows.Forms.TextBox();
            textBoxAcceleration = new System.Windows.Forms.TextBox();
            labelY = new System.Windows.Forms.Label();
            labelX = new System.Windows.Forms.Label();
            buttonReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPoint).BeginInit();
            SuspendLayout();
            // 
            // timerUpdate
            // 
            timerUpdate.Enabled = true;
            timerUpdate.Interval = 50;
            timerUpdate.Tick += TimerUpdate_Tick;
            // 
            // pictureBoxPoint
            // 
            pictureBoxPoint.BackColor = System.Drawing.Color.White;
            pictureBoxPoint.Location = new System.Drawing.Point(70, 112);
            pictureBoxPoint.Name = "pictureBoxPoint";
            pictureBoxPoint.Size = new System.Drawing.Size(100, 100);
            pictureBoxPoint.TabIndex = 0;
            pictureBoxPoint.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(66, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(53, 17);
            label1.TabIndex = 1;
            label1.Text = "Friction:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(37, 40);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(82, 17);
            label2.TabIndex = 2;
            label2.Text = "Acceleration:";
            // 
            // textBoxFriction
            // 
            textBoxFriction.Location = new System.Drawing.Point(125, 6);
            textBoxFriction.Name = "textBoxFriction";
            textBoxFriction.Size = new System.Drawing.Size(100, 23);
            textBoxFriction.TabIndex = 3;
            // 
            // textBoxAcceleration
            // 
            textBoxAcceleration.Location = new System.Drawing.Point(125, 37);
            textBoxAcceleration.Name = "textBoxAcceleration";
            textBoxAcceleration.Size = new System.Drawing.Size(100, 23);
            textBoxAcceleration.TabIndex = 4;
            // 
            // labelY
            // 
            labelY.AutoSize = true;
            labelY.Location = new System.Drawing.Point(12, 155);
            labelY.Name = "labelY";
            labelY.Size = new System.Drawing.Size(0, 17);
            labelY.TabIndex = 5;
            // 
            // labelX
            // 
            labelX.AutoSize = true;
            labelX.Location = new System.Drawing.Point(99, 226);
            labelX.Name = "labelX";
            labelX.Size = new System.Drawing.Size(0, 17);
            labelX.TabIndex = 6;
            // 
            // buttonReset
            // 
            buttonReset.Location = new System.Drawing.Point(44, 73);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new System.Drawing.Size(75, 23);
            buttonReset.TabIndex = 7;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += ButtonReset_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(285, 271);
            Controls.Add(buttonReset);
            Controls.Add(labelX);
            Controls.Add(labelY);
            Controls.Add(textBoxAcceleration);
            Controls.Add(textBoxFriction);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBoxPoint);
            KeyPreview = true;
            Name = "MainForm";
            Text = "MoveSimulation";
            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pictureBoxPoint).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.PictureBox pictureBoxPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFriction;
        private System.Windows.Forms.TextBox textBoxAcceleration;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Button buttonReset;
    }
}
