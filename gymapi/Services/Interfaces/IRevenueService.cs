using GymManagement.Api.DTOs.Revenue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagement.Api.Services.Interfaces
{
    public interface IRevenueService
    {
        Task<IEnumerable<RevenueDto>> GetAllRevenueAsync();
        Task<RevenueDto> AddRevenueAsync(CreateRevenueDto createRevenueDto);
    }
}
