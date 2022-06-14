using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly StoreContext _storeContext;

        public StoreController(ILogger<StoreController> logger, StoreContext storeContext)
        {
            _storeContext = storeContext;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _storeContext.Products.ToList();
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product product = _storeContext.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null)
            {
                throw new ArgumentException("Product does not exist", nameof(id));
            }

            return product;
        }

        [HttpPost]
        public Product CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }

            _storeContext.Products.Add(product);
            _storeContext.SaveChanges();

            return product;
        }

        [HttpPut]
        public Product ChangeProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }

            if (!_storeContext.Products.Any(x => x.ProductId == product.ProductId))
            {
                throw new ArgumentException("Product does not exist", nameof(product));
            }

            _storeContext.Products.Update(product);
            _storeContext.SaveChanges();

            return product;
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            Product product = _storeContext.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(id), "Product does not exist");
            }

            _storeContext.Products.Remove(product);
            _storeContext.SaveChanges();
        }
    }
}