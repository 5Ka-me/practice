namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> Get();
        T GetById(int id);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
