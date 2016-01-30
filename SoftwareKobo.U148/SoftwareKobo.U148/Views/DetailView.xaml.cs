using JiuYouAdUniversal;
using JiuYouAdUniversal.Models;
using JYAnalyticsUniversal;
using MicroMsg;
using MicroMsg.sdk;
using Porrey.Uwp.Ntp;
using SoftwareKobo.Social.Sina.Weibo;
using SoftwareKobo.Social.Sina.Weibo.Models;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Extensions;
using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Extensions;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class DetailView : Page, IView
    {
        private bool _isExecuting;

        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                popup.IsOpen = false;
                ExecutingMask.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                appBar.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private TaskCompletionSource<object> _domReadyTcs;

        public DetailView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        public async void ReceiveFromViewModel(dynamic parameter)
        {
            Article article = parameter as Article;
            if (article != null)
            {
                await this._domReadyTcs.Task;
                await webView.InvokeScriptAsync("setThemeMode", AppSettings.Instance.ThemeMode.ToString());
                await webView.InvokeScriptAsync("setContent", article.Content);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            JYAnalytics.TrackPageEnd(nameof(DetailView));

            Messenger.Unregister(this);

            NavigationHelper.Unregister(this.Frame);
        }

        private Feed _feed;

        private void GoBack()
        {
            if (this.Frame.CanGoBack && this.IsExecuting == false)
            {
                this.Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            JYAnalytics.TrackPageStart(nameof(DetailView));

            Messenger.Register(this);

            if (this.Frame.CanGoBack)
            {
                NavigationHelper.Register(this.Frame, () =>
                {
                    GoBack();
                });
            }

            if (e.NavigationMode != NavigationMode.Back)
            {
                this._domReadyTcs = new TaskCompletionSource<object>();
                TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs> handler = null;
                handler = (sender, args) =>
                {
                    webView.DOMContentLoaded -= handler;
                    this._domReadyTcs.SetResult(null);
                };
                webView.DOMContentLoaded += handler;
                webView.Navigate(new Uri("ms-appx-web:///Web/Views/app.html"));

                Feed feed = e.Parameter as Feed;
                if (feed != null)
                {
                    _feed = feed;
                    this.SendToViewModel(feed);
                }
            }

            SetAdVisibility();
        }

        private async void SetAdVisibility()
        {
            if (_ad == null)
            {
                return;
            }
            NtpClient client = new NtpClient();
            try
            {
                DateTime? time = await client.GetAsync(NTPSERVERS);
                if (time.HasValue)
                {
                    if (AppSettings.Instance.LastClickAdTime + TimeSpan.FromHours(12) >= time.Value)
                    {
                        _ad.Visibility = Visibility.Collapsed;
                        return;
                    }
                }
            }
            catch
            {
            }
            _ad.Visibility = Visibility.Visible;
        }

        private AdControl _ad;

        private void DetailView_Loaded(object sender, RoutedEventArgs e)
        {
            if (AdContainer.Children.Count == 0)
            {
                try
                {
                    _ad = new AdControl()
                    {
                        Visibility = Visibility.Collapsed,
                        AdType = AdType.Banner,
                        ApplicationKey = "4fd64ae0ccd8d54db0dd45d8ac733fb7"
                    };
                    _ad.AdClick += Ad_Click;
                    AdContainer.Children.Add(_ad);
                }
                catch
                {
                    // ignored
                }
            }

            SetAdVisibility();
        }

        private static readonly string[] NTPSERVERS = new string[]
        {
            "time.windows.com",
            "time.nist.gov"
        };

        private async void Ad_Click(object sender, AdClickEventArgs e)
        {
            if (e.clickResult == "1" || e.clickResult == "2")
            {
                NtpClient client = new NtpClient();
                try
                {
                    DateTime? time = await client.GetAsync(NTPSERVERS);
                    if (time.HasValue)
                    {
                        AppSettings.Instance.LastClickAdTime = time.Value;
                    }
                }
                catch
                {
                    // ignored
                }
                _ad.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnShare_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private async void PnlWeibo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            IsExecuting = true;

            WeiboClient client = null;
            try
            {
                client = await WeiboClient.CreateAsync();
            }
            catch
            {
                await new MessageDialog("授权失败").ShowAsyncEnqueue();
                IsExecuting = false;
                return;
            }

            byte[] thumbnail;
            try
            {
                thumbnail = await GetThumbnailDataAsync();
            }
            catch
            {
                await new MessageDialog("网络连接错误").ShowAsyncEnqueue();
                IsExecuting = false;
                return;
            }

            Weibo shareResult;
            try
            {
                string text = _feed.Title + "http://www.u148.net/article/" + _feed.Id + ".html";
                shareResult = await client.ShareImageAsync(thumbnail, text);
            }
            catch
            {
                await new MessageDialog("网络连接错误").ShowAsyncEnqueue();
                IsExecuting = false;
                return;
            }

            if (shareResult.IsSuccess)
            {
                await new MessageDialog("分享成功").ShowAsyncEnqueue();
                IsExecuting = false;
            }
            else
            {
                if (shareResult.ErrorCode == 21332)
                {
                    WeiboClient.ClearAuthorize();
                }

                await new MessageDialog("分享失败" + Environment.NewLine + shareResult.Error).ShowAsyncEnqueue();
                IsExecuting = false;
            }
        }

        private async Task<byte[]> GetThumbnailDataAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                return (await client.GetBufferAsync(new Uri(_feed.PicMid))).ToArray();
            }
        }

        private async void PnlWechat_Tapped(object sender, TappedRoutedEventArgs e)
        {
            IsExecuting = true;

            if (DeviceFamilyHelper.IsDesktop)
            {
                await new MessageDialog("抱歉，桌面端暂时不支持该功能").ShowAsyncEnqueue();
                IsExecuting = false;
                return;
            }

            try
            {
                int scene = SendMessageToWX.Req.WXSceneChooseByUser;
                WXWebpageMessage message = new WXWebpageMessage()
                {
                    Title = _feed.Title,
                    WebpageUrl = "http://www.u148.net/article/" + _feed.Id + ".html",
                    Description = _feed.Summary
                };
                SendMessageToWX.Req req = new SendMessageToWX.Req(message, scene);
                IWXAPI api = WXAPIFactory.CreateWXAPI("wx725b599977a3718a");
                var isValid = await api.SendReq(req);
            }
            catch
            {
                await new MessageDialog("分享失败").ShowAsyncEnqueue();
            }

            IsExecuting = false;
        }

        private void BtnComment_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CommentView), _feed);
        }

        private void WebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            switch (e.Value)
            {
                case "goforward":
                    if (this.Frame.CanGoForward)
                    {
                        this.Frame.GoForward();
                    }
                    break;

                case "goback":
                    GoBack();
                    break;
            }
        }

        private async void PnlQQ_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var builder = new StringBuilder();
            builder.Append("http://connect.qq.com/widget/shareqq/index.html?url=");
            builder.Append(WebUtility.UrlEncode("http://www.u148.net/article/" + _feed.Id + ".html"));
            builder.Append("&title=");
            builder.Append(WebUtility.UrlEncode(_feed.Title));
            builder.Append("&summary=");
            builder.Append(WebUtility.UrlEncode(_feed.Summary));
            await Launcher.LaunchUriAsync(new Uri(builder.ToString()));
        }

        private async void PnlQZone_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var builder = new StringBuilder();
            builder.Append("http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?url=");
            builder.Append(WebUtility.UrlEncode("http://www.u148.net/article/" + _feed.Id + ".html"));
            builder.Append("&summary=");
            builder.Append(WebUtility.UrlEncode(_feed.Summary));
            builder.Append("&title=");
            builder.Append(WebUtility.UrlEncode(_feed.Title));
            await Launcher.LaunchUriAsync(new Uri(builder.ToString()));
        }
    }
}