using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using FluentValidation;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductModel> _validator;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper, IValidator<ProductModel> validator)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ProductModel> Update(ProductModel productModel)
        {
            await _validator.ValidateAndThrowAsync(productModel);

            if (await _productRepository.GetById(productModel.Id) == null)
            {
                throw new ArgumentException("Product not found", nameof(productModel));
            }

            await CheckCategoryExist(productModel.CategoryId);

            var productTemp = await _productRepository.GetByName(productModel.Name);
            if (productTemp != null && productTemp.Id != productModel.Id)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            var product = await _productRepository.GetById(productModel.Id);

            _mapper.Map(productModel, product);

            await _productRepository.Update(product);

            return _mapper.Map(product, productModel);
        }

        public async Task<ProductModel> Create(ProductModel productModel)
        {
            await _validator.ValidateAndThrowAsync(productModel);

            await CheckCategoryExist(productModel.CategoryId);

            if (await _productRepository.GetByName(productModel.Name) != null)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            var product = _mapper.Map<Product>(productModel);

            await _productRepository.Create(product);

            return _mapper.Map(product, productModel);
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.GetById(id);

            CheckNullProduct(product);

            await _productRepository.Delete(product);
        }

        public async Task<IEnumerable<ProductModel>> Get()
        {
            var products = await _productRepository.Get();

            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public async Task<ProductModel> Get(int id)
        {
            var product = await _productRepository.GetById(id);

            CheckNullProduct(product);

            return _mapper.Map<ProductModel>(product);
        }

        private static void CheckNullProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product does not exist");
            }
        }

        private async Task CheckCategoryExist(int categoryId)
        {
            if (await _categoryRepository.GetById(categoryId) == null)
            {
                throw new ArgumentException("Category not found");
            }
        }
    }
}
