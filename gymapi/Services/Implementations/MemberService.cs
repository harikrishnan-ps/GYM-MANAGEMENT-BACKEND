using AutoMapper;
using GymManagement.Api.DTOs.Member;
using GymManagement.Api.Models;
using GymManagement.Api.Repositories.Interfaces;
using GymManagement.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagement.Api.Services.Implementations
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync(string? query)
        {
            var members = string.IsNullOrWhiteSpace(query)
                ? await _memberRepository.GetAllAsync()
                : await _memberRepository.SearchMembersAsync(query);

            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }

        public async Task<MemberDto?> GetMemberByIdAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            return member == null ? null : _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto> CreateMemberAsync(CreateMemberDto createMemberDto)
        {
            var member = _mapper.Map<Member>(createMemberDto);
            await _memberRepository.AddAsync(member);
            await _memberRepository.SaveChangesAsync();

            return _mapper.Map<MemberDto>(member);
        }

        public async Task<bool> UpdateMemberAsync(int id, CreateMemberDto updateMemberDto)
        {
            var existingMember = await _memberRepository.GetByIdAsync(id);
            if (existingMember == null)
            {
                return false;
            }

            _mapper.Map(updateMemberDto, existingMember);
            _memberRepository.Update(existingMember);
            return await _memberRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
            {
                return false;
            }

            _memberRepository.Delete(member);
            return await _memberRepository.SaveChangesAsync();
        }
    }
}
