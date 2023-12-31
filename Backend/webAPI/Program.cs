using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using webAPI.Authentication.JwtBearer;
using webAPI.Authentication.JwtBearer.OptionsSetup;
using webAPI.Swagger;
using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Interfaces;
using webAPI.Middlewares;
using webAPI.Services;
using webAPI.Repositories;
using webAPI.Repository;
using Microsoft.OpenApi.Models;
using webAPI.Utils;
using webAPI.Interfaces.ActivityRecommendation;
using webAPI.Interfaces.ActivityRepository;
using webAPI.Interfaces.HealthData;
using webAPI.Interfaces.HealthRecommendation;
using webAPI.Interfaces.User;
using webAPI.Interfaces.Authentication;
using DotNetEnv.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Add default appsettings.json configuration file
    .AddEnvironmentVariables() // Add environment variables to IConfiguration
    .AddDotNetEnv(".env", LoadOptions.TraversePath()) // Simply add the DotNetEnv configuration source!
    .Build();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<webAPIDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fitness App API", Version = "v1" });
});
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddAuthentication(options =>
    {
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer();

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IActivityDataRepository, ActivityDataRepository>();
builder.Services.AddScoped<IActivityDataService, ActivityDataService>();
builder.Services.AddScoped<IHealthDataRepository, HealthDataRepository>();
builder.Services.AddScoped<IHealthDataService, HealthDataService>();

builder.Services.AddScoped<IActivityRecommendationRepository, ActivityRecommendationRepository>();
builder.Services.AddScoped<IActivityRecommendationService, ActivityRecommendationService>();
builder.Services.AddScoped<IHealthRecommendationRepository, HealthRecommendationRepository>();
builder.Services.AddScoped<IHealthRecommendationService, HealthRecommendationService>();

builder.Services.AddScoped<IGPTService, GPTService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<UserMiddleware>();

app.Run();
