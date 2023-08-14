using Microsoft.AspNetCore.Mvc;
using Product.Domain;
using Product.WebAPI.Controllers.Response;

namespace Product.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _productRepository;

		public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<Product.Domain.Entity.Product>>>> GetProductList()
		{
			var result = await _productRepository.FindAllProductsAsync();
			var resp = new ServiceResponse<List<Product.Domain.Entity.Product>>();

			if (result.Count == 0)
			{
				resp.Success = false;
				resp.Message = "没有更多的数据";
				return BadRequest(resp);
			}


			resp.Data = result;
			return Ok(resp);
		}

		[HttpGet("{productId}")]
		public async Task<ActionResult<ServiceResponse<Product.Domain.Entity.Product>>> GetProductById(Guid productId)
		{
			var result = await _productRepository.FindProductByIdAsync(productId);

			var resp = new ServiceResponse<Product.Domain.Entity.Product>()
			{
				Data = result
			};

			return Ok(resp);
		}

		[HttpGet("{categoryUrl}")]
		public async Task<ActionResult<ServiceResponse<List<Product.Domain.Entity.Product>>>> GetProductByCategory(string categoryUrl)
		{
			var result = await _productRepository.FindProductByCategoryAsync(categoryUrl);

			var resp = new ServiceResponse<List<Product.Domain.Entity.Product>>()
			{
				Data = result
			};

			return Ok(resp);
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<Product.Domain.Entity.Product>>>> GetFeaturedProducts()
		{
			var result = await _productRepository.FindProductsByFeatureAsync();
			var resp = new ServiceResponse<List<Product.Domain.Entity.Product>>()
			{
				Data = result
			};

			return Ok(resp);
		}


		[HttpGet("{searchText}/{page}")]
		public async Task<ActionResult<ServiceResponse<List<Product.Domain.Entity.Product>>>> GetProductBySearch(string searchText, int page = 1)
		{
			var result = await _productRepository.FindProductBySearchAsync(searchText, page);

			var resp = new ServiceResponse<ProductSearchResponse>()
			{
				Data = new ProductSearchResponse
				{
					Products = result.Item1,
					CurrentPage = page,
					Pages = (int)result.Item2
				}
			};

			return Ok(resp);
		}

		[HttpGet("{searchText}")]
		public async Task<ActionResult<ServiceResponse<List<Product.Domain.Entity.Product>>>> GetProductBySearch(string searchText)
		{
			var result = await _productRepository.FindProductBySearchAsync(searchText);

			var resp = new ServiceResponse<List<Product.Domain.Entity.Product>>()
			{
				Data = result
			};

			return Ok(resp);
		}




		[HttpGet("{searchText}")]
		public async Task<ActionResult<ServiceResponse<List<string>>>> GetProductSearchSuggestions(string searchText)
		{
			var result = await _productRepository.GetProductSearchSuggestionsAsync(searchText);

			var resp = new ServiceResponse<List<string>>()
			{
				Data = result
			};
			return Ok(resp);
		}
	}
}
