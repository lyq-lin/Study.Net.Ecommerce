using Cart.Domain.Entity;

namespace Cart.Domain
{
	public class CartDomainService
	{
		private readonly ICartRepository _cartRepository;

		public CartDomainService(ICartRepository cartRepository)
		{
			_cartRepository = cartRepository;
		}

		public async Task<bool> AddToCart(CartItem cartItem, Guid userId)
		{
			cartItem.SetUserId(userId);

			var sameItem = await _cartRepository.FindSameCartInDb(cartItem);

			if (sameItem == null)
			{
				await _cartRepository.AddToCart(cartItem);
			}
			else
			{
				sameItem.UpdateQuantity(sameItem.Quantity + cartItem.Quantity);
			}

			return true;
		}

		public async Task<bool> UpdateQuantity(CartItem cartItem, Guid userId)
		{
			cartItem.SetUserId(userId);

			var sameItem = await _cartRepository.FindSameCartInDb(cartItem);

			if (sameItem == null)
			{
				return false;
			}

			sameItem.UpdateQuantity(cartItem.Quantity);

			return true;
		}
	}
}
