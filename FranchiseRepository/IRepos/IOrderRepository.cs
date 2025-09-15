using FranchiseRepository.Dtos;

namespace FranchiseRepository.IRepos
{
    /// <summary>
    /// Repository interface for managing Order entities.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Gets an order by its ID, including its items.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrderDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets all orders for a specific user, including their items, ordered by date descending.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderDto>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Gets a paginated list of orders for a specific user, including their items, ordered by date descending.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(IEnumerable<OrderDto> Items, int TotalCount)> GetPagedByUserIdAsync(Guid userId, int pageNumber, int pageSize);

        /// <summary>
        /// Adds a new order along with its items.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task AddAsync(OrderDto order);
    }
}
