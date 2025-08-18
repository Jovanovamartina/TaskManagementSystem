
using Application_TaskManagement.IRepositories;
using Application_TaskManagement.IServices;
using Application_TaskManagement.Mapping;
using Application_TaskManagement.Services;
using Core_TaskManagement.Entities;
using Infrastructure_TaskManagement.Database;
using Infrastructure_TaskManagement.Repository;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddJwtAuthentication();
builder.Services.AddProblemDetails();
builder.Services.AddCustomCors();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repository DI
builder.Services.AddScoped<IRepository<News>, Repository<News>>();
//Services DI
builder.Services.AddScoped<INewsService, NewsService>();



// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));







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

