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

        public async Task<ProductModel> UpdateAsync(ProductModel productModel)
        {
            await _validator.ValidateAndThrowAsync(productModel);

            if (await _productRepository.GetByIdAsync(productModel.Id) == null)
            {
                throw new ArgumentException("Product not found", nameof(productModel));
            }

            await CheckCategoryExistAsync(productModel.CategoryId);

            var productTemp = await _productRepository.GetByNameAsync(productModel.Name);
            if (productTemp != null && productTemp.Id != productModel.Id)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            var product = await _productRepository.GetByIdAsync(productModel.Id);

            _mapper.Map(productModel, product);

            await _productRepository.UpdateAsync(product);

            _mapper.Map(product, productModel);

            return productModel;
        }

        public async Task<ProductModel> CreateAsync(ProductModel productModel)
        {
            await _validator.ValidateAndThrowAsync(productModel);

            await CheckCategoryExistAsync(productModel.CategoryId);

            if (await _productRepository.GetByNameAsync(productModel.Name) != null)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            Product product = new();

            _mapper.Map(productModel, product);

            await _productRepository.CreateAsync(product);

            _mapper.Map(product, productModel);

            return productModel;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            CheckNullProduct(product);

            _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductModel>> GetAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetAsync();
            IEnumerable<ProductModel> productModels = _mapper.Map<IEnumerable<ProductModel>>(products);

            return productModels;
        }

        public async Task<ProductModel> GetAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            CheckNullProduct(product);

            ProductModel productModel = new();

            _mapper.Map(product, productModel);

            return productModel;
        }

        private void CheckNullProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product does not exist");
            }
        }

        private async Task CheckCategoryExistAsync(int categoryId)
        {
            if (await _categoryRepository.GetByIdAsync(categoryId) == null)
            {
                throw new ArgumentException("Category not found");
            }
        }
    }
}
