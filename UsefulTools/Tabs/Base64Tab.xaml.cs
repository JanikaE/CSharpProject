using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using UsefulTools.Decoders;
using UsefulTools.Helpers;

namespace UsefulTools.Tabs
{
    public sealed partial class Base64Tab : UserControl
    {
        private readonly PdfDecoder _pdfDecoder = new();
        private readonly ExcelDecoder _excelDecoder = new();

        public Base64Tab()
        {
            InitializeComponent();
            DecodeButton.Click += DecodeBase64AndShowImage;
            FormatComboBox.SelectionChanged += FormatComboBox_SelectionChanged;
            Unloaded += OnUnloaded;
        }

        private async void DecodeBase64AndShowImage(object sender, RoutedEventArgs e)
        {
            ClearAllDisplays();

            string input = InputTextBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                ShowError("请输入 Base64 字符串。");
                return;
            }

            string base64 = Base64Helper.StripDataUriPrefix(input);
            byte[] dataBytes = Base64Helper.TryDecode(base64);
            if (dataBytes == null)
            {
                ShowError("Base64 解码失败，请检查输入格式。");
                return;
            }
            if (dataBytes.Length == 0)
            {
                ShowError("解码后的数据为空。");
                return;
            }

            string error = FormatComboBox.SelectedIndex switch
            {
                0 => await JpegDecoder.DecodeAndDisplayAsync(dataBytes, ResultImage),
                1 => await _pdfDecoder.DecodeAndDisplayAsync(dataBytes, PdfViewer),
                _ => await _excelDecoder.DecodeAndDisplayAsync(dataBytes, ExcelViewer),
            };

            if (error != null)
                ShowError(error);
        }

        private void FormatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearAllDisplays();
            _pdfDecoder.CleanupTempFile();
            _excelDecoder.CleanupTempFile();
            ClearError();

            (DecodeButton.Content, InputTextBox.Header, InputTextBox.PlaceholderText) =
                FormatComboBox.SelectedIndex switch
                {
                    0 => ("解码并显示", "请输入 Base64 字符串", "支持纯 Base64 或 Data URI 格式..."),
                    1 => ("解码并显示 PDF", "请输入 PDF 的 Base64 字符串", "输入 PDF 文件的 Base64 编码..."),
                    _ => ("解码并显示 Excel", "请输入 Excel 的 Base64 字符串", "输入 Excel 文件的 Base64 编码 (.xlsx/.xls)..."),
                };
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _pdfDecoder.Dispose();
            _excelDecoder.Dispose();
        }

        private void ClearAllDisplays()
        {
            PdfViewer.CoreWebView2?.NavigateToString("<html><body></body></html>");
            PdfViewer.Visibility = Visibility.Collapsed;
            ExcelViewer.CoreWebView2?.NavigateToString("<html><body></body></html>");
            ExcelViewer.Visibility = Visibility.Collapsed;
            ResultImage.Visibility = Visibility.Collapsed;
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }

        private void ClearError()
        {
            ErrorTextBlock.Text = "";
            ErrorTextBlock.Visibility = Visibility.Collapsed;
        }
    }
}
