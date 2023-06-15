using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public class SpetialMenuMappingProfile : Profile
    {
        public SpetialMenuMappingProfile()
        {
            CreateMap<SpecialMenu, SpetialMenuGetDto>().ReverseMap();
        }
    }
}
