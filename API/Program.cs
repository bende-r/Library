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
using Application.Services;
using Infrastructure;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();

builder.Services.ConfigureApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.ConfigureAPI(builder.Configuration);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options => options
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("X-Pagination")
    .AllowCredentials());



app.Run();