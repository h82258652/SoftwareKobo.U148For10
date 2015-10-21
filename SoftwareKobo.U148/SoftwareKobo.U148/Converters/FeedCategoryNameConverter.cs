using SoftwareKobo.U148.Models;
using System;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class FeedCategoryNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is FeedCategory)
            {
                FeedCategory category = (FeedCategory)value;
                switch (category)
                {
                    case FeedCategory.Home:
                        return "首页";

                    case FeedCategory.Weibo:
                        return "微博";

                    case FeedCategory.Video:
                        return "影像";

                    case FeedCategory.Image:
                        return "图画";

                    case FeedCategory.Game:
                        return "游戏";

                    case FeedCategory.Audio:
                        return "音频";

                    case FeedCategory.Text:
                        return "文字";

                    case FeedCategory.Mix:
                        return "杂粹";

                    case FeedCategory.Piao:
                        return "漂流";

                    case FeedCategory.Fair:
                        return "集市";

                    case FeedCategory.Tasty:
                        return "短品";
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