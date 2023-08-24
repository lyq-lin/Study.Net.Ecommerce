using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using User.Domain;
using User.Domain.ValueObject;
using User.Infrastructure.DbContexts;

namespace User.Infrastructure
{
	public class UserRepository : IUserRepository
	{
		private readonly UserDbContext _dbContext;
		private readonly IDistributedCache _distributedCache;
		private readonly IMediator _mediator;

		public UserRepository(UserDbContext dbContext, IDistributedCache distributedCache, IMediator mediator)
		{
			_dbContext = dbContext;
			_distributedCache = distributedCache;
			_mediator = mediator;
		}
		public async Task AddNewLoginHistoryAsync(PhoneNumber phone, string message)
		{
			var user = await FindOneAsync(phone);
			Guid? uid = user?.Id;

			_dbContext.UserLoginHistories.Add(new Domain.Entity.UserLoginHistory(uid, message, phone));
		}

		public async Task<Domain.Entity.User?> FindOneAsync(PhoneNumber phone)
		{
			return await _dbContext.Users.Include(x => x.UserAccessFail).SingleOrDefaultAsync(x => x.PhoneNumber.PhoneNumberValue == phone.PhoneNumberValue && x.PhoneNumber.RegionNumber == phone.RegionNumber);
		}

		public async Task<Domain.Entity.User?> FindOneAsync(Guid userId)
		{
			return await _dbContext.Users.Include(x => x.UserAccessFail).SingleOrDefaultAsync(x => x.Id == userId);
		}

		public async Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phone)
		{
			string key = $"PhoneNumberCode_{phone.RegionNumber}_{phone.PhoneNumberValue}";
			string? code = await _distributedCache.GetStringAsync(key);

			_distributedCache.Remove(key);

			return code;
		}

		public Task PublishEventAsync(UserAccessResultEvent userAccessResultEvent)
		{
			return _mediator.Publish(userAccessResultEvent);
		}

		public Task SavePhoneNumberCodeAsync(PhoneNumber phone, string code)
		{
			string key = $"PhoneNumberCode_{phone.RegionNumber}_{phone.PhoneNumberValue}";

            return _distributedCache.SetStringAsync(key, code, new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
			});
		}
	}
}
