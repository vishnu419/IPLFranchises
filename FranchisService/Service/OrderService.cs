using FranchiseRepository.IRepos;
using FranchisService.IService;
using FranchisService.Models.Request;
using FranchisService.Models.Response;

namespace FranchisService.Service
{
    /// <summary>
    /// Service for managing orders.
    /// </summary>
    /// <param name="orderRepository"></param>
    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        /// <summary>
        /// Adds a new order for a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task AddAsync(Guid userId, OrderRequest order)
        {
            var orderDto = new FranchiseRepository.Dtos.OrderDto
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Items = [.. order.Items.Select(i => new FranchiseRepository.Dtos.OrderItemDto
                    {
                        Id = Guid.NewGuid(),
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    })]
            };

            await _orderRepository.AddAsync(orderDto);
        }

        /// <summary>
        /// Gets an order by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrderResponse> GetByIdAsync(Guid id)
        {
            var dto = await _orderRepository.GetByIdAsync(id);

            if (dto == null)
                return null;

            return MapToOrderResponse(dto);
        }

        /// <summary>
        /// Gets all orders for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderResponse>> GetByUserIdAsync(Guid userId)
        {
            var dtos = await _orderRepository.GetByUserIdAsync(userId);
            return dtos.Select(MapToOrderResponse);
        }

        /// <summary>
        /// Gets a paged list of orders for a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedResult<OrderResponse>> GetPagedByUserIdAsync(Guid userId, int pageNumber, int pageSize)
        {
            var (items, total) = await _orderRepository.GetPagedByUserIdAsync(userId, pageNumber, pageSize);
            
            return new PagedResult<OrderResponse>
            {
                Items = items.Select(MapToOrderResponse),
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// Maps an OrderDto to an OrderResponse.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static OrderResponse MapToOrderResponse(FranchiseRepository.Dtos.OrderDto dto)
        {
            return new OrderResponse
            {
                Id = dto.Id,
                OrderDate = dto.OrderDate,
                Items = dto.Items?.Select(MapToOrderItemResponse).ToList() ?? []
            };
        }

        /// <summary>
        /// Maps an OrderItemDto to an OrderItemResponse.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static OrderItemResponse MapToOrderItemResponse(FranchiseRepository.Dtos.OrderItemDto i)
        {
            return new OrderItemResponse
            {
                ProductId = i.ProductId,
                ProductName = i.Product?.Name ?? string.Empty,
                Quantity = i.Quantity,
                Price = i.Price
            };
        }
    }
}
