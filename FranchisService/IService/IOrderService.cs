using FranchisService.Models.Response;

namespace FranchisService.IService
{
    /// <summary>
    /// Order service interface
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Gets an order by its ID.
        /// </summary>
        Task<OrderResponse> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets all orders for a user.
        /// </summary>
        Task<IEnumerable<OrderResponse>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Gets a paged list of orders for a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedResult<OrderResponse>> GetPagedByUserIdAsync(Guid userId, int pageNumber, int pageSize);

        /// <summary>
        /// Adds a new order.
        /// </summary>
        Task AddAsync(Guid userId, Models.Request.OrderRequest order);
    }
}
