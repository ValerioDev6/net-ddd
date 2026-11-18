using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArquitectura.Domain.Abstraccions
{
    public class PaginationResult<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }

        public IReadOnlyList<TEntity>? Data { get; set; }
        public int PageCount { get; set; }
        public int ResultByPage { get; set; }
        public int PageSize { get; set; }
    }
}
