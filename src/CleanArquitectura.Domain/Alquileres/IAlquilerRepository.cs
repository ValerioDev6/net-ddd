using CleanArquitectura.Domain.Vehiculos;

namespace CleanArquitectura.Domain.Alquileres;

public interface IAlquilerRepository
{

    Task<Alquiler?> GetByIdAsync(AlquilerId id, CancellationToken cancellationToken=default);

    Task<bool> IsOverlappingAsync(
        Vehiculo vehiculo,
        DateRange duracion,
        CancellationToken cancellationToken = default
    );

    void Add(Alquiler alquiler);

}