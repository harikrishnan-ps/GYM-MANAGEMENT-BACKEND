using GymManagement.Api.Data;
using GymManagement.Api.Models;
using GymManagement.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Api.Repositories.Implementations
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Member>> SearchMembersAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return await GetAllAsync();
            }

            query = query.ToLower();
            return await _dbSet
                .Where(m => m.Name.ToLower().Contains(query) || 
                            m.Email.ToLower().Contains(query) || 
                            m.Phone.Contains(query) || 
                            m.MembershipType.ToLower().Contains(query))
                .ToListAsync();
        }

        public async Task<Member?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Email.ToLower() == email.ToLower());
        }

        public async Task<int> GetTotalMembersCountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}
