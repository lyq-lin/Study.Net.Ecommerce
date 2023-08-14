using Microsoft.EntityFrameworkCore;
using Product.Domain;
using Product.Domain.Entity;
using Product.Infrastructure.dbContexts;

namespace Product.Infrastructure
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ProductDbContext _dbContext;

		public CategoryRepository(ProductDbContext dbContext)
        {
			_dbContext = dbContext;
		}
        public async Task<List<Category>> FindAllCategoriesAsync()
		{
			var result = await _dbContext.Categories.Where(x => x.Deleted == false && x.Visible == true).ToListAsync();
			return result;
		}
	}
}
