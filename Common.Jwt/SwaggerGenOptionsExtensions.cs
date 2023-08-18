using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Jwt
{
	public static class SwaggerGenOptionsExtensions
	{
		public static void AddAuthenticationHeader(this SwaggerGenOptions swagger)
		{
			swagger.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
			{
				Description = "Authorization header . \r\n Example: Bearer ***",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Authorization"
			});

			swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme{
						Reference = new OpenApiReference{
						Type = ReferenceType.SecurityScheme,Id = "Authorization"
						},
						Scheme = "oauth2",
						Name = "Authorization",
						In = ParameterLocation.Header,
					},
					new List<string>()
				}
			});
		}
	}
}
