using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private StoreContext storeContext;

        public StoreController(ILogger<StoreController> logger, StoreContext storeContext)
        {
            this.storeContext = storeContext;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return storeContext.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            Product product = storeContext.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Product>> CreateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            storeContext.Products.Add(product);
            storeContext.SaveChanges();

            return Get();
        }

        [HttpPut]
        public ActionResult<IEnumerable<Product>> ChangeProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (!storeContext.Products.Any(x => x.ProductId == product.ProductId))
            {
                return NotFound();
            }

            storeContext.Products.Update(product);
            storeContext.SaveChanges();

            return Get();
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Product>> DeleteProduct(int id)
        {
            Product product = storeContext.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            storeContext.Products.Remove(product);
            storeContext.SaveChanges();

            return Get();
        }
    }
}