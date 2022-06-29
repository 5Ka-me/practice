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

        public IEnumerable<T> Get()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            T entity = _dbSet.Find(id);

            return entity;
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _storeContext.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _storeContext.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _storeContext.SaveChanges();
        }
    }
}
