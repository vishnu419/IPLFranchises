using FranchisService.IService;
using FranchisApi.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using FranchisService.Models.Response;

namespace FranchisTest.Controllers
{
    [TestClass]
    public class FranchiseControllerTests
    {
        private Mock<IFranchiseService> _serviceMock;
        private FranchiseController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IFranchiseService>();
            _controller = new FranchiseController(_serviceMock.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOk()
        {
            var expectedList = new List<FranchisResponse>();

            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(expectedList);

            var result = await _controller.GetAll();
            
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
