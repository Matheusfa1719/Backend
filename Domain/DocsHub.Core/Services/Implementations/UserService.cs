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

        public Task<User> GetUserByIdAsync(Guid id)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "teste@gmail.com",
                Password = "123456",
                Name = "Teste",
                Role = Enums.UserRole.Admin,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            return Task.FromResult(user);
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

        public Task<PagedList<User>> GetAllUsersAsync(int pageIndex, int pageSize)
        {
            return _userRepository.GetAllUsersAsync(pageIndex, pageSize);
        }
    }
}