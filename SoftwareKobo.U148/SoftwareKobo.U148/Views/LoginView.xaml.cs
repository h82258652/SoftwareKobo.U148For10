using SoftwareKobo.U148.Datas;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class LoginView : Page, IView
    {
        private bool _isLogining;

        private TaskCompletionSource<Tuple<string, bool, string>> _loginTcs;

        public LoginView()
        {
            this.InitializeComponent();
        }

        private bool IsLogining
        {
            get
            {
                return this._isLogining;
            }
            set
            {
                this._isLogining = value;
                this.loginingMask.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public void ReceiveFromViewModel(dynamic parameter)
        {
            Tuple<string, bool, string> package = parameter as Tuple<string, bool, string>;
            if (package != null && package.Item1 == "loginFinish" && this._loginTcs != null)
            {
                this._loginTcs.SetResult(package);
            }
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

            this.Frame.RegisterNavigateBack(() =>
            {
                this.GoBack();
            });
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.IsLogining = true;
            this._loginTcs = new TaskCompletionSource<Tuple<string, bool, string>>();
            this.SendToViewModel(Tuple.Create<string, string>("login", null));
            var result = await _loginTcs.Task;
            if (result.Item2)
            {
                await new MessageDialog("登录成功").ShowAsync();
                this.IsLogining = false;
                this.GoBack();
            }
            else
            {
                await new MessageDialog(result.Item3).ShowAsync();
                this.IsLogining = false;
            }
        }

        private void GoBack()
        {
            if (this.Frame.CanGoBack && this.IsLogining == false)
            {
                this.Frame.GoBack();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                this.SendToViewModel(Tuple.Create("password", passwordBox.Password));
            }
        }
    }
}