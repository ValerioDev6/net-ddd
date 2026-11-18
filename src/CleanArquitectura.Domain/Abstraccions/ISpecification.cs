using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CleanArquitectura.Domain.Abstraccions
{
    public interface ISpecification<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
    {
        Expression<Func<TEntity, bool>>? Creterio { get; }
        List<Expression<Func<TEntity, object>>>? Includes { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnable { get; }
    }
}
