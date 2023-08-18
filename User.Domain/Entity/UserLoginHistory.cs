using User.Domain.ValueObject;

namespace User.Domain.Entity
{
	public class UserLoginHistory : IAggregateRoot
	{
		public long Id { get; set; }
		public Guid? UserId { get; set; }
		public PhoneNumber PhoneNumber { get; set; }
		public DateTime CreatedDateTime { get; init; }
		public string Message { get; init; }

		private UserLoginHistory()
		{

		}

		public UserLoginHistory(Guid? userId, string message, PhoneNumber phoneNumber)
		{
			this.UserId = userId;
			this.CreatedDateTime = DateTime.UtcNow;
			this.Message = message;
			this.PhoneNumber = phoneNumber;
		}
	}
}
