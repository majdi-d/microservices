using microservices.productsvc;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUserName = "sa";
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var connectionString = $"Server={dbHost},1433;Database={dbName};User Id={dbUserName};Password={dbPassword};TrustServerCertificate=true;";
builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseSqlServer(connectionString));
// end dependency injection

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseHttpMetrics();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics("/metrics-text");
    endpoints.MapControllers();
}
);

app.Run();
