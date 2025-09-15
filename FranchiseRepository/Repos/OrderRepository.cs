using FranchiseRepository.Dtos;
using FranchiseRepository.IRepos;
using Microsoft.EntityFrameworkCore;

namespace FranchiseRepository.Repos
{
    /// <summary>
    /// Repository for managing Order entities.
    /// </summary>
    /// <param name="context"></param>
    public class OrderRepository(FranchisDbContext context) : BaseRepository<OrderDto>(context), IOrderRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async override Task AddAsync(OrderDto order)
        {
            if (order.Id == Guid.Empty)
                order.Id = Guid.NewGuid();
            order.OrderDate = DateTime.UtcNow;

            foreach (var item in order.Items)
            {
                item.Id = Guid.NewGuid();
                item.OrderId = order.Id;
            }

            _context.Orders.Add(order);
            _context.OrderItems.AddRange(order.Items);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets an order by its ID, including its items.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrderDto> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Where(o => o.Id == id)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    Items = _context.OrderItems
                        .Where(i => i.OrderId == o.Id).Include(x => x.Product)
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all orders for a specific user, including their items, ordered by date descending.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderDto>> GetByUserIdAsync(Guid userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            foreach (var order in orders)
            {
                order.Items = await _context.OrderItems
                    .Where(i => i.OrderId == order.Id)
                    .ToListAsync();
            }

            return orders;
        }

        /// <summary>
        /// Gets a paginated list of orders for a specific user, including their items, ordered by date descending.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<OrderDto> Items, int TotalCount)> GetPagedByUserIdAsync(Guid userId, int pageNumber, int pageSize)
        {
            var query = _context.Orders.Where(o => o.UserId == userId);
           
            var total = await query.CountAsync();
            
            var orders = await query
                .OrderByDescending(o => o.OrderDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
           
            foreach (var order in orders)
            {
                order.Items = await _context.OrderItems
                    .Where(i => i.OrderId == order.Id).Include(x=> x.Product)
                    .ToListAsync();
            }

            return (orders, total);
        }
    }
}
