using CrudAPI.Models;
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

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for structured logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

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



// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//                 .AddCookie(options =>
//                 {
//                     options.AccessDeniedPath = new PathString("/Account/AccessDenied");
//                     options.LoginPath = new PathString("/Account/Login");
//                 });

// // Configure authentication with JWT bearer tokens
// builder.services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = "your-issuer",
//             ValidAudience = "your-audience",
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
//         };
//     });
    
// // Add authorization policies
// builder.services.AddAuthorization();

// // Configure identity
// builder.services.AddIdentity<YourUserClass, YourRoleClass>(options =>
// {
//     // Configure password complexity rules and other settings
// })
// .AddEntityFrameworkStores<YourDbContext>()
// .AddDefaultTokenProviders();



builder.Services.AddDbContext<CourseapiContext>((serviceProvider, options) =>
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
