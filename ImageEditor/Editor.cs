using System.Drawing;

namespace ImageEditor
{
    public class Editor
    {
        public static void Edit(string input)
        {
            string[] args = input.Split(' ');
            string fileName = args[0];
            string type = "i";
            if (args.Length > 1)
            {
                type = args[1];
            }
            switch (type)
            {
                case "i":
                    InvertColor(fileName);
                    break;
                case "m":
                    Monochrome(fileName);
                    break;
                case "b":
                    BlackWhite(fileName);
                    break;
            }
        }

        public static void InvertColor(string fileName)
        {
            Image image = Image.FromFile(fileName);
            Bitmap bitmap = new(image);
            int w = bitmap.Width;
            int h = bitmap.Height;
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    int r = pixelColor.R;
                    int g = pixelColor.G;
                    int b = pixelColor.B;
                    Color newColor = Color.FromArgb(255 - r, 255 - g, 255 - b);
                    bitmap.SetPixel(x, y, newColor);
                }
            }
            bitmap.Save("D:\\TestFile\\InvertColor.png");
        }

        public static void Monochrome(string fileName)
        {
            Image image = Image.FromFile(fileName);
            Bitmap bitmap = new(image);
            int w = bitmap.Width;
            int h = bitmap.Height;
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    int r = pixelColor.R;
                    int g = pixelColor.G;
                    int b = pixelColor.B;
                    int gray = (r + g + b) / 3;
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    bitmap.SetPixel(x, y, newColor);
                }
            }
            bitmap.Save("D:\\TestFile\\Monochrome.png");
        }

        public static void BlackWhite(string fileName)
        {
            Image image = Image.FromFile(fileName);
            Bitmap bitmap = new(image);
            int w = bitmap.Width;
            int h = bitmap.Height;
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    int r = pixelColor.R;
                    int g = pixelColor.G;
                    int b = pixelColor.B;
                    int gray = (r + g + b) / 3;
                    if (gray < 128)
                    {
                        bitmap.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        bitmap.SetPixel(x, y, Color.White);
                    }
                }
            }
            bitmap.Save("D:\\TestFile\\BlackWhite.png");
        }
    }
}
