using FileManager.Configs;
using FileManager.Controls;
using FileManager.Forms.Backups;
using System;
using System.Windows.Forms;

namespace FileManager.UserInterface
{
    public partial class UIBackup : UserControl
    {
        public UIBackup()
        {
            InitializeComponent();
            UpdatePanel();
            checkBoxIsShowIgnore.Checked = Config.Instance.IsShowIgnore;
        }

        public void UpdatePanel()
        {
            foreach (Control control in panelPathPairs.Controls)
            {
                control.Dispose();
            }
            panelPathPairs.Controls.Clear();

            foreach (PathPair pathPair in Config.Instance.PathPairs)
            {
                RichTextBoxPathPair textBox = new(pathPair);
                panelPathPairs.Controls.Add(textBox);
            }

            SetPanelPosition();
        }

        public void SetPanelPosition()
        {
            int y = panelPathPairs.Top;
            foreach (Control control in panelPathPairs.Controls)
            {
                RichTextBoxPathPair textBox = control as RichTextBoxPathPair;
                textBox.Top = y;
                textBox.Left = panelPathPairs.Left;
                textBox.Width = panelPathPairs.Width;
                textBox.Height = (int)(textBox.PreferredHeight * 3.2);
                y += textBox.Height;
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            FormAdd formAdd = new(null);
            formAdd.ShowDialog(this);
        }

        private void CheckBoxIsShowIgnore_CheckedChanged(object sender, EventArgs e)
        {
            Config.Instance.IsShowIgnore = checkBoxIsShowIgnore.Checked;
            Config.Instance.Save();
        }

        private void ButtonExec_Click(object sender, EventArgs e)
        {
            FormExecBackup formExec = new();
            formExec.Show();
            formExec.Execute();
        }

        private void BackupPanel_Resize(object sender, EventArgs e)
        {
            SetPanelPosition();
        }
    }
}