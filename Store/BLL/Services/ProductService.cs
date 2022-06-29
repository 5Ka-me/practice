using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validators;
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

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public ProductModel Update(ProductModel productModel)
        {
            ProductValidator productValidator = new(_categoryRepository);
            productValidator.ValidateAndThrow(productModel);

            if (_productRepository.GetById(productModel.Id) == null)
            {
                throw new ArgumentException("Product not found", nameof(productModel));
            }

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
            ProductValidator productValidator = new(_categoryRepository);
            productValidator.ValidateAndThrow(productModel);

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
    }
}
