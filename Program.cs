using Microsoft.EntityFrameworkCore;
using NWRestfulAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NWRestfulAPI.Services.Interfaces;
using NWRestfulAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injektiolla v�litetty tietokantatieto kontrollereille
builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// ---------- CORS m��ritys ----------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// ---------- JWT Authentication ----------
var jwtKey = builder.Configuration["AppSettings:JwtSecret"];

if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT secret is missing in configuration!");
}

// Регистрируем AppSettings, чтобы IOptions<AppSettings> работал
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
// ------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ----- CORS k�ytt� ---------
app.UseCors("AllowAll");

// ----- JWT Authentication -----
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
