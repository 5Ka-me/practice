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

        public async Task<ProductModel> UpdateAsync(ProductModel productModel, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(productModel, cancellationToken);

            if (await _productRepository.GetByIdAsync(productModel.Id, cancellationToken) == null)
            {
                throw new ArgumentException("Product not found", nameof(productModel));
            }

            await CheckCategoryExistAsync(productModel.CategoryId, cancellationToken);

            var productTemp = await _productRepository.GetByNameAsync(productModel.Name, cancellationToken);
            if (productTemp != null && productTemp.Id != productModel.Id)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            var product = await _productRepository.GetByIdAsync(productModel.Id, cancellationToken);

            _mapper.Map(productModel, product);

            await _productRepository.UpdateAsync(product, cancellationToken);

            _mapper.Map(product, productModel);

            return productModel;
        }

        public async Task<ProductModel> CreateAsync(ProductModel productModel, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(productModel, cancellationToken);

            await CheckCategoryExistAsync(productModel.CategoryId, cancellationToken);

            if (await _productRepository.GetByNameAsync(productModel.Name, cancellationToken) != null)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            Product product = new();

            _mapper.Map(productModel, product);

            await _productRepository.CreateAsync(product, cancellationToken);

            _mapper.Map(product, productModel);

            return productModel;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);

            CheckNullProduct(product);

            await _productRepository.DeleteAsync(product, cancellationToken);
        }

        public async Task<IEnumerable<ProductModel>> GetAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await _productRepository.GetAsync(cancellationToken);
            IEnumerable<ProductModel> productModels = _mapper.Map<IEnumerable<ProductModel>>(products);

            return productModels;
        }

        public async Task<ProductModel> GetAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);

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

        private async Task CheckCategoryExistAsync(int categoryId, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.GetByIdAsync(categoryId, cancellationToken) == null)
            {
                throw new ArgumentException("Category not found");
            }
        }
    }
}
