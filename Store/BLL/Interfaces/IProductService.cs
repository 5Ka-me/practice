using BLL.Models;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> Get(CancellationToken cancellationToken);
        Task<ProductModel> Get(int id, CancellationToken cancellationToken);
        Task<ProductModel> Create(ProductModel productModel, CancellationToken cancellationToken);
        Task<ProductModel> Update(ProductModel productModel, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
