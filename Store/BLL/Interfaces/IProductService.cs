using BLL.Models;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductModel> Get();
        ProductModel Get(int id);
        ProductModel Create(ProductModel productModel);
        ProductModel Change(ProductModel productModel);
        void DeleteProduct(int id);
    }
}
