using Microsoft.AspNetCore.Mvc;
using Product.Domain;
using Product.Domain.Entity;
using Product.WebAPI.Controllers.Response;

namespace Product.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductVariantController : ControllerBase
	{
		private readonly IProductVariantRepository _productVariantRepository;

		public ProductVariantController(IProductVariantRepository productVariantRepository)
		{
			_productVariantRepository = productVariantRepository;
		}

		[HttpGet("{productId}/{productTypeId}")]
		public async Task<ActionResult<ServiceResponse<ProductVariant>>> GetVariantByProduct(Guid productId, Guid productTypeId)
		{
			var result = await _productVariantRepository.GetVariantByProductAsync(productId, productTypeId);
			var resp = new ServiceResponse<ProductVariant>() { Data = result };

			return Ok(resp);
		}
	}
}
