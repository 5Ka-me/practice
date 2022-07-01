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
        public async Task<IEnumerable<ProductViewModel>> GetAsync()
        {
            IEnumerable<ProductModel> models = await _productService.GetAsync();

            return _mapper.Map<IEnumerable<ProductViewModel>>(models);
        }

        [HttpGet("{id}")]
        public async Task<ProductViewModel> GetAsync(int id)
        {
            ProductModel productModel = await _productService.GetAsync(id);

            return _mapper.Map<ProductViewModel>(productModel);
        }

        [HttpPost]
        public async Task<ProductViewModel> CreateAsync(ProductViewModel productViewModel)
        {
            ProductModel productModel = new();

            _mapper.Map(productViewModel, productModel);

            productModel = await _productService.CreateAsync(productModel);

            _mapper.Map(productModel, productViewModel);

            return productViewModel;
        }

        [HttpPut]
        public async Task<ProductViewModel> UpdateAsync(ProductViewModel productViewModel)
        {
            ProductModel productModel = new();

            _mapper.Map(productViewModel, productModel);

            await _productService.UpdateAsync(productModel);

            _mapper.Map(productModel, productViewModel);

            return productViewModel;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _productService.DeleteAsync(id);
        }
    }
}