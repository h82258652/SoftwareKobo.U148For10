using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Helpers;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using JYAnalyticsUniversal;
using SoftwareKobo.U148.Controls;

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

            JYAnalytics.TrackPageEnd(nameof(CommentView));

            Messenger.Unregister(this);

            NavigationHelper.Unregister(this.Frame);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            JYAnalytics.TrackPageStart(nameof(CommentView));

            Messenger.Register(this);

            NavigationHelper.Register(this.Frame, () => 
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