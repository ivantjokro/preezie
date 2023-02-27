using Microsoft.EntityFrameworkCore;
using Preezie.Presentation.Model;

namespace Preezie.Domain
{
    public class UserDbContext : DbContext
    {
        public const string TableName = "User";

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(TableName);
        }

        public DbSet<User> Users { get; set; }
    }
}