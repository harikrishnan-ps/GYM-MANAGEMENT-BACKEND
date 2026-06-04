using AutoMapper;
using GymManagement.Api.DTOs.GymClass;
using GymManagement.Api.Models;
using GymManagement.Api.Repositories.Interfaces;
using GymManagement.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagement.Api.Services.Implementations
{
    public class GymClassService : IGymClassService
    {
        private readonly IGymClassRepository _classRepository;
        private readonly IMapper _mapper;

        public GymClassService(IGymClassRepository classRepository, IMapper mapper)
        {
            _classRepository = classRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassDto>> GetAllClassesAsync()
        {
            var classes = await _classRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClassDto>>(classes);
        }

        public async Task<ClassDto?> GetClassByIdAsync(int id)
        {
            var gymClass = await _classRepository.GetByIdAsync(id);
            return gymClass == null ? null : _mapper.Map<ClassDto>(gymClass);
        }

        public async Task<ClassDto> CreateClassAsync(CreateClassDto createClassDto)
        {
            var gymClass = _mapper.Map<GymClass>(createClassDto);
            await _classRepository.AddAsync(gymClass);
            await _classRepository.SaveChangesAsync();

            return _mapper.Map<ClassDto>(gymClass);
        }

        public async Task<bool> UpdateClassAsync(int id, CreateClassDto updateClassDto)
        {
            var existingClass = await _classRepository.GetByIdAsync(id);
            if (existingClass == null)
            {
                return false;
            }

            _mapper.Map(updateClassDto, existingClass);
            _classRepository.Update(existingClass);
            return await _classRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            var gymClass = await _classRepository.GetByIdAsync(id);
            if (gymClass == null)
            {
                return false;
            }

            _classRepository.Delete(gymClass);
            return await _classRepository.SaveChangesAsync();
        }
    }
}
