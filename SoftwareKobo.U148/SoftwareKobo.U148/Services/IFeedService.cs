using SoftwareKobo.U148.Models;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public interface IFeedService
    {
        Task<ResultBase<ResultList<Feed>>> GetFeedListAsync(FeedCategory category, int page = 1);

        Task<ResultBase<Article>> GetArticleAsync(Feed feed);
    }
}