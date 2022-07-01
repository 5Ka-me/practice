using BLL.Models;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAsync(CancellationToken cancellationToken);
        Task<ProductModel> GetAsync(int id, CancellationToken cancellationToken);
        Task<ProductModel> CreateAsync(ProductModel productModel, CancellationToken cancellationToken);
        Task<ProductModel> UpdateAsync(ProductModel productModel, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
