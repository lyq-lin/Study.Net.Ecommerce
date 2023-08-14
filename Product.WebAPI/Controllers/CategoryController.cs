using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Domain;
using Product.Domain.Entity;
using Product.WebAPI.Controllers.Response;

namespace Product.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
		{
			var resp = new ServiceResponse<List<Category>>();
			var result = await _categoryRepository.FindAllCategoriesAsync();

			if (result == null || result.Count == 0)
			{
				resp.Success = false;
				resp.Message = "没有更多数据";
			}
			else
			{
				resp.Data = result;
			}

			return Ok(resp);
		}
	}
}
