using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get();
        Product GetById(int id);
        Product Create(Product product);
        Product Update(Product product);
        void Delete(Product product);
        Product GetByName(string productName);
    }
}
