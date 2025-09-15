using FranchiseRepository.Repos;
using FranchiseRepository.Dtos;
using Microsoft.EntityFrameworkCore;
using FranchiseRepository;

namespace FranchisTest.Repositories
{
    [TestClass]
    public class OrderRepositoryTests
    {
        private OrderRepository _repo;
        private FranchisDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FranchisDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new FranchisDbContext(options);
            _repo = new OrderRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddAndGetByIdAsync_Works()
        {
            var order = new OrderDto { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), Items = [] };
            await _repo.AddAsync(order);
            var fetched = await _repo.GetByIdAsync(order.Id);
            Assert.IsNotNull(fetched);
            Assert.AreEqual(order.Id, fetched.Id);
        }

        [TestMethod]
        public async Task GetByUserIdAsync_ReturnsOrders()
        {
            var userId = Guid.NewGuid();
            await _repo.AddAsync(new OrderDto { Id = Guid.NewGuid(), UserId = userId, Items = [] });
            var orders = await _repo.GetByUserIdAsync(userId);
            Assert.IsTrue(orders.Any());
        }

        [TestMethod]
        public async Task GetPagedByUserIdAsync_ReturnsPaged()
        {
            var userId = Guid.NewGuid();
            for (int i = 0; i < 5; i++)
                await _repo.AddAsync(new OrderDto { Id = Guid.NewGuid(), UserId = userId, Items = [] });
            var (items, total) = await _repo.GetPagedByUserIdAsync(userId, 1, 2);
            Assert.AreEqual(5, total);
            Assert.AreEqual(2, items.Count());
        }
    }
}
