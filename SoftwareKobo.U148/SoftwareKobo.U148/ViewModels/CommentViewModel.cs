using SoftwareKobo.U148.Controls;
using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Datas;
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

        private DelegateCommand<CommentItemReviewEventArgs> _commentReviewCommand;

        public DelegateCommand<CommentItemReviewEventArgs> CommentReviewCommand
        {
            get
            {
                if (this._commentReviewCommand == null)
                {
                    this._commentReviewCommand = new DelegateCommand<CommentItemReviewEventArgs>(args =>
                    {
                        this.SendComment(args.Content, args.Comment);
                    });
                }
                return this._commentReviewCommand;
            }
        }

        private Feed _feed;

        protected override void ReceiveFromView(dynamic parameter)
        {
            Feed feed = parameter as Feed;
            if (feed != null)
            {
                this._feed = feed;
                this.Comments = new CommentCollection(this._service, this._feed);
            }
        }

        public bool IsLogined
        {
            get
            {
                return AppSettings.Instance.UserInfo != null;
            }
        }

        private async void SendComment(string content, Comment comment = null)
        {
            return;
            this.SendToView("sending");

            SendCommentResult result = await _service.SendCommentAsync(this._feed, (UserInfo)AppSettings.Instance.UserInfo, content, AppSettings.Instance.SimulateDevice, comment);
            if (result.Code == 0)
            {

            }

            this.SendToView("sended");
        }
    }
}