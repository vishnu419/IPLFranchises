using FranchiseRepository.Dtos;

namespace FranchiseRepository.IRepos
{
    /// <summary>
    /// Repository contract for franchise-related data access operations.
    /// </summary>
    public interface IFranchiseRepository
    {
        /// <summary>
        /// Gets all franchises.
        /// </summary>
        Task<IEnumerable<FranchiseDto>> GetAllAsync();
    }
}
