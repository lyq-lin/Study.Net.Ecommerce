namespace Product.Domain
{
	public interface IProductRepository
	{
		Task<List<Product.Domain.Entity.Product>> FindAllProductsAsync();
		Task<Product.Domain.Entity.Product> FindProductByIdAsync(Guid productId);
		Task<List<Product.Domain.Entity.Product>> FindProductByCategoryAsync(string categoryUrl);
		Task<List<Product.Domain.Entity.Product>> FindProductBySearchAsync(string searchText);
		Task<(List<Domain.Entity.Product>, double)> FindProductBySearchAsync(string searchText, int page);
		Task<List<string>> GetProductSearchSuggestionsAsync(string searchText);
		Task<List<Product.Domain.Entity.Product>> FindProductsByFeatureAsync();
	}
}
