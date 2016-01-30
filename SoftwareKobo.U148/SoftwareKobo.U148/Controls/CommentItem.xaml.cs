using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace SoftwareKobo.U148.Controls
{
    public sealed partial class CommentItem
    {
        public CommentItem()
        {
            InitializeComponent();
            PointerReleased += CommentItem_PointerReleased;
        }

        public event EventHandler<CommentItemReviewEventArgs> Review;

        public Comment Comment => (Comment)DataContext;

        private void BtnReview_Click(object sender, RoutedEventArgs e)
        {
            reviewRect.Visibility = Visibility.Collapsed;
            if (Comment == null || string.IsNullOrEmpty(txtReview.Text))
            {
                return;
            }
            Review?.Invoke(this, new CommentItemReviewEventArgs(Comment, txtReview.Text, txtReview));
        }

        private void CommentItem_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (AppSettings.Instance.UserInfo != null)
            {
                reviewRect.Visibility = reviewRect.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}