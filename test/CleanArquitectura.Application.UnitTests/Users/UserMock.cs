using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Domain.Users;

namespace CleanArquitectura.Application.UnitTests.Users
{
    public class UserMock
    {
        public static readonly Domain.Users.Nombre nombre = new Domain.Users.Nombre("Alfonso");

        public static readonly Apellidos apellido = new Apellidos("Quispe Alarcon");
        public static readonly Email email = new Email("vquispealarcon@gmail.com");
        public static readonly PasswordHash password = new PasswordHash("acasio2003");

        public static User Create() => User.Create(
            nombre,
            apellido,
            email,
            password
        );
    }
}
