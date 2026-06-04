using GymManagement.Api.DTOs.GymClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagement.Api.Services.Interfaces
{
    public interface IGymClassService
    {
        Task<IEnumerable<ClassDto>> GetAllClassesAsync();
        Task<ClassDto?> GetClassByIdAsync(int id);
        Task<ClassDto> CreateClassAsync(CreateClassDto createClassDto);
        Task<bool> UpdateClassAsync(int id, CreateClassDto updateClassDto);
        Task<bool> DeleteClassAsync(int id);
    }
}
