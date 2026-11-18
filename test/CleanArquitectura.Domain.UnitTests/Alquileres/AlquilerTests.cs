using CleanArquitectura.Domain.Alquileres;
using CleanArquitectura.Domain.Alquileres.Events;
using CleanArquitectura.Domain.Shared;
using CleanArquitectura.Domain.UnitTests.Insfrastructure;
using CleanArquitectura.Domain.UnitTests.Users;
using CleanArquitectura.Domain.UnitTests.Vehiculos;
using CleanArquitectura.Domain.Users;
using CleanArquitectura.Domain.Vehiculos;
using FluentAssertions;
using Xunit;

namespace CleanArquitectura.Domain.UnitTests.Alquileres;

public class AlquilerTests : BaseTest
{
    
    [Fact]
    public void Reserva_Should_RaiseAlquilerReservaDomainEvent()
    {
        
        // Arrange
        var user = User.Create(
            UsersMocks.nombre,
            UsersMocks.apellido,
            UsersMocks.email,
            UsersMocks.password
        );
        
        var precio = new Moneda(10.0m, TipoMoneda.Usd);
        var duracion = DateRange.Create(
            new  DateOnly(2020, 01, 01),
            new  DateOnly(2020, 02, 02));
        // Act

        var vehiculo = VehiculoMock.Create(precio);
        var precioService = new PrecioService();
        
        // Assert 

        var alquiler = Alquiler.Reservar(
            vehiculo,
            user.Id!,
            duracion,
            DateTime.UtcNow,
            precioService
            );
        var alquilerReservaDomainEvent = AssertDomainEventWasPublished<AlquilerReservaDomainEvent>(alquiler);
        
        alquilerReservaDomainEvent.AlquilerId.Should().Be(alquiler.Id);

    }
}