using CleanArquitectura.Application.Abstractions;
using CleanArquitectura.Application.Abstractions.Authenticacion;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Users;

namespace CleanArquitectura.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var email = new Email(request.Email.Trim().ToLowerInvariant());
        var existingUser = await _userRepository.GetByEmailAsync(email, cancellationToken);

        if (existingUser is not null)
        {
            return Result.Failure<string>(UserErrors.EmailAlreadyExists);
        }

        var passwordHash = new PasswordHash(BCrypt.Net.BCrypt.HashPassword(request.Password));
        var user = User.Create(
            new Nombre(request.Nombre.Trim()),
            new Apellidos(request.Apellidos.Trim()),
            email,
            passwordHash);
        var token = await _jwtProvider.Generate(user);

        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return token;
    }
}