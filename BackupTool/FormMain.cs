using BackupTool.Config;
using System;
using System.Windows.Forms;

namespace BackupTool
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            UpdatePanel();
        }

        private void ButtonExec_Click(object sender, EventArgs e)
        {
            FormExec formExec = new FormExec();
            formExec.ShowDialog(this);
            formExec.BringToFront();
        }

        private void UpdatePanel()
        {
            foreach (Control control in panelPathPairs.Controls)
            {
                control.Dispose();
            }
            panelPathPairs.Controls.Clear();

            int y = panelPathPairs.Top;
            foreach (PathPair pathPair in Config.Config.Instance.PathPairs)
            {
                int height = 200;
                RichTextBox textBox = new RichTextBox
                {
                    ReadOnly = true,
                    Text = pathPair.ToString(),
                    ContextMenuStrip = contextMenuStripPathPair,
                    Tag = pathPair,

                    Width = panelPathPairs.Width,
                    Height = height,  // todo: 高度自适应
                    Left = panelPathPairs.Left,
                    Top = y
                };
                y += height;

                panelPathPairs.Controls.Add(textBox);
                
            }
        }

        #region 新增/修改/删除

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            FormAdd formAdd = new FormAdd(null);
            formAdd.ShowDialog(this);
            formAdd.BringToFront();
            formAdd.FormClosed += (sender2, e2) =>
            {
                UpdatePanel();
            };
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PathPair pathPair = ((RichTextBox)sender).Tag as PathPair;

            FormAdd formAdd = new FormAdd(pathPair);
            formAdd.ShowDialog(this);
            formAdd.BringToFront();
            formAdd.FormClosed += (sender2, e2) =>
            {
                UpdatePanel();
            };
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PathPair pathPair = ((RichTextBox)sender).Tag as PathPair;
            Config.Config.Instance.DeletePair(pathPair);
            UpdatePanel();
        }

        #endregion
    }
}
