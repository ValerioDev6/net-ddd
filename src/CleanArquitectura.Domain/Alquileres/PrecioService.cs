using CleanArquitectura.Domain.Shared;
using CleanArquitectura.Domain.Vehiculos;

namespace CleanArquitectura.Domain.Alquileres;

public class PrecioService
{
    public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
    {
        var titpoMoneda = vehiculo.Precio!.TipoMoneda;
        var precioPorPeriodo = new Moneda(
            periodo.CantidadDias * vehiculo.Precio.Monto,
            titpoMoneda
            );
        decimal porcentageChange = 0;

        foreach (var accesorio in vehiculo.Accesorios)
        {
            porcentageChange += accesorio switch
            {
                Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
                Accesorio.AireAcondicionado => 0.01m,
                Accesorio.Mapas => 0.01m,
                _ => 0
            };
        }

        var accesoriosCharges = Moneda.Zero(titpoMoneda);

        if (porcentageChange  > 0)
        {
            accesoriosCharges = new Moneda(
                precioPorPeriodo.Monto *  porcentageChange,
                titpoMoneda
                );
        };

        var precioTotal = Moneda.Zero(titpoMoneda);
        precioTotal += precioPorPeriodo;

        if (!vehiculo!.Mantenimiento!.IsZero())
        {
            precioTotal += vehiculo.Mantenimiento;
        }
        
        precioTotal += accesoriosCharges;

        return new PrecioDetalle(
            precioPorPeriodo,
            vehiculo.Mantenimiento,
            accesoriosCharges,
            precioTotal
            );
    }
}