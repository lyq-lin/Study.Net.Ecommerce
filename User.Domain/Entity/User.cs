using System.Security.Cryptography;
using User.Domain.ValueObject;

namespace User.Domain.Entity
{
	public class User:IAggregateRoot
	{
		public Guid Id { get; init; }
		public DateTime DateCreated { get; init; }
		public PhoneNumber PhoneNumber { get; private set; }
		public string Name { get; private set; }
		public string Role { get; private set; }
		public byte[]? PasswordHash { get; private set; }
		public byte[]? PasswordSalt { get; private set; }
		public int JwtVersion { get; private set; }
		public UserAccessFail UserAccessFail { get; private set; }

		private User()
		{

		}

		public User(PhoneNumber phone, string name, string role = "Customer")
		{
			this.Id = Guid.NewGuid();
			this.PhoneNumber = phone;
			this.Name = name;
			this.Role = role;
			this.UserAccessFail = new UserAccessFail(this);
			DateCreated = DateTime.UtcNow;
		}

		public void ChangeName(string name)
		{
			this.Name = name;
		}

		public void UpdateJwtVersion()
		{
			this.JwtVersion++;
		}

		public void SetRole(string role)
		{
			this.Role = role;
		}

		public bool HashPassword()
		{
			return !string.IsNullOrEmpty(System.Text.Encoding.UTF8.GetString(this.PasswordHash));
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		public void ChangePassword(string password)
		{
			if (password.Length <= 3)
			{
				Console.WriteLine("密码长度必须大于3");
				return;
			}
			CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
			PasswordHash = passwordHash;
			PasswordSalt = passwordSalt;
		}

		public bool CheckPassword(string password)
		{
			using (var hmac = new HMACSHA512(PasswordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(PasswordHash);
			}
		}

		public void ChangePhoneNumber(PhoneNumber phone)
		{
			this.PhoneNumber = phone;
		}
	}
}
