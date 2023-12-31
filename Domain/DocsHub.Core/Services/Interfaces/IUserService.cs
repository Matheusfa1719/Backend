using DocsHub.Core.Models;

namespace DocsHub.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
    }
}