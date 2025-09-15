using FranchiseRepository.Repos;
using FranchiseRepository.Dtos;
using Microsoft.EntityFrameworkCore;
using FranchiseRepository;

namespace FranchisTest.Repositories
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private ProductRepository _repo;
        private FranchisDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FranchisDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new FranchisDbContext(options);
            _repo = new ProductRepository(_context);
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
            var product = GetProduct();

            await _repo.AddAsync(product);
            var fetched = await _repo.GetByIdAsync(product.Id);
            Assert.IsNotNull(fetched);
            Assert.AreEqual("ball", fetched.Name);
        }

        

        [TestMethod]
        public async Task GetAllAsync_ReturnsAll()
        {
            var product = GetProduct();

            await _repo.AddAsync(product);
            
            var all = await _repo.GetAllAsync();
            
            Assert.AreEqual(1, all.Count());
        }

        [TestMethod]
        public async Task SearchAsync_ByName_Works()
        {
            var product = GetProduct();

            await _repo.AddAsync(product);
                       
            var found = await _repo.SearchAsync("ball", Guid.Empty);
           
            Assert.AreEqual(1, found.Count());
            Assert.AreEqual("ball", found.First().Name);
        }

        [TestMethod]
        public async Task GetPagedAsync_ReturnsPaged()
        {
            for (int i = 0; i < 10; i++)
                await _repo.AddAsync(GetProduct());
            var (items, total) = await _repo.GetPagedAsync(2, 3);
            Assert.AreEqual(10, total);
            Assert.AreEqual(3, items.Count());
        }

        private static ProductDto GetProduct()
        {
            var CategoryId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var franchiseId = Guid.NewGuid();

            var product = new ProductDto
            {
                Id = productId,
                Name = "ball",
                Price = 10,
                Description = "ball",
                ImageUrl = "",
                Category = new CategoryDto() { CategoryId = CategoryId, Description = "", Name = "" },
                Franchise = new FranchiseDto() { Id = franchiseId, Name = "", Description = "" }
            };

            return product;
        }
    }
}
