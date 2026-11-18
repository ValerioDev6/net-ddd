
using CleanArquitectura.Domain.Permissions;
using CleanArquitectura.Domain.Users;

namespace CleanArquitectura.Domain.UnitTests.Users
{
    public class UsersMocks
    {
        public static readonly Domain.Users.Nombre nombre = new Domain.Users.Nombre("Alfonso");

        public static readonly Apellidos apellido = new Apellidos("Quispe Alarcon");
        public static readonly Email email = new Email("vquispealarcon@gmail.com");
        public static readonly PasswordHash password = new PasswordHash("acasio2003");

    }
}
