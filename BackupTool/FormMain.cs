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

            checkBoxIsShowIgnore.Checked = Config.Config.Instance.IsShowIgnore;
            UpdatePanel();
        }

        private void ButtonExec_Click(object sender, EventArgs e)
        {
            FormExec formExec = new FormExec();
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
                PathPairRichTextBox textBox = new PathPairRichTextBox(pathPair)
                {
                    ReadOnly = true,
                    Text = pathPair.ToString()
                };

                PathPairContextMenuStrip contextMenuStrip = new PathPairContextMenuStrip(pathPair);
                contextMenuStrip.Items.Add("Edit");
                contextMenuStrip.Items.Add("Delete");
                contextMenuStrip.ItemClicked += ContextMenuStrip_ItemClicked;
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

        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            PathPairContextMenuStrip menu = sender as PathPairContextMenuStrip;
            PathPair pathPair = menu.pathPair;
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
