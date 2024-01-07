using DocsHub.Core.Common;
using DocsHub.Core.Models;

namespace DocsHub.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<Result<User>> CreateUserAsync(User user);
        Task<bool> UserExistsAsync(string email);
        Task<PagedList<User>> GetAllUsersAsync(int pageIndex, int pageSize);
        Task<Result<User>> DeleteUserByIdAsync(Guid id);
        Task<Result<User>> UpdateUserAsync(User user);
    }
}