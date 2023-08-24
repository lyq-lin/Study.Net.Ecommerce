using Cart.Domain.Entity;

namespace Cart.Domain
{
	public interface ICartRepository
	{
		Task StoreCartItems(List<CartItem> cartItem, Guid userId);
		Task<int> GetCartItemCount(Guid userId);
		Task<List<CartItem>> GetDbCartProducts(Guid userId);
		Task AddToCart(CartItem cartItem);
		Task<bool> RemoveItemFromCart(Guid productId, Guid productTypeId, Guid userId);
		Task<CartItem> FindSameCartInDb(CartItem cartItem);
	}
}
