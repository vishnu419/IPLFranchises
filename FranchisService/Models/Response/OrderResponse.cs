namespace FranchisService.Models.Response
{
    /// <summary>
    /// Order response model
    /// </summary>
    public class OrderResponse
    {
        public Guid Id { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public required List<OrderItemResponse> Items { get; set; }
    }
}
