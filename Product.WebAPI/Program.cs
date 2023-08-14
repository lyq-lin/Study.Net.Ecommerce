using Microsoft.EntityFrameworkCore;
using Product.Domain;
using Product.Infrastructure;
using Product.Infrastructure.dbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDbContext>(opt =>
{
	opt.UseNpgsql(builder.Configuration.GetSection("ConnStr").Value);
});

//ע��ִ���
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


//���ÿ���
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

app.UseAuthorization();

app.MapControllers();

app.Run();

