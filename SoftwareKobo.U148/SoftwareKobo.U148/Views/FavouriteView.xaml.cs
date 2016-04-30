using SoftwareKobo.UniversalToolkit.Helpers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace SoftwareKobo.U148.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FavouriteView
    {
        public FavouriteView()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Frame.RegisterNavigateBack();

            //UserService service = new UserService();
            //var s = await service.AddFavouriteAsync((UserInfo)AppSettings.Instance.UserInfo, new Feed() { Id = 134511 });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            this.Frame.UnregisterNavigateBack();
        }
    }
}