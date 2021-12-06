using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TomasekRestApi.BL.Data;
using TomasekRestApi.Controllers;
using TomasekRestApi.Model.Dto;
using TomasekRestApi.Profiles;
using Xunit;

namespace RestApiUnitTests
{
    public class ProductTest
    {
        private readonly ProductsController _controller;
        private readonly IProductRepository _service;

        public ProductTest()
        {
            _service = new ProductRepositoryFake();
            var config = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductsProfile());
            });
            var mapper = config.CreateMapper();
            _controller = new ProductsController(_service,mapper);
        }

        [Fact]
        public void GetAllProducts_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAllProducts().Result;
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetAllProducts_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.GetAllProducts().Result as OkObjectResult;
            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<ProductReadDto>>(okResult.Value);
            List<ProductReadDto> list = new List<ProductReadDto>(items);
            Assert.Equal(20, list.Count);
        }
        
        [Fact]
        public void GetAllProducts_WhenCalledPagination_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAllProducts(1).Result;
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        
        [Fact]
        public void GetAllProducts_WhenCalled_ReturnsAllItemsPaginationDefault()
        {
            // Act
            var okResult = _controller.GetAllProducts(1).Result as OkObjectResult;
            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<ProductReadDto>>(okResult.Value);
            List<ProductReadDto> list = new List<ProductReadDto>(items);
            Assert.Equal(10, list.Count);
        }
        
        [Fact]
        public void GetAllProducts_WhenCalled_ReturnsAllItemsPagination()
        {
            // Act
            var okResult = _controller.GetAllProducts(1, 5).Result as OkObjectResult;
            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<ProductReadDto>>(okResult.Value);
            List<ProductReadDto> list = new List<ProductReadDto>(items);
            Assert.Equal(5, list.Count);
        }

        [Fact]
        public void GetProductById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetProductById(0).Result;
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetByProductId_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 1;
            // Act
            var okResult = _controller.GetProductById(testId).Result;
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetByProductId_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 1;
            // Act
            var okResult = _controller.GetProductById(testId).Result as OkObjectResult;
            // Assert
            Assert.IsType<ProductReadDto>(okResult.Value);
            Assert.Equal("Test1", ((ProductReadDto)okResult.Value).Name);
        }

        
        [Fact]
        public void PartialProductUpdate_Updating()
        {
            // Arrange            
            int testId = 1;
            string desc = "Partial Desc";
            _controller.PartialProductUpdate(testId, desc);
            var okResult = _controller.GetProductById(testId).Result as OkObjectResult;
            // Assert
            Assert.IsType<ProductReadDto>(okResult.Value);
            Assert.Equal(desc, ((ProductReadDto)okResult.Value).Description);
            
        }      

    }
}