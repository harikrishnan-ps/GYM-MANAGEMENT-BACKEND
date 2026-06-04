using GymManagement.Api.DTOs.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagement.Api.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllMembersAsync(string? query);
        Task<MemberDto?> GetMemberByIdAsync(int id);
        Task<MemberDto> CreateMemberAsync(CreateMemberDto createMemberDto);
        Task<bool> UpdateMemberAsync(int id, CreateMemberDto updateMemberDto);
        Task<bool> DeleteMemberAsync(int id);
    }
}
