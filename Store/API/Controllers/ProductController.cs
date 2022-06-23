using Microsoft.AspNetCore.Mvc;
using API.ViewModels;
using BLL.Interfaces;
using BLL.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<ProductViewModel> Get()
        {
            IEnumerable<ProductModel> models = _productService.Get();
            IEnumerable<ProductViewModel> viewModels = Map(models);

            return viewModels;
        }
        
        [HttpGet("{id}")]
        public ProductViewModel Get(int id)
        {
            ProductModel productModel = _productService.Get(id);
            ProductViewModel productViewModel = new();
            
            Map(productModel, productViewModel);
            
            return productViewModel;
        }

        [HttpPost]
        public ProductViewModel Create(ProductViewModel productViewModel)
        {
            ProductModel productModel = new ProductModel();
            
            Map(productViewModel, productModel);
            
            productModel = _productService.Create(productModel);

            Map(productModel, productViewModel);
            
            return productViewModel;
        }

        [HttpPut]
        public ProductViewModel Update(ProductViewModel productViewModel)
        {
            ProductModel productModel = new ProductModel();

            Map(productViewModel, productModel);
            
            _productService.Update(productModel);
            
            Map(productModel, productViewModel);
            
            return productViewModel;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }
        
        private void Map(ProductModel productModel, ProductViewModel productViewModel)
        {
            productViewModel.Id = productModel.Id;
            productViewModel.Description = productModel.Description;
            productViewModel.Name = productModel.Name;
            productViewModel.Price = productModel.Price;
            productViewModel.IsOnSale = productModel.IsOnSale;
        }
        
        private void Map(ProductViewModel productViewModel, ProductModel productModel)
        {
            productModel.Id = productViewModel.Id;
            productModel.Description = productViewModel.Description;
            productModel.Name = productViewModel.Name;
            productModel.Price = productViewModel.Price;
            productModel.IsOnSale = productViewModel.IsOnSale;
        }
        
        private IEnumerable<ProductViewModel> Map(IEnumerable<ProductModel> models)
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