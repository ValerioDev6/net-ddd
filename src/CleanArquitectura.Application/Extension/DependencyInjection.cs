using CleanArquitectura.Application.Abstractions.Behaviours;
using CleanArquitectura.Domain.Alquileres;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArquitectura.Application.Extension;

public static class DependencyInjection
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(LogginBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            
        });

        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddTransient<PrecioService>();
    
        return services;
    }
    
}