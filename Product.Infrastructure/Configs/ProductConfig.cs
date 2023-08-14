using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Infrastructure.Configs
{
	public class ProductConfig : IEntityTypeConfiguration<Product.Domain.Entity.Product>
	{
		public void Configure(EntityTypeBuilder<Domain.Entity.Product> builder)
		{
			builder.ToTable($"T_{nameof(Product.Domain.Entity.Product)}");

			builder.HasMany(x => x.Variants).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
		}
	}
}
