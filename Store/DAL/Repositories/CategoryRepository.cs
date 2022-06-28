using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreContext storeContext)
        : base(storeContext)
        { }
    }
}
