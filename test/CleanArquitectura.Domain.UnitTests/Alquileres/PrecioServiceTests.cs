using CleanArquitectura.Domain.Alquileres;
using CleanArquitectura.Domain.Shared;
using CleanArquitectura.Domain.UnitTests.Vehiculos;
using CleanArquitectura.Domain.Vehiculos;
using FluentAssertions;
using Xunit;

namespace CleanArquitectura.Domain.UnitTests.Alquileres;

public class PrecioServiceTests
{
    
    [Fact]
    public void CalcularPrecio_Should_ReturnCorrectResult()
    {
        // Arrange    
        var price = new Moneda(10.0m, TipoMoneda.Usd );
        var periodo = DateRange.Create(
            new DateOnly(2024,1,1),
            new DateOnly(2025,1,1)
            );

        var expecetedPrecioTotal = new Moneda( 
            price.Monto* periodo.CantidadDias,
            price.TipoMoneda
            );

        var vehiculo = VehiculoMock.Create(price);
        var precioService = new PrecioService();
        // Act
        
        var precioDetalle = precioService.CalcularPrecio(vehiculo, periodo);

        // Assert
        precioDetalle.PrecioTotal.Should().Be(expecetedPrecioTotal);

    }
} 