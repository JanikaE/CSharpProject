using System.Windows.Forms;

namespace WinFormUtils.Tool
{
    public class NoticeTool : Form
    {
        #region Designer

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly System.ComponentModel.IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
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
            label1 = new Label();
            ButtonYes = new Button();
            ButtonNo = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label1.Location = new System.Drawing.Point(28, 35);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(345, 144);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // ButtonYes
            // 
            ButtonYes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            ButtonYes.Location = new System.Drawing.Point(243, 184);
            ButtonYes.Margin = new Padding(4, 4, 4, 4);
            ButtonYes.Name = "ButtonYes";
            ButtonYes.Size = new System.Drawing.Size(93, 42);
            ButtonYes.TabIndex = 1;
            ButtonYes.Text = "button1";
            ButtonYes.UseVisualStyleBackColor = true;
            // 
            // ButtonNo
            // 
            ButtonNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            ButtonNo.Location = new System.Drawing.Point(58, 184);
            ButtonNo.Margin = new Padding(4, 4, 4, 4);
            ButtonNo.Name = "ButtonNo";
            ButtonNo.Size = new System.Drawing.Size(93, 42);
            ButtonNo.TabIndex = 2;
            ButtonNo.Text = "button1";
            ButtonNo.UseVisualStyleBackColor = true;
            // 
            // NoticeForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(392, 275);
            ControlBox = false;
            Controls.Add(ButtonNo);
            Controls.Add(ButtonYes);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 4, 4, 4);
            Name = "NoticeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "提示";
            TopMost = true;
            ResumeLayout(false);

        }

        #endregion

        private Label label1;
        private Button ButtonYes;
        private Button ButtonNo;

        #endregion

        private DialogResult _result = DialogResult.None;

        public DialogResult Result => _result;

        private NoticeTool(string msg, bool isShowNo, string no, string yes)
        {
            InitializeComponent();

            label1.Text = msg;
            ButtonNo.Visible = isShowNo;
            ButtonNo.Text = no;
            ButtonYes.Text = yes;
            ButtonNo.Click += (s, e) => { _result = DialogResult.No; Close(); };
            ButtonYes.Click += (s, e) => { _result = DialogResult.Yes; Close(); };
        }

        public static DialogResult Show(string msg, bool isShowNo = true, string no = "取消", string yes = "确定")
        {
            using var form = new NoticeTool(msg, isShowNo, no, yes);
            form.ShowDialog();
            return form.Result;
        }
    }
}
