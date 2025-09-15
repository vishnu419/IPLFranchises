using FranchiseRepository.Repos;
using FranchiseRepository.Dtos;
using Microsoft.EntityFrameworkCore;
using FranchiseRepository;

namespace FranchisTest.Repositories
{
    [TestClass]
    public class UserRepositoryTests
    {
        private UserRepository _repo;
        private FranchisDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FranchisDbContext>()
                .UseInMemoryDatabase(databaseName: "UserRepoTestDb")
                .Options;
            _context = new FranchisDbContext(options);
            _repo = new UserRepository(_context);
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
            var user = new UserDto { Username = "test", Email = "test@test.com", PasswordHash = "hash" };
            await _repo.AddAsync(user);
            var fetched = await _repo.GetByIdAsync(user.Id);
            Assert.IsNotNull(fetched);
            Assert.AreEqual("test", fetched.Username);
        }

        [TestMethod]
        public async Task GetByUsernameAsync_ReturnsUser()
        {
            var user = new UserDto { Username = "user2", Email = "u2@test.com", PasswordHash = "hash2" };
            await _repo.AddAsync(user);
            var fetched = await _repo.GetByUsernameAsync("user2");
            Assert.IsNotNull(fetched);
            Assert.AreEqual("user2", fetched.Username);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAll()
        {
            await _repo.AddAsync(new UserDto { Username = "a", Email = "a@a.com", PasswordHash = "h" });
            await _repo.AddAsync(new UserDto { Username = "b", Email = "b@b.com", PasswordHash = "h" });
            var all = await _repo.GetAllAsync();
            Assert.AreEqual(2, System.Linq.Enumerable.Count(all));
        }

        [TestMethod]
        public async Task UpdateAsync_UpdatesUser()
        {
            var user = new UserDto { Username = "old", Email = "old@old.com", PasswordHash = "h" };
            await _repo.AddAsync(user);
            user.Email = "new@new.com";
            await _repo.UpdateAsync(user);
            var updated = await _repo.GetByIdAsync(user.Id);
            Assert.AreEqual("new@new.com", updated.Email);
        }

        [TestMethod]
        public async Task DeleteAsync_RemovesUser()
        {
            var user = new UserDto { Username = "del", Email = "del@del.com", PasswordHash = "h" };
            await _repo.AddAsync(user);
            await _repo.DeleteAsync(user.Id);
            var deleted = await _repo.GetByIdAsync(user.Id);
            Assert.IsNull(deleted);
        }
    }
}
