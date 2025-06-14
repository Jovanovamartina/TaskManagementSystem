using Application_TaskManagement.Common;
using Application_TaskManagement.IRepositories;
using Application_TaskManagement.IServices;
using Application_TaskManagement.Services;
using Application_TaskManagement.Token;
using Core_TaskManagement.Entities;
using Core_TaskManagement.JWT;
using Infrastructure_TaskManagement.Database;
using Infrastructure_TaskManagement.Repositories;
using Infrastructure_TaskManagement.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskManagement.Extensions;
using TaskManagement.Utilities;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddJwtAuthentication();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCustomCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddTransient<IRoleSeeder, RoleSeeder>();

//Logging
LoggingConfiguration.ConfigureLogging(builder.Configuration);
builder.Host.UseSerilog();


// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

//Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();

app.Run();

