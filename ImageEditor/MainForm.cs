using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class MainForm : Form
    {
        private ImageViewForm ImageViewFormImport = null;
        private ImageViewForm ImageViewFormExport = null;

        public MainForm()
        {
            InitializeComponent();
            pictureBoxImport.Click += PictureBox_Click;
            pictureBoxExport.Click += PictureBox_Click;
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            var pictureBox = (PictureBox)sender;

            if (pictureBox == pictureBoxImport)
            {
                HandlePictureBoxClick(pictureBox, ref ImageViewFormImport);
            }
            else if (pictureBox == pictureBoxExport)
            {
                HandlePictureBoxClick(pictureBox, ref ImageViewFormExport);
            }
        }

        private static void HandlePictureBoxClick(PictureBox pictureBox, ref ImageViewForm imageViewForm)
        {
            if (imageViewForm == null || imageViewForm.IsDisposed)
            {
                if (pictureBox.Image != null)
                {
                    imageViewForm = new ImageViewForm(pictureBox.Image);
                    imageViewForm.Show();
                    imageViewForm.BringToFront();
                }
            }
            else
            {
                if (imageViewForm.WindowState == FormWindowState.Minimized) imageViewForm.WindowState = FormWindowState.Normal;
                imageViewForm.BringToFront();
            }
        }

        private void ButtonImport_Click(object o, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Multiselect = false,
                Title = "选择图片",
                Filter = "图片(*.jpg)|*.jpg"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName != "")
            {
                pictureBoxImport.ImageLocation = openFileDialog.FileName;
            }
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            if (pictureBoxExport.Image == null) return;

            SaveFileDialog saveFileDialog = new()
            {
                Title = "保存图片",
                Filter = "图片(*.jpg)|*.jpg",
                AddExtension = true,
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
            {
                Image image = pictureBoxExport.Image;
                image.Save(saveFileDialog.FileName);
            }
        }
    }
}