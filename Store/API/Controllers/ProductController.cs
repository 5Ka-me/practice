using API.Services;
using API.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

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
            IEnumerable<ProductViewModel> viewModels = MapService.Map(models);

            return viewModels;
        }

        [HttpGet("{id}")]
        public ProductViewModel Get(int id)
        {
            ProductModel productModel = _productService.Get(id);
            ProductViewModel productViewModel = new();

            MapService.Map(productModel, productViewModel);

            return productViewModel;
        }

        [HttpPost]
        public ProductViewModel Create(ProductViewModel productViewModel)
        {
            ProductModel productModel = new ProductModel();

            MapService.Map(productViewModel, productModel);

            productModel = _productService.Create(productModel);

            MapService.Map(productModel, productViewModel);

            return productViewModel;
        }

        [HttpPut]
        public ProductViewModel Update(ProductViewModel productViewModel)
        {
            ProductModel productModel = new ProductModel();

            MapService.Map(productViewModel, productModel);

            _productService.Update(productModel);

            MapService.Map(productModel, productViewModel);

            return productViewModel;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }
    }
}