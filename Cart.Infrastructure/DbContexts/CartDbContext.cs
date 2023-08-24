
using Cart.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure.DbContexts
{
	public class CartDbContext : DbContext
	{
		public CartDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<CartItem> CartItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
		}
	}
}
