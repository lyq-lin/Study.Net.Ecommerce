using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace User.WebAPI
{
	public class UnitOfWorkFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var result = await next();
			if (result.Exception != null)
			{
				return;
			}

			var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
			if (actionDesc == null)
			{
				return;
			}

			var uowAttr = actionDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
			if (uowAttr == null)
			{
				return;
			}

			foreach (var dbContextType in uowAttr.DbContextTypes)
			{
				var dbCtx = context.HttpContext.RequestServices.GetService(dbContextType) as DbContext;
				if (dbCtx != null)
				{
					await dbCtx.SaveChangesAsync();
				}
			}
		}
	}
}
