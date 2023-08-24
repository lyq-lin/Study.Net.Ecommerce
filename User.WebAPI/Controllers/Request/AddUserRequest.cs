using User.Domain.ValueObject;

namespace User.WebAPI.Controllers.Request
{
	public record AddUserRequest(PhoneNumber phone, string name, string code, string password);
}
