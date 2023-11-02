using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Specifications.Abstractions;
using Infrastructure.Data.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    internal class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity : EntityBase
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
            => _context.Add(entity);

        public void Update(TEntity entity)
            => _context.Update(entity);

        public void Delete(TEntity entity)
            => _context.Remove(entity);

        public async Task<TEntity?> GetByIdAsync(int id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetBySpecAsync(ISpecification<TEntity> specification)
            => await ApplySpecification(specification).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<TEntity>> GetListBySpecAsync(ISpecification<TEntity> specification)
            => await ApplySpecification(specification).ToListAsync();

        public async Task<int> CountBySpecAsync(ISpecification<TEntity> specification) 
            => await ApplySpecification(specification).CountAsync();

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
            => SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
    }
}
