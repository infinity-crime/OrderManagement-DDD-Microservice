using Microsoft.EntityFrameworkCore;

using OrderManagement.API.Common.Extensions;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.Services;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets("secrets.json");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Since the API project is runnable and migrations need to be placed in the infrastructure layer
builder.Services.AddDbContext<OrderManagementDbContext>(options =>
{
    options
    .UseSqlServer(connectionString,
        sqloptions => sqloptions.MigrationsAssembly("OrderManagement.Infrastructure"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.ConfigureBehaviorOptions(); // Proprietary extension method 

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/Errors");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
