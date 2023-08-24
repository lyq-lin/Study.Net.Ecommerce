using Common.Jwt;
using Common.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
		private readonly UserDomainService _domainService;
		private readonly IRabbitMqService _rabbitMqService;

		public UserController(IUserRepository userRepository, UserDbContext dbContext,UserDomainService domainService,IRabbitMqService rabbitMqService)
		{
			_userRepository = userRepository;
			_dbContext = dbContext;
			_domainService = domainService;
			_rabbitMqService = rabbitMqService;
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

			string code = await _userRepository.FindPhoneNumberCodeAsync(req.phone);

			if (code == null || code.Equals(""))
			{
				resp.Success = false;
				resp.Message = "请先获取验证码";
				return BadRequest(resp);
			}

			var user = new User.Domain.Entity.User(req.phone, req.name);
			user.ChangePassword(req.password);

			_dbContext.Users.Add(user);

			resp.Message = "注册成功";
			return Ok(resp);
		}

		[HttpPost("{password}")]
		[UnitOfWork(typeof(UserDbContext))]
		public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(string password)
		{
			ServiceResponse<bool> resp = new ServiceResponse<bool>();
			var isExist = await _userRepository.FindOneAsync(Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value));

			if (isExist == null)
			{
				resp.Success = false;
				resp.Message = "修改失败";
				return BadRequest(resp);
			}

			isExist.ChangePassword(password);

			_domainService.UpdateJwtVersion(isExist);

			_dbContext.Users.Update(isExist);

			await _rabbitMqService.PublishMessage("ycode_shop", isExist.Id.ToString(), isExist.JwtVersion.ToString(), "");

			resp.Message = "修改成功";

			return Ok(resp);
		}
	}
}
