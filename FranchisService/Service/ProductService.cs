using FranchisService.IService;
using FranchiseRepository.Dtos;
using FranchiseRepository.IRepos;
using FranchisService.Models.Response;

namespace FranchisService.Service
{
    /// <summary>
    /// Service for product-related business logic.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </remarks>
    /// <param name="productRepository">Product repository</param>
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;

        /// <inheritdoc/>
        public async Task<ProductResponse> GetByIdAsync(Guid id)
        {
            var dto = await _productRepository.GetByIdAsync(id);

            return MapToResponse(dto);
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var dtos = await _productRepository.GetAllAsync();
            return dtos.Select(MapToResponse);
        }

        /// <summary>
        /// Searches for products by name and franchise.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="franchiseId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductResponse>> SearchAsync(string name, Guid franchiseId)
        {
            // Only name is supported in repository for now
            var dtos = await _productRepository.SearchAsync(name, franchiseId);
            return dtos.Select(MapToResponse);
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task AddAsync(ProductResponse product)
        {
            var dto = MapToDto(product);
            await _productRepository.AddAsync(dto);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ProductResponse product)
        {
            var dto = MapToDto(product);
            await _productRepository.UpdateAsync(dto);
        }

        /// <summary>
        ///     Deletes a product by its ID.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedResult<ProductResponse>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _productRepository.GetPagedAsync(pageNumber, pageSize);
            return new PagedResult<ProductResponse>
            {
                Items = items.Select(MapToResponse),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// Maps a ProductDto to a ProductResponse.
        /// </summary>
        private static ProductResponse MapToResponse(ProductDto dto) => new()
        {
            Id = dto.Id,
            Name = dto.Name,
            CategoryName = dto.Category?.Name ?? string.Empty,
            Price = dto.Price,
            FranchiseName = dto.Franchise?.Name ?? string.Empty,
            ImageUrl = dto.ImageUrl,
            Description = dto.Description
        };

        /// <summary>
        /// Maps a ProductResponse to a ProductDto.
        /// </summary>
        private static ProductDto MapToDto(ProductResponse response)
        {
            return new ProductDto
            {
                Id = Guid.NewGuid(),
                Name = response.Name,
                Price = response.Price,
                FranchiseId = Guid.Empty,
                CategoryId = Guid.Empty,
                ImageUrl = response.ImageUrl,
                Description = response.Description
            };
        }
    }
}
