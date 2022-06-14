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

            CheckNullProduct(product);

            return product;
        }

        [HttpPost]
        public Product CreateProduct(Product product)
        {
            CheckNullProduct(product);

            if (!ValidateProduct(product))
            {
                throw new ArgumentException("Product has incorrect data", nameof(product));
            }

            if (_storeContext.Products.Any(x => x.ProductName == product.ProductName))
            {
                throw new ArgumentException("A product with the same name already exists", nameof(product));
            }

            _storeContext.Products.Add(product);
            _storeContext.SaveChanges();

            return product;
        }

        [HttpPut]
        public Product ChangeProduct(Product product)
        {
            CheckNullProduct(product);

            if (!_storeContext.Products.Any(x => x.ProductId == product.ProductId))
            {
                throw new ArgumentException("Product does not exist", nameof(product));
            }

            if (!ValidateProduct(product))
            {
                throw new ArgumentException("Product has incorrect data", nameof(product));
            }

            _storeContext.Products.Update(product);
            _storeContext.SaveChanges();

            return product;
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            Product product = _storeContext.Products.FirstOrDefault(x => x.ProductId == id);

            CheckNullProduct(product);

            _storeContext.Products.Remove(product);
            _storeContext.SaveChanges();
        }

        private void CheckNullProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product does not exist");
            }
        }

        private bool ValidateProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.ProductName) || product.ProductName.Length > 30)
            {
                return false;
            }

            if (string.IsNullOrEmpty(product.ProductDescription) || product.ProductDescription.Length > 200)
            {
                return false;
            }

            if (!product.ProductName.All(char.IsLetterOrDigit))
            {
                return false;
            }

            if (product.ProductPrice <= 0)
            {
                return false;
            }

            return true;
        }
    }
}