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

        private DelegateCommand<CommentItemReviewEventArgs> _commentReviewCommand;

        private bool _isSending;

        public bool IsSending
        {
            get
            {
                return this._isSending;
            }
            set
            {
                this.Set(ref this._isSending, value);
            }
        }

        private CommentCollection _comments;

        private Feed _feed;

        private DelegateCommand _refreshCommand;

        private DelegateCommand<string> _sendCommentCommand;

        public CommentViewModel(ICommentService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this._service = service;
        }

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

        public bool IsLogined
        {
            get
            {
                return AppSettings.Instance.UserInfo != null;
            }
        }

        public DelegateCommand RefreshCommand
        {
            get
            {
                if (this._refreshCommand == null)
                {
                    this._refreshCommand = new DelegateCommand(() =>
                    {
                        this.Comments.Refresh();
                    });
                }
                return this._refreshCommand;
            }
        }

        public DelegateCommand<string> SendCommentCommand
        {
            get
            {
                if (this._sendCommentCommand == null)
                {
                    this._sendCommentCommand = new DelegateCommand<string>(query =>
                    {
                        this.SendComment(query);
                    });
                }
                return this._sendCommentCommand;
            }
        }

        protected override void ReceiveFromView(dynamic parameter)
        {
            Feed feed = parameter as Feed;
            if (feed != null)
            {
                this._feed = feed;
                this.Comments = new CommentCollection(this._service, this._feed);
            }
        }

        private async void SendComment(string content, Comment comment = null)
        {
            this.IsSending = true;
            
            string message;
            try
            {
                ResultBase result = await this._service.SendCommentAsync(this._feed, (UserInfo)AppSettings.Instance.UserInfo, content, AppSettings.Instance.SimulateDevice, comment);
                if (result.Code == 0)
                {
                    message = "发送成功！";
                    this.Comments.Refresh();
                }
                else
                {
                    message = result.Message;
                }
            }
            catch
            {
                message = "网络连接失败！";
            }

            Tuple<string, string> sended = Tuple.Create<string, string>("sended", message);
            this.SendToView(sended);

            this.IsSending = false;
        }
    }
}