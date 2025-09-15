namespace FranchisService.Models.Response
{
    /// <summary>
    /// Product response model
    /// </summary>
    public class ProductResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public string FranchiseName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
