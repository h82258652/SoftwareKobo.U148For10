﻿using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class LoginView : Page, IView
    {
        public LoginView()
        {
            this.InitializeComponent();
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
                        this.GoBack();
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

        private void GoBack()
        {
            if (this.Frame.CanGoBack && this.loginingMask.Visibility != Visibility.Visible)
            {
                this.Frame.GoBack();
            }
        }
    }
}