using Windows.UI.Xaml;

namespace SoftwareKobo.U148.Extensions
{
    public static class ResourceDictionaryExtensions
    {
        public static object FindValue(this ResourceDictionary resouce, string key)
        {
            if (resouce.ContainsKey(key))
            {
                // 该资源字典直接含有该值。
                return resouce[key];
            }
            else
            {
                // 从主题字典查找。
                foreach (var theme in resouce.ThemeDictionaries)
                {
                    if ((string)theme.Key == key)
                    {
                        return theme.Value;
                    }
                }
                // 从包含的字典查找。
                foreach (var childResouce in resouce.MergedDictionaries)
                {
                    var value = FindValue(childResouce, key);
                    if (value != null)
                    {
                        return value;
                    }
                }
                return null;
            }
        }

        public static T FindValue<T>(this ResourceDictionary resouce, string key)
        {
            return (T)FindValue(resouce, key);
        }
    }
}