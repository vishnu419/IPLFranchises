using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FranchisService.IService;
using FranchisService.Models.Request;
using System.Security.Claims;

namespace FranchisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var guid))
                return Unauthorized();
            
            await _orderService.AddAsync(guid, request);
           
            return Ok(new { message = "Order placed successfully" });
        }

        // GET: api/order
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var guid))
                return Unauthorized();
           
            var orders = await _orderService.GetByUserIdAsync(guid);
           
            return Ok(orders);
        }

        // GET: api/order/paged
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var guid))
                return Unauthorized();
            
            var paged = await _orderService.GetPagedByUserIdAsync(guid, pageNumber, pageSize);
           
            return Ok(paged);
        }
    }
}
