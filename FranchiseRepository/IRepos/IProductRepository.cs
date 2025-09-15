using FranchiseRepository.Dtos;

namespace FranchiseRepository.IRepos
{
    /// <summary>
    /// Repository contract for product-related data access operations.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Gets a product by its unique identifier.
        /// </summary>
        /// <param name="id">Product ID</param>
        Task<ProductDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets all products.
        /// </summary>
        Task<IEnumerable<ProductDto>> GetAllAsync();

        /// <summary>
        /// Searches for products by name, type, and franchise.
        /// </summary>
        /// <param name="name">Product name (optional)</param>
        Task<IEnumerable<ProductDto>> SearchAsync(string name, Guid franchiseId);

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">Product to add</param>
        Task AddAsync(ProductDto product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">Product to update</param>
        Task UpdateAsync(ProductDto product);

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">Product ID</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Gets a paged list of products with total count.
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Tuple of products and total count.</returns>
        Task<(IEnumerable<ProductDto> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
    }
}
