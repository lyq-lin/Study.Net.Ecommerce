using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entity;

namespace User.Infrastructure.Configs
{
	public class UserLoginHistoryConfig : IEntityTypeConfiguration<UserLoginHistory>
	{
		public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
		{
			builder.ToTable($"T_{nameof(UserLoginHistory)}");

			builder.OwnsOne(x => x.PhoneNumber, nb =>
			{
				nb.Property(x => x.PhoneNumberValue).HasMaxLength(20).IsUnicode(false);
			});
		}
	}
}
