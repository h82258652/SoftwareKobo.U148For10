using SoftwareKobo.U148.Models;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public interface ISearchService
    {
        Task<ResultBase<ResultList<Feed>>> GetSearchResultsAsync(string keyword, int page = 1);
    }
}