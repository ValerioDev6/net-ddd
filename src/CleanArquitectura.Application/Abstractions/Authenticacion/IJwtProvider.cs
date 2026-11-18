using CleanArquitectura.Domain.Users;

namespace CleanArquitectura.Application.Abstractions.Authenticacion;

public interface IJwtProvider
{
    Task<string> Generate(User user);
}