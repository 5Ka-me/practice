using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepository
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProductById(int id);
        public Product CreateProduct(Product product);
        public Product UpdateProduct(Product product);
        public void DeleteProduct(Product product);
        public Product GetProductByName(string productName);
    }
}
