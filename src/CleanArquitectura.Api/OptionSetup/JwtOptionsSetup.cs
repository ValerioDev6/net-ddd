using System.Text;
using CleanArquitectura.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace CleanArquitectura.Api.OptionSetup;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "JwtSettings";
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);

        if (string.IsNullOrWhiteSpace(options.Issuer) ||
            string.IsNullOrWhiteSpace(options.Audience) ||
            string.IsNullOrWhiteSpace(options.SecretKey))
        {
            throw new InvalidOperationException(
                "Configure JwtSettings:Issuer, JwtSettings:Audience y JwtSettings:SecretKey mediante configuración segura.");
        }

        if (Encoding.UTF8.GetByteCount(options.SecretKey) < 32)
        {
            throw new InvalidOperationException("JwtSettings:SecretKey debe tener al menos 32 bytes.");
        }
    }
}