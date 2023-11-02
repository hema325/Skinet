using Core.Entities;
using Core.Specifications.Abstractions;

namespace Core.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetBySpecAsync(ISpecification<TEntity> specification);
        Task<IReadOnlyList<TEntity>> GetListBySpecAsync(ISpecification<TEntity> specification);
        Task<int> CountBySpecAsync(ISpecification<TEntity> specification);
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
    }
}
