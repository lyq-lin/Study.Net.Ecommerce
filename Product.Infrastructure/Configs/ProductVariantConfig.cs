using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entity;

namespace Product.Infrastructure.Configs
{
	public class ProductVariantConfig : IEntityTypeConfiguration<ProductVariant>
	{
		public void Configure(EntityTypeBuilder<ProductVariant> builder)
		{
			builder.ToTable($"T_{nameof(ProductVariant)}");

			builder.HasKey(x => new { x.ProductId, x.ProductTypeId });

			builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
			builder.Property(x => x.OriginalPrice).HasColumnType("decimal(18,2)");
		}
	}
}
