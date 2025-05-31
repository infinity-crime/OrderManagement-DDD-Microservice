using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.Services;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets("secrets.json");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OrderManagementDbContext>(options =>
{
    options.UseSqlServer(connectionString,
        sqloptions => sqloptions.MigrationsAssembly("OrderManagement.Infrastructure"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler("/Errors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
