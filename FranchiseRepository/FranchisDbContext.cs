using FranchiseRepository.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FranchiseRepository
{
    /// <summary>
    /// Entity Framework Core database context for the IPL Franchises ecommerce system.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="FranchisDbContext"/> class.
    /// </remarks>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    public class FranchisDbContext(DbContextOptions<FranchisDbContext> options) : DbContext(options)
    {

        /// <summary>
        /// Gets or sets the users table.
        /// </summary>
        public DbSet<UserDto> Users { get; set; }

        /// <summary>
        /// Gets or sets the franchises table.
        /// </summary>
        public DbSet<FranchiseDto> Franchises { get; set; }

        /// <summary>
        /// Gets or sets the categories table.
        /// </summary>
        public DbSet<CategoryDto> Categories { get; set; }

        /// <summary>
        /// Gets or sets the products table.
        /// </summary>
        public DbSet<ProductDto> Products { get; set; }

        /// <summary>
        /// Gets or sets the orders table.
        /// </summary>
        public DbSet<OrderDto> Orders { get; set; }

        /// <summary>
        /// Gets or sets the order items table.
        /// </summary>
        public DbSet<OrderItemDto> OrderItems { get; set; }

        /// <summary>
        /// Configures the model and seeds default data.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Use fixed Guids for seeding
            var miId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var cskId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var rcbId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            // Seed categories
            var jerseyCatId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var capCatId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var flagCatId = Guid.Parse("66666666-6666-6666-6666-666666666666");

            modelBuilder.Entity<CategoryDto>().HasData(
                new CategoryDto { CategoryId = jerseyCatId, Name = "Jersey", Description = "Team Jerseys" },
                new CategoryDto { CategoryId = capCatId, Name = "Cap", Description = "Team Caps" },
                new CategoryDto { CategoryId = flagCatId, Name = "Flag", Description = "Team Flags" }
            );

            modelBuilder.Entity<FranchiseDto>().HasData(
                new FranchiseDto { Id = miId, Name = "Mumbai Indians", Description = "MI Official" },
                new FranchiseDto { Id = cskId, Name = "Chennai Super Kings", Description = "CSK Official" },
                new FranchiseDto { Id = rcbId, Name = "Royal Challengers Bengaluru", Description = "RCB Official" }
            );

            modelBuilder.Entity<ProductDto>().HasData(
                new ProductDto { Id = Guid.Parse("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), Name = "MI Jersey", Price = 1299M, FranchiseId = miId, CategoryId = jerseyCatId, ImageUrl = "/assets/mi-jersey.jpg", Description = "Mumbai Indians Jersey 2025" },
                new ProductDto { Id = Guid.Parse("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), Name = "MI Cap", Price = 499M, FranchiseId = miId, CategoryId = capCatId, ImageUrl = "/assets/mi-cap.jpg", Description = "Mumbai Indians Cap" },
                new ProductDto { Id = Guid.Parse("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), Name = "CSK Jersey", Price = 1299M, FranchiseId = cskId, CategoryId = jerseyCatId, ImageUrl = "/assets/csk-jersey.jpg", Description = "Chennai Super Kings Jersey 2025" },
                new ProductDto { Id = Guid.Parse("ccccccc1-cccc-cccc-cccc-ccccccccccc1"), Name = "RCB Flag", Price = 299M, FranchiseId = rcbId, CategoryId = flagCatId, ImageUrl = "/assets/rcb-flag.jpg", Description = "Royal Challengers Bengaluru Flag" }
            );

            // Configure relationships
            modelBuilder.Entity<ProductDto>()
                .HasOne(p => p.Franchise)
                .WithMany(f => f.Products)
                .HasForeignKey(p => p.FranchiseId);

            modelBuilder.Entity<ProductDto>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
