using FranchiseRepository.Dtos;
using FranchiseRepository.IRepos;

namespace FranchiseRepository.Repos
{
    /// <summary>
    /// Repository for managing franchise data.
    /// </summary>
    /// <param name="context"></param>
    public class FranchisRepository(FranchisDbContext context) : BaseRepository<FranchiseDto>(context), IFranchiseRepository
    {
       
    }
}
