namespace FranchisService.Models.Request
{
    /// <summary>
    /// Refresh token request model
    /// </summary>
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}