using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UsefulTools
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            DecodeButton.Click += DecodeBase64AndShowImage;
        }

        /// <summary>
        /// 解码 Base64 字符串并显示为 JPEG 图片
        /// </summary>
        private async void DecodeBase64AndShowImage(object sender, RoutedEventArgs e)
        {
            // 1. 清空之前的错误信息和图片
            ErrorTextBlock.Visibility = Visibility.Collapsed;
            ResultImage.Visibility = Visibility.Collapsed;
            ResultImage.Source = null;

            // 2. 获取输入文本，验证非空
            string input = InputTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(input))
            {
                ShowError("请输入 Base64 字符串。");
                return;
            }

            // 3. 预处理：去除 Data URI 前缀（如 "data:image/jpeg;base64,"）
            string base64 = StripDataUriPrefix(input);

            // 4. Base64 解码为字节数组
            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(base64);
            }
            catch (FormatException)
            {
                ShowError("Base64 字符串格式无效，请检查输入。");
                return;
            }

            if (imageBytes.Length == 0)
            {
                ShowError("解码后的数据为空。");
                return;
            }

            // 5. 创建 InMemoryRandomAccessStream 并写入字节
            var stream = new InMemoryRandomAccessStream();
            await stream.WriteAsync(imageBytes.AsBuffer());
            stream.Seek(0);

            // 6. 创建 BitmapImage 并设置源
            var bitmap = new BitmapImage();
            try
            {
                await bitmap.SetSourceAsync(stream);
            }
            catch (Exception ex)
            {
                ShowError($"图片解码失败，数据可能不是有效的 JPEG 格式。\n({ex.Message})");
                return;
            }

            // 7. 显示结果
            ResultImage.Source = bitmap;
            ResultImage.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 去除 Data URI 前缀（如 "data:image/jpeg;base64,"），返回纯 Base64 字符串
        /// </summary>
        private static string StripDataUriPrefix(string input)
        {
            // 匹配 "data:image/...;base64," 前缀
            int base64Index = input.IndexOf(";base64,", StringComparison.OrdinalIgnoreCase);
            if (base64Index >= 0)
            {
                return input.Substring(base64Index + ";base64,".Length);
            }
            return input;
        }

        /// <summary>
        /// 在错误文本块中显示错误信息
        /// </summary>
        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }
    }
}
