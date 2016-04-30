using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit;
using SoftwareKobo.UniversalToolkit.Mvvm;
using SoftwareKobo.UniversalToolkit.Services.LauncherServices;
using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class MainView : IView
    {
        private CanvasBitmap _bitmap;

        public MainView()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        public async void ReceiveFromViewModel(dynamic parameter)
        {
            if (parameter is Feed)
            {
                if (AppSettings.Instance.ShowDetailInNewWindow == false)
                {
                    Frame.Navigate(typeof(DetailView), parameter);
                }
                else
                {
                    await Bootstrapper.Current.ShowNewWindowAsync(typeof(DetailView), parameter);
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            Messenger.Unregister(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Messenger.Register(this);

            this.SendToViewModel("navigated");
        }

        private void Canvas_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(Canvas_CreateResourcesAsync(sender, args).AsAsyncAction());
        }

        private async Task Canvas_CreateResourcesAsync(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            _bitmap = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/menu_background.jpg"));
        }

        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            GaussianBlurEffect effect = new GaussianBlurEffect()
            {
                Source = _bitmap,
                BlurAmount = 5,
                BorderMode = EffectBorderMode.Hard
            };
            args.DrawingSession.DrawImage(effect);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (canvas != null)
            {
                canvas.RemoveFromVisualTree();
                canvas = null;
            }
        }

        private void TxtSearch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (string.IsNullOrEmpty(args.QueryText) == false)
            {
                Frame.Navigate(typeof(SearchView), sender.Text);
            }
        }

        private async void BtnReview_Click(object sender, RoutedEventArgs e)
        {
            StoreService service = new StoreService();
            await service.OpenCurrentAppReviewPageAsync();
        }

        private async void BtnFeedback_Click(object sender, RoutedEventArgs e)
        {
            string uri = "mailto:h82258652@hotmail.com?subject=Windows 10 有意思吧反馈";
            await Launcher.LaunchUriAsync(new Uri(uri));
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginView));
        }

        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingView));
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutView));
        }

        private void FeedView_ScrollDown(object sender, EventArgs e)
        {
            //appBar.ClosedDisplayMode = AppBarClosedDisplayMode.Minimal;
        }

        private void FeedView_ScrollUp(object sender, EventArgs e)
        {
            //appBar.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
        }
    }
}