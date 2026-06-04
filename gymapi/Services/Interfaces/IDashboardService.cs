using GymManagement.Api.DTOs.Dashboard;
using System.Threading.Tasks;

namespace GymManagement.Api.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}
