using System;
using Windows.Data.Html;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class HtmlToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string html = value as string;
            if (html != null)
            {
                return HtmlUtilities.ConvertToText(html);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}