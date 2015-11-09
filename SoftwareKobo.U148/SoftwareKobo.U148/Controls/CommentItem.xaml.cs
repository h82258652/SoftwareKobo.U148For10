using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SoftwareKobo.U148.Controls
{
    public sealed partial class CommentItem : UserControl
    {
        public CommentItem()
        {
            this.InitializeComponent();
            this.PointerReleased += CommentItem_PointerReleased;
        }

        public event EventHandler<CommentItemReviewEventArgs> Review;

        public Comment Comment
        {
            get
            {
                return (Comment)this.DataContext;
            }
        }

        private void BtnReview_Click(object sender, RoutedEventArgs e)
        {
            reviewRect.Visibility = Visibility.Collapsed;
            if (this.Comment == null || string.IsNullOrEmpty(txtReview.Text))
            {
                return;
            }
            if (this.Review != null)
            {
                this.Review(this, new CommentItemReviewEventArgs(this.Comment, txtReview.Text));
            }
        }

        private void CommentItem_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (AppSettings.Instance.UserInfo != null)
            {
                if (reviewRect.Visibility == Visibility.Visible)
                {
                    reviewRect.Visibility = Visibility.Collapsed;
                }
                else
                {
                    reviewRect.Visibility = Visibility.Visible;
                }
            }
        }
    }
}