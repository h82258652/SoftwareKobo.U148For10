using SoftwareKobo.U148.Models;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public interface ICommentService
    {
        Task<DataResultBase<ResultList<Comment>>> GetCommentsAsync(Feed feed, int page = 1);

        Task<ResultBase> SendCommentAsync(Feed feed, UserInfo userInfo, string content, SimulateDevice device = SimulateDevice.Android, Comment reviewComment = null);
    }
}