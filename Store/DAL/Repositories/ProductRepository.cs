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

        public async Task<Product> GetByName(string productName, CancellationToken cancellationToken)
        {
            return await _storeContext.Products.SingleOrDefaultAsync(x => x.Name == productName, cancellationToken);
        }
    }
}