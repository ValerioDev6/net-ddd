using CleanArquitectura.Application.Abstractions.Clock;
using CleanArquitectura.Application.Abstractions.Email;
using CleanArquitectura.Application.Data;
using CleanArquitectura.Application.Paginations;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Alquileres;
using CleanArquitectura.Domain.Users;
using CleanArquitectura.Domain.Vehiculos;
using CleanArquitectura.Infrastructure.Context;
using CleanArquitectura.Infrastructure.Data;
using CleanArquitectura.Infrastructure.Email;
using CleanArquitectura.Infrastructure.Outbox;
using CleanArquitectura.Infrastructure.Repository;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CleanArquitectura.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCleanArquitecturaInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        services.ConfigureOptions<ProccessOutboxMessageSetup>();

        services.AddTransient<IDateTimeProvider, IDateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVehiculoRepository, VehiculoRepository>();
        services.AddScoped<IAlquilerRepository, AlquilerRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPaginationRepository, UserRepository>();


        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        return services;
    }
}
