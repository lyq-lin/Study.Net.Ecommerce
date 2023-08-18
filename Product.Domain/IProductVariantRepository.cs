using Product.Domain.Entity;

namespace Product.Domain
{
	public interface IProductVariantRepository
	{
		Task<ProductVariant> GetVariantByProductAsync(Guid productId, Guid productTypeId);
	}
}
