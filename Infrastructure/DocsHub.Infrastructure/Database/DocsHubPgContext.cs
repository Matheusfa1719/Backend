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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}


