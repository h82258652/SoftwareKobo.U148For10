using SoftwareKobo.UniversalToolkit.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace SoftwareKobo.U148.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingView : Page
    {
        public SettingView()
        {
            this.InitializeComponent();
            this.tshShowDetailInNewWindow.Visibility = HardwareButtonsHelper.IsUseable ? Visibility.Collapsed : Visibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationHelper.Register(this.Frame);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            NavigationHelper.Unregister(this.Frame);
        }
    }
}