using Microsoft.AspNetCore.Mvc;
using FranchisService.IService;
using FranchisService.Models.Response;

namespace FranchisApi.Controllers
{
    /// <summary>
    /// API endpoints for managing products.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </remarks>
    /// <param name="productService">Product service</param>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>List of products.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// Gets a paged list of products.
        /// </summary>
        /// <param name="pageNumber">Page number (1-based, default 1)</param>
        /// <param name="pageSize">Page size (default 20)</param>
        /// <returns>Paged result of products.</returns>
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<ProductResponse>>> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _productService.GetPagedAsync(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Searches for products by name and/or franchise.
        /// </summary>
        /// <param name="name">Product name (optional)</param>
        /// <param name="franchiseId">Franchise Id (optional)</param>
        /// <returns>List of matching products.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> Search([FromQuery] string? name, [FromQuery] Guid? franchiseId)
        {
            var products = await _productService.SearchAsync(name, franchiseId.GetValueOrDefault());
            return Ok(products);
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">Product to add</param>
        /// <returns>Action result</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductResponse product)
        {
            await _productService.AddAsync(product);
            return CreatedAtAction(nameof(GetAll), null);
        }
    }
}
