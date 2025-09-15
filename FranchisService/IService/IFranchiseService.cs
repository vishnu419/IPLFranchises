using FranchisService.Models.Response;

namespace FranchisService.IService
{
    /// <summary>
    /// Franchise service interface
    /// </summary>
    public interface IFranchiseService
    {
        /// <summary>
        /// Gets all franchises.
        /// </summary>
        Task<IEnumerable<FranchisResponse>> GetAllAsync();
    }
}
