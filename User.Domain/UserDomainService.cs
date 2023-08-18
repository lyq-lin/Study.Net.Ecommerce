using User.Domain.ValueObject;

namespace User.Domain
{
	public class UserDomainService
	{
		private readonly IUserRepository _userRepository;
		private readonly ISmsCodeSender _smsCodeSender;

		public UserDomainService(IUserRepository userRepository, ISmsCodeSender smsCodeSender)
		{
			_userRepository = userRepository;
			_smsCodeSender = smsCodeSender;
		}

		public void UpdateJwtVersion(User.Domain.Entity.User user)
		{
			user.UpdateJwtVersion();
		}

		public void ChangeName(User.Domain.Entity.User user, string newName)
		{
			user.ChangeName(newName);
		}

		private string GetRoleValue(int role) => role switch
		{
			1 => "Admin",
			2 => "Customer",
			_ => "Invalid Role"
		};

		public void SetRole(User.Domain.Entity.User user, int role)
		{
			user.SetRole(GetRoleValue(role));
		}

		public void ResetAccessFail(User.Domain.Entity.User user)
		{
			user.UserAccessFail.Reset();
		}

		public bool IsLockout(User.Domain.Entity.User user)
		{
			return user.UserAccessFail.IsLockOut();
		}
		public void AccessFail(User.Domain.Entity.User user)
		{
			user.UserAccessFail.Fail();
		}

		public async Task SendCodeAsync(PhoneNumber phone, string code)
		{
			await _smsCodeSender.SendAsync(phone, code);

			await _userRepository.SavePhoneNumberCodeAsync(phone, code);
		}

		public async Task<UserAccessResult> CheckPassword(PhoneNumber phone, string password)
		{
			UserAccessResult result;
			var user = await _userRepository.FindOneAsync(phone);
			if (user == null)
			{
				result = UserAccessResult.PhoneNumberNotFound;
			}
			else if (IsLockout(user))
			{
				result = UserAccessResult.Lockout;
			}
			else if (user.HashPassword() == false)
			{
				result = UserAccessResult.NoPassword;
			}
			else if (user.CheckPassword(password))
			{
				result = UserAccessResult.Ok;
			}
			else
			{
				result = UserAccessResult.PasswordError;
			}

			if (user != null)
			{
				if (result == UserAccessResult.Ok)
				{
					ResetAccessFail(user);
				}
				else
				{
					AccessFail(user);
					result = UserAccessResult.PasswordError;
				}
			}

			await _userRepository.PublishEventAsync(new UserAccessResultEvent(phone, result));
			return result;
		}

		public async Task<CheckCodeResult> CheckPhoneNumberCodeAsync(PhoneNumber phone, string code)
		{
			CheckCodeResult result;
			var user = await _userRepository.FindOneAsync(phone);
			if (user == null)
			{
				result = CheckCodeResult.PhoneNumberNotFound;
			}
			else if (IsLockout(user))
			{
				result = CheckCodeResult.Lockout;
			}

			string codeInCache = await _userRepository.FindPhoneNumberCodeAsync(phone);

			if (codeInCache == null)
			{
				result = CheckCodeResult.CodeError;
			}

			if (codeInCache == code)
			{
				ResetAccessFail(user);
				result = CheckCodeResult.Ok;
			}
			else
			{
				AccessFail(user);
				result = CheckCodeResult.CodeError;
			}

			return result;
		}
	}
}
