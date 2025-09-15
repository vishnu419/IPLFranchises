using System.ComponentModel.DataAnnotations;

namespace FranchiseRepository.Dtos
{
    /// <summary>
    /// Data Transfer Object for Order entity.
    /// </summary>
    public class OrderDto
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderItemDto> Items { get; set; } = default!;

        // Navigation property
        public UserDto? User { get; set; }
    }
}
