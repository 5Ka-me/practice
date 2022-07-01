namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}
