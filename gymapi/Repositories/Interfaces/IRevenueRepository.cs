using GymManagement.Api.Models;
using System.Threading.Tasks;

namespace GymManagement.Api.Repositories.Interfaces
{
    public interface IRevenueRepository : IGenericRepository<Revenue>
    {
        Task<decimal> GetTotalRevenueAsync();
        Task<int> GetTotalTransactionsCountAsync();
    }
}
