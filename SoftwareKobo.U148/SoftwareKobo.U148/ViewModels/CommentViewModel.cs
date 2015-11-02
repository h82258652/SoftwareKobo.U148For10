using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;

namespace SoftwareKobo.U148.ViewModels
{
    public class CommentViewModel : ViewModelBase
    {
        private readonly ICommentService _service;

        private CommentCollection _comments;

        public CommentViewModel(ICommentService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this._service = service;
        }

        public CommentCollection Comments
        {
            get
            {
                return _comments;
            }
            private set
            {
                this.Set(ref this._comments, value);
            }
        }

        protected override void ReceiveFromView(dynamic parameter)
        {
            Feed feed = parameter as Feed;
            if (feed != null)
            {
                this.Comments = new CommentCollection(this._service, feed);
            }
        }
    }
}