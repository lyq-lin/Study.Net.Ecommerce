using Microsoft.EntityFrameworkCore;
using Product.Domain;
using Product.Domain.Entity;
using Product.Infrastructure.dbContexts;

namespace Product.Infrastructure
{
	public class ProductTypeRepository : IProductTypeRepository
	{
		private readonly ProductDbContext _dbContext;

		public ProductTypeRepository(ProductDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<List<ProductType>> FindAllProductTypeAsync()
		{
			return await _dbContext.ProductTypes.ToListAsync();
		}

		public async Task<List<ProductType>> GetProductTypeByProductIdAsync(Guid ProductId)
		{
			List<ProductType> productTypes = new List<ProductType>();
			var result = await _dbContext.ProductVariants.Where(x => x.Deleted == false && x.Visible == true && x.ProductId == ProductId).ToListAsync();

			foreach (var item in result)
			{
				var type = await _dbContext.ProductTypes.SingleOrDefaultAsync(x => x.Id == item.ProductTypeId);
				productTypes.Add(type);
			}

			return productTypes;
		}

		public async Task<string> GetTypeNameByProductTypeId(Guid productTypeId)
		{
			var result = await _dbContext.ProductTypes.FindAsync(productTypeId);
			return result.Name;
		}
	}
}
