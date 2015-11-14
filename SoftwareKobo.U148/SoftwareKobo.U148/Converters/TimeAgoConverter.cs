using System;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class TimeAgoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset timeOffset;
            if (value is DateTime)
            {
                timeOffset = (DateTime)value;
            }
            else if (value is DateTimeOffset)
            {
                timeOffset = (DateTimeOffset)value;
            }
            else
            {
                return value;
            }

            TimeSpan expired = DateTimeOffset.Now - timeOffset;
            if (expired.TotalDays > 60)
            {
                return timeOffset.ToString();
            }
            else if (expired.TotalDays > 30)
            {
                return "1个月前";
            }
            else if (expired.TotalDays > 14)
            {
                return "2周前";
            }
            else if (expired.TotalDays > 7)
            {
                return "1周前";
            }
            else if (expired.TotalDays > 1)
            {
                return string.Format("{0}天前", (int)Math.Floor(expired.TotalDays));
            }
            else if (expired.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(expired.TotalHours));
            }
            else if (expired.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(expired.TotalMinutes));
            }
            else if (expired.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前", (int)Math.Floor(expired.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}