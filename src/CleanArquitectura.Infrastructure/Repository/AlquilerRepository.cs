using CleanArquitectura.Domain.Alquileres;
using CleanArquitectura.Domain.Vehiculos;
using CleanArquitectura.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitectura.Infrastructure.Repository;

internal sealed class AlquilerRepository : Repository<Alquiler, AlquilerId>, IAlquilerRepository
{
    private static readonly AlquilerStatus[] ActiveAlquilerStatuses = {
        AlquilerStatus.Reservado,
        AlquilerStatus.Confirmado,
        AlquilerStatus.Completado
    };
    
    public AlquilerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<bool> IsOverlappingAsync(
        Vehiculo vehiculo, 
        DateRange duracion, 
        CancellationToken cancellationToken = default)
    {

        return await DbContext.Set<Alquiler>()
            .AnyAsync(
                alquiler => 
                    alquiler.VehiculoId == vehiculo.Id &&
                    alquiler.Duracion!.Inicio <= duracion.Fin &&
                    alquiler.Duracion.Fin >= duracion.Inicio &&
                    ActiveAlquilerStatuses.Contains(alquiler.Status),
                cancellationToken
            );
    }
}