using Cart.Domain;
using Cart.Domain.Entity;
using Cart.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure
{
	public class CartRepository : ICartRepository
	{
		private readonly CartDbContext _dbContext;

		public CartRepository(CartDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task AddToCart(CartItem cartItem)
		{
			await _dbContext.CartItems.AddAsync(cartItem);
		}

		public async Task<CartItem> FindSameCartInDb(CartItem cartItem)
		{
			var result = await _dbContext.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId && ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId == cartItem.UserId);
			return result;
		}

		public async Task<int> GetCartItemCount(Guid userId)
		{
			var count = (await _dbContext.CartItems.Where(ci => ci.UserId == userId).ToListAsync()).Count;
			return count;
		}

		public async Task<List<CartItem>> GetDbCartProducts(Guid userId)
		{
			var result = await _dbContext.CartItems.Where(ci => ci.UserId == userId).ToListAsync();
			return result;
		}

		public async Task<bool> RemoveItemFromCart(Guid productId, Guid productTypeId, Guid userId)
		{
			var dbCartItem = await _dbContext.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.ProductTypeId == productTypeId && ci.UserId == userId);

			if (dbCartItem == null)
			{
				return false;
			}

			_dbContext.CartItems.Remove(dbCartItem);

			return true;
		}

		public async Task StoreCartItems(List<CartItem> cartItem, Guid userId)
		{
			cartItem.ForEach(cartItem => cartItem.SetUserId(userId));

			foreach (var item in cartItem)
			{
				await AddToCart(item);
			}
		}
	}
}
