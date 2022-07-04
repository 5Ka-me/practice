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

        public async Task<ProductModel> Update(ProductModel productModel, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(productModel, cancellationToken);

            if (await _productRepository.GetById(productModel.Id, cancellationToken) == null)
            {
                throw new ArgumentException("Product not found", nameof(productModel));
            }

            await CheckCategoryExist(productModel.CategoryId, cancellationToken);

            var productTemp = await _productRepository.GetByName(productModel.Name, cancellationToken);
            if (productTemp != null && productTemp.Id != productModel.Id)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            var product = await _productRepository.GetById(productModel.Id, cancellationToken);

            _mapper.Map(productModel, product);

            await _productRepository.Update(product, cancellationToken);

            return _mapper.Map(product, productModel);
        }

        public async Task<ProductModel> Create(ProductModel productModel, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(productModel, cancellationToken);

            await CheckCategoryExist(productModel.CategoryId, cancellationToken);

            if (await _productRepository.GetByName(productModel.Name, cancellationToken) != null)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            var product = _mapper.Map<Product>(productModel);

            await _productRepository.Create(product, cancellationToken);

            return _mapper.Map(product, productModel);
        }
        
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(id, cancellationToken);

            CheckNullProduct(product);

            await _productRepository.Delete(product, cancellationToken);
        }

        public async Task<IEnumerable<ProductModel>> Get(CancellationToken cancellationToken)
        {
            var products = await _productRepository.Get(cancellationToken);

            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public async Task<ProductModel> Get(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(id, cancellationToken);

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

        private async Task CheckCategoryExist(int categoryId, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.GetById(categoryId, cancellationToken) == null)
            {
                throw new ArgumentException("Category not found");
            }
        }
    }
}
