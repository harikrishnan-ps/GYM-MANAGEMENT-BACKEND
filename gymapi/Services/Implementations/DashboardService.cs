using GymManagement.Api.DTOs.Dashboard;
using GymManagement.Api.Repositories.Interfaces;
using GymManagement.Api.Services.Interfaces;
using System.Threading.Tasks;

namespace GymManagement.Api.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IGymClassRepository _classRepository;
        private readonly IRevenueRepository _revenueRepository;

        public DashboardService(
            IMemberRepository memberRepository,
            IGymClassRepository classRepository,
            IRevenueRepository revenueRepository)
        {
            _memberRepository = memberRepository;
            _classRepository = classRepository;
            _revenueRepository = revenueRepository;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var totalMembers = await _memberRepository.GetTotalMembersCountAsync();
            var totalClasses = await _classRepository.GetTotalClassesCountAsync();
            var totalRevenue = await _revenueRepository.GetTotalRevenueAsync();
            var totalTransactions = await _revenueRepository.GetTotalTransactionsCountAsync();

            return new DashboardStatsDto
            {
                TotalMembers = totalMembers,
                TotalClasses = totalClasses,
                TotalRevenue = totalRevenue,
                TotalTransactions = totalTransactions
            };
        }
    }
}
