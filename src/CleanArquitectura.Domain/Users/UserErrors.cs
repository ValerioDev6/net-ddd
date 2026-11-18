using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "No existe el usuario buscado por este id"
    );

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Las credenciales son incorrectas");

    public static Error EmailAlreadyExists = new(
        "User.EmailAlreadyExists",
        "Ya existe un usuario registrado con ese correo electrónico");
}