using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public class LunchMappingProfile : Profile
    {
        public LunchMappingProfile()
        {
            CreateMap<Lunch, LunchGetDto>().ReverseMap();
        }
    }
}
