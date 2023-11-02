using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications.Abstractions
{
    public abstract class SpecificationBase<TEntity> : ISpecification<TEntity> where TEntity : EntityBase
    {
        public SpecificationBase() { }

        public SpecificationBase(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }
        public List<Expression<Func<TEntity, object>>> _includes { get; private set; } = new();
        public IReadOnlyList<Expression<Func<TEntity, object>>> Includes => _includes;
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            _includes.Add(include);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }

        protected void ApplyPaging(int skip, int take)
        {
            IsPagingEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
