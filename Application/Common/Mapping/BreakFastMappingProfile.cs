using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public class BreakFastMappingProfile : Profile
    {
        public BreakFastMappingProfile()
        {
            CreateMap<BreakFast, BreakFastGetDto>();
            CreateMap<BreakFastGetDto, BreakFast>();
        }
    }
}
