using User.Domain.ValueObject;

namespace User.WebAPI.Controllers.Request
{
	public record LoginRequest(PhoneNumber phone, string password, string code);
}
