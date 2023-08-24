using Aop.Api.Domain;
using Common.Jwt;
using Common.RabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Order.Domain;
using Order.Infrastructure.DbContexts;
using Order.WebAPI.Controllers.Response;
using System.Security.Claims;
using System.Text.Json;

namespace Order.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IDistributedCache _cache;
		private readonly IOrderRepository _orderRepository;
		private readonly OrderDbContext _dbContext;
		private readonly OrderDomainService _domainService;
		private readonly IRabbitMqService _rabbitMqService;

		public OrderController(IDistributedCache cache, IOrderRepository orderRepository, OrderDbContext dbContext, OrderDomainService domainService, IRabbitMqService rabbitMqService)
		{
			_cache = cache;
			_orderRepository = orderRepository;
			_dbContext = dbContext;
			_domainService = domainService;
			_rabbitMqService = rabbitMqService;
		}

		private Guid GetUserId() => Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);


		[HttpGet("{orderId}"), Authorize]
		public async Task<ActionResult<ServiceResponse<List<Order.Domain.Entity.OrderItem>>>> GetOrderDetail(Guid orderId)
		{
			var result = await _orderRepository.GetOrderDetails(orderId);

			ServiceResponse<List<Order.Domain.Entity.OrderItem>> resp = new ServiceResponse<List<Order.Domain.Entity.OrderItem>>() { Data = result };
			return Ok(resp);
		}

		[HttpGet, Authorize]
		public async Task<ActionResult<ServiceResponse<List<Order.Domain.Entity.Order>>>> GetOrders()
		{
			var result = await _orderRepository.GetOrders(GetUserId());

			ServiceResponse<List<Order.Domain.Entity.Order>> resp = new ServiceResponse<List<Order.Domain.Entity.Order>>() { Data = result };
			return Ok(resp);
		}

		[HttpPost]
		[NotCheckJwtVersion]
		[UnitOfWork(typeof(OrderDbContext))]
		public async Task<string> AlipayNotify()
		{
			string result = "success";

			var form = HttpContext.Request.Form;

			if ("TRADE_SUCCESS".Equals(form["trade_status"]))
			{
				Guid outTradeNo = Guid.Parse(form["out_trade_no"]);
				decimal totalPrice = decimal.Parse(form["total_amount"]);

				List<GoodsDetail> lineItem = JsonSerializer.Deserialize<List<GoodsDetail>>(await _cache.GetStringAsync($"goods_{outTradeNo}"));
				Guid userId = JsonSerializer.Deserialize<Guid>(await _cache.GetStringAsync($"user_{outTradeNo}"));

				Order.Domain.Entity.Order newOrder = await _domainService.CreateOrder(outTradeNo, userId, totalPrice);
				foreach (var product in lineItem)
				{
					Order.Domain.Entity.OrderItem item = await _domainService.CreateOrderItem(newOrder, Guid.Parse(product.OutItemId), Guid.Parse(product.OutSkuId), decimal.Parse(product.Price), (int)product.Quantity);

					_dbContext.OrderItems.Add(item);

					//构建消息
					Dictionary<string, Guid> dict = new Dictionary<string, Guid>();
					dict.Add("ProductId", item.ProductId);
					dict.Add("ProductTypeId", item.ProductTypeId);
					dict.Add("UserId", userId);
					string CartJson = JsonSerializer.Serialize(dict);

					//发布消息
					await _rabbitMqService.PublishMessage("ycode_shop_cart", "cart", CartJson, "RemoveCart", "direct");
				}
				_dbContext.Orders.Add(newOrder);

			}
			else if ("TRADE_CLOSED".Equals(form["trade_status"]))
			{
				Console.WriteLine($"交易关闭: {DateTime.Now}");
				result = "fail";
			}
			else if ("TRADE_FINISHED".Equals(form["trade_status"]))
			{
				Console.WriteLine($"交易完结:{DateTime.Now}");
			}
			else if ("WAIT_BUYER_PAY".Equals(form["trade_status"]))
			{
				Console.WriteLine($"交易创建:{DateTime.Now}");
			}
			return result;
		}
	}
}