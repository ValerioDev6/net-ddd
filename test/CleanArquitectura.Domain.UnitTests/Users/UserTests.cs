using CleanArquitectura.Domain.Roles;
using CleanArquitectura.Domain.UnitTests.Insfrastructure;
using CleanArquitectura.Domain.Users;
using CleanArquitectura.Domain.Users.Events;
using FluentAssertions;
using Xunit;

namespace CleanArquitectura.Domain.UnitTests.Users
{

    public class UserTests : BaseTest
    {

        [Fact]
        public void Create_Should_SetPropertyValues()
        {
            // Arrange -> crear un mock file -> user Mock
            //Act

            var user = User.Create(
                UsersMocks.nombre,
                UsersMocks.apellido,
                UsersMocks.email,
                UsersMocks.password
            );

            // Assert
            user.Nombre.Should().Be(UsersMocks.nombre);
            user.Apellidos.Should().Be(UsersMocks.apellido);
            user.Email.Should().Be(UsersMocks.email);
            user.PasswordHash.Should().Be(UsersMocks.password);

        }


        [Fact]
        public void Create_Should_RaiseUserCreateeDomainEvent()
        {
            // Arrange -> crear un mock file -> user Mock
            //Act
            var user = User.Create(
                UsersMocks.nombre,
                UsersMocks.apellido,
                UsersMocks.email,
                UsersMocks.password
            );

            var domainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

            domainEvent?.UserId.Should().Be(user.Id);
        }
        
        [Fact]
        public void Create_Should_AddRegisterRoleToUser()
        {
            // Arrange -> crear un mock file -> user Mock
            //Act

            var user = User.Create(
                UsersMocks.nombre,
                UsersMocks.apellido,
                UsersMocks.email,
                UsersMocks.password
            );

            // Assert
            user.Roles.Should().Contain(Role.Cliente);

        }
    }
}
