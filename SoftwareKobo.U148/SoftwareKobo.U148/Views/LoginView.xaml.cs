using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using System.Diagnostics;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class LoginView : IView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public async void ReceiveFromViewModel(dynamic parameter)
        {
            Tuple<string, bool, string> package = parameter as Tuple<string, bool, string>;
            if (package != null)
            {
                if (package.Item1 == "loginFinish")
                {
                    if (package.Item2)
                    {
                        await new MessageDialog("登录成功").ShowAsync();
                        GoBack();
                    }
                    else
                    {
                        await new MessageDialog(package.Item3).ShowAsync();
                    }
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            Messenger.Unregister(this);

            NavigationHelper.Unregister(Frame);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Messenger.Register(this);

            NavigationHelper.Register(Frame, () =>
            {
                GoBack();
            });
        }

        private void GoBack()
        {
            if (Frame.CanGoBack && loginingMask.Visibility != Visibility.Visible)
            {
                Frame.GoBack();
            }
        }

        private void TxtEmail_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                e.Handled = true;
                txtPassword.Focus(FocusState.Programmatic);
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                e.Handled = true;

                btnLogin.Focus(FocusState.Programmatic);
                ButtonAutomationPeer buttonPeer = new ButtonAutomationPeer(btnLogin);
                IInvokeProvider invokeProvider = (IInvokeProvider)buttonPeer.GetPattern(PatternInterface.Invoke);
                Debug.Assert(invokeProvider != null);
                invokeProvider.Invoke();
            }
        }
    }
}