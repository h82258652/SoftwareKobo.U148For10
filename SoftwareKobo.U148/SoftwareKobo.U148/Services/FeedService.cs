using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Extensions;
using System;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace SoftwareKobo.U148.Services
{
    public class FeedService : IFeedService
    {
        private const string CATEGORY_LIST_TEMPLATE = "http://api.u148.net/json/{0}/{1}";

        private const string ARTICLE_TEMPLATE = "http://api.u148.net/json/article/{0}";

        public async Task<DataResultBase<ResultList<Feed>>> GetFeedListAsync(FeedCategory category, int page = 1)
        {
            if (Enum.IsDefined(typeof(FeedCategory), category) == false)
            {
                throw new ArgumentException($"{nameof(category)} is not defined.", nameof(category));
            }
            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "page should greater than zero.");
            }

            string url = string.Format(CATEGORY_LIST_TEMPLATE, (int)category, page);
            using (HttpClient client = new HttpClient())
            {
                return await client.GetJsonAsync<DataResultBase<ResultList<Feed>>>(new Uri(url));
            }
        }

        public async Task<DataResultBase<Article>> GetArticleAsync(Feed feed)
        {
            if (feed == null)
            {
                throw new ArgumentNullException(nameof(feed));
            }

            string url = string.Format(ARTICLE_TEMPLATE, feed.Id);
            using (HttpClient client = new HttpClient())
            {
                return await client.GetJsonAsync<DataResultBase<Article>>(new Uri(url));
            }
        }
    }
}