using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public class ReserveMappingProfile : Profile
    {
        public ReserveMappingProfile()
        {
            CreateMap<Reserve, ReserveGetDto>().ReverseMap();
        }
    }
}
