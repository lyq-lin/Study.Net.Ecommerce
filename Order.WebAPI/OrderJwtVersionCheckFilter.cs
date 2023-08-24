using Common.Jwt;
using Common.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Order.WebAPI.Controllers.Response;
using System.Security.Claims;
using System.Text.Json;

namespace Order.WebAPI
{
	public class OrderJwtVersionCheckFilter : IAsyncActionFilter
	{
		private readonly IRabbitMqService _rabbitMqService;
		private readonly IDistributedCache _cache;

		public OrderJwtVersionCheckFilter(IRabbitMqService rabbitMqService, IDistributedCache cache)
		{
			_rabbitMqService = rabbitMqService;
			_cache = cache;
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
			if (descriptor == null)
			{
				await next();
				return;
			}

			if (descriptor.MethodInfo.GetCustomAttributes(typeof(NotCheckJwtVersionAttribute), true).Any())
			{
				await next();
				return;
			}

			var claimJwtVersion = context.HttpContext.User.FindFirst("JwtVersion");
			if (claimJwtVersion == null)
			{
				context.Result = new ObjectResult(new ServiceResponse<string>() { Success = false, Message = "没有找到JwtVersion的内容" }) { StatusCode = 400 };
				return;
			}

			var claimUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

			long JwtVersionFromClient = long.Parse(claimJwtVersion.Value);

			var data = await _cache.GetStringAsync($"jwtv_{claimUserId.Value}");
			if (data != null)
			{
				long jwtVersion = JsonSerializer.Deserialize<long>(data);
				if (jwtVersion > JwtVersionFromClient)
				{
					context.Result = new ObjectResult(new ServiceResponse<string>() { Success = false, Message = "Jwt过时" }) { StatusCode = 400 };
					return;
				}

				await _cache.SetStringAsync($"jwtv_mock_{claimUserId.Value}", JsonSerializer.Serialize(jwtVersion), new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600)
				});
			}
			else
			{
				string jwtVersionFromRabbitMQ = await _rabbitMqService.ConsumeMessage("ycode_shop", claimUserId.Value, "");

				if (jwtVersionFromRabbitMQ != null && jwtVersionFromRabbitMQ != string.Empty)
				{
					long jwtVersion = JsonSerializer.Deserialize<long>(jwtVersionFromRabbitMQ);
					if (jwtVersion > JwtVersionFromClient)
					{
						context.Result = new ObjectResult(new ServiceResponse<string>() { Success = false, Message = "Jwt过时" }) { StatusCode = 400 };
						return;
					}

					await _cache.SetStringAsync($"jwtv_mock_{claimUserId.Value}", JsonSerializer.Serialize(jwtVersion), new DistributedCacheEntryOptions
					{
						AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600)
					});

				}
				else
				{
					var mock_jwtVersion = await _cache.GetStringAsync($"jwtv_mock_{claimUserId.Value}");
					if (mock_jwtVersion != null && mock_jwtVersion != string.Empty)
					{
						long jwtVersion = JsonSerializer.Deserialize<long>(mock_jwtVersion);
						if (jwtVersion > JwtVersionFromClient)
						{
							context.Result = new ObjectResult(new ServiceResponse<string>() { Success = false, Message = "Jwt过时" }) { StatusCode = 400 };
							return;
						}

					}
					else
					{
						context.Result = new ObjectResult(new ServiceResponse<string>() { Success = false, Message = "Jwt过时" }) { StatusCode = 400 };
						return;
					}
				}
			}

			await next();
		}
	}
}
