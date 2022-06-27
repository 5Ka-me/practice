using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Category Get(int id);
    }
}
