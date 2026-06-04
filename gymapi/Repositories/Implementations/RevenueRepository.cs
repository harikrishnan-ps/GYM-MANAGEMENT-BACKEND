using GymManagement.Api.Data;
using GymManagement.Api.Models;
using GymManagement.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymManagement.Api.Repositories.Implementations
{
    public class RevenueRepository : GenericRepository<Revenue>, IRevenueRepository
    {
        public RevenueRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _dbSet.SumAsync(r => r.Amount);
        }

        public async Task<int> GetTotalTransactionsCountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}
