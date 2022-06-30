using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext storeContext)
        : base(storeContext)
        { }

        public async Task<Product> GetByNameAsync(string productName)
        {
            Product product = await _storeContext.Products.SingleOrDefaultAsync(x => x.Name == productName);

            return product;
        }
    }
}