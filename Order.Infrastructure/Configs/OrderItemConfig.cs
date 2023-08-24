using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entity;

namespace Order.Infrastructure.Configs
{
	public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable($"T_{nameof(OrderItem)}");
			builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
			builder.HasKey(oi => new { oi.OrderId, oi.ProductId, oi.ProductTypeId });
		}
	}
}
