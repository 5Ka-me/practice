using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext storeContext)
            : base(storeContext)
        { }

        public Product GetByName(string productName)
        {
            Product product = _storeContext.Products.SingleOrDefault(x => x.Name == productName);

            return product;
        }
    }
}