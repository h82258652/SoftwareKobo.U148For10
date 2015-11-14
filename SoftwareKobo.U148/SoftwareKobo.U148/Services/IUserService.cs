using SoftwareKobo.U148.Models;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public interface IUserService
    {
        Task<DataResultBase<UserInfo>> LoginAsync(string email, string password);
    }
}