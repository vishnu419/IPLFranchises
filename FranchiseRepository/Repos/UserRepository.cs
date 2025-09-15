using FranchiseRepository.Dtos;
using FranchiseRepository.IRepos;
using Microsoft.EntityFrameworkCore;

namespace FranchiseRepository.Repos
{
    /// <summary>
    /// Repository for user-related data access operations.
    /// </summary>
    /// <param name="context"></param>
    public class UserRepository(FranchisDbContext context) : IUserRepository
    {
        private readonly FranchisDbContext _context = context;

        /// <summary>
        /// Gets a user by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// Gets a user by username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddAsync(UserDto user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UserDto user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}