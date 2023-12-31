using DocsHub.Core.Models;

namespace DocsHub.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
    }
}