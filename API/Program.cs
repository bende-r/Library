using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Data;
using Application.Mappings;
using Application.Interfaces;
using FluentValidation;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using API;
using Domain.Entities;
using Application.UseCases.BooksUseCases.AddBook;
using Microsoft.OpenApi.Models;
using MediatR;
using System.Reflection;
using Application.Behavior;
using Application;

var builder = WebApplication.CreateBuilder(args);

// 1. Подключение строки подключения из appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Добавление DbContext с использованием Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Регистрация Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()  // Замените на ваш DbContext
    .AddDefaultTokenProviders();

////// 3. Настройка аутентификации с JWT
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//          .AddJwtBearer(options =>
//          {
//              options.TokenValidationParameters = new TokenValidationParameters
//              {
//                  ValidateIssuer = true,
//                  ValidateAudience = true,
//                  ValidateLifetime = true,
//                  ValidateIssuerSigningKey = true,
//                  ValidIssuer = builder.Configuration["Jwt:Issuer"],
//                  ValidAudience = builder.Configuration["Jwt:Audience"],
//                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//              };
//          });

//// 4. Добавление политики авторизации
//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication();

builder.Services.ConfigureApplicationServices();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 5. Добавление контроллеров
builder.Services.AddControllers();

//// 6. Настройка Swagger для документации API
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

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

//// 8. Использование Swagger в среде разработки
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// 10. Использование Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1"));
}

// 9. Использование глобальной обработки ошибок (middleware)
app.UseExceptionHandler("/error");



//app.UseHttpsRedirection();
//app.UseStaticFiles();

// Добавляем поддержку маршрутизации
app.UseRouting();  // Должно быть ДО UseEndpoints
// Добавляем аутентификацию (если используется)
app.UseAuthentication();

// Добавляем авторизацию
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");



// 12. Маршрутизация для контроллеров
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
