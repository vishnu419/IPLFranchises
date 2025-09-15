using FranchiseRepository.IRepos;
using Moq;
using FranchiseRepository.Dtos;
using FranchisService.Models.Request;
using FranchisService.Service;

namespace FranchisTest.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> _repoMock;
        private OrderService _service;

        [TestInitialize]
        public void Setup()
        {
            _repoMock = new Mock<IOrderRepository>();
            _service = new OrderService(_repoMock.Object);
        }

        [TestMethod]
        public async Task AddAsync_CallsRepo()
        {
            var userId = Guid.NewGuid();
            var req = new OrderRequest { Items = new List<OrderItemRequest>() };
            _repoMock.Setup(r => r.AddAsync(It.IsAny<OrderDto>())).Returns(Task.CompletedTask).Verifiable();
            await _service.AddAsync(userId, req);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<OrderDto>()), Times.Once);
        }
    }
}
