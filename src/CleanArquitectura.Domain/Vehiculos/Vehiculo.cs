using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Shared;

namespace CleanArquitectura.Domain.Vehiculos;

public sealed class Vehiculo : Entity<VehiculoId>
{
    public Vehiculo(
        VehiculoId id,
        Modelo? modelo,
        Vin? vin,
        Direccion? direccion,
        Moneda? precio,
        Moneda? mantenimiento,
        List<Accesorio> accesorios,
        DateTime? fechaUltimoAlquilere) : base(id)
    {
        Modelo = modelo;
        Vin = vin;
        Direccion = direccion;
        Precio = precio;
        Mantenimiento = mantenimiento;
        Accesorios = accesorios;
        FechaUltimoAlquilere = fechaUltimoAlquilere;
    }

    public Modelo? Modelo { get; private set; }

    public Vin? Vin { get; private set; }

    public Direccion? Direccion { get; set; }

    // public string? Calle { get; private set; }
    // public string? Deṕartamento { get; private set; }
    // public string? Provincia { get; private set; }
    // public string? Ciudad { get; private set; }
    // public string? Pais { get; private set; }

    public Moneda? Precio { get; private set; }

    public Moneda? Mantenimiento { get; private set; }

    public DateTime? FechaUltimoAlquilere { get; internal set; }

    public List<Accesorio> Accesorios { get; private set; } = new();
}
