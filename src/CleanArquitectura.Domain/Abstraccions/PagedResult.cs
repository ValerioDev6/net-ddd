using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArquitectura.Domain.Abstraccions
{
    public interface PagedResult<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public List<TEntity> Results { get; set; }

    }
}
