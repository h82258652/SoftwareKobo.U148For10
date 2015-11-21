using JiuYouAdUniversal.Models;
using JYAnalyticsUniversal;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Extensions;
using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class DetailView : Page, IView
    {
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

            this.Frame.UnregisterNavigateBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            JYAnalytics.TrackPageStart(nameof(DetailView));

            Messenger.Register(this);

            if (this.Frame.CanGoBack)
            {
                this.Frame.RegisterNavigateBack();
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

                Feed parameter = e.Parameter as Feed;
                if (parameter != null)
                {
                    this.SendToViewModel(parameter);
                }
            }
        }

        private void DetailView_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppSettings.Instance.LastClickAdTime + TimeSpan.FromHours(12) < DateTime.Now)
            {
                this.FindName("ad");
            }
        }

        private void Ad_Click(object sender, AdClickEventArgs e)
        {
            if (e.clickResult == "1")
            {
                AppSettings.Instance.LastClickAdTime = DateTime.Now;
                ad.Visibility = Visibility.Collapsed;
            }
        }
    }
}