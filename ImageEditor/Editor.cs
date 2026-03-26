using System;
using System.Drawing;

namespace ImageEditor
{
    public class Editor
    {
        public static void InvertColor(Image image)
        {
            EditPixel(image, (color) =>
            {
                int r = color.R;
                int g = color.G;
                int b = color.B;
                return Color.FromArgb(255 - r, 255 - g, 255 - b);
            });
        }

        public static void Monochrome(Image image)
        {
            EditPixel(image, (color) =>
            {
                int r = color.R;
                int g = color.G;
                int b = color.B;
                int gray = (r + g + b) / 3;
                return Color.FromArgb(gray, gray, gray);
            });
        }

        public static void BlackWhite(Image image)
        {
            EditPixel(image, (color) =>
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

        private static void EditPixel(Image image, Func<Color, Color> func)
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
        }
    }
}
