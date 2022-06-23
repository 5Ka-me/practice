using BLL.Models;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductModel> Get();
        ProductModel Get(int id);
        ProductModel Create(ProductModel productModel);
        ProductModel Update(ProductModel productModel);
        void Delete(int id);
    }
}
