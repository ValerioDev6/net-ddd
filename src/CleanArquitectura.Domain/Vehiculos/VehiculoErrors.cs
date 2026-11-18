using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Vehiculos;

public static class VehiculoErrors
{
    public static Error NotFound = new(
        "Vehiculo.NotFound",
        "Vehiculo not found");
}