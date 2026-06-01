using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
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
        // PDF 相关状态
        private string _pdfFilePath = null;

        public MainWindow()
        {
            InitializeComponent();
            DecodeButton.Click += DecodeBase64AndShowImage;
            FormatComboBox.SelectionChanged += FormatComboBox_SelectionChanged;
        }

        /// <summary>
        /// 解码 Base64 字符串并显示为 JPEG 图片
        /// </summary>
        private async void DecodeBase64AndShowImage(object sender, RoutedEventArgs e)
        {
            // 1. 清空之前的错误信息和所有显示
            PdfViewer.CoreWebView2?.NavigateToString("<html><body></body></html>");
            PdfViewer.Visibility = Visibility.Collapsed;

            // 2. 获取输入文本，验证非空
            string input = InputTextBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                ShowError("请输入 Base64 字符串。");
                return;
            }

            // 3. 预处理：去除 Data URI 前缀
            string base64 = StripDataUriPrefix(input.Trim());

            // 4. 解码 Base64 为字节数组
            byte[] dataBytes;
            try
            {
                dataBytes = Convert.FromBase64String(base64);
            }
            catch (FormatException)
            {
                ShowError("Base64 解码失败，请检查输入格式。");
                return;
            }

            if (dataBytes.Length == 0)
            {
                ShowError("解码后的数据为空。");
                return;
            }

            // 5. 根据选择的格式分支处理
            if (FormatComboBox.SelectedIndex == 0) // JPG 图片
            {
                await DecodeAsJpegAsync(dataBytes);
            }
            else // PDF 文档
            {
                await DecodeAsPdfAsync(dataBytes);
            }
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

        /// <summary>
        /// 将字节数组解码为 JPEG 图片并显示
        /// </summary>
        private async Task DecodeAsJpegAsync(byte[] imageBytes)
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
                ShowError($"图片解码失败，数据可能不是有效的 JPEG 格式。\n({ex.Message})");
                return;
            }

            ResultImage.Source = bitmap;
            ResultImage.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 将字节数组解码为 PDF 文档并在 WebView2 中显示
        /// </summary>
        private async Task DecodeAsPdfAsync(byte[] pdfBytes)
        {
            // 验证 PDF 文件头（"%PDF"）
            if (pdfBytes.Length < 4 ||
                pdfBytes[0] != 0x25 || pdfBytes[1] != 0x50 ||
                pdfBytes[2] != 0x44 || pdfBytes[3] != 0x46)
            {
                ShowError("输入数据不是有效的 PDF 文件（文件头校验失败）。");
                return;
            }

            try
            {
                // 写入临时文件
                string tempPath = Path.Combine(
                    ApplicationData.Current.TemporaryFolder.Path,
                    $"decoded_{Guid.NewGuid():N}.pdf");
                await File.WriteAllBytesAsync(tempPath, pdfBytes);

                // 清理旧临时文件
                if (_pdfFilePath != null && File.Exists(_pdfFilePath))
                {
                    try { File.Delete(_pdfFilePath); }
                    catch { /* 忽略清理失败 */ }
                }
                _pdfFilePath = tempPath;

                // 加载 PDF（Source 属性会自动初始化 CoreWebView2）
                PdfViewer.Source = new Uri(tempPath);
                PdfViewer.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                ShowError($"PDF 加载失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 格式下拉框切换事件 — 调整界面控件可见性
        /// </summary>
        private async void FormatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 1. 清空显示内容
            CleanupPdfResources();
            ResultImage.Visibility = Visibility.Collapsed;

            // WebView2 通过导航到空白页来清空
            PdfViewer.CoreWebView2?.NavigateToString("<html><body></body></html>");
            PdfViewer.Visibility = Visibility.Collapsed;

            // 2. 清空错误提示
            ErrorTextBlock.Text = "";
            ErrorTextBlock.Visibility = Visibility.Collapsed;

            // 3. 重置 PDF 状态
            _pdfFilePath = null;

            // 4. 根据选择格式调整界面
            if (FormatComboBox.SelectedIndex == 0) // JPG 图片
            {
                DecodeButton.Content = "解码并显示";
                InputTextBox.Header = "请输入 Base64 字符串";
                InputTextBox.PlaceholderText = "支持纯 Base64 或 Data URI 格式...";
            }
            else // PDF 文档
            {
                DecodeButton.Content = "解码并显示 PDF";
                InputTextBox.Header = "请输入 PDF 的 Base64 字符串";
                InputTextBox.PlaceholderText = "输入 PDF 文件的 Base64 编码...";
            }
        }

        /// <summary>
        /// 清理 PDF 相关资源
        /// </summary>
        private void CleanupPdfResources()
        {
            if (_pdfFilePath != null && File.Exists(_pdfFilePath))
            {
                try { File.Delete(_pdfFilePath); }
                catch { /* 忽略清理失败 */ }
            }
            _pdfFilePath = null;
        }
    }
}
