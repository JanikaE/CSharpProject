using FileManager.Configs;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FileManager.Controls
{
    public partial class PathPairContextMenuStrip : ContextMenuStrip
    {
        public PathPair pathPair;
        public FormMain Form { get; set; }

        public PathPairContextMenuStrip(PathPair pathPair)
        {
            InitializeComponent();
            this.pathPair = pathPair;
        }

        public void Init()
        {
            foreach (string operate in Enum.GetNames(typeof(Operates)))
            {
                Items.Add(operate);
            }
            ItemClicked += ContextMenuStrip_ItemClicked;
        }

        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == Operates.OpenSourcePath.ToString())
            {
                OpenSourcePath();
            }
            else if (e.ClickedItem.Text == Operates.OpenTargetPath.ToString())
            {
                OpenTargetPath();
            }
            else if (e.ClickedItem.Text == Operates.Edit.ToString())
            {
                Edit();
            }
            else if (e.ClickedItem.Text == Operates.Delete.ToString())
            {
                Delete();
            }
        }

        private void OpenSourcePath()
        {
            Process.Start("explorer.exe", pathPair.SourcePath);
        }

        private void OpenTargetPath()
        {
            Process.Start("explorer.exe", pathPair.TargetPath);
        }

        private void Edit()
        {
            FormAdd formAdd = new(pathPair);
            formAdd.ShowDialog(Form);
        }

        private void Delete()
        {
            Config.Instance.PathPairs.Remove(pathPair);
            Config.Instance.Save();
            Form.UpdatePanel();
        }
    }

    public enum Operates
    {
        OpenSourcePath,
        OpenTargetPath,
        Edit,
        Delete
    }
}
