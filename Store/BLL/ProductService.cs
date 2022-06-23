using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
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

            Map(productModel, product);

            _repository.Update(product);

            Map(product, productModel);

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

            Map(productModel, product);

            _repository.Create(product);

            Map(product, productModel);

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
            IEnumerable<ProductModel> productModels;

            productModels = Map(_repository.Get().ToList());

            return productModels;
        }

        public ProductModel Get(int id)
        {
            Product product = _repository.GetById(id);

            CheckNullProduct(product);

            ProductModel productModel = new();

            Map(product, productModel);

            return productModel;
        }

        private void Map(Product product, ProductModel productModel)
        {
            productModel.Id = product.Id;
            productModel.Description = product.Description;
            productModel.Name = product.Name;
            productModel.Price = product.Price;
            productModel.IsOnSale = product.IsOnSale;
        }

        private IEnumerable<ProductModel> Map(IEnumerable<Product> products)
        {
            List<ProductModel> models = new();

            foreach (var product in products)
            {
                ProductModel productModel = new();
                Map(product, productModel);
                models.Add(productModel);
            }

            return models;
        }

        private void Map(ProductModel model, Product product)
        {
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.IsOnSale = model.IsOnSale;
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
