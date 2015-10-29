using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;
using Windows.UI.Xaml;

namespace SoftwareKobo.U148.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {
        private Article _article;

        public Article Article
        {
            get
            {
                return this._article;
            }
            private set
            {
                this.Set(ref this._article, value);
            }
        }
        
        protected override void ReceiveFromView( dynamic parameter)
        {
            if (parameter is Feed)
            {
                this.Feed = parameter;
                LoadArticleAsync(parameter);
            }
        }

        private async void LoadArticleAsync(Feed feed)
        {
            FeedService service = new FeedService();
            ResultBase<Article> result = await service.GetArticleAsync(feed);
            if (result.Code == 0)
            {
                this.Article = result.Data;
            }
        }

        private Feed _feed;

        public Feed Feed
        {
            get
            {
                return this._feed;
            }
            private set
            {
                this.Set(ref this._feed, value);
            }
        }

        private DelegateCommand _viewCommentCommand;

        public DelegateCommand ViewCommentCommand
        {
            get
            {
                if (this._viewCommentCommand == null)
                {
                    this._viewCommentCommand = new DelegateCommand(() =>
                    {
                    });
                }
                return this._viewCommentCommand;
            }
        }
    }
}