using GymManagement.Api.DTOs.Auth;
using System.Threading.Tasks;

namespace GymManagement.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
