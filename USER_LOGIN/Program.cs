using Microsoft.AspNetCore.Builder;
using USER_LOGIN.Repository;
using USER_LOGIN.Services;
using USER_LOGIN.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<DataAccess>();
// Add controllers
builder.Services.AddControllers();

// Register repository and service dependencies
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure Swagger middleware in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Login API V1");
    });
}

app.MapControllers();

app.Run();