namespace CleanArquitectura.Api.Controllers.Users;

public sealed record RegisterUserRequest(
    string Nombre,
    string Apellidos,
    string Email,
    string Password);