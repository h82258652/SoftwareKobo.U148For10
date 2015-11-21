using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using JYAnalyticsUniversal;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class CommentView : Page, IView
    {
        public CommentView()
        {
            this.InitializeComponent();
        }

        public async void ReceiveFromViewModel(dynamic parameter)
        {
            Tuple<string, string> command = parameter as Tuple<string, string>;
            if (command != null)
            {
                if (command.Item1 == "sended")
                {
                    await new MessageDialog(command.Item2).ShowAsync();
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            JYAnalytics.TrackPageEnd(nameof(CommentView));

            Messenger.Unregister(this);

            this.Frame.UnregisterNavigateBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            JYAnalytics.TrackPageStart(nameof(CommentView));

            Messenger.Register(this);

            this.Frame.RegisterNavigateBack(() =>
            {
                if (this.Frame.CanGoBack && this.sendingMask.Visibility != Visibility.Visible)
                {
                    this.Frame.GoBack();
                }
            });

            Feed parameter = e.Parameter as Feed;
            if (parameter != null)
            {
                this.SendToViewModel(parameter);
            }
        }

        private void BtnSendComment_Click(object sender, RoutedEventArgs e)
        {
            flyout.Hide();
        }
    }
}