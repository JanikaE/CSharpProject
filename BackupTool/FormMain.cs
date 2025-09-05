using BackupTool.Config;
using BackupTool.Controls;
using BackupTool.Forms;
using System;
using System.Windows.Forms;

namespace BackupTool
{
    public partial class FormMain : ScalingForm
    {
        public FormMain()
        {
            InitializeComponent();
            InitTag();

            SetRectangle();
            checkBoxIsShowIgnore.Checked = Config.Config.Instance.IsShowIgnore;
            UpdatePanel();
        }

        private void ButtonExec_Click(object sender, EventArgs e)
        {
            FormExec formExec = new();
            formExec.Show();
            formExec.Execute();
        }

        public void UpdatePanel()
        {
            foreach (Control control in panelPathPairs.Controls)
            {
                control.Dispose();
            }
            panelPathPairs.Controls.Clear();

            foreach (PathPair pathPair in Config.Config.Instance.PathPairs)
            {
                PathPairRichTextBox textBox = new(pathPair)
                {
                    ReadOnly = true,
                    Text = pathPair.ToString()
                };

                PathPairContextMenuStrip contextMenuStrip = new(pathPair);
                contextMenuStrip.Init();
                contextMenuStrip.Form = this;
                textBox.ContextMenuStrip = contextMenuStrip;
               
                panelPathPairs.Controls.Add(textBox);
            }

            SetPanelPosition();
        }

        private void SetPanelPosition()
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

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            FormAdd formAdd = new(null);
            formAdd.ShowDialog(this);
        }

        private void CheckBoxIsShowIgnore_CheckedChanged(object sender, EventArgs e)
        {
            Config.Config.Instance.IsShowIgnore = checkBoxIsShowIgnore.Checked;
            Config.Config.Instance.Save();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            SetPanelPosition();
        }
    }
}
