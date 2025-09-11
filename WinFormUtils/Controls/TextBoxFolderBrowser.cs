using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUtils.Controls
{
    public partial class TextBoxFolderBrowser : UserControl
    {
        public override string Text
        {
            get => textBox.Text;
            set
            {
                value ??= string.Empty;
                textBox.Text = value;
            }
        }

        public TextBoxFolderBrowser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new()
            {
                SelectedPath = textBox.Text,
                Description = "选择一个文件夹。"
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK && folderBrowserDialog.SelectedPath != null)
            {
                textBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
