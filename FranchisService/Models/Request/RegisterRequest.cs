namespace FranchisService.Models.Request
{
    /// <summary>
    /// Register request model
    /// </summary>
    public class RegisterRequest
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
