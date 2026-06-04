using GymManagement.Api.Data;
using GymManagement.Api.Models;
using GymManagement.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymManagement.Api.Repositories.Implementations
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<Admin?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());
        }
    }
}
