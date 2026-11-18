using CleanArquitectura.Application.Abstractions;

namespace CleanArquitectura.Application.Users.LoginUser;

public record LoginCommand(string Email, string Password) : ICommand<string>;