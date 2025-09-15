namespace FranchisService.Models.Response
{
    /// <summary>
    /// Order item response model
    /// </summary>
    public class OrderItemResponse
    {
        public Guid ProductId { get; set; }
 
        public required string ProductName { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal Price { get; set; }
    }
}
