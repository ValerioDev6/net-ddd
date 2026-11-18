using System.Text;
using CleanArquitectura.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanArquitectura.Api.OptionSetup;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptionsSetup;

    public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptionsSetup)
    {
        _jwtOptionsSetup = jwtOptionsSetup.Value;
    }

    public void Configure(JwtBearerOptions options)
    {
        ConfigureToken(options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        ConfigureToken(options);
    }

    public void ConfigureToken(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptionsSetup.Issuer,
            ValidAudience = _jwtOptionsSetup.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptionsSetup.SecretKey!))
        };
    }
}