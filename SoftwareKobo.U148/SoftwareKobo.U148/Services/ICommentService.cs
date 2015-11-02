using SoftwareKobo.U148.Models;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public interface ICommentService
    {
        Task<ResultBase<ResultList<Comment>>> GetCommentsAsync(Feed feed, int page = 1);
    }
}