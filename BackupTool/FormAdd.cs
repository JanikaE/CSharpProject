using BackupTool.Config;
using System;
using System.Windows.Forms;

namespace BackupTool
{
    public partial class FormAdd : Form
    {
        public FormAdd(PathPair pathPair)
        {
            InitializeComponent();

            if (pathPair != null)
            {
                textBoxName.Text = pathPair.Name;
                textBoxSource.Text = pathPair.SourcePath;
                textBoxTarget.Text = pathPair.TargetPath;
                PathPair = pathPair;

                Text = "修改路径";
                buttonAdd.Text = "修改";
            }
            else
            {
                Text = "新增路径";
                buttonAdd.Text = "新增";
            }

            buttonSetSourcePath.Click += SetPath;
            buttonSetTargetPath.Click += SetPath;
            buttonSetSourcePath.Tag = textBoxSource;
            buttonSetTargetPath.Tag = textBoxTarget;
        }

        private PathPair PathPair { get; set; }

        private void SetPath(object sender, EventArgs e)
        {
            Button button = sender as Button;
            TextBox textBox = button.Tag as TextBox;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                SelectedPath = textBox.Text,
                ShowNewFolderButton = false,
                Description = "选择一个文件夹。"
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog.SelectedPath != null)
                {
                    textBox.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string sourcePath = textBoxSource.Text;
            string targetPath = textBoxTarget.Text;
            if (!string.IsNullOrWhiteSpace(sourcePath) && !string.IsNullOrWhiteSpace(targetPath))
            {
                if (buttonAdd.Text == "新增")
                {
                    Config.Config.Instance.PathPairs.Add(new PathPair()
                    {
                        Name = name,
                        SourcePath = sourcePath,
                        TargetPath = targetPath
                    });
                    Config.Config.Instance.Save();
                    Close();
                }
                else if (buttonAdd.Text == "修改")
                {
                    PathPair.Name = name;
                    PathPair.SourcePath = sourcePath;
                    PathPair.TargetPath = targetPath;                 
                    Config.Config.Instance.Save();
                    Close();
                }
            }
        }

        private void FormAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain formMain = Owner as FormMain;
            formMain.UpdatePanel();
        }
    }
}
