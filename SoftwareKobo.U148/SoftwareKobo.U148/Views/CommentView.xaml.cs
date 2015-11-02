using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class CommentView : Page, IView
    {
        public CommentView()
        {
            this.InitializeComponent();
        }

        public void ReceiveFromViewModel(dynamic parameter)
        {
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            Messenger.Unregister(this);

            this.Frame.UnregisterNavigateBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Messenger.Register(this);

            this.Frame.RegisterNavigateBack();

            Feed parameter = e.Parameter as Feed;
            if (parameter != null)
            {
                this.SendToViewModel(parameter);
            }
        }
    }
}