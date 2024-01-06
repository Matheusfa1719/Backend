using System.Linq;
using DocsHub.Core.Common;
using DocsHub.Core.Models;
using DocsHub.Core.Repositories.Interfaces;
using DocsHub.Core.Services.Interfaces;

namespace DocsHub.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user;
        }

        public async Task<Result<User>> CreateUserAsync(User user)
        {
            var userExists = await UserExistsAsync(user.Email);
            if (userExists)
            {
                return Result<User>.Fail("Usuário já existe");
            }
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            user = await _userRepository.AddAsync(user);
            return Result<User>.Ok(user);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null;
        }

        public async Task<PagedList<User>> GetAllUsersAsync(int pageIndex, int pageSize)
        {
            var users = await _userRepository.GetAllUsersAsync(pageIndex, pageSize);
            var mappedUsers = users.Items.Select(user => new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = "",
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            }).ToList();

            return new PagedList<User>(mappedUsers, users.TotalCount, users.PageIndex, users.PageSize);
        }

        public async Task<Result<User>> DeleteUserByIdAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null)
            {
                return Result<User>.Fail("Usuário não encontrado");
            }
            await _userRepository.DeleteUserByIdAsync(user);
            return Result<User>.Ok(user);
        }
    }
}