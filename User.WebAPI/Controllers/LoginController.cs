using Common.Jwt;
using Common.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Tea.Utils;
using User.Domain;
using User.Infrastructure.DbContexts;
using User.WebAPI.Controllers.Request;
using User.WebAPI.Controllers.Responses;

namespace User.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly UserDomainService _domainService;
		private readonly ITokenService _tokenService;
		private readonly IUserRepository _userRepository;
		private readonly IOptionsSnapshot<JwtSetting> _options;
		private readonly IRabbitMqService _rabbitMqService;

		public LoginController(UserDomainService domainService, ITokenService tokenService, IUserRepository userRepository, IOptionsSnapshot<JwtSetting> options, IRabbitMqService rabbitMqService)
		{
			_domainService = domainService;
			_tokenService = tokenService;
			_userRepository = userRepository;
			_options = options;
			_rabbitMqService = rabbitMqService;
		}

		[HttpPost]
		[UnitOfWork(typeof(UserDbContext))]
		[NotCheckJwtVersion]
		public async Task<ActionResult<ServiceResponse<string>>> LoginByPhoneAndPassword(LoginRequest req)
		{
			ServiceResponse<string> resp = new ServiceResponse<string>();
			if (req.password.Length <= 3)
			{
				resp.Data = "密码长度必须大于3";
				resp.Success = false;
				return BadRequest(resp);
			}

			var isExist = await _userRepository.FindOneAsync(req.phone);
			if (isExist == null)
			{
				resp.Data = $"{req.phone.PhoneNumberValue}用户不存在";
				resp.Success = false;
				return BadRequest(resp);
			}

			var result = await _domainService.CheckPassword(req.phone, req.password);
			switch (result)
			{
				case UserAccessResult.Ok:
					_domainService.UpdateJwtVersion(isExist);

					List<Claim> claims = new List<Claim>()
					{
						new Claim(ClaimTypes.NameIdentifier, isExist.Id.ToString()),
						new Claim(ClaimTypes.Name,isExist.Name),
						new Claim(ClaimTypes.Role,isExist.Role),
						new Claim("JwtVersion",isExist.JwtVersion.ToString())
					};
					string jwt = _tokenService.BuildToken(claims, _options.Value);
					resp.Data = jwt;

					await _rabbitMqService.PublishMessage("ycode_shop", isExist.Id.ToString(), isExist.JwtVersion.ToString(), "");

					return Ok(resp);
				case UserAccessResult.PhoneNumberNotFound:
				case UserAccessResult.NoPassword:
				case UserAccessResult.PasswordError:
					resp.Data = "登录失败";
					resp.Success = false;
					return BadRequest(resp);
				case UserAccessResult.Lockout:
					resp.Data = "账户被锁定";
					resp.Success = false;
					return BadRequest(resp);
				default:
					throw new ApplicationException($"未知值: {result}");
			}

		}

		[HttpPost]
		[UnitOfWork(typeof(UserDbContext))]
		[NotCheckJwtVersion]
		public async Task<ActionResult<ServiceResponse<string>>> LoginByPhoneAndCode(LoginRequest req)
		{
			ServiceResponse<string> resp = new ServiceResponse<string>();

			var isExist = await _userRepository.FindOneAsync(req.phone);
			if (isExist == null)
			{
				resp.Data = $"{req.phone.PhoneNumberValue}用户不存在";
				resp.Success = false;
				return BadRequest(resp);
			}

			var result = await _domainService.CheckPhoneNumberCodeAsync(req.phone, req.code);

			switch (result)
			{
				case CheckCodeResult.Ok:
					_domainService.UpdateJwtVersion(isExist);

					List<Claim> claims = new List<Claim>()
					{
						new Claim(ClaimTypes.NameIdentifier, isExist.Id.ToString()),
						new Claim(ClaimTypes.Name,isExist.Name),
						new Claim(ClaimTypes.Role,isExist.Role),
						new Claim("JwtVersion",isExist.JwtVersion.ToString())
					};
					string jwt = _tokenService.BuildToken(claims, _options.Value);
					resp.Data = jwt;

					await _rabbitMqService.PublishMessage("ycode_shop", isExist.Id.ToString(), isExist.JwtVersion.ToString(), "");

					return Ok(resp);
				case CheckCodeResult.PhoneNumberNotFound:
				case CheckCodeResult.CodeError:
					resp.Data = "登录失败";
					resp.Success = false;
					return BadRequest(resp);
				case CheckCodeResult.Lockout:
					resp.Data = "账户被锁定";
					resp.Success = false;
					return BadRequest(resp);
				default:
					throw new ApplicationException($"未知值: {result}");
			}
		}

		[HttpPost]
		[UnitOfWork(typeof(UserDbContext))]
		public async Task<ActionResult<ServiceResponse<bool>>> Logout()
		{
			ServiceResponse<bool> resp = new ServiceResponse<bool>();
			var claim_UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var isExist = await _userRepository.FindOneAsync(Guid.Parse(claim_UserId));
			if (isExist == null)
			{
				resp.Success = false;
				resp.Message = "登出失败";
				return BadRequest(resp);
			}

			_domainService.UpdateJwtVersion(isExist);

			await _rabbitMqService.PublishMessage("ycode_shop", isExist.Id.ToString(), isExist.JwtVersion.ToString(), "");

			resp.Message = "登出成功";

			return Ok(resp);
		}

		[HttpPost]
		[NotCheckJwtVersion]
		public async Task<ActionResult<ServiceResponse<bool>>> GetCode(LoginRequest req)
		{
			ServiceResponse<bool> resp = new ServiceResponse<bool>();

			int randomNumber = Random.Shared.Next(100000, 999999);

			await _domainService.SendCodeAsync(req.phone, randomNumber.ToSafeString());
			resp.Data = true;
			resp.Message = "发送成功";
			return Ok(resp);
		}
	}
}
