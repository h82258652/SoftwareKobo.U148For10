using SoftwareKobo.U148.Utils;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Converters
{
    public class ImageCacheConverter : IValueConverter
    {
        private const string CacheFolderName = @"ImageCache";

        private static bool IsNetworkAvailable()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            return profile != null && profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var url = value as string;
            if (url == null)
            {
                return value;
            }

            if (url.StartsWith("http:", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https:", StringComparison.OrdinalIgnoreCase))
            {
                var cacheFileName = GetCacheFileName(url);
                var cacheFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, CacheFolderName,
                    cacheFileName);
                if (File.Exists(cacheFilePath))
                {
                    return cacheFilePath;
                }
                if (IsNetworkAvailable())
                {
                    DownloadImageAndCache(url, cacheFilePath);
                }
            }

            return value;
        }

        private static async void DownloadImageAndCache(string url, string cacheFilePath)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var bytes = await client.GetByteArrayAsync(url);
                    if (bytes.Length > 0)
                    {
                        await Task.Run(() =>
                        {
                            Directory.CreateDirectory(Path.Combine(ApplicationData.Current.LocalFolder.Path, CacheFolderName));
                            File.WriteAllBytes(cacheFilePath, bytes);
                        });
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public static string GetCacheFileName(string url)
        {
            return Hash.GetMd5(url) + url.Length + Hash.GetSha1(url) + Path.GetExtension(url);
        }
    }
}