using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Students_Management_Api;
using Students_Management_Api.Models;
using Students_Management_Api.Repositories;
using Students_Management_Api.Services.StudentServices;
using Students_Management_Api.Services.SupervisorServices;
using Students_Management_Api.Services.TeacherServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;


services.AddEndpointsApiExplorer();
services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

services.AddScoped<IRepository<Student>, Repository<Student>>();
services.AddScoped<IStudentService, StudentService>();
services.AddScoped<IRepository<Teacher>, Repository<Teacher>>();
services.AddScoped<ISupervisorService, SupervisorService>();
services.AddScoped<IRepository<Supervisor>, Repository<Supervisor>>();
services.AddScoped<ITeacherService, TeacherService>();

services.AddAutoMapper(typeof(Program).Assembly);

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    })
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = "186763096110-n53tnjdnusqq9eoe7ukcnnjmu4n02680.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });

services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
/*services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.IsEssential = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.None;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        });*/

services.AddAuthorization();

services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

services.AddDbContext<LibraryContext>(x => x.UseSqlServer(configuration["ConnectionStrings:sql"]));
services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<LibraryContext>()
        .AddDefaultTokenProviders();
services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name; // Change the default username claim type
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier; // Change the default user ID claim type
    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role; // Change the default role claim type
});

services.AddStackExchangeRedisCache(options => { options.Configuration = configuration["RedisCacheUrl"]; });


// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.UseCors("AllowAnyOrigin");

app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<GetIdRole>();

app.MapControllers();

app.Run();
