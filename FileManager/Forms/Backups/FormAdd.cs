using FileManager.Configs;
using FileManager.Forms;
using System;
using System.Windows.Forms;

namespace FileManager.Forms.Backups
{
    public partial class FormAdd : ScalingForm_1
    {
        public FormAdd(PathPair pathPair)
        {
            InitializeComponent();
            InitTag();

            SetRectangle();

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
        }

        private PathPair PathPair { get; set; }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string sourcePath = textBoxSource.Text;
            string targetPath = textBoxTarget.Text;
            if (!string.IsNullOrWhiteSpace(sourcePath) && !string.IsNullOrWhiteSpace(targetPath))
            {
                if (buttonAdd.Text == "新增")
                {
                    Config.Instance.PathPairs.Add(new PathPair()
                    {
                        Name = name,
                        SourcePath = sourcePath,
                        TargetPath = targetPath
                    });
                }
                else if (buttonAdd.Text == "修改")
                {
                    PathPair.Name = name;
                    PathPair.SourcePath = sourcePath;
                    PathPair.TargetPath = targetPath;
                }
                Config.Instance.Save();
                Close();
            }
        }

        private void FormAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.UIBackup.UpdatePanel();
        }
    }
}
