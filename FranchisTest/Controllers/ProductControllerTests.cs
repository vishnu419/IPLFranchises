using FranchisService.IService;
using FranchisApi.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using FranchisService.Models.Response;

namespace FranchisTest.Controllers
{
    [TestClass]
    public class ProductControllerTests
    {
        private Mock<IProductService> _serviceMock;
        private ProductController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IProductService>();
            _controller = new ProductController(_serviceMock.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<ProductResponse>());

            var result = await _controller.GetAll();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
