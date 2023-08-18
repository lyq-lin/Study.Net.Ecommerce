using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entity;

namespace User.Infrastructure.Configs
{
	public class UserAccessFailConfig : IEntityTypeConfiguration<UserAccessFail>
	{
		public void Configure(EntityTypeBuilder<UserAccessFail> builder)
		{
			builder.ToTable($"T_{nameof(UserAccessFail)}");

			builder.Property("isLockOut");
		}
	}
}
