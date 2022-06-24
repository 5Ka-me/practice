using BLL.Models;
using DAL.Entities;

namespace BLL.Services
{
    internal class MapService
    {
        internal static void Map(Product product, ProductModel productModel)
        {
            productModel.Id = product.Id;
            productModel.Description = product.Description;
            productModel.Name = product.Name;
            productModel.Price = product.Price;
            productModel.IsOnSale = product.IsOnSale;
        }

        internal static IEnumerable<ProductModel> Map(IEnumerable<Product> products)
        {
            List<ProductModel> models = new();

            foreach (var product in products)
            {
                ProductModel productModel = new();
                Map(product, productModel);
                models.Add(productModel);
            }

            return models;
        }

        internal static void Map(ProductModel model, Product product)
        {
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.IsOnSale = model.IsOnSale;
        }
    }
}
