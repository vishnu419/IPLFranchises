using System.ComponentModel.DataAnnotations;

namespace FranchiseRepository.Dtos
{
    /// <summary>
    /// Data Transfer Object for OrderItem entity.
    /// </summary>
    public class OrderItemDto
    {
        [Key]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        // Navigation properties
        public OrderDto? Order { get; set; }

        public ProductDto? Product { get; set; }
    }
}
