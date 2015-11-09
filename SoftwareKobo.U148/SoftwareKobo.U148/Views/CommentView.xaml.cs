using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class CommentView : Page, IView
    {
        public CommentView()
        {
            this.InitializeComponent();
        }

        private bool _isSending = false;

        private bool IsSending
        {
            get
            {
                return this._isSending;
            }
            set
            {
                this._isSending = value;
                this.sendingMask.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public async void ReceiveFromViewModel(dynamic parameter)
        {
            Tuple<string, string> command = parameter as Tuple<string, string>;
            if (command != null)
            {
                if (command.Item1 == "sending")
                {
                    this.IsSending = true;
                }
                else if (command.Item1 == "sended")
                {
                    await new MessageDialog(command.Item2).ShowAsync();
                    this.IsSending = false;
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
                if (this.Frame.CanGoBack && this.IsSending == false)
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