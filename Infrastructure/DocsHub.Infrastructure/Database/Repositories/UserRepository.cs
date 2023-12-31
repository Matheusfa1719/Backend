using DocsHub.Core.Models;
using DocsHub.Core.Repositories.Interfaces;
using DocsHub.Infra.Database;

namespace DocsHub.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DocsHubPgContext _context;

        public UserRepository(DocsHubPgContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
