using FileManager.Configs;
using System;
using System.Windows.Forms;

namespace FileManager.Controls
{
    public partial class PanelBackup : UserControl
    {
        public PanelBackup()
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
                PathPairRichTextBox textBox = new(pathPair)
                {
                    ReadOnly = true,
                    Text = pathPair.ToString()
                };

                PathPairContextMenuStrip contextMenuStrip = new(pathPair);
                contextMenuStrip.Init();
                textBox.ContextMenuStrip = contextMenuStrip;

                panelPathPairs.Controls.Add(textBox);
            }

            SetPanelPosition();
        }

        public void SetPanelPosition()
        {
            int y = panelPathPairs.Top;
            foreach (Control control in panelPathPairs.Controls)
            {
                PathPairRichTextBox textBox = control as PathPairRichTextBox;
                textBox.Width = panelPathPairs.Width;
                textBox.Left = panelPathPairs.Left;
                textBox.Top = y;
                textBox.Height = textBox.PreferredHeight * 3;
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
            FormExec formExec = new();
            formExec.Show();
            formExec.Execute();
        }

        private void BackupPanel_Resize(object sender, EventArgs e)
        {
            SetPanelPosition();
        }
    }
}