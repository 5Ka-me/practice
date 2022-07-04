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
        public async Task<IEnumerable<ProductViewModel>> Get(CancellationToken cancellationToken)
        {
            var models = await _productService.Get(cancellationToken);

            return _mapper.Map<IEnumerable<ProductViewModel>>(models);
        }

        [HttpGet("{id}")]
        public async Task<ProductViewModel> Get(int id, CancellationToken cancellationToken)
        {
            var productModel = await _productService.Get(id, cancellationToken);

            return _mapper.Map<ProductViewModel>(productModel);
        }

        [HttpPost]
        public async Task<ProductViewModel> Create(ProductViewModel productViewModel, CancellationToken cancellationToken)
        {
            var productModel = _mapper.Map<ProductModel>(productViewModel);

            productModel = await _productService.Create(productModel, cancellationToken);

            _mapper.Map(productModel, productViewModel);

            return productViewModel;
        }

        [HttpPut]
        public async Task<ProductViewModel> Update(ProductViewModel productViewModel, CancellationToken cancellationToken)
        {
            var productModel = _mapper.Map<ProductModel>(productViewModel);

            productModel = await _productService.Update(productModel, cancellationToken);

            return _mapper.Map(productModel, productViewModel);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _productService.Delete(id, cancellationToken);
        }
    }
}