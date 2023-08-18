using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Common.Jwt
{
	public static class AuthenticationExtensions
	{
		public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, JwtSetting jwtOpt)
		{
			return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(x => x.TokenValidationParameters = new()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt.SecKey))
				});
		}
	}
}
