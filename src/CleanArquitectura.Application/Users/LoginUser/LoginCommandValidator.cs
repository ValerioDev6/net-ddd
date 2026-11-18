using FluentValidation;

namespace CleanArquitectura.Application.Users.LoginUser;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(400);

        RuleFor(command => command.Password)
            .NotEmpty();
    }
}