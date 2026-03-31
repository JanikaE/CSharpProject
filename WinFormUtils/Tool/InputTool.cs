using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormUtils.Tool
{
    public class InputTool : Form
    {
        #region Designer

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly IContainer components;

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
            tableLayoutPanel1 = new TableLayoutPanel();
            buttonOK = new Button();
            textBox = new TextBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel1.Controls.Add(buttonOK, 1, 0);
            tableLayoutPanel1.Controls.Add(textBox, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(439, 31);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonOK
            // 
            buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonOK.Location = new Point(392, 3);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(44, 25);
            buttonOK.TabIndex = 0;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // textBox
            // 
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Location = new Point(3, 3);
            textBox.Name = "textBox";
            textBox.Size = new Size(383, 23);
            textBox.TabIndex = 1;
            // 
            // InputTool
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size(439, 31);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputTool";
            Text = "InputTool";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button buttonOK;
        private TextBox textBox;

        #endregion

        private DialogResult _result = DialogResult.None;

        public DialogResult Result => _result;

        private InputTool(string text)
        {
            InitializeComponent();

            textBox.Text = text;
            buttonOK.Click += (s, e) => { _result = DialogResult.OK; Close(); };
        }

        public static string ShowDialog(string text)
        {
            using var form = new InputTool(text);
            form.ShowDialog();
            if (form.Result == DialogResult.OK)
            {
                return form.textBox.Text;
            }
            else
            {
                return null;
            }
        }
    }
}
