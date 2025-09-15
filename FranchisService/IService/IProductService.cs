using FranchisService.Models.Response;

namespace FranchisService.IService
{
    /// <summary>
    /// Product service interface
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        Task<ProductResponse> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets all products.
        /// </summary>
        Task<IEnumerable<ProductResponse>> GetAllAsync();

        /// <summary>
        /// Searches for products by name, type, and franchise.
        /// </summary>
        Task<IEnumerable<ProductResponse>> SearchAsync(string name, Guid franchiseId);

        /// <summary>
        /// Adds a new product.
        /// </summary>
        Task AddAsync(ProductResponse product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        Task UpdateAsync(ProductResponse product);

        /// <summary>
        /// Gets a paged list of products with metadata.
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Paged result of products.</returns>
        Task<PagedResult<ProductResponse>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
