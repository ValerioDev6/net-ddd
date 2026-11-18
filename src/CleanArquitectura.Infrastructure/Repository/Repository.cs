using System.Linq.Expressions;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Infrastructure.Context;
using CleanArquitectura.Infrastructure.Extensions;
using CleanArquitectura.Infrastructure.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArquitectura.Infrastructure.Repository;

internal abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(
        TEntityId id,
        CancellationToken cancellationToken = default
    )
    {
        return await DbContext.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Add(TEntity entity)
    {
        DbContext.Add(entity);
    }

    public IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TEntityId> spec)
    {
        return SpecificationEvaluator<TEntity, TEntityId>.GetQuery(DbContext.Set<TEntity>().AsQueryable(), spec);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllWithSpec(ISpecification<TEntity, TEntityId> spec)
    {
        return await ApplySpecification(spec).ToListAsync();

    }

    public async Task<int> CountAsync(ISpecification<TEntity, TEntityId> spec)
    {

        return await ApplySpecification(spec).CountAsync();
    }


    public async Task<PagedResult<TEntity, TEntityId>> GetPaginationAsync(
        Expression<Func<TEntity, bool>>? predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true
    )
    {
        IQueryable<TEntity> queryable = DbContext.Set<TEntity>();

        if (disableTracking) queryable = queryable.AsNoTracking();

        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }

        if (includes is not null)
        {
            queryable = includes(queryable);
        }

        var skipAmount = pageSize * (page - 1);
        var totalNumberOfRecords = await queryable.CountAsync();

        var records = new List<TEntity>();

        if (string.IsNullOrEmpty(orderBy))
        {
            records = await queryable.Skip(skipAmount).Take(pageSize).ToListAsync();
        }
        else
        {
            records = await queryable
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();
        }

        var mod = totalNumberOfRecords % pageSize;

        var totalPagesCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

        return new PagedResult<TEntity, TEntityId>{
            records,
            page,
            pageSize,
            totalPagesCount,
            totalNumberOfRecords
        };
    }

}
