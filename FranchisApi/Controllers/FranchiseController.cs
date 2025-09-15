using FranchisService.IService;
using Microsoft.AspNetCore.Mvc;

namespace FranchisApi.Controllers
{
    /// <summary>
    /// Franchise controller
    /// </summary>
    /// <param name="franchiseService"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class FranchiseController(IFranchiseService franchiseService) : ControllerBase
    {
        private readonly IFranchiseService _franchiseService = franchiseService;

        /// <summary>
        /// Gets all franchises.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var franchises = await _franchiseService.GetAllAsync();

            return Ok(franchises);
        }
    }
}
