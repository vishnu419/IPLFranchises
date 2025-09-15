using FranchiseRepository.Dtos;
using FranchiseRepository.IRepos;
using Microsoft.EntityFrameworkCore;

namespace FranchiseRepository.Repos
{
    /// <summary>
    /// Repository for product-specific data access operations.
    /// </summary>
    public class ProductRepository : BaseRepository<ProductDto>, IProductRepository
    {
        public ProductRepository(FranchisDbContext context) : base(context) { }

        /// <summary>
        /// Searches for products by name, type, and franchise.
        /// </summary>
        /// <param name="name">Product name (optional)</param>
        /// <returns>List of matching products.</returns>
        public async Task<IEnumerable<ProductDto>> SearchAsync(string name, Guid franchiseId)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Franchise)
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(name))
            {
                var loweredName = name.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(loweredName) ||
                    p.Description.ToLower().Contains(loweredName));
            }

            if (franchiseId != Guid.Empty)
                query = query.Where(p => p.FranchiseId == franchiseId);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets a paged list of products with total count, including related Category and Franchise.
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Tuple of products and total count.</returns>
        public async Task<(IEnumerable<ProductDto> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Franchise)
                .AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (items, totalCount);
        }

        /// <summary>
        /// Gets a product by its unique identifier, including related Category and Franchise.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async override Task<ProductDto> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Franchise)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Gets all products, including related Category and Franchise.
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Franchise)
                .ToListAsync();
        }
    }
}
