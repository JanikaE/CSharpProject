using System;
using System.Collections.Generic;
using System.Drawing;
using Utils.Tool;
using WinFormUtils.Helper;

namespace ImageEditor
{
    public class Editor
    {
        public static Image InvertColor(Image image)
        {
            return EditPixel(image, (color) =>
            {
                int r = color.R;
                int g = color.G;
                int b = color.B;
                return Color.FromArgb(255 - r, 255 - g, 255 - b);
            });
        }

        public static Image Monochrome(Image image)
        {
            return EditPixel(image, (color) =>
            {
                int r = color.R;
                int g = color.G;
                int b = color.B;
                int gray = (r + g + b) / 3;
                return Color.FromArgb(gray, gray, gray);
            });
        }

        public static Image BlackWhite(Image image)
        {
            return EditPixel(image, (color) =>
            {
                int r = color.R;
                int g = color.G;
                int b = color.B;
                int gray = (r + g + b) / 3;
                if (gray < 128)
                {
                    return Color.Black;
                }
                else
                {
                    return Color.White;
                }
            });
        }

        public static Image ClosestColor(Image image, Color[] colors)
        {
            return EditPixel(image, (color) =>
            {
                Color closestColor = color.FindClosestLab(colors);
                return closestColor;
            });
        }

        private static Bitmap EditPixel(Image image, Func<Color, Color> func)
        {
            using (HourGlass.New())
            {
                Bitmap bitmap = new(image);
                int w = bitmap.Width;
                int h = bitmap.Height;
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        Color pixelColor = bitmap.GetPixel(x, y);
                        bitmap.SetPixel(x, y, func(pixelColor));
                    }
                }
                return bitmap;
            }
        }

        /// <summary>
        /// 将图片按指定像素大小分割为若干子图
        /// </summary>
        /// <param name="image">来源图片</param>
        /// <param name="pixelSize">每块子图的边长（像素）</param>
        /// <returns>分割后的 Bitmap 列表，按行优先排列（从左到右、从上到下）</returns>
        public static List<Bitmap> SplitImage(Image image, int pixelSize)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            if (pixelSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pixelSize), "分割像素必须大于0");

            using (HourGlass.New())
            {
                Bitmap source = new(image);
                int w = source.Width;
                int h = source.Height;

                // 整除计算行列数，忽略不足一个完整块的边缘像素
                int cols = w / pixelSize;
                int rows = h / pixelSize;

                List<Bitmap> result = new(cols * rows);

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        int srcX = col * pixelSize;
                        int srcY = row * pixelSize;

                        Bitmap piece = new(pixelSize, pixelSize);
                        using Graphics g = Graphics.FromImage(piece);
                        g.DrawImage(source,
                            new Rectangle(0, 0, pixelSize, pixelSize),   // 目标矩形
                            new Rectangle(srcX, srcY, pixelSize, pixelSize), // 源矩形
                            GraphicsUnit.Pixel);

                        result.Add(piece);
                    }
                }

                return result;
            }
        }
    }
}
