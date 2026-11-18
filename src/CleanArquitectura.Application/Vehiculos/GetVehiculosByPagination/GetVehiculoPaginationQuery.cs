using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Application.Abstractions;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Shared;
using CleanArquitectura.Domain.Vehiculos;

namespace CleanArquitectura.Application.Vehiculos.GetVehiculosByPagination
{
    public class GetVehiculoPaginationQuery : SpecificationEntry, IQuery<PaginationResult<Vehiculo, VehiculoId>>
    {
        public string? Modelo { get; init; }
    }
}
