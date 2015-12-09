using SoftwareKobo.UniversalToolkit.Helpers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class AboutView : Page
    {
        public AboutView()
        {
            this.InitializeComponent();
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