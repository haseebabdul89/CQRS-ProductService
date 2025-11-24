using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Handlers;
using Product.Application.Interfaces;
using Product.Infra.Persistance.Reposetories;
using Product.Infra.Persistence;
using Product.Infra.RabbitMq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configuration & logging
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR - scan assembly for handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpdateProductInventoryHandler>());


// RabbitMQ Consumer
builder.Services.AddSingleton<Product.Application.Interfaces.IRabbitMqConsumer, Product.Infra.RabbitMq.RabbitMqConsumer>();

// Register the repository interface → concrete class
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();
// Start consuming events
var consumer = app.Services.GetRequiredService<IRabbitMqConsumer>();
consumer.StartConsuming();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.MapControllers();
app.Run();


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
