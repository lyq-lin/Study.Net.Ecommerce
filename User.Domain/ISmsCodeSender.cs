using User.Domain.ValueObject;

namespace User.Domain
{
	public interface ISmsCodeSender
	{
		Task SendAsync(PhoneNumber phone, string code);
	}
}
