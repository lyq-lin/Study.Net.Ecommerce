using Cart.Domain;
using Cart.Domain.Entity;
using Cart.Infrastructure.DbContexts;
using Cart.WebAPI.Controllers.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cart.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartRepository _cartRepository;
		private readonly CartDomainService _domainService;

		public CartController(ICartRepository cartRepository, CartDomainService domainService)
		{
			_cartRepository = cartRepository;
			_domainService = domainService;
		}

		private Guid UserId() => Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

		[HttpPost]
		[UnitOfWork(typeof(CartDbContext))]
		public async Task<ActionResult> StoreCartItems(List<CartItem> cartItems)
		{
			await _cartRepository.StoreCartItems(cartItems, UserId());

			return Ok();
		}

		[HttpPost]
		[UnitOfWork(typeof(CartDbContext))]
		public async Task<ActionResult<ServiceResponse<bool>>> AddCartItems(CartItem cartItem)
		{
			var result = await _domainService.AddToCart(cartItem, UserId());
			ServiceResponse<bool> resp = new ServiceResponse<bool>() { Data = result };

			if (!result)
			{
				resp.Success = false;
				return BadRequest(resp);
			}
			return Ok(resp);
		}

		[HttpPut]
		[UnitOfWork(typeof(CartDbContext))]
		public async Task<ActionResult<ServiceResponse<bool>>> UpdateQuantity(CartItem cartItem)
		{
			var result = await _domainService.UpdateQuantity(cartItem, UserId());
			ServiceResponse<bool> resp = new ServiceResponse<bool>() { Data = result };

			if (!result)
			{
				resp.Success = false;
				return BadRequest(resp);
			}
			return Ok(resp);
		}

		[HttpDelete("{productId}/{productTypeId}")]
		[UnitOfWork(typeof(CartDbContext))]
		public async Task<ActionResult<ServiceResponse<bool>>> RemoveCartItem(Guid productId, Guid productTypeId)
		{
			var result = await _cartRepository.RemoveItemFromCart(productId, productTypeId, UserId());
			ServiceResponse<bool> resp = new ServiceResponse<bool> { Data = result };
			if (!result)
			{
				resp.Success = false;
				return BadRequest(resp);
			}
			return Ok(resp);
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<int>>> GetCartItemCount()
		{
			int count = await _cartRepository.GetCartItemCount(UserId());
			ServiceResponse<int> resp = new ServiceResponse<int> { Data = count };

			return Ok(resp);
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<CartItem>>>> GetCartProductsInDb()
		{
			var result = await _cartRepository.GetDbCartProducts(UserId());

			ServiceResponse<List<CartItem>> resp = new ServiceResponse<List<CartItem>> { Data = result };

			return Ok(resp);
		}
	}
}
