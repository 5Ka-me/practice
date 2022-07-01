using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected StoreContext _storeContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _dbSet = _storeContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {

            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Add(entity);
            await _storeContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
            await _storeContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Remove(entity);
            await _storeContext.SaveChangesAsync(cancellationToken);
        }
    }
}
