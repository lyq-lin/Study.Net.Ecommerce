using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entity;

namespace User.Infrastructure.Configs
{
	public class UserConfig : IEntityTypeConfiguration<User.Domain.Entity.User>
	{
		public void Configure(EntityTypeBuilder<Domain.Entity.User> builder)
		{
			builder.ToTable($"T_{nameof(Domain.Entity.User)}");

			builder.OwnsOne(x => x.PhoneNumber, nb =>
			{
				nb.Property(x => x.RegionNumber).HasMaxLength(5).IsUnicode(false);
				nb.Property(x => x.PhoneNumberValue).HasMaxLength(20).IsUnicode(false);
			});

			builder.HasOne(b => b.UserAccessFail).WithOne(f => f.User).HasForeignKey<UserAccessFail>(x => x.UserId);

			builder.Property(x => x.PasswordHash).HasMaxLength(255).IsUnicode(false);
			builder.Property(x => x.PasswordSalt).HasMaxLength(255).IsUnicode(false);
		}
	}
}
