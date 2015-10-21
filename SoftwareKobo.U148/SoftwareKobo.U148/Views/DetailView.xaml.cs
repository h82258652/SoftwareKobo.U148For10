using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using SoftwareKobo.UniversalToolkit.Helpers;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SoftwareKobo.U148.Models;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace SoftwareKobo.U148.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DetailView : Page
    {
        public DetailView()
        {
            this.InitializeComponent();
        }

        private Feed _f;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (this.Frame.CanGoBack)
            {
                this.Frame.RegisterNavigateBack();
            }

            var feed = e.Parameter as Feed;
            if (feed != null)
            {
                this._f = feed;
                try
                {
                    Services.FeedService s = new Services.FeedService();
                    var r = await s.GetArticleAsync(feed);
                    var rr = r.Data;
                    v.NavigateToString(rr.Content);
                }
                catch (Exception)
                {
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            
            this.Frame.UnregisterNavigateBack();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CommentView), _f);
        }
    }
}
