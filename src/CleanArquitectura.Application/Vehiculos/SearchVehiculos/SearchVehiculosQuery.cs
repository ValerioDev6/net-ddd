using CleanArquitectura.Application.Abstractions;

namespace CleanArquitectura.Application.Vehiculos.SearchVehiculos;

public record SearchVehiculosQuery(
    DateOnly fechaInicio,
    DateOnly fechaFin
    ): IQuery<IReadOnlyList<SearchVehiculoResponse>>;