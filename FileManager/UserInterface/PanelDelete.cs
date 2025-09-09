using FileManager.Configs;
using FileManager.Forms.Deletes;
using System;
using System.Windows.Forms;

namespace FileManager.UserInterface
{
    public partial class PanelDelete : UserControl
    {
        public PanelDelete()
        {
            InitializeComponent();
        }

        private void ButtonExec_Click(object sender, EventArgs e)
        {
            DeletePattern deletePattern = new()
            {
                SourcePath = "E:\\Code\\EPIS",
                MatchFolder = "obj",
                IsFullMatch = true,
                IsIgnoreCase = true
            };
            FormExecDelete formExecDelete = new()
            {
                DeletePattern = deletePattern
            };
            formExecDelete.Show();
            formExecDelete.Execute();
        }
    }
}
