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

        [HttpGet(Name ="JustGetSmth")]
        public IEnumerable<User> GetUser()
        {

            return storeContext.Users.ToArray();
        }
    }
}