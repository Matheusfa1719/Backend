using DocsHub.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DocsHub.Infra.Database
{
    public class DocsHubPgContext : DbContext
    {
       public DbSet<User> Users { get; set; }

         public DocsHubPgContext(DbContextOptions<DocsHubPgContext> options) : base(options)
         {
         }
    }
}


