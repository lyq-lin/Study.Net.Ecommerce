using User.Domain.ValueObject;

namespace User.Domain
{
	public interface IUserRepository
	{
		Task<User.Domain.Entity.User> FindOneAsync(PhoneNumber phone);
		Task<User.Domain.Entity.User> FindOneAsync(Guid userId);
		Task AddNewLoginHistoryAsync(PhoneNumber phone, string message);
		Task SavePhoneNumberCodeAsync(PhoneNumber phone, string code);
		Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phone);
		Task PublishEventAsync(UserAccessResultEvent userAccessResultEvent);
	}
}
