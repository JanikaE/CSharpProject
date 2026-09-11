using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Threading.Tasks;
using UsefulTools.Decoders;
using UsefulTools.Helpers;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace UsefulTools.Tabs
{
    public sealed partial class Base64Tab : UserControl
    {
        private readonly PdfDecoder _pdfDecoder = new();
        private readonly ExcelDecoder _excelDecoder = new();
        private byte[] _decodedBytes;

        public Base64Tab()
        {
            InitializeComponent();
            InputTextBoxHost.SizeChanged += (_, e) => InputTextBox.Height = e.NewSize.Height;
            DecodeButton.Click += DecodeBase64AndShowImage;
            ExportButton.Click += ExportDecodedContent;
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
            {
                ShowError(error);
                return;
            }

            _decodedBytes = dataBytes;
            ExportButton.IsEnabled = true;
            ClearError();
        }

        private async void ExportDecodedContent(object sender, RoutedEventArgs e)
        {
            if (_decodedBytes == null || _decodedBytes.Length == 0)
            {
                ShowError("请先解码成功的 Base64 内容。");
                return;
            }

            string ext = FormatComboBox.SelectedIndex switch
            {
                0 => ".jpg",
                1 => ".pdf",
                _ => ExcelDecoder.IsValidExcel(_decodedBytes)
                    ? (_decodedBytes[0] == 0x50 ? ".xlsx" : ".xls")
                    : ".xlsx",
            };

            var picker = new FolderPicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
            picker.FileTypeFilter.Add("*");

            StorageFolder folder = await picker.PickSingleFolderAsync();
            if (folder == null)
                return;

            try
            {
                string fileName = $"decoded_{DateTime.Now:yyyyMMdd_HHmmss}{ext}";
                StorageFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);
                await FileIO.WriteBytesAsync(file, _decodedBytes);
                await ShowSuccessDialog($"导出成功：\n{file.Path}");
            }
            catch (Exception ex)
            {
                ShowError($"导出失败：{ex.Message}");
            }
        }

        private async Task ShowSuccessDialog(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "导出完成",
                Content = message,
                CloseButtonText = "确定",
                XamlRoot = this.XamlRoot,
            };
            await dialog.ShowAsync();
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

        private void InputTextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InputTextBox.Clip = new RectangleGeometry
            {
                Rect = new Windows.Foundation.Rect(0, 0, e.NewSize.Width, e.NewSize.Height),
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
            _decodedBytes = null;
            ExportButton.IsEnabled = false;
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
