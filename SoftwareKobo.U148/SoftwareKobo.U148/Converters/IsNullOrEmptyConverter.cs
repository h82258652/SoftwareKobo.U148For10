using System;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class IsNullOrEmptyConverter : IValueConverter
    {
        public bool IsInversed
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string str = value as string;
            if (str == null || str.Length <= 0)
            {
                return IsInversed == false;
            }

            return IsInversed != false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}