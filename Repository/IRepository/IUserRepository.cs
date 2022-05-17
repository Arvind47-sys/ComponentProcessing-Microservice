using Api.Entities;
using System.Threading.Tasks;

namespace api.Repository.IRepository
{
    /// <summary>
    /// Repository for adding and retriving App users
    /// </summary>
    public interface IUserRepository
    {
        Task<int> GetUserId(string username);
        Task<bool> UserExists(string userName);
        Task AddNewUser(AppUser user);
        Task<AppUser> GetUser(string username);
    }
}
