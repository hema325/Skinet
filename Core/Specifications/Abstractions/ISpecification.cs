using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications.Abstractions
{
    public interface ISpecification<TEntity> where TEntity : EntityBase
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        IReadOnlyList<Expression<Func<TEntity, object>>> Includes { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
        bool IsPagingEnabled { get; }
        int Skip { get; }
        int Take { get; }
    }
}
