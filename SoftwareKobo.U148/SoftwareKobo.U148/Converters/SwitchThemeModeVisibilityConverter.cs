using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class SwitchThemeModeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ElementTheme themeMode = (ElementTheme)value;
            string btn = (string)parameter;
            if (themeMode == ElementTheme.Dark)
            {
                if (btn == "SwitchLight")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else
            {
                if (btn == "SwitchLight")
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}