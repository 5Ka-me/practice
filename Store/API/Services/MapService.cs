using API.ViewModels;
using BLL.Models;

namespace API.Services
{
    internal static class MapService
    {
        internal static void Map(ProductModel productModel, ProductViewModel productViewModel)
        {
            productViewModel.Id = productModel.Id;
            productViewModel.Description = productModel.Description;
            productViewModel.Name = productModel.Name;
            productViewModel.Price = productModel.Price;
            productViewModel.IsOnSale = productModel.IsOnSale;
        }

        internal static void Map(ProductViewModel productViewModel, ProductModel productModel)
        {
            productModel.Id = productViewModel.Id;
            productModel.Description = productViewModel.Description;
            productModel.Name = productViewModel.Name;
            productModel.Price = productViewModel.Price;
            productModel.IsOnSale = productViewModel.IsOnSale;
        }

        internal static IEnumerable<ProductViewModel> Map(IEnumerable<ProductModel> models)
        {
            List<ProductViewModel> viewModels = new();

            foreach (var model in models)
            {
                ProductViewModel productViewModel = new();
                Map(model, productViewModel);
                viewModels.Add(productViewModel);
            }

            return viewModels;
        }
    }
}
