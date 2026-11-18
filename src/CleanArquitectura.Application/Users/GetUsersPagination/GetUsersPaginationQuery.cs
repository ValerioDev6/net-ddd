using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Application.Abstractions;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Shared;
using CleanArquitectura.Domain.Users;

namespace CleanArquitectura.Application.Users.GetUsersPagination
{
    public record GetUsersPaginationQuery : PaginationParams, IQuery<PagedResult<User, UserId>>
    {

    }
}
