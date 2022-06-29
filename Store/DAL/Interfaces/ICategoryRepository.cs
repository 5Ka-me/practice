using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetById(int id);
    }
}
