using BLL.Models;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAsync();
        Task<ProductModel> GetAsync(int id);
        Task<ProductModel> CreateAsync(ProductModel productModel);
        Task<ProductModel> UpdateAsync(ProductModel productModel);
        Task DeleteAsync(int id);
    }
}
