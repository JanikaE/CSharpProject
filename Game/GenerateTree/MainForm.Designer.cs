namespace GenerateTree
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
            PictureBoxTree = new PictureBox();
            ButtonGo = new Button();
            TextBoxSeed = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            TextBoxRootRad = new TextBox();
            TextBoxRootSize = new TextBox();
            TextBoxRootLength = new TextBox();
            TextBoxMaxDepth = new TextBox();
            TextBoxMinSize = new TextBox();
            TextBoxMinLength = new TextBox();
            TextBoxMaxRad = new TextBox();
            TextBoxChildrenNumMu = new TextBox();
            TextBoxChildrenNumSigma = new TextBox();
            TextBoxSizeChangeMu = new TextBox();
            TextBoxSizeChangeSigma = new TextBox();
            TextBoxLengthChangeMu = new TextBox();
            TextBoxLengthChangeSigma = new TextBox();
            LabelDebug = new Label();
            ButtonSave = new Button();
            ButtonLoad = new Button();
            ((System.ComponentModel.ISupportInitialize)PictureBoxTree).BeginInit();
            SuspendLayout();
            // 
            // PictureBoxTree
            // 
            PictureBoxTree.BackColor = Color.White;
            PictureBoxTree.Location = new Point(12, 12);
            PictureBoxTree.Name = "PictureBoxTree";
            PictureBoxTree.Size = new Size(500, 500);
            PictureBoxTree.TabIndex = 0;
            PictureBoxTree.TabStop = false;
            PictureBoxTree.MouseDown += PictureBoxTree_MouseDown;
            PictureBoxTree.MouseMove += PictureBoxTree_MouseMove;
            PictureBoxTree.MouseUp += PictureBoxTree_MouseUp;
            // 
            // ButtonGo
            // 
            ButtonGo.Location = new Point(697, 489);
            ButtonGo.Name = "ButtonGo";
            ButtonGo.Size = new Size(75, 23);
            ButtonGo.TabIndex = 1;
            ButtonGo.Text = "Go";
            ButtonGo.UseVisualStyleBackColor = true;
            ButtonGo.Click += ButtonGo_Click;
            // 
            // TextBoxSeed
            // 
            TextBoxSeed.Location = new Point(657, 28);
            TextBoxSeed.Name = "TextBoxSeed";
            TextBoxSeed.Size = new Size(100, 23);
            TextBoxSeed.TabIndex = 2;
            TextBoxSeed.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(608, 31);
            label1.Name = "label1";
            label1.Size = new Size(37, 17);
            label1.TabIndex = 3;
            label1.Text = "Seed";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(586, 60);
            label2.Name = "label2";
            label2.Size = new Size(59, 17);
            label2.TabIndex = 4;
            label2.Text = "RootRad";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(586, 89);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 5;
            label3.Text = "RootSize";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(570, 118);
            label4.Name = "label4";
            label4.Size = new Size(75, 17);
            label4.TabIndex = 6;
            label4.Text = "RootLength";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(577, 147);
            label5.Name = "label5";
            label5.Size = new Size(68, 17);
            label5.TabIndex = 7;
            label5.Text = "MaxDepth";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(592, 176);
            label6.Name = "label6";
            label6.Size = new Size(53, 17);
            label6.TabIndex = 8;
            label6.Text = "MinSize";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(576, 206);
            label7.Name = "label7";
            label7.Size = new Size(69, 17);
            label7.TabIndex = 9;
            label7.Text = "MinLength";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(589, 235);
            label8.Name = "label8";
            label8.Size = new Size(56, 17);
            label8.TabIndex = 10;
            label8.Text = "MaxRad";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(542, 264);
            label9.Name = "label9";
            label9.Size = new Size(103, 17);
            label9.TabIndex = 11;
            label9.Text = "ChildrenNumMu";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(525, 293);
            label10.Name = "label10";
            label10.Size = new Size(120, 17);
            label10.TabIndex = 12;
            label10.Text = "ChildrenNumSigma";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(551, 322);
            label11.Name = "label11";
            label11.Size = new Size(94, 17);
            label11.TabIndex = 13;
            label11.Text = "SizeChangeMu";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(534, 351);
            label12.Name = "label12";
            label12.Size = new Size(111, 17);
            label12.TabIndex = 14;
            label12.Text = "SizeChangeSigma";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(535, 380);
            label13.Name = "label13";
            label13.Size = new Size(110, 17);
            label13.TabIndex = 15;
            label13.Text = "LengthChangeMu";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(518, 409);
            label14.Name = "label14";
            label14.Size = new Size(127, 17);
            label14.TabIndex = 16;
            label14.Text = "LengthChangeSigma";
            // 
            // TextBoxRootRad
            // 
            TextBoxRootRad.Location = new Point(657, 57);
            TextBoxRootRad.Name = "TextBoxRootRad";
            TextBoxRootRad.Size = new Size(100, 23);
            TextBoxRootRad.TabIndex = 17;
            TextBoxRootRad.Text = "0";
            // 
            // TextBoxRootSize
            // 
            TextBoxRootSize.Location = new Point(657, 86);
            TextBoxRootSize.Name = "TextBoxRootSize";
            TextBoxRootSize.Size = new Size(100, 23);
            TextBoxRootSize.TabIndex = 18;
            TextBoxRootSize.Text = "0";
            // 
            // TextBoxRootLength
            // 
            TextBoxRootLength.Location = new Point(657, 115);
            TextBoxRootLength.Name = "TextBoxRootLength";
            TextBoxRootLength.Size = new Size(100, 23);
            TextBoxRootLength.TabIndex = 19;
            TextBoxRootLength.Text = "0";
            // 
            // TextBoxMaxDepth
            // 
            TextBoxMaxDepth.Location = new Point(657, 144);
            TextBoxMaxDepth.Name = "TextBoxMaxDepth";
            TextBoxMaxDepth.Size = new Size(100, 23);
            TextBoxMaxDepth.TabIndex = 20;
            TextBoxMaxDepth.Text = "0";
            // 
            // TextBoxMinSize
            // 
            TextBoxMinSize.Location = new Point(657, 173);
            TextBoxMinSize.Name = "TextBoxMinSize";
            TextBoxMinSize.Size = new Size(100, 23);
            TextBoxMinSize.TabIndex = 21;
            TextBoxMinSize.Text = "0";
            // 
            // TextBoxMinLength
            // 
            TextBoxMinLength.Location = new Point(657, 203);
            TextBoxMinLength.Name = "TextBoxMinLength";
            TextBoxMinLength.Size = new Size(100, 23);
            TextBoxMinLength.TabIndex = 22;
            TextBoxMinLength.Text = "0";
            // 
            // TextBoxMaxRad
            // 
            TextBoxMaxRad.Location = new Point(657, 232);
            TextBoxMaxRad.Name = "TextBoxMaxRad";
            TextBoxMaxRad.Size = new Size(100, 23);
            TextBoxMaxRad.TabIndex = 23;
            TextBoxMaxRad.Text = "0";
            // 
            // TextBoxChildrenNumMu
            // 
            TextBoxChildrenNumMu.Location = new Point(657, 261);
            TextBoxChildrenNumMu.Name = "TextBoxChildrenNumMu";
            TextBoxChildrenNumMu.Size = new Size(100, 23);
            TextBoxChildrenNumMu.TabIndex = 24;
            TextBoxChildrenNumMu.Text = "0";
            // 
            // TextBoxChildrenNumSigma
            // 
            TextBoxChildrenNumSigma.Location = new Point(657, 290);
            TextBoxChildrenNumSigma.Name = "TextBoxChildrenNumSigma";
            TextBoxChildrenNumSigma.Size = new Size(100, 23);
            TextBoxChildrenNumSigma.TabIndex = 25;
            TextBoxChildrenNumSigma.Text = "0";
            // 
            // TextBoxSizeChangeMu
            // 
            TextBoxSizeChangeMu.Location = new Point(657, 319);
            TextBoxSizeChangeMu.Name = "TextBoxSizeChangeMu";
            TextBoxSizeChangeMu.Size = new Size(100, 23);
            TextBoxSizeChangeMu.TabIndex = 26;
            TextBoxSizeChangeMu.Text = "0";
            // 
            // TextBoxSizeChangeSigma
            // 
            TextBoxSizeChangeSigma.Location = new Point(657, 348);
            TextBoxSizeChangeSigma.Name = "TextBoxSizeChangeSigma";
            TextBoxSizeChangeSigma.Size = new Size(100, 23);
            TextBoxSizeChangeSigma.TabIndex = 27;
            TextBoxSizeChangeSigma.Text = "0";
            // 
            // TextBoxLengthChangeMu
            // 
            TextBoxLengthChangeMu.Location = new Point(657, 377);
            TextBoxLengthChangeMu.Name = "TextBoxLengthChangeMu";
            TextBoxLengthChangeMu.Size = new Size(100, 23);
            TextBoxLengthChangeMu.TabIndex = 28;
            TextBoxLengthChangeMu.Text = "0";
            // 
            // TextBoxLengthChangeSigma
            // 
            TextBoxLengthChangeSigma.Location = new Point(657, 406);
            TextBoxLengthChangeSigma.Name = "TextBoxLengthChangeSigma";
            TextBoxLengthChangeSigma.Size = new Size(100, 23);
            TextBoxLengthChangeSigma.TabIndex = 29;
            TextBoxLengthChangeSigma.Text = "0";
            // 
            // LabelDebug
            // 
            LabelDebug.AutoSize = true;
            LabelDebug.Location = new Point(525, 489);
            LabelDebug.Name = "LabelDebug";
            LabelDebug.Size = new Size(0, 17);
            LabelDebug.TabIndex = 30;
            // 
            // ButtonSave
            // 
            ButtonSave.Location = new Point(616, 489);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(75, 23);
            ButtonSave.TabIndex = 31;
            ButtonSave.Text = "Save";
            ButtonSave.UseVisualStyleBackColor = true;
            ButtonSave.Click += ButtonSave_Click;
            // 
            // ButtonLoad
            // 
            ButtonLoad.Location = new Point(535, 489);
            ButtonLoad.Name = "ButtonLoad";
            ButtonLoad.Size = new Size(75, 23);
            ButtonLoad.TabIndex = 32;
            ButtonLoad.Text = "Load";
            ButtonLoad.UseVisualStyleBackColor = true;
            ButtonLoad.Click += ButtonLoad_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 522);
            Controls.Add(ButtonLoad);
            Controls.Add(ButtonSave);
            Controls.Add(LabelDebug);
            Controls.Add(TextBoxLengthChangeSigma);
            Controls.Add(TextBoxLengthChangeMu);
            Controls.Add(TextBoxSizeChangeSigma);
            Controls.Add(TextBoxSizeChangeMu);
            Controls.Add(TextBoxChildrenNumSigma);
            Controls.Add(TextBoxChildrenNumMu);
            Controls.Add(TextBoxMaxRad);
            Controls.Add(TextBoxMinLength);
            Controls.Add(TextBoxMinSize);
            Controls.Add(TextBoxMaxDepth);
            Controls.Add(TextBoxRootLength);
            Controls.Add(TextBoxRootSize);
            Controls.Add(TextBoxRootRad);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TextBoxSeed);
            Controls.Add(ButtonGo);
            Controls.Add(PictureBoxTree);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "GenerateTree";
            ((System.ComponentModel.ISupportInitialize)PictureBoxTree).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox PictureBoxTree;
        private Button ButtonGo;
        private TextBox TextBoxSeed;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private TextBox TextBoxRootRad;
        private TextBox TextBoxRootSize;
        private TextBox TextBoxRootLength;
        private TextBox TextBoxMaxDepth;
        private TextBox TextBoxMinSize;
        private TextBox TextBoxMinLength;
        private TextBox TextBoxMaxRad;
        private TextBox TextBoxChildrenNumMu;
        private TextBox TextBoxChildrenNumSigma;
        private TextBox TextBoxSizeChangeMu;
        private TextBox TextBoxSizeChangeSigma;
        private TextBox TextBoxLengthChangeMu;
        private TextBox TextBoxLengthChangeSigma;
        private Label LabelDebug;
        private Button ButtonSave;
        private Button ButtonLoad;
    }
}
