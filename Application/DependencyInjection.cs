using System.Reflection;
using Application.Behavior;
using Application.Interfaces;
using Application.Services;
using Application.UseCases.AuthorsUseCases.AddAuthor;
using Application.UseCases.BooksUseCases.AddBook;

using FluentValidation;
using FluentValidation.AspNetCore;

using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {

             services.AddScoped<ITokenService, TokenService>();

        // Регистрация MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Регистрация AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
    
}