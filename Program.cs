using CrudAPI.Models;
using CrudAPI.DTOs;
using CrudAPI.Repositories;
using AutoMapper;
// using AutoMapper.Extensions.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for structured logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<MappingProfiles>();
    });
var mapper = config.CreateMapper();

var serviceProvider = builder.Services.BuildServiceProvider();

// For Database Seeding
// using (var scope = serviceProvider.CreateScope())
// {
//     // Resolve the required services
//     var userRepository = scope.ServiceProvider.GetRequiredService<MyUserRepository>(serv);
//     var productRepository = scope.ServiceProvider.GetRequiredService<ProductRepository>();

//     // Pass the mapper instance to the repositories
//     // userRepository.ConfigureMapper(mapper);
//     // productRepository.ConfigureMapper(mapper);

//     // Seed data if needed
//     MyDataSeeder.SeedData(userRepository, productRepository);
// }


// Add services to the (dependency injection) container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.02",
        Title = "My ASP.NET Crud Test API",
        Description = "An ASP.NET Core Web API for testing Cruds \nIt's all about a simpla and sample api test on 'courses' and 'students'.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "My Contact",
            Url = new Uri("tel:+237671739026")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com/license")
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Configure the parameters for validating the JWT token
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Validate the issuer of the token
            ValidateIssuer = true,
            // Validate the audience (recipient) of the token
            ValidateAudience = true,
            // Validate the token's lifetime
            ValidateLifetime = true,
            // Validate the token's signature
            ValidateIssuerSigningKey = true,

            // Specify the valid issuer of the token
            ValidIssuer = "https://your-application.com",
            // Specify the valid audience (recipient) of the token
            ValidAudience = "your-api-resource",
            // Specify the symmetric signing key used for token validation
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("keyString"))
            // IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });
    
// // Add authorization policies
// builder.services.AddAuthorization();

// // Configure identity
// builder.services.AddIdentity<YourUserClass, YourRoleClass>(options =>
// {
//     // Configure password complexity rules and other settings
// })
// .AddEntityFrameworkStores<YourDbContext>()
// .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<CourseapiContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("constring");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
    options.UseMySql(connectionString, serverVersion);
});
builder.Services.AddDbContext<ProductDbContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("constring");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
    options.UseMySql(connectionString, serverVersion);
});

// Build application and creates an instance of WebApplication
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;     
        // Swagger UI at the app's root (https://localhost:<port>/) and not https://localhost:<port>/swagger
    });
} 
else 
{
    app.UseExceptionHandler();
}

// app.UseSerilogRequestLogging(); // Use Serilog for request logging

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();

app.UseAuthentication(); // Add this line for authentication
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
