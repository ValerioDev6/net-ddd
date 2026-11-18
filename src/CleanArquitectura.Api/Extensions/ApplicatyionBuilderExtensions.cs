using CleanArquitectura.Api.Middleware;
using CleanArquitectura.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitectura.Api.Extensions;

public static class ApplicatyionBuilderExtensions
{
    public static async void ApplyMigration(this IApplicationBuilder app)
    {


        using (var scope = app.ApplicationServices.CreateAsyncScope())
        {
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = service.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(e, "An error occurred while migrating the database.");
            }

        }

    }
    public static IApplicationBuilder UserRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequstContextLoggingMiddleware>();
        return app;
    }



}
