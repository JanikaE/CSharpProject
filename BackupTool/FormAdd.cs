using BackupTool.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupTool
{
    public partial class FormAdd : Form
    {
        public FormAdd()
        {
            InitializeComponent();

            textBoxSource.Click += SetPath;
            textBoxTarget.Click += SetPath;
        }

        private void SetPath(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
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
            string sourcePath = textBoxSource.Text;
            string targetPath = textBoxTarget.Text;
            if (!string.IsNullOrWhiteSpace(sourcePath) && !string.IsNullOrWhiteSpace(targetPath))
            {
                Config.Config.Instance.PathPairs.Add(new PathPair()
                {
                    SourcePath = sourcePath,
                    TargetPath = targetPath
                });
                Config.Config.Instance.Save();
                MessageBox.Show("添加成功！");
            }
        }
    }
}
