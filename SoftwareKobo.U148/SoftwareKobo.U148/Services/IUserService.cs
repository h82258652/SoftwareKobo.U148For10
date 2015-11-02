using SoftwareKobo.U148.Models;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public interface IUserService
    {
        Task<ResultBase<UserInfo>> LoginAsync(string email, string password);
    }
}