using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetByNameAsync(string productName);
    }
}
