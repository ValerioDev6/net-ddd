using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Users;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArquitectura.Application.Paginations
{
    public interface IPaginationRepository
    {

        Task<PagedResult<User, UserId>> GetPaginationAsync(
            Expression<Func<User, bool>>? predicate,
            Func<IQueryable<User>, IIncludableQueryable<User, object>> includes,
            int page,
            int pageSize,
            string orderBy,
            bool ascending,
            bool disableTraking = true
        );

    }
}
