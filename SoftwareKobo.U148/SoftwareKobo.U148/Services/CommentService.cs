using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Extensions;
using System;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace SoftwareKobo.U148.Services
{
    public class CommentService
    {
        private const string GET_COMMENT_TEMPLATE = @"http://api.u148.net/json/get_comment/{0}/{1}";

        public async Task<ResultBase<ResultList<Comment>>> GetCommentsAsync(Feed feed, int page = 1)
        {
            if (feed == null)
            {
                throw new ArgumentNullException(nameof(feed));
            }

            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "page should greater than zero.");
            }

            string url = string.Format(GET_COMMENT_TEMPLATE, feed.Id, page);
            using (HttpClient client = new HttpClient())
            {
                return await client.GetJsonAsync<ResultBase<ResultList<Comment>>>(new Uri(url));
            }
        }
    }
}