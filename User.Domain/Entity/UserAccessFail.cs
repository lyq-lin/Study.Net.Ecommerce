namespace User.Domain.Entity
{
	public class UserAccessFail
	{
		public Guid Id { get; init; }
		public User User { get; init; }
		public Guid UserId { get; init; }

		public DateTime? LockEnd { get; private set; }
		public int AccessFailCount { get; private set; }

		private bool isLockOut;

		private UserAccessFail()
		{

		}

		public UserAccessFail(User user)
		{
			this.Id = Guid.NewGuid();
			this.User = user;
		}

		public void Reset()
		{
			this.AccessFailCount = 0;
			this.LockEnd = null;
			this.isLockOut = false;
		}

		public void Fail()
		{
			AccessFailCount++;
			if (AccessFailCount >= 3)
			{
				isLockOut = true;
				LockEnd = DateTime.UtcNow.AddMinutes(5);
			}
		}

		public bool IsLockOut()
		{
			if (this.isLockOut)
			{
				if (DateTime.UtcNow > this.LockEnd)
				{
					Reset();
					return false;
				}
				else { return true; }
			}
			else { return false; }
		}
	}
}
