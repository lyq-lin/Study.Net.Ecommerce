using Product.Domain.Entity;

namespace Product.Domain
{
	public interface IProductTypeRepository
	{
		Task<List<ProductType>> FindAllProductTypeAsync();

		Task<List<ProductType>> GetProductTypeByProductIdAsync(Guid ProductId);
		Task<string> GetTypeNameByProductTypeId(Guid productTypeId);
	}
}
