using FranchisService.Models.Request;
using FranchisService.Models.Response;

namespace FranchisService.IService
{
    /// <summary>
    /// User service interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        Task<bool> RegisterAsync(RegisterRequest registerRequest);

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        Task<AuthResponse?> LoginAsync(LoginRequest loginRequest);

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        Task<UserResponse> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        Task<UserResponse> GetByUsernameAsync(string username);

        /// <summary>
        /// Gets all users.
        /// </summary>
        Task<IEnumerable<UserResponse>> GetAllAsync();
    }
}
