using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;

        public StoreController(ILogger<StoreController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{productId}")]
        public Product GetProduct()
        {
            return new Product();
        }

        [HttpGet("{receiptId}")]
        public Receipt GetReceipt()
        {
            return new Receipt();
        }

        [HttpGet(Name ="GetProductsOnStorage")]
        public IEnumerable<(Product, int)> Get()
        {
            Storage.Products.Add((new Product() { ProductId = 1, ProductName="product1"}, 2));
            var products = Storage.Products;
            return products;
        }
    }
}