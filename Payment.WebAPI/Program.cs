using Common.Alipay;
using Common.Jwt;
using Common.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Payment.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
	opt.AddAuthenticationHeader();
});

//读取配置文件中Jwt的内容
var JwtConfig = builder.Configuration.GetSection("Jwt");
builder.Services.AddJwtAuthentication(JwtConfig.Get<JwtSetting>());

//读取配置文件中Alipay的内容
var AlipayConfig = builder.Configuration.GetSection("Alipay");
builder.Services.AddAlipay(AlipayConfig.Get<AlipaySetting>());

//注入分布式缓存
builder.Services.AddStackExchangeRedisCache(opt =>
{
	opt.Configuration = "43.143.170.48";
	opt.InstanceName = "shop_";
});

//注入过滤器
builder.Services.Configure<MvcOptions>(opt =>
{
	opt.Filters.Add<PaymentJwtVersionCheckFilter>();
});

//注入RabbitMQ
builder.Services.AddRabbitMQ();

//注入自定义服务
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();

builder.Services.AddCors(opt =>
{
	opt.AddDefaultPolicy(bui =>
	{
		bui.WithOrigins(new string[] { "http://43.143.170.48:8080" })
		.AllowAnyMethod().AllowAnyHeader().AllowCredentials();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
