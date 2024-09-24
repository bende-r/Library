using System.Reflection;
using Application.Behavior;
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

        //     services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        // Регистрация MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllAuthorsQueryHandler).Assembly));

        // Регистрация AutoMapper
        services.AddAutoMapper(typeof(GetAllAuthorsQueryHandler).Assembly);

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
    
}