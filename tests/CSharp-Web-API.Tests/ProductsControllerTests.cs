using Xunit;
using Microsoft.AspNetCore.Mvc;
using CSharp_Web_API.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace CSharp_Web_API.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public void GetProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            var result = controller.GetProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse<IEnumerable<Product>>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.NotNull(apiResponse.Data);
            Assert.NotEmpty(apiResponse.Data);
        }

        [Fact]
        public void GetProduct_ReturnsOkResult_WithProduct_WhenProductExists()
        {
            // Arrange
            var controller = new ProductsController();
            var existingProductId = 1;

            // Act
            var result = controller.GetProduct(existingProductId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse<Product>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.NotNull(apiResponse.Data);
            Assert.Equal(existingProductId, apiResponse.Data.Id);
        }

        [Fact]
        public void GetProduct_ReturnsNotFoundResult_WhenProductDoesNotExist()
        {
            // Arrange
            var controller = new ProductsController();
            var nonExistentProductId = 99;

            // Act
            var result = controller.GetProduct(nonExistentProductId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse<Product>>(notFoundResult.Value);
            Assert.False(apiResponse.Success);
            Assert.Equal("Product not found", apiResponse.Message);
        }

        [Fact]
        public void CreateProduct_ReturnsCreatedAtActionResult_WithNewProduct()
        {
            // Arrange
            var controller = new ProductsController();
            var newProduct = new Product { Name = "Monitor", Price = 199.99m, Category = "Electronics" };

            // Act
            var result = controller.CreateProduct(newProduct);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse<Product>>(createdAtActionResult.Value);
            Assert.True(apiResponse.Success);
            Assert.NotNull(apiResponse.Data);
            Assert.Equal(newProduct.Name, apiResponse.Data.Name);
            Assert.True(apiResponse.Data.Id > 0);
        }
    }
}
