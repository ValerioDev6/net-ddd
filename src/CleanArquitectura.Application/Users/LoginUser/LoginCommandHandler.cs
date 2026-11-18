using CleanArquitectura.Application.Abstractions;
using CleanArquitectura.Application.Abstractions.Authenticacion;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Users;

namespace CleanArquitectura.Application.Users.LoginUser;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvier)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvier;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = new Email(request.Email.Trim().ToLowerInvariant());
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (user is null)
        {
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }

        if (user.PasswordHash is null ||
            !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash.Value))
        {
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }

        var token = await _jwtProvider.Generate(user);
        return token;
    }
}