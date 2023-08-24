using Cart.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Infrastructure.Configs
{
	public class CartItemConfig : IEntityTypeConfiguration<CartItem>
	{
		public void Configure(EntityTypeBuilder<CartItem> builder)
		{
			builder.ToTable($"T_{nameof(CartItem)}");

			builder.HasKey(x => new { x.ProductId, x.ProductTypeId, x.UserId });
		}
	}
}
