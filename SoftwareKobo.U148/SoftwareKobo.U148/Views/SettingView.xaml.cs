using SoftwareKobo.UniversalToolkit.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class SettingView
    {
        public SettingView()
        {
            InitializeComponent();
            tshShowDetailInNewWindow.Visibility = HardwareButtonsHelper.IsUseable ? Visibility.Collapsed : Visibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationHelper.Register(Frame);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            NavigationHelper.Unregister(Frame);
        }
    }
}