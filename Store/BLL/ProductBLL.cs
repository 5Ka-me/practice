using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL
{
    public class ProductBLL : IProductBLL
    {
        private readonly IProductDAL _productDAL;

        public ProductBLL(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public ProductModel ChangeProduct(ProductModel productModel)
        {
            if (productModel == null)
            {
                throw new ArgumentNullException(nameof(productModel), "Product does not exist");
            }

            if (!ValidateProduct(productModel))
            {
                throw new ArgumentException("Product has incorrect data", nameof(productModel));
            }

            if (_productDAL.GetProductById(productModel.ProductId) == null)
            {
                throw new ArgumentException("Product does not exist", nameof(productModel));
            }

            Product productTemp = _productDAL.GetProductByName(productModel.ProductName);
            if (productTemp != null && productTemp.ProductId != productModel.ProductId)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.ProductPrice < 50;

            Product product = _productDAL.GetProductById(productModel.ProductId);

            Map(productModel, product);

            _productDAL.UpdateProduct(product);

            Map(product, productModel);

            return productModel;
        }

        public ProductModel CreateProduct(ProductModel productModel)
        {
            if (!ValidateProduct(productModel))
            {
                throw new ArgumentException("Product has incorrect data", nameof(productModel));
            }

            if (_productDAL.GetProductByName(productModel.ProductName) != null)
            {
                throw new ArgumentException("A product with the same name already exists", nameof(productModel));
            }

            productModel.IsOnSale = productModel.ProductPrice < 50;

            Product product = new();

            Map(productModel, product);

            _productDAL.CreateProduct(product);

            Map(product, productModel);

            return productModel;
        }

        public void DeleteProduct(int id)
        {
            Product product = _productDAL.GetProductById(id);

            CheckNullProduct(product);

            _productDAL.DeleteProduct(product);
        }

        public IEnumerable<ProductModel> Get()
        {
            IEnumerable<ProductModel> productModels;

            productModels = Map(_productDAL.GetProducts().ToList());

            return productModels;
        }

        public ProductModel Get(int id)
        {
            Product product = _productDAL.GetProductById(id);

            CheckNullProduct(product);

            ProductModel productModel = new();
            
            Map(product, productModel);

            return productModel;
        }

        private void Map(Product product, ProductModel productModel)
        {
            productModel.ProductId = product.ProductId;
            productModel.ProductDescription = product.ProductDescription;
            productModel.ProductName = product.ProductName;
            productModel.ProductPrice = product.ProductPrice;
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
            product.ProductName = model.ProductName;
            product.ProductDescription = model.ProductDescription;
            product.ProductPrice = model.ProductPrice;
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
            if (string.IsNullOrWhiteSpace(productModel.ProductName) || productModel.ProductName.Length > 30)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(productModel.ProductDescription) || productModel.ProductDescription.Length > 200)
            {
                return false;
            }

            if (!productModel.ProductName.All(char.IsLetterOrDigit))
            {
                return false;
            }

            if (productModel.ProductPrice <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
