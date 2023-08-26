using Cart.Domain;
using Cart.Infrastructure;
using Cart.Infrastructure.DbContexts;
using Cart.WebAPI;
using Common.Jwt;
using Common.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
	opt.AddAuthenticationHeader();
});

//Jwt
var JwtConfig = builder.Configuration.GetSection("Jwt");
builder.Services.AddJwtAuthentication(JwtConfig.Get<JwtSetting>());

//注入RabbitMQ
builder.Services.AddRabbitMQ();

//注入过滤器
builder.Services.Configure<MvcOptions>(opt =>
{
	opt.Filters.Add<CartJwtVersionCheckFilter>();
	opt.Filters.Add<UnitOfWorkFilter>();
});

//注入DbContext
builder.Services.AddDbContext<CartDbContext>(opt =>
{
	opt.UseNpgsql(builder.Configuration.GetSection("ConnStr").Value);
});

//自定义服务
builder.Services.AddScoped<CartDomainService>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

//注入Redis
builder.Services.AddStackExchangeRedisCache(opt =>
{
	opt.Configuration = "43.143.170.48";
	opt.InstanceName = "shop_";
});

builder.Services.AddCors(opt =>
{
	opt.AddDefaultPolicy(bui =>
	{
		bui.WithOrigins(new string[] { "http://43.143.170.48:8080" })
		.AllowAnyMethod().AllowAnyHeader().AllowCredentials();
	});
});

var app = builder.Build();

//在程序启动时订阅购物车清除消息队列
var serviceProvider = builder.Services.BuildServiceProvider();
var _rabbit = serviceProvider.GetService<IRabbitMqService>();

_rabbit.SubscribeMessage("ycode_shop_cart", "cart", "RemoveCart", (message) =>
{
	var dbContext = serviceProvider.GetService<CartDbContext>();
	var dict = JsonSerializer.Deserialize<Dictionary<string, Guid>>(message);

	var isExist = dbContext.CartItems.FirstOrDefault(x => x.ProductId == dict["ProductId"] && x.ProductTypeId == dict["ProductTypeId"] && x.UserId == dict["UserId"]);

	dbContext.CartItems.Remove(isExist);

	dbContext.SaveChanges();
});

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
