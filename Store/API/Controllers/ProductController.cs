using API.ViewModels;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger, IProductService productService, IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> Get()
        {
            var models = await _productService.Get();

            return _mapper.Map<IEnumerable<ProductViewModel>>(models);
        }

        [HttpGet("{id}")]
        public async Task<ProductViewModel> Get(int id)
        {
            var productModel = await _productService.Get(id);

            return _mapper.Map<ProductViewModel>(productModel);
        }

        [HttpPost]
        public async Task<ProductViewModel> Create(ProductViewModel productViewModel)
        {
            var productModel = _mapper.Map<ProductModel>(productViewModel);

            productModel = await _productService.Create(productModel);

            return _mapper.Map<ProductViewModel>(productModel);
        }

        [HttpPut]
        public async Task<ProductViewModel> Update(ProductViewModel productViewModel)
        {
            var productModel = _mapper.Map<ProductModel>(productViewModel);

            productModel = await _productService.Update(productModel);

            return _mapper.Map(productModel, productViewModel);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _productService.Delete(id);
        }
    }
}