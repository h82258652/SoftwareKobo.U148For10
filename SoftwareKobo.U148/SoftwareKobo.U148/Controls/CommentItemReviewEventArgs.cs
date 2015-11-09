using SoftwareKobo.U148.Models;
using System;

namespace SoftwareKobo.U148.Controls
{
    public class CommentItemReviewEventArgs : EventArgs
    {
        public CommentItemReviewEventArgs(Comment comment, string content)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            if (content.Length <= 0)
            {
                throw new ArgumentException("评论不能为空。", nameof(content));
            }

            this.Comment = comment;
            this.Content = content;
        }

        public Comment Comment
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