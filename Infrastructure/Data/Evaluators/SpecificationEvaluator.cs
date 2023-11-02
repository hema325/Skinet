using Core.Entities;
using Core.Specifications.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Evaluators
{
    internal static class SpecificationEvaluator<TEntity> where TEntity: EntityBase
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            query = specification.Criteria == null ? query:query.Where(specification.Criteria);
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            query = specification.OrderBy == null ? query: query.OrderBy(specification.OrderBy);
            query = specification.OrderByDescending == null ? query : query.OrderByDescending(specification.OrderByDescending);
            query = !specification.IsPagingEnabled ? query : query.Skip(specification.Skip).Take(specification.Take);

            return query;
        }
    }
}
