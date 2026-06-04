using GymManagement.Api.Models;
using System.Threading.Tasks;

namespace GymManagement.Api.Repositories.Interfaces
{
    public interface IGymClassRepository : IGenericRepository<GymClass>
    {
        Task<int> GetTotalClassesCountAsync();
    }
}
