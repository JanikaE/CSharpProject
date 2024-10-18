using System;
using System.Windows.Forms;

namespace BackupTool
{
    public partial class FormExec : Form
    {
        public FormExec()
        {
            InitializeComponent();
            Execute();
        }

        private void Execute()
        {
            // todo: Exec

            buttonOK.Enabled = true;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
