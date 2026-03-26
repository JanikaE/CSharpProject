using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageEditor
{
    public class ImageViewForm : Form
    {
        private Panel panelCanvas;
        private PictureBox pictureBox;
        private HScrollBar hScrollBar;
        private VScrollBar vScrollBar;
        private readonly Image originalImage;
        private float scale = 1.0f;
        private bool isDragging = false;
        /// <summary>
        /// 拖拽开始时的鼠标位置（相对于 panelCanvas）
        /// </summary>
        private PointF dragStartMouse;
        /// <summary>
        /// 拖拽开始时的图片偏移量
        /// </summary>
        private PointF dragStartOffset;

        public ImageViewForm(Image image)
        {
            originalImage = image ?? throw new ArgumentNullException(nameof(image));
            InitializeComponents();
            InitializeImage();
            AttachEvents();
        }

        private void InitializeComponents()
        {
            Text = "图像查看器";
            Size = new Size(800, 600);
            DoubleBuffered = true;

            // 画板容器
            panelCanvas = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = false,
                BackColor = Color.FromArgb(45, 45, 48)
            };

            // 图片控件，拉伸模式使图像随控件大小变化
            pictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            // 水平滚动条
            hScrollBar = new HScrollBar
            {
                Dock = DockStyle.Bottom,
                Height = 20,
                SmallChange = 10,
                LargeChange = 50
            };

            // 垂直滚动条
            vScrollBar = new VScrollBar
            {
                Dock = DockStyle.Right,
                Width = 20,
                SmallChange = 10,
                LargeChange = 50
            };

            // 按顺序添加控件，滚动条会占用边缘，panelCanvas 自动填充剩余区域
            Controls.Add(panelCanvas);
            Controls.Add(hScrollBar);
            Controls.Add(vScrollBar);
            panelCanvas.Controls.Add(pictureBox);
        }

        private void InitializeImage()
        {
            pictureBox.Image = originalImage;
            scale = 1.0f;
            UpdateImageSizeAndScroll();
        }

        private void AttachEvents()
        {
            Resize += (s, e) => UpdateImageSizeAndScroll();
            panelCanvas.MouseWheel += PanelCanvas_MouseWheel;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            hScrollBar.Scroll += ScrollBar_Scroll;
            vScrollBar.Scroll += ScrollBar_Scroll;
        }

        /// <summary>
        /// 根据当前缩放比例设置 PictureBox 大小，并更新滚动条与位置
        /// </summary>
        private void UpdateImageSizeAndScroll()
        {
            if (originalImage == null) return;

            int newWidth = (int)(originalImage.Width * scale);
            int newHeight = (int)(originalImage.Height * scale);
            pictureBox.Size = new Size(newWidth, newHeight);

            UpdateScrollBarRange();
            ClampAndApplyOffset();
        }

        /// <summary>
        /// 更新滚动条的范围（基于图像与面板的大小差异）
        /// </summary>
        private void UpdateScrollBarRange()
        {
            int panelW = panelCanvas.ClientSize.Width;
            int panelH = panelCanvas.ClientSize.Height;
            int imgW = pictureBox.Width;
            int imgH = pictureBox.Height;

            // 水平滚动条
            if (imgW > panelW)
            {
                hScrollBar.Maximum = imgW - panelW;
                hScrollBar.Enabled = true;
                hScrollBar.Visible = true;
            }
            else
            {
                hScrollBar.Maximum = 0;
                hScrollBar.Enabled = false;
                hScrollBar.Visible = false;
            }

            // 垂直滚动条
            if (imgH > panelH)
            {
                vScrollBar.Maximum = imgH - panelH;
                vScrollBar.Enabled = true;
                vScrollBar.Visible = true;
            }
            else
            {
                vScrollBar.Maximum = 0;
                vScrollBar.Enabled = false;
                vScrollBar.Visible = false;
            }

            // 调整滚动条步进值
            hScrollBar.LargeChange = Math.Max(1, panelW / 2);
            vScrollBar.LargeChange = Math.Max(1, panelH / 2);
        }

        /// <summary>
        /// 限制图片偏移量在合法范围内，并应用位置（同时同步滚动条值）
        /// </summary>
        private void ClampAndApplyOffset()
        {
            int panelW = panelCanvas.ClientSize.Width;
            int panelH = panelCanvas.ClientSize.Height;
            int imgW = pictureBox.Width;
            int imgH = pictureBox.Height;

            // 当前偏移
            int offsetX = pictureBox.Left;
            int offsetY = pictureBox.Top;

            // 允许的偏移范围
            int minX = panelW - imgW;   // 负值（图片比面板大时）或正值（图片比面板小时）
            int maxX = 0;
            int minY = panelH - imgH;
            int maxY = 0;

            // 如果图片比面板小，强制居中（不允许拖拽超出，避免图像移出视野）
            if (imgW <= panelW)
                offsetX = (panelW - imgW) / 2;
            else
                offsetX = Math.Max(minX, Math.Min(maxX, offsetX));

            if (imgH <= panelH)
                offsetY = (panelH - imgH) / 2;
            else
                offsetY = Math.Max(minY, Math.Min(maxY, offsetY));

            pictureBox.Location = new Point(offsetX, offsetY);

            // 同步滚动条的值（滚动条值为正，表示向左/上偏移的距离）
            try
            {
                if (hScrollBar.Visible)
                    hScrollBar.Value = -offsetX;
                if (vScrollBar.Visible)
                    vScrollBar.Value = -offsetY;
            }
            catch { }
        }

        /// <summary>
        /// 滚动条事件：改变图片位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            int newX = -hScrollBar.Value;
            int newY = -vScrollBar.Value;
            pictureBox.Location = new Point(newX, newY);
            // 确保不超出边界（当滚动条最大值变化时，值可能已经合法，但再检查一次）
            ClampAndApplyOffset();
        }

        /// <summary>
        /// 鼠标滚轮缩放（以光标为中心）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            // 获取鼠标相对于 panelCanvas 的位置
            Point mousePos = panelCanvas.PointToClient(Control.MousePosition);
            if (!panelCanvas.ClientRectangle.Contains(mousePos))
                return;

            float zoomFactor = e.Delta > 0 ? 1.1f : 0.9f;
            float newScale = scale * zoomFactor;

            // 限制缩放比例范围
            const float minScale = 0.1f;
            const float maxScale = 10.0f;
            newScale = Math.Max(minScale, Math.Min(maxScale, newScale));
            if (Math.Abs(newScale - scale) < 0.001f) return;

            // 当前图片偏移和尺寸
            int offsetX = pictureBox.Left;
            int offsetY = pictureBox.Top;
            //int oldW = pictureBox.Width;
            //int oldH = pictureBox.Height;

            // 计算鼠标位置在图片上的对应点（图片坐标）
            float imgX = mousePos.X - offsetX;
            float imgY = mousePos.Y - offsetY;

            // 新尺寸
            int newW = (int)(originalImage.Width * newScale);
            int newH = (int)(originalImage.Height * newScale);

            // 计算新偏移量，使图片上同一点仍位于鼠标下方
            int newOffsetX = (int)(mousePos.X - imgX);
            int newOffsetY = (int)(mousePos.Y - imgY);

            // 应用新缩放比例和尺寸
            scale = newScale;
            pictureBox.Size = new Size(newW, newH);

            // 更新滚动条范围（基于新尺寸）
            UpdateScrollBarRange();

            // 临时设置新偏移，然后进行边界修正
            pictureBox.Location = new Point(newOffsetX, newOffsetY);
            ClampAndApplyOffset();
        }

        /// <summary>
        /// 鼠标拖拽移动图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 仅当图片比面板大时才允许拖拽（小图已居中，无需拖拽）
                if (pictureBox.Width > panelCanvas.ClientSize.Width ||
                    pictureBox.Height > panelCanvas.ClientSize.Height)
                {
                    isDragging = true;
                    dragStartMouse = panelCanvas.PointToClient(Control.MousePosition);
                    dragStartOffset = new PointF(pictureBox.Left, pictureBox.Top);
                    pictureBox.Capture = true;
                }
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentMouse = panelCanvas.PointToClient(MousePosition);
                float dx = currentMouse.X - dragStartMouse.X;
                float dy = currentMouse.Y - dragStartMouse.Y;

                int newX = (int)(dragStartOffset.X + dx);
                int newY = (int)(dragStartOffset.Y + dy);

                pictureBox.Location = new Point(newX, newY);
                // 实时限制边界，避免图片完全拖出视野
                ClampAndApplyOffset();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                pictureBox.Capture = false;
            }
        }
    }
}