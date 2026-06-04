using AuthService.Configuration;
using AuthService.Data;
using AuthService.Features.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
);

// Register Services
builder.Services.AddScoped<IAuthService, AuthService.Features.Auth.Services.AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Configure JWT Settings environment variables
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
