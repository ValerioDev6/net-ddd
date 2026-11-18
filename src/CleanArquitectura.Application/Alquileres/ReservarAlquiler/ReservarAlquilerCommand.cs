using CleanArquitectura.Application.Abstractions;

namespace CleanArquitectura.Application.Alquileres.ReservarAlquiler;

public record ReservarAlquilerCommand(
    Guid VehiculoId,
    Guid UserId,
    DateOnly FechaInicio,
    DateOnly FechaFin
    ) : ICommand<Guid>;