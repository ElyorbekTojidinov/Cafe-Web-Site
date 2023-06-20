using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public class DinnerMappingProfile : Profile
    {
        public DinnerMappingProfile()
        {
            CreateMap<Dinner, DinnerGetDto>();
            CreateMap<DinnerGetDto, Dinner>();
        }
    }
}
