using DocsHub.Core.Common;
using DocsHub.Core.Models;

namespace DocsHub.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
         Task<PagedList<User>> GetAllUsersAsync(int pageIndex, int pageSize);
    }
}