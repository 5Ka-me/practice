using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreContext _storeContext;

        public CategoryRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public Category GetById(int id)
        {
            Category category = _storeContext.Categories.SingleOrDefault(x => x.Id == id);

            return category;
        }
    }
}
