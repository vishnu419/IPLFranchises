namespace FranchisService.Models.Request
{
    /// <summary>
    /// Order request model
    /// </summary>
    public class OrderRequest
    {
        public List<OrderItemRequest> Items { get; set; } = new();
    }

    /// <summary>
    /// Order item request model
    /// </summary>
    public class OrderItemRequest
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
