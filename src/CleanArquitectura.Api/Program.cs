using CleanArquitectura.Api.Extensions;
using CleanArquitectura.Api.Middleware;
using CleanArquitectura.Api.OptionSetup;
using CleanArquitectura.Application.Abstractions.Authenticacion;
using CleanArquitectura.Application.Extension;
using CleanArquitectura.Infrastructure.Authentication;
using CleanArquitectura.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddTransient<IJwtProvider, JwtProvider>();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddCleanArquitecturaInfrastructure(builder.Configuration);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigration();
app.SeedData();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomExceptionHandler();
app.UserRequestContextLogging();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();


public partial class Program;
