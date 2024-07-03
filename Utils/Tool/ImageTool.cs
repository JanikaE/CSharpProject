using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Versioning;

namespace Utils.Tool
{
    public static class ImageTool
    {
        [SupportedOSPlatform("windows")]
        public static string BitmapToBase64(Bitmap bitmap, ImageFormat imageFormat)
        {
            MemoryStream stream = new();
            bitmap.Save(stream, imageFormat);
            byte[] bytes = stream.GetBuffer();
            return Convert.ToBase64String(bytes);
        }

        [SupportedOSPlatform("windows")]
        public static Bitmap Base64ToBitap(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream stream = new(bytes);
            return new Bitmap(stream);
        }

        [SupportedOSPlatform("windows")]
        public static string ImageToBase64(Image image, ImageFormat imageFormat)
        {
            MemoryStream stream = new();
            image.Save(stream, imageFormat);
            byte[] bytes = stream.GetBuffer();
            return Convert.ToBase64String(bytes);
        }
    }
}
