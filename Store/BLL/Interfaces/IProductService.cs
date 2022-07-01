using BLL.Models;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> Get();
        Task<ProductModel> Get(int id);
        Task<ProductModel> Create(ProductModel productModel);
        Task<ProductModel> Update(ProductModel productModel);
        Task Delete(int id);
    }
}
