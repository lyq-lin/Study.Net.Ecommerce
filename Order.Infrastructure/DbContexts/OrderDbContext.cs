using Microsoft.EntityFrameworkCore;
using Order.Domain.Entity;

namespace Order.Infrastructure.DbContexts
{
	public class OrderDbContext : DbContext
	{
		public OrderDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Order.Domain.Entity.Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
		}
	}
}
