using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CleanArquitectura.Infrastructure.Extensions
{
    public static class PaginationExtension
    {

        public static IQueryable<T> OrderByPropertyOrField<T>(
            this IQueryable<T> queryable,
            string propertyOrFieldName,
            bool ascending = true
        )
        {
            var elementType = typeof(T);
            var orderByMethodName = ascending ? "OrderBy" : "OrderByDescending";

            var parameterExpression = Expression.Parameter(elementType);
            var propertyorFieldExpression = Expression.PropertyOrField(parameterExpression, propertyOrFieldName);

            var selector = Expression.Lambda(propertyorFieldExpression, parameterExpression);

            var orderByExpression = Expression.Call(
                typeof(Queryable),
                orderByMethodName,
                new[] { elementType, propertyorFieldExpression.Type },
                queryable.Expression,
                selector
            );

            return queryable.Provider.CreateQuery<T>(orderByExpression);

        }

    }
}
