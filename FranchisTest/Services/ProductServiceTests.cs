using FranchiseRepository.IRepos;
using Moq;
using FranchiseRepository.Dtos;
using FranchisService.Models.Request;
using FranchisService.Service;

namespace FranchisTest.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _repoMock;
        private ProductService _service;

        [TestInitialize]
        public void Setup()
        {
            _repoMock = new Mock<IProductRepository>();
            _service = new ProductService(_repoMock.Object);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsProduct()
        {
            var dto = new ProductDto { Id = Guid.NewGuid(), Name = "P" };
            _repoMock.Setup(r => r.GetByIdAsync(dto.Id)).ReturnsAsync(dto);
            var result = await _service.GetByIdAsync(dto.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual("P", result.Name);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAll()
        {
            var dtos = new List<ProductDto> { new ProductDto { Id = Guid.NewGuid(), Name = "A" } };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(dtos);
            var result = await _service.GetAllAsync();
            Assert.AreEqual(1, System.Linq.Enumerable.Count(result));
        }
    }
}
