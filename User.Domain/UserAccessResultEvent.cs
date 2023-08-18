using MediatR;
using User.Domain.ValueObject;

namespace User.Domain
{
	public record UserAccessResultEvent(PhoneNumber phone, UserAccessResult result) : INotification;
}
