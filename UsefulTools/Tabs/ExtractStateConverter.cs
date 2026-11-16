using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;

namespace UsefulTools.Tabs
{
    /// <summary>
    /// 将解压状态转换为界面上显示的文本。
    /// </summary>
    public partial class ExtractStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value switch
            {
                ExtractState.Running => "解压中...",
                ExtractState.Completed => "已完成",
                ExtractState.Failed => "失败",
                _ => "等待中",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 将解压状态转换为用于状态文本的前景色。
    /// </summary>
    public partial class ExtractStateColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value switch
            {
                ExtractState.Running => new SolidColorBrush(Colors.DodgerBlue),
                ExtractState.Completed => new SolidColorBrush(Colors.SeaGreen),
                ExtractState.Failed => new SolidColorBrush(Colors.IndianRed),
                _ => new SolidColorBrush(Colors.Gray),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}