namespace FranchisService.Models.Response
{
    /// <summary>
    /// User response model
    /// </summary>
    public class UserResponse
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string Email { get; set; }
    }
}
