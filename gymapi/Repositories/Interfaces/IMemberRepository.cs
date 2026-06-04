using GymManagement.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagement.Api.Repositories.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<IEnumerable<Member>> SearchMembersAsync(string query);
        Task<Member?> GetByEmailAsync(string email);
        Task<int> GetTotalMembersCountAsync();
    }
}
