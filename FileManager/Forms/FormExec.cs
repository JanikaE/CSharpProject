using System;
using System.Drawing;

namespace FileManager.Forms
{
    public partial class FormExec : ScalingForm_1
    {
        public FormExec()
        {
            InitializeComponent();
        }

        public virtual void Execute()
        {
            buttonOK.Enabled = true;
        }

        protected void WriteProcess(string str, Color color)
        {
            int start = richTextBoxProcess.Text.Length;
            richTextBoxProcess.Select(start, 0);
            richTextBoxProcess.SelectionColor = color;
            richTextBoxProcess.AppendText(str);
        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
