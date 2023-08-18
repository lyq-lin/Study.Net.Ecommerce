using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Common.Jwt
{
	public class TokenService : ITokenService
	{
		public string BuildToken(IEnumerable<Claim> claims, JwtSetting setting)
		{
			TimeSpan ExpiryDuration = TimeSpan.FromSeconds(setting.ExpireSeconds);

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecKey));

			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

			var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
		}
	}
}
