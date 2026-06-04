using GymManagement.Api.DTOs.Revenue;
using GymManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/revenue")]
    public class RevenueController : ControllerBase
    {
        private readonly IRevenueService _revenueService;

        public RevenueController(IRevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var revenues = await _revenueService.GetAllRevenueAsync();
            return Ok(revenues);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRevenueDto createRevenueDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newRevenue = await _revenueService.AddRevenueAsync(createRevenueDto);
            return Ok(newRevenue);
        }
    }
}
