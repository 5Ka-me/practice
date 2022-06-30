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

        public ProductModel Update(ProductModel productModel)
        {
            _validator.ValidateAndThrow(productModel);

            if (_productRepository.GetById(productModel.Id) == null)
            {
                throw new ArgumentException("Product not found", nameof(productModel));
            }

            CheckCategoryExist(productModel.CategoryId);

            Product productTemp = _productRepository.GetByName(productModel.Name);
            if (productTemp != null && productTemp.Id != productModel.Id)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            Product product = _productRepository.GetById(productModel.Id);

            _mapper.Map(productModel, product);

            _productRepository.Update(product);

            _mapper.Map(product, productModel);

            return productModel;
        }

        public ProductModel Create(ProductModel productModel)
        {
            _validator.ValidateAndThrow(productModel);

            CheckCategoryExist(productModel.CategoryId);

            if (_productRepository.GetByName(productModel.Name) != null)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            Product product = new();

            _mapper.Map(productModel, product);

            _productRepository.Create(product);

            _mapper.Map(product, productModel);

            return productModel;
        }

        public void Delete(int id)
        {
            Product product = _productRepository.GetById(id);

            CheckNullProduct(product);

            _productRepository.Delete(product);
        }

        public IEnumerable<ProductModel> Get()
        {
            IEnumerable<Product> products = _productRepository.Get();
            IEnumerable<ProductModel> productModels = _mapper.Map<IEnumerable<ProductModel>>(products);

            return productModels;
        }

        public ProductModel Get(int id)
        {
            Product product = _productRepository.GetById(id);

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

        private void CheckCategoryExist(int categoryId)
        {
            if (_categoryRepository.GetById(categoryId) == null)
            {
                throw new ArgumentException("Category not found");
            }
        }
    }
}
