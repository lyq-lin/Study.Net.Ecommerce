using Common.Jwt;
using Microsoft.AspNetCore.Mvc;
using User.Domain;
using User.Infrastructure.DbContexts;
using User.WebAPI.Controllers.Request;
using User.WebAPI.Controllers.Responses;

namespace User.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly UserDbContext _dbContext;

		public UserController(IUserRepository userRepository, UserDbContext dbContext)
		{
			_userRepository = userRepository;
			_dbContext = dbContext;
		}

		[HttpPost]
		[UnitOfWork(typeof(UserDbContext))]
		[NotCheckJwtVersion]
		public async Task<ActionResult<ServiceResponse<string>>> AddNewUser(AddUserRequest req)
		{
			ServiceResponse<string> resp = new ServiceResponse<string>();
			if (await _userRepository.FindOneAsync(req.phone) != null)
			{
				resp.Success = false;
				resp.Message = "手机号已存在";
				return BadRequest(resp);
			}

			var user = new User.Domain.Entity.User(req.phone, req.name);
			user.ChangePassword(req.passowrd);

			_dbContext.Users.Add(user);

			resp.Message = "注册成功";
			return Ok(resp);
		}
	}
}
