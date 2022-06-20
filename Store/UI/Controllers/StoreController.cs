using Microsoft.AspNetCore.Mvc;
using BLL;
using BLL.Entities;
using BLL.Data;
using UI.Models;

namespace UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly ProductManager _productManager;

        public StoreController(ILogger<StoreController> logger)
        {
            _logger = logger;
            _productManager = new ProductManager();
        }

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            var products = _productManager.Get();
            var models = Transfer(products);
            
            return models;
        }
        
        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            var product = _productManager.Get(id);
            var productModel = Transfer(product);

            return productModel;
        }

        [HttpPost]
        public Product CreateProduct(ProductModel productModel)
        {
            var product = Transfer(productModel);

            _productManager.CreateProduct(product);

            return product;
        }

        [HttpPut]
        public ProductModel ChangeProduct(ProductModel model)
        {
            var product = Transfer(model);

            _productManager.ChangeProduct(product);
            
            return model;
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            _productManager.DeleteProduct(id);
        }

        private ProductModel Transfer(Product product)
        {
            ProductModel productModel =
                new ProductModel()
                {
                    ProductDescription = product.ProductDescription,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice
                };

            return productModel;
        }
        
        private IEnumerable<ProductModel> Transfer(IEnumerable<Product> products)
        {
            List<ProductModel> models = new List<ProductModel>();

            foreach (var product in products)
            {
                models.Add(Transfer(product));
            }

            return models;
        }

        private Product Transfer(ProductModel model)
        {
            Product product =
                new Product()
                {
                    ProductId = 0,
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    ProductPrice = model.ProductPrice
                };

            return product;
        }
        
        private IEnumerable<Product> Transfer(IEnumerable<ProductModel> models)
        {
            List<Product> products = new List<Product>();

            foreach (var model in models)
            {
                products.Add(Transfer(model));
            }

            return products;
        }
    }
}