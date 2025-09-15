namespace FranchisApi.Models
{
    public class ErrorResponse
    {
        public string Error { get; set; } = string.Empty;
        public string? Detail { get; set; }
    }
}
