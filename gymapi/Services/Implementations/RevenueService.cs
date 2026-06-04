using AutoMapper;
using GymManagement.Api.DTOs.Revenue;
using GymManagement.Api.Models;
using GymManagement.Api.Repositories.Interfaces;
using GymManagement.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagement.Api.Services.Implementations
{
    public class RevenueService : IRevenueService
    {
        private readonly IRevenueRepository _revenueRepository;
        private readonly IMapper _mapper;

        public RevenueService(IRevenueRepository revenueRepository, IMapper mapper)
        {
            _revenueRepository = revenueRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RevenueDto>> GetAllRevenueAsync()
        {
            var revenues = await _revenueRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RevenueDto>>(revenues);
        }

        public async Task<RevenueDto> AddRevenueAsync(CreateRevenueDto createRevenueDto)
        {
            var revenue = _mapper.Map<Revenue>(createRevenueDto);
            revenue.PaymentDate = DateTime.UtcNow;

            await _revenueRepository.AddAsync(revenue);
            await _revenueRepository.SaveChangesAsync();

            return _mapper.Map<RevenueDto>(revenue);
        }
    }
}
