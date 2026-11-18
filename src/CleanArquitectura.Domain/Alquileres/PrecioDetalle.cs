using CleanArquitectura.Domain.Shared;
using CleanArquitectura.Domain.Vehiculos;

namespace CleanArquitectura.Domain.Alquileres;

public record PrecioDetalle(
    Moneda PrecioPorPeriodo,
    Moneda Mantenimiento,
    Moneda Accesorios,
    Moneda PrecioTotal);