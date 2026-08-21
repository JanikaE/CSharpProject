using Microsoft.UI.Xaml.Data;
using System;
using UsefulTools.Helpers;

namespace UsefulTools.Tabs
{
    public partial class BackupPolicyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int policy = value is int i ? i : 0;
            return policy switch
            {
                (int)BackupPolicy.Append => "策略:追加",
                _ => "策略:覆盖",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}