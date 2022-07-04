using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetByName(string productName, CancellationToken cancellationToken);
    }
}
