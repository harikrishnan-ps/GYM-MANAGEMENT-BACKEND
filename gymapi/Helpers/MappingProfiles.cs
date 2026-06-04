using AutoMapper;
using GymManagement.Api.Models;
using GymManagement.Api.DTOs.Member;
using GymManagement.Api.DTOs.GymClass;
using GymManagement.Api.DTOs.Revenue;

namespace GymManagement.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Member mappings
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<CreateMemberDto, Member>();

            // GymClass mappings
            CreateMap<GymClass, ClassDto>().ReverseMap();
            CreateMap<CreateClassDto, GymClass>();

            // Revenue mappings
            CreateMap<Revenue, RevenueDto>().ReverseMap();
            CreateMap<CreateRevenueDto, Revenue>();
        }
    }
}
