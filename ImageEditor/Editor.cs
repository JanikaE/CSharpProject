using System;
using System.Drawing;

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

        private static Image EditPixel(Image image, Func<Color, Color> func)
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
}
