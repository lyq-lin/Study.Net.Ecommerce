using MediatR;
using User.Domain;

namespace User.WebAPI
{
	public class UserAccessResultEventHandle : INotificationHandler<UserAccessResultEvent>
	{
		private readonly IUserRepository _userRepository;

		public UserAccessResultEventHandle(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		public Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
		{
			return _userRepository.AddNewLoginHistoryAsync(notification.phone, $"登录结果是{notification.result}");
		}
	}
}
