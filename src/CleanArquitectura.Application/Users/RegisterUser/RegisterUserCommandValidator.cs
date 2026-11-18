using System.Text;
using FluentValidation;

namespace CleanArquitectura.Application.Users.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Nombre)
            .NotEmpty()
            .Must(nombre => !string.IsNullOrWhiteSpace(nombre))
            .MaximumLength(200);

        RuleFor(command => command.Apellidos)
            .NotEmpty()
            .Must(apellidos => !string.IsNullOrWhiteSpace(apellidos))
            .MaximumLength(200);

        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(400);

        RuleFor(command => command.Password)
            .NotEmpty()
            .Must(password => !string.IsNullOrWhiteSpace(password))
            .MinimumLength(8)
            .Must(password => password is not null && Encoding.UTF8.GetByteCount(password) <= 72)
            .WithMessage("La contraseña no puede superar 72 bytes.");
    }
}