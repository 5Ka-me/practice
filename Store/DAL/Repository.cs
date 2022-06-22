using DAL.Entities;
using DAL.Interfaces;
using DAL.Data;

namespace DAL
{
    public class Repository : IRepository
    {
        private readonly StoreContext _storeContext;

        public Repository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public Product UpdateProduct(Product product)
        {
            _storeContext.Products.Update(product);
            _storeContext.SaveChanges();

            return product;
        }

        public Product CreateProduct(Product product)
        {
            _storeContext.Products.Add(product);
            _storeContext.SaveChanges();

            return product;
        }

        public void DeleteProduct(Product product)
        {
            _storeContext.Products.Remove(product);
            _storeContext.SaveChanges();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _storeContext.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            Product product = _storeContext.Products.FirstOrDefault(x => x.ProductId == id);

            return product;
        }

        public Product GetProductByName(string productName)
        {
            Product product;

            product = _storeContext.Products.FirstOrDefault(x => x.ProductName == productName);

            return product;
        }
    }
}