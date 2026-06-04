using GymManagement.Api.Data;
using GymManagement.Api.Models;
using GymManagement.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymManagement.Api.Repositories.Implementations
{
    public class GymClassRepository : GenericRepository<GymClass>, IGymClassRepository
    {
        public GymClassRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<int> GetTotalClassesCountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}
