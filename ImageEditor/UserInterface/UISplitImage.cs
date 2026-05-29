using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ImageEditor.UserInterface
{
    public partial class UISplitImage : UserControl
    {
        public MainForm MainForm { get; set; }

        /// <summary>
        /// 分割后的图片列表，按行优先排列
        /// </summary>
        private List<Bitmap> splitImages = null;
        /// <summary>
        /// 分割列数
        /// </summary>
        private int splitCols = 0;
        /// <summary>
        /// 分割行数
        /// </summary>
        private int splitRows = 0;

        public UISplitImage()
        {
            InitializeComponent();
            LoadConfig();
        }

        #region 配置持久化

        /// <summary>
        /// 从 Config 读取配置到控件
        /// </summary>
        private void LoadConfig()
        {
            numericUpDownSplit.Value = Config.Instance.SplitPixelSize;
            numericUpDownExport.Value = Config.Instance.ExportPixelSize;
            textBoxFileName.Text = Config.Instance.ExportFileName;
        }

        /// <summary>
        /// 将控件值保存到 Config 并持久化
        /// </summary>
        private void SaveConfig()
        {
            Config.Instance.SplitPixelSize = (int)numericUpDownSplit.Value;
            Config.Instance.ExportPixelSize = (int)numericUpDownExport.Value;
            Config.Instance.ExportFileName = textBoxFileName.Text;
            Config.Instance.Save();
        }

        #endregion

        #region Apply — 分割图片

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            if (MainForm?.ImportImage == null)
            {
                MessageBox.Show("请先在左侧导入图片。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveConfig();

            int splitSize = (int)numericUpDownSplit.Value;
            int exportSize = (int)numericUpDownExport.Value;

            // 分割图片
            splitImages = Editor.SplitImage(MainForm.ImportImage, splitSize);

            // 计算行列数
            Bitmap source = new(MainForm.ImportImage);
            splitCols = source.Width / splitSize;
            splitRows = source.Height / splitSize;
            source.Dispose();

            // 更新信息标签
            labelGridInfo.Text = $"Grid: {splitCols}×{splitRows} = {splitImages.Count} pieces";

            // 在 FlowLayoutPanel 中展示预览缩略图
            ShowPreview(exportSize);
            // 在主界面右侧展示分割线
            ShowSplitLint();
        }

        /// <summary>
        /// 在 FlowLayoutPanel 中展示分割后的小缩略图
        /// </summary>
        private void ShowPreview(int previewSize)
        {
            flowLayoutPanel.Controls.Clear();

            // 缩略图最大显示尺寸
            const int maxThumb = 48;
            int thumbSize = Math.Min(previewSize, maxThumb);

            foreach (var img in splitImages)
            {
                // 创建缩略图
                Bitmap thumb = new(img, new Size(thumbSize, thumbSize));
                PictureBox pb = new()
                {
                    Image = thumb,
                    Size = new Size(thumbSize, thumbSize),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Margin = new Padding(1)
                };
                flowLayoutPanel.Controls.Add(pb);
            }
        }

        /// <summary>
        /// 在主界面右侧展示分割线
        /// </summary>
        private void ShowSplitLint()
        {
            int splitSize = (int)numericUpDownSplit.Value;
            var image = new Bitmap(MainForm.ImportImage);
            using Graphics graphics = Graphics.FromImage(image);
            Pen pen = new(Color.Black, 1);
            for (int i = 1; i <= splitCols; i++)
            {
                int x = i * splitSize;
                graphics.DrawLine(pen, new PointF(x, 0), new PointF(x, image.Height));
            }
            for (int i = 1; i <= splitRows; i++)
            {
                int y = i * splitSize;
                graphics.DrawLine(pen, new PointF(0, y), new PointF(image.Width, y));
            }
            MainForm.ExportImage = image;
        }

        #endregion

        #region Export — 导出分割图片

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            if (splitImages == null || splitImages.Count == 0)
            {
                MessageBox.Show("请先点击 Apply 分割图片。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveConfig();

            // 让用户选择导出目录
            FolderBrowserDialog folderDialog = new()
            {
                Description = "选择导出目录",
                SelectedPath = Config.Instance.LastExportDirectory
            };

            if (folderDialog.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(folderDialog.SelectedPath))
                return;

            string exportDir = folderDialog.SelectedPath;
            Config.Instance.LastExportDirectory = exportDir;
            Config.Instance.Save();

            int exportSize = (int)numericUpDownExport.Value;
            string baseName = textBoxFileName.Text;
            if (string.IsNullOrWhiteSpace(baseName))
                baseName = "split";

            try
            {
                int rowDigits = splitRows.ToString().Length;
                int colDigits = splitCols.ToString().Length;
                int index = 0;
                for (int row = 1; row <= splitRows; row++)
                {
                    for (int col = 1; col <= splitCols; col++)
                    {
                        Bitmap piece = splitImages[index];
                        // 如果导出像素与分割像素不同，需要缩放
                        Bitmap output = piece;
                        if (exportSize != piece.Width || exportSize != piece.Height)
                        {
                            output = new Bitmap(piece, new Size(exportSize, exportSize));
                        }

                        string fileName = $"{baseName}{row.ToString($"D{rowDigits}")}-{col.ToString($"D{colDigits}")}.png";
                        string fullPath = Path.Combine(exportDir, fileName);
                        output.Save(fullPath, ImageFormat.Png);

                        // 如果创建了缩放副本，释放它（不释放原始 piece，它在 splitImages 列表中）
                        if (output != piece)
                            output.Dispose();

                        index++;
                    }
                }

                MessageBox.Show($"成功导出 {splitImages.Count} 张图片到:\n{exportDir}", "导出完成",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导出失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
