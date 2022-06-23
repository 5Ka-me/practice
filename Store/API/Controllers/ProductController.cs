using Microsoft.AspNetCore.Mvc;
using BLL;
using BLL.Models;
using BLL.Interfaces;

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
        public IEnumerable<ProductModel> Get()
        {
            IEnumerable<ProductModel> models = _productService.Get();

            return models;
        }
        
        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            ProductModel productModel = _productService.Get(id);

            return productModel;
        }

        [HttpPost]
        public ProductModel Create(ProductModel productModel)
        {
            _productService.Create(productModel);

            return productModel;
        }

        [HttpPut]
        public ProductModel Update(ProductModel productModel)
        {
            _productService.Update(productModel);
            
            return productModel;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }
    }
}