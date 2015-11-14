using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class MainView : Page, IView
    {
        private CanvasBitmap _bitmap;

        public MainView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        public async void ReceiveFromViewModel(dynamic parameter)
        {
            if (parameter is Feed)
            {
                if (AppSettings.Instance.ShowDetailInNewWindow == false)
                {
                    this.Frame.Navigate(typeof(DetailView), parameter);
                }
                else
                {
                    await App.Current.ShowNewWindowAsync(typeof(DetailView), parameter);
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
            args.TrackAsyncAction(this.Canvas_CreateResourcesAsync(sender, args).AsAsyncAction());
        }

        private async Task Canvas_CreateResourcesAsync(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            this._bitmap = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/menu_background.jpg"));
        }

        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            GaussianBlurEffect effect = new GaussianBlurEffect()
            {
                Source = this._bitmap,
                BlurAmount = 5,
                BorderMode = EffectBorderMode.Hard
            };
            args.DrawingSession.DrawImage(effect);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.canvas != null)
            {
                this.canvas.RemoveFromVisualTree();
                this.canvas = null;
            }
        }

        private void TxtSearch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (string.IsNullOrEmpty(args.QueryText) == false)
            {
                this.Frame.Navigate(typeof(SearchView), sender.Text);
            }
        }
    }
}