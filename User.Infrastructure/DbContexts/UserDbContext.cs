using Microsoft.EntityFrameworkCore;
using User.Domain.Entity;

namespace User.Infrastructure.DbContexts
{
	public class UserDbContext : DbContext
	{
		public DbSet<User.Domain.Entity.User> Users { get; set; }
		public DbSet<UserLoginHistory> UserLoginHistories { get; set; }
		public UserDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
		}
	}
}
