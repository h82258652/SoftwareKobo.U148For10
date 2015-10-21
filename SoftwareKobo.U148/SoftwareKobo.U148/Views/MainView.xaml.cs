using SoftwareKobo.UniversalToolkit.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace SoftwareKobo.U148.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            await App.Current.ShowNewWindowAsync(typeof(DetailView), e.ClickedItem);
           // Frame.Navigate(typeof(DetailView), e.ClickedItem);
        }

        private void feedView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AdaptiveCollectionView view = (AdaptiveCollectionView)sender;
            if (e.NewSize.Width >= 1000)
            {
                view.Mode = AdaptiveCollectionViewMode.Grid;
            }
            else
            {
                view.Mode = AdaptiveCollectionViewMode.List;
            }
        }
    }
}