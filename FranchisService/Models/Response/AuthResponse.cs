namespace FranchisService.Models.Response
{
    /// <summary>
    /// Authentication response model
    /// </summary>
    public class AuthResponse
    {
        public string AccessToken { get; set; } = string.Empty;
      
        public string RefreshToken { get; set; } = string.Empty;
      
        public string Username { get; set; } = string.Empty;
      
        public string Email { get; set; } = string.Empty;
       
        public string UserId { get; set; } = string.Empty;
    }
}