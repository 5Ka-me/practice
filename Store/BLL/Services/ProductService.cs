using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ProductModel Update(ProductModel productModel)
        {
            if (productModel == null)
            {
                throw new ArgumentNullException(nameof(productModel), "Product does not exist");
            }

            if (!ValidateProduct(productModel))
            {
                throw new ArgumentException("Product has incorrect data", nameof(productModel));
            }

            if (_repository.GetById(productModel.Id) == null)
            {
                throw new ArgumentException("Product does not exist", nameof(productModel));
            }

            Product productTemp = _repository.GetByName(productModel.Name);
            if (productTemp != null && productTemp.Id != productModel.Id)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            Product product = _repository.GetById(productModel.Id);

            _mapper.Map(productModel, product);

            _repository.Update(product);

            _mapper.Map(product, productModel);

            return productModel;
        }

        public ProductModel Create(ProductModel productModel)
        {
            if (!ValidateProduct(productModel))
            {
                throw new ArgumentException("Product has incorrect data", nameof(productModel));
            }

            if (_repository.GetByName(productModel.Name) != null)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.Price < 50;

            Product product = new();

            _mapper.Map(productModel, product);

            _repository.Create(product);

            _mapper.Map(product, productModel);

            return productModel;
        }

        public void Delete(int id)
        {
            Product product = _repository.GetById(id);

            CheckNullProduct(product);

            _repository.Delete(product);
        }

        public IEnumerable<ProductModel> Get()
        {
            IEnumerable<Product> products = _repository.Get();
            IEnumerable<ProductModel> productModels = _mapper.Map<IEnumerable<ProductModel>>(products);

            return productModels;
        }

        public ProductModel Get(int id)
        {
            Product product = _repository.GetById(id);

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

        private bool ValidateProduct(ProductModel productModel)
        {
            if (string.IsNullOrWhiteSpace(productModel.Name) || productModel.Name.Length > 30)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(productModel.Description) || productModel.Description.Length > 200)
            {
                return false;
            }

            if (!productModel.Name.All(char.IsLetterOrDigit))
            {
                return false;
            }

            if (productModel.Price <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
