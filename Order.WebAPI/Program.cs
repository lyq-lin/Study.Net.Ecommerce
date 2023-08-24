using Common.Jwt;
using Common.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Domain;
using Order.Infrastructure;
using Order.Infrastructure.DbContexts;
using Order.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
	opt.AddAuthenticationHeader();
});

//ע�������
builder.Services.Configure<MvcOptions>(opt =>
{
	opt.Filters.Add<UnitOfWorkFilter>();
	opt.Filters.Add<OrderJwtVersionCheckFilter>();
});

//ע��ֲ�ʽ����
builder.Services.AddStackExchangeRedisCache(opt =>
{
	opt.Configuration = "43.143.170.48";
	opt.InstanceName = "shop_";
});

//ע��DBContext
builder.Services.AddDbContext<OrderDbContext>(opt =>
{
	opt.UseNpgsql(builder.Configuration.GetSection("ConnStr").Value);
});

//ע��Jwt����
var JwtConfig = builder.Configuration.GetSection("Jwt");
builder.Services.AddJwtAuthentication(JwtConfig.Get<JwtSetting>());

//ע��RabbitMQ
builder.Services.AddRabbitMQ();

//ע���Զ������
builder.Services.AddScoped<OrderDomainService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();

//ע�������
builder.Services.AddCors(opt =>
{
	opt.AddDefaultPolicy(bui =>
	{
		bui.WithOrigins(new string[] { "http://localhost:8080" })
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
