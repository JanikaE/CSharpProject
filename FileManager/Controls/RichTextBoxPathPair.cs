using FileManager.Configs;
using FileManager.Forms.Backups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace FileManager.Controls
{
    public partial class RichTextBoxPathPair : RichTextBox
    {
        public PathPair pathPair;
        public ContextMenuStrip contextMenuStrip;

        public RichTextBoxPathPair(PathPair pathPair)
        {
            InitializeComponent();
            this.pathPair = pathPair;

            ReadOnly = true;
            Text = pathPair.ToString();
            contextMenuStrip = new ContextMenuStrip();
            foreach (string operate in Enum.GetNames(typeof(Operates)))
            {
                contextMenuStrip.Items.Add(operate);
            }
            contextMenuStrip.ItemClicked += ContextMenuStrip_ItemClicked;
            ContextMenuStrip = contextMenuStrip;
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
            else if (e.ClickedItem.Text == Operates.ExecOne.ToString())
            {
                ExecOne();
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
            formAdd.ShowDialog(Global.FormMain);
        }

        private void ExecOne()
        {
            FormExecBackup formExec = new();
            formExec.Show();
            formExec.Execute(new List<PathPair>() { pathPair });
        }

        private void Delete()
        {
            Config.Instance.PathPairs.Remove(pathPair);
            Config.Instance.Save();
            Global.UIBackup.UpdatePanel();
        }
    }

    public enum Operates
    {
        OpenSourcePath,
        OpenTargetPath,
        Edit,
        ExecOne,
        Delete
    }
}
