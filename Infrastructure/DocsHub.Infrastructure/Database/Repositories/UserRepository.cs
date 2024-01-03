using DocsHub.Core.Common;
using DocsHub.Core.Models;
using DocsHub.Core.Repositories.Interfaces;
using DocsHub.Infra.Database;
using Microsoft.EntityFrameworkCore;

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

        public Task<User?> GetByEmailAsync(string email)
        {
            var user = _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<PagedList<User>> GetAllUsersAsync(int pageIndex, int pageSize)
        {
            var totalRecords = await _context.Users.CountAsync();
            var users = await _context.Users
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<User>(users, totalRecords, pageIndex, pageSize);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}
