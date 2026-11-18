using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Application.Abstractions;
using CleanArquitectura.Application.Paginations;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Users;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitectura.Application.Users.GetUsersPagination

{
    public class GetUsersPaginationQueryHandler : IQueryHandler<GetUsersPaginationQuery, PagedResult<User, UserId>>
    {

        private readonly IPaginationRepository _paginationRepository;

        public GetUsersPaginationQueryHandler(IPaginationRepository paginationRepository)
        {
            _paginationRepository = paginationRepository;
        }

        public async Task<Result<PagedResult<User, UserId>>> Handle(GetUsersPaginationQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.New<User>(true);
            if (!string.IsNullOrEmpty(request.Search))
            {
                predicate = predicate.Or(p => p.Nombre == new Nombre(request.Search));
                predicate = predicate.Or(p => p.Email == new Email(request.Search));
            }

            var pagedResultUsuarios = await _paginationRepository.GetPaginationAsync(predicate,
                p => p.Include(x => x.Roles!).ThenInclude(y => y.Permissions!),
                request.PageNumber,
                request.PageSize,
                request.OrderBy!,
                request.OrderAsc
            );

            return (Result<PagedResult<User, UserId>>)pagedResultUsuarios;

        }
    }
}
