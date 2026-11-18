using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Domain.Abstraccions;

namespace CleanArquitectura.Domain.Vehiculos.Specifications
{
    public class VehiculoPaginationCountingSpecification : BaseSpecification<Vehiculo, VehiculoId>
    {
        public VehiculoPaginationCountingSpecification(string search) : base(x => string.IsNullOrEmpty(search) || x.Modelo == new Modelo(search))
        {
        }
    }
}
