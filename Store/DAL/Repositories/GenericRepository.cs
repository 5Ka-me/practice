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

        public async Task<IEnumerable<T>> Get()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Create(T entity)
        {
            _dbSet.Add(entity);
            await _storeContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _dbSet.Update(entity);
            await _storeContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _storeContext.SaveChangesAsync();
        }
    }
}
