using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace UsefulTools.Decoders
{
    /// <summary>
    /// JPEG 图片解码器 — 将字节数组解码为图片并显示
    /// </summary>
    public static class JpegDecoder
    {
        /// <summary>
        /// 将字节数组解码为 JPEG 图片并设置到 Image 控件
        /// </summary>
        /// <returns>成功返回 null，失败返回错误信息</returns>
        public static async Task<string> DecodeAndDisplayAsync(byte[] imageBytes, Image targetImage)
        {
            var stream = new InMemoryRandomAccessStream();
            await stream.WriteAsync(imageBytes.AsBuffer());
            stream.Seek(0);

            var bitmap = new BitmapImage();
            try
            {
                await bitmap.SetSourceAsync(stream);
            }
            catch (Exception ex)
            {
                return $"图片解码失败，数据可能不是有效的 JPEG 格式。\n({ex.Message})";
            }

            targetImage.Source = bitmap;
            targetImage.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            return null;
        }
    }
}
