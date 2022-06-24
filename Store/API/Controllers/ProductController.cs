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
        public IEnumerable<ProductViewModel> Get()
        {
            IEnumerable<ProductModel> models = _productService.Get();
            IEnumerable<ProductViewModel> viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(models);

            return viewModels;
        }

        [HttpGet("{id}")]
        public ProductViewModel Get(int id)
        {
            ProductModel productModel = _productService.Get(id);
            ProductViewModel productViewModel = new();

            _mapper.Map(productModel, productViewModel);

            return productViewModel;
        }

        [HttpPost]
        public ProductViewModel Create(ProductViewModel productViewModel)
        {
            ProductModel productModel = new ProductModel();

            _mapper.Map(productViewModel, productModel);

            productModel = _productService.Create(productModel);

            _mapper.Map(productModel, productViewModel);

            return productViewModel;
        }

        [HttpPut]
        public ProductViewModel Update(ProductViewModel productViewModel)
        {
            ProductModel productModel = new ProductModel();

            _mapper.Map(productViewModel, productModel);

            _productService.Update(productModel);

            _mapper.Map(productModel, productViewModel);

            return productViewModel;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }
    }
}