using FranchiseRepository.IRepos;
using FranchisService.IService;
using FranchisService.Models.Response;

namespace FranchisService.Service
{
    /// <summary>
    /// Service for managing franchise-related operations.
    /// </summary>
    /// <param name="franchiseRepository"></param>
    public class FranchisService(IFranchiseRepository franchiseRepository) : IFranchiseService
    {
        private readonly IFranchiseRepository _franchiseRepository = franchiseRepository;

        /// <summary>
        /// Gets all franchises.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FranchisResponse>> GetAllAsync()
        {
            var franchises = await _franchiseRepository.GetAllAsync();

            return franchises.Select(f => new FranchisResponse
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description
            });
        }
    }
}
