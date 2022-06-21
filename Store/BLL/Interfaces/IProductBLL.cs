using BLL.Models;

namespace BLL.Interfaces
{
    public interface IProductBLL
    {
        public IEnumerable<ProductModel> Get();
        public ProductModel Get(int id);
        public ProductModel CreateProduct(ProductModel productModel);
        public ProductModel ChangeProduct(ProductModel productModel);
        public void DeleteProduct(int id);
    }
}
