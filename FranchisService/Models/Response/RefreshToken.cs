namespace FranchisService.Models.Response
{
    /// <summary>
    /// Refresh token model
    /// </summary>
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;

        public DateTime Created { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}