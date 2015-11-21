using SoftwareKobo.U148.Models;
using System;
using Windows.UI.Xaml.Controls;

namespace SoftwareKobo.U148.Controls
{
    public class CommentItemReviewEventArgs : EventArgs
    {
        public CommentItemReviewEventArgs(Comment comment, string content, TextBox textBox)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            if (textBox == null)
            {
                throw new ArgumentNullException(nameof(textBox));
            }
            if (content.Length <= 0)
            {
                throw new ArgumentException("评论不能为空。", nameof(content));
            }

            this.Comment = comment;
            this.Content = content;
            this.TextBox = textBox;
        }

        public Comment Comment
        {
            get;
            private set;
        }

        public TextBox TextBox
        {
            get;
            private set;
        }

        public string Content
        {
            get;
            private set;
        }
    }
}