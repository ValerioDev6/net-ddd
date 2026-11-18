using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Application.Abstractions;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Shared;
using CleanArquitectura.Domain.Vehiculos;
using CleanArquitectura.Domain.Vehiculos.Specifications;

namespace CleanArquitectura.Application.Vehiculos.GetVehiculosByPagination
{
    public class GetVehiculoPaginationQueryHandler : IQueryHandler<GetVehiculoPaginationQuery, PaginationResult<Vehiculo, VehiculoId>>
    {
        private readonly IVehiculoRepository _vehiculosRepository;

        public GetVehiculoPaginationQueryHandler(IVehiculoRepository vehiculosRepository)
        {
            _vehiculosRepository = vehiculosRepository;
        }

        public async Task<Result<PaginationResult<Vehiculo, VehiculoId>>> Handle(GetVehiculoPaginationQuery request, CancellationToken cancellationToken)
        {
            var spec = new VehiculoPaginationSpecification(
                request.Sort!,
                request.PageIndex,
                request.PageSize,
                request.Modelo!
            );

            var records = await _vehiculosRepository.GetAllWithSpec(spec);

            var specCount = new VehiculoPaginationCountingSpecification(request.Modelo!);

            var totalRecords = await _vehiculosRepository.CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalRecords) / Convert.ToDecimal(request.PageSize));

            var totalPages = Convert.ToInt32(rounded);
            var recordsByPage = records.Count();

            return new PaginationResult<Vehiculo, VehiculoId>
            {
                Count = totalRecords,
                Data = records.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = recordsByPage
            };
        }
    }
}
