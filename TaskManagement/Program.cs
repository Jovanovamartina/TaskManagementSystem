using Application_TaskManagement.IRepositories;
using Application_TaskManagement.IServices;
using Application_TaskManagement.Services;
using Core_TaskManagement.Entities;
using Infrastructure_TaskManagement.Database;
using Infrastructure_TaskManagement.Repositories;
using Infrastructure_TaskManagement.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddTransient<IRoleSeeder, RoleSeeder>();



var app = builder.Build();

//var roleSeeder = app.Services.GetRequiredService<IRoleSeeder>();
//await roleSeeder.CreateRolesAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//using (var scope = app.Services.CreateScope())
//{
//    // Renaming the variable to avoid conflict
//    var roleSeederService = scope.ServiceProvider.GetRequiredService<IRoleSeeder>();

//    // Call CreateRolesAsync on the resolved instance
//    await roleSeederService.CreateRolesAsync(); // No arguments needed
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

