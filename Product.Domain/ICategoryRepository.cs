using Product.Domain.Entity;

namespace Product.Domain
{
	public interface ICategoryRepository
	{
		Task<List<Category>> FindAllCategoriesAsync();
	}
}
