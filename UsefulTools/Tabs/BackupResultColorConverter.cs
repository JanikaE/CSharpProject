using Microsoft.UI.Xaml.Data;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using System;
using UsefulTools.Helpers;

namespace UsefulTools.Tabs
{
    public class BackupResultColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value switch
            {
                BackupResultType.Error or BackupResultType.DirectoryNotFound => new SolidColorBrush(Colors.Red),
                BackupResultType.New => new SolidColorBrush(Colors.Green),
                BackupResultType.Overwrite => new SolidColorBrush(Colors.Blue),
                BackupResultType.Ignore => new SolidColorBrush(Colors.Gray),
                BackupResultType.CreateDirectory => new SolidColorBrush(Colors.Purple),
                _ => new SolidColorBrush(Colors.Black),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
