using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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
            CaseConvertButton.Click += ConvertNameCase;
            CaseCopyButton.Click += CopyConvertedResult;
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
