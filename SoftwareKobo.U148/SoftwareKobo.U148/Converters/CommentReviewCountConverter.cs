using System;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class CommentReviewCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int)
            {
                int i = (int)value;
                if (i > 99)
                {
                    return "99+";
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}