using System;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class DetailHtmlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Web/Html/template.html")).AsTask().GetAwaiter().GetResult();
            string content = FileIO.ReadTextAsync(file).AsTask().GetAwaiter().GetResult();
            return string.Format(content, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}