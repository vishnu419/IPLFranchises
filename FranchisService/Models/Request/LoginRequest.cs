
namespace FranchisService.Models.Request
{
    /// <summary>
    /// Login request model
    /// </summary>
    public class LoginRequest
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
