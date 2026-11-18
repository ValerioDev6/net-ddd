using CleanArquitectura.Domain.Shared;
using CleanArquitectura.Domain.Vehiculos;

namespace CleanArquitectura.Domain.UnitTests.Vehiculos;

internal static class VehiculoMock
{
    public static Vehiculo Create(Moneda precio, Moneda? mantenimiento = null)
        => new(
            VehiculoId.New(),
            new Modelo("Civic"),
            new Vin("CHENTEHUAWEI"),
            new Direccion("Usa", "Texas", "Lorenzo", "El paso","Av. El Alamo"),
            precio,
            mantenimiento ?? Moneda.Zero(precio.TipoMoneda),
            [],
            DateTime.UtcNow.AddYears(-1)
            );
}