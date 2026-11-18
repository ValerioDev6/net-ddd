using System.Drawing.Printing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;
using Xunit;


namespace CleanArquitectura.Application.IntegrationTests.Insfrastructure
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime

    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:16.0")
            .WithDatabase("")
            .WithUsername("postgres")
            .WithPassword("acasio2003")
            .Build();
        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {

            await _dbContainer.StopAsync();
        }



        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }
    }
}
