using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;

namespace SoftwareKobo.U148.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {
        private readonly IFeedService _service;

        private Article _article;

        private Feed _feed;

        public DetailViewModel(IFeedService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this._service = service;
        }

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

        protected override void ReceiveFromView(dynamic parameter)
        {
            Feed feed = parameter as Feed;
            if (feed != null)
            {
                this.Feed = feed;
                this.LoadArticleAsync(feed);
            }
        }

        private async void LoadArticleAsync(Feed feed)
        {
            ResultBase<Article> result = await this._service.GetArticleAsync(feed);
            if (result.Code == 0)
            {
                this.Article = result.Data;
            }
        }
    }
}