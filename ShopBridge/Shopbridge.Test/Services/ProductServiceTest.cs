using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Models.DTO;
using Shopbridge_base.Domain.Modules.Interfaces;
using Shopbridge_base.Domain.Services;
using Shopbridge_base.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge.Test.Services
{
    [TestClass]
    public class ProductServiceTest
    {
        private readonly Mock<IProductModule> mockProductModule;
        private readonly Mock<ILoggerService> mockLoggerService;
        private readonly Mock<IMapper> mockMapper;
        private ProductService _productService;

        public ProductServiceTest()
        {
            mockProductModule = new Mock<IProductModule>();
            mockLoggerService = new Mock<ILoggerService>();
            mockMapper = new Mock<IMapper>();
            _productService = new ProductService(mockProductModule.Object
                , mockLoggerService.Object
                , mockMapper.Object);
        }
        [TestMethod]
        public async Task GetProductById_Success()
        {
            //Arrange
            mockProductModule.Setup(m => m.GetProductById(It.IsAny<long>())).Returns(Task.FromResult(new ApiResponse<Product>
            {
                Data = new Product { Id = 1 }
            }));
            mockMapper.Setup(a => a.Map<ProductDTO>(It.IsAny<Product>())).Returns(new ProductDTO { Id = 1 });
            //Act
            var response = _productService.GetProductById(1);
            //Assert
            Assert.AreEqual(1, response.Id);
        }

       
    }
}