using Microsoft.AspNetCore.Mvc;
using Product.Domain;
using Product.Domain.Entity;
using Product.WebAPI.Controllers.Response;

namespace Product.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductTypeController : ControllerBase
	{
		private readonly IProductTypeRepository _productTypeRepository;

		public ProductTypeController(IProductTypeRepository productTypeRepository)
		{
			_productTypeRepository = productTypeRepository;
		}


		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<ProductType>>>> GetAllProductType()
		{
			var result = await _productTypeRepository.FindAllProductTypeAsync();

			ServiceResponse<List<ProductType>> resp = new ServiceResponse<List<ProductType>>()
			{
				Data = result
			};

			return Ok(resp);
		}

		[HttpGet("{productId}")]
		public async Task<ActionResult<ServiceResponse<List<ProductType>>>> GetProductTypeByProductId(Guid productId)
		{
			var result = await _productTypeRepository.GetProductTypeByProductIdAsync(productId);

			ServiceResponse<List<ProductType>> resp = new ServiceResponse<List<ProductType>>()
			{
				Data = result
			};

			return Ok(resp);
		}

		[HttpGet("{productTypeId}")]
		public async Task<ActionResult<ServiceResponse<string>>> GetTypeNameByTypeId(Guid productTypeId)
		{
			string TypeName =  await _productTypeRepository.GetTypeNameByProductTypeId(productTypeId);

			ServiceResponse<string> resp = new ServiceResponse<string>() { Data = TypeName };

			return Ok(resp);
		}
	}
}
