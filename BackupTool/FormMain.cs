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

        public void UpdatePanel()
        {
            foreach (Control control in panelPathPairs.Controls)
            {
                control.Dispose();
            }
            panelPathPairs.Controls.Clear();

            int y = panelPathPairs.Top;
            foreach (PathPair pathPair in Config.Config.Instance.PathPairs)
            {
                int height = 100;
                RichTextBox textBox = new RichTextBox
                {
                    ReadOnly = true,
                    Text = pathPair.ToString(),
                    Tag = pathPair,

                    Width = panelPathPairs.Width,
                    Height = height,  // todo: 高度自适应
                    Left = panelPathPairs.Left,
                    Top = y
                };

                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("Edit");
                contextMenuStrip.Items.Add("Delete");
                contextMenuStrip.Tag = textBox;
                contextMenuStrip.ItemClicked += ContextMenuStrip_ItemClicked;
                textBox.ContextMenuStrip = contextMenuStrip;

                y += height;                
                panelPathPairs.Controls.Add(textBox);
            }
        }

        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip menu = sender as ContextMenuStrip;
            RichTextBox textBox = menu.Tag as RichTextBox;
            PathPair pathPair = textBox.Tag as PathPair;
            if (e.ClickedItem.Text == "Edit")
            {
                Edit(pathPair);
            }
            else if(e.ClickedItem.Text == "Delete")
            {
                Delete(pathPair);
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
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit(PathPair pathPair)
        {
            FormAdd formAdd = new FormAdd(pathPair);
            formAdd.ShowDialog(this);
            formAdd.BringToFront();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete(PathPair pathPair)
        {
            Config.Config.Instance.PathPairs.Remove(pathPair);
            Config.Config.Instance.Save();
            UpdatePanel();
        }

        #endregion
    }
}
