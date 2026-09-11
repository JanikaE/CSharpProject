using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using UsefulTools.Converters;
using Windows.ApplicationModel.DataTransfer;

namespace UsefulTools.Tabs
{
    public sealed partial class NameCaseTab : UserControl
    {
        public NameCaseTab()
        {
            InitializeComponent();
            CaseInputTextBoxHost.SizeChanged += (_, e) => CaseInputTextBox.Height = e.NewSize.Height;
            CaseOutputTextBoxHost.SizeChanged += (_, e) => CaseOutputTextBox.Height = e.NewSize.Height;
            CaseConvertButton.Click += ConvertNameCase;
            CaseCopyButton.Click += CopyConvertedResult;
        }

        private void CaseTextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Clip = new RectangleGeometry
                {
                    Rect = new Windows.Foundation.Rect(0, 0, e.NewSize.Width, e.NewSize.Height),
                };
            }
        }

        private void ConvertNameCase(object sender, RoutedEventArgs e)
        {
            CaseErrorTextBlock.Visibility = Visibility.Collapsed;
            CaseOutputTextBox.Text = "";

            var input = CaseInputTextBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                CaseErrorTextBlock.Text = "请输入 JSON 或 XML 字符串。";
                CaseErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }

            var targetCase = CaseFormatComboBox.SelectedIndex switch
            {
                0 => NameCase.CamelCase,
                1 => NameCase.PascalCase,
                2 => NameCase.SnakeCase,
                _ => NameCase.CamelCase
            };

            try
            {
                var result = NameCaseConverter.AutoConvert(input, targetCase);
                CaseOutputTextBox.Text = result;
            }
            catch (Exception ex)
            {
                CaseErrorTextBlock.Text = $"转换失败：{ex.Message}";
                CaseErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void CopyConvertedResult(object sender, RoutedEventArgs e)
        {
            var text = CaseOutputTextBox.Text;
            if (string.IsNullOrEmpty(text))
                return;

            var dataPackage = new DataPackage();
            dataPackage.SetText(text);
            Clipboard.SetContent(dataPackage);
        }
    }
}
