using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.Jwt
{
	public interface ITokenService
	{
		string BuildToken(IEnumerable<Claim> claims, JwtSetting setting);
	}
}
