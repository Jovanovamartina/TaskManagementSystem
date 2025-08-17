
using Infrastructure_TaskManagement.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskManagement.Extensions;
using TaskManagement.Utilities;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddJwtAuthentication();
builder.Services.AddProblemDetails();
builder.Services.AddCustomCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Logging
LoggingConfiguration.ConfigureLogging(builder.Configuration);
builder.Host.UseSerilog();


// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));



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

