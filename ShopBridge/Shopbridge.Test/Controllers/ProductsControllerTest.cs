using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shopbridge_base.Controllers;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Models.DTO;
using Shopbridge_base.Domain.Modules.Interfaces;
using Shopbridge_base.Domain.Services;
using Shopbridge_base.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge.Test.Contollers
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ProductsConntollerTest
    {
        private readonly Mock<IProductService> mockProductService;
        private readonly Mock<ILoggerService> mockLoggerService;
        private readonly Mock<IMapper> mockMapper;
        private ProductsController _productController;

        public ProductsConntollerTest()
        {
            mockProductService = new Mock<IProductService>();
            mockLoggerService = new Mock<ILoggerService>();
            mockMapper = new Mock<IMapper>();
            _productController = new ProductsController(mockProductService.Object
                , mockLoggerService.Object);
        }
        [TestMethod]
        public async Task GetAllProductTest_Ok()
        {
            //Arrange
            ApiResponse<IEnumerable<ProductDTO>> products = new ApiResponse<IEnumerable<ProductDTO>>
            {
                Data = new List<ProductDTO>
                {
                    new ProductDTO{Id = 1}
                }
            };
            mockProductService.Setup(m => m.GetAllProducts()).ReturnsAsync(products);
            //Act
            var response = await _productController.GetProduct();
            var value = (response?.Result as OkObjectResult).Value;
            //Assert
            Assert.AreEqual(1, (value as ApiResponse<IEnumerable<ProductDTO>>).Data.Count());
        }
    }
}