using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Data;
using Application.Mappings;
using Application.Interfaces;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. ����������� ������ ����������� �� appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. ���������� DbContext � �������������� Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. ��������� �������������� � JWT
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

// 4. ���������� �������� �����������
builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

// 5. ���������� ������������
builder.Services.AddControllers();

// 6. ��������� Swagger ��� ������������ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 7. ��������� CORS (���� �����)
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

// 8. ������������� Swagger � ����� ����������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 9. ������������� ���������� ��������� ������ (middleware)
app.UseExceptionHandler("/error");

// 10. ��������� CORS
app.UseCors("AllowAll");

// 11. �������������� � �����������
app.UseAuthentication();
app.UseAuthorization();

// 12. ������������� ��� ������������
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
