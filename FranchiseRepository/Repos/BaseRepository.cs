using Microsoft.EntityFrameworkCore;

namespace FranchiseRepository.Repos
{
    /// <summary>
    /// Generic base repository for common CRUD operations.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
    /// </remarks>
    /// <param name="context">Database context</param>
    public class BaseRepository<TEntity>(FranchisDbContext context) where TEntity : class
    {
        protected readonly FranchisDbContext _context = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        /// <summary>
        /// Gets an entity by its ID.
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>The entity if found, otherwise null.</returns>
        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>List of all entities.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public virtual async Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an entity by its ID.
        /// </summary>
        /// <param name="id">Entity ID</param>
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
