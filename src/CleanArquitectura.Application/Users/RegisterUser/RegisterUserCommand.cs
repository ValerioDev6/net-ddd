using CleanArquitectura.Application.Abstractions;

namespace CleanArquitectura.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Nombre,
    string Apellidos,
    string Email,
    string Password) : ICommand<string>;