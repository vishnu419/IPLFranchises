using FranchiseRepository.Dtos;

namespace FranchiseRepository.IRepos
{
    /// <summary>
    /// Repository contract for user-related data access operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user by its unique identifier.
        /// </summary>
        /// <param name="id">User ID</param>
        Task<UserDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets a user by username.
        /// </summary>
        /// <param name="username">Username</param>
        Task<UserDto> GetByUsernameAsync(string username);

        /// <summary>
        /// Gets all users.
        /// </summary>
        Task<IEnumerable<UserDto>> GetAllAsync();

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">User to add</param>
        Task AddAsync(UserDto user);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user">User to update</param>
        Task UpdateAsync(UserDto user);

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">User ID</param>
        Task DeleteAsync(Guid id);
    }
}
