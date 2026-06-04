using GymManagement.Api.Models;
using System.Threading.Tasks;

namespace GymManagement.Api.Repositories.Interfaces
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<Admin?> GetByEmailAsync(string email);
    }
}
