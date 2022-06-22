using Microsoft.AspNetCore.Mvc;
using BLL;
using BLL.Models;
using BLL.Interfaces;

namespace UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly IProductBLL _productBLL;

        public StoreController(ILogger<StoreController> logger, IProductBLL productBLL)
        {
            _logger = logger;
            _productBLL = productBLL;
        }

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            IEnumerable<ProductModel> models = _productBLL.Get();

            return models;
        }
        
        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            ProductModel productModel = _productBLL.Get(id);

            return productModel;
        }

        [HttpPost]
        public ProductModel CreateProduct(ProductModel productModel)
        {
            _productBLL.CreateProduct(productModel);

            return productModel;
        }

        [HttpPut]
        public ProductModel ChangeProduct(ProductModel productModel)
        {
            _productBLL.ChangeProduct(productModel);
            
            return productModel;
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            _productBLL.DeleteProduct(id);
        }
    }
}