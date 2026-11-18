using CleanArquitectura.Domain.Vehiculos;
using CleanArquitectura.Infrastructure.Context;

namespace CleanArquitectura.Infrastructure.Repository;

internal sealed class VehiculoRepository : Repository<Vehiculo,VehiculoId>, IVehiculoRepository
{
    public VehiculoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
