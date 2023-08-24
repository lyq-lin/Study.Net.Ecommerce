using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Payment.WebAPI.Controllers.Request;
using System.Security.Claims;
using System.Text.Json;

namespace Payment.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IAopClient _clien;
		private readonly AlipayTradePagePayRequest _request;
		private readonly IDistributedCache _cache;

		public PaymentController(IAopClient clien, AlipayTradePagePayRequest request, IDistributedCache cache)
		{
			_clien = clien;
			_request = request;
			_cache = cache;
		}

		private Guid GetUserId() => Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

		[HttpPost, Authorize]
		public async Task<ActionResult<string>> CreateCheckoutSession(List<PaymentRequest> payments)
		{
			var lineItem = new List<GoodsDetail>();

			decimal TotalAmount = 0m;
			foreach (var product in payments)
			{
				lineItem.Add(new GoodsDetail
				{
					OutItemId = product.productId.ToString(),
					GoodsName = product.title,
					Quantity = product.quantity,
					Price = product.price.ToString(),
					GoodsCategory = product.productType,
					OutSkuId = product.productTypeId.ToString(),
					ShowUrl = product.imageUrl
				});
			}

			payments.ForEach(x => TotalAmount += (x.price * x.quantity));

			AlipayTradePagePayModel model = new AlipayTradePagePayModel()
			{
				OutTradeNo = Guid.NewGuid().ToString(),
				TotalAmount = TotalAmount.ToString(),
				Subject = payments.FirstOrDefault().title + $"等{payments.Count}件商品",
				ProductCode = "FAST_INSTANT_TRADE_PAY",
				GoodsDetail = lineItem,
			};

			_request.SetBizModel(model);

			AlipayTradePagePayResponse response = _clien.pageExecute(_request, null, "GET");

			if (response.IsError)
			{
				Console.WriteLine("调用失败!");
			}
			else
			{
				Console.WriteLine("调用成功!");

				//把商品细节存进缓存
				await _cache.SetStringAsync($"goods_{model.OutTradeNo}", JsonSerializer.Serialize(lineItem), new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
				});

				//把当前登录的UserId存入缓存
				await _cache.SetStringAsync($"user_{model.OutTradeNo}", JsonSerializer.Serialize(GetUserId()), new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
				});
			}

			return response.Body;
		}
	}
}
