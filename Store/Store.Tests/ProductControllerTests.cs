using API.Controllers;
using API.Profiles;
using API.ViewModels;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Moq;
using Xunit;

namespace Store.Tests
{
    public class ProductControllerTests
    {
        private readonly MapperConfiguration mapperConfiguration =
            new(cfg => cfg.AddProfile<ProductProfile>());
        private readonly Mock<IProductService> _service = new();
        private readonly Mock<CancellationTokenSource> _cancellationTokenSource = new();

        [Fact]
        public void GetAllProducts()
        {
            //Arrange
            _service.Setup(x => x.Get(_cancellationTokenSource.Object.Token).Result).Returns(GetListOfProducts());

            var mapper = mapperConfiguration.CreateMapper();
            var controller = new ProductController(null, _service.Object, mapper);
            const int expected = 4;

            //Act
            var result = controller.Get(_cancellationTokenSource.Object.Token).Result;

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected, result.Count());
        }

        private static IEnumerable<ProductModel> GetListOfProducts()
        {
            var list = new List<ProductModel>()
            {
                new() { Id = 1, Name = "Name 1", CategoryId = 1, Description = "Description 1", IsOnSale = true, Price = 167},
                new() { Id = 2, Name = "Name 2", CategoryId = 1, Description = "Description 2", IsOnSale = false, Price = 1123 },
                new() { Id = 3, Name = "Name 3", CategoryId = 1, Description = "Description 3", IsOnSale = false, Price = 4 },
                new() { Id = 4, Name = "Name 4", CategoryId = 1, Description = "Description 4", IsOnSale = true, Price = 14 }
            };

            return list;
        }

        [Fact]
        public void GetProduct()
        {
            //Arrange
            _service.Setup(x => x.Get(2, _cancellationTokenSource.Object.Token).Result).Returns(GetListOfProducts().ElementAt(1));

            var mapper = mapperConfiguration.CreateMapper();
            var controller = new ProductController(null, _service.Object, mapper);
            var expected = GetViewModel();

            //Act
            var result = controller.Get(2, _cancellationTokenSource.Object.Token).Result;

            //Assert
            Assert.NotNull(result);
            Assert.True(IsEqual(expected, result));
        }

        private static bool IsEqual(ProductViewModel firstModel, ProductViewModel secondModel)
        {
            if (firstModel.Id != secondModel.Id)
            {
                return false;
            }
            if (firstModel.CategoryId != secondModel.CategoryId)
            {
                return false;
            }
            if (firstModel.Name != secondModel.Name)
            {
                return false;
            }
            if (firstModel.Description != secondModel.Description)
            {
                return false;
            }
            if (firstModel.Price != secondModel.Price)
            {
                return false;
            }

            return true;
        }

        private static ProductViewModel GetViewModel()
        {
            var viewModel = new ProductViewModel()
            {
                Id = 2,
                Name = "Name 2",
                CategoryId = 1,
                Description = "Description 2",
                IsOnSale = false,
                Price = 1123
            };

            return viewModel;
        }

        private static ProductModel GetModel()
        {
            var model = new ProductModel()
            {
                Id = 2,
                Name = "Name 2",
                CategoryId = 1,
                Description = "Description 2",
                IsOnSale = false,
                Price = 1123
            };

            return model;
        }

        [Fact]
        public void CreateProduct()
        {
            //Arrange
            _service.Setup(x => x.Create(GetModel(), _cancellationTokenSource.Object.Token).Result).Returns(GetModel());

            var mapper = mapperConfiguration.CreateMapper();
            var controller = new ProductController(null, _service.Object, mapper);
            var viewModel = GetViewModel();

            //Act
            var result = controller.Create(viewModel, _cancellationTokenSource.Object.Token).Result;

            //Assert
            Assert.NotNull(result);
            Assert.True(IsEqual(viewModel, result));
        }

        [Fact]
        public void UpdateProduct()
        {
            //Arrange
            _service.Setup(x => x.Update(GetModel(), _cancellationTokenSource.Object.Token).Result).Returns(GetModel());

            var mapper = mapperConfiguration.CreateMapper();
            var controller = new ProductController(null, _service.Object, mapper);
            var expected = GetViewModel();

            //Act
            var result = controller.Update(GetViewModel(), _cancellationTokenSource.Object.Token).Result;

            //Assert
            Assert.NotNull(result);
            Assert.True(IsEqual(expected, result));
        }

        [Fact]
        public void DeleteProduct()
        {
            //Arrange
            _service.Setup(x => x.Delete(3, _cancellationTokenSource.Object.Token));

            var mapper = mapperConfiguration.CreateMapper();
            var controller = new ProductController(null, _service.Object, mapper);

            //Act
            controller.Delete(3, _cancellationTokenSource.Object.Token);

            //Assert
            _service.Verify(x => x.Delete(3, _cancellationTokenSource.Object.Token), Times.Exactly(1));
        }
    }
}