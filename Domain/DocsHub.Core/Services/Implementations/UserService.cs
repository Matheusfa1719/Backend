using DocsHub.Core.Models;
using DocsHub.Core.Repositories.Interfaces;
using DocsHub.Core.Services.Interfaces;

namespace DocsHub.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
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

        public async Task<User> CreateUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            user = await _userRepository.AddAsync(user);
            return user;
        }
    }
}