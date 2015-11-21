using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class Int32ToVisibilityConverter : IValueConverter
    {
        public bool IsInversed
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int i = (int)value;
            if (i > 0)
            {
                return IsInversed ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return IsInversed ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}