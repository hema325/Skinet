using Core.Entities;
using Core.Interfaces.Repositories;
using System.Collections;

namespace Infrastructure.Data.Repositories
{
    internal class UnitOfWork: IUnitOfWork
    {
        private readonly Hashtable _repositories = new();
        private readonly StoreContext _context;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
        {
            var entityType = typeof(TEntity);

            if (!_repositories.ContainsKey(entityType))
                _repositories.Add(entityType, new GenericRepository<TEntity>(_context));

            return (IGenericRepository<TEntity>)_repositories[entityType]!;
        } 
    }
}
