using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;
        Task<int> SaveChangesAsync();
    }
}
