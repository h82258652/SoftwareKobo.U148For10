using MicroMsg.sdk;
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
    public sealed partial class DetailView : IView
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
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        public async void ReceiveFromViewModel(dynamic parameter)
        {
            Article article = parameter as Article;
            if (article != null)
            {
                await _domReadyTcs.Task;
                await webView.InvokeScriptAsync("setThemeMode", AppSettings.Instance.ThemeMode.ToString());
                await webView.InvokeScriptAsync("setContent", article.Content);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            Messenger.Unregister(this);

            NavigationHelper.Unregister(Frame);
        }

        private Feed _feed;

        private void GoBack()
        {
            if (Frame.CanGoBack && IsExecuting == false)
            {
                Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Messenger.Register(this);

            if (Frame.CanGoBack)
            {
                NavigationHelper.Register(Frame, () =>
                {
                    GoBack();
                });
            }

            if (e.NavigationMode != NavigationMode.Back)
            {
                _domReadyTcs = new TaskCompletionSource<object>();
                TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs> handler = null;
                handler = (sender, args) =>
                {
                    webView.DOMContentLoaded -= handler;
                    _domReadyTcs.SetResult(null);
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
        }

        private void BtnShare_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private async void PnlWeibo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            IsExecuting = true;

            WeiboClient client;
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
                    if (Frame.CanGoForward)
                    {
                        Frame.GoForward();
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