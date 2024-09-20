using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Data;
using Application.Mappings;
using Application.Interfaces;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Подключение строки подключения из appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Добавление DbContext с использованием Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Настройка аутентификации с JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
}); 

// 4. Добавление политики авторизации
builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

// 5. Добавление контроллеров
builder.Services.AddControllers();

// 6. Настройка Swagger для документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 7. Настройка CORS (если нужно)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// 8. Использование Swagger в среде разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 9. Использование глобальной обработки ошибок (middleware)
app.UseExceptionHandler("/error");

// 10. Включение CORS
app.UseCors("AllowAll");

// 11. Аутентификация и авторизация
app.UseAuthentication();
app.UseAuthorization();

// 12. Маршрутизация для контроллеров
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
