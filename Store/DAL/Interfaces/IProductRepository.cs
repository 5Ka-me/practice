using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product GetByName(string productName);
    }
}
