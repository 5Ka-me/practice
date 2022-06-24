using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public Product Update(Product product)
        {
            _storeContext.Products.Update(product);
            _storeContext.SaveChanges();

            return product;
        }

        public Product Create(Product product)
        {
            _storeContext.Products.Add(product);
            _storeContext.SaveChanges();

            return product;
        }

        public void Delete(Product product)
        {
            _storeContext.Products.Remove(product);
            _storeContext.SaveChanges();
        }

        public IEnumerable<Product> Get()
        {
            return _storeContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            Product product = _storeContext.Products.SingleOrDefault(x => x.Id == id);

            return product;
        }

        public Product GetByName(string productName)
        {
            Product product = _storeContext.Products.SingleOrDefault(x => x.Name == productName);

            return product;
        }
    }
}