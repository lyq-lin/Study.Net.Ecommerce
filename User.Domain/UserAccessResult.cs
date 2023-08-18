namespace User.Domain
{
	public enum UserAccessResult
	{
		Ok, PhoneNumberNotFound, Lockout, NoPassword, PasswordError
	}
}
