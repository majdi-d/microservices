using microservices.customersvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using StackExchange.Redis;
using System.Configuration;
using System.Runtime.Intrinsics.X86;

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
var redis_host = Environment.GetEnvironmentVariable("REDIS_HOST");
var redis_port = Environment.GetEnvironmentVariable("REDIS_PORT");
var redis_password = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
var connectionString = $"Server={dbHost},1433;Database={dbName};User Id={dbUserName};Password={dbPassword};TrustServerCertificate=true;";

// end dependency injection
//var redisConfiguration = new ConfigurationOptions
//{
//    EndPoints = { "redis:6379" },
//    Password = "eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81",
//    AbortOnConnectFail = false,
//    Ssl = false
//};
//var redis = ConnectionMultiplexer.Connect(redisConfiguration);
//builder.Services.BuildServiceProvider();
builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(connectionString))
                .AddStackExchangeRedisCache(action =>
                {
                    action.InstanceName = "MyRedisCache";
                    action.Configuration = $"{redis_host}:{redis_port}";
                    action.ConfigurationOptions = new ConfigurationOptions
                    {
                        EndPoints = { $"{redis_host}:{redis_port}" },
                        Password = redis_password,
                        AbortOnConnectFail = false,
                        Ssl = false
                    };
                });


//var cache = serviceProvider.GetRequiredService<IDistributedCache>();
// Dependency Injection


// End Dependency injection

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
