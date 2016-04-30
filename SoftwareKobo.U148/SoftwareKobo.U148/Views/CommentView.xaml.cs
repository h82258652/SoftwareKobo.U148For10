using SoftwareKobo.U148.Controls;
using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SoftwareKobo.U148.Views
{
    public sealed partial class CommentView : IView
    {
        public CommentView()
        {
            InitializeComponent();
        }

        public async void ReceiveFromViewModel(dynamic parameter)
        {
            Tuple<string, bool, string> command = parameter as Tuple<string, bool, string>;
            if (command != null)
            {
                if (command.Item1 == "sended")
                {
                    if (command.Item2)
                    {
                        await new MessageDialog(command.Item3).ShowAsync();
                        if (_lastSendTextBox != null)
                        {
                            _lastSendTextBox.Text = string.Empty;
                        }
                    }
                    else
                    {
                        await new MessageDialog(command.Item3).ShowAsync();
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
                if (Frame.CanGoBack && sendingMask.Visibility != Visibility.Visible)
                {
                    Frame.GoBack();
                }
            });

            Feed parameter = e.Parameter as Feed;
            if (parameter != null)
            {
                this.SendToViewModel(parameter);
            }
        }

        private TextBox _lastSendTextBox;

        private void BtnSendComment_Click(object sender, RoutedEventArgs e)
        {
            _lastSendTextBox = txtReview;
            flyout.Hide();
        }

        private void CommentItem_Review(object sender, CommentItemReviewEventArgs e)
        {
            _lastSendTextBox = e.TextBox;
        }
    }
}