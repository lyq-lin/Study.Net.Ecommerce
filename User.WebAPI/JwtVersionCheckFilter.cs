using Common.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using System.Text.Json;
using User.Domain;
using User.WebAPI.Controllers.Responses;

namespace User.WebAPI
{
	public class JwtVersionCheckFilter : IAsyncActionFilter
	{
		private readonly IDistributedCache _distributedCache;
		private readonly IUserRepository _userRepository;

		public JwtVersionCheckFilter(IDistributedCache distributedCache, IUserRepository userRepository)
		{
			_distributedCache = distributedCache;
			_userRepository = userRepository;
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var descriptior = context.ActionDescriptor as ControllerActionDescriptor;
			if (descriptior == null)
			{
				await next();
				return;
			}

			if (descriptior.MethodInfo.GetCustomAttributes(typeof(NotCheckJwtVersionAttribute), true).Any())
			{
				await next();
				return;
			}

			var claimJwtVerion = context.HttpContext.User.FindFirst("JwtVersion");
			if (claimJwtVerion == null)
			{
				context.Result = new ObjectResult(new ServiceResponse<string>() { Success = false, Message = "没有找到JwtVersion的内容" }) { StatusCode = 400 };
				return;
			}

			var claimUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
			long JwtVersionFromClient = long.Parse(claimJwtVerion.Value);

			var data = await _distributedCache.GetStringAsync($"jwtv_{claimUserId.Value}");
			if (data != null)
			{
				long JwtVersion = JsonSerializer.Deserialize<long>(data);
				if (JwtVersion > JwtVersionFromClient)
				{
					context.Result = new ObjectResult(new ServiceResponse<string> { Success = false, Message = "Jwt过时" }) { StatusCode = 400 };
					return;
				}
			}
			else
			{
				var user = await _userRepository.FindOneAsync(Guid.Parse(claimUserId.Value));
				if (user == null)
				{
					context.Result = new ObjectResult(new ServiceResponse<string> { Success = false, Message = "在数据库中查询不到该用户" }) { StatusCode = 400 };
					return;
				}

				await _distributedCache.SetStringAsync(
					$"jwtv_{claimUserId.Value}",
					JsonSerializer.Serialize(user.JwtVersion),
					new DistributedCacheEntryOptions()
					{
						AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.Next(7, 10)),
						SlidingExpiration = TimeSpan.FromSeconds(5)
					});

				if (user.JwtVersion > JwtVersionFromClient)
				{
					context.Result = new ObjectResult(new ServiceResponse<string> { Success = false, Message = "Jwt过时" }) { StatusCode = 400 };
					return;
				}
			}

			await next();
		}
	}
}
